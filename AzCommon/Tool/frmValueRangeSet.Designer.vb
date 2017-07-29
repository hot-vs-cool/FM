<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmValueRangeSet
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
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.cboEndThickness = New System.Windows.Forms.ComboBox()
        Me.lblValueType = New System.Windows.Forms.Label()
        Me.txtStartThickness = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(43, 37)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(53, 23)
        Me.btnOK.TabIndex = 13
        Me.btnOK.Text = "确定"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(129, 37)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(53, 23)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "取消"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'cboEndThickness
        '
        Me.cboEndThickness.FormattingEnabled = True
        Me.cboEndThickness.Items.AddRange(New Object() {"+∞"})
        Me.cboEndThickness.Location = New System.Drawing.Point(165, 8)
        Me.cboEndThickness.Name = "cboEndThickness"
        Me.cboEndThickness.Size = New System.Drawing.Size(64, 20)
        Me.cboEndThickness.TabIndex = 11
        '
        'lblValueType
        '
        Me.lblValueType.Location = New System.Drawing.Point(66, 12)
        Me.lblValueType.Name = "lblValueType"
        Me.lblValueType.Size = New System.Drawing.Size(93, 12)
        Me.lblValueType.TabIndex = 10
        Me.lblValueType.Text = "＜厚度(mm)≤"
        Me.lblValueType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtStartThickness
        '
        Me.txtStartThickness.Location = New System.Drawing.Point(11, 7)
        Me.txtStartThickness.Name = "txtStartThickness"
        Me.txtStartThickness.Size = New System.Drawing.Size(51, 21)
        Me.txtStartThickness.TabIndex = 9
        '
        'frmValueRangeSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(237, 67)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.cboEndThickness)
        Me.Controls.Add(Me.lblValueType)
        Me.Controls.Add(Me.txtStartThickness)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmValueRangeSet"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "风管厚度"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cboEndThickness As System.Windows.Forms.ComboBox
    Friend WithEvents lblValueType As System.Windows.Forms.Label
    Friend WithEvents txtStartThickness As System.Windows.Forms.TextBox
End Class
