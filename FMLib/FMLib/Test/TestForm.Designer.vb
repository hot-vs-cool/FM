<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestForm))
        Me.bntSeletData = New System.Windows.Forms.Button()
        Me.btnSeletType = New System.Windows.Forms.Button()
        Me.mxCadObj = New AxMxDrawXLib.AxMxDrawX()
        Me.btnSeletLayer = New System.Windows.Forms.Button()
        Me.btnLookData = New System.Windows.Forms.Button()
        Me.btnOpenDWG = New System.Windows.Forms.Button()
        Me.btnLookBlockAttri = New System.Windows.Forms.Button()
        Me.btnRecoAxis = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnInscet = New System.Windows.Forms.Button()
        Me.GoMainForm = New System.Windows.Forms.Button()
        Me.btnVorter = New System.Windows.Forms.Button()
        Me.btnSetLayer = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.mxCadObj, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'bntSeletData
        '
        Me.bntSeletData.Location = New System.Drawing.Point(1014, 12)
        Me.bntSeletData.Name = "bntSeletData"
        Me.bntSeletData.Size = New System.Drawing.Size(75, 23)
        Me.bntSeletData.TabIndex = 1
        Me.bntSeletData.Text = "选择数据"
        Me.bntSeletData.UseVisualStyleBackColor = True
        '
        'btnSeletType
        '
        Me.btnSeletType.Location = New System.Drawing.Point(1014, 52)
        Me.btnSeletType.Name = "btnSeletType"
        Me.btnSeletType.Size = New System.Drawing.Size(75, 23)
        Me.btnSeletType.TabIndex = 2
        Me.btnSeletType.Text = "查看类型"
        Me.btnSeletType.UseVisualStyleBackColor = True
        '
        'mxCadObj
        '
        Me.mxCadObj.Enabled = True
        Me.mxCadObj.Location = New System.Drawing.Point(0, 4)
        Me.mxCadObj.Name = "mxCadObj"
        Me.mxCadObj.OcxState = CType(resources.GetObject("mxCadObj.OcxState"), System.Windows.Forms.AxHost.State)
        Me.mxCadObj.Size = New System.Drawing.Size(1006, 513)
        Me.mxCadObj.TabIndex = 0
        '
        'btnSeletLayer
        '
        Me.btnSeletLayer.Location = New System.Drawing.Point(1014, 94)
        Me.btnSeletLayer.Name = "btnSeletLayer"
        Me.btnSeletLayer.Size = New System.Drawing.Size(75, 23)
        Me.btnSeletLayer.TabIndex = 3
        Me.btnSeletLayer.Text = "查看图层"
        Me.btnSeletLayer.UseVisualStyleBackColor = True
        '
        'btnLookData
        '
        Me.btnLookData.Location = New System.Drawing.Point(1014, 137)
        Me.btnLookData.Name = "btnLookData"
        Me.btnLookData.Size = New System.Drawing.Size(97, 23)
        Me.btnLookData.TabIndex = 4
        Me.btnLookData.Text = "查看所有数据"
        Me.btnLookData.UseVisualStyleBackColor = True
        '
        'btnOpenDWG
        '
        Me.btnOpenDWG.Location = New System.Drawing.Point(1014, 175)
        Me.btnOpenDWG.Name = "btnOpenDWG"
        Me.btnOpenDWG.Size = New System.Drawing.Size(97, 23)
        Me.btnOpenDWG.TabIndex = 5
        Me.btnOpenDWG.Text = "打开CAD图纸"
        Me.btnOpenDWG.UseVisualStyleBackColor = True
        '
        'btnLookBlockAttri
        '
        Me.btnLookBlockAttri.Location = New System.Drawing.Point(1117, 12)
        Me.btnLookBlockAttri.Name = "btnLookBlockAttri"
        Me.btnLookBlockAttri.Size = New System.Drawing.Size(92, 23)
        Me.btnLookBlockAttri.TabIndex = 6
        Me.btnLookBlockAttri.Text = "是否有块属性"
        Me.btnLookBlockAttri.UseVisualStyleBackColor = True
        '
        'btnRecoAxis
        '
        Me.btnRecoAxis.Location = New System.Drawing.Point(1117, 52)
        Me.btnRecoAxis.Name = "btnRecoAxis"
        Me.btnRecoAxis.Size = New System.Drawing.Size(92, 23)
        Me.btnRecoAxis.TabIndex = 7
        Me.btnRecoAxis.Text = "识别轴网"
        Me.btnRecoAxis.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1117, 94)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(92, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "识别轴网"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnInscet
        '
        Me.btnInscet.Location = New System.Drawing.Point(1120, 137)
        Me.btnInscet.Name = "btnInscet"
        Me.btnInscet.Size = New System.Drawing.Size(92, 23)
        Me.btnInscet.TabIndex = 9
        Me.btnInscet.Text = "线段相交测试"
        Me.btnInscet.UseVisualStyleBackColor = True
        '
        'GoMainForm
        '
        Me.GoMainForm.Location = New System.Drawing.Point(1012, 218)
        Me.GoMainForm.Name = "GoMainForm"
        Me.GoMainForm.Size = New System.Drawing.Size(92, 23)
        Me.GoMainForm.TabIndex = 10
        Me.GoMainForm.Text = "到主页面"
        Me.GoMainForm.UseVisualStyleBackColor = True
        '
        'btnVorter
        '
        Me.btnVorter.Location = New System.Drawing.Point(1120, 175)
        Me.btnVorter.Name = "btnVorter"
        Me.btnVorter.Size = New System.Drawing.Size(92, 23)
        Me.btnVorter.TabIndex = 11
        Me.btnVorter.Text = "向量操作"
        Me.btnVorter.UseVisualStyleBackColor = True
        '
        'btnSetLayer
        '
        Me.btnSetLayer.Location = New System.Drawing.Point(1110, 218)
        Me.btnSetLayer.Name = "btnSetLayer"
        Me.btnSetLayer.Size = New System.Drawing.Size(102, 23)
        Me.btnSetLayer.TabIndex = 12
        Me.btnSetLayer.Text = "设置实体的图层"
        Me.btnSetLayer.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1012, 266)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(92, 23)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "绘制字体"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TestForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1220, 515)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnSetLayer)
        Me.Controls.Add(Me.btnVorter)
        Me.Controls.Add(Me.GoMainForm)
        Me.Controls.Add(Me.btnInscet)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnRecoAxis)
        Me.Controls.Add(Me.btnLookBlockAttri)
        Me.Controls.Add(Me.btnOpenDWG)
        Me.Controls.Add(Me.btnLookData)
        Me.Controls.Add(Me.btnSeletLayer)
        Me.Controls.Add(Me.btnSeletType)
        Me.Controls.Add(Me.bntSeletData)
        Me.Controls.Add(Me.mxCadObj)
        Me.Name = "TestForm"
        Me.Text = "Form1"
        CType(Me.mxCadObj, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents mxCadObj As AxMxDrawXLib.AxMxDrawX
    Friend WithEvents bntSeletData As Button
    Friend WithEvents btnSeletType As Button
    Friend WithEvents btnSeletLayer As System.Windows.Forms.Button
    Friend WithEvents btnLookData As Button
    Friend WithEvents btnOpenDWG As Button
    Friend WithEvents btnLookBlockAttri As Button
    Friend WithEvents btnRecoAxis As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btnInscet As Button
    Friend WithEvents GoMainForm As Button
    Friend WithEvents btnVorter As Button
    Friend WithEvents btnSetLayer As Button
    Friend WithEvents Button2 As Button
End Class
