Imports Ransa.Controls.ServicioOperacion.clsRegionVenta_DAL

Public Class clsRegionVenta_BL
    Private oRegionDato As clsRegionVenta_DAL
    Private oDt As DataTable

    Sub New()
        oRegionDato = New clsRegionVenta_DAL
    End Sub

    Property Lista() As DataTable
        Get
            Return oDt
        End Get
        Set(ByVal value As DataTable)
            oDt = value
        End Set
    End Property

    Public Function Lista_Region_Venta(ByVal pdblCodCompania As String) As DataTable
        Dim objDr As DataRow
        Dim lintCont As Integer
        Dim objDt As New DataTable
        Dim objListaDr As DataRow()
        objListaDr = oDt.Select("CCMPN='" & pdblCodCompania.Trim & "' ")
        objDt = oDt.Copy
        objDt.Rows.Clear()

        objDr = objDt.NewRow
        objDr(0) = -1
        objDr(1) = "TODOS"
        objDt.Rows.Add(objDr)

        For lintCont = 0 To objListaDr.Length - 1
            objDr = objDt.NewRow
            objDr(0) = objListaDr(lintCont).Item(0)
            objDr(1) = objListaDr(lintCont).Item(1)
            objDt.Rows.Add(objDr)
        Next lintCont

        Return objDt
    End Function

    Public Sub Crea_Lista()
        oDt = oRegionDato.Lista_RegionVenta()
    End Sub
End Class