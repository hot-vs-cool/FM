Imports System.ComponentModel

Namespace Component.Fgfj

    Public Class 风管软接头
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.风管附件, AzComponentType.风管软接头, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As HVACSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As HVACSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As HVACSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As HVACSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        <Category("尺寸"), DisplayName("宽度(mm)"), Description("软接头宽度值,单位为:mm")>
        Public Property Width As Integer

        <Category("尺寸"), DisplayName("高度(mm)"), Description("软接头高度值,单位为:mm")>
        Public Property Height As Integer

        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("软接头所附着的风管的截面公称直径,矩形风管取长边尺寸，通常由系统自动识别不需要用户设置,单位为:mm")>
        Public Property Diameter As Integer

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.风管软接头)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("宽度(mm)") = Me.Width.ToString
            dicObjProperty("高度(mm)") = Me.Height.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
            If dicProperty.ContainsKey("宽度(mm)") Then Integer.TryParse(dicProperty("宽度(mm)"), Me.Width)
            If dicProperty.ContainsKey("高度(mm)") Then Integer.TryParse(dicProperty("高度(mm)"), Me.Height)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
        End Sub

    End Class

    Public Class 风管法兰
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.风管附件, AzComponentType.风管法兰, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As HVACSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As HVACSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As HVACSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As HVACSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String = ""

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.风管法兰)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
        End Sub

    End Class

    Public Class 风管阀门
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.风管附件, AzComponentType.风管阀门, installHeight)
        End Sub

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As HVACSpeciality
            Get
                Return _SpecialityType
            End Get
            Set(ByVal value As HVACSpeciality)
                _SpecialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As HVACSystem
            Get
                Return _SystemType
            End Get
            Set(ByVal value As HVACSystem)
                _SystemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String = ""

        <Category("尺寸"), DisplayName("宽度(mm)"), Description("阀门宽度值,单位为:mm")>
        Public Property Width As Integer

        <Category("尺寸"), DisplayName("高度(mm)"), Description("阀门高度值,单位为:mm")>
        Public Property Height As Integer

        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("阀门所附着的风管的截面公称直径,矩形风管取长边尺寸，通常由系统自动识别不需要用户设置,单位为:mm")>
        Public Property Diameter As Integer

        <Category("尺寸"), DisplayName("截面周长(mm)"), Description("阀门所附着的风管的截面周长值,通常由系统自动计算不需要用户设置,单位:mm")>
        Public Property Perimeter As Integer

        <Category("其它"), DisplayName("配件类型"), Description("指示阀门类型,一般从蝶阀、止回阀、防火阀、调节阀中取")>
        Public Property ValveType As String = "蝶阀"

        <Category("其它"), DisplayName("是否保温"), Description("该属性指示风管阀门是否要计算保温工程量")>
        Public Property IsInsulate As YesNoEnum = YesNoEnum.否

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.风管阀门)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("宽度(mm)") = Me.Width.ToString
            dicObjProperty("高度(mm)") = Me.Height.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            dicObjProperty("截面周长(mm)") = Me.Perimeter.ToString
            dicObjProperty("阀门类型") = Me.ValveType
            dicObjProperty("是否保温") = Me.IsInsulate.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("宽度(mm)") Then Integer.TryParse(dicProperty("宽度(mm)"), Me.Width)
            If dicProperty.ContainsKey("高度(mm)") Then Integer.TryParse(dicProperty("高度(mm)"), Me.Height)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
            If dicProperty.ContainsKey("截面周长(mm)") Then Integer.TryParse(dicProperty("截面周长(mm)"), Me.Perimeter)
            If dicProperty.ContainsKey("阀门类型") Then Me.ValveType = dicProperty("阀门类型")
            If dicProperty.ContainsKey("是否保温") Then Me.IsInsulate = CType([Enum].Parse(GetType(YesNoEnum), dicProperty("是否保温")), YesNoEnum)
        End Sub

    End Class

End Namespace
