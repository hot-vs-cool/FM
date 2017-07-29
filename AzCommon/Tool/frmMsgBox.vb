
''' <summary>
''' 自定义简单消息框
''' </summary>
''' <remarks></remarks>
Friend Class frmMsgBox

    Private _buttonStyleType As AzTool.AzMsgBox.MsgButtons

    ''' <summary>
    ''' 获取或设置消息框按钮类型
    ''' </summary>
    Public Property ButtonType As AzTool.AzMsgBox.MsgButtons
        Get
            Return _buttonStyleType
        End Get
        Set(ByVal value As AzTool.AzMsgBox.MsgButtons)
            Me._buttonStyleType = value
            Select Case value
                Case AzTool.AzMsgBox.MsgButtons.OkCancel
                    Me.Button1.Visible = False
                    Me.Button2.Text = "确定"
                    Me.Button3.Text = "取消"
                Case AzTool.AzMsgBox.MsgButtons.OkOnly
                    Me.Button1.Visible = False
                    Me.Button2.Visible = False
                    Me.Button3.Text = "确定"
                Case AzTool.AzMsgBox.MsgButtons.YesNo
                    Me.Button1.Visible = False
                    Me.Button2.Text = "是"
                    Me.Button3.Text = "否"
                Case AzTool.AzMsgBox.MsgButtons.YesNoCancel
                    Me.Button1.Text = "是"
                    Me.Button2.Text = "否"
                    Me.Button3.Text = "取消"
                Case AzTool.AzMsgBox.MsgButtons.AbortRetryIgnore
                    Me.Button1.Text = "终止"
                    Me.Button2.Text = "重试"
                    Me.Button3.Text = "忽略"
            End Select
        End Set
    End Property

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click, Button2.Click, Button1.Click
        Dim btnSender = CType(sender, Button)
        Select Case btnSender.Text
            Case "确定"
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "取消"
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Case "是"
                Me.DialogResult = Windows.Forms.DialogResult.Yes
            Case "否"
                Me.DialogResult = Windows.Forms.DialogResult.No
            Case "终止"
                Me.DialogResult = Windows.Forms.DialogResult.Abort
            Case "重试"
                Me.DialogResult = Windows.Forms.DialogResult.Retry
            Case "忽略"
                Me.DialogResult = Windows.Forms.DialogResult.Ignore
            Case Else
                Me.DialogResult = Windows.Forms.DialogResult.None
        End Select
    End Sub

End Class