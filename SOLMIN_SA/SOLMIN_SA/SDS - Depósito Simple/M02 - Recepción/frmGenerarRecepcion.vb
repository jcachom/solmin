'Imports Ransa.TYPEDEF.Cliente
Imports Ransa.NEGO.slnSOLMIN_SDS
Imports Ransa.NEGO.slnSOLMIN_SDS.RecepcionSDS
Imports Ransa.TYPEDEF.OrdenCompra.ItemOC
Imports Ransa.NEGO
Imports Ransa.TYPEDEF
Imports Ransa.Utilitario
Imports Ransa.CONTROL
Imports System.Text
Imports Ransa.TYPEDEF.slnSOLMIN_SDS_SIMPLE



Public Class frmGenerarRecepcion



#Region " Tipo de Datos "
    Enum eTipoValidacion
        PROCESAR
        'ANULAR
        'GENERAR
        'RESTAURAR
    End Enum
#End Region

#Region " Atributos "

    '-- Contiene la información de la Cabecera
    Private objMovimientoMercaderia As clsMovimientoMercaderia
    Private pobjInformacionIngresada As clsMovimientoMercaderia
    '--Cliente
    Private intCliente_CCLNT As Int64 = 0
    Private strRazonSocial_Nombre As String = ""
    Private strTipo As String = ""
    '--Informacion Ubicacion
    Private objHashTable As New Hashtable
    '--Lista de Mercaderias
    Private ItemSelec As New List(Of TD_ItemOCForWayBill)
    '--Nro Guía Ingreso Ransa
    Public intNroGuiaRansa As Int64 = 0
    Private oHashListaLotes As New Hashtable()
    Private decEmbarque As Decimal = 0
    Private oListaProyectos As New List(Of beProyecto)
    Private oListaMovimientoMercaderia As New List(Of clsMovimientoMercaderia)

    Private intWidth As Integer = 0

    Dim oHDtLotes As New Hashtable()
    Private ListaLotes As List(Of beLoteDeMercaderia)

    Private SplitDistance As Integer = 0

    Private IsEstado As Integer = 0
    Private ShowData As Boolean = False

    'EGL - HUNDRED 
    Private strZona As String = ""

#End Region

#Region " Propiedades "

    Public WriteOnly Property Cliente_CCLNT() As Int64
        Set(ByVal value As Int64)
            intCliente_CCLNT = value
        End Set
    End Property

    ''EGL - HUNDRED 
    'Public WriteOnly Property Zona() As String
    '    Set(ByVal value As String)
    '        strZona = value
    '    End Set
    'End Property

    Public Property Tipo() As String
        Get
            Return strTipo
        End Get
        Set(ByVal value As String)
            strTipo = value
        End Set
    End Property

    Public WriteOnly Property RazonSocial() As String
        Set(ByVal value As String)
            strRazonSocial_Nombre = value
        End Set
    End Property

    Public Property pHashTable() As Hashtable
        Get
            Return objHashTable
        End Get
        Set(ByVal value As Hashtable)
            objHashTable = value
        End Set
    End Property

    Public WriteOnly Property ItemSelec_List() As List(Of TD_ItemOCForWayBill)
        Set(ByVal value As List(Of TD_ItemOCForWayBill))
            ItemSelec = value
        End Set
    End Property

    Public WriteOnly Property objInformacionIngresada() As clsMovimientoMercaderia
        Set(ByVal value As clsMovimientoMercaderia)
            pobjInformacionIngresada = value
        End Set
    End Property


    Private _EsDevolucion As Boolean = False
    Public Property EsDevolucion() As Boolean
        Get
            Return _EsDevolucion
        End Get
        Set(ByVal value As Boolean)
            _EsDevolucion = value
        End Set
    End Property



#End Region

#Region " Método y Funciones "

    Private Sub pProcesarRecepcion(ByVal blnCheckServicio As Boolean, ByRef msgMigracionCliente As String)

        If objMovimientoMercaderia.plstItemMovimientoMercaderia.Count <= 0 Then
            MessageBox.Show("No se ha agregado Item.", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Dim intNroGuiaRansa As Int64 = 0
            If mfblnRecepcion_Mercaderia(objMovimientoMercaderia, blnCheckServicio, intNroGuiaRansa, msgMigracionCliente) Then

                UcSerie_Lista.Guardar(intNroGuiaRansa, Nothing)
                'UcUbicaciones_Lista.Guardar(intNroGuiaRansa)

                'If objMovimientoMercaderia.plstItemMovimientoMercaderia.Item(0).pbolEsOutotec Then
                '    EnviarRecepcionOutotec(objMovimientoMercaderia, intNroGuiaRansa, objSeguridadBN.pUsuario)
                'End If

                'Grabar lotes Mercaderia
                Grabarlotes(objMovimientoMercaderia, intNroGuiaRansa)
                'Grabar lotes Mercaderia



                Dim objFiltro As New beDespacho
                With objFiltro
                    .PNCCLNT = objMovimientoMercaderia.pintCodigoCliente_CCLNT
                    .PNNGUIRN = intNroGuiaRansa
                    .pRazonSocial = objMovimientoMercaderia.pstrRazonSocialCliente_TCMPCL
                    .PSCTPOAT = "I"
                End With
                mReporteIngSalRansa(objFiltro)



                Me.bsaProcesarRecepcion.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False
                Me.bsaEliminarItem.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False
                Me.bsaCancelarRecepcion.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False

                'Habilitamos los tab 

                'Me.tbAsignaSerieUbicacion.Enabled = True
                UcSerie_Lista.Enabled = False
                UcProyectos_Lista.Enabled = False
                UcUbicaciones_Lista.Enabled = True

                Me.tbAsignaSerieUbicacion.SelectedIndex = 0
                Me.bsaGuardar.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True
                'Me.bsaSugerenciaUbicacion.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True
                Me.bsaCancelar.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.True
                Me.txtGuiaIngreso.Text = intNroGuiaRansa.ToString

                dgvLotes.Columns("N_CANTIDAD").ReadOnly = True  'Inhabilito la columna cantidad.

                'adicionado soporte Lotes
                '-------------------------------
                ' VisualizarTabLote(False)
                btnAgregar.Enabled = False ' ya no pueden asignar a mas lotes despues de recepcionado
                'GuardarLotesMercaderia()  'comentado por mientras JM


                '-------------------------------
            End If
            'If strTipo = "D" Then
            '    Me.Close()
            'End If
        End If
    End Sub

    'Private obeCabInterfazOutotec As beCabInfzOutotec
    'Private olbeDetInterfazOutotec As List(Of beDetInfzOutotec)
    'Private Sub EnviarRecepcionOutotec(ByVal objMovimientoMercaderia As clsMovimientoMercaderia, ByVal intNroGuiaRansa As Int64, ByVal strUsuarioSistema As String)
    '    ''Proyecto Outotec
    '    Dim obrInterfaz As New brInterfazOutoTec
    '    Dim obrGeneral As New brGeneral
    '    '================registro de cabecera========================
    '    obeCabInterfazOutotec = New beCabInfzOutotec
    '    With obeCabInterfazOutotec
    '        .ctpdoc = "PE"
    '        .codcli = objMovimientoMercaderia.pintCodigoCliente_CCLNT
    '        .npensa = intNroGuiaRansa '"??"
    '        .calmac = obrGeneral.fstrAlmacenVituralOutotec(intCliente_CCLNT, objMovimientoMercaderia.plstItemMovimientoMercaderia.Item(0).pstrZonaAlmacen_TCMZNA.Trim.Substring(0, 2))
    '        .femisi = objMovimientoMercaderia.pstrFechaRealizacion_FRLZSR
    '        .cconce = objMovimientoMercaderia.plstItemMovimientoMercaderia.Item(0).pstrTipoMovIntfz
    '        .ctpode = objMovimientoMercaderia.pstrTipoOrigen_TIPORG.Trim
    '        .coride = objMovimientoMercaderia.pstrCodigoProveedor_cliente_CPRPCL
    '        .ctpref = objMovimientoMercaderia.pstrTipoDocumentoOrigen_TIPORI.Trim
    '        .ndcref = objMovimientoMercaderia.plstItemMovimientoMercaderia.Item(0).pstrNroGuiaCliente_NGUICL
    '        .dobser = objMovimientoMercaderia.plstItemMovimientoMercaderia.Item(0).pstrObservaciones_TOBSRV
    '        .ctporc = objMovimientoMercaderia.plstItemMovimientoMercaderia.Item(0).pstrTipoOc_TPOOCM

    '        'Validamos si los conceptos tienen Oc
    '        Select Case objMovimientoMercaderia.plstItemMovimientoMercaderia.Item(0).pstrTipoMovIntfz
    '            Case "01", "03", "04", "09", "18" '"02", "03", "04", "06", "09", "11", "13", "16"
    '                .nordco = objMovimientoMercaderia.plstItemMovimientoMercaderia.Item(0).pstrNroOrdenCompraCliente_NORCCL
    '            Case Else
    '                .nordco = ""
    '        End Select
    '        .spensa = "E"
    '        .sentat = objMovimientoMercaderia.pstrEntregaAtiempo
    '        .semboc = objMovimientoMercaderia.pstrControlEmbalaje_SCNEMB
    '        .fstatu = Now.Date
    '        If strUsuarioSistema.Trim.Length > 6 Then
    '            .cusuar = strUsuarioSistema.Trim.Substring(1, 6)
    '        Else
    '            .cusuar = strUsuarioSistema
    '        End If
    '    End With
    '    '================registro de detalle=========================
    '    olbeDetInterfazOutotec = New List(Of beDetInfzOutotec)
    '    Dim intNRow As Integer = 1
    '    For Each objItemMovimientoMercaderia As clsItemMovimientoMercaderia In objMovimientoMercaderia.plstItemMovimientoMercaderia
    '        Dim obeDetInterfazOutotec As New beDetInfzOutotec
    '        With obeDetInterfazOutotec
    '            .nordsr = objItemMovimientoMercaderia.pintOrdenServicio_NORDSR
    '            .nritoc = objItemMovimientoMercaderia.pintNroItemOC_NRITOC
    '            .ctpdoc = "PE"
    '            .npensa = obeCabInterfazOutotec.npensa
    '            .codcli = objMovimientoMercaderia.pintCodigoCliente_CCLNT
    '            .norden = intNRow
    '            .citems = objItemMovimientoMercaderia.pstrCodigoMercaderia_CMRCLR
    '            .cunmed = objItemMovimientoMercaderia.pstrUnidadCantidad_CUNCNT
    '            .qitems = objItemMovimientoMercaderia.pdecCantidad_QTRMC
    '            .cubica = IIf(Me.objHashTable("TALMC").ToString.Trim.Length > 5, Me.objHashTable("TALMC").ToString.Trim.Substring(0, 5), Me.objHashTable("TALMC").ToString.Trim)
    '            .olistaProyecto = objItemMovimientoMercaderia.oListaProyecto
    '        End With
    '        olbeDetInterfazOutotec.Add(obeDetInterfazOutotec)
    '        intNRow = intNRow + 1
    '    Next
    'End Sub

#End Region

#Region " Eventos "

    'Private Sub frmGenerarRecepcion_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '    Try
    '        If obeCabInterfazOutotec IsNot Nothing Then

    '            If olbeDetInterfazOutotec.Count > 0 Then
    '                Dim olbeDetInterfaz As New List(Of beDetInfzOutotec)

    '                Dim intRow As Integer = 1
    '                Dim intCantidadProyecto As Integer = 0
    '                Dim olbeSerie As New List(Of beSerie)
    '                Dim olbeProyecto As List(Of beProyecto)
    '                Dim olbeProyectoTemp As List(Of beProyecto)
    '                Dim obeProyecto As beProyecto
    '                Dim obeDetIntFaz As beDetInfzOutotec

    '                Dim oCloneList As New CloneObject(Of beDetInfzOutotec)

    '                For Each obeDetInterfazOutotec As beDetInfzOutotec In olbeDetInterfazOutotec
    '                    obeDetIntFaz = New beDetInfzOutotec
    '                    olbeSerie = UcSerie_Lista.ListaSerieXOrdenDeServicioIngreso(obeDetInterfazOutotec.nordsr, obeDetInterfazOutotec.nritoc)
    '                    olbeProyecto = obeDetInterfazOutotec.olistaProyecto
    '                    If olbeSerie.Count > 0 Then
    '                        If olbeSerie.Count = obeDetInterfazOutotec.qitems Then
    '                            For Each obeSerie As beSerie In olbeSerie
    '                                obeDetIntFaz = oCloneList.CloneObject(obeDetInterfazOutotec)

    '                                obeDetIntFaz.norden = intRow
    '                                obeDetIntFaz.qitems = 1
    '                                obeDetIntFaz.nserie = obeSerie.PSCSRECL

    '                                If Not obeDetIntFaz.olistaProyecto Is Nothing Then
    '                                    obeProyecto = New beProyecto
    '                                    olbeProyectoTemp = New List(Of beProyecto)
    '                                    For Each obeProyec As beProyecto In olbeProyecto
    '                                        If obeProyec.PNQCNTIT2 >= obeDetIntFaz.qitems Then
    '                                            obeProyecto.PSNRFRPD = obeProyec.PSNRFRPD
    '                                            obeProyecto.PNQCNTIT2 = obeDetIntFaz.qitems
    '                                            olbeProyectoTemp.Add(obeProyecto)
    '                                            obeProyec.PNQCNTIT2 = obeProyec.PNQCNTIT2 - obeDetIntFaz.qitems
    '                                            Exit For
    '                                        End If
    '                                    Next
    '                                    obeDetIntFaz.olistaProyecto = olbeProyectoTemp
    '                                End If

    '                                olbeDetInterfaz.Add(obeDetIntFaz)
    '                                intRow = intRow + 1

    '                            Next
    '                        Else
    '                            'clonamos la entiada 
    '                            obeDetIntFaz = oCloneList.CloneObject(obeDetInterfazOutotec)

    '                            obeDetIntFaz.norden = intRow
    '                            obeDetIntFaz.qitems = obeDetInterfazOutotec.qitems - olbeSerie.Count

    '                            'asociamos el item al proyecto
    '                            If Not obeDetIntFaz.olistaProyecto Is Nothing Then
    '                                obeProyecto = New beProyecto
    '                                olbeProyectoTemp = New List(Of beProyecto)
    '                                For Each obeProyec As beProyecto In olbeProyecto
    '                                    obeProyecto = New beProyecto
    '                                    intCantidadProyecto = obeProyec.PNQCNTIT2 + intCantidadProyecto
    '                                    If intCantidadProyecto >= obeDetIntFaz.qitems Then
    '                                        obeProyecto.PSNRFRPD = obeProyec.PSNRFRPD
    '                                        obeProyecto.PNQCNTIT2 = obeDetIntFaz.qitems + obeProyec.PNQCNTIT2 - intCantidadProyecto
    '                                        olbeProyectoTemp.Add(obeProyecto)
    '                                        obeProyec.PNQCNTIT2 = obeProyec.PNQCNTIT2 - obeProyecto.PNQCNTIT2
    '                                        Exit For
    '                                    ElseIf obeProyec.PNQCNTIT2 <> 0 Then
    '                                        obeProyecto.PNQCNTIT2 = obeProyec.PNQCNTIT2
    '                                        obeProyecto.PSNRFRPD = obeProyec.PSNRFRPD
    '                                        olbeProyectoTemp.Add(obeProyecto)
    '                                        'obeProyec.PNQCNTIT2 = obeProyec.PNQCNTIT2 - obeDetIntFaz.qitems
    '                                    End If
    '                                Next
    '                                obeDetIntFaz.olistaProyecto = olbeProyectoTemp
    '                            End If
    '                            olbeDetInterfaz.Add(obeDetIntFaz)
    '                            intRow = intRow + 1

    '                            For Each obeSerie As beSerie In olbeSerie
    '                                obeDetIntFaz = oCloneList.CloneObject(obeDetInterfazOutotec)

    '                                obeDetIntFaz.norden = intRow
    '                                obeDetIntFaz.qitems = 1
    '                                obeDetIntFaz.nserie = obeSerie.PSCSRECL
    '                                'Asociamos el item al proyecto
    '                                If Not obeDetIntFaz.olistaProyecto Is Nothing Then
    '                                    obeProyecto = New beProyecto
    '                                    olbeProyectoTemp = New List(Of beProyecto)
    '                                    For Each obeProyec As beProyecto In olbeProyecto
    '                                        If obeProyec.PNQCNTIT2 >= obeDetIntFaz.qitems Then
    '                                            obeProyecto.PSNRFRPD = obeProyec.PSNRFRPD
    '                                            obeProyecto.PNQCNTIT2 = obeDetIntFaz.qitems
    '                                            olbeProyectoTemp.Add(obeProyecto)
    '                                            obeProyec.PNQCNTIT2 = obeProyec.PNQCNTIT2 - obeDetIntFaz.qitems
    '                                            Exit For
    '                                        End If
    '                                    Next
    '                                    obeDetIntFaz.olistaProyecto = olbeProyectoTemp
    '                                End If
    '                                olbeDetInterfaz.Add(obeDetIntFaz)
    '                                intRow = intRow + 1
    '                            Next
    '                        End If
    '                    Else
    '                        obeDetInterfazOutotec.norden = intRow
    '                        olbeDetInterfaz.Add(obeDetInterfazOutotec)
    '                        intRow = intRow + 1
    '                    End If
    '                Next
    '                olbeDetInterfazOutotec = Nothing
    '                olbeDetInterfazOutotec = olbeDetInterfaz
    '            End If

    '            Dim obrInterfaz As New brInterfazOutoTec
    '            If obrInterfaz.fintInsertarProceso(obeCabInterfazOutotec, olbeDetInterfazOutotec) = -1 Then
    '                HelpClass.MsgBox("Error en el proceso de envio a Outotec, Notifique este evento al Dpto. de Sistemas.")
    '            Else
    '                Dim objDespacho As New brDespacho
    '                Dim obeDespacho As New beDespacho
    '                With obeDespacho
    '                    .PSCTPIS = "I"
    '                    .PNCCLNT = obeCabInterfazOutotec.codcli
    '                    .PNNGUIRN = obeCabInterfazOutotec.npensa
    '                    .PNFRLZSR = HelpClass.CtypeDate(obeCabInterfazOutotec.femisi)
    '                    .PSSTPODP = objSeguridadBN.pstrTipoAlmacen
    '                    .PNNPRTIN = 0
    '                    .PSSTRNSM = "X"
    '                    .PNFTRNSM = Convert.ToDecimal(HelpClass.CtypeDate(DateTime.Today))
    '                    .PNHTRNSM = Convert.ToDecimal(HelpClass.NowNumeric())
    '                    .PSCUSCRT = objSeguridadBN.pUsuario
    '                    .PNFCHCRT = Convert.ToDecimal(HelpClass.CtypeDate(DateTime.Today))
    '                    .PNHRACRT = Convert.ToDecimal(HelpClass.NowNumeric())
    '                    .PSCULUSA = objSeguridadBN.pUsuario
    '                    .PNFULTAC = Convert.ToDecimal(HelpClass.CtypeDate(DateTime.Today))
    '                    .PNHULTAC = Convert.ToDecimal(HelpClass.NowNumeric())
    '                End With

    '                Dim rpta As Integer = 1
    '                rpta = objDespacho.fintRegistrarEnvioInterfaz(obeDespacho)
    '                If rpta = 0 Then
    '                    MessageBox.Show("Error al Registrar Envio Interfaz", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try


    'End Sub

    'Private Manejo_x_Posicion As String = ""
    'Private Mensaje_posicion_obligatoriedad As String = ""
    Private Manejo_x_Posicion_NivelCliente As String = ""
    Private Mensaje_AlertaDiferencia_x_ManejoPosiciones As String = ""

    Private Sub frmGenerarRecepcion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            'adicionado soporte Lotes
            '-------------------------------
            Dim obrGeneral As New brGeneral
            Dim obeGeneral As New beGeneral
            Dim oListbeGeneral As New List(Of beGeneral)
            obeGeneral.PSCODVAR = "SAMPSR"
            obeGeneral.PSCCMPRN = intCliente_CCLNT
            oListbeGeneral = obrGeneral.ListaTablaAyuda(obeGeneral)
            If oListbeGeneral.Count > 0 Then
                Manejo_x_Posicion_NivelCliente = oListbeGeneral(0).PSTDSDES
                Mensaje_AlertaDiferencia_x_ManejoPosiciones = oListbeGeneral(0).PSTDSDES2
            End If

            btnAgregar.Enabled = True
            ' Me.tbAsignaSerieUbicacion.Enabled = True
            'VisualizarTabLote(True)
            tbAsignaSerieUbicacion.TabPages.Remove(TabPageLotes)
            Me.tbAsignaSerieUbicacion.SelectedIndex = 2
            dtgLotes.AutoGenerateColumns = False

            '-----------------------------

            'me.TABDPS.itr TabPageLotes
            Me.txtNroOrdenCompra.Text = Me.objHashTable("NORCCL")

            Me.txtTipoAlmacen.Text = Me.objHashTable("CTPALM") + " <-> " + Me.objHashTable("TCMPAL")
            Me.txtAlmacen.Text = Me.objHashTable("CALMC") + " <-> " + Me.objHashTable("TALMC")

            Me.txtZona.Text = Me.objHashTable("CZNALM") + " <-> " + Me.objHashTable("TCMZNA")
            Me.txtNroGuia.Text = Me.objHashTable("NGUICL")
            Me.txtCliente.Text = Me.intCliente_CCLNT & " <-> " & Me.strRazonSocial_Nombre
            Me.txtProveedor.Text = Me.objHashTable("NOMBPR")

            'EGL - HUNDRED 
            strZona = Me.objHashTable("CZNALM")

            Dim msgValidCantOS As String = ""
            Dim psku As String = ""

            decEmbarque = Me.objHashTable("NORSCI")
            Dim rwTemp As DataRow
            For intContador As Int32 = 0 To Me.ItemSelec.Count - 1
                rwTemp = dtMercaderiaSeleccionadas.NewRow
                With rwTemp
                    psku = ("" & Me.ItemSelec.Item(intContador).pTCITCL_CodigoCliente).ToString.Trim
                    rwTemp.Item("CMRCLR") = psku
                    rwTemp.Item("TMRCD2") = ("" & Me.ItemSelec.Item(intContador).pTMRCD2_MercaDescripcion).ToString.Trim
                    rwTemp.Item("CMRCD") = ("" & Me.ItemSelec.Item(intContador).pCMRCD_CodigoRANSA).ToString.Trim
                    rwTemp.Item("NORDSR") = Me.ItemSelec.Item(intContador).pNORDSR_OrdenServicio
                    rwTemp.Item("NCNTR") = Me.ItemSelec.Item(intContador).pNCNTR_NroContrato
                    rwTemp.Item("NCRCTC") = Me.ItemSelec.Item(intContador).pNCRCTC_NroCorrelativoContrato
                    rwTemp.Item("NAUTR") = Me.ItemSelec.Item(intContador).pNAUTR_NroAutorizacionContrato
                    rwTemp.Item("CUNCNT") = Me.ItemSelec.Item(intContador).pCUNCN5_UnidadCantidad
                    rwTemp.Item("CUNPST") = Me.ItemSelec.Item(intContador).pCUNPS5_UnidadPeso
                    rwTemp.Item("CUNVLT") = Me.ItemSelec.Item(intContador).pCUNVL5_UnidadValor
                    rwTemp.Item("NORCCL") = Me.objHashTable("NORCCL")
                    rwTemp.Item("NGUICL") = Me.objHashTable("NGUICL")
                    rwTemp.Item("NRUCLL") = Me.objHashTable("NRUCLL")
                    rwTemp.Item("STPING") = Me.objHashTable("STPING")
                    rwTemp.Item("CTPALM") = Me.objHashTable("CTPALM")
                    rwTemp.Item("TALMC") = Me.objHashTable("TALMC")
                    rwTemp.Item("CALMC") = Me.objHashTable("CALMC")
                    rwTemp.Item("TCMPAL") = Me.objHashTable("TCMPAL")
                    rwTemp.Item("CZNALM") = Me.objHashTable("CZNALM")
                    rwTemp.Item("TCMZNA") = Me.objHashTable("TCMZNA")
                    rwTemp.Item("QTRMC") = Me.ItemSelec.Item(intContador).pQCNREC_QtaRecibida
                    'adicionado soporte Lotes
                    '-------------------------------
                    rwTemp.Item("LOTE") = Me.ItemSelec.Item(intContador).pQCNREC_QtaRecibida
                    '-------------------------------
                    rwTemp.Item("QTRMP") = Me.ItemSelec.Item(intContador).pQPEPQT_QtaPeso
                    rwTemp.Item("CCNTD") = Me.objHashTable("CCNTD")
                    rwTemp.Item("CTPOCN") = Me.objHashTable("CTPOCN")
                    rwTemp.Item("FUNDS2") = Me.ItemSelec.Item(intContador).pFUNDS2_Status
                    rwTemp.Item("CTPDPS") = Me.ItemSelec.Item(intContador).pCTPDPS_TipoDeposito
                    rwTemp.Item("TOBSRV") = Me.ItemSelec.Item(intContador).pTDAITM_Observaciones
                    rwTemp.Item("NRITOC") = Me.ItemSelec.Item(intContador).pNRITOC_NroItemOC
                    rwTemp.Item("FMPDME") = Me.ItemSelec.Item(intContador).pFMPDME_FechaMaxDespacho
                    'JM
                    rwTemp.Item("FUNCTL") = Me.ItemSelec.Item(intContador).pFUNCTL_FlagControlDeLotes
                    rwTemp.Item("FUBICAC") = Me.ItemSelec.Item(intContador).pFlagControlUbicacion
                    rwTemp.Item("SCNTSR") = Me.ItemSelec.Item(intContador).pFlagControlSerie

                    rwTemp.Item("FILA") = intContador + 1

                    If ItemSelec.Item(intContador).pCantOS_X_SKU > 1 Then
                        msgValidCantOS = msgValidCantOS & " [SKU " & psku & " ] tiene más de 1 O/S" & Chr(13)
                    End If



                End With
                dtMercaderiaSeleccionadas.Rows.Add(rwTemp)
            Next

            msgValidCantOS = msgValidCantOS.Trim
            If msgValidCantOS.Length > 0 Then
                MessageBox.Show(msgValidCantOS, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                bsaProcesarRecepcion.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False
            End If



            'ListarLoteMercaderiaInicial()


            intWidth = hgGuiaRemision.Width


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try




    End Sub

    'Private Sub dgMercaderia_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgMercaderiaSeleccionada.CurrentCellChanged
    '    Try
    '        If dgMercaderiaSeleccionada.RowCount = 0 OrElse dgMercaderiaSeleccionada.CurrentCellAddress.Y < 0 Then Exit Sub

    '        If dgMercaderiaSeleccionada.CurrentRow IsNot Nothing Then ListaDeLotesPorOC() 'JM


    '        Me.UcSerie_Lista.IngresoSeries(Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NRITOC").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_QTRMC").Value, Me.intCliente_CCLNT, Me.intNroGuiaRansa, objSeguridadBN.pUsuario)


    '        If dgvLotes.RowCount = 0 Then 'jm si la mercaderia selecionada  no tiene lotes carga todas las ubicaciones
    '            Me.UcUbicaciones_Lista.ConsultarIngresos(Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value, intCliente_CCLNT, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CTPALM").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CALMC").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_QTRMC").Value, objSeguridadBN.pUsuario, False, 0, dgMercaderiaSeleccionada.CurrentRow.Index, strZona)

    '        End If

    '        VerProyecto_x_OrdenServicio(Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORCCL").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NRITOC").Value, intCliente_CCLNT, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_QTRMC").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value)

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try



    'End Sub

    'Private Sub bsaEliminarItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub

    'Private Sub bsaProcesarGuia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bsaProcesarGuia.Click

    '    Try
    '        If Me.dgMercaderiaSeleccionada.RowCount <= 0 Then Exit Sub
    '        Dim fSolicInRecepcionGuia As frmRecepcionGuia = New frmRecepcionGuia
    '        With fSolicInRecepcionGuia
    '            .txtCliente.Text = intCliente_CCLNT
    '            If .ShowDialog = Windows.Forms.DialogResult.OK Then
    '                For lint_Contador As Integer = 0 To .dgMercaderiaGuia.RowCount - 1
    '                    If fSolicInRecepcionGuia.dgMercaderiaGuia.Item(0, lint_Contador).Value = True Then
    '                        Dim rwTemp As DataRow = dtMercaderiaSeleccionadas.NewRow
    '                        With rwTemp

    '                            rwTemp.Item("CMRCLR") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(2, lint_Contador).Value
    '                            rwTemp.Item("TMRCD2") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(3, lint_Contador).Value
    '                            rwTemp.Item("TOBSRV") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(4, lint_Contador).Value
    '                            rwTemp.Item("NORDSR") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(11, lint_Contador).Value
    '                            rwTemp.Item("NCNTR") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(13, lint_Contador).Value
    '                            rwTemp.Item("NCRCTC") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(14, lint_Contador).Value
    '                            rwTemp.Item("NAUTR") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(15, lint_Contador).Value
    '                            rwTemp.Item("CUNCNT") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(6, lint_Contador).Value
    '                            rwTemp.Item("CUNPST") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(8, lint_Contador).Value
    '                            rwTemp.Item("CUNVLT") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(10, lint_Contador).Value
    '                            rwTemp.Item("NORCCL") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(12, lint_Contador).Value
    '                            rwTemp.Item("NGUICL") = fSolicInRecepcionGuia.int_GuiaRemision
    '                            rwTemp.Item("STPING") = fSolicInRecepcionGuia.pstrTipoRecepcion
    '                            rwTemp.Item("CTPALM") = fSolicInRecepcionGuia.pstrTipoAlmacen
    '                            rwTemp.Item("TALMC") = fSolicInRecepcionGuia.pstrDetalleTipoAlmacen
    '                            rwTemp.Item("CALMC") = fSolicInRecepcionGuia.pstrAlmacen
    '                            rwTemp.Item("TCMPAL") = fSolicInRecepcionGuia.pstrDetalleAlmacen
    '                            rwTemp.Item("CZNALM") = fSolicInRecepcionGuia.pstrZonaAlmacen
    '                            rwTemp.Item("TCMZNA") = fSolicInRecepcionGuia.pstrDetalleZonaAlmacen
    '                            rwTemp.Item("QTRMC") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(5, lint_Contador).Value
    '                            rwTemp.Item("QTRMP") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(7, lint_Contador).Value
    '                            rwTemp.Item("CCNTD") = ""
    '                            rwTemp.Item("CTPOCN") = "NO"
    '                            rwTemp.Item("FUNDS2") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(16, lint_Contador).Value
    '                            rwTemp.Item("CMRCD") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(17, lint_Contador).Value
    '                            rwTemp.Item("NRUCLL") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(18, lint_Contador).Value
    '                            rwTemp.Item("CTPDPS") = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(19, lint_Contador).Value


    '                        End With
    '                        dtMercaderiaSeleccionadas.Rows.Add(rwTemp)
    '                        Dim objTemp As clsItemMovimientoMercaderia = New clsItemMovimientoMercaderia
    '                        With objTemp
    '                            Int64.TryParse(("" & fSolicInRecepcionGuia.dgMercaderiaGuia.Item(11, lint_Contador).Value).ToString.Trim, .pintOrdenServicio_NORDSR)
    '                            Int64.TryParse(("" & fSolicInRecepcionGuia.dgMercaderiaGuia.Item(13, lint_Contador).Value).ToString.Trim, .pintNroContrato_NCNTR)
    '                            Int64.TryParse(("" & fSolicInRecepcionGuia.dgMercaderiaGuia.Item(14, lint_Contador).Value).ToString.Trim, .pintNroCorrDetalleContrato_NCRCTC)
    '                            Int64.TryParse(("" & fSolicInRecepcionGuia.dgMercaderiaGuia.Item(15, lint_Contador).Value).ToString.Trim, .pintNroAutorizacion_NAUTR)
    '                            .pstrCodigoMercaderia_CMRCLR = ("" & fSolicInRecepcionGuia.dgMercaderiaGuia.Item(2, lint_Contador).Value).ToString.Trim
    '                            .pstrCodigoRANSA_CMRCD = ("" & fSolicInRecepcionGuia.dgMercaderiaGuia.Item(17, lint_Contador).Value).ToString.Trim

    '                            .pstrNroOrdenCompraCliente_NORCCL = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(12, lint_Contador).Value
    '                            .pstrNroGuiaCliente_NGUICL = fSolicInRecepcionGuia.int_GuiaRemision
    '                            .pstrNroRUCCliente_NRUCLL = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(18, lint_Contador).Value

    '                            .pstrTipoAlmacen_CTPALM = fSolicInRecepcionGuia.pstrTipoAlmacen
    '                            .pstrAlmacen_CALMC = fSolicInRecepcionGuia.pstrAlmacen
    '                            .pstrZonaAlmacen_CZNALM = fSolicInRecepcionGuia.pstrZonaAlmacen

    '                            Decimal.TryParse(fSolicInRecepcionGuia.dgMercaderiaGuia.Item(5, lint_Contador).Value, .pdecCantidad_QTRMC)
    '                            Decimal.TryParse(fSolicInRecepcionGuia.dgMercaderiaGuia.Item(7, lint_Contador).Value, .pdecPeso_QTRMP)
    '                            .pstrUnidadCantidad_CUNCNT = ("" & fSolicInRecepcionGuia.dgMercaderiaGuia.Item(6, lint_Contador).Value).ToString.Trim
    '                            .pstrUnidadPeso_CUNPST = ("" & fSolicInRecepcionGuia.dgMercaderiaGuia.Item(8, lint_Contador).Value).ToString.Trim
    '                            .pstrUnidadValorTransaccion_CUNVLT = ("" & fSolicInRecepcionGuia.dgMercaderiaGuia.Item(10, lint_Contador).Value).ToString.Trim

    '                            .pblnDesglose_CTPOCN = False
    '                            .pstrContenedor_CCNTD = ""
    '                            .pstrTipoMovimiento_STPING = fSolicInRecepcionGuia.pstrTipoRecepcion

    '                            .pstrSatusUnidadDespacho_FUNDS2 = ("" & fSolicInRecepcionGuia.dgMercaderiaGuia.Item(16, lint_Contador).Value).ToString.Trim
    '                            .pstrTipoDeposito_CTPDPS = fSolicInRecepcionGuia.dgMercaderiaGuia.Item(19, lint_Contador).Value
    '                        End With
    '                        objMovimientoMercaderia.AddItemMovimientoMercaderia(objTemp)
    '                    End If
    '                Next
    '            End If
    '            .Dispose()
    '            fSolicInRecepcionGuia = Nothing
    '        End With

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try


    'End Sub





    Private Function ValidarIngresoSeriexLinea(ByRef msgAlerta As String) As String

        Dim msg As String = ""
        Dim SKU As String = ""
        Dim FlagCtrlSerie As Boolean = False
        Dim msgError As String = ""
        Dim pExisteError As Boolean = False
        Dim QTotalRecepcion As Decimal = 0
        Dim OS As Decimal = 0
        'Dim FilaReg As Int64 = 0
        For IntFila As Integer = 0 To dgMercaderiaSeleccionada.Rows.Count - 1

            'If Manejo_x_Posicion_NivelCliente = "S" Then
            '    FlagCtrlUbicacion = True
            'Else
            If ("" & dgMercaderiaSeleccionada.Rows(IntFila).Cells("SCNTSR").Value) = "X" Then
                FlagCtrlSerie = True
            Else
                FlagCtrlSerie = False
            End If
            'End If
            QTotalRecepcion = dgMercaderiaSeleccionada.Rows(IntFila).Cells("S_QTRMC").Value
            OS = dgMercaderiaSeleccionada.Rows(IntFila).Cells("S_NORDSR").Value
            'FilaReg = dgMercaderiaSeleccionada.Rows(IntFila).Cells("FILA").Value
            SKU = ("" & dgMercaderiaSeleccionada.Rows(IntFila).Cells("S_CMRCLR").Value)
            msg = UcSerie_Lista.ValidarCantidadSerieXOSIngreso(pExisteError, FlagCtrlSerie, QTotalRecepcion, OS, SKU, IntFila)

            If pExisteError = True Then
                If msg <> "" Then
                    msgError = msgError & msg & Chr(10)
                End If
            Else
                If msg <> "" Then
                    msgAlerta = msgAlerta & msg & Chr(10)
                End If
            End If
        Next

        msgError = msgError.Trim
        msgAlerta = msgAlerta.Trim

        'If msgError.Length > 0 Then
        '    MessageBox.Show(msgError, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    'Exit Function
        'End If
        'If msgAlerta.Length > 0 And Mensaje_AlertaDiferencia_x_ManejoPosiciones = "S" Then
        '    If MessageBox.Show(msgAlerta & Chr(13) & " ¿Desea continuar?", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
        '        'Exit Function
        '    End If
        'End If
        Return msgError
    End Function

    Private Sub bsaProcesarRecepcion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bsaProcesarRecepcion.Click

        Try

            Dim bolClienteOutotec As Boolean = False
            Dim strTipoMovIntfz As String = ""

           
        

            If dgMercaderiaSeleccionada.RowCount <= 0 Then
                MessageBox.Show("No existe información.", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else


                Dim obrGeneral As New brGeneral


                Dim msAlerta As String = ""
                Dim msgError As String = ""
                msgError = ValidarIngresoSeriexLinea(msAlerta)
                If msgError.Length > 0 Then
                    MessageBox.Show(msgError, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If



                'Validamos que el clientes sea Outotec
                If obrGeneral.bolElClienteEsOutotec(intCliente_CCLNT) Then
                    bolClienteOutotec = True

                    Dim strError As String = ""
                    '===========SI EL CLIENTE ES OUTOTEC==============
                    Dim strTipoMovimiento As String = ""

                    'Validamos que el tipo de movimiento selecciona tenga su equivalencia en outotec
                    Dim olbeGeneral As New List(Of beGeneral)
                    olbeGeneral = obrGeneral.olEquivalenteOutotecTipoMovimientoRec(dgMercaderiaSeleccionada.Rows(0).Cells("S_STPING").Value)
                    If olbeGeneral.Count = 0 Then
                        MessageBox.Show("- El tipo de recepción no cuenta con equivalencia en la interfaz", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    strTipoMovIntfz = olbeGeneral.Item(0).PSTDSDES
                    Dim intSuma As Int32
                    'Dim oListaProyecto As New List(Of beProyecto)
                    Dim oListaProyecto2 As List(Of beProyecto)
                    For intCont As Integer = 0 To dgMercaderiaSeleccionada.RowCount - 1
                        oListaProyecto2 = New List(Of beProyecto)
                        oListaProyecto2 = VerProyecto_x_OrdenServicio(dgMercaderiaSeleccionada.Rows(intCont).Cells("S_NORDSR").Value, dgMercaderiaSeleccionada.Rows(intCont).Cells("S_NORCCL").Value, dgMercaderiaSeleccionada.Rows(intCont).Cells("S_NRITOC").Value, intCliente_CCLNT, dgMercaderiaSeleccionada.Rows(intCont).Cells("S_QTRMC").Value, dgMercaderiaSeleccionada.Rows(intCont).Cells("S_CMRCLR").Value)
                        If (oListaProyecto2.Count > 0) Then
                            intSuma = 0
                            For Each oProyecto As beProyecto In oListaProyecto2
                                intSuma += oProyecto.PNQCNTIT2
                            Next
                            If (dgMercaderiaSeleccionada.Rows(intCont).Cells("S_QTRMC").Value <> intSuma) Then
                                strError = strError + "   " + dgMercaderiaSeleccionada.Rows(intCont).Cells("S_CMRCLR").FormattedValue & Chr(10)
                            End If
                        End If
                    Next

                    If (strError <> "") Then
                        MessageBox.Show("- Las siguientes Mercaderías no estan distribuidas en los proyectos : " & Chr(10) & strError, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If
                '===========SI EL CLIENTE ES OUTOTEC===============
            End If


            Dim msj As String = String.Empty
            If Not validacion(msj) Then   'JM
                MessageBox.Show(msj, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            'adicionado soporte Lotes
            '--------------------------------------
            'If (ValidarIngresoLotesParaRecepcion() = False) Then
            '    Dim strMensaje As String = ""
            '    strMensaje = "Debe asignar todas las cantidades pendientes de Asignación"
            '    strMensaje = strMensaje & Chr(13) & "a sus Lotes"
            '    MessageBox.Show(strMensaje, "Recepción", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If
            '--------------------------------------



            Dim oListaProyecto As New List(Of beProyecto)


            objMovimientoMercaderia = New clsMovimientoMercaderia
            objMovimientoMercaderia.pintCodigoCliente_CCLNT = intCliente_CCLNT
            objMovimientoMercaderia.pstrRazonSocialCliente_TCMPCL = strRazonSocial_Nombre
            'objMovimientoMercaderia.pstrCompania_CCMPN = "EZ"
            'objMovimientoMercaderia.pstrDivision_CDVSN = "R"
            'Obtenemos la información de la cabecera registrada
            objMovimientoMercaderia.pintAnioUnidad_NANOCM = pobjInformacionIngresada.pintAnioUnidad_NANOCM
            objMovimientoMercaderia.pintCodigoCliente_CCLNT = pobjInformacionIngresada.pintCodigoCliente_CCLNT
            objMovimientoMercaderia.pintEmpresaTransporte_CTRSP = pobjInformacionIngresada.pintEmpresaTransporte_CTRSP
            objMovimientoMercaderia.pintNroDocIdentidadChofer_NLELCH = pobjInformacionIngresada.pintNroDocIdentidadChofer_NLELCH
            objMovimientoMercaderia.pintNroRUCEmpTransporte_NRUCT = pobjInformacionIngresada.pintNroRUCEmpTransporte_NRUCT
            objMovimientoMercaderia.pintServicio_CSRVC = pobjInformacionIngresada.pintServicio_CSRVC
            objMovimientoMercaderia.pstrChofer_TNMBCH = pobjInformacionIngresada.pstrChofer_TNMBCH
            objMovimientoMercaderia.pstrCompania_CCMPN = pobjInformacionIngresada.pstrCompania_CCMPN
            objMovimientoMercaderia.pstrDivision_CDVSN = pobjInformacionIngresada.pstrDivision_CDVSN
            objMovimientoMercaderia.pstrMarcaUnidad_TMRCCM = pobjInformacionIngresada.pstrMarcaUnidad_TMRCCM
            objMovimientoMercaderia.pstrNroBrevete_NBRVCH = pobjInformacionIngresada.pstrNroBrevete_NBRVCH
            objMovimientoMercaderia.pstrNroPlacaCamion_NPLCCM = pobjInformacionIngresada.pstrNroPlacaCamion_NPLCCM
            objMovimientoMercaderia.pstrObservaciones_TOBSER = pobjInformacionIngresada.pstrObservaciones_TOBSER
            objMovimientoMercaderia.pstrRazonSocialCliente_TCMPCL = pobjInformacionIngresada.pstrRazonSocialCliente_TCMPCL
            objMovimientoMercaderia.pstrRazonSocialEmpTransporte_TCMPTR = pobjInformacionIngresada.pstrRazonSocialEmpTransporte_TCMPTR

            objMovimientoMercaderia.pstrTipoOrigen_TIPORG = pobjInformacionIngresada.pstrTipoOrigen_TIPORG
            objMovimientoMercaderia.pstrTipoDocumentoOrigen_TIPORI = pobjInformacionIngresada.pstrTipoDocumentoOrigen_TIPORI
            objMovimientoMercaderia.pstrCodigoProveedor_CPRVCL = pobjInformacionIngresada.pstrCodigoProveedor_CPRVCL

            objMovimientoMercaderia.pstrCodigoProveedor_cliente_CPRPCL = pobjInformacionIngresada.pstrCodigoProveedor_cliente_CPRPCL
            objMovimientoMercaderia.pbolEsDevolucion() = EsDevolucion
            objMovimientoMercaderia.pstrFechaRealizacion_FRLZSR = pobjInformacionIngresada.pstrFechaRealizacion_FRLZSR
            objMovimientoMercaderia.pintFechaRealizacion_FRLZSR = HelpClass.CDate_a_Numero8Digitos(pobjInformacionIngresada.pstrFechaRealizacion_FRLZSR)

            'Se agrego para la interfaz con Outotec
            objMovimientoMercaderia.pstrControlEmbalaje_SCNEMB = Me.objHashTable("SCNEMB")
            If bolClienteOutotec Then
                objMovimientoMercaderia.pstrEntregaAtiempo = BuscarEntregaTiempo()
            End If
            'Añadido para Asociar costo de importacion ABB
            objMovimientoMercaderia.pdecNumSolicitudDeReferencia_NSLCRF = decEmbarque
            '
            ' Recuperamos la información ingresada en el datatable a la Lista de Clases
            Dim iCont As Integer = 0
            While iCont < dgMercaderiaSeleccionada.RowCount
                Dim objTemp As clsItemMovimientoMercaderia = New clsItemMovimientoMercaderia
                With objTemp
                    .pintOrdenServicio_NORDSR = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NORDSR").Value
                    .pintNroContrato_NCNTR = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NCNTR").Value
                    .pintNroCorrDetalleContrato_NCRCTC = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NCRCTC").Value
                    .pintNroAutorizacion_NAUTR = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NAUTR").Value
                    .pstrCodigoMercaderia_CMRCLR = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CMRCLR").Value).ToString.Trim
                    .pstrCodigoRANSA_CMRCD = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CMRCD").Value

                    .pstrNroOrdenCompraCliente_NORCCL = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NORCCL").Value).ToString.Trim
                    .pstrNroGuiaCliente_NGUICL = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NGUICL").Value).ToString.Trim
                    .pstrNroRUCCliente_NRUCLL = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NRUCLL").Value).ToString.Trim
                    .pstrNombreProveedor_NOMPRO = Me.objHashTable("NOMPRO")
                    .pstrTipoAlmacen_CTPALM = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CTPALM").Value).ToString.Trim
                    .pstrAlmacen_CALMC = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CALMC").Value).ToString.Trim
                    .pstrZonaAlmacen_CZNALM = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CZNALM").Value).ToString.Trim
                    .pstrZonaAlmacen_TCMZNA = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_TCMZNA").Value).ToString.Trim

                    .pdecCantidad_QTRMC = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_QTRMC").Value
                    .pdecPeso_QTRMP = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_QTRMP").Value
                    .pstrUnidadCantidad_CUNCNT = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CUNCNT").Value).ToString.Trim
                    .pstrUnidadPeso_CUNPST = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CUNPST").Value).ToString.Trim
                    .pstrUnidadValorTransaccion_CUNVLT = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CUNVLT").Value).ToString.Trim

                    .pblnDesglose_CTPOCN = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CTPOCN").Value
                    .pstrContenedor_CCNTD = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CCNTD").Value).ToString.Trim
                    .pstrTipoMovimiento_STPING = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_STPING").Value).ToString.Trim

                    .pstrSatusUnidadDespacho_FUNDS2 = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_FUNDS2").Value).ToString.Trim
                    .pstrTipoDeposito_CTPDPS = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CTPDPS").Value).ToString.Trim
                    .pstrObservaciones_TOBSRV = ("" & dgMercaderiaSeleccionada.Rows(iCont).Cells("S_TOBSRV").Value).ToString.Trim
                    .pintNroItemOC_NRITOC = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NRITOC").Value
                    .pdecNumSolicitudDeReferencia_NSLCRF = decEmbarque
                    .pstrTipoOc_TPOOCM = Me.objHashTable("TPOOCM")
                    .pstrCodProvCliente_CPRPCL = Me.objHashTable("CPRPCL")
                    .pstrObservaciones_TOBSRV = pobjInformacionIngresada.pstrObservaciones_TOBSRV
                End With

                If bolClienteOutotec Then
                    oListaProyecto = New List(Of beProyecto)
                    oListaProyecto = VerProyecto_x_OrdenServicio(dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NORDSR").Value, dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NORCCL").Value, dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NRITOC").Value, intCliente_CCLNT, dgMercaderiaSeleccionada.Rows(iCont).Cells("S_QTRMC").Value, dgMercaderiaSeleccionada.Rows(iCont).Cells("S_CMRCLR").Value)
                    If oListaProyecto.Count > 0 Then
                        objTemp.oListaProyecto = oListaProyecto
                    End If
                End If
                objTemp.pbolEsOutotec = bolClienteOutotec
                objTemp.pstrTipoMovIntfz = strTipoMovIntfz.Trim
                objMovimientoMercaderia.AddItemMovimientoMercaderia(objTemp)
                iCont += 1
            End While
            ' Procedemos con el procesamiento de la información
            Dim msgMigracionCliente As String = ""
            Call pProcesarRecepcion(Me.chkServicio.Checked, msgMigracionCliente)

            If msgMigracionCliente <> "" Then
                MessageBox.Show(msgMigracionCliente, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Function BuscarEntregaTiempo() As String
        Dim decFechaActual As Decimal = HelpClass.CDate_a_Numero8Digitos(pobjInformacionIngresada.pstrFechaRealizacion_FRLZSR) ' Now.Date.ToString
        Dim decFechaMaxEntrega As Decimal = 0
        For intCont As Integer = 0 To Me.dgMercaderiaSeleccionada.Rows.Count - 1
            If Not dgMercaderiaSeleccionada.Rows(intCont).Cells("S_FMPDME").Value.ToString.Equals("0") And Not dgMercaderiaSeleccionada.Rows(intCont).Cells("S_FMPDME").Value.ToString.Equals("") Then
                decFechaMaxEntrega = HelpClass.CDate_a_Numero8Digitos(dgMercaderiaSeleccionada.Rows(intCont).Cells("S_FMPDME").Value.ToString)
                Exit For
            End If
        Next
        If decFechaActual > decFechaMaxEntrega Then
            Return "N"
        Else
            Return "S"
        End If
    End Function

    Private Sub bsaGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bsaGuardar.Click
        Try
            intNroGuiaRansa = Decimal.Parse(Me.txtGuiaIngreso.Text)
            'Dim msgPendienteUbicacion As String = ""
            Dim QTotalRecepcion As Decimal = 0
            Dim PesoTotRecepcion As Decimal = 0
            'For Each Item As DataGridViewRow In dgMercaderiaSeleccionada.Rows
            '    QTotalRecepcion = QTotalRecepcion + Item.Cells("S_QTRMC").Value
            '    PesoTotRecepcion = PesoTotRecepcion + Item.Cells("S_QTRMP").Value
            'Next
            '  ValidarUbicacionDescarga_X_Posicion
            'msgPendienteUbicacion = UcUbicaciones_Lista.ValidarUbicacionDescarga(Ransa.Controls.ucUbicaciones_Dg.TipoProceso.RECEPCION, QTotalRecepcion, PesoTotRecepcion)
            'Dim msgGeneral As String = ""
            'Dim msgUbicacion As String = ""
            Dim msg As String = ""
            Dim SKU As String = ""
            Dim FlagCtrlUbicacion As Boolean = False
            Dim msgAlerta As String = ""
            Dim msgError As String = ""
            Dim pExisteError As Boolean = False
            For FilaReg As Integer = 0 To dgMercaderiaSeleccionada.Rows.Count - 1

                If Manejo_x_Posicion_NivelCliente = "S" Then
                    FlagCtrlUbicacion = True
                Else
                    If ("" & dgMercaderiaSeleccionada.Rows(FilaReg).Cells("FUBICAC").Value) = "X" Then
                        FlagCtrlUbicacion = True
                    Else
                        FlagCtrlUbicacion = False
                    End If
                End If

                QTotalRecepcion = dgMercaderiaSeleccionada.Rows(FilaReg).Cells("S_QTRMC").Value
                PesoTotRecepcion = dgMercaderiaSeleccionada.Rows(FilaReg).Cells("S_QTRMP").Value
                SKU = ("" & dgMercaderiaSeleccionada.Rows(FilaReg).Cells("S_CMRCLR").Value)
                'msg = UcUbicaciones_Lista.ValidarUbicacionDescarga_X_Posicion(pExisteError, FlagCtrlUbicacion, Ransa.Controls.ucUbicaciones_Dg.TipoProceso.RECEPCION, QTotalRecepcion, PesoTotRecepcion, SKU, 0, FilaReg)
                msg = UcUbicaciones_Lista.Validar_X_Posicion_Recepcion(pExisteError, FlagCtrlUbicacion, QTotalRecepcion, PesoTotRecepcion, SKU, 0, FilaReg)

                If pExisteError = True Then
                    If msg <> "" Then
                        msgError = msgError & msg & Chr(10)
                    End If
                Else
                    If msg <> "" Then
                        msgAlerta = msgAlerta & msg & Chr(10)
                    End If
                End If
            Next

            msgError = msgError.Trim
            msgAlerta = msgAlerta.Trim

            If msgError.Length > 0 Then
                MessageBox.Show(msgError, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            If msgAlerta.Length > 0 And Mensaje_AlertaDiferencia_x_ManejoPosiciones = "S" Then
                If MessageBox.Show(msgAlerta & Chr(13) & " ¿Desea continuar?", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            'If Manejo_x_Posicion = "S" Then
            '    If Mensaje_posicion_obligatoriedad = "S" Then
            '        If msgPendienteUbicacion.Length > 0 Then
            '            MessageBox.Show(msgPendienteUbicacion, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '            Exit Sub
            '        End If
            '    Else
            '        If msgPendienteUbicacion.Length > 0 Then
            '            If MessageBox.Show(msgPendienteUbicacion & Chr(13) & " ¿Desea continuar?", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
            '                Exit Sub
            '            End If
            '        End If
            '    End If
            'End If
            UcUbicaciones_Lista.Guardar(intNroGuiaRansa)
            If Me.chkEtiqueta.Checked Then
                ImprimirCodigoDeBarra(intNroGuiaRansa)
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub ImprimirCodigoDeBarra(ByVal intNroGuiaRansa As Decimal)
        Dim ofrmImprimirMercaderia As New frmListaCodigoImprimir
        With ofrmImprimirMercaderia
            .PNNGUIRN = intNroGuiaRansa
            .PNCCLNT = intCliente_CCLNT
            .ShowDialog()
        End With



        'Dim fInput As frmInput = New frmInput
        'Dim strMensajeError As String = ""
        'With fInput
        '    .pTitulo = "Etiqueta"
        '    .pMensaje = "¿ Desea imprimir la etiqueta ? " & vbNewLine & vbNewLine & "Ingrese el Nro. de Copias : "
        '    If .ShowDialog = Windows.Forms.DialogResult.OK Then
        '        ' If oPrinterZebra.StartWrite(GLOBAL_IMPRESORA_ZEBRA) Then
        '        Dim intNroCopias As Int16 = 0
        '        If Int16.TryParse(.pInput, intNroCopias) Then
        '            Dim obrBulto As New brBulto
        '            obeBulto.PNCATCOPYA = intNroCopias
        '            'Dim lstrEtiquetas As List(Of String) = clsAlmacen.flstrEtiqueta_Bulto(intCliente, strBulto, False, intNroCopias, strMensajeError)
        '            Dim lstrEtiquetas As List(Of String) = obrBulto.ImprimirEtiquetaBulto(obeBulto)
        '            Dim strEtiqTemp As String = ""
        '            For Each strEtiqTemp In lstrEtiquetas
        '                ' oPrinterZebra.Write(strEtiqTemp)
        '                RawPrinterHelper.SendStringToPrinter(GLOBAL_IMPRESORA_ZEBRA, strEtiqTemp)
        '            Next
        '        End If
        '        ' Else
        '        '    MessageBox.Show(oPrinterZebra.ErrorMessage, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        'End If
        '        'oPrinterZebra.EndWrite()
        '    End If
        '    .Dispose()
        '    fInput = Nothing
        'End With
    End Sub
    Private Sub bsaCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bsaCancelar.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub bsaCancelarRecepcion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bsaCancelarRecepcion.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    'Private Sub bsaSugerenciaUbicacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim frmPrintForm As New PrintForm
    '        Dim orptUbicacionGuiaIngresoS As New rptUbicacionGuiaIngresoS()
    '        Dim obeSugerenciaMercaderiaGuia As New beSugerenciaMercaderiaGuia()
    '        Dim oblUbicacionSugeridaMercaderiaGuia As New blUbicacionSugeridaMercaderiaGuia()
    '        Dim dt As New DataTable
    '        obeSugerenciaMercaderiaGuia.CCLNT2 = Me.intCliente_CCLNT
    '        obeSugerenciaMercaderiaGuia.NGUIRN = Me.intNroGuiaRansa
    '        dt = oblUbicacionSugeridaMercaderiaGuia.ListarSugerenciaMercaderia(obeSugerenciaMercaderiaGuia)
    '        orptUbicacionGuiaIngresoS.SetDataSource(dt)
    '        orptUbicacionGuiaIngresoS.Refresh()
    '        orptUbicacionGuiaIngresoS.SetParameterValue("pUsuario", objSeguridadBN.pUsuario)
    '        orptUbicacionGuiaIngresoS.SetParameterValue("pnumGuia", "GUÍA Nº:" & Me.intNroGuiaRansa.ToString.Trim)
    '        frmPrintForm.MaximizeBox = True
    '        frmPrintForm.ShowForm(orptUbicacionGuiaIngresoS, Me)
    '        Me.ShowDialog(Owner)
    '    Catch ex As Exception
    '    End Try
    'End Sub

#End Region


#Region "SoporteLote"

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click

        'adicionado soporte Lotes
        '--------------------------------------
        Try
            AgregarLote()
            VerLotes_x_OrdenServicio()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '----------------------------------
    End Sub
    Private Sub AgregarLote()

        If (dgMercaderiaSeleccionada.Rows.Count = 0) Then
            MessageBox.Show("No existen Ordenes de Servicio", "Asignación Lote", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If (ValidarIngresoParaAdicionLote() = False) Then
            Dim strMensaje As String = ""
            strMensaje = strMensaje & Chr(13) & "No tiene cantidades pendientes de asignación."
            MessageBox.Show(strMensaje, "Asignación de Lote", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim ofrmLotes As New frmLotes
        ofrmLotes.QPendienteLote = Val(("" & Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_LOTE").Value).ToString.Trim)
        ofrmLotes.ShowDialog(Me)
        If (ofrmLotes.myDialogOK = True) Then

            Dim numReg As Int64 = dgMercaderiaSeleccionada.Rows.Count - 1
            Dim oInfoLote As New beLoteDeMercaderia()
            oInfoLote = ObtenerDatosLoteMercaderia()
            Dim CantidaPendiente_a_Lote As Int64 = 0
            oInfoLote.PNQTRMC = ofrmLotes.infoLote.PNQTRMC
            oInfoLote.PNQINMC2 = ofrmLotes.infoLote.PNQTRMC
            oInfoLote.PSCRTLTE = ofrmLotes.infoLote.PSCRTLTE
            oInfoLote.PNFINGAL = ofrmLotes.infoLote.PNFINGAL
            oInfoLote.PNFPRDMR = ofrmLotes.infoLote.PNFPRDMR
            oInfoLote.PNFVNLTE = ofrmLotes.infoLote.PNFVNLTE
            oInfoLote.PSSTRNSM = "I"
            AdicionarLoteHash(oInfoLote, oInfoLote.PNQTRMC)
            ' se adiciona a la lista de Lotes agregados sin ser modificados

            Dim encontrado As Boolean = False
            For iCont As Integer = 0 To numReg
                encontrado = False
                If (oInfoLote.PNNORDSR = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NORDSR").Value) Then
                    CantidaPendiente_a_Lote = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_LOTE").Value
                    dgMercaderiaSeleccionada.Rows(iCont).Cells("S_LOTE").Value = CantidaPendiente_a_Lote - oInfoLote.PNQTRMC

                    Dim oLitsaLoteTag As New List(Of beLoteDeMercaderia)
                    oLitsaLoteTag = dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NORDSR").Tag
                    If (oLitsaLoteTag.Count > 0) Then
                        For Each oLote As beLoteDeMercaderia In oLitsaLoteTag
                            If (oLote.PSCRTLTE.Trim.ToUpper = oInfoLote.PSCRTLTE.Trim.ToUpper) Then
                                oLote.PNQINMC2 = oLote.PNQINMC2 + oInfoLote.PNQTRMC
                                oLote.PNFINGAL = oInfoLote.PNFINGAL
                                oLote.PNFPRDMR = oInfoLote.PNFPRDMR
                                oLote.PNFVNLTE = oInfoLote.PNFVNLTE
                                encontrado = True
                                Exit For
                            End If
                        Next
                        If (encontrado = False) Then
                            oLitsaLoteTag.Add(oInfoLote)
                        End If
                    Else
                        oLitsaLoteTag.Add(oInfoLote)
                    End If
                    dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NORDSR").Tag = Nothing
                    dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NORDSR").Tag = oLitsaLoteTag
                    Exit For
                End If
            Next
        End If



    End Sub
    Private Function ValidarIngresoParaAdicionLote() As Boolean
        Dim retorno As Boolean = True
        'Try
        If (dgMercaderiaSeleccionada.CurrentRow.Cells("S_LOTE").Value = 0) Then   'si es 0 todas las cantidades ya han sido asignadas a un lote          
            retorno = False
        End If
        'Catch : End Try
        Return retorno
    End Function
    Private Function ValidarIngresoLotesParaRecepcion() As Boolean
        Dim retorno As Boolean = True
        'Try
        Dim numReg As Int64 = 0
        Dim CantidadIngreso As Int64 = 0
        numReg = Me.dgMercaderiaSeleccionada.Rows.Count - 1
        For intContador As Int32 = 0 To numReg
            If (dgMercaderiaSeleccionada.Rows(intContador).Cells("S_LOTE").Value > 0) Then
                retorno = False
                Exit For
            End If
        Next
        'Catch : End Try
        Return retorno ' Si es true -> todas las cantidades de todas las ordenes de servicio estan asignado a un lote
    End Function
    Private Function ObtenerDatosLoteMercaderia() As beLoteDeMercaderia

        Dim obeLote As New beLoteDeMercaderia
        obeLote.PSCTPALM = ("" & Me.objHashTable("CTPALM")).ToString.Trim
        obeLote.PSCALMC = ("" & Me.objHashTable("CALMC")).ToString.Trim
        obeLote.PSCZNALM = ("" & Me.objHashTable("CZNALM")).ToString.Trim
        obeLote.PNCCLNT = Me.intCliente_CCLNT
        obeLote.PSCULUSA = objSeguridadBN.pUsuario
        obeLote.PNNORDSR = Val("" & Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value)
        obeLote.PNNGUIRN = Val(("" & Me.objHashTable("NGUICL")).ToString.Trim)
        Return obeLote
    End Function


    Private Sub VerLotes_x_OrdenServicio()
        dtgLotes.Rows.Clear()
        Dim oListaLote As New beList(Of beLoteDeMercaderia)
        Dim fecha As String = ""
        oListaLote = dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Tag
        For Each oLote As beLoteDeMercaderia In oListaLote
            dtgLotes.Rows.Add(oLote.PSCRTLTE, Convert.ToInt64(oLote.PNQINMC2), HelpClass.CNumero8Digitos_a_FechaDefault(oLote.PNFINGAL), HelpClass.CNumero8Digitos_a_FechaDefault(oLote.PNFPRDMR), HelpClass.CNumero8Digitos_a_FechaDefault(oLote.PNFVNLTE))
        Next
    End Sub

    Private Function VerProyecto_x_OrdenServicio(ByVal intNORDSR As Decimal, ByVal strNORCML As String, ByVal intNRITOC As Int32, ByVal intCCLNT As Int32, ByVal intQTRMC As Int32, ByVal strCMRCLR As String) As beList(Of beProyecto)
        Dim oProyecto As New beProyecto
        oProyecto.PSNORCML = strNORCML
        oProyecto.PNNRITOC = intNRITOC
        oProyecto.PNCCLNT = intCCLNT
        oProyecto.PNQTRMC = intQTRMC
        oProyecto.PSCMRCLR = strCMRCLR
        oProyecto.PNNORDSR = intNORDSR
        UcProyectos_Lista.EsDevolucion = _EsDevolucion
        Return UcProyectos_Lista.ListarProyectos(oProyecto)
    End Function

    'Private Sub ListarLoteMercaderiaInicial()

    '    Dim numReg As Int64 = dgMercaderiaSeleccionada.Rows.Count - 1
    '    Dim brLote As New brLoteDeMercaderia()
    '    Dim obeLoteFiltro As New beLoteDeMercaderiaFiltro
    '    For iCont As Integer = 0 To numReg
    '        obeLoteFiltro.PNNORDSR = Val(("" & Me.dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NORDSR").Value).ToString.Trim)
    '        dgMercaderiaSeleccionada.Rows(iCont).Cells("S_NORDSR").Tag = brLote.ListarLoteMercaderia_x_OrdenServicio(obeLoteFiltro)
    '    Next
    '    VerLotes_x_OrdenServicio()
    'End Sub
    'Private Function GuardarLotesMercaderia()
    '    Dim obrLoteMercaderia As New brLoteDeMercaderia()
    '    Dim obeLote As New beLoteDeMercaderia()
    '    obeLote = obrLoteMercaderia.InsertarLoteMercaderia(ObtenerListaLotes())
    '    Return obeLote
    'End Function

    'Private Function GuardarLotesMercaderia_JM()
    '    Dim obrLoteMercaderia As New brLoteDeMercaderia()
    '    Dim obeLote As New beLoteDeMercaderia()
    '    obeLote = obrLoteMercaderia.InsertarLoteMercaderia(ObtenerListaLotes())
    '    Return obeLote
    'End Function
    Private Sub VisualizarTabLote(ByVal ver As Boolean)
        tbAsignaSerieUbicacion.TabPages.Clear()
        If (ver = True) Then  'solo muestra el tab para lote
            tbAsignaSerieUbicacion.TabPages.Add(TabPageLotes)
        Else  ' muestra todos los tab
            tbAsignaSerieUbicacion.TabPages.Add(TabPageLotes)
            tbAsignaSerieUbicacion.TabPages.Add(TabPageSerie)
            tbAsignaSerieUbicacion.TabPages.Add(TabPageUbicacion)
            tbAsignaSerieUbicacion.TabPages.Add(TabPageProyecto)
        End If
    End Sub
    'Private Sub dgMercaderiaSeleccionada_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgMercaderiaSeleccionada.CellClick
    '    Try
    '        If (dgMercaderiaSeleccionada.CurrentCellAddress.Y < 0) Then
    '            Exit Sub
    '        End If
    '        'Dim strNORCML As String = Me.dgMercaderiaSeleccionada.Item("S_NORCCL", e.RowIndex).Value
    '        'Dim intNRITOC As Int32 = Me.dgMercaderiaSeleccionada.Item("S_NRITOC", e.RowIndex).Value
    '        'Dim intCCLNT As Int32 = intCliente_CCLNT
    '        'Dim intQTRMC As Int32 = Me.dgMercaderiaSeleccionada.Item("S_QTRMC", e.RowIndex).Value
    '        VerLotes_x_OrdenServicio()
    '        'VerProyecto_x_OrdenServicio(strNORCML, intNRITOC, intCCLNT, intQTRMC)
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub AdicionarLoteHash(ByVal obeLote As beLoteDeMercaderia, ByVal Cantidad As Int64)
        Dim PSCRTLTE_nomLote As String = ""
        Dim PNNORDSR_OS As Int64 = 0
        PSCRTLTE_nomLote = obeLote.PSCRTLTE.Trim
        PNNORDSR_OS = obeLote.PNNORDSR
        If (oHashListaLotes.ContainsKey(PNNORDSR_OS) = True) Then
            Dim obeHasLote As New Hashtable()
            Dim oHas_x_OS As New Hashtable()
            oHas_x_OS = oHashListaLotes(PNNORDSR_OS)
            If (oHas_x_OS.ContainsKey(PSCRTLTE_nomLote) = True) Then
                Dim oLote As New beLoteDeMercaderia
                oLote = oHas_x_OS(PSCRTLTE_nomLote)
                oLote.PNQTRMC = oLote.PNQTRMC + Cantidad
                oHas_x_OS(PSCRTLTE_nomLote) = oLote
            Else
                oHas_x_OS.Add(PSCRTLTE_nomLote, obeLote)
            End If
            oHashListaLotes(PNNORDSR_OS) = oHas_x_OS
        Else
            Dim obeHasLote As New Hashtable()
            obeHasLote.Add(PSCRTLTE_nomLote, obeLote)
            oHashListaLotes.Add(PNNORDSR_OS, obeHasLote)
        End If

    End Sub
    Private Function ObtenerListaLotes() As List(Of beLoteDeMercaderia)

        Dim oListaLote As New List(Of beLoteDeMercaderia)
        For Each entryP As DictionaryEntry In oHashListaLotes
            For Each entryS As DictionaryEntry In entryP.Value
                Dim oLote As New beLoteDeMercaderia()
                oLote = entryS.Value
                oListaLote.Add(oLote)
            Next
        Next
        Return oListaLote
    End Function



    'Private Sub dgMercaderiaSeleccionada_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgMercaderiaSeleccionada.KeyUp

    '    Try
    '        Select Case e.KeyCode
    '            Case Keys.Up, Keys.Down, Keys.Enter
    '                If (dgMercaderiaSeleccionada.CurrentCellAddress.Y < 0) Then
    '                    Exit Sub
    '                End If
    '                VerLotes_x_OrdenServicio()
    '        End Select
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub UcProyectos_Lista_AgregarProyecto() Handles UcProyectos_Lista.AgregarProyecto
        Dim ofrmAgregarProyecto As New frmAgregarProyecto
        Dim objOrdenCompra As New beOrdenCompra
        With objOrdenCompra
            .PNCCLNT = intCliente_CCLNT
            .PSNORCML = Me.objHashTable("NORCCL")
            .PNNRITOC = dgMercaderiaSeleccionada.CurrentRow.Cells("S_NRITOC").Value
            .PSTCITCL = dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value
        End With
        ofrmAgregarProyecto.objOrdenCompra = objOrdenCompra
        If ofrmAgregarProyecto.ShowDialog = Windows.Forms.DialogResult.OK Then
            VerProyecto_x_OrdenServicio(Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORCCL").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NRITOC").Value, intCliente_CCLNT, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_QTRMC").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value)
        End If
    End Sub

#End Region



    Private Sub btnOcultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOcultar.Click
        If hgGuiaRemision.HeaderPositionPrimary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top Then
            Call pOcultarInformacionCabecera(True)
        Else
            Call pOcultarInformacionCabecera(False)
        End If
    End Sub

    Private Sub pOcultarInformacionCabecera(ByVal blnStatus As Boolean, Optional ByVal btn As Integer = 0)
        If blnStatus Then
            hgGuiaRemision.HeaderPositionPrimary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Right
            hgGuiaRemision.Width = 25
            btnOcultar.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Near
            btnOcultar.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.FixedRight
            btnOcultar.Text = "Visualizar"
        Else
            hgGuiaRemision.HeaderPositionPrimary = ComponentFactory.Krypton.Toolkit.VisualOrientation.Top
            hgGuiaRemision.Width = intWidth
            btnOcultar.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Far
            btnOcultar.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Inherit
            btnOcultar.Text = "Ocultar"
        End If

        If btn = 0 Then
            If blnStatus Then
                SplitDistance = KryptonSplitContainer1.SplitterDistance
                KryptonSplitContainer1.SplitterDistance = KryptonSplitContainer1.Width - 30
            Else
                KryptonSplitContainer1.SplitterDistance = SplitDistance
            End If
        Else
            SplitDistance = 605
            If blnStatus Then
                KryptonSplitContainer1.SplitterDistance = KryptonSplitContainer1.Width - 30
            Else
                KryptonSplitContainer1.SplitterDistance = SplitDistance
            End If
        End If
    End Sub

    Private Sub bstEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bsaEliminarItem.Click
        Try

            Dim EliminarRegistro As Boolean = False
            If Me.dgMercaderiaSeleccionada.RowCount = 0 OrElse Me.dgMercaderiaSeleccionada.CurrentCellAddress.Y < 0 Then
                MessageBox.Show("No existe información.", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            UcSerie_Lista.EliminarSeries(dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NRITOC").Value)
            Call Sleep(500) 'espera por 2 segundos
            EliminarRegistro = True
            If EliminarRegistro Then
                dgMercaderiaSeleccionada.Rows.Remove(dgMercaderiaSeleccionada.CurrentRow)
                dgMercaderiaSeleccionada.EndEdit()
                dgMercaderiaSeleccionada.CurrentRow.Selected = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'If Me.dgMercaderiaSeleccionada.RowCount = 0 Then Me.UcSerie_Lista.IngresoSeries(0, 0, Me.intCliente_CCLNT, Me.intNroGuiaRansa, objSeguridadBN.pUsuario)
    End Sub


    Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long) 'Pausa


    'Private Sub dgMercaderiaSeleccionada_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgMercaderiaSeleccionada.CellClick
    '    Try

    '        ListaLotesPorOS()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub






    Private Sub bsaAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bsaAgregar.Click
        Try
            If dgMercaderiaSeleccionada.Rows.Count = 0 Then Exit Sub
            Dim frmMantenimientoLote As New frmMantenimientoLote
            Dim objLotes As New beLote
            frmMantenimientoLote.NUEVO = True
            frmMantenimientoLote.CCLNT = intCliente_CCLNT
            frmMantenimientoLote.NORDSR = dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value.ToString.Trim
            frmMantenimientoLote.FlagRecepcion = 1
            If frmMantenimientoLote.ShowDialog() = Windows.Forms.DialogResult.OK Then
                ListaLotesPorOrdenServicio()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub


    'Private Sub dgvLotes_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvLotes.CurrentCellChanged
    '    Try

    '        Dim OS As Int64 = 0
    '        Dim OrdenCompra As String = ""
    '        Dim NroItem As Int64 = 0
    '        Dim CantidadTrx As Decimal = 0
    '        Dim CantidadTrxLote As Decimal = 0
    '        Dim Usuario As String = ""
    '        Dim sku As String = ""
    '        Dim CodTipoAlmacen As String = ""
    '        Dim CodAlmacen As String = ""
    '        Dim FilaReg As Int64 = 0
    '        Dim CorrLote As Int64 = 0

    '        If dgvLotes.CurrentRow IsNot Nothing Then
    '            OS = Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value
    '            sku = Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value
    '            CodTipoAlmacen = Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CTPALM").Value
    '            CodAlmacen = Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CALMC").Value
    '            CantidadTrxLote = Me.dgvLotes.CurrentRow.Cells("N_CANTIDAD").Value
    '            FilaReg = dgMercaderiaSeleccionada.CurrentRow.Index
    '            CorrLote = dgvLotes.CurrentRow.DataBoundItem.Item("NCRRIN")
    '            'Me.UcUbicaciones_Lista.ConsultarIngresos(Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value, intCliente_CCLNT, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CTPALM").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CALMC").Value, Me.dgvLotes.CurrentRow.Cells("N_CANTIDAD").Value, objSeguridadBN.pUsuario, False, dgvLotes.CurrentRow.DataBoundItem.Item("NCRRIN"), dgMercaderiaSeleccionada.CurrentRow.Index, strZona)
    '            Me.UcUbicaciones_Lista.ConsultarIngresos(OS, intCliente_CCLNT, sku, CodTipoAlmacen, CodAlmacen, CantidadTrxLote, objSeguridadBN.pUsuario, False, CorrLote, FilaReg, strZona)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try


    'End Sub

    Private Sub ListaLotesPorOrdenServicio()
        'Try
        If dgMercaderiaSeleccionada.RowCount > 0 And dgMercaderiaSeleccionada.CurrentRow IsNot Nothing Then
            Dim fila As Integer = 0
            Dim objLote As New beLote
            Dim brLote As New brLote
            Dim oDt As New DataTable
            Dim tempList As New List(Of beLote)
            objLote.NORDSR = dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value
            fila = dgMercaderiaSeleccionada.CurrentRow.Index
            Dim key As New KeyValuePair(Of String, Int32)(objLote.NORDSR & "-" & fila, 0)
            Dim oDtemp As DataTable = DirectCast(oHDtLotes(key), DataTable)
            If oDtemp Is Nothing Then
                oDtemp = brLote.ListaDeLotesPorOC(objLote)
                oHDtLotes.Add(key, oDtemp)
            Else
                'Actualizar.... La temporal
                oDtemp = ActualizarTablaTemporalLotes(oDtemp, brLote.ListaDeLotesPorOC(objLote))
                oHDtLotes.Remove(key)
                oHDtLotes.Add(key, oDtemp)
            End If
            dgvLotes.AutoGenerateColumns = False
            boolCargar = False
            dgvLotes.DataSource = oDtemp
            boolCargar = True

            If oDtemp.Rows.Count = 0 Then

                Call pOcultarInformacionCabecera(True, 1)
                'ShowData = True
            Else
                'If ShowData Then
                Call pOcultarInformacionCabecera(False, 1)
                '    ShowData = False
                'End If

            End If
        End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private boolCargar As Boolean = True

    Private Function ActualizarTablaTemporalLotes(ByVal oDtTemp As DataTable, ByVal oDt As DataTable) As DataTable
        Dim dtNewData As New DataTable
        dtNewData = oDtTemp.Copy()
        'Try
        dtNewData.PrimaryKey = New DataColumn() {dtNewData.Columns("NCRRIN")}
        For Each dr As DataRow In oDt.Rows
            If dtNewData.Rows.Contains(dr("NCRRIN")) = False Then
                dtNewData.ImportRow(dr)
            End If
        Next
        'Catch ex As Exception
        '    dtNewData = Nothing
        'End Try
        Return dtNewData
    End Function


    'Private Sub dgvLotes_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLotes.CellValueChanged
    '    Try

    '        If dgvLotes.CurrentRow IsNot Nothing Then
    '            Me.UcUbicaciones_Lista.ConsultarIngresos(Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value, intCliente_CCLNT, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CTPALM").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CALMC").Value, Me.dgvLotes.CurrentRow.Cells("N_CANTIDAD").Value, objSeguridadBN.pUsuario, False, dgvLotes.CurrentRow.DataBoundItem.Item("NCRRIN"), dgMercaderiaSeleccionada.CurrentRow.Index, strZona)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try


    'End Sub


    Private Function ValidarCantidadIngresadaporMercaderia(ByVal fila As Integer, ByVal Cantidad As String) As Boolean
        Dim CantMercaderia As Decimal = 0
        Dim sumaCantidades As Decimal = 0
        Dim IsError As Boolean
        'Dim NORDSR As String
        If dgMercaderiaSeleccionada.CurrentRow IsNot Nothing Then
            'NORDSR = dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value
            CantMercaderia = Val(dgMercaderiaSeleccionada.CurrentRow.Cells("S_QTRMC").Value)
            dgvLotes.Rows(fila).Cells("N_CANTIDAD").Value = Convert.ToDecimal(Cantidad)
            If dgvLotes.RowCount > 0 Then
                For Each row As DataGridViewRow In dgvLotes.Rows
                    sumaCantidades = sumaCantidades + Convert.ToDecimal(row.Cells.Item("N_CANTIDAD").Value)
                Next
                If sumaCantidades > CantMercaderia Then
                    IsError = False
                    Return False
                Else
                    IsError = True
                    Return True
                End If
            End If
        End If
    End Function

    Private Sub dgvLotes_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvLotes.CellValidating
        Try
            'N_CANTIDAD
            'If e.FormattedValue.ToString = 0 Then Exit Sub
            ' If e.ColumnIndex = 0 And boolCargar Then
            If dgvLotes.CurrentRow Is Nothing Then Exit Sub
            If dgvLotes.Columns(e.ColumnIndex).Name = "N_CANTIDAD" And boolCargar Then
                Dim ValorCantidad As String = e.FormattedValue.ToString
                Dim fila As Integer = e.RowIndex
                If ValidarCantidadIngresadaporMercaderia(fila, ValorCantidad) = False Then
                    MessageBox.Show("Suma de cantidad recibir del lote debe de ser igual a Cantidad recibir mercadería.")
                    e.Cancel = True
                    Me.dgvLotes.CurrentCell.ErrorText = "la cantidad ingresada es mayor que  el stock en el  almacén."
                Else
                    Me.dgvLotes.CurrentCell.ErrorText = ""
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Private Function validacion(ByRef msj As String) As Boolean
        Dim r As Integer = 0

        For i As Integer = 0 To dgMercaderiaSeleccionada.Rows.Count - 1
            Dim NORDSR As String = dgMercaderiaSeleccionada.Rows(i).Cells("S_NORDSR").Value
            Dim FUNCTL As String = dgMercaderiaSeleccionada.Rows(i).Cells("FUNCTL").Value
            Dim QTRMC As Decimal = Val(dgMercaderiaSeleccionada.Rows(i).Cells("S_QTRMC").Value)

            If FUNCTL.Equals("X") Then
                'Valida Orden de Servicio Tiene Lotes Asignados
                If ValidarTieneLotesAsiganados(NORDSR, i) Then
                    'Valida La cantidad de mercaderia es igual a la suma de cantidades recibir Lote
                    If Not ValidarCantidadMercaderiaLotes(NORDSR, i, QTRMC) Then
                        msj = "Suma de cantidades recibir del lote debe de ser igual a cantidad mercaderia.:" & dgMercaderiaSeleccionada.Rows(i).Cells("S_CMRCLR").Value & ", Posición :" & i + 1
                        Return False
                    End If
                Else
                    msj = "Debe de tener lotes asignados la Mercadería :" & dgMercaderiaSeleccionada.Rows(i).Cells("S_CMRCLR").Value & ", Posición :" & i + 1
                    Return False
                End If
                'Else
                '    If ValidarTieneLotesAsiganados(NORDSR, i) Then
                '        If Not ValidarCantidadMercaderiaLotes(NORDSR, i, QTRMC) Then
                '            'S_CMRCLR
                '            'dgMercaderiaSeleccionada.Rows(i).Cells("S_CMRCLR").Value
                '            msj = "Suma de cantidades recibir del lote debe de ser igual a cantidad mercaderia :" & dgMercaderiaSeleccionada.Rows(i).Cells("S_CMRCLR").Value & ", Posición :" & i + 1
                '            Return False
                '        End If
                '    End If
            End If
        Next
        Return True
    End Function


    Private Function ValidarCantidadMercaderiaLotes(ByVal NORDSR As String, ByVal fila As Integer, ByVal Cantidad As String) As Boolean
        Dim CantMercaderia As Decimal = 0
        Dim sumaCantidades As Decimal = 0
        Dim respuesta As Boolean = False
        Dim key As New KeyValuePair(Of String, Int32)(NORDSR & "-" & fila, 0)
        Dim oDtemp As DataTable = DirectCast(oHDtLotes(key), DataTable)
        If oDtemp IsNot Nothing Then
            For Each row As DataRow In oDtemp.Rows
                sumaCantidades = sumaCantidades + Convert.ToDecimal(row.Item("CANTIDAD"))
            Next
            If sumaCantidades = Cantidad Then
                respuesta = True
            Else
                respuesta = False
            End If
        End If
        Return respuesta
    End Function


    Private Function ValidarTieneLotesAsiganados(ByVal NORDSR As String, ByVal fila As Integer) As Boolean
        Dim Resultado As Boolean
        Dim objLote As New beLote
        Dim brLote As New brLote
        objLote.NORDSR = NORDSR
        Dim key As New KeyValuePair(Of String, Int32)(NORDSR & "-" & fila, 0)
        Dim oDtemp As DataTable = DirectCast(oHDtLotes(key), DataTable)
        If oDtemp Is Nothing Then
            oDtemp = brLote.ListaDeLotesPorOC(objLote)
            oHDtLotes.Add(key, oDtemp)
        End If
        If oDtemp.Rows.Count > 0 Then
            Resultado = True
        End If
        Return Resultado
    End Function

    Private Sub Grabarlotes(ByVal objMovimientoMercaderia As clsMovimientoMercaderia, ByVal intNroGuiaRansa As String)
        Dim obeLote As beLote
        Dim i As Int32 = 0
        Dim obrLote As New brLote

        For Each objItemMovimientoMercaderia As clsItemMovimientoMercaderia In objMovimientoMercaderia.plstItemMovimientoMercaderia
            i = i
            Dim key As New KeyValuePair(Of String, Int32)(objItemMovimientoMercaderia.pintOrdenServicio_NORDSR.ToString & "-" & i, 0)

            If oHDtLotes(key) IsNot Nothing Then
                Dim oDtemp As DataTable = DirectCast(oHDtLotes(key), DataTable)
                For Each Item As DataRow In oDtemp.Rows
                    If Val(Item("CANTIDAD")) > 0 Then
                        obeLote = New beLote
                        With obeLote
                            .NORDSR = objItemMovimientoMercaderia.pintOrdenServicio_NORDSR 'Orden de servicio
                            .NCRRIN = Item("NCRRIN") 'correlativo ingreso (PK de Lote)
                            .FINGAL = objMovimientoMercaderia.pintFechaRealizacion_FRLZSR 'Fecha de recepción.
                            .CULUSA = objSeguridadBN.pUsuario 'Usuario as400.
                            .QTRMC = Val(Item("CANTIDAD")) 'cantidad recibir lote.
                            .QTRMP = 0 'peso recibir lote.
                            .CTPOAT = "I" 'movimiento de ingreso
                            .NGUIRN = intNroGuiaRansa   'guía ransa.
                            .NSLCS1 = objItemMovimientoMercaderia.pintNrSolicitudServicio_NSLCSR  'Solicitud de servicio del OS. 
                            .CTPALM = objItemMovimientoMercaderia.pstrTipoAlmacen_CTPALM  'Tipo de alm.
                            .CALMC = objItemMovimientoMercaderia.pstrAlmacen_CALMC  'almacen.
                            .CZNALM = objItemMovimientoMercaderia.pstrZonaAlmacen_CZNALM 'Zona alamcén.
                        End With
                        obrLote.RegistrarRecepcionLote(obeLote)
                        'If Not obrLote.RegistrarRecepcionLote(obeLote) Then
                        '    HelpClass.ErrorMsgBox()
                        '    Exit Sub
                        'End If
                    End If
                Next
            End If
            i = i + 1
        Next
    End Sub


    Private Sub dgvLotes_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvLotes.EditingControlShowing
        If dgvLotes.CurrentRow Is Nothing Then Exit Sub
        If dgvLotes.Columns(dgvLotes.CurrentCell.ColumnIndex).Name = "S_SF_FINGAL" Then
            ' If dgvLotes.CurrentCell.ColumnIndex = 2 Then
            Dim validar As TextBox = CType(e.Control, TextBox)
            If validar IsNot Nothing Then
                ' agregar el controlador de eventos para el KeyPress   
                AddHandler validar.KeyPress, AddressOf validar_Keypress
            Else
                RemoveHandler validar.KeyPress, AddressOf validar_Keypress
            End If
        End If

    End Sub

    Private Sub validar_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        ' obtener indice de la columna   
        Dim columna As Integer = dgvLotes.CurrentCell.ColumnIndex

        ' comprobar si la celda en edición corresponde a la columna 1 o 3   
        'S_SF_FINGAL
        ' If columna = 2 Then
        If dgvLotes.Columns(columna).Name = "S_SF_FINGAL" Then  'columna = 2 Then
            IsEstado = 1
            ' Obtener caracter   
            Dim caracter As Char = e.KeyChar

            ' comprobar si el caracter es un número o el retroceso   
            If Not Char.IsNumber(caracter) And (caracter = ChrW(Keys.Back)) = False And caracter <> "." And caracter <> "," Then
                'Me.Text = e.KeyChar   
                e.KeyChar = Chr(0)
            End If
        End If
    End Sub

    Private Sub dgMercaderiaSeleccionada_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgMercaderiaSeleccionada.DataError
        If (e.Context = DataGridViewDataErrorContexts.Display) _
            Then
            MessageBox.Show("parsing error")
        End If

    End Sub

    Private Sub dgMercaderiaSeleccionada_SelectionChanged(sender As Object, e As EventArgs) Handles dgMercaderiaSeleccionada.SelectionChanged
        Try

            Dim OS As Int64 = 0
            Dim OrdenCompra As String = ""
            Dim NroItem As Int64 = 0
            Dim CantidadTrx As Decimal = 0
            Dim Usuario As String = ""
            Dim sku As String = ""
            Dim CodTipoAlmacen As String = ""
            Dim CodAlmacen As String = ""
            Dim FilaReg As Int64 = 0
            If dgMercaderiaSeleccionada.RowCount = 0 OrElse dgMercaderiaSeleccionada.CurrentCellAddress.Y < 0 Or dgMercaderiaSeleccionada.CurrentRow Is Nothing Then Exit Sub
            ' If dgMercaderiaSeleccionada.CurrentRow IsNot Nothing Then ListaLotesPorOrdenServicio() 'JM :>  ' descomentar luego..hasta solucionar recepcion por lote
            OS = dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value
            OrdenCompra = dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORCCL").Value
            NroItem = dgMercaderiaSeleccionada.CurrentRow.Cells("S_NRITOC").Value
            CantidadTrx = dgMercaderiaSeleccionada.CurrentRow.Cells("S_QTRMC").Value
            sku = dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value
            CodTipoAlmacen = dgMercaderiaSeleccionada.CurrentRow.Cells("S_CTPALM").Value
            CodAlmacen = dgMercaderiaSeleccionada.CurrentRow.Cells("S_CALMC").Value
            Usuario = objSeguridadBN.pUsuario
            FilaReg = dgMercaderiaSeleccionada.CurrentRow.Index
            'UcSerie_Lista.IngresoSeries(OS, NroItem, CantidadTrx, intCliente_CCLNT, intNroGuiaRansa, Usuario)

            UcSerie_Lista.IngresoSeries_V2(OS, FilaReg, NroItem, CantidadTrx, intCliente_CCLNT, intNroGuiaRansa, Usuario)

            '

            'If dgvLotes.RowCount = 0 Then 'jm si la mercaderia selecionada  no tiene lotes carga todas las ubicaciones
            'Me.UcUbicaciones_Lista.ConsultarIngresos(Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value, intCliente_CCLNT, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CTPALM").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CALMC").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_QTRMC").Value, objSeguridadBN.pUsuario, False, 0, dgMercaderiaSeleccionada.CurrentRow.Index, strZona)
            ' UcUbicaciones_Lista.ConsultarIngresos(OS, intCliente_CCLNT, sku, CodTipoAlmacen, CodAlmacen, CantidadTrx, Usuario, False, 0, FilaReg, strZona)
            '  End If
            UcUbicaciones_Lista.ConsultarIngresos(OS, intCliente_CCLNT, sku, CodTipoAlmacen, CodAlmacen, CantidadTrx, Usuario, False, 0, FilaReg, strZona)
            'VerProyecto_x_OrdenServicio(Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORCCL").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NRITOC").Value, intCliente_CCLNT, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_QTRMC").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value)
            VerProyecto_x_OrdenServicio(OS, OrdenCompra, NroItem, intCliente_CCLNT, CantidadTrx, sku)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub dgvLotes_SelectionChanged(sender As Object, e As EventArgs) Handles dgvLotes.SelectionChanged
        Try

            Dim OS As Int64 = 0
            Dim OrdenCompra As String = ""
            Dim NroItem As Int64 = 0
            Dim CantidadTrx As Decimal = 0
            Dim CantidadTrxLote As Decimal = 0
            Dim Usuario As String = ""
            Dim sku As String = ""
            Dim CodTipoAlmacen As String = ""
            Dim CodAlmacen As String = ""
            Dim FilaReg As Int64 = 0
            Dim CorrLote As Int64 = 0

            If dgvLotes.CurrentRow IsNot Nothing Then
                OS = Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value
                sku = Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value
                CodTipoAlmacen = Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CTPALM").Value
                CodAlmacen = Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CALMC").Value
                CantidadTrxLote = Me.dgvLotes.CurrentRow.Cells("N_CANTIDAD").Value
                FilaReg = dgMercaderiaSeleccionada.CurrentRow.Index
                CorrLote = dgvLotes.CurrentRow.DataBoundItem.Item("NCRRIN")
                'Me.UcUbicaciones_Lista.ConsultarIngresos(Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_NORDSR").Value, intCliente_CCLNT, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CMRCLR").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CTPALM").Value, Me.dgMercaderiaSeleccionada.CurrentRow.Cells("S_CALMC").Value, Me.dgvLotes.CurrentRow.Cells("N_CANTIDAD").Value, objSeguridadBN.pUsuario, False, dgvLotes.CurrentRow.DataBoundItem.Item("NCRRIN"), dgMercaderiaSeleccionada.CurrentRow.Index, strZona)
                Me.UcUbicaciones_Lista.ConsultarIngresos(OS, intCliente_CCLNT, sku, CodTipoAlmacen, CodAlmacen, CantidadTrxLote, objSeguridadBN.pUsuario, False, CorrLote, FilaReg, strZona)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
