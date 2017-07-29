Imports System.ComponentModel

Namespace Component.Cnsb

    Public Class 散热器
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.采暖蒸汽
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.采暖

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.采暖设备, AzComponentType.散热器, installHeight)
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
        ''' 散热器宽度值,单位为:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("宽度(mm)"), Description("散热器宽度值,单位为:mm")>
        Public Property Width As Integer

        ''' <summary>
        ''' 散热器高度值,单位为:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("高度(mm)"), Description("散热器高度值,单位为:mm")>
        Public Property Height As Integer

        ''' <summary>
        ''' 散热器厚度值,单位为:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("厚度(mm)"), Description("散热器厚度值,单位为:mm")>
        Public Property Thickness As Integer

        ''' <summary>
        ''' 散热器公称直径值,单位为:mm
        ''' </summary>
        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("散热器公称直径值,单位为:mm")>
        Public Property Diameter As Integer

        ''' <summary>
        ''' 指示组成构件的材料类型
        ''' </summary>
        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String = ""

        ''' <summary>
        ''' 规格型号
        ''' </summary>
        <Category("其它"), DisplayName("规格型号"), Description("散热器的规格型号信息")>
        Public Property Model As String = ""

        ''' <summary>
        ''' 散热器片数
        ''' </summary>
        <Category("其它"), DisplayName("片数(片)"), Description("散热器的片数信息")>
        Public Property PieceCount As Integer = 3

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("宽度(mm)") Then Integer.TryParse(dicProperty("宽度(mm)"), Me.Width)
            If dicProperty.ContainsKey("高度(mm)") Then Integer.TryParse(dicProperty("高度(mm)"), Me.Height)
            If dicProperty.ContainsKey("厚度(mm)") Then Integer.TryParse(dicProperty("厚度(mm)"), Me.Thickness)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("规格型号") Then Me.Model = dicProperty("规格型号")
            If dicProperty.ContainsKey("片数(片)") Then Integer.TryParse(dicProperty("片数(片)"), Me.PieceCount)
        End Sub

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.散热器)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("宽度(mm)") = Me.Width.ToString
            dicObjProperty("高度(mm)") = Me.Height.ToString
            dicObjProperty("厚度(mm)") = Me.Thickness.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("规格型号") = Me.Model
            dicObjProperty("片数(片)") = Me.PieceCount.ToString
            Return dicObjProperty
        End Function
    End Class

End Namespace