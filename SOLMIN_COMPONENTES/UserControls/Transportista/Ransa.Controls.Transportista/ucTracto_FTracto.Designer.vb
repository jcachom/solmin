<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucTracto_FTracto
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
        Me.KryptonPanel = New ComponentFactory.Krypton.Toolkit.KryptonPanel
        Me.txtMarcaModelo = New ComponentFactory.Krypton.Toolkit.KryptonTextBox
        Me.chkEnLaCadena = New ComponentFactory.Krypton.Toolkit.KryptonCheckBox
        Me.dgTracto = New Ransa.Controls.Transportista.ucTracto_DgF01
        Me.btnSalir = New ComponentFactory.Krypton.Toolkit.KryptonButton
        Me.txtNPLCUN = New ComponentFactory.Krypton.Toolkit.KryptonTextBox
        Me.chkMientrasEscribe = New ComponentFactory.Krypton.Toolkit.KryptonCheckBox
        Me.lblNPLCUN = New ComponentFactory.Krypton.Toolkit.KryptonLabel
        Me.txtNroMTC = New ComponentFactory.Krypton.Toolkit.KryptonTextBox
        Me.lblMarcaModelo = New ComponentFactory.Krypton.Toolkit.KryptonLabel
        Me.lblNroMTC = New ComponentFactory.Krypton.Toolkit.KryptonLabel
        Me.KryptonManager = New ComponentFactory.Krypton.Toolkit.KryptonManager(Me.components)
        Me.TypeValidator = New TypeValidatorKrypton.Controls.TypeValidatorKrypton.TypeValidator
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'KryptonPanel
        '
        Me.KryptonPanel.Controls.Add(Me.txtMarcaModelo)
        Me.KryptonPanel.Controls.Add(Me.chkEnLaCadena)
        Me.KryptonPanel.Controls.Add(Me.dgTracto)
        Me.KryptonPanel.Controls.Add(Me.btnSalir)
        Me.KryptonPanel.Controls.Add(Me.txtNPLCUN)
        Me.KryptonPanel.Controls.Add(Me.chkMientrasEscribe)
        Me.KryptonPanel.Controls.Add(Me.lblNPLCUN)
        Me.KryptonPanel.Controls.Add(Me.txtNroMTC)
        Me.KryptonPanel.Controls.Add(Me.lblMarcaModelo)
        Me.KryptonPanel.Controls.Add(Me.lblNroMTC)
        Me.KryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonPanel.Location = New System.Drawing.Point(0, 0)
        Me.KryptonPanel.Name = "KryptonPanel"
        Me.KryptonPanel.Size = New System.Drawing.Size(496, 377)
        Me.KryptonPanel.StateCommon.Color1 = System.Drawing.Color.White
        Me.KryptonPanel.TabIndex = 0
        '
        'txtMarcaModelo
        '
        Me.txtMarcaModelo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMarcaModelo.CausesValidation = False
        Me.txtMarcaModelo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TypeValidator.SetEnterTAB(Me.txtMarcaModelo, True)
        Me.txtMarcaModelo.Location = New System.Drawing.Point(89, 34)
        Me.txtMarcaModelo.Name = "txtMarcaModelo"
        Me.txtMarcaModelo.Size = New System.Drawing.Size(395, 19)
        Me.txtMarcaModelo.TabIndex = 4
        Me.TypeValidator.SetTypes(Me.txtMarcaModelo, TypeValidatorKrypton.Controls.TypeValidatorKrypton.TY.Normal)
        '
        'chkEnLaCadena
        '
        Me.chkEnLaCadena.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnLaCadena.Location = New System.Drawing.Point(89, 88)
        Me.chkEnLaCadena.Name = "chkEnLaCadena"
        Me.chkEnLaCadena.Size = New System.Drawing.Size(89, 16)
        Me.chkEnLaCadena.TabIndex = 7
        Me.chkEnLaCadena.TabStop = False
        Me.chkEnLaCadena.Text = "En la cadena"
        Me.chkEnLaCadena.Values.ExtraText = ""
        Me.chkEnLaCadena.Values.Image = Nothing
        Me.chkEnLaCadena.Values.Text = "En la cadena"
        '
        'dgTracto
        '
        Me.dgTracto.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgTracto.BackColor = System.Drawing.Color.Transparent
        Me.dgTracto.Location = New System.Drawing.Point(0, 115)
        Me.dgTracto.Name = "dgTracto"
        Me.dgTracto.pPermitirSalirDoubleClick = False
        Me.dgTracto.Size = New System.Drawing.Size(496, 262)
        Me.dgTracto.TabIndex = 8
        '
        'btnSalir
        '
        Me.btnSalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.Location = New System.Drawing.Point(425, 84)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(59, 25)
        Me.btnSalir.TabIndex = 9
        Me.btnSalir.Text = "&Cerrar"
        Me.btnSalir.Values.ExtraText = ""
        Me.btnSalir.Values.Image = Nothing
        Me.btnSalir.Values.ImageStates.ImageCheckedNormal = Nothing
        Me.btnSalir.Values.ImageStates.ImageCheckedPressed = Nothing
        Me.btnSalir.Values.ImageStates.ImageCheckedTracking = Nothing
        Me.btnSalir.Values.Text = "&Cerrar"
        '
        'txtNPLCUN
        '
        Me.txtNPLCUN.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNPLCUN.CausesValidation = False
        Me.txtNPLCUN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TypeValidator.SetEnterTAB(Me.txtNPLCUN, True)
        Me.txtNPLCUN.Location = New System.Drawing.Point(89, 9)
        Me.txtNPLCUN.Name = "txtNPLCUN"
        Me.txtNPLCUN.Size = New System.Drawing.Size(395, 19)
        Me.txtNPLCUN.TabIndex = 2
        Me.TypeValidator.SetTypes(Me.txtNPLCUN, TypeValidatorKrypton.Controls.TypeValidatorKrypton.TY.Normal)
        '
        'chkMientrasEscribe
        '
        Me.chkMientrasEscribe.Checked = False
        Me.chkMientrasEscribe.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.chkMientrasEscribe.Location = New System.Drawing.Point(184, 88)
        Me.chkMientrasEscribe.Name = "chkMientrasEscribe"
        Me.chkMientrasEscribe.Size = New System.Drawing.Size(121, 16)
        Me.chkMientrasEscribe.TabIndex = 7
        Me.chkMientrasEscribe.TabStop = False
        Me.chkMientrasEscribe.Text = "Mientras se escribe"
        Me.chkMientrasEscribe.Values.ExtraText = ""
        Me.chkMientrasEscribe.Values.Image = Nothing
        Me.chkMientrasEscribe.Values.Text = "Mientras se escribe"
        '
        'lblNPLCUN
        '
        Me.lblNPLCUN.Location = New System.Drawing.Point(3, 12)
        Me.lblNPLCUN.Name = "lblNPLCUN"
        Me.lblNPLCUN.Size = New System.Drawing.Size(48, 16)
        Me.lblNPLCUN.StateCommon.ShortText.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPLCUN.TabIndex = 1
        Me.lblNPLCUN.Text = "PLACA : "
        Me.lblNPLCUN.Values.ExtraText = ""
        Me.lblNPLCUN.Values.Image = Nothing
        Me.lblNPLCUN.Values.Text = "PLACA : "
        '
        'txtNroMTC
        '
        Me.txtNroMTC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNroMTC.CausesValidation = False
        Me.txtNroMTC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TypeValidator.SetEnterTAB(Me.txtNroMTC, True)
        Me.txtNroMTC.Location = New System.Drawing.Point(89, 59)
        Me.txtNroMTC.Name = "txtNroMTC"
        Me.txtNroMTC.Size = New System.Drawing.Size(395, 19)
        Me.txtNroMTC.TabIndex = 6
        Me.TypeValidator.SetTypes(Me.txtNroMTC, TypeValidatorKrypton.Controls.TypeValidatorKrypton.TY.Normal)
        '
        'lblMarcaModelo
        '
        Me.lblMarcaModelo.Location = New System.Drawing.Point(3, 37)
        Me.lblMarcaModelo.Name = "lblMarcaModelo"
        Me.lblMarcaModelo.Size = New System.Drawing.Size(57, 16)
        Me.lblMarcaModelo.StateCommon.ShortText.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMarcaModelo.TabIndex = 3
        Me.lblMarcaModelo.Text = "MODELO :"
        Me.lblMarcaModelo.Values.ExtraText = ""
        Me.lblMarcaModelo.Values.Image = Nothing
        Me.lblMarcaModelo.Values.Text = "MODELO :"
        '
        'lblNroMTC
        '
        Me.lblNroMTC.Location = New System.Drawing.Point(3, 62)
        Me.lblNroMTC.Name = "lblNroMTC"
        Me.lblNroMTC.Size = New System.Drawing.Size(57, 16)
        Me.lblNroMTC.StateCommon.ShortText.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblNroMTC.TabIndex = 5
        Me.lblNroMTC.Text = "Nro. MTC : "
        Me.lblNroMTC.Values.ExtraText = ""
        Me.lblNroMTC.Values.Image = Nothing
        Me.lblNroMTC.Values.Text = "Nro. MTC : "
        '
        'ucTracto_FTracto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(496, 377)
        Me.ControlBox = False
        Me.Controls.Add(Me.KryptonPanel)
        Me.Name = "ucTracto_FTracto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buscar:"
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel.ResumeLayout(False)
        Me.KryptonPanel.PerformLayout()
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
    Private WithEvents TypeValidator As TypeValidatorKrypton.Controls.TypeValidatorKrypton.TypeValidator
    Private WithEvents btnSalir As ComponentFactory.Krypton.Toolkit.KryptonButton
    Private WithEvents txtNPLCUN As ComponentFactory.Krypton.Toolkit.KryptonTextBox
    Private WithEvents chkMientrasEscribe As ComponentFactory.Krypton.Toolkit.KryptonCheckBox
    Private WithEvents lblNPLCUN As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Private WithEvents txtNroMTC As ComponentFactory.Krypton.Toolkit.KryptonTextBox
    Private WithEvents lblMarcaModelo As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Private WithEvents lblNroMTC As ComponentFactory.Krypton.Toolkit.KryptonLabel
    Private WithEvents txtMarcaModelo As ComponentFactory.Krypton.Toolkit.KryptonTextBox
    Private WithEvents dgTracto As Ransa.Controls.Transportista.ucTracto_DgF01
    Private WithEvents chkEnLaCadena As ComponentFactory.Krypton.Toolkit.KryptonCheckBox
End Class
