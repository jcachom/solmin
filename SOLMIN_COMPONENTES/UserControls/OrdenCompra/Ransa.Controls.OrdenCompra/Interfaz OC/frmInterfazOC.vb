Imports Ransa.DAO.OrdenCompra
Imports RANSA.NEGO
Imports RANSA.Utilitario
Imports Db2Manager.RansaData.iSeries.DataObjects
Imports Ransa.TypeDef.OrdenCompra.OrdenCompra
Imports Ransa.TypeDef.OrdenCompra.ItemOC
Imports Ransa.TypeDef
Imports System.IO
Imports Ransa.DATA

Public Class frmInterfazOC
#Region "Declaracion de variables"
    Private _dblCodCliente As String = "0"
    Public _strUser As String = ""
    Public _strHostName As String = ""
    Private strOC As String = ""
    Private NrItemOc As String = ""
    Private olbeDetOc As List(Of beOrdenCompra)
    Private olbeDetPedOc As List(Of beOrdenCompra)
    Private olbeOrdenDeCompraNotas As List(Of beOrdenCompra)
    Private MatchOrdenDeCompra As New Predicate(Of beOrdenCompra)(AddressOf BuscarOrdendeCompra)
    Private MatchOrdenDePedCompra As New Predicate(Of beOrdenCompra)(AddressOf BuscarOrdendePedCompra)
#End Region

    Public Property pCodCliente() As Decimal
        Get
            Return _dblCodCliente
        End Get
        Set(ByVal value As Decimal)
            _dblCodCliente = value
        End Set
    End Property


    Public Property pUsuario() As String
        Get
            Return _strUser
        End Get
        Set(ByVal value As String)
            _strUser = value
        End Set
    End Property

    Private _strTerminal As String
    Public Property pTerminal() As String
        Get
            Return _strTerminal
        End Get
        Set(ByVal value As String)
            _strTerminal = value
        End Set
    End Property

    Private Sub frmInterfazOC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obrInterfazOC As New brInterfazOC
        Me.dgDetalle.AutoGenerateColumns = False
        obrInterfazOC.CargaArchivo(_dblCodCliente)
        olbeDetOc = obrInterfazOC.DetalleOC
        olbeDetPedOc = obrInterfazOC.DetalleOCPed
        dgCabecera.AutoGenerateColumns = False
        Me.dgCabecera.DataSource = obrInterfazOC.ListaOC()
        If obrInterfazOC.ListaOC().Count > 0 Then
            Me.tsbGuardar.Enabled = True
        End If

    End Sub

#Region "Eventos de Control"

    Private Sub dgCabecera_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgCabecera.SelectionChanged
        strOC = Me.dgCabecera.CurrentRow.DataBoundItem.PSNORCML
        Me.dgDetalle.AutoGenerateColumns = False
        Me.dgDetalle.DataSource = olbeDetOc.FindAll(MatchOrdenDeCompra)

    End Sub



    Private Sub tsbGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbGuardar.Click
        GrabarOc()
    End Sub


#End Region

    Private Sub dgCabecera_DataBindingComplete(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgCabecera.DataBindingComplete
        'Dim obrOc As New brOrdenDeCompra
        'For intCont As Integer = 0 To Me.dgCabecera.RowCount - 1
        '    strOC = CType(Me.dgCabecera.Rows(intCont).DataBoundItem, beOrdenCompra).PSNORCML
        '    CType(Me.dgCabecera.Rows(intCont).DataBoundItem, beOrdenCompra).PageSize = 1000
        '    CType(Me.dgCabecera.Rows(intCont).DataBoundItem, beOrdenCompra).PageNumber = 1
        '    If obrOc.ObtenerOrdenDeCompra(Me.dgCabecera.Rows(intCont).DataBoundItem).Count > 0 And obrOc.ListarDetalleOrdenDeCompra(Me.dgCabecera.Rows(intCont).DataBoundItem).Count = olbeDetOc.FindAll(MatchOrdenDeCompra).Count Then
        '        Me.dgCabecera.Rows(intCont).Cells("IMAGES").Value = My.Resources.button_ok1
        '    End If
        'Next
    End Sub

    Private Sub tsbCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbCerrar.Click
        Me.Close()
    End Sub



#Region "Funciones y procedimientos"


    Public Function BuscarOrdendeCompra(ByVal pbeOrdenCompra As beOrdenCompra) As Boolean
        If (pbeOrdenCompra.PSNORCML.Trim.Equals(strOC.Trim)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function BuscarOrdendePedCompra(ByVal pbeOrdenCompra As beOrdenCompra) As Boolean
        If ((pbeOrdenCompra.PSNORCML.Trim.Equals(strOC.Trim)) And (pbeOrdenCompra.PNNRITOC = NrItemOc)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub GrabarOc()
        Try
            Dim nRetorno As Integer = 0
            Dim obrOrdeDeCopra As New brOrdenDeCompra
            Dim obrOrdeDePedCopra As New brOrdenDeCompra
            Dim obeObservacionOc As New beOrdenCompra
            Dim olbeOcTem As New List(Of beOrdenCompra)
            Dim olbeOcPedTem As New List(Of beOrdenCompra)
            For Each obeOc As beOrdenCompra In Me.dgCabecera.DataSource
                obeOc.PSSESTRG = "A"
                obeOc.PSCUSCRT = _strUser
                obeOc.PSCULUSA = _strUser
                obeOc.PSNTRMNL = _strTerminal
                obeOc.PSTDSCML = ValidarCaracter.validaCaracter("" & obeOc.PSTDSCML & "", obeOc.PSERROR)

                If obrOrdeDeCopra.InsertarOrdenDeCompra(obeOc) = 1 Then
                    olbeOcTem.Clear()
                    strOC = obeOc.PSNORCML
                    olbeOcTem = olbeDetOc.FindAll(MatchOrdenDeCompra)
                    If (olbeOcTem.Count > 0) Then
                        For Each obeDetOc As beOrdenCompra In olbeOcTem
                            obeDetOc.PSCUSCRT = _strUser
                            obeDetOc.PSCULUSA = _strUser
                            obeDetOc.PSNTRMNL = _strTerminal

                            If obrOrdeDeCopra.InsertarDetalleOrdenDeCompra(obeDetOc) = 0 Then
                                HelpClass.ErrorMsgBox()
                                Exit Sub
                            Else
                                'Busca si cuenta con detalle el item
                                strOC = obeDetOc.PSNORCML
                                NrItemOc = obeDetOc.PNNRITOC
                                olbeOcPedTem = olbeDetPedOc.FindAll(MatchOrdenDePedCompra)
                                If olbeOcPedTem.Count > 0 Then
                                    For Each obeDetPedOc As beOrdenCompra In olbeOcPedTem
                                        obeDetPedOc.PSCUSCRT = _strUser
                                        obeDetPedOc.PSCULUSA = _strUser
                                        obeDetPedOc.PSNTRMCR = _strTerminal
                                        obeDetPedOc.PSNTRMNL = _strTerminal
                                        If obrOrdeDePedCopra.InsertarDetalleOrdenDePedCompra(obeDetPedOc) = 0 Then
                                            HelpClass.ErrorMsgBox()
                                            Exit Sub
                                        Else
                                            Dim objImportarOC As New OrdenCompra_DAL
                                            Dim oParametro As New Hashtable
                                            oParametro.Add("CodCliente", obeOc.PNCCLNT)
                                            oParametro.Add("OC", obeDetOc.NrOCCliente)
                                            oParametro.Add("CodItem", obeDetOc.NrItemCliente)
                                            oParametro.Add("TipDoc", obeOc.PSTPOOCM)
                                            oParametro.Add("norden", obeDetPedOc.PSNRFRPD)
                                            oParametro.Add("Usuario", obeDetPedOc.PSCULUSA)

                                            If objImportarOC.fintActualizarEstado(oParametro) = -1 Then
                                                HelpClass.ErrorMsgBox()
                                                Exit Sub
                                            End If
                                        End If
                                    Next
                                Else
                                    Dim objImportarOC As New OrdenCompra_DAL
                                    Dim oParametro As New Hashtable
                                    oParametro.Add("CodCliente", obeOc.PNCCLNT)
                                    oParametro.Add("OC", obeDetOc.NrOCCliente)
                                    oParametro.Add("CodItem", obeDetOc.NrItemCliente)
                                    oParametro.Add("TipDoc", obeOc.PSTPOOCM)
                                    oParametro.Add("norden", "")
                                    oParametro.Add("Usuario", obeDetOc.PSCULUSA)
                                    If objImportarOC.fintActualizarEstado(oParametro) = -1 Then
                                        HelpClass.ErrorMsgBox()
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next
                    End If

                Else
                    HelpClass.ErrorMsgBox()
                    Exit Sub
                End If
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
End Class


