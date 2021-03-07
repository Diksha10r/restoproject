Public Class Form11

    Private Sub Form11_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RestoDataSet.TABLE_ORDER' table. You can move, or remove it, as needed.
        Me.TABLE_ORDERTableAdapter.Fill(Me.RestoDataSet.TABLE_ORDER)

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
            TABLEORDERBindingSource.Filter = "(Convert(TABLE_ORDER_ID, 'System.String')LIKE '" & TextBox1.Text & "')" & _
            "OR (TABLE_ORDER_TYPE LIKE '" & TextBox1.Text & "') OR (Convert (EMPLOYEE_ID, 'System.String') LIKE '" & TextBox1.Text & "')" & _
            "OR (Convert (CUSTOMER_ID, 'System.String')LIKE '" & TextBox1.Text & "')"


            If TABLEORDERBindingSource.Count <> 0 Then
                With DataGridView1
                    .DataSource = TABLEORDERBindingSource

                End With

            Else
                MsgBox("->" & s & vbNewLine & _
                        "the search item was not found.", _
                        MsgBoxStyle.Information, "hello sir!")

                TABLEORDERBindingSource.Filter = Nothing

                With DataGridView1
                    .ClearSelection()
                    .ReadOnly = True
                    .MultiSelect = False
                    .DataSource = TABLEORDERBindingSource
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


    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        DataGridView1.Columns(e.ColumnIndex).SortMode = DataGridViewColumnSortMode.Automatic

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        Form10.Show()

    End Sub
End Class