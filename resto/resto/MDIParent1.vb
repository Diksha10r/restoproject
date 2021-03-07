Imports System.Windows.Forms

Public Class MDIParent1

   Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Do While Panel1.Width < 170
            Panel1.Width = Panel1.Width + 1
        Loop
        PictureBox1.Visible = False
        Me.Size = New System.Drawing.Size(952, 550)



       

    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Do While Panel1.Width > 36
            Panel1.Width = Panel1.Width - 1
        Loop
        PictureBox1.Visible = True
        Me.Size = New System.Drawing.Size(850, 550)

    End Sub

    

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Form2.Show()
        Form2.MdiParent = Me

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form5.Show()
        Form5.MdiParent = Me
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form9.Show()
        Form9.MdiParent = Me
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Form13.Show()
        Form13.MdiParent = Me
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Form12.Show()
        Form12.MdiParent = Me
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Form16.Show()
        Form16.MdiParent = Me
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Form8.Show()
        Form8.MdiParent = Me


    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Form17.Show()
        Form17.MdiParent = Me

    End Sub

   

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Form19.Show()
        Form19.MdiParent = Me

    End Sub
End Class
