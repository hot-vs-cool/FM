
''' <summary>
''' 几何数学类，如平行、相交、共线、角度等计算
''' </summary>
Public Class GeometryMath

    ''' <summary>
    ''' 计算空间两点之间的距离，返回距离数值
    ''' </summary>
    ''' <param name="startPoint">计算起点</param>
    ''' <param name="endPoint">计算终点</param>
    ''' <param name="isCalaZ">可选，是否计算Z坐标，默认不计算</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CalcDistance(ByVal startPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint, Optional ByVal isCalaZ As Boolean = False) As Double
        If isCalaZ Then
            Return ((startPoint.x - endPoint.x) ^ 2 + (startPoint.y - endPoint.y) ^ 2 + (startPoint.z - endPoint.z) ^ 2) ^ 0.5
        Else
            Return ((startPoint.x - endPoint.x) ^ 2 + (startPoint.y - endPoint.y) ^ 2) ^ 0.5
        End If
    End Function

    ''' <summary>
    ''' 将指定角度值转换成弧度，返回转换后的弧度值
    ''' </summary>
    ''' <param name="angleValue">角度值</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AngleToRadian(ByVal angleValue As Double) As Double
        Return Math.PI / 180 * angleValue
    End Function

    ''' <summary>
    ''' 将指定弧度值转换成角度，返回转换后的角度值
    ''' </summary>
    ''' <param name="radianValue">弧度值</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RadianToAngle(ByVal radianValue As Double) As Double
        Return 180 / Math.PI * radianValue
    End Function

    ''' <summary>
    ''' 获取由指定起点和终点的线段与X轴的夹角值
    ''' </summary>
    ''' <param name="startPoint">计算线段起点</param>
    ''' <param name="endPoint">计算线段终点</param>
    ''' <param name="isRadian">可选，指示返回值的表示类型，值为"True"表示以弧度制表示，反之为角度制</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAngle(ByVal startPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint, Optional isRadian As Boolean = True) As Double
        Dim angleValue As Double = Nothing
        Dim vector1 = endPoint.SumVector(startPoint)
        If isRadian Then
            angleValue = vector1.Angle
            'If Math.Abs(angleValue - 2 * Math.PI) < 0.0001 Then angleValue = 0
        Else
            angleValue = RadianToAngle(vector1.Angle)
            'If Math.Abs(angleValue - 360) < 0.0001 Then angleValue = 0
        End If
        Return angleValue
    End Function

    ''' <summary>
    ''' 获取指定起点和终点的线段在XY平面内与X轴的最小夹角,角度在±1°或360°±1°都返回为0°
    ''' </summary>
    ''' <param name="startPoint">计算线段起点</param>
    ''' <param name="endPoint">计算线段终点</param>
    ''' <param name="isRadian">可选，指示返回值的表示类型，值为"True"表示以弧度制表示，反之为角度制</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMinAngleToXAxis(ByVal startPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint, Optional ByVal isRadian As Boolean = True) As Double
        Dim newStartPt, newEndPt As New MxDrawPoint
        newStartPt.x = startPoint.x : newStartPt.y = startPoint.y
        newEndPt.x = endPoint.x : newEndPt.y = endPoint.y
        Dim angleReal = GetAngle(newStartPt, newEndPt)
        If angleReal > Math.PI Then
            angleReal -= Math.PI
        End If
        If Math.Abs(angleReal - Math.PI) < 0.02 Then
            Return 0
        End If
        If isRadian Then
            Return angleReal
        Else
            Return RadianToAngle(angleReal)
        End If
    End Function

    ''' <summary>
    ''' 获取指定延长值和指定方向末端的延长点
    ''' </summary>
    ''' <param name="startPoint">延长方向起点</param>
    ''' <param name="endPoint">延长方向终点</param>
    ''' <param name="extendValue">延长值</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetExtendPoint(ByVal startPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint, ByVal extendValue As Double) As MxDrawPoint
        Dim radian = GetAngle(startPoint, endPoint)
        Dim extendPoint As New MxDrawPoint
        extendPoint.x = endPoint.x + extendValue * Math.Cos(radian)
        extendPoint.y = endPoint.y + extendValue * Math.Sin(radian)
        Return extendPoint
    End Function

    ''' <summary>
    ''' 沿指定角度对指定基点偏移指定距离，返回偏移后的点坐标
    ''' </summary>
    ''' <param name="basePoint">偏移基点</param>
    ''' <param name="extendAngle">偏移角度,采用弧度制</param>
    ''' <param name="extendValue">延长值</param>
    ''' <returns></returns>
    ''' <remarks>当传入角度大于2*PI时，系统会自动进行换算至2*PI以内</remarks>
    Public Shared Function GetExtendPoint(ByVal basePoint As MxDrawPoint, ByVal extendAngle As Double, ByVal extendValue As Double) As MxDrawPoint
        Do While extendAngle > 2 * Math.PI
            extendAngle -= 2 * Math.PI
        Loop
        Dim extendPoint As New MxDrawPoint
        extendPoint.x = basePoint.x + extendValue * Math.Cos(extendAngle)
        extendPoint.y = basePoint.y + extendValue * Math.Sin(extendAngle)
        Return extendPoint
    End Function

    ''' <summary>
    ''' 获取直线外一点到到直线上的最近点(垂足)坐标
    ''' </summary>
    ''' <param name="xSorce">待求点X坐标值</param>
    ''' <param name="ySorce">待求点Y坐标值</param>
    ''' <param name="xLinePt1">直线起点X坐标值</param>
    ''' <param name="yLinePt1">直线起点Y坐标值</param>
    ''' <param name="xLinePt2">直线终点X坐标值</param>
    ''' <param name="yLinePt2">直线终点Y坐标值</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPedalPoint(ByVal xSorce As Double, ByVal ySorce As Double, ByVal xLinePt1 As Double, ByVal yLinePt1 As Double, ByVal xLinePt2 As Double, ByVal yLinePt2 As Double) As MxDrawPoint
        Dim pedalPt As New MxDrawPoint
        If Math.Abs(xLinePt1 - xLinePt2) < 1 AndAlso Math.Abs(yLinePt1 - yLinePt2) < 1 Then
            pedalPt.x = (xLinePt1 + xLinePt2) / 2
            pedalPt.y = (yLinePt1 + yLinePt2) / 2
        Else
            Dim k = -((xLinePt1 - xSorce) * (xLinePt2 - xLinePt1) + (yLinePt1 - ySorce) * (yLinePt2 - yLinePt1)) / ((xLinePt2 - xLinePt1) ^ 2 + (yLinePt2 - yLinePt1) ^ 2)
            pedalPt.x = k * (xLinePt2 - xLinePt1) + xLinePt1
            pedalPt.y = k * (yLinePt2 - yLinePt1) + yLinePt1
        End If
        Return pedalPt
    End Function

    ''' <summary>
    ''' 获取直线外一点到到直线上的最近点(垂足)坐标
    ''' </summary>
    ''' <param name="sourcePt">直线外点</param>
    ''' <param name="linePt1">直线起点</param>
    ''' <param name="linePt2">直线终点</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPedalPoint(ByVal sourcePt As MxDrawPoint, ByVal linePt1 As MxDrawPoint, ByVal linePt2 As MxDrawPoint) As MxDrawPoint
        Return GetPedalPoint(sourcePt.x, sourcePt.y, linePt1.x, linePt1.y, linePt2.x, linePt2.y)
    End Function

    ''' <summary>
    ''' 获取两直线的交点,返回的交点包含非实交的情况,如果不存在交点则返回Nothing
    ''' </summary>
    ''' <param name="line1pt1">直线1起点</param>
    ''' <param name="line1pt2">直线1终点</param>
    ''' <param name="line2pt1">直线2起点</param>
    ''' <param name="line2pt2">直线2终点</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTwoLineIntersecPt(ByVal line1pt1 As MxDrawPoint, ByVal line1pt2 As MxDrawPoint, ByVal line2pt1 As MxDrawPoint, ByVal line2pt2 As MxDrawPoint) As MxDrawPoint
        Dim Pt As New MxDrawPoint
        '够不成直线
        If IsSamePoint(line1pt1, line1pt2) Or IsSamePoint(line2pt1, line2pt2) Then Return Pt
        If IsSamePoint(line1pt1, line2pt1) Or IsSamePoint(line1pt1, line2pt2) Then Return line1pt1
        If IsSamePoint(line1pt2, line2pt1) Or IsSamePoint(line1pt2, line2pt2) Then Return line1pt2
        '与Y轴平行的情况
        If Math.Abs(line1pt1.x - line1pt2.x) < 1 AndAlso Math.Abs(line2pt1.x - line2pt2.x) < 1 Then
            Return Nothing
        ElseIf Math.Abs(line1pt1.x - line1pt2.x) < 1 Then
            Pt.x = line1pt1.x
            Pt.y = (line2pt1.y - line2pt2.y) / (line2pt1.x - line2pt2.x) * (line1pt1.x - line2pt1.x) + line2pt1.y
            Return Pt
        ElseIf Math.Abs(line2pt1.x - line2pt2.x) < 1 Then
            Pt.x = line2pt1.x
            Pt.y = (line1pt1.y - line1pt2.y) / (line1pt1.x - line1pt2.x) * (line2pt1.x - line1pt1.x) + line1pt1.y
            Return Pt
        Else
            Dim k1, k2 As Double
            k1 = (line1pt1.y - line1pt2.y) / (line1pt1.x - line1pt2.x)
            k2 = (line2pt1.y - line2pt2.y) / (line2pt1.x - line2pt2.x)
            If Math.Abs(k1 - k2) > 0.01 Then
                Pt.x = (k2 * line2pt1.x - k1 * line1pt1.x + line1pt1.y - line2pt1.y) / (k2 - k1)
                Pt.y = ((line1pt1.x - line2pt1.x) * k1 * k2 + k1 * line2pt1.y - k2 * line1pt1.y) / (k1 - k2)
                Return Pt
            Else
                ''重合或平行的情况
                If IsParallel(line1pt1, line1pt2, line2pt1, line2pt2) Then
                    If IsSamePoint(line1pt1, line2pt1) Then
                        If Math.Abs(GetAngle(line1pt2, line1pt1) - GetAngle(line2pt2, line2pt1)) < 0.1 Then Return Pt
                        Pt = line1pt1
                        Return Pt
                    ElseIf IsSamePoint(line1pt1, line2pt2) Then
                        If Math.Abs(GetAngle(line1pt2, line1pt1) - GetAngle(line2pt1, line2pt2)) < 0.1 Then Return Pt
                        Pt = line1pt1
                        Return Pt
                    ElseIf IsSamePoint(line1pt2, line2pt1) Then
                        If Math.Abs(GetAngle(line1pt1, line1pt2) - GetAngle(line2pt2, line2pt1)) < 0.1 Then Return Pt
                        Pt = line1pt2
                        Return Pt
                    ElseIf IsSamePoint(line1pt2, line2pt2) Then
                        If Math.Abs(GetAngle(line1pt1, line1pt2) - GetAngle(line2pt1, line2pt2)) < 0.1 Then Return Pt
                        Pt = line1pt2
                        Return Pt
                    End If
                End If
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' 通过指点精确度判断两点是否为同一点
    ''' </summary>
    ''' <param name="point1">点1</param>
    ''' <param name="point2">点2</param>
    ''' <param name="accuracyValue">可选，误差控制值，默认为1</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsSamePoint(ByVal point1 As MxDrawPoint, ByVal point2 As MxDrawPoint, Optional ByVal accuracyValue As Double = 1) As Boolean
        If Math.Abs(point1.x - point2.x) < accuracyValue AndAlso Math.Abs(point1.y - point2.y) < accuracyValue Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 判断点是否在线上,返回值True表示点在线上,反之点不在线上，点到线的距离偏差控制值为±2mm
    ''' </summary>
    ''' <param name="sorcePoint">目标点</param>
    ''' <param name="lineStartPoint">线段起点</param>
    ''' <param name="lineEndPoint">线段端点</param>
    ''' <param name="isRealIntersect">指示是否是需要实相交,值为"True"则点在线延长线上也返回"False"</param>
    ''' <param name="isCalcZ">可选,指示是否需要考虑Z坐标影响</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsPointOnLine(ByVal sorcePoint As MxDrawPoint, ByVal lineStartPoint As MxDrawPoint, ByVal lineEndPoint As MxDrawPoint, ByVal isRealIntersect As Boolean, Optional ByVal isCalcZ As Boolean = False) As Boolean
        If IsSamePoint(sorcePoint, lineStartPoint) OrElse IsSamePoint(sorcePoint, lineEndPoint) Then
            Return True
        End If
        Dim endVector = sorcePoint.SumVector(lineEndPoint)
        Dim startVector = sorcePoint.SumVector(lineStartPoint)
        If Not isCalcZ Then
            endVector.z = 0
            startVector.z = 0
        End If
        Dim angle1 = GetMinAngleToXAxis(sorcePoint, lineStartPoint)
        Dim angle2 = GetMinAngleToXAxis(sorcePoint, lineEndPoint)
        If Math.Abs(angle1 - angle2) < 0.02 Then '三点共线则向量积长度值为0
            If Not isRealIntersect Then Return True
            '判断点是否在线上
            If (CInt(sorcePoint.x) + 2 >= CInt(lineStartPoint.x) AndAlso CInt(sorcePoint.x) - 2 <= CInt(lineEndPoint.x)) _
    OrElse (CInt(sorcePoint.x) + 1 >= CInt(lineEndPoint.x) AndAlso CInt(sorcePoint.x) - 2 <= CInt(lineStartPoint.x)) Then
                If (CInt(sorcePoint.y) + 2 >= CInt(lineStartPoint.y) AndAlso CInt(sorcePoint.y) - 2 <= CInt(lineEndPoint.y)) _
    OrElse (CInt(sorcePoint.y) + 2 >= CInt(lineEndPoint.y) AndAlso CInt(sorcePoint.y) - 2 <= CInt(lineStartPoint.y)) Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 判断点是否在指定的正交矩形区域内;返回值“True”在举行内，否则
    ''' </summary>
    ''' <param name="sorcePt">待判断的目标点</param>
    ''' <param name="leftDownPt">矩形区域左下角坐标点</param>
    ''' <param name="rightUpPt">矩形区域右上角坐标点</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsPointOnRectangle(ByVal sorcePt As MxDrawPoint, ByVal leftDownPt As MxDrawPoint, ByVal rightUpPt As MxDrawPoint) As Boolean
        If leftDownPt.x <= sorcePt.x AndAlso rightUpPt.x >= sorcePt.x Then
            If leftDownPt.y <= sorcePt.y AndAlso rightUpPt.y >= sorcePt.y Then
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 判断两直线是否平行;返回值True表示平行,反之不平行
    ''' </summary>
    ''' <param name="lineStartPoint1">直线1起点</param>
    ''' <param name="lineEndPoint1">直线1终点</param>
    ''' <param name="lineStartPoint2">直线2起点</param>
    ''' <param name="lineEndPoint2">直线2终点</param>
    ''' <param name="isCalcZ">可选,指示是否考虑Z坐标影响,默认不考虑</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsParallel(ByVal lineStartPoint1 As MxDrawPoint, ByVal lineEndPoint1 As MxDrawPoint, ByVal lineStartPoint2 As MxDrawPoint, ByVal lineEndPoint2 As MxDrawPoint, Optional ByVal isCalcZ As Boolean = False) As Boolean
        Dim vector1 = lineStartPoint1.SumVector(lineEndPoint1)
        Dim vector2 = lineStartPoint2.SumVector(lineEndPoint2)
        If Not isCalcZ Then
            vector1.z = 0
            vector2.z = 0
        End If
        Dim angle1 = GeometryMath.GetMinAngleToXAxis(lineStartPoint1, lineEndPoint1)
        Dim angle2 = GeometryMath.GetMinAngleToXAxis(lineStartPoint2, lineEndPoint2)
        If Math.Abs(angle1 - angle2) < 0.001 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 判断两直线是否共线;返回值"True"表示共线,反之不共线
    ''' </summary>
    ''' <param name="lineStartPoint1">直线1起点</param>
    ''' <param name="lineEndPoint1">直线1终点</param>
    ''' <param name="lineStartPoint2">直线2起点</param>
    ''' <param name="lineEndPoint2">直线2终点</param>
    ''' <param name="isCalcZ">可选,指示是否考虑Z坐标影响,默认不考虑</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsTwoLineCollinear(ByVal lineStartPoint1 As MxDrawPoint, ByVal lineEndPoint1 As MxDrawPoint, ByVal lineStartPoint2 As MxDrawPoint, ByVal lineEndPoint2 As MxDrawPoint, Optional ByVal isCalcZ As Boolean = False) As Boolean
        If IsParallel(lineStartPoint1, lineEndPoint1, lineStartPoint2, lineEndPoint2, isCalcZ) Then
            If IsPointOnLine(lineStartPoint1, lineStartPoint2, lineEndPoint2, False) Then
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' 判断XY平面两矩形是否相交(含包含情况);返回值"True"表示相交,反之不相交
    ''' </summary>
    ''' <param name="minPt1">矩形1的左下角角点</param>
    ''' <param name="maxPt1">矩形1的右上角角点</param>
    ''' <param name="minPt2">矩形2的左下角角点</param>
    ''' <param name="maxPt2">矩形2的右上角角点</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsTwoRectangleIntersect(ByVal minPt1 As MxDrawPoint, ByVal maxPt1 As MxDrawPoint, ByVal minPt2 As MxDrawPoint, ByVal maxPt2 As MxDrawPoint) As Boolean
        Dim minIntersectPt, maxIntersectPt As New MxDrawPoint
        minIntersectPt.x = Math.Max(minPt1.x, minPt2.x)
        minIntersectPt.y = Math.Max(minPt1.y, minPt2.y)
        maxIntersectPt.x = Math.Min(maxPt1.x, maxPt2.x)
        maxIntersectPt.y = Math.Min(maxPt1.y, maxPt2.y)
        If minIntersectPt.x < maxIntersectPt.x AndAlso minIntersectPt.y < maxIntersectPt.y Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 按指定基点将指定点旋转指定角度,返回旋转后的点
    ''' </summary>
    ''' <param name="basePoint">选择基点</param>
    ''' <param name="sourcePoint">待选择点</param>
    ''' <param name="rotateAngle">旋转角度,以弧度表示</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RotatePoint(ByVal basePoint As MxDrawPoint, ByVal sourcePoint As MxDrawPoint, ByVal rotateAngle As Double) As MxDrawPoint
        Dim rotatePt As New MxDrawPoint
        rotatePt.x = sourcePoint.x * Math.Cos(rotateAngle) + sourcePoint.y * Math.Sin(rotateAngle)
        rotatePt.y = sourcePoint.y * Math.Cos(rotateAngle) - sourcePoint.x * Math.Sin(rotateAngle)
        Return rotatePt
    End Function

End Class
