Imports MxDrawXLib
Imports CxAzFunc
Public Class MainForm
    Private mPointCol As New Collection


    Private mAxisHandleCol As New Collection
    Private Sub CadObj_ImplementCommandEvent(sender As Object, e As AxMxDrawXLib._DMxDrawXEvents_ImplementCommandEventEvent) Handles CadObj.ImplementCommandEvent
        If mAxisHandleCol.Count = 0 Then Return

        Dim aimPt As MxDrawPoint = Nothing
        Dim originPt As MxDrawPoint = Nothing
        Dim mCxHandle As CXAxisHandle = CType(mAxisHandleCol.Item(1), CXAxisHandle)
        If mCxHandle.AxisPointCol.Count = 0 Then Return
        If mPointCol.Count = 0 Then
            Dim rbfPara As New MxDrawResbuf
            aimPt = Graphic.GraphicConstruct.ConstructPoint(0, 0)
            Dim getPoint As New MxDrawUiPrPoint
            If getPoint.go <> MCAD_McUiPrStatus.mcOk Then Return
            aimPt = getPoint.value()
            Dim tempPt = CType(mCxHandle.AxisPointCol.Item(1), KeyOfObj(Of String, MxDrawPoint)).Value
            originPt = CxAzFunc.Graphic.GraphicConstruct.ConstructPoint(tempPt.x, tempPt.y)
            Dim vorter As MxDrawVector3d = aimPt.SumVector(originPt)
            For Each pt As KeyOfObj(Of String, MxDrawPoint) In mCxHandle.AxisPointCol
                pt.Value.Add(vorter)
                If Not mPointCol.Contains(pt.Key) Then
                    mPointCol.Add(pt.Value, pt.Key)
                Else
                    Dim iiiiiiiiiiiiii = 0
                End If
            Next
        Else
            For Each pt As KeyOfObj(Of String, MxDrawPoint) In mCxHandle.AxisPointCol
                If mPointCol.Contains(pt.Key) Then
                    originPt = pt.Value
                    aimPt = CType(mPointCol.Item(pt.Key), MxDrawPoint)
                    Exit For
                End If
            Next
            If aimPt Is Nothing OrElse originPt Is Nothing Then
                Return
                Throw New CXException("没有找到轴网的基点")
            End If
        End If


        For Each handle As CXAxisHandle In mAxisHandleCol
            DrawAxis(handle, originPt, aimPt)
        Next

        mAxisHandleCol.Clear()
    End Sub


    Public Sub AddAxisHandle(ByVal handle As CXAxisHandle)
        Dim key As String = handle.GetHashCode.ToString
        If Not mAxisHandleCol.Contains(key) Then
            mAxisHandleCol.Add(handle, key)
        End If
    End Sub
    ''' <summary>
    ''' 绘制轴网
    ''' </summary>
    ''' <param name="mCxHandle"></param>
    ''' <param name="originPt"></param>
    ''' <param name="aimPt"></param>
    Private Sub DrawAxis(mCxHandle As CXAxisHandle, originPt As MxDrawPoint, aimPt As MxDrawPoint)
        For Each list As List(Of CXAxis) In mCxHandle.GetAxisCol
            For Each cxAxis In list
                Dim le = Graphic.GraphicConstruct.ConstructLine(cxAxis.Curve.GetStartPoint, cxAxis.Curve.GetEndPoint)
                SetEntityLayerName(DrawEntity(le, originPt, aimPt), FMConst.AxisLayerLine, ColorClass.GetRed)
                For Each cx In cxAxis.AxisNumbles
                    SetEntityLayerName(DrawEntity(cx.Circle, originPt, aimPt), FMConst.AxisLayerDim, ColorClass.GetGreen)
                    SetEntityLayerName(DrawEntity(cx.Text, originPt, aimPt), FMConst.AxisLayerDim, ColorClass.GetGreen)
                    SetEntityLayerName(DrawEntity(cx.DimPolyline, originPt, aimPt), FMConst.AxisLayerDim, ColorClass.GetGreen)
                Next
            Next
        Next
    End Sub


    ''' <summary>
    ''' 设置实体的图层名
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="layerName"></param>
    ''' <returns></returns>
    Private Function SetEntityLayerName(obj As Object, layerName As String, Optional ByVal color As MxDrawMcCmColor = Nothing) As Boolean
        If obj Is Nothing Then Return False
        If Not TypeOf obj Is MxDrawEntity Then Return False
        Dim pEntity As MxDrawEntity = CType(obj, MxDrawEntity)
        ''图层表记录
        Dim pLayerTableRecord As MxDrawLayerTableRecord = CType(pEntity.Database, MxDrawDatabase).GetLayerTable.GetAt(layerName)
        If pLayerTableRecord Is Nothing Then '如果没有就创建出来
            Dim tempObj As Object = CadObj.ObjectIdToObject(CadObj.AddLayer(layerName))
            pLayerTableRecord = CType(tempObj, MxDrawLayerTableRecord)
        End If
        If pLayerTableRecord Is Nothing Then Return False
        If color IsNot Nothing Then
            pLayerTableRecord.Color = color
        End If

        pEntity.Layer = layerName
        Return True
    End Function


    ''' <summary>
    ''' 绘制实体
    ''' </summary>
    ''' <param name="obj">绘制的对象</param>
    ''' <param name="originPt">移动的起始点</param>
    ''' <param name="aimPt">移动的目标点</param>
    Private Function DrawEntity(obj As Object, originPt As MxDrawPoint, aimPt As MxDrawPoint) As MxDrawEntity
        If Not TypeOf obj Is MxDrawEntity Then Return Nothing
        Dim pEntity As Object = Nothing
        If Not Graphic.GraphicConstruct.CloneEntity(obj, pEntity) Then Return Nothing
        Dim tempEntity As MxDrawEntity = CType(pEntity, MxDrawEntity)
        tempEntity.Move(originPt, aimPt)
        Dim id As Long = CadObj.DrawEntity(tempEntity)
        Dim entity As MxDrawEntity = CType(CadObj.ObjectIdToObject(id), MxDrawEntity)
        Return entity
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnDraw.Click
        CadObj.DoCommand(1)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Visible = False
        TestForm.Visible = True
    End Sub

End Class