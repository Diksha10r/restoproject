Public Class Form8

    Private Sub TreeView1_AfterSelect_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If e.Node.Name = "Node0" Then
            RichTextBox1.LoadFile("C:\Users\solan\Desktop\restaurant project\login.rtf")
        End If
        If e.Node.Name = "Node1" Then
            RichTextBox1.LoadFile("C:\Users\solan\Desktop\restaurant project\main.rtf")
        End If
        If e.Node.Name = "Node2" Then
            RichTextBox1.LoadFile("C:\Users\solan\Desktop\restaurant project\customer.rtf")
        End If

        If e.Node.Name = "Node3" Then
            RichTextBox1.LoadFile("C:\Users\solan\Desktop\restaurant project\reservation.rtf")
        End If


    End Sub
   


End Class