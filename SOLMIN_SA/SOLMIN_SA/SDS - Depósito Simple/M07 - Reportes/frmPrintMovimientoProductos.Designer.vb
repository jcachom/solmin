<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintMovimientoProductos
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
        Me.crvReporte = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.KryptonManager = New ComponentFactory.Krypton.Toolkit.KryptonManager(Me.components)
        Me.bgwGifAnimado = New System.ComponentModel.BackgroundWorker
        CType(Me.KryptonPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.KryptonPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'KryptonPanel
        '
        Me.KryptonPanel.Controls.Add(Me.crvReporte)
        Me.KryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KryptonPanel.Location = New System.Drawing.Point(0, 0)
        Me.KryptonPanel.Name = "KryptonPanel"
        Me.KryptonPanel.Size = New System.Drawing.Size(1028, 608)
        Me.KryptonPanel.TabIndex = 0
        '
        'crvReporte
        '
        Me.crvReporte.AccessibleDescription = "<"
        Me.crvReporte.ActiveViewIndex = -1
        Me.crvReporte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.crvReporte.DisplayGroupTree = False
        Me.crvReporte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvReporte.Location = New System.Drawing.Point(0, 0)
        Me.crvReporte.Name = "crvReporte"
        Me.crvReporte.SelectionFormula = ""
        Me.crvReporte.ShowCloseButton = False
        Me.crvReporte.ShowGotoPageButton = False
        Me.crvReporte.ShowGroupTreeButton = False
        Me.crvReporte.ShowRefreshButton = False
        Me.crvReporte.ShowTextSearchButton = False
        Me.crvReporte.Size = New System.Drawing.Size(1028, 608)
        Me.crvReporte.TabIndex = 56
        Me.crvReporte.ViewTimeSelectionFormula = ""
        Me.crvReporte.Visible = False
        '
        'frmPrintMovimientoProductos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 608)
        Me.Controls.Add(Me.KryptonPanel)
        Me.Name = "frmPrintMovimientoProductos"
        Me.Text = "frmPrintMovimientoProductos"
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
    Friend WithEvents crvReporte As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents bgwGifAnimado As System.ComponentModel.BackgroundWorker
End Class
