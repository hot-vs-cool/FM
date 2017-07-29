
Namespace Graphic

    ''' <summary>
    ''' 图形筛选类
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GraphicFilter

        ''' <summary>
        ''' 筛选当前CAD用户选中的所有实体,如果未选中任何实体者返回Nothing
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetSelectEntity() As List(Of MxDrawEntity)
            Dim selObj As New MxDrawSelectionSet
            selObj.Select(MCAD_McSelect.mcSelectionImpliedSelectSelect, Nothing, Nothing)
            If selObj.Count = 0 Then
                Return Nothing
            Else
                Dim lisEntity As New List(Of MxDrawEntity)
                For i = 0 To selObj.Count - 1
                    lisEntity.Add(CType(selObj.Item(i), MxDrawEntity))
                Next
                Return lisEntity
            End If
        End Function

        ''' <summary>
        ''' 获取模型空间上所有的实体对象，如果模型空间不存在任何实体，则返回Nothing。
        ''' </summary>
        ''' <param name="layerName">可选，实体所在图层名，默认获取所有图层上的实体</param>
        ''' <param name="entityType">可选，实体类型，默认获取所有实体类型</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAllEntity(Optional ByVal layerName As String = "", Optional ByVal entityType As Layer.EntityDxfTypeName = Nothing) As List(Of MxDrawEntity)
            Dim rbfFilter As MxDrawResbuf = Nothing
            If layerName.Trim <> "" Then
                rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(layerName.Trim, 8)
            End If
            If entityType <> Nothing Then
                If rbfFilter Is Nothing Then rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(entityType.ToString, 5020)
            End If
            Dim selObj As New MxDrawSelectionSet
            selObj.Select(MCAD_McSelect.mcSelectionSetAll, Nothing, Nothing, rbfFilter)
            If rbfFilter IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rbfFilter)
            If selObj.Count = 0 Then
                Return Nothing
            Else
                Dim lisEntity As New List(Of MxDrawEntity)
                For i = selObj.Count - 1 To 0 Step -1
                    lisEntity.Add(CType(selObj.Item(i), MxDrawEntity))
                Next
                Return lisEntity
            End If
        End Function

        ''' <summary>
        ''' 筛选指定点处的实体对象，如果指定点处不存在任何实体，则返回Nothing。该方法一般用于精确筛选。
        ''' </summary>
        ''' <param name="filterPoint">筛选基点</param>
        ''' <param name="offsetValue">可选，筛选区域扩充值，默认0即不进行区域扩充，一般用于精确筛选</param>
        ''' <param name="layerName">可选，待筛选的图层名称，默认筛选所有图层</param>
        ''' <param name="entityType">可选，待筛选的实体类型，默认筛选所有的类型的实体</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEntityByPoint(ByVal filterPoint As MxDrawPoint, Optional ByVal offsetValue As UShort = 0, Optional ByVal layerName As String = "", _
    Optional ByVal entityType As Layer.EntityDxfTypeName = Nothing) As List(Of MxDrawEntity)
            Dim selObj As New MxDrawSelectionSet
            Dim rbfFilter As MxDrawResbuf = Nothing
            If layerName <> "" Then
                rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(layerName, 8)
            End If
            If entityType <> Nothing Then
                If rbfFilter Is Nothing Then rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(entityType.ToString, 5020)
            End If
            If offsetValue = 0 Then
                selObj.SelectAtPoint(filterPoint, rbfFilter)
            Else
                Dim minPoint, maxPoint As New MxDrawPoint
                minPoint.x = filterPoint.x - offsetValue
                minPoint.y = filterPoint.y - offsetValue
                maxPoint.x = filterPoint.x + offsetValue
                maxPoint.y = filterPoint.y + offsetValue
                selObj.Select(MCAD_McSelect.mcSelectionSetCrossing, minPoint, maxPoint, rbfFilter)
            End If
            If rbfFilter IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rbfFilter)
            If selObj.Count = 0 Then
                Return Nothing
            Else
                Dim lisEntity As New List(Of MxDrawEntity)
                For i = 0 To selObj.Count - 1
                    lisEntity.Add(CType(selObj.Item(i), MxDrawEntity))
                Next
                Return lisEntity
            End If
            If selObj IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(selObj)
        End Function

        ''' <summary>
        ''' 筛选指定点处的所有底图对象，如果指定点处不存在任何实体，则返回Nothing。
        ''' </summary>
        ''' <param name="filterPoint">待筛选点坐标</param>
        ''' <param name="entityType">可选，筛选的实体类型，默认筛选所有实体</param>
        ''' <param name="offsetValue">可选，筛选区域扩充值，默认0即不进行区域扩充，一般用于精确筛选</param>
        ''' <param name="lisCptBlockName">可选，返回单击处块构件名称集合</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetOriginEntityByPoint(ByVal filterPoint As MxDrawPoint, Optional ByVal entityType As Layer.EntityDxfTypeName = Nothing, _
    Optional ByVal offsetValue As UShort = 0, Optional ByVal lisCptBlockName As List(Of MxDrawEntity) = Nothing) As List(Of MxDrawEntity)
            Dim rbfFilter As MxDrawResbuf = Nothing
            If entityType <> Nothing Then
                rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(entityType.ToString, Layer.DxfCode.CommonEntity)
            End If
            Dim selObj As New MxDrawSelectionSet
            If offsetValue = 0 Then
                selObj.SelectAtPoint(filterPoint, rbfFilter)
            Else
                Dim minPoint, maxPoint As New MxDrawPoint
                minPoint.x = filterPoint.x - offsetValue
                minPoint.y = filterPoint.y - offsetValue
                maxPoint.x = filterPoint.x + offsetValue
                maxPoint.y = filterPoint.y + offsetValue
                selObj.Select(MCAD_McSelect.mcSelectionSetCrossing, minPoint, maxPoint, rbfFilter)
            End If
            If rbfFilter IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rbfFilter)
            If selObj.Count = 0 Then
                Return Nothing
            Else
                Dim strLayerTag As String = "CX###_*"
                Dim lisEntity As New List(Of MxDrawEntity)
                If lisCptBlockName IsNot Nothing Then
                    For i = 0 To selObj.Count - 1
                        If selObj.Item(i).Layer Like strLayerTag Then
                            lisEntity.Add(selObj.Item(i))
                        ElseIf selObj.Item(i).Layer <> "0" Then
                            lisCptBlockName.Add(selObj.Item(i))
                        End If
                    Next
                Else
                    For i = 0 To selObj.Count - 1
                        If selObj.Item(i).Layer Like strLayerTag Then
                            lisEntity.Add(selObj.Item(i))
                        End If
                    Next
                End If
                If lisEntity.Count = 0 Then
                    Return Nothing
                Else
                    Return lisEntity
                End If
            End If
        End Function

        ''' <summary>
        ''' 筛选以指定点为中心的正方形区域内的实体，如果区域内不存在任何实体，则返回Nothing
        ''' </summary>
        ''' <param name="centerPoint">筛选正方形区域中心点</param>
        ''' <param name="offsetValue">筛选边界距中心点的偏移量</param>
        ''' <param name="layerName">可选，待筛选的图层名称，默认筛选所有图层</param>
        ''' <param name="entityType">可选，待筛选的实体类型，默认筛选所有的类型的实体</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEntityByRect(ByVal centerPoint As MxDrawPoint, ByVal offsetValue As Integer, _
   Optional ByVal layerName As String = "", Optional ByVal entityType As Layer.EntityDxfTypeName = Nothing) As List(Of MxDrawEntity)
            Dim leftBottomPt, rightUpPt As New MxDrawPoint
            rightUpPt.x = centerPoint.x + offsetValue
            rightUpPt.y = centerPoint.y + offsetValue
            leftBottomPt.x = centerPoint.x - offsetValue
            leftBottomPt.y = centerPoint.y - offsetValue
            Dim selObj As New MxDrawSelectionSet
            Dim rbfFilter As MxDrawResbuf = Nothing
            If layerName <> "" Then
                rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(layerName, 8)
            End If
            If entityType <> Nothing Then
                If rbfFilter Is Nothing Then rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(entityType.ToString, 5020)
            End If
            selObj.Select(MCAD_McSelect.mcSelectionSetCrossing, leftBottomPt, rightUpPt, rbfFilter)
            If rbfFilter IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rbfFilter)
            If selObj.Count = 0 Then
                Return Nothing
            Else
                Dim lisEntity As New List(Of MxDrawEntity)
                For i = 0 To selObj.Count - 1
                    lisEntity.Add(CType(selObj.Item(i), MxDrawEntity))
                Next
                Return lisEntity
            End If
        End Function

        ''' <summary>
        ''' 筛选指定矩形区域内的实体，如果区域内不存在任何实体，则返回Nothing
        ''' </summary>
        ''' <param name="minPoint">筛选区域左下角点</param>
        ''' <param name="maxPoint">筛选区域右上角点</param>
        ''' <param name="layerName">可选，待筛选的图层名称，默认筛选所有图层</param>
        ''' <param name="entityType">可选，待筛选的实体类型，默认筛选所有的类型的实体</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEntityByRect(ByVal minPoint As MxDrawPoint, ByVal maxPoint As MxDrawPoint, _
   Optional ByVal layerName As String = "", Optional ByVal entityType As Layer.EntityDxfTypeName = Nothing) As List(Of MxDrawEntity)
            Dim selObj As New MxDrawSelectionSet
            Dim rbfFilter As MxDrawResbuf = Nothing
            If layerName <> "" Then
                rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(layerName, 8)
            End If
            If entityType <> Nothing Then
                If rbfFilter Is Nothing Then rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(entityType.ToString, Layer.DxfCode.CommonEntity)
            End If
            selObj.Select(MCAD_McSelect.mcSelectionSetCrossing, minPoint, maxPoint, rbfFilter)
            If rbfFilter IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rbfFilter)
            If selObj.Count = 0 Then
                Return Nothing
            Else
                Dim lisEntity As New List(Of MxDrawEntity)
                For i = 0 To selObj.Count - 1
                    lisEntity.Add(CType(selObj.Item(i), MxDrawEntity))
                Next
                Return lisEntity
            End If
        End Function
        ''' <summary>
        ''' 筛选指定闭合区间区域内的实体，如果区域内不存在任何实体，则返回Nothing
        ''' </summary>
        ''' <param name="ps">闭合区间</param>
        ''' <param name="layerName">可选，待筛选的图层名称，默认筛选所有图层</param>
        ''' <param name="entityType">可选，待筛选的实体类型，默认筛选所有的类型的实体</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetEntityByPs(ByVal ps As MxDrawPoints, Optional ByVal layerName As String = "",
                                               Optional ByVal entityType As Layer.EntityDxfTypeName = Nothing) As List(Of MxDrawEntity)
            Dim selObj As New MxDrawSelectionSet
            Dim rbfFilter As MxDrawResbuf = Nothing
            If layerName <> "" Then
                rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(layerName, 8)
            End If
            If entityType <> Nothing Then
                If rbfFilter Is Nothing Then rbfFilter = New MxDrawResbuf
                rbfFilter.AddStringEx(entityType.ToString, Layer.DxfCode.CommonEntity)
            End If
            selObj.SelectByPolygon(MCAD_McSelect.mcSelectionSetCrossingPolygon, ps, rbfFilter)

            If rbfFilter IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rbfFilter)
            If selObj.Count = 0 Then
                Return Nothing
            Else
                Dim lisEntity As New List(Of MxDrawEntity)
                For i = 0 To selObj.Count - 1
                    lisEntity.Add(CType(selObj.Item(i), MxDrawEntity))
                Next
                Return lisEntity
            End If
        End Function




        ''' <summary>
        ''' 获取指定名称的块对象，如果不存在指定名称的块对象则返回Nothing
        ''' </summary>
        ''' <param name="blockName">待过滤的块名称</param>
        ''' <param name="layerName">可选，待过滤的图层名称，筛选所有图层</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetBlockByName(ByVal blockName As String, Optional ByVal layerName As String = "") As List(Of MxDrawBlockReference)
            If blockName.Trim = "" Then Return Nothing
            Dim rbfFilter As New MxDrawResbuf
            rbfFilter.AddStringEx(blockName.Trim, 2) '其中2代表块名的DXF组码
            If layerName.Trim <> "" Then
                rbfFilter.AddStringEx(layerName.Trim, 8) '其中8为图层的组码
            End If
            Dim selObj As New MxDrawSelectionSet
            selObj.Select(MCAD_McSelect.mcSelectionSetAll, Nothing, Nothing, rbfFilter)
            If selObj.Count = 0 Then
                Return Nothing
            Else
                Dim lisBlock As New List(Of MxDrawBlockReference)
                For i = 0 To selObj.Count - 1
                    lisBlock.Add(CType(selObj.Item(i), MxDrawBlockReference))
                Next
                Return lisBlock
            End If
        End Function

        ''' <summary>
        ''' 获取底图中指定名称的块对象,如果不存在指定名称的块对象，则返回Nothing
        ''' </summary>
        ''' <param name="blockName">待过滤的块名称</param>
        ''' <param name="kindProjId">可选，待筛选底图所属的分项工程ID，默认过滤所有的分项工程</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetOriginBlockByName(ByVal blockName As String, Optional ByVal kindProjId As Byte = 0) As List(Of MxDrawBlockReference)
            blockName = blockName.Trim
            If blockName = "" Then Return Nothing
            Dim rbfFilter As New MxDrawResbuf
            rbfFilter.AddStringEx(blockName, 2)
            Dim selObj As New MxDrawSelectionSet
            selObj.Select(MCAD_McSelect.mcSelectionSetAll, Nothing, Nothing, rbfFilter)
            Runtime.InteropServices.Marshal.ReleaseComObject(rbfFilter)
            If selObj.Count = 0 Then
                Return Nothing
            Else
                Dim strKindTag = "CX###_*"
                Dim blockObj As MxDrawBlockReference = Nothing
                If kindProjId <> 0 Then strKindTag = "CX" & kindProjId.ToString("000") & "_*"
                Dim lisBlock As New List(Of MxDrawBlockReference)
                For i = 0 To selObj.Count - 1
                    blockObj = CType(selObj.Item(i), MxDrawBlockReference)
                    If blockObj.Layer Like strKindTag Then
                        lisBlock.Add(blockObj)
                    End If
                Next
                Return lisBlock
            End If
        End Function

        ''' <summary>
        ''' 获取指定点处指定类型构件实体对象，如果不存在则返回Nothing
        ''' </summary>
        ''' <param name="componentType">待筛选的构件类型</param>
        ''' <param name="filterPoint">待筛选的基点</param>
        ''' <param name="offsetValue">可选，筛选区域扩充值，默认0即不进行区域扩充，一般用于精确筛选</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetComponentEntityByPoint(ByVal componentType As Component.AzComponentType, ByVal filterPoint As MxDrawPoint, Optional ByVal offsetValue As Integer = 0) As List(Of MxDrawBlockReference)
            Dim rbfFilter As New MxDrawResbuf
            rbfFilter.AddStringEx(Layer.EntityDxfTypeName.INSERT.ToString, 5020)
            Dim selObj As New MxDrawSelectionSet
            If offsetValue = 0 Then
                selObj.SelectAtPoint(filterPoint, rbfFilter)
            Else
                Dim minPt, maxPt As New MxDrawPoint
                minPt.x = filterPoint.x - offsetValue
                minPt.y = filterPoint.y - offsetValue
                maxPt.x = filterPoint.x + offsetValue
                maxPt.y = filterPoint.y + offsetValue
                selObj.Select(MCAD_McSelect.mcSelectionSetCrossing, minPt, maxPt, rbfFilter)
            End If
            Runtime.InteropServices.Marshal.ReleaseComObject(rbfFilter)
            If selObj.Count > 0 Then
                Dim strTag = "CZ###_" & componentType.ToString
                Dim lisBlock As New List(Of MxDrawBlockReference)
                For i = 0 To selObj.Count - 1
                    If Not selObj.Item(i).Layer Like strTag Then Continue For
                    lisBlock.Add(CType(selObj.Item(i), MxDrawBlockReference))
                Next
                If lisBlock.Count > 0 Then
                    Return lisBlock
                End If
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' 获取指定矩形区域处指定类型构件实体对象，如果不存在则返回Nothing
        ''' </summary>
        ''' <param name="componentType">待筛选的构件类型</param>
        ''' <param name="minPoint">筛选的矩形区域左下角角点</param>
        ''' <param name="maxPoint">筛选的矩形区域右上角角点</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetComponentEntityByRect(ByVal componentType As Component.AzComponentType, ByVal minPoint As MxDrawPoint, ByVal maxPoint As MxDrawPoint) As List(Of MxDrawEntity)
            Dim rbfFilter As New MxDrawResbuf
            rbfFilter.AddStringEx(Layer.EntityDxfTypeName.INSERT.ToString, 5020)
            Dim selObj As New MxDrawSelectionSet
            selObj.Select(MCAD_McSelect.mcSelectionSetCrossing, minPoint, maxPoint, rbfFilter)
            Runtime.InteropServices.Marshal.ReleaseComObject(rbfFilter)
            If selObj.Count > 0 Then
                Dim strTag = "CZ###_" & componentType.ToString
                Dim lisBlock As New List(Of MxDrawEntity)
                For i = 0 To selObj.Count - 1
                    If Not selObj.Item(i).Layer Like strTag Then Continue For
                    lisBlock.Add(selObj.Item(i))
                Next
                If lisBlock.Count > 0 Then
                    Return lisBlock
                End If
            End If
            Return Nothing
        End Function

    End Class

End Namespace
