
Namespace Graphic
    ''' <summary>
    ''' CAD基础图元构造类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GraphicConstruct

        ''' <summary>
        ''' 完全克隆一个实体对象 ，不支持的对象返回nothing
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CloneEntity(obj As Object, ByRef reObj As Object) As Boolean
            If TypeOf obj Is MxDrawLine Then
                Dim pLine As MxDrawLine = CType(obj, MxDrawLine)
                reObj = ConstructLine(pLine.GetStartPoint, pLine.GetEndPoint)
            ElseIf TypeOf obj Is MxDrawText
                Dim pText As MxDrawText = CType(obj, MxDrawText)
                reObj = ConstructText(pText)
                ' reObj = ConstructText(pText.TextString, pText.Position, pText.Height)
            ElseIf TypeOf obj Is MxDrawCircle
                Dim pCircle As MxDrawCircle = CType(obj, MxDrawCircle)
                reObj = ConstructCircle(pCircle.Center, pCircle.Radius)
            ElseIf TypeOf obj Is MxDrawPolyline
                Dim pPolyline As MxDrawPolyline = CType(obj, MxDrawPolyline)
                Dim tempPolyline As New MxDrawPolyline
                For i = 0 To pPolyline.NumVerts - 1
                    tempPolyline.AddVertexAt(pPolyline.GetPointAt(i))
                Next
                reObj = tempPolyline
            Else
                Return False
            End If

            Return True
        End Function

        ''' <summary>
        ''' 构造点集合对象
        ''' </summary>
        ''' <param name="pt1"></param>
        ''' <param name="pt2"></param>
        ''' <param name="offer"></param>
        ''' <returns></returns>
        Public Shared Function ConstructPsByLine(ByVal pt1 As MxDrawPoint, ByVal pt2 As MxDrawPoint, Optional ByVal offer As Double = 1) As MxDrawPoints
            If GeometryMath.IsSamePoint(pt1, pt2) Then Return Nothing
            Dim vorter As MxDrawVector3d = pt2.SumVector(pt1)
            vorter.Normalize()
            vorter.RotateByXyPlan(Math.PI / 2)
            vorter.Mult(offer)
            Dim tempPt1 As MxDrawPoint = ConstructPoint(pt1.x, pt1.y)
            Dim tempPt2 As MxDrawPoint = ConstructPoint(pt2.x, pt2.y)
            tempPt1.Sum(vorter)
            tempPt2.Sum(vorter)

            Dim ps As New MxDrawPoints
            ps.Add2(tempPt1)
            ps.Add2(tempPt2)

            vorter.Mult(-1)
            pt1.Sum(vorter)
            pt2.Sum(vorter)
            ps.Add2(pt2)
            ps.Add2(pt1)
            Return ps
        End Function




        ''' <summary>
        ''' 构造CAD字体对象
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConstructText(pText As MxDrawText) As MxDrawText
            Dim pTempText As New MxDrawText
            Dim pointObj As New MxDrawText
            pointObj.AlignmentPoint = Graphic.GraphicConstruct.ConstructPoint(pText.AlignmentPoint.x, pText.AlignmentPoint.y)
            pointObj.Position = Graphic.GraphicConstruct.ConstructPoint(pText.Position.x, pText.Position.y)
            pointObj.TextString = pText.TextString
            pointObj.Height = pText.Height
            pointObj.WidthFactor = pText.WidthFactor
            pointObj.verticalMode = pText.verticalMode
            pointObj.horizontalMode = pText.horizontalMode
            pointObj.Rotation = pText.Rotation
            pointObj.Oblique = pText.Oblique
            Return pointObj
        End Function




        ''' <summary>
        ''' 构造CAD点对象
        ''' </summary>
        ''' <param name="xCoordinate">X坐标值</param>
        ''' <param name="yCoordinate">Y坐标值</param>
        ''' <param name="zCoordinate">可选,Z坐标值默认为0</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConstructPoint(ByVal xCoordinate As Double, ByVal yCoordinate As Double, Optional ByVal zCoordinate As Double = 0) As MxDrawPoint
            Dim pointObj As New MxDrawPoint
            pointObj.x = xCoordinate
            pointObj.y = yCoordinate
            pointObj.z = zCoordinate
            Return pointObj
        End Function

        ''' <summary>
        ''' 通过两点构造线段实体对象
        ''' </summary>
        ''' <param name="startPoint">线段起点</param>
        ''' <param name="endPoint">线段端点</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConstructLine(ByVal startPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint) As MxDrawLine
            Dim lineObj As New MxDrawLine
            lineObj.StartPoint = startPoint
            lineObj.EndPoint = endPoint
            Return lineObj
        End Function


        ''' <summary>
        ''' 通过xy构造线段实体对象
        ''' </summary>
        ''' <param name="x1"></param>
        ''' <param name="y1"></param>
        ''' <param name="x2"></param>
        ''' <param name="y2"></param>
        ''' <returns></returns>
        Public Shared Function ConstructLine(ByVal x1 As Double, y1 As Double, x2 As Double, y2 As Double) As MxDrawLine
            Dim startPoint = ConstructPoint(x1, y1)
            Dim endPoint = ConstructPoint(x2, y2)
            Dim lineObj As New MxDrawLine
            lineObj.StartPoint = startPoint
            lineObj.EndPoint = endPoint
            Return lineObj
        End Function

        ''' <summary>
        ''' 通过圆心和半径构造CAD实体对象
        ''' </summary>
        ''' <param name="centerPoint">圆心坐标点</param>
        ''' <param name="radius">半径值</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConstructCircle(ByVal centerPoint As MxDrawPoint, ByVal radius As Double) As MxDrawCircle
            Dim circleObj As New MxDrawCircle
            circleObj.Center = centerPoint
            circleObj.Radius = radius
            Return circleObj
        End Function



        ' ''' <summary>
        ' ''' 通过圆弧上的三点构造一个圆弧对象,如果三点共线则返回Nothing
        ' ''' </summary>
        ' ''' <param name="startPoint">圆弧起点</param>
        ' ''' <param name="internalPoint">圆弧上任意点</param>
        ' ''' <param name="endPoint">圆弧端点</param>
        ' ''' <returns></returns>
        ' ''' <remarks></remarks>
        'Public Shared Function ConstructArc(ByVal startPoint As MxDrawPoint, ByVal internalPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint) As MxDrawArc
        '    If GeometryMath.IsPointOnLine(internalPoint, startPoint, endPoint, False) Then
        '        Return Nothing
        '    Else
        '        Dim centerPt1, centerPt2 As New MxDrawPoint
        '        centerPt1.x = (startPoint.x + internalPoint.x) / 2
        '        centerPt1.y = (startPoint.y + internalPoint.y) / 2
        '        centerPt2.x = (endPoint.x + internalPoint.x) / 2
        '        centerPt2.y = (endPoint.y + internalPoint.y) / 2
        '        Dim lineStartPt1 = GeometryMath.RotatePoint(centerPt1, startPoint, Math.PI / 2)
        '        Dim lineEndPt1 = GeometryMath.RotatePoint(centerPt1, internalPoint, Math.PI / 2)
        '        Dim lineStartPt2 = GeometryMath.RotatePoint(centerPt2, endPoint, Math.PI / 2)
        '        Dim lineEndPt2 = GeometryMath.RotatePoint(centerPt2, internalPoint, Math.PI / 2)
        '        Dim intersectPt = GeometryMath.GetTwoLineIntersecPt(lineStartPt1, lineEndPt1, lineStartPt2, lineEndPt2)
        '        Dim startAngle = GeometryMath.GetAngle(startPoint, intersectPt)
        '        Dim endAngle = GeometryMath.GetAngle(endPoint, intersectPt)
        '        Dim internalAngle = GeometryMath.GetAngle(internalPoint, intersectPt)
        '        Dim arcObj As New MxDrawArc
        '        arcObj.Center = intersectPt
        '        arcObj.Radius = GeometryMath.CalcDistance(startPoint, intersectPt)
        '        If internalAngle > startAngle AndAlso internalAngle < endAngle Then
        '            arcObj.StartAngle = startAngle
        '            arcObj.EndAngle = endAngle
        '        ElseIf internalAngle > endAngle AndAlso internalAngle < startAngle Then
        '            arcObj.StartAngle = endAngle
        '            arcObj.EndAngle = startAngle
        '        ElseIf endAngle > startAngle Then
        '            arcObj.StartAngle = endAngle
        '            arcObj.EndAngle = startAngle
        '        Else
        '            arcObj.StartAngle = startAngle
        '            arcObj.EndAngle = endAngle
        '        End If
        '        Return arcObj
        '    End If
        'End Function

        ''' <summary>
        ''' 通过圆弧上的三点构造一个圆弧对象,如果三点共线则返回Nothing
        ''' </summary>
        ''' <param name="startPoint">圆弧起点</param>
        ''' <param name="internalPoint">圆弧上任意点</param>
        ''' <param name="endPoint">圆弧端点</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConstructArc(ByVal startPoint As MxDrawPoint, ByVal internalPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint) As MxDrawArc
            If GeometryMath.IsPointOnLine(internalPoint, startPoint, endPoint, False) Then
                Return Nothing
            Else
                Dim line1, line2 As New MxDrawLine
                Dim centerPt1, centerPt2 As New MxDrawPoint
                line1.StartPoint = startPoint
                line1.EndPoint = internalPoint
                line2.StartPoint = endPoint
                line2.EndPoint = internalPoint
                centerPt1.x = (startPoint.x + internalPoint.x) / 2
                centerPt1.y = (startPoint.y + internalPoint.y) / 2
                centerPt2.x = (endPoint.x + internalPoint.x) / 2
                centerPt2.y = (endPoint.y + internalPoint.y) / 2
                line1.Rotate(centerPt1, Math.PI / 2)
                line2.Rotate(centerPt2, Math.PI / 2)
                Dim intersectPt = GeometryMath.GetTwoLineIntersecPt(line1.StartPoint, line1.EndPoint, line2.StartPoint, line2.EndPoint)
                Dim startAngle = GeometryMath.GetAngle(intersectPt, startPoint)
                Dim endAngle = GeometryMath.GetAngle(intersectPt, endPoint)
                Dim internalAngle = GeometryMath.GetAngle(intersectPt, internalPoint)
                Dim arcObj As New MxDrawArc
                arcObj.Center = intersectPt
                arcObj.Radius = GeometryMath.CalcDistance(startPoint, intersectPt)
                If internalAngle > startAngle AndAlso internalAngle < endAngle Then
                    arcObj.StartAngle = startAngle
                    arcObj.EndAngle = endAngle
                ElseIf internalAngle > endAngle AndAlso internalAngle < startAngle Then
                    arcObj.StartAngle = endAngle
                    arcObj.EndAngle = startAngle
                ElseIf endAngle > startAngle Then
                    arcObj.StartAngle = endAngle
                    arcObj.EndAngle = startAngle
                Else
                    arcObj.StartAngle = startAngle
                    arcObj.EndAngle = endAngle
                End If
                Return arcObj
            End If
        End Function

        ''' <summary>
        ''' 块对象定义,如果成功则返回定义的块表记录,否则返回Nothing
        ''' </summary>
        ''' <param name="entityObjs">构成块的对象实体</param>
        ''' <param name="originPt">可选,定义的块插入点,默认采用块外包矩形中心点作为插入点</param>
        ''' <param name="blockName">可选,定义的块名称,如果为空或已存在同名块,则定义匿名块</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DefineBlockObj(ByVal entityObjs() As MxDrawEntity, Optional ByVal originPt As MxDrawPoint = Nothing, Optional ByVal blockName As String = "") As MxDrawBlockTableRecord
            Dim cadDb = CType(MdlCommonDeclare.MxMainCadObj.GetDatabase, MxDrawDatabase)
            Dim blockTable = cadDb.GetBlockTable
            Dim originLayerName As String = ""
            If blockTable.Has(blockName) Then blockName = ""
            Dim blockRec = blockTable.Add(blockName) '创建匿名块
            If originPt Is Nothing Then
                Dim minPt, maxPt As New MxDrawPoint
                Dim xMin = Double.MaxValue : Dim yMin = Double.MaxValue
                Dim xMax = Double.MinValue : Dim yMax = Double.MinValue
                For Each entityObj In entityObjs
                    originLayerName = entityObj.Layer
                    entityObj.Layer = "0"
                    blockRec.AddEntityEx(entityObj)
                    If originLayerName <> "" Then entityObj.Layer = originLayerName
                    entityObj.GetBoundingBox(minPt, maxPt)
                    If xMin > minPt.x Then xMin = minPt.x
                    If yMin > minPt.y Then yMin = minPt.y
                    If xMax < maxPt.x Then xMax = maxPt.x
                    If yMax < maxPt.y Then yMax = maxPt.y
                Next
                originPt = New MxDrawPoint
                originPt.x = (xMin + xMax) / 2
                originPt.y = (yMin + yMax) / 2
            Else
                For Each entityObj In entityObjs
                    originLayerName = entityObj.Layer
                    entityObj.Layer = "0"
                    blockRec.AddEntityEx(entityObj)
                    If originLayerName <> "" Then entityObj.Layer = originLayerName
                Next
            End If
            blockRec.Origin = originPt
            Return blockRec
        End Function

        ''' <summary>
        ''' 将多个CAD实体对象打包成块,如果打包失败则返回Nothing
        ''' </summary>
        ''' <param name="entityObjs">构成块的实体对象</param>
        ''' <param name="originPt">指定的块插入基点</param>
        ''' <param name="blockName">可选,块名称,如果已存在同名块,则采用匿名块名,默认定义匿名块</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConstructBlockReference(ByVal entityObjs() As MxDrawEntity, ByVal originPt As MxDrawPoint, Optional ByVal blockName As String = "") As MxDrawBlockReference
            Dim blockRec = DefineBlockObj(entityObjs, originPt, blockName)
            If blockRec Is Nothing Then
                Return Nothing
            Else
                Dim blockId = MdlCommonDeclare.MxMainCadObj.DrawBlockReference(originPt.x, originPt.y, blockRec.Name, 1, 0)
                Dim blockObj = CType(MdlCommonDeclare.MxMainCadObj.ObjectIdToObject(blockId), MxDrawEntity)
                Dim returnObj = CType(blockObj.Clone, MxDrawBlockReference)
                blockObj.Erase()
                Return returnObj
            End If
        End Function

        ''' <summary>
        ''' 构造CAD多线对象,如果构造失败则返回Nothing
        ''' </summary>
        ''' <param name="startPoint">多线起点</param>
        ''' <param name="endPoint">多线终点</param>
        ''' <param name="lineCount">多线数目</param>
        ''' <param name="offsetValue">多线偏移量</param>
        ''' <param name="isClose">可选,指示多线是否闭合,默认不闭合</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConstructMultiline(ByVal startPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint,
ByVal lineCount As UShort, ByVal offsetValue As Double, Optional ByVal isClose As Boolean = False) As MxDrawEntity
            Dim baseCenterPt As New MxDrawPoint
            baseCenterPt.x = (startPoint.x + endPoint.x) / 2
            baseCenterPt.y = (startPoint.y + endPoint.y) / 2
            Dim lineLength = GeometryMath.CalcDistance(startPoint, endPoint)
            Dim rotateAngle = GeometryMath.GetMinAngleToXAxis(startPoint, endPoint)
            Dim baseEndPt = ConstructPoint(baseCenterPt.x + lineLength / 2, baseCenterPt.y)
            Dim baseStartPt = ConstructPoint(baseCenterPt.x - lineLength / 2, baseCenterPt.y)
            Dim offsetTotalValue As Double = 0 '累计偏移量
            Dim lisLineObj As New List(Of MxDrawEntity)
            Dim tempStartPt, tempEndPt As New MxDrawPoint
            If lineCount = 1 Then
                lisLineObj.Add(CType(ConstructLine(startPoint, endPoint), MxDrawEntity))
            ElseIf lineCount Mod 2 = 0 Then
                For i = 1 To lineCount / 2
                    tempEndPt.x = baseEndPt.x
                    tempEndPt.y = baseEndPt.y + (i - 1) * offsetValue + offsetValue / 2
                    tempStartPt.x = baseStartPt.x
                    tempStartPt.y = baseStartPt.y + (i - 1) * offsetValue + offsetValue / 2
                    lisLineObj.Add(CType(ConstructLine(tempStartPt, tempEndPt), MxDrawEntity))
                    tempEndPt.x = baseEndPt.x
                    tempEndPt.y = baseEndPt.y - ((i - 1) * offsetValue + offsetValue / 2)
                    tempStartPt.x = baseStartPt.x
                    tempStartPt.y = baseStartPt.y - ((i - 1) * offsetValue + offsetValue / 2)
                    lisLineObj.Add(CType(ConstructLine(tempStartPt, tempEndPt), MxDrawEntity))
                Next
                offsetTotalValue = (lineCount / 2 - 1) * offsetValue + offsetValue / 2
            Else
                For i = 1 To (lineCount - 1) / 2
                    tempEndPt.x = baseEndPt.x
                    tempEndPt.y = baseEndPt.y + i * offsetValue
                    tempStartPt.x = baseStartPt.x
                    tempStartPt.y = baseStartPt.y + i * offsetValue
                    lisLineObj.Add(CType(ConstructLine(tempStartPt, tempEndPt), MxDrawEntity))
                    tempEndPt.x = baseEndPt.x
                    tempEndPt.y = baseEndPt.y - i * offsetValue
                    tempStartPt.x = baseStartPt.x
                    tempStartPt.y = baseStartPt.y - i * offsetValue
                    lisLineObj.Add(CType(ConstructLine(tempStartPt, tempEndPt), MxDrawEntity))
                Next
                lisLineObj.Add(CType(ConstructLine(baseStartPt, baseEndPt), MxDrawEntity))
                offsetTotalValue = (lineCount - 1) / 2 * offsetValue
            End If
            Dim returnEntity As MxDrawEntity = Nothing
            If lisLineObj.Count = 0 Then
                Return Nothing
            ElseIf lisLineObj.Count = 1 Then
                returnEntity = lisLineObj(0)
            Else
                If isClose Then
                    tempStartPt.x = baseStartPt.x : tempStartPt.y = baseStartPt.y + offsetTotalValue
                    tempEndPt.x = baseStartPt.x : tempEndPt.y = baseStartPt.y - offsetTotalValue
                    lisLineObj.Add(CType(ConstructLine(tempStartPt, tempEndPt), MxDrawEntity))
                    tempStartPt.x = baseEndPt.x : tempStartPt.y = baseEndPt.y + offsetTotalValue
                    tempEndPt.x = baseEndPt.x : tempEndPt.y = baseEndPt.y - offsetTotalValue
                    lisLineObj.Add(CType(ConstructLine(tempStartPt, tempEndPt), MxDrawEntity))
                End If
                returnEntity = CType(ConstructBlockReference(lisLineObj.ToArray, baseCenterPt), MxDrawEntity)
            End If
            lisLineObj.Clear()
            returnEntity.Rotate(baseCenterPt, rotateAngle)
            Return returnEntity
        End Function






    End Class
End Namespace
