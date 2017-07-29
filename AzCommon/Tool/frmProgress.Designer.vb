<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProgress
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pgbMain = New System.Windows.Forms.ProgressBar()
        Me.pgbSub = New System.Windows.Forms.ProgressBar()
        Me.lblPrompt2 = New System.Windows.Forms.Label()
        Me.lblPrompt1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pgbMain
        '
        Me.pgbMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbMain.Location = New System.Drawing.Point(12, 30)
        Me.pgbMain.Name = "pgbMain"
        Me.pgbMain.Size = New System.Drawing.Size(346, 23)
        Me.pgbMain.Step = 1
        Me.pgbMain.TabIndex = 0
        '
        'pgbSub
        '
        Me.pgbSub.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbSub.Location = New System.Drawing.Point(12, 87)
        Me.pgbSub.Maximum = 101
        Me.pgbSub.Name = "pgbSub"
        Me.pgbSub.Size = New System.Drawing.Size(346, 23)
        Me.pgbSub.Step = 1
        Me.pgbSub.TabIndex = 1
        '
        'lblPrompt2
        '
        Me.lblPrompt2.AutoSize = True
        Me.lblPrompt2.Location = New System.Drawing.Point(12, 70)
        Me.lblPrompt2.Name = "lblPrompt2"
        Me.lblPrompt2.Size = New System.Drawing.Size(119, 12)
        Me.lblPrompt2.TabIndex = 2
        Me.lblPrompt2.Text = "执行第0项，进度100%"
        '
        'lblPrompt1
        '
        Me.lblPrompt1.AutoSize = True
        Me.lblPrompt1.Location = New System.Drawing.Point(12, 13)
        Me.lblPrompt1.Name = "lblPrompt1"
        Me.lblPrompt1.Size = New System.Drawing.Size(77, 12)
        Me.lblPrompt1.TabIndex = 3
        Me.lblPrompt1.Text = "共0项，第0项"
        '
        'frmProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 125)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblPrompt1)
        Me.Controls.Add(Me.lblPrompt2)
        Me.Controls.Add(Me.pgbSub)
        Me.Controls.Add(Me.pgbMain)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmProgress"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "进度"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pgbMain As System.Windows.Forms.ProgressBar
    Friend WithEvents pgbSub As System.Windows.Forms.ProgressBar
    Friend WithEvents lblPrompt2 As System.Windows.Forms.Label
    Friend WithEvents lblPrompt1 As System.Windows.Forms.Label
End Class
