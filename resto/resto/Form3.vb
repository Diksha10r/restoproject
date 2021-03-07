Imports System.Data.OleDb
Imports System.Text.RegularExpressions



Public Class Form3
    Dim con As New OleDbConnection

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\solan\\Desktop\\restaurant project\\resto.mdb"



    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        
        Try

            If Me.ValidateChildren And TextBox2.Text <> String.Empty And TextBox3.Text <> String.Empty And TextBox4.Text <> String.Empty And TextBox5.Text <> String.Empty Then
                custsave()
            Else
                MessageBox.Show("ENTER THE ABOVE FIELDS")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try












    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try

            If Me.ValidateChildren And TextBox1.Text <> String.Empty And TextBox2.Text <> String.Empty And TextBox3.Text <> String.Empty And TextBox4.Text <> String.Empty And TextBox5.Text <> String.Empty Then
                custupdate()
            Else
                MessageBox.Show("ENTER ABOVE FIELDS")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then
            e.Handled = True

        End If
    End Sub

  




    Private Sub TextBox2_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox2.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "ENTER NAME")
            TextBox2.Text = ""
        End If

    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then
            e.Handled = True

        End If
    End Sub

   

    Private Sub TextBox3_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox3.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "ENTER ADDRESS")
            TextBox3.Text = ""
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then
            e.Handled = True

        End If
    End Sub

    

    Private Sub TextBox4_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox4.Validating
        If DirectCast(sender, TextBox).Text.Length < 10 Then
            Me.ErrorProvider1.SetError(sender, "ENTER VALID PHONE NUMBER OF 10 DIGITS")
            TextBox4.Text = ""

        Else
            Me.ErrorProvider1.SetError(sender, "")

        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        Dim ac As String = "@"
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Asc(e.KeyChar) < 97 Or Asc(e.KeyChar) > 122 Then
                If Asc(e.KeyChar) < 46 Or Asc(e.KeyChar) > 95 Then
                    If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                        If ac.IndexOf(e.KeyChar) = -1 Then
                            e.Handled = True
                        Else
                            If (TextBox5.Text.Contains("@") And e.KeyChar = "@") Then
                                e.Handled = True
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub



    Private Sub TextBox5_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox5.Validating
        Dim pattern As String = "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$"
        Dim match As System.Text.RegularExpressions.Match = Regex.Match(TextBox5.Text.Trim(), pattern, RegexOptions.IgnoreCase)
        If (match.Success) Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "PLEASE ENTER VALID EMAIL ID")
            TextBox5.Text = ""

        End If


    End Sub

    Public Sub custsave()

        Dim id, i As Integer
        Dim nm, addr, em, query As String
        Dim cn As Long


        id = CInt(TextBox1.Text)
        nm = TextBox2.Text
        addr = TextBox3.Text
        cn = CLng(TextBox4.Text)
        em = TextBox5.Text

        con.Open()
        query = "Insert into CUSTOMER([CUSTOMER_ID],CUSTOMER_NAME,CUSTOMER_ADDRESS,CUSTOMER_CONTACT,CUSTOMER_EMAIL) values(?,?,?,?,?)"

        Dim cmd As New OleDbCommand(query, con)
        cmd.Parameters.AddWithValue("CUSTOMER_ID", id)
        cmd.Parameters.AddWithValue("CUSTOMER_NAME", nm)
        cmd.Parameters.AddWithValue("CUSTOMER_ADDRESS", addr)
        cmd.Parameters.AddWithValue("CUSTOMER_CONTACT", cn)
        cmd.Parameters.AddWithValue("CUSTOMER_EMAIL", em)

        i = cmd.ExecuteNonQuery()
        If (i > 0) Then
            MessageBox.Show("Record saved successfully")
            Form2.Show()
            Form2.dgshow()
            Me.Hide()



        Else
            MessageBox.Show("Record not saved")

        End If
        con.Close()

    End Sub

    Public Sub custupdate()
        Dim id As Integer = CInt(TextBox1.Text)
        con.Open()

        Dim query3 As String

        query3 = "Update CUSTOMER Set CUSTOMER_NAME='" & TextBox2.Text & "', CUSTOMER_ADDRESS='" & TextBox3.Text & "', CUSTOMER_CONTACT=" & TextBox4.Text & ", CUSTOMER_EMAIL='" & TextBox5.Text & "' WHERE CUSTOMER_ID=" & id & ""

        Dim cmd As New OleDbCommand(query3, con)
        Dim i As Integer
        i = cmd.ExecuteNonQuery()
        If (i > 0) Then
            MessageBox.Show("Record Updated")
            Form2.Show()
            Form2.dgshow()
            Me.Hide()
        Else
            MessageBox.Show("Record Not Updated")
        End If


        con.Close()
    End Sub

    Public Sub addclear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""

    End Sub

    
    
    

    Private Sub TextBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")
        Else
            Me.ErrorProvider1.SetError(sender, "YOU HAVE NOT SELECTED RECORD FROM DATAGRID")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        Form2.Show()

    End Sub

    
End Class