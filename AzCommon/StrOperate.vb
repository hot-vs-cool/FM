Imports System.Text.RegularExpressions

''' <summary>
''' 字符串操作类，包括字符串分割、字符串数学计算、条件运算等
''' </summary>
''' <remarks></remarks>
Public Class StrOperate
    Private Shared calcDt As New DataTable '用于字符型计算式计算
    Private Shared calcCount As New FormalCount

    ''' <summary>
    ''' 分割由连续字母和连续数字构成的字符串，如：“ABC123”或“123ABC”类型的字符串，返回构成字符串的字母和数字子字符串。
    ''' </summary>
    ''' <param name="strSource">源字符串</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SplitLetterNum(ByVal strSource As String) As KeyValuePair(Of String, Integer)
        strSource = strSource.Trim
        If Regex.IsMatch(strSource, "^[A-Za-z]+[^A-Za-z\d]{0,1}\d+$") Then
            Dim matches = Regex.Match(strSource, "^(?<A>[A-Za-z]+)[^A-Za-z\d]{0,1}(?<B>\d+)$")
            Return New KeyValuePair(Of String, Integer)(matches.Groups("A").Value, CInt(matches.Groups("B").Value))
        ElseIf Regex.IsMatch(strSource, "^\d+[^A-Za-z\d]{0,1}[A-Za-z]+$") Then
            Dim matches = Regex.Match(strSource, "^(?<A>\d+)[^A-Za-z\d]{0,1}(?<B>[A-Za-z]+)$")
            Return New KeyValuePair(Of String, Integer)(matches.Groups("B").Value, CInt(matches.Groups("A").Value))
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' 获取初始表达式包含IIF判别时的次级表达式,返回次级表达式
    ''' </summary>
    ''' <param name="expression">包含IIF判别标记的初始表达式</param>
    ''' <param name="keyParam">计算IIF判别式所需的计算参数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIfExpression(ByVal expression As String, ByVal keyParam As KeyValuePair(Of String, String)) As String
        Dim calcFormulas = Mid(expression, 5, expression.Length - 5).Split(CChar(","))
        Dim calcExpression As String = Nothing
        If Regex.IsMatch(calcFormulas(0), "'.+'$") Then
            calcExpression = calcFormulas(0).Replace(keyParam.Key, "'" & keyParam.Value & "'")
        Else
            calcExpression = calcFormulas(0).Replace(keyParam.Key, keyParam.Value)
        End If
        If CBool(calcDt.Compute(calcExpression, "")) Then
            Return calcFormulas(1)
        Else
            Return calcFormulas(2)
        End If
    End Function

    ''' <summary>
    ''' 计算字符串值,运算符支持"+"、"-"、"*"、"/"、"("、")",返回表达式计算值
    ''' </summary>
    ''' <param name="calcFormula">待计算的表达式</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CalcSimpleStrValue1(ByVal calcFormula As String) As Double
        Return CDbl(calcDt.Compute(calcFormula, ""))
    End Function

    ''' <summary>
    ''' 计算字符串值,运算符支持"+"、"-"、"*"、"/"、"^"、"("、")",返回表达式计算值
    ''' </summary>
    ''' <param name="calcFormula">待计算的表达式</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CalcSimpleStrValue2(ByVal calcFormula As String) As Double
        Dim calcValue As Double = Nothing
        If calcCount.CountStr(calcFormula, calcValue, Nothing) Then
            Return calcValue
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' 计算字符串值,计算式可包含“【】”类型的备注,运算符支持"+"、"-"、"*"、"/"、"("、")";返回值"True"表示计算成功,"False"表示计算失败
    ''' </summary>
    ''' <param name="calcFormula">初始计算表达式</param>
    ''' <param name="calcValue">可选,计算的返回值;当计算成功返回表达式世纪计算值,否则返回0</param>
    ''' <param name="dicCalcParams">可选,计算参数键值对集合</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CalcSimpleStrValue3(ByVal calcFormula As String, Optional ByRef calcValue As Double = 0, _
Optional ByVal dicCalcParams As Dictionary(Of String, String) = Nothing) As Boolean
        calcFormula = Regex.Replace(calcFormula, "【[^【】]*】", "")
        Try
            If dicCalcParams IsNot Nothing Then
                For Each calcParam In dicCalcParams
                    calcFormula = calcFormula.Replace(calcParam.Key, calcParam.Value)
                Next
            End If
            calcValue = CDbl(calcDt.Compute(calcFormula, ""))
            Return True
        Catch ex As Exception
            calcValue = 0
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 计算字符串值,计算式可包含“【】”类型的备注,但不支持"[]"、"{}"运算符和对“IIF”类型的表达式的解析,返回值"True"表示计算成功,"False"表示计算失败
    ''' </summary>
    ''' <param name="calcFormula">待计算的字符串</param>
    ''' <param name="calcValue">可选，返回的计算值</param>
    ''' <param name="errMsg">可选，计算失败时返回的错误信息</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CalcStrValue(ByVal calcFormula As String, Optional ByRef calcValue As Double = 0, Optional ByRef errMsg As String = "") As Boolean
        calcFormula = Regex.Replace(calcFormula, "【[^【】]*】", "")
        Return calcCount.CountStr(calcFormula, calcValue, errMsg)
    End Function

    ''' <summary>
    ''' 计算字符串值,返回计算结果值,该函数提供对IIF、^的解析
    ''' </summary>
    ''' <param name="originCalcExpression">初始计算表达式</param>
    ''' <param name="dicCalcParams">计算参数键值对集合</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CalcStrValue(ByVal originCalcExpression As String, ByVal dicCalcParams As Dictionary(Of String, String), ByRef calcValue As Double) As Boolean
        Dim calcExpression As String = Nothing
        If Mid(originCalcExpression, 1, 3) = "IIF" Then
            For Each calcParam In dicCalcParams
                calcExpression = GetIfExpression(originCalcExpression, calcParam)
                Exit For
            Next
        Else
            calcExpression = originCalcExpression
        End If
        For Each param In dicCalcParams
            calcExpression = calcExpression.Replace(param.Key, param.Value)
        Next
        Try
            If calcExpression.Contains(CChar("^")) Then
                Return calcCount.CountStr(calcExpression, calcValue, Nothing)
            Else
                calcValue = CDbl(calcDt.Compute(calcExpression, ""))
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 组合计算式，返回组合完的计算式
    ''' </summary>
    ''' <param name="baseFormula">待组合的基准计算式</param>
    ''' <param name="times">基准计算式出现次数</param>
    ''' <param name="multiple">基准计算式对应的套数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CombineFormula(ByVal baseFormula As String, ByVal times As Integer, Optional ByVal multiple As Byte = 1) As String
        If times = 1 AndAlso multiple = 1 Then Return baseFormula
        Dim newBaseFormula = Regex.Replace(baseFormula, "【([^【】])+】", "")
        If Regex.IsMatch(newBaseFormula, "^\d+(\.\d+)?([\+|\-|\*|/]\d+(\.\d+)?)+$") Then '不包含的括号的四则运算表达式，如“1.25+3.1+0.2”
            newBaseFormula = "(" & baseFormula & ")"
        ElseIf baseFormula.Contains(CChar("(")) Then
            newBaseFormula = "[" & baseFormula & "]"
        Else
            newBaseFormula = baseFormula
        End If
        If multiple > 1 Then
            newBaseFormula &= "*" & multiple & "【套数】"
        End If
        If times > 1 Then
            newBaseFormula &= "*" & times
        End If
        Return newBaseFormula
    End Function

    ''' <summary>
    ''' 获取配线标注文本如：BV-3×2.5+2×1.5、3×KVV3*1.5
    ''' </summary>
    ''' <param name="wiringModel1">配线规格1，如果不存在则存入空字符串</param>
    ''' <param name="wiringModel2">配线规格2，如果不存在则存入空字符串</param>
    ''' <param name="wiringCount1">配线根数1，如果不存在则存入0</param>
    ''' <param name="wiringCount2">配线根数2，如果不存在则存入0</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetWiringMarkText(ByVal wiringModel1 As String, ByVal wiringModel2 As String, ByVal wiringCount1 As Integer, ByVal wiringCount2 As Integer) As String
        Dim wiringText1 As String = ""
        Dim wiringText2 As String = ""
        Dim matchItem As Match = Nothing
        If wiringCount1 > 0 AndAlso wiringModel1 <> "" Then
            matchItem = Regex.Match(wiringModel1, "(?<A>\S+)-(?<B>\d+(\.\d+)?)$")
            If matchItem.Success Then
                wiringText1 = matchItem.Groups("A").Value & "-" & wiringCount1 & "×" & matchItem.Groups("B").Value
            ElseIf wiringCount1 <> 1 Then
                wiringText1 = wiringCount1 & "×" & wiringModel1
            Else
                wiringText1 = wiringModel1
            End If
        End If
        If wiringCount2 > 0 AndAlso wiringModel2 <> "" Then
            matchItem = Regex.Match(wiringModel2, "(?<A>\S+)-(?<B>\d+(\.\d+)?)$")
            If matchItem.Success Then
                wiringText2 = matchItem.Groups("A").Value & "-" & wiringCount2 & "×" & matchItem.Groups("B").Value
            ElseIf wiringCount2 <> 1 Then
                wiringText2 = wiringCount2 & "×" & wiringModel2
            Else
                wiringText2 = wiringModel2
            End If
        End If
        If wiringText1 <> "" AndAlso wiringText2 <> "" Then
            Return wiringText1 & "+" & wiringText2
        ElseIf wiringText1 <> "" Then
            Return wiringText1
        ElseIf wiringText2 <> "" Then
            Return wiringText2
        Else
            Return ""
        End If
    End Function

End Class
