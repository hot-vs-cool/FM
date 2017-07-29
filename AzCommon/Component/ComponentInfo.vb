Imports System.Text.RegularExpressions

Namespace Component
    ''' <summary>
    ''' 构件数据结构对象，存储绘制构件所需的信息，一般用于作为其它函数或过程的参数对象
    ''' </summary>
    Public Class ComponentInfo
        Private _calcValue As Double = 1
        Private _calcFormula As String = "1"
        Private _mainHashCode As Integer = Nothing
        Private _subHashCode1 As Integer = Nothing
        Private _subHashCode2 As Integer = Nothing
        Private _subHashCode3 As Integer = Nothing
        Private _componentType As Component.AzComponentType
        ''' <summary>
        ''' 属性是否发生变化,用于指示是否需要将属性写入数据库
        ''' </summary>
        ''' <remarks></remarks>
        Private isPropertyChange As Boolean = True
        Private dicMainProperty, dicSubProperty1, dicSubProperty2, dicSubProperty3 As Dictionary(Of String, String)

        ''' <summary>
        ''' 构件实体所属楼层号
        ''' </summary>
        Public Property FloorNum As Integer

        ''' <summary>
        ''' 构件实体所属分项工程ID值
        ''' </summary>
        Public Property KindProjId As Short

        ''' <summary>
        ''' 构件所属的系统ID
        ''' </summary>
        Public Property SystemId As Integer

        ''' <summary>
        ''' 属性主哈希值
        ''' </summary>
        Public ReadOnly Property MainHashCode As Integer
            Get
                Return Me._mainHashCode
            End Get
        End Property

        ''' <summary>
        ''' 属性次哈希值1,即不包括构件“专业类型”属性的哈希值
        ''' </summary>
        Public ReadOnly Property SubHashCode1 As Integer
            Get
                Return Me._subHashCode1
            End Get
        End Property

        ''' <summary>
        ''' 属性次哈希值2,即不包括构件“专业类型”和“系统类型”的属性哈希值
        ''' </summary>
        Public ReadOnly Property SubHashCode2 As Integer
            Get
                Return Me._subHashCode2
            End Get
        End Property

        ''' <summary>
        ''' 属性次哈希值3,即不包括构件“专业类型”和“系统类型”的属性哈希值
        ''' </summary>
        Public ReadOnly Property SubHashCode3 As Integer
            Get
                Return Me._subHashCode3
            End Get
        End Property

        ''' <summary>
        ''' 指示构件是否输出工程量。"True"表示输出工程量,"False"不输出工程量
        ''' </summary>
        Public Property IsInputQuantity As Boolean = True

        ''' <summary>
        ''' 构件安装位置基点三维坐标,一般点类型构件需要此属性
        ''' </summary>
        Public Property Position As MxDrawPoint

        ''' <summary>
        ''' 是否沿地面,一般线类型构件需要此属性
        ''' </summary>
        Public Property IsLayOnCeiling As Boolean

        ''' <summary>
        ''' 显示给用户的构件实体的标注文本，如果未设置，该值默认采用构件“构件名称”属性值
        ''' </summary>
        Public Property MarkText As String

        ''' <summary>
        ''' 构件计算式允许带备注，此值如果未设置，则默认为"1"
        ''' </summary>
        Public Property CalcFormula As String
            Get
                Return Me._calcFormula
            End Get
            Set(ByVal value As String)
                If StrOperate.CalcSimpleStrValue3(value, Me._calcValue) Then
                    Me._calcFormula = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' 只读，构件计算值，其值由“CalcFormula”决定
        ''' </summary>
        Public ReadOnly Property CalcValue As Double
            Get
                Return _calcValue
            End Get
        End Property

        ''' <summary>
        ''' 只读,构件实体所属的类型
        ''' </summary>
        Public ReadOnly Property ComponentType As Component.AzComponentType
            Get
                Return _componentType
            End Get
        End Property

        ''' <summary>
        ''' 构件实体的属性集合
        ''' </summary>
        Public Property [Property] As Dictionary(Of String, String)
            Get
                Return Me.dicMainProperty
            End Get
            Set(ByVal value As Dictionary(Of String, String))
                Me.dicMainProperty = value
                If value Is Nothing Then
                    Me._mainHashCode = 0 : Me.isPropertyChange = False
                    Me._subHashCode1 = 0 : Me.dicSubProperty1 = Nothing
                    Me._subHashCode2 = 0 : Me.dicSubProperty2 = Nothing
                    Me._subHashCode3 = 0 : Me.dicSubProperty3 = Nothing
                Else
                    Dim newPropertyId As Integer = Nothing
                    Dim keySubPropertys = ComponentProperty.GetSubComponentProperty(value, newPropertyId)
                    If newPropertyId <> Me._mainHashCode Then
                        Me.isPropertyChange = True
                        Me.MarkText = value("构件名称")
                        Me._mainHashCode = newPropertyId
                        Me._subHashCode1 = keySubPropertys(0).Key : Me.dicSubProperty1 = keySubPropertys(0).Value
                        Me._subHashCode2 = keySubPropertys(1).Key : Me.dicSubProperty2 = keySubPropertys(1).Value
                        Me._subHashCode3 = keySubPropertys(2).Key : Me.dicSubProperty3 = keySubPropertys(2).Value
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' 通过构件实体对象初始化构件数据结构对象
        ''' </summary>
        ''' <param name="entObj">构件实体对象</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal entObj As MxDrawEntity)
            '楼层号:A；系统ID:B；主哈希:C；分项ID值:D
            '管线扩展数据属性名取值范围：K-O,共5个；是否竖向管线:K；是否用户自定义:L；M:管线敷设方向(默认为1代表除沿地面外的情况,0代表为沿地面)
            Me.FloorNum = CShort(entObj.GetxDataLong2("A", 0))
            Me.SystemId = CUShort(entObj.GetxDataLong2("B", 0))
            Me._mainHashCode = entObj.GetxDataLong2("C", 0)
            Me.Property = Component.ComponentProperty.GetComponentProperty(Me._mainHashCode)
            If entObj.Layer = "NoInputCpt" Then
                Me.IsInputQuantity = False
            Else
                Me.IsInputQuantity = True
            End If
            Dim dtCptItem = AzDB.GetTableBySql("Select HashCode1,HashCode2,HashCode3 From Az_Cpt_Item Where ID=" & Me._mainHashCode)
            If dtCptItem.Rows(0)(0) Is DBNull.Value Then Me._subHashCode1 = -1 Else Me._subHashCode1 = CInt(dtCptItem.Rows(0)(0))
            If dtCptItem.Rows(0)(1) Is DBNull.Value Then Me._subHashCode2 = -1 Else Me._subHashCode2 = CInt(dtCptItem.Rows(0)(1))
            If dtCptItem.Rows(0)(2) Is DBNull.Value Then Me._subHashCode3 = -1 Else Me._subHashCode3 = CInt(dtCptItem.Rows(0)(2))
            dtCptItem.Dispose()
            Dim blockObj = CType(entObj, MxDrawBlockReference)
            Me.Position = blockObj.Position
            Me.MarkText = blockObj.AttributeItem(1).TextString
            If Me.Property("构件分组") = "管线" Then
                Dim layValue As Integer
                If entObj.GetxDataLong("M", 0, layValue) Then
                    If layValue = 0 Then Me.IsLayOnCeiling = False Else Me.IsLayOnCeiling = True
                End If
            Else
                Me.IsLayOnCeiling = True
            End If
            Me._componentType = CType([Enum].Parse(GetType(Component.AzComponentType), Me.Property("构件类型")), Component.AzComponentType)
        End Sub

        ''' <summary>
        ''' 由构件所属楼层、分项工程ID、属性集初始化构件数据结构对象
        ''' </summary>
        ''' <param name="cptFloorNum">构件所属楼层</param>
        ''' <param name="kindProjId">构件所属分项工程ID值</param>
        ''' <param name="dicProperty">构件属性集</param>
        ''' <param name="systemId">可选，系统ID，默认为0</param>
        ''' <param name="position">可选，安装位置坐标点，一般点类型构件需要此属性，单位:mm</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal cptFloorNum As Integer, ByVal kindProjId As Short, ByVal dicProperty As Dictionary(Of String, String), _
    Optional ByVal systemId As Integer = 0, Optional ByVal position As MxDrawPoint = Nothing)
            Me.FloorNum = cptFloorNum
            Me.KindProjId = kindProjId
            Me.SystemId = systemId
            Me.Property = dicProperty
            Me.IsLayOnCeiling = True
            Dim strProperty As String = ""
            If dicProperty IsNot Nothing Then
                Me.MarkText = dicProperty("构件名称")
                Me._componentType = CType([Enum].Parse(GetType(Component.AzComponentType), dicProperty("构件类型")), Component.AzComponentType)
            End If
            Me.Position = New MxDrawPoint
            If position IsNot Nothing Then
                Me.Position.x = position.x
                Me.Position.y = position.y
                Me.Position.z = position.z
            End If
        End Sub

        ''' <summary>
        ''' 将通用构件扩展数据写入构件实体,并更新属性映射记录。写入成功则返回"True",否则返回"False"
        ''' </summary>
        ''' <param name="componentObj">待写入属性的实体对象</param>
        ''' <param name="originHandle">可选,底图实体对象句柄,默认为"0",即无底图实体句柄</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function WriteCommonXData(ByRef componentObj As MxDrawEntity, Optional ByVal originHandle As String = "0") As Boolean
            '通用扩展数据属性名取值范围为：A-J,共10个
            '楼层号:A；系统ID:B；主哈希:C；分项ID值:D；底图句柄:E；构件分组:F；构件类型：G
            Dim isDelete = componentObj.DeleteXData("")
            componentObj.SetxDataLong("A", 0, Me.FloorNum)
            componentObj.SetxDataLong("B", 0, Me.SystemId)
            componentObj.SetxDataLong("C", 0, Me.MainHashCode)
            componentObj.SetxDataLong("D", 0, Me.KindProjId)
            componentObj.SetxDataString("E", 0, originHandle)
            componentObj.SetxDataString("F", 0, Me.Property("构件分组"))
            componentObj.SetxDataString("G", 0, Me.Property("构件类型"))
            If Me.isPropertyChange Then
                Me.isPropertyChange = False
                Component.ComponentProperty.WriteComponentPropertyToDb(Me._mainHashCode, dicMainProperty, Me._subHashCode1, Me._subHashCode2, Me._subHashCode3)
                If dicSubProperty1 IsNot Nothing Then Component.ComponentProperty.WriteComponentPropertyToDb(Me._subHashCode1, Me.dicSubProperty1)
                If dicSubProperty2 IsNot Nothing Then Component.ComponentProperty.WriteComponentPropertyToDb(Me._subHashCode2, Me.dicSubProperty2)
                If dicSubProperty3 IsNot Nothing Then Component.ComponentProperty.WriteComponentPropertyToDb(Me._subHashCode3, Me.dicSubProperty3)
            End If
            Return True
        End Function

        ''' <summary>
        ''' 写入构件管线构件扩展数据
        ''' </summary>
        ''' <param name="componentObj">待写入的构件实体对象</param>
        ''' <param name="isStand">是否属性管线</param>
        ''' <param name="isUserDefine">是否用户自定义</param>
        ''' <param name="isOnGround">可选，管线走向是否沿地面，默认不沿地面敷设</param>
        ''' <remarks></remarks>
        Public Sub WriteGxXData(ByRef componentObj As MxDrawEntity, ByVal isStand As Boolean, ByVal isUserDefine As Boolean, Optional ByVal isOnGround As Boolean = False)
            '管线扩展数据属性名取值范围：K-O,共5个
            '是否竖向管线:K；是否用户自定义:L；M:管线敷设方向(默认为1代表除沿地面外的情况,0代表为沿地面)
            If isStand Then
                componentObj.SetxDataLong("K", 0, 1)
            Else
                componentObj.SetxDataLong("K", 0, 0)
            End If
            If isUserDefine Then
                componentObj.SetxDataLong("L", 0, 1)
            Else
                componentObj.SetxDataLong("L", 0, 0)
            End If
            If isOnGround Then
                componentObj.SetxDataLong("M", 0, 1)
            Else
                componentObj.SetxDataLong("M", 0, 0)
            End If
        End Sub

    End Class
End Namespace
