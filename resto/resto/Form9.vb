Imports System.Data.OleDb
Imports System.Data.DataTable
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine

Imports excel = Microsoft.Office.Interop.Excel

Imports System.Globalization




Public Class Form9
    Dim con As New OleDbConnection
    Dim index As Integer
    Dim response As Integer
    Dim ttoid As String





    Dim connString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\solan\Desktop\restaurant project\resto.mdb"
    Dim excelLocation As String = "C:\Users\solan\Desktop\restaurant project\sheet2.xlsx"
    Dim MyConn As OleDbConnection
    Dim da As OleDbDataAdapter
    Dim ds As DataSet
    Dim tables As DataTableCollection
    Dim source1 As New BindingSource
    Dim APP As New excel.Application
    Dim worksheet As excel.Worksheet
    Dim workbook As excel.Workbook

    Private Sub Form9_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        workbook.Save()
        workbook.Close()
        APP.Quit()
    End Sub




    Private Sub Form9_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RestoDataSet.TABLE_ORDER' table. You can move, or remove it, as needed.
        Me.TABLE_ORDERTableAdapter.Fill(Me.RestoDataSet.TABLE_ORDER)
        


        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\solan\\Desktop\\restaurant project\\resto.mdb"
        con.Open()

        dgshow4()

        excelexport()



        Dim dte As String = ""
        Dim dtfinfo As DateTimeFormatInfo
        Dim dtstyle As String = "ddd, dd MMMM yyyy"
        dtfinfo = DateTimeFormatInfo.InvariantInfo
        dte = DateTime.Now.ToString(dtstyle, dtfinfo)
        Label8.Text = dte

        If Me.Enabled Then
            Timer1.Start()
            Label8.Visible = True
            Label9.Visible = True
        Else
            Timer1.Stop()
            Label8.Visible = False
            Label9.Visible = False

        End If

    End Sub
    Public Sub excelexport()
        workbook = APP.Workbooks.Open(excelLocation)
        worksheet = workbook.Worksheets("sheet1")
        MyConn = New OleDbConnection
        MyConn.ConnectionString = connString
        ds = New DataSet
        tables = ds.Tables
        da = New OleDbDataAdapter("Select * from [TABLE_ORDER]", MyConn) 'Change items to your database name
        da.Fill(ds, "TABLE_ORDER") 'Change items to your database name
        Dim view As New DataView(tables(0))
        source1.DataSource = view
        DataGridView1.DataSource = view
        DataGridView1.AllowUserToAddRows = False
    End Sub
    Private Function myzero(ByVal value As Integer) As String
        Return value.ToString.PadLeft(2, "0")
    End Function
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim txt As String = ""
        txt &= myzero(DateTime.Now.Hour)
        txt &= ":" & myzero(DateTime.Now.Minute)
        txt &= ":" & myzero(DateTime.Now.Second)
        Label9.Text = txt
    End Sub







    Public Sub dgshow4()
        Dim ds As New DataSet
        Dim dt As New DataTable

        ds.Tables.Add(dt)

        Dim da As New OleDbDataAdapter
        da = New OleDbDataAdapter("Select * from TABLE_ORDER", con)
        da.Fill(dt)
        DataGridView1.DataSource = dt.DefaultView
        con.Close()


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        con.Open()
        Dim cmd As New OleDbCommand("Select * from ORDER_TAB", con)
        cmd.Connection = con
        Dim oid As Object
        Dim strid As String
        Dim oidd As Integer
        cmd.CommandText = "Select max(ORDER_ID) as oid from ORDER_TAB"
        oid = cmd.ExecuteScalar
        If (oid Is DBNull.Value) Then
            oidd = 1
        Else
            strid = CType(oid, String)
            oidd = CType(strid, String)
            oidd = oidd + 1
        End If
        con.Close()


        Form10.Show()
        Form10.TextBox1.Text = oidd
        Form10.Button3.Visible = False
        Form10.Button1.Visible = True
        Form10.DataGridView1.DataSource = Nothing


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form11.Show()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try


            Form10.Show()
            Form10.Button1.Visible = False
            Form10.Button3.Visible = True

            Dim srow As DataGridViewRow
            srow = DataGridView1.Rows(index)
            srow.Cells(0).Value = Form10.TextBox1.Text

            Form10.ordershow1()
            Form10.ordertabshow()
        Catch ex As Exception
            MessageBox.Show("please select valid record or else you wont be able to insert Order ID ")

        End Try



    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick, DataGridView1.CellClick
        Try


            index = e.RowIndex
            Dim selectedrow As DataGridViewRow
            selectedrow = Me.DataGridView1.Rows(index)
            Form10.TextBox1.Text = selectedrow.Cells(0).Value.ToString

            TextBox1.Text = selectedrow.Cells(5).Value.ToString
            TextBox2.Text = selectedrow.Cells(6).Value.ToString
            TextBox3.Text = selectedrow.Cells(9).Value.ToString
        Catch ex As Exception
            MessageBox.Show("Please select valid record")
        End Try


    End Sub


    Private bitmap As Bitmap
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim srow1 As DataGridViewRow
        srow1 = DataGridView1.Rows(index)
        Dim o_id As Integer = srow1.Cells(0).Value.ToString
        Dim ccid As Integer = srow1.Cells(4).Value.ToString


        con.Open()
        Dim paidoption As String = "Paid"
        Dim query4 As String
        query4 = "Update TABLE_ORDER set TABLE_ORDER_PAID='" & paidoption & "' WHERE TABLE_ORDER_ID=" & o_id & " "
        Dim cmd As New OleDbCommand(query4, con)
        Dim i As Integer
        i = cmd.ExecuteNonQuery()
        If (i > 0) Then
            MessageBox.Show("ORDER PAID")
            Form16.TextBox2.Text = o_id
            Form16.TextBox3.Text = ccid
            Form16.Show()


        End If

        dgshow4()





        con.Close()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim srow1 As DataGridViewRow
        srow1 = DataGridView1.Rows(index)
        Dim oo_id As Integer = srow1.Cells(0).Value.ToString



        Dim writer As TextWriter = New StreamWriter("C:\Users\solan\Desktop\restaurant project\printbill.txt")

        For i As Integer = 0 To DataGridView1.Rows.Count - 2 Step +1

            For j As Integer = 0 To DataGridView1.Columns.Count - 1 Step +1

                writer.Write(vbTab & DataGridView1.Rows(i).Cells(j).Value.ToString() & vbTab & "|")

            Next

            writer.WriteLine("")
            writer.WriteLine("---------------------------------------------")

        Next
        writer.Close()
        MessageBox.Show("Data Exported")

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

        'Export Header Names Start
        Dim columnsCount As Integer = DataGridView1.Columns.Count
        For Each column In DataGridView1.Columns
            worksheet.Cells(1, column.Index + 1).Value = column.Name
        Next
        'Export Header Name End


        'Export Each Row Start
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Dim columnIndex As Integer = 0
            Do Until columnIndex = columnsCount
                worksheet.Cells(i + 2, columnIndex + 1).Value = DataGridView1.Item(columnIndex, i).Value.ToString
                columnIndex += 1
            Loop
        Next
        'Export Each Row End









    End Sub

    'Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
    'Dim bm As New Bitmap(Me.DataGridView1.Width, Me.DataGridView1.Height)
    ' DataGridView1.DrawToBitmap(bm, New Rectangle(0, 0, Me.DataGridView1.Width, Me.DataGridView1.Height))
    ' e.Graphics.DrawImage(bm, 0, 0)

    ' End Sub





    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

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