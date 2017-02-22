
Public Class SQLGenerator

    Public FilterText As String
    Public QueryText As String
    Public Parameters As List(Of String)

    'Property XOpen() As String
    '    Get
    '        Return FilterText
    '    End Get
    '    Set(value As String)
    '        XOpen = value
    '        QueryText = MakeXOpenQuery(FilterText)(0)
    '        Parameters = MakeXOpenQuery(FilterText)(1)
    '    End Set
    'End Property

    Public Shared Function CheckOpenPoReviews(UserName As String) As String

        Dim cmd As New SqlClient.SqlCommand
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "OpenTicketsQtyPerUser"
        cmd.Parameters.AddWithValue("@USERNAME", UCase(UserName))
        Dim H As String = UCase(Environment.UserName)
        'MsgBox("'" & UCase(Environment.UserName) & "'")
        cmd.Connection = PPForm.objConnCurr
        Try
            PPForm.objConnCurr.Open()


            Using RD As SqlClient.SqlDataReader = cmd.ExecuteReader

                While RD.Read
                    If RD("SREL").ToString = DBNull.Value.ToString Then GoTo notickets
                    Dim RelHold As Integer = CInt(RD("SREL")) + CInt(RD("QREL")) + CInt(RD("PREL")) + CInt(RD("EREL"))
                    Dim ShipHold As Integer = CInt(RD("SSHIP")) + CInt(RD("QSHIP")) + CInt(RD("PSHIP")) + CInt(RD("ESHIP"))


                    'Debug.Print(PPForm.TabControl1.TabPages.Count)
                    If RelHold + ShipHold <> 0 And PPForm.TabControl1.TabPages.Count <> 3 Then

                        PPForm.TabControl1.TabPages.Insert(2, PPForm.TPLIST(0))
                    ElseIf RelHold + ShipHold <> 0 Then
                        CheckOpenPoReviews = "You have " & RelHold & " active holds on release & " & ShipHold & " active holds on shipment"
                    Else
notickets:
                        CheckOpenPoReviews = "You have zero active holds at this time"

                        If PPForm.TabControl1.TabPages.Count = 3 Then
                            PPForm.TPLIST.Clear()
                            PPForm.TPLIST.Add(PPForm.TabControl1.TabPages("TabPage3"))
                            PPForm.TabControl1.TabPages.Remove(PPForm.TabControl1.TabPages("TabPage3"))
                        End If
                    End If



                End While
            End Using

        Catch ex As Exception
            Try : class1.Serialize(PPForm.ExceptionPath, ex) : Catch : End Try

        Finally
            PPForm.objConnCurr.Close()

        End Try
        Return CheckOpenPoReviews

    End Function



    Public Shared Sub PullTicketsByUser(ByRef SelectCommand As SqlClient.SqlCommand, UserName As String)
        SelectCommand.Parameters.Clear()
        SelectCommand.CommandType = CommandType.StoredProcedure
        SelectCommand.CommandText = "WFLOCAL.dbo.OpenTicketsByUser"
        SelectCommand.Parameters.AddWithValue("@USERNAME", UCase(Environment.UserName))
    End Sub

    Public Shared Sub MakeXOpenQuery(ByRef SelectCommand As SqlClient.SqlCommand, FilterText As String, SearchValue As String, Optional ExcludeSpecial As Boolean = False)
        SelectCommand.Parameters.Clear()
        SelectCommand.CommandType = CommandType.StoredProcedure
        Dim ctext As String
        '' ctext =
        '     "Select LINE_TYPE, PARTNO, QTY_DUE, RELEASE_PRICE, CUSTOMER_PARTNO, CUSTOMER_PO_NO, CUST_ORDER_LINE, CUSTOMER_NO, COMPANY_NAME, " &
        '         "DUE_VALUE, REQUIRED_D, SCHED_D, PO_RECEIVED, SALES_ORDER_NO, ORDER_LINE, QTY_SHIPPED" & vbCr &
        '     "FROM WFLOCAL.DBO.OPEN_ORDERS" & vbCr
        SelectCommand.CommandText = "WFLOCAL.dbo.X_OPEN"
        ' PPForm.CheckBoxShowLots.Visible = False
        'ppform.SoLinesDataSet.Clear()

        If FilterText = "PO" Then
            '  ctext = ctext & "WHERE CUSTOMER_PO_NO = @CUSTOMER_PO_NO"
            SelectCommand.Parameters.Add("@CUSTOMER_PO_NO", SearchValue)
        ElseIf FilterText = "SN" Then
            '  ctext = ctext & "WHERE PARTNO = @PARTNO OR Z91_REF_PARTNO = @PARTNO"
            SelectCommand.Parameters.Add("@PARTNO", SearchValue)
        ElseIf FilterText = "SO" Then
            '   ctext = ctext & "WHERE SALES_ORDER_NO = @SALES_ORDER_NO"
            SelectCommand.Parameters.Add("@SALES_ORDER_NO", SearchValue)
        ElseIf FilterText = "PN" Then
            '   ctext = ctext & "WHERE CUSTOMER_PARTNO = @CUSTOMER_PARTNO"
            SelectCommand.Parameters.Add("@CUSTOMER_PARTNO", SearchValue)
        Else

            If ExcludeSpecial Then
                SelectCommand.Parameters.Add("@PARTNO", SearchValue)
            Else
                SelectCommand.Parameters.Add("@PARTNOex", SearchValue & "%")
            End If
            SelectCommand.Parameters.Add("@CUSTOMER_PO_NO", SearchValue & "%")
            SelectCommand.Parameters.Add("@SALES_ORDER_NO", SearchValue)
            SelectCommand.Parameters.Add("@CUSTOMER_PARTNO", SearchValue & "%")
        End If

        '  If ExcludeSpecial Then ctext = ctext & "AND PARTNO NOT LIKE '%S'" & vbCr
        '  SelectCommand.CommandText = ctext & vbCrLf & "ORDER BY REQUIRED_D"

    End Sub


    Public Shared Sub MakeShipmentsQuery(ByRef SelectCommand As SqlClient.SqlCommand, ByLot As Boolean, SearchValue As String, Optional HideSpcl As Boolean = False)
        SelectCommand.Parameters.Clear()
        Dim ctext As String = ""
        SelectCommand.CommandType = CommandType.StoredProcedure

        If ByLot Then
            'ctext =
            '        "Select PRICE, QTY_SHPPED, WORKORDERNO, WEIGH_BILL, INVOICE_NO, SHIPPED_DTIME, CUSTOMER_NO, SALES_ORDER_NO, COMPANYNAME, CUSTOMER_PARTNO, WEIGH_BILL, CUSTOMER_PO_NO, METALLOT, CUSTOMER_NO, PARTNO" & vbCr &
            '        "from wflocal.dbo.SHIPMENTS" & vbCr &
            '        "WHERE PARTNO=@SEARCH OR INVOICE_NO = @SEARCHEX Or CUSTOMER_PARTNO Like @SEARCHEX Or CUSTOMER_PO_NO Like @SEARCHEX Or SALES_ORDER_NO=@SEARCH" & vbCr
            SelectCommand.CommandText = "wflocal.dbo.ShipByLot"

        Else

            SelectCommand.CommandText = "wflocal.dbo.ShipByInvoice"
            If PPForm.ShipmentsByWeek Then
                PPForm.DataGridView2.Columns("CUSTOMER_NO").HeaderText = "WK#"
                SelectCommand.CommandText = "wflocal.dbo.ShipByInvoiceFORTAYLOR"
            Else
                PPForm.DataGridView2.Columns("CUSTOMER_NO").HeaderText = "CUST_NO"
            End If
        End If
            SelectCommand.Parameters.Add("@SEARCH", SearchValue)
        SelectCommand.Parameters.Add("@SEARCHEX", SearchValue & "%")
        If HideSpcl Then SelectCommand.Parameters.AddWithValue("@PARTNOEX", 6)
        '  SelectCommand.CommandText = ctext & "order by SHIPPED_DTIME DESC"

    End Sub

    Public Shared Sub MakeLotsQuery(ByRef SelectCommand As SqlClient.SqlCommand, ByLot As Integer, SearchValue As String, Optional FilterBy As Integer = 0)

        ' If IsNothing(SelectCommand) Or IsNothing(SearchValue) Or IsNothing(ByLot) Then Exit Sub
        '  MsgBox("got to makequery")
        SelectCommand.Parameters.Clear()

        Dim ctext As String
        SelectCommand.CommandType = CommandType.StoredProcedure
        If FilterBy = 1 Then
            SelectCommand.CommandText = "WFLOCAL.DBO.LOTSBYDEPT"
            SelectCommand.Parameters.AddWithValue("@DEPARTMENT", SearchValue)
        ElseIf ByLot = 0 Then
            SelectCommand.CommandText = "wflocal.dbo.XR"
        ElseIf ByLot = 1 Then
            SelectCommand.CommandText = "wflocal.dbo.XRLot"
        ElseIf ByLot = 2 Then
            SelectCommand.CommandText = "wflocal.dbo.XRLotDetail"
        ElseIf ByLot = 3 Then
            SelectCommand.CommandText = "wflocal.dbo.X_R1"
        End If

        'SelectCommand.CommandText = ctext & "ORDER BY OPERATION"

        If FilterBy = 0 Then SelectCommand.Parameters.Add("@SHOP", SearchValue)
        'Dim p As New SqlClient.SqlParameter

        '        p.ParameterName = "@SHOP"
        '       p.Value = SearchValue
        '      p.DbType = DbType.String
        '     SelectCommand.Parameters.Add(p)
        '  MsgBox("finished")

    End Sub
End Class
