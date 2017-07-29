<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormProgress
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
        Me.prgTip = New System.Windows.Forms.ProgressBar()
        Me.LabelTime = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.prgTipCPt = New System.Windows.Forms.ProgressBar()
        Me.labelTipCPT = New System.Windows.Forms.Label()
        Me.labelTip = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'prgTip
        '
        Me.prgTip.Location = New System.Drawing.Point(2, 33)
        Me.prgTip.Name = "prgTip"
        Me.prgTip.Size = New System.Drawing.Size(330, 23)
        Me.prgTip.TabIndex = 1
        '
        'LabelTime
        '
        Me.LabelTime.AutoSize = True
        Me.LabelTime.Location = New System.Drawing.Point(165, 127)
        Me.LabelTime.Name = "LabelTime"
        Me.LabelTime.Size = New System.Drawing.Size(29, 12)
        Me.LabelTime.TabIndex = 3
        Me.LabelTime.Text = "分钟"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(89, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "已用时间:"
        '
        'prgTipCPt
        '
        Me.prgTipCPt.Location = New System.Drawing.Point(2, 90)
        Me.prgTipCPt.Name = "prgTipCPt"
        Me.prgTipCPt.Size = New System.Drawing.Size(330, 23)
        Me.prgTipCPt.TabIndex = 1
        '
        'labelTipCPT
        '
        Me.labelTipCPT.Location = New System.Drawing.Point(0, 58)
        Me.labelTipCPT.Name = "labelTipCPT"
        Me.labelTipCPT.Size = New System.Drawing.Size(329, 29)
        Me.labelTipCPT.TabIndex = 0
        Me.labelTipCPT.Text = "构件进度信息"
        Me.labelTipCPT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labelTip
        '
        Me.labelTip.Location = New System.Drawing.Point(7, 5)
        Me.labelTip.Name = "labelTip"
        Me.labelTip.Size = New System.Drawing.Size(322, 25)
        Me.labelTip.TabIndex = 0
        Me.labelTip.Text = "总进度信息"
        Me.labelTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FormProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 148)
        Me.ControlBox = False
        Me.Controls.Add(Me.LabelTime)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.prgTipCPt)
        Me.Controls.Add(Me.labelTip)
        Me.Controls.Add(Me.labelTipCPT)
        Me.Controls.Add(Me.prgTip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormProgress"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "进度"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents prgTip As System.Windows.Forms.ProgressBar
    Friend WithEvents LabelTime As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents prgTipCPt As System.Windows.Forms.ProgressBar
    Friend WithEvents labelTipCPT As System.Windows.Forms.Label
    Friend WithEvents labelTip As System.Windows.Forms.Label
End Class
