Module MdlCommonDeclare

    ''' <summary>
    ''' 计算结果精确位数
    ''' </summary>
    ''' <remarks></remarks>
    Public calcDigit As Integer = 2

    ''' <summary>
    ''' 工程主数据库,指当前打开工程的主数据库
    ''' </summary>
    ''' <remarks></remarks>
    Public g_db As SqlDB.DbClass

    ''' <summary>
    ''' 全局线宽值，一般指示多段线线宽值
    ''' </summary>
    Public Property GlobalWidth As Byte

    ''' <summary>
    ''' 主界面CAD对象
    ''' </summary>
    ''' <remarks></remarks>
    Public MxMainCadObj As AxMxDrawXLib.AxMxDrawX

    ''' <summary>
    ''' 工程配置数据文档,随工程变动
    ''' </summary>
    Public XmlProjectConfig As Xml.XmlDocument

    ''' <summary>
    ''' 安装专业基础数据XML文档,包括电气敷设信息、各专业材质信息
    ''' </summary>
    ''' <remarks></remarks>
    Public xmlSysDataConfig As Xml.XmlDocument

End Module
