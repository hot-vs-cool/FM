
Namespace Graphic

    ''' <summary>
    ''' 图形参数类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GraphicParameter

        ''' <summary>
        ''' 获取由单位长度的基础矩形图块缩放而成的矩形构件的中线起点、终点坐标
        ''' </summary>
        ''' <param name="componentObj">由单位长度的基础矩形图块缩放而成的矩形构件,如桥架、风管</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetRectangleComponentMiddleLinePt(ByVal componentObj As MxDrawBlockReference) As KeyValuePair(Of MxDrawPoint, MxDrawPoint)
            Dim startPt, endPt As MxDrawPoint
            startPt = GeometryMath.GetExtendPoint(componentObj.Position, componentObj.Rotation, componentObj.ScaleFactors.sx / 2)
            endPt = GeometryMath.GetExtendPoint(componentObj.Position, componentObj.Rotation, -componentObj.ScaleFactors.sx / 2)
            Return New KeyValuePair(Of MxDrawPoint, MxDrawPoint)(startPt, endPt)
        End Function

        ''' <summary>
        ''' 获取由单位长度的基础矩形图块缩放而成的矩形构件角点坐标点(非外包矩形)，角点坐标顺序依次为左下角、右下角、中线右端点、右上角、左上角、中线左端点
        ''' </summary>
        ''' <param name="componentObj">由单位长度的基础矩形图块缩放而成的矩形构件,如桥架、风管</param>
        ''' <param name="extendValue">可选，边界扩充值，默认值为0即不进行扩充</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetRectangleComponentVertex(ByVal componentObj As MxDrawBlockReference, Optional ByVal extendValue As Double = 0) As MxDrawPoints
            Dim polygonPts As New MxDrawPoints
            Dim angle = Math.Atan(componentObj.ScaleFactors.sy / componentObj.ScaleFactors.sx)
            Dim hypotenuseLength = ((componentObj.ScaleFactors.sx) ^ 2 + (componentObj.ScaleFactors.sy) ^ 2) ^ 0.5 / 2 + extendValue
            Dim leftBottomPt = GeometryMath.GetExtendPoint(componentObj.Position, componentObj.Rotation + Math.PI + angle, hypotenuseLength)
            Dim rightBottomPt = GeometryMath.GetExtendPoint(componentObj.Position, componentObj.Rotation - angle, hypotenuseLength)
            Dim rightUpPt = GeometryMath.GetExtendPoint(componentObj.Position, componentObj.Rotation + angle, hypotenuseLength)
            Dim leftUpPt = GeometryMath.GetExtendPoint(componentObj.Position, componentObj.Rotation + Math.PI - angle, hypotenuseLength)
            polygonPts.Add2(leftBottomPt)
            polygonPts.Add2(rightBottomPt)
            polygonPts.Add((rightBottomPt.x + rightUpPt.x) / 2, (rightBottomPt.y + rightUpPt.y) / 2, 0) '中线右端点
            polygonPts.Add2(rightUpPt) '右上角点
            polygonPts.Add2(leftUpPt) '左上角点
            polygonPts.Add((leftBottomPt.x + leftUpPt.x) / 2, (leftBottomPt.y + leftUpPt.y) / 2, 0) '中线左端点
            Return polygonPts
        End Function

        ''' <summary>
        ''' 获取CAD文本对象的文本内容(含单行和多行文本对象),获取的文本内容不去除左右两端的空格,如果获取失败则返回空字符串
        ''' </summary>
        ''' <param name="cadObj">多行文本所属CAD控件对象</param>
        ''' <param name="entityObj">待对象实体</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetTextObjContent(ByVal cadObj As AxMxDrawXLib.AxMxDrawX, ByVal entityObj As MxDrawEntity) As String
            If TypeOf entityObj Is MxDrawText Then
                Return CType(entityObj, MxDrawText).TextString
            ElseIf TypeOf entityObj Is MxDrawMText Then
                Dim mTextContent As String = ""
                Dim rbfEntityId, rbfMTextContent As New MxDrawResbuf
                rbfEntityId.AddObjectId(entityObj.ObjectID)
                rbfMTextContent = CType(cadObj.CallEx("Mx_GetMTextContent", rbfEntityId), MxDrawResbuf)
                If rbfMTextContent.AtString(0) = "OK" Then
                    mTextContent = rbfMTextContent.AtString(1)
                End If
                Runtime.InteropServices.Marshal.ReleaseComObject(rbfEntityId)
                Runtime.InteropServices.Marshal.ReleaseComObject(rbfMTextContent)
                Return mTextContent
            Else
                Return ""
            End If
        End Function

        ''' <summary>
        ''' 分解指定的实体对象，如果成功者返回分解后所得到的实体，否则返回Nothing
        ''' </summary>
        ''' <param name="blockObj">待分解的实体对象</param>
        ''' <param name="isErase">指示是否删除分解后的对象实体,只返回实体对象内存副本</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ExplodeBlockObj(ByVal blockObj As MxDrawEntity, ByVal isErase As Boolean) As List(Of MxDrawEntity)
            Dim rbfExplodeId As New MxDrawResbuf
            rbfExplodeId.AddObjectId(blockObj.ObjectID)
            Dim lisEntity As List(Of MxDrawEntity) = Nothing
            Dim rbfReturId = CType(MdlCommonDeclare.MxMainCadObj.CallEx("Mx_Explode", rbfExplodeId), MxDrawResbuf)
            If rbfReturId.AtString(0) = "Ok" Then '分解成功
                lisEntity = New List(Of MxDrawEntity)
                If isErase Then
                    For i = 1 To rbfReturId.Count - 1
                        Dim tempEntity = CType(MdlCommonDeclare.MxMainCadObj.ObjectIdToObject(rbfReturId.AtLong(i)), MxDrawEntity)
                        lisEntity.Add(CType(tempEntity.Clone, MxDrawEntity))
                        tempEntity.Erase()
                    Next
                Else
                    For i = 1 To rbfReturId.Count - 1
                        Dim tempEntity = CType(MdlCommonDeclare.MxMainCadObj.ObjectIdToObject(rbfReturId.AtLong(i)), MxDrawEntity)
                        lisEntity.Add(tempEntity)
                    Next
                End If
            End If
            Runtime.InteropServices.Marshal.ReleaseComObject(rbfExplodeId)
            Runtime.InteropServices.Marshal.ReleaseComObject(rbfReturId)
            If lisEntity IsNot Nothing AndAlso lisEntity.Count > 0 Then
                Return lisEntity
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' 获取指定ID的实体对象的位图,如果获取失败则返回Nothing
        ''' </summary>
        ''' <param name="cadObj">承载实体所属图形数据库的CAD控件对象</param>
        ''' <param name="entityId">实体ID值</param>
        ''' <param name="bytBitmap">可选构件实体对象位图的二进制字节数组</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEntityBitmap(ByVal cadObj As AxMxDrawXLib.AxMxDrawX, ByVal entityId As Long, Optional ByRef bytBitmap() As Byte = Nothing) As Bitmap
            Dim sysPath = Application.StartupPath
            If cadObj.DrawEntityToJpg(entityId, sysPath & "\Temp\Block.JPEG", 100, 50, Nothing) Then
                Dim bmpObj As Bitmap = Nothing
                Dim bytBitmaps = IO.File.ReadAllBytes(sysPath & "\Temp\Block.JPEG")
                Using msBitmap As New IO.MemoryStream(bytBitmaps)
                    bytBitmap = bytBitmaps
                    bmpObj = New Bitmap(msBitmap)
                End Using
                'IO.File.Delete(sysPath & "\Temp\Block.JPEG")
                Return bmpObj
            End If
            Return Nothing
        End Function

    End Class

End Namespace
