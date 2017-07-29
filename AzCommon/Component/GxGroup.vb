Imports System.ComponentModel
Imports System.Text.RegularExpressions

Namespace Component.Gx

    Public Class 风管
        Inherits LineCptClass

        ''' <summary>
        ''' 风管类型枚举
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum AirDuctType
            普通风管
            柔性风管
        End Enum

        ''' <summary>
        ''' 风管系统类别枚举
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum AirDuctSystemCategory
            微压
            低压
            中压
            高压
            除尘
        End Enum

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.风管, startHeight, endHeight)
        End Sub

        Private _diameter As Integer = 0
        Private _Material As String = "镀锌钢板"
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        ''' <summary>
        ''' 与构件关联的名称标识字符串
        ''' </summary>
        <Category("通用设置"), DisplayName("构件名称"), Description("与构件关联的名称标识文本")>
        Public Overloads ReadOnly Property Name As String
            Get
                If Me.Width <> 0 AndAlso Me.Height <> 0 Then
                    MyBase.Name = Me.Material & "风管 " & Me.Width & "×" & Me.Height & "(δ=" & Me.Thickness & "mm)"
                Else
                    MyBase.Name = Me.Material & "风管 " & "d=" & Me.Diameter & "(δ=" & Me.Thickness & "mm)"
                End If
                Return MyBase.Name
            End Get
        End Property

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
        ''' 宽度值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("宽度(mm)"), Description("矩形风管截面宽度,如为圆形风管此属性值为0,单位为:mm")>
        Public Property Width As Integer = 900

        ''' <summary>
        ''' 高度值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("高度(mm)"), Description("矩形风管截面高度,如为圆形风管此属性值为0,单位为:mm")>
        Public Property Height As Integer = 600

        ''' <summary>
        ''' 公称直径值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("圆形风管截面公称直径,如为矩形风管该属性值为0,单位为:mm")>
        Public Property Diameter As Integer
            Get
                Return Me._diameter
            End Get
            Set(ByVal value As Integer)
                If value = 0 Then
                    Me._diameter = value
                    If Me.Width = 0 Then Me.Width = 500
                    If Me.Height = 0 Then Me.Height = 500
                ElseIf value > 0 Then
                    Me.Width = 0
                    Me.Height = 0
                    Me._diameter = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' 壁厚值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("壁厚(mm)"), Description("风管壁厚,一般由系统根据风管材质、截面尺寸自动生成,单位为:mm")>
        Public Property Thickness As Double = 10

        ''' <summary>
        ''' 保温层厚值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("保温厚度(mm)"), Description("风管保温层厚度,一般取15、20、25,单位为:mm")>
        Public Property InsulationThickness As Double = 15

        ''' <summary>
        ''' 截面周长,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("截面周长(mm)"), Description("风管截面周长，系统会根据风管截面尺寸自动进行计算，不需要用户输入,单位为:mm")>
        Public ReadOnly Property Perimeter As Integer
            Get
                If Me.Diameter > 0 Then
                    Return CInt(Math.PI * Me.Diameter)
                Else
                    Return (Me.Width + Me.Height) * 2
                End If
            End Get
        End Property

        ''' <summary>
        ''' 风管材质
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("风管的材质信息,一般不需要此属性")>
        Public Property Material As String
            Get
                Return _Material
            End Get
            Set(ByVal value As String)
                If Trim(value) = "" Then
                    Exit Property
                Else
                    _Material = Trim(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' 风管类型
        ''' </summary>
        <Category("其它"), DisplayName("风管类型"), Description("指示风管类型")>
        Public Property Type As AirDuctType = AirDuctType.普通风管

        <Category("其它"), DisplayName("系统类别"), Description("风管所属系统类别分为中低压系统、高压系统、除尘系统")>
        Public Property SystemCategory As AirDuctSystemCategory = AirDuctSystemCategory.中压

        ''' <summary>
        ''' 是否保温
        ''' </summary>
        <Category("其它"), DisplayName("是否保温"), Description("该属性指示风管是否要计算保温工程量")>
        Public Property IsInsulate As YesNoEnum = YesNoEnum.否

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.风管)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("宽度(mm)") = Me.Width.ToString
            dicObjProperty("高度(mm)") = Me.Height.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            dicObjProperty("壁厚(mm)") = Me.Thickness.ToString
            dicObjProperty("保温厚度(mm)") = Me.InsulationThickness.ToString
            dicObjProperty("截面周长(mm)") = Me.Perimeter.ToString
            dicObjProperty("风管类型") = Me.Type.ToString
            dicObjProperty("系统类别") = Me.SystemCategory.ToString
            dicObjProperty("是否保温") = Me.IsInsulate.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("宽度(mm)") Then Integer.TryParse(dicProperty("宽度(mm)"), Me.Width)
            If dicProperty.ContainsKey("高度(mm)") Then Integer.TryParse(dicProperty("高度(mm)"), Me.Height)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
            If dicProperty.ContainsKey("壁厚(mm)") Then Double.TryParse(dicProperty("壁厚(mm)"), Me.Thickness)
            If dicProperty.ContainsKey("保温厚度(mm)") Then Double.TryParse(dicProperty("保温厚度(mm)"), Me.InsulationThickness)
            If dicProperty.ContainsKey("截面周长(mm)") Then Integer.TryParse(dicProperty("截面周长(mm)"), Me.Perimeter)
            If dicProperty.ContainsKey("风管类型") Then Me.Type = CType([Enum].Parse(GetType(AirDuctType), dicProperty("风管类型")), AirDuctType)
            If dicProperty.ContainsKey("系统类别") Then Me.SystemCategory = CType([Enum].Parse(GetType(AirDuctSystemCategory), dicProperty("系统类别")), AirDuctSystemCategory)
            If dicProperty.ContainsKey("系统类别") Then [Enum].TryParse(Of AirDuctSystemCategory)(dicProperty("系统类别"), Me.SystemCategory)
            If dicProperty.ContainsKey("是否保温") Then Me.IsInsulate = CType([Enum].Parse(GetType(YesNoEnum), dicProperty("是否保温")), YesNoEnum)
        End Sub

    End Class

    Public Class 冷媒管
        Inherits LineCptClass
        Private _Material As String = "铜管"
        Private _SystemType As HVACSystem = HVACSystem.空调冷媒水
        Private _gasDiameter As Double = 25
        Private _fluidDiameter As Double = 34
        Private _gasInsulationThickness As Double = 20
        Private _fluidInsulationThickness As Double = 15
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调水

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.冷媒管, startHeight, endHeight)
        End Sub

        ''' <summary>
        ''' 冷媒管构件名称由系统自动根据材质、气液管管径自动生成
        ''' </summary>
        Public Overloads ReadOnly Property Name As String
            Get
                MyBase.Name = (Me.Material & " φ" & Me.GasDiameter & "/" & Me.FluidDiameter).Trim
                Return MyBase.Name
            End Get
        End Property

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads ReadOnly Property SpecialityType As HVACSpeciality
            Get
                Return _SpecialityType
            End Get
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads ReadOnly Property SystemType As HVACSystem
            Get
                Return _SystemType
            End Get
        End Property

        ''' <summary>
        ''' 冷媒管气管管径值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("气管管径(mm)"), Description("冷媒管气管管径值,单位为:mm")>
        Public Property GasDiameter As Double
            Get
                Return Me._gasDiameter
            End Get
            Set(ByVal value As Double)
                If value < 0 Then Exit Property
                Me._gasDiameter = value
            End Set
        End Property

        ''' <summary>
        ''' 气管保温层厚值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("气管保温厚度(mm)"), Description("冷媒管气管保温层厚度值,单位为:mm")>
        Public Property GasInsulationThickness As Double
            Get
                Return Me._gasInsulationThickness
            End Get
            Set(ByVal value As Double)
                If value < 0 Then Exit Property
                Me._gasInsulationThickness = Math.Round(value, 3)
            End Set
        End Property

        ''' <summary>
        ''' 冷媒管液管管径值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("液管管径(mm)"), Description("冷媒管液管管径值,单位:mm")>
        Public Property FluidDiameter As Double
            Get
                Return Me._fluidDiameter
            End Get
            Set(ByVal value As Double)
                If value < 0 Then Exit Property
                Me._fluidDiameter = value
            End Set
        End Property

        ''' <summary>
        ''' 液管保温层厚值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("液管保温厚度(mm)"), Description("冷媒管液管保温层厚度值,单位为:mm")>
        Public Property FluidInsulationThickness As Double
            Get
                Return Me._fluidInsulationThickness
            End Get
            Set(ByVal value As Double)
                If value < 0 Then Exit Property
                Me._fluidInsulationThickness = Math.Round(value, 3)
            End Set
        End Property

        <Category("其它"), DisplayName("是否刷漆"), Description("该属性指示在工程量计算是是否要考虑刷漆")>
        Public Property IsPaint As YesNoEnum

        <Category("其它"), DisplayName("是否吊架"), Description("该属性指示在工程量计算是是否要考虑吊架")>
        Public Property IsHanger As YesNoEnum

        ''' <summary>
        ''' 是否保温
        ''' </summary>
        <Category("其它"), DisplayName("是否保温"), Description("该属性指示冷媒管是否要计算保温工程量")>
        Public Property IsInsulate As YesNoEnum = YesNoEnum.否

        ''' <summary>
        ''' 冷媒管材质类型
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String
            Get
                Return _Material
            End Get
            Set(ByVal value As String)
                If Trim(value) = "" Then
                    Exit Property
                Else
                    _Material = Trim(value)
                End If
            End Set
        End Property

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.管道)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("气管管径(mm)") = Me.GasDiameter.ToString
            dicObjProperty("液管管径(mm)") = Me.FluidDiameter.ToString
            dicObjProperty("气管保温厚度(mm)") = Me.GasInsulationThickness.ToString
            dicObjProperty("液管保温厚度(mm)") = Me.FluidInsulationThickness.ToString
            dicObjProperty("是否吊架") = Me.IsHanger.ToString
            dicObjProperty("是否保温") = Me.IsInsulate.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me._SpecialityType = CType([Enum].Parse(GetType(HVACSpeciality), dicProperty("专业类型")), HVACSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me._SystemType = CType([Enum].Parse(GetType(HVACSystem), dicProperty("系统类型")), HVACSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("气管管径(mm)") Then Double.TryParse(dicProperty("气管管径(mm)"), Me.GasDiameter)
            If dicProperty.ContainsKey("气管保温厚度(mm)") Then Double.TryParse(dicProperty("气管保温厚度(mm)"), Me.GasInsulationThickness)
            If dicProperty.ContainsKey("液管保温厚度(mm)") Then Double.TryParse(dicProperty("液管保温厚度(mm)"), Me.FluidInsulationThickness)
            If dicProperty.ContainsKey("是否刷漆") Then Me.IsPaint = CType([Enum].Parse(GetType(YesNoEnum), dicProperty("是否刷漆")), YesNoEnum)
            If dicProperty.ContainsKey("是否吊架") Then Me.IsHanger = CType([Enum].Parse(GetType(YesNoEnum), dicProperty("是否吊架")), YesNoEnum)
            If dicProperty.ContainsKey("是否保温") Then Me.IsInsulate = CType([Enum].Parse(GetType(YesNoEnum), dicProperty("是否保温")), YesNoEnum)
        End Sub

    End Class

    Public Class 管道
        Inherits LineCptClass
        Private _Material As String = "钢塑管"
        Private _SystemType As AzSystemType = AzSystemType.给水
        Private _diameter1 As Double = 25
        Private _diameter2 As Double = 34
        Private _insulationThickness As Double = 15
        Private _SpecialityType As AzSpecialityType = AzSpecialityType.给排水

        ''' <summary>
        ''' 管道安装位置枚举
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum InstallLocation
            室内
            室外
        End Enum

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.管道, startHeight, endHeight)
        End Sub

        Public Overloads ReadOnly Property Name As String
            Get
                MyBase.Name = Me.Material & " DN" & Me.Diameter1
                Return MyBase.Name
            End Get
        End Property

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads ReadOnly Property SpecialityType As AzSpecialityType
            Get
                Return _SpecialityType
            End Get
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads ReadOnly Property SystemType As AzSystemType
            Get
                Return _SystemType
            End Get
        End Property

        ''' <summary>
        ''' 管道的公称直径,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("管道截面公称直径,单位为:mm")>
        Public Property Diameter1 As Double
            Get
                Return Me._diameter1
            End Get
            Set(ByVal value As Double)
                If value < 0 Then Exit Property
                Me._diameter1 = Math.Round(value, calcDigit)
            End Set
        End Property

        ''' <summary>
        ''' 管道的公称外径,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("公称外径(mm)"), Description("管道截面公称外径,单位:mm")>
        Public Property Diameter2 As Double
            Get
                Return Me._diameter2
            End Get
            Set(ByVal value As Double)
                If value < 0 Then Exit Property
                Me._diameter2 = Math.Round(value, MdlCommonDeclare.calcDigit)
            End Set
        End Property

        ''' <summary>
        ''' 保温层厚值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("保温厚度(mm)"), Description("管道保温层厚度值,该属性值在""是否保温""属性为""是""的情况才生效,单位为:mm")>
        Public Property InsulationThickness As Double
            Get
                Return Me._insulationThickness
            End Get
            Set(ByVal value As Double)
                If value < 0 Then Exit Property
                Me._insulationThickness = Math.Round(value, 3)
            End Set
        End Property

        <Category("其它"), DisplayName("连接方式"), Description("管道连接方式同管道所属系统，一般不需要用户设置")>
        Public Property Junction As String = ""

        <Category("其它"), DisplayName("是否刷漆"), Description("该属性指示在工程量计算是是否要考虑刷漆")>
        Public Property IsPaint As YesNoEnum

        <Category("其它"), DisplayName("是否吊架"), Description("该属性指示在工程量计算是是否要考虑吊架")>
        Public Property IsHanger As YesNoEnum

        ''' <summary>
        ''' 是否保温
        ''' </summary>
        <Category("其它"), DisplayName("是否保温"), Description("该属性指示管道是否要计算保温工程量")>
        Public Property IsInsulate As YesNoEnum = YesNoEnum.否

        ''' <summary>
        ''' 安装位置
        ''' </summary>
        <Category("其它"), DisplayName("安装位置"), Description("该属性指示管道安装位置,取值分为室内、室外")>
        Public Property Loaction As InstallLocation = InstallLocation.室内

        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String
            Get
                Return _Material
            End Get
            Set(ByVal value As String)
                If Trim(value) = "" Then
                    Exit Property
                Else
                    _Material = Trim(value)
                End If
            End Set
        End Property

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.管道)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("公称直径(mm)") = Me.Diameter1.ToString
            dicObjProperty("公称外径(mm)") = Me.Diameter2.ToString
            dicObjProperty("保温厚度(mm)") = Me.InsulationThickness.ToString
            dicObjProperty("连接方式") = Me.Junction
            dicObjProperty("是否刷漆") = Me.IsPaint.ToString
            dicObjProperty("是否吊架") = Me.IsHanger.ToString
            dicObjProperty("是否保温") = Me.IsInsulate.ToString
            dicObjProperty("安装位置") = Me.Loaction.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me._SpecialityType = CType([Enum].Parse(GetType(AzSpecialityType), dicProperty("专业类型")), AzSpecialityType)
            If dicProperty.ContainsKey("系统类型") Then Me._SystemType = CType([Enum].Parse(GetType(AzSystemType), dicProperty("系统类型")), AzSystemType)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("公称直径(mm)") Then Double.TryParse(dicProperty("公称直径(mm)"), Me.Diameter1)
            If dicProperty.ContainsKey("公称外径(mm)") Then Double.TryParse(dicProperty("公称外径(mm)"), Me.Diameter2)
            If dicProperty.ContainsKey("保温厚度(mm)") Then Double.TryParse(dicProperty("保温厚度(mm)"), Me.InsulationThickness)
            If dicProperty.ContainsKey("连接方式") Then Me.Junction = dicProperty("连接方式")
            If dicProperty.ContainsKey("是否刷漆") Then Me.IsPaint = CType([Enum].Parse(GetType(YesNoEnum), dicProperty("是否刷漆")), YesNoEnum)
            If dicProperty.ContainsKey("是否吊架") Then Me.IsHanger = CType([Enum].Parse(GetType(YesNoEnum), dicProperty("是否吊架")), YesNoEnum)
            If dicProperty.ContainsKey("是否保温") Then Me.IsInsulate = CType([Enum].Parse(GetType(YesNoEnum), dicProperty("是否保温")), YesNoEnum)
            If dicProperty.ContainsKey("安装位置") Then Me.Loaction = CType([Enum].Parse(GetType(InstallLocation), dicProperty("安装位置")), InstallLocation)
        End Sub

    End Class

    Public Class 配线
        Inherits LineCptClass
        Private _material As String = "BV"
        Private _sectionArea As Double = 2.5
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 配线类型枚举
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum WiringType
            电线
            电缆
        End Enum

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.配线, startHeight, endHeight)
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
        <Category("其它"), DisplayName("材质"), Description("配线材质如:BV、KVV3*1.5")>
        Public Property Material As String
            Get
                Return Me._material
            End Get
            Set(value As String)
                If value.Trim = "" Then Exit Property
                Me._material = UCase(value).Trim
            End Set
        End Property

        ''' <summary>
        ''' 配线截面面积,单位:㎜²
        ''' </summary>
        <Category("尺寸"), DisplayName("截面面积(㎜²)"), Description("配线截面面积,:㎜²")>
        Public Property SectionArea As Double
            Get
                Return Me._sectionArea
            End Get
            Set(value As Double)
                If value > 0 Then
                    Me._sectionArea = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配线类型,分为电线、电缆
        ''' </summary>
        <Category("其它"), DisplayName("配线类型"), Description("指示配线所属类型,分为电线、电缆")>
        Public Property Type As WiringType = WiringType.电线

        ''' <summary>
        ''' 获取配线材质编号和型号信息,如果未解析到配线直径，则直径信息返回-1
        ''' </summary>
        ''' <param name="strWiringModel">待解析的配线规格字符串</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetWiringModelInfo(ByVal strWiringModel As String, ByRef isCombine As Boolean) As KeyValuePair(Of String, String)
            isCombine = False
            Dim matchItem = Regex.Match(strWiringModel, "(?<A>\S+)-(?<B>\d+(\.\d+)?-\d+(\.\d+)?)$") '是否符合SYWV-75-5模式的配线规格字符串
            If Not matchItem.Success Then
                matchItem = Regex.Match(strWiringModel, "(?<A>\S+)-(?<B>\d+(\.\d+)?)$") '是否符合BV-2.5模式的配线规格字符串
                isCombine = matchItem.Success
            End If
            If Not matchItem.Success Then
                matchItem = Regex.Match(strWiringModel, "(?<A>\S+)-(?<B>\d+\D\d+(\.\d+)?)$") '是否符合YJY-1*2.5模式的配线规格字符串
            End If
            If Not matchItem.Success Then
                matchItem = Regex.Match(strWiringModel, "(?<A>\S+)-(?<B>\d+\D\d+(\.\d+)?\+\d+\D\d+(\.\d+)?)$") '是否符合YJY-1*70+1*35模式的配线规格字符串
            End If
            If matchItem.Success Then
                Return New KeyValuePair(Of String, String)(matchItem.Groups("A").Value, matchItem.Groups("B").Value)
            Else
                Return New KeyValuePair(Of String, String)(strWiringModel, "")
            End If
        End Function

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.配线)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("截面面积(㎜²)") = Me.SectionArea.ToString
            dicObjProperty("配线类型") = Me.Type.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("截面面积(㎜²)") Then Double.TryParse(dicProperty("截面面积(㎜²)"), Me.SectionArea)
            If dicProperty.ContainsKey("配线类型") Then Me.Type = CType([Enum].Parse(GetType(WiringType), dicProperty("配线类型")), WiringType)
        End Sub

    End Class

    Public Class 配管
        Inherits LineCptClass
        Private _diameter As Integer = 16
        Private _material As String = "PVC"
        Private _systemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _specialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 配管敷设类别枚举
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum PipeLayCategory
            明配 = 1
            暗配 = 2
        End Enum

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.配管, startHeight, endHeight)
        End Sub

        ''' <summary>
        ''' 与构件关联的名称标识字符串
        ''' </summary>
        <Category("通用设置"), DisplayName("构件名称"), Description("与构件关联的名称标识文本")>
        Public Overloads ReadOnly Property Name As String
            Get
                MyBase.Name = Me._material & Me.Diameter & "(" & Me.LayCategory.ToString & ")"
                Return MyBase.Name
            End Get
        End Property

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型")>
        Public Overloads Property SpecialityType As ElectricalSpeciality
            Get
                Return _specialityType
            End Get
            Set(ByVal value As ElectricalSpeciality)
                _specialityType = value
                MyBase.SpecialityType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 构件所属的系统类型
        ''' </summary>
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型")>
        Public Overloads Property SystemType As ElectricalSystem
            Get
                Return _systemType
            End Get
            Set(ByVal value As ElectricalSystem)
                _systemType = value
                MyBase.SystemType = value.ToString
            End Set
        End Property

        ''' <summary>
        ''' 公称直径,单位：mm
        ''' </summary>
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("管道截面公称直径,单位为:mm")>
        Public Property Diameter As Integer
            Get
                Return Me._diameter
            End Get
            Set(value As Integer)
                If value > 0 Then
                    Me._diameter = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配管敷设类别，分为明配、暗配
        ''' </summary>
        <Category("配管"), DisplayName("敷设类别"), Description("配管敷设方式分为暗配、明配,系统会根据用户输入的敷设方式自动进行判别,一般不需要用户设置")>
        Public Property LayCategory As PipeLayCategory

        ''' <summary>
        ''' 配管材质信息
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String
            Get
                Return Me._material
            End Get
            Set(ByVal value As String)
                If Trim(value) = "" Then
                    Exit Property
                Else
                    Me._material = UCase(Trim(value))
                End If
            End Set
        End Property

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.配管)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("敷设类别") = Me.LayCategory.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("敷设类别") Then Me.LayCategory = CType([Enum].Parse(GetType(PipeLayCategory), dicProperty("敷设类别")), PipeLayCategory)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
        End Sub

    End Class

    Public Class 金属软管
        Inherits LineCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 金属软管长度类型
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum FlexiblePipeLengthType
            一米以内
            两米以内
        End Enum

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.金属软管, startHeight, endHeight)
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
        ''' 长度范围
        ''' </summary>
        <Category("尺寸"), DisplayName("长度范围"), Description("指示金属软管的长度范围，分为1000mm以内和2000mm以内")>
        Public Property LengthType As FlexiblePipeLengthType = FlexiblePipeLengthType.一米以内

        ''' <summary>
        ''' 公称直径
        ''' </summary>
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("金属软管截面公称直径,单位为:mm")>
        Public Property Diameter As Integer = 16

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.金属软管)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("长度范围") = Me.LengthType.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("长度范围") Then Me.LengthType = CType([Enum].Parse(GetType(FlexiblePipeLengthType), dicProperty("长度范围")), FlexiblePipeLengthType)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
        End Sub

    End Class

    Public Class 桥架
        Inherits LineCptClass
        Private _height As Integer = 200
        Private _Width As String = "200"
        Private _Material As String = "CT"
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.桥架, startHeight, endHeight)
        End Sub

        ''' <summary>
        ''' 与构件关联的名称标识字符串
        ''' </summary>
        <Category("通用设置"), DisplayName("构件名称"), Description("与构件关联的名称标识文本")>
        Public Overloads ReadOnly Property Name As String
            Get
                If IsNumeric(Me.Width) Then
                    MyBase.Name = Me.Material & Me.Width & "×" & Me.Height
                Else
                    MyBase.Name = Me.Material & "(" & Me.Width & ")×" & Me.Height
                End If
                Return MyBase.Name
            End Get
        End Property

        ''' <summary>
        ''' 构件所属的专业类型
        ''' </summary>
        <Category("通用设置"), DisplayName("专业类型"), Description("指示构件所属的专业类型"), Browsable(False)>
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
        <Category("通用设置"), DisplayName("系统类型"), Description("指示构件所属的系统类型"), Browsable(False)>
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
        ''' 桥架宽度值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("宽度(mm)"), Description("桥架截面宽度值,支持格式:300、200+100,单位:mm")>
        Public Property Width As String
            Get
                Return _Width
            End Get
            Set(ByVal value As String)
                If Regex.IsMatch(value, "^\d+((\+\d+)+)?$") Then
                    _Width = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' 桥架截面高度值,单位:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("高度(mm)"), Description("桥架截面高度值,单位:mm")>
        Public Property Height As Integer
            Get
                Return Me._height
            End Get
            Set(value As Integer)
                If value > 0 Then
                    Me._height = value
                End If
            End Set
        End Property

        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String
            Get
                Return _Material
            End Get
            Set(ByVal value As String)
                If Trim(value) <> "" Then
                    _Material = UCase(value.Trim)
                End If
            End Set
        End Property

        <Category("尺寸"), DisplayName("截面周长(mm)"), Description("桥架截面周长,其值由""宽度+高度""组成,由系统根据桥架截面尺寸自动计算,单位:mm")>
        Public ReadOnly Property Perimeter As Integer
            Get
                Dim calcValue As Double
                StrOperate.CalcSimpleStrValue3(Me.Width, calcValue)
                Return CInt(calcValue) + Me.Height
            End Get
        End Property

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.桥架)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("宽度(mm)") = Me.Width.ToString
            dicObjProperty("高度(mm)") = Me.Height.ToString
            dicObjProperty("截面周长(mm)") = Me.Perimeter.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("宽度(mm)") Then Me.Width = dicProperty("宽度(mm)")
            If dicProperty.ContainsKey("高度(mm)") Then Integer.TryParse(dicProperty("高度(mm)"), Me.Height)
            If dicProperty.ContainsKey("截面周长(mm)") Then Integer.TryParse(dicProperty("截面周长(mm)"), Me.Perimeter)
        End Sub

    End Class

    Public Class 配线配管
        Inherits LineCptClass
        Private _LayType As String = "CC"
        Private _DuctModel As String = "PVC16"
        Private _WiringModel1 As String = "BV-2.5"
        Private _WiringModel2 As String = ""
        Private _WiringCount1 As Integer = 3
        Private _WiringCount2 As Integer = 0
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.配线配管, startHeight, endHeight)
        End Sub

        ''' <summary>
        ''' 与构件关联的名称标识字符串
        ''' </summary>
        <Category("通用设置"), DisplayName("构件名称"), Description("与构件关联的名称标识文本")>
        Public Overloads ReadOnly Property Name As String
            Get
                If Me.WiringModel1 = "" AndAlso Me.WiringModel2 = "" Then
                    MyBase.Name = ""
                Else
                    MyBase.Name = StrOperate.GetWiringMarkText(Me.WiringModel1, Me.WiringModel2, Me.WiringCount1, Me.WiringCount2) & "/" & Me.DuctModel
                End If
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
        ''' 配管规格
        ''' </summary>
        <Category("配管"), DisplayName("配管规格"), Description("配管规格如:PVC16")>
        Public Property DuctModel As String
            Get
                Return _DuctModel
            End Get
            Set(ByVal value As String)
                value = Trim(value)
                If Regex.IsMatch(value, "\D\d+$") Then
                    _DuctModel = UCase(value)
                Else
                    MsgBox("属性格式不合法，配管规格格式为PVC16", , "提示")
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配线配管敷设方式
        ''' </summary>
        <Category("配管"), DisplayName("敷设方式"), Description("配管敷设方式如:CC、WC,FC,有多种敷设类别，中间用逗号隔开")>
        Public Property LayType As String
            Get
                Return _LayType
            End Get
            Set(ByVal value As String)
                If value.Trim = "" Then Exit Property
                Me._LayType = UCase(value.Trim)
                Dim isOutLay As Boolean = False
                Dim layTypes = _LayType.Split(CChar(","), CChar("，"))
                For Each curType In layTypes
                    If Mid(RTrim(curType), RTrim(curType).Length) = "E" Then
                        isOutLay = True
                        Me.LayCategory = 配管.PipeLayCategory.明配
                        Exit For
                    End If
                Next
                If Not isOutLay Then Me.LayCategory = 配管.PipeLayCategory.暗配
            End Set
        End Property

        ''' <summary>
        ''' 配线配管敷设类别
        ''' </summary>
        <Category("配管"), DisplayName("敷设类别"), Description("配管敷设方式分为暗配、明配,系统会根据用户输入的敷设方式自动进行判别,一般不需要用户设置")>
        Public Property LayCategory As 配管.PipeLayCategory = 配管.PipeLayCategory.暗配

        ''' <summary>
        ''' 配线规格1
        ''' </summary>
        <Category("配线"), DisplayName("配线规格1"), Description("配线规格如:BV-2.5、KVV3*2.5,当该属性值不能为空")>
        Public Property WiringModel1 As String
            Get
                Return Me._WiringModel1
            End Get
            Set(ByVal value As String)
                If Trim(value) = "" Then
                    MsgBox("配线规格1值不能为空", , "配线配管")
                Else
                    _WiringModel1 = UCase(Trim(value))
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配线根数1
        ''' </summary>
        <Category("配线"), DisplayName("配线根数1"), Description("配线根数，一般电缆取1，该值必须为大于0的整数")>
        Public Property WiringCount1 As Integer
            Get
                Return Me._WiringCount1
            End Get
            Set(ByVal value As Integer)
                If value > 0 Then
                    Me._WiringCount1 = value
                Else
                    MsgBox("配线根数1值必须为大于0的整数", , "配线配管")
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配线规格2
        ''' </summary>
        <Category("配线"), DisplayName("配线规格2"), Description("配线规格如:BV-2.5、KVV3*2.5,当该属性值空时对应的配线根数将被同步设置为0")>
        Public Property WiringModel2 As String
            Get
                Return _WiringModel2
            End Get
            Set(ByVal value As String)
                If Trim(value) = "" Then
                    Me.WiringCount2 = 0
                Else
                    Me._WiringModel2 = UCase(Trim(value))
                    If Me.WiringCount2 = 0 Then Me.WiringCount2 = 1
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配线根数2
        ''' </summary>
        <Category("配线"), DisplayName("配线根数2"), Description("配线根数，一般电缆取1，当值为0时对应配线规格信息将被同步设置为空值")>
        Public Property WiringCount2 As Integer
            Get
                Return _WiringCount2
            End Get
            Set(ByVal value As Integer)
                If value > 0 AndAlso Me.WiringModel2 = "" Then
                    Exit Property
                ElseIf value < 0 Then
                    MsgBox("配线根数值不能为负。", , "提示")
                    Exit Property
                ElseIf value = 0 AndAlso _WiringCount1 = 0 Then
                    Exit Property
                Else
                    If value = 0 Then _WiringModel2 = ""
                    _WiringCount2 = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配线类型,分为电线、电缆
        ''' </summary>
        <Category("其它"), DisplayName("配线类型"), Description("指示配线所属类型,分为电线、电缆")>
        Public Property Type As WiringType = WiringType.电线

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.配线配管)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("敷设方式") = Me.LayType
            dicObjProperty("敷设类别") = Me.LayCategory.ToString
            dicObjProperty("配管规格") = Me.DuctModel
            dicObjProperty("配线规格1") = Me.WiringModel1
            dicObjProperty("配线规格2") = Me.WiringModel2
            dicObjProperty("配线根数1") = Me.WiringCount1.ToString
            dicObjProperty("配线根数2") = Me.WiringCount2.ToString
            dicObjProperty("配线类型") = Me.Type.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("敷设方式") Then Me.LayType = dicProperty("敷设方式")
            If dicProperty.ContainsKey("敷设类别") Then Me.LayCategory = CType([Enum].Parse(GetType(配管.PipeLayCategory), dicProperty("敷设类别")), 配管.PipeLayCategory)
            If dicProperty.ContainsKey("配管规格") Then Me.DuctModel = dicProperty("配管规格")
            If dicProperty.ContainsKey("配线规格1") Then Me.WiringModel1 = dicProperty("配线规格1")
            If dicProperty.ContainsKey("配线规格2") Then Me.WiringModel2 = dicProperty("配线规格2")
            If dicProperty.ContainsKey("配线根数1") Then Integer.TryParse(dicProperty("配线根数1"), Me.WiringCount1)
            If dicProperty.ContainsKey("配线根数2") Then Integer.TryParse(dicProperty("配线根数2"), Me.WiringCount2)
            If dicProperty.ContainsKey("配线类型") Then Me.Type = CType([Enum].Parse(GetType(WiringType), dicProperty("配线类型")), WiringType)
        End Sub

    End Class

    Public Class 桥架配线
        Inherits LineCptClass
        Private _WiringModel1 As String = "BV-2.5"
        Private _WiringModel2 As String = ""
        Private _WiringCount1 As Integer = 3
        Private _WiringCount2 As Integer = 0
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.桥架配线, startHeight, endHeight)
        End Sub

        ''' <summary>
        ''' 与构件关联的名称标识字符串
        ''' </summary>
        <Category("通用设置"), DisplayName("构件名称"), Description("与构件关联的名称标识文本")>
        Public Overloads ReadOnly Property Name As String
            Get
                If Me.WiringModel1 = "" AndAlso Me.WiringModel2 = "" Then
                    MyBase.Name = ""
                Else
                    MyBase.Name = StrOperate.GetWiringMarkText(Me.WiringModel1, Me.WiringModel2, Me.WiringCount1, Me.WiringCount2).Replace("(CT)", "") & "(CT)"
                End If
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
        ''' 桥架配线敷设方式
        ''' </summary>
        <Category("配线"), DisplayName("敷设方式"), Description("配管敷设方式如:CC、WC,FC,有多种敷设类别，中间用逗号隔开")>
        Public Property LayType As String = "CT"

        ''' <summary>
        ''' 配线规格1
        ''' </summary>
        <Category("配线"), DisplayName("配线规格1"), Description("配线规格如:BV-2.5、KVV3*2.5,当该属性值不能为空")>
        Public Property WiringModel1 As String
            Get
                Return Me._WiringModel1
            End Get
            Set(ByVal value As String)
                If Trim(value) = "" Then
                    MsgBox("配线规格1值不能为空", , "桥架配线")
                Else
                    _WiringModel1 = UCase(Trim(value))
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配线根数1
        ''' </summary>
        <Category("配线"), DisplayName("配线根数1"), Description("配线根数，一般电缆取1，该值必须为大于0的整数")>
        Public Property WiringCount1 As Integer
            Get
                Return Me._WiringCount1
            End Get
            Set(ByVal value As Integer)
                If value > 0 Then
                    Me._WiringCount1 = value
                Else
                    MsgBox("配线根数1值必须为大于0的整数", , "桥架配线")
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配线规格2
        ''' </summary>
        <Category("配线"), DisplayName("配线规格2"), Description("配线规格如:BV-2.5、KVV3*2.5,当该属性值空时对应的配线根数将被同步设置为0")>
        Public Property WiringModel2 As String
            Get
                Return _WiringModel2
            End Get
            Set(ByVal value As String)
                If Trim(value) = "" AndAlso Me.WiringModel1 = "" Then
                    Exit Property
                Else
                    _WiringModel2 = UCase(Trim(value))
                    If Me.WiringCount2 = 0 Then Me.WiringCount2 = 1
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配线根数2
        ''' </summary>
        <Category("配线"), DisplayName("配线根数2"), Description("配线根数，一般电缆取1，当值为0时对应配线规格信息将被同步设置为空值")>
        Public Property WiringCount2 As Integer
            Get
                Return _WiringCount2
            End Get
            Set(ByVal value As Integer)
                If value > 0 AndAlso Me.WiringModel2 = "" Then
                    Exit Property
                ElseIf value < 0 Then
                    MsgBox("配线根数值不能为负。", , "提示")
                    Exit Property
                ElseIf value = 0 AndAlso _WiringCount1 = 0 Then
                    Exit Property
                Else
                    If value = 0 Then _WiringModel2 = ""
                    _WiringCount2 = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' 配线类型,分为电线、电缆
        ''' </summary>
        <Category("其它"), DisplayName("配线类型"), Description("指示配线所属类型,分为电线、电缆")>
        Public Property Type As WiringType = WiringType.电线

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.桥架配线)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("敷设方式") = Me.LayType
            dicObjProperty("配线规格1") = Me.WiringModel1
            dicObjProperty("配线规格2") = Me.WiringModel2
            dicObjProperty("配线根数1") = Me.WiringCount1.ToString
            dicObjProperty("配线根数2") = Me.WiringCount2.ToString
            dicObjProperty("配线类型") = Me.Type.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("敷设方式") Then Me.LayType = dicProperty("敷设方式")
            If dicProperty.ContainsKey("配线规格1") Then Me.WiringModel1 = dicProperty("配线规格1")
            If dicProperty.ContainsKey("配线规格2") Then Me.WiringModel2 = dicProperty("配线规格2")
            If dicProperty.ContainsKey("配线根数1") Then Integer.TryParse(dicProperty("配线根数1"), Me.WiringCount1)
            If dicProperty.ContainsKey("配线根数2") Then Integer.TryParse(dicProperty("配线根数2"), Me.WiringCount2)
            If dicProperty.ContainsKey("配线类型") Then Me.Type = CType([Enum].Parse(GetType(WiringType), dicProperty("配线类型")), WiringType)
        End Sub

    End Class

    Public Class 风机盘管
        Inherits LineCptClass
        Private _SystemType As HVACSystem = HVACSystem.送风
        Private _SpecialityType As HVACSpeciality = HVACSpeciality.空调风

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.风机盘管, startHeight, endHeight)
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
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.风机盘管)
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

    Public Class 防雷接地线
        Inherits LineCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.防雷接地线, startHeight, endHeight)
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

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.防雷接地线)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
        End Sub

    End Class

    Public Class 避雷引下线
        Inherits LineCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 避雷引下线敷设方式类型
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum ThunderUnderLineLocation
            沿建筑物
            沿金属构件
            沿建筑主筋
            其它方式
        End Enum

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.避雷引下线, startHeight, endHeight)
        End Sub

        ''' <summary>
        ''' 构件名称
        ''' </summary>
        <Category("通用"), DisplayName("构件名称"), Description("指示避雷引下线关联的名称表示文本,由系统自动设定")>
        Public Overloads ReadOnly Property Name As String
            Get
                MyBase.Name = (Me.ComponentType.ToString & " " & Me.LayType.ToString & "引下").Trim
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
        ''' 敷设方式
        ''' </summary>
        <Category("位置"), DisplayName("敷设方式"), Description("指示避雷引下线的敷设方式,一般取沿建筑主筋")>
        Public Property LayType As ThunderUnderLineLocation = ThunderUnderLineLocation.沿建筑主筋

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.避雷引下线)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("敷设方式") = Me.LayType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("敷设方式") Then Me.LayType = CType([Enum].Parse(GetType(ThunderUnderLineLocation), dicProperty("敷设方式")), ThunderUnderLineLocation)
        End Sub

    End Class

    Public Class 避雷网
        Inherits LineCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 避雷网敷设方式类型枚举
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum ThunderGridLayType
            沿折板
            沿混凝土块
        End Enum

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.避雷网, startHeight, endHeight)
        End Sub

        ''' <summary>
        ''' 构件名称
        ''' </summary>
        <Category("通用"), DisplayName("构件名称"), Description("指示避雷网关联的名称表示文本,由系统自动设定")>
        Public Overloads ReadOnly Property Name As String
            Get
                MyBase.Name = (Me.ComponentType.ToString & " " & Me.LayType.ToString & " " & Me.Material).Trim
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
        <Category("其它"), DisplayName("材质"), Description("指示避雷引下线的材质信息")>
        Public Property Material As String = "φ12热镀锌圆钢"

        ''' <summary>
        ''' 敷设方式
        ''' </summary>
        <Category("其它"), DisplayName("敷设方式"), Description("指示避雷网敷设方式，一般取沿折板")>
        Public Property LayType As ThunderGridLayType = ThunderGridLayType.沿折板

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.避雷网)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("敷设方式") = Me.LayType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("敷设方式") Then Me.LayType = CType([Enum].Parse(GetType(ThunderGridLayType), dicProperty("敷设方式")), ThunderGridLayType)
        End Sub

    End Class

    Public Class 均压环
        Inherits LineCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 均压环计量单位类型
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum EqualRingUnit
            米
            平方米
        End Enum

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.均压环, startHeight, endHeight)
        End Sub

        ''' <summary>
        ''' 构件名称
        ''' </summary>
        <Category("通用"), DisplayName("构件名称"), Description("指示均压环关联的名称表示文本,由系统自动设定")>
        Public Overloads ReadOnly Property Name As String
            Get
                MyBase.Name = (Me.ComponentType.ToString & " " & Me.Material).Trim
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
        <Category("其它"), DisplayName("材质"), Description("指示构件的材质信息")>
        Public Property Material As String = "40×4镀锌扁钢"

        ''' <summary>
        ''' 均压环计量单位
        ''' </summary>
        <Category("其它"), DisplayName("计量单位"), Description("均压环的计量单位,一般取米")>
        Public Property MeasureUnit As EqualRingUnit = EqualRingUnit.米

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.均压环)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("计量单位") = Me.MeasureUnit.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("计量单位") Then Me.MeasureUnit = CType([Enum].Parse(GetType(EqualRingUnit), dicProperty("计量单位")), EqualRingUnit)
        End Sub

    End Class

    Public Class 接地母线
        Inherits LineCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.防雷接地
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 防雷接地母线安装位置
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum BusBarLoaction
            户内
            户外
        End Enum

        ''' <summary>
        ''' 接地母线敷设类型
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum BusBarLayType
            明敷
            暗敷
        End Enum

        Public Sub New(Optional ByVal startHeight As Integer = 0, Optional ByVal endHeight As Integer = 0)
            MyBase.New(AzComponentType.接地母线, startHeight, endHeight)
        End Sub

        ''' <summary>
        ''' 构件名称
        ''' </summary>
        <Category("通用"), DisplayName("构件名称"), Description("指示接地母线关联的名称表示文本,由系统自动设定")>
        Public Overloads ReadOnly Property Name As String
            Get
                MyBase.Name = (Me.Location.ToString & Me.ComponentType.ToString & " " & Me.LayType.ToString & " " & Me.Material).Trim
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
        <Category("其它"), DisplayName("材质"), Description("指示避雷引下线的材质信息")>
        Public Property Material As String = "40×4镀锌扁钢"

        ''' <summary>
        ''' 安装位置
        ''' </summary>
        <Category("位置"), DisplayName("安装位置"), Description("指示防雷接地母线的安装位置,取户内、户外")>
        Public Property Location As BusBarLoaction = BusBarLoaction.户内

        ''' <summary>
        ''' 敷设方式
        ''' </summary>
        <Category("其它"), DisplayName("敷设方式"), Description("指示避雷网敷设方式,一般采用明敷设方式")>
        Public Property LayType As BusBarLayType = BusBarLayType.明敷

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.接地母线)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("安装位置") = Me.Location.ToString
            dicObjProperty("敷设方式") = Me.LayType.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("安装位置") Then Me.Location = CType([Enum].Parse(GetType(BusBarLoaction), dicProperty("安装位置")), BusBarLoaction)
            If dicProperty.ContainsKey("敷设方式") Then Me.LayType = CType([Enum].Parse(GetType(BusBarLayType), dicProperty("敷设方式")), BusBarLayType)
        End Sub

    End Class

End Namespace
