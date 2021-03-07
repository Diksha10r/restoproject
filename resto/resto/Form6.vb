Imports System.Data.OleDb
Imports System.Text.RegularExpressions

Public Class Form6
    Dim con As New OleDbConnection


    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\solan\\Desktop\\restaurant project\\resto.mdb"



    End Sub

    Public Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click
        Form4.Show()
       


    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            If Me.ValidateChildren And TextBox2.Text <> String.Empty And ComboBox1.Text <> String.Empty And NumericUpDown1.Text <> String.Empty Then
                reservesave()
            Else
                MessageBox.Show("ENTER ABOVE FIELDS")
            End If


        Catch ex As Exception
            MsgBox(ex.Message)

        End Try





    End Sub

 
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try

            If Me.ValidateChildren And TextBox1.Text <> String.Empty And TextBox2.Text <> String.Empty And ComboBox1.Text <> String.Empty And NumericUpDown1.Text <> String.Empty Then
                reserveupdate()
            Else
                MessageBox.Show("ENTER ABOVE FIELDS")
            End If


        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
      
    End Sub

    Public Sub addclear()
        TextBox1.Text = ""
        TextBox2.Text = ""
       
        ComboBox1.Text = ""


    End Sub

    

 
   

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.MinDate = Date.Today
        DateTimePicker1.MaxDate = Date.Today


    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        DateTimePicker2.MinDate = System.DateTime.Now()

    End Sub

    Private Sub NumericUpDown1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles NumericUpDown1.Validating

    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        NumericUpDown1.Maximum = "8"
        NumericUpDown1.Minimum = "1"



    End Sub

    Public Sub reservesave()
        Dim rid, cid, nop, i As Integer
        Dim enm As String
        Dim rdate As String
        Dim rtime As String


        rid = CInt(TextBox1.Text)
        cid = TextBox2.Text
        rdate = DateTimePicker1.Value.Date
        rtime = DateTimePicker2.Value.Hour


        enm = ComboBox1.Text
        nop = NumericUpDown1.Value

        con.Open()
        Dim query As String
        query = "Insert into RESERVATION([RESERVATION_ID],[CUSTOMER_ID],RESERVATION_DATE,RESERVATION_TIME,EVENT_TYPE,[NUMBER_OF_PEOPLE]) values(?,?,?,?,?,?)"

        Dim cmd As New OleDbCommand(query, con)
        cmd.Parameters.AddWithValue("RESERVATION_ID", rid)
        cmd.Parameters.AddWithValue("CUSTOMER_ID", cid)
        cmd.Parameters.AddWithValue("RESERVATION_DATE", rdate)
        cmd.Parameters.AddWithValue("RESERVATION_TIME", rtime)

        cmd.Parameters.AddWithValue("EVENT_TYPE", enm)
        cmd.Parameters.AddWithValue("NUMBER_OF_PEOPLE", nop)

        i = cmd.ExecuteNonQuery()
        If (i > 0) Then
            MessageBox.Show("Record saved successfully")
            Form5.DataGridView1.DataSource = Nothing
            Form5.dgshow()
            Form5.Show()
            Me.Hide()



        Else
            MessageBox.Show("NOT SAVED ")
            


        End If
        con.Close()

    End Sub

    Public Sub reserveupdate()
        Dim id As Integer = CInt(TextBox1.Text)
        Dim resdate As String = DateTimePicker1.Value.Date
        Dim restime As String = DateTimePicker2.Value.Hour
        con.Open()

        Dim query3 As String

        query3 = "Update RESERVATION Set CUSTOMER_ID=" & TextBox2.Text & ",RESERVATION_DATE='" & resdate & "', RESERVATION_TIME= '" & restime & "', EVENT_TYPE='" & ComboBox1.Text & "', NUMBER_OF_PEOPLE=" & NumericUpDown1.Value & " WHERE RESERVATION_ID=" & id & " "

        Dim cmd As New OleDbCommand(query3, con)
        Dim i As Integer
        i = cmd.ExecuteNonQuery()
        If (i > 0) Then
            MessageBox.Show("record updated")
            Form5.DataGridView1.DataSource = Nothing
            Form5.dgshow()
            Form5.Show()
            Me.Hide()

        Else
            MessageBox.Show("record not updated")
        End If


        con.Close()
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then
            e.Handled = True

        End If
    End Sub

   


    Private Sub TextBox2_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox2.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 And DirectCast(sender, TextBox).Text.Length <= 2 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "ENTER CUSTOMER NO")
            TextBox2.Text = ""
        End If
    End Sub

    

    Private Sub ComboBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ComboBox1.Validating
        If DirectCast(sender, ComboBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")
        Else
            Me.ErrorProvider1.SetError(sender, "ENTER OR SELECT EVENT TYPE")
        End If

    End Sub



  

    Private Sub TextBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")
        Else
            Me.ErrorProvider1.SetError(sender, "you have not selected record from datagrid")
        End If
    End Sub
End Class