Namespace My

    ' The following events are available for MyApplication: 
    ' 
    ' Startup: Raised when the application starts, before the startup form is created. 
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally. 
    ' UnhandledException: Raised if the application encounters an unhandled exception. 
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected. 
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup

            'Debug.Print(Dir(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\SalesTrackData\*"))

            '   Try

            'If Not FileIO.FileSystem.DirectoryExists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\SalesTrackData") Then
            'FileIO.FileSystem.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\SalesTrackData")
            'End If

            ' UpdateData()
            Dim j As String(,) = getListOfMotonum()

                PPForm.PSQLArray = j
                '   Catch ex As Exception
doitagain:
                '   FileIO.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\SalesTrackData\" & Dir(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\SalesTrackData\*"))
                'If InStr(ex.ToString, "Microsoft.ACE.OLEDB.12.0", CompareMethod.Text) > 0 Then
                '    MsgBox("An error occured with your system's database enging" & Chr(10) & "Try installing the file: 'S:\PRASINOS\mods\WINDOWSFORMS\AccessDatabaseEngine.exe'" & Chr(10) & Chr(10) & Chr(10) & "See below for exception message:" & Chr(10) & Chr(10) & Chr(10) & ex.Message)
                'End If
                '     MsgBox("ERL=" & Err.Erl & Chr(10) & "InnerException=" & ex.ToString & Chr(10) & "Message=" & ex.Message & Chr(10) & "Source=" & ex.Source.ToString)
                ' End Try
                My.Settings.Save()

        End Sub

        Private Sub UpdateData()
tryonemoregain:

            ' If Not FileIO.FileSystem.DirectoryExists("\\slfs01\shared\prasinos\HOLDPULLS") Then


            Try
                    Dim ThisUserDocs As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                    ThisUserDocs = Replace(ThisUserDocs, "mreyes", "mgonzales")
                    ' If Not FileIO.FileSystem.DirectoryExists(ThisUserDocs & "\SalesTrackData") Then
                    ' FileIO.FileSystem.CreateDirectory(ThisUserDocs & "\SalesTrackData")
                    'End If

                    ' If Not FileIO.FileSystem.FileExists(ThisUserDocs & "\SalesTrackData\SalesInfo.accdb") Then
                    ' FileIO.FileSystem.CopyFile("\\SLFS01\shared\prasinos\solines.accdb", ThisUserDocs & "\SalesTrackData\SalesInfo.accdb", True)
                    'End If
                    ' If Not FileIO.FileSystem.FileExists(ThisUserDocs & "\SalesTrackData\StaticData.accdb") Then
                    ' FileIO.FileSystem.CopyFile("\\SLFS01\shared\prasinos\StaticData.accdb", ThisUserDocs & "\SalesTrackData\StaticData.accdb", True)
                    'End If

                    'PPForm.Text = "Open Orders Search (" & CStr(FileIO.FileSystem.GetFileInfo(ThisUserDocs & "\SalestrackData\Salesinfo.accdb").LastWriteTime.ToString) & ")"

                Catch ex As Exception

                    ' PPForm.Text = PPForm.Text & " OFFLINE"
                End Try
            ' Else
            '  Threading.Thread.Sleep(1000)
            ' GoTo tryonemoregain
            ' End If

        End Sub

        Public Function getListOfMotonum() As String(,)
            Dim SQL As String = "SELECT DISTINCT CUSTOMER_PARTNO, CASE WHEN LEN(PARTNO)=4 THEN '0' + PARTNO ELSE PARTNO END AS PARTNO 
                                    FROM WFLOCAL..SHOPS
                                    ORDER BY PARTNO DESC"
            Try
                If Not My.Computer.Network.Ping("10.60.3.1") Then MsgBox("Server SLREPORT01 is offline. Contact IT.")
            Catch ex As Exception
                MsgBox("Machine does not seem to be on the San Leandro Network. Check your connection and click OK.")
            End Try

            Dim ShopList As New List(Of String)
            Dim PartList As New List(Of String)
            Dim x As Integer = 0
            Using CMD As New SqlClient.SqlCommand(SQL)
                Using CN As New SqlClient.SqlConnection("Server=SLREPORT01; Database=WFLocal; User Id=PrasinosApps; Password=Wyman123-;Connection Timeout = 3;")
                    CMD.Connection = CN
                    Try

                        CN.Open()
                        'Debug.Print(CN.ConnectionTimeout)
                        Using sqrd As SqlClient.SqlDataReader = CMD.ExecuteReader

                            Do While sqrd.Read
                                PartList.Add(sqrd("CUSTOMER_PARTNO").ToString)
                                ShopList.Add(sqrd("PARTNO").ToString)
                                x = x + 1
                            Loop
                        End Using
                    Catch e As Exception
                        MessageBox.Show("There was an error accessing your server. DETAIL: " & e.ToString())
                    Finally
                        CN.Close()
                        ' Do some logging or something. 
                    End Try
                End Using
            End Using

            Dim ReturnArray(1, PartList.Count) As String
            x = 0
            For Each part In PartList
                ReturnArray(0, x) = part
                x = x + 1
            Next
            x = 0
            For Each Shop In ShopList
                ReturnArray(1, x) = Shop
                x = x + 1
            Next
            Return ReturnArray

        End Function

    End Class

End Namespace
