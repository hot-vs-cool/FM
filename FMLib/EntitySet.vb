Imports MxDrawXLib
''' <summary>
''' 实体顺序集合,只对应一个图层
''' </summary>
Public Class EntitySet
    Implements System.Collections.Generic.IEnumerable(Of MxDrawEntity)

    Private pItims As New List(Of MxDrawEntity)
    ''' <summary>
    ''' 实体集合
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Items As IList(Of MxDrawEntity)
        Get
            Return pItims
        End Get
    End Property


    Private pLaryer As String
    ''' <summary>
    ''' 图层名称
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Laryer As String
        Get
            Return pLaryer
        End Get
    End Property

    ''' <summary>
    ''' 实体个数
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Count As Integer
        Get
            Return pItims.Count
        End Get
    End Property

    ''' <summary>
    ''' 添加数据，如果图层名称不一样抛出异常CXException
    ''' </summary>
    ''' <param name="v"></param>
    Public Sub Add(v As MxDrawEntity)
        If pLaryer Is Nothing OrElse pLaryer = "" Then pLaryer = v.Layer
        If v.Layer = pLaryer Then
            pItims.Add(v)
        Else
            Throw New CXException("添加的图层名称不一样")
        End If
    End Sub

    Private Function GetIEnumerator() As IEnumerator(Of MxDrawEntity) Implements IEnumerable(Of MxDrawEntity).GetEnumerator
        Return pItims.GetEnumerator
    End Function

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return pItims.GetEnumerator
    End Function
End Class


