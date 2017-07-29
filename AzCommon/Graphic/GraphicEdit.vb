Namespace Graphic
    ''' <summary>
    ''' 图形编辑类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GraphicEdit

        ''' <summary>
        ''' 合并共线直线,返回合并后的直线对象
        ''' </summary>
        ''' <param name="lineObj1">待合并直线1</param>
        ''' <param name="lineObj2">待合并直线2</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function MergeCollinearLine(ByVal lineObj1 As MxDrawLine, ByVal lineObj2 As MxDrawLine) As MxDrawLine
            Dim lineAngle = GeometryMath.GetMinAngleToXAxis(lineObj1.StartPoint, lineObj1.EndPoint)
            Dim startPt1, startPt2, endPt1, endPt2 As New MxDrawPoint
            lineObj1.GetBoundingBox(startPt1, endPt1)
            lineObj2.GetBoundingBox(startPt2, endPt2)
            Dim newLineStartPt, newLineEndPt As New MxDrawPoint
            If lineAngle > Math.PI / 2 Then '直线与X轴的夹角大于90度
                newLineStartPt.x = Math.Min(startPt1.x, startPt2.x)
                newLineStartPt.y = Math.Max(endPt1.y, endPt2.y)
                newLineEndPt.x = Math.Max(endPt1.x, endPt2.x)
                newLineEndPt.y = Math.Min(startPt1.y, startPt2.y)
            Else
                newLineStartPt.x = Math.Min(startPt1.x, startPt2.x)
                newLineStartPt.y = Math.Min(startPt1.y, startPt2.y)
                newLineEndPt.x = Math.Max(endPt1.x, endPt2.x)
                newLineEndPt.y = Math.Max(endPt1.y, endPt2.y)
            End If
            Return GraphicConstruct.ConstructLine(newLineStartPt, newLineEndPt)
        End Function

        ''' <summary>
        ''' 通过指定基准线和搜索精度合并与之共线的同图层所有线段,返回合并后的直线对象(为内存副本)
        ''' </summary>
        ''' <param name="baseLine">合并基准线</param>
        ''' <param name="layerName">基准线所在的图层名</param>
        ''' <param name="thresholdValue">搜索精度</param>
        ''' <param name="lisMergeEntityId">返回,被合并的对象的ObjectId值</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function MergeCollinearLineBySearch(ByVal baseLine As MxDrawLine, ByVal layerName As String, ByVal thresholdValue As Integer, ByRef lisMergeEntityId As List(Of Long)) As MxDrawLine
            Dim mergeLine = CType(baseLine.Clone, MxDrawLine)
            Dim leftBottomPt, rightUpPt As New MxDrawPoint
            If baseLine.ObjectID > 0 Then lisMergeEntityId.Add(baseLine.ObjectID)
            '搜索合并起点位置
            Dim extendStartPt = GeometryMath.GetExtendPoint(baseLine.StartPoint, baseLine.EndPoint, thresholdValue)
            rightUpPt.x = Math.Max(extendStartPt.x, baseLine.StartPoint.x) + 1 : rightUpPt.y = Math.Max(extendStartPt.y, baseLine.StartPoint.y) + 1
            leftBottomPt.x = Math.Min(extendStartPt.x, baseLine.StartPoint.x) - 1 : leftBottomPt.y = Math.Min(extendStartPt.y, baseLine.StartPoint.y) - 1
            Dim lisStartFilterEntity = Graphic.GraphicFilter.GetEntityByRect(leftBottomPt, rightUpPt, layerName, Layer.EntityDxfTypeName.LINE)
            For Each filterLineObj As MxDrawLine In lisStartFilterEntity
                If lisMergeEntityId.Contains(filterLineObj.ObjectID) Then Continue For
                If GeometryMath.IsTwoLineCollinear(baseLine.StartPoint, baseLine.EndPoint, filterLineObj.StartPoint, filterLineObj.EndPoint) Then
                    mergeLine = Graphic.GraphicEdit.MergeCollinearLine(mergeLine, filterLineObj)
                    lisMergeEntityId.Add(filterLineObj.ObjectID)
                    mergeLine = MergeCollinearLineBySearch(mergeLine, layerName, thresholdValue, lisMergeEntityId)
                End If
            Next
            '搜索合并终点位置
            Dim extendEndPt = GeometryMath.GetExtendPoint(baseLine.EndPoint, baseLine.StartPoint, thresholdValue)
            rightUpPt.x = Math.Max(extendEndPt.x, baseLine.EndPoint.x) + 1 : rightUpPt.y = Math.Max(extendEndPt.y, baseLine.EndPoint.y) + 1
            leftBottomPt.x = Math.Min(extendEndPt.x, baseLine.EndPoint.x) - 1 : leftBottomPt.y = Math.Min(extendEndPt.y, baseLine.EndPoint.y) - 1
            Dim lisEndFilterEntity = Graphic.GraphicFilter.GetEntityByRect(leftBottomPt, rightUpPt, layerName, Layer.EntityDxfTypeName.LINE)
            For Each filterLineObj As MxDrawLine In lisEndFilterEntity
                If lisMergeEntityId.Contains(filterLineObj.ObjectID) Then Continue For
                If GeometryMath.IsTwoLineCollinear(baseLine.StartPoint, baseLine.EndPoint, filterLineObj.StartPoint, filterLineObj.EndPoint) Then
                    mergeLine = Graphic.GraphicEdit.MergeCollinearLine(mergeLine, filterLineObj)
                    lisMergeEntityId.Add(filterLineObj.ObjectID)
                    mergeLine = MergeCollinearLineBySearch(mergeLine, layerName, thresholdValue, lisMergeEntityId)
                End If
            Next
            Return mergeLine
        End Function

    End Class

End Namespace
