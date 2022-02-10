Imports IBM.Data.DB2.iSeries
Imports CrystalDecisions.CrystalReports.Engine
Imports Db2Manager.RansaData.iSeries.DataObjects
Imports RANSA.NEGO.slnSOLMIN_SDS.DespachoSDSW


Namespace slnSOLMIN_SDS.DespachoSDSW.Reportes
    Public Class clsFiltros_ReporteGuiaDespacho
#Region " Atributos "
        Private intCCLNT As Int64 = 0
        Private strTCMPCL As String = ""
        Private strEmpresa As String = ""
        Private intNGUIRN As Int64 = 0
#End Region
#Region " Propiedades "
        Public Property pintCodigoCliente_CCLNT() As Int64
            Get
                Return intCCLNT
            End Get
            Set(ByVal value As Int64)
                intCCLNT = value
            End Set
        End Property

        Public Property pstrRazonSocialCliente_TCMPCL() As String
            Get
                Return strTCMPCL
            End Get
            Set(ByVal value As String)
                strTCMPCL = value
            End Set
        End Property

        Public Property pstrRazonSocialEmpresa() As String
            Get
                Return strEmpresa
            End Get
            Set(ByVal value As String)
                strEmpresa = value
            End Set
        End Property

        Public Property pintNroGuiaRansa_NGUIRN() As Int64
            Get
                Return intNGUIRN
            End Get
            Set(ByVal value As Int64)
                intNGUIRN = value
            End Set
        End Property
#End Region
    End Class

    Public Class clsFiltros_ReporteGuiaRemision
#Region " Atributos "
        Private intCCLNT As Int64 = 0
        Private intNGUIRM As Int64 = 0
#End Region
#Region " Propiedades "
        Public Property pintCodigoCliente_CCLNT() As Int64
            Get
                Return intCCLNT
            End Get
            Set(ByVal value As Int64)
                intCCLNT = value
            End Set
        End Property

        Public Property pintNroGuiaRemision_NGUIRM() As Int64
            Get
                Return intNGUIRM
            End Get
            Set(ByVal value As Int64)
                intNGUIRM = value
            End Set
        End Property
#End Region
    End Class

    Public Class clsPARAM_GuiaRemisionDS
#Region " Atributos "
        Private intCCLNT As Int64 = 0
        Private intNGUIRN As Int64 = 0
        Private intNGUIRM As Int64 = 0
        Private dteFGUIRM As Date
        Private strSMTGRM As String = ""
        Private strTOBORM As String = ""
        Private intCPLNOR As Integer = 0
        Private intCPLNCL As Integer = 0
        Private intCPRVCL As Integer = 0
        Private intCPLCLP As Integer = 0
        Private intCTRSPT As Integer = 0
        Private strNPLCCM As String = ""
        Private strNPLACP As String = ""
        Private strNBRVCH As String = ""
        Private strTOBSRM As String = ""
        Private intNDCMRF As Integer = 0
        Private intTDCFCC As Int32 = 0
        Private intNDCFCC As Int64 = 0
        Private dteFDCFCC As Date
#End Region
#Region " Propiedades "
        Public Property pintCodigoCliente_CCLNT() As Int64
            Get
                Return intCCLNT
            End Get
            Set(ByVal value As Int64)
                intCCLNT = value
            End Set
        End Property

        Public Property pintNroGuiaRansa_NGUIRN() As Int64
            Get
                Return intNGUIRN
            End Get
            Set(ByVal value As Int64)
                intNGUIRN = value
            End Set
        End Property

        Public Property pintNroGuiaRemision_NGUIRM() As Int64
            Get
                Return intNGUIRM
            End Get
            Set(ByVal value As Int64)
                intNGUIRM = value
            End Set
        End Property

        Public Property pdteFechaEmision_FGUIRM() As Date
            Get
                Return dteFGUIRM
            End Get
            Set(ByVal value As Date)
                dteFGUIRM = value
            End Set
        End Property

        Public ReadOnly Property pstrFechaEmision_FFec_FGUIRM() As String
            Get
                Dim strResultado As String = ""
                If dteFGUIRM.Year > 1 Then strResultado = dteFGUIRM.ToString("dd/MM/yyyy")
                Return strResultado
            End Get
        End Property

        Public ReadOnly Property pstrFechaEmision_FNum_FGUIRM() As String
            Get
                Dim strResultado As String = ""
                If dteFGUIRM.Year > 1 Then strResultado = dteFGUIRM.ToString("yyyyMMdd")
                Return strResultado
            End Get
        End Property

        Public ReadOnly Property pintFechaEmision_FGUIRM() As Integer
            Get
                Dim intResultado As Integer = 0
                If dteFGUIRM.Year > 1 Then Integer.TryParse(dteFGUIRM.ToString("yyyyMMdd"), intResultado)
                Return intResultado
            End Get
        End Property

        Public Property pstrMotivoTraslado_SMTGRM() As String
            Get
                pstrMotivoTraslado_SMTGRM = strSMTGRM
            End Get
            Set(ByVal value As String)
                strSMTGRM = value
            End Set
        End Property

        Public Property pstrObservacionesOtroMotivoTraslado_TOBORM() As String
            Get
                pstrObservacionesOtroMotivoTraslado_TOBORM = strTOBORM
            End Get
            Set(ByVal value As String)
                strTOBORM = value
            End Set
        End Property

        Public Property pintPlantaOrigen_CPLNOR() As Integer
            Get
                pintPlantaOrigen_CPLNOR = intCPLNOR
            End Get
            Set(ByVal value As Integer)
                intCPLNOR = value
            End Set
        End Property

        Public Property pintPlantaDestino_CPLNCL() As Integer
            Get
                pintPlantaDestino_CPLNCL = intCPLNCL
            End Get
            Set(ByVal value As Integer)
                intCPLNCL = value
            End Set
        End Property

        Public Property pintClienteTercero_CPRVCL() As Integer
            Get
                pintClienteTercero_CPRVCL = intCPRVCL
            End Get
            Set(ByVal value As Integer)
                intCPRVCL = value
            End Set
        End Property

        Public Property pintPlantaClienteTercero_CPLCLP() As Integer
            Get
                pintPlantaClienteTercero_CPLCLP = intCPLCLP
            End Get
            Set(ByVal value As Integer)
                intCPLCLP = value
            End Set
        End Property

        Public Property pintEmpresaTransporte_CTRSPT() As Integer
            Get
                pintEmpresaTransporte_CTRSPT = intCTRSPT
            End Get
            Set(ByVal value As Integer)
                intCTRSPT = value
            End Set
        End Property

        Public Property pintEmpresaTransporte2doTramo_NDCMRF() As Integer
            Get
                pintEmpresaTransporte2doTramo_NDCMRF = intNDCMRF
            End Get
            Set(ByVal value As Integer)
                intNDCMRF = value
            End Set
        End Property

        Public Property pstrPlacaUnidad_NPLCCM() As String
            Get
                pstrPlacaUnidad_NPLCCM = strNPLCCM
            End Get
            Set(ByVal value As String)
                strNPLCCM = value
            End Set
        End Property

        Public Property pstrNroBrevete_NBRVCH() As String
            Get
                pstrNroBrevete_NBRVCH = strNBRVCH
            End Get
            Set(ByVal value As String)
                strNBRVCH = value
            End Set
        End Property

        Public Property pstrObservacionesGuiaRemision_TOBSRM() As String
            Get
                pstrObservacionesGuiaRemision_TOBSRM = strTOBSRM
            End Get
            Set(ByVal value As String)
                strTOBSRM = value
            End Set
        End Property

        Public Property pintTipoDocumento_TDCFCC() As Int32
            Get
                Return intTDCFCC
            End Get
            Set(ByVal value As Int32)
                intTDCFCC = value
            End Set
        End Property

        Public Property pintNroDocumento_NDCFCC() As Int64
            Get
                Return intNDCFCC
            End Get
            Set(ByVal value As Int64)
                intNDCFCC = value
            End Set
        End Property

        Public Property pdteFechaEmisionDocumento_FDCFCC() As Date
            Get
                Return dteFDCFCC
            End Get
            Set(ByVal value As Date)
                dteFDCFCC = value
            End Set
        End Property

        Public ReadOnly Property pstrFechaEmisionDocumento_FFec_FDCFCC() As String
            Get
                Dim strResultado As String = ""
                If dteFDCFCC.Year > 1 Then strResultado = dteFDCFCC.ToString("dd/MM/yyyy")
                Return strResultado
            End Get
        End Property

        Public ReadOnly Property pstrFechaEmisionDocumento_FNum_FDCFCC() As String
            Get
                Dim strResultado As String = ""
                If dteFDCFCC.Year > 1 Then strResultado = dteFDCFCC.ToString("yyyyMMdd")
                Return strResultado
            End Get
        End Property

        Public ReadOnly Property pintFechaEmisionDocumento_FDCFCC() As Integer
            Get
                Dim intResultado As Integer = 0
                If dteFDCFCC.Year > 1 Then Integer.TryParse(dteFDCFCC.ToString("yyyyMMdd"), intResultado)
                Return intResultado
            End Get
        End Property
#End Region
    End Class
End Namespace

Namespace slnSOLMIN_SDS.DespachoSDSW.Reportes.Generador
    Public Class clsReportesDespacho
        Inherits clsBasicClassSDSW
#Region " Atributos "
        Private strUsuarioSistema As String = ""
#End Region
#Region " Propiedades "

#End Region
#Region " Funciones y Procedimientos "
        ' Funci�n para obtener los Parametros de Impresi�n de la Guia de Remision
        Private Function fobjParamatrosGuiaRemision(ByVal intCliente As Int64, ByRef strMensajeError As String) As clsParamImprGuiaRemision
            Dim dstParametrosGenerales As DataSet
            Dim objParametros As clsParamImprGuiaRemision = New clsParamImprGuiaRemision
            Dim rwListaBaseDatos As DataRow()
            Try
                dstParametrosGenerales = New DataSet()
                dstParametrosGenerales.ReadXml("Servidores.xml")
                rwListaBaseDatos = dstParametrosGenerales.Tables("ParamGuiaRemisionDS").Select("Cliente='" & intCliente.ToString & "'")
                If rwListaBaseDatos.Length = 0 Then
                    rwListaBaseDatos = dstParametrosGenerales.Tables("ParamGuiaRemisionDS").Select("Cliente='999999'")
                End If
                With objParametros
                    .pintNroLineasPorGuiaRemision = rwListaBaseDatos(0).Item("NroLineaPorGuia")
                    .pstrModeloGuiaRemision = rwListaBaseDatos(0).Item("ModReporte")
                End With
            Catch ex As Exception
                strMensajeError = ex.Message
            End Try
            Return objParametros
        End Function

        Private Function fdstGenerarGuiaRemision(ByVal objParam As clsPARAM_GuiaRemisionDS, ByRef intNroLineas As Int32, _
                                                 ByRef strMensajeError As String) As DataSet
            Dim objData As New DataSet
            Dim objSqlManager As SqlManager = New SqlManager
            Dim objParametros As Parameter = New Parameter
            objSqlManager.TransactionController(TransactionType.Automatic)

            With objParam
                ' Ingresamos los parametros del Procedure
                objParametros.Add("IN_CCLNT", .pintCodigoCliente_CCLNT)
                objParametros.Add("IN_NGUIRN", .pintNroGuiaRansa_NGUIRN)
                objParametros.Add("IN_NGUIRM", .pintNroGuiaRemision_NGUIRM)
                objParametros.Add("IN_FGUIRM", .pstrFechaEmision_FNum_FGUIRM)
                objParametros.Add("IN_SMTGRM", .pstrMotivoTraslado_SMTGRM)
                objParametros.Add("IN_TOBORM", .pstrObservacionesOtroMotivoTraslado_TOBORM)
                objParametros.Add("IN_CPLNOR", .pintPlantaOrigen_CPLNOR)
                objParametros.Add("IN_CPLNCL", .pintPlantaDestino_CPLNCL)
                objParametros.Add("IN_CPRVCL", .pintClienteTercero_CPRVCL)
                objParametros.Add("IN_CPLCLP", .pintPlantaClienteTercero_CPLCLP)
                objParametros.Add("IN_CTRSPT", .pintEmpresaTransporte_CTRSPT)
                objParametros.Add("IN_NDCMRF", .pintEmpresaTransporte2doTramo_NDCMRF)
                objParametros.Add("IN_NPLCCM", .pstrPlacaUnidad_NPLCCM)
                objParametros.Add("IN_NBRVCH", .pstrNroBrevete_NBRVCH)
                objParametros.Add("IN_TOBSRM", .pstrObservacionesGuiaRemision_TOBSRM)
                objParametros.Add("IN_TDCFCC", .pintTipoDocumento_TDCFCC)
                objParametros.Add("IN_NDCFCC", .pintNroDocumento_NDCFCC)
                objParametros.Add("IN_FDCFCC", .pstrFechaEmisionDocumento_FNum_FDCFCC)
                objParametros.Add("IN_NLINGR", intNroLineas)
                objParametros.Add("IN_USUARI", strUsuarioSistema)

                If strMensajeError <> "" Then
                    Return objData
                    Exit Function
                End If
            End With
            Try
                Dim cnx As iDB2Connection = objSqlManager.Conectar(objSqlManager.Conectar())
                Dim cmdAdaptador As iDB2DataAdapter
                Dim cmd As New iDB2Command

                cmd.Connection = cnx
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "SP_SOLMIN_SA_SDS_GUIA_REMISION_GENERAR"

                Dim i As Integer
                Dim SqlParametro As iDB2Parameter

                If Not (objParametros Is Nothing) Then
                    For i = 1 To objParametros.Count
                        SqlParametro = CType(objParametros.Item(i), iDB2Parameter)
                        cmd.Parameters.Add(SqlParametro.ParameterName, SqlParametro.Value)
                    Next
                End If

                cnx.Open()
                cmdAdaptador = New iDB2DataAdapter(cmd)
                cmdAdaptador.Fill(objData, "dtGuiaRemision")
                cnx.Close()
                cmd = Nothing
                cnx = Nothing
                objSqlManager = Nothing
                objParametros = Nothing
            Catch ex As Exception
                Throw New Exception(ex.ToString())
            End Try
            Return objData
        End Function

        Private Function fdstDataGuiaRemision(ByVal intCliente As Int64, ByVal intNroGuiaRemision As Int64, ByRef strMensajeError As String) As DataSet
            'Dim objData As New EstructuraDespachos
            Dim objData As New DataSet
            Dim objSqlManager As SqlManager = New SqlManager
            Dim objParametros As Parameter = New Parameter
            objSqlManager.TransactionController(TransactionType.Automatic)

            ' Ingresamos los parametros del Procedure
            objParametros.Add("IN_CCLNT", intCliente)
            objParametros.Add("IN_NGUIRM", intNroGuiaRemision)

            If strMensajeError <> "" Then
                Return objData
                Exit Function
            End If
            Try
                Dim cnx As iDB2Connection = objSqlManager.Conectar(objSqlManager.Conectar())
                Dim cmdAdaptador As iDB2DataAdapter
                Dim cmd As New iDB2Command

                cmd.Connection = cnx
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "SP_SOLMIN_SA_SDS_GUIA_REMISION_IMPRIMIR"

                Dim i As Integer
                Dim SqlParametro As iDB2Parameter

                If Not (objParametros Is Nothing) Then
                    For i = 1 To objParametros.Count
                        SqlParametro = CType(objParametros.Item(i), iDB2Parameter)
                        cmd.Parameters.Add(SqlParametro.ParameterName, SqlParametro.Value)
                    Next
                End If

                cnx.Open()
                cmdAdaptador = New iDB2DataAdapter(cmd)
                cmdAdaptador.Fill(objData, "dtGuiaRemision")
                cnx.Close()
                cmd = Nothing
                cnx = Nothing
                objSqlManager = Nothing
                objParametros = Nothing
            Catch ex As Exception
                Throw New Exception(ex.ToString())
            End Try
            Return objData
        End Function

#End Region
#Region " M�todos "
        Sub New(ByVal UsuarioSistema As String)
            strUsuarioSistema = UsuarioSistema
        End Sub

        '-------------------------------------------
        '-- Imprimir Reporte de Guia de Despacho --
        '-------------------------------------------
        Public Function frptObtenerGuiaDespacho(ByVal objFiltro As clsFiltros_ReporteGuiaDespacho, ByRef strMensajeError As String) As ReportDocument
            Dim rdocGuiaRemision As ReportDocument = Nothing
            Dim dtResultado As DataTable = Nothing
            strMensajeError = ""
            dtResultado = fdtSDSWResultadoConsulta(strMensajeError, "SDS_RECE_GRCRNS_01", objFiltro.pintNroGuiaRansa_NGUIRN)
            dtResultado.TableName = "dtInformacionGuiaDespacho"
            If strMensajeError = "" Then
                rdocGuiaRemision = New rptGuiaDespacho
                rdocGuiaRemision.SetDataSource(dtResultado)
                rdocGuiaRemision.Refresh()
                rdocGuiaRemision.SetParameterValue("pCliente", objFiltro.pintCodigoCliente_CCLNT)
                rdocGuiaRemision.SetParameterValue("pRazonSocialCliente", objFiltro.pstrRazonSocialCliente_TCMPCL)
                rdocGuiaRemision.SetParameterValue("pUsuario", strUsuarioSistema)
                rdocGuiaRemision.SetParameterValue("pNroGuiaRansa", objFiltro.pintNroGuiaRansa_NGUIRN)
                rdocGuiaRemision.SetParameterValue("pEmpresa", objFiltro.pstrRazonSocialEmpresa)
            End If
            Return rdocGuiaRemision
        End Function

        '------------------------------------------
        '-- Imprimir Reporte de Guia de Remisi�n --
        '------------------------------------------
        Public Function frptGenerarGuiaRemision(ByVal objParam As clsPARAM_GuiaRemisionDS, ByRef strMensajeError As String) As ReportDocument
            Dim objParametro As clsParamImprGuiaRemision
            Dim rdocGuiaRemision As ReportDocument = Nothing

            objParametro = fobjParamatrosGuiaRemision(objParam.pintCodigoCliente_CCLNT, strMensajeError)
            If strMensajeError = "" Then
                Select Case objParametro.pstrModeloGuiaRemision
                    Case "M1"
                        rdocGuiaRemision = New rptGuiaRemisionSDS_M01
                    Case "M2"
                        rdocGuiaRemision = New rptGuiaRemisionSDS_M02
                    Case Else
                        rdocGuiaRemision = New rptGuiaRemisionSDS_M01
                End Select
                rdocGuiaRemision.SetDataSource(fdstGenerarGuiaRemision(objParam, objParametro.pintNroLineasPorGuiaRemision, strMensajeError))
                rdocGuiaRemision.Refresh()
            End If
            Return rdocGuiaRemision
        End Function

        Public Function frptObtenerGuiaRemision(ByVal objFiltro As clsFiltros_ReporteGuiaRemision, ByRef strMensajeError As String) As ReportDocument
            Dim objParametro As clsParamImprGuiaRemision
            Dim rdocGuiaRemision As ReportDocument = Nothing

            objParametro = fobjParamatrosGuiaRemision(objFiltro.pintCodigoCliente_CCLNT, strMensajeError)
            If strMensajeError = "" Then
                Select Case objParametro.pstrModeloGuiaRemision
                    Case "M1"
                        rdocGuiaRemision = New rptGuiaRemisionSDS_M01
                    Case "M2"
                        rdocGuiaRemision = New rptGuiaRemisionSDS_M02
                    Case Else
                        rdocGuiaRemision = New rptGuiaRemisionSDS_M01
                End Select
                rdocGuiaRemision.SetDataSource(fdstDataGuiaRemision(objFiltro.pintCodigoCliente_CCLNT, objFiltro.pintNroGuiaRemision_NGUIRM, strMensajeError))
                rdocGuiaRemision.Refresh()
            End If
            Return rdocGuiaRemision
        End Function
#End Region
    End Class
End Namespace