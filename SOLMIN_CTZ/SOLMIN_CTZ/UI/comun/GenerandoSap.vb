Imports System.Windows.Forms
Public Class GenerandoSap
    Private Sub GenerandoSap_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        Me.pgbSAP.Step = 10
        Me.pgbSAP.Maximum = 30
        Me.pgbSAP.Style = ProgressBarStyle.Marquee
        Me.pgbSAP.MarqueeAnimationSpeed = 60

    End Sub

    Public Property Texto() As String
        Get
            Return Me.lblTexto.Text
        End Get
        Set(ByVal value As String)
            Me.lblTexto.Text = value
        End Set
    End Property

End Class