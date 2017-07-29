''' <summary>
''' 一个楼层的dwg图层数据
''' </summary>
Public Class DWGLayerData
    ''' <summary>
    ''' 图层名称集合
    ''' </summary>
    ''' <returns></returns>
    Public Property LayerNameCol As New Collection
    ''' <summary>
    ''' 图层圆集合
    ''' </summary>
    ''' <returns></returns>
    Public Property CircleCol As New Collection
    ''' <summary>
    ''' 图层的弧集合
    ''' </summary>
    ''' <returns></returns>
    Public Property ArcCol As New Collection
    ''' <summary>
    ''' 图层的线集合
    ''' </summary>
    ''' <returns></returns>
    Public Property LineCol As New Collection
    ''' <summary>
    ''' 图层的字体集合
    ''' </summary>
    ''' <returns></returns>
    Public Property TextCol As New Collection
    ''' <summary>
    ''' 多段线集合
    ''' </summary>
    ''' <returns></returns>
    Public Property PolyLineCol As New Collection

End Class
