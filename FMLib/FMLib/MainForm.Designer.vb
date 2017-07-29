<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.CadObj = New AxMxDrawXLib.AxMxDrawX()
        Me.btnDraw = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        CType(Me.CadObj, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CadObj
        '
        Me.CadObj.Enabled = True
        Me.CadObj.Location = New System.Drawing.Point(1, 1)
        Me.CadObj.Name = "CadObj"
        Me.CadObj.OcxState = CType(resources.GetObject("CadObj.OcxState"), System.Windows.Forms.AxHost.State)
        Me.CadObj.Size = New System.Drawing.Size(1046, 808)
        Me.CadObj.TabIndex = 0
        '
        'btnDraw
        '
        Me.btnDraw.Location = New System.Drawing.Point(1053, 12)
        Me.btnDraw.Name = "btnDraw"
        Me.btnDraw.Size = New System.Drawing.Size(75, 23)
        Me.btnDraw.TabIndex = 1
        Me.btnDraw.Text = "绘制"
        Me.btnDraw.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(924, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(75, 23)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "返回"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1170, 810)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnDraw)
        Me.Controls.Add(Me.CadObj)
        Me.Name = "MainForm"
        Me.Text = "MainForm"
        CType(Me.CadObj, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CadObj As AxMxDrawXLib.AxMxDrawX
    Friend WithEvents btnDraw As Button
    Friend WithEvents btnBack As Button
End Class
