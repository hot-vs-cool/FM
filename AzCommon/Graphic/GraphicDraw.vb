Imports System.Text.RegularExpressions

Namespace Graphic

    ''' <summary>
    ''' CAD实体绘制类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GraphicDraw

        ''' <summary>
        ''' 写入块对象属性
        ''' </summary>
        ''' <param name="entComponent">待写入的构件图块对象</param>
        ''' <param name="componentInfo">待写入的构件图块对应的数据对象</param>
        ''' <param name="markTextAlignPt">与构件图块关联的标记文本对齐点</param>
        ''' <remarks></remarks>
        Private Shared Sub WriteBlockProperty(ByVal entComponent As MxDrawBlockReference, ByVal componentInfo As Component.ComponentInfo, ByVal markTextAlignPt As MxDrawPoint)
            '块属性重置
            If entComponent.AttributeCount < 4 Then
                Do While entComponent.AttributeCount < 4
                    entComponent.AppendAttribute()
                Loop
            ElseIf entComponent.AttributeCount > 4 Then
                Do While entComponent.AttributeCount > 4
                    entComponent.AttributeItem(4).Erase()
                Loop
            End If
            '写入构件实体标注文本
            Dim attMartText = entComponent.AttributeItem(0)
            attMartText.Height = 100
            attMartText.Tag = "MarkText"
            attMartText.AlignmentPoint = markTextAlignPt
            attMartText.verticalMode = MCAD_McVerticalAlignment.mcVerticalAlignmentTop '设置文本顶端对齐
            attMartText.TextString = componentInfo.MarkText
            attMartText.IsInvisible = False '"False"表示显示该属性文本
            attMartText.colorIndex = MCAD_COLOR.mcWhite
            '写入单构件计算式
            Dim attCalcFormula = entComponent.AttributeItem(1)
            attCalcFormula.Tag = "CalcFormula"
            attCalcFormula.TextString = componentInfo.CalcFormula
            '写入单构件默认套数
            Dim attCalcMultiple = entComponent.AttributeItem(2)
            attCalcMultiple.Tag = "Multiple"
            attCalcMultiple.TextString = "1"
            '写入单构件计算值
            Dim attCalcValue = entComponent.AttributeItem(3)
            attCalcValue.Tag = "CalcValue"
            attCalcValue.TextString = componentInfo.CalcValue.ToString
        End Sub

        ''' <summary>
        ''' 原位重绘构件,一般用于将底图中的实体,转换成构件实体,如果转换成功则返回构件实体ID,否则返回0
        ''' </summary>
        ''' <param name="entOrigin">底图实体对象</param>
        ''' <param name="componentInfo">构件数据对象</param>
        ''' <param name="markTextPt">可选,标注文本插入基点,默认采用图形中心点作为文本插入点</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DrawCptInSameLocation(ByRef entOrigin As MxDrawEntity, ByVal componentInfo As Component.ComponentInfo, _
    Optional ByVal markTextPt As MxDrawPoint = Nothing) As Long
            Dim centerPoint As New MxDrawPoint
            If markTextPt Is Nothing Then
                markTextPt = New MxDrawPoint
                Dim minPt, maxPt As New MxDrawPoint
                entOrigin.GetBoundingBox(minPt, maxPt)
                centerPoint.x = (minPt.x + maxPt.x) / 2
                centerPoint.y = (minPt.y + maxPt.y) / 2
                markTextPt.x = maxPt.x : markTextPt.y = minPt.y
            End If
            Dim blockObj As MxDrawBlockReference = Nothing
            If TypeOf entOrigin Is MxDrawBlockReference Then
                Dim blockOrigin = CType(entOrigin, MxDrawBlockReference)
                Dim scale = blockOrigin.ScaleFactors
                blockObj = blockOrigin.Database.CurrentSpace.InsertBlock(blockOrigin.Position.x, blockOrigin.Position.y, _
            blockOrigin.GetBlockName, 1, blockOrigin.Rotation)
                blockObj.ScaleFactors = blockOrigin.ScaleFactors
            Else
                Dim blockRec = GraphicConstruct.DefineBlockObj({entOrigin})
                blockObj = entOrigin.Database.CurrentSpace.InsertBlock(centerPoint.x, centerPoint.y, blockRec.Name)
            End If
            Dim insertPt = blockObj.Position : insertPt.z = componentInfo.Position.z
            blockObj.Position = insertPt
            Dim layerRec = Layer.LayerOperate.InitLayer(componentInfo.ComponentType, componentInfo.KindProjId)
            blockObj.Layer = layerRec.Name
            componentInfo.WriteCommonXData(CType(blockObj, MxDrawEntity), entOrigin.handle)
            WriteBlockProperty(blockObj, componentInfo, markTextPt)
            blockObj.AssertWriteEnabled()
            entOrigin.Visible = False '底图隐藏
            Return blockObj.ObjectID
        End Function

        ''' <summary>
        ''' 通过图形数据库块表记录绘制构件,一般用于批量识别底图中的实体,如果绘制成功则返回构件实体ID,否则返回0
        ''' </summary>
        ''' <param name="blockRec">待绘制块表记录对象</param>
        ''' <param name="componentInfo">构件数据对象</param>
        ''' <param name="rotateAngle">可选，选择角度默认为0</param>
        ''' <param name="scale">可选，缩放比例，默认为1即不缩放</param>
        ''' <param name="markTextPt">可选,标注文本插入基点,默认采用图块基点作为文本左上角对齐点</param>
        ''' <param name="markTextHeight">可选,标注文本高度值,默认采用100</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DrawCptByBlockRecorder(ByRef blockRec As MxDrawBlockTableRecord, ByVal componentInfo As Component.ComponentInfo, Optional ByVal rotateAngle As Double = 0, _
Optional ByVal scale As Double = 1, Optional ByVal markTextPt As MxDrawPoint = Nothing, Optional ByVal markTextHeight As Integer = 100) As Long
            Dim blockObj = blockRec.Database.CurrentSpace.InsertBlock(componentInfo.Position.x, componentInfo.Position.y, blockRec.Name, scale, rotateAngle)
            Dim layerRec = Layer.LayerOperate.InitLayer(componentInfo.ComponentType, componentInfo.KindProjId)
            blockObj.Layer = layerRec.Name
            blockObj.LineType = "ByLayer"
            blockObj.Position = componentInfo.Position
            blockObj.Lineweight = MCAD_LWEIGHT.mcLnWtByLayer
            blockObj.TrueColor.ColorMethod = MCAD_McColorMethod.mcColorMethodByLayer
            componentInfo.WriteCommonXData(CType(blockObj, MxDrawEntity))
            If markTextPt Is Nothing Then markTextPt = componentInfo.Position
            WriteBlockProperty(blockObj, componentInfo, markTextPt)
            blockObj.AssertWriteEnabled()
            Dim entityObjId = blockObj.ObjectID
            Return entityObjId
        End Function

        ''' <summary>
        ''' 绘制安装专业水平线类型构件
        ''' </summary>
        ''' <param name="cadObj">承载图形数据库的CAD控件对象</param>
        ''' <param name="startPoint">构件起点坐标</param>
        ''' <param name="endPoint">构件终点坐标</param>
        ''' <param name="originHandle">底图图形句柄,默认为“0”即不存在底图句柄,如果句柄指向的对象实体存在则会隐藏该实体</param>
        ''' <param name="textHeight">可选，备注文本高度，默认为100</param>
        ''' <param name="constantWidth">可选，多段线固定线宽值，默认采用全局线宽</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DrawLineCpt(ByVal cadObj As AxMxDrawXLib.AxMxDrawX, ByVal startPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint, _
    ByVal lineCptInfo As Component.ComponentInfo, Optional ByVal originHandle As String = "0", Optional ByVal textHeight As Integer = 100, Optional ByVal constantWidth As Integer = 40) As Long
            Dim scaleFactor As New MxDrawScale3d
            Dim blockRec As MxDrawBlockTableRecord = Nothing
            Dim layerRec = Layer.LayerOperate.InitLayer(lineCptInfo.ComponentType, lineCptInfo.KindProjId)
            If lineCptInfo.IsLayOnCeiling Then
                blockRec = CType(cadObj.GetDatabase, MxDrawDatabase).GetBlockTable.GetAt("CX00$SolidLine$002A")
            Else
                blockRec = CType(cadObj.GetDatabase, MxDrawDatabase).GetBlockTable.GetAt("CX00$DashLine$002A")
            End If
            Dim lineAngle = GeometryMath.GetMinAngleToXAxis(startPoint, endPoint)
            '计算直线中点坐标
            Dim centerPt As New MxDrawPoint
            centerPt.x = (startPoint.x + endPoint.x) / 2
            centerPt.y = (startPoint.y + endPoint.y) / 2
            centerPt.z = lineCptInfo.Position.z
            '线类型构件绘制
            scaleFactor.sx = GeometryMath.CalcDistance(startPoint, endPoint) : scaleFactor.sy = constantWidth
            Dim blockLine = blockRec.Database.CurrentSpace.InsertBlock(centerPt.x, centerPt.y, blockRec.Name, 1, lineAngle)
            blockLine.Position = centerPt
            blockLine.Layer = layerRec.Name
            blockLine.ScaleFactors = scaleFactor
            WriteBlockProperty(blockLine, lineCptInfo, centerPt)
            '写入备注文本字符串属性
            Dim attMark = blockLine.AttributeItem(0)
            attMark.Height = textHeight
            attMark.horizontalMode = MCAD_McHorizontalAlignment.mcHorizontalAlignmentCenter
            '计算属性文本中心点坐标
            Dim minPt As MxDrawPoint = Nothing
            Dim maxPt As MxDrawPoint = Nothing
            attMark.GetBoundingBox(minPt, maxPt)
            Dim centerTextPoint As New MxDrawPoint
            centerTextPoint.y = (minPt.y + maxPt.y) / 2
            centerTextPoint.x = (minPt.x + maxPt.x) / 2
            Dim movePt As MxDrawPoint = Nothing
            If Math.Abs(lineAngle - Math.PI / 2) < 0.001 OrElse lineAngle > Math.PI / 2 Then
                movePt = GeometryMath.GetExtendPoint(centerPt, lineAngle - Math.PI / 2, textHeight / 2 + constantWidth / 2 + 35)
                attMark.Rotate(centerTextPoint, lineAngle + Math.PI)
            Else
                movePt = GeometryMath.GetExtendPoint(centerPt, lineAngle + Math.PI / 2, textHeight / 2 + constantWidth / 2 + 35)
                attMark.Rotate(centerTextPoint, lineAngle)
            End If
            attMark.Move(centerTextPoint, movePt)
            '如果存在底图句柄则隐藏底图对象实体
            If originHandle <> "0" Then
                Dim entObj = cadObj.HandleToObject(originHandle)
                If entObj IsNot Nothing Then CType(entObj, MxDrawEntity).Visible = False
            End If
            '将实体属性写入扩展数据
            lineCptInfo.WriteCommonXData(CType(blockLine, MxDrawEntity), originHandle)
            lineCptInfo.WriteGxXData(CType(blockLine, MxDrawEntity), False, False)
            blockLine.AssertWriteEnabled()
            Return blockLine.ObjectID
        End Function

        ''' <summary>
        ''' 通过预定义块对象绘制安装专业水平线类型构件
        ''' </summary>
        ''' <param name="blockRec">预定义的快对象对应的块表记录</param>
        ''' <param name="scaleFactor">对象空间缩放系数</param>
        ''' <param name="startPoint">构件起点坐标</param>
        ''' <param name="endPoint">构件终点坐标</param>
        ''' <param name="textHeight">可选，备注文本高度，默认为100</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DrawLineCptByBlockRec(ByVal blockRec As MxDrawBlockTableRecord, ByVal scaleFactor As MxDrawScale3d, ByVal startPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint, _
    ByVal lineCptInfo As Component.ComponentInfo, Optional ByVal textHeight As Integer = 100) As Long
            Dim layerRec = Layer.LayerOperate.InitLayer(lineCptInfo.ComponentType, lineCptInfo.KindProjId)
            Dim lineAngle = GeometryMath.GetMinAngleToXAxis(startPoint, endPoint)
            '计算直线中点坐标
            Dim centerLinePoint As New MxDrawPoint
            centerLinePoint.x = (startPoint.x + endPoint.x) / 2
            centerLinePoint.y = (startPoint.y + endPoint.y) / 2
            centerLinePoint.z = lineCptInfo.Position.z
            Dim blockObj = blockRec.Database.CurrentSpace.InsertBlock(centerLinePoint.x, centerLinePoint.y, blockRec.Name, 1, lineAngle)
            blockObj.ScaleFactors = scaleFactor
            Dim insertPt = blockObj.Position
            insertPt.z = lineCptInfo.Position.z
            blockObj.Position = insertPt
            blockObj.Layer = layerRec.Name
            WriteBlockProperty(blockObj, lineCptInfo, insertPt)
            '写入备注文本字符串属性
            Dim attMark = blockObj.AttributeItem(0)
            attMark.Height = textHeight
            attMark.horizontalMode = MCAD_McHorizontalAlignment.mcHorizontalAlignmentCenter
            '计算属性文本中心点坐标
            Dim minPt As MxDrawPoint = Nothing
            Dim maxPt As MxDrawPoint = Nothing
            attMark.GetBoundingBox(minPt, maxPt)
            Dim centerTextPoint As New MxDrawPoint
            centerTextPoint.y = (minPt.y + maxPt.y) / 2
            centerTextPoint.x = (minPt.x + maxPt.x) / 2
            Dim movePt As MxDrawPoint = Nothing
            If Math.Abs(lineAngle - Math.PI / 2) < 0.001 OrElse lineAngle > Math.PI / 2 Then
                movePt = GeometryMath.GetExtendPoint(centerLinePoint, lineAngle - Math.PI / 2, textHeight / 2 + 35)
                attMark.Rotate(centerTextPoint, lineAngle + Math.PI)
            Else
                movePt = GeometryMath.GetExtendPoint(centerLinePoint, lineAngle + Math.PI / 2, textHeight / 2 + 35)
                attMark.Rotate(centerTextPoint, lineAngle)
            End If
            attMark.Move(centerTextPoint, movePt)
            attMark.colorIndex = MCAD_COLOR.mcWhite
            '将实体属性写入扩展数据
            lineCptInfo.WriteCommonXData(CType(blockObj, MxDrawEntity))
            lineCptInfo.WriteGxXData(CType(blockObj, MxDrawEntity), False, False)
            blockObj.AssertWriteEnabled()
            Return blockObj.ObjectID
        End Function

        ''' <summary>
        ''' 通过以构造的实体对象绘制安装专业水平线类型构件
        ''' </summary>
        ''' <param name="entityObj">待绘制线性构件实体对象</param>
        ''' <param name="startPoint">构件起点坐标</param>
        ''' <param name="endPoint">构件终点坐标</param>
        ''' <param name="textHeight">可选，备注文本高度，默认为100</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DrawLineCptByEntity(ByVal entityObj As MxDrawEntity, ByVal startPoint As MxDrawPoint, ByVal endPoint As MxDrawPoint, _
    ByVal lineCptInfo As Component.ComponentInfo, Optional ByVal textHeight As Integer = 100) As Long
            If entityObj Is Nothing Then Return Nothing
            Dim layerRec = Layer.LayerOperate.InitLayer(lineCptInfo.ComponentType, lineCptInfo.KindProjId)
            Dim lineAngle = GeometryMath.GetMinAngleToXAxis(startPoint, endPoint)
            '计算直线中点坐标
            Dim centerLinePoint As New MxDrawPoint
            centerLinePoint.x = (startPoint.x + endPoint.x) / 2
            centerLinePoint.y = (startPoint.y + endPoint.y) / 2
            centerLinePoint.z = lineCptInfo.Position.z
            Dim blockObj As MxDrawBlockReference = Nothing
            If TypeOf entityObj Is MxDrawBlockReference Then
                blockObj = CType(MxMainCadObj.ObjectIdToObject(MdlCommonDeclare.MxMainCadObj.DrawEntity(entityObj)), MxDrawBlockReference)
                Dim insertPt = blockObj.Position : insertPt.z = lineCptInfo.Position.z
                blockObj.Position = insertPt
            Else
                Dim cadDb = CType(MdlCommonDeclare.MxMainCadObj.GetDatabase, MxDrawDatabase)
                Dim blockTable = cadDb.GetBlockTable
                Dim blockRec = blockTable.Add("") '创建匿名块
                blockRec.Origin = centerLinePoint '设置块基点
                Dim addEntity = blockRec.AddEntityEx(entityObj)
                addEntity.Layer = "0" : addEntity.colorIndex = MCAD_COLOR.mcByLayer
                addEntity.Close()
                blockObj = CType(MxMainCadObj.ObjectIdToObject(MxMainCadObj.DrawBlockReference(centerLinePoint.x, centerLinePoint.y, blockRec.Name, 1, 0)), MxDrawBlockReference)
                blockObj.Position = centerLinePoint
            End If
            '线类型构件绘制
            blockObj.Layer = layerRec.Name
            '写入备注文本字符串属性
            Dim attMark = blockObj.AppendAttribute
            attMark.Height = textHeight
            attMark.IsInvisible = False
            attMark.Tag = "Mark"
            attMark.TextString = lineCptInfo.MarkText
            attMark.horizontalMode = MCAD_McHorizontalAlignment.mcHorizontalAlignmentCenter
            attMark.WidthFactor = 0.8
            MdlCommonDeclare.MxMainCadObj.SetEntityDrawOrder(attMark.ObjectID, 2) '顶级显示
            '计算属性文本中心点坐标
            Dim minPt As MxDrawPoint = Nothing
            Dim maxPt As MxDrawPoint = Nothing
            attMark.GetBoundingBox(minPt, maxPt)
            Dim centerTextPoint As New MxDrawPoint
            centerTextPoint.y = (minPt.y + maxPt.y) / 2
            centerTextPoint.x = (minPt.x + maxPt.x) / 2
            Dim movePt As MxDrawPoint = Nothing
            If Math.Abs(lineAngle - Math.PI / 2) < 0.001 OrElse lineAngle > Math.PI / 2 Then
                movePt = GeometryMath.GetExtendPoint(centerLinePoint, lineAngle - Math.PI / 2, textHeight / 2 + 35)
                attMark.Rotate(centerTextPoint, lineAngle + Math.PI)
            Else
                movePt = GeometryMath.GetExtendPoint(centerLinePoint, lineAngle + Math.PI / 2, textHeight / 2 + 35)
                attMark.Rotate(centerTextPoint, lineAngle)
            End If
            attMark.Move(centerTextPoint, movePt)
            attMark.colorIndex = MCAD_COLOR.mcWhite
            '写入单构件计算式
            Dim attFormula = blockObj.AppendAttribute
            attFormula.Tag = "CalcFormula"
            attFormula.TextString = lineCptInfo.CalcFormula
            '写入单构件默认套数
            Dim attCalcMultiple = blockObj.AppendAttribute
            attCalcMultiple.Tag = "Multiple"
            attCalcMultiple.TextString = "1"
            '写入单构件计算值
            Dim attCalcValue = blockObj.AppendAttribute
            attCalcValue.Tag = "CalcValue"
            attCalcValue.TextString = lineCptInfo.CalcValue.ToString
            '将实体属性写入扩展数据
            lineCptInfo.WriteCommonXData(CType(blockObj, MxDrawEntity), entityObj.handle)
            lineCptInfo.WriteGxXData(CType(blockObj, MxDrawEntity), False, False)
            If TypeOf entityObj Is MxDrawBlockReference Then
                blockObj.SetxDataDouble("Angle", 0, CType(entityObj, MxDrawBlockReference).Rotation)
            End If
            blockObj.AssertWriteEnabled()
            entityObj.Visible = False
            Return blockObj.ObjectID
        End Function

    End Class

End Namespace
