Imports MxDrawXLib


Public Class OrderClass

    ''' <summary>
    ''' 点进行排序 ， 从小到大
    ''' </summary>
    ''' <param name="pt1"></param>
    ''' <param name="pt2"></param>
    ''' <remarks></remarks>
    'Public Shared Sub PtOrder(ByRef pt1 As PointDouble, ByRef pt2 As PointDouble)
    '    Dim line As MxDrawLine
    '    Dim Angle As Double = ComponentMath.GetLineAngle(pt1, pt2)
    '    If Math.Abs(Angle - 90) < 2 OrElse Math.Abs(Angle - 270) < 2 Then
    '        If pt1.Y > pt2.Y Then
    '            Dim pt As PointDouble = pt1
    '            pt1 = pt2
    '            pt2 = pt
    '        End If
    '    Else
    '        If pt1.X > pt2.X Then
    '            Dim pt As PointDouble = pt1
    '            pt1 = pt2
    '            pt2 = pt
    '        End If
    '    End If
    'End Sub


End Class
