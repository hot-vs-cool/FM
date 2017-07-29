Imports System.ComponentModel

Namespace Component.Ntsb

    Public Class 风机
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.通风设备, AzComponentType.风机, installHeight)
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

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.风机)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
        End Sub

    End Class

    Public Class 风口
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.通风设备, AzComponentType.风机, installHeight)
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

        ''' <summary>
        ''' 长度值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("长度(mm)"), Description("矩形风口截面长度,圆形风口不需要此属性,单位为:mm")>
        Public Property Length As Integer = 600

        ''' <summary>
        ''' 宽度值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("宽度(mm)"), Description("风口截面宽度,圆形风口不需要此属性,单位为:mm")>
        Public Property Width As Integer = 900

        ''' <summary>
        ''' 公称直径,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("风口所附着的风管的截面公称直径,矩形风口不需要此属性,单位为:mm")>
        Public Property Diameter As Integer

        ''' <summary>
        ''' 截面周长,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("截面周长(mm)"), Description("风口所附着的风管的截面周长值,通常由系统自动计算不需要用户设置,单位:mm")>
        Public ReadOnly Property Perimeter As Integer
            Get
                If Me.Width <> 0 AndAlso Me.Length <> 0 Then
                    Return (Me.Width + Me.Length) * 2
                Else
                    Return CInt(Math.PI * Me.Diameter)
                End If
            End Get
        End Property

        ''' <summary>
        ''' 截面面积,单位:㎜²
        ''' </summary>
        <Category("尺寸"), DisplayName("截面面积(㎜²)"), Description("风口截面面积,由系统自动计算,:㎜²")>
        Public ReadOnly Property SectionArea As Double
            Get
                If Me.Width <> 0 AndAlso Me.Length <> 0 Then
                    Return (Me.Width * Me.Length)
                Else
                    Return CInt(Math.PI * (Me.Diameter / 2) ^ 2)
                End If
            End Get
        End Property

        ''' <summary>
        ''' 风口重量,单位:kg
        ''' </summary>
        <Category("其它"), DisplayName("设备重量(kg)"), Description("风口重量,单位:kg")>
        Public Property Weight As Double

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.风口)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("长度(mm)") = Me.Length.ToString
            dicObjProperty("宽度(mm)") = Me.Width.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            dicObjProperty("截面周长(mm)") = Me.Perimeter.ToString
            dicObjProperty("截面面积(㎜²)") = Me.SectionArea.ToString
            dicObjProperty("设备重量(kg)") = Me.Weight.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("长度(mm)") Then Integer.TryParse(dicProperty("长度(mm)"), Me.Length)
            If dicProperty.ContainsKey("宽度(mm)") Then Integer.TryParse(dicProperty("宽度(mm)"), Me.Width)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
            If dicProperty.ContainsKey("周长(mm)") Then Integer.TryParse(dicProperty("周长(mm)"), Me.Perimeter)
            If dicProperty.ContainsKey("截面面积(㎜²)") Then Double.TryParse(dicProperty("截面面积(㎜²)"), Me.SectionArea)
            If dicProperty.ContainsKey("设备重量(kg)") Then Double.TryParse(dicProperty("设备重量(kg)"), Me.Weight)
        End Sub

    End Class

    Public Class 风帽
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.通风设备, AzComponentType.风机, installHeight)
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

        ''' <summary>
        ''' 指示组成构件的材料类型
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String = ""

        ''' <summary>
        ''' 风帽重量,单位:kg
        ''' </summary>
        <Category("其它"), DisplayName("设备重量(kg)"), Description("风帽重量值,单位:kg")>
        Public Property Weight As Double

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.风帽)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("设备重量(kg)") = Me.Weight.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("设备重量(kg)") Then Double.TryParse(dicProperty("设备重量(kg)"), Me.Weight)
        End Sub

    End Class

    Public Class 风罩
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.通风设备, AzComponentType.风机, installHeight)
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

        ''' <summary>
        ''' 指示组成构件的材料类型
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String = ""

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.风罩)
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

    Public Class 静压箱
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.通风设备, AzComponentType.静压箱, installHeight)
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

        ''' <summary>
        ''' 静压箱宽度，单位：mm
        ''' </summary>
        <Category("尺寸"), DisplayName("宽度(mm)"), Description("静压箱宽度值,单位:mm")>
        Public Property Width As Integer = 200

        ''' <summary>
        ''' 静压箱高度，单位：mm
        ''' </summary>
        <Category("尺寸"), DisplayName("高度(mm)"), Description("静压箱高度值,单位:mm")>
        Public Property Height As Integer = 500

        ''' <summary>
        ''' 静压箱厚度值，单位：mm
        ''' </summary>
        <Category("尺寸"), DisplayName("厚度(mm)"), Description("静压箱厚度值,单位:mm")>
        Public Property Thickness As Integer = 700

        <Category("尺寸"), DisplayName("表面积(㎡)"), Description("静压箱表面积,由系统根据静压箱尺寸自动计算,单位:㎡")>
        Public ReadOnly Property SufaceArea As Double
            Get
                Return Math.Round((Me.Width * Me.Height * 2 + Me.Width * Me.Thickness * 2 + Me.Height * Me.Thickness * 2) / 10 ^ 6, 3)
            End Get
        End Property

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.静压箱)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("宽度(mm)") = Me.Width.ToString
            dicObjProperty("厚度(mm)") = Me.Thickness.ToString
            dicObjProperty("高度(mm)") = Me.Height.ToString
            dicObjProperty("表面积(㎡)") = Me.SufaceArea.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
            If dicProperty.ContainsKey("宽度(mm)") Then Integer.TryParse(dicProperty("宽度(mm)"), Me.Width)
            If dicProperty.ContainsKey("厚度(mm)") Then Integer.TryParse(dicProperty("厚度(mm)"), Me.Thickness)
            If dicProperty.ContainsKey("高度(mm)") Then Integer.TryParse(dicProperty("高度(mm)"), Me.Height)
            If dicProperty.ContainsKey("表面积(㎡)") Then Double.TryParse(dicProperty("表面积(㎡)"), Me.SufaceArea)
        End Sub

    End Class

    Public Class 空调机组
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.通风设备, AzComponentType.空调机组, installHeight)
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

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.空调机组)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
        End Sub

    End Class

    Public Class 空调设备
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.通风设备, AzComponentType.空调设备, installHeight)
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

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.空调设备)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
        End Sub

    End Class

    Public Class 消声器
        Inherits PointCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.通风设备, AzComponentType.消声器, installHeight)
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

        ''' <summary>
        ''' 消声器宽度，单位：mm
        ''' </summary>
        <Category("尺寸"), DisplayName("宽度(mm)"), Description("消声器宽度值,单位:mm")>
        Public Property Width As Integer = 500

        ''' <summary>
        ''' 消声器高度，单位：mm
        ''' </summary>
        <Category("尺寸"), DisplayName("高度(mm)"), Description("消声器高度值,单位:mm")>
        Public Property Height As Integer = 700

        ''' <summary>
        ''' 消声器长度值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("长度(mm)"), Description("消声器长度值,单位:mm")>
        Public Property Length As Integer

        ''' <summary>
        ''' 消声器截面周长，单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("截面周长(mm)"), Description("该属性由系统自动计算不需要用户设置,单位:mm")>
        Public ReadOnly Property Perimeter As Integer
            Get
                Return Me.Width + Me.Height
            End Get
        End Property

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.消声器)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("宽度(mm)") = Me.Width.ToString
            dicObjProperty("高度(mm)") = Me.Height.ToString
            dicObjProperty("长度(mm)") = Me.Length.ToString
            dicObjProperty("截面周长(mm)") = Me.Perimeter.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
            If dicProperty.ContainsKey("宽度(mm)") Then Integer.TryParse(dicProperty("宽度(mm)"), Me.Width)
            If dicProperty.ContainsKey("高度(mm)") Then Integer.TryParse(dicProperty("高度(mm)"), Me.Height)
            If dicProperty.ContainsKey("长度(mm)") Then Integer.TryParse(dicProperty("长度(mm)"), Me.Length)
            If dicProperty.ContainsKey("周长(mm)") Then Integer.TryParse(dicProperty("周长(mm)"), Me.Perimeter)
        End Sub

    End Class

End Namespace

