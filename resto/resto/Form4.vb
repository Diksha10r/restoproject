Public Class Form4

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form2.Show()
        Me.Hide()

    End Sub

    Private Sub Form4_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RestoDataSet.CUSTOMER' table. You can move, or remove it, as needed.
        Me.CUSTOMERTableAdapter.Fill(Me.RestoDataSet.CUSTOMER)
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
            CUSTOMERBindingSource.Filter = "(Convert(CUSTOMER_ID, 'System.String')LIKE '" & TextBox1.Text & "')" & _
            "OR (CUSTOMER_NAME LIKE '" & TextBox1.Text & "') OR (CUSTOMER_ADDRESS LIKE '" & TextBox1.Text & "')" & _
            "OR (Convert (CUSTOMER_CONTACT, 'System.String')LIKE '" & TextBox1.Text & "')"


            If CUSTOMERBindingSource.Count <> 0 Then
                With DataGridView1
                    .DataSource = CUSTOMERBindingSource

                End With

            Else
                MsgBox("->" & s & vbNewLine & _
                        "the search item was not found.", _
                        MsgBoxStyle.Information, "hello sir!")

                CUSTOMERBindingSource.Filter = Nothing

                With DataGridView1
                    .ClearSelection()
                    .ReadOnly = True
                    .MultiSelect = False
                    .DataSource = CUSTOMERBindingSource
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

   
End Class