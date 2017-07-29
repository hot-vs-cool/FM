Namespace Component

    ''' <summary>
    ''' 构件编辑类，包括构件属性查询、修改
    ''' </summary>
    Public Class EditComponent

        ''' <summary>
        ''' 获取构件默认属性集合
        ''' </summary>
        ''' <param name="componentType">待获取默认属性的构件类型</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetCptDefaultProperty(ByVal componentType As AzComponentType) As Dictionary(Of String, String)
            Dim xmlNode = xmlSysDataConfig.SelectSingleNode("/AzDataConfig/CptProperty/" & componentType.ToString)
            If xmlNode Is Nothing Then
                Return Nothing
            Else
                Dim dicProperty As New Dictionary(Of String, String)
                For Each nodeProperty As Xml.XmlElement In xmlNode.ChildNodes
                    dicProperty.Add(nodeProperty.GetAttribute("Name"), nodeProperty.GetAttribute("Value"))
                Next
                Return dicProperty
            End If
        End Function

    End Class
End Namespace
