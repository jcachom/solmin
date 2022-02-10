Imports SOLMIN_SC.Entidad
Imports Ransa.Utilitario
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports System.IO
Imports SOLMIN_SC.Negocio

Public Class frmCompararOC

    Private hssfworkbook As HSSFWorkbook

    'Private obj_Excel As Object
    'Private obj_Workbook As Object
    'Private obj_Worksheet As Object
    'Private olbeDetOc As New List(Of beOrdenCompra)
    'Private obj_SheetName As String
    'Private _dblCodCliente As Decimal
    'Private _strUsuario As String



    Private _PNCCLNT As Decimal
    Public Property PNCCLNT() As Decimal
        Get
            Return _PNCCLNT
        End Get
        Set(ByVal value As Decimal)
            _PNCCLNT = value
        End Set
    End Property


    Private _PSCCMPN As String
    Public Property PSCCMPN() As String
        Get
            Return _PSCCMPN
        End Get
        Set(ByVal value As String)
            _PSCCMPN = value
        End Set
    End Property


    Private _PSCDVSN As String
    Public Property PSCDVSN() As String
        Get
            Return _PSCDVSN
        End Get
        Set(ByVal value As String)
            _PSCDVSN = value
        End Set
    End Property


    Private _PNCPLNDV As Decimal
    Public Property PNCPLNDV() As Decimal
        Get
            Return _PNCPLNDV
        End Get
        Set(ByVal value As Decimal)
            _PNCPLNDV = value
        End Set
    End Property


    Private Sub frmCompararOC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ListarOCSegumiento()
    End Sub

    Private Sub ListarOCSegumiento()
        Dim obrOrdenCompra As New clsOC
        Dim obeOrdenCompra As New beOrdenCompra
        dgOcSeguimiento.AutoGenerateColumns = False
        With obeOrdenCompra
            .PSCCMPN = PSCCMPN
            .PSCDVSN = PSCDVSN
            .PNCPLNDV = PNCPLNDV
            .PNCCLNT = PNCCLNT
        End With
        dgOcSeguimiento.DataSource = obrOrdenCompra.flstListaDetalleOrdenConSeguimientoLocal(obeOrdenCompra)

    End Sub

    Private Sub bsaCerrarVentana_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub InitializeWorkbook(ByVal path As String)
 

        Try
            Using file As FileStream = New FileStream(path, FileMode.Open, FileAccess.Read)
                hssfworkbook = New HSSFWorkbook(file)
            End Using
        Catch ex As Exception
            HelpClass.MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub btnCargarCabecera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)






    End Sub


    'Private Sub CargaArchivo_1()

    '    Me.OpenFileDialog.Multiselect = False

    '    Me.OpenFileDialog.InitialDirectory = (System.Environment.GetFolderPath _
    '    (System.Environment.SpecialFolder.Personal))

    '    Me.OpenFileDialog.Filter = "Text Files (*.txt )|*.txt;|All files (*.*)|*.*"
    '    If Me.OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then ' SI DIO CLICK EN ACEPTAR
    '        Me.TxtRutaCabecera.Text = Me.OpenFileDialog.FileName


    '        Dim objReader As New IO.StreamReader(Me.OpenFileDialog.FileName, System.Text.Encoding.GetEncoding(1252))
    '        Dim objReader2 As New IO.StreamReader(Me.OpenFileDialog.FileName, System.Text.Encoding.GetEncoding(1252))

    '        Dim sLine As String = ""

    '        Dim obeOrdenDeCompra As New Entidad.beOrdenCompra
    '        Dim obeOrdenDeCompraDet As New Entidad.beOrdenCompra
    '        Dim obeOrdenDeCompraNotas As New Entidad.beOrdenCompra

    '        Dim olbeProveedor As New List(Of Ransa.TypeDef.Proveedor.beProveedor)


    '        olbeDetOc.Clear()
    '        olbeOrdenDeCompraNotas.Clear()


    '        Dim sDato As String = String.Empty
    '        Dim PSNORCML As String = String.Empty
    '        Dim PSTCITPR As String = String.Empty

    '        Dim psPrecio As String = ""
    '        Dim psCantidad As String = ""
    '        Dim psToleranciaMinina As String = ""
    '        Dim psToleranciamaxima As String = ""

    '        Dim blResultado As Boolean = False
    '        Dim blAA As Boolean = False
    '        Dim blH1 As Boolean = False
    '        Dim blL1 As Boolean = False
    '        Dim blZZ As Boolean = False
    '        Dim strMensaje As String = String.Empty

    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '        Do
    '            sLine = objReader.ReadLine()
    '            If Not sLine Is Nothing AndAlso Not sLine.ToString.Equals("") Then
    '                sDato = CortaTexto(0, 2, sLine)
    '                Select Case sDato
    '                    Case "AA"
    '                        blAA = True
    '                        If fboolValidarAA(sLine) Then
    '                            Exit Sub
    '                        End If
    '                    Case "H1"
    '                        blH1 = True
    '                        If fboolValidarH1(sLine) Then
    '                            Exit Sub
    '                        End If
    '                    Case "L1"
    '                        blL1 = True
    '                        If fboolValidarL1(sLine) Then
    '                            Exit Sub
    '                        End If
    '                    Case "ZZ"
    '                        blZZ = True
    '                End Select
    '            End If
    '        Loop Until sLine Is Nothing OrElse sLine.ToString.Equals("")

    '        If blAA And blH1 And blL1 And blZZ Then
    '            blResultado = True
    '        End If
    '        If blResultado = False Then
    '            dgCabecera.DataSource = Nothing
    '            dgDetalle.DataSource = Nothing
    '            MessageBox.Show("Estructura Incorrecta", "Error de carga", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            objReader.Close()
    '            objReader2.Close()
    '            Exit Sub
    '        End If


    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '        If blResultado Then
    '            olbeOrdenDeCompra.Clear()

    '            Do
    '                sLine = objReader2.ReadLine()

    '                If Not sLine Is Nothing AndAlso Not sLine.ToString.Equals("") Then

    '                    sLine = validaCaracter(sLine, strMensaje)
    '                    If strMensaje <> String.Empty Then
    '                        MessageBox.Show(strMensaje, "Aviso de Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                        objReader.Close()
    '                        objReader2.Close()
    '                        Exit Sub
    '                    End If

    '                    obeProveedor = New beProveedor

    '                    sDato = sLine.Substring(0, 2)
    '                    Select Case sDato
    '                        Case "AA"
    '                            _dblCodCliente = CortaTexto(14, 6, sLine)  'codigo de cliente
    '                        Case "H1"     'CABECERA
    '                            If (_dblCodCliente = "3273") Then
    '                                AUXCODCLIENTE = ""
    '                                If InStr(1, CortaTexto(287, 11, sLine), "20100130468") > 0 Then
    '                                    AUXCODCLIENTE = 3273
    '                                End If
    '                                If InStr(1, CortaTexto(287, 11, sLine), "20192779333") > 0 Then
    '                                    AUXCODCLIENTE = 11496
    '                                End If
    '                                If InStr(1, CortaTexto(287, 11, sLine), "20489174023") > 0 Then
    '                                    AUXCODCLIENTE = 21625
    '                                End If
    '                                If InStr(1, CortaTexto(287, 11, sLine), "20419387658") > 0 Then
    '                                    AUXCODCLIENTE = 16168
    '                                End If
    '                                If InStr(1, CortaTexto(287, 11, sLine), "20381444997") > 0 Then
    '                                    AUXCODCLIENTE = 11121
    '                                End If
    '                                If InStr(1, CortaTexto(287, 11, sLine), "20253106167") > 0 Then
    '                                    AUXCODCLIENTE = 102
    '                                End If
    '                                If InStr(1, CortaTexto(287, 11, sLine), "20131644524") > 0 Then
    '                                    AUXCODCLIENTE = 20337
    '                                End If
    '                                If InStr(1, CortaTexto(287, 11, sLine), "20501322343") > 0 Then
    '                                    AUXCODCLIENTE = 20337
    '                                End If
    '                                If InStr(1, CortaTexto(287, 11, sLine), "20252810634") > 0 Then
    '                                    AUXCODCLIENTE = 44887
    '                                End If
    '                                If InStr(1, CortaTexto(287, 11, sLine), "20513798611") > 0 Then
    '                                    AUXCODCLIENTE = 40582
    '                                End If
    '                            End If

    '                            obeOrdenDeCompra = New beOrdenCompra
    '                            obeProveedor = New beProveedor
    '                            With obeOrdenDeCompra
    '                                If (_dblCodCliente = "3273") Then
    '                                    .PNCCLNT = AUXCODCLIENTE
    '                                Else
    '                                    .PNCCLNT = _dblCodCliente
    '                                End If
    '                                PSNORCML = CortaTexto(2, 15, sLine)    ' NNROPO = Numero de Orden de Compra
    '                                .PSNORCML = PSNORCML.Trim
    '                                .PSTPAISEM = CortaTexto(184, 40, sLine).Trim 'NPAEMB = pais de embarque    
    '                                .PNFORCML = CortaTexto(117, 8, sLine)    'DCREPO = Fecha Creación Orden de Compra
    '                                .PSFORCML_INI = HelpClass.CNumero8Digitos_a_Fecha(CortaTexto(117, 8, sLine))
    '                                .PSNMONOC = CortaTexto(153, 5, sLine).Trim 'NMONEDA = Moneda
    '                                .PSTPAGME = CortaTexto(133, 20, sLine).Trim 'NTERPAG = Termino de Pago
    '                                .PSTNOMCOM = CortaTexto(334, 50, sLine).Trim 'TNOMCOM= Nombre de Comprador
    '                                PSTCITPR = CortaTexto(314, 10, sLine) 'CCODPRO = Código de Proveedor
    '                                .PSTCITPR = PSTCITPR.Trim
    '                                .PNCPRVCL = 0 ' CODIGO DE CLIENTE TERCERO   - generado
    '                                .PSCPRPCL = PSTCITPR.Trim ' Codigo del proveedor - Cliente
    '                                .PSTDSCML = CortaTexto(17, 50, sLine).Trim 'TDESPO = Desc. General Orden de Compra
    '                                .PSTEMPFAC = CortaTexto(264, 50, sLine).Trim 'NEMPFAC = Empresa a facturar

    '                                .PSTCTCST = CortaTexto(125, 5, sLine).Trim 'CCENCOS = Centro de Costo
    '                                .PSTTINTC = CortaTexto(130, 3, sLine).Trim 'NTERCOM = Termino de la Compra
    '                                .PNCMEDTR = BuscaMediotransporte(CortaTexto(158, 1, sLine)) 'FMEDEM = Medio de Embarque  
    '                                .PSCREGEMB = CortaTexto(332, 2, sLine) 'CREGEMB = Región de Embarque
    '                                .PSTLUGEM = CortaTexto(224, 20, sLine) 'NPTOEMB = Puerto de Embarque
    '                                .PSTDEFIN = CortaTexto(244, 20, sLine) 'NPTODES = Puerto de Desembarque Cliente
    '                                .PNNTPDES = Val(CortaTexto(159, 1, sLine)) 'FURG = Nivel de Urgencia     
    '                                .PSSESTRG = "A"
    '                                .PSNSVP = "A"
    '                                .PSSTRANS = ""
    '                                .PSSFLGES = "A"

    '                                .PSNTRMNL = _strHostName.ToUpper
    '                                .PSUSUARIO = _strUser
    '                                .PSCUSCRT = _strUser
    '                                .PSNTRMCR = _strHostName.ToUpper
    '                                olbeOrdenDeCompra.Add(obeOrdenDeCompra)



    '                                'datos del proveedor

    '                                obeProveedor.PSCPRPCL = PSTCITPR  ' Codigo de proveedor
    '                                'obeProveedor.PNCPRVCL = 0     ' Codigo Generado
    '                                obeProveedor.PNCCLNT = _dblCodCliente      ' CLIENTE       PSTPRCL1
    '                                obeProveedor.PSTPRVCL = CortaTexto(364, 30, sLine) 'NNOMPRO= Nombre Proveedor
    '                                obeProveedor.PNNRUCPR = 0
    '                                obeProveedor.PSTNACPR = PSTCITPR
    '                                obeProveedor.PSTDRPRC = CortaTexto(544, 35, sLine) ' TDRPRC = Direccion        -tamaño 80 segun archivo
    '                                obeProveedor.PSTDRDST = CortaTexto(544, 70, sLine) ' TDRPRC = Direccion        -tamaño 80 segun archivo
    '                                obeProveedor.PNCPAIS = 0
    '                                obeProveedor.PSTNROFX = ""
    '                                obeProveedor.PSTLFNO1 = CortaTexto(409, 40, sLine)   'NFAXPRO = Fax Proveedor
    '                                obeProveedor.PSTEMAI2 = CortaTexto(394, 15, sLine) 'NTELPRO = Teléfono Proveedor

    '                                obeProveedor.PSTPRSCN = CortaTexto(449, 40, sLine) 'NCTOPRO = Contacto Proveedor
    '                                obeProveedor.PSTLFNO2 = CortaTexto(489, 15, sLine) 'NTCTOPR = Teléfono contacto Proveedor
    '                                obeProveedor.PSTEMAI3 = CortaTexto(504, 40, sLine) 'TMCTOPR = Mail contacto Proveedor

    '                                obeProveedor.PNNDSDMP = 0
    '                                obeProveedor.PSNTRMNL = _strHostName.ToUpper
    '                                obeProveedor.PSUSUARIO = _strUser
    '                                .PNCPRVCL = ObtenerCodigoProveedor(obeProveedor)


    '                                .PSTPRSCN = CortaTexto(449, 40, sLine) 'NCTOPRO = Contacto Proveedor
    '                                .PSTLFNO2 = CortaTexto(489, 15, sLine) 'NTCTOPR = Teléfono contacto Proveedor
    '                                .PSTEMAI3 = CortaTexto(504, 40, sLine) 'TMCTOPR = Mail contacto Proveedor

    '                            End With

    '                        Case "L1"  'DELTALLE
    '                            obeOrdenDeCompraDet = New beOrdenCompra
    '                            With obeOrdenDeCompraDet
    '                                If (_dblCodCliente = "3273") Then
    '                                    .PNCCLNT = AUXCODCLIENTE
    '                                Else
    '                                    .PNCCLNT = _dblCodCliente
    '                                End If
    '                                .PSNORCML = PSNORCML.Trim     'NNROPO = Numero de Orden de Compra
    '                                .PSTDITIN = CortaTexto(173, 100, sLine) ' TDECOIN = Descripción corta en Ingles
    '                                .PSCPTDAR = CortaTexto(304, 15, sLine) ' MPDAARA = Numero de Partida Arancelaria

    '                                psCantidad = CortaTexto(52, 10, sLine) & "." & CortaTexto(62, 5, sLine)

    '                                If psCantidad.Length > 1 Then
    '                                    .PNQCNTIT = Val(psCantidad) ' QCANORD= Cantidad Ordenada
    '                                End If

    '                                .PNQSDOIT = 0
    '                                .PNQINEMP = 0
    '                                .PSTUNDIT = CortaTexto(67, 4, sLine)   'CUNDMED= Unidad de Medida
    '                                psPrecio = CortaTexto(271, 10, sLine) & "." & CortaTexto(281, 5, sLine) 'SPREUND = Precio Unitario

    '                                If psPrecio.Length > 1 Then
    '                                    .PNIVTOIT = .PNQCNTIT * Convert.ToDecimal(psPrecio)
    '                                    .PNIVTOIT = Format(.PNIVTOIT, "0000000000.00000")
    '                                    .PNIVUNIT = Val(psPrecio)
    '                                End If

    '                                .PNFMPDME = CortaTexto(286, 8, sLine)  'DPRODES = Fec Máx. Prov. despachará merc.
    '                                .PNFMPIME = Val(CortaTexto(294, 8, sLine))  'DMATCLI = Fec. Máx. material Req. en Cliente
    '                                .PSTLUGEN = CortaTexto(517, 50, sLine) 'TLUGEN = Lugar entrega al cliente
    '                                .PSTLUGOR = CortaTexto(567, 50, sLine) 'TLUREC = Lugar recepción (alm. origen) - CONFIRMAR
    '                                .PSSFLGES = "A"
    '                                .PSNTRMNL = _strHostName.ToUpper
    '                                .PSUSUARIO = _strUser
    '                                .PSCUSCRT = _strUser
    '                                .PSNTRMCR = _strHostName.ToUpper
    '                                .PSSESTRG = "A"
    '                                .PSCULUSA = _strUser
    '                                .PNQSTKIT = 0

    '                                psToleranciaMinina = CortaTexto(617, 10, sLine) & "." & CortaTexto(627, 5, sLine)    'QTOLMIN = Cantidad tolerancia mínima
    '                                .PNQTOLMIN = Val(IIf(psToleranciaMinina.Length > 1, psToleranciaMinina, 0)) 'QTOLMIN = Cantidad tolerancia mínima
    '                                psToleranciamaxima = CortaTexto(632, 10, sLine) & "." & CortaTexto(642, 5, sLine)
    '                                .PNQTOLMAX = Val(IIf(psToleranciamaxima.Length > 1, psToleranciamaxima, 0))
    '                                .PSTRFRN = CortaTexto(647, 50, sLine) 'TREFRN1 = Referencia 1 Cliente (Solicitante)
    '                                .PSTRFRNA = CortaTexto(697, 50, sLine) 'TREFRN2 = Referencia 2 Cliente (Lote)
    '                                .PSTRFRNB = CortaTexto(747, 50, sLine) 'TREFRN3 = Referencia 3 Cliente (otro.)
    '                                .PSTRFRN1 = CortaTexto(797, 10, sLine) 'TREFRN4 = Referencia 4 Cliente (Centro Dest.)
    '                                .PSTRFRN2 = CortaTexto(807, 10, sLine) 'TREFRN5 = Referencia 5 Cliente (Almacen Dest)
    '                                .PNNRITOC = CortaTexto(17, 5, sLine) ' Nro de Item  MNROITE
    '                                .PSTCITCL = CortaTexto(22, 15, sLine) ' CMERCLI = Código de Mercadería (Cliente)
    '                                .PSTCITPR = PSTCITPR   'codigo de proveedor    CCODPRO
    '                                .PSTDITES = CortaTexto(71, 100, sLine).Trim ' TDECOCA = Descripción corta en castellano
    '                                .PNFMPDME = CortaTexto(286, 8, sLine)     'DPRODES = Fec Máx. Prov. despachará merc.

    '                                olbeDetOc.Add(obeOrdenDeCompraDet)
    '                            End With
    '                        Case "H2"  'Notas
    '                            obeOrdenDeCompraNotas = New beOrdenCompra

    '                            With obeOrdenDeCompraNotas
    '                                If (_dblCodCliente = "3273") Then
    '                                    .PNCCLNT = AUXCODCLIENTE
    '                                Else
    '                                    .PNCCLNT = _dblCodCliente
    '                                End If
    '                                .PSNORCML = CortaTexto(2, 15, sLine)
    '                                .PSTNOTAS = CortaTexto(17, 587, sLine)
    '                                .PSNTRMNL = _strHostName.ToUpper
    '                                .PSUSUARIO = _strUser
    '                                .PSCUSCRT = _strUser
    '                                .PSNTRMCR = _strHostName.ToUpper
    '                            End With
    '                            If obeOrdenDeCompraNotas.PSTNOTAS.Length > 0 Then olbeOrdenDeCompraNotas.Add(obeOrdenDeCompraNotas)

    '                    End Select

    '                End If
    '            Loop Until sLine Is Nothing OrElse sLine.ToString.Equals("")
    '            objReader.Close()
    '            objReader2.Close()
    '            Me.dgCabecera.DataSource = Nothing
    '            Me.dgCabecera.AutoGenerateColumns = False
    '            Me.dgCabecera.DataSource = olbeOrdenDeCompra
    '            tsbGuardar.Enabled = True
    '        End If

    '    End If
    'End Sub

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

    Private Sub btnAbrirCabecera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer
            Dim Columnas As Integer = 12
            ' -- Verifica si existe el archivo   
            'If Len(Dir(Me.TxtRutaXlsCabecera.Text)) = 0 Then
            '    MsgBox("No se ha encontrado el archivo: " & Me.TxtRutaXlsCabecera.Text, vbCritical)
            '    Exit Sub
            'End If
            ''===================================================================
            '' -- RUTINA EXCEL   Instancia Excel  
            'If obj_Excel Is Nothing Then
            '    obj_Excel = CreateObject("Excel.Application")
            '    obj_Workbook = obj_Excel.Workbooks.Open(Me.TxtRutaXlsCabecera.Text)
            'End If
            '' -- Referencia la Hoja, por defecto la hoja activa  
            'If obj_SheetName = vbNullString Then
            '    obj_Worksheet = obj_Workbook.ActiveSheet
            'Else
            '    obj_Worksheet = obj_Workbook.Sheets(obj_SheetName)
            'End If
            ''==================Crear lista de Objeto de tipo OC beOrdenDeCOMPRA
            'Dim olbeOrdenDeCompra As New List(Of beOrdenCompra)
            'Dim olbeOrdenDeCompraMatch As List(Of beOrdenCompra)
            'Dim obeOrdenDeCompra As New beOrdenCompra
            'Dim strReferencia As String = ""
            'For i = 1 To NmCount.Value - 2
            '    obeOrdenDeCompra = New beOrdenCompra
            '    olbeOrdenDeCompraMatch = New List(Of beOrdenCompra)
            '    strOC = obj_Worksheet.Cells(i + 1, 5).Value
            '    strRef = ""

            '    obeOrdenDeCompra.PSNREFCL = obj_Worksheet.Cells(i + 1, 1).Value.ToString.Trim

            '    obeOrdenDeCompra.PSTPOOCM = obj_Worksheet.Cells(i + 1, 6).Value.ToString.Trim 'Tipo de OC

            '    obeOrdenDeCompra.PSTRFRN = ("" & obj_Worksheet.Cells(i + 1, 3).Value & "")
            '    obeOrdenDeCompra.PSTCMAEM = ("" & obj_Worksheet.Cells(i + 1, 12).Value & "") 'Cod. De Ruta De Aprobación
            '    obeOrdenDeCompra.PSNORCML = obj_Worksheet.Cells(i + 1, 5).Value 'OC
            '    obeOrdenDeCompra.PNFROCMP = obj_Worksheet.Cells(i + 1, 9).Value 'fecha de entrega
            '    obeOrdenDeCompra.PSREFDO1 = ("" & obj_Worksheet.Cells(i + 1, 10).Value & "") 'cuenta contable
            '    obeOrdenDeCompra.PSCPRPCL = ("" & obj_Worksheet.Cells(i + 1, 7).Value & "") 'codigo de proveedor de cliente 
            '    obeOrdenDeCompra.PSTNOMCOM = ("" & obj_Worksheet.Cells(i + 1, 8).Value & "") '

            '    If obj_Worksheet.Cells(i + 1, 4).Value.ToString.Trim = "300" OrElse obj_Worksheet.Cells(i + 1, 4).Value.ToString.Trim = "324" OrElse obj_Worksheet.Cells(i + 1, 4).Value.ToString.Trim = "331" OrElse _
            '   obj_Worksheet.Cells(i + 1, 4).Value.ToString.Trim = "400" OrElse obj_Worksheet.Cells(i + 1, 4).Value.ToString.Trim = "427" Then
            '        obeOrdenDeCompra.PSLOTE = obj_Worksheet.Cells(i + 1, 10).Value.ToString.Trim.Substring(0, 4)
            '    Else
            '        obeOrdenDeCompra.PSLOTE = obj_Worksheet.Cells(i + 1, 4).Value
            '    End If

            '    Dim obrGeneral As New brGeneral
            '    Dim obeGeneral As New beGeneral
            '    Dim olbeGeneral As New List(Of beGeneral)
            '    With obeGeneral
            '        .PSNLTECL = obeOrdenDeCompra.PSLOTE
            '    End With
            '    olbeGeneral = obrGeneral.LotesDeEntregaXCliente(obeGeneral)
            '    If olbeGeneral.Count > 0 Then
            '        obeOrdenDeCompra.PNCCLNT = olbeGeneral.Item(0).PNCCLNT
            '    Else
            '        obeOrdenDeCompra.PNCCLNT = 0
            '    End If
            '    If obeOrdenDeCompra.PNCCLNT = 0 Then
            '        HelpClass.MsgBox("No se puede identificar a que cliente pertenece el código de Lote : " & obeOrdenDeCompra.PSLOTE, MessageBoxIcon.Information)
            '        Exit Sub
            '    End If
            '    obeOrdenDeCompra.PNCPRVCL = BuscarProveedor(obj_Worksheet.Cells(i + 1, 7).Value, obeOrdenDeCompra.PSNORCML, obeOrdenDeCompra.PNCCLNT)

            '    obeOrdenDeCompra.PSNRUCPR = Val(obj_Worksheet.Cells(i + 1, 14).Value) 'Ruc del proveedor se necesita para saber si es locar o importacion
            '    If obeOrdenDeCompra.PSNRUCPR.ToString.Length > 8 Then
            '        obeOrdenDeCompra.PSTTINTC = "LOC"
            '    Else
            '        obeOrdenDeCompra.PSTTINTC = "FOB"
            '    End If
            '    If obeOrdenDeCompra.PNCPRVCL = 0 Then
            '        Dim obeProveedor As New beProveedor
            '        With obeProveedor
            '            .PNCCLNT = obeOrdenDeCompra.PNCCLNT
            '            .PSCPRPCL = obj_Worksheet.Cells(i + 1, 7).Value 'Cod proveedor cliente
            '            If ("" & obj_Worksheet.Cells(i + 1, 13).Value & "").ToString.Trim.Length > 30 Then
            '                .PSTPRVCL = ("" & obj_Worksheet.Cells(i + 1, 13).Value & "").ToString.Trim.Substring(0, 30) 'Descripcion del proveedor
            '                .PSTPRCL1 = ("" & obj_Worksheet.Cells(i + 1, 13).Value & "").ToString.Trim.Substring(30, ("" & obj_Worksheet.Cells(i + 1, 13).Value & "").ToString.Trim.Length - 30) 'Descripcion extedida del proveedor
            '            Else
            '                .PSTPRVCL = ("" & obj_Worksheet.Cells(i + 1, 13).Value & "").ToString.Trim
            '            End If
            '            'Ruc del proveedor
            '            If IsNumeric(obj_Worksheet.Cells(i + 1, 14).Value()) Then
            '                .PNNRUCPR = obj_Worksheet.Cells(i + 1, 14).Value()
            '            Else
            '                .PNNRUCPR = 0
            '            End If


            '            .PSCUSCRT = strUsuario
            '        End With
            '        obeOrdenDeCompra.PNCPRVCL = GrabarProveedoCliente(obeProveedor)
            '        If obeOrdenDeCompra.PNCPRVCL = 0 Then
            '            Exit Sub
            '        End If
            '    End If
            '    obeOrdenDeCompra.PNFORCML = HelpClass.CFecha_a_Numero8Digitos(DateTime.Now)
            '    obeOrdenDeCompra.PNFSOLIC = HelpClass.CFecha_a_Numero8Digitos(DateTime.Now)


            '    olbeOrdenDeCompra.Add(obeOrdenDeCompra)
            'Next
            'Dim oucOrdena As UCOrdena(Of beOrdenCompra)
            'oucOrdena = New UCOrdena(Of beOrdenCompra)("PSNORCML", UCOrdena(Of beOrdenCompra).TipoOrdenacion.Ascendente)
            'olbeOrdenDeCompra.Sort(oucOrdena)
            'For intRow As Integer = olbeOrdenDeCompra.Count - 1 To 1 Step -1
            '    If olbeOrdenDeCompra.Item(intRow).PSNORCML = olbeOrdenDeCompra.Item(intRow - 1).PSNORCML Then
            '        olbeOrdenDeCompra.RemoveAt(intRow)
            '    End If
            'Next
            'Me.dgExcelCabecera.AutoGenerateColumns = False
            'Me.dgExcelCabecera.DataSource = olbeOrdenDeCompra
            'Call Limpiar()
            'If (Me.dgExcelCabecera.Rows.Count > 0) Then
            '    btnExcelDetalle.Enabled = True
            '    brnAbrirDetalle.Enabled = True
            'Else
            '    btnExcelDetalle.Enabled = False
            '    brnAbrirDetalle.Enabled = False
            'End If
            'brnAbrirDetalle.Enabled = True
            'btnExcelDetalle.Enabled = True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'Call Limpiar()
        End Try

    End Sub

    Private Sub tsbGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbGuardar.Click
        Procesar()
    End Sub

    Private Sub Procesar()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim obrOc As New clsOC

            For Each obeOc As beOrdenCompra In dgOcSeguimiento.DataSource
                'Buscar si esta  dtgOCArchivoExcel
                Dim olstOcExiste As List(Of beOrdenCompra)
                strOrdenCompra = obeOc.PSNORCML
                intNroItem = obeOc.PNNRITOC
                olstOcExiste = dtgOCArchivoExcel.DataSource.FindAll(MatchOrdenCompra)
                If olstOcExiste.Count > 0 Then
                    obeOc.PNEXISTE = 1
                Else
                    obeOc.PNEXISTE = 0
                    obeOc.PSFLGPEN = 1
                End If
            Next
            'Actualizamos con
            Dim olstOc As New List(Of beOrdenCompra)
            olstOc = dgOcSeguimiento.DataSource.FindAll(MatchNoexiste)
            If obrOc.finActualizarEstadoSeguimiento(olstOc) = 0 Then
                HelpClass.ErrorMsgBox()
            End If

            'Orden de compra no xistentes
            Dim olstOcNoExistentes As New List(Of beOrdenCompra)
            olstOcNoExistentes = dtgOCArchivoExcel.DataSource.FindAll(MatchNoexiste)

            'Orden de compra xistentes
            Dim olstOcConSeguimiento As New List(Of beOrdenCompra)
            olstOcConSeguimiento = dgOcSeguimiento.DataSource.FindAll(MatchExiste)

            Dim olstOcConSeguimientoExcel As New List(Of beOrdenCompra)
            olstOcConSeguimientoExcel = dtgOCArchivoExcel.DataSource.FindAll(MatchExiste)

            'Se va hacer seguimiento
            Dim olstOCeSeguimiento As New List(Of beOrdenCompra)

            For Each obeOc As beOrdenCompra In olstOcConSeguimiento
                Dim olstOcExiste As List(Of beOrdenCompra)
                strOrdenCompra = obeOc.PSNORCML
                intNroItem = obeOc.PNNRITOC
                olstOcExiste = olstOcConSeguimientoExcel.FindAll(MatchOrdenCompra)
                If olstOcExiste.Count > 0 Then
                    olstOcConSeguimientoExcel.Remove(olstOcExiste.Item(0))
                End If
                olstOCeSeguimiento.Add(obeOc)
            Next

            For Each obeOc As beOrdenCompra In olstOcConSeguimientoExcel
                olstOCeSeguimiento.Add(obeOc)
            Next


            'Creamos la OC
            Dim oDs As New DataSet
            Dim oDt1 As New DataTable
            oDt1.TableName = "Oc en Segumiento"
            Dim oDt2 As New DataTable
            oDt2.TableName = "Oc quitado de Seguimiento"
            Dim oDt3 As New DataTable
            oDt3.TableName = "Oc no existentes"
            Dim oColumm As DataColumn

            '==========Data Table 01====================
            oColumm = New DataColumn
            oColumm.ColumnName = "Orden de Compra"
            oColumm.DataType = GetType(String)
            oDt1.Columns.Add(oColumm)


            oColumm = New DataColumn
            oColumm.ColumnName = "Nro. Item"
            oColumm.DataType = GetType(Integer)
            oDt1.Columns.Add(oColumm)

            oColumm = New DataColumn
            oColumm.ColumnName = "Término Internacional"
            oColumm.DataType = GetType(String)
            oDt1.Columns.Add(oColumm)

            '==========Data Table 02====================
            oColumm = New DataColumn
            oColumm.ColumnName = "Orden de Compra"
            oColumm.DataType = GetType(String)
            oDt2.Columns.Add(oColumm)


            oColumm = New DataColumn
            oColumm.ColumnName = "Nro Item"
            oColumm.DataType = GetType(Integer)
            oDt2.Columns.Add(oColumm)

            oColumm = New DataColumn
            oColumm.ColumnName = "Término Internacional"
            oColumm.DataType = GetType(String)
            oDt2.Columns.Add(oColumm)


            '==========Data Table 03====================
            oColumm = New DataColumn
            oColumm.ColumnName = "Orden de Compra"
            oColumm.DataType = GetType(String)
            oDt3.Columns.Add(oColumm)


            oColumm = New DataColumn
            oColumm.ColumnName = "Nro. Item"
            oColumm.DataType = GetType(Integer)
            oDt3.Columns.Add(oColumm)



            'Oc Quitado en seguimiento
            Dim oDr1 As DataRow
            For Each obeOCNoExistentes As beOrdenCompra In olstOCeSeguimiento
                oDr1 = oDt1.NewRow
                oDr1(0) = obeOCNoExistentes.PSNORCML
                oDr1(1) = obeOCNoExistentes.PNNRITOC
                oDr1(2) = obeOCNoExistentes.PSTTINTC
                oDt1.Rows.Add(oDr1)
            Next

            'Oc Quitado se seguimiento
            Dim oDr2 As DataRow

            For Each obeOCNoExistentes As beOrdenCompra In olstOc
                oDr2 = oDt2.NewRow
                oDr2(0) = obeOCNoExistentes.PSNORCML
                oDr2(1) = obeOCNoExistentes.PNNRITOC
                oDr2(2) = obeOCNoExistentes.PSTTINTC
                oDt2.Rows.Add(oDr2)
            Next

            'Orden No existente
            Dim oDr3 As DataRow

            For Each obeOCNoExistentes As beOrdenCompra In olstOcNoExistentes
                oDr3 = oDt3.NewRow
                oDr3(0) = obeOCNoExistentes.PSNORCML
                oDr3(1) = obeOCNoExistentes.PNNRITOC
                oDt3.Rows.Add(oDr3)
            Next

            oDs.Tables.Add(oDt1)
            oDs.Tables.Add(oDt2)
            oDs.Tables.Add(oDt3)

            Dim strlTitulo As New List(Of String)

            strlTitulo.Add(" \n" & " ")
            HelpClass.ExportExcel_Formato01(oDs, "Resumen de ordenes procesadas", strlTitulo)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
        


    End Sub

#Region "Búsqueda de Checkpoint"

    Private strOrdenCompra As String = ""
    Private intNroItem As Decimal = 0

    Private MatchOrdenCompra As New Predicate(Of beOrdenCompra)(AddressOf BuscarOrdenCompra)

    Public Function BuscarOrdenCompra(ByVal pbeOrdenCompra As beOrdenCompra) As Boolean
        If (pbeOrdenCompra.PSNORCML = strOrdenCompra) And (pbeOrdenCompra.PNNRITOC = intNroItem) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private MatchNoexiste As New Predicate(Of beOrdenCompra)(AddressOf BuscarOcNoExiste)

    Public Function BuscarOcNoExiste(ByVal pbeOrdenCompra As beOrdenCompra) As Boolean
        If (pbeOrdenCompra.PNEXISTE = 0) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private MatchExiste As New Predicate(Of beOrdenCompra)(AddressOf BuscarOcExistente)

    Public Function BuscarOcExistente(ByVal pbeOrdenCompra As beOrdenCompra) As Boolean
        If (pbeOrdenCompra.PNEXISTE = 1) Then
            Return True
        Else
            Return False
        End If
    End Function


#End Region



    Private Sub dtgOCArchivoExcel_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs)
        For intX As Integer = 0 To Me.dtgOCArchivoExcel.RowCount - 1
            If dtgOCArchivoExcel.Rows(intX).DataBoundItem.PNEXISTE = 1 Then
                dtgOCArchivoExcel.Rows(intX).Cells("EXISTE").Value = "SI"
            Else
                dtgOCArchivoExcel.Rows(intX).Cells("EXISTE").Value = "NO"
            End If
        Next



    End Sub

 
    <STAThread()> _
    Private Sub tsbBuscarArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBuscarArchivo.Click
        Try

            'No Seleccionar Multiples Archivos 
            Me.OpenFileDialog.Multiselect = False
            'EL DIRECTORIO INICIAL ES MIS DOCUMENTOS
            Me.OpenFileDialog.InitialDirectory = (System.Environment.GetFolderPath _
            (System.Environment.SpecialFolder.Personal))
            'FILTRO LAS EXTENSIONES QUE QUIERO MOSTRAR
            Me.OpenFileDialog.Filter = "Excel Files (*.XLS;*.CSV;)|*.XLS;*.CSV;|All files (*.*)|*.*"
            If Me.OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then ' SI DIO CLICK EN ACEPTAR
                Me.TxtRutaCabecera.Text = Me.OpenFileDialog.FileName
                Me.Cursor = Cursors.WaitCursor
                InitializeWorkbook(Me.TxtRutaCabecera.Text)

                Dim Pagina As Sheet = hssfworkbook.GetSheetAt(0)
                Dim Rows As System.Collections.IEnumerator
                Rows = Pagina.GetRowEnumerator()
                Dim olbeOrdenDeCompra As New List(Of beOrdenCompra)

                Rows.MoveNext()
                While (Rows.MoveNext())
                    Dim obeDetOC As New beOrdenCompra
                    Dim row As Row = CType(Rows.Current, HSSFRow)
                    obeDetOC.PNCCLNT = PNCCLNT
                    If row.GetCell(0).CellType = CellType.STRING Then
                        obeDetOC.PSNORCML = row.GetCell(0).StringCellValue
                    ElseIf (row.GetCell(0).CellType = CellType.NUMERIC) Then
                        obeDetOC.PSNORCML = row.GetCell(0).NumericCellValue.ToString
                    End If
                    If row.GetCell(1).CellType = CellType.NUMERIC Then
                        obeDetOC.PNNRITOC = row.GetCell(1).NumericCellValue
                    End If
                    olbeOrdenDeCompra.Add(obeDetOC)
                End While

                'Verificamos si Existe y actualizamos el estado del seguimiento OC
                'Dim obrOrdenCompra As New clsOC
                'obeDetOC.PNEXIS
                Dim obrOrdenCompra As New clsOC
                For Each obeOC As beOrdenCompra In olbeOrdenDeCompra
                    'Verificamos si Existe y actualizamos el estado del seguimiento OC
                    obeOC.PNEXISTE = obrOrdenCompra.fintVerificarEstadoSegumientoOcLocal(obeOC)
                    If obeOC.PNEXISTE = -1 Then
                        HelpClass.ErrorMsgBox()
                        Exit Sub
                    End If
                Next
                dtgOCArchivoExcel.AutoGenerateColumns = False
                dtgOCArchivoExcel.DataSource = olbeOrdenDeCompra
                tsbGuardar.Enabled = True
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            HelpClass.MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub tsbBitacora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBitacora.Click
        Try

            'No Seleccionar Multiples Archivos 
            Me.OpenFileDialog.Multiselect = False
            'EL DIRECTORIO INICIAL ES MIS DOCUMENTOS
            Me.OpenFileDialog.InitialDirectory = (System.Environment.GetFolderPath _
            (System.Environment.SpecialFolder.Personal))
            'FILTRO LAS EXTENSIONES QUE QUIERO MOSTRAR
            Me.OpenFileDialog.Filter = "Excel Files (*.XLS;*.CSV;)|*.XLS;*.CSV;|All files (*.*)|*.*"
            If Me.OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then ' SI DIO CLICK EN ACEPTAR
                Me.TxtRutaCabecera.Text = Me.OpenFileDialog.FileName

                Me.Cursor = Cursors.WaitCursor
                InitializeWorkbook(Me.TxtRutaCabecera.Text)

                Dim intQlineas As Integer = 0
                Dim Pagina As Sheet = hssfworkbook.GetSheetAt(0)
                Dim Rows As System.Collections.IEnumerator
                Rows = Pagina.GetRowEnumerator()
                Dim obrCheckPoint As New clsBitacora
                Dim intRows As Integer = 0
                Rows.MoveNext()
                While (Rows.MoveNext())
                    Dim obeBitacora As New beBitacora
                    Dim row As Row = CType(Rows.Current, HSSFRow)
                    Dim strObservacion As String = ""
                    If row.GetCell(2).CellType = CellType.STRING Then
                        strObservacion = row.GetCell(2).StringCellValue.ToString.Trim
                    End If
                   
                    intQlineas = Math.Ceiling(strObservacion.Trim.Length / 250)

                    For intLine As Integer = 1 To intQlineas
                        obeBitacora = New beBitacora
                        With obeBitacora
                            .PNCCLNT = PNCCLNT
                            If row.GetCell(0).CellType = CellType.STRING Then
                                .PSNORCML = row.GetCell(0).StringCellValue
                            ElseIf (row.GetCell(0).CellType = CellType.NUMERIC) Then
                                .PSNORCML = row.GetCell(0).NumericCellValue.ToString
                            End If
                            If row.GetCell(1).CellType = CellType.NUMERIC Then
                                .PNNRITOC = row.GetCell(1).NumericCellValue
                            End If
                            If intLine = intQlineas Then
                                .PNTFCOBS = Ransa.Utilitario.HelpClass.CDate_a_Numero8Digitos(Date.Now.ToShortDateString)
                                .PSTOBS = strObservacion.Trim.Substring(250 * (intLine - 1), strObservacion.Trim.Length - 250 * (intLine - 1))
                            Else
                                .PNTFCOBS = Ransa.Utilitario.HelpClass.CDate_a_Numero8Digitos(Date.Now.ToShortDateString)
                                .PSTOBS = strObservacion.Trim.Substring(250 * (intLine - 1), 250)
                            End If
                            .PSCUSCRT = HelpUtil.UserName
                        End With
                        If obrCheckPoint.insertar_bitacora_itemOC(obeBitacora) = False Then
                            HelpClass.ErrorMsgBox()
                            Exit For
                        End If
                    Next

                End While
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            HelpClass.MsgBox(ex.Message)
        End Try
    End Sub
End Class
