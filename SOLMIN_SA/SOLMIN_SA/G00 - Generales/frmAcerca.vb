Public NotInheritable Class frmAcerca
	Private Sub frmAcerca_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		' Establezca el t?tulo del formulario.
		Dim ApplicationTitle As String
		If My.Application.Info.Title <> "" Then
			ApplicationTitle = My.Application.Info.Title
		Else
			ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
		End If
		Me.Text = String.Format("Acerca de {0}", ApplicationTitle)
		' Inicialice el texto mostrado en el cuadro Acerca de.
		' TODO: Personalice la informaci?n del ensamblado de la aplicaci?n en el panel "Aplicaci?n" del 
		'    cuadro de di?logo propiedades del proyecto (bajo el men? "Proyecto").
		Me.LabelProductName.Text = My.Application.Info.ProductName.ToUpper
		Me.LabelVersion.Text = String.Format("Versi?n {0}", My.Application.Info.Version.ToString)
		Me.LabelCopyright.Text = My.Application.Info.Copyright
		Me.LabelCompanyName.Text = My.Application.Info.CompanyName
		Me.TextBoxDescription.Text = My.Application.Info.Description
	End Sub

	Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
		Me.Close()
	End Sub
End Class
