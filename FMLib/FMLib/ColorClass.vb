Imports MxDrawXLib
Imports CxAzFunc
Public Class ColorClass

    Public Shared Function GetGreen() As MxDrawMcCmColor
        ' pLayerTableRecord.Color.colorIndex = pEntity.colorIndex
        '  pEntity.TrueColor.GetBlue()
        ' pLayerTableRecord.Color.ColorMethod = MCAD_McColorMethod.mcColorMethodByRGB
        ' 设置图层颜色
        '  pLayerTableRecord.Color.SetRGB(255, 0, 255)
        Dim newColor As New MxDrawMcCmColor
        newColor.SetRGB(0, 255, 0)
        'pLayerTableRecord.Color = pEntity.TrueColor
        Return newColor
    End Function

    Public Shared Function GetRed() As MxDrawMcCmColor
        ' pLayerTableRecord.Color.colorIndex = pEntity.colorIndex
        '  pEntity.TrueColor.GetBlue()
        ' pLayerTableRecord.Color.ColorMethod = MCAD_McColorMethod.mcColorMethodByRGB
        ' 设置图层颜色
        '  pLayerTableRecord.Color.SetRGB(255, 0, 255)
        Dim newColor As New MxDrawMcCmColor
        newColor.SetRGB(255, 0, 0)
        'pLayerTableRecord.Color = pEntity.TrueColor
        Return newColor
    End Function



End Class
