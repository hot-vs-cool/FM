
''' <summary>
''' CAD控件系统变量控制类
''' </summary>
''' <remarks></remarks>
Public Class MxCadSystemVar

    ''' <summary>
    ''' 布尔类型的系统变量名称枚举
    ''' </summary>
    Public Enum MxblnSysVarName As Byte
        ''' <summary>
        ''' 栅格捕捉
        ''' </summary>
        GRIDMODE
        ''' <summary>
        ''' 正交
        ''' </summary>
        ORTHOMODE
        ''' <summary>
        ''' 对象捕捉
        ''' </summary>
        OSMODE
        ''' <summary>
        ''' 动态追踪
        ''' </summary>
        DYNTRACE
        ''' <summary>
        ''' 动态输入
        ''' </summary>
        DYNINPUT
        ''' <summary>
        ''' 线宽显示
        ''' </summary>
        LWDISPLAY
    End Enum

    ''' <summary>
    ''' 获取指定的CAD控件布尔类型系统变量值
    ''' </summary>
    ''' <param name="mxCadObj">待获取系统变量值的CAD控件</param>
    ''' <param name="sysVarType">布尔类型系统变量枚举</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBlnSysVar(ByVal mxCadObj As AxMxDrawXLib.AxMxDrawX, ByVal sysVarType As MxblnSysVarName) As Boolean
        Dim blnValue As Integer = Nothing
        If mxCadObj.GetSysVarLong(sysVarType.ToString, blnValue) Then
            If blnValue = 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 设置指定类型的CAD控件布尔类型的系统变量值
    ''' </summary>
    ''' <param name="mxCadObj">待设置系统变量的CAD控件</param>
    ''' <param name="sysVarName">系统变量类型枚举</param>
    ''' <param name="isOpen">指示是否启用该变量名对应的系统功能;"True"启用,"False"停用</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SetBlnSysVar(ByVal mxCadObj As AxMxDrawXLib.AxMxDrawX, ByVal sysVarName As MxblnSysVarName, ByVal isOpen As Boolean) As Boolean
        If isOpen Then
            Return mxCadObj.SetSysVarLong(sysVarName.ToString, 1)
        Else
            Return mxCadObj.SetSysVarLong(sysVarName.ToString, 0)
        End If
    End Function

End Class
