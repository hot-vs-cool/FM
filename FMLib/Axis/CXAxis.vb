Imports MxDrawXLib
Imports AxMxDrawXLib
Imports CxAzFunc.Graphic
Imports CxAzFunc


Public Class KeyOfObj(Of K, V)

    Public Sub SetKeyOfObj(Key As K, Value As V)
        Me.Key = Key
        Me.Value = Value
    End Sub


    Public Key As K
    Public Value As V
End Class

''' <summary>
''' 识别出的轴网句柄
''' </summary>
Public Class CXAxisHandle

    Private pAxisCol As New Collection '轴网集合


    Private pAxisPointCol As New Collection '轴网的交点集合

    Public ReadOnly Property AxisPointCol As Collection
        Get
            Return pAxisPointCol
        End Get
    End Property


    Public ReadOnly Property GetAxisCol As Collection
        Get
            Return pAxisCol
        End Get
    End Property
    ''' <summary>
    ''' 添加轴网
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddCXAxis(ByVal cxAxis As CXAxis)

        RecoAxisInserctPt(cxAxis)
        If pAxisCol.Count = 0 Then
            Dim tempList As New List(Of CXAxis)
            tempList.Add(cxAxis)
            pAxisCol.Add(tempList)
            Return
        End If
        Dim pLine1 As MxDrawLine = cxAxis.Curve
        Dim bn As Boolean = False
        For Each axisList As List(Of CXAxis) In pAxisCol
            Dim pLine2 As MxDrawLine = axisList.Item(0).Curve
            If GeometryMath.IsParallel(pLine1.StartPoint, pLine1.EndPoint, pLine2.StartPoint, pLine2.EndPoint) Then
                Dim index As Integer = GetAddParallelCurveIndex(axisList, cxAxis)
                If index <> -1 Then axisList.Insert(index, cxAxis)
                bn = True
                Exit For
            End If
        Next

        If Not bn Then
            Dim tempList As New List(Of CXAxis)
            tempList.Add(cxAxis)
            pAxisCol.Add(tempList)
        End If


    End Sub


    ''' <summary>
    ''' 识别轴网的交点坐标
    ''' </summary>
    Private Sub RecoAxisInserctPt(ByVal cxAxis As CXAxis)
        If pAxisCol.Count = 0 Then Return
        If cxAxis.AxisNumbles Is Nothing OrElse cxAxis.AxisNumbles.Count = 0 Then Return
        Dim key1 As String = cxAxis.AxisNumbles.Item(0).Text.TextString

        Dim pLine1 As MxDrawCurve = cxAxis.Curve
        For Each axisList As List(Of CXAxis) In pAxisCol
            Dim pLine2 As MxDrawCurve = axisList.Item(0).Curve
            If GeometryMath.IsParallel(pLine1.GetStartPoint, pLine1.GetEndPoint, pLine2.GetStartPoint, pLine2.GetEndPoint) Then
                Continue For
            End If
            For Each tempcxAxis In axisList
                If tempcxAxis.AxisNumbles Is Nothing OrElse tempcxAxis.AxisNumbles.Count = 0 Then Continue For
                Dim tempPLine As MxDrawCurve = tempcxAxis.Curve
                Dim rePs As MxDrawPoints = Nothing
                If GeometryMath.IsEntityInserct(pLine1, tempPLine, MCAD_McExtendOption.mcExtendNone, rePs) Then
                    Dim key2 As String = tempcxAxis.AxisNumbles.Item(0).Text.TextString
                    Dim key As String = key1 & ":" & key2
                    Dim kv As KeyOfObj(Of String, MxDrawPoint) = New KeyOfObj(Of String, MxDrawPoint)
                    kv.SetKeyOfObj(key, rePs.Item(0))
                    If Not pAxisPointCol.Contains(key) Then
                        pAxisPointCol.Add(kv, key)
                    End If
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' 平行线段排序添加
    ''' </summary>
    ''' <param name="list"></param>
    ''' <param name="CXAxis"></param>
    Private Function GetAddParallelCurveIndex(ByRef list As List(Of CXAxis), CXAxis As CXAxis) As Integer
        If list Is Nothing Then Return -1
        If list.Count = 0 Then Return -1
        Dim pCurve As MxDrawCurve = list.Item(0).Curve
        '不平行直接返回 
        If Not GeometryMath.IsParallel(pCurve.GetStartPoint, pCurve.GetEndPoint, CXAxis.Curve.GetStartPoint, CXAxis.Curve.GetEndPoint) Then Return -1


        If TypeOf CXAxis.Curve Is MxDrawLine Then
            Return GetAddParallelLineIndex(list, CXAxis)

        ElseIf TypeOf CXAxis.Curve Is MxDrawArc
            Dim pArc As MxDrawArc = CType(CXAxis.Curve, MxDrawArc)
        End If
        Return -1
    End Function

    ''' <summary>
    ''' 添加直的平行线
    ''' </summary>
    ''' <param name="list"></param>
    ''' <param name="CXAxis"></param>
    Private Function GetAddParallelLineIndex(ByRef list As List(Of CXAxis), CXAxis As CXAxis) As Integer
        Dim xle As MxDrawLine = GraphicConstruct.ConstructLine(0, 0, 10, 0)
        Dim yle As MxDrawLine = GraphicConstruct.ConstructLine(0, 0, 0, 10)
        Dim curve As MxDrawCurve = CXAxis.Curve
        If GeometryMath.IsParallel(xle.StartPoint, xle.EndPoint, curve.GetEndPoint, curve.GetStartPoint) Then '和x轴平行
            '求出和y轴的交点进行比较
            Dim curvePt As MxDrawPoint = Get2LineInserctPt(yle, curve)
            Dim listPt1 As MxDrawPoint = Get2LineInserctPt(yle, list.Item(0).Curve)
            If listPt1.y > curvePt.y Then Return 0
            Dim listPt2 As MxDrawPoint = Get2LineInserctPt(yle, list.Item(list.Count - 1).Curve)
            If curvePt.y > listPt2.y Then Return list.Count - 1
            For i = 0 To list.Count - 2
                If curvePt.y > listPt1.y AndAlso curvePt.y < listPt2.y Then
                    ' list.Insert(i, curve)
                    Return i
                End If
            Next
        Else

            Dim curvePt As MxDrawPoint = Get2LineInserctPt(xle, curve)
            Dim listPt1 As MxDrawPoint = Get2LineInserctPt(xle, list.Item(0).Curve)
            If listPt1.x > curvePt.x Then Return 0
            Dim listPt2 As MxDrawPoint = Get2LineInserctPt(xle, list.Item(list.Count - 1).Curve)
            If curvePt.x > listPt2.x Then Return list.Count - 1
            For i = 0 To list.Count - 2
                If curvePt.x > listPt1.x AndAlso curvePt.x < listPt2.x Then
                    ' list.Insert(i, curve)
                    Return i
                End If
            Next
        End If
        Return -1
    End Function

    Private Sub AddParallelArc(ByRef list As List(Of MxDrawCurve), curve As MxDrawCurve)

    End Sub


    Private Function Get2LineInserctPt(le1 As Object, le2 As Object) As MxDrawPoint
        Dim rePs As MxDrawPoints
        If GeometryMath.IsEntityInserct(le1, le2, MCAD_McExtendOption.mcExtendBoth, rePs) Then
            Return rePs.Item(0)
        End If
        Return rePs.Item(0)
    End Function






End Class

''' <summary>
''' 单根轴网
''' </summary>
Public Class CXAxis
    ''' <summary>
    ''' 轴号
    ''' </summary>
    Public AxisNumbles As New List(Of CXAxisNumble)
    ''' <summary>
    ''' 轴线,一般为轴线和弧线
    ''' </summary>
    Public Curve As MxDrawCurve
End Class


''' <summary>
''' 轴号
''' </summary>
Public Class CXAxisNumble
    ''' <summary>
    ''' 字体
    ''' </summary>
    Public Text As MxDrawText
    ''' <summary>
    ''' 圆
    ''' </summary>
    Public Circle As MxDrawCircle

    ''' <summary>
    ''' 轴号所连接的标注线
    ''' </summary>
    Public DimPolyline As MxDrawPolyline


    '   Private PolyLineLayers As List(Of String)

    ''' <summary>
    ''' 添加标注线
    ''' </summary>
    ''' <param name="line"></param>
    Public Sub AddDimLines(line As MxDrawLine)
        If DimPolyline Is Nothing Then
            DimPolyline = New MxDrawPolyline
            If GeometryMath.IsPtOnCircleEdge(Circle, line.StartPoint) Then '判断点是否在圆上
                DimPolyline.AddVertexAt(line.StartPoint)
                DimPolyline.AddVertexAt(line.EndPoint)
            Else
                DimPolyline.AddVertexAt(line.EndPoint)
                DimPolyline.AddVertexAt(line.StartPoint)
            End If
            'SetPolyLineLayers(line.Layer)
        Else
            If DimPolyline.GetEndPoint.IsEqualTo(line.StartPoint, 1) Then '判断点是否相同
                DimPolyline.AddVertexAt(line.EndPoint)
                '  SetPolyLineLayers(line.Layer)
            ElseIf DimPolyline.GetEndPoint.IsEqualTo(line.EndPoint, 1)
                DimPolyline.AddVertexAt(line.StartPoint)
                ' SetPolyLineLayers(line.Layer)
            End If
        End If
    End Sub

    '''' <summary>
    '''' 设置标注线的图层名称
    '''' </summary>
    '''' <param name="Layer"></param>
    'Private Sub SetPolyLineLayers(Layer As String)
    '    If PolyLineLayers Is Nothing Then PolyLineLayers = New List(Of String)
    '    If Not PolyLineLayers.Contains(Layer) Then
    '        PolyLineLayers.Add(Layer)
    '    End If
    'End Sub
    '''' <summary>
    '''' 设置标注线的图层名称
    '''' </summary>
    'Private Function GetPolyLineLayers() As String()
    '    If PolyLineLayers Is Nothing Then Return Nothing
    '    Return PolyLineLayers.ToArray
    'End Function
End Class
