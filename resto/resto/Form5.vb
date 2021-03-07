Imports System.Data.OleDb
Imports System.Data.DataTable
Imports System.Globalization
Imports System.IO

Public Class Form5
    Dim con As New OleDbConnection
    Dim index As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        con.Open()
        Dim cmd As New OleDbCommand("Select * from RESERVATION", con)
        cmd.Connection = con
        Dim oid As Object
        Dim strid As String
        Dim oidd As Integer
        cmd.CommandText = "Select max(RESERVATION_ID) as oid from RESERVATION"
        oid = cmd.ExecuteScalar
        If (oid Is DBNull.Value) Then
            oidd = 1
        Else
            strid = CType(oid, String)
            oidd = CType(strid, String)
            oidd = oidd + 1
        End If
        con.Close()



        Form6.Show()
        Form6.addclear()
        Form6.TextBox1.Text = oidd
        Form6.Button3.Visible = False
        Form6.Button1.Visible = True

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Try

            Me.Hide()

            Form6.Show()
            Form6.Button1.Visible = False
            Form6.Button3.Visible = True
            Dim srow As DataGridViewRow
            srow = DataGridView1.Rows(index)
            srow.Cells(0).Value = Form6.TextBox1.Text
            srow.Cells(1).Value = Form6.TextBox2.Text
            srow.Cells(2).Value = Form6.DateTimePicker1.Value.Date
            srow.Cells(3).Value = Form6.DateTimePicker2.Value.Hour
            srow.Cells(4).Value = Form6.ComboBox1.Text
            srow.Cells(5).Value = Form6.NumericUpDown1.Value
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form7.Show()


    End Sub

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.RESERVATIONTableAdapter.Fill(Me.RestoDataSet.RESERVATION)
        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\solan\\Desktop\\restaurant project\\resto.mdb"

        con.Open()


        dgshow()

        Dim dte As String = ""
        Dim dtfinfo As DateTimeFormatInfo
        Dim dtstyle As String = "ddd, dd MMMM yyyy"
        dtfinfo = DateTimeFormatInfo.InvariantInfo
        dte = DateTime.Now.ToString(dtstyle, dtfinfo)
        Label6.Text = dte

        If Me.Enabled Then
            Timer1.Start()
            Label6.Visible = True
            Label7.Visible = True
        Else
            Timer1.Stop()
            Label6.Visible = False
            Label7.Visible = False

        End If



    End Sub

    Public Sub dgshow()
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds.Tables.Add(dt)


        Dim da As New OleDbDataAdapter

        da = New OleDbDataAdapter("Select * from RESERVATION", con)
        da.Fill(dt)
        DataGridView1.DataSource = dt.DefaultView


        con.Close()



    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try

        
            Dim srow1 As DataGridViewRow
            srow1 = DataGridView1.Rows(index)
            Dim res_id As Integer = srow1.Cells(0).Value.ToString
            con.Open()

            Dim query4 As String
            query4 = "Delete from RESERVATION WHERE RESERVATION_ID=" & res_id & " "
            Dim cmd As New OleDbCommand(query4, con)
            Dim i As Integer
            i = cmd.ExecuteNonQuery()
            If (i > 0) Then
                MessageBox.Show("Record Deleted")
            Else
                MessageBox.Show("NOT DELETED")
            End If
            con.Close()
            dgshow()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick, DataGridView1.CellClick
        Try

            index = e.RowIndex
            Dim selectedrow As DataGridViewRow
            selectedrow = Me.DataGridView1.Rows(index)
            TextBox1.Text = selectedrow.Cells(0).Value.ToString
            TextBox2.Text = selectedrow.Cells(1).Value.ToString

            Form6.TextBox1.Text = selectedrow.Cells(0).Value.ToString
            Form6.TextBox2.Text = selectedrow.Cells(1).Value.ToString

            Form6.ComboBox1.Text = selectedrow.Cells(4).Value.ToString
            Form6.NumericUpDown1.Value = selectedrow.Cells(5).Value.ToString
        Catch ex As Exception
            MessageBox.Show("Please select valid Record")
        End Try

    End Sub


    Private Function myzero(ByVal value As Integer) As String
        Return value.ToString.PadLeft(2, "0")
    End Function
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim txt As String = ""
        txt &= myzero(DateTime.Now.Hour)
        txt &= ":" & myzero(DateTime.Now.Minute)
        txt &= ":" & myzero(DateTime.Now.Second)
        Label7.Text = txt
    End Sub

    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
       
        If (TypeOf (e.Exception) Is ConstraintException) Then
            Dim view As DataGridView = CType(sender, DataGridView)
            view.Rows(e.RowIndex).ErrorText = "an error"
            view.Rows(e.RowIndex).Cells(e.ColumnIndex) _
                .ErrorText = "an error"

            e.ThrowException = False
        End If
    End Sub
End Class