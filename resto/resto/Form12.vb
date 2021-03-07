Imports System.Data.OleDb
Imports System.Text.RegularExpressions

Public Class Form12
    Dim con As New OleDbConnection

    Private Sub Form12_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\\solan\\Desktop\\restaurant project\\resto.mdb"
        con.Open()


        Dim cmd As New OleDbCommand("Select * from EMPLOYEE", con)
        cmd.Connection = con
        Dim oid As Object
        Dim strid As String
        Dim oidd As Integer
        cmd.CommandText = "Select max(EMPLOYEE_ID) as oid from EMPLOYEE"
        oid = cmd.ExecuteScalar
        If (oid Is DBNull.Value) Then
            oidd = 1
        Else
            strid = CType(oid, String)
            oidd = CType(strid, String)
            oidd = oidd + 1
        End If
        TextBox1.Text = oidd

        con.Close()


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            If Me.ValidateChildren And TextBox2.Text <> String.Empty And ComboBox1.Text <> String.Empty And TextBox3.Text <> String.Empty And TextBox4.Text <> String.Empty And RadioButton1.Text <> String.Empty And RadioButton2.Text <> String.Empty Then
                empsave()
            Else
                MessageBox.Show("ENTER ABOVE FIELDS")
            End If


        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub
    Public Sub empsave()
        Dim eid, i As Integer
        Dim ename, egen, eaddr, edob, edoj, ejob As String
        Dim econ As Long

        eid = CInt(TextBox1.Text)
        edob = DateTimePicker1.Value.Date
        ename = TextBox2.Text
        If RadioButton1.Checked = True Then
            egen = RadioButton1.Text

        Else
            egen = RadioButton2.Text

        End If
        eaddr = TextBox3.Text
        edoj = DateTimePicker2.Value
        ejob = ComboBox1.Text
        econ = CLng(TextBox4.Text)

        con.Open()
        Dim query As String
        query = "Insert into EMPLOYEE ([EMPLOYEE_ID],EMPLOYEE_DATE_OF_BIRTH,EMPLOYEE_GENDER,EMPLOYEE_NAME,EMPLOYEE_ADDRESS,EMPLOYEE_DATE_OF_JOINING,EMPLOYEE_JOB_TYPE,[EMPLOYEE_CONTACT]) values(?,?,?,?,?,?,?,?)"

        Dim cmd As New OleDbCommand(query, con)
        cmd.Parameters.AddWithValue("EMPLOYEE_ID", eid)
        cmd.Parameters.AddWithValue("EMPLOYEE_DATE_OF_BIRTH", edob)
        cmd.Parameters.AddWithValue("EMPLOYEE_GENDER", egen)
        cmd.Parameters.AddWithValue("EMPLOYEE_NAME", ename)
        cmd.Parameters.AddWithValue("EMPLOYEE_ADDRESS", eaddr)
        cmd.Parameters.AddWithValue("EMPLOYE_DATE_OF_JOINING", edoj)
        cmd.Parameters.AddWithValue("EMPLOYEE_JOB_TYPE", ejob)
        cmd.Parameters.AddWithValue("EMPLOYEE_CONTACT", econ)

        i = cmd.ExecuteNonQuery()
        If (i > 0) Then
            MessageBox.Show("Record saved successfully")
        Else
            MessageBox.Show("Record not saved")
        End If
        con.Close()
        Me.Hide()




    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        DateTimePicker2.MinDate = Date.Now
       


    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged






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
            Me.ErrorProvider1.SetError(sender, "ENTER EMPLOYEE NAME")
            TextBox2.Text = ""

        End If
    End Sub

  

    Private Sub ComboBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ComboBox1.Validating
        If DirectCast(sender, ComboBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")
        Else
            Me.ErrorProvider1.SetError(sender, "ENTER JOB TYPE")
            ComboBox1.Text = ""

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

   

  

    
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        con.Close()
        Me.Hide()
    End Sub

 

    
End Class