Public Class PSQLForm

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        If e.ColumnIndex = 2 Then
            ' DialogResult = Windows.Forms.DialogResult.OK
            PPForm.LookUpValue.Text = DataGridView1.CurrentCell.Value
            If Not PPForm.OpenTechCard(DataGridView1.CurrentCell.Value) Then MsgBox("I CANT FIND THE CARD IN ASSY")
            PPForm.QueryTableForData()
            Me.Close()
        End If
    End Sub

    Private Sub PSQLForm_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub

    Private Sub VIEWWAXTECHCARDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIEWWAXTECHCARDToolStripMenuItem.Click
        If Not PPForm.OpenTechCard(DataGridView1.CurrentCell.Value) Then MsgBox("I CANT FIND THE CARD IN ASSY")
    End Sub

    Private Sub LOOKUPOPENSSHIPMENTSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LOOKUPOPENSSHIPMENTSToolStripMenuItem.Click
        PPForm.LookUpValue.Text = DataGridView1.CurrentCell.Value
        PPForm.QueryTableForData()
        Me.Close()
    End Sub

End Class