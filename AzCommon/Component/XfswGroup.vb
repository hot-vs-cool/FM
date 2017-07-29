Imports System.ComponentModel

Namespace Component.Xfsw

    Public Class 灭火器
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.消防
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.消防

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.消防水卫, AzComponentType.灭火器, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As WaterSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As WaterSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As WaterSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As WaterSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.灭火器)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
        End Sub

    End Class

    Public Class 喷淋头
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.喷淋
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.消防

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.消防水卫, AzComponentType.喷淋头, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As WaterSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As WaterSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As WaterSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As WaterSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.喷淋头)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
        End Sub

    End Class

    Public Class 水泵
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.泵房
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.消防水卫, AzComponentType.水泵, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As WaterSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As WaterSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As WaterSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As WaterSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 水泵设备的重量信息
        ''' </summary>
        <Category("其它"), DisplayName("设备重量(t)"), Description("水泵设备重量信息，单位以吨计")>
        Public Property Weight As Double = 0

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.水泵)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("设备重量(t)") = Me.Weight.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("设备重量(t)") Then Double.TryParse(dicProperty("设备重量(t)"), Me.Weight)
        End Sub

    End Class

    Public Class 水箱
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.给水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.消防水卫, AzComponentType.水箱, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As WaterSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As WaterSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As WaterSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As WaterSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 规格形状
        ''' </summary>
        <Category("其它"), DisplayName("规格形状"), Description("指示水箱的规格形状属性")>
        Public Property Shape As String = ""

        ''' <summary>
        ''' 水箱容积
        ''' </summary>
        <Category("尺寸"), DisplayName("体积(m³)"), Description("指示水箱容积信息,单位:m³")>
        Public Property Volume As Double = 0

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.水箱)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("规格形状") = Me.Shape
            dicObjProperty("体积(m³)") = Math.Round(Me.Volume, 3).ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("规格形状") Then Me.Shape = dicProperty("规格形状")
            If dicProperty.ContainsKey("体积(m³)") Then Double.TryParse(dicProperty("体积(m³)"), Me.Volume)
        End Sub

    End Class

    Public Class 消火栓
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.喷淋
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.消防

        ''' <summary>
        ''' 消火栓的安装位置枚举
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum HydrantInstallType
            室内
            室外
        End Enum

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.消防水卫, AzComponentType.消火栓, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As WaterSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As WaterSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As WaterSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As WaterSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 公称直径,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("消火栓公称直径,一般取与消火栓相连的管道直径,单位为:mm")>
        Public Property Diameter As Integer = 16

        ''' <summary>
        ''' 安装位置
        ''' </summary>
        <Category("其它"), DisplayName("安装位置"), Description("指示消火栓的安装位置,分为室内和室外")>
        Public Property InstallType As HydrantInstallType = HydrantInstallType.室外

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.消火栓)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            dicObjProperty("安装位置") = Me.InstallType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
            If dicProperty.ContainsKey("安装位置") Then Me.InstallType = CType([Enum].Parse(GetType(HydrantInstallType), dicProperty("安装位置")), HydrantInstallType)
        End Sub

    End Class

End Namespace
