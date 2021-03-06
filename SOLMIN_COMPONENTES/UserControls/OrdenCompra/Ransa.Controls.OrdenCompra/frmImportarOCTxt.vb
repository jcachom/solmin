Imports Ransa.DAO.OrdenCompra
Imports RANSA.Utilitario
Imports Db2Manager.RansaData.iSeries.DataObjects
Imports Ransa.TypeDef.OrdenCompra.OrdenCompra
Imports Ransa.TypeDef.OrdenCompra.ItemOC
Imports Ransa.TypeDef
Imports Ransa.NEGO
Imports Ransa.DATA


Public Class frmImportarOCTxt
#Region "Atributos"
    Private obj_Excel As Object
    Private obj_Workbook As Object
    Private obj_Worksheet As Object
    Private olbeDetOc As New List(Of beOrdenCompra)
    Private olbeObservacionesDetOc As New List(Of beOrdenCompra)
    Private obj_SheetName As String
    Private _dblCodCliente As Decimal
    Private _strUsuario As String

    Public Property dblCodCliente() As Decimal
        Get
            Return _dblCodCliente
        End Get
        Set(ByVal value As Decimal)
            _dblCodCliente = value
        End Set
    End Property


    Public Property strUsuario() As String
        Get
            Return _strUsuario
        End Get
        Set(ByVal value As String)
            _strUsuario = value
        End Set
    End Property

    Private _pTerminal As String
    Public Property pTerminal() As String
        Get
            Return _pTerminal
        End Get
        Set(ByVal value As String)
            _pTerminal = value
        End Set
    End Property

#End Region

    Private Sub btnCargarCabecera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargarCabecera.Click
        'No Seleccionar Multiples Archivos 
        Me.OpenFileDialog.Multiselect = False
        'EL DIRECTORIO INICIAL ES MIS DOCUMENTOS
        Me.OpenFileDialog.InitialDirectory = (System.Environment.GetFolderPath _
        (System.Environment.SpecialFolder.Personal))
        'FILTRO LAS EXTENSIONES QUE QUIERO MOSTRAR
        Me.OpenFileDialog.Filter = "Excel Files (*.txt )|*.txt;|All files (*.*)|*.*"
        If Me.OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then ' SI DIO CLICK EN ACEPTAR
            Me.TxtRutaCabecera.Text = Me.OpenFileDialog.FileName
            Dim objReader As New IO.StreamReader(Me.OpenFileDialog.FileName)
            Dim sLine As String = ""
            Dim olbeOrdenDeCompra As New List(Of beOrdenCompra)
            Dim obeOrdenDeCompra As New beOrdenCompra
            Dim olbeProveedor As New List(Of beProveedor)
            Dim obeProveedor As New beProveedor
            Do
                sLine = objReader.ReadLine()
                If Not sLine Is Nothing AndAlso Not sLine.ToString.Equals("") Then
                    obeOrdenDeCompra = New beOrdenCompra
                    obeProveedor = New beProveedor
                    With obeOrdenDeCompra
                        Dim strOc As Object
                        strOc = sLine.Split(""",""")
                        .PNCCLNT = _dblCodCliente
                        .PSNORCML = strOc(5)
                        .PNFORCML = HelpClass.CDate_a_Numero8Digitos(strOc(7))
                        .PSNMONOC = strOc(13)
                        .PNFSOLIC = HelpClass.CDate_a_Numero8Digitos(strOc(15))
                        .PSTLUGEN = strOc(17)
                        .PSTRFRNA = IIf(strOc(19).ToString.Equals(""), strOc(21), strOc(19))
                        .PSTTINTC = strOc(23)
                        .PSTPAGME = strOc(27)
                        If ("" & strOc(29) & "").ToString.Trim.Length > 30 Then
                            obeProveedor.PSTPRVCL = ("" & strOc(29) & "").ToString.Trim.Substring(0, 30) 'Descripcion del proveedor
                            obeProveedor.PSTPRCL1 = ("" & strOc(29) & "").ToString.Trim.Substring(30, ("" & strOc(35) & strOc(37) & "").ToString.Trim.Length - 30) 'Descripcion extedida del proveedor
                        Else
                            obeProveedor.PSTPRVCL = ("" & strOc(29) & "").ToString.Trim
                        End If
                        .PSCPRPCL = strOc(31)
                        If ("" & strOc(35) & strOc(37) & "").ToString.Trim.Length > 30 Then
                            obeProveedor.PSTDRPRC = ("" & strOc(35) & strOc(37) & "").ToString.Trim.Substring(0, 35) 'Descripcion del proveedor
                            obeProveedor.PSTPRSCN = ("" & strOc(35) & strOc(37) & "").ToString.Trim.Substring(35, ("" & strOc(35) & strOc(37) & "").ToString.Trim.Length - 35) 'Descripcion extedida del proveedor
                        Else
                            obeProveedor.PSTDRPRC = ("" & strOc(35) & strOc(37) & "").ToString.Trim
                        End If
                        obeProveedor.PSTNACPR = HelpClass.CortarString(strOc(39), 30)
                        .PSTPAISEM = HelpClass.CortarString(strOc(45), 24)
                        obeProveedor.PNCPAIS = BuscarCodigoPais(HelpClass.CortarString(strOc(45), 24))
                        'obeProveedor.PSTPRSCN = IIf(strOc(47).ToString.Equals(""), strOc(49), strOc(45))--Pendiente por que la tabla se va modificar
                        obeProveedor.PSTLFNO1 = HelpClass.CortarString(strOc(51), 15)
                        obeProveedor.PSTEMAI2 = HelpClass.CortarString(strOc(53), 40)
                        .PSTNOMCOM = HelpClass.CortarString(IIf(strOc(55).ToString.Trim.Equals(""), strOc(57), strOc(55)), 50)
                        .PNCPRVCL = BuscarProveedor(obeProveedor)
                        If .PNCPRVCL = 0 Then
                            Exit Sub
                        End If
                    End With
                    olbeOrdenDeCompra.Add(obeOrdenDeCompra)
                End If
            Loop Until sLine Is Nothing OrElse sLine.ToString.Equals("")
            objReader.Close()
            Me.dgExcelCabecera.AutoGenerateColumns = False
            Me.dgExcelCabecera.DataSource = olbeOrdenDeCompra
            btnCargarDetalle.Enabled = True
        End If
    End Sub

    Private Function BuscarCodigoPais(ByVal strNombrePais As String) As Decimal
        Return 0
    End Function

    'Private Sub btnAbrirCabecera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbrirCabecera.Click
    '    Try
    '        Dim i As Integer
    '        Dim j As Integer
    '        Dim Columnas As Integer = 12
    '        ' -- Verifica si existe el archivo   
    '        If Len(Dir(Me.TxtRutaXlsCabecera.Text)) = 0 Then
    '            MsgBox("No se ha encontrado el archivo: " & Me.TxtRutaXlsCabecera.Text, vbCritical)
    '            Exit Sub
    '        End If
    '        '===================================================================
    '        ' -- RUTINA EXCEL   Instancia Excel  
    '        If obj_Excel Is Nothing Then
    '            obj_Excel = CreateObject("Excel.Application")
    '            obj_Workbook = obj_Excel.Workbooks.Open(Me.TxtRutaXlsCabecera.Text)
    '        End If
    '        ' -- Referencia la Hoja, por defecto la hoja activa  
    '        If obj_SheetName = vbNullString Then
    '            obj_Worksheet = obj_Workbook.ActiveSheet
    '        Else
    '            obj_Worksheet = obj_Workbook.Sheets(obj_SheetName)
    '        End If
    '        '==================Crear lista de Objeto de tipo OC beOrdenDeCOMPRA
    '        Dim olbeOrdenDeCompra As New List(Of beOrdenCompra)
    '        Dim olbeOrdenDeCompraMatch As List(Of beOrdenCompra)
    '        Dim obeOrdenDeCompra As New beOrdenCompra
    '        Dim strReferencia As String = ""
    '        For i = 1 To NmCount.Value - 2
    '            obeOrdenDeCompra = New beOrdenCompra
    '            olbeOrdenDeCompraMatch = New List(Of beOrdenCompra)
    '            strOC = obj_Worksheet.Cells(i + 1, 5).Value
    '            strRef = ""

    '            'olbeOrdenDeCompraMatch = olbeOrdenDeCompra.FindAll(MatchOrdenDeCompra)
    '            'If olbeOrdenDeCompraMatch.Count > 0 Then
    '            '    strNrRef = obj_Worksheet.Cells(i + 1, 1).Value.ToString.Trim
    '            '    If olbeOrdenDeCompraMatch.FindAll(MatchOcBusquedaRef).Count = 0 Then
    '            '        obeOrdenDeCompra.PSNREFCL = strRef & "," & obj_Worksheet.Cells(i + 1, 1).Value
    '            '    Else
    '            '        obeOrdenDeCompra.PSNREFCL = strRef
    '            '    End If
    '            'Else
    '            '    obeOrdenDeCompra.PSNREFCL = obj_Worksheet.Cells(i + 1, 1).Value
    '            'End If
    '            obeOrdenDeCompra.PSNREFCL = obj_Worksheet.Cells(i + 1, 1).Value.ToString.Trim
    '            obeOrdenDeCompra.PSTRFRN = obj_Worksheet.Cells(i + 1, 3).Value
    '            obeOrdenDeCompra.PSTCMAEM = obj_Worksheet.Cells(i + 1, 12).Value 'Cod. De Ruta De Aprobación
    '            obeOrdenDeCompra.PSNORCML = obj_Worksheet.Cells(i + 1, 5).Value 'OC
    '            obeOrdenDeCompra.PNFROCMP = obj_Worksheet.Cells(i + 1, 9).Value 'fecha de entrega
    '            obeOrdenDeCompra.PSREFDO1 = obj_Worksheet.Cells(i + 1, 10).Value 'cuenta contable
    '            obeOrdenDeCompra.PSCPRPCL = obj_Worksheet.Cells(i + 1, 7).Value 'codigo de proveedor de cliente 
    '            obeOrdenDeCompra.PSTNOMCOM = obj_Worksheet.Cells(i + 1, 8).Value '

    '            If obj_Worksheet.Cells(i + 1, 4).Value.ToString.Trim = "300" OrElse obj_Worksheet.Cells(i + 1, 4).Value.ToString.Trim = "324" OrElse obj_Worksheet.Cells(i + 1, 4).Value.ToString.Trim = "331" OrElse _
    '           obj_Worksheet.Cells(i + 1, 4).Value.ToString.Trim = "400" OrElse obj_Worksheet.Cells(i + 1, 4).Value.ToString.Trim = "427" Then
    '                obeOrdenDeCompra.PSLOTE = obj_Worksheet.Cells(i + 1, 10).Value.ToString.Trim.Substring(0, 4)
    '            Else
    '                obeOrdenDeCompra.PSLOTE = obj_Worksheet.Cells(i + 1, 4).Value
    '            End If

    '            Dim obrGeneral As New brGeneral
    '            Dim obeGeneral As New beGeneral
    '            Dim olbeGeneral As New List(Of beGeneral)
    '            With obeGeneral
    '                .PSNLTECL = obeOrdenDeCompra.PSLOTE
    '            End With
    '            olbeGeneral = obrGeneral.LotesDeEntregaXCliente(obeGeneral)
    '            If olbeGeneral.Count > 0 Then
    '                obeOrdenDeCompra.PNCCLNT = olbeGeneral.Item(0).PNCCLNT
    '            Else
    '                obeOrdenDeCompra.PNCCLNT = 0
    '            End If
    '            If obeOrdenDeCompra.PNCCLNT = 0 Then
    '                HelpClass.MsgBox("No se puede identificar a que cliente pertenece el código de Lote : " & obeOrdenDeCompra.PSLOTE, MessageBoxIcon.Information)
    '                Exit Sub
    '            End If
    '            obeOrdenDeCompra.PNCPRVCL = BuscarProveedor(obj_Worksheet.Cells(i + 1, 7).Value, obeOrdenDeCompra.PSNORCML, obeOrdenDeCompra.PNCCLNT)
    '            If obeOrdenDeCompra.PNCPRVCL = 0 Then
    '                Dim obeProveedor As New beProveedor
    '                With obeProveedor
    '                    .PNCCLNT = obeOrdenDeCompra.PNCCLNT
    '                    .PSCPRPCL = obj_Worksheet.Cells(i + 1, 7).Value 'Cod proveedor cliente
    '                    If ("" & obj_Worksheet.Cells(i + 1, 13).Value & "").ToString.Trim.Length > 30 Then
    '                        .PSTPRVCL = ("" & obj_Worksheet.Cells(i + 1, 13).Value & "").ToString.Trim.Substring(0, 30) 'Descripcion del proveedor
    '                        .PSTPRCL1 = ("" & obj_Worksheet.Cells(i + 1, 13).Value & "").ToString.Trim.Substring(30, ("" & obj_Worksheet.Cells(i + 1, 13).Value & "").ToString.Trim.Length - 30) 'Descripcion extedida del proveedor
    '                    Else
    '                        .PSTPRVCL = ("" & obj_Worksheet.Cells(i + 1, 13).Value & "").ToString.Trim
    '                    End If
    '                    .PNNRUCPR = obj_Worksheet.Cells(i + 1, 14).Value 'Ruc del proveedor
    '                    .PSCUSCRT = strUsuario
    '                End With
    '                obeOrdenDeCompra.PNCPRVCL = GrabarProveedoCliente(obeProveedor)
    '                If obeOrdenDeCompra.PNCPRVCL = 0 Then
    '                    Exit Sub
    '                End If
    '            End If
    '            obeOrdenDeCompra.PNFORCML = HelpClass.CFecha_a_Numero8Digitos(DateTime.Now)
    '            obeOrdenDeCompra.PNFSOLIC = HelpClass.CFecha_a_Numero8Digitos(DateTime.Now)


    '            olbeOrdenDeCompra.Add(obeOrdenDeCompra)
    '        Next
    '        Dim oucOrdena As UCOrdena(Of beOrdenCompra)
    '        oucOrdena = New UCOrdena(Of beOrdenCompra)("PSNORCML", UCOrdena(Of beOrdenCompra).TipoOrdenacion.Ascendente)
    '        olbeOrdenDeCompra.Sort(oucOrdena)
    '        For intRow As Integer = olbeOrdenDeCompra.Count - 1 To 1 Step -1
    '            If olbeOrdenDeCompra.Item(intRow).PSNORCML = olbeOrdenDeCompra.Item(intRow - 1).PSNORCML Then
    '                olbeOrdenDeCompra.RemoveAt(intRow)
    '            End If
    '        Next
    '        Me.dgExcelCabecera.AutoGenerateColumns = False
    '        Me.dgExcelCabecera.DataSource = olbeOrdenDeCompra
    '        Call Limpiar()
    '        If (Me.dgExcelCabecera.Rows.Count > 0) Then
    '            btnExcelDetalle.Enabled = True
    '            brnAbrirDetalle.Enabled = True
    '        Else
    '            btnExcelDetalle.Enabled = False
    '            brnAbrirDetalle.Enabled = False
    '        End If
    '        brnAbrirDetalle.Enabled = True
    '        btnExcelDetalle.Enabled = True

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Call Limpiar()
    '    End Try

    'End Sub

#Region "Metodo"

#Region "Búsqueda de Item"
    Private strOC As String = ""
    Private strRef As String = ""
    Private strNrRef As String = ""
    Private dblNrItem As Decimal = 0

    Private MatchOrdenDeCompra As New Predicate(Of beOrdenCompra)(AddressOf BuscarOrdendeCompra)

    Public Function BuscarOrdendeCompra(ByVal pbeOrdenCompra As beOrdenCompra) As Boolean
        If (pbeOrdenCompra.PSNORCML.Trim.Equals(strOC.Trim)) Then
            strRef = pbeOrdenCompra.PSNREFCL
            Return True
        Else
            Return False
        End If
    End Function

    Private MatchOcBusquedaRef As New Predicate(Of beOrdenCompra)(AddressOf BuscarNrRef)

    Public Function BuscarNrRef(ByVal pbeOrdenCompra As beOrdenCompra) As Boolean
        If (pbeOrdenCompra.PSNREFCL.Trim.Equals(strNrRef)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private MatchBuscarItemRepetidos As New Predicate(Of beOrdenCompra)(AddressOf BuscarItemRepetidos)

    Public Function BuscarItemRepetidos(ByVal pbeOrdenCompra As beOrdenCompra) As Boolean
        If (pbeOrdenCompra.PSNORCML.Trim.Equals(strOC.Trim)) And pbeOrdenCompra.PNNRITOC = dblNrItem Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

    Private Sub Limpiar()
        obj_Workbook = Nothing
        obj_Excel = Nothing
        obj_Worksheet = Nothing
    End Sub

    ''' <summary>
    ''' Busca el proveedor de acuerdo al código del proveedor del cliente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BuscarProveedor(ByVal obeProveedor As beProveedor) As Decimal
        'se busca los proveedores del cliente plus
        Dim decCodProv As Decimal = 0
        Dim obrProveedor As New brProveedor
        decCodProv = obrProveedor.ObtenerCodigoProveedorPorCodCliente(obeProveedor)
        If decCodProv = 0 Then
            decCodProv = GrabarProveedoCliente(obeProveedor)
            If decCodProv = 0 Then
                Return 0
            End If
        End If
        Return decCodProv
    End Function

    ''' <summary>
    ''' 'Graba proveedor de cliente
    ''' </summary>
    ''' <param name="obeProveedor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GrabarProveedoCliente(ByVal obeProveedor As beProveedor) As Decimal
        'se busca los proveedores del cliente plus
        Dim decCodProv As Decimal = 0
        Dim obrProveedor As New brProveedor
        decCodProv = obrProveedor.GrabarProveedorDeCliente(obeProveedor)
        If decCodProv = 0 Then
            HelpClass.ErrorMsgBox()
            Return 0
        End If
        Return decCodProv
    End Function


#End Region


    Private Sub btnCargarDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargarDetalle.Click
        olbeDetOc.Clear()
        'No Seleccionar Multiples Archivos 
        Me.OpenFileDialog.Multiselect = False
        'EL DIRECTORIO INICIAL ES MIS DOCUMENTOS
        Me.OpenFileDialog.InitialDirectory = (System.Environment.GetFolderPath _
        (System.Environment.SpecialFolder.Personal))
        'FILTRO LAS EXTENSIONES QUE QUIERO MOSTRAR
        Me.OpenFileDialog.Filter = "Excel Files (*.txt )|*.txt;|All files (*.*)|*.*"
        If Me.OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then ' SI DIO CLICK EN ACEPTAR
            Me.TxtRutaDetalle.Text = Me.OpenFileDialog.FileName
            Dim objReader As New IO.StreamReader(Me.OpenFileDialog.FileName)
            Dim sLine As String = ""
            Dim obeOrdenDeCompra As New beOrdenCompra
            Do
                sLine = objReader.ReadLine()
                If Not sLine Is Nothing AndAlso Not sLine.ToString.Equals("") Then
                    obeOrdenDeCompra = New beOrdenCompra
                    'obeProveedor = New beProveedor
                    With obeOrdenDeCompra
                        Dim strOc As Object
                        strOc = sLine.Split(""",""")
                        .PNCCLNT = _dblCodCliente
                        .PSNORCML = strOc(1)
                        .PNNRITOC = strOc(3)
                        .PSTCITCL = HelpClass.CortarString(strOc(5), 20)
                        .PSTCITPR = HelpClass.CortarString(strOc(7), 20)
                        .PSTDITES = HelpClass.CortarString(strOc(9), 100)
                        .PNQCNTIT = strOc(11)
                        .PSTUNDIT = strOc(13)
                        .PNIVUNIT = strOc(15)
                        .PNIVTOIT = strOc(17)
                        .PNFMPDME = HelpClass.CDate_a_Numero8Digitos(strOc(23))
                    End With
                    olbeDetOc.Add(obeOrdenDeCompra)
                End If
            Loop Until sLine Is Nothing OrElse sLine.ToString.Equals("")
            objReader.Close()
            Me.dgExcelDetalle.AutoGenerateColumns = False
            Me.dgExcelCabecera.RefreshEdit()
            dgExcelCabecera_SelectionChanged(Nothing, Nothing)
            tsbGuardar.Enabled = True
            btnCargarObservaciones.Enabled = True
        End If
    End Sub

    'Private Sub brnAbrirDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brnAbrirDetalle.Click
    '    Try
    '        Dim n As Integer
    '        Dim i As Integer
    '        Dim j As Integer
    '        Dim Columnas As Integer = 12
    '        ' -- Verifica si existe el archivo   
    '        If Len(Dir(Me.TxtRutaXlsDetalle.Text)) = 0 Then
    '            MsgBox("No se ha encontrado el archivo: " & Me.TxtRutaXlsDetalle.Text, vbCritical)
    '            Exit Sub
    '        End If
    '        '===================================================================
    '        ' -- RUTINA EXCEL   Instancia Excel  
    '        If obj_Excel Is Nothing Then
    '            obj_Excel = CreateObject("Excel.Application")
    '            obj_Workbook = obj_Excel.Workbooks.Open(Me.TxtRutaXlsDetalle.Text)
    '        End If
    '        ' -- Referencia la Hoja, por defecto la hoja activa  
    '        If obj_SheetName = vbNullString Then
    '            obj_Worksheet = obj_Workbook.ActiveSheet
    '        Else
    '            obj_Worksheet = obj_Workbook.Sheets(obj_SheetName)
    '        End If
    '        olbeDetOc = New List(Of beOrdenCompra)
    '        Dim obeOrdenCompra As beOrdenCompra
    '        For i = 1 To NmCount1.Value - 2
    '            obeOrdenCompra = New beOrdenCompra
    '            With obeOrdenCompra
    '                '======================
    '                ' obeOrdenCompra.PNCCLNT = Decimal.Parse(_dblCodCliente)
    '                obeOrdenCompra.PSNORCML = obj_Worksheet.Cells(i + 1, 3).Value 'Oc
    '                If IsNumeric(obj_Worksheet.Cells(i + 1, 5).Value) Then 'DOC_NROLINEA'
    '                    obeOrdenCompra.PNNRITOC = obj_Worksheet.Cells(i + 1, 5).Value
    '                End If
    '                'obeOrdenCompra.PSTRFRN = obj_Worksheet.Cells(i + 1, 1).Value 'USUARIO QUE SOLICITANTE
    '                obeOrdenCompra.PSTCITCL = obj_Worksheet.Cells(i + 1, 7).Value 'DOC_CODIGOITEM'
    '                obeOrdenCompra.PSTDITES = ("" & obj_Worksheet.Cells(i + 1, 8).Value & "") 'DOC_DESCRIPCIONITEM'
    '                If IsNumeric(obj_Worksheet.Cells(i + 1, 9).Value) Then 'DOC_CANTIDADITEM'
    '                    obeOrdenCompra.PNQCNTIT = Decimal.Parse(obj_Worksheet.Cells(i + 1, 9).Value)
    '                End If
    '                obeOrdenCompra.PSTUNDIT = obj_Worksheet.Cells(i + 1, 10).Value 'DOC_CODUNIDMEDIDA'
    '                obeOrdenCompra.PSTDITIN = ""
    '                obeOrdenCompra.PNFMPDME = HelpClass.CFecha_a_Numero8Digitos(DateTime.Now)
    '                obeOrdenCompra.PSTLUGEN = ""
    '                obeOrdenCompra.PSTCTCST = ""
    '                obeOrdenCompra.PNIVUNIT = 0
    '                obeOrdenCompra.PNIVTOIT = 0
    '                obeOrdenCompra.PSTCITPR = ""
    '                obeOrdenCompra.PSCPTDAR = ""
    '                obeOrdenCompra.PNFMPIME = 0
    '                obeOrdenCompra.PSTLUGOR = ""
    '                obeOrdenCompra.PNQTOLMIN = 0
    '                obeOrdenCompra.PNQTOLMAX = 0
    '                olbeDetOc.Add(obeOrdenCompra)
    '                strOC = obeOrdenCompra.PSNORCML
    '                dblNrItem = obeOrdenCompra.PNNRITOC
    '                If olbeDetOc.FindAll(MatchBuscarItemRepetidos).Count > 1 Then
    '                    HelpClass.MsgBox("Error: La Orden" & obeOrdenCompra.PSNORCML & "Cuenta con uno o más Items repetidos", MessageBoxIcon.Information)
    '                    Exit Sub
    '                End If
    '                '===========================================
    '            End With
    '        Next
    '        Me.dgExcelDetalle.AutoGenerateColumns = False
    '        Me.dgExcelCabecera.RefreshEdit()
    '        dgExcelCabecera_SelectionChanged(Nothing, Nothing)
    '        Call Limpiar()
    '        tsbGuardar.Enabled = True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Call Limpiar()
    '    End Try
    'End Sub

    Private Sub dgExcelCabecera_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgExcelCabecera.SelectionChanged
        strOC = Me.dgExcelCabecera.CurrentRow.DataBoundItem.PSNORCML
        Me.dgExcelDetalle.AutoGenerateColumns = False
        Me.dgExcelDetalle.DataSource = olbeDetOc.FindAll(MatchOrdenDeCompra)
    End Sub

    Private Sub tsbGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbGuardar.Click
        If Me.dgExcelCabecera.Rows.Count > 0 Then GrabarOc()
    End Sub

    Private Sub GrabarOc()
        Try
            Dim obrOrdeDeCopra As New brOrdenDeCompra
            Dim olbeOcTem As New List(Of beOrdenCompra)
            For Each obeOc As beOrdenCompra In Me.dgExcelCabecera.DataSource
                obeOc.PSSESTRG = "A"
                obeOc.PSCUSCRT = strUsuario
                obeOc.PSCULUSA = strUsuario
                If obrOrdeDeCopra.InsertarOrdenDeCompra(obeOc) = 1 Then
                    olbeOcTem.Clear()
                    strOC = obeOc.PSNORCML
                    olbeOcTem = olbeDetOc.FindAll(MatchOrdenDeCompra)
                    For Each obeDetOc As beOrdenCompra In olbeOcTem
                        obeDetOc.PSSESTRG = "A"
                        obeDetOc.PNCCLNT = obeOc.PNCCLNT
                        obeDetOc.PSCUSCRT = strUsuario
                        obeDetOc.PSCULUSA = strUsuario
                        obeDetOc.PSTRFRN = obeOc.PSTRFRN
                        obeDetOc.PNFMPDME = obeOc.PNFROCMP
                        obeDetOc.PNFMPIME = obeOc.PNFROCMP
                        obeDetOc.PSTLUGEN = obeOc.PSTLUGEN
                        obeDetOc.PSTRFRNA = obeOc.PSTRFRNA
                        If obrOrdeDeCopra.InsertarDetalleOrdenDeCompra(obeDetOc) = 0 Then
                            HelpClass.ErrorMsgBox()
                            Exit Sub
                        End If
                    Next
                Else
                    HelpClass.ErrorMsgBox()
                    Exit Sub
                End If
            Next
            For Each obeObservacionOc As beOrdenCompra In olbeObservacionesDetOc
                obeObservacionOc.PSSESTRG = "A"
                obeObservacionOc.PSCUSCRT = strUsuario
                obeObservacionOc.PSCULUSA = strUsuario
                Dim intCant As Integer = Math.Ceiling(obeObservacionOc.PSTNOTAS.Trim.Length / 100)
                Dim strObservacionOc As String = ""
                strObservacionOc = obeObservacionOc.PSTNOTAS.Trim
                obrOrdeDeCopra.EliminarObservacionOrdenDeCompra(obeObservacionOc)
                For i As Integer = 0 To intCant - 1
                    obeObservacionOc.PNNCRRLT = i
                    If intCant = 1 Then
                        obeObservacionOc.PSTNOTAS = strObservacionOc
                    ElseIf (intCant - 1 = i) Then
                        obeObservacionOc.PSTNOTAS = strObservacionOc.Substring(i * 100, strObservacionOc.Length - (i) * 100)
                    Else
                        obeObservacionOc.PSTNOTAS = strObservacionOc.Substring(i * 100, 100).Trim
                    End If
                    If obrOrdeDeCopra.InsertarObservacionOrdenDeCompra(obeObservacionOc) = 0 Then
                        HelpClass.ErrorMsgBox()
                        Exit Sub
                    End If
                Next
            Next
            HelpClass.MsgBox("Registro Satisfactorio.")
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            HelpClass.ErrorMsgBox()
        End Try

    End Sub



    Private Sub bsaCerrarVentana_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bsaCerrarVentana.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub btnCargarObservaciones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCargarObservaciones.Click
        olbeObservacionesDetOc.Clear()
        'No Seleccionar Multiples Archivos 
        Me.OpenFileDialog.Multiselect = False
        'EL DIRECTORIO INICIAL ES MIS DOCUMENTOS
        Me.OpenFileDialog.InitialDirectory = (System.Environment.GetFolderPath _
        (System.Environment.SpecialFolder.Personal))
        'FILTRO LAS EXTENSIONES QUE QUIERO MOSTRAR
        Me.OpenFileDialog.Filter = "Excel Files (*.txt )|*.txt;|All files (*.*)|*.*"
        If Me.OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then ' SI DIO CLICK EN ACEPTAR
            Me.txtRutaObservaciones.Text = Me.OpenFileDialog.FileName
            Dim objReader As New IO.StreamReader(Me.OpenFileDialog.FileName)
            Dim sLine As String = ""
            Dim obeOrdenDeCompra As New beOrdenCompra
            Do
                sLine = objReader.ReadLine()
                If Not sLine Is Nothing AndAlso Not sLine.ToString.Equals("") Then
                    obeOrdenDeCompra = New beOrdenCompra
                    'obeProveedor = New beProveedor
                    With obeOrdenDeCompra
                        Dim strOc As Object
                        strOc = sLine.Split(""",""")
                        .PNCCLNT = _dblCodCliente
                        .PSNORCML = strOc(1).ToString.Substring(4, strOc(1).ToString.Length - 4).Replace("�", "")
                        .PSTNOTAS = strOc(3)
                    End With
                    olbeObservacionesDetOc.Add(obeOrdenDeCompra)
                End If
            Loop Until sLine Is Nothing OrElse sLine.ToString.Equals("")
            objReader.Close()
            tsbGuardar.Enabled = True
            btnCargarObservaciones.Enabled = True
        End If
    End Sub
End Class
