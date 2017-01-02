﻿'Imports System.Runtime.InteropServices
'Imports System.Windows.Forms
Imports Microsoft.Office.Interop
Imports System.Net
Imports System.Net.NetworkInformation
Imports System
Imports System.Runtime.InteropServices
Imports System.ComponentModel

Class class1

    Public Shared ClassID As Int16
    Public Downloading As Boolean = False
    Public FileNum As Integer
    Public tmp = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\test.temp" 'O.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "snafu.fubar")
    Public Shared TimeToDownload As String
    Public TIMESTARTED As String
    Dim GlobalFile As String
    Dim wc As WebClient


    Sub SpeedTest(MB As Integer)
        If Downloading Then Exit Sub
        Downloading = True
        wc = New WebClient
        AddHandler wc.DownloadProgressChanged, AddressOf wc_ProgressChanged
        AddHandler wc.DownloadFileCompleted, AddressOf wc_DownloadDone
        Dim y As New Random
        Dim u As Integer
        u = y.Next(0)
        FileNum = u

        If MB = 100 Then
            '10, 20, 50, 100, 200, 512
            If u = 0 Then
                wc.DownloadFileAsync(New Uri("http://download.thinkbroadband.com/" & MB & "MB.zip"), tmp, Stopwatch.StartNew)
            ElseIf u = 1 Then
                wc.DownloadFileAsync(New Uri("http://speedtest.wdc01.softlayer.com/downloads/test" & MB & ".Zip"), tmp, Stopwatch.StartNew)
            ElseIf u = 2 Then
                wc.DownloadFileAsync(New Uri("http://speedtest.tele2.net/100MB.zip" & MB & ".Zip"), tmp, Stopwatch.StartNew)
            ElseIf u = 3 Then
                wc.DownloadFileAsync(New Uri("http://speedtest.fremont.linode.com/100MB-fremont.bin" & MB & ".Zip"), tmp, Stopwatch.StartNew)
            ElseIf u = 4 Then
                wc.DownloadFileAsync(New Uri("http://speedtest.dallas.linode.com/100MB-dallas.bin" & MB & ".Zip"), tmp, Stopwatch.StartNew)


            End If
        Else
            wc.DownloadFileAsync(New Uri("http://download.thinkbroadband.com/" & MB & "MB.zip"), tmp, Stopwatch.StartNew)
        End If
    End Sub

    Public Sub wc_DownloadDone(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs)
        Downloading = False
        TimeToDownload = (DirectCast(e.UserState, Stopwatch).ElapsedMilliseconds / 1000).ToString
        ClassID = 0
        ' PPForm.ActiveForm.Text = "done in " & TimeToDownload
        'Debug.Print(Dir("\\slfs01\shared\prasinos\SpeedTest\MACHINES\" & Environment.MachineName & "\test*"))
        'FileIO.FileSystem.DeleteFile("\\slfs01\shared\prasinos\SpeedTest\MACHINES\" & Environment.MachineName & "\" & Dir("\\slfs01\shared\prasinos\SpeedTest\MACHINES\" & Environment.MachineName & "\RUNNING*"))
    End Sub

    Public Sub wc_ProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
        If (DirectCast(e.UserState, Stopwatch).ElapsedMilliseconds / 1000) > 600 Then
            RemoveHandler wc.DownloadProgressChanged, AddressOf wc_ProgressChanged
            Downloading = False
            TimeToDownload = "99999"
            Dim texttowrite As String
            texttowrite = texttowrite & " | " & Now() & " | (" & FileNum & ") |"
            texttowrite = texttowrite & FileIO.FileSystem.GetFileInfo(tmp).Length & " | "
            texttowrite = texttowrite & Environment.UserName & "@" & Environment.MachineName & " | " & "TIMEOUT"
            Try : FileIO.FileSystem.WriteAllText("\\slfs01\shared\prasinos\SpeedTest\" & GlobalFile & "MBTest.txt", texttowrite, True) : Catch : End Try
        End If

    End Sub

    Public Sub RunSpeedTests(state As Object, FileSize As Integer)
        ' Try
        '  Dim FileSizes As Integer()
        '   FileSizes = {100, 10, 1}
        ' For Each FileSize In FileSizes
        TimeToDownload = ""
        Debug.Print(FileSize)
        GlobalFile = FileSize.ToString
        TIMESTARTED = Now
        SpeedTest(FileSize)
        Do While Downloading
            Threading.Thread.Sleep(100)
        Loop
        If Downloading = False Then

            Dim ap As String = ""

            Dim ips As New List(Of String)
            Dim addresses() As Net.IPAddress = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName).AddressList
            For i As Integer = addresses.Count / 2 To addresses.Count - 1
                ap = ap & "[" & addresses(i).ToString & "]"
                Debug.Print(ap)
            Next

            Dim y As Boolean = False
            Dim texttowrite As String = TIMESTARTED
            texttowrite = texttowrite & " | " & Now() & " | (" & FileNum & ") |"
            texttowrite = texttowrite & FileIO.FileSystem.GetFileInfo(tmp).Length & " | "
            texttowrite = texttowrite & Environment.UserName & "@" & Environment.MachineName & " | " & TimeToDownload & " | " & ap & " |  PING: "

            texttowrite = texttowrite & GetPingMs("www.google.com") & " | " & GetPingMs("www.google.com") & " | " & GetPingMs("www.google.com") & " | " & GetPingMs("www.google.com") & " | " &
                                               GetPingMs("www.google.com") & " | " & GetPingMs("www.google.com") & " | " & GetPingMs("www.google.com") & vbCrLf
            Dim r As New Random
            FileIO.FileSystem.DeleteFile("\\slfs01\shared\prasinos\SpeedTest\MACHINES\" & Environment.MachineName & "\test" & FileSize & ".txt")
            Dim c As Integer = CInt(r.NextDouble() * 2 * 60 * 1000)
            Threading.Thread.Sleep(c)

            Try
                FileIO.FileSystem.WriteAllText("\\slfs01\shared\prasinos\SpeedTest\" & Environment.MachineName.ToString & " " & FileSize.ToString & "MBTest.txt", texttowrite, True)
                FileIO.FileSystem.WriteAllText("\\slfs01\shared\prasinos\SpeedTest\" & FileSize.ToString & "MBTest.txt", texttowrite, True)
                y = True
            Catch e As Exception

                Exit Sub
            End Try
            Return
        End If

    End Sub

    Public Function GetPingMs(ByRef hostNameOrAddress As String) As String
        Threading.Thread.Sleep(500)
        Dim ping As New System.Net.NetworkInformation.Ping
        Try
            Return ping.Send(hostNameOrAddress, 1000).RoundtripTime
        Catch ex As PingException
            Return 0
        End Try
    End Function

    Public Shared Sub EmailFile(FileNames As List(Of String), Recipients As String(), MessageBody As String, Subject As String, Optional CC As String() = Nothing, Optional SaveAsPath As String = Nothing, Optional Send As Boolean = False, Optional PCC As Boolean = True)

        Dim OutLookApp As New Outlook.Application
        Dim Mail As Outlook.MailItem = OutLookApp.CreateItem(Outlook.OlItemType.olMailItem)

        For Each File In FileNames
            Try
                Mail.Attachments.Add(File)
            Catch
            End Try
        Next File
        Dim mailRecipient As Outlook.Recipient
        For Each address In Recipients
            If PCC Then address = address & "@pccstructurals.com"
            mailRecipient.Resolve()
            If Not mailRecipient.Resolved Then MsgBox(address)
        Next

        If Not IsNothing(CC) Then
            For Each address In CC
                address = address & "@pccstructurals.com"
                Mail.CC = Mail.CC & address & ";"
            Next
        End If
        Mail.Recipients.ResolveAll()

        Mail.HTMLBody = MessageBody
        Mail.Subject = Subject
        If Not IsNothing(SaveAsPath) Then Mail.SaveAs(SaveAsPath)

        Mail.Display()
    End Sub


    Public Shared Function Dicef(stTwo As String, stOne As String) As Double
        Dim nx As HashSet(Of String) = New HashSet(Of String)
        Dim ny As HashSet(Of String) = New HashSet(Of String)
        For i = 0 To Len(stOne) - 2
            Dim x1 As Char = stOne.Chars(i)
            Dim x2 As Char = stOne.Chars(i + 1)
            Dim temp As String = x1.ToString + x2.ToString
            nx.Add(temp)
        Next

        For j = 0 To Len(stTwo) - 2
            Dim y1 As Char = stTwo.Chars(j)
            Dim y2 As Char = stTwo.Chars(j + 1)
            Dim temp As String = y1.ToString + y2.ToString
            ny.Add(temp)
        Next j

        Dim intersection As New HashSet(Of String)(nx)
        intersection.IntersectWith(ny)
        Dim dbOne As Double = intersection.Count
        Return (2 * dbOne) / (nx.Count + ny.Count)
    End Function


    Public Shared Sub BringUpActiveForms()
        Dim Openforms As String
        For Each frm In My.Application.OpenForms
            Openforms = Openforms & frm.Name
        Next

        If InStr(Openforms, "XRLot") > 0 Then
            XRLOT.WindowState = FormWindowState.Minimized
            XRLOT.Show()
            XRLOT.WindowState = FormWindowState.Normal
        End If

        If InStr(Openforms, "XTLForm") > 0 Then
            XTLForm.WindowState = FormWindowState.Minimized
            XTLForm.Show()
            XTLForm.WindowState = FormWindowState.Normal
        End If

        PPForm.WindowState = FormWindowState.Minimized
        PPForm.WindowState = FormWindowState.Normal
        PPForm.Show()
        PPForm.LookUpValue.Focus()


        'If Not (class1.XOpenVisible) And Not (class1.XTLVisible) And Not (class1.XRVisible) Then CallDefaultForm()
    End Sub

    Public Shared Sub UpdateData()

        'Dim ShareddbPath As String = "\\slfs01\shared\prasinos\SOLines.accdb"
        '' If Environment.UserName = "PPrasinos" Then ShareddbPath = "\\slfs01\shared\prasinos\SOLines1.accdb"

        'Cursor.Current = Cursors.WaitCursor

        ''If Environment.UserName = "PPRASINOS" Then
        ''    ShareddbPath = Replace(ShareddbPath, "SOLines", "SOLines1")
        ''End If

        'If Not FileIO.FileSystem.DirectoryExists("\\slfs01\shared\prasinos\HOLDPULLS") Then

        '    'If Environment.UserName = "PPRASINOS" Then GoTo skip
        '    Dim TimeDifference As New Random
        '    Try
        '        Dim ThisUserDocs As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        '        ThisUserDocs = Replace(ThisUserDocs, "mreyes", "mgonzales")
        '        If Not FileIO.FileSystem.DirectoryExists(ThisUserDocs & "\SalesTrackData") Then
        '            FileIO.FileSystem.CreateDirectory(ThisUserDocs & "\SalesTrackData")
        '        End If

        '        If FileIO.FileSystem.FileExists(ThisUserDocs & "\SalesTrackData\SalesInfo.accdb") Then

        '            If DateTime.Compare(Now, FileIO.FileSystem.GetFileInfo(ThisUserDocs & "\SalesTrackData\SalesInfo.accdb").LastWriteTime.AddSeconds(TimeDifference.Next(1, 30))) > 0 Then
        '                Dim LastUpdate As Date = FileIO.FileSystem.GetFileInfo(ShareddbPath).LastWriteTime
        '                Dim LastUpdateInt As Integer = (LastUpdate.DayOfYear * 24 * 60) + (Hour(LastUpdate) * 60) + Minute(LastUpdate)
        '                Dim LastPull As Date = FileIO.FileSystem.GetFileInfo(ThisUserDocs & "\SALESTRACKDATA\SALESINFO.ACCDB").LastWriteTime
        '                Dim LastPullInt As Integer = (LastPull.DayOfYear * 24 * 60) + (Hour(LastPull) * 60) + Minute(LastPull)

        '                If LastUpdateInt - LastPullInt > 5 Then
        '                    '     PPForm.Text = "UPDATING..."
        '                    ' FileIO.FileSystem.CopyFile(ShareddbPath, ThisUserDocs & "\SalesTrackData\SalesInfo.accdb", True)
        '                End If
        '            End If
        '        Else
        '            '  PPForm.Text = "UPDATING..."
        '            ' FileIO.FileSystem.CopyFile(ShareddbPath, ThisUserDocs & "\SalesTrackData\SalesInfo.accdb", True)
        '        End If
        '        '     PPForm.Text = "Open Orders Search (" & CStr(FileIO.FileSystem.GetFileInfo(ThisUserDocs & "\SALESTRACKDATA\SALESINFO.ACCDB").LastWriteTime.ToString) & ")"

        '    Catch ex As Exception
        '        ' If Environment.UserName = "MREYES" Or Environment.UserName = "PPRASINOS" Then MsgBox(ex.Message & "  found in code on 1:")
        '        '                PPForm.Text = PPForm.Text & " OFFLINE"

        '    End Try
        'End If
        'Cursor.Current = Cursors.Default
skip:
    End Sub

    Public Shared Function GetUserPasswordandFex() As String()
        Dim h As New Random

        Dim Usernames() As String = {"pprasinos", "hfaizi", "mreyes", "MALMARAZ", "MARJMAND", "HYANG", "GWONG", "VDELACRUZ", "JTIBAYAN", "JSOLIS", "ASINGH", "GREYES", "JPIMENTEL", "TOSULLIVAN", "MMARTIN", "VLOPEZ", "SLI", "JIMPERIAL", "JHERNANDEZ", "FHARO", "CGOUTAMA", "HGOMEZ", "EGONZALEZ", "CDAROSA"}

        Dim y As Integer = h.Next(0, Usernames.Length)
        Dim ps As String

        Dim FexAdd As String = "&IBIMR_sub_action=MR_MY_REPORT"
        If Usernames(y) <> "pprasinos" Then
            FexAdd = "&IBIMR_sub_action=MR_MY_REPORT&IBIMR_proxy_id=pprasino.htm&"
            ps = ChrW(112) & ChrW(97) & ChrW(115) & ChrW(115) & ChrW(50) & ChrW(48) & ChrW(49) & ChrW(53)
        Else
            ps = ChrW(87) & ChrW(121) & ChrW(109) & ChrW(97) & ChrW(110) & ChrW(49) & ChrW(50) & ChrW(51) & ChrW(45)
        End If
        Debug.Print(Usernames(y))
        Return {Usernames(y), ps, FexAdd}
    End Function

    Public Shared Sub PullHistory(LotNo As String)
        Dim wf As New WebfocusDLL.WebfocusModule
        Dim ref As String = "http://opsfocus01:8080/ibi_apps/Controller?WORP_REQUEST_TYPE=WORP_LAUNCH_CGI&IBIMR_action=MR_RUN_FEX&IBIMR_domain=qavistes/qavistes.htm&IBIMR_folder=qavistes/qavistes.htm%23wipandshopco&IBIMR_fex=pprasino/wo_move_history_8ball.fex&IBIMR_flags=myreport%2CinfoAssist%2Creport%2Croname%3Dqavistes/mrv/workorder_moves.fex%2CisFex%3Dtrue%2CrunPowerPoint%3Dtrue&IBIMR_sub_action=MR_MY_REPORT&WORP_MRU=true&&WORP_MPV=ab_gbv&WORKORDER_NO="
        ref = ref & LotNo
        Dim j As New Object
        Dim LOGININFO() As String
        LOGININFO = GetUserPasswordandFex()
        wf.LogIn("PPRASINOS", "Wyman123-")
        Do Until wf.IsLoggedIn
            LOGININFO = GetUserPasswordandFex()
            wf.LogIn(LOGININFO(0), LOGININFO(1))
        Loop
        ref = Replace(ref, "&IBIMR_sub_action=MR_MY_REPORT", LOGININFO(2))

        Cursor.Current = Cursors.WaitCursor
        If wf.IsLoggedIn Then
            Debug.Print(ref)
            j = wf.GetReporth(ref, 10000)
        End If

        If Not IsNothing(j) Then
            LotHistoryForm.DataGridView1.Rows.Clear()
            LotHistoryForm.DataGridView1.Columns.Clear()
            For Each st As String In j(0)
                Dim newcol As New DataGridViewColumn
                LotHistoryForm.DataGridView1.Columns.Add(st, st)

            Next st

            For x = 1 To j.length - 2

                Dim newr(j(0).length - 1) As String
                For y = 0 To j(0).length - 1
                    newr(y) = j(x)(y)
                Next
                LotHistoryForm.DataGridView1.Rows.Add()
                LotHistoryForm.DataGridView1.Rows(x - 1).SetValues(newr)
            Next
            LotHistoryForm.DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            LotHistoryForm.Timer1.Interval = 240000
            LotHistoryForm.Timer1.Start()
            LotHistoryForm.Show()
            Cursor.Current = Cursors.Default
            LotHistoryForm.Text = LotNo
            LotHistoryForm.TextBox1.Text = LotNo
        End If

    End Sub

    Public Shared Function SOFolderPopup(Shop As String, SO As String)
        If Mid(Shop, 1, 1) = "0" Then Shop = Mid(Shop, 2, 4)
        If Right(Shop, 1) = "S" Then Shop = Replace(Shop, "S", "")
        Dim SNG As String
        Dim ShopGroup As String
        Dim FPath As String

        If IsNumeric(Shop) Then
            SNG = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(CStr(Shop + 1000000), 4), 2) & "000"
            ShopGroup = SNG & "-" & Microsoft.VisualBasic.Right(SNG + 100999, 5)
        Else
            ShopGroup = "M0001-X9999\M0001-X9999"
        End If
        FPath = "\\slfs01\shared\SALES ORDERS\" & ShopGroup & "\" & Shop & "\"
        FPath = FPath & Dir(FPath & SO & "*", FileAttribute.Directory) & "\"
        'FPath = "\\slfs01\shared\SALES ORDERS\" & ShopGroup & "\" & Shop & "\" & Dir(SO & "*", FileAttribute.Directory) & "\"

        If Not FileIO.FileSystem.DirectoryExists(FPath) Then
            MsgBox("SO NOT FOUND")
            FPath = ("\\slfs01\shared\SALES ORDERS\" & ShopGroup & "\" & Shop & "\")
            If Not FileIO.FileSystem.DirectoryExists(FPath) Then FPath = "\\slfs01\shared\SALES ORDERS\" & ShopGroup
        End If
        Dim openFileDialog1 As New OpenFileDialog()
        Dim myStream As IO.Stream = Nothing
        openFileDialog1.InitialDirectory = FPath
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                myStream = openFileDialog1.OpenFile()
                System.Diagnostics.Process.Start(openFileDialog1.FileName)
                If (myStream IsNot Nothing) Then
                    ' Insert code to read the stream here. 
                End If
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            Finally
                ' Check this again, since we need to make sure we didn't throw an exception on open. 
                If (myStream IsNot Nothing) Then
                    myStream.Close()
                End If
            End Try
        End If
    End Function

    'Public Shared Sub UpdateDbValue(WorkOrders As String(), StopWorkValue As String)
    '    Dim myConnection As New OleDb.OleDbConnection
    '    myConnection.ConnectionString = My.Settings.SOLinesMainDBCs

    '    myConnection.Open()
    '    Dim cmd As OleDb.OleDbCommand
    '    Dim str As String
    '    Try
    '        For Each WO In WorkOrders
    '            str = "UPDATE CERT_ERRORS SET STOPWORK=? WHERE WORKORDERNO=?"
    '            cmd = New OleDb.OleDbCommand(str, myConnection)
    '            cmd.Parameters.AddWithValue("@WORKORDERNO", WO)
    '            cmd.Parameters.AddWithValue("@STOPWORK", StopWorkValue)
    '            cmd.ExecuteNonQuery()
    '        Next WO

    '        myConnection.Close()
    '    Catch ex As Exception
    '        MessageBox.Show("There was an error processing your request. Please try again." & vbCrLf & vbCrLf &
    '                                   "Original Error:" & vbCrLf & vbCrLf & ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        myConnection.Close()
    '    End Try
    '    myConnection.Close()

    'End Sub

End Class


Friend NotInheritable Class NativeMethods

    Private Sub New()
    End Sub

    <DllImport("User32.dll")> Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    End Function


    <DllImport("User32.dll")> Public Shared Function UnregisterHotKey(ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    End Function

    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As IntPtr) As Integer

End Class

