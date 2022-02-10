
'Imports SOLMIN_ST.ENTIDADES.Operaciones
'Imports SOLMIN_ST.ENTIDADES.mantenimientos
'Imports SOLMIN_ST.NEGOCIO
'Imports SOLMIN_ST.NEGOCIO.Operaciones
'Imports SOLMIN_ST.NEGOCIO.mantenimientos
'Imports SOLMIN_ST.ENTIDADES
Imports Ransa.Utilitario
Imports System.Threading
Imports System.Windows.Forms
Imports System.Text

Public Class ucSolicitudRequerimiento

    Private _pSystem_prefix As String = ""
    Public Property pSystem_prefix() As String
        Get
            Return _pSystem_prefix
        End Get
        Set(ByVal value As String)
            _pSystem_prefix = value
        End Set
    End Property

    Private _pMailAccount As String = ""
    Public Property pMailAccount() As String
        Get
            Return _pMailAccount
        End Get
        Set(ByVal value As String)
            _pMailAccount = value
        End Set
    End Property

    Private _pMailpassword As String = ""
    Public Property pMailpassword() As String
        Get
            Return _pMailpassword
        End Get
        Set(ByVal value As String)
            _pMailpassword = value
        End Set
    End Property

    Private _pMailAccount_Gmail As String = ""
    Public Property pMailAccount_Gmail() As String
        Get
            Return _pMailAccount_Gmail
        End Get
        Set(ByVal value As String)
            _pMailAccount_Gmail = value
        End Set
    End Property

    Private _pMailPassword_Gmail As String = ""
    Public Property pMailPassword_Gmail() As String
        Get
            Return _pMailPassword_Gmail
        End Get
        Set(ByVal value As String)
            _pMailPassword_Gmail = value
        End Set
    End Property

    Private _pMailto_Error As String = ""
    Public Property pMailto_Error() As String
        Get
            Return _pMailto_Error
        End Get
        Set(ByVal value As String)
            _pMailto_Error = value
        End Set
    End Property

    Private _pUsuario As String = ""
    Public Property pUsuario() As String
        Get
            Return _pUsuario
        End Get
        Set(ByVal value As String)
            _pUsuario = value
        End Set
    End Property

    Private _pNameForm As String = ""
    Public Property pNameForm() As String
        Get
            Return _pNameForm
        End Get
        Set(ByVal value As String)
            _pNameForm = value
        End Set
    End Property

    Private gEnumOpc As EnumMan = EnumMan.Neutro
    Enum EnumMan
        Neutro
        Nuevo
        Editar
    End Enum
    Private TabSeleccionado As String = ""
    Private oHebraReqServ As Thread
    Dim dtNegocio As DataTable
    Private oHebraEnvioEmailReqServ As Thread

    'oHebraCheckPointYRC_ITEM_EMB_CHK_EMB = New Thread(AddressOf Enviar_Items_Embarcador_CheckPoint)
    '                  oHebraCheckPointYRC_ITEM_EMB_CHK_EMB.Start()
    Sub Activated()
        Try

            'Me.KryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2007Blue

            If dgvDatos.CurrentRow IsNot Nothing Then
                ''dgvPreAsignacion.DataSource = Nothing
                dgvDatosSolicitud.DataSource = Nothing
                ''Listar_Unidades_Programadas()
                'Listar_Solicitudes()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private isCheckRequerimiento As Boolean = False

    Sub pInicializar()

        Try

            Me.KryptonManager.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2007Blue

            TabSeleccionado = TabRequerimiento.SelectedTab.Name

            gEnumOpc = EnumMan.Neutro
            HabilitarOpcion(EnumMan.Neutro, TabRequerimiento.SelectedTab.Name)
            Estado_Controles(False)

            dgvDatos.AutoGenerateColumns = False
            dgvDatosSolicitud.AutoGenerateColumns = False
            ''dgvPreAsignacion.AutoGenerateColumns = False
            dgvDimensiones.AutoGenerateColumns = False
            Cargar_Compania()
            txtClienteFiltro.CCMPN = cmbCompania.CCMPN_CodigoCompania
            Cargar_Estado()
            Cargar_Prioridad()
            'Cargar_Prioridad_Filtro()

            Lista_Tipo_Mercaderia()
            Carga_MedioTransporte()

            dtpHoraIniReq.Enabled = False
            dtpHoraFinReq.Enabled = False
            dtpHoraIniAtencion.Enabled = False
            dtpHoraFinAtencion.Enabled = False
            txtNroReqFiltro.Enabled = False
            chkFechaAtencion.Checked = False
            chkHora.Checked = False
            chkHoraAtencion.Checked = False

            chkSolicitud.Checked = False
            txtSolicitudFiltro.Enabled = False

            'Habilitar_Botones(Estado)
            'Habilitar_Controles(Estado)

            Validar_Acceso_Opciones_Usuario()

            chkFechaRequerimiento.Checked = True
            chkFechaRequerimiento_CheckedChanged(Nothing, Nothing)

            'Validad_Botones(False)

            cargarComboNegocio()
            Cargar_Responsable()

            TabRequerimiento.TabPages.Remove(TabSolicitud)
            TabRequerimiento.TabPages.Remove(TabUnidadesProgramadas)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Private Sub cargarComboNegocio()


        Dim objBE As New RequerimientoServicio
        Dim objBLL As New RequerimientoServicio_BLL

        Dim dtNegocio_data As New DataTable
        Dim dr As DataRow

        Dim Lista1 As New List(Of RequerimientoServicio)
        Dim entidad1 As RequerimientoServicio
        Dim Lista2 As New List(Of RequerimientoServicio)
        Dim entidad2 As New RequerimientoServicio

        dtNegocio_data = objBLL.Lista_Negocio(cmbCompania.CCMPN_CodigoCompania)

        dr = dtNegocio_data.NewRow
        dr("CCMPN") = "EZ"
        dr("CRGVTA") = "0"
        dr("TCRVTA") = "TODOS"
        dtNegocio_data.Rows.InsertAt(dr, 0)

        For Each item As DataRow In dtNegocio_data.Rows

            entidad1 = New RequerimientoServicio
            entidad1.CRGVTA = item("CRGVTA")
            entidad1.TCRVTA = item("TCRVTA")
            Lista1.Add(entidad1)
        Next

        cmbNegocio.DisplayMember = "TCRVTA"
        cmbNegocio.ValueMember = "CRGVTA"
        Me.cmbNegocio.DataSource = Lista1

        For i As Integer = 0 To cmbNegocio.Items.Count - 1
            If cmbNegocio.Items.Item(i).Value = "0" Then
                cmbNegocio.SetItemChecked(i, True)
            End If
        Next

        If cmbNegocio.Text = "" Then
            cmbNegocio.SetItemChecked(1, True)
        End If

        dtNegocio = New DataTable
        Dim dr1 As DataRow

        dtNegocio.Columns.Add("CRGVTA_Codigo")
        dtNegocio.Columns.Add("TCRVTA_Descripcion")

        For Each item As DataRow In dtNegocio_data.Rows
            dr1 = dtNegocio.NewRow
            dr1("CRGVTA_Codigo") = item("CRGVTA")
            dr1("TCRVTA_Descripcion") = item("TCRVTA")
            dtNegocio.Rows.Add(dr1)
        Next


    End Sub

    Private Function Lista_Negocios() As String

        Dim strCadDocumentos As String = ""
        Dim valida As Boolean = False
        For i As Integer = 0 To cmbNegocio.CheckedItems.Count - 1
            For j As Integer = 0 To dtNegocio.Rows.Count - 1
                If (dtNegocio.Rows(j).Item("CRGVTA_Codigo") = cmbNegocio.CheckedItems(i).Value) Then
                    If (dtNegocio.Rows(j).Item("CRGVTA_Codigo") = "0") Then
                        valida = True
                    End If
                End If
            Next
        Next

        If valida = True Then
            For i As Integer = 0 To cmbNegocio.CheckedItems.Count - 1
                For j As Integer = 0 To dtNegocio.Rows.Count - 1
                    If (dtNegocio.Rows(j).Item("CRGVTA_Codigo") <> "0") Then
                        strCadDocumentos += dtNegocio.Rows(j).Item("CRGVTA_Codigo") & ","
                    End If
                Next
            Next
        Else
            For i As Integer = 0 To cmbNegocio.CheckedItems.Count - 1
                For j As Integer = 0 To dtNegocio.Rows.Count - 1
                    If (dtNegocio.Rows(j).Item("CRGVTA_Codigo") = cmbNegocio.CheckedItems(i).Value) Then
                        strCadDocumentos += dtNegocio.Rows(j).Item("CRGVTA_Codigo") & ","
                    End If
                Next
            Next
        End If

        If strCadDocumentos <> "" Then strCadDocumentos = strCadDocumentos.Trim.Substring(0, strCadDocumentos.Trim.Length - 1)

        'If strCadDocumentos = "" Then
        '    For j As Integer = 0 To dtNegocio.Rows.Count - 1
        '        If (dtNegocio.Rows(j).Item("CRGVTA_Codigo") <> "0") Then
        '            strCadDocumentos += dtNegocio.Rows(j).Item("CRGVTA_Codigo") & ","
        '        End If
        '    Next
        '    If strCadDocumentos <> "" Then strCadDocumentos = strCadDocumentos.Trim.Substring(0, strCadDocumentos.Trim.Length - 1)

        'End If

        Return strCadDocumentos

    End Function


    Private Sub Cargar_Responsable()
        Dim oListColum As New Hashtable
        Dim oColumnas As New DataGridViewTextBoxColumn
        Dim obrIncidencias As New RequerimientoServicio_BLL
        Dim obeResponsable As New beDestinatario
        Dim Etiquetas As New List(Of String)
        oListColum = New Hashtable
        oColumnas = New DataGridViewTextBoxColumn
        oColumnas.Name = "PSNCRRIT"
        oColumnas.DataPropertyName = "PSNCRRIT"
        oColumnas.HeaderText = "C�digo"
        oColumnas.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        oColumnas.Visible = False
        oListColum.Add(1, oColumnas)
        oColumnas = New DataGridViewTextBoxColumn
        oColumnas.Name = "PSTNMYAP"
        oColumnas.DataPropertyName = "PSTNMYAP"
        oColumnas.HeaderText = "Nombre"
        oColumnas.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        oListColum.Add(2, oColumnas)
        oColumnas = New DataGridViewTextBoxColumn
        oColumnas.Name = "PSEMAILTO"
        oColumnas.DataPropertyName = "PSEMAILTO"
        oColumnas.HeaderText = "Correo"
        oColumnas.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        oListColum.Add(3, oColumnas)

        With obeResponsable
            .PNCCLNT = 999999
            .PSTIPPROC = "ST_REQSERV"
        End With

        Etiquetas.Add("Nombre")
        Etiquetas.Add("Email")

        Me.ucResponsable.Etiquetas_Form = Etiquetas
        Me.ucResponsable.DataSource = obrIncidencias.olisListarResponsables(obeResponsable)
        Me.ucResponsable.ListColumnas = oListColum
        Me.ucResponsable.Cargas()
        Me.ucResponsable.DispleyMember = "PSEMAILTO"
        Me.ucResponsable.ValueMember = "PSTNMYAP"
    End Sub

    Private Sub Validar_Acceso_Opciones_Usuario()
        Dim objParametro As New Hashtable
        Dim objLogica As New Acceso_Opciones_Usuario_BLL
        Dim objEntidad As New mantenimientos.ClaseGeneral

        objParametro.Add("MMCAPL", _pSystem_prefix) ''MainModule.getAppSetting("System_prefix"))
        objParametro.Add("MMCUSR", _pUsuario)
        objParametro.Add("MMNPVB", _pNameForm)
        objEntidad = objLogica.Validar_Acceso_Opciones_Usuario(objParametro)

        If objEntidad.STSADI = "" Then
            Me.btnNuevo.Visible = False
            Me.btnMedida.Visible = False
        End If
        If objEntidad.STSCHG = "" Then
            Me.btnModificar.Visible = False
        End If
        If objEntidad.STSELI = "" Then
            Me.btnEliminar.Visible = False
        End If

        'If objEntidad.STSVIS = "" Then
        '    Me.btnCerrarReq.Visible = False
        'End If



    End Sub

    Private Sub Carga_MedioTransporte()
        Dim obj_Logica_MedioTransporte As New MedioTransporte_BLL
        Dim objTabla As DataTable = obj_Logica_MedioTransporte.Lista_MedioTrasnporteTabla(cmbCompania.CCMPN_CodigoCompania)
        Me.cboMedioTransporte.DataSource = objTabla.Copy
        Me.cboMedioTransporte.ValueMember = "CMEDTR"
        Me.cboMedioTransporte.DisplayMember = "TNMMDT"
    End Sub




    Sub Lista_Tipo_Mercaderia()

        Dim objBLL As New RequerimientoServicio_BLL
        Dim dtIncPara As New DataTable
        Dim dr As DataRow

        dtIncPara = objBLL.Lista_Tipo_Mercaderia

        dr = dtIncPara.NewRow
        dr("TIPOCNT") = 0
        dr("TIPOCNT_S") = ":: Seleccione ::"
        dtIncPara.Rows.InsertAt(dr, 0)

        cmbMercaderia.DataSource = dtIncPara
        cmbMercaderia.DisplayMember = "TIPOCNT_S"
        cmbMercaderia.ValueMember = "TIPOCNT"
        cmbMercaderia.SelectedValue = 0

    End Sub



    'Private Function TipoSolicitud()
    '    Dim oDt As New DataTable

    '    oDt.Columns.Add("COD")
    '    oDt.Columns.Add("NOM")

    '    Dim oDr As DataRow

    '    oDr = oDt.NewRow
    '    oDr.Item(0) = "I"
    '    oDr.Item(1) = "Ida"
    '    oDt.Rows.Add(oDr)

    '    oDr = oDt.NewRow
    '    oDr.Item(0) = "R"
    '    oDr.Item(1) = "Retorno"
    '    oDt.Rows.Add(oDr)

    '    oDr = oDt.NewRow
    '    oDr.Item(0) = "V"
    '    oDr.Item(1) = "Ida y Vuelta"
    '    oDt.Rows.Add(oDr)

    '    Return oDt

    'End Function



    'Private Function TipoPrioridad()
    '    Dim oDt As New DataTable

    '    oDt.Columns.Add("COD")
    '    oDt.Columns.Add("NOM")
    '    Dim oDr As DataRow
    '    oDr = oDt.NewRow
    '    oDr.Item(0) = "N"
    '    oDr.Item(1) = "NORMAL"
    '    oDt.Rows.Add(oDr)
    '    oDr = oDt.NewRow
    '    oDr.Item(0) = "U"
    '    oDr.Item(1) = "URGENTE"
    '    oDt.Rows.Add(oDr)
    '    Return oDt

    'End Function


    Private Sub Cargar_Compania()

        cmbCompania.Usuario = _pUsuario
        cmbCompania.pActualizar()
        cmbCompania.HabilitarCompaniaActual(Ransa.Utilitario.MainModuleDeploy.IdCompaniaDeploy)

    End Sub

    Private Sub cmbCompania_SelectionChangeCommitted(ByVal obeCompania As Ransa.Controls.UbicacionPlanta.Compania.beCompania) Handles cmbCompania.SelectionChangeCommitted

        Try
            cmbDivision.Usuario = _pUsuario
            cmbDivision.Compania = obeCompania.CCMPN_CodigoCompania
            If obeCompania.CCMPN_CodigoCompania = "EZ" Then
                cmbDivision.DivisionDefault = "T"
            End If
            cmbDivision.pActualizar()
            cmbDivision_SelectionChangeCommitted(Nothing)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmbDivision_SelectionChangeCommitted(ByVal obeDivision As Ransa.Controls.UbicacionPlanta.Division.beDivision) Handles cmbDivision.SelectionChangeCommitted
        Try
            Me.cmbPlanta.Usuario = _pUsuario
            Me.cmbPlanta.CodigoCompania = cmbDivision.Compania
            Me.cmbPlanta.CodigoDivision = cmbDivision.Division
            Me.cmbPlanta.PlantaDefault = 1
            Me.cmbPlanta.pActualizar()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Sub Cargar_Estado()

        Dim dtEstado As New DataTable
        Dim dr As DataRow

        dtEstado.Columns.Add("SESREQ", Type.GetType("System.String"))
        dtEstado.Columns.Add("SESREQ_S", Type.GetType("System.String"))

        dr = dtEstado.NewRow
        dr("SESREQ") = "0"
        dr("SESREQ_S") = "Todos"
        dtEstado.Rows.Add(dr)

        dr = dtEstado.NewRow
        dr("SESREQ") = "P"
        dr("SESREQ_S") = "Pendiente"
        dtEstado.Rows.Add(dr)

        dr = dtEstado.NewRow
        dr("SESREQ") = "A"
        dr("SESREQ_S") = "En Atenci�n"
        dtEstado.Rows.Add(dr)

        dr = dtEstado.NewRow
        dr("SESREQ") = "C"
        dr("SESREQ_S") = "Cerrado"
        dtEstado.Rows.Add(dr)

        cmbEstado.DataSource = dtEstado
        cmbEstado.DisplayMember = "SESREQ_S"
        cmbEstado.ValueMember = "SESREQ"
        cmbEstado.SelectedValue = "P"

    End Sub

    'Sub Cargar_Prioridad_Filtro()

    '    Dim dtPrioridad As New DataTable
    '    dtPrioridad = Prioridad.copy
    '    Dim dr As DataRow

    '    'dtPrioridad.Columns.Add("SPRSTR", Type.GetType("System.String"))
    '    'dtPrioridad.Columns.Add("SPRSTR_S", Type.GetType("System.String"))

    '    dr = dtPrioridad.NewRow
    '    dr("SPRSTR") = "0"
    '    dr("SPRSTR_S") = "Todos"
    '    dtPrioridad.Rows.InsertAt(dr, 0)

    '    'dr = dtPrioridad.NewRow
    '    'dr("SPRSTR") = "N"
    '    'dr("SPRSTR_S") = "Normal"
    '    'dtPrioridad.Rows.Add(dr)

    '    'dr = dtPrioridad.NewRow
    '    'dr("SPRSTR") = "U"
    '    'dr("SPRSTR_S") = "Urgente"
    '    'dtPrioridad.Rows.Add(dr)

    '    'dr = dtPrioridad.NewRow
    '    'dr("SPRSTR") = "C"
    '    'dr("SPRSTR_S") = "Carga Caliente"
    '    'dtPrioridad.Rows.Add(dr)

    '    cmbPrioridadFiltro.DataSource = dtPrioridad
    '    cmbPrioridadFiltro.DisplayMember = "SPRSTR_S"
    '    cmbPrioridadFiltro.ValueMember = "SPRSTR"
    '    cmbPrioridadFiltro.SelectedValue = "0"

    'End Sub


    'Private Sub Prioridad()
    '    Dim dtPrioridad As New DataTable
    '    Dim dr As DataRow

    '    dtPrioridad.Columns.Add("SPRSTR", Type.GetType("System.String"))
    '    dtPrioridad.Columns.Add("SPRSTR_S", Type.GetType("System.String"))


    '    dr = dtPrioridad.NewRow
    '    dr("SPRSTR") = "N"
    '    dr("SPRSTR_S") = "Normal"
    '    dtPrioridad.Rows.Add(dr)

    '    dr = dtPrioridad.NewRow
    '    dr("SPRSTR") = "U"
    '    dr("SPRSTR_S") = "Urgente"

    '    dr = dtPrioridad.NewRow
    '    dr("SPRSTR") = "C"
    '    dr("SPRSTR_S") = "Carga caliente"

    '    dtPrioridad.Rows.Add(dr)

    'End Sub


    Sub Cargar_Prioridad()

        Dim objBLL As New RequerimientoServicio_BLL
        Dim dtPrioridadReq As New DataTable
        'Dim dr As DataRow
        dtPrioridadReq = objBLL.Lista_Prioridad_Requerimiento

 
        cmbPrioridad.DataSource = dtPrioridadReq.Copy
        cmbPrioridad.DisplayMember = "SPRSTR_S"
        cmbPrioridad.ValueMember = "SPRSTR"
        cmbPrioridad.SelectedValue = "N"


        Dim dtPrioridadFiltro As New DataTable
        dtPrioridadFiltro = dtPrioridadReq.Copy
        Dim dr As DataRow
 
        dr = dtPrioridadFiltro.NewRow
        dr("SPRSTR") = "0"
        dr("SPRSTR_S") = "Todos"
        dtPrioridadFiltro.Rows.InsertAt(dr, 0)

        cmbPrioridadFiltro.DataSource = dtPrioridadFiltro
        cmbPrioridadFiltro.DisplayMember = "SPRSTR_S"
        cmbPrioridadFiltro.ValueMember = "SPRSTR"
        cmbPrioridadFiltro.SelectedValue = "0"


    End Sub

    Private Sub NroReq_Solo_Numeros(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            e.Handled = Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = ControlChars.Back)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Buscar(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Listar_Requerimiento_Servicio()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Listar_Requerimiento_Servicio()
        'Try
        Dim Entidad As New RequerimientoServicio
        Dim Negocio As New RequerimientoServicio_BLL
        Dim Mensaje As String = ""

        'If txtClienteFiltro.pCodigo = 0 Then
        '    Mensaje = "Seleccione un cliente" & Environment.NewLine
        'End If

        If Lista_Negocios.ToString.Trim = "" Then
            Mensaje = Mensaje & "Seleccione un Negocio"
        End If

        If Mensaje.ToString.Trim.Length > 0 Then
            MessageBox.Show(Mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        dgvDatos.DataSource = Nothing
        'dgvDatosSolicitud.DataSource = Nothing
        ''dgvPreAsignacion.DataSource = Nothing
        dtgRecursosAsignados.DataSource = Nothing
        dgvDimensiones.DataSource = Nothing

        Limpiar_Controles()
        gEnumOpc = EnumMan.Neutro
        HabilitarOpcion(EnumMan.Neutro, TabRequerimiento.SelectedTab.Name)


        dgvDatos.DataSource = Negocio.Lista_Requerimiento_Servicio(Obtener_Filtro)
        If dgvDatos.RowCount > 0 Then

            For Each Item As DataGridViewRow In dgvDatos.Rows
                If (Item.Cells("NMRGIM").Value > 0) Then
                    Item.Cells("IMG_NMRGIM").Value = My.Resources.text_block
                    'Item.Cells("IMG_ENVIO").Value = My.Resources.ark_selectall
                Else
                    Item.Cells("IMG_NMRGIM").Value = My.Resources.CuadradoBlanco
                    'Item.Cells("IMG_ENVIO").Value = My.Resources.ark_selectall
                End If

                If (Item.Cells("NROENV").Value > 0) Then
                    Item.Cells("IMG_ENVIO").Value = My.Resources.text_block
                Else
                    Item.Cells("IMG_ENVIO").Value = My.Resources.CuadradoBlanco
                End If

                If (Item.Cells("SJTTR").Value = "X") Then
                    Item.Cells("IMG_UNPROG").Value = My.Resources.text_block
                Else
                    Item.Cells("IMG_UNPROG").Value = My.Resources.CuadradoBlanco
                End If

                If (Item.Cells("SOLICT").Value > 0) Then
                    Item.Cells("IMG_SOL").Value = My.Resources.text_block
                Else
                    Item.Cells("IMG_SOL").Value = My.Resources.CuadradoBlanco
                End If
            Next
        End If




        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

    End Sub

    Private Function Validar() As Boolean
        Dim strMensajeError As String = ""

        If Me.cmbDivision.Division <> "T" Then
            strMensajeError &= "* Seleccionar divisi�n Transportes" & vbNewLine
        End If

        If Me.UcCliente_TxtF011.pCodigo = 0 Then
            strMensajeError &= "* Seleccionar cliente" & vbNewLine
        End If

        If cmbPrioridad.SelectedValue = "0" Then strMensajeError &= "* Seleccione tipo prioridad" & vbNewLine
        If cmbMercaderia.SelectedValue = 0 Then strMensajeError &= "* Seleccione tipo mercader�a" & vbNewLine
        'If chkReparto.Checked = False And chkTraslado.Checked = False Then strMensajeError &= "* Seleccione tipo requerimiento" & vbNewLine
        If txtPeso.Text.ToString.Trim = "" Then strMensajeError &= "* Ingrese peso(kg)" & vbNewLine
        'If txtLargo.Text.ToString.Trim = "" Then strMensajeError &= "* Ingrese largo(m)" & vbNewLine
        'If txtAncho.Text.ToString.Trim = "" Then strMensajeError &= "* Ingrese ancho(m)" & vbNewLine
        'If txtAlto.Text.ToString.Trim = "" Then strMensajeError &= "* Ingrese alto(m)" & vbNewLine

        If txtOS.Text.ToString.Trim = "" Then
            strMensajeError &= "* Seleccione  orden de servicio" & vbNewLine
        Else
            If txtLugarOrigen.Text.ToString.Trim = "" Then strMensajeError &= "* Asignar localidad origen" & vbNewLine
            If txtLugarDestino.Text.ToString.Trim = "" Then strMensajeError &= "* Asignar localidad destino" & vbNewLine
        End If

        If txtDireccionOrigen.Text.ToString.Trim = "" Then
            strMensajeError &= "* Ingrese direcci�n origen" & vbNewLine
        End If

        If txtDireccionDestino.Text.ToString.Trim = "" Then
            strMensajeError &= "* Ingrese direcci�n destino" & vbNewLine
        End If

        If txtResponsable.Text.ToString.Trim = "" Then
            strMensajeError &= "* Seleccione un Responsable" & vbNewLine
        End If
        If strMensajeError.Length > 0 Then
            MessageBox.Show(strMensajeError, "Aviso:", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return True
        End If
        Return False
    End Function

    Private Sub Eliminar(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Try
            If dgvDatos.CurrentRow Is Nothing Then Exit Sub

            Dim Negocio As New RequerimientoServicio_BLL

            If dgvDatos.CurrentRow IsNot Nothing Then
                Dim objSolicitudTransporte As New RequerimientoServicio_BLL
                Dim Respuesta As New List(Of Operaciones.Solicitud_Transporte)
                Dim numRequerimiento As Decimal = dgvDatos.CurrentRow.Cells("NUMREQT").Value
                Dim objSolicitud As New Operaciones.Solicitud_Transporte
                'objSolicitud.NUMREQT = numRequerimiento
                'Respuesta = objSolicitudTransporte.Listar_Solicitud_Transporte_Estado_Requerimiento(objSolicitud)

                'If Respuesta.Count > 0 Then
                '    MessageBox.Show("No se puede eliminar, existen solicitudes asignadas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Exit Sub
                'End If

                objSolicitud.NUMREQT = numRequerimiento
                objSolicitud.CCMPN = Me.cmbCompania.CCMPN_CodigoCompania
                Respuesta = objSolicitudTransporte.Listar_Estado_Solicitud_Requerimiento(objSolicitud)
                If Respuesta.Count > 0 Then
                    If Respuesta.Item(0).SESREQ = "A" Or Respuesta.Item(0).SESREQ = "C" Or Respuesta.Item(0).SJTTR = "X" Then
                        MessageBox.Show("No se puede eliminar. El Requerimiento est� en Atenci�n, Cerrado y/o en Junta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If

            End If

            'If dgvDatos.CurrentRow.Cells("SESREQ").Value.ToString.Trim = "P" Then
            Dim Entidad As New RequerimientoServicio

            If MessageBox.Show("�Desea eliminar el registro?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Entidad.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
                Entidad.NUMREQT = CDec(dgvDatos.CurrentRow.Cells("NUMREQT").Value)
                Entidad.CUSCRT = _pUsuario
                Entidad.NTRMCR = Environment.MachineName
                Negocio.intEliminarRequerimientoServicio(Entidad)
                Buscar(Nothing, Nothing)
            End If
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Exportar(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            If dgvDatos.Rows.Count = 0 Then
                Exit Sub
            End If

            Dim NPOI_SC As New Ransa.Utilitario.HelpClass_NPOI_SC

            ''Dim ListaExcel As List(Of RequerimientoServicio) = Me.dgvDatos.DataSource

            Dim dtExcel As New DataTable
            Dim dr As DataRow

            dtExcel.Columns.Add("NUMREQT").Caption = NPOI_SC.FormatDato("Nro. Req.", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("NORSRT").Caption = NPOI_SC.FormatDato("O/S", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TCRVTA").Caption = NPOI_SC.FormatDato("Negocio", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CLCLOR_S").Caption = NPOI_SC.FormatDato("Lugar Origen", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CLCLDS_S").Caption = NPOI_SC.FormatDato("Lugar Destino", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CCLNT_S").Caption = NPOI_SC.FormatDato("Cliente", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CCLNFC_S").Caption = NPOI_SC.FormatDato("Cliente (Facturaci�n)", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TIPOCNT_S").Caption = NPOI_SC.FormatDato("Tipo Mercader�a", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("FECREQ_S").Caption = NPOI_SC.FormatDato("Fecha Req.", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("HRAREQ_S").Caption = NPOI_SC.FormatDato("Hora Req.", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("FCHATN_D").Caption = NPOI_SC.FormatDato("Fecha para Atenci�n", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("HRAATN_D").Caption = NPOI_SC.FormatDato("Hora para Atenci�n", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("SPRSTR_S").Caption = NPOI_SC.FormatDato("Prioridad", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("QSLCIT").Caption = NPOI_SC.FormatDato("Cant. Veh�culos", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("QUNIDJUNTA").Caption = NPOI_SC.FormatDato("Unid. Programadas (Junta)", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("QUNIDSOLICITADAS").Caption = NPOI_SC.FormatDato("Unid. Solicitadas", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("QUNIDATENDIDAS").Caption = NPOI_SC.FormatDato("Unid. Atendidas", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("TNMMDT_1").Caption = NPOI_SC.FormatDato("Medio Transporte", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TOBERV").Caption = NPOI_SC.FormatDato("Descripci�n Mercader�a", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("REFDO1").Caption = NPOI_SC.FormatDato("Doc. Referencia", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("QPSOMR").Caption = NPOI_SC.FormatDato("Peso total (kg)", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("TDRCOR").Caption = NPOI_SC.FormatDato("Direcci�n Origen", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TDRENT").Caption = NPOI_SC.FormatDato("Direcci�n Destino", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TIRALC").Caption = NPOI_SC.FormatDato("Responsable", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("PRSCNT").Caption = NPOI_SC.FormatDato("Contacto", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("SESREQ_S").Caption = NPOI_SC.FormatDato("Estado Req.", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TOBS").Caption = NPOI_SC.FormatDato("Observaciones", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CUSCRT").Caption = NPOI_SC.FormatDato("Usuario Creador", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CULUSA").Caption = NPOI_SC.FormatDato("Usuario Actualizador", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("NORSRTSR").Caption = NPOI_SC.FormatDato("O/S Sol. Req.", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CLCLORSR_S").Caption = NPOI_SC.FormatDato("Lugar Origen Sol. Req.", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CLCLDSSR_S").Caption = NPOI_SC.FormatDato("Lugar Destino Sol. Req.", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("NDCORMSR").Caption = NPOI_SC.FormatDato("Nro. Doc. Origen Sol. Req.", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("PNCDTRSR").Caption = NPOI_SC.FormatDato("Nro. Ope. Agencias Sol. Req.", NPOI_SC.keyDatoTexto)


            'dtExcel_1 = HelpClass.GetDataSetNative(ListaExcel).Copy

            Dim columna As String = ""

            For FilaGrid As Integer = 0 To dgvDatos.Rows.Count - 1
                dr = dtExcel.NewRow
                For Each ItemCol As DataColumn In dtExcel.Columns
                    columna = ItemCol.ColumnName
                    If dgvDatos.Columns(columna) IsNot Nothing Then
                        dr(columna) = dgvDatos.Rows(FilaGrid).Cells(columna).Value
                    End If
                Next
                dtExcel.Rows.Add(dr)
            Next

            'For Each item As RequerimientoServicio In ListaExcel
            '    dr = dtExcel.NewRow
            '    dr("TCRVTA") = item.TCRVTA
            '    dr("CCLNT_S") = item.CCLNT_S
            '    dr("CCLNT_S") = item.CCLNFC_S

            '    dr("NUMREQT") = item.NUMREQT
            '    dr("NORSRT") = item.NORSRT

            '    dr("SPRSTR_S") = item.SPRSTR_S
            '    dr("TIPOCNT_S") = item.TIPOCNT_S

            '    dr("FECREQ_S") = item.FECREQ_S
            '    dr("HRAREQ_S") = item.HRAREQ_S

            '    dr("FCHATN_D") = item.FCHATN_D
            '    dr("HRAATN_D") = item.HRAATN_D

            '    dr("QSLCIT") = item.QSLCIT
            '    dr("QUNIDJUNTA") = item.QUNIDJUNTA
            '    dr("QUNIDSOLICITADAS") = item.QUNIDSOLICITADAS

            '    dr("QPSOMR") = item.QPSOMR
            '    dr("TNMMDT_1") = item.TNMMDT
            '    dr("TOBERV") = item.TOBERV


            '    ''dr("QMTLRG") = item.QMTLRG
            '    ''dr("QMTALT") = item.QMTALT
            '    '' dr("QMTANC") = item.QMTANC

            '    ''dr("NORSRT") = item.NORSRT
            '    ''dr("NDCORM") = item.NDCORM
            '    dr("CLCLOR_S") = item.CLCLOR_S
            '    dr("CLCLDS_S") = item.CLCLDS_S
            '    dr("TDRCOR") = item.TDRCOR
            '    dr("TDRENT") = item.TDRENT

            '    ''dr("TIPSRV_S") = item.TIPSRV_S
            '    dr("REFDO1") = item.REFDO1
            '    dr("TOBS") = item.TOBS
            '    dr("CUSCRT") = item.CUSCRT
            '    dr("CULUSA") = item.CULUSA
            '    dr("SESREQ_S") = item.SESREQ_S

            '    dr("TIRALC") = item.TIRALC
            '    dr("PRSCNT") = item.PRSCNT

            '    dtExcel.Rows.Add(dr)
            'Next

            Dim ListaDatatable As New List(Of DataTable)
            dtExcel.TableName = "REQUERIMIENTO_SERVICIO"
            ListaDatatable.Add(dtExcel.Copy)

            Dim ListaTitulos As New List(Of String)
            ListaTitulos.Add("LISTA DE REQUERIMIENTOS DE SERVICIO")

            Dim oLisFiltro As New List(Of SortedList)
            Dim F As New SortedList
            F.Add(0, "Compa�ia:| " & cmbCompania.CCMPN_Descripcion)
            F.Add(1, "Divisi�n:|" & cmbDivision.DivisionDescripcion)
            F.Add(2, "Planta:| " & cmbPlanta.DescripcionPlanta)

            'If chkFecha.Checked = True Then
            '    F.Add(5, "Fecha:| " & Me.dtpFechaInicioReq.Value.Date & " - " & Me.dtpFechaFinReq.Value.Date)
            'End If
            'F.Add(5, "Fecha:| " & Me.dtpFechaInicioReq.Value.Date & " - " & Me.dtpFechaFinReq.Value.Date)

            oLisFiltro.Add(F)

            NPOI_SC.ExportExcelGeneralReleaseMultiple(ListaDatatable, ListaTitulos, Nothing, oLisFiltro, 0)


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub







    'Private Sub dgvDatos_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try

    '        Dim lint_indice As Integer = Me.dgvDatos.CurrentCellAddress.Y

    '        Select Case ("" & dgvDatos.Item("SESREQ", lint_indice).Value).ToString().Trim()

    '            Case "P"
    '                'btnAsignarSolicitud.Enabled = False
    '                'btnModificarSolicitud.Enabled = False
    '                'btnCancelar.Enabled = False
    '                'btnAnularSolicitud.Enabled = False
    '                'Validad_Botones(False)
    '                'Valida_Botones(True)

    '                btnModificar.Enabled = True
    '                btnEliminar.Enabled = True
    '                btnAtender.Enabled = True

    '            Case "R"
    '                'btnAsignarSolicitud.Enabled = True
    '                'btnModificarSolicitud.Enabled = False
    '                'btnCancelar.Enabled = False
    '                'btnAnularSolicitud.Enabled = False
    '                'Validad_Botones(False)
    '                'Valida_Botones(False)

    '                btnModificar.Enabled = False
    '                btnEliminar.Enabled = False
    '                btnAtender.Enabled = False

    '            Case "A"
    '                'btnAsignarSolicitud.Enabled = False
    '                'btnModificarSolicitud.Enabled = True
    '                'btnModificarSolicitud.Text = "Modificar"
    '                'btnCancelar.Enabled = True
    '                'btnAnularSolicitud.Enabled = True
    '                'Validad_Botones(False)
    '                'Cargar_Informacion()
    '                'Valida_Botones(False)

    '                btnModificar.Enabled = False
    '                btnEliminar.Enabled = False
    '                btnAtender.Enabled = False

    '        End Select



    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try


    'End Sub

    'Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        dgvDatos_SelectionChanged(Nothing, Nothing)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    Private Sub chkHora_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHora.CheckedChanged
        Try
            dtpHoraIniReq.Enabled = chkHora.Checked
            dtpHoraFinReq.Enabled = chkHora.Checked
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub KryptonCheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHoraAtencion.CheckedChanged

        Try
            dtpHoraIniAtencion.Enabled = chkHoraAtencion.Checked
            dtpHoraFinAtencion.Enabled = chkHoraAtencion.Checked
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub dgvDatos_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    '    If dgvDatos.Columns.Item(e.ColumnIndex).Name = "CHK" Then
    '        If dgvDatos.CurrentRow.Cells("SESREQ").Value <> "P" Then
    '            dgvDatos.CurrentRow.Cells("CHK").Value = False
    '            dgvDatos.EndEdit()
    '        End If
    '    End If


    'End Sub


    Private Sub txtCantidadSolicitada_SoloNumeros(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidadSolicitada.KeyPress
        Try
            e.Handled = Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = ControlChars.Back)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub Limpiar_Controles()

        UcCliente_TxtF011.pClear()
        txtNroReq.Text = ""
        'cmbMercaderia.SelectedValue = 0
        'cmbPrioridad.SelectedValue = "N"
        cboMedioTransporte.SelectedValue = 4
        txtCantidadSolicitada.Text = ""
        txtPeso.Text = ""
        'txtLargo.Text = ""
        'txtAncho.Text = ""
        'txtAlto.Text = ""
        txtOS.Text = ""
        txtOSAgencia.Text = ""
        txtOSAgencia.Tag = ""
        txtUbigeoOrigen.Text = ""
        txtUbigeoDestino.Text = ""
        txtLugarOrigen.Text = ""
        txtLugarDestino.Text = ""
        txtTipoUnidad.Text = ""
        'txtMercaderia.Text = ""
        txtDireccionOrigen.Text = ""
        txtDireccionDestino.Text = ""
        txtDocRef.Text = ""
        txtObservaciones.Text = ""
        txtLugarOrigen.Tag = ""
        txtLugarDestino.Tag = ""
        dtpFechaReg.Value = Now.Date
        dtpFechaAten.Value = Now.Date

        txtDescripcionMercaderia.Text = ""
        txtContacto.Text = ""
        txtResponsable.Text = ""
        txtEmail.Text = ""

    End Sub


    'UTILIZAR SOLO PARA INHABILITAR
    Private Sub Estado_Controles(ByVal estado As Boolean)
        UcCliente_TxtF011.Enabled = estado
        cmbMercaderia.Enabled = estado
        'cboMedioTransporte.SelectedValue = 4
        cboMedioTransporte.Enabled = estado
        cmbPrioridad.Enabled = estado
        cboMedioTransporte.Enabled = estado
        txtCantidadSolicitada.Enabled = estado
        txtPeso.Enabled = estado
        'txtLargo.Enabled = estado
        'txtAncho.Enabled = estado
        'txtAlto.Enabled = estado
        txtDireccionOrigen.Enabled = estado
        txtDireccionDestino.Enabled = estado
        txtDocRef.Enabled = estado
        txtObservaciones.Enabled = estado
        btnBuscaOrdenServicio.Enabled = estado
        btnAsignarOSAgencias.Enabled = estado
        dtpFechaReg.Enabled = estado
        dtpHoraReg.Enabled = estado
        dtpFechaAten.Enabled = estado
        dtpHoraAten.Enabled = estado

        txtDescripcionMercaderia.Enabled = estado
        txtContacto.Enabled = estado
        ucResponsable.Enabled = estado

        'chkReparto.Enabled = estado
        'chkTraslado.Enabled = estado
    End Sub


    'Sub Habilitar_Controles(ByVal Estado As Mantenimiento)

    '    Select Case Estado

    '        Case Mantenimiento.Neutro

    '            UcCliente_TxtF011.Enabled = False
    '            cmbMercaderia.Enabled = False
    '            cboMedioTransporte.SelectedValue = 4
    '            cmbPrioridad.Enabled = False
    '            cboMedioTransporte.Enabled = False
    '            txtCantidadSolicitada.Enabled = False
    '            txtPeso.Enabled = False
    '            txtLargo.Enabled = False
    '            txtAncho.Enabled = False
    '            txtAlto.Enabled = False
    '            txtDireccionOrigen.Enabled = False
    '            txtDireccionDestino.Enabled = False
    '            txtDocRef.Enabled = False
    '            txtObservaciones.Enabled = False
    '            btnBuscaOrdenServicio.Enabled = False
    '            btnAsignarOSAgencias.Enabled = False

    '            dtpFechaReg.Enabled = False
    '            dtpHoraReg.Enabled = False
    '            dtpFechaAten.Enabled = False
    '            dtpHoraAten.Enabled = False

    '            chkReparto.Enabled = False
    '            chkTraslado.Enabled = False

    '        Case Mantenimiento.Nuevo

    '            UcCliente_TxtF011.Enabled = True
    '            cmbMercaderia.Enabled = True
    '            cmbMercaderia.SelectedValue = 4
    '            cmbPrioridad.Enabled = True
    '            cboMedioTransporte.Enabled = True
    '            cboMedioTransporte.SelectedValue = 4
    '            txtCantidadSolicitada.Enabled = True
    '            txtPeso.Enabled = True
    '            txtLargo.Enabled = True
    '            txtAncho.Enabled = True
    '            txtAlto.Enabled = True
    '            txtDireccionOrigen.Enabled = True
    '            txtDireccionDestino.Enabled = True
    '            txtDocRef.Enabled = True
    '            txtObservaciones.Enabled = True
    '            btnBuscaOrdenServicio.Enabled = True
    '            btnAsignarOSAgencias.Enabled = True

    '            dtpFechaReg.Enabled = True
    '            dtpHoraReg.Enabled = True
    '            dtpFechaAten.Enabled = True
    '            dtpHoraAten.Enabled = True

    '            chkReparto.Enabled = True
    '            chkTraslado.Enabled = True
    '            chkTraslado.Checked = True


    '        Case Mantenimiento.Modificar

    '            UcCliente_TxtF011.Enabled = False
    '            cmbMercaderia.Enabled = True
    '            cmbPrioridad.Enabled = True
    '            cboMedioTransporte.Enabled = True
    '            txtCantidadSolicitada.Enabled = True
    '            txtPeso.Enabled = True
    '            txtLargo.Enabled = True
    '            txtAncho.Enabled = True
    '            txtAlto.Enabled = True
    '            txtDireccionOrigen.Enabled = True
    '            txtDireccionDestino.Enabled = True
    '            txtDocRef.Enabled = True
    '            txtObservaciones.Enabled = True
    '            btnBuscaOrdenServicio.Enabled = True
    '            btnAsignarOSAgencias.Enabled = True

    '            dtpFechaReg.Enabled = True
    '            dtpHoraReg.Enabled = True
    '            dtpFechaAten.Enabled = True
    '            dtpHoraAten.Enabled = True

    '            chkReparto.Enabled = True
    '            chkTraslado.Enabled = True


    '    End Select



    'End Sub

    'Sub Habilitar_Botones(ByVal Estado As Mantenimiento)


    '    Select Case Estado

    '        Case Mantenimiento.Neutro

    '            btnNuevo.Enabled = True
    '            btnModificar.Enabled = True
    '            btnCancelar.Enabled = False
    '            btnEliminar.Enabled = True
    '            btnModificar.Text = "Modificar"
    '            btnAtender.Enabled = True
    '            btnCerrarReq.Enabled = True

    '        Case Mantenimiento.Nuevo

    '            btnNuevo.Enabled = False
    '            btnModificar.Enabled = True
    '            btnCancelar.Enabled = True
    '            btnEliminar.Enabled = False
    '            btnAtender.Enabled = False
    '            btnCerrarReq.Enabled = False

    '        Case Mantenimiento.Modificar

    '            btnNuevo.Enabled = False
    '            btnModificar.Enabled = True
    '            btnCancelar.Enabled = True
    '            btnEliminar.Enabled = False
    '            btnAtender.Enabled = False
    '            btnCerrarReq.Enabled = False

    '    End Select


    'End Sub

    'Sub Habilitar_Botones_A(ByVal Estado As Mantenimiento)


    '    Select Case Estado

    '        Case Mantenimiento.Neutro

    '            btnNuevo.Enabled = True
    '            btnModificar.Enabled = False
    '            btnCancelar.Enabled = False
    '            btnEliminar.Enabled = False
    '            btnModificar.Text = "Modificar"
    '            btnAtender.Enabled = True

    '        Case Mantenimiento.Nuevo

    '            btnNuevo.Enabled = False
    '            btnModificar.Enabled = True
    '            btnCancelar.Enabled = True
    '            btnEliminar.Enabled = False
    '            btnAtender.Enabled = False

    '    End Select

    'End Sub

    Private Sub HabilitarOpcion(ByVal opc As EnumMan, ByVal tabActivo As String)

        Select Case tabActivo
            Case "TabDatosRequerimiento"
                Select Case opc
                    Case EnumMan.Neutro
                        btnNuevo.Enabled = True
                        btnModificar.Enabled = True
                        btnGuardar.Visible = False
                        btnGuardar.Enabled = False
                        btnCancelar.Enabled = False
                        btnEliminar.Enabled = True
                        ''btnCerrarReq.Enabled = True
                        btnMedida.Enabled = False
                        btnExportarExcel.Enabled = True

                    Case EnumMan.Nuevo
                        btnNuevo.Enabled = False
                        btnModificar.Enabled = False
                        btnGuardar.Visible = True
                        btnGuardar.Enabled = True
                        btnCancelar.Enabled = True
                        btnEliminar.Enabled = False
                        ''btnCerrarReq.Enabled = False
                        btnMedida.Enabled = False
                        ''btnExportarExcel.Enabled = False

                    Case EnumMan.Editar
                        btnNuevo.Enabled = False
                        btnModificar.Enabled = False
                        btnGuardar.Visible = True
                        btnGuardar.Enabled = True
                        btnCancelar.Enabled = True
                        btnEliminar.Enabled = False
                        ''btnCerrarReq.Enabled = False
                        btnMedida.Enabled = False
                        ''btnExportar.Enabled = False
                End Select
            Case "TabMedidas"
                btnNuevo.Enabled = False
                btnModificar.Enabled = False
                btnGuardar.Visible = False
                btnGuardar.Enabled = False
                btnCancelar.Enabled = False
                btnEliminar.Enabled = False
                '' btnCerrarReq.Enabled = True
                btnMedida.Enabled = True
                ''btnExportar.Enabled = True

            Case "TabSolicitud"
                btnNuevo.Enabled = False
                btnModificar.Enabled = False
                btnGuardar.Visible = False
                btnGuardar.Enabled = False
                btnCancelar.Enabled = False
                btnEliminar.Enabled = False
                ''btnCerrarReq.Enabled = True
                btnMedida.Enabled = False
                ''btnExportar.Enabled = True

            Case "TabUnidadesProgramadas"
                btnMedida.Enabled = False
        End Select

    End Sub


    'Private Sub btnCerrarReq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrarReq.Click
    '    Try

    '        Dim ReqServ As New RequerimientoServicio
    '        Dim ReqServ_Estado As New RequerimientoServicio
    '        Dim Negocio As New RequerimientoServicio_BLL

    '        If dgvDatos.CurrentRow IsNot Nothing Then

    '            ReqServ.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
    '            ReqServ.CDVSN = dgvDatos.CurrentRow.Cells("CDVSN").Value
    '            ReqServ.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value

    '            ReqServ_Estado = Negocio.Lista_Requerimiento_Servicio_Estado_Actual(ReqServ)

    '            If ReqServ_Estado.SESREQ.ToString.Trim = "C" Then
    '                MessageBox.Show("El requerimiento ya se encuentra cerrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If

    '            For Each fila As DataGridViewRow In dgvDatosSolicitud.Rows
    '                If fila.Cells("SESSLC").Value.ToString.Trim <> "C" Then
    '                    MessageBox.Show("Las solicitudes asignadas deben estar cerradas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Exit Sub
    '                End If
    '            Next

    '            If MessageBox.Show("Est� seguro de cerrar la atenci�n del requerimiento?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

    '                ReqServ.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
    '                ReqServ.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
    '                ReqServ.CUSCRT = MainModule.USUARIO
    '                ReqServ.NTRMCR = Environment.MachineName
    '                ReqServ.SESREQ = "C"
    '                Negocio.intActualizarRequerimientoSolicitud(ReqServ)
    '                Buscar(Nothing, Nothing)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub Nuevo_Requerimiento(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try

            Limpiar_Controles()
            gEnumOpc = EnumMan.Nuevo
            HabilitarOpcion(EnumMan.Nuevo, TabRequerimiento.SelectedTab.Name)

            dgvDimensiones.DataSource = Nothing
            UcCliente_TxtF011.Enabled = True
            cmbMercaderia.SelectedValue = 4
            cmbPrioridad.SelectedValue = "N"
            cboMedioTransporte.SelectedValue = 4

            cmbMercaderia.Enabled = True
            cboMedioTransporte.Enabled = True
            cmbPrioridad.Enabled = True
            txtCantidadSolicitada.Enabled = True
            txtPeso.Enabled = True
            'txtLargo.Enabled = True
            'txtAncho.Enabled = True
            'txtAlto.Enabled = True
            txtDireccionOrigen.Enabled = True
            txtDireccionDestino.Enabled = True
            txtDocRef.Enabled = True
            txtObservaciones.Enabled = True
            btnBuscaOrdenServicio.Enabled = True
            btnAsignarOSAgencias.Enabled = True
            'dtpFechaReg.Enabled = True
            'dtpHoraReg.Enabled = True
            dtpFechaAten.Enabled = True
            dtpHoraAten.Enabled = True

            txtDescripcionMercaderia.Enabled = True
            txtContacto.Enabled = True

            ucResponsable.Enabled = True

            dtpHoraReg.Value = Date.Parse(String.Format("{0:HH:mm:ss}", Date.Now))
            dtpHoraAten.Value = Date.Parse(String.Format("{0:HH:mm:ss}", Date.Now))

            'chkReparto.Enabled = True
            'chkTraslado.Enabled = True
            'chkTraslado.Checked = True
            'Estado = Mantenimiento.Nuevo
            'Habilitar_Controles(Estado)
            'Habilitar_Botones(Estado)
            'btnModificar.Text = "Guardar"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function AsignarEntRequerimiento() As RequerimientoServicio
        Dim Entidad As New RequerimientoServicio
        Dim Negocio As New RequerimientoServicio_BLL
        ' With Entidad
        Entidad.CCMPN = cmbCompania.CCMPN_CodigoCompania
        Entidad.CDVSN = cmbDivision.Division
        Entidad.CPLNDV = cmbPlanta.Planta
        Entidad.CCLNT = UcCliente_TxtF011.pCodigo
        Entidad.CCLNT_S = UcCliente_TxtF011.pRazonSocial

        Entidad.NORSRT = Val(txtOS.Text)
        Entidad.NDCORM = Val(txtOSAgencia.Text)

        Entidad.SESREQ = "P"
        Entidad.SPRSTR = cmbPrioridad.SelectedValue
        Entidad.SPRSTR_S = cmbPrioridad.Text.ToString.Trim
        Entidad.TIPOCNT = cmbMercaderia.SelectedValue
        Entidad.TIPOCNT_S = cmbMercaderia.Text.ToString.Trim

        'If chkReparto.Checked = True Then
        '    Entidad.TIPSRV = "R"
        '    Entidad.TIPSRV_S = "Reparto"
        'Else
        '    Entidad.TIPSRV = "T"
        '    Entidad.TIPSRV_S = "Traslado"
        'End If
        Entidad.TIPSRV = "T"

        Entidad.FECREQ = CDec(String.Format("{0:yyyyMMdd}", dtpFechaReg.Value))
        Entidad.FECREQ_S = String.Format("{0:d/M/yyyy}", dtpFechaReg.Value)
        Entidad.HRAREQ = CDec(String.Format("{0:HHmmss}", dtpHoraReg.Value))
        Entidad.HRAREQ_S = String.Format("{0:HH:mm:ss}", dtpHoraReg.Value)

        Entidad.FCHATN = CDec(String.Format("{0:yyyyMMdd}", dtpFechaAten.Value))
        Entidad.FCHATN_D = String.Format("{0:d/M/yyyy}", dtpFechaAten.Value)
        Entidad.HRAATN = CDec(String.Format("{0:HHmmss}", dtpHoraAten.Value))
        Entidad.HRAATN_D = String.Format("{0:HH:mm:ss}", dtpHoraAten.Value)

        Entidad.QPSOMR = CDec(Val(txtPeso.Text))
        'Entidad.QMTLRG = CDec(Val(txtLargo.Text))
        'Entidad.QMTALT = CDec(Val(txtAlto.Text))
        'Entidad.QMTANC = CDec(Val(txtAncho.Text))
        Entidad.REFDO1 = txtDocRef.Text.ToString.Trim
        Entidad.TOBS = txtObservaciones.Text.ToString.Trim

        Entidad.CLCLOR = txtLugarOrigen.Tag
        Entidad.CLCLOR_S = txtLugarOrigen.Text
        Entidad.CLCLDS = txtLugarDestino.Tag
        Entidad.CLCLDS_S = txtLugarDestino.Text

        Entidad.TDRCOR = txtDireccionOrigen.Text.ToString.Trim
        Entidad.TDRENT = txtDireccionDestino.Text.ToString.Trim

        Entidad.CMEDTR = cboMedioTransporte.SelectedValue
        Entidad.QSLCIT = Val(txtCantidadSolicitada.Text)
        'Entidad.PNCDTR = _PNCDTR_Operacion
        Entidad.PNCDTR = ("" & txtOSAgencia.Tag).ToString.Trim

        ' txtOSAgencia.Tag 

        Entidad.CUSCRT = _pUsuario
        Entidad.NTRMCR = Environment.MachineName

        Entidad.NUMREQT = CDec(Val(txtNroReq.Text))
        Entidad.CULUSA = _pUsuario
        Entidad.NTRMNL = Environment.MachineName

        Entidad.TOBERV = txtDescripcionMercaderia.Text.Trim
        Entidad.PRSCNT = txtContacto.Text

        Entidad.TIRALC = txtResponsable.Text.Trim

        Return Entidad
    End Function

    Private Num_RequerimientoEnvio As Decimal = 0D
    Private Sub Enviar_Requerimiento_Servicio()
        Try
            Dim Entidad As New RequerimientoServicio
            Control.CheckForIllegalCrossThreadCalls = False
            Dim Envio As New RequerimientoServicioEnvio_BLL
            Envio.CULUSA = _pUsuario
            Envio.MailAccount = _pMailAccount ''System.Configuration.ConfigurationManager.AppSettings("MailFrom")
            Envio.Mailpassword = _pMailpassword ''System.Configuration.ConfigurationManager.AppSettings("MailFromClave")
            Envio.MailAccount_Gmail = _pMailAccount_Gmail ''System.Configuration.ConfigurationManager.AppSettings("MailCO")
            Envio.MailPassword_Gmail = _pMailPassword_Gmail ''System.Configuration.ConfigurationManager.AppSettings("MailCOClave")
            Envio.Mailto_Error = _pMailto_Error ''System.Configuration.ConfigurationManager.AppSettings("emailto_error")
            ' Entidad.NUMREQT = Num_RequerimientoEnvio
            If Envio.EnviarEmail_RequerimientoServicio(Num_RequerimientoEnvio, cmbCompania.CCMPN_CodigoCompania) = 1 Then
                'OK
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            Dim TabSelect As String = TabRequerimiento.SelectedTab.Name
            Dim Num_Requerimiento As Decimal = 0
            Select Case TabSelect
                Case "TabDatosRequerimiento"
                    Select Case gEnumOpc
                        Case EnumMan.Nuevo

                            If Validar() Then Exit Sub
                            Dim Entidad As New RequerimientoServicio
                            Dim Negocio As New RequerimientoServicio_BLL
                            Entidad = AsignarEntRequerimiento()
                            Num_Requerimiento = Negocio.intInsertarRequerimientoServicio(Entidad)

                            If MessageBox.Show("Se cre� el requerimiento:  " & Num_Requerimiento & Environment.NewLine & "�Desea agregar dimensiones a la mercader�a?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                                Dim ObjFrm As New frmDimensiones

                                ObjFrm.pCCMPN = cmbCompania.CCMPN_CodigoCompania
                                ObjFrm.pNUMREQT = Num_Requerimiento
                                ObjFrm.pUsuario = _pUsuario
                                ObjFrm.ShowDialog()

                                dgvDatos_SelectionChanged(Nothing, Nothing)

                            End If

                            'If Entidad.SPRSTR = "U" Then
                            Num_RequerimientoEnvio = Num_Requerimiento
                            'oHebraReqServ = New Thread(AddressOf Enviar_Requerimiento_Servicio)
                            'oHebraReqServ.Start()
                            'End If

                            gEnumOpc = EnumMan.Neutro
                            HabilitarOpcion(EnumMan.Neutro, TabRequerimiento.SelectedTab.Name)
                            Estado_Controles(False)
                            Buscar(Nothing, Nothing)

                        Case EnumMan.Editar

                            If Validar() Then Exit Sub
                            Dim Entidad As New RequerimientoServicio
                            Dim Negocio As New RequerimientoServicio_BLL
                            Entidad = AsignarEntRequerimiento()
                            Negocio.intActualizarRequerimientoServicio(Entidad)
                            gEnumOpc = EnumMan.Neutro
                            HabilitarOpcion(EnumMan.Neutro, TabRequerimiento.SelectedTab.Name)
                            Estado_Controles(False)
                            MessageBox.Show("Registro actualizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Buscar(Nothing, Nothing)


                    End Select
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub Modificar_Requerimiento(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        Try

            If dgvDatos.CurrentRow Is Nothing Then Exit Sub


            Dim TabSelect As String = TabRequerimiento.SelectedTab.Name

            Select Case TabSelect
                Case "TabDatosRequerimiento"


                    If dgvDatos.CurrentRow IsNot Nothing Then
                        Dim objSolicitudTransporte As New RequerimientoServicio_BLL
                        Dim Respuesta As New List(Of Operaciones.Solicitud_Transporte)
                        'Dim numRequerimiento As Decimal = dgvDatos.CurrentRow.Cells("NUMREQT").Value
                        'Dim objSolicitud As New Operaciones.Solicitud_Transporte
                        'objSolicitud.NUMREQT = numRequerimiento
                        'Respuesta = objSolicitudTransporte.Listar_Solicitud_Transporte_Estado_Requerimiento(objSolicitud)


                        Dim objSolTrsp As New Operaciones.Solicitud_Transporte
                        objSolTrsp.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
                        objSolTrsp.CCMPN = Me.cmbCompania.CCMPN_CodigoCompania

                        Respuesta = objSolicitudTransporte.Listar_Estado_Solicitud_Requerimiento(objSolTrsp)
                        If Respuesta.Count > 0 Then
                            If Respuesta.Item(0).SESREQ = "A" Or Respuesta.Item(0).SESREQ = "C" Or Respuesta.Item(0).SJTTR = "X" Then
                                MessageBox.Show("No se puede modificar. El Requerimiento est� en Atenci�n, Cerrado y/o en Junta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                        End If
                    End If

                    gEnumOpc = EnumMan.Editar
                    HabilitarOpcion(EnumMan.Editar, TabRequerimiento.SelectedTab.Name)

                    UcCliente_TxtF011.Enabled = False
                    cmbMercaderia.Enabled = True
                    cmbPrioridad.Enabled = True
                    cboMedioTransporte.Enabled = True
                    txtCantidadSolicitada.Enabled = True
                    txtPeso.Enabled = True
                    'txtLargo.Enabled = True
                    'txtAncho.Enabled = True
                    'txtAlto.Enabled = True
                    txtDireccionOrigen.Enabled = True
                    txtDireccionDestino.Enabled = True
                    txtDocRef.Enabled = True
                    txtObservaciones.Enabled = True
                    btnBuscaOrdenServicio.Enabled = True
                    btnAsignarOSAgencias.Enabled = True

                    'dtpFechaReg.Enabled = True
                    'dtpHoraReg.Enabled = True
                    dtpFechaAten.Enabled = True
                    dtpHoraAten.Enabled = True

                    txtDescripcionMercaderia.Enabled = True
                    txtContacto.Enabled = True
                    ucResponsable.Enabled = True

                    'chkReparto.Enabled = True
                    'chkTraslado.Enabled = True
            End Select

            'If btnModificar.Text = "Guardar" Then

            '    If Validar() Then Exit Sub

            '    Dim Entidad As New RequerimientoServicio
            '    Dim Negocio As New RequerimientoServicio_BLL

            '    With Entidad
            '        .CCMPN = cmbCompania.CCMPN_CodigoCompania
            '        .CDVSN = cmbDivision.Division
            '        .CPLNDV = cmbPlanta.Planta
            '        .CCLNT = UcCliente_TxtF011.pCodigo
            '        .CCLNT_S = UcCliente_TxtF011.pRazonSocial

            '        .NORSRT = Val(txtOS.Text)
            '        .NDCORM = Val(txtOSAgencia.Text)

            '        .SESREQ = "P"
            '        .SPRSTR = cmbPrioridad.SelectedValue
            '        .SPRSTR_S = cmbPrioridad.Text.ToString.Trim
            '        .TIPOCNT = cmbMercaderia.SelectedValue
            '        .TIPOCNT_S = cmbMercaderia.Text.ToString.Trim

            '        If chkReparto.Checked = True Then
            '            .TIPSRV = "R"
            '            .TIPSRV_S = "Reparto"
            '        Else
            '            .TIPSRV = "T"
            '            .TIPSRV_S = "Traslado"
            '        End If

            '        .FRGTRO = CDec(String.Format("{0:yyyyMd}", dtpFechaReg.Value))
            '        .FRGTRO_S = String.Format("{0:d/M/yyyy}", dtpFechaReg.Value)
            '        .HRGTRO = CDec(String.Format("{0:HHmmss}", dtpHoraReg.Value))
            '        .HRGTRO_S = String.Format("{0:HH:mm:ss}", dtpHoraReg.Value)

            '        .FECREQ = CDec(String.Format("{0:yyyyMd}", dtpFechaAten.Value))
            '        .FECREQ_S = String.Format("{0:d/M/yyyy}", dtpFechaAten.Value)
            '        .HRAREQ = CDec(String.Format("{0:HHmmss}", dtpHoraAten.Value))
            '        .HRAREQ_S = String.Format("{0:HH:mm:ss}", dtpHoraAten.Value)

            '        .QPSOMR = CDec(Val(txtPeso.Text))
            '        .QMTLRG = CDec(Val(txtLargo.Text))
            '        .QMTALT = CDec(Val(txtAlto.Text))
            '        .QMTANC = CDec(Val(txtAncho.Text))
            '        .REFDO1 = txtDocRef.Text.ToString.Trim
            '        .TOBS = txtObservaciones.Text.ToString.Trim

            '        .CLCLOR = txtLugarOrigen.Tag
            '        .CLCLOR_S = txtLugarOrigen.Text
            '        .CLCLDS = txtLugarDestino.Tag
            '        .CLCLDS_S = txtLugarDestino.Text

            '        .TDRCOR = txtDireccionOrigen.Text.ToString.Trim
            '        .TDRENT = txtDireccionDestino.Text.ToString.Trim

            '        .CMEDTR = cboMedioTransporte.SelectedValue
            '        .QSLCIT = txtCantidadSolicitada.Text
            '        .PNCDTR = _PNCDTR_Operacion

            '    End With

            '    Dim Num_Requerimiento As Integer = 0

            '    If Val(txtNroReq.Text) > 0 Then

            '        Entidad.NUMREQT = CDec(Val(txtNroReq.Text))
            '        Entidad.CULUSA = MainModule.USUARIO
            '        Entidad.NTRMNL = Environment.MachineName

            '        Negocio.intActualizarRequerimientoServicio(Entidad)
            '        MessageBox.Show("Registro actualizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)


            '    Else

            '        Entidad.CUSCRT = MainModule.USUARIO
            '        Entidad.NTRMCR = Environment.MachineName

            '        Num_Requerimiento = Negocio.intInsertarRequerimientoServicio(Entidad)
            '        MessageBox.Show("Requerimiento ingresado" & Environment.NewLine & " C�digo: " & Num_Requerimiento, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)

            '        If Entidad.SPRSTR = "U" Then

            '            Dim Envio As New RequerimientoServicioEnvio_BLL

            '            Envio.CULUSA = MainModule.USUARIO
            '            Envio.MailAccount = System.Configuration.ConfigurationManager.AppSettings("MailFrom")
            '            Envio.Mailpassword = System.Configuration.ConfigurationManager.AppSettings("MailFromClave")

            '            Envio.MailAccount_Gmail = System.Configuration.ConfigurationManager.AppSettings("MailCO")
            '            Envio.MailPassword_Gmail = System.Configuration.ConfigurationManager.AppSettings("MailCOClave")

            '            Entidad.NUMREQT = Num_Requerimiento

            '            If Envio.EnviarEmail_RequerimientoServicio(Entidad) = 1 Then
            '                'OK
            '            End If

            '        End If

            '    End If

            '    Buscar(Nothing, Nothing)
            '    btnModificar.Text = "Modificar"
            '    Estado = MANTENIMIENTO.Neutro
            '    Habilitar_Controles(Estado)
            '    Habilitar_Botones(Estado)

            'Else
            '    If dgvDatos.CurrentRow Is Nothing Then
            '        Exit Sub
            '    End If

            '    btnModificar.Text = "Guardar"
            '    Estado = MANTENIMIENTO.MODIFICAR
            '    Habilitar_Controles(Estado)
            '    Habilitar_Botones(Estado)
            'End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    ''Me.AccionCancelar()
    '    Try
    'Dim tabSelect As String = TabSolicitudFlete.SelectedTab.Name
    '        gEnumOpc = EnumMan.Neutro
    '        HabilitarOpcion(EnumMan.Neutro, tabSelect)
    '        Limpiar_Controles()
    '        Estado_Controles(False)
    '        Cargar_Datos_Solicitud()
    '        Cargar_Unidades_Asignadas_Solicitud()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    Private Sub Cancelar_Requerimiento(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        Try
            Dim tabSelect As String = TabRequerimiento.SelectedTab.Name
            gEnumOpc = EnumMan.Neutro
            HabilitarOpcion(EnumMan.Neutro, tabSelect)
            Limpiar_Controles()
            ''dgvPreAsignacion.DataSource = Nothing
            dgvDatosSolicitud.DataSource = Nothing
            '    Estado_Controles(False)
            ' Estado = Mantenimiento.Neutro
            'Habilitar_Controles(Estado)
            'Habilitar_Botones(False)
            Estado_Controles(False)
            CargarDatosRequerimiento()
            ''Listar_Unidades_Programadas()
            ''Listar_Solicitudes()
            '  btnModificar.Text = "Modificar"
            ' Cargar_Informacion(Nothing, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarDatosRequerimiento()
        Limpiar_Controles()
        If dgvDatos.CurrentRow Is Nothing Then Exit Sub
        Dim Entidad As New RequerimientoServicio
        UcCliente_TxtF011.pCargar(CLng(dgvDatos.CurrentRow.Cells("CCLNT").Value))
        txtNroReq.Text = dgvDatos.CurrentRow.Cells("NUMREQT").Value
        cmbPrioridad.SelectedValue = dgvDatos.CurrentRow.Cells("SPRSTR").Value
        cmbMercaderia.SelectedValue = dgvDatos.CurrentRow.Cells("TIPOCNT").Value

        dtpFechaReg.Value = Date.Parse(dgvDatos.CurrentRow.Cells("FECREQ_S").Value.ToString.Trim)
        dtpHoraReg.Value = Date.Parse(dgvDatos.CurrentRow.Cells("HRAREQ_S").Value.ToString.Trim)

        dtpFechaAten.Value = Date.Parse(dgvDatos.CurrentRow.Cells("FCHATN_D").Value.ToString.Trim)
        dtpHoraAten.Value = Date.Parse(dgvDatos.CurrentRow.Cells("HRAATN_D").Value)

        txtPeso.Text = Val(dgvDatos.CurrentRow.Cells("QPSOMR").Value)
        'txtLargo.Text = Val(dgvDatos.CurrentRow.Cells("QMTLRG").Value)
        'txtAlto.Text = Val(dgvDatos.CurrentRow.Cells("QMTALT").Value)
        'txtAncho.Text = Val(dgvDatos.CurrentRow.Cells("QMTANC").Value)
        txtDireccionOrigen.Text = dgvDatos.CurrentRow.Cells("TDRCOR").Value.ToString.Trim
        txtDireccionDestino.Text = dgvDatos.CurrentRow.Cells("TDRENT").Value.ToString.Trim
        txtOS.Text = dgvDatos.CurrentRow.Cells("NORSRT").Value
        txtOSAgencia.Text = dgvDatos.CurrentRow.Cells("NDCORM").Value
        txtOSAgencia.Tag = dgvDatos.CurrentRow.Cells("PNCDTR").Value
        'If dgvDatos.CurrentRow.Cells("TIPSRV").Value.ToString.Trim = "R" Then
        '    chkReparto.Checked = True
        'Else
        '    chkTraslado.Checked = True
        'End If
        txtDocRef.Text = dgvDatos.CurrentRow.Cells("REFDO1").Value
        txtObservaciones.Text = dgvDatos.CurrentRow.Cells("TOBS").Value
        cboMedioTransporte.SelectedValue = IIf(dgvDatos.CurrentRow.Cells("CMEDTR").Value = 0D, 0D, dgvDatos.CurrentRow.Cells("CMEDTR").Value)
        txtCantidadSolicitada.Text = Val(dgvDatos.CurrentRow.Cells("QSLCIT").Value)
        If dgvDatos.CurrentRow.Cells("CLCLOR").Value > 0 Then
            txtLugarOrigen.Text = dgvDatos.CurrentRow.Cells("CLCLOR").Value & " -> " & dgvDatos.CurrentRow.Cells("CLCLOR_S").Value
            txtLugarOrigen.Tag = dgvDatos.CurrentRow.Cells("CLCLOR").Value
        Else
            txtLugarOrigen.Text = ""
            txtLugarOrigen.Tag = 0D
        End If
        If dgvDatos.CurrentRow.Cells("CLCLDS").Value > 0 Then
            txtLugarDestino.Text = dgvDatos.CurrentRow.Cells("CLCLDS").Value & " -> " & dgvDatos.CurrentRow.Cells("CLCLDS_S").Value
            txtLugarDestino.Tag = dgvDatos.CurrentRow.Cells("CLCLDS").Value
        Else
            txtLugarDestino.Text = ""
            txtLugarDestino.Tag = 0D
        End If

        txtUbigeoOrigen.Text = dgvDatos.CurrentRow.Cells("CUBORI_S").Value.ToString.Trim
        txtUbigeoDestino.Text = dgvDatos.CurrentRow.Cells("CUBDES_S").Value.ToString.Trim
        txtTipoUnidad.Text = dgvDatos.CurrentRow.Cells("CUNDVH").Value.ToString.Trim & " -> " & dgvDatos.CurrentRow.Cells("CUNDVH_D").Value.ToString.Trim
        'txtMercaderia.Text = dgvDatos.CurrentRow.Cells("CMRCDR_D").Value.ToString.Trim

        txtDescripcionMercaderia.Text = dgvDatos.CurrentRow.Cells("TOBERV").Value.ToString.Trim
        txtContacto.Text = dgvDatos.CurrentRow.Cells("PRSCNT").Value.ToString.Trim

        txtResponsable.Text = dgvDatos.CurrentRow.Cells("TIRALC").Value.ToString.Trim
        ''ucResponsable_CambioDeTexto(txtResponsable.Text)

        'Dim objOrdenServicio As New NEGOCIO.Operaciones.OrdenServicio_BLL

        'Dim Lista As New List(Of ENTIDADES.Operaciones.OrdenServicioTransporte)
        'Dim objParametros As New List(Of String)
        'objParametros.Add(dgvDatos.CurrentRow.Cells("NORSRT").Value)
        'objParametros.Add(CLng(dgvDatos.CurrentRow.Cells("CCLNT").Value))
        'objParametros.Add(dgvDatos.CurrentRow.Cells("CCMPN").Value)
        'objParametros.Add(dgvDatos.CurrentRow.Cells("CDVSN").Value)
        'objParametros.Add(dgvDatos.CurrentRow.Cells("CPLNDV").Value)
        'Lista = objOrdenServicio.Listar_Ordenes_Servicio(objParametros)
        'For Each Registro As ENTIDADES.Operaciones.OrdenServicioTransporte In Lista
        '    If Registro.PNTORG = dgvDatos.CurrentRow.Cells("CLCLOR").Value And Registro.PNTDES = dgvDatos.CurrentRow.Cells("CLCLDS").Value Then
        '        txtUbigeoOrigen.Text = IIf(Registro.CUBORI = 0, "", Registro.CUBORI & " -> " & Registro.UBIGEO_O)
        '        txtUbigeoDestino.Text = IIf(Registro.CUBDES = 0, "", Registro.CUBDES & " -> " & Registro.UBIGEO_O)
        '        txtTipoUnidad.Text = Registro.CTPUNV
        '        txtMercaderia.Text = Registro.CMRCDR
        '    End If
        'Next

        Entidad.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
        Entidad.NCSOTR = dgvDatos.CurrentRow.Cells("NORSRT").Value
        Entidad.CCLNT = dgvDatos.CurrentRow.Cells("CCLNT").Value
        Entidad.CMEDTR = IIf(dgvDatos.CurrentRow.Cells("CMEDTR").Value = 0D, 0D, dgvDatos.CurrentRow.Cells("CMEDTR").Value)
        Entidad.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
        Entidad.CDVSN = dgvDatos.CurrentRow.Cells("CDVSN").Value
        Entidad.CPLNDV = dgvDatos.CurrentRow.Cells("CPLNDV").Value
        Entidad.CUSCRT = dgvDatos.CurrentRow.Cells("CUSCRT").Value
        Entidad.SPRSTR = dgvDatos.CurrentRow.Cells("SPRSTR").Value

        ' Lista Dimensiones

        Dim ObjDimensiones As New Dimensiones
        Dim objDimensiones_BLL As New Dimensiones_BLL


        With ObjDimensiones
            .NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
            .CCMPN = cmbCompania.CCMPN_CodigoCompania
        End With

        dgvDimensiones.DataSource = objDimensiones_BLL.fListar_Dimensiones(ObjDimensiones)




    End Sub

    Private Sub dgvDatos_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDatos.CellContentClick
        If (dgvDatos.CurrentRow Is Nothing) Then
            Exit Sub
        End If
        Dim Columna As String = dgvDatos.Columns(e.ColumnIndex).Name
        Try
            If Columna = "QUNIDJUNTA" Then
                Dim ofrmVerUnidadesProgramadas As New frmVerUnidadesProgramadas
                ofrmVerUnidadesProgramadas.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
                ofrmVerUnidadesProgramadas.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
                ofrmVerUnidadesProgramadas.ShowDialog()
            ElseIf Columna = "QUNIDSOLICITADAS" Then
                Dim ofrmVerSolicitudes As New frmVerSolicitudes
                Dim Entidad As New Operaciones.Solicitud_Transporte
                Entidad.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
                Entidad.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
                ofrmVerSolicitudes.Entidad = Entidad
                ofrmVerSolicitudes.ShowDialog()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dgvDatos_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDatos.CellContentDoubleClick
        If (dgvDatos.CurrentRow Is Nothing) Then
            Exit Sub
        End If
        Dim Columna As String = dgvDatos.Columns(e.ColumnIndex).Name
        Try
            If Columna = "IMG_NMRGIM" AndAlso dgvDatos.CurrentRow.Cells("NMRGIM").Value > 0 Then
                Dim CCLNT As Decimal = dgvDatos.CurrentRow.Cells("CCLNT").Value
                Dim CCMPN As String = dgvDatos.CurrentRow.Cells("CCMPN").Value
                Dim NMRGIM As Decimal = dgvDatos.CurrentRow.Cells("NMRGIM").Value
                Dim TABLE_NAME As String = "RZST48"
                Dim USER_NAME As String = ""
                'Dim ofrmAdjuntarDocumentos As New frmAdjuntarDocumentos(NMRGIM, Nothing, CCLNT, TABLE_NAME, CCMPN, USER_NAME, frmAdjuntarDocumentos.EnumAdjunto.Requerimiento)
                'ofrmAdjuntarDocumentos.TipoModo = frmAdjuntarDocumentos.EnumModo.Edicion
                'ofrmAdjuntarDocumentos.ShowDialog()
                'Listar_Requerimiento_Servicio()
            ElseIf Columna = "IMG_ENVIO" AndAlso dgvDatos.CurrentRow.Cells("NROENV").Value > 0 Then
                Dim beSegEnvioReq As New SegEnvioRequerimiento
                beSegEnvioReq.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
                beSegEnvioReq.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
                Dim ofrmListaNotificacion As New frmListaNotificacion(beSegEnvioReq)
                ofrmListaNotificacion.ShowDialog()
            ElseIf Columna = "IMG_UNPROG" AndAlso dgvDatos.CurrentRow.Cells("SJTTR").Value = "X" Then
                Dim ofrmVerUnidadesProgramadas As New frmVerUnidadesProgramadas
                ofrmVerUnidadesProgramadas.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
                ofrmVerUnidadesProgramadas.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
                ofrmVerUnidadesProgramadas.ShowDialog()
            ElseIf Columna = "IMG_SOL" AndAlso dgvDatos.CurrentRow.Cells("SOLICT").Value > 0 Then
                Dim ofrmVerSolicitudes As New frmVerSolicitudes
                Dim Entidad As New Operaciones.Solicitud_Transporte
                Entidad.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
                Entidad.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
                ofrmVerSolicitudes.Entidad = Entidad
                ofrmVerSolicitudes.ShowDialog()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvDatos_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvDatos.SelectionChanged
        Try
            If dgvDatos.CurrentRow Is Nothing Then
                Exit Sub
            End If
            gEnumOpc = EnumMan.Neutro
            HabilitarOpcion(EnumMan.Neutro, TabRequerimiento.SelectedTab.Name)
            Estado_Controles(False)
            'HabilitarOpcionxEstadoRequerimiento()
            Limpiar_Controles()
            ''dgvPreAsignacion.DataSource = Nothing
            'dgvDatosSolicitud.DataSource = Nothing
            dgvDimensiones.DataSource = Nothing
            dtgRecursosAsignados.DataSource = Nothing
            CargarDatosRequerimiento()
            'Listar_Unidades_Programadas()
            'Listar_Solicitudes()
            Listar_Unidades_Solicitadas()
            ' TabRequerimiento_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub Listar_Unidades_Programadas()
    '    dgvPreAsignacion.DataSource = Nothing
    '    If dgvDatos.CurrentRow IsNot Nothing Then
    '        Dim Negocio As New RequerimientoServicio_BLL
    '        dgvPreAsignacion.AutoGenerateColumns = False
    '        Dim numRequerimiento As Decimal = dgvDatos.CurrentRow.Cells("NUMREQT").Value
    '        Dim ccmpnReq As String = dgvDatos.CurrentRow.Cells("CCMPN").Value
    '        dgvPreAsignacion.DataSource = Negocio.Lista_Unidades_Programadas(ccmpnReq, numRequerimiento, "T")
    '    End If
    'End Sub

    Private Sub Listar_Unidades_Solicitadas()
        dtgRecursosAsignados.DataSource = Nothing
        If dgvDatos.CurrentRow IsNot Nothing Then
            Dim objSolicitudTransporte As New RequerimientoServicio_BLL
            dtgRecursosAsignados.AutoGenerateColumns = False
            Dim objEntidad As New mantenimientos.ClaseGeneral

            objEntidad.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
            objEntidad.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
            dtgRecursosAsignados.DataSource = objSolicitudTransporte.Obtener_Detalle_Solicitud_Asignada_X_Requerimiento(objEntidad)

            ''Dim dwvrow As DataGridViewRow
            ''Dim Lista As New List(Of mantenimientos.ClaseGeneral)
            ''Lista = Nothing
            ''Me.dtgRecursosAsignados.Rows.Clear()
            ''dtgRecursosAsignados.DataSource = Lista

        End If
    End Sub


    'Private Sub Listar_Solicitudes()
    '    dgvDatosSolicitud.DataSource = Nothing
    '    If dgvDatos.CurrentRow IsNot Nothing Then
    '        Dim objSolicitudTransporte As New RequerimientoServicio_BLL
    '        Dim numRequerimiento As Decimal = dgvDatos.CurrentRow.Cells("NUMREQT").Value
    '        Dim objSolicitud As New Operaciones.Solicitud_Transporte
    '        objSolicitud.NUMREQT = numRequerimiento
    '        dgvDatosSolicitud.DataSource = objSolicitudTransporte.Listar_Solicitud_Transporte_Estado_Requerimiento(objSolicitud)
    '    End If
    'End Sub

    Private Sub btnBuscaOrdenServicio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscaOrdenServicio.Click
        If UcCliente_TxtF011.pCodigo > 0 Then
            Try

                Dim objFormBuscarOrdenServicio As New frmBuscarOrdenServicio
                With objFormBuscarOrdenServicio
                    .CodigoCliente = UcCliente_TxtF011.pCodigo
                    .CCMPN = cmbCompania.CCMPN_CodigoCompania
                    .CDVSN = cmbDivision.Division
                    .USUARIO = _pUsuario
                    .WindowState = FormWindowState.Maximized
                End With

                objFormBuscarOrdenServicio.ShowDialog()
                Activated()

                If objFormBuscarOrdenServicio.objOrdenServicioTransporteBE IsNot Nothing Then

                    txtOS.Text = objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.NORSRT

                    If objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CLCLOR.ToString.Trim = "0" Or objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CLCLOR.ToString.Trim = "" Then
                        txtLugarOrigen.Text = ""
                        txtLugarOrigen.Tag = 0
                    Else
                        txtLugarOrigen.Text = objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CLCLOR & " -> " & objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.PNTORG
                        txtLugarOrigen.Tag = objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CLCLOR

                    End If

                    If objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CLCLDS.ToString.Trim = "0" Or objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CLCLDS.ToString.Trim = "" Then
                        txtLugarDestino.Text = ""
                        txtLugarDestino.Tag = 0
                    Else
                        txtLugarDestino.Text = objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CLCLDS & " -> " & objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.PNTDES
                        txtLugarDestino.Tag = objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CLCLDS
                    End If

                    If objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CUBORI = 0 Or objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CUBORI.ToString.Trim = "" Then
                        txtUbigeoOrigen.Text = ""
                    Else
                        txtUbigeoOrigen.Text = objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CUBORI & " -> " & objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.UBIGEO_O
                    End If

                    If objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CUBDES = 0 Or objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CUBDES.ToString.Trim = "" Then
                        txtUbigeoDestino.Text = ""
                    Else
                        txtUbigeoDestino.Text = objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CUBDES & " -> " & objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.UBIGEO_D
                    End If

                    txtTipoUnidad.Text = objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CTPUNV
                    'txtMercaderia.Text = objFormBuscarOrdenServicio.objOrdenServicioTransporteBE.CMRCDR

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Debe seleccionar Cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub

    Private Sub btnAsignarOSAgencias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAsignarOSAgencias.Click
        Try

            Dim objfrmOSAgenciasRansa As New frmOperacionAgenciasRansaPopup
            objfrmOSAgenciasRansa.Codigo_Cliente = txtClienteFiltro.pCodigo
            objfrmOSAgenciasRansa.ShowDialog(Me)
            Me.txtOSAgencia.Text = objfrmOSAgenciasRansa.OrdenServio_AgenciasRansa
            txtOSAgencia.Tag = objfrmOSAgenciasRansa.Operacion_Agencias
            '_PNCDTR_Operacion = objfrmOSAgenciasRansa.Operacion_Agencias
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub Atender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAtender.Click

    '    Try
    '        Dim objFrm As New frmAtenderRequerimiento
    '        Dim Entidad As New RequerimientoServicio

    '        If dgvDatos.CurrentRow IsNot Nothing Then

    '            With Entidad

    '                .NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
    '                .CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
    '                .CDVSN = dgvDatos.CurrentRow.Cells("CDVSN").Value
    '                .CPLNDV = dgvDatos.CurrentRow.Cells("CPLNDV").Value
    '                .CCLNT = dgvDatos.CurrentRow.Cells("CCLNT").Value
    '                .CCLNT_S = dgvDatos.CurrentRow.Cells("CCLNT_S").Value

    '                .NORSRT = dgvDatos.CurrentRow.Cells("NORSRT").Value
    '                .NDCORM = dgvDatos.CurrentRow.Cells("NDCORM").Value
    '                .PNCDTR = dgvDatos.CurrentRow.Cells("PNCDTR").Value
    '                .SESREQ = "P"
    '                .SPRSTR = dgvDatos.CurrentRow.Cells("SPRSTR").Value
    '                .SPRSTR_S = dgvDatos.CurrentRow.Cells("SPRSTR_S").Value
    '                .TIPOCNT = dgvDatos.CurrentRow.Cells("TIPOCNT").Value
    '                .TIPOCNT_S = dgvDatos.CurrentRow.Cells("TIPOCNT_S").Value

    '                .TIPSRV = dgvDatos.CurrentRow.Cells("TIPSRV").Value
    '                .TIPSRV_S = dgvDatos.CurrentRow.Cells("TIPSRV_S").Value

    '                .FECREQ = CDec(String.Format("{0:yyyyMMdd}", CDate(dgvDatos.CurrentRow.Cells("FECREQ_S").Value)))
    '                .FECREQ_S = dgvDatos.CurrentRow.Cells("FECREQ_S").Value.ToString.Trim

    '                .HRAREQ = CDec(String.Format("{0:HHmmss}", CDate(dgvDatos.CurrentRow.Cells("HRAREQ_S").Value)))
    '                .HRAREQ_S = dgvDatos.CurrentRow.Cells("HRAREQ_S").Value.ToString.Trim

    '                .FCHATN = CDec(String.Format("{0:yyyyMMdd}", CDate(dgvDatos.CurrentRow.Cells("FCHATN_D").Value)))
    '                .FCHATN_D = dgvDatos.CurrentRow.Cells("FCHATN_D").Value

    '                .HRAATN = CDec(String.Format("{0:HHmmss}", CDate(dgvDatos.CurrentRow.Cells("HRAATN_D").Value)))
    '                .HRAATN_D = dgvDatos.CurrentRow.Cells("HRAATN_D").Value

    '                .QPSOMR = dgvDatos.CurrentRow.Cells("QPSOMR").Value
    '                .QMTLRG = dgvDatos.CurrentRow.Cells("QMTLRG").Value
    '                .QMTALT = dgvDatos.CurrentRow.Cells("QMTALT").Value
    '                .QMTANC = dgvDatos.CurrentRow.Cells("QMTANC").Value
    '                .REFDO1 = dgvDatos.CurrentRow.Cells("REFDO1").Value
    '                .TOBS = dgvDatos.CurrentRow.Cells("TOBS").Value

    '                .CLCLOR = dgvDatos.CurrentRow.Cells("CLCLOR").Value
    '                .CLCLOR_S = dgvDatos.CurrentRow.Cells("CLCLOR_S").Value
    '                .CLCLDS = dgvDatos.CurrentRow.Cells("CLCLDS").Value
    '                .CLCLDS_S = dgvDatos.CurrentRow.Cells("CLCLDS_S").Value

    '                .TDRCOR = dgvDatos.CurrentRow.Cells("TDRCOR").Value.ToString.Trim
    '                .TDRENT = dgvDatos.CurrentRow.Cells("TDRENT").Value.ToString.Trim

    '                .CMEDTR = dgvDatos.CurrentRow.Cells("CMEDTR").Value
    '                .QSLCIT = dgvDatos.CurrentRow.Cells("QSLCIT").Value

    '                .CMRCDR = dgvDatos.CurrentRow.Cells("CMRCDR").Value
    '                .CTPOSR = dgvDatos.CurrentRow.Cells("CTPOSR").Value.ToString.Trim
    '                '.CUNDVH = dgvDatos.CurrentRow.Cells("CUNDVH_D").Value
    '                .CUNDMD = dgvDatos.CurrentRow.Cells("CUNDMD").Value.ToString.Trim
    '                .QMRCDR = dgvDatos.CurrentRow.Cells("QMRCDR_2").Value

    '                .SESREQ = dgvDatos.CurrentRow.Cells("SESREQ").Value.ToString.Trim

    '            End With

    '            objFrm.Entidad = Entidad
    '            objFrm.pUsuario = MainModule.USUARIO

    '            objFrm.ShowDialog()
    '            If objFrm.myDialogResult = True Then
    '                Buscar(Nothing, Nothing)
    '            End If

    '            'If objFrm.Respuesta = "SI" Then
    '            '    Buscar(Nothing, Nothing)
    '            'End If

    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub




    'Private Sub TabRequerimiento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabRequerimiento.SelectedIndexChanged
    '    Try
    '        If TabSelect <> TabRequerimiento.SelectedTab.Name Then
    '            If (gEnumOpc = Mantenimiento.Modificar Or gEnumOpc = Mantenimiento.Nuevo) Then
    '                TabRequerimiento.SelectTab(TabSelect)
    '                MessageBox.Show("Debe guardar o cancelar la acci�n", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Sub
    '            ElseIf gEnumOpc = Mantenimiento.Neutro Then
    '                gEnumOpc = Mantenimiento.Neutro
    '                'HabilitarOpcion(Mantenimiento.Neutro, TabSolicitudFlete.SelectedTab.Name)
    '                '  HabilitarOpcionxEstadoSolicitud()
    '            End If
    '        End If
    '        TabSeleccionado = TabSolicitudFlete.SelectedTab.Name
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    'Private Sub HabilitarOpcionxEstadoRequerimiento()
    '    'Dim Estado_Actual_Eliminar As Boolean = Me.btnEliminar.Enabled
    '    Dim TabSelect As String = TabRequerimiento.SelectedTab.Name
    '    Dim EstadoReq As String = ""
    '    If dgvDatos.Rows.Count > 0 Then
    '        EstadoReq = ("" & dgvDatos.CurrentRow.Cells("SESREQ").Value).ToString.Trim
    '    End If
    '    Select Case EstadoReq
    '        'Case "P"
    '        '    Select Case TabSelect
    '        '        Case "TabDatosRequerimiento"
    '        '            btnModificar.Enabled = False
    '        '            btnGuardar.Visible = False
    '        '            btnEliminar.Enabled = False
    '        '            btnNuevo.Enabled = False
    '        '    End Select
    '        'Case "A"
    '        '    Select Case TabSelect
    '        '        Case "TabDatosRequerimiento"

    '        '            btnModificar.Enabled = False
    '        '            btnEliminar.Enabled = False
    '        '            btnNuevo.Enabled = False
    '        '            btnAtender.Enabled = False
    '        '            btnCancelar.Enabled = False

    '        '    End Select
    '        Case "C"
    '            Select Case TabSelect
    '                Case "TabDatosRequerimiento"
    '                    btnModificar.Enabled = False
    '                    btnCerrarReq.Enabled = False
    '                    btnEliminar.Enabled = False
    '                    btnAtender.Enabled = False
    '            End Select
    '        Case "A"
    '            Select Case TabSelect
    '                Case "TabDatosRequerimiento"
    '                    btnModificar.Enabled = False
    '                    btnCerrarReq.Enabled = True
    '                    btnEliminar.Enabled = False
    '                    btnAtender.Enabled = True
    '            End Select
    '    End Select

    'End Sub

    Private Sub TabRequerimiento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabRequerimiento.SelectedIndexChanged
        Try
            If TabSeleccionado <> TabRequerimiento.SelectedTab.Name Then
                If (gEnumOpc = EnumMan.Editar Or gEnumOpc = EnumMan.Nuevo) Then
                    TabRequerimiento.SelectTab(TabSeleccionado)
                    MessageBox.Show("Debe guardar o cancelar la acci�n", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                ElseIf gEnumOpc = EnumMan.Neutro Then
                    gEnumOpc = EnumMan.Neutro
                    HabilitarOpcion(EnumMan.Neutro, TabRequerimiento.SelectedTab.Name)
                    'HabilitarOpcionxEstadoRequerimiento()
                End If
            End If
            TabSeleccionado = TabRequerimiento.SelectedTab.Name
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    'Private Sub TabRequerimiento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabRequerimiento.SelectedIndexChanged

    '    Try

    '        If dgvDatos.CurrentRow IsNot Nothing Then

    '            Select Case dgvDatos.CurrentRow.Cells("SESREQ").Value.ToString.Trim

    '                Case "P"

    '                    Select Case TabRequerimiento.SelectedTab.Name

    '                        Case "TabDatosRequerimiento"
    '                            If Estado = Mantenimiento.Modificar Or Estado = Mantenimiento.Nuevo Then
    '                                Exit Select
    '                            End If
    '                            Habilitar_Botones(Estado)
    '                            Habilitar_Controles(Estado)
    '                            'Habilitar_Botones(Mantenimiento.Neutro)

    '                        Case Else

    '                            If Estado = Mantenimiento.Modificar Or Estado = Mantenimiento.Nuevo Then
    '                                MessageBox.Show("Debe guardar o cancelar la acci�n", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                                TabRequerimiento.SelectTab("TabDatosRequerimiento")
    '                                Exit Select
    '                            End If
    '                            btnModificar.Enabled = False
    '                            btnEliminar.Enabled = False
    '                            btnNuevo.Enabled = False

    '                    End Select

    '                Case "A"

    '                    Select Case TabRequerimiento.SelectedTab.Name

    '                        Case "TabDatosRequerimiento"
    '                            If Estado = Mantenimiento.Modificar Or Estado = Mantenimiento.Nuevo Then
    '                                Exit Select
    '                            End If
    '                            Habilitar_Botones_A(Estado)
    '                            Habilitar_Controles(Estado)
    '                            'Habilitar_Botones(Mantenimiento.Neutro)
    '                            btnEliminar.Enabled = False

    '                        Case Else

    '                            If Estado = Mantenimiento.Modificar Or Estado = Mantenimiento.Nuevo Then
    '                                MessageBox.Show("Debe guardar o cancelar la acci�n", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                                TabRequerimiento.SelectTab("TabDatosRequerimiento")
    '                                Exit Select
    '                            End If
    '                            btnModificar.Enabled = False
    '                            btnEliminar.Enabled = False
    '                            btnNuevo.Enabled = False

    '                    End Select


    '                Case "C"

    '                    btnModificar.Enabled = False
    '                    btnEliminar.Enabled = False
    '                    btnNuevo.Enabled = False
    '                    btnAtender.Enabled = False
    '                    btnCancelar.Enabled = False

    '            End Select

    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    Private Sub dgvDatosSolicitud_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDatosSolicitud.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Exit Sub
            If dgvDatosSolicitud.Columns(e.ColumnIndex).Name = "IMAGEN" Then
                Dim ObjFrm As New frmVerUnidadesAsignadas
                Dim Entidad As New RequerimientoServicio
                Entidad.NCSOTR = dgvDatosSolicitud.CurrentRow.Cells("NCSOTR").Value
                Entidad.CCMPN = cmbCompania.CCMPN_CodigoCompania
                ObjFrm.Entidad = Entidad
                ObjFrm.ShowDialog()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvDatosSolicitud_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgvDatosSolicitud.DataBindingComplete
        Try
            For Each Item As DataGridViewRow In dgvDatosSolicitud.Rows
                If (Item.Cells("UNIDADES").Value > 0) Then
                    Item.Cells("IMAGEN").Value = My.Resources.text_block
                Else
                    Item.Cells("IMAGEN").Value = My.Resources.CuadradoBlanco
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkFechaAtencion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFechaAtencion.CheckedChanged
        Try
            dtpFechaInicioAtencion.Enabled = chkFechaAtencion.Checked
            dtpFechaFinAtencion.Enabled = chkFechaAtencion.Checked

            dtpHoraIniAtencion.Enabled = chkFechaAtencion.Checked
            dtpHoraFinAtencion.Enabled = chkFechaAtencion.Checked
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkNumReq_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNumReq.CheckedChanged
        Try

            dtpFechaInicioReq.Enabled = Not chkNumReq.Checked
            dtpFechaFinReq.Enabled = Not chkNumReq.Checked
            txtNroReqFiltro.Enabled = chkNumReq.Checked
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkSolicitud_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSolicitud.CheckedChanged
        Try

            txtSolicitudFiltro.Enabled = chkSolicitud.Checked

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Ver_Medidas(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMedida.Click
        Try

            If dgvDatos.Rows.Count = 0 Then
                Exit Sub
            End If

            If Val(txtNroReq.Text) = 0 Then
                MessageBox.Show("Debe registrar el requerimiento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim objSolicitud As New Operaciones.Solicitud_Transporte
            Dim objSolicitudTransporte As New RequerimientoServicio_BLL
            Dim Respuesta As New List(Of Operaciones.Solicitud_Transporte)
            Dim numRequerimiento As Decimal = dgvDatos.CurrentRow.Cells("NUMREQT").Value
            objSolicitud.NUMREQT = numRequerimiento
            objSolicitud.CCMPN = Me.cmbCompania.CCMPN_CodigoCompania
            Respuesta = objSolicitudTransporte.Listar_Estado_Solicitud_Requerimiento(objSolicitud)
            If Respuesta.Count > 0 Then
                If Respuesta.Item(0).SESREQ = "A" Or Respuesta.Item(0).SESREQ = "C" Or Respuesta.Item(0).SJTTR = "X" Then
                    MessageBox.Show("No se puede agregar medidas. El Requerimiento est� en Atenci�n, Cerrado y/o en Junta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If

            'If dgvDatosSolicitud.Rows.Count > 0 Then
            '    MessageBox.Show("No se pueden agregar medidas, existen solicitudes asignadas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If

            Dim ObjFrm As New frmDimensiones
            ObjFrm.pUsuario = _pUsuario
            ObjFrm.pCCMPN = cmbCompania.CCMPN_CodigoCompania
            ObjFrm.pNUMREQT = Val(txtNroReq.Text)

            ObjFrm.ShowDialog()
            dgvDatos_SelectionChanged(Nothing, Nothing)


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub chkFechaRequerimiento_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFechaRequerimiento.CheckedChanged
        Try
            dtpFechaInicioReq.Enabled = chkFechaRequerimiento.Checked
            dtpFechaFinReq.Enabled = chkFechaRequerimiento.Checked
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ucResponsable_CambioDeTexto(ByVal objData As Object) Handles ucResponsable.CambioDeTexto

        If ucResponsable.Resultado IsNot Nothing Then
            txtResponsable.Text = CType(ucResponsable.Resultado, beDestinatario).PSTNMYAP
            txtEmail.Text = CType(ucResponsable.Resultado, beDestinatario).PSEMAILTO
        End If
    End Sub

    Private Sub txtResponsable_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtResponsable.TextChanged

        txtEmail.Text = ""

    End Sub

    Private Sub txtNroReqFiltro_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroReqFiltro.KeyPress
        Try
            e.Handled = Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = ControlChars.Back)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSolicitudFiltro_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSolicitudFiltro.KeyPress
        Try
            e.Handled = Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = ControlChars.Back)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAdjuntar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjuntar.Click
        If dgvDatos.CurrentCellAddress.Y < 0 Then
            Exit Sub
        End If
        If (dgvDatos.CurrentRow Is Nothing) Then
            Exit Sub
        End If
        Try

            Dim CCMPN As String = dgvDatos.CurrentRow.Cells("CCMPN").Value
            'Dim NUMREQ As String = dgvDatos.CurrentRow.Cells("NUMREQT").Value
            Dim NMRGIM As Decimal = dgvDatos.CurrentRow.Cells("NMRGIM").Value
            Dim CCLNT As String = dgvDatos.CurrentRow.Cells("CCLNT").Value

            Dim TABLE_NAME As String = "RZST48"
            'Dim USER_NAME As String = _pUsuario
            'Dim ofrmAdjuntarDocumentos As New frmAdjuntarDocumentos(NMRGIM, Nothing, CCLNT, TABLE_NAME, CCMPN, _pUsuario, frmAdjuntarDocumentos.EnumAdjunto.Requerimiento)
            'ofrmAdjuntarDocumentos.pNUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
            'ofrmAdjuntarDocumentos.TipoModo = frmAdjuntarDocumentos.EnumModo.Edicion
            'ofrmAdjuntarDocumentos.ShowDialog()

            Dim objRequerimientoServicio As New RequerimientoServicio_BLL
            Dim olstRequerimientoServicio As New List(Of RequerimientoServicio)
            Dim objEntidad As New RequerimientoServicio
            objEntidad.CCMPN = dgvDatos.CurrentRow.Cells("CCMPN").Value
            objEntidad.NUMREQT = dgvDatos.CurrentRow.Cells("NUMREQT").Value
            olstRequerimientoServicio = objRequerimientoServicio.Obtener_NroImagen_X_Requerimiento_Servicio(objEntidad)
            If olstRequerimientoServicio.Count > 0 Then
                If olstRequerimientoServicio.Item(0).NUMREQT > 0 And olstRequerimientoServicio.Item(0).NMRGIM > 0 Then
                    dgvDatos.CurrentRow.Cells("NMRGIM").Value = olstRequerimientoServicio.Item(0).NMRGIM
                    dgvDatos.CurrentRow.Cells("IMG_NMRGIM").Value = My.Resources.text_block
                ElseIf olstRequerimientoServicio.Item(0).NUMREQT > 0 And olstRequerimientoServicio.Item(0).NMRGIM = 0 Then
                    dgvDatos.CurrentRow.Cells("NMRGIM").Value = olstRequerimientoServicio.Item(0).NMRGIM
                    dgvDatos.CurrentRow.Cells("IMG_NMRGIM").Value = My.Resources.CuadradoBlanco
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private beSegEnvioReq As New SegEnvioRequerimiento
    Private Sub btnEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        If dgvDatos.CurrentCellAddress.Y < 0 Then
            Exit Sub
        End If
        If (dgvDatos.CurrentRow Is Nothing) Then
            Exit Sub
        End If

        Try

            Dim NUMREQT As String = dgvDatos.CurrentRow.Cells("NUMREQT").Value
            Dim CCMPN As String = dgvDatos.CurrentRow.Cells("CCMPN").Value

            Dim ofrmEnviarNotificacion As New frmEnviarNotificacion(NUMREQT, CCMPN)
            'ofrmEnviarNotificacion.ShowDialog()

            If ofrmEnviarNotificacion.ShowDialog = Windows.Forms.DialogResult.Yes Then
                dgvDatos.CurrentRow.Cells("NROENV").Value = 1
                dgvDatos.CurrentRow.Cells("IMG_ENVIO").Value = My.Resources.text_block
                beSegEnvioReq = New SegEnvioRequerimiento
                beSegEnvioReq = ofrmEnviarNotificacion.beSegEnvioReq
                oHebraEnvioEmailReqServ = New Thread(AddressOf Enviar_Requerimiento_Servicio_Notificacion)
                oHebraEnvioEmailReqServ.Start()
                'Enviar_Requerimiento_Servicio_Notificacion(ofrmEnviarNotificacion.beSegEnvioReq) '(NUMREQT, CCMPN, ofrmEnviarNotificacion.EMAIL_RESP)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Enviar_Requerimiento_Servicio_Notificacion() 'ByVal NUMREQT As String, ByVal CCMPN As String, ByVal EMAIL_RESP As String)
        Try
            Dim Entidad As New RequerimientoServicio
            Control.CheckForIllegalCrossThreadCalls = False
            Dim Envio As New RequerimientoServicioEnvio_BLL
            Envio.CULUSA = _pUsuario
            Envio.MailAccount = _pMailAccount ''System.Configuration.ConfigurationManager.AppSettings("MailFrom")
            Envio.Mailpassword = _pMailpassword ''System.Configuration.ConfigurationManager.AppSettings("MailFromClave")
            Envio.MailAccount_Gmail = _pMailAccount_Gmail ''System.Configuration.ConfigurationManager.AppSettings("MailCO")
            Envio.MailPassword_Gmail = _pMailPassword_Gmail ''System.Configuration.ConfigurationManager.AppSettings("MailCOClave")
            Envio.Mailto_Error = _pMailto_Error ''System.Configuration.ConfigurationManager.AppSettings("emailto_error")
            ' Entidad.NUMREQT = Num_RequerimientoEnvio

            If Envio.Enviar_Email_Requerimiento_Servicio_Notificacion(beSegEnvioReq) = 1 Then 'NUMREQT, CCMPN, EMAIL_RESP
                beSegEnvioReq.CUSCRT = _pUsuario
                beSegEnvioReq.NTRMCR = Environment.MachineName
                Dim obeSegEnvReq As New SegEnvioRequerimiento
                obeSegEnvReq = Envio.Registrar_Envio_Requerimiento_Servicio(beSegEnvioReq)
                'If obeSegEnvReq.NUMREQT > 0 And beSegEnvioReq.NCRRSG > 0 Then
                '    'dgvDatos.CurrentRow.Cells("NROENV").Value = beSegEnvioReq.NCRRSG
                '    'dgvDatos.CurrentRow.Cells("IMG_ENVIO").Value = My.Resources.text_block
                'End If
                MessageBox.Show("Correo enviado", "Envio email", MessageBoxButtons.OK)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function Obtener_Filtro() As RequerimientoServicio
        Dim Entidad As New RequerimientoServicio
        'Dim Negocio As New RequerimientoServicio_BLL
        'Dim Mensaje As String = ""

        'If Lista_Negocios.ToString.Trim = "" Then
        '    Mensaje = Mensaje & "Seleccione un Negocio"
        'End If

        'If Mensaje.ToString.Trim.Length > 0 Then
        '    MessageBox.Show(Mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Return Entidad
        '    Exit Function
        'End If

        Entidad.CCMPN = cmbCompania.CCMPN_CodigoCompania
        Entidad.CDVSN = cmbDivision.Division
        Entidad.CPLNDV = cmbPlanta.Planta
        Entidad.SESREQ = cmbEstado.SelectedValue
        Entidad.CCLNT = txtClienteFiltro.pCodigo
        Entidad.SPRSTR = cmbPrioridadFiltro.SelectedValue

        If chkFechaRequerimiento.Checked = True Then
            Entidad.FECREQ_INI = CDec(String.Format("{0:yyyyMMdd}", dtpFechaInicioReq.Value))
        Else
            Entidad.FECREQ_INI = 0D
        End If

        If chkFechaRequerimiento.Checked = True Then
            Entidad.FECREQ_FIN = CDec(String.Format("{0:yyyyMMdd}", dtpFechaFinReq.Value))
        Else
            Entidad.FECREQ_FIN = 0D
        End If

        If chkHora.Checked = True Then
            Entidad.HRAREQ_INI = CDec(String.Format("{0:HHmmss}", dtpHoraIniReq.Value))
            Entidad.HRAREQ_FIN = CDec(String.Format("{0:HHmmss}", dtpHoraFinReq.Value))
        Else
            Entidad.HRAREQ_INI = 0D
            Entidad.HRAREQ_FIN = 0D
        End If

        'FECHA DE REQUERIMIENTO ATENDIDO

        If chkFechaAtencion.Checked = True Then
            Entidad.FCHATN_INI = CDec(String.Format("{0:yyyyMMdd}", dtpFechaInicioAtencion.Value))
            Entidad.FCHATN_FIN = CDec(String.Format("{0:yyyyMMdd}", dtpFechaFinAtencion.Value))
        Else
            Entidad.FCHATN_INI = 0D
            Entidad.FCHATN_FIN = 0D
        End If

        If chkHoraAtencion.Checked = True Then
            Entidad.HRAATN_INI = CDec(String.Format("{0:HHmmss}", dtpHoraIniAtencion.Value))
            Entidad.HRAATN_FIN = CDec(String.Format("{0:HHmmss}", dtpHoraFinAtencion.Value))
        Else
            Entidad.HRAATN_INI = 0D
            Entidad.HRAATN_FIN = 0D
        End If

        If chkNumReq.Checked = True Then
            Entidad.NUMREQT = CDec(Val(txtNroReqFiltro.Text))
        Else
            Entidad.NUMREQT = 0D
        End If

        If chkSolicitud.Checked = True Then
            Entidad.NCSOTR = CDec(Val(txtSolicitudFiltro.Text))
        Else
            Entidad.NCSOTR = 0D
        End If

        Entidad.CRGVTA = Lista_Negocios()
        If chkEnJunta.Checked = True Then
            Entidad.SJTTR = "X"
        Else
            Entidad.SJTTR = "T"
        End If
        Return Entidad
    End Function

    Private Function Listar_Requerimiento_Servicio_Exportar_Detalle() As DataTable
        Dim odtReqServ As New DataTable
        Try
            Dim Entidad As New RequerimientoServicio
            Dim Negocio As New RequerimientoServicio_BLL
            Dim Mensaje As String = ""


            Entidad.CCMPN = cmbCompania.CCMPN_CodigoCompania
            Entidad.CDVSN = cmbDivision.Division
            Entidad.CPLNDV = cmbPlanta.Planta
            Entidad.SESREQ = cmbEstado.SelectedValue
            Entidad.CCLNT = txtClienteFiltro.pCodigo
            Entidad.SPRSTR = cmbPrioridadFiltro.SelectedValue

            If chkFechaRequerimiento.Checked = True Then
                Entidad.FECREQ_INI = CDec(String.Format("{0:yyyyMMdd}", dtpFechaInicioReq.Value))
            Else
                Entidad.FECREQ_INI = 0D
            End If

            If chkFechaRequerimiento.Checked = True Then
                Entidad.FECREQ_FIN = CDec(String.Format("{0:yyyyMMdd}", dtpFechaFinReq.Value))
            Else
                Entidad.FECREQ_FIN = 0D
            End If

            If chkHora.Checked = True Then
                Entidad.HRAREQ_INI = CDec(String.Format("{0:HHmmss}", dtpHoraIniReq.Value))
                Entidad.HRAREQ_FIN = CDec(String.Format("{0:HHmmss}", dtpHoraFinReq.Value))
            Else
                Entidad.HRAREQ_INI = 0D
                Entidad.HRAREQ_FIN = 0D
            End If

            'FECHA DE REQUERIMIENTO ATENDIDO

            If chkFechaAtencion.Checked = True Then
                Entidad.FCHATN_INI = CDec(String.Format("{0:yyyyMMdd}", dtpFechaInicioAtencion.Value))
                Entidad.FCHATN_FIN = CDec(String.Format("{0:yyyyMMdd}", dtpFechaFinAtencion.Value))
            Else
                Entidad.FCHATN_INI = 0D
                Entidad.FCHATN_FIN = 0D
            End If

            If chkHoraAtencion.Checked = True Then
                Entidad.HRAATN_INI = CDec(String.Format("{0:HHmmss}", dtpHoraIniAtencion.Value))
                Entidad.HRAATN_FIN = CDec(String.Format("{0:HHmmss}", dtpHoraFinAtencion.Value))
            Else
                Entidad.HRAATN_INI = 0D
                Entidad.HRAATN_FIN = 0D
            End If

            If chkNumReq.Checked = True Then
                Entidad.NUMREQT = CDec(Val(txtNroReqFiltro.Text))
            Else
                Entidad.NUMREQT = 0D
            End If

            If chkSolicitud.Checked = True Then
                Entidad.NCSOTR = CDec(Val(txtSolicitudFiltro.Text))
            Else
                Entidad.NCSOTR = 0D
            End If

            Entidad.CRGVTA = Lista_Negocios()
            If chkEnJunta.Checked = True Then
                Entidad.SJTTR = "X"
            Else
                Entidad.SJTTR = "T"
            End If


            odtReqServ = Negocio.Lista_Requerimiento_Servicio_Detalle_Exportar(Obtener_Filtro)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return odtReqServ

    End Function

    Private Sub btnExportarGeneral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportarGeneral.Click
        Try

            If dgvDatos.Rows.Count = 0 Then
                Exit Sub
            End If
            Dim Mensaje As String = ""
            If Lista_Negocios.ToString.Trim = "" Then
                Mensaje = Mensaje & "Seleccione un Negocio"
            End If

            If Mensaje.ToString.Trim.Length > 0 Then
                MessageBox.Show(Mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Exit Sub
            End If


            Dim NPOI_SC As New Ransa.Utilitario.HelpClass_NPOI_SC

            Dim dtExcel As New DataTable
            Dim dr As DataRow

            dtExcel.Columns.Add("NUMREQT").Caption = NPOI_SC.FormatDato("Nro. Req.", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TCRVTA").Caption = NPOI_SC.FormatDato("Negocio", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("NORSRT").Caption = NPOI_SC.FormatDato("O/S", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CLCLOR_S").Caption = NPOI_SC.FormatDato("Lugar Origen", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CLCLDS_S").Caption = NPOI_SC.FormatDato("Lugar Destino", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CCLNT_S").Caption = NPOI_SC.FormatDato("Cliente", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CCLNFC_S").Caption = NPOI_SC.FormatDato("Cliente (Facturaci�n)", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TIPOCNT_S").Caption = NPOI_SC.FormatDato("Tipo Mercader�a", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("FECREQ").Caption = NPOI_SC.FormatDato("Fecha Req.", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("HRAREQ").Caption = NPOI_SC.FormatDato("Hora Req.", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("FCHATN").Caption = NPOI_SC.FormatDato("Fecha para Atenci�n", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("HRAATN").Caption = NPOI_SC.FormatDato("Hora para Atenci�n", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("SPRSTR_S").Caption = NPOI_SC.FormatDato("Prioridad", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("QSLCIT").Caption = NPOI_SC.FormatDato("Cant. Veh�culos", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("QUNIDJUNTA").Caption = NPOI_SC.FormatDato("Unid. Programadas (Junta)", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("QUNIDSOLICITADAS").Caption = NPOI_SC.FormatDato("Unid. Solicitadas", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("QATNAN").Caption = NPOI_SC.FormatDato("Unid. Atendidas", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("TNMMDT").Caption = NPOI_SC.FormatDato("Medio Transporte", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TOBERV").Caption = NPOI_SC.FormatDato("Descripci�n Mercader�a", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("REFDO1").Caption = NPOI_SC.FormatDato("Doc. Referencia", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("QPSOMR").Caption = NPOI_SC.FormatDato("Peso total (kg)", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("TDRCOR").Caption = NPOI_SC.FormatDato("Direcci�n Origen", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TDRENT").Caption = NPOI_SC.FormatDato("Direcci�n Destino", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TIRALC").Caption = NPOI_SC.FormatDato("Responsable", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("PRSCNT").Caption = NPOI_SC.FormatDato("Contacto", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("SESREQ_S").Caption = NPOI_SC.FormatDato("Estado Req.", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TOBS").Caption = NPOI_SC.FormatDato("Observaciones", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CUSCRT").Caption = NPOI_SC.FormatDato("Usuario Creador", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CULUSA").Caption = NPOI_SC.FormatDato("Usuario Actualizador", NPOI_SC.keyDatoTexto)
            'dtExcel.Columns.Add("NORSRTSR").Caption = NPOI_SC.FormatDato("O/S Sol. Req.", NPOI_SC.keyDatoTexto)
            ''dtExcel.Columns.Add("CLCLORSR_S").Caption = NPOI_SC.FormatDato("Lugar Origen Sol. Req.", NPOI_SC.keyDatoTexto)
            ''dtExcel.Columns.Add("CLCLDSSR_S").Caption = NPOI_SC.FormatDato("Lugar Destino Sol. Req.", NPOI_SC.keyDatoTexto)
            ''dtExcel.Columns.Add("NDCORMSR").Caption = NPOI_SC.FormatDato("Nro. Doc. Origen Sol. Req.", NPOI_SC.keyDatoTexto)
            ''dtExcel.Columns.Add("PNCDTRSR").Caption = NPOI_SC.FormatDato("Nro. Ope. Agencias Sol. Req.", NPOI_SC.keyDatoTexto)


            Dim columna As String = ""
            Dim odtReqServ As New DataTable
            Dim Negocio As New RequerimientoServicio_BLL

            odtReqServ = Negocio.Lista_Requerimiento_Servicio(Obtener_Filtro)
            CopiarDatosTo(odtReqServ, dtExcel)


            Dim ListaDatatable As New List(Of DataTable)
            dtExcel.TableName = "REQUERIMIENTO_SERVICIO"
            ListaDatatable.Add(dtExcel.Copy)

            Dim ListaTitulos As New List(Of String)
            ListaTitulos.Add("LISTA DE REQUERIMIENTOS DE SERVICIO")

            Dim oLisFiltro As New List(Of SortedList)
            Dim F As New SortedList
            F.Add(0, "Compa�ia:| " & cmbCompania.CCMPN_Descripcion)
            F.Add(1, "Divisi�n:|" & cmbDivision.DivisionDescripcion)
            F.Add(2, "Planta:| " & cmbPlanta.DescripcionPlanta)


            oLisFiltro.Add(F)

            NPOI_SC.ExportExcelGeneralReleaseMultiple(ListaDatatable, ListaTitulos, Nothing, oLisFiltro, 0)


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExportarDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportarDetalle.Click
        Try

            If dgvDatos.Rows.Count = 0 Then
                Exit Sub
            End If

            Dim NPOI_SC As New Ransa.Utilitario.HelpClass_NPOI_SC

            Dim dtExcel As New DataTable
            Dim dr As DataRow

            dtExcel.Columns.Add("NUMREQT").Caption = NPOI_SC.FormatDato("Nro. Req.", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("NORSRT").Caption = NPOI_SC.FormatDato("O/S", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TCRVTA").Caption = NPOI_SC.FormatDato("Negocio", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CLCLOR_S").Caption = NPOI_SC.FormatDato("Lugar Origen", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CLCLDS_S").Caption = NPOI_SC.FormatDato("Lugar Destino", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CCLNT_S").Caption = NPOI_SC.FormatDato("Cliente", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("CCLNFC_S").Caption = NPOI_SC.FormatDato("Cliente (Facturaci�n)", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TIPOCNT_S").Caption = NPOI_SC.FormatDato("Tipo Mercader�a", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("FECREQ").Caption = NPOI_SC.FormatDato("Fecha Req.", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("HRAREQ").Caption = NPOI_SC.FormatDato("Hora Req.", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("FCHATN").Caption = NPOI_SC.FormatDato("Fecha para Atenci�n", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("HRAATN").Caption = NPOI_SC.FormatDato("Hora para Atenci�n", NPOI_SC.keyDatoFecha)
            dtExcel.Columns.Add("SPRSTR_S").Caption = NPOI_SC.FormatDato("Prioridad", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("QSLCIT").Caption = NPOI_SC.FormatDato("Cant. Veh�culos", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("QUNIDJUNTA").Caption = NPOI_SC.FormatDato("Unid. Programadas (Junta)", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("QUNIDSOLICITADAS").Caption = NPOI_SC.FormatDato("Unid. Solicitadas", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("QUNIDATENDIDAS").Caption = NPOI_SC.FormatDato("Unid. Atendidas", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("TNMMDT_1").Caption = NPOI_SC.FormatDato("Medio Transporte", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TOBERV").Caption = NPOI_SC.FormatDato("Descripci�n Mercader�a", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("REFDO1").Caption = NPOI_SC.FormatDato("Doc. Referencia", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("QPSOMR").Caption = NPOI_SC.FormatDato("Peso total (kg)", NPOI_SC.keyDatoNumero)
            dtExcel.Columns.Add("TDRCOR").Caption = NPOI_SC.FormatDato("Direcci�n Origen", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TDRENT").Caption = NPOI_SC.FormatDato("Direcci�n Destino", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TIRALC").Caption = NPOI_SC.FormatDato("Responsable", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("PRSCNT").Caption = NPOI_SC.FormatDato("Contacto", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("SESREQ_S").Caption = NPOI_SC.FormatDato("Estado Req.", NPOI_SC.keyDatoTexto)
            dtExcel.Columns.Add("TOBS").Caption = NPOI_SC.FormatDato("Observaciones", NPOI_SC.keyDatoTexto)
            'dtExcel.Columns.Add("CUSCRT").Caption = NPOI_SC.FormatDato("Usuario Creador", NPOI_SC.keyDatoTexto)
            'dtExcel.Columns.Add("CULUSA").Caption = NPOI_SC.FormatDato("Usuario Actualizador", NPOI_SC.keyDatoTexto)


            'dtExcel.Columns.Add("NORSRTSR").Caption = NPOI_SC.FormatDato("O/S Sol. Req.", NPOI_SC.keyDatoTexto)
            'dtExcel.Columns.Add("CLCLORSR_S").Caption = NPOI_SC.FormatDato("Lugar Origen Sol. Req.", NPOI_SC.keyDatoTexto)
            'dtExcel.Columns.Add("CLCLDSSR_S").Caption = NPOI_SC.FormatDato("Lugar Destino Sol. Req.", NPOI_SC.keyDatoTexto)
            'dtExcel.Columns.Add("NDCORMSR").Caption = NPOI_SC.FormatDato("Nro. Doc. Origen Sol. Req.", NPOI_SC.keyDatoTexto)
            'dtExcel.Columns.Add("PNCDTRSR").Caption = NPOI_SC.FormatDato("Nro. Ope. Agencias Sol. Req.", NPOI_SC.keyDatoTexto)


            Dim columna As String = ""

            Dim odtReqSer As New DataTable
            Dim Negocio As New RequerimientoServicio_BLL
            odtReqSer = Negocio.Lista_Requerimiento_Servicio_Detalle_Exportar(Obtener_Filtro)
            CopiarDatosTo(odtReqSer, dtExcel)
            'odtReqSer = Listar_Requerimiento_Servicio_Exportar_Detalle()

            Dim ListaDatatable As New List(Of DataTable)
            dtExcel.TableName = "REQUERIMIENTO_SERVICIO"
            ListaDatatable.Add(dtExcel.Copy)

            Dim ListaTitulos As New List(Of String)
            ListaTitulos.Add("LISTA DE REQUERIMIENTOS DE SERVICIO DETALLE")

            Dim oLisFiltro As New List(Of SortedList)
            Dim F As New SortedList
            F.Add(0, "Compa�ia:| " & cmbCompania.CCMPN_Descripcion)
            F.Add(1, "Divisi�n:|" & cmbDivision.DivisionDescripcion)
            F.Add(2, "Planta:| " & cmbPlanta.DescripcionPlanta)


            oLisFiltro.Add(F)

            NPOI_SC.ExportExcelGeneralReleaseMultiple(ListaDatatable, ListaTitulos, Nothing, oLisFiltro, 0)


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function CopiarDatosTo(ByVal dtOrigen As DataTable, ByRef dtDestino As DataTable) As DataTable

        dtDestino.Rows.Clear()

        Dim NameColumna As String = ""

        Dim dr As DataRow

        For Fila As Integer = 0 To dtOrigen.Rows.Count - 1

            dr = dtDestino.NewRow

            For Columna As Integer = 0 To dtDestino.Columns.Count - 1

                NameColumna = dtDestino.Columns(Columna).ColumnName

                If dtOrigen.Columns(NameColumna) IsNot Nothing Then

                    dr(NameColumna) = dtOrigen.Rows(Fila)(NameColumna)

                End If

            Next

            dtDestino.Rows.Add(dr)

        Next

        Return dtDestino.Copy

    End Function

End Class
