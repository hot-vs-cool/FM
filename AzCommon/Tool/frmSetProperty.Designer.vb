<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetProperty
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
        Me.laName = New System.Windows.Forms.Label()
        Me.lblCptGroup = New System.Windows.Forms.Label()
        Me.txtComponentName = New System.Windows.Forms.TextBox()
        Me.cboCptGroup = New System.Windows.Forms.ComboBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.cboInstallHeight = New System.Windows.Forms.ComboBox()
        Me.cboCptClass = New System.Windows.Forms.ComboBox()
        Me.lblCptClass = New System.Windows.Forms.Label()
        Me.laInstallHeight = New System.Windows.Forms.Label()
        Me.btnExtractCptName = New System.Windows.Forms.Button()
        Me.splCptProperty = New System.Windows.Forms.SplitContainer()
        Me.lblPrompt = New System.Windows.Forms.Label()
        Me.btnCollapse = New System.Windows.Forms.Button()
        Me.prgCptProperty = New System.Windows.Forms.PropertyGrid()
        CType(Me.splCptProperty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splCptProperty.Panel1.SuspendLayout()
        Me.splCptProperty.Panel2.SuspendLayout()
        Me.splCptProperty.SuspendLayout()
        Me.SuspendLayout()
        '
        'laName
        '
        Me.laName.AutoSize = True
        Me.laName.Location = New System.Drawing.Point(5, 9)
        Me.laName.Name = "laName"
        Me.laName.Size = New System.Drawing.Size(53, 12)
        Me.laName.TabIndex = 0
        Me.laName.Text = "构件名称"
        '
        'lblCptGroup
        '
        Me.lblCptGroup.AutoSize = True
        Me.lblCptGroup.Location = New System.Drawing.Point(5, 39)
        Me.lblCptGroup.Name = "lblCptGroup"
        Me.lblCptGroup.Size = New System.Drawing.Size(53, 12)
        Me.lblCptGroup.TabIndex = 2
        Me.lblCptGroup.Text = "构件分组"
        '
        'txtComponentName
        '
        Me.txtComponentName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtComponentName.Location = New System.Drawing.Point(62, 6)
        Me.txtComponentName.Name = "txtComponentName"
        Me.txtComponentName.Size = New System.Drawing.Size(162, 21)
        Me.txtComponentName.TabIndex = 3
        '
        'cboCptGroup
        '
        Me.cboCptGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCptGroup.FormattingEnabled = True
        Me.cboCptGroup.Location = New System.Drawing.Point(62, 36)
        Me.cboCptGroup.Name = "cboCptGroup"
        Me.cboCptGroup.Size = New System.Drawing.Size(162, 20)
        Me.cboCptGroup.TabIndex = 5
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(50, 144)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(64, 23)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "确定"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'cboInstallHeight
        '
        Me.cboInstallHeight.FormattingEnabled = True
        Me.cboInstallHeight.Items.AddRange(New Object() {"H", "1.40", "1.50", "2.70", "2.80", "2.90", "3.00", "3.00", "3.10", "3.20"})
        Me.cboInstallHeight.Location = New System.Drawing.Point(62, 96)
        Me.cboInstallHeight.Name = "cboInstallHeight"
        Me.cboInstallHeight.Size = New System.Drawing.Size(162, 20)
        Me.cboInstallHeight.TabIndex = 15
        Me.cboInstallHeight.Text = "H"
        '
        'cboCptClass
        '
        Me.cboCptClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCptClass.FormattingEnabled = True
        Me.cboCptClass.Location = New System.Drawing.Point(62, 66)
        Me.cboCptClass.Name = "cboCptClass"
        Me.cboCptClass.Size = New System.Drawing.Size(162, 20)
        Me.cboCptClass.TabIndex = 14
        '
        'lblCptClass
        '
        Me.lblCptClass.AutoSize = True
        Me.lblCptClass.Location = New System.Drawing.Point(5, 69)
        Me.lblCptClass.Name = "lblCptClass"
        Me.lblCptClass.Size = New System.Drawing.Size(53, 12)
        Me.lblCptClass.TabIndex = 12
        Me.lblCptClass.Text = "构件类型"
        '
        'laInstallHeight
        '
        Me.laInstallHeight.AutoSize = True
        Me.laInstallHeight.Location = New System.Drawing.Point(5, 99)
        Me.laInstallHeight.Name = "laInstallHeight"
        Me.laInstallHeight.Size = New System.Drawing.Size(59, 12)
        Me.laInstallHeight.TabIndex = 8
        Me.laInstallHeight.Text = "高度（m）"
        '
        'btnExtractCptName
        '
        Me.btnExtractCptName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExtractCptName.Location = New System.Drawing.Point(120, 144)
        Me.btnExtractCptName.Name = "btnExtractCptName"
        Me.btnExtractCptName.Size = New System.Drawing.Size(64, 23)
        Me.btnExtractCptName.TabIndex = 8
        Me.btnExtractCptName.Text = "名称提取"
        Me.btnExtractCptName.UseVisualStyleBackColor = True
        '
        'splCptProperty
        '
        Me.splCptProperty.Location = New System.Drawing.Point(2, 3)
        Me.splCptProperty.Name = "splCptProperty"
        Me.splCptProperty.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splCptProperty.Panel1
        '
        Me.splCptProperty.Panel1.Controls.Add(Me.lblPrompt)
        Me.splCptProperty.Panel1.Controls.Add(Me.btnCollapse)
        Me.splCptProperty.Panel1.Controls.Add(Me.laName)
        Me.splCptProperty.Panel1.Controls.Add(Me.cboInstallHeight)
        Me.splCptProperty.Panel1.Controls.Add(Me.txtComponentName)
        Me.splCptProperty.Panel1.Controls.Add(Me.lblCptGroup)
        Me.splCptProperty.Panel1.Controls.Add(Me.cboCptClass)
        Me.splCptProperty.Panel1.Controls.Add(Me.cboCptGroup)
        Me.splCptProperty.Panel1.Controls.Add(Me.lblCptClass)
        Me.splCptProperty.Panel1.Controls.Add(Me.laInstallHeight)
        '
        'splCptProperty.Panel2
        '
        Me.splCptProperty.Panel2.Controls.Add(Me.prgCptProperty)
        Me.splCptProperty.Panel2Collapsed = True
        Me.splCptProperty.Size = New System.Drawing.Size(234, 360)
        Me.splCptProperty.SplitterDistance = 145
        Me.splCptProperty.SplitterWidth = 2
        Me.splCptProperty.TabIndex = 16
        '
        'lblPrompt
        '
        Me.lblPrompt.AutoSize = True
        Me.lblPrompt.Location = New System.Drawing.Point(32, 122)
        Me.lblPrompt.Name = "lblPrompt"
        Me.lblPrompt.Size = New System.Drawing.Size(53, 12)
        Me.lblPrompt.TabIndex = 19
        Me.lblPrompt.Text = "更多属性"
        '
        'btnCollapse
        '
        Me.btnCollapse.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnCollapse.Location = New System.Drawing.Point(4, 118)
        Me.btnCollapse.Name = "btnCollapse"
        Me.btnCollapse.Size = New System.Drawing.Size(24, 21)
        Me.btnCollapse.TabIndex = 17
        Me.btnCollapse.Text = "+"
        Me.btnCollapse.UseVisualStyleBackColor = True
        '
        'prgCptProperty
        '
        Me.prgCptProperty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prgCptProperty.LineColor = System.Drawing.SystemColors.ControlLight
        Me.prgCptProperty.Location = New System.Drawing.Point(0, 0)
        Me.prgCptProperty.Name = "prgCptProperty"
        Me.prgCptProperty.PropertySort = System.Windows.Forms.PropertySort.Categorized
        Me.prgCptProperty.Size = New System.Drawing.Size(150, 46)
        Me.prgCptProperty.TabIndex = 0
        Me.prgCptProperty.ToolbarVisible = False
        '
        'frmSetProperty
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(239, 172)
        Me.Controls.Add(Me.btnExtractCptName)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.splCptProperty)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetProperty"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "属性设置"
        Me.splCptProperty.Panel1.ResumeLayout(False)
        Me.splCptProperty.Panel1.PerformLayout()
        Me.splCptProperty.Panel2.ResumeLayout(False)
        CType(Me.splCptProperty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splCptProperty.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents laName As System.Windows.Forms.Label
    Friend WithEvents lblCptGroup As System.Windows.Forms.Label
    Friend WithEvents txtComponentName As System.Windows.Forms.TextBox
    Friend WithEvents cboCptGroup As System.Windows.Forms.ComboBox
    Friend WithEvents cboCptClass As System.Windows.Forms.ComboBox
    Friend WithEvents lblCptClass As System.Windows.Forms.Label
    Friend WithEvents laInstallHeight As System.Windows.Forms.Label
    Friend WithEvents splCptProperty As System.Windows.Forms.SplitContainer
    Friend WithEvents btnCollapse As System.Windows.Forms.Button
    Friend WithEvents prgCptProperty As System.Windows.Forms.PropertyGrid
    Friend WithEvents lblPrompt As System.Windows.Forms.Label
    Public WithEvents btnOK As System.Windows.Forms.Button
    Public WithEvents btnExtractCptName As System.Windows.Forms.Button
    Public WithEvents cboInstallHeight As System.Windows.Forms.ComboBox
End Class
