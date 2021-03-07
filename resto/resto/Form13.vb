Imports System.Data.OleDb
Imports System.Data.DataTable

Public Class Form13
    Dim con As New OleDbConnection
    Dim index As Integer
    Dim response As Integer



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        con.Open()
        Dim cmd As New OleDbCommand("Select * from ITEMS", con)
        cmd.Connection = con
        Dim oid As Object
        Dim strid As String
        Dim oidd As Integer
        cmd.CommandText = "Select max(ITEM_ID) as oid from ITEMS"
        oid = cmd.ExecuteScalar
        If (oid Is DBNull.Value) Then
            oidd = 1
        Else
            strid = CType(oid, String)
            oidd = CType(strid, String)
            oidd = oidd + 1
        End If
        con.Close()

        Form14.Show()
        Form14.addclear()
        Form14.TextBox1.Text = oidd
        Form14.Button3.Visible = False

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            Form14.Show()
            Form14.Button1.Visible = False
            Dim srow As DataGridViewRow
            srow = DataGridView1.Rows(index)
            srow.Cells(0).Value = Form14.TextBox1.Text
            srow.Cells(1).Value = Form14.TextBox2.Text
            srow.Cells(2).Value = Form14.ComboBox1.Text
            srow.Cells(3).Value = Form14.TextBox3.Text
            srow.Cells(4).Value = Form14.TextBox4.Text
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub Form13_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RestoDataSet.ITEMS' table. You can move, or remove it, as needed.
        Me.ITEMSTableAdapter.Fill(Me.RestoDataSet.ITEMS)

        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\\solan\\Desktop\\restaurant project\\resto.mdb"
        con.Open()

        dgshow()

    End Sub

    Public Sub dgshow()
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds.Tables.Add(dt)

        Dim da As New OleDbDataAdapter

        da = New OleDbDataAdapter("Select * from ITEMS", con)
        da.Fill(dt)
        DataGridView1.DataSource = dt.DefaultView
        con.Close()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim srow1 As DataGridViewRow
        srow1 = DataGridView1.Rows(index)
        Dim item_id As Integer = srow1.Cells(0).Value.ToString
        con.Open()

        Dim query4 As String
        query4 = "Delete from ITEMS WHERE ITEM_ID=" & item_id & " "
        Dim cmd As New OleDbCommand(query4, con)
        Dim i As Integer
        i = cmd.ExecuteNonQuery()
        If (i > 0) Then MessageBox.Show("Record Deleted")
        dgshow()




        con.Close()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick, DataGridView1.CellClick
        Try

            index = e.RowIndex
            Dim selectedrow As DataGridViewRow
            selectedrow = Me.DataGridView1.Rows(index)
            TextBox1.Text = selectedrow.Cells(0).Value.ToString
            TextBox2.Text = selectedrow.Cells(1).Value.ToString
            TextBox3.Text = selectedrow.Cells(2).Value.ToString
            Form14.TextBox1.Text = selectedrow.Cells(0).Value.ToString
            Form14.TextBox2.Text = selectedrow.Cells(1).Value.ToString
            Form14.ComboBox1.Text = selectedrow.Cells(2).Value.ToString
            Form14.TextBox3.Text = selectedrow.Cells(3).Value.ToString
            Form14.TextBox4.Text = selectedrow.Cells(4).Value.ToString


        Catch ex As Exception
            MessageBox.Show("Please select valid record")
        End Try


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form15.Show()

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