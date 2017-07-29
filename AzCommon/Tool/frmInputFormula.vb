Imports System.Drawing
Imports System.Text.RegularExpressions

''' <summary>
''' 简单计算式输入窗体，主要用于手动布置竖向构件时输入竖向构件高度
''' </summary>
''' <remarks></remarks>
Public Class frmInputFormula
    Private _heightFormula As String = "1.4"

    ''' <summary>
    ''' 用户输入的计算式字符串
    ''' </summary>
    Public ReadOnly Property HeightFormula As String
        Get
            Return _heightFormula
        End Get
    End Property

    Private Sub frmSetStandPipeWiringHeight_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Drawing.Point(Me.Owner.Location.X + 185, Me.Owner.Location.Y + 170)
    End Sub

    Private Sub txtHeight_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtHeight.KeyDown
        If e.KeyCode = Keys.F12 Then
            txtHeight.SelectedText = "【】"
            If txtHeight.SelectionStart > 0 Then txtHeight.SelectionStart -= 1
        End If
    End Sub

    Private Sub txtHeight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtHeight.KeyPress
        If e.KeyChar = "h" Then
            e.KeyChar = CChar("H")
        End If
    End Sub

    Private Sub txtHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHeight.TextChanged
        Dim calcFormula = txtHeight.Text.Replace("H", "3.01")
        If StrOperate.CalcSimpleStrValue3(calcFormula) Then
            lblPromote.ForeColor = Color.Blue
            Me._heightFormula = txtHeight.Text.Trim
            lblPromote.Text = "提示：高度输入支持3.0-1.5+0.3*2或H-2.5，" & vbCr & "其中H表示当前层高，按F12输入""【】"""
        Else
            Me._heightFormula = ""
            lblPromote.ForeColor = Color.Red
            lblPromote.Text = "警告：计算式输入不合法"
        End If
    End Sub

End Class