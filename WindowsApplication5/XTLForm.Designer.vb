<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XTLForm
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
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.KeepInFrontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.XTLShop = New System.Windows.Forms.TextBox()
        Me.PARTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OP_DESCR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WC_DESC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WORKCENTER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DWELL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView3
        '
        Me.DataGridView3.AllowUserToAddRows = False
        Me.DataGridView3.AllowUserToDeleteRows = False
        Me.DataGridView3.AllowUserToOrderColumns = True
        Me.DataGridView3.AllowUserToResizeRows = False
        Me.DataGridView3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView3.CausesValidation = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.NullValue = "N/A"
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView3.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PARTNO, Me.OP, Me.OP_DESCR, Me.WC_DESC, Me.WORKCENTER, Me.DWELL})
        Me.DataGridView3.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DataGridView3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.DataGridView3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DataGridView3.Location = New System.Drawing.Point(-3, 20)
        Me.DataGridView3.Margin = New System.Windows.Forms.Padding(1)
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.ReadOnly = True
        Me.DataGridView3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DataGridView3.RowHeadersVisible = False
        Me.DataGridView3.RowHeadersWidth = 10
        Me.DataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView3.Size = New System.Drawing.Size(603, 532)
        Me.DataGridView3.TabIndex = 4
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.KeepInFrontToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(145, 26)
        '
        'KeepInFrontToolStripMenuItem
        '
        Me.KeepInFrontToolStripMenuItem.Name = "KeepInFrontToolStripMenuItem"
        Me.KeepInFrontToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.KeepInFrontToolStripMenuItem.Text = "Keep In Front"
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TextBox1.Location = New System.Drawing.Point(0, 0)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(600, 20)
        Me.TextBox1.TabIndex = 6
        '
        'XTLShop
        '
        Me.XTLShop.Dock = System.Windows.Forms.DockStyle.Top
        Me.XTLShop.Location = New System.Drawing.Point(0, 20)
        Me.XTLShop.Name = "XTLShop"
        Me.XTLShop.Size = New System.Drawing.Size(600, 20)
        Me.XTLShop.TabIndex = 7
        Me.XTLShop.Visible = False
        '
        'PARTNO
        '
        Me.PARTNO.DataPropertyName = "PARTNO"
        Me.PARTNO.HeaderText = "SHOP"
        Me.PARTNO.MaxInputLength = 10
        Me.PARTNO.Name = "PARTNO"
        Me.PARTNO.ReadOnly = True
        Me.PARTNO.Width = 62
        '
        'OP
        '
        Me.OP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.OP.DataPropertyName = "OPERATION_NO"
        Me.OP.HeaderText = "OP"
        Me.OP.MaxInputLength = 10
        Me.OP.Name = "OP"
        Me.OP.ReadOnly = True
        Me.OP.Width = 48
        '
        'OP_DESCR
        '
        Me.OP_DESCR.DataPropertyName = "OPERATION_DESCR"
        Me.OP_DESCR.HeaderText = "DESC"
        Me.OP_DESCR.MaxInputLength = 100
        Me.OP_DESCR.Name = "OP_DESCR"
        Me.OP_DESCR.ReadOnly = True
        Me.OP_DESCR.Width = 59
        '
        'WC_DESC
        '
        Me.WC_DESC.DataPropertyName = "WC_DESC"
        Me.WC_DESC.HeaderText = "WC_DESC"
        Me.WC_DESC.MaxInputLength = 100
        Me.WC_DESC.Name = "WC_DESC"
        Me.WC_DESC.ReadOnly = True
        Me.WC_DESC.Width = 84
        '
        'WORKCENTER
        '
        Me.WORKCENTER.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.WORKCENTER.DataPropertyName = "WORKCENTER"
        Me.WORKCENTER.HeaderText = "WC"
        Me.WORKCENTER.MaxInputLength = 10
        Me.WORKCENTER.Name = "WORKCENTER"
        Me.WORKCENTER.ReadOnly = True
        Me.WORKCENTER.Width = 45
        '
        'DWELL
        '
        Me.DWELL.DataPropertyName = "DWELL"
        Me.DWELL.HeaderText = "DWELL"
        Me.DWELL.Name = "DWELL"
        Me.DWELL.ReadOnly = True
        Me.DWELL.Width = 68
        '
        'XTLForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 562)
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.DataGridView3)
        Me.Controls.Add(Me.XTLShop)
        Me.Controls.Add(Me.TextBox1)
        Me.DoubleBuffered = True
        Me.Name = "XTLForm"
        Me.Text = "TimeLine"
        Me.TopMost = True
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    ' Friend WithEvents XTLcleanTableAdapter As WindowsApplication5.SOLinesDataSetTableAdapters.XTLCleanTableAdapter
    Friend WithEvents SHOPDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LOCDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OPDWELLDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DWELLDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents KeepInFrontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents XTLShop As System.Windows.Forms.TextBox
    Friend WithEvents PARTNO As DataGridViewTextBoxColumn
    Friend WithEvents OP As DataGridViewTextBoxColumn
    Friend WithEvents OP_DESCR As DataGridViewTextBoxColumn
    Friend WithEvents WC_DESC As DataGridViewTextBoxColumn
    Friend WithEvents WORKCENTER As DataGridViewTextBoxColumn
    Friend WithEvents DWELL As DataGridViewTextBoxColumn
End Class
