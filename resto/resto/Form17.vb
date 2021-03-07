Imports System.Data.OleDb

Public Class Form17
    Dim r As New Random()
    Dim con As New OleDbConnection



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Dim i As Integer = +1


        PictureBox1.Image = My.Resources.ResourceManager.GetObject("img" & r.Next(i, 50))








    End Sub

    Private Sub Form17_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RestoDataSet.ITEMS' table. You can move, or remove it, as needed.
        Me.ITEMSTableAdapter.Fill(Me.RestoDataSet.ITEMS)

        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\\solan\\Desktop\\restaurant project\\resto.mdb"

        Timer1.Start()
        Timer2.Start()

    End Sub

    
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        dgshow()

    End Sub
    Public Sub dgshow()
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds.Tables.Add(dt)


        Dim da As New OleDbDataAdapter
        Dim query As String
        query = "Select ITEM_NAME,MENU_TYPE,ITEM_PRICE from ITEMS where MENU_TYPE='" & ComboBox1.Text & "'"
        da = New OleDbDataAdapter(query, con)
        da.Fill(dt)
        DataGridView1.DataSource = dt.DefaultView
    End Sub

   

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick

        DataGridView1.Columns(e.ColumnIndex).SortMode = DataGridViewColumnSortMode.Automatic

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Dim i As Integer = +1

        PictureBox2.Image = My.Resources.ResourceManager.GetObject("img_" & r.Next(i, 50))
    End Sub
End Class