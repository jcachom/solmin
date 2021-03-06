Imports System.Text

Public Class frmOpcionFactura

#Region "Atributos"

  Private intEstado As Int32 = 0
  Private strCompania As String = ""
  Private strDivision As String = ""
  Private decPlanta As Decimal = 0
  Private odtPlantaZonaFacturacion As New DataTable
  Private boolCargado As Boolean = False
    Private iCodigoMoneda As Integer = 0
    Private _EsPT As Boolean = False
    'Private CPLNDVFactura As Decimal = 0
    'Public listaoOperaciones As String = ""
#End Region

#Region "Properties"

  Public WriteOnly Property Estado() As Int32
    Set(ByVal value As Int32)
      intEstado = value
    End Set
  End Property

  Public WriteOnly Property Compania() As String
    Set(ByVal value As String)
      strCompania = value
    End Set
  End Property
  Public WriteOnly Property Division() As String
    Set(ByVal value As String)
      strDivision = value
    End Set
  End Property
  Public WriteOnly Property CodigoMoneda() As Integer
    Set(ByVal value As Integer)
      iCodigoMoneda = value
    End Set
  End Property



    Public Property Planta() As Decimal
        Get
            Return decPlanta
        End Get
        Set(ByVal value As Decimal)
            decPlanta = value
        End Set
    End Property


    'Public Property CPLNDV_Factura() As Decimal
    '    Get
    '        Return CPLNDVFactura
    '    End Get
    '    Set(ByVal value As Decimal)
    '        CPLNDVFactura = value
    '    End Set
    'End Property

    Public Property EsPT() As Boolean
        Get
            Return _EsPT
        End Get
        Set(ByVal value As Boolean)
            _EsPT = value
        End Set
    End Property


#End Region

#Region "Eventos"

  Private Sub frmOpcionFactura_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim objPlantaFacturacion As New NEGOCIO.PlantaFacturacion_BLL
    Try
      If intEstado <> 0 Then
        Me.rbtnPreFactura.Checked = False
        Me.rbtnFactura.Checked = True
                Me.gpbTipoDocumento.Enabled = False
      End If
      Select Case iCodigoMoneda
        Case 1
          'Me.gpbMoneda.Enabled = False
                    Me.rbtnDolares.Checked = False
                    Me.rbtnSoles.Checked = True
                Case 100
                    'Me.gpbMoneda.Enabled = False
                    Me.rbtnDolares.Checked = True
                    Me.rbtnSoles.Checked = False

            End Select
      Dim dtFechaActual As Date
      Dim oFacturaNeg As New NEGOCIO.Operaciones.Factura_Transporte_BLL
      dtFechaActual = oFacturaNeg.Obtener_Fecha_Servidor
            'Me.dtpFechaFactura.Value = dtFechaActual
            'Me.dtpFechaServicio.Value = dtFechaActual
            'Select Case dtFechaActual.Day
            '  Case 1, 2
            '    Me.lblFechaFactura.Visible = True
            '    Me.dtpFechaFactura.Visible = True
            'End Select
            'Select Case dtFechaActual.Month
            '  Case 4
            '    Me.chkFechaServicio.Visible = True
            '    Me.dtpFechaServicio.Visible = True
            'End Select

      Me.CargarPlanta()
            Me.Cargar_Zona_Facturaci?n()
           
            Me.Cargar_Tipo_Factura()


            boolCargado = True
            odtPlantaZonaFacturacion = objPlantaFacturacion.Listar_Planta_Zona_Facturacion(strCompania, strDivision)
            ListaZonaFacturacionPorDefecto(cmbPlanta.Planta)

            '_EsPT = False

            If (_EsPT = True) Then
                ucAprobador.Enabled = True
                ' chkAprobacion.Enabled = True
                Me.CargarAprobadores(strCompania)
            Else
                ucAprobador.Enabled = False 'CSR-HUNDRED-SGR-DEF-SALMON-ST-FASE2-VENTA-INTERNA-PT
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
  End Sub

  Private Sub ListaZonaFacturacionPorDefecto(ByVal Planta As Decimal)
    Dim CZNFCC As Decimal = 0
    Dim drPlanta() As DataRow
    drPlanta = odtPlantaZonaFacturacion.Select("CPLNDV=" & Planta)
    If (drPlanta.Length > 0) Then
      CZNFCC = drPlanta(0)("CZNFCC")
      cboZonaFacturacion.Codigo = CZNFCC
    Else
      If (strCompania = "EZ") Then
        cboZonaFacturacion.Codigo = "32"
      Else
        cboZonaFacturacion.Limpiar()
      End If
    End If
  End Sub

  Private Sub CargarPlanta()
    cmbPlanta.Usuario = MainModule.USUARIO
    cmbPlanta.CodigoCompania = strCompania
        cmbPlanta.CodigoDivision = strDivision

        If decPlanta > 0 Then
            cmbPlanta.PlantaDefault = decPlanta
        Else
            cmbPlanta.PlantaDefault = 1
        End If

        cmbPlanta.pActualizar()

    End Sub

  Private Sub rbtnFactura_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnFactura.CheckedChanged
    Try
      If Me.rbtnFactura.Checked = True Then
        Me.gpbMoneda.Enabled = True
        Me.txtPreFactura.Text = ""
        Me.cboZonaFacturacion.Enabled = True
      Else
        Me.gpbMoneda.Enabled = False
        Me.txtPreFactura.Text = Me.txtPreFactura.Tag
        Me.cboZonaFacturacion.Enabled = False
      End If
    Catch ex As Exception
      MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub

  Private Function ValidarIngreso() As String
    Dim msgValidacion As New StringBuilder
        If cboZonaFacturacion.NoHayCodigo = True Then
            msgValidacion.Append("Selecione Zona Facturaci?n")
        End If

        ''<DESCOMNTAR CUANDO SE IMPLEMENTE VENTA INTERNA>
        'If (chkAprobacion.Checked And ucAprobador.Resultado Is Nothing) Then
        '    msgValidacion.Append("Selecione un Aprobador")
        'End If
        '</>
      
        If (_EsPT = True) And (Me.rbtnFactura.Checked) = True Then 'CSR-HUNDRED-CORRECTIVO-VENTA INTERNA
            If (ucAprobador.Resultado Is Nothing) Then
                msgValidacion.Append("Selecione un Aprobador")
            End If
        End If

        Return msgValidacion.ToString.Trim
  End Function

  Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
    Dim msgValidacion As String
    msgValidacion = ValidarIngreso()
    If (msgValidacion.Length <> 0) Then
      MessageBox.Show(msgValidacion, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Exit Sub
        End If
        'If rbtnPreFactura.Checked = True Then
        '    Dim CantOperaciones As Int32 = 0
        '    CantOperaciones = listaoOperaciones.Split(",").Length
        '    If CantOperaciones > 1 Then
        '        MessageBox.Show("Para generar una operaci?n debe seleccionar una Operaci?n.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        Exit Sub
        '    End If
        'End If
        'Me.Tag = Me.cboTipoFactura.ComboBox.SelectedIndex
        'Me.Tag = Me.cboTipoFactura.SelectedIndex
        Me.Tag = Me.cboTipoFactura.SelectedValue
        decPlanta = cmbPlanta.Planta
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

  Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
    Me.DialogResult = Windows.Forms.DialogResult.Cancel
  End Sub

    'Private Sub chkFechaServicio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.dtpFechaServicio.Enabled = Me.chkFechaServicio.Checked
    'End Sub

    Private Sub cmbPlanta_Seleccionar(ByVal obePlanta As Ransa.Controls.UbicacionPlanta.Planta.bePlanta) Handles cmbPlanta.Seleccionar
        Try
            If (boolCargado = True) Then
                ListaZonaFacturacionPorDefecto(cmbPlanta.Planta)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "M?todos"
  Private Sub Cargar_Zona_Facturaci?n()
    Dim objPlantaFacturacion As New NEGOCIO.PlantaFacturacion_BLL
    Try
      With Me.cboZonaFacturacion
        .DataSource = objPlantaFacturacion.Listar_Planta_Facturacion_Combo(strCompania)
        .KeyField = "CZNFCC"
        .ValueField = "TZNFCC"
        .DataBind()
      End With
    Catch ex As Exception
      MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try

  End Sub

    'Private Sub Cargar_Tipo_Factura()
    '  Me.cboTipoFactura.ComboBox.DisplayMember = "ESTADO"
    '  Me.cboTipoFactura.ComboBox.ValueMember = "VALUE"
    '  Me.cboTipoFactura.ComboBox.DataSource = Tipo_Factura()
    'End Sub
    Private Sub Cargar_Tipo_Factura()
        Me.cboTipoFactura.DisplayMember = "ESTADO"
        Me.cboTipoFactura.ValueMember = "VALUE"
        If EsPT = True Then
            Me.cboTipoFactura.DataSource = Tipo_Factura_PT()
            Me.cboTipoFactura.SelectedValue = 0 '2 'INI-ECM-ActualizacionTarifario[RF002]-160516
        Else
            Me.cboTipoFactura.DataSource = Tipo_Factura()
            cboTipoFactura.SelectedValue = 0
        End If


    End Sub

    Private Function Tipo_Factura() As DataTable
        Dim odt As New DataTable
        odt.TableName = "dtListaTipoFactura"
        Dim oDr As DataRow
        odt.Columns.Add("VALUE", Type.GetType("System.Int32"))
        odt.Columns.Add("ESTADO", Type.GetType("System.String"))

        oDr = odt.NewRow
        oDr.Item("VALUE") = 0
        oDr.Item("ESTADO") = "NORMAL"
        odt.Rows.Add(oDr)

        oDr = odt.NewRow
        oDr.Item("VALUE") = 1
        oDr.Item("ESTADO") = "RESUMIDA"
        odt.Rows.Add(oDr)

        oDr = odt.NewRow
        oDr.Item("VALUE") = 2
        oDr.Item("ESTADO") = "DETALLADA"
        odt.Rows.Add(oDr)


        'oDr = odt.NewRow
        'oDr.Item("VALUE") = 3
        'oDr.Item("ESTADO") = "LOTE"
        'odt.Rows.Add(oDr)

        Return odt

    End Function

    Private Function Tipo_Factura_PT() As DataTable
        Dim odt As New DataTable
        odt.TableName = "dtListaTipoFactura"
        Dim oDr As DataRow
        odt.Columns.Add("VALUE", Type.GetType("System.Int32"))
        odt.Columns.Add("ESTADO", Type.GetType("System.String"))

        oDr = odt.NewRow
        oDr.Item("VALUE") = 0
        oDr.Item("ESTADO") = "NORMAL"
        odt.Rows.Add(oDr)
        'INI-ECM-ActualizacionTarifario[RF002]-160516
        'oDr = odt.NewRow
        'oDr.Item("VALUE") = 1
        'oDr.Item("ESTADO") = "RESUMIDA"
        'odt.Rows.Add(oDr)

        'oDr = odt.NewRow
        'oDr.Item("VALUE") = 2
        'oDr.Item("ESTADO") = "DETALLADA"
        'odt.Rows.Add(oDr)
        'FIN-ECM-ActualizacionTarifario[RF002]-160516
        Return odt

    End Function

    Private Sub CargarAprobadores(ByVal strCompania As String)
        'CSR-HUNDRED-SGR-DEF-SALMON-ST-FASE2-VENTA-INTERNA-PT-INICIO
        Dim oListColum As New Hashtable
        Dim Etiquetas As New List(Of String)
        Dim objAprobador As New NEGOCIO.PlantaFacturacion_BLL
        Dim Aprobador As New List(Of ENTIDADES.mantenimientos.Aprobadores)

        Dim oColumnas As New DataGridViewTextBoxColumn
        oColumnas.Name = "USRCCO"
        oColumnas.DataPropertyName = "USRCCO"
        oColumnas.HeaderText = "USUARIO"
        oListColum.Add(1, oColumnas)

        oColumnas = New DataGridViewTextBoxColumn
        oColumnas.Name = "NOMBRE"
        oColumnas.DataPropertyName = "NOMBRE"
        oColumnas.HeaderText = "NOMBRE"
        oListColum.Add(2, oColumnas)

        ' ucAprobador.Etiquetas_Form = Etiquetas
        Aprobador = objAprobador.ListarAprobador(strCompania)

        ucAprobador.DataSource = Aprobador
        ucAprobador.ListColumnas = oListColum
        ucAprobador.Cargas()
        ucAprobador.Limpiar()
        ucAprobador.ValueMember = "USRCCO"
        ucAprobador.DispleyMember = "NOMBRE"
        'CSR-HUNDRED-SGR-DEF-SALMON-ST-FASE2-VENTA-INTERNA-PT-FIN
    End Sub
#End Region
 
End Class