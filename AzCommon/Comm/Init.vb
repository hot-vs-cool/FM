
''' <summary>
''' 项目引用初始化
''' </summary>
Public Class Init

    ''' <summary>
    ''' 初始化工程数据文件
    ''' </summary>
    ''' <param name="db">工程项目数据库</param>
    ''' <remarks></remarks>
    Public Shared Sub InitProjDB(ByRef db As SqlDB.DbClass)
        MdlCommonDeclare.g_db = db
    End Sub

    ''' <summary>
    ''' 初始化系统配置数据文件
    ''' </summary>
    ''' <param name="mxCadObj">主CAD对象</param>
    ''' <param name="xmlConfig">系统配置文档</param>
    ''' <remarks></remarks>
    Public Shared Sub InitSysDataConfig(ByRef mxCadObj As AxMxDrawXLib.AxMxDrawX, ByRef xmlConfig As Xml.XmlDocument)
        MxMainCadObj = mxCadObj
        xmlSysDataConfig = xmlConfig
    End Sub

    ''' <summary>
    ''' 更新工程配置数据文件
    ''' </summary>
    ''' <param name="xmlProj">工程配置文档对象</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateProjDataConfig(ByRef xmlProj As Xml.XmlDocument)
        MdlCommonDeclare.XmlProjectConfig = xmlProj
    End Sub

    ''' <summary>
    ''' 更新全局线宽值
    ''' </summary>
    ''' <param name="globalWidth"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateGlobalWidth(ByVal globalWidth As Byte)
        MdlCommonDeclare.GlobalWidth = globalWidth
    End Sub

End Class
