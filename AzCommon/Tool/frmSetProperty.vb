Imports System.Xml

Public Class frmSetProperty
    Private _blockName As String = ""
    Private _blockObj As MxDrawBlockReference
    Private _componentName As String = ""
    Private _specialityCategroy As AzSpecialityCategory = Nothing

    ''' <summary>
    ''' 当前楼层层高值,单位:mm
    ''' </summary>
    Public Property FloorHeight As Integer

    ''' <summary>
    ''' 当前分项工程所属ID值
    ''' </summary>
    Public Property KindProjId As Integer

    ''' <summary>
    ''' 当前点类型构件对象
    ''' </summary>
    Public Property AzCptObj As Component.PointCptClass

    ''' <summary>
    ''' 当前CAD块实体实体对象
    ''' </summary>
    Public Property BlockObj As MxDrawBlockReference
        Get
            Return _blockObj
        End Get
        Set(ByVal value As MxDrawBlockReference)
            Me._blockObj = value
            If value IsNot Nothing Then
                _blockName = value.GetBlockName
            End If
        End Set
    End Property

    ''' <summary>
    ''' 当前图块名称
    ''' </summary>
    Public ReadOnly Property BlockName As String
        Get
            Return _blockName
        End Get
    End Property

    ''' <summary>
    ''' 调用该窗体时的专业类别
    ''' </summary>
    ''' <value></value>
    Public Property SpecialityCategory As AzSpecialityCategory
        Get
            Return _specialityCategroy
        End Get
        Set(ByVal value As AzSpecialityCategory)
            _specialityCategroy = value
            Dim groupNames() As String = Nothing
            If value = AzSpecialityCategory.电气工程 Then
                groupNames = [Enum].GetNames(GetType(Component.ElectricalGroup))
            ElseIf value = AzSpecialityCategory.水暖工程 Then
                groupNames = [Enum].GetNames(GetType(Component.PlumbingGroup))
            Else
                groupNames = [Enum].GetNames(GetType(Component.HVACGroup))
            End If
            cboCptGroup.Items.Clear()
            For Each groupName In groupNames
                cboCptGroup.Items.Add(groupName)
            Next
            cboCptGroup.SelectedIndex = 0
        End Set
    End Property

    ''' <summary>
    ''' 指示与构件关联的名称标识文本
    ''' </summary>
    Public Property ComponentName As String
        Get
            Return _componentName
        End Get
        Set(ByVal value As String)
            _componentName = value.Trim
        End Set
    End Property

    ''' <summary>
    ''' 当前窗体关闭原因枚举
    ''' </summary>
    Public Property FormCloseReason As CloseReason

    ''' <summary>
    ''' 窗体关闭原因枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CloseReason
        ''' <summary>
        ''' 无状态
        ''' </summary>
        None
        ''' <summary>
        ''' 构件定义成功
        ''' </summary>
        DefintOK
        ''' <summary>
        ''' 名称提取
        ''' </summary>
        ExtractName
        ''' <summary>
        ''' 用户取消操作
        ''' </summary>
        Cancel
    End Enum

    Private Sub frmSetProperty_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Me.FormCloseReason = CloseReason.None
        Me.txtComponentName.Text = Me._componentName
        Dim g = Me.splCptProperty.Panel1.CreateGraphics
        g.DrawLine(Drawing.Pens.Black, 85, 128, 221, 128)
        g.Dispose()
    End Sub

    Private Sub cboCptGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCptGroup.SelectedIndexChanged
        cboCptClass.Items.Clear()
        Dim nodeCptClasses = xmlSysDataConfig.SelectSingleNode("/AzDataConfig/CptHierarchy/" & cboCptGroup.Text)
        For Each nodeCptClass As XmlElement In nodeCptClasses
            cboCptClass.Items.Add(nodeCptClass.Name)
        Next
        cboCptClass.SelectedIndex = 0
    End Sub

    Private Sub cboCptClass_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCptClass.SelectedIndexChanged
        AzCptObj = Component.InitCptClass.InitPointComponentObj(cboCptGroup.Text, cboCptClass.Text)
        prgCptProperty.SelectedObject = AzCptObj
        AzCptObj.Name = txtComponentName.Text.Trim
        Dim calcFormula = cboInstallHeight.Text.Replace("H", (Me.FloorHeight / 1000).ToString)
        If StrOperate.CalcStrValue(calcFormula, AzCptObj.InstallHeight) Then
            AzCptObj.InstallHeight = Math.Round(AzCptObj.InstallHeight, 2)
        End If
    End Sub

    Private Sub btnCollapse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCollapse.Click
        Dim g = Me.splCptProperty.Panel1.CreateGraphics
        If btnCollapse.Text = "+" Then
            btnCollapse.Text = "-"
            splCptProperty.Panel2Collapsed = False
            Me.Size = New Drawing.Size(Me.Size.Width, 425)
            g.DrawLine(Drawing.Pens.Gray, 85, 128, 221, 128)
        Else
            btnCollapse.Text = "+"
            splCptProperty.Panel2Collapsed = True
            Me.Size = New Drawing.Size(Me.Size.Width, 200)
            g.DrawLine(Drawing.Pens.Black, 85, 128, 221, 128)
        End If
        g.Dispose()
    End Sub

    Private Sub btnExtractCptName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtractCptName.Click
        Me.DialogResult = Windows.Forms.DialogResult.Retry
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim hashName = Me.BlockName.GetHashCode
        '增加图块位图到数据库
        Dim dtBlock = AzDB.GetTableBySql("Select Bitmap From Az_Block_Bitmap Where ID=" & hashName)
        If dtBlock.Rows.Count = 0 Then
            Dim bytBitmap() As Byte = Nothing
            If Graphic.GraphicParameter.GetEntityBitmap(MdlCommonDeclare.MxMainCadObj, Me.BlockObj.ObjectID, bytBitmap) Is Nothing Then
                MsgBox("CAD图块损坏", , "回路识别")
                Me.Close() : Exit Sub
            Else
                g_db.SqlParameters.Add(hashName) : g_db.SqlParameters.Add(Me.BlockName)
                g_db.SqlParameters.Add(bytBitmap) : g_db.SqlParameters.Add(Me.BlockObj.ScaleFactors.sx)
                g_db.ExecSQLPara("Insert Into Az_Block_Bitmap(ID,BlockName,Bitmap,XScale) Values(?,?,?,?)", True, "更新数据库图块位图失败。")
            End If
        End If
        dtBlock.Dispose()
        Dim mainPropertyId As Integer = Nothing
        Dim dicMainProperty = Me.AzCptObj.GetComponentProperty
        Dim keySubPropertys = Component.ComponentProperty.GetSubComponentProperty(dicMainProperty, mainPropertyId)
        Component.ComponentProperty.WriteComponentPropertyToDb(mainPropertyId, dicMainProperty, keySubPropertys(0).Key, keySubPropertys(1).Key, keySubPropertys(2).Key)
        For Each keySubProperty In keySubPropertys
            If keySubProperty.Value Is Nothing Then Continue For
            Component.ComponentProperty.WriteComponentPropertyToDb(keySubProperty.Key, keySubProperty.Value)
        Next
        g_db.SqlParameters.Add(hashName) : g_db.SqlParameters.Add(Me.BlockName)
        g_db.SqlParameters.Add(mainPropertyId) : g_db.SqlParameters.Add(Me.AzCptObj.Name)
        g_db.SqlParameters.Add(Me.AzCptObj.ComponentGroup)
        g_db.SqlParameters.Add(Me.AzCptObj.ComponentType)
        If Me.cboInstallHeight.ForeColor = Drawing.Color.Red Then
            g_db.SqlParameters.Add(Me.AzCptObj.InstallHeight)
        Else
            g_db.SqlParameters.Add(Me.cboInstallHeight.Text.Trim)
        End If
        g_db.SqlParameters.Add(KindProjId)
        g_db.ExecSQLPara("Insert Into Az_Block_Mapping Values(?,?,?,?,?,?,?,?)", True, "增加数据库块表映射记录失败。")
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cboInstallHeight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboInstallHeight.KeyPress
        If e.KeyChar = "h" Then
            e.KeyChar = CChar("H")
        ElseIf Not (IsNumeric(e.KeyChar) OrElse e.KeyChar = CChar(".") OrElse Asc(e.KeyChar) = 8 OrElse e.KeyChar = "H" OrElse _
        e.KeyChar = "+" OrElse e.KeyChar = "*" OrElse e.KeyChar = "/" OrElse e.KeyChar = "(" OrElse e.KeyChar = ")") Then
            e.KeyChar = CChar("")
        End If
    End Sub

    Private Sub cboInstallHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboInstallHeight.TextChanged
        If cboInstallHeight.Text = "" Then
            cboInstallHeight.ForeColor = Drawing.Color.Red
            If AzCptObj IsNot Nothing Then AzCptObj.InstallHeight = 0
        ElseIf IsNumeric(cboInstallHeight.Text) Then
            cboInstallHeight.ForeColor = Drawing.Color.Black
            If AzCptObj IsNot Nothing Then AzCptObj.InstallHeight = CDbl(cboInstallHeight.Text)
        ElseIf cboInstallHeight.Text = "H" Then
            cboInstallHeight.ForeColor = Drawing.Color.Black
            If AzCptObj IsNot Nothing Then AzCptObj.InstallHeight = -1
        Else
            Dim calcValue As Double = Nothing
            Dim calcFormula = cboInstallHeight.Text.Replace("H", (Me.FloorHeight / 1000).ToString)
            If StrOperate.CalcSimpleStrValue3(calcFormula, calcValue) Then
                cboInstallHeight.ForeColor = Drawing.Color.Black
                If AzCptObj IsNot Nothing Then AzCptObj.InstallHeight = Math.Round(calcValue, calcDigit)
            Else
                cboInstallHeight.ForeColor = Drawing.Color.Red
            End If
        End If
        prgCptProperty.Refresh()
    End Sub

    Private Sub prgCptProperty_PropertyValueChanged(ByVal s As System.Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles prgCptProperty.PropertyValueChanged
        Select Case e.ChangedItem.Label
            Case "安装高度(m)"
                If CDbl(e.ChangedItem.Value) < 0 Then
                    Me.cboInstallHeight.Text = "H"
                Else
                    Me.cboInstallHeight.Text = e.ChangedItem.Value.ToString
                End If
            Case "构件名称"
                Me.txtComponentName.Text = e.ChangedItem.Value.ToString
        End Select
    End Sub

    Private Sub txtComponentName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComponentName.TextChanged
        Dim strHeigth As String = ""
        txtComponentName.Text = txtComponentName.Text.Trim
        Dim dicProperty = MatchCpt(txtComponentName.Text, strHeigth)
        If dicProperty Is Nothing Then
            If Me.SpecialityCategory = AzSpecialityCategory.电气工程 Then
                cboCptGroup.Text = "电气设备"
                cboCptClass.Text = "灯具"
            ElseIf Me.SpecialityCategory = AzSpecialityCategory.水暖工程 Then
                cboCptGroup.Text = "厨卫设备"
                cboCptClass.Text = "厨具"
            Else
                cboCptGroup.Text = "通风设备"
                cboCptClass.Text = "风机"
            End If
            Me.cboInstallHeight.Text = AzCptObj.InstallHeight.ToString
        Else
            cboCptGroup.Text = dicProperty("构件分组")
            cboCptClass.Text = dicProperty("构件类型")
            AzCptObj.InitComponentProperty(dicProperty)
            If strHeigth = "H" Then
                AzCptObj.InstallHeight = -1
            Else
                Dim calcFormula = strHeigth.Replace("H", CStr(Me.FloorHeight / 1000))
                StrOperate.CalcSimpleStrValue3(calcFormula, AzCptObj.InstallHeight)
                AzCptObj.InstallHeight = Math.Round(AzCptObj.InstallHeight, calcDigit)
            End If
            cboInstallHeight.Text = strHeigth
        End If
        AzCptObj.Name = txtComponentName.Text
        prgCptProperty.Refresh()
    End Sub

    ''' <summary>
    ''' 通过构件名称匹配构件类型实例
    ''' </summary>
    ''' <param name="componentName">构件名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MatchCpt(ByVal componentName As String, ByRef installHeight As String) As Dictionary(Of String, String)
        Dim isMatch As Boolean
        Dim keyWords() As String = Nothing
        Dim dicProperty As Dictionary(Of String, String) = Nothing
        Dim nodeMatchItems = xmlSysDataConfig.SelectSingleNode("AzDataConfig/CptMatch/" & Me.SpecialityCategory.ToString)
        For Each nodeMatchItem As XmlElement In nodeMatchItems.ChildNodes
            isMatch = True
            keyWords = nodeMatchItem.GetAttribute("KewWord").Split(CChar(";"))
            For Each keyWord In keyWords '判断关键字是否匹配
                If Not componentName.Contains(keyWord) Then
                    isMatch = False
                    Exit For
                End If
            Next
            If isMatch Then
                dicProperty = New Dictionary(Of String, String)
                dicProperty.Add("构件分组", nodeMatchItem.GetAttribute("Group"))
                dicProperty.Add("构件类型", nodeMatchItem.Name)
                installHeight = nodeMatchItem.GetAttribute("InstallHeight")
                For Each nodeProperty As XmlElement In nodeMatchItem.ChildNodes
                    dicProperty.Add(nodeProperty.GetAttribute("Name"), nodeProperty.GetAttribute("Value"))
                Next
                Exit For
            End If
        Next
        Return dicProperty
    End Function

End Class