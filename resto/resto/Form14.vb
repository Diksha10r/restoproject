Imports System.Data.OleDb

Public Class Form14
    Dim con As New OleDbConnection


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try

            If Me.ValidateChildren And TextBox1.Text <> String.Empty And TextBox2.Text <> String.Empty And TextBox3.Text <> String.Empty And TextBox4.Text <> String.Empty And ComboBox1.Text <> String.Empty Then
                itemsave()

            Else
                MessageBox.Show("ENTER THE ABOVE FIELDS")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


    End Sub
    Public Sub itemsave()
        Dim id, price As Integer
        Dim nm, mtype, des As String

        id = CInt(TextBox1.Text)
        nm = TextBox1.Text
        mtype = ComboBox1.Text
        price = CInt(TextBox3.Text)
        des = TextBox4.Text

        con.Open()
        Dim query As String
        query = "Insert into ITEMS ([ITEM_ID],ITEM_NAME,MENU_TYPE,[PRICE],DESCRIPTION) values (?,?,?,?,?)"
        Dim cmd As New OleDbCommand(query, con)
        cmd.Parameters.AddWithValue("ITEM_ID", id)
        cmd.Parameters.AddWithValue("ITEM_NAME", nm)
        cmd.Parameters.AddWithValue("MENU_TYPE", mtype)
        cmd.Parameters.AddWithValue("PRICE", price)
        cmd.Parameters.AddWithValue("DESCRIPTION", des)

        Dim i As Integer
        i = cmd.ExecuteNonQuery()
        If (i > 0) Then
            MessageBox.Show("Record Saved successfully")
            Form13.Show()
            Form13.dgshow()
            Me.Hide()

        Else
            MessageBox.Show("Record not saved ")
        End If
        con.Close()
    End Sub

    Private Sub Form14_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\solan\\Desktop\\restaurant project\\resto.mdb"
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try

            If Me.ValidateChildren And TextBox1.Text <> String.Empty And TextBox2.Text <> String.Empty And TextBox3.Text <> String.Empty And TextBox4.Text <> String.Empty And ComboBox1.Text <> String.Empty Then
                itemupdate()

            Else
                MessageBox.Show("ENTER THE ABOVE FIELDS")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try



    End Sub
    Public Sub itemupdate()
        Dim id As Integer = CInt(TextBox1.Text)
        con.Open()
        Dim query3 As String

        query3 = "Update ITEMS Set ITEM_NAME='" & TextBox2.Text & "', MENU_TYPE='" & ComboBox1.Text & "', PRICE=" & TextBox3.Text & ", DESCRIPTION='" & TextBox4.Text & "' WHERE ITEM_ID=" & id & " "

        Dim cmd As New OleDbCommand(query3, con)
        Dim i As Integer
        i = cmd.ExecuteNonQuery()
        If (i > 0) Then
            MessageBox.Show("Record Updated")
            Form13.Show()
            Form13.dgshow()
            Me.Hide()
        Else
            MessageBox.Show("Record not updated")

        End If
        con.Close()

    End Sub
    Public Sub addclear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        ComboBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""

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
            Me.ErrorProvider1.SetError(sender, "ITEM NAME")
            TextBox2.Text = ""
        End If
    End Sub

    

    Private Sub ComboBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ComboBox1.Validating
        If DirectCast(sender, ComboBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "Select Menu Type")
            ComboBox1.Text = ""
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then
            e.Handled = True

        End If
    End Sub

    Private Sub TextBox3_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox3.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "ENTER PRICE")
            TextBox3.Text = ""
        End If
    End Sub

    

    

    Private Sub TextBox4_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox4.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "ENTER DESCRIPTION")
            TextBox4.Text = ""
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()

    End Sub


    Private Sub TextBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")
        Else
            Me.ErrorProvider1.SetError(sender, "YOU HAVE NOT SELECTED RECORD FROM DATAGRID ")
        End If
    End Sub
End Class