Namespace AzTool
    ''' <summary>
    ''' 显示可包含文本和按钮的消息框
    ''' </summary>
    Public Class AzMsgBox

        ''' <summary>
        ''' 对话框按钮类型枚举
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum MsgButtons
            ''' <summary>
            ''' “确定”和“取消”按钮
            ''' </summary>
            OkCancel
            ''' <summary>
            ''' 仅“确定”按钮（默认）
            ''' </summary>
            OkOnly
            ''' <summary>
            ''' “是”和“否”按钮
            ''' </summary>
            YesNo
            ''' <summary>
            ''' “是”、“否”和“取消”按钮
            ''' </summary>
            YesNoCancel
            ''' <summary>
            ''' “终止”、“重试”和“忽略”按钮
            ''' </summary>
            AbortRetryIgnore
        End Enum

        ''' <summary>
        '''显示具有指定文本消息框
        ''' </summary>
        ''' <param name="text">要在消息框中显示的文本</param>
        ''' <remarks></remarks>
        Public Shared Function Show(ByVal text As String) As DialogResult
            Dim frmDialog As New frmMsgBox
            frmDialog.lblTipMsg.Text = text.Trim
            frmDialog.ButtonType = MsgButtons.OkOnly
            Return frmDialog.ShowDialog
        End Function

        ''' <summary>
        '''在指定所有者对象面前显示具有指定文本消息框
        ''' </summary>
        ''' <param name="owner">拥有模式对话框的对象</param>
        ''' <param name="text">要在消息框中显示的文本</param>
        ''' <remarks></remarks>
        Public Shared Function Show(ByVal owner As System.Windows.Forms.IWin32Window, ByVal text As String) As DialogResult
            Dim frmDialog As New frmMsgBox
            frmDialog.lblTipMsg.Text = text.Trim
            frmDialog.ButtonType = MsgButtons.OkOnly
            Return frmDialog.ShowDialog(owner)
        End Function

        ''' <summary>
        '''显示具有指定文本和按钮的消息框
        ''' </summary>
        ''' <param name="text">要在消息框中显示的文本</param>
        ''' <param name="buttons">指定在消息框中显示的按钮类型</param>
        ''' <remarks></remarks>
        Public Shared Function Show(ByVal text As String, ByVal buttons As MsgButtons) As DialogResult
            Dim frmDialog As New frmMsgBox
            frmDialog.lblTipMsg.Text = text.Trim
            frmDialog.ButtonType = buttons
            Return frmDialog.ShowDialog
        End Function

        ''' <summary>
        '''在指定所有者对象面前显示具有指定文本和按钮的消息框
        ''' </summary>
        ''' <param name="owner">拥有模式对话框的对象</param>
        ''' <param name="text">要在消息框中显示的文本</param>
        ''' <param name="buttons">指定在消息框中显示的按钮类型</param>
        ''' <remarks></remarks>
        Public Shared Function Show(ByVal owner As System.Windows.Forms.IWin32Window, ByVal text As String, ByVal buttons As MsgButtons) As DialogResult
            Dim frmDialog As New frmMsgBox
            frmDialog.lblTipMsg.Text = text.Trim
            frmDialog.ButtonType = buttons
            Return frmDialog.ShowDialog(owner)
        End Function

        ''' <summary>
        ''' 显示具有指定文本、标题和按钮的消息框
        ''' </summary>
        ''' <param name="text">要在消息框中显示的文本</param>
        ''' <param name="caption">要在消息框标题栏显示的文本</param>
        ''' <param name="buttons">指定在消息框中显示的按钮类型</param>
        ''' <remarks></remarks>
        Public Shared Function Show(ByVal text As String, ByVal caption As String, ByVal buttons As MsgButtons) As DialogResult
            Dim frmDialog As New frmMsgBox
            frmDialog.lblTipMsg.Text = text.Trim
            frmDialog.Text = caption.Trim
            frmDialog.ButtonType = buttons
            Return frmDialog.ShowDialog
        End Function

        ''' <summary>
        ''' 在指定所有者对象面前显示具有指定文本、标题和按钮的消息框
        ''' </summary>
        ''' <param name="owner">拥有模式对话框的对象</param>
        ''' <param name="text">要在消息框中显示的文本</param>
        ''' <param name="caption">要在消息框标题栏显示的文本</param>
        ''' <param name="buttons">指定在消息框中显示的按钮类型</param>
        ''' <remarks></remarks>
        Public Shared Function Show(ByVal owner As System.Windows.Forms.IWin32Window, ByVal text As String, ByVal caption As String, ByVal buttons As MsgButtons) As DialogResult
            Dim frmDialog As New frmMsgBox
            frmDialog.lblTipMsg.Text = text.Trim
            frmDialog.Text = caption.Trim
            frmDialog.ButtonType = buttons
            Return frmDialog.ShowDialog(owner)
        End Function

    End Class
End Namespace
