<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NotificationForm
    Inherits System.Windows.Forms.Form

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
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.CheckBoxALL = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CheckBoxANY = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBoxANYScrapped = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxOperation = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEmail.Location = New System.Drawing.Point(1, 29)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(285, 20)
        Me.TextBoxEmail.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.TextBoxEmail, "Seporate with ';' for multiple recipients")
        '
        'CheckBoxALL
        '
        Me.CheckBoxALL.AutoSize = True
        Me.CheckBoxALL.ContextMenuStrip = Me.ContextMenuStrip1
        Me.CheckBoxALL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CheckBoxALL.Location = New System.Drawing.Point(8, 114)
        Me.CheckBoxALL.Name = "CheckBoxALL"
        Me.CheckBoxALL.Size = New System.Drawing.Size(180, 17)
        Me.CheckBoxALL.TabIndex = 3
        Me.CheckBoxALL.Text = "ALL lots enter specified operation"
        Me.ToolTip1.SetToolTip(Me.CheckBoxALL, "...CURRENTLY UNAVAILABLE..." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Hold notification until at least one daughter from" &
        " all lots specified have hit or passed the specified operation" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Rule will term" &
        "inate once notification is sent")
        Me.CheckBoxALL.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'CheckBoxANY
        '
        Me.CheckBoxANY.AutoSize = True
        Me.CheckBoxANY.Location = New System.Drawing.Point(8, 137)
        Me.CheckBoxANY.Name = "CheckBoxANY"
        Me.CheckBoxANY.Size = New System.Drawing.Size(186, 17)
        Me.CheckBoxANY.TabIndex = 4
        Me.CheckBoxANY.Text = "ANY lots enter specified operation"
        Me.ToolTip1.SetToolTip(Me.CheckBoxANY, "Hold notification until at least one daughter from all lots specified have hit or" &
        " passed the specified operation" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Rule will terminate once notification is sent" &
        "")
        Me.CheckBoxANY.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Notify and Terminate When:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Email:"
        '
        'CheckBoxANYScrapped
        '
        Me.CheckBoxANYScrapped.AutoSize = True
        Me.CheckBoxANYScrapped.Location = New System.Drawing.Point(8, 187)
        Me.CheckBoxANYScrapped.Name = "CheckBoxANYScrapped"
        Me.CheckBoxANYScrapped.Size = New System.Drawing.Size(217, 17)
        Me.CheckBoxANYScrapped.TabIndex = 7
        Me.CheckBoxANYScrapped.Text = "ANY lot is scrapped (includes daughters)"
        Me.ToolTip1.SetToolTip(Me.CheckBoxANYScrapped, "Notify when any lot or daughter of any lot is scrapped or WIPed to ENG or MRO" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) &
        "Rule will NOT terminate when notifcation is sent")
        Me.CheckBoxANYScrapped.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Notify When:"
        '
        'TextBoxOperation
        '
        Me.TextBoxOperation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxOperation.Location = New System.Drawing.Point(65, 52)
        Me.TextBoxOperation.Name = "TextBoxOperation"
        Me.TextBoxOperation.Size = New System.Drawing.Size(80, 20)
        Me.TextBoxOperation.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.TextBoxOperation, "Operation number for notification" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Use zero for DOC/PAC/FinGoods")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Operation:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1, 210)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(285, 23)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Submit"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'NotificationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 233)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxOperation)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CheckBoxANYScrapped)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CheckBoxANY)
        Me.Controls.Add(Me.CheckBoxALL)
        Me.Controls.Add(Me.TextBoxEmail)
        Me.Name = "NotificationForm"
        Me.Text = "Create Notification"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxEmail As TextBox
    Friend WithEvents CheckBoxALL As CheckBox
    Friend WithEvents CheckBoxANY As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckBoxANYScrapped As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxOperation As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
End Class
