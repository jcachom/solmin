<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSolicitudTransporteReqto
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
        Me.UcSolicitudTransportes1 = New SOLMIN_ST.ucSolicitudTransportes
        Me.SuspendLayout()
        '
        'UcSolicitudTransportes1
        '
        Me.UcSolicitudTransportes1.BackColor = System.Drawing.Color.White
        Me.UcSolicitudTransportes1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcSolicitudTransportes1.Location = New System.Drawing.Point(0, 0)
        Me.UcSolicitudTransportes1.Name = "UcSolicitudTransportes1"
        Me.UcSolicitudTransportes1.pEsRequerimiento = True
        Me.UcSolicitudTransportes1.pNombreForm = ""
        Me.UcSolicitudTransportes1.pRequerimientoServicio = Nothing
        Me.UcSolicitudTransportes1.Size = New System.Drawing.Size(1146, 728)
        Me.UcSolicitudTransportes1.TabIndex = 0
        '
        'frmSolicitudTransporteReqto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1146, 728)
        Me.Controls.Add(Me.UcSolicitudTransportes1)
        Me.Name = "frmSolicitudTransporteReqto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Solicitud Transporte por Requerimiento"
        Me.ResumeLayout(False)

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents UcSolicitudTransportes1 As SOLMIN_ST.ucSolicitudTransportes
End Class
