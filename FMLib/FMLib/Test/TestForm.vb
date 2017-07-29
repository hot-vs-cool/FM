Imports MxDrawXLib
Imports CxAzFunc
Public Class TestForm

    Dim controlName As String

    Dim mainForm As New MainForm

    Public cxAxisHanle As CXAxisHandle
    Private Sub TestForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        mainForm.Show()
        mainForm.Visible = False
    End Sub


    Private Sub mxCadObj_ImplementCommandEvent(sender As Object, e As AxMxDrawXLib._DMxDrawXEvents_ImplementCommandEventEvent) Handles mxCadObj.ImplementCommandEvent
        Dim pTestClass As New TestClass
        If e.iCommandId = 1 Then
            pTestClass.Execute2(Me, controlName)
        ElseIf e.iCommandId = 2 Then
            pTestClass.Execute1(Me, controlName)
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles bntSeletData.Click
        SendDoCommond(sender)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnSeletType.Click
        SendDoCommond(sender)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeletLayer.Click
        SendDoCommond(sender)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnLookData.Click
        SendDoCommond(sender)
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnOpenDWG.Click
        SendDoCommond(sender)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles btnLookBlockAttri.Click
        SendDoCommond(sender)
    End Sub

    Private Sub SendDoCommond(ByVal sender As Object, Optional ByVal id As Short = 1)
        If TypeOf sender Is Control Then
            Dim pControl As Control = CType(sender, Control)
            Me.mxCadObj.DoCommand(id)
            controlName = pControl.Name
        End If

    End Sub

    Private Sub btnRecoAxis_Click(sender As Object, e As EventArgs) Handles btnRecoAxis.Click
        SendDoCommond(sender)
    End Sub



    Private Sub btnInscet_Click(sender As Object, e As EventArgs) Handles btnInscet.Click
        SendDoCommond(sender, 2)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        SendDoCommond(sender, 2)
    End Sub

    Private Sub GoMainForm_Click(sender As Object, e As EventArgs) Handles GoMainForm.Click
        If cxAxisHanle Is Nothing Then Return
        mainForm.Visible = True
        mainForm.AddAxisHandle(cxAxisHanle)
        Me.Visible = False
    End Sub

    Private Sub btnVorter_Click(sender As Object, e As EventArgs) Handles btnVorter.Click
        SendDoCommond(sender, 2)
    End Sub

    Private Sub btnSetLayer_Click(sender As Object, e As EventArgs) Handles btnSetLayer.Click
        SendDoCommond(sender, 2)
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        SendDoCommond(sender, 2)
    End Sub
End Class
