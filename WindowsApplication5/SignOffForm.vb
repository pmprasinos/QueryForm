Imports System.ComponentModel

Public Class SignOffForm
    Dim cmd As New SqlClient.SqlCommand
    Dim CNUM As String = ""
    Dim ReAssign As String = ""
    Public Shared SoNumber As String
    Dim LockChecks As Boolean = False
    Private Sub SignOffForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        cmd.Connection = PPForm.objConnCurr
        GetUsersAndNotes()
        PullTicketInfo()
        CheckChecks()
        If CheckBoxQualityRel.Checked And CheckBoxQualityShip.Checked And CheckBoxEngRel.Checked And CheckBoxEngShip.Checked And CheckBoxEngRel.Checked And CheckBoxSalesRel.Checked And CheckBoxSalesShip.Checked And CheckBoxProdRel.Checked And CheckBoxProdShip.Checked Then
            For Each C As Control In Me.Controls
                C.Enabled = False
            Next C
        End If
        If Not (CheckBoxSalesRel.Checked And CheckBoxSalesShip.Checked And CheckBoxQualityRel.Checked And CheckBoxQualityShip.Checked And CheckBoxEngRel.Checked And CheckBoxEngShip.Checked And CheckBoxProdRel.Checked And CheckBoxProdShip.Checked) Then
            RichTextBox2.Enabled = True
            RichTextBox1.Enabled = True
            Button1.Enabled = True
        End If
    End Sub

    Private Sub CheckChecks()
        If Not LockChecks Then
            LockChecks = True
            If CheckBoxSalesRel.Enabled Then
                CheckBoxSalesShip.Enabled = CheckBoxSalesRel.Checked
                ' CheckBoxSalesRel.Enabled = Not CheckBoxSalesShip.Checked
            End If
            If CheckBoxProdRel.Enabled Then
                CheckBoxProdShip.Enabled = CheckBoxProdRel.Checked
                'CheckBoxProdRel.Enabled = Not CheckBoxProdShip.Checked
            End If
            If CheckBoxEngRel.Enabled Then
                CheckBoxEngShip.Enabled = CheckBoxEngRel.Checked
                'CheckBoxEngRel.Enabled = Not CheckBoxEngShip.Checked
            End If
            If CheckBoxQualityRel.Enabled Then
                CheckBoxQualityShip.Enabled = CheckBoxQualityRel.Checked
                'CheckBoxQualityRel.Enabled = Not CheckBoxQualityShip.Checked
                'CheckBoxQualityRel.Enabled = Not CheckBoxQualityRel.Checked
            End If
            LockChecks = False
        End If


    End Sub



    Private Sub GetUsersAndNotes()
        Dim ACCSE As New AutoCompleteStringCollection
        Dim ACCSQ As New AutoCompleteStringCollection
        Dim ACCSP As New AutoCompleteStringCollection
        Dim ACCSS As New AutoCompleteStringCollection
        PPForm.objConnCurr.Open()

        Try
            Dim role As String = GetRole()
            If InStr(role, "S") Then
                CheckBoxProdRel.Enabled = True
                CheckBoxEngRel.Enabled = True
                CheckBoxSalesRel.Enabled = True
                TextBoxQual.Enabled = True
            End If
            If InStr(role, "Q") > 0 Then
                CheckBoxQualityRel.Enabled = True
                TextBoxQual.Enabled = True

            End If
            If InStr(role, "E") > 0 Then
                CheckBoxEngRel.Enabled = True
                TextBoxEng.Enabled = True
            End If
            If InStr(role, "P") > 0 Then
                CheckBoxProdRel.Enabled = True
                TextBoxProd.Enabled = True
            End If

            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT ISNULL(username, '') AS USERNAME, ISNULL(ROLE,'') AS ROLE FROM WFLOCAL..USERS WHERE ISNULL(ROLE, '')<>''"


            Using SQR As SqlClient.SqlDataReader = cmd.ExecuteReader
                Do While SQR.Read()

                    If InStr(SQR("ROLE"), "S") Then ACCSS.Add(SQR("USERNAME"))
                    If InStr(SQR("ROLE"), "Q") Then ACCSQ.Add(SQR("USERNAME"))
                    If InStr(SQR("ROLE"), "P") Then ACCSP.Add(SQR("USERNAME"))
                    If InStr(SQR("ROLE"), "E") Then ACCSE.Add(SQR("USERNAME"))
                Loop
            End Using
        Catch : Finally

            PPForm.objConnCurr.Close()
        End Try
        TextBoxEng.AutoCompleteCustomSource = ACCSE
        TextBoxEng.AutoCompleteSource = AutoCompleteSource.CustomSource
        TextBoxSales.AutoCompleteCustomSource = ACCSS
        TextBoxSales.AutoCompleteSource = AutoCompleteSource.CustomSource
        TextBoxQual.AutoCompleteCustomSource = ACCSQ
        TextBoxQual.AutoCompleteSource = AutoCompleteSource.CustomSource
        TextBoxProd.AutoCompleteCustomSource = ACCSP
        TextBoxProd.AutoCompleteSource = AutoCompleteSource.CustomSource
        CheckChecks()
    End Sub

    Public Function PullTicketInfo() As String()
        LockChecks = True
        PPForm.objConnCurr.Open()
        Dim Role(4) As String
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT SALES_ORDER_NO, TTIMESTAMP, USERNAME, CUST_NO, NOTES, QUALITY, ENG, SALES, PROD, ISNULL(QREL, 'FALSE') AS QREL, ISNULL(QSHIP, 'FALSE') AS QSHIP, ISNULL(EREL, 'FALSE') AS EREL, ISNULL(ESHIP, 'FALSE') AS ESHIP, " & vbCrLf &
                            " ISNULL(SREL, 'FALSE') AS SREL, ISNULL(SSHIP, 'FALSE') AS SSHIP, ISNULL(PREL, 'FALSE') AS PREL, ISNULL(PSHIP, 'FALSE') AS PSHIP FROM WFLOCAL..PO_REVIEW WHERE SALES_ORDER_NO =@SO" & vbCrLf &
                            "ORDER BY TTIMESTAMP DESC"

            If PPForm.TabControl1.SelectedIndex = 0 Then
                cmd.Parameters.AddWithValue("@SO", PPForm.DataGridView1.Item(PPForm.DataGridView1.Columns("SALES_ORDER").Index, PPForm.DataGridView1.SelectedCells(0).RowIndex).Value)
            Else
                cmd.Parameters.AddWithValue("@SO", PPForm.DataGridView3.Item(PPForm.DataGridView3.Columns("SO3").Index, PPForm.DataGridView3.SelectedCells(0).RowIndex).Value)
            End If


            Using SQR As SqlClient.SqlDataReader = cmd.ExecuteReader
                If SQR.HasRows Then
                    Dim FIRSTROW As Boolean = True
                    Do While SQR.Read()

                        RichTextBox1.Text = SQR("TTIMESTAMP") & SQR("USERNAME") & "--" & SQR("NOTES") & vbCrLf & vbCrLf & RichTextBox1.Text
                        If FIRSTROW Then
                            TextBoxQual.Text = SQR("QUALITY").ToString
                            TextBoxEng.Text = SQR("ENG").ToString
                            TextBoxSales.Text = SQR("SALES").ToString
                            TextBoxProd.Text = SQR("PROD").ToString
                            CheckBoxQualityRel.Checked = CBool(SQR("QREL").ToString)
                            CheckBoxQualityShip.Checked = CBool(SQR("QSHIP").ToString)
                            CheckBoxSalesRel.Checked = CBool(SQR("SREL").ToString)
                            CheckBoxSalesRel.Enabled = Not CBool(SQR("SREL").ToString)
                            CheckBoxSalesShip.Checked = CBool(SQR("SSHIP").ToString)
                            CheckBoxEngRel.Checked = CBool(SQR("EREL").ToString)
                            CheckBoxEngRel.Enabled = Not CBool(SQR("EREL").ToString)
                            CheckBoxEngShip.Checked = CBool(SQR("ESHIP").ToString)
                            CheckBoxProdRel.Checked = CBool(SQR("PREL").ToString)
                            CheckBoxProdRel.Enabled = Not CBool(SQR("PREL").ToString)
                            CheckBoxProdShip.Checked = CBool(SQR("PSHIP").ToString)
                            CNUM = SQR("CUST_NO").ToString
                            Me.Text = "Sign of Activity for Sales Order: " & SQR("SALES_ORDER_NO").ToString & "Customer: " & SQR("CUST_NO").ToString
                            FIRSTROW = False
                        End If
                    Loop
                End If
            End Using
        Catch ex As Exception
            Try : class1.Serialize(PPForm.ExceptionPath, ex) : Catch : End Try
        Finally
            PPForm.objConnCurr.Close()
            LockChecks = False
        End Try

        Return Role

    End Function

    Private Function GetRole() As String

        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT ROLE FROM WFLOCAL..USERS WHERE USERNAME = @USERNAME"
        cmd.Parameters.AddWithValue("@USERNAME", UCase(Environment.UserName))
        'cmd.Parameters.AddWithValue("@USERNAME", "bkenjale")

        Dim Role As String
        Using SQR As SqlClient.SqlDataReader = cmd.ExecuteReader
            If SQR.HasRows Then
                SQR.Read()
                Role = SQR("ROLE")
            End If
        End Using

        Return Role
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBoxQual.Text = "" Then
            MsgBox("Must assign a quality rep")
            Exit Sub
        End If
        cmd.CommandText = "INSERT INTO WFLOCAL..PO_REVIEW ( USERNAME, SALES_ORDER_NO, TTIMESTAMP, NOTES, ASSIGN_TO, QUALITY, ENG, SALES, PROD, CUST_NO, EREL, ESHIP, PREL, PSHIP, QREL, QSHIP, SREL, SSHIP) " & vbCrLf &
                        "VALUES (@USERNAME, @SALES_ORDER_NO, @TTIMESTAMP, @NOTES, @ASSIGN_TO, @QUALITY, @ENG, @SALES, @PROD, @CUST_NO, @EREL, @ESHIP, @PREL, @PSHIP, @QREL, @QSHIP, @SREL, @SSHIP)"
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@USERNAME", Environment.UserName)
        '  Debug.Print(PPForm.DataGridView1.Item(PPForm.DataGridView1.Columns("SALES_ORDER").Index, PPForm.DataGridView1.SelectedCells(0).RowIndex).Value)
        If PPForm.TabControl1.SelectedIndex = 0 Then
            cmd.Parameters.AddWithValue("@SALES_ORDER_NO", PPForm.DataGridView1.Item(PPForm.DataGridView1.Columns("SALES_ORDER").Index, PPForm.DataGridView1.SelectedCells(0).RowIndex).Value)
        Else
            cmd.Parameters.AddWithValue("@SALES_ORDER_NO", PPForm.DataGridView3.Item(PPForm.DataGridView3.Columns("SO3").Index, PPForm.DataGridView3.SelectedCells(0).RowIndex).Value)
        End If
        cmd.Parameters.AddWithValue("@TTIMESTAMP", DateTime.Now())
        cmd.Parameters.AddWithValue("@NOTES", RichTextBox2.Text)
        cmd.Parameters.AddWithValue("@ENG", UCase(TextBoxEng.Text))
        cmd.Parameters.AddWithValue("@QUALITY", UCase(TextBoxQual.Text))
        cmd.Parameters.AddWithValue("@SALES", UCase(TextBoxSales.Text))
        cmd.Parameters.AddWithValue("@PROD", UCase(TextBoxProd.Text))
        cmd.Parameters.AddWithValue("@QREL", CheckBoxQualityRel.Checked)
        cmd.Parameters.AddWithValue("@QSHIP", CheckBoxQualityShip.Checked)
        cmd.Parameters.AddWithValue("@SREL", CheckBoxSalesRel.Checked)
        cmd.Parameters.AddWithValue("@SSHIP", CheckBoxSalesShip.Checked)
        cmd.Parameters.AddWithValue("@EREL", CheckBoxEngRel.Checked)
        cmd.Parameters.AddWithValue("@ESHIP", CheckBoxEngShip.Checked)
        cmd.Parameters.AddWithValue("@PREL", CheckBoxProdRel.Checked)
        cmd.Parameters.AddWithValue("@PSHIP", CheckBoxProdShip.Checked)
        cmd.Parameters.AddWithValue("@CUST_NO", CNUM)
        cmd.Parameters.AddWithValue("@ASSIGN_TO", "")
        PPForm.objConnCurr.Open()
        Try
            cmd.ExecuteNonQuery()
        Catch : Finally
            PPForm.objConnCurr.Close()
        End Try
        Me.Close()


    End Sub

    Private Sub CheckBoxSalesShip_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBoxSalesShip.CheckStateChanged
        CheckChecks()
    End Sub

    Private Sub CheckBoxQualityRel_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBoxQualityRel.CheckStateChanged
        CheckChecks()
    End Sub

    Private Sub CheckBoxQualityShip_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxQualityShip.CheckStateChanged
        CheckChecks()
    End Sub

    Private Sub CheckBoxEngRel_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBoxEngRel.CheckStateChanged
        CheckChecks()
    End Sub

    Private Sub CheckBoxEngShip_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBoxEngShip.CheckStateChanged
        CheckChecks()
    End Sub

    Private Sub CheckBoxProdRel_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBoxProdRel.CheckStateChanged
        CheckChecks()
    End Sub

    Private Sub CheckBoxProdShip_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBoxProdShip.CheckStateChanged
        CheckChecks()
    End Sub

    Private Sub CheckBoxSalesRel_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBoxSalesRel.CheckStateChanged
        CheckChecks()
    End Sub

    Private Sub SignOffForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        RichTextBox1.Clear()
        RichTextBox2.Clear()
        TextBoxProd.Clear()
        TextBoxEng.Clear()
        TextBoxQual.Clear()
        TextBoxSales.Clear()
        CheckBoxEngRel.Checked = False
        CheckBoxEngShip.Checked = False
        CheckBoxProdRel.Checked = False
        CheckBoxProdShip.Checked = False
        CheckBoxSalesShip.Checked = False
        CheckBoxSalesRel.Checked = False
        CheckBoxQualityRel.Checked = False
        CheckBoxQualityShip.Checked = False
    End Sub

    Private Sub SignOffForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Threading.Thread.Sleep(100)
    End Sub

End Class