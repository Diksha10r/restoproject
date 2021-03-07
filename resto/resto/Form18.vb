Public Class Form18

    Private Sub Form18_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CrystalReportViewer1.ReportSource = Nothing

        Form9.Show()

    End Sub

    Private Sub Form18_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave






    End Sub


    Private Sub Form18_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class