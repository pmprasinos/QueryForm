Imports System.Runtime.InteropServices
Imports System
Imports System.IO
Imports System.IO.FileStream
Imports System.Threading.Tasks
Imports System.Collections.Generic


Public Class PPForm

    Dim ShipmentsTable As New DataTable
    Dim OpensTable As New DataTable
    Dim ShipmentsDataAdapter As New SqlClient.SqlDataAdapter
    Dim OpensTableAdapter As New SqlClient.SqlDataAdapter
    Dim objConnCurr As New SqlClient.SqlConnection("Server=SLREPORT01; Database=WFLocal; persist security info=False; trusted_connection=Yes;")


    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        QueryTableForData()
    End Sub

    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Environment.UserName = "PPrasinos" Then MsgBox("phil")
        If My.Settings.UpgradeSettings Then
            If Environment.UserName = "PPrasinos" Then MsgBox("Settings Upgraded")
            My.Settings.Upgrade()
            My.Settings.UpgradeSettings = False
            My.Settings.Save()
        End If

        ShipmentsDataAdapter.SelectCommand = New SqlClient.SqlCommand
        ShipmentsDataAdapter.SelectCommand.Connection = objConnCurr
        DataGridView2.DataSource = ShipmentsTable

        ShareddbPath = "\\slfs01\Shared\prasinos\SOLines.accdb"
        If Environment.UserName = "PPRASINOS" Then ShareddbPath = Replace(ShareddbPath, "SOLines", "SOLines")
        Dim y As Object = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Microsoft\Office\Common\UserInfo", "UserName", "Another User")

        Try
            Dim timesincemin As Integer = (Now.Hour * 60) + Now.Minute
            timer1.Enabled = True
            timer1.Interval = 5 * 1000

            RegisterHotKey(Me.Handle, 100, MOD_ALT, Keys.Z)
            RegisterHotKey(Me.Handle, 200, MOD_ALT, Keys.C)
            NotifyIcon2.Icon = My.Resources.FAVICON
            NotifyIcon2.Visible = True
            NotifyIcon2.ContextMenuStrip = ContextMenuStrip1

            Dim UserPath As String = "\\slfs01\Shared\prasinos\mods\windowsforms\application files\users\users.txt"

            Me.DisplayAlloy = True
            If Environment.UserName <> "PPrasinos" Then
                Dim sb As New System.IO.StreamWriter(UserPath, True)
                sb.WriteLine(Environment.UserName & Chr(9) & Now & Chr(13))
                sb.Close()
            End If
            Me.Text = "Open Orders Search (" & CStr(FileIO.FileSystem.GetFileInfo(ShareddbPath).LastWriteTime.ToString) & ")"
            Me.LookUpValue.Text = "?????"
            ' Me.DataGridView1.Sort(DataGridView1.Columns(11), System.ComponentModel.ListSortDirection.Ascending)
            class1.UpdateData()
        Catch : End Try
        DataGridView2.DataSource = ShipmentsTable
        DataGridView1.DataSource = OpensTable
        RestoreColumnOrderShips(DataGridView1)
        RestoreColumnOrderOpens(DataGridView1)
    End Sub


    Public Sub QueryTableForData(Optional FocusForm As String = "OPENS")
        '  Dim j As Integer = Await ppform.copystat

        Dim CText As String

        FIVEDIGSHOP = "?????"
        If InStr(LookUpValue.Text, "Q") And Not (InStr(LookUpValue.Text, "Q0")) Then LookUpValue.Text = Replace(LookUpValue.Text, "Q", "Q0")
        If Len(LookUpValue.Text) = 4 Then LookUpValue.Text = "0" & LookUpValue.Text

        Dim CheckVal As String = LookUpValue.Text
        If CheckVal = "" Then CheckVal = "   "
        If Me.TabControl1.SelectedTab.Name = "TabPage1" Then
            OpensTable.Clear()

            OpensTableAdapter.SelectCommand = New SqlClient.SqlCommand
            OpensTableAdapter.SelectCommand.Connection = objConnCurr
            Try

                objConnCurr.Open()

                SQLGenerator.MakeXOpenQuery(OpensTableAdapter.SelectCommand, ComboBox1.Text, LookUpValue.Text, HideSPCLCheckBox.Checked)

                OpensTableAdapter.Fill(OpensTable)
            Catch
            Finally
                objConnCurr.Close()
            End Try
            Try
                If Me.DataGridView1.RowCount <> 0 Then
                    FIVEDIGSHOP = Me.DataGridView1(1, 0).Value
                    If Len(FIVEDIGSHOP) <> 5 And Me.DataGridView1.RowCount > 2 Then
                        FIVEDIGSHOP = Me.DataGridView1(1, 1).Value
                    End If
                Else
                    FIVEDIGSHOP = LookUpValue.Text
                End If
            Catch

            End Try
        Else


            RemoveHandler DataGridView2.SelectionChanged, AddressOf DataGridView2_SelectionChanged

            ShipmentsTable.Clear()

            objConnCurr.Open()


            Try
                ShipmentsDataAdapter.SelectCommand.Parameters.Clear()
                Dim bylot As Boolean
                If CheckBoxShowLots.Checked Then
                    bylot = True
                    DataGridView2.Columns("WORKORDERNO").Visible = True
                Else
                    bylot = False
                    DataGridView2.Columns("WORKORDERNO").Visible = False
                End If
                ComboBox1.Text = "ALL"
                SQLGenerator.MakeShipmentsQuery(ShipmentsDataAdapter.SelectCommand, bylot, LookUpValue.Text)




                ShipmentsDataAdapter.Fill(ShipmentsTable)
            Catch
            Finally


                objConnCurr.Close()
            End Try

            'If HideSPCLCheckBox.Checked Then
            '    Try
            '        For row = 0 To DataGridView1.RowCount - 1
            '            If Len(DataGridView2.Item(DataGridView2.Columns("INVOICE_NO").Index, row).Value.ToString) = 6 Then
            '                DataGridView2.Rows(row).Visible = False
            '            End If
            '        Next
            '    Catch
            '    End Try
            'End If


            Me.CheckBoxShowLots.Visible = True

            AddHandler DataGridView2.SelectionChanged, AddressOf DataGridView2_SelectionChanged

            Try
                If DataGridView2.RowCount <> 0 Then
                    FIVEDIGSHOP = DataGridView2.Item(DataGridView2.Columns("SN").Index, 0).Value
                Else
                    FIVEDIGSHOP = LookUpValue.Text
                End If
            Catch
            End Try
        End If
        If InStr(FIVEDIGSHOP, "?") > 0 Then FIVEDIGSHOP = LookUpValue.Text


        Me.WindowState = FormWindowState.Normal
        Dim OPENFORMS As String
        For Each FRM In Application.OpenForms
            OPENFORMS = OPENFORMS & FRM.name
        Next

        If InStr(OPENFORMS, "XRLot", CompareMethod.Text) > 0 Then
            XRLOT.TextBox1.Text = FIVEDIGSHOP
            CallXRLot()
        End If

        If InStr(OPENFORMS, "XTLForm", CompareMethod.Text) > 0 Then
            XTLForm.TextBox1.Text = FIVEDIGSHOP
            CallXTL()
        End If

        Me.Activate()
        LookUpValue.Focus()
        LookUpValue.SelectAll()

    End Sub



    Private Sub DataGridView1_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick

        Dim SO, SNG, ShopGroup, FPath, Shop As String
        If e.ColumnIndex = 1 Then
            Shop = Me.DataGridView1(1, e.RowIndex).Value
            Try
                If Mid(Shop, 1, 1) = "0" Then Shop = Mid(Shop, 2, 4)
            Catch
                Shop = LookUpValue.Text
            End Try
            If OpenTechCard(Shop) = False Then MsgBox("I CANT FIND THE CARD In ASSY")

        End If
        If e.ColumnIndex = 3 Then

            Shop = Replace(Me.DataGridView1(DataGridView1.Columns("PARTNO").Index, e.RowIndex).Value, "S", "")
            'If Mid(Shop, 1, 1) = "0" Then Shop = Mid(Shop, 2, 4)
            SO = Me.DataGridView1(DataGridView1.Columns("SALES_ORDER").Index, e.RowIndex).Value
            class1.SOFolderPopup(Shop, SO)
            'If IsNumeric(Shop) Then
            '    SNG = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(CStr(Shop + 1000000), 4), 2) & "000"
            '    ShopGroup = SNG & "-" & Microsoft.VisualBasic.Right(SNG + 100999, 5)
            'Else
            '    ShopGroup = "M0001-X9999\M0001-X9999"
            'End If
            'FPath = ("\\slfs01\Shared\SALES ORDERS\" & ShopGroup & "\" & Shop & "\" & SO & "\")
            'If Not FileIO.FileSystem.DirectoryExists(FPath) Then MsgBox("SO Not FOUND")
            'Dim openFileDialog1 As New OpenFileDialog()
            'Dim myStream As IO.Stream = Nothing
            'openFileDialog1.InitialDirectory = FPath
            'If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    Try
            '        myStream = openFileDialog1.OpenFile()
            '        System.Diagnostics.Process.Start(openFileDialog1.FileName)
            '        If (myStream IsNot Nothing) Then
            '            ' Insert code to read the stream here. 
            '        End If
            '    Catch Ex As Exception
            '        MessageBox.Show("Cannot read file from disk. Original Error: " & Ex.Message)
            '    Finally
            '        ' Check this again, since we need to make sure we didn't throw an exception on open. 
            '        If (myStream IsNot Nothing) Then
            '            myStream.Close()
            '        End If
            '    End Try
            'End If
        End If
    End Sub




    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        SaveColumnOrderOpens(DataGridView1)
        SaveColumnOrderShips(DataGridView2)
        e.Cancel = False
        UnregisterHotKey(Me.Handle, 100)
        UnregisterHotKey(Me.Handle, 200)

    End Sub

    Private Sub NotifyIcon2_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon2.DoubleClick
        class1.BringUpActiveForms()
    End Sub

    Private Async Function CopyFileAsync(FromPath As String, Topath As String, TimeOut As Integer) As Task(Of Integer)

        Using SourceStream As FileStream = File.Open(FromPath, FileMode.Open)
            Using DestinationStream As FileStream = File.Create(Topath)
                Await SourceStream.CopyToAsync(DestinationStream)
            End Using
        End Using
        Return 1
    End Function



    Private Sub ExitPPFormToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitPPFormToolStripMenuItem.Click
        TimeToClose = True

        Me.Close()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        Dim i As Integer = 0
        Dim j As Integer
        Dim selectedCellCount As Integer = DataGridView1.GetCellCount(DataGridViewElementStates.Selected)
        TextBox2.Text = "Count:   " & selectedCellCount
        Dim OpenQty As Long
        Dim ExtVal As Decimal
        For j = 0 To DataGridView1.Rows.Count + 1
            For i = 0 To selectedCellCount - 1
                If Me.DataGridView1.SelectedCells(i).RowIndex = j Then
                    If Me.DataGridView1(DataGridView1.Columns("QTY_DUE").Index, DataGridView1.SelectedCells(i).RowIndex).Value <> 0 Then OpenQty = OpenQty + CLng(Me.DataGridView1(DataGridView1.Columns("QTY_DUE").Index, DataGridView1.SelectedCells(i).RowIndex).Value)
                    If Me.DataGridView1(DataGridView1.Columns("DUE_VALUE").Index, DataGridView1.SelectedCells(i).RowIndex).Value <> vbNull Then ExtVal = ExtVal + CDec(Me.DataGridView1(DataGridView1.Columns("DUE_VALUE").Index, DataGridView1.SelectedCells(i).RowIndex).Value)
                    GoTo nextrow
                End If
            Next i
nextrow:
        Next j
        TextBox1.Text = "Open:    " & OpenQty & ControlChars.NewLine & "Value:  $" & Format(ExtVal, "n2")
    End Sub



    Sub CallXOpen()
        Me.WindowState = FormWindowState.Minimized
        Me.WindowState = FormWindowState.Normal
        Me.Activate()
        Me.Focus()
    End Sub

    Sub CallXRLot()
        Dim OPENFORMS As String
        For Each FRM In Application.OpenForms
            OPENFORMS = OPENFORMS & FRM.name
        Next

        If InStr(OPENFORMS, "XRLot", CompareMethod.Text) = 0 Then
            XRLOT.Show()
        End If

        Dim shop As String

        'XRLOT.Activate()
        'class1.XRVisible = True

        If TabControl1.SelectedIndex = 0 Then
            CheckBoxShowLots.Visible = False
            If DataGridView1.RowCount <> 0 Then
                shop = Me.DataGridView1(DataGridView1.Columns("PARTNO").Index, 0).Value
            Else
                shop = "????"
            End If
        ElseIf TabControl1.SelectedIndex = 1 Then
            If DataGridView2.RowCount <> 0 Then
                shop = Me.DataGridView2(DataGridView2.Columns("SN").Index, 0).Value
            Else
                shop = "????"
            End If


        End If

        If shop = "????" Then shop = LookUpValue.Text
        If InStr(shop, "S") = 6 Then shop = Replace(shop, "S", "")

        If shop = "" Then shop ="????"
        If XRLOT.TextBox1.Text <> shop Then
            XRLOT.TextBox1.Text = shop
        End If

        XRLOT.UPDATEQUERYXR()

    End Sub

    Sub CallXTL()
        Dim OPENFORMS As String
        For Each FRM In Application.OpenForms
            OPENFORMS = OPENFORMS & FRM.name
        Next

        If InStr(OPENFORMS, "XTLForm", CompareMethod.Text) = 0 Then
            XTLForm.Show()
        End If

        Dim shop As String
        Try
            If DataGridView1.RowCount <> 0 Then
                shop = Me.DataGridView1(1, 0).Value
            Else
                shop = "?????"
            End If
            If XTLForm.XTLShop.Text <> shop Then
                XTLForm.XTLShop.Text = shop
            End If
            XTLForm.QueryXTL()
        Catch ex As Exception
            'XTLForm.SOLinesDataSet.Clear()
            XTLForm.Visible = True
        End Try

    End Sub

    Function OpenTechCard(shop As String) As Boolean
        Dim FPATH As String
        Dim FName As String
        Dim dirInfo As System.IO.DirectoryInfo = FileIO.FileSystem.GetDirectoryInfo("\\SLFS01\Tech Cards\Tech Card Manager\Wax\Assembly")
        Dim f() As System.IO.FileInfo = dirInfo.GetFiles("*" & shop & "*")
        If f.Length > 0 Then
            FName = f(f.Length - 1).ToString

        End If

        FName = Dir("\\slfs01\tech cards\Tech Card Manager\Wax\Assembly\*" & shop & "*")
        If FileIO.FileSystem.FileExists("\\slfs01\tech cards\Tech Card Manager\Wax\Assembly\" & FName) Then
            FPATH = "\\slfs01\tech cards\Tech Card Manager\Wax\Assembly\" & FName
        ElseIf Dir("\\SLFS01\Tech Cards\Old TECH CARD_PDF and TRAV_SKETCH\Assy Tech Cards (New)\" & shop & "*") <> "" Then
            FPATH = "\\SLFS01\Tech Cards\Old TECH CARD_PDF and TRAV_SKETCH\Assy Tech Cards (New)\" & Dir("\\SLFS01\TECH CARDS\oLD tech card_pdf AND trav_sketch\Assy Tech Cards (New)\" & shop & "*")
        Else
            FName = Dir("\\slfs01\tech cards\Old TECH CARD_PDF\Assy Tech Cards (New)\" & shop & "*")
            FPATH = "\\slfs01\tech cards\Old TECH CARD_PDF\Assy Tech Cards (New)\" & FName
            If Not FileIO.FileSystem.FileExists(FPATH) Then
                FName = Dir("\\SLFS01\Tech Cards\Tech Card Manager\Traveler Sketch\trvsk." & shop & "*")
                FPATH = "\\SLFS01\Tech Cards\Tech Card manager\Traveler Sketch\" & FName
            End If
        End If
        If FileIO.FileSystem.FileExists(FPATH) Then
            Process.Start(FPATH)
            FPATH = ""
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub ViewWaxTechCardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewWaxTechCardToolStripMenuItem.Click
        Dim SHOP As String
        Try
            SHOP = Me.DataGridView1(DataGridView1.Columns("PARTNO").Index, DataGridView1.SelectedCells(0).RowIndex).Value
        Catch ex As Exception
            SHOP = LookUpValue.Text
        End Try

        If OpenTechCard(SHOP) = False Then MsgBox("I CANT FIND THE CARD IN ASSY")
    End Sub

    Private Sub KeepInFrontToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles KeepInFrontToolStripMenuItem.Click

        Me.KeepInFrontToolStripMenuItem.Checked = Not (Me.KeepInFrontToolStripMenuItem.Checked)
        Me.TopMost = KeepInFrontToolStripMenuItem.Checked
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        QueryTableForData()
    End Sub

    Private Sub DataGridView2_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView2.SelectionChanged
        Dim i As Integer = 0
        Dim j As Integer
        Dim selectedCellCount As Integer = DataGridView2.GetCellCount(DataGridViewElementStates.Selected)
        TextBox2.Text = "Count:   " & selectedCellCount
        Dim OpenQty As Long
        Dim ExtVal As Decimal
        For j = 0 To DataGridView2.Rows.Count + 1
            For i = 0 To selectedCellCount - 1
                If Me.DataGridView2.SelectedCells(i).RowIndex = j Then
                    Dim PriceColumn As Integer = DataGridView2.Columns("PRICE").Index
                    Dim QtyColumn As Integer = DataGridView2.Columns("QTY_SHPPED").Index
                    Dim RowSelected As Integer = DataGridView2.SelectedCells(i).RowIndex
                    If Me.DataGridView2(QtyColumn, RowSelected).Value <> vbNull Or Me.DataGridView2(QtyColumn, RowSelected).Value > 0 Then OpenQty = OpenQty + CLng(Me.DataGridView2(QtyColumn, RowSelected).Value)
                    If Me.DataGridView2(PriceColumn, RowSelected).Value <> vbNull Then ExtVal = ExtVal + (CDec(Me.DataGridView2(PriceColumn, RowSelected).Value) * CDec(Me.DataGridView2(QtyColumn, RowSelected).Value))
                    GoTo nextrow
                End If
            Next i
nextrow:
        Next j
        TextBox1.Text = "Open:    " & OpenQty & ControlChars.NewLine & "Value:  $" & Format(ExtVal, "n2")
    End Sub

    Public Shared Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer1.Tick
        timer1.Interval = 5 * 60 * 1000
        class1.UpdateData()
    End Sub


    Public Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Text = "UPDATING..."
        Dim ThisUserDocs As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        ThisUserDocs = Replace(ThisUserDocs, "mreyes", "mgonzales")
        Me.Cursor = Cursors.WaitCursor
        'FileIO.FileSystem.DeleteDirectory(ThisUserDocs & "\SalesTrackData", FileIO.DeleteDirectoryOption.DeleteAllContents)

        FileIO.FileSystem.CopyFile(ShareddbPath, ThisUserDocs & "\SalesTrackData\SalesInfo.accdb", True)


        Me.Cursor = Cursors.Default
        Me.Text = "Open Orders Search (" & CStr(FileIO.FileSystem.GetFileInfo(ThisUserDocs & "\SALESTRACKDATA\SALESINFO.ACCDB").LastWriteTime.ToString) & ")"
    End Sub

    Private Sub HideSPCLCheckBox_CheckStateChanged(sender As Object, e As EventArgs) Handles HideSPCLCheckBox.CheckStateChanged
        QueryTableForData()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowLots.CheckedChanged
        QueryTableForData()
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentDoubleClick
        Dim SOCol As Integer = DataGridView2.Columns("SO").Index
        Dim SNCol As Integer = DataGridView2.Columns("SN").Index
        Dim RowNum As Integer = e.RowIndex
        Dim DGVal As String

        If DataGridView2.Item(SNCol, RowNum).Value = "SPCL" Or DataGridView2.Item(SNCol, RowNum).Value = "TOOL" Or DataGridView2.Item(SNCol, RowNum).Value = "MISC" Then
            SNCol = DataGridView2.Columns("PARTNODataGridViewTextBoxColumn").Index
        End If

        If e.ColumnIndex = SOCol Then

            class1.SOFolderPopup(DataGridView2.Item(SNCol, RowNum).Value, DataGridView2.Item(SOCol, RowNum).Value)
        End If
        If e.ColumnIndex = SNCol Then

            OpenTechCard(DataGridView2.Item(e.ColumnIndex, e.RowIndex).Value)
        End If
        If e.ColumnIndex = DataGridView2.Columns("WEIGH_BILL").Index Then
            TRACKURL = "nothing"

            DGVal = DataGridView2(e.ColumnIndex, e.RowIndex).Value
            DGVal = Replace(DGVal, " ", "")
            Dim lendg As Integer = Len(DGVal)
            DGVal = Replace(DGVal, "-", "")

            If lendg = 12 Then
                TRACKURL = "https://www.fedex.com/Tracking?action=track&tracknumbers=" & DGVal
            ElseIf lendg = 10 Then
                TRACKURL = "https://www.dhl.com/content/g0/en/express/tracking.shtml?brand=DHL&AWB=" & DGVal
            Else
                TRACKURL = "https://wwwapps.ups.com/WebTracking/track?track=yes&trackNums=" & DGVal
            End If

            Process.Start("C:\Program Files\Internet Explorer\iexplore.exe", TRACKURL)
        End If

    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        If Me.DataGridView1.GetCellCount(
             DataGridViewElementStates.Selected) > 0 Then

            Try

                ' Add the selection to the clipboard.
                Clipboard.SetDataObject(
                    Me.DataGridView1.GetClipboardContent())

                ' Replace the text box contents with the clipboard text.
                Me.TextBox1.Text = Clipboard.GetText()

            Catch ex As System.Runtime.InteropServices.ExternalException
                Me.TextBox1.Text =
                    "The Clipboard could not be accessed. Please try again."
            End Try

        End If

    End Sub

    Public Sub CopyToClipboard()

        Dim dataObj As DataObject = DataGridView1.GetClipboardContent()
        If dataObj IsNot Nothing Then
            Clipboard.SetDataObject(dataObj)
        End If
    End Sub

    Private Sub HideTrayIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideTrayIconToolStripMenuItem.Click
        Me.NotifyIcon2.Visible = Not Me.NotifyIcon2.Visible

    End Sub

    Private Sub PSQLSearch(SearchTerm As String)
        Dim j As Integer = PSQLArray.Length / 2 : Dim x As Integer = 0
        Dim ResultList As New List(Of Double)
        Dim pn As String = ""
        Do While x < j - 1
            x = x + 1
            'End If
            ResultList.Add(class1.Dicef(SearchTerm, PSQLArray(1, x)))
            If SearchTerm = PSQLArray(0, x) Then pn = PSQLArray(1, x)
        Loop
        Dim longresult(3, 5) As String
        Dim u As Integer = 0
        For x = 0 To 4
            u = ResultList.IndexOf(ResultList.Max)
            longresult(0, x) = x + 1
            longresult(1, x) = PSQLArray(0, u + 1)
            longresult(2, x) = PSQLArray(1, u + 1)
            longresult(3, x) = ResultList(u)
            PSQLForm.DataGridView1.Rows.Add(longresult(3, x), longresult(0, x), longresult(1, x), longresult(2, x))
            ResultList(u) = 0
        Next
        If pn <> "" Then PSQLForm.DataGridView1.Rows.Add(1, "SN", SearchTerm, pn)

        PSQLForm.Show()
        PSQLForm.Focus()
    End Sub




    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PSQLSearch(LookUpValue.Text)
    End Sub

    Private Sub ViewQuotesFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewQuotesFolderToolStripMenuItem.Click
        Dim SHOP As String
        Try
            If Len(Me.LookUpValue.Text) = 5 Then
                SHOP = LookUpValue.Text

            ElseIf TabControl1.SelectedIndex = 1 Then
                SHOP = Me.DataGridView2(DataGridView2.Columns("SN").Index, 0).Value
            Else

                SHOP = Me.DataGridView1(DataGridView1.Columns("SHOP").Index, 0).Value
            End If


        Catch
            SHOP = LookUpValue.Text
        End Try
        If InStr(" " & SHOP, 1) = " 0" Then SHOP = Replace(" " & SHOP, " 0", "")

        Try
            'Process.Start("\\slfs01\shared\sales\quotes\byshop\" & SHOP)
            Dim openFileDialog1 As New OpenFileDialog()

            openFileDialog1.InitialDirectory = "\\slfs01\shared\sales\quotes\byshop\" & SHOP
            If Dir("\\slfs01\shared\sales\quotes\byshop\" & SHOP & "\") = "" Then openFileDialog1.InitialDirectory = "\\slfs01\shared\sales\quotes\byshop\"
            If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                System.Diagnostics.Process.Start(openFileDialog1.FileName)
            End If
        Catch ex As Exception
            MsgBox("No quote folder found for shop: " & SHOP)
        End Try

    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case (id.ToString)
                Case "100"
                    class1.BringUpActiveForms()
                    QueryTableForData()
                    LookUpValue.Focus()
                Case "200"
                    Dim selectedtext As String
                    selectedtext = My.Computer.Clipboard.GetText(1)
                    LookUpValue.Text = Replace(Replace(Trim(My.Computer.Clipboard.GetText(1)), Chr(10), ""), Chr(13), "")
                    class1.BringUpActiveForms()
                    QueryTableForData()
                    LookUpValue.Focus()
            End Select
        End If

        MyBase.WndProc(m)

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent1()
        components = New System.ComponentModel.Container()
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Text = "Form1"
    End Sub


    <DllImport("User32.dll")> Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    End Function


    <DllImport("User32.dll")> Public Shared Function UnregisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    End Function

    Private Sub SaveColumnOrderOpens(grid1 As DataGridView)
        If My.Settings.OpenOrdersColumnOrder Is Nothing Then
            My.Settings.OpenOrdersColumnOrder = New System.Collections.Specialized.StringCollection()
        End If
        My.Settings.OpenOrdersColumnOrder.Clear()
        For Each col In grid1.Columns
            Debug.Print(col.index & "   " & col.name & "    " & col.displayindex)
            My.Settings.OpenOrdersColumnOrder.Add(col.DisplayIndex.ToString())
        Next
        My.Settings.ShowShipLots = Me.CheckBoxShowLots.Checked
        My.Settings.ShowSpcl = Not Me.HideSPCLCheckBox.Checked
        My.Settings.Save()
        My.Settings.Upgrade()
    End Sub

    Private Sub RestoreColumnOrderOpens(ByVal WhichGrid As DataGridView)
        Me.CheckBoxShowLots.Checked = My.Settings.ShowShipLots
        Me.HideSPCLCheckBox.Checked = My.Settings.ShowSpcl

        Dim BigNumberArray(My.Settings.OpenOrdersColumnOrder.Count - 1) As Decimal

        If Not My.Settings.OpenOrdersColumnOrder Is Nothing Then
            For i = 0 To My.Settings.OpenOrdersColumnOrder.Count - 1
                Dim IndexPart As Integer = CInt(My.Settings.OpenOrdersColumnOrder(i))
                WhichGrid.Columns(i).DisplayIndex = IndexPart
            Next
        End If
    End Sub

    Private Sub SaveColumnOrderShips(grid1 As DataGridView)

        If My.Settings.ShipmentsColumnOrder Is Nothing Then
            My.Settings.ShipmentsColumnOrder = New System.Collections.Specialized.StringCollection()
        End If
        My.Settings.ShipmentsColumnOrder.Clear()
        For Each col In grid1.Columns
            'Debug.Print(col.index & "   " & col.name & "    " & col.displayindex)
            My.Settings.ShipmentsColumnOrder.Add(col.DisplayIndex.ToString())
        Next
        My.Settings.Save()
        My.Settings.Upgrade()
    End Sub

    Private Sub RestoreColumnOrderShips(ByVal WhichGrid As DataGridView)
        Dim BigNumberArray(My.Settings.ShipmentsColumnOrder.Count - 1) As Decimal
        If Not My.Settings.ShipmentsColumnOrder Is Nothing Then
            For i = 0 To My.Settings.ShipmentsColumnOrder.Count - 1
                Dim IndexPart As Integer = CInt(My.Settings.ShipmentsColumnOrder(i))
                WhichGrid.Columns(i).DisplayIndex = IndexPart
            Next
        End If
    End Sub

    Private Sub CopyWHeadersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyWHeadersToolStripMenuItem.Click
        If TabControl1.SelectedIndex = 0 Then CopyDataGridViewCells(DataGridView1, True)
        If TabControl1.SelectedIndex = 1 Then CopyDataGridViewCells(DataGridView2, True)
    End Sub

    Public Shared Sub CopyDataGridViewCells(dg As DataGridView, Optional IncludeHeaders As Boolean = False)
        dg.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        If dg.GetCellCount(DataGridViewElementStates.Selected) > 0 Then
            Try
                Clipboard.SetDataObject(dg.GetClipboardContent())
            Catch ex As System.Runtime.InteropServices.ExternalException

            End Try

        End If
        dg.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
    End Sub

    Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        '    Try
        CallXRLot()
            '  Catch EX As Exception
        '  MsgBox(EX.Message)
        '  End Try

    End Sub


    Private Sub ViewWIPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewWIPToolStripMenuItem.Click
        CallXRLot()
    End Sub

    Private Sub ViewTimelineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewTimelineToolStripMenuItem.Click

        CallXTL()
    End Sub



End Class

