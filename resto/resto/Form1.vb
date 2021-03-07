Imports System.Data.OleDb


Public Class Form1

    Dim con As New OleDbConnection

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\solan\\Desktop\\restaurant project\\resto.mdb"
        con.Open()



    End Sub
    Public Function ask()
        Dim dt As New DataTable
        Dim ds As New DataSet
        ds.Tables.Add(dt)


        Dim da As New OleDb.OleDbDataAdapter
        da = New OleDbDataAdapter("Select * from LOGIN", con)
        da.Fill(dt)


        For Each DataRow In dt.Rows
            If TextBox1.Text = DataRow.item(0) And TextBox2.Text = DataRow.item(1) Then
                con.Close()
                Return True
            End If
        Next
        con.Close()
        Return False

    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        
        If ask() = True Then
            MessageBox.Show("Successfully loged in")
            Timer1.Start()
        Else
            MessageBox.Show("Invalid Details")
        End If
        


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Start()
        Static cnt As Integer
        If cnt = 100 Then cnt = 0
        Me.ProgressBar1.Value = cnt
        cnt = cnt + 10
        ProgressBar1.Increment(10)

        If ProgressBar1.Value = 10 Then  Label3.Text = "Loading Forms...!!!"


        If ProgressBar1.Value = 20 Then Label3.Text = "Connecting Database...!!!"


        If ProgressBar1.Value = 40 Then Label3.Text = "Prepareing User Interface...!!!"


        If ProgressBar1.Value = 60 Then Label3.Text = "Checking Connectivity...!!!"


        If ProgressBar1.Value = 80 Then Label3.Text = "Preparing Accounts Info...!!!"


        If ProgressBar1.Value = 90 Then Label3.Text = "Welcome...!!!"


        If ProgressBar1.Value = ProgressBar1.Maximum Then
            Timer1.Stop()
            MDIParent1.Show()
            Me.Hide()

        End If
    End Sub
End Class
