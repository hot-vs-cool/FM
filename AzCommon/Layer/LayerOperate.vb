Imports MxDrawXLib

Namespace Layer

    ''' <summary>
    ''' 图层操作，包括图层的初始化，过滤等
    ''' </summary>
    ''' <remarks></remarks>
    Public Class LayerOperate

        ''' <summary>
        ''' 初始化主CAD图层，返回层表记录对象
        ''' </summary>
        ''' <param name="componentType">待初始化的图层所属构件类型</param>
        ''' <param name="kindProjId">待初始化图层所属分项工程ID</param>
        ''' <param name="isSetCurrent">可选，是否把初始化的图层设置为当前活动图层，默认不设置</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function InitLayer(ByVal componentType As Component.AzComponentType, ByVal kindProjId As Integer, Optional ByVal isSetCurrent As Boolean = False) As MxDrawLayerTableRecord
            Dim cptName = componentType.ToString
            Dim layerRec As MxDrawLayerTableRecord = Nothing
            Dim layerName As String = "CZ" & kindProjId.ToString("000") & "_" & cptName
            Dim layerTable = CType(MxMainCadObj.GetDatabase, MxDrawDatabase).GetLayerTable
            layerRec = layerTable.GetAt(layerName, False)
            If layerRec IsNot Nothing Then Return layerRec
            Dim layerId = MxMainCadObj.AddLayer(layerName)
            layerRec = CType(MxMainCadObj.ObjectIdToObject(layerId), MxDrawLayerTableRecord)
            Dim nodeLayerInfo = CType(xmlSysDataConfig.SelectSingleNode("/AzDataConfig/LayerHierarchy/" & cptName), Xml.XmlElement)
            Dim colorObj As New MxDrawMcCmColor
            colorObj.SetRGB(CInt(nodeLayerInfo.GetAttribute("R")), CInt(nodeLayerInfo.GetAttribute("G")), CInt(nodeLayerInfo.GetAttribute("B")))
            layerRec.Color = colorObj
            Select Case nodeLayerInfo.GetAttribute("Lineweight")
                Case "0.00"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt000
                Case "0.05"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt005
                Case "0.09"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt009
                Case "0.13"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt013
                Case "0.15"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt015
                Case "0.18"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt018
                Case "0.20"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt020
                Case "0.25"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt025
                Case "0.30"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt030
                Case "0.35"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt035
                Case "0.40"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt040
                Case "0.50"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt050
                Case "0.53"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt053
                Case "0.60"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt060
                Case "0.70"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt070
                Case "0.80"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt080
                Case "0.90"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt090
                Case "1.00"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt100
                Case "1.06"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt106
                Case "1.20"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt120
                Case "1.40"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt140
                Case "1.58"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt158
                Case "2.00"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt200
                Case "2.11"
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWt211
                Case Else
                    layerRec.Lineweight = MCAD_LWEIGHT.mcLnWtByLwDefault
            End Select
            layerTable.Close()
            Return layerRec
        End Function

        ''' <summary>
        ''' 设置所有CAD图层的可见性
        ''' </summary>
        ''' <param name="isVisible">图层可见性。“True”表示设置图层为可见，“False”表示设置图层不可见</param>
        ''' <remarks></remarks>
        Public Shared Sub SetAllLayerVisible(ByVal cadObj As AxMxDrawXLib.AxMxDrawX, ByVal isVisible As Boolean)
            Dim cadDb = CType(cadObj.GetDatabase, MxDrawDatabase)
            Dim layerTable As MxDrawLayerTable = cadDb.GetLayerTable
            Dim iterLayer = layerTable.NewIterator
            If isVisible Then
                Do Until iterLayer.Done
                    iterLayer.GetRecord.IsOff = False
                    iterLayer.Step()
                Loop
            Else
                Do Until iterLayer.Done
                    iterLayer.GetRecord.IsOff = True
                    iterLayer.Step()
                Loop
            End If
            Runtime.InteropServices.Marshal.ReleaseComObject(iterLayer)
        End Sub

        ''' <summary>
        ''' 设置图层的可见性
        ''' </summary>
        ''' <param name="layerName">图层名称</param>
        ''' <param name="isVisiable">图层可见性。“True”表示设置图层为可见，“False”表示设置图层不可见</param>
        ''' <remarks></remarks>
        Public Shared Sub SetLayerVisible(ByVal cadObj As AxMxDrawXLib.AxMxDrawX, ByVal layerName As String, ByVal isVisiable As Boolean)
            Dim cadDb = CType(cadObj.GetDatabase, MxDrawDatabase)
            Dim layerTable As MxDrawLayerTable = cadDb.GetLayerTable
            Dim layerRec As MxDrawLayerTableRecord = layerTable.GetAt(layerName)
            If layerRec IsNot Nothing Then
                layerRec.IsOff = Not isVisiable
            End If
        End Sub

        ''' <summary>
        ''' 获取所有底图图层记录对象,如果不存任何对象则返回Nothing
        ''' </summary>
        ''' <param name="kindProjId">可选，底图所属的分项工程ID，默认获取所有分项工程的底图图层记录对象</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetOriginLayer(Optional ByVal kindProjId As Byte = 0) As List(Of MxDrawLayerTableRecord)
            Dim lisLayerRec As New List(Of MxDrawLayerTableRecord)
            Dim cadDB = CType(MxMainCadObj.GetDatabase, MxDrawDatabase)
            Dim layerTable = cadDB.GetLayerTable
            Dim layerRec As MxDrawLayerTableRecord = Nothing
            Dim iteLayer = layerTable.NewIterator
            Dim strTag As String = "CX###_*"
            If kindProjId > 0 Then strTag = "CX" & kindProjId.ToString("000") & "_*"
            Do Until iteLayer.Done
                layerRec = iteLayer.GetRecord
                If layerRec.Name Like strTag Then
                    lisLayerRec.Add(layerRec)
                End If
                iteLayer.Step()
            Loop
            Runtime.InteropServices.Marshal.ReleaseComObject(iteLayer)
            If lisLayerRec.Count = 0 Then
                Return Nothing
            Else
                Return lisLayerRec
            End If
        End Function

        ''' <summary>
        ''' 获取安装构件层表记录，如果不存在层表记录则返回Nothing
        ''' </summary>
        ''' <param name="kindProjId">可选，待获取的图层所属的分项工程ID值，默认获取所有分项工程的层表记录</param>
        ''' <param name="componentType">可选，待获取的层表记录所属的构件类型，默认获取所有类型的层表记录</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetCptLayer(Optional ByVal kindProjId As Byte = 0, _
    Optional ByVal componentType As Component.AzComponentType = Nothing) As List(Of MxDrawLayerTableRecord)
            Dim layerTable = CType(MxMainCadObj.GetDatabase, MxDrawDatabase).GetLayerTable
            Dim iteLayer = layerTable.NewIterator
            Dim strTag = "CZ###_"
            If kindProjId <> 0 Then strTag = "CZ" & kindProjId.ToString("000") & "_"
            If componentType = Nothing Then
                strTag &= "*"
            Else
                strTag &= componentType.ToString
            End If
            Dim layerRec As MxDrawLayerTableRecord = Nothing
            Dim lisLayerRec As New List(Of MxDrawLayerTableRecord)
            Do Until iteLayer.Done
                layerRec = iteLayer.GetRecord
                If layerRec.Name Like strTag Then
                    lisLayerRec.Add(layerRec)
                End If
                iteLayer.Step()
            Loop
            Runtime.InteropServices.Marshal.ReleaseComObject(iteLayer)
            If lisLayerRec.Count = 0 Then
                Return Nothing
            Else
                Return lisLayerRec
            End If
        End Function

    End Class

End Namespace
