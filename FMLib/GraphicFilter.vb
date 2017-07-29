Imports MxDrawXLib
Imports CxAzFunc
Public Class GraphicFilter


    ''' <summary>
    ''' 通过图层过滤数据
    ''' </summary>
    ''' <param name="laryerName">图层名称</param>
    ''' <returns></returns>
    Public Shared Function GetEntityOfLayer(laryerName As String, Optional entityType As String = Nothing) As List(Of MxDrawEntity)
        Dim pResbuf As New MxDrawResbuf
        If laryerName IsNot Nothing Then pResbuf.AddStringEx(laryerName, Layer.DxfCode.LayerName) 'Layer.EntityDxfTypeName.CIRCLE.ToString
        If entityType IsNot Nothing Then pResbuf.AddStringEx(entityType, Layer.DxfCode.CommonEntity)
        Dim pSelectionSet As New MxDrawSelectionSet '获取所有的数据
        pSelectionSet.Select(MCAD_McSelect.mcSelectionSetAll, Nothing, Nothing, pResbuf)
        Dim reCol As New List(Of MxDrawEntity)
        For i = 0 To pSelectionSet.Count - 1
            reCol.Add(pSelectionSet.Item(i))
        Next
        Return reCol
    End Function


    ''' <summary>
    ''' 通过类型获取实体
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function GetEntityOfType(type As Layer.EntityDxfTypeName) As List(Of MxDrawEntity)
        Dim pResbuf As New MxDrawResbuf
        pResbuf.AddStringEx(type.ToString, Layer.DxfCode.CommonEntity)
        Dim pSelectionSet As New MxDrawSelectionSet '获取所有的数据
        pSelectionSet.Select(MCAD_McSelect.mcSelectionSetAll, Nothing, Nothing, pResbuf)
        Dim reList As New List(Of MxDrawEntity)
        For i = 0 To pSelectionSet.Count - 1
            Dim pEntity As MxDrawEntity = pSelectionSet.Item(i)
            reList.Add(pEntity)
        Next
        Return reList
    End Function
    Public Shared Function GetEntityOfBox(ByVal pt1 As MxDrawPoint, ByVal pt2 As MxDrawPoint, Optional ByVal type As String = Nothing) As List(Of MxDrawEntity)
        Dim pResbuf As MxDrawResbuf = Nothing
        If type IsNot Nothing Then
            pResbuf = New MxDrawResbuf
            pResbuf.AddStringEx(type, Layer.DxfCode.CommonEntity)
        End If
        Dim pSelectionSet As New MxDrawSelectionSet   '获取所有的数据
        pSelectionSet.Select(MCAD_McSelect.mcSelectionSetCrossing, pt1, pt2, pResbuf)
        Dim pList As New List(Of MxDrawEntity)
        For i = 0 To pSelectionSet.Count - 1
            pList.Add(pSelectionSet.Item(i))
        Next
        Return pList
    End Function


    'Public Shared Function GetEntityOfBox(pt1 As MxDrawPoint, pt2 As MxDrawPoint, Optional ByVal type As Layer.EntityDxfTypeName = Layer.EntityDxfTypeName.NONE) As List(Of MxDrawEntity)
    '    Dim pResbuf As MxDrawResbuf = Nothing
    '    If type <> Layer.EntityDxfTypeName.NONE Then
    '        pResbuf = New MxDrawResbuf
    '        pResbuf.AddStringEx(type.ToString, Layer.DxfCode.CommonEntity)
    '    End If
    '    Dim pSelectionSet As MxDrawSelectionSet = Nothing  '获取所有的数据
    '    pSelectionSet.Select(MCAD_McSelect.mcSelectionSetCrossing, pt1, pt2, pResbuf)

    '    Dim pList As New List(Of MxDrawEntity)
    '    For i = 0 To pSelectionSet.Count - 1
    '        pList.Add(pSelectionSet.Item(i))
    '    Next
    '    Return pList
    'End Function

    ''' <summary>
    ''' 通过点过滤
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetEntityOfPoint(ByVal pt As MxDrawPoint, Optional ByVal type As Layer.EntityDxfTypeName = Layer.EntityDxfTypeName.NONE) As List(Of MxDrawEntity)
        Dim pResbuf As MxDrawResbuf = Nothing
        If type <> Layer.EntityDxfTypeName.NONE Then
            pResbuf = New MxDrawResbuf
            pResbuf.AddStringEx(type.ToString, Layer.DxfCode.CommonEntity)
        End If

        Dim pSelectionSet As New MxDrawSelectionSet '获取所有的数据
        pSelectionSet.SelectAtPoint(pt, pResbuf)
        Dim pList As New List(Of MxDrawEntity)
        For i = 0 To pSelectionSet.Count - 1
            pList.Add(pSelectionSet.Item(i))
        Next
        Return pList
    End Function

End Class
