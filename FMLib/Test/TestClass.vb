Imports MxDrawXLib
Imports AxMxDrawXLib
Imports CxAzFunc
Imports System.Text.RegularExpressions

''' <summary>
''' 测试类
''' </summary>
Public Class TestClass

    Private mxCadObj As AxMxDrawX
    Private mForm As TestForm

    Private mEvent As AxMxDrawXLib._DMxDrawXEvents_ImplementCommandEventEvent

    Private mRegex As New Regex("[0-9]|[a-z]|[A-Z]") '过滤轴网字符

    ''' <summary>
    ''' 直接测试，没有交互
    ''' </summary>
    ''' <param name="form"></param>
    ''' <param name="controlName"></param>
    Sub Execute1(ByVal form As TestForm, ByVal controlName As String)
        mForm = form
        mxCadObj = mForm.mxCadObj
        Dim getEntityObj As New MxDrawUiPrEntity

        Select Case controlName
            Case mForm.btnSetLayer.Name
                If getEntityObj.go <> MCAD_McUiPrStatus.mcOk Then Return
                Dim pEntity As MxDrawEntity = getEntityObj.Entity
                SetEntityLayerName(pEntity, "313136")

            Case mForm.Button2.Name
                Dim pText As New MxDrawText
                pText.Position = Graphic.GraphicConstruct.ConstructPoint(0, 0)
                pText.TextString = "ABC"
                pText.Height = 10
                '  pText.WidthFactor = 300
                mxCadObj.DrawEntity(pText)

            Case mForm.btnVorter.Name
                Dim originPt As MxDrawPoint = Graphic.GraphicConstruct.ConstructPoint(0, 0)
                Dim aimPt As MxDrawPoint = Graphic.GraphicConstruct.ConstructPoint(0, 10)
                Dim vorter = aimPt.SumVector(originPt)
                originPt.Add(vorter)
                originPt.Sum(vorter)
                Dim ii = 0
                Dim strStr As String = "dfgsd1463wer546"
                If mRegex.IsMatch(strStr) Then
                    Dim ss = mRegex.Match(strStr)
                    Dim ssssssssssssss = 0
                End If
            Case mForm.btnLookData.Name '获取所有的数据
                'Explode()
                Dim pSelectionSet As New MxDrawSelectionSet '获取所有的数据
                pSelectionSet.Select(MCAD_McSelect.mcSelectionSetAll, Nothing, Nothing, Nothing)
                Dim pDwgData As New DWGLayerData
                For i = 0 To pSelectionSet.Count - 1
                    GetDraphLayerData(pSelectionSet.Item(i), pDwgData)
                Next
                Dim k = 0
            Case mForm.btnRecoAxis.Name '识别轴网
                Dim getPoint As New MxDrawUiPrPoint
                If getPoint.go <> MCAD_McUiPrStatus.mcOk Then Return
                Dim pt1 As MxDrawPoint = getPoint.value()
                If getPoint.go <> MCAD_McUiPrStatus.mcOk Then Return
                Dim pt2 As MxDrawPoint = getPoint.value()

            Case mForm.Button1.Name
                Dim pRecoAxis As New RecoAxis
                Dim handle As CXAxisHandle = pRecoAxis.GetAxis()
                If handle Is Nothing Then Return
                mForm.cxAxisHanle = handle
                If handle.AxisPointCol.Count > 0 Then
                    MsgBox("识别轴网成功")
                End If
            Case mForm.btnInscet.Name
                Dim selObj As New MxDrawSelectionSet
                selObj.Select(MCAD_McSelect.mcSelectionSetUserSelect, Nothing, Nothing)
                If selObj.Count > 0 Then
                    Dim newDb As New MxDrawDatabase
                    For i = 0 To selObj.Count - 1
                        Dim entObj = CType(selObj.Item(i).Clone, MxDrawEntity)
                        newDb.CurrentSpace.AddEntityEx(entObj)
                    Next
                    newDb.SaveAs("D:\tets.dwg", 10)
                End If
        End Select
    End Sub

    ''' <summary>
    ''' 设置实体的图层名
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="layerName"></param>
    ''' <returns></returns>
    Private Function SetEntityLayerName(obj As Object, layerName As String) As Boolean
        If Not TypeOf obj Is MxDrawEntity Then Return False
        Dim pEntity As MxDrawEntity = CType(obj, MxDrawEntity)
        ''图层表记录
        Dim pLayerTableRecord As MxDrawLayerTableRecord = CType(pEntity.Database, MxDrawDatabase).GetLayerTable.GetAt(layerName)
        If pLayerTableRecord Is Nothing Then '如果没有就创建出来
            Dim tempObj As Object = mxCadObj.ObjectIdToObject(mxCadObj.AddLayer(layerName))
            pLayerTableRecord = CType(tempObj, MxDrawLayerTableRecord)
        End If
        If pLayerTableRecord Is Nothing Then Return False
        ' pLayerTableRecord.Color.ColorMethod = MCAD_McColorMethod.mcColorMethodByRGB
        '设置图层颜色
        '  pLayerTableRecord.Color.SetRGB(255, 0, 255)
        Dim newColor As New MxDrawMcCmColor
        newColor.SetRGB(255, 0, 255)
        'newColor.
        pLayerTableRecord.Color = newColor
        pEntity.Layer = layerName
        Return True
    End Function


    Sub Execute2(ByVal form As TestForm, ByVal controlName As String)
        mForm = form
        mxCadObj = mForm.mxCadObj

        Dim getEntityObj As New MxDrawUiPrEntity
        If getEntityObj.go <> MCAD_McUiPrStatus.mcOk Then Return
        ''  Return
        Dim pEntity As MxDrawEntity = getEntityObj.Entity
        Select Case controlName
            Case mForm.bntSeletData.Name

                Dim layerName = getEntityObj.Entity.Layer
                Dim rbfFilter As New MxDrawResbuf
                rbfFilter.AddStringEx(getEntityObj.Entity.Dxf0, Layer.DxfCode.CommonEntity)
                Dim selObj As New MxDrawSelectionSet
                selObj.Select(MCAD_McSelect.mcSelectionSetAll, Nothing, Nothing, Nothing)
                Me.mxCadObj.ClearCurrentSelect()
                Dim list As New List(Of Long)
                For i = 0 To selObj.Count - 1
                    list.Add(selObj.Item(i).ObjectID)
                Next
                mxCadObj.UpdateDisplay()
                Runtime.InteropServices.Marshal.ReleaseComObject(rbfFilter)
            Case mForm.btnSeletType.Name

                MsgBox(GetEntityType(pEntity))
            Case mForm.btnSeletLayer.Name

                MsgBox(pEntity.Layer)
          
            Case mForm.btnOpenDWG.Name
                Me.mxCadObj.SendStringToExecute("OpenDwg")
            Case mForm.btnLookBlockAttri.Name

                IsBlockAttribute(pEntity)
            Case mForm.btnRecoAxis.Name '识别轴网
                Dim getPoint As New MxDrawUiPrPoint
                If getPoint.go <> MCAD_McUiPrStatus.mcOk Then Return
                Dim pt1 As MxDrawPoint = getPoint.value()
                If getPoint.go <> MCAD_McUiPrStatus.mcOk Then Return
                Dim pt2 As MxDrawPoint = getPoint.value()
                'Explode()
                ' Dim list = GraphicFilter.GetEntityOfBox(pt1, pt2)
                Dim aaa = 0

        End Select
    End Sub

    ''' <summary>
    ''' 判断块是否有属性值
    ''' </summary>
    ''' <param name="pEntity"></param>
    Private Sub IsBlockAttribute(pEntity As MxDrawEntity)
        If TypeOf pEntity Is MxDrawBlockReference Then
            Dim blockObj = CType(pEntity, MxDrawBlockReference)
            If blockObj.AttributeCount > 0 Then
                Dim strProperty As String = ""
                For i = 0 To blockObj.AttributeCount - 1
                    If blockObj.AttributeItem(i).IsInvisible Then
                        strProperty &= vbCr & blockObj.AttributeItem(i).Tag & "：" & blockObj.AttributeItem(i).TextString & " 不显示"
                    Else
                        strProperty &= vbCr & blockObj.AttributeItem(i).Tag & "：" & blockObj.AttributeItem(i).TextString & “显示"
                    End If
                Next
                MsgBox(strProperty)
            Else
                MsgBox("不存在块属性")
            End If
        Else
            MsgBox("不是块")
        End If
    End Sub

    ''' <summary>
    ''' 获取实体的类型
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <returns></returns>
    Private Function GetEntityType(entity As MxDrawEntity) As String
        Dim typeName As String = ""
        If TypeOf entity Is MxDrawLine Then '直线对象
            Dim pLine1 As MxDrawLine = CType(entity, MxDrawLine)
            Dim pl = CType(mxCadObj.ObjectIdToObject(pLine1.ObjectID), MxDrawLine)
            typeName = "直线对象：MxDrawLine"
        ElseIf TypeOf entity Is MxDrawPointEntity '点对象
            Dim pPointEntity As MxDrawPointEntity = CType(entity, MxDrawPointEntity)
            '  Dim pPoint As MxDrawPoint = CType(pEntity, MxDrawPoint)
            Dim pPoint As MxDrawPoint = pPointEntity.Position
            typeName = "点对象：MxDrawPoint"
            'ElseIf TypeOf pEntity Is MxDrawCurve
            '    Dim pCurve As MxDrawCurve = CType(pEntity, MxDrawCurve)
            '    typeName = "MxDrawCurve"
        ElseIf TypeOf entity Is MxDrawHatch  '填充对象
            Dim pText As MxDrawHatch = CType(entity, MxDrawHatch)
            typeName = "填充对象：MxDrawHatch"
        ElseIf TypeOf entity Is MxDrawXline '射线
            Dim pText As MxDrawXline = CType(entity, MxDrawXline)
            typeName = "射线对象：MxDrawXLine"

        ElseIf TypeOf entity Is MxDrawAttributeDefinition  '块属性对象 继承MxDrawText
            Dim pAttributeDefinition As MxDrawAttributeDefinition = CType(entity, MxDrawAttributeDefinition)
            typeName = "块属性对象：MxDrawAttributeDefinition"
        ElseIf TypeOf entity Is MxDrawAttribute   '块属性对象 继承MxDrawText
            Dim pAttribute As MxDrawAttribute = CType(entity, MxDrawAttribute)
            typeName = "块属性对象：MxDrawAttribute"
        ElseIf TypeOf entity Is MxDrawText '字体对象
            Dim pText As MxDrawText = CType(entity, MxDrawText)
            typeName = "字体对象：MxDrawText"
        ElseIf TypeOf entity Is MxDrawMText '多行字体对象
            Dim pText As MxDrawMText = CType(entity, MxDrawMText)
            typeName = "多行字体对象：MxDrawMText"
        ElseIf TypeOf entity Is MxDrawArc '弧对象
            Dim pArc As MxDrawArc = CType(entity, MxDrawArc)
            typeName = "弧对象：MxDrawArc"
        ElseIf TypeOf entity Is MxDrawEllipse '椭圆对象
            Dim pEllipse As MxDrawEllipse = CType(entity, MxDrawEllipse)
            typeName = "椭圆对象：MxDrawEllipse"
        ElseIf TypeOf entity Is MxDrawCircle '圆对象
            Dim pCircle As MxDrawCircle = CType(entity, MxDrawCircle)
            typeName = "圆对象：MxDrawCircle"
        ElseIf TypeOf entity Is MxDrawPolyline '多段线对象
            Dim pPolyline As MxDrawPolyline = CType(entity, MxDrawPolyline)
            typeName = "多段线对象：MxDrawPolyline"
        ElseIf TypeOf entity Is MxDrawAttributeDefinition  '多段线对象
            Dim pAttribute As MxDrawAttributeDefinition = CType(entity, MxDrawAttributeDefinition)
            typeName = "文字对象：MxDrawAttributeDefinition"
        ElseIf TypeOf entity Is MxDrawDimAngular Then '标注对象

        ElseIf TypeOf entity Is MxDrawDimension Then '标注对象
            Dim pDimension As MxDrawDimension = CType(entity, MxDrawDimension)
            typeName = "标注对象：MxDrawDimension"
        ElseIf TypeOf entity Is MxDrawBlockReference Then '块对象


            Dim pBlock As MxDrawBlockReference = CType(entity, MxDrawBlockReference)
            Dim pResbuf As MxDrawResbuf = pBlock.Explode()

            Dim pMxDrawDb As MxDrawDatabase = CType(mxCadObj.GetDatabase, MxDrawDatabase)
            Dim pBlockTableRecord As MxDrawBlockTableRecord = pMxDrawDb.GetBlockTable().GetAt(pBlock.GetBlockName)
            Dim pBlockTableRecordIterator As MxDrawBlockTableRecordIterator = pBlockTableRecord.NewIterator
            typeName += "块对象：MxDrawBlockReference{"
            pBlockTableRecordIterator.Start()
            Do Until pBlockTableRecordIterator.Done
                Dim pTempEntity As MxDrawEntity = pBlockTableRecordIterator.GetEntity
                typeName += GetEntityType(pTempEntity) & ","
                pBlockTableRecordIterator.Step()
            Loop
            '  Dim attCount = pBlock.AttributeCount
            '  MsgBox(pBlock.AttributeItem(0).TextString)
            typeName += "}"
        End If
        Return typeName
    End Function

    ''' <summary>
    ''' 炸开数据块
    ''' </summary>
    Private Sub Explode()
        Dim rbfFilter As New MxDrawResbuf
        rbfFilter.AddStringEx(Layer.EntityDxfTypeName.INSERT.ToString, Layer.DxfCode.CommonEntity)
        Dim blockIdCol As New Collection
        Do
            Dim selObj As New MxDrawSelectionSet
            selObj.Select(MCAD_McSelect.mcSelectionSetAll, Nothing, Nothing, rbfFilter)
            Dim bn1 As Boolean = False
            For i = 0 To selObj.Count - 1
                Dim blockObj = CType(selObj.Item(i), MxDrawBlockReference)
                If Not blockIdCol.Contains(blockObj.ObjectID.ToString) Then
                    Exit For
                End If
                bn1 = True
            Next
            If selObj.Count = 0 OrElse bn1 Then
                Exit Do
            Else
                For i = 0 To selObj.Count - 1
                    Dim blockObj = CType(selObj.Item(i), MxDrawBlockReference)
                    For j = 0 To blockObj.AttributeCount - 1
                        Dim attri As MxDrawAttribute = blockObj.AttributeItem(j)
                        If attri.IsInvisible Then Continue For
                        Dim name As String = attri.TextString
                        'If Regex.IsMatch(name, "^[\u4E00-\u9FA5]+$") Then
                        '    blockObj.Explode() : blockObj.Erase()
                        'End If
                        '  attribute.IsInvisible =
                    Next

                    Dim iteBlock = blockObj.BlockTableRecord.NewIterator
                    iteBlock.Start()
                    Dim bn As Boolean = False
                    Do Until iteBlock.Done
                        If TypeOf iteBlock.GetEntity Is MxDrawCircle Then
                            Dim pC = iteBlock.GetEntity
                        ElseIf TypeOf iteBlock.GetEntity Is MxDrawText Then
                            Dim pT As MxDrawText = CType(iteBlock.GetEntity, MxDrawText)
                        Else
                            bn = True
                            Exit Do
                        End If
                        iteBlock.Step()
                    Loop

                    If bn Then '如果不是轴号和轴号内的文字则砸开
                        blockObj.Explode()
                        blockObj.Erase()
                    Else '将认定为轴号和轴号内的文字存储起来
                        Dim id As String = blockObj.ObjectID.ToString
                        If Not blockIdCol.Contains(id) Then
                            blockIdCol.Add(id, id)
                        End If
                    End If
                    ' blockObj.Erase()
                Next
            End If
        Loop
    End Sub




    ''' <summary>
    ''' 递归获取所有图层的集合数据
    ''' </summary>
    ''' <param name="entity"></param>
    ''' <param name="reDataCol"></param>
    ''' <param name="reNameCol"></param>
    Private Sub GetDraphLayerData(entity As MxDrawEntity, ByRef reDwgData As DWGLayerData)

        If TypeOf entity Is MxDrawLine Then '直线对象
            '  pDwgData.LineCol.Add(entity)
            AddDataCol(entity, reDwgData.LineCol, reDwgData.LayerNameCol)
        ElseIf TypeOf entity Is MxDrawText Then
            ' pDwgData.TextCol+ .Add(entity)
            AddDataCol(entity, reDwgData.TextCol, reDwgData.LayerNameCol)
        ElseIf TypeOf entity Is MxDrawArc Then
            AddDataCol(entity, reDwgData.ArcCol, reDwgData.LayerNameCol)
        ElseIf TypeOf entity Is MxDrawPolyline Then
            AddDataCol(entity, reDwgData.PolyLineCol, reDwgData.LayerNameCol)
        ElseIf TypeOf entity Is MxDrawCircle Then
            AddDataCol(entity, reDwgData.CircleCol, reDwgData.LayerNameCol)
        ElseIf TypeOf entity Is MxDrawBlockReference Then
            ' Dim pBlock As MxDrawBlockReference = CType(entity.Clone, MxDrawBlockReference) '复制
            'Dim blockCopy As MxDrawBlockReference = entity.Copy
            'Dim pResbuf As MxDrawResbuf = blockCopy.Explode() '炸开
            '' Dim strResult = pResbuf.AtString(0)
            'If pResbuf.Count = 0 Then Return
            'Dim pSelectionSet As New MxDrawSelectionSet '获取所有的数据
            'pSelectionSet.Select(MCAD_McSelect.mcSelectionSetAll, Nothing, Nothing, pResbuf)
            'Runtime.InteropServices.Marshal.ReleaseComObject(pResbuf)
            'For i = 0 To pSelectionSet.Count - 1
            '    Dim pEntity As MxDrawEntity = pSelectionSet.Item(i)
            '    GetDraphLayerData(pEntity, reDataCol, reNameCol)
            '    pEntity.Erase()
            'Next
        End If
    End Sub


    Private Sub AddDataCol(entity As MxDrawEntity, ByRef reDataCol As Collection, ByRef reNameCol As Collection)
        If reDataCol.Contains(entity.Layer) Then
            Dim tempCol As Collection = CType(reDataCol.Item(entity.Layer), Collection)
            tempCol.Add(entity)
        Else
            Dim tempCol As New Collection
            tempCol.Add(entity)
            reDataCol.Add(tempCol, entity.Layer)
            If Not reNameCol.Contains(entity.Layer) Then
                reNameCol.Add(entity.Layer, entity.Layer)
            End If

        End If
    End Sub


    Private Sub DrawEntity(obj As Object, originPt As MxDrawPoint, aimPt As MxDrawPoint, layer As String)
        Dim pEntity As MxDrawEntity = CType(obj, MxDrawEntity)
        Dim tempEntity As MxDrawEntity = CType(pEntity.Clone, MxDrawEntity)
        tempEntity.Move(originPt, aimPt)
        ' tempEntity.Layer = layer
        mxCadObj.LayerName = layer
        Dim id As Long = mxCadObj.DrawEntity(tempEntity)
        Dim entity As MxDrawEntity = CType(mxCadObj.ObjectIdToObject(id), MxDrawEntity)
        entity.Layer = layer
    End Sub
End Class
