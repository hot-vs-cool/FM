
''' <summary>
''' 数值范围设置窗体
''' </summary>
''' <remarks></remarks>
Public Class frmValueRangeSet
    Private _valueType As String = "厚度"
    ''' <summary>
    ''' 指示是否允许输入小数
    ''' </summary>
    Public Property IsAllowDecimal As Boolean
    ''' <summary>
    ''' 输入的数值类型,如厚度、周长、面积
    ''' </summary>
    Public Property ValueType As String
        Get
            Return Me._valueType
        End Get
        Set(ByVal value As String)
            Me._valueType = value.Trim
            Me.lblValueType.Text = "＜" & Me._valueType & "(mm)≤"
        End Set
    End Property
    ''' <summary>
    ''' 起始数值值
    ''' </summary>
    Public Property StartValue As Double
        Set(ByVal value As Double)
            Me.txtStartThickness.Text = value.ToString
        End Set
        Get
            Dim value As Double = Nothing
            Double.TryParse(Me.txtStartThickness.Text, value)
            Return value
        End Get
    End Property
    ''' <summary>
    ''' 终止数值值
    ''' </summary>
    Public Property EndValue As String
        Get
            If IsNumeric(Me.cboEndThickness.Text) Then
                Return Me.cboEndThickness.Text.Trim
            Else
                Return "+∞"
            End If
        End Get
        Set(ByVal value As String)
            value = value.Trim
            If value = "+∞" Then
                Me.cboEndThickness.Text = "+∞"
            ElseIf IsNumeric(value) Then
                Me.cboEndThickness.Text = CDbl(value).ToString
            End If
        End Set
    End Property

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If txtStartThickness.Text = "" Then
            MsgBox("起始" & Me.ValueType & "值不能为空", , Me.ValueType & "设置")
            Exit Sub
        ElseIf Not IsNumeric(txtStartThickness.Text) Then
            MsgBox("起始" & Me.ValueType & "值输入不合法", , Me.ValueType & "设置")
            Exit Sub
        End If
        If cboEndThickness.Text = "" Then
            MsgBox("终止" & Me.ValueType & "值不能为空", , Me.ValueType & "设置")
            Exit Sub
        ElseIf cboEndThickness.Text <> "+∞" AndAlso Not IsNumeric(cboEndThickness.Text) Then
            MsgBox("终止" & Me.ValueType & "值输入不合法", , Me.ValueType & "设置")
            Exit Sub
        End If
        If cboEndThickness.Text <> "+∞" AndAlso CDbl(txtStartThickness.Text) > CDbl(cboEndThickness.Text) Then
            MsgBox("起始" & Me.ValueType & "小于终止" & Me.ValueType, , Me.ValueType & "设置")
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub txtStartThickness_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStartThickness.KeyPress, cboEndThickness.KeyPress
        If Me.IsAllowDecimal Then
            If Not (IsNumeric(e.KeyChar) OrElse e.KeyChar = CChar(".") OrElse Asc(e.KeyChar) = 8) Then
                e.KeyChar = CChar("")
            End If
        Else
            If Not (IsNumeric(e.KeyChar) OrElse Asc(e.KeyChar) = 8) Then
                e.KeyChar = CChar("")
            End If
        End If
    End Sub

End Class