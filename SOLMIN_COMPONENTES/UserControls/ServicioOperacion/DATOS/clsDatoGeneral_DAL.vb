Imports Db2Manager.RansaData.iSeries.DataObjects
 

Public Class clsDatoGeneral_DAL
    Private objSql As New SqlManager

    Public Function Lista_Tipo_Dato_General(ByVal CCMPN As String, ByVal CODVAR As String) As List(Of TipoDatoGeneral)
        Dim Lista As New List(Of TipoDatoGeneral)
        Dim tb As New DataTable
        Dim tipoDatoGeneral As TipoDatoGeneral
        Dim objParam As New Parameter

        objParam.Add("PSCODVAR", CODVAR)
        tb = objSql.ExecuteDataTable("SP_SOLMIN_ST_LISTAR_TIPO_DATO_GENERAL_X_CODIGO", objParam)
        For Each Item As DataRow In tb.Rows
            tipoDatoGeneral = New TipoDatoGeneral
            tipoDatoGeneral.CODIGO_TIPO = Item("CODIGO_TIPO").ToString.Trim
            tipoDatoGeneral.DESC_TIPO = Item("DESC_TIPO").ToString.Trim
            Lista.Add(tipoDatoGeneral)
        Next
      
        Return Lista
    End Function

End Class




