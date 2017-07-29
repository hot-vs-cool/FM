Imports System.ComponentModel

Namespace Component

    ''' <summary>
    ''' 安装构件组别枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum AzComponentGroup
        管线 = 1
        电气设备 = 2
        电气附件 = 3
        消防报警设备 = 4
        防雷接地设备 = 5
        厨卫设备 = 6
        采暖设备 = 7
        消防水卫 = 8
        给排水点 = 9
        管道附件 = 10
        通风设备 = 11
        风管附件 = 12
    End Enum

    ''' <summary>
    ''' 安装全部构件类型ID枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum AzComponentType
        '管线
        配线 = 1
        配管 = 2
        桥架 = 3
        金属软管 = 4
        配线配管 = 5
        桥架配线 = 6
        避雷网 = 7
        均压环 = 8
        接地母线 = 9
        避雷引下线 = 10
        防雷接地线 = 11
        风管 = 12
        冷媒管 = 13
        风机盘管 = 14
        管道 = 15
        '电气设备
        开关 = 16
        插座 = 17
        灯具 = 18
        配电箱 = 19
        设备仪表 = 20
        '电气附件
        套管 = 21
        接线盒 = 22
        电缆头 = 23
        '消防报警设备
        报警设备 = 24
        控制模块 = 25
        '防雷接地设备
        避雷针 = 26
        接地极 = 27
        焊接点 = 28
        等电位箱 = 29
        等电位连接 = 30
        接地跨接线 = 31
        避雷网接地测试点 = 32
        '厨卫设备
        厨具 = 33
        小便器 = 34
        大便器 = 35
        盥洗盆 = 36
        淋浴器 = 37
        '采暖设备
        散热器 = 38
        '消防水卫
        水泵 = 39
        水箱 = 40
        灭火器 = 41
        喷淋头 = 42
        消火栓 = 43
        '给排水点
        水表 = 44
        地漏 = 45
        水龙头 = 46
        清扫口 = 47
        污水井 = 48
        '管道附件
        管道阀门 = 49
        管道配件 = 50
        管道套管 = 51
        仪表装置 = 52
        沟槽连接件 = 53
        管道连接件 = 54
        '通风设备
        风机 = 55
        风口 = 56
        风帽 = 57
        风罩 = 58
        静压箱 = 59
        消声器 = 60
        空调机组 = 61
        空调设备 = 62
        '风管附件
        风管阀门 = 63
        风管法兰 = 64
        风管软接头 = 65
    End Enum

    ''' <summary>
    ''' 电气工程所属的构件类型ID枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ElecComponentType
        '管线
        配线 = 1
        配管 = 2
        桥架 = 3
        金属软管 = 4
        配线配管 = 5
        桥架配线 = 6
        避雷网 = 7
        均压环 = 8
        接地母线 = 9
        避雷引下线 = 10
        防雷接地线 = 11
        '电气设备
        开关 = 16
        插座 = 17
        灯具 = 18
        配电箱 = 19
        设备仪表 = 20
        '电气附件
        套管 = 21
        接线盒 = 22
        电缆头 = 23
        '消防报警设备
        报警设备 = 24
        控制模块 = 25
        '防雷接地设备
        避雷针 = 26
        接地极 = 27
        焊接点 = 28
        等电位箱 = 29
        等电位连接 = 30
        接地跨接线 = 31
        避雷网接地测试点 = 32
    End Enum

    ''' <summary>
    ''' 水暖工程所属构件类型ID枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum PlumbingComponentType
        '管线
        管道 = 15
        '厨卫设备
        厨具 = 33
        小便器 = 34
        大便器 = 35
        盥洗盆 = 36
        淋浴器 = 37
        '采暖设备
        散热器 = 38
        '消防水卫
        水泵 = 39
        水箱 = 40
        灭火器 = 41
        喷淋头 = 42
        消火栓 = 43
        '给排水点
        水表 = 44
        地漏 = 45
        水龙头 = 46
        清扫口 = 47
        污水井 = 48
        '管道附件
        管道阀门 = 49
        管道配件 = 50
        管道套管 = 51
        仪表装置 = 52
        沟槽连接件 = 53
        管道连接件 = 54
    End Enum

    ''' <summary>
    ''' 通风空调所属构件类型ID枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum HVACComponentType
        '管线
        风管 = 12
        冷媒管 = 13
        风机盘管 = 14
        '通风设备
        风机 = 55
        风口 = 56
        风帽 = 57
        风罩 = 58
        静压箱 = 59
        消声器 = 60
        空调机组 = 61
        空调设备 = 62
        '风管附件
        风管阀门 = 63
        风管法兰 = 64
        风管软接头 = 65
    End Enum

    ''' <summary>
    ''' 电气工程构件组别枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ElectricalGroup
        电气设备 = 2
        电气附件 = 3
        消防报警设备 = 4
        防雷接地设备 = 5
    End Enum

    ''' <summary>
    ''' 水暖工程构件组别枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum PlumbingGroup
        厨卫设备 = 6
        采暖设备 = 7
        消防水卫 = 8
        给排水点 = 9
        管道附件 = 10
    End Enum

    ''' <summary>
    ''' 通风空调工程构件组别枚举
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum HVACGroup
        通风设备 = 11
        风管附件 = 12
    End Enum

    ''' <summary>
    ''' 安装专业构件基类
    ''' </summary>
    ''' <remarks>其它安装构件类全部由其派生</remarks>
    Public MustInherit Class BaseComponent
        Private _ComponentName As String = ""
        Private _ComponentGroup As String = ""
        Private _ComponentType As String = ""

        ''' <summary>
        ''' 根据构件组别、构件类型初始化安装构件对象基类
        ''' </summary>
        ''' <param name="componentGroup">构件组别</param>
        ''' <param name="componentType">构件类型</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal componentGroup As AzComponentGroup, ByVal componentType As AzComponentType)
            Me._ComponentType = componentType.ToString
            Me._ComponentGroup = componentGroup.ToString
        End Sub

        ''' <summary>
        ''' 与构件关联的名称标识字符串
        ''' </summary>
        <Category("通用设置"), DisplayName("构件名称"), Description("与构件关联的名称标识文本")>
        Public Overridable Property Name As String
            Get
                Return Me._ComponentName
            End Get
            Set(ByVal value As String)
                Me._ComponentName = value.Trim
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统组别
        ''' </summary>
        <Category("通用设置"), DisplayName("构件分组"), Description("构件所属的系统组别，由系统默认定义")>
        Public Overridable ReadOnly Property ComponentGroup As String
            Get
                Return Me._ComponentGroup
            End Get
        End Property

        ''' <summary>
        ''' 构件所属类型
        ''' </summary>
        <Category("通用设置"), DisplayName("构件类型"), Description("指示构件所属的类型")>
        Public Overridable ReadOnly Property ComponentType As String
            Get
                Return _ComponentType
            End Get
        End Property

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Property SpecialityType As String = ""

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Property SystemType As String = ""

        ''' <summary>
        ''' 获取安装构件对象字符型属性集,该方法在派生类里面必须被重写
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride Function GetComponentProperty() As Dictionary(Of String, String)

        ''' <summary>
        ''' 初始化安装构件对象属性,该方法在派生类里面必须被重写
        ''' </summary>
        ''' <param name="dicProperty">用于初始化构件对象的字符型属性集</param>
        ''' <remarks></remarks>
        Public MustOverride Sub InitComponentProperty(ByVal dicProperty As Dictionary(Of String, String))

    End Class

    ''' <summary>
    ''' 安装专业点类型构件基础类，由安装专业构件基类派生
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class PointCptClass
        Inherits BaseComponent
        Private _installHeight As Double = 0

        ''' <summary>
        ''' 根据构件组别、构件类型、和安装高度初始化对象
        ''' </summary>
        ''' <param name="componentGroup">构件组别</param>
        ''' <param name="componentType">构件类型</param>
        ''' <param name="installHeight">构件安装高度,单位mm</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal componentGroup As AzComponentGroup, ByVal componentType As AzComponentType, ByVal installHeight As Double)
            MyBase.New(componentGroup, componentType)
            _InstallHeight = installHeight
        End Sub

        ''' <summary>
        ''' 指示构件安装高度值,值为-1代表同层高，单位:m
        ''' </summary>
        <Category("位置"), DisplayName("安装高度(m)"), Description("构件相对其所属的楼层的楼地面高度值,值为-1代表同层高，单位:m")>
        Public Property InstallHeight As Double
            Get
                Return _installHeight
            End Get
            Set(ByVal value As Double)
                If value < 0 Then
                    value = -1
                ElseIf Math.Abs(value + 1) < 0.0001 Then
                    value = 0
                End If
                If Math.Abs(Me._installHeight - value) > 0.001 Then
                    Me._installHeight = Math.Round(value, 2)
                End If
            End Set
        End Property

        Public MustOverride Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))

        Public MustOverride Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)

    End Class

    ''' <summary>
    ''' 安装专业线性构件基类，由安装专业构件基类派生
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class LineCptClass
        Inherits BaseComponent
        Private _StartHeight, _EndHeight As Integer

        ''' <summary>
        ''' 根据构件类型、起点高、终点高初始化安装线型构件对象属性
        ''' </summary>
        ''' <param name="componentType">构件类型</param>
        ''' <param name="startHeight">起点高</param>
        ''' <param name="endHeight">终点高</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal componentType As AzComponentType, ByVal startHeight As Integer, ByVal endHeight As Integer)
            MyBase.New(AzComponentGroup.管线, componentType)
            _StartHeight = startHeight
            _EndHeight = endHeight
        End Sub

        ''' <summary>
        ''' 线型构件起始点安装高度,单位:mm
        ''' </summary>
        <Category("位置"), DisplayName("起点高(mm)"), Description("线性构件的起点相对其所属的楼层的楼面高度值,值为-1代表同层高，单位:mm")>
        Public ReadOnly Property StartHeight As Integer
            Get
                Return _StartHeight
            End Get
        End Property

        ''' <summary>
        ''' 线型构件起终点安装高度,单位:mm
        ''' </summary>
        <Category("位置"), DisplayName("终点高(mm)"), Description("线性构件终点相对其所属的楼层的楼面高度值,值为-1代表同层高，单位:mm")>
        Public ReadOnly Property EndHeight As Integer
            Get
                Return _EndHeight
            End Get
        End Property

        Public MustOverride Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))

        Public MustOverride Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)

    End Class

    ''' <summary>
    ''' 构件对象初始化类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class InitCptClass

        ''' <summary>
        ''' 初始化安装点类型构件对象实例
        ''' </summary>
        ''' <param name="componentGroup">待初始化构件分组</param>
        ''' <param name="componentType">待初始化构件类型</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function InitPointComponentObj(ByVal componentGroup As String, ByVal componentType As String, Optional ByVal installHeight As Double = 0) As PointCptClass
            Dim componentPointObj As PointCptClass = Nothing
            Select Case componentGroup
                Case "厨卫设备"
                    componentPointObj = InitCwsbObj(componentType, installHeight)
                Case "电气设备"
                    componentPointObj = InitDqsbObj(componentType, installHeight)
                Case "电气附件"
                    componentPointObj = InitDqfjObj(componentType, installHeight)
                Case "给排水点"
                    componentPointObj = InitJpsdObj(componentType, installHeight)
                Case "通风设备"
                    componentPointObj = InitTfsbObj(componentType, installHeight)
                Case "采暖设备"
                    componentPointObj = InitCnsbObj(componentType, installHeight)
                Case "风管附件"
                    componentPointObj = InitFgfjObj(componentType, installHeight)
                Case "管道附件"
                    componentPointObj = InitGdfjObj(componentType, installHeight)
                Case "消防报警设备"
                    componentPointObj = InitXfbjsbObj(componentType, installHeight)
                Case "消防水卫"
                    componentPointObj = InitXfswObj(componentType, installHeight)
                Case "防雷接地设备"
                    componentPointObj = InitFljdsbObj(componentType, installHeight)
            End Select
            Return componentPointObj
        End Function

        ''' <summary>
        ''' 初始化安装线类型构件
        ''' </summary>
        ''' <param name="componentType">待初始化构件类型</param>
        ''' <param name="startHeight">线性构件起始点高度,单位：mm</param>
        ''' <param name="endHeight">线性构件终止点高度,单位：mm</param>
        Public Shared Function InitLineComponentObj(ByVal componentType As String, Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0) As BaseComponent
            Return InitGxObj(componentType, startHeight, endHeight)
        End Function

        ''' <summary>
        ''' 实例化厨卫设备构件对象
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitCwsbObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim cwsbObj As PointCptClass = Nothing
            Select Case componentType
                Case "厨具"
                    cwsbObj = New Cwsb.厨具(InstallHeight)
                Case "大便器"
                    cwsbObj = New Cwsb.大便器(InstallHeight)
                Case "盥洗盆"
                    cwsbObj = New Cwsb.盥洗盆(InstallHeight)
                Case "小便器"
                    cwsbObj = New Cwsb.小便器(InstallHeight)
                Case "淋浴器"
                    cwsbObj = New Cwsb.淋浴器(InstallHeight)
            End Select
            Return cwsbObj
        End Function

        ''' <summary>
        ''' 实例化电气设备构件对象
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitDqsbObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim dqsbObj As PointCptClass = Nothing
            Select Case componentType
                Case "插座"
                    dqsbObj = New Dqsb.插座(InstallHeight)
                Case "灯具"
                    dqsbObj = New Dqsb.灯具(InstallHeight)
                Case "配电箱"
                    dqsbObj = New Dqsb.配电箱(InstallHeight)
                Case "设备仪表"
                    dqsbObj = New Dqsb.设备仪表(InstallHeight)
                Case "开关"
                    dqsbObj = New Dqsb.开关(InstallHeight)
            End Select
            Return dqsbObj
        End Function

        ''' <summary>
        ''' 实例化电气附件构件对象
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitDqfjObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim dqfjObj As PointCptClass = Nothing
            Select Case componentType
                Case "电缆头"
                    dqfjObj = New Dqfj.电缆头(InstallHeight)
                Case "接线盒"
                    dqfjObj = New Dqfj.接线盒(InstallHeight)
                Case "套管"
                    dqfjObj = New Dqfj.套管(InstallHeight)
            End Select
            Return dqfjObj
        End Function

        ''' <summary>
        ''' 实例化给水点构件对象
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitJpsdObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim jpsdObj As PointCptClass = Nothing
            Select Case componentType
                Case "水表"
                    jpsdObj = New Jpsd.水表(InstallHeight)
                Case "水龙头"
                    jpsdObj = New Jpsd.水龙头(InstallHeight)
                Case "污水井"
                    jpsdObj = New Jpsd.污水井(InstallHeight)
                Case "地漏"
                    jpsdObj = New Jpsd.地漏(InstallHeight)
                Case "清扫口"
                    jpsdObj = New Jpsd.清扫口(InstallHeight)
            End Select
            Return jpsdObj
        End Function

        ''' <summary>
        ''' 实例化管线构件对象
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitGxObj(ByVal componentType As String, Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0) As LineCptClass
            Dim gxObj As LineCptClass = Nothing
            Select Case componentType
                Case "风管"
                    gxObj = New Gx.风管(startHeight, endHeight)
                Case "冷媒管"
                    gxObj = New Gx.冷媒管(startHeight, endHeight)
                Case "管道"
                    gxObj = New Gx.管道(startHeight, endHeight)
                Case "配线"
                    gxObj = New Gx.配线(startHeight, endHeight)
                Case "配管"
                    gxObj = New Gx.配管(startHeight, endHeight)
                Case "金属软管"
                    gxObj = New Gx.金属软管(startHeight, endHeight)
                Case "桥架"
                    gxObj = New Gx.桥架(startHeight, endHeight)
                Case "配线配管"
                    gxObj = New Gx.配线配管(startHeight, endHeight)
                Case "桥架配线"
                    gxObj = New Gx.桥架配线(startHeight, endHeight)
                Case "防雷接地线"
                    gxObj = New Gx.防雷接地线(startHeight, endHeight)
                Case "风机盘管"
                    gxObj = New Gx.风机盘管(startHeight, endHeight)
                Case "避雷引下线"
                    gxObj = New Gx.避雷引下线(startHeight, endHeight)
                Case "避雷网"
                    gxObj = New Gx.避雷网(startHeight, endHeight)
                Case "均压环"
                    gxObj = New Gx.均压环(startHeight, endHeight)
                Case "接地母线"
                    gxObj = New Gx.接地母线(startHeight, endHeight)
            End Select
            Return gxObj
        End Function

        ''' <summary>
        ''' 实例化通风设备构件对象
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitTfsbObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim ntsbObj As PointCptClass = Nothing
            Select Case componentType
                Case "风机"
                    ntsbObj = New Ntsb.风机(InstallHeight)
                Case "风口"
                    ntsbObj = New Ntsb.风口(InstallHeight)
                Case "风帽"
                    ntsbObj = New Ntsb.风帽(InstallHeight)
                Case "风罩"
                    ntsbObj = New Ntsb.风罩(InstallHeight)
                Case "静压箱"
                    ntsbObj = New Ntsb.静压箱(InstallHeight)
                Case "空调机组"
                    ntsbObj = New Ntsb.空调机组(InstallHeight)
                Case "空调设备"
                    ntsbObj = New Ntsb.空调设备(InstallHeight)
                Case "消声器"
                    ntsbObj = New Ntsb.消声器(InstallHeight)
            End Select
            Return ntsbObj
        End Function

        ''' <summary>
        ''' 初始化采暖设备
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitCnsbObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim cnsbObj As PointCptClass = Nothing
            Select Case componentType
                Case "散热器"
                    cnsbObj = New Cnsb.散热器(InstallHeight)
            End Select
            Return cnsbObj
        End Function

        ''' <summary>
        ''' 实例化风管附件构件对象
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitFgfjObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim fgfjObj As PointCptClass = Nothing
            Select Case componentType
                Case "风管软接头"
                    fgfjObj = New Fgfj.风管软接头(InstallHeight)
                Case "风管阀门"
                    fgfjObj = New Fgfj.风管阀门(InstallHeight)
                Case "风管法兰"
                    fgfjObj = New Fgfj.风管法兰(InstallHeight)
            End Select
            Return fgfjObj
        End Function

        ''' <summary>
        ''' 实例化管道附件构件对象
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitGdfjObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim gdfjObj As PointCptClass = Nothing
            Select Case componentType
                Case "管道套管"
                    gdfjObj = New Gdfj.管道套管(InstallHeight)
                Case "沟槽连接件"
                    gdfjObj = New Gdfj.沟槽连接件(InstallHeight)
                Case "管道连接件"
                    gdfjObj = New Gdfj.管道连接件(InstallHeight)
                Case "管道配件"
                    gdfjObj = New Gdfj.管道配件(InstallHeight)
                Case "管道阀门"
                    gdfjObj = New Gdfj.管道阀门(InstallHeight)
                Case "仪表装置"
                    gdfjObj = New Gdfj.仪表装置(InstallHeight)
            End Select
            Return gdfjObj
        End Function

        ''' <summary>
        ''' 实例化消防报警设备构件对象
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitXfbjsbObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim xfbjsbObj As PointCptClass = Nothing
            Select Case componentType
                Case "报警设备"
                    xfbjsbObj = New Xfbjsb.报警设备(InstallHeight)
                Case "控制模块"
                    xfbjsbObj = New Xfbjsb.控制模块(InstallHeight)
            End Select
            Return xfbjsbObj
        End Function

        ''' <summary>
        ''' 实例化消防水卫构件对象
        ''' </summary>
        ''' <param name="componentType">待初始化对象的类型</param>
        Private Shared Function InitXfswObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim xfswObj As PointCptClass = Nothing
            Select Case componentType
                Case "灭火器"
                    xfswObj = New Xfsw.灭火器(InstallHeight)
                Case "喷淋头"
                    xfswObj = New Xfsw.喷淋头(InstallHeight)
                Case "水泵"
                    xfswObj = New Xfsw.水泵(InstallHeight)
                Case "水箱"
                    xfswObj = New Xfsw.水箱(InstallHeight)
                Case "消火栓"
                    xfswObj = New Xfsw.消火栓(InstallHeight)
            End Select
            Return xfswObj
        End Function

        ''' <summary>
        ''' 实例化防雷接地设备
        ''' </summary>
        ''' <param name="componentType">待初始化构件类型</param>
        Private Shared Function InitFljdsbObj(ByVal componentType As String, Optional ByVal InstallHeight As Double = 0) As PointCptClass
            Dim fljdsbObj As PointCptClass = Nothing
            Select Case componentType
                Case "避雷针"
                    fljdsbObj = New FljdsbGroup.避雷针(InstallHeight)
                Case "等电位连接"
                    fljdsbObj = New FljdsbGroup.等电位连接(InstallHeight)
                Case "接地极"
                    fljdsbObj = New FljdsbGroup.接地极(InstallHeight)
                Case "焊接点"
                    fljdsbObj = New FljdsbGroup.焊接点(InstallHeight)
                Case "等电位箱"
                    fljdsbObj = New FljdsbGroup.等电位箱(InstallHeight)
                Case "避雷网接地测试点"
                    fljdsbObj = New FljdsbGroup.避雷网接地测试点(InstallHeight)
                Case "接地跨接线"
                    fljdsbObj = New FljdsbGroup.接地跨接线(InstallHeight)
            End Select
            Return fljdsbObj
        End Function

    End Class

End Namespace
