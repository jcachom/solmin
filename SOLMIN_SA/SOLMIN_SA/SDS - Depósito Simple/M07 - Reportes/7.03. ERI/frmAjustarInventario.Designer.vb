<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAjustarInventario
    Inherits ComponentFactory.Krypton.Toolkit.KryptonForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAjustarInventario))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.hgFiltros = New ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup
        Me.bsaOcultarFiltros = New ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup
        Me.bsaCerrar = New ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup
        Me.txtFecInv = New ComponentFactory.Krypton.Toolkit.KryptonTextBox
        Me.grpLeyenda = New System.Windows.Forms.GroupBox
        Me.lblDetalleOpcional = New ComponentFactory.Krypton.Toolkit.KryptonLabel
        Me.lblDetalleObligatorio = New ComponentFactory.Krypton.Toolkit.KryptonLabel
        Me.lblOpcional = New System.Windows.Forms.Label
        Me.lblObligatorio = New System.Windows.Forms.Label
        Me.txtNroDocEri = New ComponentFactory.Krypton.Toolkit.KryptonTextBox
        Me.lblMercadaeria = New ComponentFactory.Krypton.Toolkit.KryptonLabel
        Me.KryptonLabel1 = New ComponentFactory.Krypton.Toolkit.KryptonLabel
        Me.KryptonLabel5 = New ComponentFactory.Krypton.Toolkit.KryptonLabel
        Me.Contenedor1 = New ComponentFactory.Krypton.Toolkit.KryptonSplitContainer
        Me.Contenedor2 = New ComponentFactory.Krypton.Toolkit.KryptonSplitContainer
        Me.dgMercaderia = New ComponentFactory.Krypton.Toolkit.KryptonDataGridView
        Me.NORDSR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CMRCLR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NORDSR1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TCMPMR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.QSLMC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.QSFMC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TPAJUSTE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CNTREGULA = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FUNDS2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CMRCD = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NCNTR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NCRCTC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NAUTR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CUNCN5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CUNPS5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CUNVL5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CTPDP6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tsMenuOpciones = New System.Windows.Forms.ToolStrip
        Me.lblTitulo = New System.Windows.Forms.ToolStripLabel
        Me.hgOS = New ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup
        Me.bsaProcesar = New ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup
        Me.dtgKardex = New ComponentFactory.Krypton.Toolkit.KryptonDataGridView
        Me.PSCTPALM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PSDESTIPO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PSCALMC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PSDESALM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PSCZNALM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PSDESZONA = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PNQSLMC2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PNQSLMP2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.dgMercaderiaSeleccionada = New ComponentFactory.Krypton.Toolkit.KryptonDataGridView
        Me.tsMenuProcesar = New System.Windows.Forms.ToolStrip
        Me.tss08 = New System.Windows.Forms.ToolStripLabel
        Me.btnEliminarItem = New System.Windows.Forms.ToolStripButton
        Me.tss06 = New System.Windows.Forms.ToolStripSeparator
        Me.btnProcesarDespacho = New System.Windows.Forms.ToolStripButton
        Me.tss05 = New System.Windows.Forms.ToolStripSeparator
        Me.S_CMRCLR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PSTIPOAJUSTE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_CMRCD = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_TCMPMR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_NORDSR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_NCNTR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_NCRCTC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_NAUTR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_CUNCNT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_CUNPST = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_CUNVLT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_NORCCL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_NGUICL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_NRUCLL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_STPING = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_CTPALM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_CALMC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_TCMPAL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_CZNALM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_TCMZNA = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_QTRMC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_QTRMP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_QDSVGN = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_CCNTD = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_CTPOCN = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.S_FUNDS2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_CTPDPS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.S_TOBSRV = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.hgFiltros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.hgFiltros.Panel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hgFiltros.Panel.SuspendLayout()
        Me.hgFiltros.SuspendLayout()
        Me.grpLeyenda.SuspendLayout()
        CType(Me.Contenedor1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Contenedor1.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Contenedor1.Panel1.SuspendLayout()
        CType(Me.Contenedor1.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Contenedor1.Panel2.SuspendLayout()
        Me.Contenedor1.SuspendLayout()
        CType(Me.Contenedor2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Contenedor2.Panel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Contenedor2.Panel1.SuspendLayout()
        CType(Me.Contenedor2.Panel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Contenedor2.Panel2.SuspendLayout()
        Me.Contenedor2.SuspendLayout()
        CType(Me.dgMercaderia, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsMenuOpciones.SuspendLayout()
        CType(Me.hgOS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.hgOS.Panel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hgOS.Panel.SuspendLayout()
        Me.hgOS.SuspendLayout()
        CType(Me.dtgKardex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgMercaderiaSeleccionada, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsMenuProcesar.SuspendLayout()
        Me.SuspendLayout()
        '
        'hgFiltros
        '
        Me.hgFiltros.AutoSize = True
        Me.hgFiltros.ButtonSpecs.AddRange(New ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup() {Me.bsaOcultarFiltros, Me.bsaCerrar})
        Me.hgFiltros.Dock = System.Windows.Forms.DockStyle.Top
        Me.hgFiltros.HeaderVisibleSecondary = False
        Me.hgFiltros.Location = New System.Drawing.Point(0, 0)
        Me.hgFiltros.Name = "hgFiltros"
        '
        'hgFiltros.Panel
        '
        Me.hgFiltros.Panel.Controls.Add(Me.txtFecInv)
        Me.hgFiltros.Panel.Controls.Add(Me.grpLeyenda)
        Me.hgFiltros.Panel.Controls.Add(Me.txtNroDocEri)
        Me.hgFiltros.Panel.Controls.Add(Me.lblMercadaeria)
        Me.hgFiltros.Panel.Controls.Add(Me.KryptonLabel1)
        Me.hgFiltros.Panel.Controls.Add(Me.KryptonLabel5)
        Me.hgFiltros.Size = New System.Drawing.Size(1039, 126)
        Me.hgFiltros.StateNormal.HeaderPrimary.Content.ShortText.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hgFiltros.TabIndex = 3
        Me.hgFiltros.Text = "Filtros"
        Me.hgFiltros.ValuesPrimary.Description = ""
        Me.hgFiltros.ValuesPrimary.Heading = "Filtros"
        Me.hgFiltros.ValuesPrimary.Image = Nothing
        Me.hgFiltros.ValuesSecondary.Description = ""
        Me.hgFiltros.ValuesSecondary.Heading = "Resultado"
        Me.hgFiltros.ValuesSecondary.Image = Nothing
        '
        'bsaOcultarFiltros
        '
        Me.bsaOcultarFiltros.ExtraText = ""
        Me.bsaOcultarFiltros.Image = Nothing
        Me.bsaOcultarFiltros.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone
        Me.bsaOcultarFiltros.Text = "Ocultar"
        Me.bsaOcultarFiltros.ToolTipImage = Nothing
        Me.bsaOcultarFiltros.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowDown
        Me.bsaOcultarFiltros.UniqueName = "4FD0578D687F46FC4FD0578D687F46FC"
        '
        'bsaCerrar
        '
        Me.bsaCerrar.ExtraText = ""
        Me.bsaCerrar.Image = Nothing
        Me.bsaCerrar.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone
        Me.bsaCerrar.Text = "Cerrar"
        Me.bsaCerrar.ToolTipImage = Nothing
        Me.bsaCerrar.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.Close
        Me.bsaCerrar.UniqueName = "C90E4396C7B04154C90E4396C7B04154"
        '
        'txtFecInv
        '
        Me.txtFecInv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFecInv.Enabled = False
        Me.txtFecInv.Location = New System.Drawing.Point(158, 50)
        Me.txtFecInv.Name = "txtFecInv"
        Me.txtFecInv.Size = New System.Drawing.Size(150, 22)
        Me.txtFecInv.TabIndex = 36
        '
        'grpLeyenda
        '
        Me.grpLeyenda.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpLeyenda.BackColor = System.Drawing.Color.Transparent
        Me.grpLeyenda.Controls.Add(Me.lblDetalleOpcional)
        Me.grpLeyenda.Controls.Add(Me.lblDetalleObligatorio)
        Me.grpLeyenda.Controls.Add(Me.lblOpcional)
        Me.grpLeyenda.Controls.Add(Me.lblObligatorio)
        Me.grpLeyenda.Location = New System.Drawing.Point(1263, 12)
        Me.grpLeyenda.Name = "grpLeyenda"
        Me.grpLeyenda.Size = New System.Drawing.Size(110, 70)
        Me.grpLeyenda.TabIndex = 16
        Me.grpLeyenda.TabStop = False
        Me.grpLeyenda.Text = "Leyenda"
        Me.grpLeyenda.Visible = False
        '
        'lblDetalleOpcional
        '
        Me.lblDetalleOpcional.Location = New System.Drawing.Point(37, 44)
        Me.lblDetalleOpcional.Name = "lblDetalleOpcional"
        Me.lblDetalleOpcional.Size = New System.Drawing.Size(59, 20)
        Me.lblDetalleOpcional.TabIndex = 20
        Me.lblDetalleOpcional.Text = "Opcional"
        Me.lblDetalleOpcional.Values.ExtraText = ""
        Me.lblDetalleOpcional.Values.Image = Nothing
        Me.lblDetalleOpcional.Values.Text = "Opcional"
        '
        'lblDetalleObligatorio
        '
        Me.lblDetalleObligatorio.Location = New System.Drawing.Point(37, 22)
        Me.lblDetalleObligatorio.Name = "lblDetalleObligatorio"
        Me.lblDetalleObligatorio.Size = New System.Drawing.Size(73, 20)
        Me.lblDetalleObligatorio.TabIndex = 18
        Me.lblDetalleObligatorio.Text = "Obligatorio"
        Me.lblDetalleObligatorio.Values.ExtraText = ""
        Me.lblDetalleObligatorio.Values.Image = Nothing
        Me.lblDetalleObligatorio.Values.Text = "Obligatorio"
        '
        'lblOpcional
        '
        Me.lblOpcional.AutoSize = True
        Me.lblOpcional.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblOpcional.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOpcional.Location = New System.Drawing.Point(12, 45)
        Me.lblOpcional.Name = "lblOpcional"
        Me.lblOpcional.Size = New System.Drawing.Size(18, 15)
        Me.lblOpcional.TabIndex = 19
        Me.lblOpcional.Text = "   "
        '
        'lblObligatorio
        '
        Me.lblObligatorio.AutoSize = True
        Me.lblObligatorio.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblObligatorio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblObligatorio.Location = New System.Drawing.Point(12, 22)
        Me.lblObligatorio.Name = "lblObligatorio"
        Me.lblObligatorio.Size = New System.Drawing.Size(18, 15)
        Me.lblObligatorio.TabIndex = 17
        Me.lblObligatorio.Text = "   "
        '
        'txtNroDocEri
        '
        Me.txtNroDocEri.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNroDocEri.Enabled = False
        Me.txtNroDocEri.Location = New System.Drawing.Point(158, 24)
        Me.txtNroDocEri.Name = "txtNroDocEri"
        Me.txtNroDocEri.Size = New System.Drawing.Size(278, 22)
        Me.txtNroDocEri.TabIndex = 9
        '
        'lblMercadaeria
        '
        Me.lblMercadaeria.Location = New System.Drawing.Point(75, 50)
        Me.lblMercadaeria.Name = "lblMercadaeria"
        Me.lblMercadaeria.Size = New System.Drawing.Size(68, 20)
        Me.lblMercadaeria.TabIndex = 8
        Me.lblMercadaeria.Text = "Fecha Inv : "
        Me.lblMercadaeria.Values.ExtraText = ""
        Me.lblMercadaeria.Values.Image = Nothing
        Me.lblMercadaeria.Values.Text = "Fecha Inv : "
        '
        'KryptonLabel1
        '
        Me.KryptonLabel1.Location = New System.Drawing.Point(55, 24)
        Me.KryptonLabel1.Name = "KryptonLabel1"
        Me.KryptonLabel1.Size = New System.Drawing.Size(88, 20)
        Me.KryptonLabel1.TabIndex = 2
        Me.KryptonLabel1.Text = "Nro. Doc. ERI : "
        Me.KryptonLabel1.Values.ExtraText = ""
        Me.KryptonLabel1.Values.Image = Nothing
        Me.KryptonLabel1.Values.Text = "Nro. Doc. ERI : "
        '
        'KryptonLabel5
        '
        Me.KryptonLabel5.AutoSize = False
        Me.KryptonLabel5.Location = New System.Drawing.Point(797, 63)
        Me.KryptonLabel5.Name = "KryptonLabel5"
        Me.KryptonLabel5.Size = New System.Drawing.Size(31, 30)
        Me.KryptonLabel5.TabIndex = 35
        Me.KryptonLabel5.Values.ExtraText = ""
        Me.KryptonLabel5.Values.Image = Nothing
        Me.KryptonLabel5.Values.Text = ""
        '
        'Contenedor1
        '
        Me.Contenedor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Contenedor1.Location = New System.Drawing.Point(0, 126)
        Me.Contenedor1.Name = "Contenedor1"
        Me.Contenedor1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'Contenedor1.Panel1
        '
        Me.Contenedor1.Panel1.Controls.Add(Me.Contenedor2)
        '
        'Contenedor1.Panel2
        '
        Me.Contenedor1.Panel2.Controls.Add(Me.dgMercaderiaSeleccionada)
        Me.Contenedor1.Panel2.Controls.Add(Me.tsMenuProcesar)
        Me.Contenedor1.Size = New System.Drawing.Size(1039, 361)
        Me.Contenedor1.SplitterDistance = 180
        Me.Contenedor1.TabIndex = 100
        '
        'Contenedor2
        '
        Me.Contenedor2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Contenedor2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Contenedor2.Location = New System.Drawing.Point(0, 0)
        Me.Contenedor2.Name = "Contenedor2"
        '
        'Contenedor2.Panel1
        '
        Me.Contenedor2.Panel1.Controls.Add(Me.dgMercaderia)
        Me.Contenedor2.Panel1.Controls.Add(Me.tsMenuOpciones)
        '
        'Contenedor2.Panel2
        '
        Me.Contenedor2.Panel2.Controls.Add(Me.hgOS)
        Me.Contenedor2.Size = New System.Drawing.Size(1039, 180)
        Me.Contenedor2.SplitterDistance = 494
        Me.Contenedor2.TabIndex = 98
        '
        'dgMercaderia
        '
        Me.dgMercaderia.AllowUserToAddRows = False
        Me.dgMercaderia.AllowUserToDeleteRows = False
        Me.dgMercaderia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgMercaderia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgMercaderia.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NORDSR, Me.CMRCLR, Me.NORDSR1, Me.TCMPMR, Me.QSLMC, Me.QSFMC, Me.TPAJUSTE, Me.CNTREGULA, Me.FUNDS2, Me.CMRCD, Me.NCNTR, Me.NCRCTC, Me.NAUTR, Me.CUNCN5, Me.CUNPS5, Me.CUNVL5, Me.CTPDP6})
        Me.dgMercaderia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgMercaderia.Location = New System.Drawing.Point(0, 25)
        Me.dgMercaderia.MultiSelect = False
        Me.dgMercaderia.Name = "dgMercaderia"
        Me.dgMercaderia.ReadOnly = True
        Me.dgMercaderia.RowHeadersWidth = 20
        Me.dgMercaderia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgMercaderia.Size = New System.Drawing.Size(494, 155)
        Me.dgMercaderia.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList
        Me.dgMercaderia.StateNormal.Background.Color1 = System.Drawing.Color.White
        Me.dgMercaderia.TabIndex = 93
        '
        'NORDSR
        '
        Me.NORDSR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.NORDSR.DataPropertyName = "NCRRDT"
        Me.NORDSR.HeaderText = "Nro. Correlativo"
        Me.NORDSR.Name = "NORDSR"
        Me.NORDSR.ReadOnly = True
        Me.NORDSR.Width = 110
        '
        'CMRCLR
        '
        Me.CMRCLR.DataPropertyName = "CMRCLR"
        Me.CMRCLR.HeaderText = "Cod. Mercader?a"
        Me.CMRCLR.Name = "CMRCLR"
        Me.CMRCLR.ReadOnly = True
        Me.CMRCLR.Width = 113
        '
        'NORDSR1
        '
        Me.NORDSR1.DataPropertyName = "NORDSR"
        Me.NORDSR1.HeaderText = "Orden de Servicio"
        Me.NORDSR1.Name = "NORDSR1"
        Me.NORDSR1.ReadOnly = True
        Me.NORDSR1.Width = 119
        '
        'TCMPMR
        '
        Me.TCMPMR.DataPropertyName = "TCMPMR"
        Me.TCMPMR.HeaderText = "Mercader?a"
        Me.TCMPMR.Name = "TCMPMR"
        Me.TCMPMR.ReadOnly = True
        Me.TCMPMR.Width = 95
        '
        'QSLMC
        '
        Me.QSLMC.DataPropertyName = "QSLMC"
        Me.QSLMC.HeaderText = "Stock Sistema"
        Me.QSLMC.Name = "QSLMC"
        Me.QSLMC.ReadOnly = True
        Me.QSLMC.Width = 101
        '
        'QSFMC
        '
        Me.QSFMC.DataPropertyName = "QSFMC"
        Me.QSFMC.HeaderText = "Stock Fisico"
        Me.QSFMC.Name = "QSFMC"
        Me.QSFMC.ReadOnly = True
        Me.QSFMC.Width = 91
        '
        'TPAJUSTE
        '
        Me.TPAJUSTE.DataPropertyName = "TPAJUSTE"
        Me.TPAJUSTE.HeaderText = "Tipo de Ajuste"
        Me.TPAJUSTE.Name = "TPAJUSTE"
        Me.TPAJUSTE.ReadOnly = True
        Me.TPAJUSTE.Width = 103
        '
        'CNTREGULA
        '
        Me.CNTREGULA.DataPropertyName = "CNTREGULA"
        Me.CNTREGULA.HeaderText = "Cantidad Regularizar"
        Me.CNTREGULA.Name = "CNTREGULA"
        Me.CNTREGULA.ReadOnly = True
        Me.CNTREGULA.Width = 133
        '
        'FUNDS2
        '
        Me.FUNDS2.DataPropertyName = "FUNDS2"
        Me.FUNDS2.HeaderText = "Unidad de medida"
        Me.FUNDS2.Name = "FUNDS2"
        Me.FUNDS2.ReadOnly = True
        Me.FUNDS2.Width = 122
        '
        'CMRCD
        '
        Me.CMRCD.DataPropertyName = "CMRCD"
        Me.CMRCD.HeaderText = "CMRCD"
        Me.CMRCD.Name = "CMRCD"
        Me.CMRCD.ReadOnly = True
        Me.CMRCD.Visible = False
        Me.CMRCD.Width = 78
        '
        'NCNTR
        '
        Me.NCNTR.DataPropertyName = "NCNTR"
        Me.NCNTR.HeaderText = "NCNTR"
        Me.NCNTR.Name = "NCNTR"
        Me.NCNTR.ReadOnly = True
        Me.NCNTR.Visible = False
        Me.NCNTR.Width = 76
        '
        'NCRCTC
        '
        Me.NCRCTC.DataPropertyName = "NCRCTC"
        Me.NCRCTC.HeaderText = "NCRCTC"
        Me.NCRCTC.Name = "NCRCTC"
        Me.NCRCTC.ReadOnly = True
        Me.NCRCTC.Visible = False
        Me.NCRCTC.Width = 83
        '
        'NAUTR
        '
        Me.NAUTR.DataPropertyName = "NAUTR"
        Me.NAUTR.HeaderText = "NAUTR"
        Me.NAUTR.Name = "NAUTR"
        Me.NAUTR.ReadOnly = True
        Me.NAUTR.Visible = False
        Me.NAUTR.Width = 75
        '
        'CUNCN5
        '
        Me.CUNCN5.DataPropertyName = "CUNCN5"
        Me.CUNCN5.HeaderText = "CUNCN5"
        Me.CUNCN5.Name = "CUNCN5"
        Me.CUNCN5.ReadOnly = True
        Me.CUNCN5.Visible = False
        Me.CUNCN5.Width = 84
        '
        'CUNPS5
        '
        Me.CUNPS5.DataPropertyName = "CUNPS5"
        Me.CUNPS5.HeaderText = "CUNPS5"
        Me.CUNPS5.Name = "CUNPS5"
        Me.CUNPS5.ReadOnly = True
        Me.CUNPS5.Visible = False
        Me.CUNPS5.Width = 80
        '
        'CUNVL5
        '
        Me.CUNVL5.DataPropertyName = "CUNVL5"
        Me.CUNVL5.HeaderText = "CUNVL5"
        Me.CUNVL5.Name = "CUNVL5"
        Me.CUNVL5.ReadOnly = True
        Me.CUNVL5.Visible = False
        Me.CUNVL5.Width = 80
        '
        'CTPDP6
        '
        Me.CTPDP6.DataPropertyName = "CTPDP6"
        Me.CTPDP6.HeaderText = "CTPDP6"
        Me.CTPDP6.Name = "CTPDP6"
        Me.CTPDP6.ReadOnly = True
        Me.CTPDP6.Visible = False
        Me.CTPDP6.Width = 79
        '
        'tsMenuOpciones
        '
        Me.tsMenuOpciones.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tsMenuOpciones.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsMenuOpciones.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblTitulo})
        Me.tsMenuOpciones.Location = New System.Drawing.Point(0, 0)
        Me.tsMenuOpciones.Name = "tsMenuOpciones"
        Me.tsMenuOpciones.Size = New System.Drawing.Size(494, 25)
        Me.tsMenuOpciones.TabIndex = 94
        '
        'lblTitulo
        '
        Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(192, 22)
        Me.lblTitulo.Text = "Lista de Productos a Regularizar"
        '
        'hgOS
        '
        Me.hgOS.ButtonSpecs.AddRange(New ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup() {Me.bsaProcesar})
        Me.hgOS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.hgOS.HeaderVisibleSecondary = False
        Me.hgOS.Location = New System.Drawing.Point(0, 0)
        Me.hgOS.Name = "hgOS"
        '
        'hgOS.Panel
        '
        Me.hgOS.Panel.Controls.Add(Me.dtgKardex)
        Me.hgOS.Size = New System.Drawing.Size(540, 180)
        Me.hgOS.StateNormal.HeaderPrimary.Content.ShortText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hgOS.TabIndex = 95
        Me.hgOS.Text = "Kardex de Producto"
        Me.hgOS.ValuesPrimary.Description = ""
        Me.hgOS.ValuesPrimary.Heading = "Kardex de Producto"
        Me.hgOS.ValuesPrimary.Image = Nothing
        Me.hgOS.ValuesSecondary.Description = ""
        Me.hgOS.ValuesSecondary.Heading = "Description"
        Me.hgOS.ValuesSecondary.Image = Nothing
        '
        'bsaProcesar
        '
        Me.bsaProcesar.ExtraText = ""
        Me.bsaProcesar.Image = CType(resources.GetObject("bsaProcesar.Image"), System.Drawing.Image)
        Me.bsaProcesar.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Cluster
        Me.bsaProcesar.Text = "Procesar"
        Me.bsaProcesar.ToolTipImage = Nothing
        Me.bsaProcesar.UniqueName = "45745F99952B495545745F99952B4955"
        '
        'dtgKardex
        '
        Me.dtgKardex.AllowUserToAddRows = False
        Me.dtgKardex.AllowUserToDeleteRows = False
        Me.dtgKardex.AllowUserToResizeColumns = False
        Me.dtgKardex.AllowUserToResizeRows = False
        Me.dtgKardex.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dtgKardex.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PSCTPALM, Me.PSDESTIPO, Me.PSCALMC, Me.PSDESALM, Me.PSCZNALM, Me.PSDESZONA, Me.PNQSLMC2, Me.PNQSLMP2})
        Me.dtgKardex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtgKardex.Location = New System.Drawing.Point(0, 0)
        Me.dtgKardex.MultiSelect = False
        Me.dtgKardex.Name = "dtgKardex"
        Me.dtgKardex.ReadOnly = True
        Me.dtgKardex.RowHeadersWidth = 20
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtgKardex.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dtgKardex.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dtgKardex.Size = New System.Drawing.Size(538, 149)
        Me.dtgKardex.StandardTab = True
        Me.dtgKardex.StateCommon.Background.Color1 = System.Drawing.Color.White
        Me.dtgKardex.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList
        Me.dtgKardex.TabIndex = 25
        '
        'PSCTPALM
        '
        Me.PSCTPALM.DataPropertyName = "PSCTPALM"
        Me.PSCTPALM.HeaderText = "PSCTPALM"
        Me.PSCTPALM.Name = "PSCTPALM"
        Me.PSCTPALM.ReadOnly = True
        Me.PSCTPALM.Visible = False
        Me.PSCTPALM.Width = 93
        '
        'PSDESTIPO
        '
        Me.PSDESTIPO.DataPropertyName = "PSDESTIPO"
        Me.PSDESTIPO.HeaderText = "Tipo Almac?n"
        Me.PSDESTIPO.Name = "PSDESTIPO"
        Me.PSDESTIPO.ReadOnly = True
        Me.PSDESTIPO.Width = 110
        '
        'PSCALMC
        '
        Me.PSCALMC.DataPropertyName = "PSCALMC"
        Me.PSCALMC.HeaderText = "PSCALMC"
        Me.PSCALMC.Name = "PSCALMC"
        Me.PSCALMC.ReadOnly = True
        Me.PSCALMC.Visible = False
        Me.PSCALMC.Width = 86
        '
        'PSDESALM
        '
        Me.PSDESALM.DataPropertyName = "PSDESALM"
        Me.PSDESALM.HeaderText = "Almac?n"
        Me.PSDESALM.Name = "PSDESALM"
        Me.PSDESALM.ReadOnly = True
        Me.PSDESALM.Width = 83
        '
        'PSCZNALM
        '
        Me.PSCZNALM.DataPropertyName = "PSCZNALM"
        Me.PSCZNALM.HeaderText = "PSCZNALM"
        Me.PSCZNALM.Name = "PSCZNALM"
        Me.PSCZNALM.ReadOnly = True
        Me.PSCZNALM.Visible = False
        Me.PSCZNALM.Width = 94
        '
        'PSDESZONA
        '
        Me.PSDESZONA.DataPropertyName = "PSDESZONA"
        Me.PSDESZONA.HeaderText = "Zona Almac?n"
        Me.PSDESZONA.Name = "PSDESZONA"
        Me.PSDESZONA.ReadOnly = True
        Me.PSDESZONA.Width = 113
        '
        'PNQSLMC2
        '
        Me.PNQSLMC2.DataPropertyName = "PNQSLMC2"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PNQSLMC2.DefaultCellStyle = DataGridViewCellStyle1
        Me.PNQSLMC2.HeaderText = "Saldo Inventario"
        Me.PNQSLMC2.Name = "PNQSLMC2"
        Me.PNQSLMC2.ReadOnly = True
        Me.PNQSLMC2.Width = 121
        '
        'PNQSLMP2
        '
        Me.PNQSLMP2.DataPropertyName = "PNQSLMP2"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PNQSLMP2.DefaultCellStyle = DataGridViewCellStyle2
        Me.PNQSLMP2.HeaderText = "Peso Inventario"
        Me.PNQSLMP2.Name = "PNQSLMP2"
        Me.PNQSLMP2.ReadOnly = True
        Me.PNQSLMP2.Width = 117
        '
        'dgMercaderiaSeleccionada
        '
        Me.dgMercaderiaSeleccionada.AllowUserToAddRows = False
        Me.dgMercaderiaSeleccionada.AllowUserToDeleteRows = False
        Me.dgMercaderiaSeleccionada.AllowUserToResizeColumns = False
        Me.dgMercaderiaSeleccionada.AllowUserToResizeRows = False
        Me.dgMercaderiaSeleccionada.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgMercaderiaSeleccionada.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.S_CMRCLR, Me.PSTIPOAJUSTE, Me.S_CMRCD, Me.S_TCMPMR, Me.S_NORDSR, Me.S_NCNTR, Me.S_NCRCTC, Me.S_NAUTR, Me.S_CUNCNT, Me.S_CUNPST, Me.S_CUNVLT, Me.S_NORCCL, Me.S_NGUICL, Me.S_NRUCLL, Me.S_STPING, Me.S_CTPALM, Me.S_CALMC, Me.S_TCMPAL, Me.S_CZNALM, Me.S_TCMZNA, Me.S_QTRMC, Me.S_QTRMP, Me.S_QDSVGN, Me.S_CCNTD, Me.S_CTPOCN, Me.S_FUNDS2, Me.S_CTPDPS, Me.S_TOBSRV})
        Me.dgMercaderiaSeleccionada.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgMercaderiaSeleccionada.Location = New System.Drawing.Point(0, 25)
        Me.dgMercaderiaSeleccionada.MultiSelect = False
        Me.dgMercaderiaSeleccionada.Name = "dgMercaderiaSeleccionada"
        Me.dgMercaderiaSeleccionada.ReadOnly = True
        Me.dgMercaderiaSeleccionada.RowHeadersWidth = 20
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgMercaderiaSeleccionada.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgMercaderiaSeleccionada.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgMercaderiaSeleccionada.Size = New System.Drawing.Size(1039, 151)
        Me.dgMercaderiaSeleccionada.StandardTab = True
        Me.dgMercaderiaSeleccionada.StateCommon.Background.Color1 = System.Drawing.Color.White
        Me.dgMercaderiaSeleccionada.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList
        Me.dgMercaderiaSeleccionada.TabIndex = 96
        '
        'tsMenuProcesar
        '
        Me.tsMenuProcesar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tsMenuProcesar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsMenuProcesar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tss08, Me.btnEliminarItem, Me.tss06, Me.btnProcesarDespacho, Me.tss05})
        Me.tsMenuProcesar.Location = New System.Drawing.Point(0, 0)
        Me.tsMenuProcesar.Name = "tsMenuProcesar"
        Me.tsMenuProcesar.Size = New System.Drawing.Size(1039, 25)
        Me.tsMenuProcesar.TabIndex = 97
        '
        'tss08
        '
        Me.tss08.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tss08.Name = "tss08"
        Me.tss08.Size = New System.Drawing.Size(121, 22)
        Me.tss08.Text = "Lista de Mercader?a"
        '
        'btnEliminarItem
        '
        Me.btnEliminarItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnEliminarItem.Enabled = False
        Me.btnEliminarItem.Image = CType(resources.GetObject("btnEliminarItem.Image"), System.Drawing.Image)
        Me.btnEliminarItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEliminarItem.Name = "btnEliminarItem"
        Me.btnEliminarItem.Size = New System.Drawing.Size(70, 22)
        Me.btnEliminarItem.Text = "&Eliminar"
        '
        'tss06
        '
        Me.tss06.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tss06.Name = "tss06"
        Me.tss06.Size = New System.Drawing.Size(6, 25)
        '
        'btnProcesarDespacho
        '
        Me.btnProcesarDespacho.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnProcesarDespacho.Enabled = False
        Me.btnProcesarDespacho.Image = CType(resources.GetObject("btnProcesarDespacho.Image"), System.Drawing.Image)
        Me.btnProcesarDespacho.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnProcesarDespacho.Name = "btnProcesarDespacho"
        Me.btnProcesarDespacho.Size = New System.Drawing.Size(127, 22)
        Me.btnProcesarDespacho.Text = "&Procesar Despacho"
        '
        'tss05
        '
        Me.tss05.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tss05.Name = "tss05"
        Me.tss05.Size = New System.Drawing.Size(6, 25)
        '
        'S_CMRCLR
        '
        Me.S_CMRCLR.DataPropertyName = "PSCMRCLR"
        Me.S_CMRCLR.HeaderText = "Mercaderia"
        Me.S_CMRCLR.Name = "S_CMRCLR"
        Me.S_CMRCLR.ReadOnly = True
        Me.S_CMRCLR.Width = 95
        '
        'PSTIPOAJUSTE
        '
        Me.PSTIPOAJUSTE.DataPropertyName = "PSTIPOAJUSTE"
        Me.PSTIPOAJUSTE.HeaderText = "PSTIPOAJUSTE"
        Me.PSTIPOAJUSTE.Name = "PSTIPOAJUSTE"
        Me.PSTIPOAJUSTE.ReadOnly = True
        Me.PSTIPOAJUSTE.Visible = False
        Me.PSTIPOAJUSTE.Width = 114
        '
        'S_CMRCD
        '
        Me.S_CMRCD.DataPropertyName = "PSCMRCD"
        Me.S_CMRCD.HeaderText = "C?d. Ransa"
        Me.S_CMRCD.Name = "S_CMRCD"
        Me.S_CMRCD.ReadOnly = True
        Me.S_CMRCD.Width = 95
        '
        'S_TCMPMR
        '
        Me.S_TCMPMR.DataPropertyName = "PSTCMPMR"
        Me.S_TCMPMR.HeaderText = "Detalle"
        Me.S_TCMPMR.Name = "S_TCMPMR"
        Me.S_TCMPMR.ReadOnly = True
        Me.S_TCMPMR.Width = 72
        '
        'S_NORDSR
        '
        Me.S_NORDSR.DataPropertyName = "PNNORDSR"
        Me.S_NORDSR.HeaderText = "Orden Servicio"
        Me.S_NORDSR.Name = "S_NORDSR"
        Me.S_NORDSR.ReadOnly = True
        Me.S_NORDSR.Width = 113
        '
        'S_NCNTR
        '
        Me.S_NCNTR.DataPropertyName = "PNNCNTR"
        Me.S_NCNTR.HeaderText = "No. Contrato"
        Me.S_NCNTR.Name = "S_NCNTR"
        Me.S_NCNTR.ReadOnly = True
        Me.S_NCNTR.Width = 105
        '
        'S_NCRCTC
        '
        Me.S_NCRCTC.DataPropertyName = "PNNCRCTC"
        Me.S_NCRCTC.HeaderText = "Correlativo"
        Me.S_NCRCTC.Name = "S_NCRCTC"
        Me.S_NCRCTC.ReadOnly = True
        Me.S_NCRCTC.Width = 94
        '
        'S_NAUTR
        '
        Me.S_NAUTR.DataPropertyName = "PNNAUTR"
        Me.S_NAUTR.HeaderText = "No Autorizaci?n"
        Me.S_NAUTR.Name = "S_NAUTR"
        Me.S_NAUTR.ReadOnly = True
        Me.S_NAUTR.Width = 122
        '
        'S_CUNCNT
        '
        Me.S_CUNCNT.DataPropertyName = "PSCUNCNT"
        Me.S_CUNCNT.HeaderText = "U. Cantidad"
        Me.S_CUNCNT.Name = "S_CUNCNT"
        Me.S_CUNCNT.ReadOnly = True
        Me.S_CUNCNT.Width = 98
        '
        'S_CUNPST
        '
        Me.S_CUNPST.DataPropertyName = "PSCUNPST"
        Me.S_CUNPST.HeaderText = "U. Peso"
        Me.S_CUNPST.Name = "S_CUNPST"
        Me.S_CUNPST.ReadOnly = True
        Me.S_CUNPST.Width = 75
        '
        'S_CUNVLT
        '
        Me.S_CUNVLT.DataPropertyName = "PSCUNVLT"
        Me.S_CUNVLT.HeaderText = "U. Valor"
        Me.S_CUNVLT.Name = "S_CUNVLT"
        Me.S_CUNVLT.ReadOnly = True
        Me.S_CUNVLT.Width = 77
        '
        'S_NORCCL
        '
        Me.S_NORCCL.DataPropertyName = "PSNORCCL"
        Me.S_NORCCL.HeaderText = "No O/C"
        Me.S_NORCCL.Name = "S_NORCCL"
        Me.S_NORCCL.ReadOnly = True
        Me.S_NORCCL.Width = 77
        '
        'S_NGUICL
        '
        Me.S_NGUICL.DataPropertyName = "PSNGUICL"
        Me.S_NGUICL.HeaderText = "No Gu?a Cliente"
        Me.S_NGUICL.Name = "S_NGUICL"
        Me.S_NGUICL.ReadOnly = True
        Me.S_NGUICL.Width = 119
        '
        'S_NRUCLL
        '
        Me.S_NRUCLL.DataPropertyName = "PSNRUCLL"
        Me.S_NRUCLL.HeaderText = "No RUC Cliente"
        Me.S_NRUCLL.Name = "S_NRUCLL"
        Me.S_NRUCLL.ReadOnly = True
        Me.S_NRUCLL.Width = 118
        '
        'S_STPING
        '
        Me.S_STPING.DataPropertyName = "PSSTPING"
        Me.S_STPING.HeaderText = "Tipo Mov."
        Me.S_STPING.Name = "S_STPING"
        Me.S_STPING.ReadOnly = True
        Me.S_STPING.Width = 90
        '
        'S_CTPALM
        '
        Me.S_CTPALM.DataPropertyName = "PSCTPALM"
        Me.S_CTPALM.HeaderText = "Tipo Almac?n"
        Me.S_CTPALM.Name = "S_CTPALM"
        Me.S_CTPALM.ReadOnly = True
        Me.S_CTPALM.Width = 110
        '
        'S_CALMC
        '
        Me.S_CALMC.DataPropertyName = "PSCALMC"
        Me.S_CALMC.HeaderText = "Almac?n"
        Me.S_CALMC.Name = "S_CALMC"
        Me.S_CALMC.ReadOnly = True
        Me.S_CALMC.Width = 83
        '
        'S_TCMPAL
        '
        Me.S_TCMPAL.DataPropertyName = "PSTCMPAL"
        Me.S_TCMPAL.HeaderText = "Detalle"
        Me.S_TCMPAL.Name = "S_TCMPAL"
        Me.S_TCMPAL.ReadOnly = True
        Me.S_TCMPAL.Width = 72
        '
        'S_CZNALM
        '
        Me.S_CZNALM.DataPropertyName = "PSCZNALM"
        Me.S_CZNALM.HeaderText = "Zona"
        Me.S_CZNALM.Name = "S_CZNALM"
        Me.S_CZNALM.ReadOnly = True
        Me.S_CZNALM.Width = 63
        '
        'S_TCMZNA
        '
        Me.S_TCMZNA.DataPropertyName = "PSTCMZNA"
        Me.S_TCMZNA.HeaderText = "Detalle"
        Me.S_TCMZNA.Name = "S_TCMZNA"
        Me.S_TCMZNA.ReadOnly = True
        Me.S_TCMZNA.Width = 72
        '
        'S_QTRMC
        '
        Me.S_QTRMC.DataPropertyName = "PNQTRMC"
        Me.S_QTRMC.HeaderText = "Cantidad"
        Me.S_QTRMC.Name = "S_QTRMC"
        Me.S_QTRMC.ReadOnly = True
        Me.S_QTRMC.Width = 84
        '
        'S_QTRMP
        '
        Me.S_QTRMP.DataPropertyName = "PNQTRMP"
        Me.S_QTRMP.HeaderText = "Peso"
        Me.S_QTRMP.Name = "S_QTRMP"
        Me.S_QTRMP.ReadOnly = True
        Me.S_QTRMP.Width = 61
        '
        'S_QDSVGN
        '
        Me.S_QDSVGN.DataPropertyName = "PNQDSVGN"
        Me.S_QDSVGN.HeaderText = "No D?as Vigencia"
        Me.S_QDSVGN.Name = "S_QDSVGN"
        Me.S_QDSVGN.ReadOnly = True
        Me.S_QDSVGN.Width = 125
        '
        'S_CCNTD
        '
        Me.S_CCNTD.DataPropertyName = "PSCCNTD"
        Me.S_CCNTD.HeaderText = "Contenedor"
        Me.S_CCNTD.Name = "S_CCNTD"
        Me.S_CCNTD.ReadOnly = True
        Me.S_CCNTD.Width = 99
        '
        'S_CTPOCN
        '
        Me.S_CTPOCN.DataPropertyName = "PSCTPOCN"
        Me.S_CTPOCN.FalseValue = "FALSE"
        Me.S_CTPOCN.HeaderText = "Desglosa"
        Me.S_CTPOCN.Name = "S_CTPOCN"
        Me.S_CTPOCN.ReadOnly = True
        Me.S_CTPOCN.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.S_CTPOCN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.S_CTPOCN.TrueValue = "TRUE"
        Me.S_CTPOCN.Width = 83
        '
        'S_FUNDS2
        '
        Me.S_FUNDS2.DataPropertyName = "PSFUNDS2"
        Me.S_FUNDS2.HeaderText = "U. Despacho"
        Me.S_FUNDS2.Name = "S_FUNDS2"
        Me.S_FUNDS2.ReadOnly = True
        Me.S_FUNDS2.Width = 102
        '
        'S_CTPDPS
        '
        Me.S_CTPDPS.DataPropertyName = "PSCTPDPS"
        Me.S_CTPDPS.HeaderText = "Tipo Dep?sito"
        Me.S_CTPDPS.Name = "S_CTPDPS"
        Me.S_CTPDPS.ReadOnly = True
        Me.S_CTPDPS.Width = 110
        '
        'S_TOBSRV
        '
        Me.S_TOBSRV.DataPropertyName = "PSTOBSRV"
        Me.S_TOBSRV.HeaderText = "Observaciones"
        Me.S_TOBSRV.Name = "S_TOBSRV"
        Me.S_TOBSRV.ReadOnly = True
        Me.S_TOBSRV.Width = 113
        '
        'frmAjustarInventario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1039, 487)
        Me.Controls.Add(Me.Contenedor1)
        Me.Controls.Add(Me.hgFiltros)
        Me.Name = "frmAjustarInventario"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ajuste de Inventario"
        CType(Me.hgFiltros.Panel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hgFiltros.Panel.ResumeLayout(False)
        Me.hgFiltros.Panel.PerformLayout()
        CType(Me.hgFiltros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hgFiltros.ResumeLayout(False)
        Me.grpLeyenda.ResumeLayout(False)
        Me.grpLeyenda.PerformLayout()
        CType(Me.Contenedor1.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Contenedor1.Panel1.ResumeLayout(False)
        CType(Me.Contenedor1.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Contenedor1.Panel2.ResumeLayout(False)
        Me.Contenedor1.Panel2.PerformLayout()
        CType(Me.Contenedor1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Contenedor1.ResumeLayout(False)
        CType(Me.Contenedor2.Panel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Contenedor2.Panel1.ResumeLayout(False)
        Me.Contenedor2.Panel1.PerformLayout()
        CType(Me.Contenedor2.Panel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Contenedor2.Panel2.ResumeLayout(False)
        CType(Me.Contenedor2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Contenedor2.ResumeLayout(False)
        CType(Me.dgMercaderia, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsMenuOpciones.ResumeLayout(False)
        Me.tsMenuOpciones.PerformLayout()
        CType(Me.hgOS.Panel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hgOS.Panel.ResumeLayout(False)
        CType(Me.hgOS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hgOS.ResumeLayout(False)
        CType(Me.dtgKardex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgMercaderiaSeleccionada, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsMenuProcesar.ResumeLayout(False)
        Me.tsMenuProcesar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents hgFiltros As ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup
    Friend WithEvents bsaOcultarFiltros As ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup
    Friend WithEvents bsaCerrar As ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup
    Friend WithEvents grpLeyenda As System.Windows.Forms.GroupBox
    Friend WithEvents lblDetalleOpcional As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents lblDetalleObligatorio As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents lblOpcional As System.Windows.Forms.Label
    Friend WithEvents lblObligatorio As System.Windows.Forms.Label
    Friend WithEvents txtNroDocEri As ComponentFactory.Krypton.Toolkit.KryptonTextBox
    Friend WithEvents lblMercadaeria As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel1 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents KryptonLabel5 As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Friend WithEvents txtFecInv As ComponentFactory.Krypton.Toolkit.KryptonTextBox
    Friend WithEvents Contenedor1 As ComponentFactory.Krypton.Toolkit.KryptonSplitContainer
    Friend WithEvents Contenedor2 As ComponentFactory.Krypton.Toolkit.KryptonSplitContainer
    Friend WithEvents dgMercaderia As ComponentFactory.Krypton.Toolkit.KryptonDataGridView
    Private WithEvents tsMenuOpciones As System.Windows.Forms.ToolStrip
    Private WithEvents lblTitulo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents hgOS As ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup
    Friend WithEvents bsaProcesar As ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup
    Friend WithEvents dtgKardex As ComponentFactory.Krypton.Toolkit.KryptonDataGridView
    Friend WithEvents PSCTPALM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PSDESTIPO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PSCALMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PSDESALM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PSCZNALM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PSDESZONA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PNQSLMC2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PNQSLMP2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgMercaderiaSeleccionada As ComponentFactory.Krypton.Toolkit.KryptonDataGridView
    Private WithEvents tsMenuProcesar As System.Windows.Forms.ToolStrip
    Private WithEvents tss08 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnEliminarItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents tss06 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnProcesarDespacho As System.Windows.Forms.ToolStripButton
    Friend WithEvents tss05 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents NORDSR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CMRCLR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NORDSR1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TCMPMR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QSLMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QSFMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TPAJUSTE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CNTREGULA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FUNDS2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CMRCD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NCNTR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NCRCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NAUTR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CUNCN5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CUNPS5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CUNVL5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CTPDP6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CMRCLR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PSTIPOAJUSTE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CMRCD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_TCMPMR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_NORDSR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_NCNTR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_NCRCTC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_NAUTR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CUNCNT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CUNPST As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CUNVLT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_NORCCL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_NGUICL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_NRUCLL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_STPING As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CTPALM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CALMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_TCMPAL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CZNALM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_TCMZNA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_QTRMC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_QTRMP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_QDSVGN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CCNTD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CTPOCN As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents S_FUNDS2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_CTPDPS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_TOBSRV As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
