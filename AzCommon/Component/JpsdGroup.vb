Imports System.ComponentModel

Namespace Component.Jpsd

    Public Class 水表
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.给水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Enum JsdEnum
            水表
            水龙头
        End Enum

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.给排水点, AzComponentType.水表, InstallHeight)
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
        ''' 连接方式
        ''' </summary>
        <Category("其它"), DisplayName("连接方式"), Description("水表的连接方式")>
        Public Property JunctionType As String = ""

        ''' <summary>
        ''' 排水点所属的宿主构件截面公称直径
        ''' </summary>
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("排水点所属的宿主构件截面公称直径,地漏系统会根据其所属管道自动取值,其它排水点构件由用户设置,单位为:mm")>
        Public Property Diameter As Integer = 25

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.水表)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("连接方式") = Me.JunctionType
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("连接方式") Then Me.JunctionType = dicProperty("连接方式")
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
        End Sub

    End Class

    Public Class 水龙头
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.给水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Enum JsdEnum
            水表
            水龙头
        End Enum

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.给排水点, AzComponentType.水龙头, InstallHeight)
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
        ''' 排水点所属的宿主构件截面公称直径
        ''' </summary>
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("排水点所属的宿主构件截面公称直径,地漏系统会根据其所属管道自动取值,其它排水点构件由用户设置,单位为:mm")>
        Public Property Diameter As Integer = 25

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.水龙头)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
        End Sub

    End Class

    Public Class 污水井
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.污水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.给排水点, AzComponentType.污水井, InstallHeight)
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
        ''' 指示组成构件的材料类型
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String = ""

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.污水井)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
        End Sub

    End Class

    Public Class 地漏
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.污水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.给排水点, AzComponentType.地漏, InstallHeight)
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
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("池井所附着的管道的截面公称直径，通常由系统自动识别不需要用户设置,单位为:mm")>
        Public Property Diameter As Integer

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.地漏)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
        End Sub

    End Class

    Public Class 清扫口
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.污水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.给排水点, AzComponentType.清扫口, InstallHeight)
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
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("清扫口所附着的管道的截面公称直径，通常由系统自动识别不需要用户设置,单位为:mm")>
        Public Property Diameter As Integer

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.清扫口)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
        End Sub

    End Class

End Namespace
