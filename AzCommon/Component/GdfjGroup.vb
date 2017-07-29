Imports System.ComponentModel

Namespace Component.Gdfj

    Public Class 沟槽连接件
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.给水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.管道附件, AzComponentType.沟槽连接件, installHeight)
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
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.沟槽连接件)
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

    Public Class 管道连接件
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.给水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.管道附件, AzComponentType.管道连接件, installHeight)
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

        <Category("其它"), Description("指示连接件是否异径,一般由系统自动设置")>
        Public IsReducer As YesNoEnum = YesNoEnum.是

        <Category("其它"), Description("指示连接件是否为机械式,一般由系统自动设置")>
        Public IsMechanical As YesNoEnum = YesNoEnum.是

        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("连接件所附着管道的截面公称直径，通常由系统自动识别不需要用户设置,单位为:mm")>
        Public Property Diameter As Integer

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.管道连接件)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            dicObjProperty("是否异径") = Me.IsReducer.ToString
            dicObjProperty("是否机械式") = Me.IsMechanical.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
            If dicProperty.ContainsKey("是否异径") Then Me.IsReducer = CType([Enum].Parse(GetType(YesNoEnum), dicProperty("是否异径")), YesNoEnum)
            If dicProperty.ContainsKey("是否机械式") Then Me.IsMechanical = CType([Enum].Parse(GetType(YesNoEnum), dicProperty("是否机械式")), YesNoEnum)
        End Sub

    End Class

    Public Class 管道配件
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.给水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.管道附件, AzComponentType.管道配件, installHeight)
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

        <Category("其它"), DisplayName("材质"), Description("构件的材质信息,一般不需要此属性")>
        Public Property Material As String = ""

        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("风口、风帽、风罩、阀门所附着的风管的截面公称直径,矩形风管取长边尺寸，通常由系统自动识别不需要用户设置,单位为:mm")>
        Public Property Diameter As Integer

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.管道配件)
            dicObjProperty("构件名称") = Me.Name
            dicObjProperty("专业类型") = Me.SpecialityType.ToString
            dicObjProperty("系统类型") = Me.SystemType.ToString
            dicObjProperty("材质") = Me.Material
            dicObjProperty("公称直径(mm)") = Me.Diameter.ToString
            Return dicObjProperty
        End Function

        Public Overrides Sub InitComponentProperty(ByVal dicProperty As System.Collections.Generic.Dictionary(Of String, String))
            If dicProperty.ContainsKey("构件名称") Then Me.Name = dicProperty("构件名称")
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
        End Sub

    End Class

    Public Class 管道阀门
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.给水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.管道附件, AzComponentType.管道阀门, installHeight)
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

        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("风口、风帽、风罩、阀门所附着的风管的截面公称直径,矩形风管取长边尺寸，通常由系统自动识别不需要用户设置,单位为:mm")>
        Public Property Diameter As Integer

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.管道阀门)
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

    Public Class 管道套管
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.给水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

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
            MyBase.New(AzComponentGroup.管道附件, AzComponentType.管道套管, installHeight)
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
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.管道套管)
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
            If dicProperty.ContainsKey("专业类型") Then Me.SpecialityType = CType([Enum].Parse(GetType(WaterSpeciality), dicProperty("专业类型")), WaterSpeciality)
            If dicProperty.ContainsKey("系统类型") Then Me.SystemType = CType([Enum].Parse(GetType(WaterSystem), dicProperty("系统类型")), WaterSystem)
            If dicProperty.ContainsKey("材质") Then Me.Material = dicProperty("材质")
            If dicProperty.ContainsKey("公称直径(mm)") Then Integer.TryParse(dicProperty("公称直径(mm)"), Me.Diameter)
            If dicProperty.ContainsKey("套管类别") Then Me.Category = CType([Enum].Parse(GetType(SleeveCategory), dicProperty("套管类别")), SleeveCategory)
            If dicProperty.ContainsKey("安装位置") Then Me.Loaction = CType([Enum].Parse(GetType(SleeveLocation), dicProperty("安装位置")), SleeveLocation)
        End Sub

    End Class

    Public Class 仪表装置
        Inherits PointCptClass
        Private _SystemType As WaterSystem = WaterSystem.给水
        Private _SpecialityType As WaterSpeciality = WaterSpeciality.给排水

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.管道附件, AzComponentType.仪表装置, installHeight)
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

        <Category("尺寸"), DisplayName("公称直径(mm)"), Description("风口、风帽、风罩、阀门所附着的风管的截面公称直径,矩形风管取长边尺寸，通常由系统自动识别不需要用户设置,单位为:mm")>
        Public Property Diameter As Integer

        Public Overrides Function GetComponentProperty() As System.Collections.Generic.Dictionary(Of String, String)
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.仪表装置)
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
