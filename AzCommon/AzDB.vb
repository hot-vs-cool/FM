
''' <summary>
''' 安装数据操作类
''' </summary>
''' <remarks></remarks>
Public Class AzDB

    ''' <summary>
    ''' 从数据库获取指定表中指定字段的最大值
    ''' </summary>
    ''' <param name="tblName">表名</param>
    ''' <param name="strField">字段名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTableMaxValue(ByVal tblName As String, ByVal strField As String) As String
        Dim ds As DataSet = Nothing
        Dim sqlCmd As String = "Select Max(" & strField & ") As maxValue From " & tblName
        If g_db.QueryInterfacePara(sqlCmd, ds, """从数据库获取" & strField & """字段最大值失败。") Then
            If ds.Tables(0).Rows.Count = 0 Then
                Return Nothing
            Else
                Return ds.Tables(0).Rows(0)("maxValue").ToString
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' 从数据库获取指定表的指定整型字段最大值，如果表为空则返回0
    ''' </summary>
    ''' <param name="tblName">表名</param>
    ''' <param name="idField">字段名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTableMaxInteger(ByVal tblName As String, ByVal idField As String) As Integer
        Dim ds As DataSet = Nothing
        Dim sqlCmd As String = "Select Max(" & idField & ") As maxValue From " & tblName
        If g_db.QueryInterfacePara(sqlCmd, ds, """从数据库获取" & idField & """字段最大值失败。") Then
            If ds.Tables(0).Rows.Count = 0 Then
                Return Nothing
            Else
                If IsNumeric(ds.Tables(0).Rows(0)("maxValue")) Then
                    Return CInt(ds.Tables(0).Rows(0)("maxValue"))
                End If
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' 从数据库获取指定表的指定整型字段的最小值，如果表为空则返回0
    ''' </summary>
    ''' <param name="tblName">表名</param>
    ''' <param name="idField">字段名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTableMinInteger(ByVal tblName As String, ByVal idField As String) As Integer
        Dim ds As DataSet = Nothing
        Dim sqlCmd As String = "Select Min(" & idField & ") As minValue From " & tblName
        If g_db.QueryInterfacePara(sqlCmd, ds, """从数据库获取" & idField & """字段最大值失败。") Then
            If ds.Tables(0).Rows.Count = 0 Then
                Return Nothing
            Else
                Return CInt(ds.Tables(0).Rows(0)("minValue"))
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' 从数据库查询指定表指定字段是否已经存在指定值
    ''' </summary>
    ''' <param name="tblName">待查询数据库表名</param>
    ''' <param name="strField">待带查询字段名</param>
    ''' <param name="fieldValue">待查询字段值</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsExistValue(ByVal tblName As String, ByVal strField As String, ByVal fieldValue As String) As Boolean
        Dim ds As DataSet = Nothing
        Dim sqlCmd = "Select " & strField & " From " & tblName & " Where " & strField & "=" & fieldValue
        g_db.SqlParameters.Clear() : g_db.SqlParameters.Add(fieldValue)
        If g_db.QueryInterfacePara(sqlCmd, ds, "") Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 从数据库查询指定表指定字段是否已经存在指定值
    ''' </summary>
    ''' <param name="tblName">待查询数据库表名</param>
    ''' <param name="dicFields">查询条件键值对集合</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsExistValue(ByVal tblName As String, ByVal dicFields As Dictionary(Of String, String)) As Boolean
        Dim ds As DataSet = Nothing
        Dim sqlCmd = "Select * From " & tblName & " Where "
        Dim strFields As String = Nothing
        g_db.SqlParameters.Clear()
        For Each field In dicFields
            strFields &= " And " & field.Key & "=?"
            g_db.SqlParameters.Add(field.Value)
        Next
        sqlCmd &= Mid(strFields, 6)
        If g_db.QueryInterfacePara(sqlCmd, ds, "数据库查询失败。") Then
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 从数据库获取指定名称,指定字段的表;如果成功返回一个DataTable对象,否则返回Nothing
    ''' </summary>
    ''' <param name="tableName">待获取表的名称</param>
    ''' <param name="fieldNams">待获取的字段名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTable(ByVal tableName As String, ByVal ParamArray fieldNams() As String) As DataTable
        Dim ds As DataSet = Nothing
        Dim sqlCmd As String = Nothing
        If fieldNams.Length = 0 Then
            sqlCmd = "Select * From " & tableName
        Else
            For Each fieldName In fieldNams
                sqlCmd &= "," & fieldName
            Next
            sqlCmd = "Select " & Mid(sqlCmd, 2) & " From " & tableName
        End If
        If g_db.QueryInterfacePara(sqlCmd, ds, "加载表""" & tableName & """数据失败。") Then
            Dim tblReturn = ds.Tables(0)
            ds.Dispose()
            Return tblReturn
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' 对工程数据库执行指定的SQL查询语句;成功则返回查询到的DataTable对象,否则返回Nothing
    ''' </summary>
    ''' <param name="sqlCmd">待直线的SQL语句</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTableBySql(ByVal sqlCmd As String) As DataTable
        Dim ds As DataSet = Nothing
        If g_db.QueryInterfacePara(sqlCmd, ds, "执行数据库查询失败。") Then
            Dim tblReturn = ds.Tables(0)
            ds.Dispose()
            Return tblReturn
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' 读取Excel文件到DataTable
    ''' </summary>
    ''' <param name="excelPath">待读取的Excel文件路径</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ReadExcellToDt(ByVal excelPath As String) As DataTable
        Dim strCon As String = Nothing
        Dim extendName = System.IO.Path.GetExtension(excelPath)
        If extendName = ".xlsx" Then
            strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & excelPath & ";Extended Properties='Excel 12.0;HDR=YES'"
        ElseIf extendName = ".xls" Then
            strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & excelPath & ";Extended Properties=Excel 8.0"
        End If
        Dim conOledb As New OleDb.OleDbConnection(strCon)
        conOledb.Open()
        Dim sheetName As String = ""
        Dim dtXls = conOledb.GetSchema("Tables")
        Dim dtReader = New DataTableReader(dtXls)
        While dtReader.Read
            sheetName = dtReader("Table_Name").ToString
            If MsgBox("当前表为""" & sheetName.Trim(CChar("$")) & """是否打开?", MsgBoxStyle.YesNo, "提示") = MsgBoxResult.Yes Then
                Exit While
            End If
        End While
        Dim adOledb As New OleDb.OleDbDataAdapter("Select * From [" & sheetName & "]", conOledb)
        Dim ds As New DataSet
        adOledb.Fill(ds, sheetName)
        conOledb.Close()
        Return ds.Tables(sheetName)
    End Function

End Class
