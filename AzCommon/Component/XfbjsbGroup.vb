Imports System.ComponentModel

Namespace Component.Xfbjsb

    Public Class 报警设备
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.消防报警
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.弱电

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.消防报警设备, AzComponentType.报警设备, installHeight)
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
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.报警设备)
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

    Public Class 控制模块
        Inherits PointCptClass
        Private _SystemType As ElectricalSystem = ElectricalSystem.消防报警
        Private _SpecialityType As ElectricalSpeciality = ElectricalSpeciality.弱电

        Public Sub New(Optional ByVal InstallHeight As Double = 0)
            MyBase.New(AzComponentGroup.消防报警设备, AzComponentType.控制模块, installHeight)
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
            Dim dicObjProperty = ComponentProperty.GetDefautCptProperty(AzComponentType.控制模块)
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
