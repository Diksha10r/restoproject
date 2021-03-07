Public Class Form19

   Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If e.Node.Name = "Node0" Then
            RichTextBox1.Text = "The Customer Report was designed to provide a monthly statement to Principal Investigators, Directors, Program Managers, etc.Every store or company must have its own customer service report sets to gauge its success with the customers. Customer is the king for any commercial establishment and a company’s success and failure depends on the experience it provides for the clients.The service report filled by the customers help a firm to understand its strengths and where it needs to improve."
            Button1.Visible = True
            Button2.Visible = False
            Button3.Visible = False
            Button4.Visible = False
            Button5.Visible = False

        End If
        If e.Node.Name = "Node1" Then
            RichTextBox1.Text = "Reservation is actually keeping something reserved for a particular person or a party. Reservation system is very important now a days as some parties reserve tables for their special occasions like anniversary, birthday etc as the parties want to avoid any kind of mess or to wait and pay a good amount for it. Most of the restaurants use reservation log but some restaurants follow the policy of first come first serve. Sometimes this system is useful for the manager but sometimes they suffer loss as well. Because sometimes instead of coming after reservation, parties change their plans and the manager suffers loss. So in order to avoid such cancellations some of the managers try to make reservations more than the available space but if no cancellations are made then the manager suffers and he has to reserve a table in the comparable hotel and pay for table and the tax of that hotel."
            Button2.Visible = True
            Button1.Visible = False
            Button3.Visible = False
            Button4.Visible = False
            Button5.Visible = False

        End If
        If e.Node.Name = "Node2" Then
            RichTextBox1.Text = "A Restaurant Receipt Template is useful in recording all payments made by the customers in a restaurant. It helps in recording and tracking expected expenses and income of the restaurant business, especially for future references. So, use a restaurant receipt template to create a restaurant receipt for your customers. There are different kinds of templates to choose from, depending on your requirements and preferences."
            Button3.Visible = True
            Button1.Visible = False
            Button2.Visible = False
            Button4.Visible = False
            Button5.Visible = False

        End If
        If e.Node.Name = "Node3" Then
            RichTextBox1.Text = "This Employee Profile provides you with a summary view of your employees. It displays vital statistics such as address and telephone numbers.These reports are designed to give you control over schedules, certification compliance and contact information"
            Button4.Visible = True
            Button5.Visible = False
            Button1.Visible = False
            Button2.Visible = False
            Button3.Visible = False

        End If
        If e.Node.Name = "Node4" Then
            RichTextBox1.Text = "The end goal is to find out what you can improve upon, make it clear to the customer that their feedback is indeed important to you, and then follow up on that feedback to improve the customer’s comprehensive experience. It’s only through return business and first-degree referrals that restaurants survive in a competitive environment."
            Button5.Visible = True
            Button1.Visible = False
            Button2.Visible = False
            Button3.Visible = False
            Button4.Visible = False

        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Form20.Show()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form21.Show()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Form22.Show()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form23.Show()

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Form24.Show()

    End Sub
End Class