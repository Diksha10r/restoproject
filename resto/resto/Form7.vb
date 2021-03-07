

Public Class Form7

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form5.Show()
        Me.Hide()

    End Sub

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RestoDataSet.RESERVATION' table. You can move, or remove it, as needed.
        Me.RESERVATIONTableAdapter.Fill(Me.RestoDataSet.RESERVATION)
        With DataGridView1
            .ClearSelection()
            .ReadOnly = True
            .MultiSelect = False

        End With
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        On Error GoTo searcherr

        If TextBox1.Text = "" Then
            Exit Sub
        Else
            Dim s As String = TextBox1.Text
            RESERVATIONBindingSource.Filter = "(Convert(RESERVATION_ID, 'System.String')LIKE '" & TextBox1.Text & "')" & _
            "OR (Convert (CUSTOMER_ID, 'System.String')LIKE '" & TextBox1.Text & "') OR (EVENT_TYPE LIKE '" & TextBox1.Text & "')"


            If RESERVATIONBindingSource.Count <> 0 Then
                With DataGridView1
                    .DataSource = RESERVATIONBindingSource

                End With

            Else
                MsgBox("->" & s & vbNewLine & _
                        "the search item was not found.", _
                        MsgBoxStyle.Information, "hello sir!")

                RESERVATIONBindingSource.Filter = Nothing

                With DataGridView1
                    .ClearSelection()
                    .ReadOnly = True
                    .MultiSelect = False
                    .DataSource = RESERVATIONBindingSource
                End With


            End If
        End If
errexit:
        Exit Sub

searcherr:
        MsgBox("error number" & Err.Number & vbNewLine & _
                "error description " & Err.Description, MsgBoxStyle.Critical, _
                "reset error!")
        Resume errexit

    End Sub



End Class