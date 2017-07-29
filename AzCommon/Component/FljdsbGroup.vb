Imports CxAzFunc.Component
Imports System.ComponentModel

Public Class FljdsbGroup

    Public Class 避雷针
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.防雷接地设备, AzComponentType.避雷针, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As ElectricalSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As ElectricalSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As ElectricalSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As ElectricalSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 配线材质信息
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("指示防雷接地线的材质信息")>
        Public Property Material As String = ""

        ''' <summary>
        ''' 安装位置，一般取烟囱、平屋面、墙、金属容器顶部、构筑物
        ''' </summary>
        <Category("位置"), DisplayName("安装位置"), Description("指示避雷针的安装位置,一般取烟囱、平屋面、墙、金属容器顶部、构筑物")>
        Public Property Loaction As String = "平屋面"

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.避雷针)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("安装位置") = Me.Loaction
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("安装位置") Then Me.Loaction = dicProperty("安装位置")
        End Sub

    End Class

    Public Class 等电位连接
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.防雷接地设备, AzComponentType.等电位连接, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As ElectricalSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As ElectricalSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As ElectricalSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As ElectricalSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 配线材质信息
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("指示构件的材质信息")>
        Public Property Material As String = ""

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.等电位连接)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
        End Sub

    End Class

    Public Class 接地极
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.防雷接地设备, AzComponentType.接地极, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As ElectricalSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As ElectricalSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As ElectricalSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As ElectricalSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 配线材质信息
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("指示构件的材质信息")>
        Public Property Material As String = ""

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.接地极)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
        End Sub

    End Class

    Public Class 焊接点
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 焊接点安装位置类型
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum WeldPointLocation
            柱与圈梁
            桩与圈梁
            其它
        End Enum

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.防雷接地设备, AzComponentType.焊接点, InstallHeight)
        End Sub

        ''' <summary>
        ''' 构件名称
        ''' </summary>
        <Category("通用"), DisplayName("构件名称"), Description("指示避雷网关联的名称表示文本,由系统自动设定")>
        Public Overloads ReadOnly Property Name As String
            Get
                MyBase.Name = (Me.Location.ToString & Me.ComponentType.ToString).Trim
                Return MyBase.Name
            End Get
        End Property

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As ElectricalSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As ElectricalSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As ElectricalSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As ElectricalSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 配线材质信息
        ''' </summary>
        <Category("位置"), DisplayName("安装位置"), Description("指示焊接点的安装位置")>
        Public Property Location As WeldPointLocation = WeldPointLocation.柱与圈梁

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.焊接点)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("安装位置") = Me.Location.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("安装位置") Then Me.Location = CType([Enum].Parse(GetType(WeldPointLocation), dicProperty("安装位置")), WeldPointLocation)
        End Sub

    End Class

    Public Class 等电位箱
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.消防报警
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.弱电

        ''' <summary>
        ''' 等电位箱安装方式
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum MEPInstallType
            明装
            暗装
        End Enum

        Public Sub New(Optional ByVal InstallHeight As Double = 1500)
            MyBase.New(AzComponentGroup.防雷接地设备, AzComponentType.等电位箱, InstallHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As ElectricalSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As ElectricalSpeciality)
                _SpecialityType = value
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As ElectricalSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As ElectricalSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 等电位箱安装方式
        ''' </summary>
        <Category("其它"), DisplayName("安装方式"), Description("指示构件所属的系统类型")>
        Public Property InstallType As MEPInstallType = MEPInstallType.暗装

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.等电位箱)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("安装方式") = Me.InstallType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("安装方式") Then Me.InstallType = CType([Enum].Parse(GetType(MEPInstallType), dicProperty("安装方式")), MEPInstallType)
        End Sub

    End Class

    Public Class 避雷网接地测试点
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.防雷接地设备, AzComponentType.避雷网接地测试点, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As ElectricalSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As ElectricalSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As ElectricalSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As ElectricalSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 配线材质信息
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("指示构件的材质信息")>
        Public Property Material As String = ""

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.避雷网接地测试点)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
        End Sub

    End Class

    Public Class 接地跨接线
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.防雷接地设备, AzComponentType.接地跨接线, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As ElectricalSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As ElectricalSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As ElectricalSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As ElectricalSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 配线材质信息
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("指示构件的材质信息")>
        Public Property Material As String = ""

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.接地跨接线)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
        End Sub

    End Class

End Class
