
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class XTLForm
    Dim XTLDataAdapter As New SqlClient.SqlDataAdapter
    Dim XTLtable As New DataTable

    Private Sub XTLForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub XTLForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveColumnOrder(DataGridView3)
    End Sub


    Private Sub XTLForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RestoreColumnOrder(Me.DataGridView3)
        DataGridView3.DataSource = XTLtable
        TextBox1.CharacterCasing = CharacterCasing.Upper
        If Len(PPForm.LookUpValue.Text) = 5 Then
            Me.TextBox1.Text = PPForm.LookUpValue.Text
        ElseIf Len(PPForm.LookUpValue.Text) <> 5 Then

            If PPForm.DataGridView1.RowCount <> 0 Then
                Me.TextBox1.Text = PPForm.DataGridView1(1, 0).Value
            ElseIf PPForm.datagridview2.RowCount <> 0 Then
                Me.TextBox1.Text = PPForm.DataGridView2(2, 0).Value
            End If
        End If
        Me.TopMost = False
        KeepInFrontToolStripMenuItem.Checked = False
        QueryXTL()
        'XTLcleanTableAdapter.Fill(SOLinesDataSet.XTLClean, PARTNO:=XTLSHOP.Text)
    End Sub

    Public Sub QueryXTL()

        'Dim XTLBindingSource As BindingSource
        'Dim StaticDataset As DataSet
        'Dim StatConnect As OleDb.OleDbConnectionStringBuilder



        Dim objConn As New SqlClient.SqlConnection("Server=SLREPORT01; Database=WFLocal; User Id=PrasinosApps; Password=Wyman123-;")


        Try
            XTLtable.Clear()
            objConn.Open()
            XTLDataAdapter.SelectCommand = New SqlClient.SqlCommand
            XTLDataAdapter.SelectCommand.CommandText = "Select PARTNO, OPERATION_NO, OPERATION_DESCR, WC_DESC, WORKCENTER, DWELL, MILESTONE From WFLOCAL..TIMELINE where PARTNO=@partno  ORDER BY OPERATION_NO"
            XTLDataAdapter.SelectCommand.Parameters.AddWithValue("@PARTNO", TextBox1.Text)
            XTLDataAdapter.SelectCommand.Connection = objConn

            XTLDataAdapter.Fill(XTLtable)

            ' XTLtable.Columns.Item("CURR_OP_DESCR").ColumnName = "OP_DESCR"
        Catch
        Finally
            objConn.Close()
        End Try

        Me.WindowState = FormWindowState.Minimized

        Me.WindowState = FormWindowState.Normal
        Me.Activate()

    End Sub


    Private Sub KeepInFrontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeepInFrontToolStripMenuItem.Click
        KeepInFrontToolStripMenuItem.Checked = Not (KeepInFrontToolStripMenuItem.Checked)
        Me.TopMost = KeepInFrontToolStripMenuItem.Checked
    End Sub


    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If InStr(TextBox1.Text, "Q") And Not (InStr(TextBox1.Text, "Q0")) Then TextBox1.Text = Replace(TextBox1.Text, "Q", "Q0")
            If Len(TextBox1.Text) = 4 Then TextBox1.Text = "0" & TextBox1.Text
            QueryXTL()
            TextBox1.SelectAll()
        End If
    End Sub

    Private Sub SaveColumnOrder(grid1 As DataGridView)

        If My.Settings.XTLColumnOrder Is Nothing Then
            My.Settings.XTLColumnOrder = New System.Collections.Specialized.StringCollection()
        End If

        My.Settings.XTLColumnOrder.Clear()
        Debug.Print("")
        For Each col In DataGridView3.Columns
            Debug.Print(col.index & "   " & col.name & "    " & col.displayindex)

            My.Settings.XTLColumnOrder.Add(col.DisplayIndex.ToString())
            'My.Settings(WhichAppSetting).add(col.displayindex.ToString())
        Next

        My.Settings.Save()
        My.Settings.Upgrade()

    End Sub

    Private Sub RestoreColumnOrder(ByVal WhichGrid As DataGridView)
        If Not My.Settings.UpgradeSettings Then
            My.Settings.Upgrade()
            My.Settings.UpgradeSettings = True
        End If
        ' My.Settings.Reload()
        If Not My.Settings.XTLColumnOrder Is Nothing Then
            For i = 0 To My.Settings.XTLColumnOrder.Count - 1
                Dim IndexPart As Integer = CInt(My.Settings.XTLColumnOrder(i).ToString)
                Debug.Print(My.Settings.XTLColumnOrder(i).ToString)
                DataGridView3.Columns(i).DisplayIndex = IndexPart
                Debug.Print(i & "     " & DataGridView3.Columns(i).Name & "    " & My.Settings.XTLColumnOrder(i).ToString)
            Next
        End If
    End Sub
End Class