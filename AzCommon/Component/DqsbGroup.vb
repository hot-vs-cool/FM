Imports System.ComponentModel

Namespace Component.Dqsb

    Public Class 插座
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal InstallHeight As Double = 1400)
            MyBase.New(AzComponentGroup.电气设备, AzComponentType.插座, installHeight)
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

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.插座)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
        End Sub

    End Class

    Public Class 灯具
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 灯具安装方式
        ''' </summary>
        Public Enum LampInstallType
            吸顶
            壁装
            吊链
            吊管
        End Enum

        Public Sub New(Optional ByVal InstallHeight As Double = 1400)
            MyBase.New(AzComponentGroup.电气设备, AzComponentType.灯具, installHeight)
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
        ''' 灯具的安装方式，分为吸顶、壁挂、吊链、吊管
        ''' </summary>
        <Category("其它"), DisplayName("安装方式"), Description("指示灯具的安装方式，分为吸顶、壁挂、吊链、吊管")>
        Public Property InstallType As LampInstallType = LampInstallType.吸顶

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.灯具)
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
            If dicProperty.ContainsKey("安装方式") Then Me.InstallType = CType([Enum].Parse(GetType(LampInstallType), dicProperty("安装方式")), LampInstallType)
        End Sub

    End Class

    Public Class 配电箱
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal InstallHeight As Double = 1500)
            MyBase.New(AzComponentGroup.电气设备, AzComponentType.配电箱, InstallHeight)
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
        ''' 配电箱宽度值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("宽度(mm)"), Description("配电箱宽度值,单位:mm")>
        Public Property Width As Integer = 200

        ''' <summary>
        ''' 配电箱高度值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("高度(mm)"), Description("配电箱高度值,单位:mm")>
        Public Property Height As Integer = 500

        ''' <summary>
        ''' 配电箱厚度值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("厚度(mm)"), Description("配电箱厚度值,单位:mm")>
        Public Property Thickness As Integer = 700

        ''' <summary>
        ''' 配电箱半周长,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("半周长(mm)"), Description("配电箱柜半周长,其值由""宽度+高度""组成,由系统根据配电箱柜尺寸自动计算,单位:mm")>
        Public ReadOnly Property Perimeter As Integer
            Get
                Return (Me.Height + Me.Width)
            End Get
        End Property

        ''' <summary>
        ''' 配电箱开关模数
        ''' </summary>
        <Category("其它"), DisplayName("开关模数"), Description("指示配电箱的开关模数数量")>
        Public Property Modular As Integer = 20

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.配电箱)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("宽度(mm)") = Me.Width.ToString
            dicObjProperty("高度(mm)") = Me.Height.ToString
            dicObjProperty("厚度(mm)") = Me.Thickness.ToString
            dicObjProperty("半周长(mm)") = Me.Perimeter.ToString
            dicObjProperty("开关模数") = Me.Modular.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("宽度(mm)") Then Integer.TryParse(dicProperty("宽度(mm)"), Me.Width)
            If dicProperty.ContainsKey("高度(mm)") Then Integer.TryParse(dicProperty("高度(mm)"), Me.Height)
            If dicProperty.ContainsKey("厚度(mm)") Then Integer.TryParse(dicProperty("厚度(mm)"), Me.Thickness)
            If dicProperty.ContainsKey("半周长(mm)") Then Integer.TryParse(dicProperty("半周长(mm)"), Me.Perimeter)
            If dicProperty.ContainsKey("开关模数") Then Integer.TryParse(dicProperty("开关模数"), Me.Modular)
        End Sub

    End Class

    Public Class 设备仪表
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal InstallHeight As Double = 1400)
            MyBase.New(AzComponentGroup.电气设备, AzComponentType.设备仪表, installHeight)
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

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.设备仪表)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
        End Sub

    End Class

    Public Class 开关
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal InstallHeight As Double = 1400)
            MyBase.New(AzComponentGroup.电气设备, AzComponentType.开关, installHeight)
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

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.开关)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
        End Sub

    End Class

End Namespace
