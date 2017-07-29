Imports System.ComponentModel

Namespace Component.Dqfj

    Public Class 接线盒
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 接线盒材质类型
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum JunctionBoxMaterial
            钢制
            塑料
        End Enum

        ''' <summary>
        ''' 接线盒类型
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum JunctionBoxType
            灯头盒
            接线盒
            开关插座盒
        End Enum

        Public Sub New(Optional ByVal InstallHeight As Double = 1400)
            MyBase.New(AzComponentGroup.电气设备, AzComponentType.接线盒, installHeight)
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
        ''' 接线盒的类型
        ''' </summary>
        <Category("其它"), DisplayName("类型"), Description("接线盒的类型,分为灯头盒、接线盒、开关插座盒")>
        Public Property Type As JunctionBoxType = JunctionBoxType.灯头盒

        ''' <summary>
        ''' 指示组成构件的材料类型
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("接线盒的材质信息,分为钢制和塑料")>
        Public Property Material As JunctionBoxMaterial = JunctionBoxMaterial.塑料

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.接线盒)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("类型") = Me.Type.ToString
            dicObjProperty("材质") = Me.Material.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("类型") Then Me.Type = CType([Enum].Parse(GetType(JunctionBoxType), dicProperty("类型")), JunctionBoxType)
            If dicProperty.ContainsKey("材质") Then Me.Material = CType([Enum].Parse(GetType(JunctionBoxMaterial), dicProperty("材质")), JunctionBoxMaterial)
        End Sub

    End Class

    Public Class 电缆头
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.电气设备, AzComponentType.电缆头, installHeight)
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
        ''' 电缆头材质信息
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("电缆头材质信息，一般取与之关联的电缆材质")>
        Public Property Material As String = "BV"

        ''' <summary>
        ''' 电缆头截面面积,单位:㎜²
        ''' </summary>
        <Category("尺寸"), DisplayName("截面面积(㎜²)"), Description("电缆头截面面积,一般取与之关联的电缆截面面积,单位:㎜²")>
        Public Property SectionArea As Double = 2.5

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.电缆头)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("截面面积(㎜²)") = Me.SectionArea.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("截面面积(㎜²)") Then Double.TryParse(dicProperty("截面面积(㎜²)"), SectionArea)
        End Sub

    End Class

    Public Class 套管
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.照明插座
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.强电

        ''' <summary>
        ''' 套管类别枚举
        ''' </summary>
        Public Enum SleeveCategory
            刚性
            普通
        End Enum

        ''' <summary>
        ''' 套管安装位置
        ''' </summary>
        Public Enum SleeveLocation
            穿墙
            穿梁
            穿楼板
        End Enum

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.电气附件, AzComponentType.套管, installHeight)
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
        ''' 指示组成构件的材料类型
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String = ""

        ''' <summary>
        ''' 公称直径,单位：mm
        ''' </summary>
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("套管截面公称直径,单位为:mm")>
        Public Property Diameter As Integer = 16

        ''' <summary>
        ''' 套管类型,分为刚性和普通
        ''' </summary>
        <Category("其它"), DisplayName("套管类别"), Description("指示套管所属的类型,分为刚性和普通")>
        Public Property Category As SleeveCategory = SleeveCategory.普通

        ''' <summary>
        ''' 套管安装位置
        ''' </summary>
        <Category("其它"), DisplayName("安装位置"), Description("指示套管所在的位置,分为穿墙、穿梁、穿楼板")>
        Public Property Loaction As SleeveLocation = SleeveLocation.穿墙

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.套管)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            dicObjProperty("套管类别") = Me.Category.ToString
            dicObjProperty("安装位置") = Me.Loaction.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(ElectricalSpeciality), dicProperty("专业类型")), ElectricalSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(ElectricalSystem), dicProperty("系统类型")), ElectricalSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
            If dicProperty.ContainsKey("套管类别") Then Me.Category = CType([Enum].Parse(GetType(SleeveCategory), dicProperty("套管类别")), SleeveCategory)
            If dicProperty.ContainsKey("安装位置") Then Me.Loaction = CType([Enum].Parse(GetType(SleeveLocation), dicProperty("安装位置")), SleeveLocation)
        End Sub

    End Class

End Namespace
