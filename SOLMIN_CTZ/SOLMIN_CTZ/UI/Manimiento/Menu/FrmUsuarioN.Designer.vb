<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUsuarioN
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
        Me.UcUsuarioBusq = New Ransa.Controls.Menu.ucUsuarioBusq
        Me.KryptonManager = New ComponentFactory.Krypton.Toolkit.KryptonManager(Me.components)
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'KryptonPanel
        '
        Me.KryptonPanel.Controls.Add(Me.UcUsuarioBusq)
        Me.KryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonPanel.Location = New System.Drawing.Point(0, 0)
        Me.KryptonPanel.Name = "KryptonPanel"
        Me.KryptonPanel.Size = New System.Drawing.Size(752, 603)
        Me.KryptonPanel.TabIndex = 0
        '
        'UcUsuarioBusq
        '
        Me.UcUsuarioBusq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcUsuarioBusq.Location = New System.Drawing.Point(0, 0)
        Me.UcUsuarioBusq.Name = "UcUsuarioBusq"
        Me.UcUsuarioBusq.pInfo_MMCUSR = ""
        Me.UcUsuarioBusq.pInfo_MMNUSR = ""
        Me.UcUsuarioBusq.pInfo_SESTRG = ""
        Me.UcUsuarioBusq.pnumReg = CType(0, Long)
        Me.UcUsuarioBusq.pVerBotonBuscar = True
        Me.UcUsuarioBusq.pVerBotonEliminar = True
        Me.UcUsuarioBusq.pVerBotonExportar = True
        Me.UcUsuarioBusq.pVerBotonModificar = True
        Me.UcUsuarioBusq.pVerBotonNuevo = True
        Me.UcUsuarioBusq.Size = New System.Drawing.Size(752, 603)
        Me.UcUsuarioBusq.TabIndex = 0
        '
        'FrmUsuarioN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(752, 603)
        Me.Controls.Add(Me.KryptonPanel)
        Me.Name = "FrmUsuarioN"
        Me.Text = "Usuario"
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.KryptonPanel.ResumeLayout(False)
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
    Friend WithEvents UcUsuarioBusq As Ransa.Controls.Menu.ucUsuarioBusq
End Class
