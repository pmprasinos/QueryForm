
Imports System.Data
Imports System.Data.SqlClient
Imports WebfocusDLL
Imports System.Threading.Thread
Imports System.Runtime.CompilerServices
Imports Microsoft.Office.Interop
Imports System.Net
Imports System.Net.NetworkInformation
'Imports ClassLibrary1

Module SQLUpdater
    Private Downloading As Boolean = False
    Dim LogInInfo As String()
    Dim Fuckups As Integer = 0
    Dim ConnectionString As String = "Server=SLREPORT01; Database=WFLocal; User Id=PrasinosApps; Password=Wyman123-;Connection Timeout = 3;"
    Private tmp = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\test.temp" 'O.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "snafu.fubar")
    Public TimeToDownload As String
    Public StartTime As String


    Public Function UpdateShipments(AfterDT As Date)
        Dim AfterDate As String = MakeWebfocusDate(AfterDT)
        Dim wf As New WebfocusModule
        wf = wfLogin(wf)
        Dim ShipRef As String = "http://opsfocus01:8080/ibi_apps/Controller?WORP_REQUEST_TYPE=WORP_LAUNCH_CGI&IBIMR_action=MR_RUN_FEX&IBIMR_domain=qavistes/qavistes.htm&IBIMR_folder=qavistes/qavistes.htm%23salesshipmen&IBIMR_fex=pprasino/full_shipreport_by_lothtml.fex&IBIMR_flags=myreport%2CinfoAssist%2Creport%2Croname%3Dqavistes/mrv/shipping_data.fex%2CisFex%3Dtrue%2CrunPowerPoint%3Dtrue&IBIMR_sub_action=MR_MY_REPORT&WORP_MRU=true&&WORP_MPV=ab_gbv&SHIPPED_D=" & AfterDate & "&IBIMR_random=58708"
        ShipRef = Replace(ShipRef, "&IBIMR_sub_action=MR_MY_REPORT", LogInInfo(2))
    End Function


    Public Function UpdateWIP(WF As WebfocusModule) As Boolean
        UpdateAppend(WF, GetWFIds(WF.GetRequests))
        Return True
    End Function


    Public Function UpdateOpens()
        Try
            If FileIO.FileSystem.FileExists("\\slfs01\shared\prasinos\8ball\LOCKFILE1.txt") Then
                Debug.Print("LASTWRITE " & DateDiff(DateInterval.Minute, FileIO.FileSystem.GetFileInfo("\\slfs01\shared\prasinos\8ball\LOCKFILE1.txt").CreationTime, Now))
            Else

                Dim wf As New WebfocusModule
                wf = wfLogin(wf)
                OpensUpdater(wf)
            End If
        Catch

        Finally

        End Try
    End Function


    Public Function ShouldUpdate(MaxAge As Integer, TableName As String)
        ExecStoredProcedure("wflocal..getlastupdate", True, {"@tablename", "scrap"})

    End Function

    Public Function ExecStoredProcedure(Procedurename As String, IsProcedure As Boolean, Optional Params As Object() = Nothing) As Object()()
        Dim StList As New List(Of Object())
        Using cn As New SqlConnection(ConnectionString)

            Using cmd As New SqlCommand
                cmd.CommandText = Procedurename
                cmd.Connection = cn
                cmd.CommandType = CommandType.Text
                If IsProcedure Then cmd.CommandType = CommandType.StoredProcedure
                If Not IsNothing(Params) Then
                    For x = 0 To (Params.Count / 2) - 1
                        cmd.Parameters.AddWithValue(Params(x), Params(x + 1))
                    Next
                End If
                Try
                    cn.Open()
                    Using DR As SqlClient.SqlDataReader = cmd.ExecuteReader
                        'DR.VisibleFieldCount

                        Do While DR.Read
                            Dim h(DR.VisibleFieldCount) As Object
                            DR.GetValues(h)
                            StList.Add(h)
                        Loop
                    End Using
                Catch ex As Exception
                    Try : class1.Serialize(PPForm.ExceptionPath, ex) : Catch : End Try
                Finally
                    cn.Close()
                End Try
            End Using
        End Using
        ExecStoredProcedure = StList.ToArray
    End Function

    'Private Function wfLogin(wf As WebfocusModule) As WebfocusModule
    '    If IsNothing(wf) Then wf = New WebfocusModule

    '    If Not wf.IsLoggedIn Then

    '        LogInInfo = GetUserPasswordandFex()
    '        wf.LogIn("pprasinos", "Wyman123-")
    '        Do Until wf.IsLoggedIn
    '            LogInInfo = GetUserPasswordandFex()
    '            wf.LogIn(LogInInfo(0), LogInInfo(1))
    '        Loop
    '    End If

    '    Return wf
    'End Function

    Private Function FullUpdate(wf As WebfocusModule)
        Dim afterDate As String
        Dim beforeDate As String

        wfLogin(wf)
        Dim PARTLIST As New List(Of String)
        Using cn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand
                cmd.CommandText = "SELECT DISTINCT PARTNO FROM WFLOCAL..CERT_ERRORS with (NOLOCK) WHERE ISNULL(DAYS_IN_WC,49) < 50 AND PARTNO NOT LIKE '%S'"
                cmd.Connection = cn
                cn.Open()
                Using DR As SqlClient.SqlDataReader = cmd.ExecuteReader
                    Do While DR.Read
                        PARTLIST.Add(DR("PARTNO"))
                    Loop
                End Using
                cn.Close()
                cmd.Parameters.Clear()
            End Using
        End Using
        PARTLIST.Sort()


        For I = PARTLIST.Count - 1 To 0 Step -1
            Dim PART As String = PARTLIST(I)
            PART = Trim(PART)
            Dim WipHistoryRef As String = "http://opsfocus01:8080/ibi_apps/Controller?WORP_REQUEST_TYPE=WORP_LAUNCH_CGI&IBIMR_action=MR_RUN_FEX&IBIMR_domain=qavistes/qavistes.htm&IBIMR_folder=qavistes/qavistes.htm%23wipandshopco&IBIMR_fex=pprasino/wo_move_history_8ball_for_sql.fex&IBIMR_flags=myreport%2CinfoAssist%2Creport%2Croname%3Dqavistes/mrv/workorder_moves.fex%2CisFex%3Dtrue%2CrunPowerPoint%3Dtrue&IBIMR_sub_action=MR_MY_REPORT&WORP_MRU=true&PARTNO=" & PART & "&WORP_MPV=ab_gbv&&IBIMR_random=13866&"
            'console.WriteLine(PARTLIST.IndexOf(PART) & "   " & PART)
            wf.GetReporthAsync(WipHistoryRef, "wiphist")
            'Threading.Thread.Sleep(5000)
            UpdateAppend(wf, GetWFIds(wf.GetRequests))
            ' Threading.Thread.Sleep(1000)
            wf = Nothing
            wf = New WebfocusModule
            wf.LogIn("PPRASINOS", "Wyman123-")

        Next


        For q = 0 To 128
            'console.Write(q & " ")
            Dim Span As Integer = 5
            beforeDate = MakeWebfocusDate(Today.AddDays(-q * Span))
            afterDate = MakeWebfocusDate(Today.AddDays(-1 - ((q + 1) * Span)))
            'console.WriteLine(beforeDate & "-" & afterDate)
            Dim InvRef1 As String = "qavistes/qavistes.htm#wipandshopco    pprasinos:pprasino/inventorybyms.fex "

            Dim LaborRef1 As String = "http://opsfocus01:8080/ibi_apps/Controller?WORP_REQUEST_TYPE=WORP_LAUNCH_CGI&IBIMR_action=MR_RUN_FEX&IBIMR_domain=qavistes/qavistes.htm&IBIMR_folder=qavistes/qavistes.htm%23laborreporti&IBIMR_fex=pprasino/labor_part_detail_workorders_with_esh_for_sql_for_testing.fex&IBIMR_flags=myreport%2CinfoAssist%2Creport%2Croname%3Dqavistes/mrv/labor_part_detail_workorders_with_esh.fex%2CisFex%3Dtrue%2CrunPowerPoint%3Dtrue&IBIMR_sub_action=MR_MY_REPORT&WORP_MRU=true&&WORP_MPV=ab_gbv&GECHARGE_DATE=" & afterDate & "&LECHARGE_DATE=" & beforeDate & "&IBIMR_random=24311&"
            Dim TputRef1 As String = "http://opsfocus01:8080/ibi_apps/Controller?WORP_REQUEST_TYPE=WORP_LAUNCH_CGI&IBIMR_action=MR_RUN_FEX&IBIMR_domain=qavistes/qavistes.htm&IBIMR_folder=qavistes/qavistes.htm%23thruputrepor&IBIMR_fex=pprasino/esh_and_tput_for_flex_for_sql.fex&IBIMR_flags=myreport%2CinfoAssist%2Creport%2Croname%3Dqavistes/mrv/thruput_detail_data.fex%2CisFex%3Dtrue%2CrunPowerPoint%3Dtrue&IBIMR_sub_action=MR_MY_REPORT&WORP_MRU=true&&WORP_MPV=ab_gbv&TP_DATE_COMPELTED=" & afterDate & "&LE_TP_DATE_COMPELTED=" & beforeDate & "&IBIMR_random=31846"
            Dim ScrapRef1 As String = "http://opsfocus01:8080/ibi_apps/Controller?WORP_REQUEST_TYPE=WORP_LAUNCH_CGI&IBIMR_action=MR_RUN_FEX&IBIMR_domain=qavistes/qavistes.htm&IBIMR_folder=qavistes/qavistes.htm%23scrapdatatqg&IBIMR_fex=pprasino/scrap_report_including_nodefect.fex&IBIMR_flags=myreport%2CinfoAssist%2Creport%2Croname%3Dqavistes/mrv/scrap_data.fex%2CisFex%3Dtrue%2CrunPowerPoint%3Dtrue&IBIMR_sub_action=MR_MY_REPORT&WORP_MRU=true&&WORP_MPV=ab_gbv&DISP_D=" & afterDate & "&LEDISP_D=" & beforeDate & "&IBIMR_random=96021"
            TputRef1 = Replace(TputRef1, "&IBIMR_sub_action=MR_MY_REPORT", LogInInfo(2))
            ' ScrapRef1 = Replace(ScrapRef1, "&IBIMR_sub_action=MR_MY_REPORT", LogInInfo(2))
            ' LaborRef1 = Replace(LaborRef1, "&IBIMR_sub_action=MR_MY_REPORT", LogInInfo(2))



            If Today.DayOfWeek = DayOfWeek.Monday Then wf.GetReporthAsync(TputRef1, "tput")
            If Today.DayOfWeek = DayOfWeek.Tuesday Then wf.GetReporthAsync(ScrapRef1, "scrap")
            If Today.DayOfWeek = DayOfWeek.Wednesday Then wf.GetReporthAsync(LaborRef1, "labor")

            '  Threading.Thread.Sleep(2000)
            UpdateAppend(wf, GetWFIds(wf.GetRequests))
            ' Threading.Thread.Sleep(1000)

            wf = Nothing
            wf = wfLogin(wf)

        Next q

    End Function


    Private Function GetPingMs(ByRef hostNameOrAddress As String)
        Dim ping As New System.Net.NetworkInformation.Ping
        Return ping.Send(hostNameOrAddress).RoundtripTime
        Threading.Thread.Sleep(1000)
    End Function

    Private Function GetUserPasswordandFex() As String()
        Dim h As New Random

        Dim Usernames() As String = {"hfaizi", "mreyes", "MALMARAZ", "MARJMAND", "HYANG", "GWONG", "VDELACRUZ", "JTIBAYAN", "JSOLIS", "ASINGH", "GREYES", "JPIMENTEL", "TOSULLIVAN", "MMARTIN", "VLOPEZ", "SLI", "JIMPERIAL", "JHERNANDEZ", "FHARO", "CGOUTAMA", "HGOMEZ", "EGONZALEZ", "CDAROSA"}

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

    Function wfLogin(ByRef wf As WebfocusModule) As WebfocusModule
        If IsNothing(wf) Then wf = New WebfocusModule

        If Not wf.IsLoggedIn Then

            LogInInfo = GetUserPasswordandFex()
            wf.LogIn("pprasinos", "Wyman123-")
            Do Until wf.IsLoggedIn
                LogInInfo = GetUserPasswordandFex()
                wf.LogIn(LogInInfo(0), LogInInfo(1))
            Loop
        End If

        Return wf
    End Function

    Private Function GetWFIds(Requests As String) As String()
        Dim k() As String = Requests.Split(vbLf, 100, StringSplitOptions.RemoveEmptyEntries)
        For w = 0 To k.Length - 1
            'Debug.Print(Split(k(w), vbTab)(1))
            ' Dim g As Object = Split(k(w), vbTab)
            Dim y As Object = k(w).Split(" ", 100, StringSplitOptions.RemoveEmptyEntries)
            '   k(w) = k(w).Split(New Char() {" "}).re

            k(w) = y(1)

        Next
        Return k
    End Function

    Private Function WithinString(String1 As String, String2 As String) As Boolean
        If InStr(String1, String2, CompareMethod.Text) <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function MakeWebfocusDate(Indate As Date) As String
        Dim vDay As String = Indate.Day
        Dim Vmonth As String = Month(Indate)
        Dim vYear As String = Year(Indate)
        If Len(vDay) = 1 Then vDay = "0" & vDay
        If Len(Vmonth) = 1 Then Vmonth = "0" & Vmonth
        MakeWebfocusDate = Indate.ToString("MMddyyyy")
    End Function



    Private Function NotificationEmails() As Int16

        Dim RawPull() As String = Split(FileIO.FileSystem.ReadAllText("\\slfs01\shared\prasinos\8ball\Notifications\Notifications.txt"), vbCrLf)

        Dim WOList As New List(Of String)


        Using cn As New SqlConnection(ConnectionString)
            Using cmd As New SqlCommand
                cmd.CommandText = "select * from wflocal..NOTIFICATIONS a  
                                    left join wflocal..CERT_ERRORS b 
                                    on a.WORKORDERNO=b.WORKORDERNO
                                    where a.OPERATIONNO<b.OPERATION
                                   "
                cmd.Connection = cn
                cn.Open()
                Using DR As SqlClient.SqlDataReader = cmd.ExecuteReader
                    Do While DR.Read
                        Dim MsgString As String = "This email Is to notify that lot " & DR("WORKORDERNO").ToString & " has reached Or passed operation " & DR("OPERATIONNO").ToString & Chr(10) & Chr(13) & vbCrLf & vbCrLf & "This Is an automated message"
                        EmailFile(DR("EMAIL").ToString, MsgString, "Movement notification:  " & DR("WORKORDERNO").ToString, True)
                        WOList.Add(DR("WORKORDERNO").ToString & "|" & DR("OPERATIONNO").ToString & "|" & DR("EMAIL").ToString)
                    Loop
                End Using

                For Each WO In WOList
                    cmd.CommandText = "DELETE FROM WFLOCAL..NOTIFICATIONS WHERE WORKORDERNO =@WORKORDERNO AND EMAIL=@EMAIL AND OPERATIONNO=@OPERATIONNO"
                    cmd.Parameters.AddWithValue("@WORKORDERNO", Split(WO, "|")(0))
                    cmd.Parameters.AddWithValue("@EMAIL", Split(WO, "|")(2))
                    cmd.Parameters.AddWithValue("@OPERATIONNO", Split(WO, "|")(1))
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                Next
                cn.Close()
                cmd.Parameters.Clear()
            End Using
        End Using

        Return 0
    End Function

    Sub EmailFile(Recipient As String, MessageBody As String, Subject As String, Optional Send As Boolean = False)

        Dim OutLookApp As New Outlook.Application
        Dim Mail As Outlook.MailItem = OutLookApp.CreateItem(Outlook.OlItemType.olMailItem)

        Dim mailRecipient As Outlook.Recipient

        mailRecipient = Mail.Recipients.Add(Recipient)
        mailRecipient.Resolve()

        Mail.Recipients.ResolveAll()

        Mail.HTMLBody = MessageBody
        Mail.Subject = Subject
        Mail.Save()
        If Send Then
            Mail.Send()
        Else
            Mail.Display()
        End If

    End Sub

    Private Sub RemoveTicket(TicketID As String)
        FileIO.FileSystem.CopyFile("\\slfs01\shared\prasinos\8ball\Notifications\Notifications.txt", My.Computer.FileSystem.SpecialDirectories.Desktop & "\Notifications.txt", True)

        Dim OutString As String = ""
        Dim instring() As String = Split(FileIO.FileSystem.ReadAllText(My.Computer.FileSystem.SpecialDirectories.Desktop & "\Notifications.txt"), vbCrLf)
        For Each textline In instring
            If InStr(textline, TicketID) = 0 Then OutString = OutString & textline & vbCrLf
        Next textline



        FileIO.FileSystem.WriteAllText("\\slfs01\shared\prasinos\8ball\Notifications\Notifications.txt", OutString, False)
    End Sub

    Public Sub UpdateAppend(WF As WebfocusDLL.WebfocusModule, RespNames() As String)
        'Threading.Thread.Sleep(60000)
        Dim trans As SqlTransaction

        Dim RefFind() As String = {"ships", "fingoods", "lots", "certs", "scrap", "partdata", "xtl", "tput", "labor", "labor1", "wiphist"}
        Dim TableNames() As String = {"SHIPMENTS1", "CERT_ERRORS1", "CERT_ERRORS1", "CERT_ERRORS1", "SCRAP1", "ALLOYS1", "TIMELINE1", "TPUT1", "LABOR1", "LABOR1", "WIP_MOVE_HIST1"}
        Dim UpdatedRows As Integer = 0
        Using cn As New SqlConnection(ConnectionString)
            cn.Open()
            Try
                Using cmd As New SqlCommand("", cn)
                    trans = cn.BeginTransaction(Environment.UserName)
                    cmd.Connection = cn
                    cmd.Transaction = trans

                    Dim Query As String
                    Dim TABLES() As String
                    TABLES = TableNames

                    If InStr(WF.GetRequests, "lots") <> 0 Then
                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = "UPDATE WFLOCAL.DBO.CERT_ERRORS1 SET ACTIVE = 2 WHERE ACTIVE <> 0"
                        cmd.ExecuteNonQuery()
                    End If

                    For P = 0 To RespNames.Length - 1
                        If RespNames(P) = Nothing Or RespNames(P) = "opens" Then GoTo NEXTP
                        Dim j As New Object
                        j = WF.GetResponse(RespNames(P)).Response
                        Dim TableName As String

                        Dim e As Integer
                        Dim t As Boolean
                        ' Try
                        For ind = 0 To RefFind.Length - 1
                            If RefFind(ind) = RespNames(P) Then TableName = TableNames(ind)
                        Next

                        'console.Write(TableName)
                        'console.CursorLeft = 0
                        cmd.CommandType = CommandType.Text
                        Query = "SELECT column_name, data_type FROM WFLOCAL.INFORMATION_SCHEMA.COLUMNS" & vbCrLf &
                "WHERE WFLOCAL.INFORMATION_SCHEMA.COLUMNS.TABLE_NAME='" & TableName & "'"
                        cmd.CommandText = Query

                        Dim ColumnInfo As New List(Of String())
                        Dim CSVColumns As String = ""
                        Dim CSVUPDATE As String = ""

                        Using dr As SqlDataReader = cmd.ExecuteReader
                            While dr.Read()
                                Dim Y As Integer = GetColumnNumber(j, dr("column_name").ToString)
                                If Y <> -1 Then
                                    ColumnInfo.Add({dr("column_name").ToString, dr("data_type").ToString, Y})
                                    CSVColumns = CSVColumns & "ROW." & dr("column_name").ToString & ", "
                                    CSVUPDATE = CSVUPDATE & dr("column_name").ToString & " = @" & dr("column_name").ToString & ","
                                End If
                            End While
                        End Using
                        ColumnInfo.Add({"ACTIVE", "int", 0})

                        CSVColumns = CSVColumns & "ROW.ACTIVE, "

                        CSVUPDATE = CSVUPDATE & "ROW.ACTIVE = @ACTIVE,"

                        CSVColumns = Left(CSVColumns, Len(CSVColumns) - 2)
                        CSVUPDATE = Left(CSVUPDATE, Len(CSVUPDATE) - 1)
                        PPForm.Text = "PLEASE WAIT, UPDATING " & TableName & "..."
                        cmd.CommandType = CommandType.StoredProcedure
                        If TableName = "SCRAP1" Then
                            cmd.CommandText = "WFLOCAL.DBO.UpdateScrap1"
                        ElseIf TableName = "CERT_ERRORS1" Then
                            cmd.CommandText = "WFLOCAL.DBO.UPDATEAPPENDWIP1"
                        ElseIf TableName = "TIMELINE1" Then
                            cmd.CommandText = "WFLOCAL.DBO.XTLupdateAppend1"
                        ElseIf TableName = "ALLOYS1" Then
                            cmd.CommandText = "	MERGE WFLOCAL..ALLOYS1 AS TARGET
                                            USING (SELECT @PARTNO, @ALLOY_DESCR, @MATERIAL_SPEC, @PART_DESCR, @PIECES_PER_MOLD, @SELLING_PRICE, @POUR_WEIGHT, @STOP_RELEASE, @PART_STATUS, @ROUT_REV) AS SOURCE 
                                                          (PARTNO,  ALLOY_DESCR,  MATERIAL_SPEC,  PART_DESCR,  PIECES_PER_MOLD,  SELLING_PRICE,  POUR_WEIGHT,  STOP_RELEASE,  PART_STATUS, ROUT_REV)
                                            ON TARGET.PARTNO=SOURCE.PARTNO
                                            WHEN MATCHED THEN
                                                    UPDATE SET	ALLOY_DESCR=SOURCE.ALLOY_DESCR,  
                                                                MATERIAL_SPEC=SOURCE.MATERIAL_SPEC,
                                                                PART_DESCR=SOURCE.PART_DESCR,
                                                                PIECES_PER_MOLD=SOURCE.PIECES_PER_MOLD,
                                                                SELLING_PRICE=SOURCE.SELLING_PRICE,
                                                                POUR_WEIGHT=SOURCE.POUR_WEIGHT,
                                                                STOP_RELEASE=SOURCE.STOP_RELEASE,
                                                                PART_STATUS=SOURCE.PART_STATUS,
                                                                ROUT_REV=SOURCE.ROUT_REV 
                                            WHEN NOT MATCHED THEN
                                                    INSERT (PARTNO,  ALLOY_DESCR,  MATERIAL_SPEC,  PART_DESCR,  PIECES_PER_MOLD,  SELLING_PRICE,  POUR_WEIGHT,  STOP_RELEASE,  PART_STATUS, ROUT_REV)
                                                    values (@PARTNO, @ALLOY_DESCR, @MATERIAL_SPEC, @PART_DESCR, @PIECES_PER_MOLD, @SELLING_PRICE, @POUR_WEIGHT, @STOP_RELEASE, @PART_STATUS, @ROUT_REV);
                                                "
                            cmd.CommandType = CommandType.Text
                        ElseIf TableName = "SHIPMENTS1" Then

                            cmd.CommandText = "WFLOCAL.DBO.AddShipments1"
                        ElseIf TableName = "TPUT1" Then
                            cmd.CommandText = "WFLOCAL.DBO.UpdateThruput1"
                        ElseIf TableName = "LABOR1" Then
                            cmd.CommandText = "WFLOCAL.DBO.UpdateLabor1"
                        ElseIf TableName = "WIP_MOVE_HIST1" Then
                            cmd.CommandText = "WFLOCAL.DBO.UPDATE_WIP_HIST1"
                        End If

                        Dim CT As Long = 1
                        For RowNum = 1 To j.length - 1

                            With cmd.Parameters
                                .Clear()
                                For Each Col In ColumnInfo

                                    If Col(1) = "nvarchar" Or Col(1) = "nchar" Then

                                        .Add("@" & Col(0), SqlDbType.NVarChar).Value = j(RowNum)(Col(2))

                                    ElseIf Col(1) = "float" And Col(0) <> "ACTIVE" Then
                                        'Debug.Print(j(RowNum)(Col(2)))
                                        j(RowNum)(Col(2)) = Replace(j(RowNum)(Col(2)), "R", "1")
                                        j(RowNum)(Col(2)) = Replace(j(RowNum)(Col(2)), "N", "0")
                                        j(RowNum)(Col(2)) = Replace(j(RowNum)(Col(2)), "Y", "2")

                                        If Replace(j(RowNum)(Col(2)), ",", "") = "." Then
                                            .Add("@" & Col(0), SqlDbType.Float).Value = 0
                                        Else
                                            Dim s As Double = j(RowNum)(Col(2))
                                            .Add("@" & Col(0), SqlDbType.Float).Value = s
                                        End If
                                    ElseIf InStr(Col(1), "smallint", CompareMethod.Text) <> 0 Then
                                        If Replace(j(RowNum)(Col(2)), ",", "") = "." Then
                                            .Add("@" & Col(0), SqlDbType.SmallInt).Value = 0
                                        Else
                                            Dim S As Int16 = Replace(j(RowNum)(Col(2)), ",", "") * 1
                                            .Add("@" & Col(0), SqlDbType.SmallInt).Value = S
                                        End If
                                    ElseIf InStr(Col(1), "int", CompareMethod.Text) <> 0 And Col(0) <> "ACTIVE" Then
                                        If Replace(j(RowNum)(Col(2)), ",", "") = "." Then
                                            .Add("@" & Col(0), SqlDbType.Int).Value = 0
                                        Else
                                            Dim S As Integer = (0 & Replace(Replace(j(RowNum)(Col(2)), ",", ""), ".", "")) * 1
                                            .AddWithValue("@" & Col(0), S)
                                        End If
                                    ElseIf InStr(Col(1), "Date", CompareMethod.Text) <> 0 Then

                                        Dim dt As DateTime = #1/1/1900#

                                        If Replace(j(RowNum)(Col(2)), ",", "") = "." Then

                                        Else
                                            dt = DateTime.Parse(j(RowNum)(Col(2)))
                                            'Debug.Print(dt.ToString)
                                            If dt.Year > 1900 Then
                                                .Add("@" & Col(0), SqlDbType.DateTime).Value = dt
                                            Else
                                                dt = Now.AddYears(-100)
                                                .Add("@" & Col(0), SqlDbType.DateTime).Value = dt
                                            End If
                                        End If
                                    End If
                                Next
                                .Add("@ACTIVE", SqlDbType.Int).Value = 1
                            End With


                            t = True

                            ' Try
                            cmd.ExecuteNonQuery()
                            'If e <> -1 Then Stop
                            t = False
                            CT = CT + 1
                            PPForm.Text = Split(PPForm.Text & " -- ", " -- ")(0) & " -- (" & CT & "/" & j.length & ") "


                        Next

NEXTP:
                    Next P

                    If InStr(WF.GetRequests, "lots") <> 0 Then

                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = "UPDATE WFLOCAL.DBO.CERT_ERRORS1 Set ACTIVE = 0 WHERE ACTIVE = 2"
                        Dim RWS As Integer = cmd.ExecuteNonQuery()
                        '    If RWS <> -1 Then Stop
                        UpdatedRows = UpdatedRows + RWS
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "wflocal..cleanup1"
                        cmd.Parameters.Clear()
                        cmd.ExecuteNonQuery()

                    End If
                    If InStr(WF.GetRequests, "ships") > 0 Then
                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = "update wflocal..shipments1 set shipped_dtime = getdate() where invoice_no = 'PACK(1).pdf' "
                        cmd.ExecuteNonQuery()
                    End If
                End Using
                trans.Commit()
            Catch ex As Exception
                Try : class1.Serialize(PPForm.ExceptionPath, ex) : Catch : End Try
                Try
                    trans.Rollback()
                Catch ex2 As Exception
                    Try : class1.Serialize(PPForm.ExceptionPath, ex2) : Catch : End Try
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                    Console.WriteLine("  Message: {0}", ex2.Message)
                End Try

            End Try

        End Using
        PPForm.Text = "PLEASE WAIT, UPDATING TABLES...DONE"
    End Sub


    '    Public Sub UpdateAppendUsingTables(WF As WebfocusDLL.WebfocusModule, RespNames() As String)
    '        'Threading.Thread.Sleep(60000)
    '        Dim ty As New Stopwatch
    '        ty.Start()
    '        Dim RefFind() As String = {"ships", "fingoods", "lots", "certs", "scrap", "partdata", "xtl", "tput", "labor", "labor1", "wiphist", "opens"}
    '        Dim TableNames() As String = {"SHIPMENTS", "CERT_ERRORS", "CERT_ERRORS", "CERT_ERRORS", "SCRAP", "ALLOYS", "TIMELINE", "TPUT", "LABOR", "LABOR", "WIP_MOVE_HIST", "OPEN_ORDERS"}
    '        Dim UpdatedRows As Integer = 0
    '        Using tcn As New SqlConnection(ConnectionString)
    '            tcn.Open()
    '            Dim Trans As SqlTransaction
    '            Dim tcmd As SqlCommand = tcn.CreateCommand()
    '            Trans = tcn.BeginTransaction("SampleTransaction")

    '            tcmd.Connection = tcn
    '            tcmd.Transaction = Trans

    '            Try

    '                Dim TABLES() As String
    '                TABLES = TableNames


    '                If InStr(WF.GetRequests, "lots") <> 0 Then
    '                    tcmd.CommandType = CommandType.Text
    '                    tcmd.CommandText = "UPDATE WFLOCAL.DBO.CERT_ERRORS1 SET ACTIVE = 2 WHERE ACTIVE <> 0"
    '                End If

    '                If InStr(WF.GetRequests, "opens") > 0 Then
    '                    tcmd.CommandText = "UPDATE wflocal.dbo.OPEN_ORDERS Set ACTIVE = 2 WHERE ACTIVE <> 0"
    '                    tcmd.ExecuteNonQuery()
    '                End If




    '                For P = 0 To RespNames.Length - 1
    '                    If RespNames(P) = Nothing Then GoTo NEXTP
    '                    Dim j As New Object
    '                    j = WF.GetResponse(RespNames(P)).Response
    '                    Dim TableName As String
    '                    Dim e As Integer
    '                    Dim t As Boolean

    '                    For ind = 0 To RefFind.Length - 1
    '                        If RefFind(ind) = RespNames(P) Then TableName = TableNames(ind)
    '                    Next

    '                    Dim SBC As New SqlBulkCopy(tcn, SqlBulkCopyOptions.TableLock, Trans)
    '                    Dim dt As New DataTable
    '                    dt.TableName = TableName

    '                    'console.Write(TableName)
    '                    'console.CursorLeft = 0
    '                    tcmd.CommandType = CommandType.Text
    '                    tcmd.CommandText = "SELECT column_name, data_type FROM WFLOCAL.INFORMATION_SCHEMA.COLUMNS" & vbCrLf &
    '        "WHERE WFLOCAL.INFORMATION_SCHEMA.COLUMNS.TABLE_NAME='" & TableName & "'"


    '                    Dim ColumnInfo As New List(Of String())
    '                    Dim CSVColumns As String = ""
    '                    Dim CSVUPDATE As String = ""

    '                    Using dr As SqlDataReader = tcmd.ExecuteReader
    '                        Dim K As Integer = 0
    '                        Dim ret As Type
    '                        While dr.Read()
    '                            If InStr(dr("column_name").ToString, "ACTIVE") > 0 Then GoTo SkipColumn
    '                            If InStr(dr("data_type").ToString, "char") > 0 Then ret = Type.GetType("system.string", True, True)
    '                            If InStr(dr("data_type").ToString, "int") > 0 Then ret = Type.GetType("system.Int32", True, True)
    '                            If InStr(dr("data_type").ToString, "fl") > 0 Then ret = Type.GetType("system.double", True, True)
    '                            If InStr(dr("data_type").ToString, "date") > 0 Then ret = Type.GetType("system.DateTime", True, True)
    '                            'If InStr(dr("data_type").ToString, "time") > 0 Then ret = ret.GetType("system.DateTime", True, True)
    '                            If InStr(dr("data_type").ToString, "bit") > 0 Then ret = Type.GetType("system.Boolean", True, True)
    '                            dt.Columns.Add(dr("column_name").ToString, ret)
    '                            'SBC.ColumnMappings.Add(dt.Columns(dt.Columns.Count - 1).ColumnMapping, K)
    '                            SBC.ColumnMappings.Add(dt.Columns(dt.Columns.Count - 1).ColumnName, dr("column_name").ToString)
    'SkipColumn:
    '                            K = K + 1
    '                        End While
    '                    End Using
    '                    dt.Columns.Add("ACTIVE", Type.GetType("system.Int32", True, True))
    '                    SBC.ColumnMappings.Add("ACTIVE", "ACTIVE")

    '                    'ColumnInfo.Add({"ACTIVE", "int", 0})

    '                    Dim CT As Long = 1
    '                    Dim sw As New Stopwatch
    '                    sw.Start()
    '                    For RowNum = 1 To j.length - 1
    '                        Dim dr As DataRow = dt.NewRow()
    '                        For Each Col As DataColumn In dt.Columns

    '                            'Dim k As Object = Col.
    '                            Dim d As Int16 = 0
    '                            Dim FoundCol As Boolean = False
    '                            Do Until FoundCol
    '                                If j(RowNum)(d) = "." Then j(0)(d) = vbNull
    '                                If Col.ColumnName = "EXT_VALUE" And j(0)(d) = "Field14" Then FoundCol = True

    '                                If Col.ColumnName = j(0)(d) Then FoundCol = True
    '                                If Col.ColumnName = "SHIPPED_DATE" And j(0)(d) = "SHIPPED_DTIME" Then FoundCol = True

    '                                If Not FoundCol Then d = d + 1
    '                                If d = j(0).length - 1 Or FoundCol Then

    '                                    If Col.ColumnName = "SCANNED" Then

    '                                    ElseIf Col.ColumnName = "ACTIVE" Then
    '                                        dr(Col) = 1
    '                                    ElseIf FoundCol = True Then

    '                                        dr(Col) = j(RowNum)(d)
    '                                    Else
    '                                        'Debug.Print(Col.ColumnName)
    '                                    End If
    '                                    FoundCol = True
    '                                End If
    '                            Loop
    '                        Next Col
    '                        dt.Rows.Add(dr)
    '                        CT = CT + 1
    '                    Next RowNum
    '                    Debug.Print("LOCAL TABLE GENERATED IN " & (sw.ElapsedMilliseconds / 1000) & " SECONDS")
    '                    sw.Restart()
    '                    Dim OrigTable As New DataTable



    '                    If TableName = "SHIPMENTS" Then

    '                        tcmd.CommandText = "DELETE FROM WFLOCAL..TEMPSHIPMENTS"
    '                        tcmd.ExecuteNonQuery()

    '                        SBC.BulkCopyTimeout = 10
    '                        SBC.DestinationTableName = "WFLOCAL..TEMPSHIPMENTS"


    '                        SBC.WriteToServer(dt)
    '                        tcmd.CommandText = "	MERGE WFLOCAL.DBO.SHIPMENTS1 AS T
    '	                                            USING WFLOCAL.DBO.TEMPSHIPMENTS AS S

    '	                                            ON T.WORKORDERNO = S.WORKORDERNO AND T.INVOICE_NO = S.INVOICE_NO AND T.ORDER_LINE=S.ORDER_LINE AND T.QTY_SHPPED=S.QTY_SHPPED

    '	                                            WHEN NOT MATCHED THEN
    '		                                            INSERT(WORKORDERNO, INVOICE_NO, SHIPPED_DTIME, PARTNO, CUSTOMER_NO, COMPANYNAME, SALES_ORDER_NO, CUSTOMER_PO_NO, WEIGH_BILL, CUSTOMER_PARTNO, 
    '			                                            QTY_SHPPED, ORDER_LINE, PRICE, Field14, LINE_TYPE, EXT_VALUE, SHIPPED_DATE)

    '		                                            VALUES(S.WORKORDERNO, S.INVOICE_NO, S.SHIPPED_DTIME, S.PARTNO, S.CUSTOMER_NO, S.COMPANYNAME, S.SALES_ORDER_NO, S.CUSTOMER_PO_NO, 
    '			                                             S.WEIGH_BILL, S.CUSTOMER_PARTNO, S.QTY_SHPPED, S.ORDER_LINE, S.PRICE, S.Field14, S.LINE_TYPE, S.FIELD14, CONVERT(DATE, S.SHIPPED_DTIME))

    '	                                            WHEN MATCHED THEN
    '		                                            UPDATE SET T.FIELD14 = S.FIELD14,
    '		                                            T.LINE_TYPE = S.LINE_TYPE;"

    '                        tcmd.ExecuteNonQuery()
    '                        tcmd.CommandText = "update wflocal..shipments1 Set shipped_dtime = getdate() where invoice_no = 'PACK(1).pdf' "
    '                        tcmd.ExecuteNonQuery()


    '                    ElseIf TableName = "OPEN_ORDERS" Then

    '                        SBC.BulkCopyTimeout = 10
    '                        SBC.DestinationTableName = "WFLOCAL..TEMPOPENS"
    '                        tcmd.CommandText = "DELETE FROM " & SBC.DestinationTableName
    '                        tcmd.ExecuteNonQuery()
    '                        SBC.WriteToServer(dt)
    '                        tcmd.CommandText = "MERGE WFLOCAL..OPEN_ORDERS1 AS T
    '                                                USING WFLOCAL..TEMPOPENS AS S
    '                                                ON T.SALES_ORDER_NO = S.SALES_ORDER_NO AND T.ORDER_LINE=S.ORDER_LINE

    '                                                WHEN NOT MATCHED BY TARGET 
    '		                                                THEN INSERT(ADDED_BY, SALES_ORDER_NO , ORDER_LINE , LINE_TYPE , PARTNO , QTY_DUE , RELEASE_PRICE , QTY_SHIPPED , CUSTOMER_PARTNO, 
    '					                                                CUSTOMER_PO_NO, CUST_ORDER_LINE , CUSTOMER_NO , COMPANY_NAME , LEADTIME , DUE_VALUE , REQUIRED_D , SCHED_D , 
    '					                                                SALESORDER_STATUS ,  PART_STATUS , LINE_STATUS , PO_RECEIVED, SHIP_CONDITION, CUSTOMER_STATUS , 
    '					                                                Z91_PSEUDO_DRAWING , Z91_REF_PARTNO , PART_DESCR , SALES_COMMENTS, ACTIVE)

    '		                                                VALUES(UPPER(S.ADDED_BY), S.SALES_ORDER_NO , S.ORDER_LINE , S.LINE_TYPE , S.PARTNO , S.QTY_DUE , S.RELEASE_PRICE , S.QTY_SHIPPED , 
    '			                                                    S.CUSTOMER_PARTNO , S.CUSTOMER_PO_NO , S.CUST_ORDER_LINE , S.CUSTOMER_NO , S.COMPANY_NAME , S.LEADTIME , 
    '			                                                    S.DUE_VALUE , S.REQUIRED_D , S.SCHED_D , S.SALESORDER_STATUS ,  S.PART_STATUS , 
    '			                                                    S.LINE_STATUS , S.PO_RECEIVED , S.SHIP_CONDITION , S.CUSTOMER_STATUS , S.Z91_PSEUDO_DRAWING , 
    '			                                                    S.Z91_REF_PARTNO , S.PART_DESCR , S.SALES_COMMENTS, 1)
    '                                                WHEN MATCHED 
    '                                                    THEN UPDATE	SET 
    '                                                        LINE_TYPE = ISNULL(S.LINE_TYPE,T.LINE_TYPE),
    '                                                        PARTNO = S.PARTNO,
    '                                                        QTY_DUE = S.QTY_DUE,
    '                                                        RELEASE_PRICE = S.RELEASE_PRICE,
    '                                                        QTY_SHIPPED = S.QTY_SHIPPED,
    '                                                        CUSTOMER_PARTNO = ISNULL(S.CUSTOMER_PARTNO,T.CUSTOMER_PARTNO),
    '                                                        CUSTOMER_PO_NO = ISNULL(S.CUSTOMER_PO_NO,T.CUSTOMER_PO_NO),
    '                                                        CUST_ORDER_LINE = ISNULL(S.CUST_ORDER_LINE,T.CUST_ORDER_LINE),
    '                                                        COMPANY_NAME = ISNULL(S.COMPANY_NAME,T.COMPANY_NAME),
    '                                                        LEADTIME = ISNULL(S.LEADTIME,T.LEADTIME),
    '                                                        DUE_VALUE = ISNULL(S.DUE_VALUE,T.DUE_VALUE),
    '                                                        REQUIRED_D = ISNULL(S.REQUIRED_D,T.REQUIRED_D),
    '                                                        SCHED_D = ISNULL(S.SCHED_D,T.SCHED_D),
    '                                                        SALESORDER_STATUS = ISNULL(S.SALESORDER_STATUS,T.SALESORDER_STATUS),
    '                                                        PART_STATUS = ISNULL(S.PART_STATUS,T.PART_STATUS),
    '                                                        LINE_STATUS = ISNULL(S.LINE_STATUS,T.LINE_STATUS),
    '                                                        PO_RECEIVED = ISNULL(S.PO_RECEIVED,T.PO_RECEIVED),
    '                                                        SHIP_CONDITION = ISNULL(S.SHIP_CONDITION,T.SHIP_CONDITION),
    '                                                        CUSTOMER_STATUS = ISNULL(S.CUSTOMER_STATUS,T.CUSTOMER_STATUS),
    '                                                        Z91_PSEUDO_DRAWING = ISNULL(S.Z91_PSEUDO_DRAWING,T.Z91_PSEUDO_DRAWING),
    '                                                        Z91_REF_PARTNO = ISNULL(S.Z91_REF_PARTNO,T.Z91_REF_PARTNO),
    '                                                        PART_DESCR = ISNULL(S.PART_DESCR,T.PART_DESCR),
    '                                                        SALES_COMMENTS = ISNULL(S.SALES_COMMENTS,T.SALES_COMMENTS),
    '                                                        ACTIVE=1
    '                                                WHEN NOT MATCHED BY SOURCE
    '                                                    THEN UPDATE SET 
    '                                                        ACTIVE = 0;"

    '                        tcmd.ExecuteNonQuery()

    '                        ' tcmd.commandText = "UPDATE wflocal.dbo.OPEN_ORDERS1 SET ACTIVE = 0 WHERE ACTIVE = 2"
    '                        ' tcmd.executenonquery()
    '                        tcmd.CommandText = "INSERT INTO WFLOCAL.DBO.PO_REVIEW1  (SALES_ORDER_NO, CUST_NO, SALES, USERNAME, ttimestamp, prel, pship, erel, eship)" & vbCrLf &
    '                                        "Select DISTINCT B.SALES_ORDER_NO, B.CUSTOMER_NO, B.ADDED_BY, B.ADDED_BY, getdate(), 1, 1, 1, 1" & vbCrLf &
    '                                        "From DBO.OPEN_ORDERS1 B" & vbCrLf &
    '                                        "Where Not EXISTS(SELECT distinct  B.SALES_ORDER_NO" & vbCrLf &
    '                                        "From DBO.PO_REVIEW1" & vbCrLf &
    '                                        "Where PO_REVIEW1.SALES_ORDER_NO = B.SALES_ORDER_NO" & vbCrLf &
    '                                        ")" & vbCrLf &
    '                                        "" & vbCrLf
    '                        tcmd.ExecuteNonQuery()
    '                        tcmd.Parameters.Clear()
    '                        tcmd.CommandType = CommandType.StoredProcedure
    '                        tcmd.CommandText = "wflocal.dbo.CleanTickets"
    '                        tcmd.ExecuteNonQuery()


    '                    ElseIf TableName = "CERT_ERRORS" Then

    '                        tcmd.CommandText = "DELETE FROM WFLOCAL..TEMPWIP"
    '                        tcmd.ExecuteNonQuery()

    '                        ' cmd.ExecuteNonQuery()

    '                        SBC.BulkCopyTimeout = 10
    '                        SBC.DestinationTableName = "WFLOCAL..TEMPWIP"

    '                        '    Debug.Print(Now)
    '                        'cmd.ExecuteNonQuery()
    '                        SBC.WriteToServer(dt)
    '                        Dim extrastring As String = ")"

    '                        ' If P = 1 Then extrastring = "AND S.WC_DESCR = T.WC_DESCR)"
    '                        tcmd.CommandText = "MERGE WFLOCAL.DBO.CERT_ERRORS1 AS T
    '	                                            USING (SELECT DISTINCT WORKORDERNO,	SUM(QTY) AS ORDERSTATUS,	METALLOT,	STOPWORK,	MRB_OK_TO_SHIP,	SHIP_HOLD,	WORKCENTER,	max(WC_DESCR) as WC_DESCR,	LOT_TYPE,	SELLING_PRICE,	PARTNO,	
    '			                                                  OPERATION,	SUM(QTY) AS QTY,	SCHEDULE_PRI_N,	OPER_DESC,	1 as ACTIVE,	MILESTONE,	MAX(DAYS_IN_WC) AS DAYS_IN_WC,	LAST_LABOR_CHG_D,	ACCUM_STD_VALUE,	HOURS_IN_OPER
    '			                                            FROM WFLOCAL..TEMPWIP 
    '				                                        GROUP BY WORKORDERNO,	METALLOT,	STOPWORK,	MRB_OK_TO_SHIP,	SHIP_HOLD,	WORKCENTER,		LOT_TYPE,	SELLING_PRICE,	PARTNO,	
    '			                                                     OPERATION,	SCHEDULE_PRI_N,	OPER_DESC, ACTIVE,	MILESTONE,	LAST_LABOR_CHG_D,	ACCUM_STD_VALUE,	HOURS_IN_OPER
    '		                                               ) AS S
    '	                                            ON (T.WORKORDERNO = S.WORKORDERNO " & extrastring & " 

    '                                                WHEN NOT MATCHED BY TARGET
    '		                                            THEN INSERT(HOURS_IN_OPER, PARTNO, WORKORDERNO, ORDERSTATUS, METALLOT, STOPWORK, MRB_OK_TO_SHIP, SHIP_HOLD, WORKCENTER, OPERATION, OPER_DESC, WC_DESCR, 
    '			                                             LOT_TYPE, SELLING_PRICE, QTY, MILESTONE, ACTIVE, SCHEDULE_PRI_N, LAST_LABOR_CHG_D, ACCUM_STD_VALUE)

    '		                                                VALUES(S.HOURS_IN_OPER, S.PARTNO, S.WORKORDERNO, S.ORDERSTATUS, S.METALLOT, S.STOPWORK, S.MRB_OK_TO_SHIP, S.SHIP_HOLD, S.WORKCENTER, 
    '		                                                S.OPERATION, S.OPER_DESC, S.WC_DESCR, S.LOT_TYPE, S.SELLING_PRICE, S.QTY, S.MILESTONE, 
    '		                                                S.ACTIVE, S.SCHEDULE_PRI_N, S.LAST_LABOR_CHG_D, S.ACCUM_STD_VALUE)

    '	                                            WHEN MATCHED 
    '		                                            THEN UPDATE SET	
    '			                                            HOURS_IN_OPER=S.HOURS_IN_OPER,
    '			                                            ORDERSTATUS = ISNULL(S.ORDERSTATUS,T.ORDERSTATUS),
    '			                                            PARTNO = ISNULL(S.PARTNO,T.PARTNO),
    '			                                            METALLOT = ISNULL(S.METALLOT,T.METALLOT),
    '			                                            STOPWORK = ISNULL(S.STOPWORK,T.STOPWORK),
    '			                                            MRB_OK_TO_SHIP = ISNULL(S.MRB_OK_TO_SHIP,T.MRB_OK_TO_SHIP),
    '			                                            SHIP_HOLD = ISNULL(S.SHIP_HOLD,T.SHIP_HOLD),
    '			                                            WORKCENTER = ISNULL(S.WORKCENTER,T.WORKCENTER),
    '			                                            OPERATION = ISNULL(S.OPERATION,T.OPERATION),
    '			                                            WC_DESCR = ISNULL(S.WC_DESCR, T.WC_DESCR),
    '			                                            OPER_DESC = ISNULL(S.OPER_DESC, T.OPER_DESC),
    '			                                            LOT_TYPE  = ISNULL(S.LOT_TYPE, T.LOT_TYPE),
    '			                                            SELLING_PRICE  = ISNULL(S.SELLING_PRICE,T.SELLING_PRICE),
    '			                                            DAYS_IN_WC  = ISNULL(S.DAYS_IN_WC ,T.DAYS_IN_WC ),
    '			                                            QTY=ISNULL(S.QTY, T.QTY),
    '			                                            MILESTONE=ISNULL(S.MILESTONE, CASE WHEN T.OPERATION>9998 THEN 13 ELSE T.MILESTONE END),
    '			                                            SCHEDULE_PRI_N = ISNULL(S.SCHEDULE_PRI_N, T.SCHEDULE_PRI_N),
    '			                                            LAST_LABOR_CHG_D=ISNULL(S.LAST_LABOR_CHG_D, T.LAST_LABOR_CHG_D),
    '			                                            ACCUM_STD_VALUE=ISNULL(S.ACCUM_STD_VALUE, T.ACCUM_STD_VALUE),
    '			                                            ACTIVE = 1
    '	                                            WHEN NOT MATCHED BY SOURCE AND ACTIVE <> 1
    '			                                            THEN UPDATE SET ACTIVE = 2;"

    '                        tcmd.ExecuteNonQuery()


    '                        'cmd.ExecuteNonQuery()

    '                        ' Debug.Print(Now)
    '                    End If

    '                    Debug.Print(SBC.DestinationTableName & " MERGED WITH " & TableName & " USING " & RespNames(P) & " IN " & (sw.ElapsedMilliseconds / 1000) & " SECONDS")
    '                    sw.Restart()
    '                    ''console.CursorLeft = 20
    '                    ''console.WriteLine(TableName & " UPDATED Using " & RespNames(P))


    '                    'cmd.CommandText = "WFLOCAL.DBO.AddShipments"
    '                    'ElseIf TableName = "TPUT" Then
    '                    'cmd.CommandText = "WFLOCAL.DBO.UpdateThruput"
    '                    'ElseIf TableName = "LABOR" Then
    '                    'cmd.CommandText = "WFLOCAL.DBO.UpdateLabor"
    '                    'ElseIf TableName = "WIP_MOVE_HIST" Then
    '                    'cmd.CommandText = "WFLOCAL.DBO.UPDATE_WIP_HIST"


    '                    '  'console.WriteLine("  (" & UpdatedRows & " Rows Updated)")
    'NEXTP:
    '                Next P


    '                tcmd.CommandType = CommandType.Text
    '                If InStr(WF.GetRequests, "lots") <> 0 Then

    '                    tcmd.CommandText = "UPDATE WFLOCAL.DBO.CERT_ERRORS1 Set ACTIVE = 0 WHERE ACTIVE = 2"
    '                    'Dim RWS As Integer = cmd.ExecuteNonQuery()
    '                    '    If RWS <> -1 Then Stop
    '                    '       UpdatedRows = UpdatedRows + RWS
    '                    '   
    '                    tcmd.ExecuteNonQuery()
    '                    tcmd.CommandType = CommandType.StoredProcedure
    '                    tcmd.CommandText = "wflocal..cleanup"
    '                    tcmd.Parameters.Clear()
    '                    tcmd.ExecuteNonQuery()

    '                End If
    '                tcmd.CommandType = CommandType.Text
    '                If InStr(WF.GetRequests, "ships") > 0 Then
    '                    tcmd.CommandText = "DELETE FROM WFLOCAL..TEMPSHIPMENTS"
    '                ElseIf InStr(WF.GetRequests, "opens") > 0 Then
    '                    tcmd.CommandText = "DELETE FROM WFLOCAL..TEMPOPENS"
    '                ElseIf InStr(WF.GetRequests, "lots") > 0 Then
    '                    tcmd.CommandText = "DELETE FROM WFLOCAL..TEMPWIP"
    '                End If
    '                tcmd.ExecuteNonQuery()
    '                Trans.Commit()
    '            Catch ex As Exception
    '                Console.WriteLine("Commit Exception Type: {0}", ex.GetType())
    '                Console.WriteLine("  Message: {0}", ex.Message)
    '                Try
    '                    Trans.Rollback()
    '                Catch ex2 As Exception
    '                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
    '                    Console.WriteLine("  Message: {0}", ex2.Message)
    '                End Try
    '            End Try
    '        End Using
    '        Debug.Print("FULL UPDATE COMPLETED IN " & ty.ElapsedMilliseconds / 1000 & " seconds")
    '        ' PPForm.Text = "PLEASE WAIT, UPDATING TABLES...DONE"
    '    End Sub

    Private Sub ExecuteSqlNonQueryTransaction(ByRef cmd As SqlCommand, cn As SqlConnection)
        Dim Trans As SqlTransaction
        Trans = cn.BeginTransaction()
        cmd.Transaction = Trans
        Try
            Dim t As Integer = cmd.ExecuteNonQuery()
            Debug.Print(t & " rows affected")
            Trans.Commit()
            'Console.WriteLine("Transaction Committed")
        Catch ex As Exception
            Try : class1.Serialize(PPForm.ExceptionPath, ex) : Catch : End Try
            MsgBox("Commit Exception Type: {0}", ex.GetType().ToString)
            MsgBox("  Message: {0}", ex.Message)
            ' Attempt to roll back the transaction. 
            Try
                Trans.Rollback()
            Catch ex2 As Exception
                Try : class1.Serialize(PPForm.ExceptionPath, ex2) : Catch : End Try
                ' This catch block will handle any errors that may have occurred 
                ' on the server that would cause the rollback to fail, such as 
                ' a closed connection.
                MsgBox("Rollback Exception Type: {0}", ex2.GetType().ToString)
                MsgBox("  Message: {0}", ex2.Message)
            End Try

        End Try
    End Sub

    Sub OpensUpdater(wf As WebfocusModule)
        Dim trans As SqlTransaction
        Dim cdataset As New DataSet

        Dim j As Object = wf.GetResponse("opens").Response

        Dim ConnectionString As String = "Server=SLREPORT01; Database=WFLocal; persist security info=False; trusted_connection=Yes;"
        Using cn As New SqlConnection(ConnectionString)
            Try
                cn.Open()

                Using cmd As New SqlCommand("", cn)
                    trans = cn.BeginTransaction(Environment.UserName)
                    cmd.Connection = cn
                    cmd.Transaction = trans
                    Dim Query As String = "UPDATE wflocal.dbo.OPEN_ORDERS1 Set ACTIVE = 2 WHERE ACTIVE <> 0"
                    cmd.CommandText = Query
                    cmd.ExecuteNonQuery()
                    'If Hour(Now) = 20 Then
                    ' cmd.CommandText = "DELETE FROM WFLOCAL.DBO.OPEN_ORDERS WHERE ACTIVE =2"
                    'cmd.ExecuteNonQuery()
                    'End If

                    Query = "Select column_name, data_type FROM WFLOCAL.INFORMATION_SCHEMA.COLUMNS" & vbCrLf &
                            "WHERE WFLOCAL.INFORMATION_SCHEMA.COLUMNS.TABLE_NAME='OPEN_ORDERS1'"
                    cmd.CommandText = Query

                    Dim ColumnInfo As New List(Of String())
                    Dim ColNumbers As New List(Of Integer)

                    Using dr As SqlDataReader = cmd.ExecuteReader
                        While dr.Read()
                            Dim Y As Integer = GetColumnNumber(j, dr("column_name").ToString)
                            If Y <> -1 Then
                                ColumnInfo.Add({dr("column_name").ToString, dr("data_type").ToString, Y})
                            End If
                        End While
                    End Using

                    Dim QueryRoot As String = "INSERT INTO WFLOCAL.DBO.OPEN_ORDERS1 ("

                    For Each col In ColumnInfo
                        QueryRoot = QueryRoot & col(0) & ", "
                    Next
                    QueryRoot = Left(QueryRoot, Len(QueryRoot) - 2) & ") values ("
                    For Each col In ColumnInfo
                        QueryRoot = QueryRoot & "@" & col(0) & ", "
                    Next
                    QueryRoot = Left(QueryRoot, Len(QueryRoot) - 2) & ")"

                    For RowNum = 1 To j.length - 1
                        With cmd.Parameters
                            Query = QueryRoot
                            .Clear()

                            For Each Col In ColumnInfo

                                If Col(1) = "nvarchar" Then
                                    .Add("@" & Col(0), SqlDbType.NVarChar).Value = j(RowNum)(Col(2))
                                    'Query = Replace(Query, "@" & Col(0), "'" & j(RowNum)(Col(2)) & "'")
                                ElseIf Col(1) = "float" Then
                                    ' Debug.Print(j(RowNum)(Col(2)))
                                    .Add("@" & Col(0), SqlDbType.Float).Value = Replace(j(RowNum)(Col(2)), ",", "")
                                    'Query = Replace(Query, "@" & Col(0), CLng(j(RowNum)(Col(2))))
                                ElseIf Col(1) = "datetime" Then
                                    Dim dt As DateTime = DateTime.Parse(j(RowNum)(Col(2)))
                                    'Debug.Print(dt.ToString)
                                    If dt.Year > 1900 Then
                                        .Add("@" & Col(0), SqlDbType.DateTime).Value = dt
                                    Else
                                        dt = Now.AddYears(-100)
                                        .Add("@" & Col(0), SqlDbType.DateTime).Value = dt
                                    End If
                                    'Query = Replace(Query, "@" & Col(0), CLng(j(RowNum)(Col(2))))
                                End If
                            Next Col
                            .AddWithValue("@ACTIVE", 1)
                        End With

                        'console.Write(RowNum + 1 & "/" & j.length)

                        'console.CursorLeft = 0
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "WFLOCAL.DBO.OPENUPDATER1"
                        Dim y As Integer = cmd.ExecuteNonQuery()
                        'If y <> -1 Then Stop
                    Next RowNum
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = "UPDATE wflocal.dbo.OPEN_ORDERS1 SET ACTIVE = 0 WHERE ACTIVE = 2"
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "INSERT INTO WFLOCAL.DBO.PO_REVIEW  (SALES_ORDER_NO, CUST_NO, SALES, USERNAME, ttimestamp, prel, pship, erel, eship)" & vbCrLf &
                                            "Select DISTINCT B.SALES_ORDER_NO, B.CUSTOMER_NO, B.ADDED_BY, B.ADDED_BY, getdate(), 1, 1, 1, 1" & vbCrLf &
                                            "From DBO.OPEN_ORDERS B with (NOLOCK)" & vbCrLf &
                                            "Where Not EXISTS(SELECT distinct  B.SALES_ORDER_NO" & vbCrLf &
                                            "From DBO.PO_REVIEW with (NOLOCK)" & vbCrLf &
                                            "Where PO_REVIEW.SALES_ORDER_NO = B.SALES_ORDER_NO" & vbCrLf &
                                            ")" & vbCrLf &
                                            "" & vbCrLf
                    ' cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "wflocal.dbo.CleanTickets"
                    '  cmd.ExecuteNonQuery()
                    'Threading.Thread.Sleep(1)


                End Using
            Catch ex As Exception
                MsgBox("Commit Exception Type: {0}", ex.GetType().ToString)
                MsgBox("  Message: {0}", ex.Message)
                Try : class1.Serialize(PPForm.ExceptionPath, ex) : Catch : End Try
                ' Attempt to roll back the transaction. 
                Try
                    trans.Rollback()
                Catch ex2 As Exception
                    ' This catch block will handle any errors that may have occurred 
                    ' on the server that would cause the rollback to fail, such as 
                    ' a closed connection.
                    MsgBox("Rollback Exception Type: {0}", ex2.GetType().ToString)
                    MsgBox("  Message: {0}", ex2.Message)
                    Try : class1.Serialize(PPForm.ExceptionPath, ex2) : Catch : End Try
                End Try

            End Try
        End Using

    End Sub

    Private Function GetColumnNumber(InputTable()() As String, ColumLabel As String) As Integer

        Dim x As Integer = 0
        Do While ColumLabel <> InputTable(0)(x) And x < UBound(InputTable(0))
            x = x + 1
        Loop
        If x = UBound(InputTable(0)) And ColumLabel <> InputTable(0)(x) Then
            Return -1
            Debug.Print(ColumLabel)
        Else
            Return x
        End If
    End Function

End Module
