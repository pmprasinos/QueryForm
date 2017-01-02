Public Class LotHistoryForm


    Private Sub LotHistoryForm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Timer1.Stop()
        Timer1.Start()
    End Sub

    Private Sub LotHistoryForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode.Equals(Keys.Enter) Then class1.PullHistory(TextBox1.Text)
    End Sub

    Private Sub Timer1_Tick() Handles Timer1.Tick
        Me.Close()
    End Sub


End Class