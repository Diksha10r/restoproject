Public Class Form15

    Private Sub Form15_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RestoDataSet.ITEMS' table. You can move, or remove it, as needed.
        Me.ITEMSTableAdapter.Fill(Me.RestoDataSet.ITEMS)
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
            ITEMSBindingSource.Filter = "(Convert(ITEM_ID, 'System.String')LIKE '" & TextBox1.Text & "')" & _
            "OR (ITEM_NAME LIKE '" & TextBox1.Text & "') OR (MENU_TYPE LIKE '" & TextBox1.Text & "')"



            If ITEMSBindingSource.Count <> 0 Then
                With DataGridView1
                    .DataSource = ITEMSBindingSource

                End With

            Else
                MsgBox("->" & s & vbNewLine & _
                        "the search item was not found.", _
                        MsgBoxStyle.Information, "hello sir!")

                ITEMSBindingSource.Filter = Nothing

                With DataGridView1
                    .ClearSelection()
                    .ReadOnly = True
                    .MultiSelect = False
                    .DataSource = ITEMSBindingSource
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


    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        Form14.Show()

    End Sub
End Class
 