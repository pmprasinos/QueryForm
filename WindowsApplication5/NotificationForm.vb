Public Class NotificationForm

    Public Shared Lots As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim TagLine As String
        Dim Options As String
        Dim cmd As New SqlClient.SqlCommand
        cmd.Connection = PPForm.objConnCurr
        cmd.CommandText = "INSERT INTO WFLOCAL..NOTIFICATIONS (WORKORDERNO, OPERATIONNO, EMAIL, USERNAME, DTSTAMP, OPTIONS)" & vbCrLf &
                                    "VALUES(@WORKORDERNO, @OPERATIONNO, @EMAIL, @USERNAME, @DTSTAMP, @OPTIONS)"

        cmd.Parameters.AddWithValue("@OPERATIONNO", TextBoxOperation.Text)
        cmd.Parameters.AddWithValue("@EMAIL", TextBoxEmail.Text)
        cmd.Parameters.AddWithValue("@USERNAME", Environment.UserName)
        cmd.Parameters.AddWithValue("@DTSTAMP", DateTime.Now)
        cmd.Parameters.AddWithValue("@WORKORDERNO", "")
        Options = ""
        If CheckBoxALL.Checked Then Options = "ALL(" & Split(Lots).Length & ")"
        If CheckBoxANYScrapped.Checked Then Options = Options & " SCRAP"
        cmd.Parameters.AddWithValue("@OPTIONS", Options)
        PPForm.objConnCurr.Open()
        Try
            For Each Lot As String In Split(Lots, ",")
                cmd.Parameters.Item("@WORKORDERNO").Value = Lot
                If Lot <> "" Then
                    cmd.ExecuteNonQuery()
                End If
            Next Lot

        Catch ex As Exception : Finally
            PPForm.objConnCurr.Close()
        End Try

        Me.Close()
    End Sub



    Private Sub CheckBoxALL_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxALL.CheckedChanged
        If CheckBoxANY.Checked Then CheckBoxANY.CheckState = CheckState.Unchecked
    End Sub

    Private Sub CheckBoxANY_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxANY.CheckedChanged
        If CheckBoxALL.Checked Then CheckBoxALL.CheckState = CheckState.Unchecked
    End Sub
End Class