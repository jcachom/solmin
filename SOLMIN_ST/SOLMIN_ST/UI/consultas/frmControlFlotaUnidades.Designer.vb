<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmControlFlotaUnidades
  Inherits ComponentFactory.Krypton.Toolkit.KryptonForm

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.KryptonPanel = New ComponentFactory.Krypton.Toolkit.KryptonPanel
        Me.dtgControlFlotaUnidades = New ComponentFactory.Krypton.Toolkit.KryptonDataGridView
        Me.KryptonPanel1 = New ComponentFactory.Krypton.Toolkit.KryptonPanel
        Me.btnRegresar = New ComponentFactory.Krypton.Toolkit.KryptonButton
        Me.KryptonManager = New ComponentFactory.Krypton.Toolkit.KryptonManager(Me.components)
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NMRCRL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FECSEG_S = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NPLCUN = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NPLCAC = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TCMCND = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TDSDES = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GPSLAT = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GPSLON = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FECGPS = New System.Windows.Forms.DataGridViewLinkColumn
        Me.HORGPS = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SEGWAP = New System.Windows.Forms.DataGridViewLinkColumn
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel.SuspendLayout()
        CType(Me.dtgControlFlotaUnidades, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.KryptonPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'KryptonPanel
        '
        Me.KryptonPanel.Controls.Add(Me.dtgControlFlotaUnidades)
        Me.KryptonPanel.Controls.Add(Me.KryptonPanel1)
        Me.KryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonPanel.Location = New System.Drawing.Point(0, 0)
        Me.KryptonPanel.Name = "KryptonPanel"
        Me.KryptonPanel.Size = New System.Drawing.Size(1091, 614)
        Me.KryptonPanel.TabIndex = 0
        '
        'dtgControlFlotaUnidades
        '
        Me.dtgControlFlotaUnidades.AllowUserToAddRows = False
        Me.dtgControlFlotaUnidades.AllowUserToDeleteRows = False
        Me.dtgControlFlotaUnidades.AllowUserToResizeColumns = False
        Me.dtgControlFlotaUnidades.AllowUserToResizeRows = False
        Me.dtgControlFlotaUnidades.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dtgControlFlotaUnidades.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dtgControlFlotaUnidades.ColumnHeadersHeight = 45
        Me.dtgControlFlotaUnidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dtgControlFlotaUnidades.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NMRCRL, Me.FECSEG_S, Me.NPLCUN, Me.NPLCAC, Me.TCMCND, Me.TDSDES, Me.GPSLAT, Me.GPSLON, Me.FECGPS, Me.HORGPS, Me.SEGWAP})
        Me.dtgControlFlotaUnidades.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtgControlFlotaUnidades.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dtgControlFlotaUnidades.Location = New System.Drawing.Point(0, 0)
        Me.dtgControlFlotaUnidades.MultiSelect = False
        Me.dtgControlFlotaUnidades.Name = "dtgControlFlotaUnidades"
        Me.dtgControlFlotaUnidades.ReadOnly = True
        Me.dtgControlFlotaUnidades.RowHeadersVisible = False
        Me.dtgControlFlotaUnidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dtgControlFlotaUnidades.Size = New System.Drawing.Size(1091, 525)
        Me.dtgControlFlotaUnidades.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList
        Me.dtgControlFlotaUnidades.StateCommon.HeaderColumn.Content.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtgControlFlotaUnidades.StateCommon.HeaderColumn.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Center
        Me.dtgControlFlotaUnidades.TabIndex = 8
        Me.dtgControlFlotaUnidades.TabStop = False
        '
        'KryptonPanel1
        '
        Me.KryptonPanel1.Controls.Add(Me.btnRegresar)
        Me.KryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.KryptonPanel1.Location = New System.Drawing.Point(0, 525)
        Me.KryptonPanel1.Name = "KryptonPanel1"
        Me.KryptonPanel1.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.HeaderSecondary
        Me.KryptonPanel1.Size = New System.Drawing.Size(1091, 89)
        Me.KryptonPanel1.TabIndex = 7
        '
        'btnRegresar
        '
        Me.btnRegresar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRegresar.Location = New System.Drawing.Point(865, 0)
        Me.btnRegresar.Name = "btnRegresar"
        Me.btnRegresar.Size = New System.Drawing.Size(224, 87)
        Me.btnRegresar.StateCommon.Content.ShortText.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRegresar.TabIndex = 6
        Me.btnRegresar.Text = "REGRESAR"
        Me.btnRegresar.Values.ExtraText = ""
        Me.btnRegresar.Values.Image = Global.SOLMIN_ST.My.Resources.Resources.flecha
        Me.btnRegresar.Values.ImageStates.ImageCheckedNormal = Nothing
        Me.btnRegresar.Values.ImageStates.ImageCheckedPressed = Nothing
        Me.btnRegresar.Values.ImageStates.ImageCheckedTracking = Nothing
        Me.btnRegresar.Values.Text = "REGRESAR"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "NRITEM"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn1.HeaderText = "Item"
        Me.DataGridViewTextBoxColumn1.MaxInputLength = 20
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 88
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "NPLCUN"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn2.HeaderText = "Tracto"
        Me.DataGridViewTextBoxColumn2.MaxInputLength = 10
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 109
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "NPLCAC"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn3.HeaderText = "Acoplado"
        Me.DataGridViewTextBoxColumn3.MaxInputLength = 10
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 209
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "TOBS"
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewTextBoxColumn4.HeaderText = "Condición"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "TOBGFH"
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridViewTextBoxColumn5.HeaderText = "GFH"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "TDSDES"
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridViewTextBoxColumn6.HeaderText = "GFH"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "GPSLAT"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Latitud"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        Me.DataGridViewTextBoxColumn7.Width = 113
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "GPSLON"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Longitud"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 135
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "HORGPS"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Hora GPS"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.Visible = False
        Me.DataGridViewTextBoxColumn9.Width = 150
        '
        'NMRCRL
        '
        Me.NMRCRL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.NMRCRL.DataPropertyName = "NMRCRL"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NMRCRL.DefaultCellStyle = DataGridViewCellStyle1
        Me.NMRCRL.HeaderText = "Item"
        Me.NMRCRL.MaxInputLength = 20
        Me.NMRCRL.Name = "NMRCRL"
        Me.NMRCRL.ReadOnly = True
        Me.NMRCRL.Visible = False
        Me.NMRCRL.Width = 88
        '
        'FECSEG_S
        '
        Me.FECSEG_S.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.FECSEG_S.DataPropertyName = "FECSEG_S"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FECSEG_S.DefaultCellStyle = DataGridViewCellStyle2
        Me.FECSEG_S.HeaderText = "Fecha"
        Me.FECSEG_S.Name = "FECSEG_S"
        Me.FECSEG_S.ReadOnly = True
        Me.FECSEG_S.Width = 109
        '
        'NPLCUN
        '
        Me.NPLCUN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.NPLCUN.DataPropertyName = "NPLCUN"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NPLCUN.DefaultCellStyle = DataGridViewCellStyle3
        Me.NPLCUN.HeaderText = "N° Placa Tracto"
        Me.NPLCUN.MaxInputLength = 10
        Me.NPLCUN.Name = "NPLCUN"
        Me.NPLCUN.ReadOnly = True
        Me.NPLCUN.Width = 209
        '
        'NPLCAC
        '
        Me.NPLCAC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.NPLCAC.DataPropertyName = "NPLCAC"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NPLCAC.DefaultCellStyle = DataGridViewCellStyle4
        Me.NPLCAC.HeaderText = "N° Placa Acoplado"
        Me.NPLCAC.Name = "NPLCAC"
        Me.NPLCAC.ReadOnly = True
        Me.NPLCAC.Width = 242
        '
        'TCMCND
        '
        Me.TCMCND.DataPropertyName = "TCMCND"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TCMCND.DefaultCellStyle = DataGridViewCellStyle5
        Me.TCMCND.HeaderText = "Condición"
        Me.TCMCND.Name = "TCMCND"
        Me.TCMCND.ReadOnly = True
        Me.TCMCND.Width = 151
        '
        'TDSDES
        '
        Me.TDSDES.DataPropertyName = "TDSDES"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TDSDES.DefaultCellStyle = DataGridViewCellStyle6
        Me.TDSDES.HeaderText = "GFH"
        Me.TDSDES.Name = "TDSDES"
        Me.TDSDES.ReadOnly = True
        Me.TDSDES.Width = 92
        '
        'GPSLAT
        '
        Me.GPSLAT.DataPropertyName = "GPSLAT"
        Me.GPSLAT.HeaderText = "Latitud"
        Me.GPSLAT.Name = "GPSLAT"
        Me.GPSLAT.ReadOnly = True
        Me.GPSLAT.Visible = False
        Me.GPSLAT.Width = 113
        '
        'GPSLON
        '
        Me.GPSLON.DataPropertyName = "GPSLON"
        Me.GPSLON.HeaderText = "Longitud"
        Me.GPSLON.Name = "GPSLON"
        Me.GPSLON.ReadOnly = True
        Me.GPSLON.Visible = False
        Me.GPSLON.Width = 135
        '
        'FECGPS
        '
        Me.FECGPS.ActiveLinkColor = System.Drawing.Color.Teal
        Me.FECGPS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.FECGPS.DataPropertyName = "FECGPS"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FECGPS.DefaultCellStyle = DataGridViewCellStyle7
        Me.FECGPS.HeaderText = "GPS"
        Me.FECGPS.LinkColor = System.Drawing.Color.Green
        Me.FECGPS.Name = "FECGPS"
        Me.FECGPS.ReadOnly = True
        Me.FECGPS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.FECGPS.Text = ""
        Me.FECGPS.ToolTipText = "Hacer click para ver localización de Vehículo en Google Maps"
        Me.FECGPS.VisitedLinkColor = System.Drawing.Color.Teal
        '
        'HORGPS
        '
        Me.HORGPS.DataPropertyName = "HORGPS"
        Me.HORGPS.HeaderText = "Hora GPS"
        Me.HORGPS.Name = "HORGPS"
        Me.HORGPS.ReadOnly = True
        Me.HORGPS.Visible = False
        Me.HORGPS.Width = 150
        '
        'SEGWAP
        '
        Me.SEGWAP.ActiveLinkColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SEGWAP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SEGWAP.DataPropertyName = "SEGWAP"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SEGWAP.DefaultCellStyle = DataGridViewCellStyle8
        Me.SEGWAP.HeaderText = "WAP"
        Me.SEGWAP.LinkColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SEGWAP.Name = "SEGWAP"
        Me.SEGWAP.ReadOnly = True
        Me.SEGWAP.Text = ""
        Me.SEGWAP.ToolTipText = "Hacer click para ver seguimiento WAP"
        Me.SEGWAP.VisitedLinkColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'frmControlFlotaUnidades
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1091, 614)
        Me.ControlBox = False
        Me.Controls.Add(Me.KryptonPanel)
        Me.Name = "frmControlFlotaUnidades"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.StateCommon.Header.Content.ShortText.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text = "Control & Gestión de Unidades"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel.ResumeLayout(False)
        CType(Me.dtgControlFlotaUnidades, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.KryptonPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents KryptonPanel As ComponentFactory.Krypton.Toolkit.KryptonPanel
    Friend WithEvents KryptonManager As ComponentFactory.Krypton.Toolkit.KryptonManager

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents btnRegresar As ComponentFactory.Krypton.Toolkit.KryptonButton
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KryptonPanel1 As ComponentFactory.Krypton.Toolkit.KryptonPanel
    Friend WithEvents dtgControlFlotaUnidades As ComponentFactory.Krypton.Toolkit.KryptonDataGridView
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NMRCRL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FECSEG_S As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NPLCUN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NPLCAC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TCMCND As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TDSDES As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GPSLAT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GPSLON As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FECGPS As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents HORGPS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SEGWAP As System.Windows.Forms.DataGridViewLinkColumn
End Class
