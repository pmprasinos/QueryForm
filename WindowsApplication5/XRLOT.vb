Imports System.ComponentModel
Imports System.Runtime.CompilerServices



Public Class XRLOT
    '
    Dim XRlotInit As Boolean = False
    Dim XRLotDataAdapter As New SqlClient.SqlDataAdapter
    Dim AlloyDataAdapter As New SqlClient.SqlDataAdapter
    Dim objConn As SqlClient.SqlConnection
    Dim XRLottable As New DataTable
    Dim WorkOrderToLookup As String
    Dim wf As WebfocusDLL.WebfocusModule
    Public j As Object
    Public MaxAge As Int16
    Public restoreorder As Boolean = False

    'Dim objconn As New SqlClient.SqlConnection

    Private Sub XRLOT_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveColumnOrder()
        My.Settings.Save()
    End Sub


    Private Sub XRLOT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
        TextBox1.CharacterCasing = CharacterCasing.Upper
        RemoveHandler Me.SHOWLOTS.CheckedChanged, AddressOf SHOWLOTS_CheckedChanged
        RemoveHandler Me.ShowDetail.CheckedChanged, AddressOf ShowDetail_CheckedChanged
        SHOWLOTS.Checked = My.Settings.ShowWIPLots

        objConn = New SqlClient.SqlConnection("Server=SLREPORT01; Database=WFLocal; persist security info=False; trusted_connection=Yes;")
        XRLotDataAdapter.SelectCommand = New SqlClient.SqlCommand
        XRLotDataAdapter.SelectCommand.Connection = objConn
        AlloyDataAdapter.SelectCommand = New SqlClient.SqlCommand
        AlloyDataAdapter.SelectCommand.Connection = objConn
        Debug.Print(Now)

        'objConn.ConnectionString = "Server=SLREPORT01; Database=WFLocal; persist security info=False; trusted_connection=Yes;"


        ' Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.WORKORDER, Me.LAB, Me.TYPE, Me.SN, Me.OP, Me.WC, Me.WCDESC, Me.STOPWORK, Me.MRBOK, Me.SHPH, Me.METALLOT, Me.CHEM_RESULTS, Me.HEATTREAT_RESULTS, Me.HARDNESS_ML_RESULTS, Me.HARDNESS_CASTING_RESULTS, Me.RTCONTROL_RESULTS, Me.MECH_RESULTS, Me.ID, Me.QTY})
        With DataGridView1
            .Columns("AGE").Width = 40
            .Columns("WORKORDER").Width = 120
            .Columns("LAB").Width = 100
            .Columns("TYPE").Width = 40
            .Columns("SN").Width = 60
            .Columns("OP").Width = 40
            .Columns("WC").Width = 40
            .Columns("WCDESC").Width = 150
            .Columns("OP_DESC").Width = 150
            .Columns("STOPWORK").Width = 30
            .Columns("MRBOK").Width = 30
            .Columns("SHPH").Width = 30
            .Columns("METALLOT").Width = 70

            '.Columns("CHEM_RESULTS").Width = 100
            '  .Columns("HEATTREAT_RESULTS").Width = 100
            '  .Columns("RTCONTROL_RESULTS").Width = 100
            ' .Columns("HARDNESS_ML_RESULTS").Width = 100
            '  .Columns("ID").Width = 20
            .Columns("QTY").Width = 40
            ' .Columns("RTCONTROL_RESULTS").Width = 100
            ' .Columns("HARDNESS_CASTING_RESULTS").Width = 100
            '  .Columns("HARDNESS_ML_RESULTS").Width = 100

        End With
        Debug.Print(Now)
        If My.Settings.UpgradeSettings Then
            My.Settings.UpgradeSettings = False
            My.Settings.Upgrade()
            My.Settings.Save()
        End If
        Debug.Print(Now)


        DataGridView1.DataSource = XRLottable
        ToolStripMenuItem1.Checked = False
        TopMost = ToolStripMenuItem1.Checked
        'VISLOTSTableAdapter.ClearBeforeFill = True

        If SHOWLOTS.Checked Then CheckBoxWCSum.Visible = False
        Debug.Print(Now)
        ' SHOWLOTS.Checked = True
        AddHandler Me.SHOWLOTS.CheckedChanged, AddressOf SHOWLOTS_CheckedChanged
        AddHandler Me.ShowDetail.CheckedChanged, AddressOf ShowDetail_CheckedChanged
    End Sub

    Private Sub SHOWLOTS_CheckedChanged(sender As Object, e As EventArgs) Handles SHOWLOTS.CheckedChanged

        ShowDetail.Visible = SHOWLOTS.Checked
            CheckBoxWCSum.Visible = Not SHOWLOTS.Checked

            If restoreorder Then
            UPDATEQUERYXR()
        End If
    End Sub



    Sub UPDATEQUERYXR()
        Dim t As Date = Now
        Debug.Print("NOW")

        RemoveHandler Me.DataGridView1.SelectionChanged, AddressOf Me.DataGridView1_SelectionChanged
        ' Me.SOLinesDataSet.Clear()

        '  If ShowDetail.Checked Then SHOWLOTS.CheckState = CheckState.Checked
        If (InStr(TextBox1.Text, "Q") Or InStr(TextBox1.Text, "q0")) And Not ((InStr(TextBox1.Text, "Q0")) Or InStr(TextBox1.Text, "q0")) Then TextBox1.Text = Replace(TextBox1.Text, "Q", "Q0")
        If Len(TextBox1.Text) = 4 Then TextBox1.Text = "0" & TextBox1.Text
        Try
            If PPForm.currentxrlookup <> TextBox1.Text Then
                PPForm.alloy = "ALLOY: " & AlloysQuery(0, TextBox1.Text)

                TextBox2.Text = PPForm.alloy
                PPForm.currentxrlookup = TextBox1.Text
                If PPForm.DisplayAlloy Then
                    '  PPForm.alloy = PPForm.alloy & "  |  PROGRAM: " & AlloysQuery(2, TextBox1.Text)
                    'If AlloysQuery(1, TextBox1.Text) = CDbl(0) Then
                    '    ' Debug.Print(ALLOYSTableAdapter.PPEQuery(TextBox1.Text).ToString)
                    '    PPForm.alloy = PPForm.alloy & "  |  STOPREL: N"
                    'Else
                    '    PPForm.alloy = PPForm.alloy & "  |  STOPREL: Y"

                    'End If
                    ' PPForm.alloy = PPForm.alloy & "  |  PPE:" & ALLOYSTableAdapter.PPEQuery(TextBox1.Text)
                End If
            End If

        Catch
        End Try
        TextBox2.Text = PPForm.alloy
        Debug.Print((Now - t).ToString)
        Dim s As String = ""
        s = TextBox1.Text
        Dim f As Integer
        If SHOWLOTS.Checked Then
            ' MsgBox(1)
            ShowDetail.Visible = True

            If ShowDetail.Checked Then
                ' Try
                FillLots(2, s)
            Else
                FillLots(1, s)
            End If
            Debug.Print("trav" & (Now - t).ToString)
            Try : CheckTravlers() : Catch : End Try
            Debug.Print((Now - t).ToString)
        Else
            '    Try
            If CheckBoxWCSum.Checked Then
                FillLots(3, s)
            Else
                FillLots(0, s)
            End If
            ShowDetail.Visible = False



        End If
        Debug.Print((Now - t).ToString)
        HyperLinkFonts()
        Debug.Print((Now - t).ToString)
        ' Me.Show()
        'If DataGridView1.SortOrder.ToString <> "none" Then
        'DataGridView1.Sort(DataGridView1.SortedColumn, DataGridView1.SortOrder - 1)
        'End If
        Me.WindowState = FormWindowState.Minimized

        Me.WindowState = FormWindowState.Normal
        'Me.BringToFront()
        Me.Activate()
        ' RestoreColumnOrder()

        AddHandler Me.DataGridView1.SelectionChanged, AddressOf Me.DataGridView1_SelectionChanged

    End Sub


    Private Function AlloysQuery(CommandNum As Integer, SearchShop As String) As String
        Dim S As Object
        AlloysQuery = ""

        objConn.Open()
        Try
            AlloyDataAdapter.SelectCommand.Parameters.Clear()
            AlloyDataAdapter.SelectCommand.CommandText = "SELECT  ALLOY_DESCR FROM  ALLOYS WHERE  (PARTNO = @PARTNO)"
            AlloyDataAdapter.SelectCommand.Parameters.AddWithValue("@PARTNO", TextBox1.Text)
            S = AlloyDataAdapter.SelectCommand.ExecuteScalar
        Finally
            objConn.Close()

        End Try
        Return S
    End Function


    Private Sub FillLots(ByLot As Integer, SearchTerm As String)
        XRLotDataAdapter.SelectCommand.CommandText = ""
        For Each column As DataGridViewColumn In Me.DataGridView1.Columns
            If Not column.Visible Then column.Visible = True
        Next

        If ComboBox1.SelectedItem.ToString = "DEPT" Then
            If ByLot <> 3 And ByLot <> 2 Then
                ByLot = 1
                Me.DataGridView1.Columns("MS").Visible = True
            End If

        Else
            Me.DataGridView1.Columns("MS").Visible = False
        End If

        Try
            XRLottable.Clear()
            XRLotDataAdapter.SelectCommand.CommandText = ""
            With Me.DataGridView1
                If ByLot <> 3 Then .Columns("VALUE").Visible = False
                If ByLot = 0 Or ByLot = 3 Then
                    .Columns("WORKORDER").Visible = False
                    .Columns("TYPE").Visible = False
                Else
                    .Columns("SN").Visible = False
                End If

                If ByLot = 2 Then

                Else
                    .Columns("SHPH").Visible = False
                    .Columns("STOPWORK").Visible = False
                    .Columns("LAB").Visible = False
                    .Columns("METALLOT").Visible = False
                    .Columns("MRBOK").Visible = False
                    If ByLot = 3 Then
                        .Columns("OP").Visible = False
                        .Columns("OP_DESC").Visible = False
                    End If
                End If
            End With

            objConn.Open()
            If ComboBox1.SelectedItem.ToString = "DEPT" Then
                SQLGenerator.MakeLotsQuery(XRLotDataAdapter.SelectCommand, ByLot, SearchTerm, 1)
            Else
                SQLGenerator.MakeLotsQuery(XRLotDataAdapter.SelectCommand, ByLot, SearchTerm)
            End If

            XRLotDataAdapter.Fill(XRLottable)

        Catch ex As Exception
            MsgBox("ERROR IN QUERY" & vbCrLf & vbCrLf & ex.Message.ToString & vbCrLf & vbCrLf & ex.InnerException.ToString)
        Finally

            objConn.Close()
        End Try

    End Sub


    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If ComboBox1.SelectedItem.ToString = "DEPT" Then
                FillLots(1, TextBox1.Text)
                Exit Sub
            End If
            If PPForm.Visible Then
                If InStr(TextBox1.Text, "Q") And Not (InStr(TextBox1.Text, "Q0")) Then
                    TextBox1.Text = Replace(TextBox1.Text, "Q", "Q0")
                ElseIf Len(TextBox1.Text) = 4 Then
                    TextBox1.Text = "0" & TextBox1.Text
                End If

                PPForm.LookUpValue.Text = TextBox1.Text()
                PPForm.QueryTableForData("XRLOT")
            Else
                Me.TextBox1.SelectAll()
            End If

        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Me.ToolStripMenuItem1.Checked = Not (Me.ToolStripMenuItem1.Checked)
        Me.TopMost = Me.ToolStripMenuItem1.Checked

    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick

        If e.ColumnIndex = DataGridView1.Columns("WORKORDER").Index Then
            Dim OutPath As String = "\\slfs01\shared\Doc-Pac-Ship\TravScan\"
            Dim LotNo As String = DataGridView1(e.ColumnIndex, e.RowIndex).Value
            Dim ShopNo As String = Mid(LotNo, 3, 5)
            Dim outfile As String = OutPath & ShopNo & "\" & LotNo & ".pdf"
            If Dir(outfile) = "" Then
                If FileIO.FileSystem.DirectoryExists(OutPath & ShopNo) Then
                    Process.Start(OutPath & ShopNo)
                Else
                    MsgBox("No travlers for " & ShopNo & "were found.")
                End If
            Else
                Process.Start(OutPath & ShopNo)
            End If
        End If

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.MouseUp
        If ComboBox1.SelectedItem.ToString = "DEPT" Then Exit Sub
        Dim A As Integer = 0
        Dim B As Integer
        Dim GoodToGoQty As Long
        Dim selectedCellCount As Integer = DataGridView1.GetCellCount(DataGridViewElementStates.Selected)
        'TextBox2.Text = "Count:   " & selectedCellCount
        Dim TOTQty As Long = 0
        Dim TOTVal As Long = 0

        For B = 0 To DataGridView1.Rows.Count
            For A = 0 To selectedCellCount - 1

                If Me.DataGridView1.SelectedCells(A).RowIndex = B Then
                        Dim qtyadd As Long = 0
                        Try : qtyadd = CLng(Me.DataGridView1(DataGridView1.Columns("QTY").Index, DataGridView1.SelectedCells(A).RowIndex).Value) : Catch : End Try
                        Dim valadd As Long = 0
                        Try : valadd = CLng(Me.DataGridView1(Me.DataGridView1.Columns("VALUE").Index, DataGridView1.SelectedCells(A).RowIndex).Value) : Catch : End Try
                        TOTQty = TOTQty + qtyadd
                        TOTVal = TOTVal + valadd


                    GoTo nextrow

                    End If

            Next A
nextrow:
        Next B
        TextBox2.Text = "    QTY:    " & FormatNumber(TOTQty, 0)
        If PPForm.DisplayAlloy Then
            TextBox2.Text = TextBox2.Text & "                        " & PPForm.alloy
            If Environment.UserName <> "DataCollSL" Then TextBox2.Text = TextBox2.Text & vbCrLf & "VALUE:  " & FormatCurrency(TOTVal, 2)
        End If
    End Sub

    Private Sub ShowTimelineToolStripMenuItem_Click(sender As Object, e As EventArgs)
        PPForm.CallXTL()
    End Sub

    Private Sub ShowOpenOrdersToolStripMenuItem_Click(sender As Object, e As EventArgs)
        PPForm.CallXOpen()
    End Sub

    Private Sub ViewWaxTechCardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewWaxTechCardToolStripMenuItem.Click
        If Not (PPForm.OpenTechCard(TextBox1.Text)) Then MsgBox("I CANT FIND THE CARD IN ASSY")
    End Sub

    Private Sub OPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OPToolStripMenuItem.Click
        DataGridView1.Columns(4).Visible = Not (DataGridView1.Columns(3).Visible)
        DataGridView1.Columns(5).Visible = Not (DataGridView1.Columns(4).Visible)
    End Sub

    Private Sub WORKCENTERToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WORKCENTERToolStripMenuItem.Click
        DataGridView1.Columns(6).Visible = Not (DataGridView1.Columns(5).Visible)
        DataGridView1.Columns(7).Visible = Not (DataGridView1.Columns(6).Visible)
    End Sub


    Private Sub ShowDetail_CheckedChanged(sender As Object, e As EventArgs) Handles ShowDetail.CheckedChanged
        If restoreorder Then UPDATEQUERYXR()
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        PPForm.CopyDataGridViewCells(DataGridView1, False)
        'RestoreColumnOrder()
    End Sub


    Private Sub HyperLinkFonts()
        For y = 0 To DataGridView1.RowCount - 1
            DataGridView1.Item(Me.DataGridView1.Columns("OP").Index, y).Value = CInt(DataGridView1.Item(Me.DataGridView1.Columns("OP").Index, y).FormattedValue)
            DataGridView1.Item(Me.DataGridView1.Columns("WORKORDER").Index, y).Style.ForeColor = Color.Blue
            DataGridView1.Item(Me.DataGridView1.Columns("WORKORDER").Index, y).Style.Font = New Font(DataGridView.DefaultFont, FontStyle.Underline)
        Next y

    End Sub

    Private Sub DataGridView1_Sorted(sender As Object, e As EventArgs) Handles DataGridView1.Sorted
        HyperLinkFonts()
    End Sub

    Private Sub CheckTravlersToolStripMenuItem_Click(sender As Object, e As EventArgs)
        CheckTravlers()
    End Sub

    Private Sub CheckTravlers()
        'My.Settings.MaxAge = 0
        'If ToolStripMenuItem6.Checked Then My.Settings.MaxAge = 100
        'If ToolStripMenuItem5.Checked Then My.Settings.MaxAge = 50
        'If ToolStripMenuItem4.Checked Then My.Settings.MaxAge = 25
        'If ToolStripMenuItem3.Checked Then My.Settings.MaxAge = 10
        'If ToolStripMenuItem1.Checked Then My.Settings.MaxAge = 5
        'My.Settings.Save()
        If ComboBox1.SelectedItem.ToString = "DEPT" Then Exit Sub
        Dim UnfoundFiles As String = ""
        Dim OutPath As String = "\\slfs01\shared\Doc-Pac-Ship\TravScan\" & TextBox1.Text
        If Len(TextBox1.Text) = 0 Then Exit Sub
        Dim FileList As String = ""
        If TextBox1.Text = "" Then Exit Sub
        If FileIO.FileSystem.DirectoryExists(OutPath) Then
            For Each fi In FileIO.FileSystem.GetDirectoryInfo(OutPath).GetFiles("*", IO.SearchOption.AllDirectories)
                FileList = FileList & fi.ToString
            Next
        Else
            FileList = ""
        End If
        DataGridView1.SelectAll()
        For row = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Item(DataGridView1.Columns("AGE").Index, row).Value.ToString <> "." Then
                If DataGridView1.Item(DataGridView1.Columns("AGE").Index, row).Value > My.Settings.MaxAge And MaxAge <> 0 Then DataGridView1.Rows(row).Visible = False
            End If
            ' Debug.Print(DataGridView1.Item(DataGridView1.Columns("WC").Index, row).Value.ToString)

            If InStr(DataGridView1.Item(DataGridView1.Columns("WC").Index, row).Value.ToString, "450") <> 0 Or Len(DataGridView1.Item(DataGridView1.Columns.Item("WC").Index, row).Value.ToString) = 0 Then
                Dim LotNo As String = DataGridView1.Item(DataGridView1.Columns.Item("WORKORDER").Index, row).Value
                Dim j As Integer = InStr(1, FileList, LotNo, CompareMethod.Text)
                Dim t As Integer = InStr(FileList, LotNo)
                If InStr(1, FileList, LotNo) = 0 Then
                    UnfoundFiles = UnfoundFiles & LotNo & vbCrLf & "       "
                    DataGridView1.Item(DataGridView1.Columns("OP_DESC").Index, row).Value = "NO TRAV FOUND"
                    DataGridView1.Item(DataGridView1.Columns("OP").Index, row).Value = "9999"
                Else
                    DataGridView1.Item(DataGridView1.Columns("OP_DESC").Index, row).Value = "TRAV SCANNED"
                    DataGridView1.Item(DataGridView1.Columns("OP").Index, row).Value = "10000"
                End If
            End If
        Next
        ' My.Settings.Save()

    End Sub

    Private Sub SaveColumnOrder()
        '  MsgBox(2)
        Try

            If My.Settings.XRColumnText Is Nothing Then My.Settings.XRColumnText = New System.Collections.Specialized.StringCollection
            '  My.Settings.XRColumnOrder.Clear()
            My.Settings.XRColumnText.Clear()
            Dim S As String = ""
            For Each col In DataGridView1.Columns

                Try
                    My.Settings.XRColumnText.Add(col.name.ToString & "'" & col.displayindex.ToString & "'" & col.index.ToString)
                    S = S & col.name.ToString & "'" & col.displayindex.ToString & "'" & col.index.ToString & vbCrLf
                Catch
                    MsgBox("NA1")
                End Try
            Next
            My.Settings.ShowWIPLots = SHOWLOTS.Checked
            '   If Environment.UserName = "PPrasinos" Then MsgBox(S)
            My.Settings.ShowShipLots = SHOWLOTS.Checked
            My.Settings.Save()

        Catch : End Try
        '  My.Settings.Upgrade()
    End Sub

    Private Sub RestoreColumnOrder()
        Try
            '   MsgBox(1)
            ' RemoveHandler DataGridView1.ColumnDisplayIndexChanged, AddressOf DataGridView1_ColumnDisplayIndexChanged
            'My.Settings.Reload()

            If Not My.Settings.UpgradeSettings Then
                My.Settings.Upgrade()
                My.Settings.UpgradeSettings = True
            End If
            Dim s As String = ""
            If Not My.Settings.XRColumnText Is Nothing Then
                For Each col In DataGridView1.Columns
                    col.visible = True
                    Dim cannotfind As Boolean = False
                    For Each line In My.Settings.XRColumnText
                        If IsNothing(col) Then Stop
                        If DataGridView1.Columns(Split(line, "'")(0)).Name = col.name Then
                            col.displayindex = Split(line, "'")(1)
                            s = s & line & vbCrLf
                            cannotfind = True
                        Else

                        End If
                    Next line
                    '   If cannotfind = False Then MsgBox(col.name)
                Next col

                'Next
                'For i = 0 To My.Settings.XRColumnText.Count - 1
                '    Try
                '        Dim y As Object = My.Settings.XRColumnText
                '    Dim thisone() As String = Split(My.Settings.XRColumnText(i), "'")
                '    DataGridView1.Columns(Split(My.Settings.XRColumnText(i), "'")(0)).DisplayIndex = Split(My.Settings.XRColumnText(i), "'")(1)
                '        Dim col As Object = DataGridView1.Columns(Split(My.Settings.XRColumnText(i), "'")(0))
                '        S = S & col.name.ToString & "'" & col.displayindex.ToString & "'" & col.index.ToString & vbCrLf
                '        '     DataGridView1.Columns(Split(My.Settings.XRColumnText(i), "'")(0)).Visible = True
                '    Catch
                '        MsgBox("NAA")
                '    End Try
                'Next
            End If
            '   If Environment.UserName = "PPrasinos" Then MsgBox(s)
            SHOWLOTS.Checked = My.Settings.ShowWIPLots
        Catch : End Try
        'AddHandler DataGridView1.ColumnDisplayIndexChanged, AddressOf DataGridView1_ColumnDisplayIndexChanged
    End Sub

    Private Sub GenerateTicketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateTicketToolStripMenuItem.Click
        Dim lf As String = "<br>" & Chr(10) & Chr(13)
        Dim MessageString As String = lf & lf & lf & lf & "Please see below for applicable workorders:" & lf
        Dim LogString As String = ""
        Dim ShopNum As String
        Dim Lots As String = ""

        For Each cell As DataGridViewCell In DataGridView1.SelectedCells
            If cell.ColumnIndex = DataGridView1.Columns("WORKORDER").Index Then
                LogString = LogString & "G" & vbTab & Now.ToString & vbTab & Environment.UserName & vbTab & cell.Value & vbTab & vbCrLf
                Lots = Lots & cell.Value & ";"
                MessageString = MessageString & cell.Value & lf
                ShopNum = DataGridView1.Item(DataGridView1.Columns("SN").Index, cell.RowIndex).Value
            End If
        Next

        MessageString = MessageString & "<font color='white'>" & "GS" & Now.ToBinary & "</font>"

        'class1.UpdateDbValue(Split(Lots, ";"), Now.ToString)

        Dim ShopFolder As String = "\\slfs01\shared\prasinos\8Ball\ShopIssues\" & ShopNum & "\"
        If Not FileIO.FileSystem.DirectoryExists(ShopFolder) Then FileIO.FileSystem.CreateDirectory(ShopFolder)
        FileIO.FileSystem.WriteAllText(ShopFolder & "Stamps.txt", LogString, True)
        class1.EmailFile(New List(Of String), {}, MessageString, ShopNum & " DOC issue: ", {"pprasinos"}, ShopFolder & Format(Now, "yyyyMMddHHmmss") & ".msg")

    End Sub

    Private Sub ShowTimelineToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ShowTimelineToolStripMenuItem.Click
        XTLForm.Show()
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStrip1.Opening
        PullLotHistoryToolStripMenuItem.Visible = False
        If DataGridView1.SelectedCells.Count = 1 Then
            If DataGridView1.SelectedCells(0).ColumnIndex = DataGridView1.Columns("WORKORDER").Index Then PullLotHistoryToolStripMenuItem.Visible = True
        End If
    End Sub


    Public Sub PullLotHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PullLotHistoryToolStripMenuItem.Click
        class1.PullHistory(DataGridView1.SelectedCells(0).Value)
    End Sub

    Private Sub CreateNofificationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateNofificationToolStripMenuItem.Click
        Dim lots As String = ""
        For Each cell As DataGridViewCell In DataGridView1.SelectedCells
            If cell.ColumnIndex = DataGridView1.Columns("WORKORDER").Index Then
                lots = lots & cell.Value & ","
            End If
        Next
        NotificationForm.Lots = lots
        NotificationForm.Show()
        NotificationForm.TextBoxEmail.Text = Environment.UserName & "@pccstructurals.com"
        NotificationForm.TopMost = True
    End Sub

    Private Sub XRLOT_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' FillLots(0, "")
        If Not restoreorder Then
            RestoreColumnOrder()
            restoreorder = True
            ShowDetail.Checked = False
        End If
        If Not XRlotInit Then
            XRlotInit = True
            If SHOWLOTS.Checked Then
                FillLots(1, TextBox1.Text)
            Else
                FillLots(0, TextBox1.Text)
            End If

        End If


    End Sub

    Private Sub CopyWHeadersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyWHeadersToolStripMenuItem.Click
        PPForm.CopyDataGridViewCells(DataGridView1, True)
        SaveColumnOrder()
    End Sub

    Private Sub ButtonMovementInq_Click(sender As Object, e As EventArgs) Handles ButtonMovementInq.Click

        If DataGridView1.SelectedCells.Count = 1 Then class1.PullHistory(DataGridView1.Item(DataGridView1.Columns("WORKORDER").Index, DataGridView1.SelectedCells(0).RowIndex).Value)
            If DataGridView1.RowCount = 0 Then class1.PullHistory("")

    End Sub

    Private Sub CheckBoxWCSum_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxWCSum.CheckedChanged
        UPDATEQUERYXR()
    End Sub

End Class