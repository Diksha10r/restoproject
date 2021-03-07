Imports System.Data.OleDb
Imports System.Data.DataTable
Imports System.Text.RegularExpressions


Public Class Form10
    Dim con As New OleDbConnection
    Dim odate, otime, otype, omenu, oitemnm As String
    Dim oprice, oitemid, oid, orid As Integer
    Dim toid As Integer

    Dim index As Integer
    Dim response As Integer

    Dim ttoid As Integer
    Dim uutoid As Integer




    Private Sub Form10_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RestoDataSet.ORDER_TAKE' table. You can move, or remove it, as needed.
        Me.ORDER_TAKETableAdapter.Fill(Me.RestoDataSet.ORDER_TAKE)





        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\\solan\\Desktop\\restaurant project\\resto.mdb"

        con.Open()



        custshow()

        empshow()

       
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
      

        Dim cmd As New OleDbCommand
        Dim da As New OleDbDataAdapter
        Dim query1 As String
        query1 = "Select * from ITEMS where MENU_TYPE='" & ComboBox2.Text & "'"
        With cmd
            .Connection = con
            .CommandText = query1
        End With
        Dim dt As New DataTable
        da.SelectCommand = cmd
        da.Fill(dt)
        With ComboBox3
            .DataSource = dt
            .DisplayMember = "ITEM_NAME"
        End With
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim cmd As New OleDbCommand
        Dim da As New OleDbDataAdapter
        Dim reader As OleDbDataReader
        Dim query2 As String

        query2 = "Select ITEM_ID,ITEM_PRICE from ITEMS where ITEM_NAME='" & ComboBox3.Text & "'"
        With cmd
            .Connection = con
            .CommandText = query2
        End With
        reader = cmd.ExecuteReader()
        While reader.Read()

            oitemid = reader(0).ToString()
            oprice = reader(1).ToString()
          
        End While

    End Sub



 

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            If Me.ValidateChildren And TextBox1.Text <> String.Empty And ComboBox1.Text <> String.Empty And ComboBox2.Text <> String.Empty And ComboBox3.Text <> String.Empty And ComboBox4.Text <> String.Empty And ComboBox5.Text <> String.Empty And ComboBox6.Text <> String.Empty Then
                additem()
            Else
                MessageBox.Show("ENTER ABOVE FIELDS")
            End If

 Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub additem()
        oid = CInt(TextBox1.Text)
        Dim oqua, itemid, itempr As Integer
        oqua = CInt(InputBox("enter quantity"))
        otype = ComboBox1.Text
        itemid = oitemid
        oitemnm = ComboBox3.Text
        omenu = ComboBox2.Text
        itempr = oprice
        Dim cid = ComboBox6.Text


        Dim tot As Integer
        tot = oqua * itempr



        Dim query As String
        query = "Insert into ORDER_TAKE ([ORDER_ID],[ITEM_ID],ITEM_NAME,MENU_TYPE,[ITEM_QUANTITY],[ITEM_PRICE],[ITEM_AMOUNT]) values(?,?,?,?,?,?,?)"
        Dim cmd As New OleDbCommand(query, con)
        cmd.Parameters.AddWithValue("ORDER_ID", oid)
        cmd.Parameters.AddWithValue("ITEM_ID", itemid)
        cmd.Parameters.AddWithValue("ITEM_NAME", oitemnm)
        cmd.Parameters.AddWithValue("MENU_TYPE", omenu)
        cmd.Parameters.AddWithValue("ITEM_QUANTITY", oqua)
        cmd.Parameters.AddWithValue("ITEM_PRICE", itempr)
        cmd.Parameters.AddWithValue("ITEM_AMOUNT", tot)
        Dim I As Integer
        I = cmd.ExecuteNonQuery()
        If (I > 0) Then
            MessageBox.Show("ITEM ADDED")
            dgshow1()
        Else
            MessageBox.Show("ITEM NOT ADDED")
        End If


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
       



        Dim strid As String
        Dim toidd As Object
        Dim cmmd As New OleDbCommand("Select * from TABLE_ORDER", con)
        cmmd.Connection = con
        cmmd.CommandText = "Select max(TABLE_ORDER_ID) as toidd from TABLE_ORDER"
        toidd = cmmd.ExecuteScalar
        If (toidd Is DBNull.Value) Then
            toid = 1
        Else
            strid = CType(toidd, String)
            toid = CType(strid, String)
            toid = toid + 1
        End If

      
        ordertab()


        tableorder()


    End Sub

    Public Sub dgshow1()
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds.Tables.Add(dt)
       

        Dim da As New OleDbDataAdapter
        Dim query6 As String
        query6 = "Select * from ORDER_TAKE where ORDER_ID= " & oid
        da = New OleDbDataAdapter(query6, con)
        da.Fill(dt)
        DataGridView1.DataSource = dt.DefaultView
    End Sub


    Public Sub custshow()
        Dim ccmd As New OleDbCommand
        Dim daa As New OleDbDataAdapter
        Dim query7 As String
        query7 = "Select * from CUSTOMER"
        With ccmd
            .Connection = con
            .CommandText = query7
        End With
        Dim dtt As New DataTable
        daa.SelectCommand = ccmd
        daa.Fill(dtt)
        With ComboBox6
            .DataSource = dtt
            .DisplayMember = "CUSTOMER_ID"
        End With
    End Sub

    Public Sub ordertab()
        Dim oid, eid, tot, cid As Integer
        Dim tbid, otype, query8, odate, otime As String
        oid = TextBox1.Text
        odate = DateTimePicker1.Value.Date
        otime = DateTimePicker2.Value.Hour
        otype = ComboBox1.Text
        eid = ComboBox4.Text
        tbid = ComboBox5.Text
        cid = ComboBox6.Text

        tot = (From row As DataGridViewRow In DataGridView1.Rows Where row.Cells(6).FormattedValue.ToString() <> String.Empty Select Convert.ToInt32(row.Cells(6).FormattedValue)).Sum().ToString()
        TextBox2.Text = tot

        query8 = "Insert into ORDER_TAB ([ORDER_ID],ORDER_DATE,ORDER_TIME,[CUSTOMER_ID],[EMPLOYEE_ID],ORDER_TYPE,TABLE_ID,[TOTAL]) values(?,?,?,?,?,?,?,?)"
        Dim cmd As New OleDbCommand(query8, con)
        cmd.Parameters.AddWithValue("ORDER_ID", oid)
        cmd.Parameters.AddWithValue("ORDER_DATE", odate)
        cmd.Parameters.AddWithValue("ORDER_TIME", otime)
        cmd.Parameters.AddWithValue("CUSTOMER_ID", cid)
        cmd.Parameters.AddWithValue("EMPLOYEE_ID", eid)
        cmd.Parameters.AddWithValue("ORDER_TYPE", otype)
        cmd.Parameters.AddWithValue("TABLE_ID", tbid)
        cmd.Parameters.AddWithValue("TOTAL", tot)

        Dim i As Integer
        i = cmd.ExecuteNonQuery()
        If (i > 0) Then
            MessageBox.Show("ORDER TAKEN")
        Else
            MessageBox.Show("ORDER NOT TAKEN")
        End If


    End Sub

    Public Sub tableorder()

        Dim oid, eid, tot, cid As Integer
        Dim tbid, otype, odate, otime As String

        Dim topa As String
        topa = "false"

        Dim query9 As String
        oid = TextBox1.Text
        odate = DateTimePicker1.Value.Date
        otime = DateTimePicker2.Value.Hour
        otype = ComboBox1.Text
        eid = ComboBox4.Text
        tbid = ComboBox5.Text
        cid = ComboBox6.Text

        tot = (From row As DataGridViewRow In DataGridView1.Rows Where row.Cells(6).FormattedValue.ToString() <> String.Empty Select Convert.ToInt32(row.Cells(6).FormattedValue)).Sum().ToString()
        TextBox2.Text = tot



        query9 = "Insert into TABLE_ORDER ([ORDER_ID],[TABLE_ORDER_ID],TABLE_ORDER_DATE,TABLE_ORDER_TIME,[CUSTOMER_ID],[EMPLOYEE_ID],TABLE_ORDER_TYPE,TABLE_ID,TABLE_ORDER_PAID,[TABLE_ORDER_PRICE]) values(?,?,?,?,?,?,?,?,?,?)"
        Dim ccmd As New OleDbCommand(query9, con)
        ccmd.Parameters.AddWithValue("ORDER_ID", oid)
        ccmd.Parameters.AddWithValue("TABLE_ORDER_ID", toid)
        ccmd.Parameters.AddWithValue("TABLE_ORDER_DATE", odate)
        ccmd.Parameters.AddWithValue("TABLE_ORDER_TIME", otime)
        ccmd.Parameters.AddWithValue("CUSTOMER_ID", cid)
        ccmd.Parameters.AddWithValue("EMPLOYEE_ID", eid)
        ccmd.Parameters.AddWithValue("TABLE_ORDER_TYPE", otype)
        ccmd.Parameters.AddWithValue("TABLE_ID", tbid)
        ccmd.Parameters.AddWithValue("TABLE_ORDER_PAID", topa)
        ccmd.Parameters.AddWithValue("TABLE_ORDER_PRICE", tot)
        Dim x As Integer
        x = ccmd.ExecuteNonQuery()
        If (x > 0) Then
            MsgBox("***********")
            MessageBox.Show("TABLE ORDER DONE")
            Form9.dgshow4()
            Me.Hide()

        Else
            MessageBox.Show("TABLE ORDER NOT TAKEN")

        End If
    End Sub


    Public Sub empshow()
        Dim cmd As New OleDbCommand
        Dim da As New OleDbDataAdapter
        Dim query3 As String
        query3 = "Select * from EMPLOYEE where EMPLOYEE_JOB_TYPE='waiter' "
        With cmd
            .Connection = con
            .CommandText = query3
        End With
        Dim dt As New DataTable
        da.SelectCommand = cmd
        da.Fill(dt)
        With ComboBox4
            .DataSource = dt
            .DisplayMember = "EMPLOYEE_ID"
        End With
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick, DataGridView1.CellClick
        Try

       
            index = e.RowIndex
            Dim selectedrow As DataGridViewRow
            selectedrow = Me.DataGridView1.Rows(index)
            Me.ComboBox2.Text = selectedrow.Cells(3).Value.ToString
            Me.ComboBox3.Text = selectedrow.Cells(2).Value.ToString

        Catch ex As Exception
            MessageBox.Show("Please select valid record")
        End Try

    End Sub

    Public Sub ordershow1()
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds.Tables.Add(dt)

        orid = CInt(TextBox1.Text)

        Dim da As New OleDbDataAdapter
        Dim queryordershow As String
        queryordershow = "Select * from ORDER_TAKE where ORDER_ID= " & orid
        da = New OleDbDataAdapter(queryordershow, con)
        da.Fill(dt)
        DataGridView1.DataSource = dt.DefaultView
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim srow As DataGridViewRow
        srow = DataGridView1.Rows(index)

        srow.Cells(3).Value = Me.ComboBox2.Text
        srow.Cells(2).Value = Me.ComboBox3.Text


     



        ordertabupdate()

        tableorderupdate()


    End Sub

    Public Sub ordertabshow()
        Dim query10 As String

        orid = CInt(TextBox1.Text)
        query10 = "Select * from ORDER_TAB where ORDER_ID= " & orid

        Dim dr As OleDbDataReader
        Dim cmd As New OleDbCommand(query10, con)
        cmd.CommandText = query10
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            ComboBox6.Text = dr.Item("CUSTOMER_ID")
            ComboBox1.Text = dr.Item("ORDER_TYPE")
            ComboBox4.Text = dr.Item("EMPLOYEE_ID")
            ComboBox5.Text = dr.Item("TABLE_ID")
            DateTimePicker1.Value = dr.Item("ORDER_DATE")

            dr.Close()

        End If

    End Sub

    Public Sub ordertabupdate()
        Dim uoid, ueid, utot, ucid As Integer
        Dim utbid, uotype, uorderatb, uodate, uotime As String
        uoid = TextBox1.Text
        uodate = DateTimePicker1.Value.Date
        uotime = DateTimePicker2.Value.Hour
        uotype = ComboBox1.Text
        ueid = ComboBox4.Text
        utbid = ComboBox5.Text
        ucid = ComboBox6.Text

        utot = (From row As DataGridViewRow In DataGridView1.Rows Where row.Cells(6).FormattedValue.ToString() <> String.Empty Select Convert.ToInt32(row.Cells(6).FormattedValue)).Sum().ToString()

        uorderatb = "Update ORDER_TAB set ORDER_DATE=?, ORDER_TIME=?, CUSTOMER_ID=?, EMPLOYEE_ID=?, ORDER_TYPE=?, TABLE_ID=?, TOTAL=? where ORDER_ID=? "
        Dim uocmd As New OleDbCommand(uorderatb, con)
        uocmd.Parameters.AddWithValue("ORDER_DATE", uodate)
        uocmd.Parameters.AddWithValue("ORDER_TIME", uotime)
        uocmd.Parameters.AddWithValue("CUSTOMER_ID", ucid)
        uocmd.Parameters.AddWithValue("EMPLOYEE_ID", ueid)
        uocmd.Parameters.AddWithValue("ORDER_TYPE", uotype)
        uocmd.Parameters.AddWithValue("TABLE_ID", utbid)
        uocmd.Parameters.AddWithValue("TOTAL", utot)
        uocmd.Parameters.AddWithValue("ORDER_ID", uoid)

        Dim uo As Integer
        uo = uocmd.ExecuteNonQuery()
        If (uo > 0) Then
            MessageBox.Show("ORDER UPDATED")
        Else
            MessageBox.Show("ORDERNOT UPDATED")
        End If

    End Sub

    Public Sub tableorderupdate()
        Dim utoid, uteid, utcid, uttot, utbid As Integer
        Dim uttbid, utotype, utodate, utotime As String
        Dim uttopa As String
        uttopa = "false"

        Dim utableorder As String
        utbid = TextBox1.Text
        utoid = TextBox1.Text
        utodate = DateTimePicker1.Value.Date
        utotime = DateTimePicker2.Value.Hour
        utotype = ComboBox1.Text
        uteid = ComboBox4.Text
        uttbid = ComboBox5.Text
        utcid = ComboBox6.Text

        uttot = (From row As DataGridViewRow In DataGridView1.Rows Where row.Cells(6).FormattedValue.ToString() <> String.Empty Select Convert.ToInt32(row.Cells(6).FormattedValue)).Sum().ToString()

        utableorder = "Update TABLE_ORDER set ORDER_ID=?, TABLE_ORDER_DATE=?, TABLE_ORDER_TIME=?, CUSTOMER_ID=?, EMPLOYEE_ID=?, TABLE_ORDER_TYPE=?, TABLE_ID=?,TABLE_ORDER_PAID=?, TABLE_ORDER_PRICE=? where TABLE_ORDER_ID=? "
        Dim utbcmd As New OleDbCommand(utableorder, con)
        utbcmd.Parameters.AddWithValue("ORDER_ID", utoid)
        utbcmd.Parameters.AddWithValue("TABLE_ORDER_DATE", utodate)
        utbcmd.Parameters.AddWithValue("TABLE_ORDER_TIME", utotime)
        utbcmd.Parameters.AddWithValue("CUSTOMER_ID", utcid)
        utbcmd.Parameters.AddWithValue("EMPLOYEE_ID", uteid)
        utbcmd.Parameters.AddWithValue("TABLE_ORDER_TYPE", utotype)
        utbcmd.Parameters.AddWithValue("TABLE_ID", uttbid)
        utbcmd.Parameters.AddWithValue("TABLE_ORDER_PAID", uttopa)
        utbcmd.Parameters.AddWithValue("TABLE_ORDER_PRICE", uttot)
        utbcmd.Parameters.AddWithValue("TABLE_ORDER_ID", utbid)

        Dim utb As Integer
        utb = utbcmd.ExecuteNonQuery()
        If (utb > 0) Then
            MessageBox.Show("TABLE ORDER UPDATED")


            Form9.Show()
            Form9.dgshow4()

            Me.Hide()
        Else
            MessageBox.Show("TABLE ORDER NOT UPDATED")
        End If


    End Sub


    Private Sub ComboBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ComboBox1.Validating
        If DirectCast(sender, ComboBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "SELECT ORDER TYPE")
            ComboBox1.Text = ""
        End If
    End Sub

    Private Sub ComboBox2_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ComboBox2.Validating
        If DirectCast(sender, ComboBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "SELECT MENU")
            ComboBox2.Text = ""
        End If
    End Sub

    Private Sub ComboBox3_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ComboBox3.Validating
        If DirectCast(sender, ComboBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "SELECT ITEM")
            ComboBox3.Text = ""
        End If
    End Sub

    

    Private Sub ComboBox6_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ComboBox6.Validating
        If DirectCast(sender, ComboBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "SELECT CUSTOMER NO")
            ComboBox6.Text = ""
        End If
    End Sub

    

    Private Sub ComboBox4_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ComboBox4.Validating
        If DirectCast(sender, ComboBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "SELECT CUSTOMER NO")
            ComboBox4.Text = ""
        End If
    End Sub

    

    Private Sub ComboBox5_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ComboBox5.Validating
        If DirectCast(sender, ComboBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")

        Else
            Me.ErrorProvider1.SetError(sender, "SELECT TABLE NO.")
            ComboBox5.Text = ""
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged

    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.MaxDate = Date.Now
        DateTimePicker1.MinDate = Date.Now


    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        DateTimePicker2.MinDate = System.DateTime.Now()

    End Sub

    

    Private Sub TextBox1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBox1.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorProvider1.SetError(sender, "")
        Else
            Me.ErrorProvider1.SetError(sender, "YOU HAVE NOT SELECTED RECORD FROM DATAGRID")
        End If

    End Sub

    
End Class