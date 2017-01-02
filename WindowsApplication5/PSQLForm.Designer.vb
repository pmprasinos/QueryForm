<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PSQLForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.MATCH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RANK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SHOP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PART = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.VIEWWAXTECHCARDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LOOKUPOPENSSHIPMENTSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MATCH, Me.RANK, Me.SHOP, Me.PART})
        Me.DataGridView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DataGridView1.Location = New System.Drawing.Point(0, 1)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DataGridView1.Size = New System.Drawing.Size(375, 183)
        Me.DataGridView1.TabIndex = 0
        '
        'MATCH
        '
        Me.MATCH.HeaderText = "MATCH"
        Me.MATCH.Name = "MATCH"
        Me.MATCH.Visible = False
        Me.MATCH.Width = 50
        '
        'RANK
        '
        Me.RANK.HeaderText = "#"
        Me.RANK.Name = "RANK"
        Me.RANK.ReadOnly = True
        Me.RANK.Width = 30
        '
        'SHOP
        '
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle1.NullValue = Nothing
        Me.SHOP.DefaultCellStyle = DataGridViewCellStyle1
        Me.SHOP.HeaderText = "SHOP"
        Me.SHOP.Name = "SHOP"
        Me.SHOP.ReadOnly = True
        '
        'PART
        '
        Me.PART.HeaderText = "PART"
        Me.PART.Name = "PART"
        Me.PART.ReadOnly = True
        Me.PART.Width = 200
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VIEWWAXTECHCARDToolStripMenuItem, Me.LOOKUPOPENSSHIPMENTSToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(233, 48)
        '
        'VIEWWAXTECHCARDToolStripMenuItem
        '
        Me.VIEWWAXTECHCARDToolStripMenuItem.Name = "VIEWWAXTECHCARDToolStripMenuItem"
        Me.VIEWWAXTECHCARDToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
        Me.VIEWWAXTECHCARDToolStripMenuItem.Text = "VIEW WAX TECH CARD"
        '
        'LOOKUPOPENSSHIPMENTSToolStripMenuItem
        '
        Me.LOOKUPOPENSSHIPMENTSToolStripMenuItem.Name = "LOOKUPOPENSSHIPMENTSToolStripMenuItem"
        Me.LOOKUPOPENSSHIPMENTSToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
        Me.LOOKUPOPENSSHIPMENTSToolStripMenuItem.Text = "LOOK UP OPENS/SHIPMENTS"
        '
        'PSQLForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(376, 169)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "PSQLForm"
        Me.Text = "PSQLForm"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents MATCH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RANK As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SHOP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PART As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents VIEWWAXTECHCARDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LOOKUPOPENSSHIPMENTSToolStripMenuItem As ToolStripMenuItem
End Class
