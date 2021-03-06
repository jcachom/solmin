Imports Ransa.DAO.OrdenCompra
Imports RANSA.NEGO
Imports RANSA.Utilitario
Imports Db2Manager.RansaData.iSeries.DataObjects
Imports Ransa.TypeDef.OrdenCompra.OrdenCompra
Imports Ransa.TypeDef.OrdenCompra.ItemOC
Imports Ransa.TypeDef
Imports System.IO
Imports Ransa.DATA

Public Class frmImportarOC
#Region "Declaracion de variables"
    Private _dblCodCliente As String = "0"
    Private AUXCODCLIENTE As String = ""
    Private olbeDetOc As New List(Of beOrdenCompra)
    Private NEMPFAC As Decimal = 0

    Private olbeOrdenDeCompraNotas As New List(Of beOrdenCompra)
    Private olbeOrdenDeCompra As New List(Of beOrdenCompra)
    Private olbeOrdenDeCompraProveedor As New List(Of beProveedor)
    Public _strUser As String = ""
    Public _strHostName As String = ""
    Private strRef As String = ""
    Private strOC As String = ""
    Private MatchOrdenDeCompra As New Predicate(Of beOrdenCompra)(AddressOf BuscarOrdendeCompra)
#End Region

#Region "Funciones y procedimientos"

    Private Function ObtenerCodigoProveedor(ByVal obeProveedor As beProveedor) As Decimal

        Dim decCodProv As Decimal = 0
        Dim obrProveedor As New brProveedor


        decCodProv = GrabarProveedoCliente(obeProveedor)
        If decCodProv = 0 Then
            Return 0
        End If

        Return decCodProv
    End Function

    Private Function GrabarProveedoCliente(ByVal obeProveedor As beProveedor) As Decimal

        Dim decCodProv As Decimal = 0
        Dim obrProveedor As New brProveedor
        decCodProv = obrProveedor.GrabarProveedorDeCliente_v2(obeProveedor)
        If decCodProv = 0 Then
            'HelpClass.ErrorMsgBox()
            'Return 0
        End If
        Return decCodProv
    End Function

    Public Function BuscarOrdendeCompra(ByVal pbeOrdenCompra As beOrdenCompra) As Boolean
        If (pbeOrdenCompra.PSNORCML.Trim.Equals(strOC.Trim)) Then
            strRef = pbeOrdenCompra.PSNREFCL
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub CargaArchivo_1()

        Me.OpenFileDialog.Multiselect = False

        Me.OpenFileDialog.InitialDirectory = (System.Environment.GetFolderPath _
        (System.Environment.SpecialFolder.Personal))

        Me.OpenFileDialog.Filter = "Text Files (*.txt )|*.txt;|All files (*.*)|*.*"
        If Me.OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then ' SI DIO CLICK EN ACEPTAR
            Me.TxtRutaCabecera.Text = Me.OpenFileDialog.FileName


            Dim objReader As New IO.StreamReader(Me.OpenFileDialog.FileName, System.Text.Encoding.GetEncoding(1252))
            Dim objReader2 As New IO.StreamReader(Me.OpenFileDialog.FileName, System.Text.Encoding.GetEncoding(1252))

            Dim sLine As String = ""

            Dim obeOrdenDeCompra As New beOrdenCompra
            Dim obeOrdenDeCompraDet As New beOrdenCompra
            Dim obeOrdenDeCompraNotas As New beOrdenCompra

            Dim olbeProveedor As New List(Of beProveedor)


            olbeDetOc.Clear()
            olbeOrdenDeCompraNotas.Clear()

            Dim obeProveedor As New beProveedor
            Dim sDato As String = String.Empty
            Dim PSNORCML As String = String.Empty
            Dim PSTCITPR As String = String.Empty

            Dim psPrecio As String = ""
            Dim psCantidad As String = ""
            Dim psToleranciaMinina As String = ""
            Dim psToleranciamaxima As String = ""

            Dim blResultado As Boolean = False
            Dim blAA As Boolean = False
            Dim blH1 As Boolean = False
            Dim blL1 As Boolean = False
            Dim blZZ As Boolean = False
            Dim strMensaje As String = String.Empty

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Do
                sLine = objReader.ReadLine()
                If Not sLine Is Nothing AndAlso Not sLine.ToString.Equals("") Then
                    sDato = CortaTexto(0, 2, sLine)
                    Select Case sDato
                        Case "AA"
                            blAA = True
                            If fboolValidarAA(sLine) Then
                                Exit Sub
                            End If
                        Case "H1"
                            blH1 = True
                            If fboolValidarH1(sLine) Then
                                Exit Sub
                            End If
                        Case "L1"
                            blL1 = True
                            If fboolValidarL1(sLine) Then
                                Exit Sub
                            End If
                        Case "ZZ"
                            blZZ = True
                    End Select
                End If
            Loop Until sLine Is Nothing OrElse sLine.ToString.Equals("")

            If blAA And blH1 And blL1 And blZZ Then
                blResultado = True
            End If
            If blResultado = False Then
                dgCabecera.DataSource = Nothing
                dgDetalle.DataSource = Nothing
                MessageBox.Show("Estructura Incorrecta", "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                objReader.Close()
                objReader2.Close()
                Exit Sub
            End If


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If blResultado Then
                olbeOrdenDeCompra.Clear()

                Do
                    sLine = objReader2.ReadLine()

                    If Not sLine Is Nothing AndAlso Not sLine.ToString.Equals("") Then

                        sLine = validaCaracter(sLine, strMensaje)
                        If strMensaje <> String.Empty Then
                            MessageBox.Show(strMensaje, "Aviso de Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            objReader.Close()
                            objReader2.Close()
                            Exit Sub
                        End If

                        obeProveedor = New beProveedor

                        sDato = sLine.Substring(0, 2)
                        Select Case sDato
                            Case "AA"
                                _dblCodCliente = CortaTexto(14, 6, sLine)  'codigo de cliente
                            Case "H1"     'CABECERA
                                If (_dblCodCliente = "3273") Then
                                    AUXCODCLIENTE = ""
                                    If InStr(1, CortaTexto(287, 11, sLine), "20100130468") > 0 Then
                                        AUXCODCLIENTE = 3273
                                    End If
                                    If InStr(1, CortaTexto(287, 11, sLine), "20192779333") > 0 Then
                                        AUXCODCLIENTE = 11496
                                    End If
                                    If InStr(1, CortaTexto(287, 11, sLine), "20489174023") > 0 Then
                                        AUXCODCLIENTE = 21625
                                    End If
                                    If InStr(1, CortaTexto(287, 11, sLine), "20419387658") > 0 Then
                                        AUXCODCLIENTE = 16168
                                    End If
                                    If InStr(1, CortaTexto(287, 11, sLine), "20381444997") > 0 Then
                                        AUXCODCLIENTE = 11121
                                    End If
                                    If InStr(1, CortaTexto(287, 11, sLine), "20253106167") > 0 Then
                                        AUXCODCLIENTE = 102
                                    End If
                                    If InStr(1, CortaTexto(287, 11, sLine), "20131644524") > 0 Then
                                        AUXCODCLIENTE = 20337
                                    End If
                                    If InStr(1, CortaTexto(287, 11, sLine), "20501322343") > 0 Then
                                        AUXCODCLIENTE = 20337
                                    End If
                                    If InStr(1, CortaTexto(287, 11, sLine), "20252810634") > 0 Then
                                        AUXCODCLIENTE = 44887
                                    End If
                                    If InStr(1, CortaTexto(287, 11, sLine), "20513798611") > 0 Then
                                        AUXCODCLIENTE = 40582
                                    End If
                                End If

                                obeOrdenDeCompra = New beOrdenCompra
                                obeProveedor = New beProveedor
                                With obeOrdenDeCompra
                                    If (_dblCodCliente = "3273") Then
                                        .PNCCLNT = AUXCODCLIENTE
                                    Else
                                        .PNCCLNT = _dblCodCliente
                                    End If
                                    PSNORCML = CortaTexto(2, 15, sLine)    ' NNROPO = Numero de Orden de Compra
                                    .PSNORCML = PSNORCML.Trim
                                    .PSTPAISEM = CortaTexto(184, 40, sLine).Trim 'NPAEMB = pais de embarque    
                                    .PNFORCML = CortaTexto(117, 8, sLine)    'DCREPO = Fecha Creación Orden de Compra
                                    .PSFORCML_INI = HelpClass.CNumero8Digitos_a_Fecha(CortaTexto(117, 8, sLine))
                                    .PSNMONOC = CortaTexto(153, 5, sLine).Trim 'NMONEDA = Moneda
                                    .PSTPAGME = CortaTexto(133, 20, sLine).Trim 'NTERPAG = Termino de Pago
                                    .PSTNOMCOM = CortaTexto(334, 50, sLine).Trim 'TNOMCOM= Nombre de Comprador
                                    PSTCITPR = CortaTexto(314, 10, sLine) 'CCODPRO = Código de Proveedor
                                    .PSTCITPR = PSTCITPR.Trim
                                    .PNCPRVCL = 0 ' CODIGO DE CLIENTE TERCERO   - generado
                                    .PSCPRPCL = PSTCITPR.Trim ' Codigo del proveedor - Cliente
                                    .PSTDSCML = CortaTexto(17, 50, sLine).Trim 'TDESPO = Desc. General Orden de Compra
                                    .PSTEMPFAC = CortaTexto(264, 50, sLine).Trim 'NEMPFAC = Empresa a facturar

                                    .PSTCTCST = CortaTexto(125, 5, sLine).Trim 'CCENCOS = Centro de Costo
                                    .PSTTINTC = CortaTexto(130, 3, sLine).Trim 'NTERCOM = Termino de la Compra
                                    .PNCMEDTR = BuscaMediotransporte(CortaTexto(158, 1, sLine)) 'FMEDEM = Medio de Embarque  
                                    .PSCREGEMB = CortaTexto(332, 2, sLine) 'CREGEMB = Región de Embarque
                                    .PSTLUGEM = CortaTexto(224, 20, sLine) 'NPTOEMB = Puerto de Embarque
                                    .PSTDEFIN = CortaTexto(244, 20, sLine) 'NPTODES = Puerto de Desembarque Cliente
                                    .PNNTPDES = Val(CortaTexto(159, 1, sLine)) 'FURG = Nivel de Urgencia     
                                    .PSSESTRG = "A"
                                    .PSNSVP = "A"
                                    .PSSTRANS = ""
                                    .PSSFLGES = "A"

                                    .PSNTRMNL = _strHostName.ToUpper
                                    .PSUSUARIO = _strUser
                                    .PSCUSCRT = _strUser
                                    .PSNTRMCR = _strHostName.ToUpper
                                    olbeOrdenDeCompra.Add(obeOrdenDeCompra)



                                    'datos del proveedor

                                    obeProveedor.PSCPRPCL = PSTCITPR  ' Codigo de proveedor
                                    'obeProveedor.PNCPRVCL = 0     ' Codigo Generado
                                    obeProveedor.PNCCLNT = _dblCodCliente      ' CLIENTE       PSTPRCL1
                                    obeProveedor.PSTPRVCL = CortaTexto(364, 30, sLine) 'NNOMPRO= Nombre Proveedor
                                    obeProveedor.PNNRUCPR = 0
                                    obeProveedor.PSTNACPR = PSTCITPR
                                    obeProveedor.PSTDRPRC = CortaTexto(544, 35, sLine) ' TDRPRC = Direccion        -tamaño 80 segun archivo
                                    obeProveedor.PSTDRDST = CortaTexto(544, 70, sLine) ' TDRPRC = Direccion        -tamaño 80 segun archivo
                                    obeProveedor.PNCPAIS = 0
                                    obeProveedor.PSTNROFX = ""
                                    obeProveedor.PSTLFNO1 = CortaTexto(409, 40, sLine)   'NFAXPRO = Fax Proveedor
                                    obeProveedor.PSTEMAI2 = CortaTexto(394, 15, sLine) 'NTELPRO = Teléfono Proveedor

                                    obeProveedor.PSTPRSCN = CortaTexto(449, 40, sLine) 'NCTOPRO = Contacto Proveedor
                                    obeProveedor.PSTLFNO2 = CortaTexto(489, 15, sLine) 'NTCTOPR = Teléfono contacto Proveedor
                                    obeProveedor.PSTEMAI3 = CortaTexto(504, 40, sLine) 'TMCTOPR = Mail contacto Proveedor

                                    obeProveedor.PNNDSDMP = 0
                                    obeProveedor.PSNTRMNL = _strHostName.ToUpper
                                    obeProveedor.PSUSUARIO = _strUser
                                    .PNCPRVCL = ObtenerCodigoProveedor(obeProveedor)


                                    .PSTPRSCN = CortaTexto(449, 40, sLine) 'NCTOPRO = Contacto Proveedor
                                    .PSTLFNO2 = CortaTexto(489, 15, sLine) 'NTCTOPR = Teléfono contacto Proveedor
                                    .PSTEMAI3 = CortaTexto(504, 40, sLine) 'TMCTOPR = Mail contacto Proveedor

                                End With

                            Case "L1"  'DELTALLE
                                obeOrdenDeCompraDet = New beOrdenCompra
                                With obeOrdenDeCompraDet
                                    If (_dblCodCliente = "3273") Then
                                        .PNCCLNT = AUXCODCLIENTE
                                    Else
                                        .PNCCLNT = _dblCodCliente
                                    End If
                                    .PSNORCML = PSNORCML.Trim     'NNROPO = Numero de Orden de Compra
                                    .PSTDITIN = CortaTexto(173, 100, sLine) ' TDECOIN = Descripción corta en Ingles
                                    .PSCPTDAR = CortaTexto(304, 15, sLine) ' MPDAARA = Numero de Partida Arancelaria

                                    psCantidad = CortaTexto(52, 10, sLine) & "." & CortaTexto(62, 5, sLine)

                                    If psCantidad.Length > 1 Then
                                        .PNQCNTIT = Val(psCantidad) ' QCANORD= Cantidad Ordenada
                                    End If

                                    .PNQSDOIT = 0
                                    .PNQINEMP = 0
                                    .PSTUNDIT = CortaTexto(67, 4, sLine)   'CUNDMED= Unidad de Medida
                                    psPrecio = CortaTexto(271, 10, sLine) & "." & CortaTexto(281, 5, sLine) 'SPREUND = Precio Unitario

                                    If psPrecio.Length > 1 Then
                                        .PNIVTOIT = .PNQCNTIT * Convert.ToDecimal(psPrecio)
                                        .PNIVTOIT = Format(.PNIVTOIT, "0000000000.00000")
                                        .PNIVUNIT = Val(psPrecio)
                                    End If

                                    .PNFMPDME = CortaTexto(286, 8, sLine)  'DPRODES = Fec Máx. Prov. despachará merc.
                                    .PNFMPIME = Val(CortaTexto(294, 8, sLine))  'DMATCLI = Fec. Máx. material Req. en Cliente
                                    .PSTLUGEN = CortaTexto(517, 50, sLine) 'TLUGEN = Lugar entrega al cliente
                                    .PSTLUGOR = CortaTexto(567, 50, sLine) 'TLUREC = Lugar recepción (alm. origen) - CONFIRMAR
                                    .PSSFLGES = "A"
                                    .PSNTRMNL = _strHostName.ToUpper
                                    .PSUSUARIO = _strUser
                                    .PSCUSCRT = _strUser
                                    .PSNTRMCR = _strHostName.ToUpper
                                    .PSSESTRG = "A"
                                    .PSCULUSA = _strUser
                                    .PNQSTKIT = 0

                                    psToleranciaMinina = CortaTexto(617, 10, sLine) & "." & CortaTexto(627, 5, sLine)    'QTOLMIN = Cantidad tolerancia mínima
                                    .PNQTOLMIN = Val(IIf(psToleranciaMinina.Length > 1, psToleranciaMinina, 0)) 'QTOLMIN = Cantidad tolerancia mínima
                                    psToleranciamaxima = CortaTexto(632, 10, sLine) & "." & CortaTexto(642, 5, sLine)
                                    .PNQTOLMAX = Val(IIf(psToleranciamaxima.Length > 1, psToleranciamaxima, 0))
                                    .PSTRFRN = CortaTexto(647, 50, sLine) 'TREFRN1 = Referencia 1 Cliente (Solicitante)
                                    .PSTRFRNA = CortaTexto(697, 50, sLine) 'TREFRN2 = Referencia 2 Cliente (Lote)
                                    .PSTRFRNB = CortaTexto(747, 50, sLine) 'TREFRN3 = Referencia 3 Cliente (otro.)
                                    .PSTRFRN1 = CortaTexto(797, 10, sLine) 'TREFRN4 = Referencia 4 Cliente (Centro Dest.)
                                    .PSTRFRN2 = CortaTexto(807, 10, sLine) 'TREFRN5 = Referencia 5 Cliente (Almacen Dest)
                                    .PNNRITOC = CortaTexto(17, 5, sLine) ' Nro de Item  MNROITE
                                    .PSTCITCL = CortaTexto(22, 15, sLine) ' CMERCLI = Código de Mercadería (Cliente)
                                    .PSTCITPR = PSTCITPR   'codigo de proveedor    CCODPRO
                                    .PSTDITES = CortaTexto(71, 100, sLine).Trim ' TDECOCA = Descripción corta en castellano
                                    .PNFMPDME = CortaTexto(286, 8, sLine)     'DPRODES = Fec Máx. Prov. despachará merc.

                                    olbeDetOc.Add(obeOrdenDeCompraDet)
                                End With
                            Case "H2"  'Notas
                                obeOrdenDeCompraNotas = New beOrdenCompra

                                With obeOrdenDeCompraNotas
                                    If (_dblCodCliente = "3273") Then
                                        .PNCCLNT = AUXCODCLIENTE
                                    Else
                                        .PNCCLNT = _dblCodCliente
                                    End If
                                    .PSNORCML = CortaTexto(2, 15, sLine)
                                    .PSTNOTAS = CortaTexto(17, 587, sLine)
                                    .PSNTRMNL = _strHostName.ToUpper
                                    .PSUSUARIO = _strUser
                                    .PSCUSCRT = _strUser
                                    .PSNTRMCR = _strHostName.ToUpper
                                End With
                                If obeOrdenDeCompraNotas.PSTNOTAS.Length > 0 Then olbeOrdenDeCompraNotas.Add(obeOrdenDeCompraNotas)

                        End Select

                    End If
                Loop Until sLine Is Nothing OrElse sLine.ToString.Equals("")
                objReader.Close()
                objReader2.Close()
                Me.dgCabecera.DataSource = Nothing
                Me.dgCabecera.AutoGenerateColumns = False
                Me.dgCabecera.DataSource = olbeOrdenDeCompra
                tsbGuardar.Enabled = True
            End If

        End If
    End Sub

    Private Function fboolValidarAA(ByVal sLine As String) As Boolean
        Dim strError As String = String.Empty
        Dim strFechaTransmision As String = String.Empty
        Dim strHoraTransmision As String = String.Empty
        Dim strCodCliente As String = String.Empty

        strFechaTransmision = CortaTexto(2, 8, sLine)
        If Not IsNumeric(strFechaTransmision) Then
            strError = strError & "Fecha de Transmisión (DFTRAPO)==> '" & strFechaTransmision & "' Formato de fecha inválido, debe ser 'YYYYMMDD" & Chr(10)
        End If
        strHoraTransmision = CortaTexto(10, 4, sLine)
        If Not IsNumeric(strHoraTransmision) Then
            strError = strError & "Hora de Transmisión (DHTRAPO)==> No numérico." & Chr(10)
        End If
        strCodCliente = CortaTexto(14, 6, sLine)
        If Not IsNumeric(strCodCliente) Then
            strError = strError & "Código de cliente (CCODCLI)==> No numérico." & Chr(10)
        End If
        If strError.Trim.Length > 0 Then
            MessageBox.Show(strError, "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return True
        End If
        Return False
    End Function

    Private Function fboolValidarH1(ByVal sLine As String) As Boolean
        Dim strError As String = String.Empty
        'Orden de compra
        If CortaTexto(2, 15, sLine).Trim.Equals("") Then
            strError = strError & "* Orden de Compra (NNROPO)==> Sin dato." & Chr(10)
        End If
        'Fecha de reacion OC
        If Not IsNumeric(CortaTexto(117, 8, sLine)) Then
            strError = strError & "* Fecha de creación de Orden de Compra (DCREPO)==> " & CortaTexto(117, 8, sLine) & " Formato de fecha inválido, debe ser 'YYYYMMDD'." & Chr(10)
        End If
        'Incoterm
        'If Not CortaTexto(130, 3, sLine).Trim.Equals("") Then
        '    Select Case CortaTexto(130, 3, sLine).Trim
        '        Case "FOB"
        '        Case "CIF"
        '        Case "EXW"
        '        Case "FAS"
        '        Case "FCA"
        '        Case "CFR"
        '        Case "CPT"
        '        Case "CIP"
        '        Case "DAF"
        '        Case "DES"
        '        Case "DEQ"
        '        Case "DDU"
        '        Case "DDP"
        '        Case "C&F"
        '        Case "LOC"
        '        Case Else
        '            strError = strError & "* Término de Compra -INCOTERM- (NTERCOM)==>  " & CortaTexto(130, 3, sLine).Trim & "  irreconocido solo válidos" & _
        '                                                       "(FOB,CIF,EXW.FAS,FCA,CFR,CPT,CIP,DAF,DES,DEQ,DDU,DDP,C&F,LOC)." & Chr(10)
        '    End Select
        'End If

        'Medio de embarque
        'If CortaTexto(158, 1, sLine).ToString.Trim.Equals("") Then
        '    strError = strError & "* Medio de Embarque (FMEDEM)==> Sin dato." & Chr(10)
        'End If

        'Nivel de urgencia
        If CortaTexto(159, 1, sLine).Trim.Equals("") Then
            strError = strError & "* Nivel de Urgencia (FURG)==> Sin dato." & Chr(10)
        End If

        'Pais de embarque
        If CortaTexto(184, 40, sLine).Trim.Equals("") Then
            strError = strError & "* País de Embarque (NPAEMB)==> Sin dato." & Chr(10)
        End If

        'Puerto de Desembarque Cliente
        If CortaTexto(244, 20, sLine).Trim.Equals("") Then
            strError = strError & "* Puerto de Desembarque Cliente (NPTODES)==> Sin dato." & Chr(10)
        End If

        'Empresa a facturar
        If CortaTexto(264, 50, sLine).Trim.Equals("") Then
            strError = strError & "* Empresa a facturar (NEMPFAC)==> Sin dato." & Chr(10)
        End If

        'Codigo del proveedor
        If CortaTexto(314, 10, sLine).Trim.Equals("") Then
            strError = strError & "* Código de Proveedor (CCODPRO)==> Sin dato." & Chr(10)
        End If

        'Región de Embarque
        If CortaTexto(332, 2, sLine).Trim.Equals("") Then
            strError = "* Región de Embarque (CREGEMB)==> Sin dato." & Chr(10)
        End If

        'Nombre de Comprador
        If CortaTexto(334, 50, sLine).Trim.Equals("") Then
            strError = strError & "* Nombre de Comprador (TNOMCOM)==> Sin dato." & Chr(10)
        End If

        'Nombre Proveedor
        If CortaTexto(364, 30, sLine).Trim.Equals("") Then
            strError = strError & "* Nombre Proveedor (NNOMPRO)==> Sin dato." & Chr(10)
        End If

        If strError.Trim.Length > 0 Then
            MessageBox.Show("Existe error con la OC: " & CortaTexto(2, 15, sLine) & Chr(10) & strError, "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return True
        End If
        Return False
    End Function

    Private Function fboolValidarL1(ByVal sLine As String) As Boolean
        Dim strError As String = String.Empty
        'Nro. Item
        If Not IsNumeric(CortaTexto(17, 5, sLine)) Then
            strError = strError & "* Número de Item (MNROITE)==> No Numérico." & Chr(10)
        End If

        'Cantidad Ordenada
        If Not IsNumeric(CortaTexto(52, 10, sLine) & "." & CortaTexto(62, 5, sLine)) Then
            strError = strError & "* Cantidad Ordenada (QCANORD)==> No Numérico." & Chr(10)
        End If

        'Unidad de Medida
        If CortaTexto(67, 4, sLine).Trim.Equals("") Then
            strError = strError & "* Unidad de Medida (CUNDMED)==> Sin dato." & Chr(10)
        End If

        'Precio unitario
        If Not IsNumeric(CortaTexto(271, 10, sLine) & "." & CortaTexto(281, 5, sLine)) Then
            strError = strError & "* Precio unitario (SPREUND)==> No numerico." & Chr(10)
        End If

        'Fec Máx. Prov. 
        If Not IsNumeric(CortaTexto(286, 8, sLine)) Then
            strError = strError & "* Fec Máx. Prov. despachará merc. (DPRODES)==> ' " & CortaTexto(286, 8, sLine) & " '  formato de fecha inválido, debe ser 'YYYYMMDD'." & Chr(10)
        End If

        'Fec Max. Merc. almacén cliente
        If Not IsNumeric(CortaTexto(294, 8, sLine)) Then
            strError = strError & "* Fec Max. Merc. almacén cliente (DMATCLI)==>  ' " & CortaTexto(294, 8, sLine) & " '  formato de fecha inválido, debe ser 'YYYYMMDD'." & Chr(10)
        End If

        'Cantidad Tolerancia Minima 
        'If Not IsNumeric(CortaTexto(617, 10, sLine) & "." & CortaTexto(627, 5, sLine)) Then
        '    strError = strError & "* Cantidad Tolerancia Minima (QTOLMIN)==> No Numérico." & Chr(10)
        'End If

        ''Tolerancia Máxima
        'If Not IsNumeric(CortaTexto(632, 10, sLine) & "." & CortaTexto(642, 5, sLine)) Then
        '    strError = strError & "* Cantidad Tolerancia Máxima (QTOLMAX)==> No Numérico." & Chr(10)
        'End If

        If strError.Trim.Length > 0 Then
            MessageBox.Show("Existe error con la OC: " & CortaTexto(2, 15, sLine) & " y Nro. Item " & CortaTexto(17, 5, sLine) & Chr(10) & strError, "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return True
        End If
        Return False
    End Function



    ''' <summary>
    ''' Hecho especialmente para el formato antiguo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargaArchivo_2()

        Me.OpenFileDialog.Multiselect = False

        Me.OpenFileDialog.InitialDirectory = (System.Environment.GetFolderPath _
        (System.Environment.SpecialFolder.Personal))

        Me.OpenFileDialog.Filter = "Text Files (*.txt )|*.txt;|All files (*.*)|*.*"
        If Me.OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then ' SI DIO CLICK EN ACEPTAR
            Me.TxtRutaCabecera.Text = Me.OpenFileDialog.FileName


            Dim objReader As New IO.StreamReader(Me.OpenFileDialog.FileName, System.Text.Encoding.GetEncoding(1252))
            Dim objReader2 As New IO.StreamReader(Me.OpenFileDialog.FileName, System.Text.Encoding.GetEncoding(1252))

            Dim sLine As String = ""

            Dim obeOrdenDeCompra As New beOrdenCompra
            Dim obeOrdenDeCompraDet As New beOrdenCompra
            Dim obeOrdenDeCompraNotas As New beOrdenCompra

            Dim olbeProveedor As New List(Of beProveedor)


            olbeDetOc.Clear()
            olbeOrdenDeCompraNotas.Clear()

            Dim obeProveedor As New beProveedor
            Dim sDato As String = String.Empty
            Dim PSNORCML As String = String.Empty
            Dim PSTCITPR As String = String.Empty

            Dim psPrecio As String = ""
            Dim psCantidad As String = ""
            Dim psToleranciaMinina As String = ""
            Dim psToleranciamaxima As String = ""

            Dim blResultado As Boolean = False
            Dim blAA As Boolean = False
            Dim blH1 As Boolean = False
            Dim blL1 As Boolean = False
            Dim blZZ As Boolean = False
            Dim strMensaje As String = String.Empty
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Do
                sLine = objReader.ReadLine()
                If Not sLine Is Nothing AndAlso Not sLine.ToString.Equals("") Then
                    sDato = CortaTexto(0, 2, sLine)

                    Select Case sDato
                        Case "AA"
                            blAA = True
                            If fboolValidar2AA(sLine) Then
                                Exit Sub
                            End If
                        Case "H1"
                            blH1 = True
                            If fboolValidar2H1(sLine) Then
                                Exit Sub
                            End If
                        Case "L1"
                            blL1 = True
                            If fboolValidar2L1(sLine) Then
                                Exit Sub
                            End If
                        Case "ZZ"
                            blZZ = True
                    End Select

                End If

            Loop Until sLine Is Nothing OrElse sLine.ToString.Equals("")

            If blAA And blH1 And blL1 And blZZ Then
                blResultado = True
            End If


            If blResultado = False Then

                dgCabecera.DataSource = Nothing
                dgDetalle.DataSource = Nothing
                MessageBox.Show("Estructura Incorrecta", "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                objReader.Close()
                objReader2.Close()

                Exit Sub
            End If


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If blResultado Then
                olbeOrdenDeCompra.Clear()

                Do
                    sLine = objReader2.ReadLine()

                    If Not sLine Is Nothing AndAlso Not sLine.ToString.Equals("") Then

                        sLine = validaCaracter(sLine, strMensaje)
                        If strMensaje <> String.Empty Then
                            MessageBox.Show(strMensaje, "Aviso de Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            objReader.Close()
                            objReader2.Close()
                            Exit Sub
                        End If

                        obeProveedor = New beProveedor

                        sDato = sLine.Substring(0, 2)
                        Select Case sDato
                            Case "AA"
                                _dblCodCliente = CortaTexto(14, 6, sLine)  'codigo de cliente

                            Case "H1"     'CABECERA
                                obeOrdenDeCompra = New beOrdenCompra
                                obeProveedor = New beProveedor

                                With obeOrdenDeCompra
                                    .PNCCLNT = _dblCodCliente
                                    PSNORCML = CortaTexto(2, 15, sLine)    ' NNROPO = Numero de Orden de Compra
                                    .PSNORCML = PSNORCML.Trim
                                    .PSTDSCML = CortaTexto(18, 100, sLine).Trim 'TDESPO = Desc. General Orden de Compra
                                    .PNFORCML = CortaTexto(118, 8, sLine)    'DCREPO = Fecha Creación Orden de Compra
                                    .PSTCTCST = CortaTexto(126, 5, sLine).Trim 'CCENCOS = Centro de Costo
                                    .PSTTINTC = CortaTexto(131, 3, sLine).Trim 'NTERCOM = Termino de la Compra
                                    .PSTPAGME = CortaTexto(134, 20, sLine).Trim 'NTERPAG = Termino de Pago
                                    .PSNMONOC = CortaTexto(154, 5, sLine).Trim 'NMONEDA = Moneda
                                    .PNCMEDTR = BuscaMediotransporte(CortaTexto(159, 1, sLine)) 'FMEDEM = Medio de Embarque 
                                    .PNNTPDES = CortaTexto(160, 1, sLine) 'FURG = Nivel de Urgencia 

                                    .PSTPAISEM = CortaTexto(185, 40, sLine).Trim 'NPAEMB = pais de embarque    
                                    .PSTLUGEM = CortaTexto(225, 20, sLine) 'NPTOEMB = Puerto de Embarque
                                    .PSTDEFIN = CortaTexto(245, 20, sLine) 'NPTODES = Puerto de Desembarque Cliente
                                    .PSTEMPFAC = CortaTexto(265, 50, sLine).Trim 'NEMPFAC = Empresa a facturar
                                    .PSCREGEMB = CortaTexto(333, 2, sLine) 'CREGEMB = Región de Embarque


                                    PSTCITPR = CortaTexto(315, 10, sLine) 'CCODPRO = Código de Proveedor
                                    .PSTCITPR = PSTCITPR.Trim
                                    .PNCPRVCL = 0 ' CODIGO DE CLIENTE TERCERO   - generado
                                    .PSCPRPCL = PSTCITPR.Trim ' Codigo del proveedor - Cliente
                                    '.PSFORCML_INI = HelpClass.CNumero8Digitos_a_Fecha(CortaTexto(117, 8, sLine))
                                    .PSTNOMCOM = CortaTexto(335, 50, sLine).Trim 'TNOMCOM= Nombre de Comprador

                                    .PSSESTRG = "A"
                                    .PSNSVP = "A"
                                    .PSSTRANS = ""
                                    .PSSFLGES = "A"

                                    .PSNTRMNL = _strHostName.ToUpper
                                    .PSUSUARIO = _strUser
                                    .PSCUSCRT = _strUser
                                    .PSNTRMCR = _strHostName.ToUpper
                                    olbeOrdenDeCompra.Add(obeOrdenDeCompra)



                                    'datos del proveedor

                                    obeProveedor.PSCPRPCL = PSTCITPR  ' Codigo de proveedor
                                    'obeProveedor.PNCPRVCL = 0     ' Codigo Generado
                                    obeProveedor.PNCCLNT = _dblCodCliente      ' CLIENTE       PSTPRCL1
                                    obeProveedor.PSTPRVCL = CortaTexto(365, 30, sLine) 'NNOMPRO= Nombre Proveedor
                                    obeProveedor.PNNRUCPR = 0
                                    obeProveedor.PSTNACPR = PSTCITPR
                                    obeProveedor.PSTDRPRC = CortaTexto(545, 35, sLine) ' TDRPRC = Direccion        -tamaño 80 segun archivo
                                    obeProveedor.PSTDRDST = CortaTexto(545, 70, sLine) ' TDRPRC = Direccion        -tamaño 80 segun archivo
                                    obeProveedor.PNCPAIS = 0
                                    obeProveedor.PSTNROFX = CortaTexto(410, 40, sLine)   'NFAXPRO = Fax Proveedor
                                    obeProveedor.PSTLFNO1 = CortaTexto(395, 15, sLine) 'NTELPRO = Teléfono Proveedor
                                    obeProveedor.PSTEMAI2 = ""
                                    obeProveedor.PSTPRSCN = CortaTexto(450, 40, sLine) 'NCTOPRO = Contacto Proveedor
                                    obeProveedor.PSTLFNO2 = CortaTexto(490, 15, sLine) 'NTCTOPR = Teléfono contacto Proveedor
                                    obeProveedor.PSTEMAI3 = CortaTexto(505, 40, sLine) 'TMCTOPR = Mail contacto Proveedor
                                    obeProveedor.PNNDSDMP = 0
                                    obeProveedor.PSNTRMNL = _strHostName.ToUpper
                                    obeProveedor.PSUSUARIO = _strUser

                                    .PNCPRVCL = ObtenerCodigoProveedor(obeProveedor)

                                    .PSTPRSCN = CortaTexto(450, 40, sLine) 'NCTOPRO = Contacto Proveedor
                                    .PSTLFNO2 = CortaTexto(490, 15, sLine) 'NTCTOPR = Teléfono contacto Proveedor
                                    .PSTEMAI3 = CortaTexto(505, 40, sLine) 'TMCTOPR = Mail contacto Proveedor


                                End With
                            Case "L1"  'DELTALLE


                                obeOrdenDeCompraDet = New beOrdenCompra
                                With obeOrdenDeCompraDet
                                    .PNCCLNT = _dblCodCliente
                                    .PSNORCML = PSNORCML.Trim     'NNROPO = Numero de Orden de Compra
                                    .PNNRITOC = CortaTexto(18, 5, sLine) ' Nro de Item  MNROITE
                                    .PSTCITCL = CortaTexto(23, 15, sLine) ' CMERCLI = Código de Mercadería (Cliente)

                                    .PSTCITPR = PSTCITPR   'codigo de proveedor    CCODPRO
                                    .PSTDITIN = CortaTexto(174, 100, sLine) ' TDECOIN = Descripción corta en Ingles
                                    .PSTDITES = CortaTexto(74, 100, sLine).Trim ' TDECOCA = Descripción corta en castellano

                                    psCantidad = CortaTexto(55, 10, sLine) & "." & CortaTexto(66, 4, sLine)
                                    If psCantidad.Length > 1 Then
                                        .PNQCNTIT = Val(psCantidad) ' QCANORD= Cantidad Ordenada
                                    End If
                                    .PNQSDOIT = 0
                                    .PNQINEMP = 0
                                    .PSTUNDIT = CortaTexto(70, 4, sLine)   'CUNDMED= Unidad de Medida
                                    psPrecio = CortaTexto(274, 12, sLine) & "." & CortaTexto(287, 5, sLine) 'SPREUND = Precio Unitario

                                    .PNFMPDME = CortaTexto(291, 8, sLine)     'DPRODES = Fec Máx. Prov. despachará merc.
                                    .PNFMPIME = Val(CortaTexto(299, 8, sLine))  'DMATCLI = Fec. Máx. material Req. en Cliente

                                    .PSCPTDAR = CortaTexto(307, 15, sLine) ' MPDAARA = Numero de Partida Arancelaria


                                    If psPrecio.Length > 1 Then
                                        .PNIVTOIT = .PNQCNTIT * Convert.ToDecimal(psPrecio)
                                        .PNIVTOIT = Format(.PNIVTOIT, "0000000000.00000")
                                        .PNIVUNIT = Val(psPrecio)
                                    End If



                                    .PSTLUGEN = CortaTexto(518, 50, sLine) 'TLUGEN = Lugar entrega al cliente
                                    .PSTLUGOR = CortaTexto(568, 50, sLine) 'TLUREC = Lugar recepción (alm. origen) - CONFIRMAR
                                    .PSSFLGES = "A"
                                    .PSNTRMNL = _strHostName.ToUpper
                                    .PSUSUARIO = _strUser
                                    .PSCUSCRT = _strUser
                                    .PSNTRMCR = _strHostName.ToUpper
                                    .PSSESTRG = "A"
                                    .PSCULUSA = _strUser
                                    .PNQSTKIT = 0
                                    'psToleranciaMinina = CortaTexto(617, 10, sLine) & "." & CortaTexto(627, 5, sLine)    'QTOLMIN = Cantidad tolerancia mínima
                                    '.PNQTOLMIN = Val(IIf(psToleranciaMinina.Length > 1, psToleranciaMinina, 0)) 'QTOLMIN = Cantidad tolerancia mínima
                                    'psToleranciamaxima = CortaTexto(632, 10, sLine) & "." & CortaTexto(642, 5, sLine)
                                    '.PNQTOLMAX = Val(IIf(psToleranciamaxima.Length > 1, psToleranciamaxima, 0))
                                    '.PSTRFRN = CortaTexto(647, 50, sLine) 'TREFRN1 = Referencia 1 Cliente (Solicitante)
                                    '.PSTRFRNA = CortaTexto(697, 50, sLine) 'TREFRN2 = Referencia 2 Cliente (Lote)
                                    '.PSTRFRNB = CortaTexto(747, 50, sLine) 'TREFRN3 = Referencia 3 Cliente (otro.)
                                    '.PSTRFRN1 = CortaTexto(797, 10, sLine) 'TREFRN4 = Referencia 4 Cliente (Centro Dest.)
                                    '.PSTRFRN2 = CortaTexto(807, 10, sLine) 'TREFRN5 = Referencia 5 Cliente (Almacen Dest)
                                    olbeDetOc.Add(obeOrdenDeCompraDet)
                                End With
                                'Case "H2"  'Notas
                                '    obeOrdenDeCompraNotas = New beOrdenCompra

                                '    With obeOrdenDeCompraNotas
                                '        .PNCCLNT = _dblCodCliente
                                '        .PSNORCML = CortaTexto(2, 15, sLine)
                                '        .PSTNOTAS = CortaTexto(17, 587, sLine)
                                '        .PSNTRMNL = _strHostName.ToUpper
                                '        .PSUSUARIO = _strUser
                                '        .PSCUSCRT = _strUser
                                '        .PSNTRMCR = _strHostName.ToUpper
                                '    End With
                                '    If obeOrdenDeCompraNotas.PSTNOTAS.Length > 0 Then olbeOrdenDeCompraNotas.Add(obeOrdenDeCompraNotas)

                        End Select

                    End If
                Loop Until sLine Is Nothing OrElse sLine.ToString.Equals("")
                objReader.Close()
                objReader2.Close()
                Me.dgCabecera.DataSource = Nothing
                Me.dgCabecera.AutoGenerateColumns = False
                Me.dgCabecera.DataSource = olbeOrdenDeCompra
                tsbGuardar.Enabled = True
            End If

        End If
    End Sub

#Region "validad Vista 2"

    Private Function fboolValidar2AA(ByVal sLine As String) As Boolean
        Dim strError As String = String.Empty
        Dim strFechaTransmision As String = String.Empty
        Dim strHoraTransmision As String = String.Empty
        Dim strCodCliente As String = String.Empty

        strFechaTransmision = CortaTexto(2, 8, sLine)
        If Not IsNumeric(strFechaTransmision) Then
            strError = strError & "Fecha de Transmisión (DFTRAPO)==> '" & strFechaTransmision & "' Formato de fecha inválido, debe ser 'YYYYMMDD" & Chr(10)
        End If
        strHoraTransmision = CortaTexto(10, 4, sLine)
        If Not IsNumeric(strHoraTransmision) Then
            strError = strError & "Hora de Transmisión (DHTRAPO)==> No numérico." & Chr(10)
        End If
        strCodCliente = CortaTexto(14, 6, sLine)
        If Not IsNumeric(strCodCliente) Then
            strError = strError & "Código de cliente (CCODCLI)==> No numérico." & Chr(10)
        End If
        If strError.Trim.Length > 0 Then
            MessageBox.Show(strError, "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return True
        End If
        Return False
    End Function

    Private Function fboolValidar2H1(ByVal sLine As String) As Boolean
        Dim strError As String = String.Empty
        'Orden de compra
        If CortaTexto(2, 15, sLine).Trim.Equals("") Then
            strError = strError & "* Orden de Compra (NNROPO)==> Sin dato." & Chr(10)
        End If
        'Fecha de reacion OC
        If Not IsNumeric(CortaTexto(118, 8, sLine)) Then
            strError = strError & "* Fecha de creación de Orden de Compra (DCREPO)==> " & CortaTexto(117, 8, sLine) & " Formato de fecha inválido, debe ser 'YYYYMMDD'." & Chr(10)
        End If
        'Incoterm
        'If Not CortaTexto(131, 3, sLine).Trim.Equals("") Then
        '    Select Case CortaTexto(130, 3, sLine).Trim
        '        Case "FOB"
        '        Case "CIF"
        '        Case "EXW"
        '        Case "FAS"
        '        Case "FCA"
        '        Case "CFR"
        '        Case "CPT"
        '        Case "CIP"
        '        Case "DAF"
        '        Case "DES"
        '        Case "DEQ"
        '        Case "DDU"
        '        Case "DDP"
        '        Case "C&F"
        '        Case "LOC"
        '        Case Else
        '            strError = strError & "* Término de Compra -INCOTERM- (NTERCOM)==>  " & CortaTexto(131, 3, sLine).Trim & "  irreconocido solo válidos" & _
        '                                                       "(FOB,CIF,EXW.FAS,FCA,CFR,CPT,CIP,DAF,DES,DEQ,DDU,DDP,C&F,LOC)." & Chr(10)
        '    End Select
        'End If

        'Medio de embarque
        If CortaTexto(159, 1, sLine).ToString.Trim.Equals("") Then
            strError = strError & "* Medio de Embarque (FMEDEM)==> Sin dato." & Chr(10)
        End If

        'Nivel de urgencia
        If CortaTexto(160, 1, sLine).Trim.Equals("") Then
            strError = strError & "* Nivel de Urgencia (FURG)==> Sin dato." & Chr(10)
        End If

        'Pais de embarque
        If CortaTexto(185, 40, sLine).Trim.Equals("") Then
            strError = strError & "* País de Embarque (NPAEMB)==> Sin dato." & Chr(10)
        End If

        'Puerto de Desembarque Cliente
        If CortaTexto(245, 20, sLine).Trim.Equals("") Then
            strError = strError & "* Puerto de Desembarque Cliente (NPTODES)==> Sin dato." & Chr(10)
        End If

        'Empresa a facturar
        If CortaTexto(265, 50, sLine).Trim.Equals("") Then
            strError = strError & "* Empresa a facturar (NEMPFAC)==> Sin dato." & Chr(10)
        End If

        'Codigo del proveedor
        If CortaTexto(315, 10, sLine).Trim.Equals("") Then
            strError = strError & "* Código de Proveedor (CCODPRO)==> Sin dato." & Chr(10)
        End If

        'Región de Embarque
        If CortaTexto(333, 2, sLine).Trim.Equals("") Then
            strError = "* Región de Embarque (CREGEMB)==> Sin dato." & Chr(10)
        End If

        'Nombre de Comprador
        If CortaTexto(335, 50, sLine).Trim.Equals("") Then
            strError = strError & "* Nombre de Comprador (TNOMCOM)==> Sin dato." & Chr(10)
        End If

        'Nombre Proveedor
        If CortaTexto(365, 30, sLine).Trim.Equals("") Then
            strError = strError & "* Nombre Proveedor (NNOMPRO)==> Sin dato." & Chr(10)
        End If

        If strError.Trim.Length > 0 Then
            MessageBox.Show("Existe error con la OC: " & CortaTexto(2, 15, sLine) & Chr(10) & strError, "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return True
        End If
        Return False
    End Function

    Private Function fboolValidar2L1(ByVal sLine As String) As Boolean
        Dim strError As String = String.Empty
        'Nro. Item
        If Not IsNumeric(CortaTexto(18, 5, sLine)) Then
            strError = strError & "* Número de Item (MNROITE)==> No Numérico." & Chr(10)
        End If

        'Cantidad Ordenada
        If Not IsNumeric(CortaTexto(55, 10, sLine) & "." & CortaTexto(66, 4, sLine)) Then
            strError = strError & "* Cantidad Ordenada (QCANORD)==> No Numérico." & Chr(10)
        End If

        'Unidad de Medida
        If CortaTexto(70, 4, sLine).Trim.Equals("") Then
            strError = strError & "* Unidad de Medida (CUNDMED)==> Sin dato." & Chr(10)
        End If

        'Precio unitario
        If Not IsNumeric(CortaTexto(274, 12, sLine) & "." & CortaTexto(287, 5, sLine)) Then
            strError = strError & "* Precio unitario (SPREUND)==> No numerico." & Chr(10)
        End If

        'Fec Máx. Prov. 
        If Not IsNumeric(CortaTexto(291, 8, sLine)) Then
            strError = strError & "* Fec Máx. Prov. despachará merc. (DPRODES)==> ' " & CortaTexto(291, 8, sLine) & " '  formato de fecha inválido, debe ser 'YYYYMMDD'." & Chr(10)
        End If

        'Fec Max. Merc. almacén cliente
        If Not IsNumeric(CortaTexto(299, 8, sLine)) Then
            strError = strError & "* Fec Max. Merc. almacén cliente (DMATCLI)==>  ' " & CortaTexto(299, 8, sLine) & " '  formato de fecha inválido, debe ser 'YYYYMMDD'." & Chr(10)
        End If

        'Cantidad Tolerancia Minima 
        'If Not IsNumeric(CortaTexto(617, 10, sLine) & "." & CortaTexto(627, 5, sLine)) Then
        '    strError = strError & "* Cantidad Tolerancia Minima (QTOLMIN)==> No Numérico." & Chr(10)
        'End If

        'Tolerancia Máxima
        'If Not IsNumeric(CortaTexto(632, 10, sLine) & "." & CortaTexto(642, 5, sLine)) Then
        '    strError = strError & "* Cantidad Tolerancia Máxima (QTOLMAX)==> No Numérico." & Chr(10)
        'End If

        If strError.Trim.Length > 0 Then
            MessageBox.Show("Existe error con la OC: " & CortaTexto(2, 15, sLine) & " y Nro. Item " & CortaTexto(17, 5, sLine) & Chr(10) & strError, "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return True
        End If
        Return False
    End Function

#End Region


    Private Function CortaTexto(ByVal PsIni As Integer, ByVal psFin As Integer, ByVal strCadena As String) As String
        Dim strResultado As String = String.Empty

        If strCadena.Length < PsIni Then
            strResultado = strResultado
        Else

            If (PsIni + psFin) > strCadena.Length Then
                strResultado = strCadena.Substring(PsIni, strCadena.Length - PsIni)
            Else
                strResultado = strCadena.Substring(PsIni, psFin)
            End If
        End If

        Return strResultado.Replace("�", "").Trim
    End Function

    Private Function validaCaracter(ByVal strCadena As String, ByRef strMensaje As String) As String

        Dim sLine As String = String.Empty
        Dim strRuta As String = Application.StartupPath & "\\Texto\\" & "txtValidaCaracter.txt"


        If File.Exists(strRuta) Then

            Dim objReaderValida As New IO.StreamReader(strRuta, System.Text.Encoding.GetEncoding(1252))

            Do
                sLine = objReaderValida.ReadLine()

                If Not sLine Is Nothing AndAlso Not sLine.ToString.Equals("") Then
                    strCadena = strCadena.Replace(sLine.Substring(0, 1), sLine.Substring(1, sLine.Length - 1))
                End If

            Loop Until sLine Is Nothing OrElse sLine.ToString.Equals("")

            objReaderValida.Close()
        Else
            strMensaje = "No se encuentra el archivo de validación de caracteres, Notifique a Sistemas"
        End If
        Return strCadena
    End Function

    Private Function BuscaMediotransporte(ByVal strMedioT As String) As Integer
        Dim nRetorno As Integer = 0
        Dim obrGeneral As New brGeneral
        Dim opbeGeneral As New beGeneral

        Select Case strMedioT
            Case "A"
                strMedioT = "AEREO"
            Case "M"
                strMedioT = "MARITIMO"
            Case "P"
                strMedioT = "POSTAL"
            Case "T"
                strMedioT = "TERRESTRE"
            Case "F"
                strMedioT = "FLUVIAL"

        End Select

        Dim Mlist = obrGeneral.ListaMedioTransporte(opbeGeneral)

        For Each obeGeneral As beGeneral In Mlist
            If strMedioT = obeGeneral.PSTNMMDT.Trim Then
                nRetorno = obeGeneral.PNCMEDTR
                Exit For
            End If
        Next


        Return nRetorno
    End Function

    Private Sub GrabarOc()
        Try
            Dim nRetorno As Integer = 0
            Dim obrOrdeDeCopra As New brOrdenDeCompra
            Dim obeObservacionOc As New beOrdenCompra
            Dim olbeOcTem As New List(Of beOrdenCompra)
            For Each obeOc As beOrdenCompra In Me.dgCabecera.DataSource
                obeOc.PSSESTRG = "A"
                obeOc.PSCUSCRT = _strUser
                obeOc.PSCULUSA = _strUser


                If obrOrdeDeCopra.InsertarOrdenDeCompra(obeOc) = 1 Then
                    olbeOcTem.Clear()
                    strOC = obeOc.PSNORCML
                    olbeOcTem = olbeDetOc.FindAll(MatchOrdenDeCompra)
                    For Each obeDetOc As beOrdenCompra In olbeOcTem


                        If obrOrdeDeCopra.InsertarDetalleOrdenDeCompra(obeDetOc) = 0 Then
                            HelpClass.ErrorMsgBox()
                            Exit Sub
                        End If
                    Next
                Else
                    HelpClass.ErrorMsgBox()
                    Exit Sub
                End If





                'Notas


                For Each obeObservacionOc In olbeOrdenDeCompraNotas
                    If obeOc.PSNORCML = obeObservacionOc.PSNORCML Then
                        obeObservacionOc.PSSESTRG = "A"
                        obeObservacionOc.PSCUSCRT = _strUser
                        obeObservacionOc.PSCULUSA = _strUser
                        Dim intCant As Integer = Math.Ceiling(obeObservacionOc.PSTNOTAS.Trim.Length / 100)
                        Dim strObservacionOc As String = ""
                        strObservacionOc = obeObservacionOc.PSTNOTAS.Trim
                        obrOrdeDeCopra.EliminarObservacionOrdenDeCompra(obeObservacionOc)
                        For i As Integer = 0 To intCant - 1
                            obeObservacionOc.PNNCRRLT = i + 1
                            If intCant = 1 Then
                                obeObservacionOc.PSTNOTAS = strObservacionOc.Trim
                            ElseIf (intCant - 1 = i) Then
                                obeObservacionOc.PSTNOTAS = strObservacionOc.Substring(i * 100, strObservacionOc.Length - (i) * 100).Trim
                            Else
                                obeObservacionOc.PSTNOTAS = strObservacionOc.Substring(i * 100, 100).Trim
                            End If
                            If obrOrdeDeCopra.InsertarObservacionOrdenDeCompra(obeObservacionOc) = 0 Then
                                HelpClass.ErrorMsgBox()
                                Exit Sub
                            End If
                        Next
                    End If
                Next


            Next


            HelpClass.MsgBox("Registro Satisfactorio.")
            Me.DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            HelpClass.ErrorMsgBox()
        End Try
    End Sub

    Private Function ValidaOrdenCompra(ByVal _PNCCLNT As Decimal, ByVal _strOc As String) As Integer
        Dim obeOrdenCompra As New beOrdenCompra
        Dim obrOrdeCompra = New brOrdenDeCompra

        obeOrdenCompra.PNCCLNT = _PNCCLNT
        obeOrdenCompra.PSNORCML = _strOc

        Return obrOrdeCompra.ListarOrdenDeCompra(obeOrdenCompra)
    End Function

#End Region

#Region "Eventos de Control"

    Private Sub dgCabecera_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgCabecera.SelectionChanged
        strOC = Me.dgCabecera.CurrentRow.DataBoundItem.PSNORCML
        Me.dgDetalle.AutoGenerateColumns = False
        Me.dgDetalle.DataSource = olbeDetOc.FindAll(MatchOrdenDeCompra)

    End Sub

    Private Sub bsaCerrarVentana_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bsaCerrarVentana.Click
        Me.Close()
    End Sub

    Private Sub tsbGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbGuardar.Click

        GrabarOc()

    End Sub

    Private Sub btnCargarCabecera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargarCabecera.Click
        Try
            If chkFormatoGYM.Checked Then
                CargaArchivo_2()
            Else
                CargaArchivo_1()
            End If

        Catch ex As Exception
            HelpClass.ErrorMsgBox()
        End Try

    End Sub

#End Region
    Private Sub dgCabecera_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgCabecera.DataBindingComplete
        Dim obrOc As New brOrdenDeCompra
        For intCont As Integer = 0 To Me.dgCabecera.RowCount - 1
            strOC = CType(Me.dgCabecera.Rows(intCont).DataBoundItem, beOrdenCompra).PSNORCML
            CType(Me.dgCabecera.Rows(intCont).DataBoundItem, beOrdenCompra).PageSize = 1000
            CType(Me.dgCabecera.Rows(intCont).DataBoundItem, beOrdenCompra).PageNumber = 1
            If obrOc.ObtenerOrdenDeCompra(Me.dgCabecera.Rows(intCont).DataBoundItem).Count > 0 And obrOc.ListarDetalleOrdenDeCompra(Me.dgCabecera.Rows(intCont).DataBoundItem).Count = olbeDetOc.FindAll(MatchOrdenDeCompra).Count Then
                Me.dgCabecera.Rows(intCont).Cells("IMAGES").Value = My.Resources.button_ok1
            End If
        Next
    End Sub
End Class
