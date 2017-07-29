Imports System.Windows.Forms

Public Class FormProgress
    Private tipInfo As String    '提示信息
    Private tipInfoCPT As String    '提示信息
    Private curVal As Double    '当前进度值0~100
    Private curValCPT As Double    '当前进度值0~100
    Private run As Boolean = False
    Private IsHide As Boolean = False
    Private beginTime As Date

    Private endTimeStr As String = "" ' 结束时间
    ''' <summary>进度线程</summary>
    Private Sub ProgressThread()
        Dim frm As New FormProgress
        frm.Show()
        While (run)
            If IsHide Then
                frm.Hide()
                frm.labelTip.Text = tipInfo
                frm.labelTipCPT.Text = tipInfoCPT
                frm.prgTip.Value = CInt(curVal)
                frm.prgTipCPt.Value = CInt(curValCPT)
                frm.LabelTime.Text = SetTime(Now)
                Threading.Thread.Sleep(100)
                Application.DoEvents()
            Else
                frm.Show()
                frm.labelTip.Text = tipInfo
                frm.labelTipCPT.Text = tipInfoCPT
                frm.prgTip.Value = CInt(curVal)
                frm.prgTipCPt.Value = CInt(curValCPT)
                frm.LabelTime.Text = SetTime(Now)
                Threading.Thread.Sleep(100)
                Application.DoEvents()
            End If
        End While
    End Sub


    ''' <summary>开始进度</summary>
    Public Sub StartProgress(ByVal tipInfo As String)
        Me.tipInfo = tipInfo
        Me.tipInfoCPT = ""
        curVal = 0
        curValCPT = 0
        run = True
        beginTime = Now
        Dim t As New Threading.Thread(AddressOf ProgressThread)

        t.IsBackground = True

        t.Start()
    End Sub

    ''' <summary>停止进度</summary>
    Public Sub EndProgress()
        run = False
    End Sub

    ''' <summary>设置进度信息</summary>
    ''' <param name="tipInfo">提示信息</param>
    ''' <param name="val">进度值0~100</param>
    Public Sub SetProgress(ByVal tipInfo As String, ByVal val As Double)
        Me.tipInfo = tipInfo
        If val > 100 Then
            val = 100
        End If
        curVal = val
    End Sub

    ''' <summary>设置进度信息</summary>
    ''' <param name="tipInfo">提示信息</param>
    ''' <param name="val">进度值0~100</param>
    Public Sub SetProgressCPT(ByVal tipInfo As String, ByVal val As Double)
        Me.tipInfoCPT = tipInfo
        If val > 100 Then
            val = 100
        End If
        curValCPT = val
        'SetTime(Now)
    End Sub

    Private Function SetTime(ByVal curtm As Date) As String
        Dim hour As Integer = (curtm.Hour - beginTime.Hour)
        Dim minute As Integer = (curtm.Minute - beginTime.Minute)
        Dim second As Integer = (curtm.Second - beginTime.Second)
        If second < 0 Then
            minute -= 1
            second += 60
        End If
        If minute < 0 Then
            If beginTime.Date <> curtm.Date Then
                hour = 24 - beginTime.Hour + curtm.Hour + 1
                hour -= 1
                minute += 60
            Else
                hour -= 1
                minute += 60
            End If

        End If

        'times = "本次计算共用时： " + CStr(hour) + " 小时 " + CStr(minute) + " 分 " + CStr(second) + " 秒 "

        Dim tm As String = CStr(hour) & "时  " & CStr(minute) & "分钟  " & CStr(second) & "秒"
        endTimeStr = tm
        Return tm
    End Function
    ''' <summary>
    ''' 获取结束时间
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEndTime() As String
        Return endTimeStr
    End Function

    ''' <summary>
    ''' 得当前值
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurProgress() As Double
        Return curVal
    End Function


End Class