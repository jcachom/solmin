<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpcionAgregarOperacion
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
    Me.KryptonManager = New ComponentFactory.Krypton.Toolkit.KryptonManager(Me.components)
    Me.MenuBar = New System.Windows.Forms.ToolStrip
    Me.btnCancelar = New System.Windows.Forms.ToolStripButton
    Me.Separador = New System.Windows.Forms.ToolStripSeparator
    Me.btnAceptar = New System.Windows.Forms.ToolStripButton
    Me.groupElegir = New ComponentFactory.Krypton.Toolkit.KryptonGroup
    Me.btnCerrar = New ComponentFactory.Krypton.Toolkit.KryptonButton
    Me.btnOperacion = New ComponentFactory.Krypton.Toolkit.KryptonButton
    Me.rbtnOperacion = New ComponentFactory.Krypton.Toolkit.KryptonRadioButton
    Me.txtOperacion = New ComponentFactory.Krypton.Toolkit.KryptonTextBox
    Me.rbtnGuiaTransporte = New ComponentFactory.Krypton.Toolkit.KryptonRadioButton
    Me.panelOpcion = New ComponentFactory.Krypton.Toolkit.KryptonPanel
    Me.MenuBar.SuspendLayout()
    CType(Me.groupElegir, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.groupElegir.Panel, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.groupElegir.Panel.SuspendLayout()
    Me.groupElegir.SuspendLayout()
    CType(Me.panelOpcion, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.panelOpcion.SuspendLayout()
    Me.SuspendLayout()
    '
    'MenuBar
    '
    Me.MenuBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
    Me.MenuBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnCancelar, Me.Separador, Me.btnAceptar})
    Me.MenuBar.Location = New System.Drawing.Point(0, 0)
    Me.MenuBar.Name = "MenuBar"
    Me.MenuBar.Size = New System.Drawing.Size(263, 25)
    Me.MenuBar.TabIndex = 5
    '
    'btnCancelar
    '
    Me.btnCancelar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
    Me.btnCancelar.Image = Global.SOLMIN_ST.My.Resources.Resources.button_cancel
    Me.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.btnCancelar.Name = "btnCancelar"
    Me.btnCancelar.Size = New System.Drawing.Size(69, 22)
    Me.btnCancelar.Text = "Cancelar"
    '
    'Separador
    '
    Me.Separador.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
    Me.Separador.Name = "Separador"
    Me.Separador.Size = New System.Drawing.Size(6, 25)
    '
    'btnAceptar
    '
    Me.btnAceptar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
    Me.btnAceptar.Image = Global.SOLMIN_ST.My.Resources.Resources.button_ok1
    Me.btnAceptar.ImageTransparentColor = System.Drawing.Color.Magenta
    Me.btnAceptar.Name = "btnAceptar"
    Me.btnAceptar.Size = New System.Drawing.Size(64, 22)
    Me.btnAceptar.Text = "Aceptar"
    '
    'groupElegir
    '
    Me.groupElegir.Dock = System.Windows.Forms.DockStyle.Fill
    Me.groupElegir.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone
    Me.groupElegir.Location = New System.Drawing.Point(0, 25)
    Me.groupElegir.Name = "groupElegir"
    Me.groupElegir.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue
    '
    'groupElegir.Panel
    '
    Me.groupElegir.Panel.Controls.Add(Me.btnCerrar)
    Me.groupElegir.Panel.Controls.Add(Me.btnOperacion)
    Me.groupElegir.Panel.Controls.Add(Me.rbtnOperacion)
    Me.groupElegir.Panel.Controls.Add(Me.txtOperacion)
    Me.groupElegir.Panel.Controls.Add(Me.rbtnGuiaTransporte)
    Me.groupElegir.Size = New System.Drawing.Size(263, 65)
    Me.groupElegir.TabIndex = 426
    '
    'btnCerrar
    '
    Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnCerrar.Location = New System.Drawing.Point(220, -25)
    Me.btnCerrar.Name = "btnCerrar"
    Me.btnCerrar.Size = New System.Drawing.Size(8, 25)
    Me.btnCerrar.TabIndex = 428
    Me.btnCerrar.Text = "."
    Me.btnCerrar.Values.ExtraText = ""
    Me.btnCerrar.Values.Image = Nothing
    Me.btnCerrar.Values.ImageStates.ImageCheckedNormal = Nothing
    Me.btnCerrar.Values.ImageStates.ImageCheckedPressed = Nothing
    Me.btnCerrar.Values.ImageStates.ImageCheckedTracking = Nothing
    Me.btnCerrar.Values.Text = "."
    '
    'btnOperacion
    '
    Me.btnOperacion.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Custom3
    Me.btnOperacion.Enabled = False
    Me.btnOperacion.Location = New System.Drawing.Point(228, 32)
    Me.btnOperacion.Name = "btnOperacion"
    Me.btnOperacion.Size = New System.Drawing.Size(25, 22)
    Me.btnOperacion.TabIndex = 427
    Me.btnOperacion.Values.ExtraText = ""
    Me.btnOperacion.Values.Image = Global.SOLMIN_ST.My.Resources.Resources.cell_layout
    Me.btnOperacion.Values.ImageStates.ImageCheckedNormal = Nothing
    Me.btnOperacion.Values.ImageStates.ImageCheckedPressed = Nothing
    Me.btnOperacion.Values.ImageStates.ImageCheckedTracking = Nothing
    Me.btnOperacion.Values.Text = ""
    '
    'rbtnOperacion
    '
    Me.rbtnOperacion.Location = New System.Drawing.Point(8, 34)
    Me.rbtnOperacion.Name = "rbtnOperacion"
    Me.rbtnOperacion.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue
    Me.rbtnOperacion.Size = New System.Drawing.Size(75, 16)
    Me.rbtnOperacion.TabIndex = 1
    Me.rbtnOperacion.TabStop = False
    Me.rbtnOperacion.Tag = ""
    Me.rbtnOperacion.Text = "Operaci�n"
    Me.rbtnOperacion.Values.ExtraText = ""
    Me.rbtnOperacion.Values.Image = Nothing
    Me.rbtnOperacion.Values.Text = "Operaci�n"
    '
    'txtOperacion
    '
    Me.txtOperacion.Enabled = False
    Me.txtOperacion.Location = New System.Drawing.Point(127, 33)
    Me.txtOperacion.MaxLength = 10
    Me.txtOperacion.Name = "txtOperacion"
    Me.txtOperacion.Size = New System.Drawing.Size(100, 19)
    Me.txtOperacion.TabIndex = 6
    Me.txtOperacion.TabStop = False
    '
    'rbtnGuiaTransporte
    '
    Me.rbtnGuiaTransporte.Checked = True
    Me.rbtnGuiaTransporte.Location = New System.Drawing.Point(8, 8)
    Me.rbtnGuiaTransporte.Name = "rbtnGuiaTransporte"
    Me.rbtnGuiaTransporte.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue
    Me.rbtnGuiaTransporte.Size = New System.Drawing.Size(115, 16)
    Me.rbtnGuiaTransporte.TabIndex = 0
    Me.rbtnGuiaTransporte.TabStop = False
    Me.rbtnGuiaTransporte.Tag = ""
    Me.rbtnGuiaTransporte.Text = "Gu�a Transportista"
    Me.rbtnGuiaTransporte.Values.ExtraText = ""
    Me.rbtnGuiaTransporte.Values.Image = Nothing
    Me.rbtnGuiaTransporte.Values.Text = "Gu�a Transportista"
    '
    'panelOpcion
    '
    Me.panelOpcion.Controls.Add(Me.groupElegir)
    Me.panelOpcion.Controls.Add(Me.MenuBar)
    Me.panelOpcion.Dock = System.Windows.Forms.DockStyle.Fill
    Me.panelOpcion.Location = New System.Drawing.Point(0, 0)
    Me.panelOpcion.Name = "panelOpcion"
    Me.panelOpcion.Size = New System.Drawing.Size(263, 90)
    Me.panelOpcion.StateCommon.Color1 = System.Drawing.Color.White
    Me.panelOpcion.TabIndex = 1
    '
    'frmOpcionAgregarOperacion
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(263, 90)
    Me.ControlBox = False
    Me.Controls.Add(Me.panelOpcion)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Name = "frmOpcionAgregarOperacion"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Opci�n Agregar Operaci�n"
    Me.MenuBar.ResumeLayout(False)
    Me.MenuBar.PerformLayout()
    CType(Me.groupElegir.Panel, System.ComponentModel.ISupportInitialize).EndInit()
    Me.groupElegir.Panel.ResumeLayout(False)
    Me.groupElegir.Panel.PerformLayout()
    CType(Me.groupElegir, System.ComponentModel.ISupportInitialize).EndInit()
    Me.groupElegir.ResumeLayout(False)
    CType(Me.panelOpcion, System.ComponentModel.ISupportInitialize).EndInit()
    Me.panelOpcion.ResumeLayout(False)
    Me.panelOpcion.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents KryptonManager As ComponentFactory.Krypton.Toolkit.KryptonManager

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

  End Sub

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub
  Friend WithEvents MenuBar As System.Windows.Forms.ToolStrip
  Friend WithEvents btnCancelar As System.Windows.Forms.ToolStripButton
  Friend WithEvents Separador As System.Windows.Forms.ToolStripSeparator
  Friend WithEvents btnAceptar As System.Windows.Forms.ToolStripButton
  Friend WithEvents groupElegir As ComponentFactory.Krypton.Toolkit.KryptonGroup
  Friend WithEvents btnCerrar As ComponentFactory.Krypton.Toolkit.KryptonButton
  Friend WithEvents btnOperacion As ComponentFactory.Krypton.Toolkit.KryptonButton
  Friend WithEvents rbtnOperacion As ComponentFactory.Krypton.Toolkit.KryptonRadioButton
  Friend WithEvents txtOperacion As ComponentFactory.Krypton.Toolkit.KryptonTextBox
  Friend WithEvents rbtnGuiaTransporte As ComponentFactory.Krypton.Toolkit.KryptonRadioButton
  Friend WithEvents panelOpcion As ComponentFactory.Krypton.Toolkit.KryptonPanel
End Class
