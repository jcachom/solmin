Imports Db2Manager.RansaData.iSeries.DataObjects
Imports SOLMIN_SC.Entidad
Public Class clsCalendario
    Public Function ActualizarCalendario(ByVal obeCalendario As beCalendario) As Int32
        Dim retorno As Int32 = 0
        Dim lobjSql As New SqlManager
        Dim lobjParams As New Parameter
        lobjParams.Add("PNFFRDO", obeCalendario.PNFFRDO)
        lobjParams.Add("PSCULUSA", ConfigurationWizard.UserName)
        lobjParams.Add("PSNTRMNL", Ransa.Utilitario.HelpClass.NombreMaquina)
        lobjParams.Add("PSTFRDO", obeCalendario.PSTFRDO)
        lobjParams.Add("PSFERIADO", obeCalendario.PSFERIADO)
        lobjSql.ExecuteNonQuery("SP_SOLSC_ACTUALIZAR_CALENDARIO", lobjParams)
        retorno = 1
        Return retorno
    End Function
    Public Function ListarCalendarioMensual(ByVal obeCalendario As beCalendario) As DataTable
        Dim odt As New DataTable
        Dim lobjSql As New SqlManager
        Dim lobjParams As New Parameter
        lobjParams.Add("PNANIO", obeCalendario.PNANIO)
        lobjParams.Add("PNMES", obeCalendario.PNMES)
        lobjParams.Add("PNMAXDIAMES", obeCalendario.PNMAXDIAMES)
        odt = lobjSql.ExecuteDataTable("SP_SOLSC_LISTAR_CALENDARIO_MENSUAL", lobjParams)
        Return odt
    End Function

End Class
