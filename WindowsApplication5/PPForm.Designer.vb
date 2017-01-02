<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class PPForm

    Inherits System.Windows.Forms.Form
    Public PSQLArray As String(,)
    Public SOLinesUpdateTime As Date
    Public Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As IntPtr) As Long
    Public Const MOD_ALT As Integer = &H1
    Public Const WM_HOTKEY As Integer = &H312
    Public currentxrlookup As String
    Public alloy As String
    Public DisplayAlloy As Boolean
    Public TRACKURL As String
    Public FIVEDIGSHOP As String
    Public TimeToClose As Boolean
    Public XTLFormLocation As System.Drawing.Point
    Public XRLOTFormLocation As System.Drawing.Point
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    'Friend ShipmentsTableAdapter As WindowsApplication5.SOLinesDataSetTableAdapters.ShipmentsTableAdapter
    Friend DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend CUSTOMOPENSTableAdapter As WindowsApplication5.SOLinesDataSetTableAdapters.CUSTOMOPENSTableAdapter
    Friend CUSTOMOPENSBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend ShipmentsBindingSource3 As System.Windows.Forms.BindingSource
    Friend ShipmentsBindingSource4 As System.Windows.Forms.BindingSource
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents HideSPCLCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ShipmentsByLotBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ShipmentsByLotTableAdapter As WindowsApplication5.SOLinesDataSetTableAdapters.ShipmentsByLotTableAdapter
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents ShipmentsByLotBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents CheckBoxShowLots As System.Windows.Forms.CheckBox
    Friend WithEvents SALES_ORDER_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CUSTOMER_PO_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HideTrayIconToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents ViewQuotesFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public XOpenFormLocation As System.Drawing.Point

    Friend Shared WithEvents timer1 As New Timer
    Friend WithEvents ContextMenuStrip3 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ViewTimelineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewWIPToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents LookUpValue As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents NotifyIcon2 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExitPPFormToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents XTLcleanBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents XtlCleanTableAdapter1 As WindowsApplication5.SOLinesDataSetTableAdapters.XTLCleanTableAdapter
    Friend WithEvents ViewWaxTechCardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KeepInFrontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public Shared ShareddbPath As String
    Friend WithEvents CopyStat As Task(Of Integer)
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.LookUpValue = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.LINE_TYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PARTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CUSTOMER_PARTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SALES_ORDER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ORDER_LINE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CUSTOMER_PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CUSTOMER_ORDER_LINE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_DUE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_SHIPPED = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.REQUIRED_D = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SCHED_D = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RELEASE_PRICE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DUE_VALUE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CUSTOMER_N = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COMANY_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SALESORDER_STATUS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CUSTOMOPENSBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.SN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CUST_PARTNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SHIPPED_DTIME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WORKORDERNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_SHPPED = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INVOICE_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COMPANYNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CUSTOMER_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WEIGH_BILL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRICE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShipmentsByLotBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ShipmentsByLotBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ContextMenuStrip3 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ViewQuotesFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewTimelineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewWIPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewWaxTechCardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KeepInFrontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyWHeadersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExitPPFormToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HideTrayIconToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.XTLcleanBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.NotifyIcon2 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.HideSPCLCheckBox = New System.Windows.Forms.CheckBox()
        Me.CheckBoxShowLots = New System.Windows.Forms.CheckBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.ShipmentsBindingSource4 = New System.Windows.Forms.BindingSource(Me.components)
        Me.XtlCleanTableAdapter1 = New WindowsApplication5.SOLinesDataSetTableAdapters.XTLCleanTableAdapter()
        Me.CUSTOMOPENSTableAdapter = New WindowsApplication5.SOLinesDataSetTableAdapters.CUSTOMOPENSTableAdapter()
        Me.ShipmentsBindingSource3 = New System.Windows.Forms.BindingSource(Me.components)
        Me.ShipmentsByLotTableAdapter = New WindowsApplication5.SOLinesDataSetTableAdapters.ShipmentsByLotTableAdapter()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CUSTOMOPENSBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShipmentsByLotBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShipmentsByLotBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip3.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.XTLcleanBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShipmentsBindingSource4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShipmentsBindingSource3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(1069, 258)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(73, 24)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "LOAD"
        Me.Button1.UseCompatibleTextRendering = True
        Me.Button1.UseVisualStyleBackColor = True
        '
        'LookUpValue
        '
        Me.LookUpValue.AllowDrop = True
        Me.LookUpValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LookUpValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.LookUpValue.HideSelection = False
        Me.LookUpValue.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.LookUpValue.Location = New System.Drawing.Point(848, 260)
        Me.LookUpValue.Margin = New System.Windows.Forms.Padding(0)
        Me.LookUpValue.MaxLength = 30
        Me.LookUpValue.Name = "LookUpValue"
        Me.LookUpValue.Size = New System.Drawing.Size(168, 20)
        Me.LookUpValue.TabIndex = 3
        '
        'TabControl1
        '
        Me.TabControl1.AccessibleName = "TabControl1"
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.Padding = New System.Drawing.Point(4, 2)
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1149, 258)
        Me.TabControl1.TabIndex = 5
        '
        'TabPage1
        '
        Me.TabPage1.AccessibleName = "TabPage1"
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 20)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1141, 234)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "OPEN ORDERS"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LINE_TYPE, Me.PARTNO, Me.CUSTOMER_PARTNO, Me.SALES_ORDER, Me.ORDER_LINE, Me.CUSTOMER_PO, Me.CUSTOMER_ORDER_LINE, Me.QTY_DUE, Me.QTY_SHIPPED, Me.REQUIRED_D, Me.SCHED_D, Me.RELEASE_PRICE, Me.DUE_VALUE, Me.CUSTOMER_N, Me.COMANY_NAME, Me.SALESORDER_STATUS})
        Me.DataGridView1.DataSource = Me.CUSTOMOPENSBindingSource
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(1135, 228)
        Me.DataGridView1.TabIndex = 0
        '
        'LINE_TYPE
        '
        Me.LINE_TYPE.DataPropertyName = "LINE_TYPE"
        Me.LINE_TYPE.HeaderText = "TPE"
        Me.LINE_TYPE.Name = "LINE_TYPE"
        Me.LINE_TYPE.ReadOnly = True
        Me.LINE_TYPE.ToolTipText = "Sales order line type code"
        Me.LINE_TYPE.Width = 30
        '
        'PARTNO
        '
        Me.PARTNO.DataPropertyName = "PARTNO"
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue
        Me.PARTNO.DefaultCellStyle = DataGridViewCellStyle1
        Me.PARTNO.HeaderText = "SHOP"
        Me.PARTNO.Name = "PARTNO"
        Me.PARTNO.ReadOnly = True
        Me.PARTNO.ToolTipText = "PCC part number"
        Me.PARTNO.Width = 60
        '
        'CUSTOMER_PARTNO
        '
        Me.CUSTOMER_PARTNO.DataPropertyName = "CUSTOMER_PARTNO"
        Me.CUSTOMER_PARTNO.HeaderText = "CUSTOMER_PN"
        Me.CUSTOMER_PARTNO.Name = "CUSTOMER_PARTNO"
        Me.CUSTOMER_PARTNO.ReadOnly = True
        Me.CUSTOMER_PARTNO.ToolTipText = "Customer partno or drawing number"
        '
        'SALES_ORDER
        '
        Me.SALES_ORDER.DataPropertyName = "SALES_ORDER_NO"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue
        Me.SALES_ORDER.DefaultCellStyle = DataGridViewCellStyle2
        Me.SALES_ORDER.HeaderText = "SO"
        Me.SALES_ORDER.Name = "SALES_ORDER"
        Me.SALES_ORDER.ReadOnly = True
        Me.SALES_ORDER.ToolTipText = "Sales order"
        Me.SALES_ORDER.Width = 70
        '
        'ORDER_LINE
        '
        Me.ORDER_LINE.DataPropertyName = "ORDER_LINE"
        Me.ORDER_LINE.HeaderText = "LI"
        Me.ORDER_LINE.Name = "ORDER_LINE"
        Me.ORDER_LINE.ReadOnly = True
        Me.ORDER_LINE.ToolTipText = "Sales order line #"
        Me.ORDER_LINE.Width = 40
        '
        'CUSTOMER_PO
        '
        Me.CUSTOMER_PO.DataPropertyName = "CUSTOMER_PO_NO"
        Me.CUSTOMER_PO.HeaderText = "CUSTOMER_PO"
        Me.CUSTOMER_PO.Name = "CUSTOMER_PO"
        Me.CUSTOMER_PO.ReadOnly = True
        Me.CUSTOMER_PO.ToolTipText = "Customer purchase order #"
        '
        'CUSTOMER_ORDER_LINE
        '
        Me.CUSTOMER_ORDER_LINE.DataPropertyName = "CUST_ORDER_LINE"
        Me.CUSTOMER_ORDER_LINE.HeaderText = "LI"
        Me.CUSTOMER_ORDER_LINE.Name = "CUSTOMER_ORDER_LINE"
        Me.CUSTOMER_ORDER_LINE.ReadOnly = True
        Me.CUSTOMER_ORDER_LINE.ToolTipText = "PO line number"
        Me.CUSTOMER_ORDER_LINE.Width = 40
        '
        'QTY_DUE
        '
        Me.QTY_DUE.DataPropertyName = "QTY_DUE"
        Me.QTY_DUE.HeaderText = "QTY"
        Me.QTY_DUE.Name = "QTY_DUE"
        Me.QTY_DUE.ReadOnly = True
        Me.QTY_DUE.ToolTipText = "Outstanding line balance"
        Me.QTY_DUE.Width = 40
        '
        'QTY_SHIPPED
        '
        Me.QTY_SHIPPED.DataPropertyName = "QTY_SHIPPED"
        Me.QTY_SHIPPED.HeaderText = "SHP"
        Me.QTY_SHIPPED.Name = "QTY_SHIPPED"
        Me.QTY_SHIPPED.ReadOnly = True
        Me.QTY_SHIPPED.ToolTipText = "Total quantity shipped"
        Me.QTY_SHIPPED.Width = 40
        '
        'REQUIRED_D
        '
        Me.REQUIRED_D.DataPropertyName = "REQUIRED_D"
        Me.REQUIRED_D.HeaderText = "REQ"
        Me.REQUIRED_D.Name = "REQUIRED_D"
        Me.REQUIRED_D.ReadOnly = True
        Me.REQUIRED_D.ToolTipText = "'Date Req' field in Vis sales order grid maintenance. Used to calculate LTPO and " &
    "should reflect purchase order dates"
        Me.REQUIRED_D.Width = 70
        '
        'SCHED_D
        '
        Me.SCHED_D.DataPropertyName = "SCHED_D"
        Me.SCHED_D.HeaderText = "PLAN"
        Me.SCHED_D.Name = "SCHED_D"
        Me.SCHED_D.ReadOnly = True
        Me.SCHED_D.ToolTipText = "'Date Prom' field in Vis sales order grid maintenance. Used for planning purposes" &
    " and omitted for LTPO"
        Me.SCHED_D.Width = 70
        '
        'RELEASE_PRICE
        '
        Me.RELEASE_PRICE.DataPropertyName = "RELEASE_PRICE"
        DataGridViewCellStyle3.Format = "C2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.RELEASE_PRICE.DefaultCellStyle = DataGridViewCellStyle3
        Me.RELEASE_PRICE.HeaderText = "PRICE"
        Me.RELEASE_PRICE.Name = "RELEASE_PRICE"
        Me.RELEASE_PRICE.ReadOnly = True
        Me.RELEASE_PRICE.ToolTipText = "Unit price"
        Me.RELEASE_PRICE.Width = 60
        '
        'DUE_VALUE
        '
        Me.DUE_VALUE.DataPropertyName = "DUE_VALUE"
        DataGridViewCellStyle4.Format = "C2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.DUE_VALUE.DefaultCellStyle = DataGridViewCellStyle4
        Me.DUE_VALUE.HeaderText = "EXT"
        Me.DUE_VALUE.Name = "DUE_VALUE"
        Me.DUE_VALUE.ReadOnly = True
        Me.DUE_VALUE.ToolTipText = "Extended price"
        Me.DUE_VALUE.Width = 70
        '
        'CUSTOMER_N
        '
        Me.CUSTOMER_N.DataPropertyName = "CUSTOMER_NO"
        Me.CUSTOMER_N.HeaderText = "CUST#"
        Me.CUSTOMER_N.Name = "CUSTOMER_N"
        Me.CUSTOMER_N.ReadOnly = True
        Me.CUSTOMER_N.ToolTipText = "Customer number"
        Me.CUSTOMER_N.Width = 70
        '
        'COMANY_NAME
        '
        Me.COMANY_NAME.DataPropertyName = "COMPANY_NAME"
        Me.COMANY_NAME.HeaderText = "CUSTOMER"
        Me.COMANY_NAME.Name = "COMANY_NAME"
        Me.COMANY_NAME.ReadOnly = True
        Me.COMANY_NAME.ToolTipText = "Company name"
        '
        'SALESORDER_STATUS
        '
        Me.SALESORDER_STATUS.DataPropertyName = "PO_RECEIVED"
        DataGridViewCellStyle5.NullValue = "NONE"
        Me.SALESORDER_STATUS.DefaultCellStyle = DataGridViewCellStyle5
        Me.SALESORDER_STATUS.HeaderText = "SHIP HOLDS"
        Me.SALESORDER_STATUS.Name = "SALESORDER_STATUS"
        Me.SALESORDER_STATUS.ReadOnly = True
        Me.SALESORDER_STATUS.ToolTipText = "Sales controlled values that can stop shipment on an order"
        '
        'TabPage2
        '
        Me.TabPage2.AccessibleName = "TabPage2"
        Me.TabPage2.Controls.Add(Me.DataGridView2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 20)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1092, 234)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "SHIPMENTS"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToOrderColumns = True
        Me.DataGridView2.AllowUserToResizeRows = False
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SN, Me.CUST_PARTNO, Me.SHIPPED_DTIME, Me.WORKORDERNO, Me.QTY_SHPPED, Me.INVOICE_NO, Me.SO, Me.PO, Me.COMPANYNAME, Me.CUSTOMER_NO, Me.WEIGH_BILL, Me.PRICE})
        Me.DataGridView2.DataSource = Me.ShipmentsByLotBindingSource1
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView2.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.Size = New System.Drawing.Size(1086, 228)
        Me.DataGridView2.TabIndex = 0
        '
        'SN
        '
        Me.SN.DataPropertyName = "PARTNO"
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle6.NullValue = Nothing
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White
        Me.SN.DefaultCellStyle = DataGridViewCellStyle6
        Me.SN.HeaderText = "SHOP"
        Me.SN.Name = "SN"
        Me.SN.Width = 50
        '
        'CUST_PARTNO
        '
        Me.CUST_PARTNO.DataPropertyName = "CUSTOMER_PARTNO"
        Me.CUST_PARTNO.HeaderText = "CUSTOMER_PN"
        Me.CUST_PARTNO.Name = "CUST_PARTNO"
        '
        'SHIPPED_DTIME
        '
        Me.SHIPPED_DTIME.DataPropertyName = "SHIPPED_DTIME"
        DataGridViewCellStyle7.Format = "g"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.SHIPPED_DTIME.DefaultCellStyle = DataGridViewCellStyle7
        Me.SHIPPED_DTIME.HeaderText = "DATE"
        Me.SHIPPED_DTIME.Name = "SHIPPED_DTIME"
        Me.SHIPPED_DTIME.ReadOnly = True
        Me.SHIPPED_DTIME.ToolTipText = "Date ane"
        '
        'WORKORDERNO
        '
        Me.WORKORDERNO.DataPropertyName = "WORKORDERNO"
        Me.WORKORDERNO.HeaderText = "WO"
        Me.WORKORDERNO.Name = "WORKORDERNO"
        '
        'QTY_SHPPED
        '
        Me.QTY_SHPPED.DataPropertyName = "QTY_SHPPED"
        DataGridViewCellStyle8.Format = "N0"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.QTY_SHPPED.DefaultCellStyle = DataGridViewCellStyle8
        Me.QTY_SHPPED.HeaderText = "QTY"
        Me.QTY_SHPPED.Name = "QTY_SHPPED"
        Me.QTY_SHPPED.Width = 40
        '
        'INVOICE_NO
        '
        Me.INVOICE_NO.DataPropertyName = "INVOICE_NO"
        Me.INVOICE_NO.HeaderText = "INVOICE"
        Me.INVOICE_NO.Name = "INVOICE_NO"
        Me.INVOICE_NO.Width = 70
        '
        'SO
        '
        Me.SO.DataPropertyName = "SALES_ORDER_NO"
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Blue
        Me.SO.DefaultCellStyle = DataGridViewCellStyle9
        Me.SO.HeaderText = "SO"
        Me.SO.Name = "SO"
        Me.SO.Width = 70
        '
        'PO
        '
        Me.PO.DataPropertyName = "CUSTOMER_PO_NO"
        Me.PO.HeaderText = "PO"
        Me.PO.Name = "PO"
        '
        'COMPANYNAME
        '
        Me.COMPANYNAME.DataPropertyName = "COMPANYNAME"
        Me.COMPANYNAME.HeaderText = "COMPANY"
        Me.COMPANYNAME.Name = "COMPANYNAME"
        Me.COMPANYNAME.Width = 150
        '
        'CUSTOMER_NO
        '
        Me.CUSTOMER_NO.DataPropertyName = "CUSTOMER_NO"
        Me.CUSTOMER_NO.HeaderText = "CUST_NO"
        Me.CUSTOMER_NO.Name = "CUSTOMER_NO"
        Me.CUSTOMER_NO.Width = 70
        '
        'WEIGH_BILL
        '
        Me.WEIGH_BILL.DataPropertyName = "WEIGH_BILL"
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Blue
        Me.WEIGH_BILL.DefaultCellStyle = DataGridViewCellStyle10
        Me.WEIGH_BILL.HeaderText = "WEIGH_BILL"
        Me.WEIGH_BILL.Name = "WEIGH_BILL"
        Me.WEIGH_BILL.Width = 120
        '
        'PRICE
        '
        Me.PRICE.DataPropertyName = "PRICE"
        DataGridViewCellStyle11.Format = "C2"
        DataGridViewCellStyle11.NullValue = Nothing
        Me.PRICE.DefaultCellStyle = DataGridViewCellStyle11
        Me.PRICE.HeaderText = "PRICE"
        Me.PRICE.Name = "PRICE"
        Me.PRICE.ReadOnly = True
        Me.PRICE.Width = 60
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.DropDownWidth = 10
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"SN", "PN", "SO", "PO", "ALL"})
        Me.ComboBox1.Location = New System.Drawing.Point(1019, 260)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(44, 21)
        Me.ComboBox1.TabIndex = 11
        Me.ComboBox1.Text = "ALL"
        '
        'ContextMenuStrip3
        '
        Me.ContextMenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewQuotesFolderToolStripMenuItem, Me.ViewTimelineToolStripMenuItem, Me.ViewWIPToolStripMenuItem, Me.ViewWaxTechCardToolStripMenuItem, Me.KeepInFrontToolStripMenuItem, Me.CopyToolStripMenuItem, Me.CopyWHeadersToolStripMenuItem})
        Me.ContextMenuStrip3.Name = "ContextMenuStrip3"
        Me.ContextMenuStrip3.ShowCheckMargin = True
        Me.ContextMenuStrip3.ShowImageMargin = False
        Me.ContextMenuStrip3.Size = New System.Drawing.Size(182, 158)
        '
        'ViewQuotesFolderToolStripMenuItem
        '
        Me.ViewQuotesFolderToolStripMenuItem.Name = "ViewQuotesFolderToolStripMenuItem"
        Me.ViewQuotesFolderToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.ViewQuotesFolderToolStripMenuItem.Text = "View Quotes"
        '
        'ViewTimelineToolStripMenuItem
        '
        Me.ViewTimelineToolStripMenuItem.Name = "ViewTimelineToolStripMenuItem"
        Me.ViewTimelineToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.ViewTimelineToolStripMenuItem.Text = "View Timeline"
        '
        'ViewWIPToolStripMenuItem
        '
        Me.ViewWIPToolStripMenuItem.Name = "ViewWIPToolStripMenuItem"
        Me.ViewWIPToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.ViewWIPToolStripMenuItem.Text = "View WIP"
        '
        'ViewWaxTechCardToolStripMenuItem
        '
        Me.ViewWaxTechCardToolStripMenuItem.Name = "ViewWaxTechCardToolStripMenuItem"
        Me.ViewWaxTechCardToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.ViewWaxTechCardToolStripMenuItem.Text = "View Wax Tech Card"
        '
        'KeepInFrontToolStripMenuItem
        '
        Me.KeepInFrontToolStripMenuItem.Name = "KeepInFrontToolStripMenuItem"
        Me.KeepInFrontToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.KeepInFrontToolStripMenuItem.Text = "Keep In Front"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        '
        'CopyWHeadersToolStripMenuItem
        '
        Me.CopyWHeadersToolStripMenuItem.Name = "CopyWHeadersToolStripMenuItem"
        Me.CopyWHeadersToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.CopyWHeadersToolStripMenuItem.Text = "Copy w/Headers"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitPPFormToolStripMenuItem, Me.HideTrayIconToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(186, 48)
        '
        'ExitPPFormToolStripMenuItem
        '
        Me.ExitPPFormToolStripMenuItem.Name = "ExitPPFormToolStripMenuItem"
        Me.ExitPPFormToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ExitPPFormToolStripMenuItem.Text = "Exit PPForm"
        '
        'HideTrayIconToolStripMenuItem
        '
        Me.HideTrayIconToolStripMenuItem.Name = "HideTrayIconToolStripMenuItem"
        Me.HideTrayIconToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.HideTrayIconToolStripMenuItem.Text = "Show/Hide Tray Icon"
        '
        'XTLcleanBindingSource
        '
        Me.XTLcleanBindingSource.DataMember = "XTLclean"
        '
        'NotifyIcon2
        '
        Me.NotifyIcon2.Text = "PMPLOOKUP"
        Me.NotifyIcon2.Visible = True
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Location = New System.Drawing.Point(658, 258)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(108, 37)
        Me.TextBox1.TabIndex = 7
        Me.TextBox1.WordWrap = False
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Location = New System.Drawing.Point(516, 259)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(87, 37)
        Me.TextBox2.TabIndex = 9
        Me.TextBox2.WordWrap = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(22, 258)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(73, 27)
        Me.Button2.TabIndex = 10
        Me.Button2.Text = "VIEW WIP"
        Me.Button2.UseCompatibleTextRendering = True
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(101, 262)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(37, 21)
        Me.Button3.TabIndex = 12
        Me.Button3.Text = "REF"
        Me.Button3.UseCompatibleTextRendering = True
        Me.Button3.UseVisualStyleBackColor = True
        '
        'HideSPCLCheckBox
        '
        Me.HideSPCLCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.HideSPCLCheckBox.AutoSize = True
        Me.HideSPCLCheckBox.Checked = True
        Me.HideSPCLCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.HideSPCLCheckBox.Location = New System.Drawing.Point(153, 263)
        Me.HideSPCLCheckBox.Name = "HideSPCLCheckBox"
        Me.HideSPCLCheckBox.Size = New System.Drawing.Size(78, 17)
        Me.HideSPCLCheckBox.TabIndex = 1
        Me.HideSPCLCheckBox.Text = "Hide SPCL"
        Me.HideSPCLCheckBox.UseVisualStyleBackColor = True
        '
        'CheckBoxShowLots
        '
        Me.CheckBoxShowLots.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxShowLots.AutoSize = True
        Me.CheckBoxShowLots.Location = New System.Drawing.Point(237, 263)
        Me.CheckBoxShowLots.Name = "CheckBoxShowLots"
        Me.CheckBoxShowLots.Size = New System.Drawing.Size(56, 17)
        Me.CheckBoxShowLots.TabIndex = 13
        Me.CheckBoxShowLots.Text = "By Lot"
        Me.CheckBoxShowLots.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(800, 258)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(45, 24)
        Me.Button4.TabIndex = 14
        Me.Button4.Text = "PSQL"
        Me.Button4.UseCompatibleTextRendering = True
        Me.Button4.UseVisualStyleBackColor = True
        '
        'XtlCleanTableAdapter1
        '
        Me.XtlCleanTableAdapter1.ClearBeforeFill = True
        '
        'CUSTOMOPENSTableAdapter
        '
        Me.CUSTOMOPENSTableAdapter.ClearBeforeFill = True
        '
        'ShipmentsByLotTableAdapter
        '
        Me.ShipmentsByLotTableAdapter.ClearBeforeFill = True
        '
        'PPForm
        '
        Me.AcceptButton = Me.Button1
        Me.AllowDrop = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(1149, 286)
        Me.ContextMenuStrip = Me.ContextMenuStrip3
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.CheckBoxShowLots)
        Me.Controls.Add(Me.HideSPCLCheckBox)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.LookUpValue)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(10000, 10000)
        Me.Name = "PPForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Open Orders Search"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CUSTOMOPENSBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShipmentsByLotBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShipmentsByLotBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip3.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.XTLcleanBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShipmentsBindingSource4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShipmentsBindingSource3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CopyWHeadersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QTY As DataGridViewTextBoxColumn
    Friend WithEvents SHOP As DataGridViewTextBoxColumn
    Friend WithEvents PNDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents SODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LINEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PODataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents CUSTLINEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PRICEDataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents OPENQTYDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents EXTVALUEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents CUSTDATEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PLANDATEDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents CUSTNUMDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents STOPSHIPDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents LINE_TYPE As DataGridViewTextBoxColumn
    Friend WithEvents PARTNO As DataGridViewTextBoxColumn
    Friend WithEvents CUSTOMER_PARTNO As DataGridViewTextBoxColumn
    Friend WithEvents SALES_ORDER As DataGridViewTextBoxColumn
    Friend WithEvents ORDER_LINE As DataGridViewTextBoxColumn
    Friend WithEvents CUSTOMER_PO As DataGridViewTextBoxColumn
    Friend WithEvents CUSTOMER_ORDER_LINE As DataGridViewTextBoxColumn
    Friend WithEvents QTY_DUE As DataGridViewTextBoxColumn
    Friend WithEvents QTY_SHIPPED As DataGridViewTextBoxColumn
    Friend WithEvents REQUIRED_D As DataGridViewTextBoxColumn
    Friend WithEvents SCHED_D As DataGridViewTextBoxColumn
    Friend WithEvents RELEASE_PRICE As DataGridViewTextBoxColumn
    Friend WithEvents DUE_VALUE As DataGridViewTextBoxColumn
    Friend WithEvents CUSTOMER_N As DataGridViewTextBoxColumn
    Friend WithEvents COMANY_NAME As DataGridViewTextBoxColumn
    Friend WithEvents SALESORDER_STATUS As DataGridViewTextBoxColumn
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SN As DataGridViewTextBoxColumn
    Friend WithEvents CUST_PARTNO As DataGridViewTextBoxColumn
    Friend WithEvents SHIPPED_DTIME As DataGridViewTextBoxColumn
    Friend WithEvents WORKORDERNO As DataGridViewTextBoxColumn
    Friend WithEvents QTY_SHPPED As DataGridViewTextBoxColumn
    Friend WithEvents INVOICE_NO As DataGridViewTextBoxColumn
    Friend WithEvents SO As DataGridViewTextBoxColumn
    Friend WithEvents PO As DataGridViewTextBoxColumn
    Friend WithEvents COMPANYNAME As DataGridViewTextBoxColumn
    Friend WithEvents CUSTOMER_NO As DataGridViewTextBoxColumn
    Friend WithEvents WEIGH_BILL As DataGridViewTextBoxColumn
    Friend WithEvents PRICE As DataGridViewTextBoxColumn
End Class
