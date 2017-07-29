Imports MxDrawXLib

Public Class GeometryMath

    Private Const mPrecision As Double = 1


    Public Shared Sub ScaleBoudingBox(ByRef minPt As MxDrawPoint, ByRef maxPt As MxDrawPoint, ByVal scale As Double)
        minPt.x = minPt.x - scale
        minPt.y = minPt.y - scale
        maxPt.x = maxPt.x + scale
        maxPt.y = maxPt.y + scale
    End Sub



    ''' <summary>
    ''' 判断点是否在圆上，包括在圆边线上
    ''' </summary>
    ''' <param name="pCircle"></param>
    ''' <param name="pt"></param>
    ''' <returns></returns>
    Public Shared Function IsPtOnCircle(pCircle As MxDrawCircle, pt As MxDrawPoint) As Boolean
        Dim len As Double = pCircle.Center.DistanceTo(pt)
        If pCircle.Radius >= len Then
            Return True
        End If
        Return False
    End Function


    ''' <summary>
    '''  判断点是否在圆的边线上
    ''' </summary>
    ''' <param name="pCircle"></param>
    ''' <param name="pt"></param>
    ''' <param name="precision">误差范围</param>
    ''' <returns></returns>
    Public Shared Function IsPtOnCircleEdge(pCircle As MxDrawCircle, pt As MxDrawPoint, Optional ByVal precision As Double = mPrecision) As Boolean
        Dim len As Double = pCircle.Center.DistanceTo(pt)
        If Math.Abs(pCircle.Radius - len) < precision Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    '''  判断点是否在圆的边线上
    ''' </summary>
    ''' <param name="pLine"></param>
    ''' <param name="pt"></param>
    ''' <param name="precision">误差范围</param>
    ''' <returns></returns>
    Public Shared Function IsPtOnLine(pLine As MxDrawCurve, pt As MxDrawPoint, Optional ByVal precision As Double = mPrecision) As Boolean
        Dim tempLen As Double = pLine.GetDistAtPoint2(pt)
        If tempLen > -0.01 AndAlso tempLen < 1.01 Then
            Dim len As Double = pLine.GetDistAtPoint2(pt)
            If len > precision Then
                Return False
            End If
            Return True
        End If



        Return False
    End Function


    Public Shared Function IsEntityInserct(obj1 As Object, obj2 As Object, ByVal extendOption As MCAD_McExtendOption, Optional ByRef rePs As MxDrawPoints = Nothing) As Boolean
        If Not TypeOf obj1 Is MxDrawEntity OrElse Not TypeOf obj1 Is MxDrawEntity Then Return False

        Dim pEntity1 As MxDrawEntity = CType(obj1, MxDrawEntity)
        '  Dim pEntity2 As MxDrawEntity = CType(obj2, MxDrawEntity)
        rePs = pEntity1.IntersectWith(obj2, extendOption)
        If rePs.Count = 0 Then Return False

        Return True
    End Function

    ''' <summary>
    ''' 判断线是否相交于点
    ''' </summary>
    ''' <param name="curve"></param>
    ''' <param name="pt"></param>
    ''' <param name="precision"></param>
    ''' <returns></returns>
    Public Shared Function IsInserctPt(curve As MxDrawCurve, pt As MxDrawPoint, Optional precision As Double = mPrecision) As Boolean
        Dim pLen As Double = curve.GetDistAtPoint2(pt)

        If pLen <= precision Then Return True
        Return False
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
    ''' 将指定弧度值转换成角度，返回转换后的角度值
    ''' </summary>
    ''' <param name="radianValue">弧度值</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RadianToAngle(ByVal radianValue As Double) As Double
        Return 180 / Math.PI * radianValue
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
End Class
