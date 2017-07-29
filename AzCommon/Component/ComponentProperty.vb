Imports System.Xml
Imports System.Text.RegularExpressions

Namespace Component

    ''' <summary>
    ''' 构件属性操作类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ComponentProperty

        ''' <summary>
        ''' 获取安装工程所有构件类型目录，并按所属构件分组分类
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetCptClassList() As Dictionary(Of String, List(Of String))
            Dim groupName As String = ""
            Dim dicCptClass As New Dictionary(Of String, List(Of String))
            Dim nodeMain = xmlSysDataConfig.SelectSingleNode("/AzDataConfig/CptHierarchy")
            For Each nodeGroup As XmlElement In nodeMain.ChildNodes
                groupName = nodeGroup.Name
                Dim lisClass As New List(Of String)
                dicCptClass.Add(groupName, lisClass)
                For Each nodeClass As XmlElement In nodeGroup.ChildNodes
                    dicCptClass(groupName).Add(nodeClass.Name)
                Next
            Next
            Return dicCptClass
        End Function

        ''' <summary>
        ''' 获取指定分组的构件类型目录
        ''' </summary>
        ''' <param name="componentGroup">待获取的分组类型</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetCptClassListByGroup(ByVal componentGroup As Component.AzComponentGroup) As List(Of String)
            Dim lisCptClass As New List(Of String)
            Dim nodeGroup = xmlSysDataConfig.SelectSingleNode("/AzDataConfig/CptHierarchy/" & componentGroup.ToString)
            For Each nodeClass As XmlElement In nodeGroup.ChildNodes
                lisCptClass.Add(nodeClass.Name)
            Next
            Return lisCptClass
        End Function

        ''' <summary>
        ''' 获取指定类型的构件默认属性集
        ''' </summary>
        ''' <param name="componentType">待获取属性的构件类型</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetDefautCptProperty(ByVal componentType As Component.AzComponentType) As Dictionary(Of String, String)
            Dim dicProperty As New Dictionary(Of String, String)
            Dim nodeMain = xmlSysDataConfig.SelectSingleNode("/AzDataConfig/CptProperty/" & componentType.ToString)
            For Each nodeProperty As XmlElement In nodeMain.ChildNodes
                dicProperty.Add(nodeProperty.GetAttribute("Name"), nodeProperty.GetAttribute("Value"))
            Next
            Return dicProperty
        End Function

        ''' <summary>
        ''' 获取指定类型的构件默认属性集,如果不存在指定类型的构件属性则返回Nothing
        ''' </summary>
        ''' <param name="componentType">待获取属性的构件类型</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetDefautCptProperty(ByVal componentType As String) As Dictionary(Of String, String)
            Dim nodeMain = xmlSysDataConfig.SelectSingleNode("/AzDataConfig/CptProperty/" & componentType)
            If nodeMain Is Nothing Then Return Nothing
            Dim dicProperty As New Dictionary(Of String, String)
            For Each nodeProperty As XmlElement In nodeMain.ChildNodes
                dicProperty.Add(nodeProperty.GetAttribute("Name"), nodeProperty.GetAttribute("Value"))
            Next
            Return dicProperty
        End Function

        ''' <summary>
        ''' 获取制定记录ID的构件属性到集合，如果数据库不存在构件属性则返回Nothing
        ''' </summary>
        ''' <param name="recPropertyId">与构件关联的属性记录ID值</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetComponentProperty(ByVal recPropertyId As Integer) As Dictionary(Of String, String)
            Dim dtCptProperty = AzDB.GetTableBySql("Select * From Az_Cpt_Property Where Pid=" & recPropertyId & " Order By ID")
            If dtCptProperty.Rows.Count = 0 Then
                dtCptProperty.Dispose()
                Return Nothing
            Else
                Dim dicProperty As New Dictionary(Of String, String)
                For Each dr As DataRow In dtCptProperty.Rows
                    dicProperty.Add(dr("Name").ToString, dr("Value").ToString)
                Next
                dtCptProperty.Dispose()
                Return dicProperty
            End If
        End Function

        ''' <summary>
        ''' 获取当前工程项目所有已实例化构件属性集合
        ''' </summary>
        ''' <param name="dicSubIdMapping">返回的主哈希和次哈希映射集合,不包含不存在次哈希的构件哈希映射,如果次哈希不存在则以0占位</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAllComponentProperty(Optional ByRef dicSubIdMapping As Dictionary(Of Integer, Integer()) = Nothing) As Dictionary(Of Integer, Dictionary(Of String, String))
            Dim dtCptItem = AzDB.GetTable("Az_Cpt_Item")
            Dim dicAllProperty As New Dictionary(Of Integer, Dictionary(Of String, String))
            dicSubIdMapping = New Dictionary(Of Integer, Integer())
            For Each dr As DataRow In dtCptItem.Rows
                dicAllProperty.Add(CInt(dr(0)), New Dictionary(Of String, String))
                Dim subHashs(2) As Integer
                If dr("HashCode1") IsNot DBNull.Value Then subHashs(0) = CInt(dr("HashCode1"))
                If dr("HashCode2") IsNot DBNull.Value Then subHashs(1) = CInt(dr("HashCode2"))
                If dr("HashCode3") IsNot DBNull.Value Then subHashs(2) = CInt(dr("HashCode3"))
                For Each subHash In subHashs
                    If subHash <> 0 Then
                        dicSubIdMapping.Add(CInt(dr("ID")), subHashs)
                        Exit For
                    End If
                Next
            Next
            Dim dtCptProperty = AzDB.GetTable("Az_Cpt_Property", "Pid", "Name", "Value")
            For Each dr As DataRow In dtCptProperty.Rows
                dicAllProperty(CInt(dr(0))).Add(dr(1).ToString, dr(2).ToString)
            Next
            dtCptItem.Dispose() : dtCptProperty.Dispose()
            Return dicAllProperty
        End Function

        ''' <summary>
        ''' 获取构件子构件属性集和对应的哈希值,返回值共3个,如果对应位置处不存在子构件则为Nothing
        ''' </summary>
        ''' <param name="dicMainProperty">主构件属性集</param>
        ''' <param name="mainHashCode">可选,返回的主构件属性哈希值</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSubComponentProperty(ByVal dicMainProperty As Dictionary(Of String, String), Optional ByRef mainHashCode As Integer = 0) As KeyValuePair(Of Integer, Dictionary(Of String, String))()
            '计算主属性哈希值
            Dim strProperty As String = ""
            For Each mainProperty In dicMainProperty
                strProperty &= mainProperty.Key & "=" & mainProperty.Value & ";"
            Next
            mainHashCode = strProperty.GetHashCode
            '获取子属性和对应的哈希值
            Dim keySubPropertys(2) As KeyValuePair(Of Integer, Dictionary(Of String, String))
            Dim dicSubProperty1, dicSubProperty2, dicSubProperty3 As Dictionary(Of String, String)
            Select Case dicMainProperty("构件类型")
                Case "配线配管"
                    '配管属性
                    dicSubProperty1 = Component.ComponentProperty.GetDefautCptProperty(AzComponentType.配管)
                    dicSubProperty1("构件名称") = dicMainProperty("配管规格") & "(" & dicMainProperty("敷设类别") & ")"
                    dicSubProperty1("专业类型") = dicMainProperty("专业类型")
                    dicSubProperty1("系统类型") = dicMainProperty("系统类型")
                    Dim matchItem = Regex.Match(dicMainProperty("配管规格"), "^(?<A>.+\D)(?<B>\d+)$")
                    If matchItem.Success Then
                        dicSubProperty1("材质") = matchItem.Groups("A").Value.Replace("DN=", "").Trim
                        dicSubProperty1("公称直径(mm)") = matchItem.Groups("B").Value
                    Else
                        dicSubProperty1("材质") = dicMainProperty("配管规格")
                        matchItem = Regex.Match(dicMainProperty("配管规格"), "(?<A>\d+)")
                        If matchItem.Success Then
                            dicSubProperty1("公称直径(mm)") = matchItem.Groups("A").Value
                        Else
                            dicSubProperty1("公称直径(mm)") = "0"
                        End If
                    End If
                    dicSubProperty1("敷设类别") = dicMainProperty("敷设类别")
                    strProperty = ""
                    For Each pipeProperty In dicSubProperty1
                        strProperty &= pipeProperty.Key & "=" & pipeProperty.Value & ";"
                    Next
                    keySubPropertys(0) = New KeyValuePair(Of Integer, Dictionary(Of String, String))(strProperty.GetHashCode, dicSubProperty1)
                    '配线规格1属性
                    dicSubProperty2 = Component.ComponentProperty.GetDefautCptProperty(AzComponentType.配线)
                    dicSubProperty2("构件名称") = dicMainProperty("配线规格1")
                    dicSubProperty2("构件类型") = "配线"
                    dicSubProperty2("专业类型") = dicMainProperty("专业类型")
                    dicSubProperty2("系统类型") = dicMainProperty("系统类型")
                    Dim keyWiringInfo = CxAzFunc.Component.Gx.配线.GetWiringModelInfo(dicMainProperty("配线规格1"), Nothing)
                    dicSubProperty2("材质") = keyWiringInfo.Key
                    If keyWiringInfo.Value = "" Then
                        dicSubProperty2("截面面积(㎜²)") = "0"
                    ElseIf IsNumeric(keyWiringInfo.Value) Then
                        dicSubProperty2("截面面积(㎜²)") = keyWiringInfo.Value
                    Else
                        matchItem = Regex.Match(keyWiringInfo.Value, "\d+\D(?<A>\d+(\.\d+)?)\+\d+\D(?<B>\d+(\.\d+)?)$")
                        If matchItem.Success Then
                            If CDbl(matchItem.Groups("A").Value) > CDbl(matchItem.Groups("B").Value) Then
                                dicSubProperty2("截面面积(㎜²)") = matchItem.Groups("A").Value
                            Else
                                dicSubProperty2("截面面积(㎜²)") = matchItem.Groups("B").Value
                            End If
                        Else
                            matchItem = Regex.Match(keyWiringInfo.Value, "\d+\D(?<A>\d+(\.\d+)?)$")
                            If matchItem.Success Then
                                dicSubProperty2("截面面积(㎜²)") = matchItem.Groups("A").Value
                            Else
                                dicSubProperty2("截面面积(㎜²)") = "0"
                            End If
                        End If
                    End If
                    dicSubProperty2("配线类型") = dicMainProperty("配线类型")
                    strProperty = ""
                    For Each pipeProperty In dicSubProperty2
                        strProperty &= pipeProperty.Key & "=" & pipeProperty.Value & ";"
                    Next
                    keySubPropertys(1) = New KeyValuePair(Of Integer, Dictionary(Of String, String))(strProperty.GetHashCode, dicSubProperty2)
                    '配线规格2属性
                    If dicMainProperty("配线规格2") <> "" AndAlso dicMainProperty("配线根数2") <> "0" Then
                        dicSubProperty3 = Component.ComponentProperty.GetDefautCptProperty(AzComponentType.配线)
                        dicSubProperty3("构件名称") = dicMainProperty("配线规格2")
                        dicSubProperty3("构件类型") = "配线"
                        dicSubProperty3("专业类型") = dicMainProperty("专业类型")
                        dicSubProperty3("系统类型") = dicMainProperty("系统类型")
                        keyWiringInfo = CxAzFunc.Component.Gx.配线.GetWiringModelInfo(dicMainProperty("配线规格2"), Nothing)
                        dicSubProperty3("材质") = keyWiringInfo.Key
                        If keyWiringInfo.Value = "" Then
                            dicSubProperty3("截面面积(㎜²)") = "0"
                        ElseIf IsNumeric(keyWiringInfo.Value) Then
                            dicSubProperty3("截面面积(㎜²)") = keyWiringInfo.Value
                        Else
                            matchItem = Regex.Match(keyWiringInfo.Value, "\d+\D(?<A>\d+(\.\d+)?)\+\d+\D(?<B>\d+(\.\d+)?)$")
                            If matchItem.Success Then
                                If CDbl(matchItem.Groups("A").Value) > CDbl(matchItem.Groups("B").Value) Then
                                    dicSubProperty3("截面面积(㎜²)") = matchItem.Groups("A").Value
                                Else
                                    dicSubProperty3("截面面积(㎜²)") = matchItem.Groups("B").Value
                                End If
                            Else
                                matchItem = Regex.Match(keyWiringInfo.Value, "\d+\D(?<A>\d+(\.\d+)?)$")
                                If matchItem.Success Then
                                    dicSubProperty3("截面面积(㎜²)") = matchItem.Groups("A").Value
                                Else
                                    dicSubProperty3("截面面积(㎜²)") = "0"
                                End If
                            End If
                        End If
                        dicSubProperty3("配线类型") = dicMainProperty("配线类型")
                        strProperty = ""
                        For Each pipeProperty In dicSubProperty3
                            strProperty &= pipeProperty.Key & "=" & pipeProperty.Value & ";"
                        Next
                        keySubPropertys(2) = New KeyValuePair(Of Integer, Dictionary(Of String, String))(strProperty.GetHashCode, dicSubProperty3)
                    End If
                Case "桥架配线"
                    '配线规格1属性
                    dicSubProperty2 = Component.ComponentProperty.GetDefautCptProperty(AzComponentType.配线)
                    dicSubProperty2("构件名称") = dicMainProperty("配线规格1") & "(CT)"
                    dicSubProperty2("构件类型") = "配线"
                    dicSubProperty2("专业类型") = dicMainProperty("专业类型")
                    dicSubProperty2("系统类型") = dicMainProperty("系统类型")
                    Dim keyWiringInfo = CxAzFunc.Component.Gx.配线.GetWiringModelInfo(dicMainProperty("配线规格1"), Nothing)
                    dicSubProperty2("材质") = keyWiringInfo.Key
                    If keyWiringInfo.Value = "" Then
                        dicSubProperty2("截面面积(㎜²)") = "0"
                    ElseIf IsNumeric(keyWiringInfo.Value) Then
                        dicSubProperty2("截面面积(㎜²)") = keyWiringInfo.Value
                    Else
                        Dim matchItem = Regex.Match(keyWiringInfo.Value, "\d+\D(?<A>\d+(\.\d+)?)\+\d+\D(?<B>\d+(\.\d+)?)$")
                        If matchItem.Success Then
                            If CDbl(matchItem.Groups("A").Value) > CDbl(matchItem.Groups("B").Value) Then
                                dicSubProperty2("截面面积(㎜²)") = matchItem.Groups("A").Value
                            Else
                                dicSubProperty2("截面面积(㎜²)") = matchItem.Groups("B").Value
                            End If
                        Else
                            matchItem = Regex.Match(keyWiringInfo.Value, "\d+\D(?<A>\d+(\.\d+)?)$")
                            If matchItem.Success Then
                                dicSubProperty2("截面面积(㎜²)") = matchItem.Groups("A").Value
                            Else
                                dicSubProperty2("截面面积(㎜²)") = "0"
                            End If
                        End If
                    End If
                    dicSubProperty2("配线类型") = dicMainProperty("配线类型")
                    strProperty = ""
                    For Each pipeProperty In dicSubProperty2
                        strProperty &= pipeProperty.Key & "=" & pipeProperty.Value & ";"
                    Next
                    keySubPropertys(1) = New KeyValuePair(Of Integer, Dictionary(Of String, String))(strProperty.GetHashCode, dicSubProperty2)
                    '配线规格2属性
                    If dicMainProperty("配线规格2") <> "" AndAlso dicMainProperty("配线根数2") <> "0" Then
                        dicSubProperty3 = Component.ComponentProperty.GetDefautCptProperty(AzComponentType.配线)
                        dicSubProperty3("构件名称") = dicMainProperty("配线规格2") & "(CT)"
                        dicSubProperty3("构件类型") = "配线"
                        dicSubProperty3("专业类型") = dicMainProperty("专业类型")
                        dicSubProperty3("系统类型") = dicMainProperty("系统类型")
                        keyWiringInfo = CxAzFunc.Component.Gx.配线.GetWiringModelInfo(dicMainProperty("配线规格2"), Nothing)
                        dicSubProperty3("材质") = keyWiringInfo.Key
                        If keyWiringInfo.Value = "" Then
                            dicSubProperty3("截面面积(㎜²)") = "0"
                        ElseIf IsNumeric(keyWiringInfo.Value) Then
                            dicSubProperty3("截面面积(㎜²)") = keyWiringInfo.Value
                        Else
                            Dim matchItem = Regex.Match(keyWiringInfo.Value, "\d+\D(?<A>\d+(\.\d+)?)\+\d+\D(?<B>\d+(\.\d+)?)$")
                            If matchItem.Success Then
                                If CDbl(matchItem.Groups("A").Value) > CDbl(matchItem.Groups("B").Value) Then
                                    dicSubProperty3("截面面积(㎜²)") = matchItem.Groups("A").Value
                                Else
                                    dicSubProperty3("截面面积(㎜²)") = matchItem.Groups("B").Value
                                End If
                            Else
                                matchItem = Regex.Match(keyWiringInfo.Value, "\d+\D(?<A>\d+(\.\d+)?)$")
                                If matchItem.Success Then
                                    dicSubProperty3("截面面积(㎜²)") = matchItem.Groups("A").Value
                                Else
                                    dicSubProperty3("截面面积(㎜²)") = "0"
                                End If
                            End If
                        End If
                        dicSubProperty3("配线类型") = dicMainProperty("配线类型")
                        strProperty = ""
                        For Each pipeProperty In dicSubProperty3
                            strProperty &= pipeProperty.Key & "=" & pipeProperty.Value & ";"
                        Next
                        keySubPropertys(2) = New KeyValuePair(Of Integer, Dictionary(Of String, String))(strProperty.GetHashCode, dicSubProperty3)
                    End If
                Case "冷媒管"
                    dicSubProperty1 = Component.ComponentProperty.GetDefautCptProperty(AzComponentType.管道)
                    dicSubProperty1("构件名称") = dicMainProperty("材质") & " φ" & dicMainProperty("气管管径(mm)")
                    dicSubProperty1("专业类型") = dicMainProperty("专业类型")
                    dicSubProperty1("系统类型") = dicMainProperty("系统类型")
                    dicSubProperty1("材质") = dicMainProperty("材质")
                    dicSubProperty1("保温厚度(mm)") = dicMainProperty("气管保温厚度(mm)")
                    dicSubProperty1("公称直径(mm)") = dicMainProperty("气管管径(mm)")
                    dicSubProperty1("公称外径(mm)") = dicMainProperty("气管管径(mm)")
                    dicSubProperty1("连接方式") = "螺纹连接"
                    dicSubProperty1("安装位置") = "室内"
                    dicSubProperty1("是否刷漆") = "否"
                    dicSubProperty1("是否吊架") = dicMainProperty("是否吊架")
                    dicSubProperty1("是否保温") = dicMainProperty("是否保温")
                    strProperty = ""
                    dicSubProperty2 = New Dictionary(Of String, String)
                    For Each keyProperty In dicSubProperty1
                        dicSubProperty2.Add(keyProperty.Key, keyProperty.Value)
                        strProperty &= keyProperty.Key & "=" & keyProperty.Value & ";"
                    Next
                    keySubPropertys(0) = New KeyValuePair(Of Integer, Dictionary(Of String, String))(strProperty.GetHashCode, dicSubProperty1)
                    dicSubProperty2("构件名称") = dicMainProperty("材质") & " φ" & dicMainProperty("液管管径(mm)")
                    dicSubProperty2("公称直径(mm)") = dicMainProperty("液管管径(mm)")
                    dicSubProperty2("公称外径(mm)") = dicMainProperty("液管管径(mm)")
                    dicSubProperty2("保温厚度(mm)") = dicMainProperty("液管保温厚度(mm)")
                    strProperty = ""
                    For Each keyProperty In dicSubProperty2
                        strProperty &= keyProperty.Key & "=" & keyProperty.Value & ";"
                    Next
                    keySubPropertys(1) = New KeyValuePair(Of Integer, Dictionary(Of String, String))(strProperty.GetHashCode, dicSubProperty2)
            End Select
            Return keySubPropertys
        End Function

        ''' <summary>
        ''' 写入构件属性到数据库
        ''' </summary>
        ''' <param name="recPropertyId">属性集合对应的ID,即哈希值</param>
        ''' <param name="dicProperty">待写入的属性集合</param>
        ''' <param name="subHash1">可选,子哈希值1,默认为0即不存在子哈希</param>
        ''' <param name="subHash2">可选,子哈希值2,默认为0即不存在子哈希</param>
        ''' <param name="subHash3">可选,子哈希值3,默认为0即不存在子哈希</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function WriteComponentPropertyToDb(ByVal recPropertyId As Integer, ByVal dicProperty As Dictionary(Of String, String), _
     Optional ByVal subHash1 As Integer = 0, Optional ByVal subHash2 As Integer = 0, Optional ByVal subHash3 As Integer = 0) As Boolean
            Dim dtCptItem = AzDB.GetTableBySql("Select ID From Az_Cpt_Item Where ID=" & recPropertyId)
            If dtCptItem.Rows.Count > 0 Then
                dtCptItem.Dispose()
                Return True
            Else
                dtCptItem.Dispose()
                g_db.SqlParameters.Add(recPropertyId)
                g_db.SqlParameters.Add([Enum].Parse(GetType(Component.AzComponentType), dicProperty("构件类型")))
                g_db.SqlParameters.Add(dicProperty("构件名称"))
                If subHash1 = 0 Then g_db.SqlParameters.Add(DBNull.Value) Else g_db.SqlParameters.Add(subHash1)
                If subHash2 = 0 Then g_db.SqlParameters.Add(DBNull.Value) Else g_db.SqlParameters.Add(subHash2)
                If subHash3 = 0 Then g_db.SqlParameters.Add(DBNull.Value) Else g_db.SqlParameters.Add(subHash3)
                If g_db.ExecSQLPara("Insert Into Az_Cpt_Item(ID,TypeId,CptName,HashCode1,HashCode2,HashCode3) Values(?,?,?,?,?,?)", True, "增加数据库构件实例失败。") Then
                    For Each keyProperty In dicProperty
                        g_db.SqlParameters.Add(recPropertyId)
                        g_db.SqlParameters.Add(keyProperty.Key)
                        g_db.SqlParameters.Add(keyProperty.Value)
                        If Not g_db.ExecSQLPara("Insert Into Az_Cpt_Property(Pid,Name,Value) Values(?,?,?)", True, "增加构件属性到数据库失败。") Then
                            g_db.ExecSQLPara("Delete From Az_Cpt_Item Where ID=" & recPropertyId, True, "数据回滚失败。")
                            g_db.ExecSQLPara("Delete From Az_Cpt_Property Where Pid=" & recPropertyId, True, "数据回滚失败。")
                            Return False
                        End If
                    Next
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

    End Class
End Namespace
