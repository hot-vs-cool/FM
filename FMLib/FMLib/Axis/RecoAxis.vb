Imports MxDrawXLib
Imports CxAzFunc

'''' <summary>
''''识别轴网数据类， 数据规则，用一个删一个
'''' </summary>
'Public Class RecoAxis1

'    Private mDataCol As Collection '无用
'    Private mNameCol As Collection '无用

'    Private mDwgData As DWGLayerData '图层的全部数据


'    Private mCircleCol As New Collection '圆集合 MxDrawEllipse

'    Private mCXAxisNumbleCol As New Collection '存储组合数据 ，圆、文字、标注线

'    ''' <summary>
'    ''' 单根轴网集合 ,有可能是MxDrawLine;有可能是CXAxisNumble+MxDrawLine +CXAxisNumble;有可能是CXAxisNumble+MxDrawLine 
'    ''' </summary>
'    Private mCXAxisCol As New Collection

'    Sub New(dwgData As DWGLayerData)

'        mDwgData = dwgData
'    End Sub
'    Public Function GetAxis() As Collection
'        ' If mDwgData Is Nothing Then Return Nothing
'        ''  RecoAxisCircle() '获取圆
'        'RecoAxisCircleText() '获取到圆和字体组合的轴号
'        'RecoAxisDimPolyLine() '获取轴网线标注

'        'RecoAxisLine()


'        Return Nothing
'    End Function


'    ''' <summary>
'    ''' 识别出轴网线
'    ''' </summary>
'    Private Sub RecoAxisLine()
'        Dim i = 1
'        While i <= mCXAxisNumbleCol.Count
'            Dim pLine As MxDrawPolyline = CType(mCXAxisNumbleCol.Item(i), CXAxisNumble).DimPolyline
'            If pLine Is Nothing Then Throw New CXException(FMConston.POLINE_IS_NOTHING)
'            Dim pt As MxDrawPoint = CType(mCXAxisNumbleCol.Item(i), CXAxisNumble).DimPolyline.GetEndPoint
'            Dim j = 1
'            While j <= mDataCol.Count
'                Dim tempCol As Collection = CType(mDataCol.Item(j), Collection)
'                If tempCol.Count = 0 Then mDataCol.Remove(j) : Continue While
'                Dim k = 1
'                While k <= tempCol.Count


'                End While

'            End While






'        End While


'    End Sub


'    ''' <summary>
'    ''' 获取轴网轴号旁边的轴线标注
'    ''' </summary>
'    Private Sub RecoAxisDimPolyLine()
'        For i = 1 To mNameCol.Count
'            Dim layName As String = CType(mNameCol.Item(i), String)
'            Dim tempCol As Collection = CType(mDataCol.Item(layName), Collection)
'            Dim isBreck As Boolean = False
'            Dim addCount As Integer
'            Dim j = 1
'            While j <= tempCol.Count
'                Dim pEntity As MxDrawEntity = CType(tempCol.Item(j), MxDrawEntity)
'                If TypeOf pEntity Is MxDrawLine Then
'                    Dim pLine As MxDrawLine = CType(pEntity, MxDrawLine)
'                    Dim isIn As Boolean = False
'                    For Each pcx As CXAxisNumble In mCXAxisNumbleCol
'                        If GeometryMath.IsPtOnCircleEdge(pcx.Circle, pLine.StartPoint) Then
'                            isIn = True
'                            pcx.AddDimLines(pLine)
'                            Exit For
'                        ElseIf GeometryMath.IsPtOnCircleEdge(pcx.Circle, pLine.EndPoint)
'                            isIn = True
'                            pcx.AddDimLines(pLine)
'                            Exit For
'                        End If
'                    Next
'                    If isIn Then
'                        tempCol.Remove(j)
'                        Continue While
'                    End If
'                End If
'                j += 1
'            End While
'            If addCount > 5 Then Exit For
'        Next
'    End Sub


'    ''' <summary>
'    ''' 获取轴号数据 ，（有专门的算法）
'    ''' </summary>
'    Private Sub RecoAxisCircle()
'        'For i = 1 To mNameCol.Count
'        '    Dim layName As String = CType(mNameCol.Item(i), String)
'        '    Dim tempCol As Collection = CType(mDataCol.Item(layName), Collection)
'        '    Dim isBreck As Boolean = False
'        '    Dim addCount As Integer
'        '    Dim j = 1
'        '    While j <= tempCol.Count
'        '        Dim pEntity As MxDrawEntity = CType(tempCol.Item(j), MxDrawEntity)
'        '        If TypeOf pEntity Is MxDrawCircle Then
'        '            addCount = addCount + 1
'        '            mCircleCol.Add(pEntity)
'        '            tempCol.Remove(j)
'        '        Else
'        '            j += 1
'        '        End If
'        '    End While
'        '    If addCount > 5 Then Exit For
'        'Next
'    End Sub

'    ''' <summary>
'    ''' 获取轴号数据中的文字 ，（有专门的算法）
'    ''' </summary>
'    Private Sub RecoAxisCircleText()
'        Dim list As New List(Of String)

'        For i = 1 To mDwgData.CircleCol.Count
'            Dim layName As String = CType(mDwgData.LayerNameCol.Item(i), String)
'            If Not mDwgData.CircleCol.Contains(layName) Then Continue For
'            Dim tempCol As Collection = CType(mDwgData.CircleCol.Item(layName), Collection)
'            Dim isBreck As Boolean = False
'            Dim addCount As Integer
'            Dim j = 1

'            While j <= tempCol.Count
'                Dim pEntity As MxDrawEntity = CType(tempCol.Item(j), MxDrawEntity)
'                If TypeOf pEntity Is MxDrawText Then
'                    Dim pText As MxDrawText = CType(pEntity, MxDrawText)
'                    Dim pCXAxisNumble As CXAxisNumble = Nothing
'                    If IsTextOnCircle(pText, pCXAxisNumble) Then
'                        list.Add(pText.TextString)
'                        mCXAxisNumbleCol.Add(pCXAxisNumble)
'                        tempCol.Remove(j)
'                        Continue While
'                    End If
'                End If
'                j += 1
'            End While

'            If addCount > 5 Then Exit For
'        Next
'    End Sub

'    ''' <summary>
'    ''' 判断字体是否在圆的里面
'    ''' </summary>
'    ''' <param name="text"></param>
'    ''' <returns></returns>
'    Private Function IsTextOnCircle(text As MxDrawText, Optional ByRef reCXAxisNumble As CXAxisNumble = Nothing) As Boolean
'        If mCircleCol Is Nothing Then Return False
'        Dim i = 1
'        While i <= mCircleCol.Count
'            Dim pCircle As MxDrawCircle = CType(mCircleCol.Item(i), MxDrawCircle)
'            If GeometryMath.IsPtOnCircle(pCircle, text.Position) Then
'                reCXAxisNumble = New CXAxisNumble
'                reCXAxisNumble.Circle = pCircle
'                reCXAxisNumble.Text = text
'                mCircleCol.Remove(i)
'                Return True
'            End If
'            i += 1
'        End While
'        Return False
'    End Function
'End Class

''' <summary>
'''识别轴网数据类， 数据规则，需将图砸开
''' </summary>
Public Class RecoAxis
    ''' <summary>
    ''' 存储组合数据 ，圆、文字、标注线
    ''' </summary>
    Private mCXAxisNumbleCol As New Collection '

    ''' <summary>
    ''' 标注线的id ,在识别轴网线的时候需要用到
    ''' </summary>
    Private mDimLineIdCol As New Collection '

    ''' <summary>
    ''' 可能是轴符块的集合（包含字体和圆）
    ''' </summary>
    Private mAxisSignBlockCol As New Collection

    ''' <summary>
    ''' 获取轴网的信息句柄
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAxis() As CXAxisHandle
        Explode() '将数据块砸开，除轴号属性参数

        RecoCXAxisNumbleCol() '识别圆和文字、标注线

        Return RecoAxisLine() '识别轴网线
    End Function

    ''' <summary>
    ''' 识别圆和字体
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>返回和圆相交的线图层名</remarks>
    Private Function RecoCXAxisNumbleCol() As Boolean
        '获取到图纸的所有圆
        Dim pCircleList As List(Of MxDrawEntity) = GraphicFilter.GetEntityOfType(Layer.EntityDxfTypeName.CIRCLE)
        For Each pEntity As MxDrawEntity In pCircleList
            Dim pt1, pt2 As New MxDrawPoint
            pEntity.GetBoundingBox(pt1, pt2) '获取圆的外包矩形宽
            GeometryMath.ScaleBoudingBox(pt1, pt2, 10)
            Dim pResbuf As New MxDrawResbuf '通过外包矩形宽筛选字体和圆
            Dim pFilterStr As String = Layer.EntityDxfTypeName.LINE.ToString & "," & Layer.EntityDxfTypeName.TEXT.ToString
            Dim plistObj = GraphicFilter.GetEntityOfBox(pt1, pt2, pFilterStr)
            Dim pCXAxisNumble As New CXAxisNumble
            pCXAxisNumble.Circle = CType(pEntity.Clone, MxDrawCircle)
            For Each tempEntity In plistObj
                If TypeOf tempEntity Is MxDrawText Then
                    pCXAxisNumble.Text = CType(tempEntity.Clone, MxDrawText)
                ElseIf TypeOf tempEntity Is MxDrawLine Then
                    '识别轴网线
                    RecoAxisDimPolyLine(pCXAxisNumble, CType(tempEntity, MxDrawLine))
                End If
            Next
            If pCXAxisNumble.Text Is Nothing OrElse pCXAxisNumble.DimPolyline Is Nothing Then Continue For
            mCXAxisNumbleCol.Add(pCXAxisNumble)
        Next

        '通过块识别CXAxisNumble
        If mAxisSignBlockCol.Count = 0 Then Return True
        For Each pBlock As MxDrawBlockReference In mAxisSignBlockCol
            Dim pCXAxisNumble As CXAxisNumble = GetCXAxisNumbleByBlock(pBlock)
            If pCXAxisNumble Is Nothing Then Continue For

            Dim pt1, pt2 As New MxDrawPoint
            pBlock.GetBoundingBox(pt1, pt2) '获取圆的外包矩形宽
            GeometryMath.ScaleBoudingBox(pt1, pt2, 10)
            Dim pResbuf As New MxDrawResbuf '通过外包矩形宽筛选字体和圆
            Dim pFilterStr As String = Layer.EntityDxfTypeName.LINE.ToString
            Dim plistObj = GraphicFilter.GetEntityOfBox(pt1, pt2, pFilterStr)
            For Each tempEntity In plistObj
                '识别轴网线
                RecoAxisDimPolyLine(pCXAxisNumble, CType(tempEntity, MxDrawLine))
                If pCXAxisNumble.DimPolyline IsNot Nothing Then Exit For
            Next
            If pCXAxisNumble.DimPolyline Is Nothing Then Continue For
            mCXAxisNumbleCol.Add(pCXAxisNumble)
        Next
        Return True
    End Function

    ''' <summary>
    ''' 通过轴网块获取CXAxisNumble对象
    ''' </summary>
    ''' <param name="blockObj"></param>
    Private Function GetCXAxisNumbleByBlock(blockObj As MxDrawBlockReference) As CXAxisNumble
        Dim pCXAxisNumble As New CXAxisNumble
        For i = 0 To blockObj.AttributeCount - 1 '从块属性中获取字体和圆
            Dim attr As MxDrawAttribute = CType(blockObj.AttributeItem(i), MxDrawAttribute)
            If attr.IsInvisible Then Continue For 'true 为不显示
            pCXAxisNumble.Text = CType(attr.Clone, MxDrawText)
            Exit For
        Next
        Dim pResbuf As MxDrawResbuf = blockObj.Explode
        For i = 0 To pResbuf.Count - 1
            Dim Obj As MxDrawMcDbObject = pResbuf.AtObject(i)
            Dim pObj As MxDrawMcDbObject = CType(Obj.Clone, MxDrawMcDbObject)
            If TypeOf pObj Is MxDrawCircle Then
                pCXAxisNumble.Circle = CType(pObj.Clone, MxDrawCircle)
            ElseIf TypeOf pObj Is MxDrawText Then
                Dim pText As MxDrawText = CType(pObj, MxDrawText)
                If pText.TextString <> "" Then
                    pCXAxisNumble.Text = CType(pObj.Clone, MxDrawText)
                End If
            End If
            Obj.Erase()
        Next

        ''  pResbuf.
        'Dim iteBlock = blockObj.BlockTableRecord.NewIterator
        'iteBlock.Start()
        'Do Until iteBlock.Done '纯块获取字体和圆
        '    If TypeOf iteBlock.GetEntity Is MxDrawCircle Then
        '        pCXAxisNumble.Circle = CType(iteBlock.GetEntity, MxDrawCircle)
        '    ElseIf TypeOf iteBlock.GetEntity Is MxDrawText Then
        '        Dim pText As MxDrawText = CType(iteBlock.GetEntity, MxDrawText)
        '        If pText.TextString <> "" Then
        '            pCXAxisNumble.Text = CType(iteBlock.GetEntity, MxDrawText)
        '        End If
        '    End If
        '    iteBlock.Step()
        'Loop


        If pCXAxisNumble.Circle Is Nothing OrElse pCXAxisNumble.Text Is Nothing Then Return Nothing
        Return pCXAxisNumble
    End Function

    Private Sub RecoAxisDimPolyLine(ByRef cXAxisNumble As CXAxisNumble, pLine As MxDrawLine)
        Dim pCircle As MxDrawCircle = cXAxisNumble.Circle
        If GeometryMath.IsPtOnCircleEdge(pCircle, pLine.StartPoint, 10) OrElse GeometryMath.IsPtOnCircleEdge(pCircle, pLine.EndPoint, 10) Then
            cXAxisNumble.AddDimLines(pLine)
            Dim objIdStr As String = pLine.ObjectID.ToString
            If Not mDimLineIdCol.Contains(objIdStr) Then mDimLineIdCol.Add(objIdStr, objIdStr)


            Dim aimpt As MxDrawPoint = cXAxisNumble.DimPolyline.GetEndPoint
            '通过点扩大1m进行过滤 '
            Dim pLineList As List(Of MxDrawEntity) = Graphic.GraphicFilter.GetEntityByPoint(aimpt, 1, pLine.Layer, Layer.EntityDxfTypeName.LINE)

            ''获取有关图层的线集合
            'Dim pLineList As List(Of MxDrawEntity) = GraphicFilter.GetEntityOfLayer(pLine.Layer, Layer.EntityDxfTypeName.LINE.ToString)
            'Dim i = 0
            '可能存在相连的标注线
            Dim k = 0
            While k < pLineList.Count
                Dim pLinek As MxDrawLine = CType(pLineList.Item(k), MxDrawLine)
                If pLine.ObjectID = pLinek.ObjectID Then k += 1 : Continue While
                Dim pt As MxDrawPoint = cXAxisNumble.DimPolyline.GetEndPoint
                '标注线与标注线之间是相连的
                If pt.IsEqualTo(pLinek.StartPoint, 1) OrElse pt.IsEqualTo(pLinek.EndPoint, 1) Then
                    cXAxisNumble.AddDimLines(pLinek)
                    Dim objIdStr1 As String = pLinek.ObjectID.ToString
                    If Not mDimLineIdCol.Contains(objIdStr1) Then mDimLineIdCol.Add(objIdStr1, objIdStr1)
                    pLineList.RemoveAt(k)
                    Exit While
                Else
                    k += 1
                End If
            End While
        End If
    End Sub

    ''' <summary>
    ''' 识别轴网线
    ''' </summary>
    ''' <remarks></remarks>
    Private Function RecoAxisLine() As CXAxisHandle
        Dim i = 1
        Dim cxAxisHandle As New CXAxisHandle
        While i <= mCXAxisNumbleCol.Count
            Dim pCXAxisNumble As CXAxisNumble = CType(mCXAxisNumbleCol.Item(i), CXAxisNumble)
            mCXAxisNumbleCol.Remove(i)
            Dim startPt As MxDrawPoint = pCXAxisNumble.DimPolyline.GetEndPoint
            Dim Pt As MxDrawPoint = pCXAxisNumble.DimPolyline.GetPointAt(pCXAxisNumble.DimPolyline.NumVerts - 2)
            Dim endPt As MxDrawPoint = CxAzFunc.GeometryMath.GetExtendPoint(Pt, startPt, 1000)

            Dim ps As MxDrawPoints = CxAzFunc.Graphic.GraphicConstruct.ConstructPsByLine(startPt, endPt, 2)
            Dim pLineList As List(Of MxDrawEntity) = Graphic.GraphicFilter.GetEntityByPs(ps, , Layer.EntityDxfTypeName.LINE)
            '通过点扩大1mm进行过滤
            '   Dim pLineList As List(Of MxDrawEntity) = Graphic.GraphicFilter.GetEntityByRect(startPt, endPt,, Layer.EntityDxfTypeName.LINE)
            '  Dim pLineList As List(Of MxDrawEntity) = Graphic.GraphicFilter.GetEntityByPoint(pt, 1,, Layer.EntityDxfTypeName.LINE)
            If pLineList Is Nothing Then Continue While
            Dim tempList As New List(Of MxDrawEntity)
            For Each pEntity In pLineList '如果筛选到的是轴网标注的id则排除
                If mDimLineIdCol.Contains(pEntity.ObjectID.ToString) Then Continue For
                Dim pLine As MxDrawCurve = CType(pEntity, MxDrawCurve)
                If Not CxAzFunc.GeometryMath.IsParallel(pLine.GetStartPoint, pLine.GetEndPoint, startPt, endPt) Then Continue For
                '没有判断是否共线的情况
                tempList.Add(pEntity)
            Next

            If tempList.Count < 1 Then Continue While 'mCXAxisNumbleCol.Remove(i) :
            'If pCXAxisNumble.Text.TextString = "M" Then
            '    Dim tempStr As String = ""
            'End If
            Dim pCxAxis As New CXAxis
            pCxAxis.AxisNumbles.Add(pCXAxisNumble)
            pCxAxis.Curve = CType(tempList.Item(0), MxDrawCurve)
            Dim tempPt1 As MxDrawPoint = pCxAxis.Curve.GetStartPoint
            Dim tempPt2 As MxDrawPoint = pCxAxis.Curve.GetEndPoint
            Dim j = 1
            While j <= mCXAxisNumbleCol.Count '再次寻找轴号
                Dim pCXAxisNumblej As CXAxisNumble = CType(mCXAxisNumbleCol.Item(j), CXAxisNumble)
                Dim ptj As MxDrawPoint = pCXAxisNumblej.DimPolyline.GetEndPoint
                '判断点是否在线上
                If CxAzFunc.GeometryMath.IsPointOnLine(ptj, tempPt1, tempPt2, True) Then
                    'If pCXAxisNumblej.Text.TextString = "2" Then 'GeometryMath.IsPtOnLine(pCxAxis.Curve, ptj)
                    '    Dim tempStr As String = ""
                    'End If
                    pCxAxis.AxisNumbles.Add(pCXAxisNumblej)
                    mCXAxisNumbleCol.Remove(j)
                    Exit While
                Else
                    j += 1
                End If
            End While
            cxAxisHandle.AddCXAxis(pCxAxis)
        End While
        Return cxAxisHandle
    End Function

    ''' <summary>
    ''' 炸开数据块
    ''' </summary>
    Private Sub Explode()
        ' Dim blockIdCol As New Collection '块的id集合
        Do
            Dim selObj As MxDrawSelectionSet = GetBlockSelectionSet(mAxisSignBlockCol)
            If selObj Is Nothing Then Exit Do
            For i = 0 To selObj.Count - 1
                Dim blockObj = CType(selObj.Item(i), MxDrawBlockReference)
                If IsBlockAttributeAxis(blockObj) Then
                    Dim key As String = blockObj.ObjectID.ToString
                    '将认定为轴号和轴号内的文字存储起来
                    If Not mAxisSignBlockCol.Contains(key) Then mAxisSignBlockCol.Add(blockObj, key)
                Else '如果不是轴号和轴号内的文字则砸开
                    blockObj.Explode()
                    blockObj.Erase()
                End If
            Next
        Loop
    End Sub


    ''' <summary>
    ''' 判断块属性是否为轴网的轴号和轴号中的字体
    ''' </summary>
    ''' <returns></returns>
    Private Function IsBlockAttributeAxis(blockObj As MxDrawBlockReference) As Boolean
        '如果块中有除圆和字体的数据 返回false
        If Not BlockIsTextAndCircle(blockObj) Then Return False

        If blockObj.AttributeCount = 0 Then Return True


        '判断 字体是否有0-9，a-z，A-Z,/ 否则返回错误
        For j = 0 To blockObj.AttributeCount - 1
            Dim attri As MxDrawAttribute = blockObj.AttributeItem(j)
            If attri.IsInvisible Then Continue For
            Dim name As String = attri.TextString
            'If Regex.IsMatch(name, "^[\u4E00-\u9FA5]+$") Then
            '    blockObj.Explode() : blockObj.Erase()
            'End If
            '  attribute.IsInvisible =
        Next

        Return True
    End Function

    ''' <summary>
    ''' 判断块是否是文字和圆
    ''' </summary>
    ''' <param name="blockObj"></param>
    ''' <returns></returns>
    Private Function BlockIsTextAndCircle(blockObj As MxDrawBlockReference) As Boolean
        Dim iteBlock = blockObj.BlockTableRecord.NewIterator
        iteBlock.Start()
        Do Until iteBlock.Done
            If TypeOf iteBlock.GetEntity Is MxDrawCircle Then
                Dim pC = iteBlock.GetEntity
            ElseIf TypeOf iteBlock.GetEntity Is MxDrawText Then
                Dim pT As MxDrawText = CType(iteBlock.GetEntity, MxDrawText)
            Else
                Return False
            End If
            iteBlock.Step()
        Loop
        Return True
    End Function


    ''' <summary>
    ''' 获取需要砸开的数据块 
    ''' </summary>
    ''' <param name="blockIdCol"></param>
    ''' <returns></returns>
    Private Function GetBlockSelectionSet(blockIdCol As Collection) As MxDrawSelectionSet
        Dim rbfFilter As New MxDrawResbuf
        rbfFilter.AddStringEx(Layer.EntityDxfTypeName.INSERT.ToString, Layer.DxfCode.CommonEntity)
        Dim selObj As New MxDrawSelectionSet
        selObj.Select(MCAD_McSelect.mcSelectionSetAll, Nothing, Nothing, rbfFilter)
        If selObj.Count = 0 Then Return Nothing
        For i = 0 To selObj.Count - 1
            Dim blockObj = CType(selObj.Item(i), MxDrawBlockReference)
            If blockIdCol Is Nothing OrElse blockIdCol.Count = 0 Then Return selObj
            If Not blockIdCol.Contains(blockObj.ObjectID.ToString) Then
                Return selObj
            End If
        Next
        Return Nothing
    End Function

End Class

