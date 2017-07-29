Public Class FormalCount
    Public stack1 As New Stack '存放操作数
    Public stack2 As New Stack '存放操作符号
    Public str As String
    Private len As Integer
    Private temp As String = ""
    Private nofpoint As Integer = 0 '每个操作数的小数点个数
    Private a() As String = Nothing '把字符串分成每个字符

    ''' <summary>
    ''' 判断是否为运算符（）+-*/#这几个字符
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsOper(ByVal e As String) As Boolean
        Select Case e
            Case "*"
                Return True
            Case "/"
                Return True
            Case "+"
                Return True
            Case "-"
                Return True
            Case "^"
                Return True
            Case "sqrt"
                Return True
            Case "sin"
                Return True
            Case "cos"
                Return True
            Case "tan"
                Return True
            Case "cot"
                Return True
            Case "("
                Return True
            Case ")"
                Return True
            Case "#"
                Return True
            Case Else
                Return (False)
        End Select
    End Function

    ''' <summary>
    ''' 栈内运算符号的优先
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function inOperatorLever(ByVal e As String) As Integer

        Select Case e

            Case "+"
                Return 2
            Case "-"
                Return 2
            Case "*"
                Return 4
            Case "/"
                Return 4
            Case "sin"
                Return 6
            Case "cos"
                Return 6
            Case "tan"
                Return 6
            Case "ctan"
                Return 6
            Case "sqrt"
                Return 6
            Case "^"
                Return 7
            Case "("
                Return 0
            Case ")"
                Return 9

            Case Else

                Return -1
        End Select
    End Function

    ''' <summary>
    ''' 堆栈外的运算符优先级
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutOperatorLever(ByVal e As String) As Integer
        Select Case e
            Case CChar("+")
                Return 1
            Case CChar("-")
                Return 1
            Case CChar("*")
                Return 3
            Case CChar("/")
                Return 3
            Case "sin"
                Return 5
            Case "cos"
                Return 5
            Case "tan"
                Return 5
            Case "cot"
                Return 5
            Case "sqrt"
                Return 5
            Case CChar("^")
                Return 8
            Case CChar("(")
                Return 9
            Case CChar(")")
                Return 0
            Case Else
                Return -1
        End Select
    End Function

    ''' <summary>
    ''' 算术的四则运算
    ''' </summary>
    ''' <param name="DFirstin"></param>
    ''' <param name="oper"></param>
    ''' <param name="dLastin"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Count(ByVal DFirstin As Double, ByVal oper As String, ByVal dLastin As Double) As Double
        Select Case oper
            Case "+"
                Return DFirstin + dLastin
            Case "-"
                Return DFirstin - dLastin
            Case "*"
                Return DFirstin * dLastin
            Case "/"
                If dLastin = 0 Then Return 0
                Return DFirstin / dLastin
            Case "^"
                Return Math.Pow(DFirstin, dLastin)
            Case "sin"
                Return Math.Sin(dLastin * Math.PI / 180)
            Case "cos"
                Return Math.Cos(dLastin * Math.PI / 180)
            Case "tan"
                Return Math.Tan(dLastin * Math.PI / 180)
            Case "cot"
                Return 1 / Math.Tan(dLastin * Math.PI / 180)
            Case "sqrt"
                Return Math.Sqrt(dLastin)
            Case Else
                Return 0
        End Select
    End Function

    ''' <summary>
    ''' 计算字符串计算式运算结果;返回值“True”表示计算成功,反之计算失败
    ''' </summary>
    ''' <param name="str">待计算的字符串表达式</param>
    ''' <param name="result">计算结果</param>
    ''' <param name="wrongstr">计算失败的时候返回的错误信息文本</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CountStr(ByVal str As String, ByRef result As Double, ByRef wrongstr As String) As Boolean
        If str Is Nothing OrElse str.Length = 0 Then  'str为空，或者长度为0
            result = 0
            wrongstr = ""
            Return True
        End If
        If GetStr(str, "(") <> GetStr(str, ")") Or GetStr(str, "[") <> GetStr(str, "]") Or GetStr(str, "{") <> GetStr(str, "}") Then
            result = 0
            wrongstr = "括号不匹配"
            Return False
        End If
        str = str.Replace("[", "(")
        str = str.Replace("]", ")")
        str = str.Replace("{", "(")
        str = str.Replace("}", ")")

        Dim len As Integer = str.Length  '字符串长度
        ReDim a(len - 1)
        For index As Integer = 0 To str.Length - 1
            a(index) = str.Substring(index, 1)
        Next
        Dim i As Integer = 0      '遍历数组
        Dim firstIN As Double  '第一个操作数
        Dim lastIN As Double  '第二个操作数
        Dim operate As String '运算符号
        Dim g As Integer = 0 '判断传进的每个字符是否合法

        stack1.Clear()
        stack2.Clear()

        While i < a.Length   '取字符串的每个字符
            g = 0

            If a(i) = "" Then  '传进空字符继续传下一个字符
                Continue While
            End If

            If (IsNumeric(a(i))) Then      '若是数字的处理
                g = 1
                Dim Nstr As String = ""
                If Not Number(i, Nstr) Then
                    result = 0
                    temp = ""
                    nofpoint = 0
                    wrongstr = Nstr
                    Return False
                End If
                i = i + 1
                Continue While
            End If

            '若是小数点处理
            If a(i) = "." Then
                If i + 1 >= a.Length Then Return False
                g = 1
                If i = 0 OrElse Not IsNumeric(a(i - 1)) Then
                    nofpoint = 0
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，小数点出错"
                    Return False
                End If
                If i = a.Length - 1 OrElse Not IsNumeric(a(i + 1)) Then
                    If i < a.Length - 1 Then
                        nofpoint = 0
                        result = 0
                        wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & a(i + 1) & "，小数点后面跟的字符出错"
                        Return False
                    End If
                End If
                If nofpoint > 0 Then
                    nofpoint = 0
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i - 1) & a(i) & "，小数点个数超过1出错"
                    Return False
                End If
                temp = temp + a(i)
                nofpoint += 1
                i += 1
                Continue While
            End If

            '若是左边括号处理
            If a(i) = "(" Then
                g = 1
                If i = a.Length - 1 Then
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，左括号在最后一位"
                    Return False
                End If
                'Dim s As String = a(i + 1)
                If Not IsNumeric(a(i + 1)) And a(i + 1) <> "s" And a(i + 1) <> "c" And a(i + 1) <> "t" And a(i + 1) <> "(" And a(i + 1) <> "-" Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & a(i + 1) & "，左括号后面跟的字符出错"
                End If
                stack2.Push(a(i))
                i += 1
                Continue While

            End If

            '若是减号处理
            If a(i) = "-" Then
                g = 1
                If i = a.Length - 1 Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，减号在最后一位"
                    Return False
                End If
                'Dim s As String = a(i + 1)
                If Not IsNumeric(a(i + 1)) And a(i + 1) <> "s" And a(i + 1) <> "c" And a(i + 1) <> "t" And a(i + 1) <> "(" Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & a(i + 1) & "，减号后面跟的字符出错"
                    Return False
                End If
                If i = 0 OrElse a(i - 1) = "(" Then
                    stack1.Push(0)
                    stack2.Push(a(i))
                    i += 1
                    Continue While
                End If

                While True
                    If stack2.Count = 0 OrElse inOperatorLever(CStr(stack2.Peek)) < OutOperatorLever(a(i)) Then
                        stack2.Push(a(i))
                        i += 1
                        Exit While
                    End If
                    If inOperatorLever(CStr(stack2.Peek)) = OutOperatorLever(a(i)) Then
                        wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & ",操作符出错"
                        Return False
                    Else
                        If stack1.Count < 2 Then
                            wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & ",操作符出错"
                            Return False
                        End If
                        lastIN = CDbl(stack1.Pop())
                        firstIN = CDbl(stack1.Pop())
                        operate = CStr(stack2.Pop())
                        stack1.Push(Count(firstIN, operate, lastIN))
                        'stack2.Push(a(i))
                        'i += 1
                        'Continue While
                    End If
                End While
                Continue While
            End If

            '若是右边括号处理
            If a(i) = (")") Then
                g = 1
                If i < a.Length - 1 AndAlso a(i + 1) <> "+" AndAlso a(i + 1) <> "-" AndAlso a(i + 1) <> "*" AndAlso a(i + 1) <> "/" AndAlso a(i + 1) <> "^" AndAlso a(i + 1) <> ")" AndAlso a(i + 1) <> " " Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & a(i + 1) & "，右括号后面跟的字符出错"
                    Return False
                End If
                If i = 0 Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，右括号在首字符"
                    Return False
                End If
                If stack1.Count = 0 Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，右括号出错"
                    Return False
                End If
                If stack2.Count = 0 Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，右括号出错"
                    Return False
                End If
                If Not brackets(i, wrongstr) Then
                    result = 0
                End If
                i += 1
                Continue While
            End If


            '若是加减乘除平方处理
            If a(i) = "+" Or a(i) = "*" Or a(i) = "/" Or a(i) = "^" Then
                g = 1
                Dim Jstr As String = ""
                If Not JjccPf(i, Jstr) Then
                    result = 0
                    wrongstr = Jstr
                    Return False
                End If
                i += 1
                Continue While
            End If

            If a(i) = "s" Then
                If i + 3 > a.Length - 1 Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，sin或sqrt输入出错"
                    Return False
                End If
                If a(i + 1) = "i" And a(i + 2) = "n" And IsNumeric(a(i + 3)) Or (i + 4 < a.Length AndAlso a(i + 3) = "(") Then
                    i += 3
                    While True
                        If stack2.Count = 0 OrElse inOperatorLever(CStr(stack2.Peek)) < OutOperatorLever("sin") Then
                            stack1.Push("0")
                            stack2.Push("sin")
                            Exit While
                        ElseIf inOperatorLever(CStr(stack2.Peek)) = OutOperatorLever("sin") Then
                            result = 0
                            wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & a(i + 1) & a(i + 2) & "，sin或sqrt输入出错"
                            Return False
                        Else
                            If stack1.Count < 2 Then
                                result = 0
                                wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，sin或sqrt输入出错"
                                Return False
                            End If
                            lastIN = CDbl(stack1.Pop())
                            firstIN = CDbl(stack1.Pop())
                            operate = CStr(stack2.Pop())
                            stack1.Push(Count(firstIN, operate, lastIN))
                            'stack2.Push("sin")
                        End If
                    End While
                    Continue While
                End If
                If i + 4 > a.Length - 1 Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，sin或sqrt输入出错"
                    Return False
                End If
                If a(i + 1) = "q" And a(i + 2) = "r" And a(i + 3) = "t" AndAlso a(i + 4) = "(" Then
                    i += 4
                    While True
                        If stack2.Count = 0 OrElse inOperatorLever(CStr(stack2.Peek)) < OutOperatorLever("sqrt") Then
                            stack1.Push("0")
                            stack2.Push("sqrt")
                            Exit While
                        ElseIf inOperatorLever(CStr(stack2.Peek)) = OutOperatorLever("sqrt") Then
                            result = 0
                            wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，sin或sqrt输入出错"
                            Return False
                        Else
                            If stack1.Count < 2 Then
                                result = 0
                                wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，sin或sqrt输入出错"
                                Return False
                            End If
                            lastIN = CDbl(stack1.Pop())
                            firstIN = CDbl(stack1.Pop())
                            operate = CStr(stack2.Pop())
                            stack1.Push(Count(firstIN, operate, lastIN))
                            'stack2.Push("sqrt")

                        End If
                    End While
                    Continue While
                End If
                result = 0
                wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，sin或sqrt输入出错"
                Return False
            End If
            If a(i) = "t" Then
                If i + 3 > a.Length - 1 Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，tan 输入出错"
                    Return False
                End If
                If (a(i + 1) <> "a" Or a(i + 2) <> "n" Or Not IsNumeric(a(i + 3))) Then
                    If (i + 4 < a.Length AndAlso a(i + 3) = "(") Then
                    Else
                        result = 0
                        wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，tan 输入出错"
                        Return False
                    End If
                End If
                i += 3
                While True
                    If stack2.Count = 0 OrElse inOperatorLever(CStr(stack2.Peek)) < OutOperatorLever("tan") Then
                        stack1.Push("0")
                        stack2.Push("tan")
                        Exit While
                    ElseIf inOperatorLever(CStr(stack2.Peek)) = OutOperatorLever("tan") Then
                        result = 0
                        wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，tan 输入出错"
                        Return False
                    Else
                        If stack1.Count < 2 Then
                            result = 0
                            wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，tan 输入出错"
                            Return False
                        End If
                        lastIN = CDbl(stack1.Pop())
                        firstIN = CDbl(stack1.Pop())
                        operate = CStr(stack2.Pop())
                        stack1.Push(Count(firstIN, operate, lastIN))
                        'stack2.Push("tan")
                    End If
                End While
                Continue While
            End If

            If a(i) = "c" Then
                If i + 3 > a.Length - 1 Then
                    result = 0
                    wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，cos 或 cot 输入出错"
                    Return False
                End If
                If a(i + 1) = "O" And a(i + 2) = "s" And IsNumeric(a(i + 3)) Or (i + 4 < a.Length AndAlso a(i + 3) = "(") Then
                    i += 3
                    While True
                        If stack2.Count = 0 OrElse inOperatorLever(CStr(stack2.Peek)) < OutOperatorLever("cos") Then
                            stack1.Push("0")
                            stack2.Push("cos")
                            Exit While
                        ElseIf inOperatorLever(CStr(stack2.Peek)) = OutOperatorLever("cos") Then
                            result = 0
                            wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，cos 或 cot 输入出错"
                            Return False
                        Else
                            If stack1.Count < 2 Then
                                result = 0
                                wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，cos 或 cot 输入出错"
                                Return False
                            End If
                            lastIN = CDbl(stack1.Pop())
                            firstIN = CDbl(stack1.Pop())
                            operate = CStr(stack2.Pop())
                            stack1.Push(Count(firstIN, operate, lastIN))
                            'stack2.Push("cos")
                        End If
                    End While
                    Continue While
                End If

                If a(i + 1) = "0" And a(i + 2) = "t" And IsNumeric(a(i + 3)) Or (i + 4 < a.Length AndAlso a(i + 3) = "(") Then
                    i += 3
                    While True
                        If stack2.Count = 0 OrElse inOperatorLever(CStr(stack2.Peek)) < OutOperatorLever("ctan") Then
                            stack1.Push("0")
                            stack2.Push("cot")
                            Exit While
                        ElseIf inOperatorLever(CStr(stack2.Peek)) = OutOperatorLever("cot") Then
                            result = 0
                            wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，cos 或 cot 输入出错"
                            Return False
                        Else
                            If stack1.Count < 2 Then
                                result = 0
                                wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，cos 或 cot 输入出错"
                                Return False
                            End If
                            lastIN = CDbl(stack1.Pop())
                            firstIN = CDbl(stack1.Pop())
                            operate = CStr(stack2.Pop())
                            stack1.Push(Count(firstIN, operate, lastIN))
                            'stack2.Push("cot")
                        End If
                    End While
                    Continue While
                End If
                result = 0
                wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，cos 或 cot 输入出错"
                Return False
            End If

            If (g = 0) Then
                result = 0
                wrongstr = "第" & CStr(i + 1) & "个字符" & a(i) & "，cos 或 cot 输入出错"
                Return False
            End If
        End While
        While (stack2.Count <> 0)
            If stack1.Count < 2 Then
                result = 0
                wrongstr = " 输入出错"
                Return False
            End If
            lastIN = CDbl(stack1.Pop())
            firstIN = CDbl(stack1.Pop())
            operate = CStr(stack2.Pop())
            If operate = "(" Or operate = ")" Then
                result = 0
                wrongstr = " 输入出错"
                Return False
            Else
                stack1.Push(Count(firstIN, operate, lastIN))
            End If
        End While
        If stack1.Count = 0 Then
            result = 0
            wrongstr = "输入出错"
            Return False
        End If
        result = CDbl(stack1.Pop())
        wrongstr = ""
        Return True
    End Function

    ''' <summary>
    ''' 右边括号处理
    ''' </summary>
    ''' <param name="i"></param>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function brackets(ByVal i As Integer, ByRef str As String) As Boolean
        If CStr(stack2.Peek) = "(" Then
            stack2.Pop()
            Return True
        End If
        If stack1.Count < 2 Then
            str = "第" & CStr(i + 1) & "个字符" & a(i) & "，右括号出错"
            Return False
        End If
        Dim lastIN As Double = CDbl(stack1.Pop())
        Dim firstIN As Double = CDbl(stack1.Pop())
        Dim operate As String = CStr(stack2.Pop())
        If stack2.Count = 0 Then
            str = "第" & CStr(i + 1) & "个字符" & a(i) & "，右括号出错"
            Return False
        End If
        stack1.Push(Count(firstIN, operate, lastIN))
        brackets(i, str)
        Return False
    End Function

    ''' <summary>
    ''' 数字处理
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns></returns>
    Private Function Number(ByVal i As Integer, ByRef str As String) As Boolean

        If i < a.Length - 1 AndAlso ((IsNumeric(a(i + 1))) Or a(i + 1) = ".") Then    '若后面跟着的字符还是数字，或者小数点与temp 相连接，小数点个数加1。继续取下一个字符
            temp = temp + a(i)
            Return True
        End If
        '如果小数点的个数大于1或者后面跟着的不是+-*/）这5个运算符 出错
        If (i < a.Length - 1 AndAlso a(i + 1) <> "-" AndAlso a(i + 1) <> "+" AndAlso a(i + 1) <> "*" _
            AndAlso a(i + 1) <> "/" AndAlso a(i + 1) <> "^" AndAlso a(i + 1) <> ")" AndAlso a(i + 1) <> "") Then
            str = "第" & CStr(i + 1) & "个字符" & a(i) & a(i + 1) & ",数字后面跟着字符不符合"
            Return False
        End If

        '标准的情况后把这个字符连入temp，并入栈，清空小数点个数，和temp
        temp = temp + a(i)
        If temp.Substring(0, 1) = "0" Then
            If temp.Length > 1 AndAlso temp.Substring(1, 1) <> "." Then
                str = "0后面跟着字符不符合"
                Return False
            End If
        End If
        Dim num As Double = CDbl(CStr((temp)))
        stack1.Push(num)
        nofpoint = 0
        temp = ""
        Return True
    End Function

    ''' <summary>
    ''' 加减乘除处理
    ''' </summary>
    ''' <param name="i"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JjccPf(ByVal i As Integer, ByRef str As String) As Boolean
        If i = 0 Then
            str = "第" & CStr(i + 1) & "个字符" & a(i) & ",操作符在第一位"
            Return False
        End If
        If i = a.Length - 1 Then    '如果加减乘除开方的字符是最后一个则出错
            str = "第" & CStr(i + 1) & "个字符" & a(i) & ",操作符在最后一位"
            Return False
        End If
        If Not IsNumeric(a(i + 1)) And a(i + 1) <> "(" And a(i + 1) <> "s" And a(i + 1) <> "c" And a(i + 1) <> "t" Then  '“+、-、*、/、^”后面跟的不是数字或“（”，错误 
            str = "第" & CStr(i + 1) & "个字符" & a(i) & a(i + 1) & ",操作符后面跟着字符不符合"
            Return False
        End If
        While True
            If stack2.Count = 0 OrElse inOperatorLever(CStr(stack2.Peek)) < OutOperatorLever(a(i)) Then
                stack2.Push(a(i))
                Return True
            End If
            If inOperatorLever(CStr(stack2.Peek)) = OutOperatorLever(a(i)) Then
                str = "第" & CStr(i + 1) & "个字符" & a(i) & ",操作符出错"
                Return False
            Else
                If stack1.Count < 2 Then
                    str = "第" & CStr(i + 1) & "个字符" & a(i) & ",操作符出错"
                    Return False
                End If
                Dim lastIN As Double = CDbl(stack1.Pop())
                Dim firstIN As Double = CDbl(stack1.Pop())
                Dim operate As String = CStr(stack2.Pop())
                stack1.Push(Count(firstIN, operate, lastIN))
                'stack2.Push(a(i))
            End If
        End While
        Return False
    End Function

    Private Function GetStr(ByVal str As String, ByVal indexStr As String) As Integer
        If str Is Nothing OrElse str.Length = 0 Then Return 0
        If indexStr Is Nothing OrElse str.Length = 0 Then Return 0
        Dim Count As Integer = 0
        For i As Integer = 0 To str.Length - 1
            If str.Substring(i, 1) = indexStr Then
                Count += 1
            End If
        Next
        Return Count
    End Function

End Class
