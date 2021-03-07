Imports System.Data.OleDb

Public Class Form16
    Dim con As New OleDbConnection

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            If Me.ValidateChildren And TextBox2.Text <> String.Empty And TextBox3.Text <> String.Empty Then
                feedbacksave()
            Else
                MessageBox.Show("ENTER THE ABOVE FIELDS")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try




    End Sub
    Public Sub feedbacksave()
        con.Open()
        Dim fre, por, fla, pre, md As String
        Dim ffn, cid, bno As Integer
        Dim fdate As String

        ffn = CInt(TextBox1.Text)
        fdate = DateTimePicker1.Value.Date
        cid = CInt(TextBox2.Text)
        bno = CInt(TextBox3.Text)

        If RadioButton1.Checked = True Then
            fre = RadioButton1.Text
        ElseIf RadioButton2.Checked = True Then
            fre = RadioButton2.Text
        ElseIf RadioButton3.Checked = True Then
            fre = RadioButton3.Text
        Else
            fre = RadioButton4.Text

        End If

        If RadioButton5.Checked = True Then
            por = RadioButton5.Text
        ElseIf RadioButton6.Checked = True Then
            por = RadioButton6.Text
        ElseIf RadioButton7.Checked = True Then
            por = RadioButton7.Text
        Else
            por = RadioButton8.Text
        End If

        If RadioButton9.Checked = True Then
            fla = RadioButton9.Text
        ElseIf RadioButton10.Checked = True Then
            fla = RadioButton10.Text
        ElseIf RadioButton11.Checked = True Then
            fla = RadioButton11.Text
        Else
            fla = RadioButton12.Text

        End If

        If RadioButton13.Checked = True Then
            pre = RadioButton13.Text
        ElseIf RadioButton14.Checked = True Then
            pre = RadioButton14.Text
        ElseIf RadioButton15.Checked = True Then
            pre = RadioButton15.Text
        Else
            pre = RadioButton16.Text

        End If

        If RadioButton17.Checked = True Then
            md = RadioButton17.Text
        ElseIf RadioButton18.Checked = True Then
            md = RadioButton18.Text
        ElseIf RadioButton19.Checked = True Then
            md = RadioButton19.Text
        Else
            md = RadioButton20.Text

        End If


        Dim query As String
        query = "Insert into FEEDBACK([FEEDBACK_FORM_NO],FEEDBACK_DATE,[CUSTOMER_ID],[BILL_ID],FRESHNESS,PORTION_SIZE,FLAVOUR,PRESENTATION,MENU_IS_DIVERSE) values(?,?,?,?,?,?,?,?,?)"

        Dim cmd As New OleDbCommand(query, con)
        cmd.Parameters.AddWithValue("FEEDBACK_FORM_NO", ffn)
        cmd.Parameters.AddWithValue("FEEDBACK_DATE", fdate)
        cmd.Parameters.AddWithValue("CUSTOMER_ID", cid)
        cmd.Parameters.AddWithValue("BILL_ID", bno)
        cmd.Parameters.AddWithValue("FRESHNESS", fre)
        cmd.Parameters.AddWithValue("PORTION_SIZE", por)
        cmd.Parameters.AddWithValue("FLAVOUR", fla)
        cmd.Parameters.AddWithValue("PRESENTATION", pre)
        cmd.Parameters.AddWithValue("MENU_IS_DIVERSE", md)
        Dim i As Integer
        i = cmd.ExecuteNonQuery()
        If (i > 0) Then
            MessageBox.Show("FEEDBACK TAKEN")
        Else
            MessageBox.Show("FEEDBACK NOT TAKEN")
        End If
    End Sub

    Private Sub Form16_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\\solan\\Desktop\\restaurant project\\resto.mdb"
        con.Open()

        Dim cmd As New OleDbCommand("Select * from FEEDBACK", con)
        cmd.Connection = con
        Dim oid As Object
        Dim strid As String
        Dim oidd As Integer
        cmd.CommandText = "Select max(FEEDBACK_FORM_NO) as oid from FEEDBACK"
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

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.MaxDate = Date.Now
        DateTimePicker1.MinDate = Date.Now

    End Sub

    

    Private Sub TextBox2_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox2.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "CUSTOMER ID")
            TextBox2.Text = ""
        End If
    End Sub

   

    Private Sub TextBox3_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox3.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "BILL ID")
            TextBox3.Text = ""
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()

    End Sub
End Class