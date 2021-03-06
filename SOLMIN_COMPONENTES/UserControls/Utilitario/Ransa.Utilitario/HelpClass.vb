Imports NPOI.HSSF.UserModel
Imports System.Windows.Forms
Imports System.Drawing
Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.IO
Imports System.IO.Directory
Imports System.Data
Imports System.Reflection
Imports System.Data.OleDb
Imports System.ComponentModel
Imports NPOI.HPSF
Imports NPOI.POIFS.FileSystem
Imports NPOI.SS.UserModel
Imports System.Text.RegularExpressions
Imports ComponentFactory.Krypton.Toolkit

Public MustInherit Class HelpClass

  ''' <summary>
  ''' Cadena por la derecha
  ''' </summary>
  ''' <returns>Right</returns>
    ''' <remarks></remarks>
    ''' 
    Public Shared Function ServerRANSA() As String
        Return "RANSA"
    End Function
    Public Shared Function ServerRANSAT01() As String
        Return "RANSAT01"
    End Function
    Public Shared Function ServerRANSA01() As String
        Return "RANSA01"
    End Function

    Public Enum TipoLista
        OPERACION
        CONSISTENCIA
        PRELIQUIDACION
    End Enum

  Public Shared Function StringRight(ByVal cadena As String, ByVal numCaracteres As Int32) As String
    Try

      cadena = cadena.Trim
      If (cadena <> "") Then
        If (numCaracteres > cadena.Length) Then
          numCaracteres = 0
        End If
        cadena = cadena.Substring(cadena.Length - numCaracteres, numCaracteres)
      End If

    Catch ex As Exception

    End Try

    Return cadena
  End Function

  ''' <summary>
  ''' Convierte formato de fecha (yyyy/mm/dd) Número a   yyyymmdd
  ''' </summary>
  ''' <param name="fecha">Fecha en formato Date</param>
  ''' <returns>Fecha en Número</returns>
  ''' <remarks></remarks>
  Public Shared Function CDate_a_Numero8Digitos(ByVal fecha As Date) As String
    Dim yy As String = fecha.Year
    Dim mm As String = fecha.Month
    Dim dd As String = fecha.Day

    If mm.Length = 1 Then
      mm = "0" & mm
    End If
    If dd.Length = 1 Then
      dd = "0" & dd
    End If
    Return yy & mm & dd
  End Function

  Public Shared Function CortarString(ByVal strCadena As String, ByVal intMaximoCaracteres As Integer) As String
    If strCadena.ToString.Trim.Length > intMaximoCaracteres Then
      strCadena = strCadena.Substring(0, intMaximoCaracteres)
    End If
    Return strCadena
  End Function

  ''' <summary>
  ''' convierte fecha(string) en número yyyymmdd
  ''' </summary>
  ''' <param name="fecha"> fecha(string)</param>
  ''' <returns>Fecha en Número</returns>
  ''' <remarks></remarks>
  Public Shared Function CFecha_a_Numero8Digitos(ByVal fecha As String) As String
    Dim y As String = fecha.Substring(6, 4)
    Dim m As String = fecha.Substring(3, 2)
    Dim d As String = fecha.Substring(0, 2)
    Return y & m & d
  End Function

  ''' <summary>
  ''' Actualiza la celda seleccionada 
  ''' </summary>
  ''' <param name="grilla">Nombre del DataGridView</param>
  ''' <param name="valorBusqueda">Valor de búqueda</param>
  ''' <param name="columnaBusqueda">Columna en la que  va ha realizar la búsqueda</param>
  ''' <remarks></remarks>
  Public Shared Sub seleccionarFila(ByVal grilla As DataGridView, ByVal valorBusqueda As String, ByVal columnaBusqueda As String)
    For i As Integer = 0 To grilla.Rows.Count - 1
      If grilla.Item(columnaBusqueda, i).Value.Equals(valorBusqueda) Then
        grilla.CurrentCell = grilla.Rows(i).Cells(0)
      End If
    Next
    End Sub

    Public Shared Function CFecha_de_8_a_10(ByVal fecha As String) As String
        If fecha = "" OrElse fecha = "0" Then Return ""
        Dim y As String = fecha.Substring(0, 4)
        Dim m As String = fecha.Substring(4, 2)
        Dim d As String = fecha.Substring(6, 2)
        Return y & "-" & m & "-" & d
    End Function

  ''' <summary>
  ''' Link de la solicitud de transporte
  ''' </summary>
  ''' <returns>link de transporte</returns>
  ''' <remarks></remarks>
  Public Shared Function getURLDocLinksSolTransp() As String
    Return getSetting("URLDocLinksSolTransp")
  End Function
  Public Shared Function getURLDocLinksSolAlmacen() As String
    Return getSetting("URLDocLinksSolAlmacen")
  End Function


  ''' <summary>
  ''' Resta en día a una fecha 
  ''' </summary>
  ''' <param name="Fecha">Fecha a la que se va restar</param>
  ''' <param name="CantidadDias">Cantidad de días a restar</param>
  ''' <returns>Retornar fecha restada</returns>
  ''' <remarks></remarks>
  Public Shared Function RestarFechas(ByVal Fecha As Date, ByVal CantidadDias As Long) As Date
    Dim span As TimeSpan = New TimeSpan(Fecha.Ticks)
    Dim sp As TimeSpan = span.Subtract(TimeSpan.FromDays(CantidadDias))
    Return New Date(sp.Ticks)
  End Function

  ''' <summary>
  ''' Muestra un mensaje en un cuadro de diálogo
  ''' </summary>
  ''' <param name="Texto">Texto a mostrar en el mensaje</param>
  ''' <remarks></remarks>
  Public Shared Sub MsgBox(ByVal Texto As String)
    Dim SistNom As String = getSetting("Sistema")
    MessageBox.Show(Texto, SistNom, MessageBoxButtons.OK)
  End Sub

  ''' <summary>
  ''' Muestra un mensaje en un cuadro de diálogo
  ''' </summary>
  ''' <param name="Texto">Texto a mostrar en el mensaje</param>
  ''' <param name="i">MessageBoxIcon</param>
  ''' <remarks></remarks>
  Public Shared Sub MsgBox(ByVal Texto As String, ByVal i As System.Windows.Forms.MessageBoxIcon)
    Dim SistNom As String = getSetting("Sistema")
    MessageBox.Show(Texto, SistNom, MessageBoxButtons.OK, i)
  End Sub

  ''' <summary>
  ''' Muestra un mensaje de error en un cuadro de diálogo
  ''' </summary>
  ''' <remarks></remarks>
  ''' 
  Public Shared Sub ErrorMsgBox()
    Dim SistNom As String = getSetting("Sistema")
    MessageBox.Show(getErrMessage, SistNom, MessageBoxButtons.OK)

  End Sub

  ''' <summary>
  ''' Muestra un mensaje de pregunta en un cuadro de diálogo
  ''' </summary>
  ''' <param name="Texto">Texto a mostrar en el mensaje</param>
  ''' <returns>Retorna  Yes o No</returns>
  ''' <remarks></remarks>
  Public Shared Function RspMsgBox(ByVal Texto As String) As DialogResult
    Dim SistNom As String = getSetting("Sistema")
    Return MessageBox.Show(Texto, SistNom, MessageBoxButtons.YesNo)
  End Function

  ''' <summary>
  ''' Retorna mensaje de error
  ''' </summary>
  ''' <returns>error</returns>
  ''' <remarks></remarks>
  Public Shared Function getErrMessage() As String
    Return Configuration.ConfigurationSettings.AppSettings("MsgErr").ToString()
  End Function

  Public Shared Function getSetting(ByVal Nombre As String) As String
    Return Configuration.ConfigurationSettings.AppSettings(Nombre).ToString()
  End Function

  ''' <summary>
  ''' Convierte fecha en Número 
  ''' </summary>
  ''' <param name="obj">Fecha</param>
  ''' <returns>Número</returns>
  ''' <remarks></remarks>
  Public Shared Function CtypeDate(ByVal obj As Object) As String
    Dim objDate As New Date
    If obj.Equals("") = True Then
      objDate = DateTime.Today
    Else
      objDate = obj
    End If
    Return objDate.Year & "" & IIf(objDate.Month < 10, "0" & objDate.Month, objDate.Month) & "" & IIf(objDate.Day < 10, "0" & objDate.Day, objDate.Day)
  End Function

  ''' <summary>
  ''' Convierte fecha (yyyy/mm/dd hh:mm:ss) en Cadena (yyyy-mm-dd)
  ''' </summary>
  ''' <param name="obj">Fecha</param>
  ''' <returns>Cadena en formato (YYYY-MM-DD) </returns>
  ''' <remarks></remarks>
  Public Shared Function DateConvert(ByVal obj As Object) As String
    Dim objDate As New Date
    If obj.Equals("") = True Then
      objDate = DateTime.Today
    Else
      objDate = obj
    End If
    Return objDate.Year & "-" & IIf(objDate.Month < 10, "0" & objDate.Month, objDate.Month) & "-" & IIf(objDate.Day < 10, "0" & objDate.Day, objDate.Day)

  End Function

  ''' <summary>
  ''' Conviete de fecha (yyyy/mm/dd hh:mm:ss) en fecha (yyyy/mm/dd )
  ''' </summary>
  ''' <param name="obj">Fecha</param>
  ''' <returns>Fecha en formato (yyyy/mm/dd )</returns>
  ''' <remarks></remarks>
  Public Shared Function RowFilterDate(ByVal obj As Date) As String
    Return IIf(obj.Month < 10, "0" & obj.Month, obj.Month) & "/" & IIf(obj.Day < 10, "0" & obj.Day, obj.Day) & "/" & obj.Year
  End Function

  Public Shared Sub OpenWeb(ByVal Url As String)
    On Error Resume Next
    System.Windows.Forms.Help.ShowHelp(New Form(), Url)
  End Sub

  Public Shared Function getTextFile(ByVal Archivo As String) As StringBuilder
    Dim str As New StringBuilder
    Try
      Dim oFile As New IO.StreamReader(Archivo)
      While oFile.EndOfStream = False
        str.Append(oFile.ReadLine)
      End While
    Catch : End Try
    Return str
  End Function

  Public Shared Sub WriteTextFile(ByVal Archivo As String, ByVal Texto As String)
    Try
      Dim oFile As New IO.StreamWriter(Application.StartupPath & "\" & Archivo, False, Encoding.ASCII)
      oFile.WriteLine(Texto.ToString())
      oFile.Close()
    Catch : End Try

  End Sub

  ''' <summary>
  ''' Retorna Número sin comas
  ''' </summary>
  ''' <param name="numero">Número</param>
  ''' <returns>Número son comas</returns>
  ''' <remarks></remarks>
  Public Shared Function FormatoNumerico_Sin_Comas(ByVal numero As String) As String
    Return numero.Replace(",", "")
  End Function

  ''' <summary>
  ''' Retorna el Nombre de la PC
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
    Public Shared Function NombreMaquina() As String
        Dim pcname As String = My.Computer.Name.ToString()
        If pcname.Length > 10 Then
            pcname = pcname.Substring(0, 10)
        End If
        Return pcname
    End Function

  ''' <summary>
  ''' Retorna la fecha actual en número yyyymmdd
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function TodayNumeric() As String
    Return Today.Year & "" & IIf(Today.Month < 10, "0" & Today.Month, Today.Month) & "" & IIf(Today.Day < 10, "0" & Today.Day, Today.Day)
  End Function

  ''' <summary>
  ''' Retorna el año actual
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function TodayAnio() As Int64
    Return Today.Year
  End Function

  ''' <summary>
  ''' Retorna numero mes actual
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function TodayMes() As Int64
    Return Today.Month
  End Function

  ''' <summary>
  ''' Retorna cantidad de dias de febrero
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function EsBisiesto(ByVal anio As Int64) As Boolean
    'Dim bisiesto As Boolean = False
    'If ((anio Mod 4) = 0) And ((anio Mod 100) <> 0 Or (anio Mod 400) = 0) Then
    '    bisiesto = True
    'Else
    '    bisiesto = False
    'End If
    'Return bisiesto
    Dim bisiesto As Boolean = False
    If (Date.IsLeapYear(anio)) Then
      bisiesto = True
    Else
      bisiesto = False
    End If
    Return bisiesto

  End Function

  ''' <summary>
  ''' Retorna cantidad de dias del mes segun año
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DiasMesPorAño(ByVal anio As Int64, ByVal mes As Int64) As Int64
    Dim dias As Int64 = 0
    Dim odtMeses As New DataTable()
    Dim filtro As String = ""
    Dim MaxDiasMesNum As Int32 = 0
    Dim keymes As String = ""
    odtMeses = Meses(anio)
    keymes = mes.ToString
    If (keymes.Length <= 1) Then
      keymes = "0" & keymes
    End If
    filtro = "key='" & keymes & "'"
    dias = odtMeses.Select(filtro)(0).Item("max")
    Return dias

  End Function


  ''' <summary>
  ''' Retorna cantidad de dias de febrero
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function DiasFebreroAnio(ByVal anio As Int64) As Int64
    'Dim dias As Int64 = 0
    'If ((anio Mod 4) = 0) And ((anio Mod 100) <> 0 Or (anio Mod 400) = 0) Then
    '    dias = 29
    'Else
    '    dias = 28
    'End If
    'Return dias
    Dim dias As Int64 = 0
    If (Date.IsLeapYear(anio)) Then
      dias = 29
    Else
      dias = 28
    End If
    Return dias
  End Function

  ''' <summary>
  ''' Retorna meses del año
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function Meses(ByVal anio As Int64) As DataTable

    Dim odt As New DataTable()
    odt.Columns.Add("key")
    odt.Columns.Add("value")
    odt.Columns.Add("value2")
    odt.Columns.Add("min")
    odt.Columns.Add("max")
    Dim dr As DataRow
    Dim dia As Integer = Date.Today.Month
    Dim ci As Globalization.CultureInfo
    ci = New Globalization.CultureInfo("es-ES")
    Dim Mes As String = ""
    Dim MesAbr As String = ""
    Dim NumMes As String = ""
    For index As Integer = 1 To 12
      dr = odt.NewRow()
      NumMes = StringRight("0" & index, 2)
      Mes = Convert.ToDateTime(CNumero8Digitos_a_FechaDefault(anio.ToString & StringRight("0" & NumMes, 2) & "01")).ToString("MMMM", ci)
      Mes = StrConv(Mes, VbStrConv.ProperCase)
      dr.Item("key") = NumMes
      dr.Item("value") = Mes
      dr.Item("value2") = Mes.Substring(0, 3)
      dr.Item("min") = 1
      dr.Item("max") = Date.DaysInMonth(anio, index)
      odt.Rows.Add(dr)
    Next
    Return odt



  End Function

  ''' <summary>
  ''' Retorna Hora actual en Número hhmmss
  ''' </summary>
  ''' <returns>Hora</returns>
  ''' <remarks></remarks>
  Public Shared Function NowNumeric() As String
    Return IIf(Now.Hour < 10, "0" & Now.Hour, Now.Hour) & "" & IIf(Now.Minute < 10, "0" & Now.Minute, Now.Minute) & "" & IIf(Now.Second < 10, "0" & Now.Second, Now.Second)
  End Function

  ''' <summary>
  ''' Convierte número de ocho yyyymmdd digitos a cadena en formato (yyyy/mm/dd)
  ''' </summary>
  ''' <param name="oFecha">Número en formato yyyymmdd</param>
  ''' <returns>Retorna cadena en formato yyyy/mm/dd</returns>
  ''' <remarks></remarks>
  Public Shared Function CNumero8Digitos_a_Fecha(ByVal oFecha As Object) As String
    Dim y As String = ""
    Dim m As String = ""
    Dim d As String = ""

    y = Left(oFecha.ToString(), 4)
    m = Right(Left(oFecha.ToString(), 6), 2)
    d = Right(oFecha.ToString(), 2)
    Return d & "/" & m & "/" & y
  End Function


  ''' <summary>
  ''' Convierte número de ocho yyyymmdd digitos a cadena en formato (dd/mm/yyyy)
  ''' </summary>
  ''' <returns>Retorna cadena en formato yyyy/mm/dd</returns>
  ''' <remarks></remarks>

  Public Shared Function CNumero8Digitos_a_FechaDefault(ByVal oFechaNumero As Int64) As String
    Dim y As String = ""
    Dim m As String = ""
    Dim d As String = ""
    Dim FechaFormada As String = ""
    Dim oFecha As String = oFechaNumero.ToString()
    If (Val(oFecha) = 0) Then
      FechaFormada = ""
    Else
      y = Left(oFecha.ToString(), 4)
      m = Right(Left(oFecha.ToString(), 6), 2)
      d = Right(oFecha.ToString(), 2)
      FechaFormada = d & "/" & m & "/" & y

    End If
    Return FechaFormada
  End Function


  Public Shared Function FechaNum_a_Fecha(ByVal xFecha As Object) As String
    Dim FechaNum As String = ("" & xFecha).ToString.Trim
    Dim y As String = ""
    Dim m As String = ""
    Dim d As String = ""
    Dim FechaFormada As String = ""
    If (Val(FechaNum) = 0 OrElse FechaNum = "") Then
      FechaFormada = ""
    Else
      y = Left(FechaNum, 4).PadLeft(2, "0")
      m = Right(Left(FechaNum, 6), 2).PadLeft(2, "0")
      d = Right(FechaNum, 2).PadLeft(2, "0")
      FechaFormada = d & "/" & m & "/" & y
    End If
    Return FechaFormada
  End Function

  Public Shared Function HoraNum_a_Hora(ByVal oHora As Object) As String
    Dim Hora As String = ("" & oHora).ToString.Trim
    Dim h As String = ""
    Dim m As String = ""
    Dim s As String = ""
    If Hora.ToString.Trim.Length < 6 Then
      Hora = "0" & Hora
    End If
    h = Left(Hora, 2).PadLeft(2, "0")
    m = Right(Left(Hora, 4), 2).PadLeft(2, "0")
    s = Right(Hora, 2).PadLeft(2, "0")
    Return h + ":" + m + ":" + s
    End Function

    Public Shared Function HoraNum_a_Hora_Cadena(ByVal oHora As Object) As String
        Dim Hora As String = ("" & oHora).ToString.Trim
        If CType(Hora, String) = "0" OrElse CType(Hora, String) = String.Empty Then
            Return ""
        Else
            'Dim Hora As String = ("" & oHora).ToString.Trim
            Dim h As String = ""
            Dim m As String = ""
            Dim s As String = ""
            If Hora.ToString.Trim.Length < 6 Then
                Hora = "0" & Hora
            End If
            h = Left(Hora, 2).PadLeft(2, "0")
            m = Right(Left(Hora, 4), 2).PadLeft(2, "0")
            s = Right(Hora, 2).PadLeft(2, "0")
            Return h + ":" + m + ":" + s
        End If
    End Function


    Public Shared Function HoraNum_a_Hora_Default(ByVal oHora As Object) As String
        Dim Hora As String = ("" & oHora).ToString.Trim
        Dim h As String = ""
        Dim m As String = ""
        Dim s As String = ""
        If (Hora <> "0" OrElse Hora <> "") AndAlso Hora.ToString.Trim.Length < 6 Then
            Hora = "0" & Hora

            h = Left(Hora, 2).PadLeft(2, "0")
            m = Right(Left(Hora, 4), 2).PadLeft(2, "0")
            s = Right(Hora, 2).PadLeft(2, "0")
        End If
        'If h.Length > 0  AndAlso m.Length > 0 AndAlso s.Length > 0 Then
        If h <> "00" And h.Length > 0 AndAlso m.Length > 0 AndAlso s.Length > 0 Then
            Hora = h + ":" + m + ":" + s
        Else
            Hora = ""
        End If
        Return Hora
        'Return h + ":" + m + ":" + s
    End Function




  ''' <summary>
  ''' Convierte número de ocho digitos yyyymmdd a fecha en formato (yyyy/mm/dd)
  ''' </summary>
  ''' <param name="oFecha">>Número en formato yyyymmdd</param>
  ''' <returns>retorna fecha en formato yyyy/mm/dd</returns>
  ''' <remarks></remarks>
Public Shared Function CNumero_a_Fecha(ByVal oFecha As Object) As Date
    Dim y As Integer
    Dim m As Integer
    Dim d As Integer

    y = Left(oFecha.ToString(), 4)
    m = Right(Left(oFecha.ToString(), 6), 2)
    d = Right(oFecha.ToString(), 2)

    Dim objDate As New Date(y, m, d)
    Return objDate
    End Function



  ''' <summary>
  ''' Convierte número de ocho digitos yyyymmdd a fecha en formato (yyyy/mm/dd)
  ''' </summary>
  ''' <param name="oHora">>Número en formato yyyymmdd</param>
  ''' <returns>retorna fecha en formato yyyy/mm/dd</returns>
  ''' <remarks></remarks>
  Public Shared Function CNumero_a_Hora(ByVal oHora As Object) As Date
    Dim h As String
    Dim m As String
    Dim s As String
    If oHora.ToString.Trim.Length < 6 Then
      oHora = "0" & oHora
    End If

    h = Left(oHora.ToString(), 2)
    m = Right(Left(oHora.ToString(), 4), 2)
    s = Right(oHora.ToString(), 2)
    Return h + ":" + m + ":" + s
  End Function


    Public Shared Function IList_DataTable(Of T)(ByVal list As IList(Of T)) As DataTable
        Dim td As New DataTable
        Dim entityType As Type = GetType(T)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(entityType)

        For Each prop As PropertyDescriptor In properties
            td.Columns.Add(prop.Name, prop.PropertyType)
        Next

        For Each item As T In list
            Dim row As DataRow = td.NewRow()

            For Each prop As PropertyDescriptor In properties
                row(prop.Name) = prop.GetValue(item)
            Next

            td.Rows.Add(row)
        Next

        Return td
    End Function

  ''' <summary>
  ''' Convierte Lista de Objetos en DataTable  
  ''' </summary>
  ''' <typeparam name="T"> List(Of T)</typeparam>
  ''' <param name="list">Lista de Objetos</param>
  ''' <returns>DataTable</returns>
  ''' <remarks></remarks>
  Public Shared Function GetDataSetNative(Of T)(ByVal list As List(Of T)) As DataTable
    Try
      Dim _resultDataSet As New DataSet()
      Dim _resultDataTable As New DataTable("dtb")
      Dim _resultDataRow As DataRow = Nothing
      If list.Count > 0 Then
        Dim _itemProperties() As PropertyInfo = list.Item(0).GetType().GetProperties()
        _itemProperties = list.Item(0).GetType().GetProperties()
        For Each p As PropertyInfo In _itemProperties
          _resultDataTable.Columns.Add(p.Name, _
                 p.GetGetMethod.ReturnType())
        Next
        For Each item As T In list
          _itemProperties = item.GetType().GetProperties()
          _resultDataRow = _resultDataTable.NewRow()
          For Each p As PropertyInfo In _itemProperties
                        If p.CanWrite Then
                            If p.GetValue(item, Nothing) Is DBNull.Value Then
                                _resultDataRow(p.Name) = nothing
                            Else
                                _resultDataRow(p.Name) = p.GetValue(item, Nothing)
                            End If

                        End If
          Next
          _resultDataTable.Rows.Add(_resultDataRow)
        Next
        _resultDataSet.Tables.Add(_resultDataTable)
        Return _resultDataSet.Tables(0)
      Else
        Return New Data.DataTable

      End If
    Catch ex As Exception

    End Try

  End Function

  ''' <summary>
  ''' Valida Solo números
  ''' </summary>
  Public Shared Function SoloNumeros(ByVal Keyascii As Short) As Short

    If InStr("1234567890.", Chr(Keyascii)) = 0 Then
      SoloNumeros = 0
    Else
      SoloNumeros = Keyascii
    End If
    Select Case Keyascii
      Case 8
        SoloNumeros = Keyascii
      Case 13
        SoloNumeros = Keyascii
      Case 32
        SoloNumeros = Keyascii
    End Select

  End Function

  ''' <summary>
  ''' Valida solo Letras
  ''' </summary>
  Public Shared Function SoloLetras(ByVal KeyAscii As Integer) As Integer
    KeyAscii = Asc(UCase(Chr(KeyAscii)))
    If InStr("1234567890", Chr(KeyAscii)) <> 0 Then
      SoloLetras = 0
    Else
      SoloLetras = KeyAscii
    End If
    Select Case KeyAscii
      Case 8
        SoloLetras = KeyAscii
      Case 13
        SoloLetras = KeyAscii
      Case 32
        SoloLetras = KeyAscii
    End Select
  End Function

  ''' <summary>
  ''' Convierte  Hora a número
  ''' </summary>
  ''' <param name="Hora"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function ConvertHoraNumeric(ByVal Hora As String) As String
    Dim lstr_hora As String = Hora.Trim.Substring(0, 2) & Hora.Trim.Substring(3, 2) & Hora.Trim.Substring(6, 2)
    Return lstr_hora
  End Function

  Public Shared Function ConvertFechaGPS_Fecha(ByVal Fecha As String) As String
    Dim lstr_Fecha As String = CType(Fecha, Date).AddDays(-1).ToShortDateString
    Return lstr_Fecha
  End Function

  ''' <summary>
  ''' Hora actual 
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Shared Function Now_Hora() As String
    Return IIf(Now.Hour < 10, "0" & Now.Hour, Now.Hour) & ":" & IIf(Now.Minute < 10, "0" & Now.Minute, Now.Minute) & ":" & IIf(Now.Second < 10, "0" & Now.Second, Now.Second)
  End Function

  Public Shared Function ImageConversion(ByVal image As System.Drawing.Image, ByVal formatoImagen As System.Drawing.Imaging.ImageFormat) As String
    If image Is Nothing Then Return ""
    Dim memoryStream As System.IO.MemoryStream = New System.IO.MemoryStream
    image.Save(memoryStream, formatoImagen)
    Dim value As String = ""
    For intCnt As Integer = 0 To memoryStream.ToArray.Length - 1
      value = value & memoryStream.ToArray(intCnt) & ","
    Next
    Return value
  End Function

  Public Shared Sub ExportarExcel_XML(ByVal objListdt As List(Of DataTable))
    'averiguando si es que existe el directorio a exportar
    Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tempo\"
    If IO.Directory.Exists(path) = False Then
      IO.Directory.CreateDirectory(path)
    End If
    Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
    Dim xls As New IO.StreamWriter(archivo, True, Encoding.UTF8)
    xls.WriteLine("<?xml version=""1.0""?>")
    xls.WriteLine("<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""")
    xls.WriteLine("xmlns:o=""urn:schemas-microsoft-com:office:office""")
    xls.WriteLine("xmlns:x=""urn:schemas-microsoft-com:office:excel""")
    xls.WriteLine("xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""")
    xls.WriteLine("xmlns:html=""http://www.w3.org/TR/REC-html40"">")



    For Each odtg As DataTable In objListdt
      xls.WriteLine("<Worksheet ss:Name=""" & odtg.TableName & """>")
      xls.WriteLine(" <Table> ")

      xls.WriteLine(" <Row>")
      xls.WriteLine(" </Row>")
      xls.WriteLine(" <Row>")
      xls.WriteLine(" </Row>")
      xls.WriteLine(" <Row>")
      xls.WriteLine(" </Row>")

      xls.WriteLine(" <Row>")
      Dim strStyl As String = ""
      For columna As Integer = 0 To odtg.Columns.Count - 1
        'xls.WriteLine("<Style ss:ID=""s64"" ss:Parent='s" & strStyl & columna.ToString.Trim & "'>")
        'xls.WriteLine(" <Alignment ss:Horizontal='Center' ss:Vertical='Center' ss:ShrinkToFit='1'")
        'xls.WriteLine(" ss:WrapText='1'/>")
        'xls.WriteLine("<Borders/>")
        'xls.WriteLine("<Font ss:FontName='Arial' x:Family='Swiss' ss:Size='9' ss:Color='#FFFFFF' ")
        'xls.WriteLine(" ss:Bold='1'/>")
        'xls.WriteLine(" <Interior ss:Color='#008000' ss:Pattern='Solid'/>")
        'xls.WriteLine(" <NumberFormat ss:Format='@'/>")
        'xls.WriteLine("</Style>")
        xls.WriteLine(" <Cell   ><Data ss:Type=""String"" >" & odtg.Columns(columna).ColumnName.ToString & "</Data></Cell>")
      Next
      xls.WriteLine(" </Row>")
      For fila As Integer = 0 To odtg.Rows.Count - 1
        xls.WriteLine("<Row>")
        For columna As Integer = 0 To odtg.Columns.Count - 1
          xls.WriteLine("<Cell><Data ss:Type=""String"">" & odtg.Rows(fila).Item(columna) & "</Data></Cell>")
        Next
        xls.WriteLine(" </Row>")
      Next
      xls.WriteLine(" </Table>")
      xls.WriteLine(" </Worksheet>")

    Next
    xls.WriteLine(" </Workbook>")
    xls.Flush()
    xls.Close()
    xls.Dispose()
    AbrirDocumento(archivo)
  End Sub
  ''' <summary>
  ''' Exporta a Excel mediante DDL MOPI
  ''' </summary>
  ''' <param name="objListdt"></param>
  ''' <param name="title"></param>
  ''' <remarks></remarks>
  Public Shared Sub ExportExcel(ByVal objListdt As List(Of DataTable), ByVal title As String, Optional ByVal filtro As Hashtable = Nothing)
    Try
      Dim path As String = Application.StartupPath.ToString
      Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
      Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

      Dim objWorkBook As New HSSFWorkbook()
      Dim objWorkSheet As New HSSFSheet(objWorkBook)
      For Each odtg As DataTable In objListdt
        objWorkSheet = objWorkBook.CreateSheet(odtg.TableName)
        objWorkSheet.CreateRow(3)
        '=============Style======================
        Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

        Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontFiltro.FontHeight = 165

        Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontCab.FontHeight = 220

        Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFont.FontHeight = 170

        styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFiltro.SetFont(oFontFiltro)
        styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

        styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
        styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleCab.SetFont(oFontCab)
        styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        style.SetFont(oFont)
        style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        style.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleNumber.SetFont(oFontFiltro)
        styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
        styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleMonto.SetFont(oFontFiltro)
        styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
        styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFecha.SetFont(oFontFiltro)
        styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND
        '============================================
        Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
        'create the anchor
        Dim anchor As HSSFClientAnchor
        anchor = New HSSFClientAnchor(0, 0, 0, 120, 0, 0, _
         0, 0)
        If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
          'load the picture and get the picture index in the workbook
          Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
          'Reset the image to the original size.
          picture.Resize(0.5)
        End If
        If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
          'load the picture and get the picture index in the workbook
          Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
          'Reset the image to the original size.
          picture.Resize(0.5)
        End If
        Dim rowActual As Integer = 6
        '===============Titulo
        objWorkSheet.CreateRow(rowActual).Height = 100 * 4
        '===========================
        For x As Integer = 0 To odtg.Columns.Count - 1
          objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = styleCab
        Next
        '===========Titulo es centrado segun tamaño de grilla===========
        objWorkSheet.GetRow(rowActual).GetCell(0).SetCellValue(title)
        objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual, 0, rowActual, odtg.Columns.Count - 1))
        '==============Verificamos Filtro a utilizar==================
        If filtro Is Nothing Then
          rowActual = rowActual + 1
        Else
          Dim x As Integer = 0
          For Each items As DictionaryEntry In filtro
            x = x + 1
            objWorkSheet.CreateRow(rowActual + x)

            objWorkSheet.GetRow(rowActual + x).CreateCell(0).CellStyle = style
            objWorkSheet.GetRow(rowActual + x).GetCell(0).SetCellValue(items.Key.ToString.Trim)

            objWorkSheet.GetRow(rowActual + x).CreateCell(1).CellStyle = styleFiltro
            objWorkSheet.GetRow(rowActual + x).GetCell(1).SetCellValue(items.Value.ToString.Trim)

          Next
          rowActual = rowActual + filtro.Count + 1
        End If
        '===================Se cargan Las Cabeceras=====================
        Dim flgCabDoble As Boolean = False
        objWorkSheet.CreateRow(rowActual)
        For x As Integer = 0 To odtg.Columns.Count - 1
          '===Estilo===
          objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
          '===Valores===
          Dim valorCab As String = odtg.Columns(x).ColumnName.ToString().Trim()
          Dim valorCabDoble As String()
          valorCabDoble = Split(valorCab, "\n")
          If valorCabDoble.Length = 2 Then
            flgCabDoble = True
          Else
            flgCabDoble = False
          End If
          objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
        Next
        rowActual = rowActual + 1
        If flgCabDoble = True Then
          '==========Limpiamos los Cells Repetidos en el Row Anterior=========
          For x As Integer = 0 To odtg.Columns.Count - 1
            If x < odtg.Columns.Count - 1 Then
              If objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x + 1).StringCellValue Then
                Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                objWorkSheet.GetRow(rowActual - 1).GetCell(x + 1).SetCellValue("")
                '===Union===
                Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + 1)
                objWorkSheet.AddMergedRegion(Region)
              End If
            End If
                    Next

          '===================================================================
          objWorkSheet.CreateRow(rowActual)
          For x As Integer = 0 To odtg.Columns.Count - 1
            '===Estilo===
            objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
            '===Valores===
            Dim valorCab As String = odtg.Columns(x).ColumnName.ToString().Trim()
            Dim valorCabDoble As String()
            valorCabDoble = Split(valorCab, "\n")
            If valorCabDoble.Length = 2 Then
              objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
            End If
          Next
          rowActual = rowActual + 1
        End If
        '===================Se Carga El Detalle============================
        For i As Integer = 0 To odtg.Rows.Count - 1
          objWorkSheet.CreateRow(i + rowActual)
          For x As Integer = 0 To odtg.Columns.Count - 1
            Dim celda As String = odtg.Rows(i).Item(x).ToString().Trim()
            If IsNumeric(celda) Then
              If celda.Contains(".") Then
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda))
              ElseIf (celda.ToString.Substring(0, 1) = "0") Then
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(odtg.Rows(i).Item(x).ToString().Trim())
                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
              Else
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda))
              End If
            Else
              If IsDate(celda) Then
                celda = String.Format("{0:d}", CDate(celda))
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
              Else
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(odtg.Rows(i).Item(x).ToString().Trim())
                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
              End If
            End If

          Next
        Next
        '    objWorkSheet.GroupRow(rowActual, rowActual + odtg.Rows.Count)
        '===================================================================
        For z As Integer = 0 To odtg.Columns.Count - 1
          objWorkSheet.AutoSizeColumn(z, True)
        Next
      Next
      objWorkBook.Write(fs)
      fs.Close()
      AbrirDocumento(archivo)
    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try

    End Sub


    ''' <summary>
    ''' Exporta a Excel mediante DDL MOPI
    ''' </summary>
    ''' <param name="objListdt"></param>
    ''' <param name="title"></param>
    ''' <remarks></remarks>
    Public Shared Sub ExportExcelStockProductoXAlmacen(ByVal objDs As DataSet, ByVal title As String, Optional ByVal filtro As Hashtable = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()
            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            Dim odtTemp As New DataTable
            Dim IndiceColum As Integer = 0
            Dim indice As Integer = 0
            Dim y As Integer = 0 'Posicion Inicial por la Izquierda
            Dim odtg As New DataTable
            odtg = objDs.Tables(0)
            odtTemp = odtg.Copy
            objWorkSheet = objWorkBook.CreateSheet(odtg.TableName)
            objWorkSheet.CreateRow(3)
            '=============Style======================
            Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

            Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontFiltro.FontHeight = 165

            Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220

            Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFont.FontHeight = 170

            styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.SetFont(oFontFiltro)
            styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

            styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            style.SetFont(oFont)
            style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            style.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.SetFont(oFontFiltro)
            styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
            styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.SetFont(oFontFiltro)
            styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
            styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFecha.SetFont(oFontFiltro)
            styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND
            '============================================
            Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
            'create the anchor
            Dim anchor As HSSFClientAnchor
            anchor = New HSSFClientAnchor(0, 0, 0, 120, 0, 0, _
             0, 0)
            If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                'Reset the image to the original size.
                picture.Resize(0.5)
            End If
            If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                'Reset the image to the original size.
                picture.Resize(0.5)
            End If
            Dim rowActual As Integer = 6
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 4
            '===========================
            For x As Integer = 0 To odtg.Columns.Count - 1
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = styleCab
            Next
            '===========Titulo es centrado segun tamaño de grilla===========
            objWorkSheet.GetRow(rowActual).GetCell(0).SetCellValue(title)
            objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual, 0, rowActual, odtg.Columns.Count - 1))
            '==============Verificamos Filtro a utilizar==================
            If filtro Is Nothing Then
                rowActual = rowActual + 1
            Else
                Dim x As Integer = 0
                For Each items As DictionaryEntry In filtro
                    x = x + 1
                    objWorkSheet.CreateRow(rowActual + x)

                    objWorkSheet.GetRow(rowActual + x).CreateCell(0).CellStyle = style
                    objWorkSheet.GetRow(rowActual + x).GetCell(0).SetCellValue(items.Key.ToString.Trim)

                    objWorkSheet.GetRow(rowActual + x).CreateCell(1).CellStyle = styleFiltro
                    objWorkSheet.GetRow(rowActual + x).GetCell(1).SetCellValue(items.Value.ToString.Trim)

                Next
                rowActual = rowActual + filtro.Count + 1
            End If
            '===================Se cargan Las Cabeceras=====================
            Dim flgCabDoble As Boolean = False
            objWorkSheet.CreateRow(rowActual)
            For x As Integer = 0 To odtg.Columns.Count - 1
                '===Estilo===
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                '===Valores===
                Dim valorCab As String = odtg.Columns(x).ColumnName.ToString().Trim()
                Dim valorCabDoble As String()
                valorCabDoble = Split(valorCab, "\n")
                If valorCabDoble.Length = 2 Then
                    flgCabDoble = True
                Else
                    flgCabDoble = False
                End If
                objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
            Next
            rowActual = rowActual + 1
            If flgCabDoble = True Then
                '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                For x As Integer = 0 To odtg.Columns.Count - 1
                    If x < odtg.Columns.Count - 1 Then
                        If objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x + 1).StringCellValue Then
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            objWorkSheet.GetRow(rowActual - 1).GetCell(x + 1).SetCellValue("")
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + 1)
                            objWorkSheet.AddMergedRegion(Region)
                        End If
                    End If
                Next

                '===================================================================
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To odtg.Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = odtg.Columns(x).ColumnName.ToString().Trim()
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                    End If
                Next
                rowActual = rowActual + 1
            End If
            '===================Se Carga El Detalle============================
            Dim drTemp As DataRow = Nothing

            For i As Integer = 0 To odtg.Rows.Count - 1
                objWorkSheet.CreateRow(i + rowActual)
                IndiceColum = 0
                For x As Integer = 0 To odtg.Columns.Count - 1
                    y = IndiceColum
                    Dim celda As String = odtg.Rows(i).Item(x).ToString().Trim()
                    If drTemp IsNot Nothing _
                    AndAlso odtTemp.Rows(i).Item("TIPO ALMACEN \n COD").ToString = drTemp.Item("TIPO ALMACEN \n COD") _
                    AndAlso odtTemp.Rows(i).Item("TIPO ALMACEN \n DETALLE").ToString = drTemp.Item("TIPO ALMACEN \n DETALLE") _
                    AndAlso odtTemp.Rows(i).Item("ALMACEN \n COD").ToString = drTemp.Item("ALMACEN \n COD") _
                    AndAlso odtTemp.Rows(i).Item("ALMACEN \n DETALLE").ToString = drTemp.Item("ALMACEN \n DETALLE") _
                    AndAlso odtTemp.Rows(i).Item("ZONA ALMACEN \n COD").ToString = drTemp.Item("ZONA ALMACEN \n COD") _
                    AndAlso odtTemp.Rows(i).Item("ZONA ALMACEN \n DETALLE").ToString = drTemp.Item("ZONA ALMACEN \n DETALLE") _
                    AndAlso odtTemp.Rows(i).Item("CÓDIGO \n MERCADERIA").ToString = drTemp.Item("CÓDIGO \n MERCADERIA") _
                    AndAlso odtTemp.Rows(i).Item("ORDEN \n SERVICIO").ToString = drTemp.Item("ORDEN \n SERVICIO") _
                    AndAlso odtTemp.Rows(i).Item("CODIGO \n RANSA").ToString = drTemp.Item("CODIGO \n RANSA") _
                    AndAlso odtTemp.Rows(i).Item("DETALLE \n MERCADERIA").ToString = drTemp.Item("DETALLE \n MERCADERIA") _
                    AndAlso odtTemp.Rows(i).Item("SALDO \n CANTIDAD").ToString = drTemp.Item("SALDO \n CANTIDAD") _
                    AndAlso odtTemp.Rows(i).Item("SALDO \n UNIDAD").ToString = drTemp.Item("SALDO \n UNIDAD") _
                    AndAlso odtTemp.Rows(i).Item("SALDO \n PESO").ToString = drTemp.Item("SALDO \n PESO") _
                    AndAlso odtTemp.Rows(i).Item("SALDO \n UNIDAD ").ToString = drTemp.Item("SALDO \n UNIDAD ") Then

                        'If Not odtg.Columns(x).ColumnName.ToString.Trim.Contains("_") AndAlso _
                        If Not odtg.Columns(x).ColumnName.ToString.Trim.Equals("PEDIDO") AndAlso _
                        Not odtg.Columns(x).ColumnName.ToString.Trim.Equals("SALDO \n PROYECTO") Then
                            celda = ""
                        End If
                    End If

                    If IsNumeric(celda) Then
                        If celda.Contains(".") Then
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(y).CellStyle = styleMonto
                            objWorkSheet.GetRow(indice + rowActual).GetCell(y).SetCellValue(Convert.ToDouble(celda))
                        ElseIf (celda.ToString.Substring(0, 1) = "0") Then
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(y).SetCellValue(odtg.Rows(i).Item(x).ToString().Trim())
                            objWorkSheet.GetRow(indice + rowActual).GetCell(y).CellStyle = styleFiltro
                        Else
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(y).CellStyle = styleNumber
                            objWorkSheet.GetRow(indice + rowActual).GetCell(y).SetCellValue(Convert.ToDouble(celda))
                        End If
                    Else
                        If IsDate(celda) Then
                            celda = String.Format("{0:d}", CDate(celda))
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(y).CellStyle = styleFecha
                            objWorkSheet.GetRow(indice + rowActual).GetCell(y).SetCellValue(celda)
                        Else
                            objWorkSheet.GetRow(i + rowActual).CreateCell(y).SetCellValue(celda) '
                            objWorkSheet.GetRow(indice + rowActual).GetCell(y).CellStyle = styleFiltro
                        End If
                    End If
                    IndiceColum = IndiceColum + 1
                Next
                drTemp = odtTemp.Rows(i)
                indice = indice + 1
            Next
            '    objWorkSheet.GroupRow(rowActual, rowActual + odtg.Rows.Count)
            '===================================================================
            For z As Integer = 0 To odtg.Columns.Count - 1
                objWorkSheet.AutoSizeColumn(z, True)
            Next
            'Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

  ''' <summary>
  ''' Exporta a Excel mediante DDL MOPI
  ''' </summary>
  ''' <param name="objListdt"></param>
  ''' <param name="title"></param>
  ''' <remarks></remarks>
  Public Shared Sub ExportExcel_ConTitulos(ByVal objListdt As List(Of DataTable), ByVal title As List(Of String), Optional ByVal filtro As Hashtable = Nothing)
    Try
      Dim path As String = Application.StartupPath.ToString
      Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
      Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

      Dim objWorkBook As New HSSFWorkbook()
      Dim objWorkSheet As New HSSFSheet(objWorkBook)
      For Each odtg As DataTable In objListdt
        objWorkSheet = objWorkBook.CreateSheet(odtg.TableName)
        objWorkSheet.CreateRow(3)
        '=============Style======================
        Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim styleTitulo As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

        Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontFiltro.FontHeight = 165

        Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontCab.FontHeight = 220

        Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFont.FontHeight = 170

        Dim oFontTitulo As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontTitulo.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontTitulo.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
        oFontTitulo.FontHeight = 190

        styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFiltro.BorderRight = CellBorderType.THIN
        styleFiltro.BorderBottom = CellBorderType.THIN
        styleFiltro.BorderLeft = CellBorderType.THIN
        styleFiltro.BorderTop = CellBorderType.THIN
        styleFiltro.SetFont(oFontFiltro)
        styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

        styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
        styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleCab.BorderRight = CellBorderType.THIN
        styleCab.BorderBottom = CellBorderType.THIN
        styleCab.BorderLeft = CellBorderType.THIN
        styleCab.BorderTop = CellBorderType.THIN
        styleCab.SetFont(oFontCab)
        styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER

        style.FillPattern = FillPatternType.SOLID_FOREGROUND
        style.BorderRight = CellBorderType.THIN
        style.BorderBottom = CellBorderType.THIN
        style.BorderLeft = CellBorderType.THIN
        style.BorderTop = CellBorderType.THIN
        style.SetFont(oFont)
        style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        style.FillPattern = FillPatternType.SOLID_FOREGROUND

        'styleTitulo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
        styleTitulo.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleTitulo.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
        styleTitulo.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleTitulo.SetFont(oFontTitulo)
        '===============================
        Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleNumber.BorderRight = CellBorderType.THIN
        styleNumber.BorderBottom = CellBorderType.THIN
        styleNumber.BorderLeft = CellBorderType.THIN
        styleNumber.BorderTop = CellBorderType.THIN

        styleNumber.SetFont(oFontFiltro)
        styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
        styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleMonto.BorderRight = CellBorderType.THIN
        styleMonto.BorderBottom = CellBorderType.THIN
        styleMonto.BorderLeft = CellBorderType.THIN
        styleMonto.BorderTop = CellBorderType.THIN
        styleMonto.SetFont(oFontFiltro)
        styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
        styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFecha.BorderRight = CellBorderType.THIN
        styleFecha.BorderBottom = CellBorderType.THIN
        styleFecha.BorderLeft = CellBorderType.THIN
        styleFecha.BorderTop = CellBorderType.THIN
        styleFecha.SetFont(oFontFiltro)
        styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND
        '============================================
        Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
        'create the anchor
        Dim anchor As HSSFClientAnchor
        anchor = New HSSFClientAnchor(0, 0, 0, 50, 0, 0, _
         0, 0)
        If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
          'load the picture and get the picture index in the workbook
          Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
          'Reset the image to the original size.
          ' picture.Resize()
          picture.Resize(0.5)
        End If
        If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
          'load the picture and get the picture index in the workbook
          Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
          'Reset the image to the original size.
          picture.Resize(0.5)
        End If
        Dim rowActual As Integer = 4
        '===============Titulo
        objWorkSheet.CreateRow(rowActual).Height = 100 * 2
        '===========================

        '===========Titulo es centrado segun tamaño de grilla===========
        'objWorkSheet.GetRow(rowActual).GetCell(0).SetCellValue(title)
        'objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual, 0, rowActual, odtg.Columns.Count - 1))
        '==============Verificamos Filtro a utilizar==================

        For intCont As Integer = 0 To title.Count - 1
          Dim strDescripcionTitulo As String()
          strDescripcionTitulo = Split(title.Item(intCont), "\n")

          objWorkSheet.CreateRow(rowActual + 1)
          objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTitulo
          objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

          objWorkSheet.GetRow(rowActual + 1).CreateCell(1)
          ' objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = style
          objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))

          rowActual += 1
        Next
        objWorkSheet.CreateRow(rowActual + 1)
        rowActual += 1
        If filtro Is Nothing Then
          rowActual = rowActual + 1
        Else
          Dim x As Integer = 0
          For Each items As DictionaryEntry In filtro
            x = x + 1
            objWorkSheet.CreateRow(rowActual + x)
            objWorkSheet.GetRow(rowActual + x).CreateCell(0).CellStyle = style
            objWorkSheet.GetRow(rowActual + x).GetCell(0).SetCellValue(items.Key.ToString.Trim)

            objWorkSheet.GetRow(rowActual + x).CreateCell(1).CellStyle = styleFiltro
            objWorkSheet.GetRow(rowActual + x).GetCell(1).SetCellValue(items.Value.ToString.Trim)

          Next
          rowActual = rowActual + filtro.Count + 1
        End If
        '===================Se cargan Las Cabeceras=====================
        Dim flgCabDoble As Boolean = False
        objWorkSheet.CreateRow(rowActual)
        For x As Integer = 0 To odtg.Columns.Count - 1
          '===Estilo===
          objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
          '===Valores===
          Dim valorCab As String = odtg.Columns(x).ColumnName.ToString().Trim()
          Dim valorCabDoble As String()
          valorCabDoble = Split(valorCab, "\n")
          If valorCabDoble.Length = 2 Then
            flgCabDoble = True
          Else
            flgCabDoble = False
          End If
          objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
        Next
        rowActual = rowActual + 1
        If flgCabDoble = True Then
          '==========Limpiamos los Cells Repetidos en el Row Anterior=========
          For x As Integer = 0 To odtg.Columns.Count - 1
            If x < odtg.Columns.Count - 1 Then
              If objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x + 1).StringCellValue Then
                Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                objWorkSheet.GetRow(rowActual - 1).GetCell(x + 1).SetCellValue("")
                '===Union===
                Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + 1)
                objWorkSheet.AddMergedRegion(Region)
              End If
            End If
          Next
          '===================================================================
          objWorkSheet.CreateRow(rowActual)
          For x As Integer = 0 To odtg.Columns.Count - 1
            '===Estilo===
            objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
            '===Valores===
            Dim valorCab As String = odtg.Columns(x).ColumnName.ToString().Trim()
            Dim valorCabDoble As String()
            valorCabDoble = Split(valorCab, "\n")
            If valorCabDoble.Length = 2 Then
              objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
            End If
          Next
          rowActual = rowActual + 1
        End If
        '===================Se Carga El Detalle============================
        For i As Integer = 0 To odtg.Rows.Count - 1
          objWorkSheet.CreateRow(i + rowActual)
          For x As Integer = 0 To odtg.Columns.Count - 1
            Dim celda As String = odtg.Rows(i).Item(x).ToString().Trim()
            If IsNumeric(celda) Then
              If celda.Contains(".") Then
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda))
              Else
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda))
              End If
            Else
              If IsDate(celda) Then
                celda = String.Format("{0:d}", CDate(celda))
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
              Else
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(odtg.Rows(i).Item(x).ToString().Trim())
                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
              End If
            End If

          Next
        Next

        Dim cIniV As String
                Dim cFinV As String
                'cIniV = "F" & (12).ToString
                'cFinV = "F" & (12 + odtg.Rows.Count - 1).ToString
                cIniV = "J" & (12).ToString
                cFinV = "J" & (12 + odtg.Rows.Count - 1).ToString
        Dim dblTotalBultos As String = "SUM(" & cIniV & ":" & cFinV & ")"
        objWorkSheet.CreateRow(rowActual + odtg.Rows.Count)
                objWorkSheet.GetRow(rowActual + odtg.Rows.Count).CreateCell(9).CellStyle = styleMonto
                objWorkSheet.GetRow(rowActual + odtg.Rows.Count).GetCell(9).CellFormula = (dblTotalBultos)

                'cIniV = "G" & (12).ToString
                'cFinV = "G" & (12 + odtg.Rows.Count - 1).ToString
                cIniV = "K" & (12).ToString
                cFinV = "K" & (12 + odtg.Rows.Count - 1).ToString
        Dim sumKg As String = "SUM(" & cIniV & ":" & cFinV & ")"
                objWorkSheet.GetRow(rowActual + odtg.Rows.Count).CreateCell(10).CellStyle = styleMonto
                objWorkSheet.GetRow(rowActual + odtg.Rows.Count).GetCell(10).CellFormula = (sumKg)


        '    objWorkSheet.GroupRow(rowActual, rowActual + odtg.Rows.Count)
        '===================================================================
        For z As Integer = 0 To odtg.Columns.Count - 1
          objWorkSheet.AutoSizeColumn(z, True)
        Next
      Next
      objWorkBook.Write(fs)
      fs.Close()
      AbrirDocumento(archivo)
    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try

  End Sub


    ''' <summary>
    '''Exportar Excel con Titulo
    ''' </summary>
    ''' <param name="oDs"></param>
    ''' <remarks></remarks>
    Public Shared Sub ExportExcelConSeguimientoOC(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1
                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.WrapText = True
                styleFiltro.VerticalAlignment = VerticalAlignment.CENTER

                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.WrapText = True
                styleCab.VerticalAlignment = VerticalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.WrapText = True
                style.VerticalAlignment = VerticalAlignment.CENTER
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.WrapText = True
                stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.WrapText = True
                styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.WrapText = True
                styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN
                styleNumber.WrapText = True
                styleNumber.VerticalAlignment = VerticalAlignment.CENTER
                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.WrapText = True
                styleMonto.VerticalAlignment = VerticalAlignment.CENTER
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.WrapText = True
                styleFecha.VerticalAlignment = VerticalAlignment.CENTER
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                'Style Color Verte
                Dim styleTextoColorVerde As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleTextoColorVerde.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleTextoColorVerde.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleTextoColorVerde.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleTextoColorVerde.BorderRight = CellBorderType.THIN
                styleTextoColorVerde.BorderBottom = CellBorderType.THIN
                styleTextoColorVerde.BorderLeft = CellBorderType.THIN
                styleTextoColorVerde.BorderTop = CellBorderType.THIN
                styleTextoColorVerde.WrapText = True
                styleTextoColorVerde.VerticalAlignment = VerticalAlignment.CENTER
                styleTextoColorVerde.SetFont(oFontFiltro)
                styleTextoColorVerde.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTextoColorVerde.FillPattern = FillPatternType.SOLID_FOREGROUND

                'Style Color Verte
                Dim styleTextoColorAmarillo As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleTextoColorAmarillo.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleTextoColorAmarillo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.YELLOW.index
                styleTextoColorAmarillo.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.YELLOW.index
                styleTextoColorAmarillo.BorderRight = CellBorderType.THIN
                styleTextoColorAmarillo.BorderBottom = CellBorderType.THIN
                styleTextoColorAmarillo.BorderLeft = CellBorderType.THIN
                styleTextoColorAmarillo.BorderTop = CellBorderType.THIN
                styleTextoColorAmarillo.WrapText = True
                styleTextoColorAmarillo.VerticalAlignment = VerticalAlignment.CENTER
                styleTextoColorAmarillo.SetFont(oFontFiltro)
                styleTextoColorAmarillo.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTextoColorAmarillo.FillPattern = FillPatternType.SOLID_FOREGROUND

                'Style Color Verte
                Dim styleTextoColorRojo As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleTextoColorRojo.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleTextoColorRojo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.RED.index
                styleTextoColorRojo.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.RED.index
                styleTextoColorRojo.BorderRight = CellBorderType.THIN
                styleTextoColorRojo.BorderBottom = CellBorderType.THIN
                styleTextoColorRojo.BorderLeft = CellBorderType.THIN
                styleTextoColorRojo.BorderTop = CellBorderType.THIN
                styleTextoColorRojo.WrapText = True
                styleTextoColorRojo.VerticalAlignment = VerticalAlignment.CENTER
                styleTextoColorRojo.SetFont(oFontFiltro)
                styleTextoColorRojo.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTextoColorRojo.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.WrapText = True
                styleLink.VerticalAlignment = VerticalAlignment.CENTER
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                    Next

                    'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                Next

                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        flgCabDoble = True
                    End If
                    objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))

                Next
                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next
                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If
                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

                    objWorkSheet.CreateRow(i + rowActual)

                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1

                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If oDs.Tables(intTable).Columns(x).ColumnName = "CLASIFICACIÓN" Then
                            Select Case celda
                                Case "1"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleTextoColorVerde
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Case "2"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleTextoColorAmarillo
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Case "3"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleTextoColorRojo
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Case Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                            End Select



                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° PED. RANSA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° CONTRATO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                            Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                            Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                            Try
                                link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                If celda <> "" Then
                                    celda = "2"
                                End If
                            Catch : End Try
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda.ToString)
                            'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                End If

                            Else
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                                End If
                            End If
                            '****
                        Else
                            If TypoDato.Equals("System.Date") Then
                                celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            ElseIf TypoDato.Equals("System.DateTime") Then
                                If celda <> "" Then
                                    celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            ElseIf TypoDato.Equals("System.Byte[]") Then


                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                Catch : End Try

                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                                If Not celda Is "" Then
                                    Dim anchorImagen As HSSFClientAnchor
                                    anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                    Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                    imagenAlmacen.LineStyle = 1
                                    imagenAlmacen.Resize()
                                End If
                            Else
                                If celda Is DBNull.Value Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            End If
                        End If

                    Next
                Next

                ''Suma los registros indicados
                Dim strFormula As String
                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If Not listSuma Is Nothing Then
                    For Each items As DictionaryEntry In listSuma
                        strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 7).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 6).ToString & ")"
                        If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMonto
                        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                    Next
                End If
                '===================================================================
                For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Shared Sub ExportExcelConSeguimientoOC_V2(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)


            '  Dim intTable As Integer = 0
            ' For intTable As Integer = 0 To oDs.Tables.Count - 1
            'objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
            objWorkSheet = objWorkBook.CreateSheet("Reporte V2")
            objWorkSheet.CreateRow(3)
            '=============Style======================
            Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

            Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontFiltro.FontHeight = 165


            Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontlink.FontName = "Wingdings"
            oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontlink.FontHeight = 250


            Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220

            Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFont.FontHeight = 170

            Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
            oFontTituloConcepto.FontHeight = 190

            Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
            oFontTituloDescripcion.FontHeight = 170

            styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.BorderRight = CellBorderType.THIN
            styleFiltro.WrapText = True
            styleFiltro.VerticalAlignment = VerticalAlignment.CENTER

            styleFiltro.BorderBottom = CellBorderType.THIN
            styleFiltro.BorderLeft = CellBorderType.THIN
            styleFiltro.BorderTop = CellBorderType.THIN
            styleFiltro.SetFont(oFontFiltro)
            styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

            styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleCab.WrapText = True
            styleCab.VerticalAlignment = VerticalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleCab.BorderRight = CellBorderType.THIN
            styleCab.BorderBottom = CellBorderType.THIN
            styleCab.BorderLeft = CellBorderType.THIN
            styleCab.BorderTop = CellBorderType.THIN
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            style.WrapText = True
            style.VerticalAlignment = VerticalAlignment.CENTER
            style.FillPattern = FillPatternType.SOLID_FOREGROUND
            style.BorderRight = CellBorderType.THIN
            style.BorderBottom = CellBorderType.THIN
            style.BorderLeft = CellBorderType.THIN
            style.BorderTop = CellBorderType.THIN
            style.SetFont(oFont)

            stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
            stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
            stylePorcentaje.BorderRight = CellBorderType.THIN
            stylePorcentaje.BorderBottom = CellBorderType.THIN
            stylePorcentaje.BorderLeft = CellBorderType.THIN
            stylePorcentaje.BorderTop = CellBorderType.THIN
            stylePorcentaje.WrapText = True
            stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
            stylePorcentaje.SetFont(oFontFiltro)

            styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.WrapText = True
            styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloConcepto.SetFont(oFontTituloConcepto)

            styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.WrapText = True
            styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloDescripcion.SetFont(oFontTituloDescripcion)

            '===============================
            Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.BorderRight = CellBorderType.THIN
            styleNumber.BorderBottom = CellBorderType.THIN
            styleNumber.BorderLeft = CellBorderType.THIN
            styleNumber.BorderTop = CellBorderType.THIN
            styleNumber.WrapText = True
            styleNumber.VerticalAlignment = VerticalAlignment.CENTER
            styleNumber.SetFont(oFontFiltro)
            styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
            styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.BorderRight = CellBorderType.THIN
            styleMonto.BorderBottom = CellBorderType.THIN
            styleMonto.BorderLeft = CellBorderType.THIN
            styleMonto.BorderTop = CellBorderType.THIN
            styleMonto.WrapText = True
            styleMonto.VerticalAlignment = VerticalAlignment.CENTER
            styleMonto.SetFont(oFontFiltro)
            styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
            styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFecha.BorderRight = CellBorderType.THIN
            styleFecha.BorderBottom = CellBorderType.THIN
            styleFecha.BorderLeft = CellBorderType.THIN
            styleFecha.BorderTop = CellBorderType.THIN
            styleFecha.WrapText = True
            styleFecha.VerticalAlignment = VerticalAlignment.CENTER
            styleFecha.SetFont(oFontFiltro)
            styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

            'Style Color Verte
            Dim styleTextoColorVerde As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleTextoColorVerde.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleTextoColorVerde.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            styleTextoColorVerde.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            styleTextoColorVerde.BorderRight = CellBorderType.THIN
            styleTextoColorVerde.BorderBottom = CellBorderType.THIN
            styleTextoColorVerde.BorderLeft = CellBorderType.THIN
            styleTextoColorVerde.BorderTop = CellBorderType.THIN
            styleTextoColorVerde.WrapText = True
            styleTextoColorVerde.VerticalAlignment = VerticalAlignment.CENTER
            styleTextoColorVerde.SetFont(oFontFiltro)
            styleTextoColorVerde.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTextoColorVerde.FillPattern = FillPatternType.SOLID_FOREGROUND

            'Style Color Verte
            Dim styleTextoColorAmarillo As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleTextoColorAmarillo.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleTextoColorAmarillo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.YELLOW.index
            styleTextoColorAmarillo.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.YELLOW.index
            styleTextoColorAmarillo.BorderRight = CellBorderType.THIN
            styleTextoColorAmarillo.BorderBottom = CellBorderType.THIN
            styleTextoColorAmarillo.BorderLeft = CellBorderType.THIN
            styleTextoColorAmarillo.BorderTop = CellBorderType.THIN
            styleTextoColorAmarillo.WrapText = True
            styleTextoColorAmarillo.VerticalAlignment = VerticalAlignment.CENTER
            styleTextoColorAmarillo.SetFont(oFontFiltro)
            styleTextoColorAmarillo.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTextoColorAmarillo.FillPattern = FillPatternType.SOLID_FOREGROUND

            'Style Color Verte
            Dim styleTextoColorRojo As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleTextoColorRojo.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleTextoColorRojo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.RED.index
            styleTextoColorRojo.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.RED.index
            styleTextoColorRojo.BorderRight = CellBorderType.THIN
            styleTextoColorRojo.BorderBottom = CellBorderType.THIN
            styleTextoColorRojo.BorderLeft = CellBorderType.THIN
            styleTextoColorRojo.BorderTop = CellBorderType.THIN
            styleTextoColorRojo.WrapText = True
            styleTextoColorRojo.VerticalAlignment = VerticalAlignment.CENTER
            styleTextoColorRojo.SetFont(oFontFiltro)
            styleTextoColorRojo.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTextoColorRojo.FillPattern = FillPatternType.SOLID_FOREGROUND

            Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleLink.BorderRight = CellBorderType.THIN
            styleLink.BorderBottom = CellBorderType.THIN
            styleLink.BorderLeft = CellBorderType.THIN
            styleLink.BorderTop = CellBorderType.THIN
            styleLink.WrapText = True
            styleLink.VerticalAlignment = VerticalAlignment.CENTER
            styleLink.SetFont(oFontlink)
            styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND

            Dim rowActual As Integer = 2
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2
            '===========================

            '===========Titulo es centrado segun tamaño de grilla===========
            If Not strTitulo Is Nothing Then
                objWorkSheet.CreateRow(rowActual + 1)
                For x As Integer = 0 To oDs.Tables(0).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                Next
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(0).Columns.Count - 1))
                rowActual += 1
            End If
            '==============Verificamos Filtro a utilizar==================


            For intCont As Integer = 0 To olstrFiltros.Count - 1
                Dim strDescripcionTitulo As String()
                strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                objWorkSheet.CreateRow(rowActual + 1)
                objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                For x As Integer = 1 To oDs.Tables(0).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                Next

                'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(0).Columns.Count - 1))
                rowActual += 1
            Next

            objWorkSheet.CreateRow(rowActual + 1)

            rowActual += 2
            '===================Se cargan Las Cabeceras=====================

            For intTable As Integer = 0 To oDs.Tables.Count - 1
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        flgCabDoble = True
                    End If
                    objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))

                Next
                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next
                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If
                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

                    objWorkSheet.CreateRow(i + rowActual)

                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1

                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If oDs.Tables(intTable).Columns(x).ColumnName = "CLASIFICACIÓN" Then
                            Select Case celda
                                Case "1"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleTextoColorVerde
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Case "2"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleTextoColorAmarillo
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Case "3"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleTextoColorRojo
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Case Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                            End Select



                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° PED. RANSA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° CONTRATO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                            Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                            Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                            Try
                                link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                If celda <> "" Then
                                    celda = "2"
                                End If
                            Catch : End Try
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda.ToString)
                            'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                End If

                            Else
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                                End If
                            End If
                            '****
                        Else
                            If TypoDato.Equals("System.Date") Then
                                celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            ElseIf TypoDato.Equals("System.DateTime") Then
                                If celda <> "" Then
                                    celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            ElseIf TypoDato.Equals("System.Byte[]") Then


                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                Catch : End Try

                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                                If Not celda Is "" Then
                                    Dim anchorImagen As HSSFClientAnchor
                                    anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                    Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                    imagenAlmacen.LineStyle = 1
                                    imagenAlmacen.Resize()
                                End If
                            Else
                                If celda Is DBNull.Value Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            End If
                        End If

                    Next
                Next
                rowActual = rowActual + oDs.Tables(intTable).Rows.Count + 1

            Next

            ''Suma los registros indicados
            'Dim strFormula As String
            'objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
            'If Not listSuma Is Nothing Then
            '    For Each items As DictionaryEntry In listSuma
            '        strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 7).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 6).ToString & ")"
            '        If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
            '        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMonto
            '        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
            '    Next
            'End If
            '===================================================================
            For z As Integer = 0 To oDs.Tables(0).Columns.Count - 1
                objWorkSheet.AutoSizeColumn(z, True)
            Next

            '============================================logo=================================
            'create the anchor
            Dim anchor As HSSFClientAnchor
            ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
            anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
            If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                picture.Resize(0.5)
            End If
            If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                'Reset the image to the original size.
                picture.Resize(0.5)
            End If
            ''============================================logo=================================

            'Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Shared Sub ExportExcelConSeguimientoOC_V3(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivoTemplate As String = path & "\Template\template.xls" 'path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim archivoReporte As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivoTemplate, IO.FileMode.Open, IO.FileAccess.Read)
            Dim objWorkBook As New NPOIV2.HSSF.UserModel.HSSFWorkbook(fs, True)
            Dim objWorkSheet As NPOIV2.HSSF.UserModel.HSSFSheet
            Dim indexFiltro As Integer = 2
            Dim indexTabla1 As Integer = 7
            Dim indexTabla2 As Integer = 12
            Dim indexTabla3 As Integer = 17
            Dim indexTabla4 As Integer = 24
            Dim strFiltro As String()
            objWorkSheet = objWorkBook.GetSheet("Hoja1")

            'llenamos los filtros
            '--------------------------------------
            For i As Integer = 0 To olstrFiltros.Count - 1
                strFiltro = Split(olstrFiltros.Item(i), "\n")
                objWorkSheet.GetRow(indexFiltro + i).GetCell(1).SetCellValue(strFiltro(1))
            Next

            'llenamos el detalle
            '--------------------------------------
            For row As Integer = 0 To oDs.Tables(0).Rows.Count - 1
                For col As Integer = 0 To oDs.Tables(0).Columns.Count - 1
                    If oDs.Tables(0).Rows(row).Item(col).ToString.Trim <> "" Then
                        objWorkSheet.GetRow(indexTabla1 + row).GetCell(col + 1).SetCellValue(Convert.ToDecimal(Val(oDs.Tables(0).Rows(row).Item(col).ToString.Trim)))
                    End If
                Next
            Next

            For row As Integer = 0 To oDs.Tables(1).Rows.Count - 1
                For col As Integer = 0 To oDs.Tables(1).Columns.Count - 1
                    If oDs.Tables(1).Rows(row).Item(col).ToString <> "" Then
                        objWorkSheet.GetRow(indexTabla2 + row).GetCell(col + 1).SetCellValue(Convert.ToDecimal(Val(oDs.Tables(1).Rows(row).Item(col).ToString.Trim)))
                    End If
                Next
            Next

            For row As Integer = 0 To oDs.Tables(2).Rows.Count - 1
                For col As Integer = 0 To oDs.Tables(2).Columns.Count - 1
                    If oDs.Tables(2).Rows(row).Item(col).ToString <> "" Then
                        objWorkSheet.GetRow(indexTabla3 + row).GetCell(col + 1).SetCellValue(Convert.ToDecimal(Val(oDs.Tables(2).Rows(row).Item(col).ToString.Trim)))
                    End If

                Next
            Next
            'Calculamos la suma verticalmente
            '--------------------------------------
            Dim suma As Decimal = 0
            For row As Integer = 0 To oDs.Tables(0).Rows.Count - 2
                For col As Integer = 0 To oDs.Tables(0).Columns.Count - 1
                    If IsDBNull(oDs.Tables(0).Rows(row).Item(col)) = False Then
                        suma += Convert.ToDecimal(oDs.Tables(0).Rows(row).Item(col).ToString.Trim)
                    End If
                Next

                objWorkSheet.GetRow(indexTabla4 + row).GetCell(1).SetCellValue(suma)
                suma = 0
            Next
            
            Dim indexTabla5 As Integer = 40
            For row As Integer = 0 To oDs.Tables(3).Rows.Count - 1
                For col As Integer = 1 To oDs.Tables(3).Columns.Count - 1
                    If oDs.Tables(3).Rows(row).Item(col).ToString <> "" Then
                        objWorkSheet.GetRow(indexTabla5 + col).GetCell(1).SetCellValue(Convert.ToDecimal(oDs.Tables(3).Rows(row).Item(col).ToString.Trim))
                    End If
                Next
            Next
            objWorkSheet.ForceFormulaRecalculation = True
            Dim file As New FileStream(archivoReporte, FileMode.Create)

            objWorkBook.Write(file)
            file.Close()
            AbrirDocumento(archivoReporte)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
  ''' <summary>
  '''Exportar Excel con Titulo
  ''' </summary>
  ''' <param name="oDs"></param>
  ''' <remarks></remarks>
  Public Shared Sub ExportExcelConTitulos(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
    Try
      Dim path As String = Application.StartupPath.ToString
      Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
      Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

      Dim objWorkBook As New HSSFWorkbook()

      Dim objWorkSheet As New HSSFSheet(objWorkBook)
      For intTable As Integer = 0 To oDs.Tables.Count - 1
        objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
        objWorkSheet.CreateRow(3)
        '=============Style======================
        Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

        Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontFiltro.FontHeight = 165


        Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontlink.FontName = "Wingdings"
        oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontlink.FontHeight = 250


        Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontCab.FontHeight = 220

        Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFont.FontHeight = 170

        Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
        oFontTituloConcepto.FontHeight = 190

        Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
        oFontTituloDescripcion.FontHeight = 170

        styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.WrapText = True
                styleFiltro.VerticalAlignment = VerticalAlignment.CENTER

        styleFiltro.BorderBottom = CellBorderType.THIN
        styleFiltro.BorderLeft = CellBorderType.THIN
        styleFiltro.BorderTop = CellBorderType.THIN
        styleFiltro.SetFont(oFontFiltro)
        styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

        styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.WrapText = True
                styleCab.VerticalAlignment = VerticalAlignment.CENTER
        styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleCab.BorderRight = CellBorderType.THIN
        styleCab.BorderBottom = CellBorderType.THIN
        styleCab.BorderLeft = CellBorderType.THIN
        styleCab.BorderTop = CellBorderType.THIN
        styleCab.SetFont(oFontCab)
        styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.WrapText = True
                style.VerticalAlignment = VerticalAlignment.CENTER
        style.FillPattern = FillPatternType.SOLID_FOREGROUND
        style.BorderRight = CellBorderType.THIN
        style.BorderBottom = CellBorderType.THIN
        style.BorderLeft = CellBorderType.THIN
        style.BorderTop = CellBorderType.THIN
        style.SetFont(oFont)

        stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
        stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
        stylePorcentaje.BorderRight = CellBorderType.THIN
        stylePorcentaje.BorderBottom = CellBorderType.THIN
        stylePorcentaje.BorderLeft = CellBorderType.THIN
        stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.WrapText = True
                stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
        stylePorcentaje.SetFont(oFontFiltro)

        styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.WrapText = True
                styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
        styleTituloConcepto.SetFont(oFontTituloConcepto)

        styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.WrapText = True
                styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
        styleTituloDescripcion.SetFont(oFontTituloDescripcion)

        '===============================
        Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleNumber.BorderRight = CellBorderType.THIN
        styleNumber.BorderBottom = CellBorderType.THIN
        styleNumber.BorderLeft = CellBorderType.THIN
        styleNumber.BorderTop = CellBorderType.THIN
                styleNumber.WrapText = True
                styleNumber.VerticalAlignment = VerticalAlignment.CENTER
        styleNumber.SetFont(oFontFiltro)
        styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
        styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleMonto.BorderRight = CellBorderType.THIN
        styleMonto.BorderBottom = CellBorderType.THIN
        styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.WrapText = True
                styleMonto.VerticalAlignment = VerticalAlignment.CENTER
        styleMonto.SetFont(oFontFiltro)
        styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
        styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFecha.BorderRight = CellBorderType.THIN
        styleFecha.BorderBottom = CellBorderType.THIN
        styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.WrapText = True
                styleFecha.VerticalAlignment = VerticalAlignment.CENTER
        styleFecha.SetFont(oFontFiltro)
        styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

        Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleLink.BorderRight = CellBorderType.THIN
        styleLink.BorderBottom = CellBorderType.THIN
        styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.WrapText = True
                styleLink.VerticalAlignment = VerticalAlignment.CENTER
        styleLink.SetFont(oFontlink)
        styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND

        Dim rowActual As Integer = 2
        '===============Titulo
        objWorkSheet.CreateRow(rowActual).Height = 100 * 2
        '===========================

        '===========Titulo es centrado segun tamaño de grilla===========
        If Not strTitulo Is Nothing Then
          objWorkSheet.CreateRow(rowActual + 1)
          For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
            objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
          Next
          objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
          objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
          rowActual += 1
        End If
        '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                    Next

                    'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                Next

        objWorkSheet.CreateRow(rowActual + 1)

        rowActual += 2
        '===================Se cargan Las Cabeceras=====================
        Dim flgCabDoble As Boolean = False
        objWorkSheet.CreateRow(rowActual)
        For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
          '===Estilo===
          objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
          '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
          Dim valorCabDoble As String()
          valorCabDoble = Split(valorCab, "\n")
          If valorCabDoble.Length = 2 Then
            flgCabDoble = True
          End If
          objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))

        Next
        rowActual = rowActual + 1
        Dim intRepite As Integer = 0
        If flgCabDoble = True Then
          '==========Limpiamos los Cells Repetidos en el Row Anterior=========
          For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
            'If x < oDs.Tables(intTable).Columns.Count - 1 Then
            intRepite = 0
            For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
              If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                intRepite = intRepite + 1
              End If
            Next
            If intRepite > 1 Then
              intRepite = intRepite - 1
              Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
              '===Union===
              Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
              objWorkSheet.AddMergedRegion(Region)
              x = x + intRepite
            End If
            ' End If
          Next
          '===================================================================
          objWorkSheet.CreateRow(rowActual)
          For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
            '===Estilo===
            objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
            '===Valores===
            Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
            Dim valorCabDoble As String()
            valorCabDoble = Split(valorCab, "\n")
            If valorCabDoble.Length = 2 Then
              objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
            End If
          Next
          rowActual = rowActual + 1
        End If
        '===================Se Carga El Detalle============================
        For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

          objWorkSheet.CreateRow(i + rowActual)

          For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1

            Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
            Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

            If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
              objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
              objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "CLIENTE" And TypoDato = "System.Decimal" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "INGRESOS \n CANTIDAD" Or oDs.Tables(intTable).Columns(x).ColumnName = "INGRESOS \n PESO" Or oDs.Tables(intTable).Columns(x).ColumnName = "INGRESOS \n VOLUMEN" Then

                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDecimal(celda))
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
              objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° PED. RANSA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° CONTRATO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

              Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
              Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
              Try
                link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                If celda <> "" Then
                  celda = "2"
                End If
              Catch : End Try
              objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
              objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
              objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda.ToString)
              'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
              objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
              objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

            ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

              If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                If celda Is "" Then
                  objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                Else
                  objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                End If

              Else
                If celda Is "" Then
                  objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                  objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                Else
                  objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                  objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                End If
                            End If
                            '****
                        Else
                            If TypoDato.Equals("System.Date") Then
                                celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            ElseIf TypoDato.Equals("System.DateTime") Then
                                If celda <> "" Then
                                    celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            ElseIf TypoDato.Equals("System.Byte[]") Then


                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                Catch : End Try

                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                                If Not celda Is "" Then
                                    Dim anchorImagen As HSSFClientAnchor
                                    anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                    Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                    imagenAlmacen.LineStyle = 1
                                    imagenAlmacen.Resize()
                                End If
                            Else
                                If celda Is DBNull.Value Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            End If
                        End If
                    Next
                Next


        ''Suma los registros indicados
        Dim strFormula As String
        objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
        If Not listSuma Is Nothing Then
          For Each items As DictionaryEntry In listSuma
            strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 7).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 6).ToString & ")"
            If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMonto
            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
          Next
        End If
        '===================================================================
        For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
          objWorkSheet.AutoSizeColumn(z, True)
        Next

        '============================================logo=================================
        'create the anchor
        Dim anchor As HSSFClientAnchor
        ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
        anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
        If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
          'load the picture and get the picture index in the workbook
          Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
          picture.Resize(0.5)
        End If
        If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
          'load the picture and get the picture index in the workbook
          Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
          'Reset the image to the original size.
          picture.Resize(0.5)
        End If
        ''============================================logo=================================

      Next
      objWorkBook.Write(fs)
      fs.Close()
      AbrirDocumento(archivo)
    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try

    End Sub

    'CSR-HUNDRED-INICIO
    Public Shared Sub ExportExcelInventarioBultosXPedido(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing, Optional ByVal olstrCombFilas As List(Of String) = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1
                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.WrapText = True
                styleFiltro.VerticalAlignment = VerticalAlignment.CENTER

                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.WrapText = True
                styleCab.VerticalAlignment = VerticalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.WrapText = True
                style.VerticalAlignment = VerticalAlignment.CENTER
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)
 
                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.WrapText = True
                styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.WrapText = True
                styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN
                styleNumber.WrapText = True
                styleNumber.VerticalAlignment = VerticalAlignment.CENTER
                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.WrapText = True
                styleMonto.VerticalAlignment = VerticalAlignment.CENTER
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleNumberIzquierda As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumberIzquierda.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleNumberIzquierda.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumberIzquierda.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumberIzquierda.BorderRight = CellBorderType.THIN
                styleNumberIzquierda.BorderBottom = CellBorderType.THIN
                styleNumberIzquierda.BorderLeft = CellBorderType.THIN
                styleNumberIzquierda.BorderTop = CellBorderType.THIN
                styleNumberIzquierda.WrapText = True
                styleNumberIzquierda.VerticalAlignment = VerticalAlignment.CENTER
                styleNumberIzquierda.SetFont(oFontFiltro)
                styleNumberIzquierda.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumberIzquierda.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                '===============================
                Dim styleNumberCenter As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumberCenter.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleNumberCenter.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumberCenter.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumberCenter.BorderRight = CellBorderType.THIN
                styleNumberCenter.BorderBottom = CellBorderType.THIN
                styleNumberCenter.BorderLeft = CellBorderType.THIN
                styleNumberCenter.BorderTop = CellBorderType.THIN
                styleNumberCenter.WrapText = True
                styleNumberCenter.VerticalAlignment = VerticalAlignment.CENTER
                styleNumberCenter.SetFont(oFontFiltro)
                styleNumberCenter.DataFormat = HSSFDataFormat.GetBuiltinFormat("0")
                styleNumberCenter.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================

                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.WrapText = True
                styleFecha.VerticalAlignment = VerticalAlignment.CENTER
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.WrapText = True
                styleLink.VerticalAlignment = VerticalAlignment.CENTER
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                    Next

                    'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                Next

                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        flgCabDoble = True
                    End If
                    objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))

                Next
                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next
                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If

                Dim RowInicioTCMPCL As Integer = rowActual
                Dim RowFinTCMPCL As Integer = 0
                Dim RowInicioTIPOALM As Integer = rowActual
                Dim RowFinTIPOALM As Integer = 0
                Dim RowInicioALMACEN As Integer = rowActual
                Dim RowFinALMACEN As Integer = 0

                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

                    objWorkSheet.CreateRow(i + rowActual)

                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1

                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If oDs.Tables(intTable).Columns(x).ColumnName = "COD. CLIENTE" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "NRO. ITEM" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "SALDO CANTIDAD" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "NRO. PEDIDO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "BULTO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumberIzquierda
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "CANTIDAD PEDIDO PENDIENTE" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "ORDEN COMPRA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumberIzquierda
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "COD. MATERIAL" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumberIzquierda
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "NRO. SECUENCIA PEDIDO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumberCenter
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "FECHA INGRESO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)




                        ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                End If

                            Else
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                                End If
                            End If
                            '****
                        Else
                            If TypoDato.Equals("System.Date") Then
                                celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            ElseIf TypoDato.Equals("System.DateTime") Then
                                If celda <> "" Then
                                    celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            ElseIf TypoDato.Equals("System.Byte[]") Then


                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                Catch : End Try

                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                                If Not celda Is "" Then
                                    Dim anchorImagen As HSSFClientAnchor
                                    anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                    Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                    imagenAlmacen.LineStyle = 1
                                    imagenAlmacen.Resize()
                                End If
                            Else
                                If celda Is DBNull.Value Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            End If
                        End If
                        Dim lstrTemp As String = ""

                    Next
                Next

                Dim PosFinalColTCMPCL As Integer = 0
                Dim PosFinalColTIPALM As Integer = 0
                Dim PosFinalColALMACEN As Integer = 0

                If oDs.Tables(intTable).TableName.Contains("Resumen") Then
                    Dim ColClienteINI As String = ""
                    Dim ColTipAlmacenINI As String = ""
                    Dim ColAlmacenINI As String = ""

                    If (oDs.Tables(intTable).Rows.Count > 0) Then
                        ColClienteINI = oDs.Tables(intTable).Rows(0).Item("RAZON SOCIAL").ToString.Trim
                        ColTipAlmacenINI = oDs.Tables(intTable).Rows(0).Item("RAZON SOCIAL").ToString.Trim & "-" & oDs.Tables(intTable).Rows(0).Item("TIPO ALMACEN").ToString.Trim
                        ColAlmacenINI = oDs.Tables(intTable).Rows(0).Item("RAZON SOCIAL").ToString.Trim & "-" & oDs.Tables(intTable).Rows(0).Item("TIPO ALMACEN").ToString.Trim & "-" & oDs.Tables(intTable).Rows(0).Item("ALMACEN").ToString.Trim
                    End If
                    Dim ColClienteACT As String = ""
                    Dim ColTipAlmacenACT As String = ""
                    Dim ColAlmacenACT As String = ""
                    Dim ColumnaName As String = ""
                    For c As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        ColumnaName = oDs.Tables(intTable).Columns(c).ColumnName

                        If ColumnaName = "RAZON SOCIAL" Then
                            For r As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                                ColClienteACT = ("" & oDs.Tables(intTable).Rows(r).Item("RAZON SOCIAL")).ToString.Trim
                                If (ColClienteACT <> ColClienteINI) Then
                                    RowFinTCMPCL = (r + rowActual) - 1
                                    PosFinalColTCMPCL = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioTCMPCL, c, RowFinTCMPCL, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioTCMPCL = RowFinTCMPCL + 1
                                    ColClienteINI = ColClienteACT
                                ElseIf r = oDs.Tables(intTable).Rows.Count - 1 AndAlso ColClienteACT = ColClienteINI Then
                                    RowFinTCMPCL = (r + rowActual)
                                    PosFinalColTCMPCL = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioTCMPCL, c, RowFinTCMPCL, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioTCMPCL = RowFinTCMPCL + 1
                                    ColClienteINI = ColClienteACT

                                End If
                            Next
                        ElseIf ColumnaName = "TIPO ALMACEN" Then
                            For r As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                                ColTipAlmacenACT = (oDs.Tables(intTable).Rows(r).Item("RAZON SOCIAL").ToString.Trim & "-" & oDs.Tables(intTable).Rows(r).Item("TIPO ALMACEN")).ToString.Trim
                                If (ColTipAlmacenACT <> ColTipAlmacenINI) Then
                                    RowFinTIPOALM = (r + rowActual) - 1
                                    PosFinalColTIPALM = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioTIPOALM, c, RowFinTIPOALM, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioTIPOALM = RowFinTIPOALM + 1
                                    ColTipAlmacenINI = ColTipAlmacenACT
                                ElseIf r = oDs.Tables(intTable).Rows.Count - 1 AndAlso ColTipAlmacenACT = ColTipAlmacenINI Then
                                    RowFinTIPOALM = (r + rowActual)
                                    PosFinalColTIPALM = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioTIPOALM, c, RowFinTIPOALM, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioTIPOALM = RowFinTIPOALM + 1
                                    ColTipAlmacenINI = ColTipAlmacenACT
                                End If
                            Next
                        ElseIf ColumnaName = "ALMACEN" Then
                            For r As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                                ColAlmacenACT = (oDs.Tables(intTable).Rows(r).Item("RAZON SOCIAL").ToString.Trim & "-" & oDs.Tables(intTable).Rows(r).Item("TIPO ALMACEN")).ToString.Trim & "-" & oDs.Tables(intTable).Rows(r).Item("ALMACEN").ToString.Trim
                                If (ColAlmacenACT <> ColAlmacenINI) Then
                                    RowFinALMACEN = (r + rowActual) - 1
                                    PosFinalColALMACEN = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioALMACEN, c, RowFinALMACEN, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioALMACEN = RowFinALMACEN + 1
                                    ColAlmacenINI = ColAlmacenACT
                                ElseIf r = oDs.Tables(intTable).Rows.Count - 1 AndAlso ColAlmacenACT = ColAlmacenINI Then
                                    RowFinALMACEN = (r + rowActual)
                                    PosFinalColALMACEN = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioALMACEN, c, RowFinALMACEN, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioALMACEN = RowFinALMACEN + 1
                                    ColAlmacenINI = ColAlmacenACT
                                End If
                            Next
                        End If
                    Next
                End If


                Dim strFormula As String
                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If oDs.Tables(intTable).TableName.Contains("Resumen") Then

                Else
                    If Not listSuma Is Nothing Then
                        For Each items As DictionaryEntry In listSuma
                            strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 7).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 6).ToString & ")"
                            If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMonto
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                        Next
                    End If
                End If

                '===================================================================
                For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
    'CSR-HUNDRED-FIN

    Public Shared Sub ExportExcelConTitulosAlmacen(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing, Optional ByVal olstrCombFilas As List(Of String) = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1
                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.WrapText = True
                styleFiltro.VerticalAlignment = VerticalAlignment.CENTER

                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.WrapText = True
                styleCab.VerticalAlignment = VerticalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.WrapText = True
                style.VerticalAlignment = VerticalAlignment.CENTER
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.WrapText = True
                stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.WrapText = True
                styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.WrapText = True
                styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN
                styleNumber.WrapText = True
                styleNumber.VerticalAlignment = VerticalAlignment.CENTER
                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.WrapText = True
                styleMonto.VerticalAlignment = VerticalAlignment.CENTER
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.WrapText = True
                styleFecha.VerticalAlignment = VerticalAlignment.CENTER
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.WrapText = True
                styleLink.VerticalAlignment = VerticalAlignment.CENTER
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                    Next

                    'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                Next

                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        flgCabDoble = True
                    End If
                    objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))

                Next
                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next
                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If

                Dim RowInicioTCMPCL As Integer = rowActual
                Dim RowFinTCMPCL As Integer = 0
                Dim RowInicioTIPOALM As Integer = rowActual
                Dim RowFinTIPOALM As Integer = 0
                Dim RowInicioALMACEN As Integer = rowActual
                Dim RowFinALMACEN As Integer = 0

                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

                    objWorkSheet.CreateRow(i + rowActual)

                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1

                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "CLIENTE" And TypoDato = "System.Decimal" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "INGRESOS \n CANTIDAD" Or oDs.Tables(intTable).Columns(x).ColumnName = "INGRESOS \n PESO" Or oDs.Tables(intTable).Columns(x).ColumnName = "INGRESOS \n VOLUMEN" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDecimal(celda))
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° PED. RANSA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° CONTRATO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                            Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                            Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                            Try
                                link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                If celda <> "" Then
                                    celda = "2"
                                End If
                            Catch : End Try
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda.ToString)
                            'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                End If

                            Else
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                                End If
                            End If
                            '****
                        Else
                            If TypoDato.Equals("System.Date") Then
                                celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            ElseIf TypoDato.Equals("System.DateTime") Then
                                If celda <> "" Then
                                    celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            ElseIf TypoDato.Equals("System.Byte[]") Then


                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                Catch : End Try

                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                                If Not celda Is "" Then
                                    Dim anchorImagen As HSSFClientAnchor
                                    anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                    Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                    imagenAlmacen.LineStyle = 1
                                    imagenAlmacen.Resize()
                                End If
                            Else
                                If celda Is DBNull.Value Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            End If
                        End If
                        Dim lstrTemp As String = ""
                        'If olstrCombFilas IsNot Nothing Then
                        '    lstrTemp = oDs.Tables(intTable).Rows(0).Item(x).ToString.Trim ' odtDataSource.Rows(0).Item(ListNameDelFilDuplic(FilaTable)).ToString.Trim
                        'End If



                    Next
                Next

                'Dim RowInicioTCMPCL As Integer = rowActual
                'Dim RowFinTCMPCL As Integer = 0
                'Dim RowInicioTIPOALM As Integer = rowActual
                'Dim RowInicioTIPOALM As Integer = 0
                'Dim RowInicioALMACEN As Integer = rowActual
                'Dim RowFinALMACEN As Integer = 0
                Dim PosFinalColTCMPCL As Integer = 0
                Dim PosFinalColTIPALM As Integer = 0
                Dim PosFinalColALMACEN As Integer = 0

                If oDs.Tables(intTable).TableName.Contains("Resumen") Then
                    Dim ColClienteINI As String = ""
                    Dim ColTipAlmacenINI As String = ""
                    Dim ColAlmacenINI As String = ""

                    If (oDs.Tables(intTable).Rows.Count > 0) Then
                        ColClienteINI = oDs.Tables(intTable).Rows(0).Item("RAZON SOCIAL").ToString.Trim
                        ColTipAlmacenINI = oDs.Tables(intTable).Rows(0).Item("RAZON SOCIAL").ToString.Trim & "-" & oDs.Tables(intTable).Rows(0).Item("TIPO ALMACEN").ToString.Trim
                        ColAlmacenINI = oDs.Tables(intTable).Rows(0).Item("RAZON SOCIAL").ToString.Trim & "-" & oDs.Tables(intTable).Rows(0).Item("TIPO ALMACEN").ToString.Trim & "-" & oDs.Tables(intTable).Rows(0).Item("ALMACEN").ToString.Trim
                    End If
                    Dim ColClienteACT As String = ""
                    Dim ColTipAlmacenACT As String = ""
                    Dim ColAlmacenACT As String = ""
                    'Dim lstrTemp As Integer = 0
                    Dim ColumnaName As String = ""
                    'If olstrCombFilas IsNot Nothing Then
                    'For intRows As Integer = 0 To olstrCombFilas.Count - 1
                    For c As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'lstrTemp = oDs.Tables(intTable).Rows(0).Item(olstrCombFilas.Item(intRows)).ToString.Trim
                        ColumnaName = oDs.Tables(intTable).Columns(c).ColumnName

                        If ColumnaName = "RAZON SOCIAL" Then
                            For r As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                                ColClienteACT = ("" & oDs.Tables(intTable).Rows(r).Item("RAZON SOCIAL")).ToString.Trim
                                If (ColClienteACT <> ColClienteINI) Then
                                    RowFinTCMPCL = (r + rowActual) - 1
                                    PosFinalColTCMPCL = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioTCMPCL, c, RowFinTCMPCL, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioTCMPCL = RowFinTCMPCL + 1
                                    ColClienteINI = ColClienteACT
                                ElseIf r = oDs.Tables(intTable).Rows.Count - 1 AndAlso ColClienteACT = ColClienteINI Then
                                    RowFinTCMPCL = (r + rowActual)
                                    PosFinalColTCMPCL = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioTCMPCL, c, RowFinTCMPCL, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioTCMPCL = RowFinTCMPCL + 1
                                    ColClienteINI = ColClienteACT

                                End If
                            Next
                        ElseIf ColumnaName = "TIPO ALMACEN" Then
                            For r As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                                ColTipAlmacenACT = (oDs.Tables(intTable).Rows(r).Item("RAZON SOCIAL").ToString.Trim & "-" & oDs.Tables(intTable).Rows(r).Item("TIPO ALMACEN")).ToString.Trim
                                If (ColTipAlmacenACT <> ColTipAlmacenINI) Then
                                    RowFinTIPOALM = (r + rowActual) - 1
                                    PosFinalColTIPALM = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioTIPOALM, c, RowFinTIPOALM, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioTIPOALM = RowFinTIPOALM + 1
                                    ColTipAlmacenINI = ColTipAlmacenACT
                                ElseIf r = oDs.Tables(intTable).Rows.Count - 1 AndAlso ColTipAlmacenACT = ColTipAlmacenINI Then
                                    RowFinTIPOALM = (r + rowActual)
                                    PosFinalColTIPALM = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioTIPOALM, c, RowFinTIPOALM, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioTIPOALM = RowFinTIPOALM + 1
                                    ColTipAlmacenINI = ColTipAlmacenACT
                                End If
                            Next
                        ElseIf ColumnaName = "ALMACEN" Then
                            For r As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                                ColAlmacenACT = (oDs.Tables(intTable).Rows(r).Item("RAZON SOCIAL").ToString.Trim & "-" & oDs.Tables(intTable).Rows(r).Item("TIPO ALMACEN")).ToString.Trim & "-" & oDs.Tables(intTable).Rows(r).Item("ALMACEN").ToString.Trim
                                If (ColAlmacenACT <> ColAlmacenINI) Then
                                    RowFinALMACEN = (r + rowActual) - 1
                                    PosFinalColALMACEN = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioALMACEN, c, RowFinALMACEN, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioALMACEN = RowFinALMACEN + 1
                                    ColAlmacenINI = ColAlmacenACT
                                ElseIf r = oDs.Tables(intTable).Rows.Count - 1 AndAlso ColAlmacenACT = ColAlmacenINI Then
                                    RowFinALMACEN = (r + rowActual)
                                    PosFinalColALMACEN = (r + rowActual) - 1
                                    Dim Region As New NPOI.SS.Util.Region(RowInicioALMACEN, c, RowFinALMACEN, c)
                                    objWorkSheet.AddMergedRegion(Region)
                                    RowInicioALMACEN = RowFinALMACEN + 1
                                    ColAlmacenINI = ColAlmacenACT
                                End If
                            Next
                        End If
                    Next
                    'Next
                End If



                'Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                'objWorkSheet.AddMergedRegion(Region)
                'End If
                ''Suma los registros indicados
                Dim strFormula As String
                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If oDs.Tables(intTable).TableName.Contains("Resumen") Then

                Else
                    If Not listSuma Is Nothing Then
                        For Each items As DictionaryEntry In listSuma
                            strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 7).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 6).ToString & ")"
                            If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMonto
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                        Next
                    End If
                End If

                '===================================================================
                For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub


  ''' <summary>
  '''Exportar Excel con Titulo
  ''' </summary>
  ''' <param name="oDs"></param>
  ''' <remarks></remarks>
    Public Shared Sub ExportExcelConsultaProvicionVenta(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1
                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim styleDinamico As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165

                Dim oFontFiltroResumen As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltroResumen.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltroResumen.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontFiltroResumen.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFiltroResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFiltroResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFiltroResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltroResumen.BorderRight = CellBorderType.THIN
                styleFiltroResumen.BorderBottom = CellBorderType.THIN
                styleFiltroResumen.BorderLeft = CellBorderType.THIN
                styleFiltroResumen.BorderTop = CellBorderType.THIN
                styleFiltroResumen.SetFont(oFontFiltroResumen)
                styleFiltroResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltroResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN

                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00") 'cambio 
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoTotal As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoTotal.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoTotal.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.BorderRight = CellBorderType.THIN
                styleMontoTotal.BorderBottom = CellBorderType.THIN
                styleMontoTotal.BorderLeft = CellBorderType.THIN
                styleMontoTotal.BorderTop = CellBorderType.THIN
                styleMontoTotal.SetFont(oFontFiltroResumen)
                styleMontoTotal.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoTotal.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleMontoResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoResumen.BorderRight = CellBorderType.THIN
                styleMontoResumen.BorderBottom = CellBorderType.THIN
                styleMontoResumen.BorderLeft = CellBorderType.THIN
                styleMontoResumen.BorderTop = CellBorderType.THIN
                styleMontoResumen.SetFont(oFontFiltroResumen)
                styleMontoResumen.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoResumen.FillPattern = FillPatternType.SOLID_FOREGROUND



                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFechaResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFechaResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFechaResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFechaResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFechaResumen.BorderRight = CellBorderType.THIN
                styleFechaResumen.BorderBottom = CellBorderType.THIN
                styleFechaResumen.BorderLeft = CellBorderType.THIN
                styleFechaResumen.BorderTop = CellBorderType.THIN
                styleFechaResumen.SetFont(oFontFiltroResumen)
                styleFechaResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFechaResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND







                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                            Continue For
                        End If
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x + 1).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1



                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")

                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(0))
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(2).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 2, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1
                Next
                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    If oDs.Tables(intTable).Columns(x).ColumnName <> "ESTADOOP" Then
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = Replace(oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim(), Chr(34), "")
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            flgCabDoble = True
                        End If
                        objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(0))
                    End If
                Next


                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If
                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                    objWorkSheet.CreateRow(i + rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                        'Continue For
                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If oDs.Tables(intTable).Rows(i).Item(0).ToString.Trim.Equals("-1") Then
                            'objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                            'objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = style
                            Select Case TypoDato
                                Case "System.Decimal", "System.Int32", "System.Double", "System.Integer"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMontoResumen
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If
                                Case "System.Date"
                                    celda = String.Format("{0:d}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFechaResumen
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)

                                Case "System.DateTime"
                                    celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                Case Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltroResumen
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltroResumen
                                    End If

                            End Select
                        Else

                            If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                    If celda <> "" Then
                                        celda = "2"
                                    End If
                                Catch : End Try
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleLink
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda.ToString)
                                'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "OPERACIÓN" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "NRO. REVISIÓN" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "N° FACTURA" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                                If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMonto
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If

                                Else
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToInt64(Val(celda)))
                                    End If
                                End If


                            Else
                                If TypoDato.Equals("System.Date") Then
                                    celda = String.Format("{0:d}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                ElseIf TypoDato.Equals("System.Byte[]") Then


                                    Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                    Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                    Try
                                        link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                    Catch : End Try

                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).Hyperlink = link
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleLink
                                    If Not celda Is "" Then
                                        Dim anchorImagen As HSSFClientAnchor
                                        anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                        Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                        imagenAlmacen.LineStyle = 1
                                        imagenAlmacen.Resize()
                                    End If

                                ElseIf TypoDato.Equals("System.DateTime") Then
                                    celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    End If

                                End If
                            End If
                        End If

                    Next
                Next

                ''Suma los registros indicados
                Dim strFormula As String

                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If Not listSuma Is Nothing Then
                    For Each items As DictionaryEntry In listSuma
                        If Math.Ceiling(items.Key) = (intTable + 2) Then
                            strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ")"
                            If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMontoTotal
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                        End If
                    Next
                End If
                '===================================================================
                For z As Integer = 1 To oDs.Tables(intTable).Columns.Count
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 1, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub


    Public Shared Sub ExportarPrueba(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1
                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim styleDinamico As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165

                Dim oFontFiltroResumen As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltroResumen.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltroResumen.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontFiltroResumen.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFiltroResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFiltroResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFiltroResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltroResumen.BorderRight = CellBorderType.THIN
                styleFiltroResumen.BorderBottom = CellBorderType.THIN
                styleFiltroResumen.BorderLeft = CellBorderType.THIN
                styleFiltroResumen.BorderTop = CellBorderType.THIN
                styleFiltroResumen.SetFont(oFontFiltroResumen)
                styleFiltroResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltroResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN

                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoTotal As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoTotal.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoTotal.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.BorderRight = CellBorderType.THIN
                styleMontoTotal.BorderBottom = CellBorderType.THIN
                styleMontoTotal.BorderLeft = CellBorderType.THIN
                styleMontoTotal.BorderTop = CellBorderType.THIN
                styleMontoTotal.SetFont(oFontFiltroResumen)
                styleMontoTotal.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoTotal.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleMontoResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoResumen.BorderRight = CellBorderType.THIN
                styleMontoResumen.BorderBottom = CellBorderType.THIN
                styleMontoResumen.BorderLeft = CellBorderType.THIN
                styleMontoResumen.BorderTop = CellBorderType.THIN
                styleMontoResumen.SetFont(oFontFiltroResumen)
                styleMontoResumen.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoResumen.FillPattern = FillPatternType.SOLID_FOREGROUND



                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFechaResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFechaResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFechaResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFechaResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFechaResumen.BorderRight = CellBorderType.THIN
                styleFechaResumen.BorderBottom = CellBorderType.THIN
                styleFechaResumen.BorderLeft = CellBorderType.THIN
                styleFechaResumen.BorderTop = CellBorderType.THIN
                styleFechaResumen.SetFont(oFontFiltroResumen)
                styleFechaResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFechaResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND







                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                            Continue For
                        End If
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x + 1).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1

                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")

                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(0))
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(2).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    rowActual += 1
                Next
                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    If oDs.Tables(intTable).Columns(x).ColumnName <> "ESTADOOP" Then
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = Replace(oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim(), Chr(34), "")
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            flgCabDoble = True
                        End If
                        objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(0))
                    End If
                Next


                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If
                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                    objWorkSheet.CreateRow(i + rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                        'Continue For
                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If oDs.Tables(intTable).Rows(i).Item(0).ToString.Trim.Equals("-1") Then
                            'objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                            'objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = style
                            Select Case TypoDato
                                Case "System.Decimal", "System.Int32", "System.Double", "System.Integer"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMontoResumen
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If
                                Case "System.Date"
                                    celda = String.Format("{0:d}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFechaResumen
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)

                                Case "System.DateTime"
                                    celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                Case Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltroResumen
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltroResumen
                                    End If

                            End Select
                        Else

                            If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                    If celda <> "" Then
                                        celda = "2"
                                    End If
                                Catch : End Try
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleLink
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda.ToString)
                                'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "OPERACIÓN" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "NRO. REVISIÓN" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "N° FACTURA" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                                If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMonto
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If

                                Else
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToInt64(Val(celda)))
                                    End If
                                End If


                            Else
                                If TypoDato.Equals("System.Date") Then
                                    celda = String.Format("{0:d}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                ElseIf TypoDato.Equals("System.Byte[]") Then


                                    Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                    Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                    Try
                                        link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                    Catch : End Try

                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).Hyperlink = link
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleLink
                                    If Not celda Is "" Then
                                        Dim anchorImagen As HSSFClientAnchor
                                        anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                        Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                        imagenAlmacen.LineStyle = 1
                                        imagenAlmacen.Resize()
                                    End If

                                ElseIf TypoDato.Equals("System.DateTime") Then
                                    celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    End If

                                End If
                            End If
                        End If

                    Next
                Next

                ''Suma los registros indicados
                Dim strFormula As String

                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)

             
                strFormula = "SERIES(,Reporte1!$B$7:$B$9,Reporte1!$C$7:$C$9,1)"
                If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(20 - 1).CellStyle = styleTituloDescripcion
                objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(20 - 1).CellFormula = (strFormula)


                '===================================================================
                For z As Integer = 1 To oDs.Tables(intTable).Columns.Count
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 1, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Shared Sub ExportExcelConsultaFacturadasFM(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try

            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1

                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim styleDinamico As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165

                Dim oFontFiltroResumen As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltroResumen.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltroResumen.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontFiltroResumen.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFiltroResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFiltroResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFiltroResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltroResumen.BorderRight = CellBorderType.THIN
                styleFiltroResumen.BorderBottom = CellBorderType.THIN
                styleFiltroResumen.BorderLeft = CellBorderType.THIN
                styleFiltroResumen.BorderTop = CellBorderType.THIN
                styleFiltroResumen.SetFont(oFontFiltroResumen)
                styleFiltroResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltroResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN

                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoTotal As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoTotal.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoTotal.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.BorderRight = CellBorderType.THIN
                styleMontoTotal.BorderBottom = CellBorderType.THIN
                styleMontoTotal.BorderLeft = CellBorderType.THIN
                styleMontoTotal.BorderTop = CellBorderType.THIN
                styleMontoTotal.SetFont(oFontFiltroResumen)
                styleMontoTotal.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoTotal.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleMontoResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoResumen.BorderRight = CellBorderType.THIN
                styleMontoResumen.BorderBottom = CellBorderType.THIN
                styleMontoResumen.BorderLeft = CellBorderType.THIN
                styleMontoResumen.BorderTop = CellBorderType.THIN
                styleMontoResumen.SetFont(oFontFiltroResumen)
                styleMontoResumen.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoResumen.FillPattern = FillPatternType.SOLID_FOREGROUND



                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFechaResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFechaResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFechaResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFechaResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFechaResumen.BorderRight = CellBorderType.THIN
                styleFechaResumen.BorderBottom = CellBorderType.THIN
                styleFechaResumen.BorderLeft = CellBorderType.THIN
                styleFechaResumen.BorderTop = CellBorderType.THIN
                styleFechaResumen.SetFont(oFontFiltroResumen)
                styleFechaResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFechaResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND







                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                            Continue For
                        End If
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x + 1).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1



                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")

                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(0))
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(2).CellStyle = styleTituloDescripcion
                    If intTable = 0 Then
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    ElseIf intTable <> 0 And intCont = 1 Then
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(oDs.Tables(intTable).TableName)
                    Else
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    End If
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 2, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1
                Next
                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    If oDs.Tables(intTable).Columns(x).ColumnName <> "ESTADOOP" Then
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = Replace(oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim(), Chr(34), "")
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length > 1 Then
                            flgCabDoble = True
                        End If
                        objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(0))
                    End If
                Next


                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas + 1).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length > 1 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1

                    '============================================================================
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas + 1).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 3 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(2))
                        End If
                    Next
                    rowActual = rowActual + 1

                End If

                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                    objWorkSheet.CreateRow(i + rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                        'Continue For
                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If oDs.Tables(intTable).Rows(i).Item(0).ToString.Trim.Equals("-1") Then
                            'objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                            'objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = style
                            Select Case TypoDato
                                Case "System.Decimal", "System.Int32", "System.Double", "System.Integer"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMontoResumen
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If
                                Case "System.Date"
                                    celda = String.Format("{0:d}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFechaResumen
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)

                                Case "System.DateTime"
                                    celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                Case Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltroResumen
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltroResumen
                                    End If

                            End Select
                        Else

                            If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                    If celda <> "" Then
                                        celda = "2"
                                    End If
                                Catch : End Try
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleLink
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda.ToString)
                                'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "OPERACIÓN" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "NRO. REVISIÓN" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "N° FACTURA" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                                If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMonto
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If

                                Else
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToInt64(Val(celda)))
                                    End If
                                End If


                            Else
                                If TypoDato.Equals("System.Date") Then
                                    celda = String.Format("{0:d}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                ElseIf TypoDato.Equals("System.Byte[]") Then


                                    Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                    Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                    Try
                                        link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                    Catch : End Try

                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).Hyperlink = link
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleLink
                                    If Not celda Is "" Then
                                        Dim anchorImagen As HSSFClientAnchor
                                        anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                        Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                        imagenAlmacen.LineStyle = 1
                                        imagenAlmacen.Resize()
                                    End If

                                ElseIf TypoDato.Equals("System.DateTime") Then
                                    celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    End If

                                End If
                            End If
                        End If

                    Next
                Next

                ''Suma los registros indicados
                Dim strFormula As String
                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If Not listSuma Is Nothing Then
                 
                    For Each items As DictionaryEntry In listSuma
                        If Math.Ceiling(items.Key) = (intTable + 2) Then
                            strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ")"
                            If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMontoTotal
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                        End If
                    Next
                End If
                '===================================================================
                For z As Integer = 1 To oDs.Tables(intTable).Columns.Count
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 1, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Shared Sub ExportExcel_Formato01(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try

            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1

                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim styleDinamico As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165

                Dim oFontFiltroResumen As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltroResumen.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltroResumen.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontFiltroResumen.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFiltroResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFiltroResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFiltroResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltroResumen.BorderRight = CellBorderType.THIN
                styleFiltroResumen.BorderBottom = CellBorderType.THIN
                styleFiltroResumen.BorderLeft = CellBorderType.THIN
                styleFiltroResumen.BorderTop = CellBorderType.THIN
                styleFiltroResumen.SetFont(oFontFiltroResumen)
                styleFiltroResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltroResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN

                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoTotal As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoTotal.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoTotal.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.BorderRight = CellBorderType.THIN
                styleMontoTotal.BorderBottom = CellBorderType.THIN
                styleMontoTotal.BorderLeft = CellBorderType.THIN
                styleMontoTotal.BorderTop = CellBorderType.THIN
                styleMontoTotal.SetFont(oFontFiltroResumen)
                styleMontoTotal.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoTotal.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleMontoResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoResumen.BorderRight = CellBorderType.THIN
                styleMontoResumen.BorderBottom = CellBorderType.THIN
                styleMontoResumen.BorderLeft = CellBorderType.THIN
                styleMontoResumen.BorderTop = CellBorderType.THIN
                styleMontoResumen.SetFont(oFontFiltroResumen)
                styleMontoResumen.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoResumen.FillPattern = FillPatternType.SOLID_FOREGROUND



                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFechaResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFechaResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFechaResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFechaResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFechaResumen.BorderRight = CellBorderType.THIN
                styleFechaResumen.BorderBottom = CellBorderType.THIN
                styleFechaResumen.BorderLeft = CellBorderType.THIN
                styleFechaResumen.BorderTop = CellBorderType.THIN
                styleFechaResumen.SetFont(oFontFiltroResumen)
                styleFechaResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFechaResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND







                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                            Continue For
                        End If
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x + 1).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1



                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")

                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(0))
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(2).CellStyle = styleTituloDescripcion
                    If intTable = 0 Then
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    ElseIf intTable <> 0 And intCont = 1 Then
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(oDs.Tables(intTable).TableName)
                    Else
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    End If
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 2, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1
                Next
                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    If oDs.Tables(intTable).Columns(x).ColumnName <> "ESTADOOP" Then
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = Replace(oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim(), Chr(34), "")
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length > 1 Then
                            flgCabDoble = True
                        End If
                        objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(0))
                    End If
                Next


                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas + 1).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length > 1 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1

                    '============================================================================
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas + 1).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 3 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(2))
                        End If
                    Next
                    rowActual = rowActual + 1

                End If

                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                    objWorkSheet.CreateRow(i + rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                        'Continue For
                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).Caption
                        If TypoDato = oDs.Tables(intTable).Columns.Item(x).ColumnName.ToString Then
                            TypoDato = oDs.Tables(intTable).Columns.Item(x).DataType.ToString
                        End If

                        'If oDs.Tables(intTable).Rows(i).Item(0).ToString.Trim.Equals("-1") Then
                        'objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                        'objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = style
                        Try
                            Select Case TypoDato
                                Case "System.Decimal", "System.Double", "Decimal"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMonto
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If
                                Case "System.Int32", "System.Integer", "Numero"
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToInt64(Val(celda)))
                                    End If
                                Case "System.Date", "Fecha"

                                    If IsDate(celda) Then
                                        celda = String.Format("{0:d}", CDate(celda))
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    End If



                                Case "System.DateTime", "FechaHora"
                                    celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                Case Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    End If

                            End Select
                        Catch ex As Exception

                        End Try
                       
                        'Else

                        'If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
                        '    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                        'ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                        '    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                        'ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                        '    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                        'ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                        '    Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                        '    Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                        '    Try
                        '        link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                        '        If celda <> "" Then
                        '            celda = "2"
                        '        End If
                        '    Catch : End Try
                        '    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).Hyperlink = link
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleLink
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda.ToString)
                        '    'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                        'ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                        '    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                        'ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "OPERACIÓN" Then
                        '    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                        'ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "NRO. REVISIÓN" Then
                        '    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                        'ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "N° FACTURA" Then
                        '    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                        'ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                        '    If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                        '        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMonto
                        '        If celda Is "" Then
                        '            objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                        '        Else
                        '            objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                        '        End If

                        '    Else
                        '        If celda Is "" Then
                        '            objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                        '            objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                        '        Else
                        '            objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                        '            objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToInt64(Val(celda)))
                        '        End If
                        '    End If


                        'Else
                        '    If TypoDato.Equals("System.Date") Then
                        '        celda = String.Format("{0:d}", CDate(celda))
                        '        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                        '        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                        '    ElseIf TypoDato.Equals("System.Byte[]") Then


                        '        Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                        '        Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                        '        Try
                        '            link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                        '        Catch : End Try

                        '        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).Hyperlink = link
                        '        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleLink
                        '        If Not celda Is "" Then
                        '            Dim anchorImagen As HSSFClientAnchor
                        '            anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                        '            Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                        '            imagenAlmacen.LineStyle = 1
                        '            imagenAlmacen.Resize()
                        '        End If

                        '    ElseIf TypoDato.Equals("System.DateTime") Then
                        '        celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                        '        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                        '        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                        '    Else
                        '        If celda Is DBNull.Value Then
                        '            objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                        '            objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                        '        Else
                        '            objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                        '            objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                        '        End If

                        '    End If
                        'End If
                        'End If

                    Next
                Next

                ''Suma los registros indicados
                Dim strFormula As String
                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If Not listSuma Is Nothing Then

                    For Each items As DictionaryEntry In listSuma
                        If Math.Ceiling(items.Key) = (intTable + 2) Then
                            strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ")"
                            If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMontoTotal
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                        End If
                    Next
                End If
                '===================================================================
                For z As Integer = 1 To oDs.Tables(intTable).Columns.Count
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 1, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub


    Public Shared Sub ExportExcel_Formato02(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal ohstFiltros As Hashtable, Optional ByVal listSuma As Hashtable = Nothing)
        Try

            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1

                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim styleDinamico As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
                Dim olstrFiltros As New List(Of String)
                'Asignamos los filtros
                olstrFiltros = ohstFiltros(intTable)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165

                Dim oFontFiltroResumen As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltroResumen.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltroResumen.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontFiltroResumen.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFiltroResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFiltroResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFiltroResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltroResumen.BorderRight = CellBorderType.THIN
                styleFiltroResumen.BorderBottom = CellBorderType.THIN
                styleFiltroResumen.BorderLeft = CellBorderType.THIN
                styleFiltroResumen.BorderTop = CellBorderType.THIN
                styleFiltroResumen.SetFont(oFontFiltroResumen)
                styleFiltroResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltroResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN

                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoTotal As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoTotal.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoTotal.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.BorderRight = CellBorderType.THIN
                styleMontoTotal.BorderBottom = CellBorderType.THIN
                styleMontoTotal.BorderLeft = CellBorderType.THIN
                styleMontoTotal.BorderTop = CellBorderType.THIN
                styleMontoTotal.SetFont(oFontFiltroResumen)
                styleMontoTotal.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoTotal.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleMontoResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoResumen.BorderRight = CellBorderType.THIN
                styleMontoResumen.BorderBottom = CellBorderType.THIN
                styleMontoResumen.BorderLeft = CellBorderType.THIN
                styleMontoResumen.BorderTop = CellBorderType.THIN
                styleMontoResumen.SetFont(oFontFiltroResumen)
                styleMontoResumen.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoResumen.FillPattern = FillPatternType.SOLID_FOREGROUND



                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFechaResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFechaResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFechaResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFechaResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFechaResumen.BorderRight = CellBorderType.THIN
                styleFechaResumen.BorderBottom = CellBorderType.THIN
                styleFechaResumen.BorderLeft = CellBorderType.THIN
                styleFechaResumen.BorderTop = CellBorderType.THIN
                styleFechaResumen.SetFont(oFontFiltroResumen)
                styleFechaResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFechaResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND







                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                            Continue For
                        End If
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x + 1).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1



                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")

                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(0))
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(2).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))

                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 2, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1
                Next
                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    If oDs.Tables(intTable).Columns(x).ColumnName <> "ESTADOOP" Then
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = Replace(oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim(), Chr(34), "")
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length > 1 Then
                            flgCabDoble = True
                        End If
                        objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(0))
                    End If
                Next


                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas + 1).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length > 1 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1

                    '============================================================================
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas + 1).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 3 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(2))
                        End If
                    Next
                    rowActual = rowActual + 1

                End If

                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                    objWorkSheet.CreateRow(i + rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                        'Continue For
                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).Caption
                        If TypoDato = oDs.Tables(intTable).Columns.Item(x).ColumnName.ToString Then
                            TypoDato = oDs.Tables(intTable).Columns.Item(x).DataType.ToString
                        End If

                        'If oDs.Tables(intTable).Rows(i).Item(0).ToString.Trim.Equals("-1") Then
                        'objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                        'objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = style
                        Try
                            Select Case TypoDato
                                Case "System.Decimal", "System.Double", "Decimal"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMonto
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If
                                Case "System.Int32", "System.Integer", "Numero"
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToInt64(Val(celda)))
                                    End If
                                Case "System.Date", "Fecha"

                                    If IsDate(celda) Then
                                        celda = String.Format("{0:d}", CDate(celda))
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    End If



                                Case "System.DateTime", "FechaHora"
                                    If celda = "" Then 'ECM-CorrectivoSolmin(SA_SC_CTZ)-[300516]
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    Else
                                        celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                    End If

                                Case Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    End If

                            End Select
                        Catch ex As Exception

                        End Try


                    Next
                Next

                ''Suma los registros indicados
                Dim strFormula As String
                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If Not listSuma Is Nothing Then

                    For Each items As DictionaryEntry In listSuma
                        If Math.Ceiling(items.Key) = (intTable + 2) Then
                            strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ")"
                            If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMontoTotal
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                        End If
                    Next
                End If
                '===================================================================
                For z As Integer = 1 To oDs.Tables(intTable).Columns.Count
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 1, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Shared Sub ExportExcelConsultaOperaciones(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1
                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim styleDinamico As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165

                Dim oFontFiltroResumen As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltroResumen.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltroResumen.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontFiltroResumen.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFiltroResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFiltroResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFiltroResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltroResumen.BorderRight = CellBorderType.THIN
                styleFiltroResumen.BorderBottom = CellBorderType.THIN
                styleFiltroResumen.BorderLeft = CellBorderType.THIN
                styleFiltroResumen.BorderTop = CellBorderType.THIN
                styleFiltroResumen.SetFont(oFontFiltroResumen)
                styleFiltroResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltroResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN

                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleMontoResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoResumen.BorderRight = CellBorderType.THIN
                styleMontoResumen.BorderBottom = CellBorderType.THIN
                styleMontoResumen.BorderLeft = CellBorderType.THIN
                styleMontoResumen.BorderTop = CellBorderType.THIN
                styleMontoResumen.SetFont(oFontFiltroResumen)
                styleMontoResumen.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoResumen.FillPattern = FillPatternType.SOLID_FOREGROUND



                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFechaResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFechaResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFechaResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFechaResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFechaResumen.BorderRight = CellBorderType.THIN
                styleFechaResumen.BorderBottom = CellBorderType.THIN
                styleFechaResumen.BorderLeft = CellBorderType.THIN
                styleFechaResumen.BorderTop = CellBorderType.THIN
                styleFechaResumen.SetFont(oFontFiltroResumen)
                styleFechaResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFechaResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND







                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                            Continue For
                        End If
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1



                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")

                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(0))
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(2).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    rowActual += 1
                Next
                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    If oDs.Tables(intTable).Columns(x).ColumnName <> "ESTADOOP" Then
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            flgCabDoble = True
                        End If
                        objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
                    End If
                Next


                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If
                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                    objWorkSheet.CreateRow(i + rowActual)
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                        'Continue For
                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If oDs.Tables(intTable).Rows(i).Item(0).ToString.Trim.Equals("-1") Then
                            'objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                            'objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = style
                            Select Case TypoDato
                                Case "System.Decimal", "System.Int32", "System.Double", "System.Integer"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMontoResumen
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If
                                Case "System.Date"
                                    If celda = "" Then 'ECM-CorrectivoSolmin(SA_SC_CTZ)-[300516]
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    Else
                                        celda = String.Format("{0:d}", CDate(celda))
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFechaResumen
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                    End If

                                Case "System.DateTime"
                                    If celda = "" Then 'ECM-CorrectivoSolmin(SA_SC_CTZ)-[300516]
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    Else
                                        celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                    End If
                                Case Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltroResumen
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltroResumen
                                    End If

                            End Select
                        Else

                            If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                    If celda <> "" Then
                                        celda = "2"
                                    End If
                                Catch : End Try
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda.ToString)
                                'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "OPERACIÓN" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Val(celda))
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "NRO. REVISIÓN" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Val(celda))
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "N° FACTURA" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Val(celda))
                            ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                                If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If

                                Else
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                                    End If
                                End If


                            Else
                                If TypoDato.Equals("System.Date") Then

                                    If celda = "" Then 'ECM-CorrectivoSolmin(SA_SC_CTZ)-[300516]
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    Else
                                        celda = String.Format("{0:d}", CDate(celda))
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                    End If
                                   
                                ElseIf TypoDato.Equals("System.Byte[]") Then


                                    Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                    Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                    Try
                                        link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                    Catch : End Try

                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                                    If Not celda Is "" Then
                                        Dim anchorImagen As HSSFClientAnchor
                                        anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                        Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                        imagenAlmacen.LineStyle = 1
                                        imagenAlmacen.Resize()
                                    End If

                                ElseIf TypoDato.Equals("System.DateTime") Then
                                    If celda = "" Then 'ECM-CorrectivoSolmin(SA_SC_CTZ)-[300516]
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    Else
                                        celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                    End If
                                Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    End If

                                End If
                                End If
                        End If

                    Next
                Next

                ''Suma los registros indicados
                Dim strFormula As String

                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If Not listSuma Is Nothing Then
                    For Each items As DictionaryEntry In listSuma
                        strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 6).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 5).ToString & ")"
                        If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMonto
                        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                    Next
                End If
                '===================================================================
                For z As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 1, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

  ''' <summary>
  '''Exportar Excel con Titulo
  ''' </summary>
  ''' <param name="oDs"></param>
  ''' <remarks></remarks>
  Public Shared Sub ExportExcelConTitulos(ByVal oDs As DataSet, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
    Try
      Dim path As String = Application.StartupPath.ToString
      Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
      Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

      Dim objWorkBook As New HSSFWorkbook()
      Dim objWorkSheet As New HSSFSheet(objWorkBook)
      For intTable As Integer = 0 To oDs.Tables.Count - 1
        objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
        objWorkSheet.CreateRow(3)
        '=============Style======================
        Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

        Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontFiltro.FontHeight = 165

        Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontCab.FontHeight = 220

        Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFont.FontHeight = 170

        Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
        oFontTituloConcepto.FontHeight = 190

        Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
        oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
        oFontTituloDescripcion.FontHeight = 170

        styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFiltro.BorderRight = CellBorderType.THIN
        styleFiltro.BorderBottom = CellBorderType.THIN
        styleFiltro.BorderLeft = CellBorderType.THIN
        styleFiltro.BorderTop = CellBorderType.THIN
        styleFiltro.SetFont(oFontFiltro)
        styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

        styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
        styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleCab.BorderRight = CellBorderType.THIN
        styleCab.BorderBottom = CellBorderType.THIN
        styleCab.BorderLeft = CellBorderType.THIN
        styleCab.BorderTop = CellBorderType.THIN
        styleCab.SetFont(oFontCab)
        styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER

        style.FillPattern = FillPatternType.SOLID_FOREGROUND
        style.BorderRight = CellBorderType.THIN
        style.BorderBottom = CellBorderType.THIN
        style.BorderLeft = CellBorderType.THIN
        style.BorderTop = CellBorderType.THIN
        style.SetFont(oFont)

        stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
        stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
        stylePorcentaje.BorderRight = CellBorderType.THIN
        stylePorcentaje.BorderBottom = CellBorderType.THIN
        stylePorcentaje.BorderLeft = CellBorderType.THIN
        stylePorcentaje.BorderTop = CellBorderType.THIN

        stylePorcentaje.SetFont(oFontFiltro)

        styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
        styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleTituloConcepto.SetFont(oFontTituloConcepto)

        styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
        styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleTituloDescripcion.SetFont(oFontTituloDescripcion)

        '===============================
        Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleNumber.BorderRight = CellBorderType.THIN
        styleNumber.BorderBottom = CellBorderType.THIN
        styleNumber.BorderLeft = CellBorderType.THIN
        styleNumber.BorderTop = CellBorderType.THIN

        styleNumber.SetFont(oFontFiltro)
        styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
        styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleMonto.BorderRight = CellBorderType.THIN
        styleMonto.BorderBottom = CellBorderType.THIN
        styleMonto.BorderLeft = CellBorderType.THIN
        styleMonto.BorderTop = CellBorderType.THIN
        styleMonto.SetFont(oFontFiltro)
        styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
        styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFecha.BorderRight = CellBorderType.THIN
        styleFecha.BorderBottom = CellBorderType.THIN
        styleFecha.BorderLeft = CellBorderType.THIN
        styleFecha.BorderTop = CellBorderType.THIN
        styleFecha.SetFont(oFontFiltro)
        styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

        Dim rowActual As Integer = 2
        '===============Titulo
        objWorkSheet.CreateRow(rowActual).Height = 100 * 2

        '==============Verificamos Filtro a utilizar==================

        For intCont As Integer = 0 To olstrFiltros.Count - 1
          Dim strDescripcionTitulo As String()
          strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
          objWorkSheet.CreateRow(rowActual + 1)
          objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
          objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))
          objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
          objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
          rowActual += 1
        Next
        objWorkSheet.CreateRow(rowActual + 1)

        rowActual += 2
        '===================Se cargan Las Cabeceras=====================
        Dim flgCabDoble As Boolean = False
        objWorkSheet.CreateRow(rowActual)
        For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
          '===Estilo===
          objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
          '===Valores===
          Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
          Dim valorCabDoble As String()
          valorCabDoble = Split(valorCab, "\n")
          If valorCabDoble.Length = 2 Then
            flgCabDoble = True
          End If
          objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
        Next
        rowActual = rowActual + 1
        Dim intRepite As Integer = 0
        If flgCabDoble = True Then
          '==========Limpiamos los Cells Repetidos en el Row Anterior=========
          For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
            'If x < oDs.Tables(intTable).Columns.Count - 1 Then
            intRepite = 0
            For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
              If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                intRepite = intRepite + 1
              End If
            Next
            If intRepite > 1 Then
              intRepite = intRepite - 1
              Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
              '===Union===
              Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
              objWorkSheet.AddMergedRegion(Region)
              x = x + intRepite
            End If
            ' End If
          Next
          '===================================================================
          objWorkSheet.CreateRow(rowActual)
          For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
            '===Estilo===
            objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
            '===Valores===
            Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
            Dim valorCabDoble As String()
            valorCabDoble = Split(valorCab, "\n")
            If valorCabDoble.Length = 2 Then
              objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
            End If
          Next
          rowActual = rowActual + 1
        End If
        '===================Se Carga El Detalle============================
        For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

          objWorkSheet.CreateRow(i + rowActual)

          For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1

            Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
            Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

            If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
              objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
              objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
              '

            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
              objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
              objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
              objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
              objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)

            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
              objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
              objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)

            ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

              If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                If celda Is "" Then
                  objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                Else
                  objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                End If

              Else
                If celda Is "" Then
                  objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                  objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                Else
                  objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                  objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                End If
              End If


            Else
              If TypoDato.Equals("System.Date") Then
                celda = String.Format("{0:d}", CDate(celda))
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
              ElseIf TypoDato.Equals("System.Byte[]") Then
                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                If Not celda Is "" Then
                  Dim anchorImagen As HSSFClientAnchor
                  anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                  Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                  imagenAlmacen.LineStyle = 1
                  imagenAlmacen.Resize()
                End If
              Else
                If celda Is DBNull.Value Then
                  objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                  objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                Else
                  objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                  objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                End If

              End If
            End If

          Next
        Next

        ''Suma los registros indicados
        Dim strFormula As String
        objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
        If Not listSuma Is Nothing Then
          For Each items As DictionaryEntry In listSuma
            strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 6).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 5).ToString & ")"
            If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMonto
            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
          Next
        End If
        '===================================================================
        For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
          objWorkSheet.AutoSizeColumn(z, True)
        Next

        '============================================logo=================================
        'create the anchor
        Dim anchor As HSSFClientAnchor
        ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
        anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
        If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
          'load the picture and get the picture index in the workbook
          Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
          picture.Resize(0.5)
        End If
        If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
          'load the picture and get the picture index in the workbook
          Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
          'Reset the image to the original size.
          picture.Resize(0.5)
        End If
        ''============================================logo=================================

      Next
      objWorkBook.Write(fs)
      fs.Close()
      AbrirDocumento(archivo)
    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try

  End Sub

  ''' <summary>
  ''' Exportar Excel con Titulo
  ''' </summary>
  ''' <param name="oDtg"></param>
  ''' <param name="strTitulo"></param>
  ''' <param name="olstrFiltros"></param>
  ''' <param name="listSuma"></param>
  ''' <remarks></remarks>

    Public Shared Sub ExportExcelConTitulos(ByVal oDtg As DataGridView, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try


            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()
            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)


            objWorkSheet = objWorkBook.CreateSheet(oDtg.Name)
            objWorkSheet.CreateRow(3)
            '=============Style======================
            Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

            Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontFiltro.FontHeight = 165

            Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220

            Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFont.FontHeight = 170

            Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
            oFontTituloConcepto.FontHeight = 190

            Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
            oFontTituloDescripcion.FontHeight = 170

            styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.BorderRight = CellBorderType.THIN
            styleFiltro.BorderBottom = CellBorderType.THIN
            styleFiltro.BorderLeft = CellBorderType.THIN
            styleFiltro.BorderTop = CellBorderType.THIN
            styleFiltro.SetFont(oFontFiltro)
            styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

            styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleCab.BorderRight = CellBorderType.THIN
            styleCab.BorderBottom = CellBorderType.THIN
            styleCab.BorderLeft = CellBorderType.THIN
            styleCab.BorderTop = CellBorderType.THIN
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER

            style.FillPattern = FillPatternType.SOLID_FOREGROUND
            style.BorderRight = CellBorderType.THIN
            style.BorderBottom = CellBorderType.THIN
            style.BorderLeft = CellBorderType.THIN
            style.BorderTop = CellBorderType.THIN
            style.SetFont(oFont)

            stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
            stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
            stylePorcentaje.BorderRight = CellBorderType.THIN
            stylePorcentaje.BorderBottom = CellBorderType.THIN
            stylePorcentaje.BorderLeft = CellBorderType.THIN
            stylePorcentaje.BorderTop = CellBorderType.THIN

            stylePorcentaje.SetFont(oFontFiltro)

            styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.SetFont(oFontTituloConcepto)

            styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.SetFont(oFontTituloDescripcion)

            '===============================
            Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.BorderRight = CellBorderType.THIN
            styleNumber.BorderBottom = CellBorderType.THIN
            styleNumber.BorderLeft = CellBorderType.THIN
            styleNumber.BorderTop = CellBorderType.THIN

            styleNumber.SetFont(oFontFiltro)
            styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
            styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.BorderRight = CellBorderType.THIN
            styleMonto.BorderBottom = CellBorderType.THIN
            styleMonto.BorderLeft = CellBorderType.THIN
            styleMonto.BorderTop = CellBorderType.THIN
            styleMonto.SetFont(oFontFiltro)
            styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
            styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFecha.BorderRight = CellBorderType.THIN
            styleFecha.BorderBottom = CellBorderType.THIN
            styleFecha.BorderLeft = CellBorderType.THIN
            styleFecha.BorderTop = CellBorderType.THIN
            styleFecha.SetFont(oFontFiltro)
            styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

            Dim rowActual As Integer = 2
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2
            '===========================
            Dim intColumns As Integer = 0

            For x As Integer = 0 To oDtg.Columns.Count - 1
                If oDtg.Columns(x).Visible Then
                    intColumns += 1
                End If
            Next

            If Not strTitulo Is Nothing Then
                objWorkSheet.CreateRow(rowActual + 1)
                For x As Integer = 0 To intColumns - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                Next
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, intColumns - 1))
                rowActual += 1
            End If

            For intCont As Integer = 0 To olstrFiltros.Count - 1
                Dim strDescripcionTitulo As String()
                strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                objWorkSheet.CreateRow(rowActual + 1)
                objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                For x As Integer = 1 To intColumns - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                Next

                'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, intColumns - 1))
                rowActual += 1
            Next
            objWorkSheet.CreateRow(rowActual + 1)

            rowActual += 2




            '===================Se cargan Las Cabeceras=====================
            Dim flgCabDoble As Boolean = False
            Dim nColumnas As Integer = 0
            objWorkSheet.CreateRow(rowActual)

            For x As Integer = 0 To oDtg.ColumnCount - 1

                If oDtg.Columns(x).Visible And oDtg.Columns(x).CellType.FullName.Equals("System.Windows.Forms.DataGridViewTextBoxCell") Or oDtg.Columns(x).Visible And oDtg.Columns(x).CellType.FullName.Equals("System.Windows.Forms.DataGridViewLinkCell") Then
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(nColumnas).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDtg.Columns(x).HeaderText.Replace(Chr(13), " ").Replace(Chr(10), " ")
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        flgCabDoble = True
                    End If
                    objWorkSheet.GetRow(rowActual).GetCell(nColumnas).SetCellValue(valorCabDoble(0))
                    nColumnas += 1
                End If

            Next
            rowActual = rowActual + 1
            Dim intRepite As Integer = 0
            If flgCabDoble = True Then
                '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                For x As Integer = 0 To oDtg.ColumnCount - 1
                    If oDtg.Columns(x).Visible Then
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDtg.ColumnCount - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    End If

                Next
                '===================================================================
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDtg.ColumnCount - 1

                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDtg.Columns(x).HeaderText.ToString().Trim()
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                    End If

                Next
                rowActual = rowActual + 1
            End If



            '===================Se Carga El Detalle============================
            Dim TypoDato As String = String.Empty
            For i As Integer = 0 To oDtg.RowCount - 1

                nColumnas = 0

                objWorkSheet.CreateRow(i + rowActual)

                For x As Integer = 0 To oDtg.ColumnCount - 1
                    Dim celda As String = String.Empty

                    If (oDtg.Columns(x).CellType.FullName.Equals("System.Windows.Forms.DataGridViewTextBoxCell") Or oDtg.Columns(x).Visible And oDtg.Columns(x).CellType.FullName.Equals("System.Windows.Forms.DataGridViewLinkCell")) And oDtg.Columns(x).Visible Then
                        celda = ("" & oDtg.Rows(i).Cells(x).Value & "").ToString().Trim()
                        TypoDato = oDtg.Rows(i).Cells(x).ValueType().FullName

                        If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).CellStyle = styleMonto
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue(Convert.ToDouble(Val(celda)))
                                End If

                            Else
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue(Convert.ToInt64(Val(celda)))
                                End If
                            End If


                        Else
                            If TypoDato.Equals("System.Date") Then
                                celda = String.Format("{0:d}", CDate(celda))
                                objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).CellStyle = styleFecha
                                objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue(celda)
                            ElseIf TypoDato.Equals("System.Byte[]") Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).CellStyle = styleFiltro
                                If Not celda Is "" Then
                                    Dim anchorImagen As HSSFClientAnchor
                                    anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                    Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDtg.Rows(i).Cells(x).Value, objWorkBook)), HSSFPicture)
                                    imagenAlmacen.LineStyle = 1
                                    imagenAlmacen.Resize()
                                End If
                            Else
                                If celda Is DBNull.Value Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).SetCellValue("")
                                    objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).CellStyle = styleFiltro
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).SetCellValue(celda)
                                    objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).CellStyle = styleFiltro
                                End If

                            End If

                        End If
                        nColumnas += 1
                    End If
                Next


            Next

            ''Suma los registros indicados
            Dim strFormula As String
            objWorkSheet.CreateRow(rowActual + oDtg.RowCount)
            If Not listSuma Is Nothing Then
                For Each items As DictionaryEntry In listSuma
                    strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + 6).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDtg.RowCount + olstrFiltros.Count + 5).ToString & ")"
                    If oDtg.RowCount = 0 Then strFormula = "0"
                    objWorkSheet.GetRow(rowActual + oDtg.RowCount).CreateCell(items.Value - 1).CellStyle = styleMonto
                    objWorkSheet.GetRow(rowActual + oDtg.RowCount).GetCell(items.Value - 1).CellFormula = (strFormula)
                Next
            End If
            '===================================================================
            For z As Integer = 0 To oDtg.ColumnCount - 1

                objWorkSheet.AutoSizeColumn(z, True)

            Next

            '============================================logo============================

            'create the anchor
            Dim anchor As HSSFClientAnchor
            anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, _
             0, 0)
            If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                picture.Resize(0.5)
            End If
            If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                'Reset the image to the original size.
                picture.Resize(0.5)
            End If
            '============================================logo============================

            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

  ''' <summary>
  ''' Exportar Excel con Titulo
  ''' </summary>
  ''' <param name="oDtg"></param>
  ''' <param name="strTitulo"></param>
  ''' <param name="olstrFiltros"></param>
  ''' <param name="listSuma"></param>
  ''' <remarks></remarks>
  Public Shared Sub ExportExcelFormatoConga(ByVal oDtg As DataGridView, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
    Try


      Dim path As String = Application.StartupPath.ToString
      Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
      Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

      Dim objWorkBook As New HSSFWorkbook()
      Dim objWorkSheet As New HSSFSheet(objWorkBook)

      objWorkSheet = objWorkBook.CreateSheet(oDtg.Name)
      objWorkSheet.CreateRow(3)
      '=============Style======================
      Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
      Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
      Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

      Dim style01 As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
      Dim style02 As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
      Dim style03 As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
      Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
      Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
      Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

      Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
      oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
      oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
      oFontFiltro.FontHeight = 165

      Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
      oFontCab.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
      oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
      oFontCab.FontHeight = 220

      'Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
      'oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
      'oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
      'oFont.FontHeight = 170

      Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
      oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
      oFont.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
      oFont.FontHeight = 170

      Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
      oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
      oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
      oFontTituloConcepto.FontHeight = 190

      Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
      oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
      oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
      oFontTituloDescripcion.FontHeight = 170

      styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      styleFiltro.BorderRight = CellBorderType.THIN
      styleFiltro.BorderBottom = CellBorderType.THIN
      styleFiltro.BorderLeft = CellBorderType.THIN
      styleFiltro.BorderTop = CellBorderType.THIN
      styleFiltro.SetFont(oFontFiltro)
      styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
      styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

      styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
      styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
      styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      styleCab.BorderRight = CellBorderType.THIN
      styleCab.BorderBottom = CellBorderType.THIN
      styleCab.BorderLeft = CellBorderType.THIN
      styleCab.BorderTop = CellBorderType.THIN
      styleCab.SetFont(oFontCab)
      styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
      styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND


      style01.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
      style01.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_TURQUOISE.index
      style01.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
      style01.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      style01.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
      style01.FillPattern = FillPatternType.SOLID_FOREGROUND
      style01.BorderRight = CellBorderType.THIN
      style01.BorderRight = CellBorderType.THIN
      style01.BorderBottom = CellBorderType.THIN
      style01.BorderLeft = CellBorderType.THIN
      style01.BorderTop = CellBorderType.THIN
      style01.SetFont(oFont)


      style02.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
      style02.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LEMON_CHIFFON.index
      style02.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
      style02.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      style02.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
      style02.FillPattern = FillPatternType.SOLID_FOREGROUND
      style02.BorderRight = CellBorderType.THIN
      style02.BorderRight = CellBorderType.THIN
      style02.BorderBottom = CellBorderType.THIN
      style02.BorderLeft = CellBorderType.THIN
      style02.BorderTop = CellBorderType.THIN
      style02.SetFont(oFont)


      style03.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
      style03.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_ORANGE.index
      style03.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
      style03.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      style03.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
      style03.FillPattern = FillPatternType.SOLID_FOREGROUND
      style03.BorderRight = CellBorderType.THIN
      style03.BorderRight = CellBorderType.THIN
      style03.BorderBottom = CellBorderType.THIN
      style03.BorderLeft = CellBorderType.THIN
      style03.BorderTop = CellBorderType.THIN
      style03.SetFont(oFont)


      style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
      style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
      style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
      style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
      style.FillPattern = FillPatternType.SOLID_FOREGROUND
      style.BorderRight = CellBorderType.THIN
      style.BorderRight = CellBorderType.THIN
      style.BorderBottom = CellBorderType.THIN
      style.BorderLeft = CellBorderType.THIN
      style.BorderTop = CellBorderType.THIN
      style.SetFont(oFont)


      stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
      stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
      stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
      stylePorcentaje.BorderRight = CellBorderType.THIN
      stylePorcentaje.BorderBottom = CellBorderType.THIN
      stylePorcentaje.BorderLeft = CellBorderType.THIN
      stylePorcentaje.BorderTop = CellBorderType.THIN

      stylePorcentaje.SetFont(oFontFiltro)

      styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
      styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
      styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
      styleTituloConcepto.SetFont(oFontTituloConcepto)

      styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
      styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
      styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
      styleTituloDescripcion.SetFont(oFontTituloDescripcion)

      '===============================
      Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
      styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
      styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      styleNumber.BorderRight = CellBorderType.THIN
      styleNumber.BorderBottom = CellBorderType.THIN
      styleNumber.BorderLeft = CellBorderType.THIN
      styleNumber.BorderTop = CellBorderType.THIN

      styleNumber.SetFont(oFontFiltro)
      styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
      styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
      '===============================
      Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
      styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
      styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      styleMonto.BorderRight = CellBorderType.THIN
      styleMonto.BorderBottom = CellBorderType.THIN
      styleMonto.BorderLeft = CellBorderType.THIN
      styleMonto.BorderTop = CellBorderType.THIN
      styleMonto.SetFont(oFontFiltro)
      styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
      styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
      '===============================
      Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
      styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
      styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
      styleFecha.BorderRight = CellBorderType.THIN
      styleFecha.BorderBottom = CellBorderType.THIN
      styleFecha.BorderLeft = CellBorderType.THIN
      styleFecha.BorderTop = CellBorderType.THIN
      styleFecha.SetFont(oFontFiltro)
      styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
      styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

      Dim rowActual As Integer = 2
      '===============Titulo
      objWorkSheet.CreateRow(rowActual).Height = 100 * 2
      '===========================

      If Not strTitulo Is Nothing Then
        objWorkSheet.CreateRow(rowActual + 1)
        For x As Integer = 0 To oDtg.Columns.Count - 1
          objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
        Next
        objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
        objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDtg.Columns.Count - 1))
        rowActual += 1
      End If

      For intCont As Integer = 0 To olstrFiltros.Count - 1
        Dim strDescripcionTitulo As String()
        strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
        objWorkSheet.CreateRow(rowActual + 1)
        objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
        objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

        objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
        objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
        rowActual += 1
      Next
      objWorkSheet.CreateRow(rowActual + 1)

      rowActual += 2




      '===================Se cargan Las Cabeceras=====================
      Dim flgCabDoble As Boolean = False
      Dim nColumnas As Integer = 0
      objWorkSheet.CreateRow(rowActual)

      For x As Integer = 0 To oDtg.ColumnCount - 1
        If oDtg.Columns(x).CellType.FullName.Equals("System.Windows.Forms.DataGridViewTextBoxCell") Then
          '===Estilo===

          '===Valores===
          Dim valorCab As String = oDtg.Columns(x).HeaderText.Replace(Chr(13), " ").Replace(Chr(10), " ")
          Select Case x
            Case 0, 1, 2, 3, 4
              valorCab = "PURCHASE ORDER DETAILS \n" & valorCab
              objWorkSheet.GetRow(rowActual).CreateCell(nColumnas).CellStyle = style01
            Case 5, 6, 7, 8, 9, 10
              valorCab = "TRAFFIC DETAILS \n" & valorCab
              objWorkSheet.GetRow(rowActual).CreateCell(nColumnas).CellStyle = style02
            Case 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
              valorCab = "DISPATCH DETAILS \n" & valorCab
              objWorkSheet.GetRow(rowActual).CreateCell(nColumnas).CellStyle = style03
          End Select
          Dim valorCabDoble As String()
          valorCabDoble = Split(valorCab, "\n")
          If valorCabDoble.Length = 2 Then
            flgCabDoble = True
          End If
          objWorkSheet.GetRow(rowActual).GetCell(nColumnas).SetCellValue(valorCabDoble(0))
          nColumnas += 1
        End If
        If Not oDtg.Columns(x).Visible Then
          oDtg.Columns.RemoveAt(x)
        End If


      Next
      rowActual = rowActual + 1
      Dim intRepite As Integer = 0
      If flgCabDoble = True Then
        '==========Limpiamos los Cells Repetidos en el Row Anterior=========
        For x As Integer = 0 To oDtg.ColumnCount - 1
          'If x < oDs.Tables(intTable).Columns.Count - 1 Then
          intRepite = 0
          For intColumnas As Integer = 0 To oDtg.ColumnCount - 1
            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
              intRepite = intRepite + 1
            End If
          Next
          If intRepite > 1 Then
            intRepite = intRepite - 1
            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
            '===Union===
            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
            objWorkSheet.AddMergedRegion(Region)
            x = x + intRepite
          End If
          'End If

        Next
        '===================================================================
        objWorkSheet.CreateRow(rowActual)
        For x As Integer = 0 To oDtg.ColumnCount - 1
          objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
          '===Valores===
          Dim valorCab As String = oDtg.Columns(x).HeaderText.ToString().Trim()
          Select Case x
            Case 0, 1, 2, 3, 4
              valorCab = "PURCHASE ORDER DETAILS \n" & valorCab
            Case 5, 6, 7, 8, 9, 10
              valorCab = "TRAFFIC DETAILS \n" & valorCab
            Case 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
              valorCab = "DISPATCH DETAILS \n" & valorCab
          End Select

          Dim valorCabDoble As String()
          valorCabDoble = Split(valorCab, "\n")

          If valorCabDoble.Length = 2 Then
            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
          End If
        Next
        rowActual = rowActual + 1
      End If



      '===================Se Carga El Detalle============================
      For i As Integer = 0 To oDtg.RowCount - 1

        nColumnas = 0

        objWorkSheet.CreateRow(i + rowActual)

        For x As Integer = 0 To oDtg.ColumnCount - 1
          Dim celda As String = String.Empty

          If oDtg.Columns(x).CellType.FullName.Equals("System.Windows.Forms.DataGridViewTextBoxCell") And oDtg.Columns(x).Visible Then

            celda = ("" & oDtg.Rows(i).Cells(x).Value & "").ToString().Trim()

            If IsNumeric(celda) And celda = "0" Or celda = "0.000" Then celda = " "
            If IsNumeric(celda) Then
              If celda.Contains(".") Then

                If oDtg.Columns(nColumnas).HeaderText.ToString.Trim = "PORCENTAJE" Then
                  objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).CellStyle = stylePorcentaje
                Else
                  objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).CellStyle = styleMonto
                End If

              Else
                objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).CellStyle = styleNumber
              End If
              If nColumnas = 0 Then
                objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue(celda.ToString)
              Else
                If celda.Substring(0, 1) = "0" And celda.Length > 1 Then

                  If oDtg.Columns(x).HeaderText.ToString.Trim = "PORCENTAJE" Then
                    objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue(Convert.ToDouble(celda) / 100)
                  Else
                    objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue(celda.ToString)
                  End If

                ElseIf celda.Length > 10 Then
                  objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue(celda.ToString)
                Else
                  If oDtg.Columns(x).HeaderText.ToString.Trim = "PORCENTAJE" Then
                    objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue(Convert.ToDouble(celda) / 100)
                  Else

                    objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue(Convert.ToDouble(celda))

                  End If
                End If
              End If
            Else
              If IsDate(celda) Then

                If Not oDtg.Columns(x).HeaderText.ToString.Trim = "Hora Seg." Then
                  celda = String.Format("{0:d}", CDate(celda))
                End If
                objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).CellStyle = styleFecha
                objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).SetCellValue(celda)
              Else

                objWorkSheet.GetRow(i + rowActual).CreateCell(nColumnas).SetCellValue(celda.ToString.Trim)
                objWorkSheet.GetRow(i + rowActual).GetCell(nColumnas).CellStyle = styleFiltro


              End If
            End If


            nColumnas += 1
          End If


        Next


            Next

            ''Suma los registros indicados
            Dim strFormula As String
            objWorkSheet.CreateRow(rowActual + oDtg.RowCount)
            If Not listSuma Is Nothing Then
                For Each items As DictionaryEntry In listSuma
                    strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + 6).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDtg.RowCount + olstrFiltros.Count + 5).ToString & ")"
                    If oDtg.RowCount = 0 Then strFormula = "0"
                    objWorkSheet.GetRow(rowActual + oDtg.RowCount).CreateCell(items.Value - 1).CellStyle = styleMonto
                    objWorkSheet.GetRow(rowActual + oDtg.RowCount).GetCell(items.Value - 1).CellFormula = (strFormula)
                Next
            End If
            '===================================================================
            For z As Integer = 0 To oDtg.ColumnCount - 1

                objWorkSheet.AutoSizeColumn(z, True)

            Next

            '============================================logo============================
            Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
            'create the anchor
            Dim anchor As HSSFClientAnchor
            anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, _
             0, 0)
            If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                picture.Resize(0.5)
            End If
            If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                'Reset the image to the original size.
                picture.Resize(0.5)
            End If
            '============================================logo============================

            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

  End Sub


  ''' <summary>
  ''' Exportar Fluvial Version 2.00
  ''' 'Estilos Generales para Excel
  ''' </summary>
  ''' <param name="style"></param>
  ''' <param name="tipo"></param>
  ''' <remarks></remarks>
  Private Shared Sub Estilos_Excel_NPOI(ByVal style As NPOI.SS.UserModel.CellStyle, ByVal tipo As String)
    Select Case tipo
      Case "Filtro"
        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        style.FillPattern = FillPatternType.SOLID_FOREGROUND
      Case "Cabecera"
        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        style.FillPattern = FillPatternType.SOLID_FOREGROUND
        style.BorderRight = CellBorderType.THIN
      Case "Normal"
        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
        style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        style.FillPattern = FillPatternType.SOLID_FOREGROUND
        style.BorderRight = CellBorderType.THIN
        style.BorderBottom = CellBorderType.THIN
        style.BorderLeft = CellBorderType.THIN
        style.BorderTop = CellBorderType.THIN
      Case "Titulo"
        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        style.FillPattern = FillPatternType.SOLID_FOREGROUND
      Case "Numero"
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00") '"#.##0,00"
        style.FillPattern = FillPatternType.SOLID_FOREGROUND
        style.BorderRight = CellBorderType.THIN
        style.BorderBottom = CellBorderType.THIN
        style.BorderLeft = CellBorderType.THIN
        style.BorderTop = CellBorderType.THIN
      Case "Monto"
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
        style.FillPattern = FillPatternType.SOLID_FOREGROUND
        style.BorderRight = CellBorderType.THIN
        style.BorderBottom = CellBorderType.THIN
        style.BorderLeft = CellBorderType.THIN
        style.BorderTop = CellBorderType.THIN
      Case "Fecha"
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        style.FillPattern = FillPatternType.SOLID_FOREGROUND
        style.BorderRight = CellBorderType.THIN
        style.BorderBottom = CellBorderType.THIN
        style.BorderLeft = CellBorderType.THIN
        style.BorderTop = CellBorderType.THIN
    End Select
  End Sub

  Private Shared Function LetraNumero_NPOI(ByVal i As Integer) As String
    i = i - 1
    Dim AbcD As New Hashtable()
    AbcD.Add(0, "A")
    AbcD.Add(1, "B")
    AbcD.Add(2, "C")
    AbcD.Add(3, "D")
    AbcD.Add(4, "E")
    AbcD.Add(5, "F")
    AbcD.Add(6, "G")
    AbcD.Add(7, "H")
    AbcD.Add(8, "I")
    AbcD.Add(9, "J")
    AbcD.Add(10, "K")
    AbcD.Add(11, "L")
    AbcD.Add(12, "M")
    AbcD.Add(13, "N")
    AbcD.Add(14, "O")
    AbcD.Add(15, "P")
    AbcD.Add(16, "Q")
    AbcD.Add(17, "R")
    AbcD.Add(18, "S")
    AbcD.Add(19, "T")
    AbcD.Add(20, "U")
    AbcD.Add(21, "V")
    AbcD.Add(22, "W")
    AbcD.Add(23, "X")
    AbcD.Add(24, "Y")
    AbcD.Add(25, "Z")
    If i > 25 Then
      Dim intX, intY As Integer
      intX = Math.Floor(i / 26)
      intY = i - intX * 26
      Return AbcD(intX - 1).ToString + AbcD(intY).ToString
    Else
      Return AbcD(i).ToString
    End If

  End Function
  Private Shared Function CargaImagen_NPOI(ByRef url As String, ByVal wb As HSSFWorkbook, Optional ByVal height As Integer = 100, Optional ByVal width As Integer = 100) As Integer
    Try
      Dim request As Net.HttpWebRequest = DirectCast(Net.HttpWebRequest.Create(url), Net.HttpWebRequest)
      Dim response As Net.HttpWebResponse = DirectCast(request.GetResponse, Net.HttpWebResponse)
      Dim img As Image = Image.FromStream(response.GetResponseStream())
      Dim objbitmap As Bitmap
      Dim ms As New MemoryStream
      objbitmap = New Bitmap(img, height, width)
      response.Close()
      objbitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp)
      ms.Position = 0
      Dim Buffer As Byte() = ms.ToArray
      Return wb.AddPicture(Buffer, PictureType.JPEG)
    Catch ex As Exception
      Return Nothing
    End Try
  End Function

  Private Shared Function CargaImagen_NPOI2(ByRef url As Byte(), ByVal wb As HSSFWorkbook, Optional ByVal height As Integer = 100, Optional ByVal width As Integer = 100) As Integer
    Try
      'Dim Buffer As Byte() = url.tos
      Return wb.AddPicture(url, PictureType.JPEG)
    Catch ex As Exception
      Return Nothing
    End Try
  End Function


  '==================================================================

  Private Shared Function LoadImage(ByVal path As String, ByVal wb As HSSFWorkbook) As Integer
    Dim file As New FileStream(path, FileMode.Open, FileAccess.Read)
    Dim buffer As Byte() = New Byte(file.Length - 1) {}
    file.Read(buffer, 0, CInt(file.Length))
    Return wb.AddPicture(buffer, PictureType.JPEG)
  End Function
  ''' <summary>
  ''' Exporta a Excel Lista de Tablas
  ''' </summary>
  ''' 
  Public Shared Sub ExportarExcel_Table(ByVal objListdt As List(Of DataTable))


    Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tempo\"
    If IO.Directory.Exists(path) = False Then
      IO.Directory.CreateDirectory(path)
    End If
    Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
    Dim xls As New IO.StreamWriter(archivo, True, Encoding.Default)


    xls.WriteLine("<TABLE border='1' >")
    xls.WriteLine("<tr>")
    xls.Write("<td >  </td>")
    xls.Write("<td >  </td>")
    xls.Write("<td >  </td>")
    xls.Write("<td style='background:#336600; color:#FFFFFF; text-align:center; line-height:18px; Font(-weight): bold()' >   Titulo  </td>")
    xls.WriteLine("</tr>")
    xls.WriteLine("</TABLE>")

    For Each odtg As DataTable In objListdt
      xls.WriteLine(" <%  Response.AddHeader( 'Content-type', 'application/vnd.ms-excel')   Response.AddHeader( 'Content-Disposition', 'attachment; filename=reporte3.xls')    Response.ContentType = 'Content-type: application/vnd.ms-excel' %> ")
      xls.WriteLine("<html>")
      xls.WriteLine("<body>")
      xls.WriteLine("<TABLE  id='post3458713'  border='1' >")
      xls.WriteLine("<tr>")
      For columna As Integer = 0 To odtg.Columns.Count - 1
        xls.Write("<td style='background:#336600; color:#FFFFFF; text-align:center; line-height:18px; Font(-weight): bold()' >" & odtg.Columns.Item(columna).ColumnName & "</td>")
      Next
      xls.WriteLine("</tr>")
      For fila As Integer = 0 To odtg.Rows.Count - 1
        xls.WriteLine("<tr>")
        For columna As Integer = 0 To odtg.Columns.Count - 1
          If Not ((odtg.Rows.Item(fila).Item(columna) Is DBNull.Value) OrElse odtg.Rows.Item(fila).Item(columna) = Nothing) Then
            xls.Write("<td>" & odtg.Rows.Item(fila).Item(columna) & "</td>")
          Else
            xls.Write("<td> </td>")
          End If

        Next
        xls.WriteLine("</tr>")
      Next
      xls.WriteLine("</TABLE>")
      xls.WriteLine("</body>")
      xls.WriteLine("</html>")
    Next
    xls.Flush()
    xls.Close()
    xls.Dispose()
    AbrirDocumento(archivo)
  End Sub

  ''' <summary>
  ''' Exporta a Excel Lista de DataGridView por medio de HTML incluye valores con cero
  ''' </summary>
  Public Shared Sub ExportarExcel_Add0(ByVal objListDtg As List(Of DataGridView))
    Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tempo\"
    If IO.Directory.Exists(path) = False Then
      IO.Directory.CreateDirectory(path)
    End If
    Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
    Dim xls As New IO.StreamWriter(archivo, True, Encoding.Default)

    For Each odtg As DataGridView In objListDtg
      xls.WriteLine("<TABLE border='1' >")
      xls.WriteLine("<tr>")
      For columna As Integer = 0 To odtg.Columns.Count - 1
        If odtg.Columns.Item(columna).Visible Then
          xls.Write("<td style='background:#FFA200; text-align:center; line-height:18px; Font(-weight): bold()' >" & odtg.Columns.Item(columna).HeaderText.ToString() & "</td>")
        End If
      Next
      xls.WriteLine("</tr>")
      For fila As Integer = 0 To odtg.Rows.Count - 1
        xls.WriteLine("<tr>")
        For columna As Integer = 0 To odtg.Columns.Count - 1
          If odtg.Columns.Item(columna).Visible Then
            If (odtg.Item(columna, fila).Value IsNot DBNull.Value And odtg.Item(columna, fila).Value IsNot Nothing) Then
              xls.Write("<td>" & odtg.Item(columna, fila).Value & "</td>")
            Else
              xls.Write("<td> </td>")
            End If
          End If
        Next
        xls.WriteLine("</tr>")
      Next
      xls.WriteLine("</TABLE>")
    Next
    xls.Flush()
    xls.Close()
    xls.Dispose()
    AbrirDocumento(archivo)
  End Sub

  ''' <summary>
  ''' Exporta a Excel Lista de DataGridView por medio de HTML
  ''' </summary>
  Public Shared Sub ExportarExcel_HTML(ByVal objListDtg As List(Of DataGridView))
    Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tempo\"
    If IO.Directory.Exists(path) = False Then
      IO.Directory.CreateDirectory(path)
    End If
    Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
    Dim xls As New IO.StreamWriter(archivo, True, Encoding.Default)

    For Each odtg As DataGridView In objListDtg
      xls.WriteLine("<TABLE border='1' >")
      xls.WriteLine("<tr>")
      For columna As Integer = 0 To odtg.Columns.Count - 1
        If odtg.Columns.Item(columna).Visible Then
          xls.Write("<td style='background:#FFA200; text-align:center; line-height:18px; Font(-weight): bold()' >" & odtg.Columns.Item(columna).HeaderText.ToString() & "</td>")
        End If
      Next
      xls.WriteLine("</tr>")
      For fila As Integer = 0 To odtg.Rows.Count - 1
        xls.WriteLine("<tr>")
        For columna As Integer = 0 To odtg.Columns.Count - 1
          If odtg.Columns.Item(columna).Visible Then
            If Not ((odtg.Item(columna, fila).Value Is DBNull.Value) OrElse odtg.Item(columna, fila).Value = Nothing) Then
              xls.Write("<td>" & odtg.Item(columna, fila).Value & "</td>")
            Else
              xls.Write("<td> </td>")
            End If
          End If
        Next
        xls.WriteLine("</tr>")
      Next
      xls.WriteLine("</TABLE>")
    Next
    xls.Flush()
    xls.Close()
    xls.Dispose()
    AbrirDocumento(archivo)
  End Sub

  ''' <summary>
  ''' Exporta a Excel Lista de DataGridView por medio de XML
  ''' </summary>
  Public Shared Sub ExportarExcel_XML(ByVal objListDtg As List(Of DataGridView))
    'averiguando si es que existe el directorio a exportar
    Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tempo\"
    If IO.Directory.Exists(path) = False Then
      IO.Directory.CreateDirectory(path)
    End If
    Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xml" 'xml
    Dim xls As New IO.StreamWriter(archivo, True, Encoding.UTF8)
    xls.WriteLine("<?xml version=""1.0""?>")
    xls.WriteLine("<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""")
    xls.WriteLine("xmlns:o=""urn:schemas-microsoft-com:office:office""")
    xls.WriteLine("xmlns:x=""urn:schemas-microsoft-com:office:excel""")
    xls.WriteLine("xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""")
    xls.WriteLine("xmlns:html=""http://www.w3.org/TR/REC-html40"">")

    For Each odtg As DataGridView In objListDtg
      xls.WriteLine("<Worksheet ss:Name=""" & odtg.Tag.ToString & """>")
      xls.WriteLine(" <Table> ")
      xls.WriteLine(" <Row>")
      For columna As Integer = 0 To odtg.Columns.Count - 1
        xls.WriteLine(" <Cell><Data ss:Type=""String"">" & odtg.Columns.Item(columna).HeaderText.ToString & "</Data></Cell>")
      Next
      xls.WriteLine(" </Row>")
      For fila As Integer = 0 To odtg.Rows.Count - 1
        xls.WriteLine("<Row>")
        For columna As Integer = 0 To odtg.Columns.Count - 1
          If odtg.Columns.Item(columna).Visible Then
            xls.WriteLine("<Cell><Data ss:Type=""String"">" & odtg.Item(columna, fila).Value.ToString() & "</Data></Cell>")
          End If
        Next
        xls.WriteLine(" </Row>")
      Next
      xls.WriteLine(" </Table>")
      xls.WriteLine(" </Worksheet>")

    Next
    xls.WriteLine(" </Workbook>")
    xls.Flush()
    xls.Close()
    xls.Dispose()
    AbrirDocumento(archivo)
  End Sub

#Region "Exportar Excel lista de DataGridView con oledb"

  Public Shared Sub ListaATexto(ByVal objListDtg As DataGridView, ByVal Archivo As String, ByVal Separador As String)
    Using sw As New StreamWriter(Archivo, False, System.Text.Encoding.Default)
      For I As Integer = 0 To objListDtg.ColumnCount - 1
        sw.Write(objListDtg.Columns(I).HeaderText)
        If I = objListDtg.ColumnCount - 1 Then Exit For
        sw.Write(Separador)
      Next
      sw.WriteLine()
      For I As Integer = 0 To objListDtg.Rows.Count - 1
        For J As Integer = 0 To objListDtg.Columns.Count - 1
          sw.Write(objListDtg.Rows(I).Cells(J).Value)
          If J = objListDtg.ColumnCount - 1 Then Exit For
          sw.Write(Separador)
        Next
        sw.WriteLine()
      Next
    End Using
  End Sub

  Public Shared Sub TextoAExcel(ByVal Archivo As String, ByVal Hoja As String, Optional ByVal Eliminar As Boolean = True)
    Dim Ruta As String = Application.StartupPath
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
    Dim Nombre As String = Path.GetFileNameWithoutExtension(Archivo)
    Dim ArchivoExcel As String = Ruta & "\" & Nombre & ".xls"
    If Eliminar Then
      If File.Exists(ArchivoExcel) Then File.Delete(ArchivoExcel)
    End If
    Using con As New OleDbConnection("provider=Microsoft.Jet.oledb.4.0;data source=" & Ruta & ";extended properties=Text;")
      con.Open()
      Dim cmd As New OleDbCommand("Select * Into [" & Hoja.Trim & "] In ''[Excel 8.0;Database=" & ArchivoExcel & "]From " & Nombre & "#TXT", con)
      cmd.ExecuteNonQuery()
    End Using
  End Sub

  ''' <summary>
  ''' Exporta a Excel Lista de DataGridView por medio de oledb
  ''' </summary>
  Public Shared Sub ExportarExcel_OLEDB(ByVal objListDtg As List(Of DataGridView), ByVal Separador As String, ByVal Formato As Boolean)
    Dim Archivo As String = "Exportar"
    Dim ArchivoExcel As String = Application.StartupPath & "\" & Archivo & ".xls"
    If File.Exists(ArchivoExcel) Then File.Delete(ArchivoExcel)
    For Each odtg As DataGridView In objListDtg
      ListaATexto(odtg, Archivo & ".txt", ",")
      TextoAExcel(Archivo & ".txt", odtg.Tag.Trim.ToString, False)
    Next
    'FormatearLibro(ArchivoExcel)
    AbrirDocumento(ArchivoExcel)
  End Sub

  Private Shared Sub FormatearLibro(ByVal Archivo As String)
    If File.Exists(Archivo) Then
      Dim xls As Object
      xls = CreateObject("scalc.Application")
      xls.Visible = False
      Dim Libro As Object = xls.Workbooks.Open(Archivo)
      With Libro
        Dim Hoja As Object = .Sheets.Item(1)
        Dim Rango As Object = xls.Selection.CurrentRegion
        Rango.Rows(1).Font.Bold = True
        Rango.Rows(1).Font.ColorIndex = 15
        Rango.Rows(1).Borders(9).LineStyle = 1
        For I As Integer = 7 To 11
          Rango.Borders(I).LineStyle = 1
        Next
        Dim Columna As Object = Hoja.Columns.AutoFit
        Rango.Select()
        Rango.Copy()
        For I As Integer = 2 To .Sheets.Count
          Dim Hoja2 As Object = .Sheets.Item(I)
          Hoja2.Select()
          Hoja2.Range("A1").PasteSpecial(Paste:=-4122, Operation:=-4142, SkipBlanks:=False, Transpose:=False)
          Columna = Hoja2.Columns.AutoFit()
        Next
        xls.CutCopyMode = False
        Columna = Nothing
        Rango = Nothing
        Hoja = Nothing
      End With
      Libro.Close(True)
      Libro = Nothing
      xls.Quit()
      xls = Nothing
    End If
  End Sub

#End Region

  ''' <summary>
  ''' Abre cualquier tipo de documento
  ''' </summary>
  Public Shared Sub AbrirDocumento(ByVal Path As String)
    Try
      Dim InfoProceso As New System.Diagnostics.ProcessStartInfo
      Dim Proceso As New System.Diagnostics.Process
      With InfoProceso
        .FileName = Path
        .CreateNoWindow = True
        .ErrorDialog = True
        .UseShellExecute = True
        .WindowStyle = ProcessWindowStyle.Normal
      End With
      Proceso.StartInfo = InfoProceso
      Proceso.Start()
      Proceso.Dispose()
    Catch
    End Try
  End Sub

  ''' <summary>
  ''' Obtener Parte Numerica
  ''' </summary>
  Public Shared Function ParteNumerica(ByVal obj As String) As String

    Dim cadena As String = ""
    Dim posPuntoDec As Int32 = 0
    cadena = obj.Trim
    If (IsNumeric(cadena)) Then
      posPuntoDec = cadena.IndexOf(".")
      If (posPuntoDec <> -1) Then
        cadena = cadena.Substring(0, posPuntoDec)
      End If
    Else
      cadena = 0
    End If
    Return cadena
  End Function
  Public Shared Function paginarDataDridView(ByVal dtPaginar As DataTable, ByVal inicial As Integer, ByVal final As Integer)
    Dim dtnew As New DataTable
    dtnew = dtPaginar.Clone
    For i As Integer = inicial To final
      If Not (i > (dtPaginar.Rows.Count - 1)) Then
        dtnew.ImportRow(dtPaginar.Rows(i))
      End If
    Next
    Return dtnew
  End Function

  Public Shared Function num_paginas(ByVal dtPaginar As DataTable, ByVal paginaSize As Integer)
    Dim TotalPaginas As Integer
    If dtPaginar.Rows.Count Mod paginaSize = 0 Then
      TotalPaginas = dtPaginar.Rows.Count / paginaSize
    Else
      Dim value As Double = dtPaginar.Rows.Count / paginaSize

      TotalPaginas = Math.Round(value) '+ 1
      If (value - Math.Round(value)) > 0 Then
        TotalPaginas = Math.Round(value) + 1
      Else
        TotalPaginas = Math.Round(value) '+ 1
      End If
    End If
    Return TotalPaginas
  End Function

  Public Shared Function ObjectToInt64(ByVal obj As Object) As Int64
    If (obj Is Nothing Or IsDBNull(obj)) Then Return 0 Else Return Convert.ToInt64(obj)
  End Function
  Public Shared Function ObjectToUInt64(ByVal obj As Object) As UInt64
    If (obj Is Nothing Or IsDBNull(obj)) Then Return 0 Else Return Convert.ToUInt64(obj)
  End Function
  Public Shared Function ObjectToUInt32(ByVal obj As Object) As UInt32
    If (obj Is Nothing Or IsDBNull(obj)) Then Return 0 Else Return Convert.ToUInt32(obj)
  End Function
  Public Shared Function ObjectToInt32(ByVal obj As Object) As Int32
    If (obj Is Nothing Or IsDBNull(obj)) Then Return 0 Else Return Convert.ToInt32(obj)
  End Function
  Public Shared Function ObjectToInt16(ByVal obj As Object) As Int16
    If (obj Is Nothing Or IsDBNull(obj)) Then Return 0 Else Return Convert.ToInt16(obj)
  End Function
  Public Shared Function ObjectToDecimal(ByVal obj As Object) As Decimal
    If (obj Is Nothing Or IsDBNull(obj)) Then Return 0.0 Else Return Convert.ToDecimal(obj)
  End Function
  Public Shared Function StringToDecimal(ByVal obj As Object) As Decimal
    If (obj Is Nothing Or obj = "") Then Return 0 Else Return Convert.ToDecimal(obj)
  End Function
  Public Shared Function ObjectToInteger(ByVal obj As Object) As Integer
    Return ObjectToInt32(obj)
  End Function
  Public Shared Function ObjectToDouble(ByVal obj As Object) As Double
    If (obj Is Nothing Or IsDBNull(obj)) Then Return 0 Else Return Convert.ToDouble(obj)
  End Function
  Public Shared Function ObjectToBoolean(ByVal obj As Object) As Double
    If (obj Is Nothing Or IsDBNull(obj)) Then Return 0 Else Return Convert.ToBoolean(obj)
  End Function
  Public Shared Function ObjectToString(ByVal obj As Object) As String
    If (obj Is Nothing Or IsDBNull(obj)) Then Return "" Else Return Convert.ToString(obj).Trim
  End Function
  Public Shared Function ObjectToDateTime(ByVal obj As Object) As DateTime
    If (obj Is Nothing Or IsDBNull(obj)) Then Return DateTime.MinValue Else Return Convert.ToDateTime(obj)
  End Function



  Public Shared Function ToDecimalCvr(ByVal obj As Object) As Decimal
    Dim Valor As String = ("" & obj).ToString.Trim
    If Valor = "" Then
      Valor = 0.0
    End If
    Return Convert.ToDecimal(Valor)
  End Function
  Public Shared Function ToStringCvr(ByVal obj As Object) As String
    Dim Valor As String = ("" & obj).ToString.Trim
    Return Valor
  End Function




  ''' <summary>
  ''' 
  ''' </summary>
  ''' <returns>Right</returns>
  ''' <remarks></remarks>
  Public Shared Function MaxFecha() As Int32
    Dim Fecha As Int32 = 0
    Dim ci As Globalization.CultureInfo
    ci = New Globalization.CultureInfo("es-ES")

    Try
      Fecha = Date.MaxValue.ToString("yyyy", ci)
    Catch ex As Exception
    End Try
    Return Fecha
  End Function

  Public Shared Function HashToXmlString(ByVal objLista As List(Of Hashtable)) As String
    Dim objXmlData As New StringBuilder()
    objXmlData.Append("<DATATABLE>")
    For i As Integer = 0 To objLista.Count - 1
      Dim objData As Hashtable = DirectCast(objLista(i), Hashtable)
      objXmlData.Append("<ROW>")
      For Each de As DictionaryEntry In objData
        Dim key As String = DirectCast(de.Key, String)
        Dim val As String = DirectCast(de.Value, String)
        objXmlData.Append("<" & key & ">" & val & "</" & key & ">")
      Next
      objXmlData.Append("</ROW>")
    Next
    objXmlData.Append("</DATATABLE>")

    'Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tempo\"
    'If IO.Directory.Exists(path) = False Then
    '    IO.Directory.CreateDirectory(path)
    'End If
    'Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".txt" 'xml
    'Dim xls As New IO.StreamWriter(archivo, True, Encoding.Default)
    'xls.WriteLine(objXmlData.ToString())
    'xls.Flush()
    'xls.Close()
    'xls.Dispose()
    'AbrirDocumento(archivo)

    Return objXmlData.ToString()
  End Function

  Public Shared Function HashToXmlString(ByVal objData As Hashtable) As String
    Dim objXmlData As New StringBuilder()
    objXmlData.Append("<DATATABLE>")
    objXmlData.Append("<ROW>")
    For Each de As DictionaryEntry In objData
      Dim key As String = DirectCast(de.Key, String)
      Dim val As String = DirectCast(de.Value, String)
      objXmlData.Append("<" & key & ">" & val & "</" & key & ">")
    Next
    objXmlData.Append("</ROW>")
    objXmlData.Append("</DATATABLE>")
    objXmlData.ToString()


    'Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tempo\"
    'If IO.Directory.Exists(path) = False Then
    '    IO.Directory.CreateDirectory(path)
    'End If
    'Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".txt" 'xml
    'Dim xls As New IO.StreamWriter(archivo, True, Encoding.Default)
    'xls.WriteLine(objXmlData.ToString())
    'xls.Flush()
    'xls.Close()
    'xls.Dispose()
    'AbrirDocumento(archivo)
    Return objXmlData.ToString()
  End Function

  Public Shared Function ConversionImagen(ByVal nombrearchivo As String) As Byte()
    'Declaramos fs para poder abrir la imagen.
    Dim fs As New FileStream(nombrearchivo, FileMode.Open)

    ' Declaramos un lector binario para pasar la imagen
    ' a bytes
    Dim br As New BinaryReader(fs)
    Dim imagen As Byte() = New Byte(CInt(fs.Length) - 1) {}
    br.Read(imagen, 0, CInt(fs.Length))
    br.Close()
    fs.Close()
    Return imagen
  End Function

  Public Shared Function LoadImageFromUrl(ByRef url As String, Optional ByVal originalsize As Boolean = False, Optional ByVal height As Integer = 80, Optional ByVal width As Integer = 60) As Bitmap
    Try
      Dim request As Net.HttpWebRequest = DirectCast(Net.HttpWebRequest.Create(url), Net.HttpWebRequest)
      Dim response As Net.HttpWebResponse = DirectCast(request.GetResponse, Net.HttpWebResponse)
      Dim img As Image = Image.FromStream(response.GetResponseStream())
      Dim objbitmap As Bitmap
      If originalsize = False Then
        objbitmap = New Bitmap(img, height, width)
      Else
        objbitmap = img
      End If
      response.Close()
      Return objbitmap
    Catch
      Return Nothing
    End Try
  End Function

  Public Shared Function ImageToByte(ByVal img As Image) As Byte()
    Try
      Dim imgStream As IO.MemoryStream = New IO.MemoryStream()

      img.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png)
      imgStream.Close()
      Dim byteArray As Byte() = imgStream.ToArray()
      imgStream.Dispose()
      Return byteArray
    Catch ex As Exception
      Return Nothing
    End Try

  End Function

  Public Shared Sub ExportExcelInventario(ByVal loCabecera As List(Of String), ByVal oDs As DataSet)
    '=====================================================================
    Dim path As String = Application.StartupPath.ToString
    Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
    Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)


    Dim objWorkBook As New HSSFWorkbook()
    Dim memoryStream As New MemoryStream()
    Dim objWorkSheet As New HSSFSheet(objWorkBook)

    Dim odtg As New DataTable
    Dim odtFiltro As New DataTable
    Dim odtTotales As New DataTable

    'odtFiltro = oDs.Tables(0)
    odtg = oDs.Tables(0)
    ' odtTotales = oDs.Tables(3)
    '  Dim NroItem As Integer = 1
    '=============Stilos======================
    Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
    Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
    Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

    Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
    oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
    oFontFiltro.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
    oFontFiltro.FontHeight = 165

    Dim oFontTitulo As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
    oFontTitulo.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
    oFontTitulo.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
    oFontTitulo.FontHeight = 210


    Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
    oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index
    oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
    oFontCab.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
    oFontCab.FontHeight = 165

    Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
    oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
    oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
    oFont.FontHeight = 170

    styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleFiltro.SetFont(oFontFiltro)
    styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
    styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND
    '===============================
    Dim styleTitulo As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
    styleTitulo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleTitulo.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleTitulo.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
    styleTitulo.SetFont(oFontTitulo)
    styleTitulo.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
    styleTitulo.FillPattern = FillPatternType.SOLID_FOREGROUND
    '===============================
    styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
    styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
    styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleCab.SetFont(oFontCab)
    styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
    styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND
    styleCab.BorderRight = CellBorderType.THIN
    styleCab.BorderBottom = CellBorderType.THIN
    styleCab.BorderLeft = CellBorderType.THIN
    styleCab.BorderTop = CellBorderType.THIN

    '===============================
    style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
    style.SetFont(oFont)
    style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
    style.FillPattern = FillPatternType.SOLID_FOREGROUND
    style.BorderRight = CellBorderType.THIN
    style.BorderBottom = CellBorderType.THIN
    style.BorderLeft = CellBorderType.THIN
    style.BorderTop = CellBorderType.THIN
    '===============================
    Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
    styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
    styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleNumber.SetFont(oFont)
    styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00") '"#.##0,00"
    styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
    styleNumber.BorderRight = CellBorderType.THIN
    styleNumber.BorderBottom = CellBorderType.THIN
    styleNumber.BorderLeft = CellBorderType.THIN
    styleNumber.BorderTop = CellBorderType.THIN

    '===============================
    Dim styleTotales As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
    styleTotales.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
    styleTotales.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleTotales.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleTotales.SetFont(oFontFiltro)
    styleTotales.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
    styleTotales.FillPattern = FillPatternType.SOLID_FOREGROUND
    styleTotales.BorderRight = CellBorderType.THIN
    styleTotales.BorderBottom = CellBorderType.THIN
    styleTotales.BorderLeft = CellBorderType.THIN
    styleTotales.BorderTop = CellBorderType.THIN
    '===============================

    Dim styleTextNegitra As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
    styleTextNegitra.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
    styleTextNegitra.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleTextNegitra.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleTextNegitra.SetFont(oFontFiltro)
    styleTextNegitra.FillPattern = FillPatternType.SOLID_FOREGROUND
    styleTextNegitra.BorderRight = CellBorderType.THIN
    styleTextNegitra.BorderBottom = CellBorderType.THIN
    styleTextNegitra.BorderLeft = CellBorderType.THIN
    styleTextNegitra.BorderTop = CellBorderType.THIN

    '===============================
    Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
    styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
    styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleMonto.SetFont(oFont)
    styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
    styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
    styleMonto.BorderRight = CellBorderType.THIN
    styleMonto.BorderBottom = CellBorderType.THIN
    styleMonto.BorderLeft = CellBorderType.THIN
    styleMonto.BorderTop = CellBorderType.THIN
    '===============================

    Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
    styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
    styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
    styleFecha.SetFont(oFont)
    styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
    styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND
    styleFecha.BorderRight = CellBorderType.THIN
    styleFecha.BorderBottom = CellBorderType.THIN
    styleFecha.BorderLeft = CellBorderType.THIN
    styleFecha.BorderTop = CellBorderType.THIN
    '============================================
    Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
    'create the anchor
    Dim anchor As HSSFClientAnchor
    anchor = New HSSFClientAnchor(0, 0, 0, 100, 0, 0, 0, 0)
    Dim rutaImagen As String = Application.StartupPath & " \Resources\logo.png"
    Try
      Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(rutaImagen, objWorkBook)), HSSFPicture)
      picture.Resize(0.4)
    Catch ex As Exception

    End Try

    '===================Se cargan Las Variables de Trabajo=====================
    Dim TotalKG As Double = 0
    Dim TotalM3 As Double = 0
    Dim TotalBUL As Double = 0
    Dim indice As Integer = 0
    Dim rowActual As Integer = 1
    Dim IndiceColum As Integer = 0
    Dim indiceRowAnt As Integer = 1
    Dim y As Integer = 0 'Posicion Inicial por la Izquierda
    Dim txtFiltro As String = "" 'Valor del filtro en posicion Inicial Actual
    Dim txtFiltro2 As String = "" 'Valor del filtro en posicion Final Actual
    Dim addEspacios As Integer = 8
    Dim n As Integer = 0
    Dim RowInicio As Integer = 0
    Dim RowInicioAnt As Integer = 0

    '---------------Creamos los rows--------------
    objWorkSheet = objWorkBook.CreateSheet("Inventario")
    For s As Integer = 0 To (odtg.Rows.Count + 10 + loCabecera.Count + addEspacios)
      objWorkSheet.CreateRow(s)
    Next

    'Titulo Principal
    objWorkSheet.GetRow(indice - 1 + rowActual).CreateCell(3).CellStyle = styleTitulo
    txtFiltro = loCabecera(0).ToString()
    objWorkSheet.GetRow(indice - 1 + rowActual).GetCell(3).SetCellValue(txtFiltro)
    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(indice - 1 + rowActual, 3, indice - 1 + rowActual, 6))
    indice = indice + 3

    ''Se crear Los Titulos de Los filtros
    For Xfiltro As Integer = 1 To loCabecera.Count - 1
      objWorkSheet.GetRow(indice - 1 + rowActual).CreateCell(0).CellStyle = styleFiltro
      txtFiltro = loCabecera(Xfiltro).ToString()
      objWorkSheet.GetRow(indice - 1 + rowActual).GetCell(0).SetCellValue(txtFiltro)
      objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(indice - 1 + rowActual, 0, indice - 1 + rowActual, 5))
      indice = indice + 1
    Next

    'Nombre Para los Detalles
    ''Cambiamos los Indices de las Columnas (Ordenamos el Detalle)
    '-------------------------------------------------

    '-------------------------------------------------
    '-------------------------------------------------
    indice = indice + 1
    '===Generamos la Cabecera
    For j As Integer = 0 To odtg.Columns.Count - 1
      objWorkSheet.GetRow(indice - 1 + rowActual).CreateCell(IndiceColum).CellStyle = styleCab
      Dim valorCab As String = odtg.Columns(j).ColumnName.ToString().Trim()
      objWorkSheet.GetRow(indice - 1 + rowActual).GetCell(IndiceColum).SetCellValue(valorCab)
      IndiceColum = IndiceColum + 1
    Next

    'Generamos los Registros del Detalle
    For i As Integer = 0 To odtg.Rows.Count - 1
      Dim txtFiltro_ As String = ""
      IndiceColum = 0
      For x As Integer = 0 To odtg.Columns.Count - 1
        y = IndiceColum
                Dim celda As String = odtg.Rows(i).Item(x).ToString().Trim()
                Dim TypoDato As String = odtg.Columns.Item(x).DataType.ToString
                If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                    If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                        objWorkSheet.GetRow(indice + rowActual).CreateCell(x).CellStyle = styleMonto
                        If celda Is "" Then
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue("")
                        Else
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                        End If

                    Else
                        If celda Is "" Then
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue("")
                        Else
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                        End If
                    End If
                    '****
                Else
                    If TypoDato.Equals("System.Date") Then
                        celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                        objWorkSheet.GetRow(indice + rowActual).CreateCell(x).CellStyle = styleFecha
                        objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf TypoDato.Equals("System.DateTime") Then
                        If celda <> "" Then
                            celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).CellStyle = styleFecha
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue(celda)
                        Else
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).SetCellValue(celda)
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).CellStyle = styleFiltro
                        End If

                    Else
                        If celda Is DBNull.Value Then
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).SetCellValue("")
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).CellStyle = styleFiltro
                        Else
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).SetCellValue(celda)
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).CellStyle = style
                        End If

                    End If
                End If

                'If IsNumeric(celda) Then
                '  If odtg.Columns(x).ColumnName.Equals("Guía Proveedor") Then
                '    celda = HelpClass.SetFormatoGuiaRemision(celda)
                '    objWorkSheet.GetRow(indice + rowActual).CreateCell(y).CellStyle = style
                '    objWorkSheet.GetRow(indice + rowActual).GetCell(y).SetCellValue(celda)
                '  ElseIf odtg.Columns(x).ColumnName.Equals("Orden de Compra") Then
                '    objWorkSheet.GetRow(indice + rowActual).CreateCell(y).CellStyle = style
                '    objWorkSheet.GetRow(indice + rowActual).GetCell(y).SetCellValue(celda)
                '  ElseIf celda.Contains(".") Then
                '    objWorkSheet.GetRow(indice + rowActual).CreateCell(y).CellStyle = styleMonto
                '    objWorkSheet.GetRow(indice + rowActual).GetCell(y).SetCellValue(Convert.ToDouble(celda))
                '  ElseIf celda.Substring(0, 1) = "0" AndAlso celda.Length > 1 Then
                '    objWorkSheet.GetRow(indice + rowActual).CreateCell(y).CellStyle = style
                '    objWorkSheet.GetRow(indice + rowActual).GetCell(y).SetCellValue(celda)
                '  Else
                '    objWorkSheet.GetRow(indice + rowActual).CreateCell(y).CellStyle = styleNumber
                '    objWorkSheet.GetRow(indice + rowActual).GetCell(y).SetCellValue(Convert.ToDouble(celda))
                '  End If
                'Else
                '  If IsDate(celda) Then
                '    celda = String.Format("{0:d}", CDate(celda))
                '    objWorkSheet.GetRow(indice + rowActual).CreateCell(y).CellStyle = styleFecha
                '    objWorkSheet.GetRow(indice + rowActual).GetCell(y).SetCellValue(celda)
                '  Else
                '    objWorkSheet.GetRow(indice + rowActual).CreateCell(y).SetCellValue(odtg.Rows(i).Item(x).ToString().Trim())
                '    objWorkSheet.GetRow(indice + rowActual).GetCell(y).CellStyle = style
                '  End If
                'End If
        IndiceColum = IndiceColum + 1
      Next
      'NroItem = NroItem + 1
      TotalBUL += Convert.ToDouble(odtg.Rows(i)("CANTIDAD_BULTO"))
      TotalKG += Convert.ToDouble(odtg.Rows(i)("PESO_BULTO"))
      TotalM3 += Convert.ToDouble(odtg.Rows(i)("VOLUMEN"))



      indice = indice + 1
    Next
    indice = indice + 1

    '===Validaindicetotalesmos  ToTALES

    objWorkSheet.GetRow(indice + rowActual).CreateCell(odtg.Columns("CANTIDAD_BULTO").Ordinal - 1).CellStyle = styleTotales
    objWorkSheet.GetRow(indice + rowActual).GetCell(odtg.Columns("CANTIDAD_BULTO").Ordinal - 1).SetCellValue("TOTAL")

    txtFiltro2 = TotalBUL
    If txtFiltro2 = "" Then txtFiltro2 = "0"
    objWorkSheet.GetRow(indice + rowActual).CreateCell(odtg.Columns("CANTIDAD_BULTO").Ordinal).CellStyle = styleTotales
    objWorkSheet.GetRow(indice + rowActual).GetCell(odtg.Columns("CANTIDAD_BULTO").Ordinal).SetCellValue(Convert.ToDouble(txtFiltro2))

    txtFiltro2 = TotalKG
    If txtFiltro2 = "" Then txtFiltro2 = "0"
    objWorkSheet.GetRow(indice + rowActual).CreateCell(odtg.Columns("PESO_BULTO").Ordinal).CellStyle = styleTotales
    objWorkSheet.GetRow(indice + rowActual).GetCell(odtg.Columns("PESO_BULTO").Ordinal).SetCellValue(Convert.ToDouble(txtFiltro2))

    txtFiltro2 = TotalM3
    If txtFiltro2 = "" Then txtFiltro2 = "0"
    objWorkSheet.GetRow(indice + rowActual).CreateCell(odtg.Columns("VOLUMEN").Ordinal).CellStyle = styleTotales
    objWorkSheet.GetRow(indice + rowActual).GetCell(odtg.Columns("VOLUMEN").Ordinal).SetCellValue(Convert.ToDouble(txtFiltro2))

    indice = indice + 1

    objWorkSheet.DefaultColumnWidth = 25
    For z As Integer = 0 To odtg.Columns.Count + 1
      objWorkSheet.AutoSizeColumn(z)
      If z = 0 Then
        objWorkSheet.SetColumnWidth(0, 10000)
      End If
    Next

    For intSheet As Integer = 1 To 2
      Dim odtResumen As New DataTable
      odtResumen = oDs.Tables(intSheet)

      'Crear Una Nueva Hoja Para el Resumen 
      objWorkSheet = objWorkBook.CreateSheet("Resumen_" + intSheet.ToString)
      For s As Integer = 0 To (odtResumen.Rows.Count + loCabecera.Count + 60 + addEspacios)
        '(odtg.Rows.Count + odtFiltro.Rows.Count + odtResumen.Rows.Count + odtTotales.Rows.Count) 
        objWorkSheet.CreateRow(s)
      Next

      indice = 1
      ' Se crear Los Titulos de Los filtros en la Nueva Hoja
      For Xfiltro As Integer = 1 To loCabecera.Count - 1
        objWorkSheet.GetRow(indice - 1).CreateCell(0).CellStyle = styleFiltro
        txtFiltro = loCabecera(Xfiltro).ToString()
        objWorkSheet.GetRow(indice - 1).GetCell(0).SetCellValue(txtFiltro)
        objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(indice - 1, 0, indice - 1, 5))
        indice = indice + 1
      Next
      indice = indice + 3
      '===Generamos la Cabecera Del Resumen
      Dim INICIO, FIN As Integer
      If intSheet = 1 Then
        INICIO = 2
      Else
        INICIO = 3
      End If
      FIN = 2

      For j As Integer = 0 To odtResumen.Columns.Count - 1
        y = j
        objWorkSheet.GetRow(indice - 1).CreateCell(y).CellStyle = styleCab
        If odtResumen.Columns(j).ColumnName.Equals("ORDEN_COMPRA") Then
          objWorkSheet.GetRow(indice - 1).GetCell(y).SetCellValue("ORDEN_COMPRA")
        Else
          Dim valorCab As String = Split(odtResumen.Columns(j).ColumnName.ToString().Trim(), "_")(0)
          objWorkSheet.GetRow(indice - 1).GetCell(y).SetCellValue(valorCab)
        End If


        If j > 0 AndAlso FIN - INICIO = 2 Then
          If intSheet = 1 OrElse (odtResumen.Columns(j).ColumnName.ToString().Trim().IndexOf("_") <> -1 AndAlso intSheet = 2) Then
            objWorkSheet.GetRow(indice - 2).CreateCell(y).CellStyle = styleCab
            objWorkSheet.GetRow(indice - 2).CreateCell(y + 1).CellStyle = styleCab
            objWorkSheet.GetRow(indice - 2).CreateCell(y + 2).CellStyle = styleCab
            Dim valorCab1 As String = Split(odtResumen.Columns(j).ColumnName.ToString().Trim(), "_")(1)
            objWorkSheet.GetRow(indice - 2).GetCell(y).SetCellValue(valorCab1)
            Dim Region As New NPOI.SS.Util.Region(indice - 2, INICIO, indice - 2, FIN)
            objWorkSheet.AddMergedRegion(Region)
            INICIO = FIN + 1
            ' Else
            ' INICIO = 3
            ' FIN = 3
          End If

        End If
        FIN = FIN + 1
      Next

      'Crea el Detalle del Resumen
      Dim strCliente As String = ""
      Dim bPrimero As Boolean
      Dim lstInicioReg As New List(Of String)

      For z As Integer = 0 To odtResumen.Rows.Count - 1 '
        '===Verificamos Cambio de  Grupo de Datos===
        If z = 0 OrElse odtResumen.Rows(z).Item("CLIENTE").ToString = strCliente Then
          'indice = indice + 1
          bPrimero = False
        Else
          indice = indice + 1
          bPrimero = True
          '  NroItem = 1
        End If

        '===Validamos inicio de un nuevo grupo de registros para poner las cabeceras
        If bPrimero OrElse z = 0 Then
          ''''''''''''''''''''''''''' Se Coloca la Imagen en el Lado Izquierdo
          ' strCliente = odtResumen.Rows(z).Item("CLIENTE").ToString().Trim()
          objWorkSheet.GetRow(indice + z).CreateCell(0).SetCellValue(odtResumen.Rows(z).Item("CLIENTE").ToString().Trim())
          objWorkSheet.GetRow(indice + z).GetCell(0).CellStyle = styleTextNegitra
          RowInicio = indice + z
        End If

        ' ''===Generamos Los Detalles de los Registros===
        IndiceColum = 1
        For x As Integer = 1 To odtResumen.Columns.Count - 1 '
          y = IndiceColum

          Dim celda As String = odtResumen.Rows(z).Item(x).ToString().Trim()

          If IsNumeric(celda) Then
            If odtg.Columns(x).ColumnName.Equals("ORDEN_COMPRA") Then
              objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = style
              objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(celda)
            ElseIf celda.Contains(".") Then
              objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = styleMonto
              objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(Convert.ToDouble(celda))
            ElseIf celda.Substring(0, 1) = "0" AndAlso celda.Length > 1 Then
              objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = style
              objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(celda)
            Else
              objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = styleNumber
              objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(Convert.ToDouble(celda))
            End If
          Else
            If IsDate(celda) Then
              celda = String.Format("{0:d}", CDate(celda))
              objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = styleFecha
              objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(celda)
            Else
              objWorkSheet.GetRow(indice + z).CreateCell(y).SetCellValue(celda)
              objWorkSheet.GetRow(indice + z).GetCell(y).CellStyle = style
            End If
          End If

          'If IsNumeric(celda) Then
          '    objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = styleMonto
          '    objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(Convert.ToDouble(celda))
          'Else
          '    objWorkSheet.GetRow(indice + z).CreateCell(y).SetCellValue()
          '    objWorkSheet.GetRow().GetCell(y).CellStyle = style
          'End If
          IndiceColum = IndiceColum + 1

        Next

        '===Obtenemos el Row de la ultima Variable
        strCliente = odtResumen.Rows(z).Item("CLIENTE").ToString
        ' NroItem = NroItem + 1
        ' =Validamos Finalización de un nuevo grupo de registros

        If bPrimero Then
          IndiceColum = 0
          For j As Integer = 1 To odtResumen.Columns.Count - 2 '
            y = IndiceColum + 3
            If j = 1 Then
              objWorkSheet.GetRow(indice + z - 1).CreateCell(1).CellStyle = styleTotales
              objWorkSheet.GetRow(indice + z - 1).GetCell(1).SetCellValue("SUB TOTAL")
            End If

            Dim cIni As String
            Dim cFin As String
            cIni = HelpClass.LetraNumero(y) & (RowInicioAnt + 1).ToString
            cFin = HelpClass.LetraNumero(y) & (indice + z - 1).ToString

            Dim sumKg As String = "SUM(" & cIni & ":" & cFin & ")"
            objWorkSheet.GetRow(indice + z - 1).CreateCell(y - 1).CellStyle = styleTotales
            objWorkSheet.GetRow(indice + z - 1).GetCell(y - 1).CellFormula = (sumKg)

            'Borra lo de la Columna con la sumatoria de las O/C
            If intSheet = 2 Then
              objWorkSheet.GetRow(indice + z - 1).GetCell(2).CellFormula = ""
            Else

            End If


            Dim Region As New NPOI.SS.Util.Region(RowInicioAnt + 1, 0, indice + z - 1, 0)
            objWorkSheet.AddMergedRegion(Region)
            IndiceColum = IndiceColum + 1
          Next
          ' Almacena las Posiciones de Los Totales
          lstInicioReg.Add(indice + z)
        End If
        RowInicioAnt = RowInicio
      Next

      ' Suma Los Totales Generados
      IndiceColum = 0
      'Almacena la ultima Posicion para Los Totales
      lstInicioReg.Add(indice + odtResumen.Rows.Count + 1)
      For z As Integer = 1 To odtResumen.Columns.Count - 2 '

        y = IndiceColum + 3
        If z = 1 Then
          objWorkSheet.GetRow(indice + odtResumen.Rows.Count).CreateCell(1).CellStyle = styleTotales
          objWorkSheet.GetRow(indice + odtResumen.Rows.Count).GetCell(1).SetCellValue("SUB TOTAL")
          objWorkSheet.GetRow(indice + odtResumen.Rows.Count + 2).CreateCell(y - 2).CellStyle = styleTotales
          objWorkSheet.GetRow(indice + odtResumen.Rows.Count + 2).GetCell(y - 2).SetCellValue("TOTALES")
        End If
        Dim cIniV As String
        Dim cFinV As String
        cIniV = HelpClass.LetraNumero(y) & (RowInicioAnt + 1).ToString
        cFinV = HelpClass.LetraNumero(y) & (indice + odtResumen.Rows.Count).ToString

        Dim sumKg As String = "SUM(" & cIniV & ":" & cFinV & ")"
        objWorkSheet.GetRow(indice + odtResumen.Rows.Count).CreateCell(y - 1).CellStyle = styleTotales
        objWorkSheet.GetRow(indice + odtResumen.Rows.Count).GetCell(y - 1).CellFormula = (sumKg)

        Dim Region As New NPOI.SS.Util.Region(RowInicioAnt + 1, 0, indice + odtResumen.Rows.Count, 0)
        objWorkSheet.AddMergedRegion(Region)
        sumKg = "SUM("

        For reg As Integer = 0 To lstInicioReg.Count - 2
          sumKg = sumKg & HelpClass.LetraNumero(y) & lstInicioReg(reg).ToString & ","
        Next
        sumKg = sumKg & HelpClass.LetraNumero(y) & lstInicioReg(lstInicioReg.Count - 1).ToString & ")"
        objWorkSheet.GetRow(indice + odtResumen.Rows.Count + 2).CreateCell(y - 1).CellStyle = styleTotales
        objWorkSheet.GetRow(indice + odtResumen.Rows.Count + 2).GetCell(y - 1).CellFormula = (sumKg)

        'Borra lo de la Columna con la sumatoria de las O/C
        If intSheet = 2 Then
          objWorkSheet.GetRow(indice + odtResumen.Rows.Count).GetCell(2).CellFormula = ""
          objWorkSheet.GetRow(indice + odtResumen.Rows.Count + 2).GetCell(2).CellFormula = ""
        End If
        IndiceColum = IndiceColum + 1
      Next


      For z As Integer = 0 To odtResumen.Columns.Count
        objWorkSheet.AutoSizeColumn(z)
        If z = 0 Then
          objWorkSheet.SetColumnWidth(0, 10000)
        End If
      Next
    Next

    objWorkBook.Write(fs)
    fs.Close()
    AbrirDocumento(archivo)


    End Sub

    Public Shared Sub ExportExcelResumenMensualAlmacenesInventario(ByVal loCabecera As List(Of String), ByVal oDs As DataSet)
        '=====================================================================
        Dim path As String = Application.StartupPath.ToString
        Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
        Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)


        Dim objWorkBook As New HSSFWorkbook()
        Dim memoryStream As New MemoryStream()
        Dim objWorkSheet As New HSSFSheet(objWorkBook)

        Dim odtg As New DataTable
        Dim odtFiltro As New DataTable
        Dim odtTotales As New DataTable

        'odtFiltro = oDs.Tables(0)
        odtg = oDs.Tables(0)
        ' odtTotales = oDs.Tables(3)
        '  Dim NroItem As Integer = 1
        '=============Stilos======================
        Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

        Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontFiltro.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
        oFontFiltro.FontHeight = 165

        Dim oFontTitulo As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontTitulo.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontTitulo.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
        oFontTitulo.FontHeight = 210


        Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index
        oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFontCab.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
        oFontCab.FontHeight = 165

        Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
        oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
        oFont.FontHeight = 170

        styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFiltro.SetFont(oFontFiltro)
        styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        Dim styleTitulo As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleTitulo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleTitulo.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleTitulo.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleTitulo.SetFont(oFontTitulo)
        styleTitulo.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleTitulo.FillPattern = FillPatternType.SOLID_FOREGROUND
        '===============================
        styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
        styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleCab.SetFont(oFontCab)
        styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND
        styleCab.BorderRight = CellBorderType.THIN
        styleCab.BorderBottom = CellBorderType.THIN
        styleCab.BorderLeft = CellBorderType.THIN
        styleCab.BorderTop = CellBorderType.THIN

        '===============================
        style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
        style.SetFont(oFont)
        style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        style.FillPattern = FillPatternType.SOLID_FOREGROUND
        style.BorderRight = CellBorderType.THIN
        style.BorderBottom = CellBorderType.THIN
        style.BorderLeft = CellBorderType.THIN
        style.BorderTop = CellBorderType.THIN
        '===============================
        Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleNumber.SetFont(oFont)
        styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00") '"#.##0,00"
        styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
        styleNumber.BorderRight = CellBorderType.THIN
        styleNumber.BorderBottom = CellBorderType.THIN
        styleNumber.BorderLeft = CellBorderType.THIN
        styleNumber.BorderTop = CellBorderType.THIN

        '===============================
        Dim styleTotales As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleTotales.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
        styleTotales.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleTotales.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleTotales.SetFont(oFontFiltro)
        styleTotales.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
        styleTotales.FillPattern = FillPatternType.SOLID_FOREGROUND
        styleTotales.BorderRight = CellBorderType.THIN
        styleTotales.BorderBottom = CellBorderType.THIN
        styleTotales.BorderLeft = CellBorderType.THIN
        styleTotales.BorderTop = CellBorderType.THIN
        '===============================

        Dim styleTextNegitra As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleTextNegitra.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
        styleTextNegitra.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleTextNegitra.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleTextNegitra.SetFont(oFontFiltro)
        styleTextNegitra.FillPattern = FillPatternType.SOLID_FOREGROUND
        styleTextNegitra.BorderRight = CellBorderType.THIN
        styleTextNegitra.BorderBottom = CellBorderType.THIN
        styleTextNegitra.BorderLeft = CellBorderType.THIN
        styleTextNegitra.BorderTop = CellBorderType.THIN

        '===============================
        Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
        styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleMonto.SetFont(oFont)
        styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
        styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
        styleMonto.BorderRight = CellBorderType.THIN
        styleMonto.BorderBottom = CellBorderType.THIN
        styleMonto.BorderLeft = CellBorderType.THIN
        styleMonto.BorderTop = CellBorderType.THIN
        '===============================

        Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
        styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
        styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
        styleFecha.SetFont(oFont)
        styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
        styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND
        styleFecha.BorderRight = CellBorderType.THIN
        styleFecha.BorderBottom = CellBorderType.THIN
        styleFecha.BorderLeft = CellBorderType.THIN
        styleFecha.BorderTop = CellBorderType.THIN
        '============================================
        Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
        'create the anchor
        Dim anchor As HSSFClientAnchor
        anchor = New HSSFClientAnchor(0, 0, 0, 100, 0, 0, 0, 0)
        Dim rutaImagen As String = Application.StartupPath & " \Resources\logo.png"
        Try
            Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(rutaImagen, objWorkBook)), HSSFPicture)
            picture.Resize(0.4)
        Catch ex As Exception

        End Try

        '===================Se cargan Las Variables de Trabajo=====================
        Dim TotalKG As Double = 0
        Dim TotalM3 As Double = 0
        Dim TotalBUL As Double = 0
        Dim indice As Integer = 0
        Dim rowActual As Integer = 1
        Dim IndiceColum As Integer = 0
        Dim indiceRowAnt As Integer = 1
        Dim y As Integer = 0 'Posicion Inicial por la Izquierda
        Dim txtFiltro As String = "" 'Valor del filtro en posicion Inicial Actual
        Dim txtFiltro2 As String = "" 'Valor del filtro en posicion Final Actual
        Dim addEspacios As Integer = 8
        Dim n As Integer = 0
        Dim RowInicio As Integer = 0
        Dim RowInicioAnt As Integer = 0

        '---------------Creamos los rows--------------
        objWorkSheet = objWorkBook.CreateSheet("Inventario")
        For s As Integer = 0 To (odtg.Rows.Count + 10 + loCabecera.Count + addEspacios)
            objWorkSheet.CreateRow(s)
        Next

        'Titulo Principal
        objWorkSheet.GetRow(indice - 1 + rowActual).CreateCell(3).CellStyle = styleTitulo
        txtFiltro = loCabecera(0).ToString()
        objWorkSheet.GetRow(indice - 1 + rowActual).GetCell(3).SetCellValue(txtFiltro)
        objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(indice - 1 + rowActual, 3, indice - 1 + rowActual, 6))
        indice = indice + 3

        ''Se crear Los Titulos de Los filtros
        For Xfiltro As Integer = 1 To loCabecera.Count - 1
            objWorkSheet.GetRow(indice - 1 + rowActual).CreateCell(0).CellStyle = styleFiltro
            txtFiltro = loCabecera(Xfiltro).ToString()
            objWorkSheet.GetRow(indice - 1 + rowActual).GetCell(0).SetCellValue(txtFiltro)
            objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(indice - 1 + rowActual, 0, indice - 1 + rowActual, 5))
            indice = indice + 1
        Next

        'Nombre Para los Detalles
        ''Cambiamos los Indices de las Columnas (Ordenamos el Detalle)
        '-------------------------------------------------

        '-------------------------------------------------
        '-------------------------------------------------
        indice = indice + 1
        '===Generamos la Cabecera
        For j As Integer = 0 To odtg.Columns.Count - 1
            objWorkSheet.GetRow(indice - 1 + rowActual).CreateCell(IndiceColum).CellStyle = styleCab
            Dim valorCab As String = odtg.Columns(j).ColumnName.ToString().Trim()
            objWorkSheet.GetRow(indice - 1 + rowActual).GetCell(IndiceColum).SetCellValue(valorCab)
            IndiceColum = IndiceColum + 1
        Next

        'Generamos los Registros del Detalle
        For i As Integer = 0 To odtg.Rows.Count - 1
            Dim txtFiltro_ As String = ""
            IndiceColum = 0
            For x As Integer = 0 To odtg.Columns.Count - 1
                y = IndiceColum
                Dim celda As String = odtg.Rows(i).Item(x).ToString().Trim()
                Dim TypoDato As String = odtg.Columns.Item(x).DataType.ToString
                If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                    If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                        objWorkSheet.GetRow(indice + rowActual).CreateCell(x).CellStyle = styleMonto
                        If celda Is "" Then
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue("")
                        Else
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                        End If

                    Else
                        If celda Is "" Then
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue("")
                        Else
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                        End If
                    End If
                    '****
                Else
                    If TypoDato.Equals("System.Date") Then
                        celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                        objWorkSheet.GetRow(indice + rowActual).CreateCell(x).CellStyle = styleFecha
                        objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf TypoDato.Equals("System.DateTime") Then
                        If celda <> "" Then
                            celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).CellStyle = styleFecha
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).SetCellValue(celda)
                        Else
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).SetCellValue(celda)
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).CellStyle = styleFiltro
                        End If

                    Else
                        If celda Is DBNull.Value Then
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).SetCellValue("")
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).CellStyle = styleFiltro
                        Else
                            objWorkSheet.GetRow(indice + rowActual).CreateCell(x).SetCellValue(celda)
                            objWorkSheet.GetRow(indice + rowActual).GetCell(x).CellStyle = style
                        End If

                    End If
                End If

                
                IndiceColum = IndiceColum + 1
            Next
            'NroItem = NroItem + 1
            TotalBUL += Convert.ToDouble(odtg.Rows(i)("CANTIDAD_BULTO"))
            TotalKG += Convert.ToDouble(odtg.Rows(i)("PESO_BULTO"))
            TotalM3 += Convert.ToDouble(odtg.Rows(i)("VOLUMEN"))



            indice = indice + 1
        Next
        indice = indice + 1

        '===Validaindicetotalesmos  ToTALES

        objWorkSheet.GetRow(indice + rowActual).CreateCell(odtg.Columns("CANTIDAD_BULTO").Ordinal - 1).CellStyle = styleTotales
        objWorkSheet.GetRow(indice + rowActual).GetCell(odtg.Columns("CANTIDAD_BULTO").Ordinal - 1).SetCellValue("TOTAL")

        txtFiltro2 = TotalBUL
        If txtFiltro2 = "" Then txtFiltro2 = "0"
        objWorkSheet.GetRow(indice + rowActual).CreateCell(odtg.Columns("CANTIDAD_BULTO").Ordinal).CellStyle = styleTotales
        objWorkSheet.GetRow(indice + rowActual).GetCell(odtg.Columns("CANTIDAD_BULTO").Ordinal).SetCellValue(Convert.ToDouble(txtFiltro2))

        txtFiltro2 = TotalKG
        If txtFiltro2 = "" Then txtFiltro2 = "0"
        objWorkSheet.GetRow(indice + rowActual).CreateCell(odtg.Columns("PESO_BULTO").Ordinal).CellStyle = styleTotales
        objWorkSheet.GetRow(indice + rowActual).GetCell(odtg.Columns("PESO_BULTO").Ordinal).SetCellValue(Convert.ToDouble(txtFiltro2))

        txtFiltro2 = TotalM3
        If txtFiltro2 = "" Then txtFiltro2 = "0"
        objWorkSheet.GetRow(indice + rowActual).CreateCell(odtg.Columns("VOLUMEN").Ordinal).CellStyle = styleTotales
        objWorkSheet.GetRow(indice + rowActual).GetCell(odtg.Columns("VOLUMEN").Ordinal).SetCellValue(Convert.ToDouble(txtFiltro2))

        indice = indice + 1

        objWorkSheet.DefaultColumnWidth = 25
        For z As Integer = 0 To odtg.Columns.Count + 1
            objWorkSheet.AutoSizeColumn(z)
            If z = 0 Then
                objWorkSheet.SetColumnWidth(0, 10000)
            End If
        Next

        For intSheet As Integer = 1 To 1
            Dim odtResumen As New DataTable
            odtResumen = oDs.Tables(intSheet)

            'Crear Una Nueva Hoja Para el Resumen 
            objWorkSheet = objWorkBook.CreateSheet("Resumen_" + intSheet.ToString)
            For s As Integer = 0 To (odtResumen.Rows.Count + loCabecera.Count + 60 + addEspacios)
                '(odtg.Rows.Count + odtFiltro.Rows.Count + odtResumen.Rows.Count + odtTotales.Rows.Count) 
                objWorkSheet.CreateRow(s)
            Next

            indice = 1
            ' Se crear Los Titulos de Los filtros en la Nueva Hoja
            For Xfiltro As Integer = 1 To loCabecera.Count - 1
                objWorkSheet.GetRow(indice - 1).CreateCell(0).CellStyle = styleFiltro
                txtFiltro = loCabecera(Xfiltro).ToString()
                objWorkSheet.GetRow(indice - 1).GetCell(0).SetCellValue(txtFiltro)
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(indice - 1, 0, indice - 1, 5))
                indice = indice + 1
            Next
            indice = indice + 3
            '===Generamos la Cabecera Del Resumen
            Dim INICIO, FIN As Integer
            If intSheet = 1 Then
                INICIO = 2
            Else
                INICIO = 3
            End If
            FIN = 2

            For j As Integer = 0 To odtResumen.Columns.Count - 1
                y = j
                objWorkSheet.GetRow(indice - 1).CreateCell(y).CellStyle = styleCab
                If odtResumen.Columns(j).ColumnName.Equals("ORDEN_COMPRA") Then
                    objWorkSheet.GetRow(indice - 1).GetCell(y).SetCellValue("ORDEN_COMPRA")
                Else
                    Dim valorCab As String = Split(odtResumen.Columns(j).ColumnName.ToString().Trim(), "_")(0)
                    objWorkSheet.GetRow(indice - 1).GetCell(y).SetCellValue(valorCab)
                End If


                If j > 0 AndAlso FIN - INICIO = 2 Then
                    If intSheet = 1 OrElse (odtResumen.Columns(j).ColumnName.ToString().Trim().IndexOf("_") <> -1 AndAlso intSheet = 2) Then
                        objWorkSheet.GetRow(indice - 2).CreateCell(y).CellStyle = styleCab
                        objWorkSheet.GetRow(indice - 2).CreateCell(y + 1).CellStyle = styleCab
                        objWorkSheet.GetRow(indice - 2).CreateCell(y + 2).CellStyle = styleCab
                        Dim valorCab1 As String = Split(odtResumen.Columns(j).ColumnName.ToString().Trim(), "_")(1)
                        objWorkSheet.GetRow(indice - 2).GetCell(y).SetCellValue(valorCab1)
                        Dim Region As New NPOI.SS.Util.Region(indice - 2, INICIO, indice - 2, FIN)
                        objWorkSheet.AddMergedRegion(Region)
                        INICIO = FIN + 1
                        ' Else
                        ' INICIO = 3
                        ' FIN = 3
                    End If

                End If
                FIN = FIN + 1
            Next

            'Crea el Detalle del Resumen
            Dim strCliente As String = ""
            Dim bPrimero As Boolean
            Dim lstInicioReg As New List(Of String)

            For z As Integer = 0 To odtResumen.Rows.Count - 1 '
                '===Verificamos Cambio de  Grupo de Datos===
                If z = 0 OrElse odtResumen.Rows(z).Item("CLIENTE").ToString = strCliente Then
                    'indice = indice + 1
                    bPrimero = False
                Else
                    indice = indice + 1
                    bPrimero = True
                    '  NroItem = 1
                End If

                '===Validamos inicio de un nuevo grupo de registros para poner las cabeceras
                If bPrimero OrElse z = 0 Then
                    ''''''''''''''''''''''''''' Se Coloca la Imagen en el Lado Izquierdo
                    ' strCliente = odtResumen.Rows(z).Item("CLIENTE").ToString().Trim()
                    objWorkSheet.GetRow(indice + z).CreateCell(0).SetCellValue(odtResumen.Rows(z).Item("CLIENTE").ToString().Trim())
                    objWorkSheet.GetRow(indice + z).GetCell(0).CellStyle = styleTextNegitra
                    RowInicio = indice + z
                End If

                ' ''===Generamos Los Detalles de los Registros===
                IndiceColum = 1
                For x As Integer = 1 To odtResumen.Columns.Count - 1 '
                    y = IndiceColum

                    Dim celda As String = odtResumen.Rows(z).Item(x).ToString().Trim()

                    If IsNumeric(celda) Then
                        If odtg.Columns(x).ColumnName.Equals("ORDEN_COMPRA") Then
                            objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = style
                            objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(celda)
                        ElseIf celda.Contains(".") Then
                            objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = styleMonto
                            objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(Convert.ToDouble(celda))
                        ElseIf celda.Substring(0, 1) = "0" AndAlso celda.Length > 1 Then
                            objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = style
                            objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(celda)
                        Else
                            objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = styleNumber
                            objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(Convert.ToDouble(celda))
                        End If
                    Else
                        If IsDate(celda) Then
                            celda = String.Format("{0:d}", CDate(celda))
                            objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = styleFecha
                            objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(celda)
                        Else
                            objWorkSheet.GetRow(indice + z).CreateCell(y).SetCellValue(celda)
                            objWorkSheet.GetRow(indice + z).GetCell(y).CellStyle = style
                        End If
                    End If

                    'If IsNumeric(celda) Then
                    '    objWorkSheet.GetRow(indice + z).CreateCell(y).CellStyle = styleMonto
                    '    objWorkSheet.GetRow(indice + z).GetCell(y).SetCellValue(Convert.ToDouble(celda))
                    'Else
                    '    objWorkSheet.GetRow(indice + z).CreateCell(y).SetCellValue()
                    '    objWorkSheet.GetRow().GetCell(y).CellStyle = style
                    'End If
                    IndiceColum = IndiceColum + 1

                Next

                '===Obtenemos el Row de la ultima Variable
                strCliente = odtResumen.Rows(z).Item("CLIENTE").ToString
                ' NroItem = NroItem + 1
                ' =Validamos Finalización de un nuevo grupo de registros

                If bPrimero Then
                    IndiceColum = 0
                    For j As Integer = 1 To odtResumen.Columns.Count - 2 '
                        y = IndiceColum + 3
                        If j = 1 Then
                            objWorkSheet.GetRow(indice + z - 1).CreateCell(1).CellStyle = styleTotales
                            objWorkSheet.GetRow(indice + z - 1).GetCell(1).SetCellValue("SUB TOTAL")
                        End If

                        Dim cIni As String
                        Dim cFin As String
                        cIni = HelpClass.LetraNumero(y) & (RowInicioAnt + 1).ToString
                        cFin = HelpClass.LetraNumero(y) & (indice + z - 1).ToString

                        Dim sumKg As String = "SUM(" & cIni & ":" & cFin & ")"
                        objWorkSheet.GetRow(indice + z - 1).CreateCell(y - 1).CellStyle = styleTotales
                        objWorkSheet.GetRow(indice + z - 1).GetCell(y - 1).CellFormula = (sumKg)

                        'Borra lo de la Columna con la sumatoria de las O/C
                        If intSheet = 2 Then
                            objWorkSheet.GetRow(indice + z - 1).GetCell(2).CellFormula = ""
                        Else

                        End If


                        Dim Region As New NPOI.SS.Util.Region(RowInicioAnt + 1, 0, indice + z - 1, 0)
                        objWorkSheet.AddMergedRegion(Region)
                        IndiceColum = IndiceColum + 1
                    Next
                    ' Almacena las Posiciones de Los Totales
                    lstInicioReg.Add(indice + z)
                End If
                RowInicioAnt = RowInicio
            Next

            ' Suma Los Totales Generados
            IndiceColum = 0
            'Almacena la ultima Posicion para Los Totales
            lstInicioReg.Add(indice + odtResumen.Rows.Count + 1)
            For z As Integer = 1 To odtResumen.Columns.Count - 2 '

                y = IndiceColum + 3
                If z = 1 Then
                    objWorkSheet.GetRow(indice + odtResumen.Rows.Count).CreateCell(1).CellStyle = styleTotales
                    objWorkSheet.GetRow(indice + odtResumen.Rows.Count).GetCell(1).SetCellValue("SUB TOTAL")
                    objWorkSheet.GetRow(indice + odtResumen.Rows.Count + 2).CreateCell(y - 2).CellStyle = styleTotales
                    objWorkSheet.GetRow(indice + odtResumen.Rows.Count + 2).GetCell(y - 2).SetCellValue("TOTALES")
                End If
                Dim cIniV As String
                Dim cFinV As String
                cIniV = HelpClass.LetraNumero(y) & (RowInicioAnt + 1).ToString
                cFinV = HelpClass.LetraNumero(y) & (indice + odtResumen.Rows.Count).ToString

                Dim sumKg As String = "SUM(" & cIniV & ":" & cFinV & ")"
                objWorkSheet.GetRow(indice + odtResumen.Rows.Count).CreateCell(y - 1).CellStyle = styleTotales
                objWorkSheet.GetRow(indice + odtResumen.Rows.Count).GetCell(y - 1).CellFormula = (sumKg)

                Dim Region As New NPOI.SS.Util.Region(RowInicioAnt + 1, 0, indice + odtResumen.Rows.Count, 0)
                objWorkSheet.AddMergedRegion(Region)
                sumKg = "SUM("

                For reg As Integer = 0 To lstInicioReg.Count - 2
                    sumKg = sumKg & HelpClass.LetraNumero(y) & lstInicioReg(reg).ToString & ","
                Next
                sumKg = sumKg & HelpClass.LetraNumero(y) & lstInicioReg(lstInicioReg.Count - 1).ToString & ")"
                objWorkSheet.GetRow(indice + odtResumen.Rows.Count + 2).CreateCell(y - 1).CellStyle = styleTotales
                objWorkSheet.GetRow(indice + odtResumen.Rows.Count + 2).GetCell(y - 1).CellFormula = (sumKg)

                'Borra lo de la Columna con la sumatoria de las O/C
                If intSheet = 2 Then
                    objWorkSheet.GetRow(indice + odtResumen.Rows.Count).GetCell(2).CellFormula = ""
                    objWorkSheet.GetRow(indice + odtResumen.Rows.Count + 2).GetCell(2).CellFormula = ""
                End If
                IndiceColum = IndiceColum + 1
            Next


            For z As Integer = 0 To odtResumen.Columns.Count
                objWorkSheet.AutoSizeColumn(z)
                If z = 0 Then
                    objWorkSheet.SetColumnWidth(0, 10000)
                End If
            Next
        Next

        objWorkBook.Write(fs)
        fs.Close()
        AbrirDocumento(archivo)


    End Sub

  Public Shared Function LetraNumero(ByVal i As Integer) As String
    i = i - 1
    Dim AbcD As New Hashtable()
    AbcD.Add(0, "A")
    AbcD.Add(1, "B")
    AbcD.Add(2, "C")
    AbcD.Add(3, "D")
    AbcD.Add(4, "E")
    AbcD.Add(5, "F")
    AbcD.Add(6, "G")
    AbcD.Add(7, "H")
    AbcD.Add(8, "I")
    AbcD.Add(9, "J")
    AbcD.Add(10, "K")
    AbcD.Add(11, "L")
    AbcD.Add(12, "M")
    AbcD.Add(13, "N")
    AbcD.Add(14, "O")
    AbcD.Add(15, "P")
    AbcD.Add(16, "Q")
    AbcD.Add(17, "R")
    AbcD.Add(18, "S")
    AbcD.Add(19, "T")
    AbcD.Add(20, "U")
    AbcD.Add(21, "V")
    AbcD.Add(22, "W")
    AbcD.Add(23, "X")
    AbcD.Add(24, "Y")
    AbcD.Add(25, "Z")
    If i > 25 Then
      Dim intX, intY As Integer
      intX = Math.Floor(i / 26)
      intY = i - intX * 26
      Return AbcD(intX - 1).ToString + AbcD(intY).ToString
    Else
      Return AbcD(i).ToString
    End If
  End Function

  Public Shared Function SetFormatoGuiaRemision(ByVal oValor As String) As String
    Dim n1 As String
    Dim n2 As String
    n1 = Right(oValor, 7)
    If IsNumeric(oValor) Then
      Select Case oValor.Length
        Case 1
          Return "000-000000" & oValor
        Case 2
          Return "000-00000" & oValor
        Case 3
          Return "000-0000" & oValor
        Case 4
          Return "000-000" & oValor
        Case 5
          Return "000-00" & oValor
        Case 6
          Return "000-0" & oValor

        Case 7
          Return "000-" & oValor
        Case 8
          n2 = Left(oValor, 1)
          Return "00" & n2 & "-" & n1
        Case 9
          n2 = Left(oValor, 2)
          Return "0" & n2 & "-" & n1

        Case 10
          n2 = Left(oValor, 3)
          Return n2 & "-" & n1
        Case Else
          Return oValor
      End Select
    Else
      Return oValor
    End If

  End Function

#Region "Numero a Letras"
  Public Shared Function NumeroLetras_RNS(ByVal numero As String) As String
    '********Declara variables de tipo cadena************
    Dim palabras, entero, dec, flag As String
    palabras = ""
    entero = ""
    dec = ""
    flag = ""
    '********Declara variables de tipo entero***********
    Dim x, y As Integer

    flag = "N"

    '**********Número Negativo***********
    If Mid(numero, 1, 1) = "-" Then
      numero = Mid(numero, 2, Len(numero) - 1).ToString
      palabras = "menos "
    End If

    '**********Si tiene ceros a la izquierda*************
    For x = 1 To Len(numero)
      If Mid(numero, 1, 1) = "0" Then
        numero = Trim(Mid(numero, 2, Len(numero)).ToString)
        If Trim(Len(numero)) = 0 Then palabras = ""
      Else
        Exit For
      End If
    Next

    '*********Dividir parte entera y decimal************
    For y = 1 To Len(numero)
      If Mid(numero, y, 1) = "." Then
        flag = "S"
      Else
        If flag = "N" Then
          entero = entero + Mid(numero, y, 1)
        Else
          dec = dec + Mid(numero, y, 1)
        End If
      End If
    Next y

    If Len(dec) = 1 Then dec = dec & "0"

    '===========================================================        
    Dim numeroTexto As String
    numeroTexto = Num2Text(CDbl(entero))
    '**********proceso de conversión***********
    flag = "N"

    If dec <> "" Then
      Return numeroTexto & " " & palabras & " y " & dec & "/100 "
    Else
      Return numeroTexto & " y 00/100 "
    End If
  End Function



  Public Shared Function Num2Text(ByVal value As Double) As String
    '====SOLO ENTEROS====
    Select Case value
      Case 0 : Num2Text = "CERO"
      Case 1 : Num2Text = "UN"
      Case 2 : Num2Text = "DOS"
      Case 3 : Num2Text = "TRES"
      Case 4 : Num2Text = "CUATRO"
      Case 5 : Num2Text = "CINCO"
      Case 6 : Num2Text = "SEIS"
      Case 7 : Num2Text = "SIETE"
      Case 8 : Num2Text = "OCHO"
      Case 9 : Num2Text = "NUEVE"
      Case 10 : Num2Text = "DIEZ"
      Case 11 : Num2Text = "ONCE"
      Case 12 : Num2Text = "DOCE"
      Case 13 : Num2Text = "TRECE"
      Case 14 : Num2Text = "CATORCE"
      Case 15 : Num2Text = "QUINCE"
      Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
      Case 20 : Num2Text = "VEINTE"
      Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
      Case 30 : Num2Text = "TREINTA"
      Case 40 : Num2Text = "CUARENTA"
      Case 50 : Num2Text = "CINCUENTA"
      Case 60 : Num2Text = "SESENTA"
      Case 70 : Num2Text = "SETENTA"
      Case 80 : Num2Text = "OCHENTA"
      Case 90 : Num2Text = "NOVENTA"
      Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
      Case 100 : Num2Text = "CIEN"
      Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
      Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
      Case 500 : Num2Text = "QUINIENTOS"
      Case 700 : Num2Text = "SETECIENTOS"
      Case 900 : Num2Text = "NOVECIENTOS"
      Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
      Case 1000 : Num2Text = "MIL"
      Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
      Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
        If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
      Case 1000000 : Num2Text = "UN MILLON"
      Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
      Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES "
        If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
      Case 1000000000000.0# : Num2Text = "UN BILLON"
      Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
      Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
        If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
    End Select
  End Function
#End Region

    Public Shared Function flSendMail(ByVal strServer As String, ByVal strMailCo As String, ByVal strMailFrom As String, ByVal strMailFromClave As String, _
                           ByVal strAdress As String, ByVal strCC As String, ByVal strAsunto As String, ByVal strBody As String, Optional ByVal bolIsHtml As Boolean = False, Optional ByRef strSMSRespuesta As String = "") As Boolean
        Try
            'Dim server As New Net.Mail.SmtpClient(strServer)
            'server.Port = 587
            'server.EnableSsl = True
            'server.Credentials = New System.Net.NetworkCredential(strMailFrom, strMailFromClave)
            'Dim mail As New Net.Mail.MailMessage()
            'mail.From = New Net.Mail.MailAddress(strMailFrom)
            'Dim Mails() As String = strAdress.Split(";")

            'For i As Integer = 0 To Mails.Length - 1
            '    mail.To.Add(Mails(i))
            'Next
            'If strCC.Length > 0 Then
            '    Dim MailsCC() As String = strCC.Split(";")

            '    For i As Integer = 0 To MailsCC.Length - 1
            '        mail.CC.Add(MailsCC(i))
            '    Next
            'End If
            'If strMailCo.Length > 0 Then
            '    mail.Bcc.Add(strMailCo)
            'End If
            'mail.Subject = strAsunto
            'mail.Body = strBody
            'mail.IsBodyHtml = bolIsHtml
            'server.Send(mail)
            Dim objMail As New EmailService
            Dim objservice As New ws_solmin_common_task
            objMail.Destinatarios = strAdress
            objMail.DestinatariosCopia = strCC
            objMail.Mensaje = strBody
            objMail.Titulo = strAsunto
            strSMSRespuesta = objservice.SendMail(strMailFrom, strMailFromClave, objMail)
        Catch ex As Exception
            strSMSRespuesta = ex.Message
            Return False
        End Try
        Return True
    End Function

  Public Shared Function validar_Mail(ByVal sMail As String) As Boolean
    Return Regex.IsMatch(sMail, _
            "^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")
  End Function

  Public Shared Function ConvierteLetraCapital(ByVal paramTexto As String) As String
    Dim _miTexto As String = ""
    If paramTexto.Length <> 0 Then
      _miTexto = paramTexto.ToLower
      _miTexto = _miTexto.Substring(0, 1).ToUpper & _
      _miTexto.Substring(1, _miTexto.Length - 1)
    End If

    Return _miTexto
  End Function

  Public Shared Function MedioEnvioConvertString(ByVal intMedio As MedioEnvio) As String
    Dim strMedio As String = ""
    Select Case intMedio
      Case MedioEnvio.TERRESTRE
        strMedio = "TERRESTRE"
      Case MedioEnvio.FLUVIAL
        strMedio = "FLUVIAL"
      Case MedioEnvio.AEREO
        strMedio = "AEREO"
    End Select
    Return strMedio
  End Function

  Enum MedioEnvio
    TERRESTRE = 1
    AEREO = 2
    FLUVIAL = 3
    End Enum

    Public Shared Sub HabilitarCompaniaActual(ByVal pComboBox As Control, ByVal CodCompania As String)
        If TypeOf pComboBox Is ComboBox Then
            CType(pComboBox, ComboBox).SelectedValue = CodCompania
            If CType(pComboBox, ComboBox).SelectedIndex > -1 Then
                CType(pComboBox, ComboBox).Enabled = False
            Else
                CType(pComboBox, ComboBox).SelectedIndex = 0
            End If
        ElseIf TypeOf pComboBox Is KryptonComboBox Then
            CType(pComboBox, KryptonComboBox).SelectedValue = CodCompania
            If CType(pComboBox, KryptonComboBox).SelectedIndex > -1 Then
                CType(pComboBox, KryptonComboBox).ComboBox.Enabled = False
            Else
                CType(pComboBox, KryptonComboBox).SelectedIndex = 0
            End If
        End If
    End Sub


    Public Shared Function validaCaracter(ByVal strCadena As String, ByRef strMensaje As String) As String

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
    Public Shared Sub ExportExcelConTitulosModeloPerenco(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1
                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.WrapText = True
                styleFiltro.VerticalAlignment = VerticalAlignment.CENTER

                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.WrapText = True
                styleCab.VerticalAlignment = VerticalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.WrapText = True
                style.VerticalAlignment = VerticalAlignment.CENTER
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.WrapText = True
                stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.WrapText = True
                styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.WrapText = True
                styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN
                styleNumber.WrapText = True
                styleNumber.VerticalAlignment = VerticalAlignment.CENTER
                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.WrapText = True
                styleMonto.VerticalAlignment = VerticalAlignment.CENTER
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.WrapText = True
                styleFecha.VerticalAlignment = VerticalAlignment.CENTER
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.WrapText = True
                styleLink.VerticalAlignment = VerticalAlignment.CENTER
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                    Next

                    'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                Next

                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1

                    If oDs.Tables(intTable).Columns(x).ColumnName <> "CREFFW" Then
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            flgCabDoble = True
                        End If
                        objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
                    End If
           

                Next
                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next
                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If
                Dim intSinRow As Integer = 0
                intSinRow = rowActual
                '===================Se Carga El Detalle============================
                Dim numFilas() As DataRow
                Dim row As Integer = 0
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

                    'numFilas = oDs.Tables(intTable).Select(String.Format("P.O={0}", oDs.Tables(intTable).Rows(i).Item("P.O").ToString().Trim()))
                    
                    objWorkSheet.CreateRow(i + rowActual)

                    ' For row = 0 To numFilas.Length - 1
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1

                        If oDs.Tables(intTable).Columns(x).ColumnName = "CREFFW" Then
                            Continue For
                        End If
                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° PED. RANSA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° CONTRATO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                            Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                            Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                            Try
                                link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                If celda <> "" Then
                                    celda = "2"
                                End If
                            Catch : End Try
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda.ToString)
                            'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                End If

                            Else
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                                End If
                            End If
                            '****
                        Else
                            If TypoDato.Equals("System.Date") Then
                                celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            ElseIf TypoDato.Equals("System.DateTime") Then
                                If celda <> "" Then
                                    celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            ElseIf TypoDato.Equals("System.Byte[]") Then


                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                Catch : End Try

                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                                If Not celda Is "" Then
                                    Dim anchorImagen As HSSFClientAnchor
                                    anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                    Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                    imagenAlmacen.LineStyle = 1
                                    imagenAlmacen.Resize()
                                End If
                            Else
                                If celda Is DBNull.Value Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If
                            End If
                        End If
                    Next 'fin de columnas
                Next

                Dim intRepetido As Integer = 0
                Dim Region_X As NPOI.SS.Util.Region
                For x As Integer = oDs.Tables(intTable).Rows.Count - 1 To 1 Step -1
                    'CREFFW 
                    intRepetido += 1

                    If oDs.Tables(intTable).Rows(x).Item("CREFFW") <> oDs.Tables(intTable).Rows(x - 1).Item("CREFFW") Then
                        For intColumas As Integer = 0 To 7
                            Region_X = New NPOI.SS.Util.Region(x + rowActual, intColumas, x + rowActual + intRepetido - 1, intColumas)
                            objWorkSheet.AddMergedRegion(Region_X)
                        Next
                        Region_X = New NPOI.SS.Util.Region(x + rowActual, 13, x + rowActual + intRepetido - 1, 13)
                        objWorkSheet.AddMergedRegion(Region_X)

                        For intColumas As Integer = 15 To 24
                            Region_X = New NPOI.SS.Util.Region(x + rowActual, intColumas, x + rowActual + intRepetido - 1, intColumas)
                            objWorkSheet.AddMergedRegion(Region_X)
                        Next
                       

                        intRepetido = 0
                    End If

                    If oDs.Tables(intTable).Rows(x).Item("CREFFW") = oDs.Tables(intTable).Rows(x - 1).Item("CREFFW") And x = 1 Then
                        For intColumas As Integer = 0 To 7
                            Region_X = New NPOI.SS.Util.Region(rowActual, intColumas, rowActual + intRepetido, intColumas)
                            objWorkSheet.AddMergedRegion(Region_X)
                        Next
                        Region_X = New NPOI.SS.Util.Region(rowActual, 13, rowActual + intRepetido, 13)
                        objWorkSheet.AddMergedRegion(Region_X)
                        For intColumas As Integer = 15 To 24
                            Region_X = New NPOI.SS.Util.Region(x + rowActual, intColumas, x + rowActual + intRepetido - 1, intColumas)
                            objWorkSheet.AddMergedRegion(Region_X)
                        Next
                        intRepetido = 0
                    End If

                Next

                ''Suma los registros indicados
                Dim strFormula As String
                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If Not listSuma Is Nothing Then
                    For Each items As DictionaryEntry In listSuma
                        strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 7).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 6).ToString & ")"
                        If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMonto
                        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                    Next
                End If
                '===================================================================
                For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Shared Sub ExportExcelConsultaProvisiones(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try

            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1

                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim styleDinamico As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165

                Dim oFontFiltroResumen As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltroResumen.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltroResumen.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontFiltroResumen.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFiltroResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFiltroResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFiltroResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltroResumen.BorderRight = CellBorderType.THIN
                styleFiltroResumen.BorderBottom = CellBorderType.THIN
                styleFiltroResumen.BorderLeft = CellBorderType.THIN
                styleFiltroResumen.BorderTop = CellBorderType.THIN
                styleFiltroResumen.SetFont(oFontFiltroResumen)
                styleFiltroResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltroResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN

                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoTotal As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoTotal.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoTotal.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.BorderRight = CellBorderType.THIN
                styleMontoTotal.BorderBottom = CellBorderType.THIN
                styleMontoTotal.BorderLeft = CellBorderType.THIN
                styleMontoTotal.BorderTop = CellBorderType.THIN
                styleMontoTotal.SetFont(oFontFiltroResumen)
                styleMontoTotal.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoTotal.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleMontoResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoResumen.BorderRight = CellBorderType.THIN
                styleMontoResumen.BorderBottom = CellBorderType.THIN
                styleMontoResumen.BorderLeft = CellBorderType.THIN
                styleMontoResumen.BorderTop = CellBorderType.THIN
                styleMontoResumen.SetFont(oFontFiltroResumen)
                styleMontoResumen.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoResumen.FillPattern = FillPatternType.SOLID_FOREGROUND



                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFechaResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFechaResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFechaResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFechaResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFechaResumen.BorderRight = CellBorderType.THIN
                styleFechaResumen.BorderBottom = CellBorderType.THIN
                styleFechaResumen.BorderLeft = CellBorderType.THIN
                styleFechaResumen.BorderTop = CellBorderType.THIN
                styleFechaResumen.SetFont(oFontFiltroResumen)
                styleFechaResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFechaResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND







                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                            Continue For
                        End If
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x + 1).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1



                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")

                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(0))
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(2).CellStyle = styleTituloDescripcion
                    If intTable = 0 Then
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    ElseIf intTable <> 0 And intCont = 1 Then
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(oDs.Tables(intTable).TableName)
                    Else
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    End If
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 2, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1
                Next
                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    If oDs.Tables(intTable).Columns(x).ColumnName <> "ESTADOOP" Then
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = Replace(oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim(), Chr(34), "")
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length > 1 Then
                            flgCabDoble = True
                        End If
                        objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(0))
                    End If
                Next


                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas + 1).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length > 1 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1

                    '============================================================================
                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas + 1).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 3 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(2))
                        End If
                    Next
                    rowActual = rowActual + 1

                End If

                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                    objWorkSheet.CreateRow(i + rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                        'Continue For
                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If oDs.Tables(intTable).Rows(i).Item(0).ToString.Trim.Equals("-1") Then
                            'objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                            'objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = style
                            Select Case TypoDato
                                Case "System.Decimal", "System.Int32", "System.Double", "System.Integer"
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMontoResumen
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If
                                Case "System.Date"
                                    celda = String.Format("{0:d}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFechaResumen
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)

                                Case "System.DateTime"
                                    celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                Case Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltroResumen
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltroResumen
                                    End If

                            End Select
                        Else

                            If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(celda) / 100)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                    If celda <> "" Then
                                        celda = "2"
                                    End If
                                Catch : End Try
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleLink
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda.ToString)
                                'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = stylePorcentaje
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "OPERACIÓN" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "NRO. REVISIÓN" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "N° FACTURA" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Val(celda))
                            ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                                If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleMonto
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If

                                Else
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(Convert.ToInt64(Val(celda)))
                                    End If
                                End If


                            Else
                                If TypoDato.Equals("System.Date") Then
                                    celda = String.Format("{0:d}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                ElseIf TypoDato.Equals("System.Byte[]") Then


                                    Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                    Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                    Try
                                        link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                    Catch : End Try

                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).Hyperlink = link
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleLink
                                    If Not celda Is "" Then
                                        Dim anchorImagen As HSSFClientAnchor
                                        anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                        Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                        imagenAlmacen.LineStyle = 1
                                        imagenAlmacen.Resize()
                                    End If

                                ElseIf TypoDato.Equals("System.DateTime") Then
                                    celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                Else
                                    If celda Is DBNull.Value Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).CellStyle = styleFiltro
                                    End If

                                End If
                            End If
                        End If



                    Next
                Next

                'Dim totalMontoMes As Decimal = 0
                'If intTable > 0 Then
                '    For c As Integer = 5 To oDs.Tables(intTable).Columns.Count - 1
                '        objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                '        For f As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                '            totalMontoMes += IIf(IsDBNull(oDs.Tables(intTable).Rows(f)(c)), 0, oDs.Tables(intTable).Rows(f)(c))
                '        Next

                '        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(c + 3).CellStyle = styleMontoTotal
                '        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(c + 3).CellFormula = (totalMontoMes)

                '        totalMontoMes = 0
                '    Next
                'End If

                ''Suma los registros indicados
                Dim strFormula As String
                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If Not listSuma Is Nothing Then

                    For Each items As DictionaryEntry In listSuma
                        If Math.Ceiling(items.Key) = (intTable + 2) Then
                            strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ")"
                            If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMontoTotal
                            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                        End If
                    Next
                End If


                '===================================================================
                For z As Integer = 1 To oDs.Tables(intTable).Columns.Count
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 1, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    '== INICIO == CAH - 20150723 =='
    Public Shared Sub ExportExcelReporteRotacion(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String))
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1
                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.WrapText = True
                styleFiltro.VerticalAlignment = VerticalAlignment.CENTER

                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.WrapText = True
                styleCab.VerticalAlignment = VerticalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.WrapText = True
                style.VerticalAlignment = VerticalAlignment.CENTER
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                '== INICIO == CAH - 20150723 =='
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                '== FIN == CAH - 20150723 =='
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.WrapText = True
                stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.WrapText = True
                styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.WrapText = True
                styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '== INICIO == CAH - 20150723 =='
                Dim styleNumbercuatro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumbercuatro.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumbercuatro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumbercuatro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumbercuatro.BorderRight = CellBorderType.THIN
                styleNumbercuatro.BorderBottom = CellBorderType.THIN
                styleNumbercuatro.BorderLeft = CellBorderType.THIN
                styleNumbercuatro.BorderTop = CellBorderType.THIN
                styleNumbercuatro.WrapText = True
                styleNumbercuatro.VerticalAlignment = VerticalAlignment.CENTER
                styleNumbercuatro.SetFont(oFontFiltro)
                styleNumbercuatro.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN
                styleNumber.WrapText = True
                styleNumber.VerticalAlignment = VerticalAlignment.CENTER
                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.WrapText = True
                styleMonto.VerticalAlignment = VerticalAlignment.CENTER
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.WrapText = True
                styleFecha.VerticalAlignment = VerticalAlignment.CENTER
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.WrapText = True
                styleLink.VerticalAlignment = VerticalAlignment.CENTER
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                    Next

                    'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                Next

                objWorkSheet.CreateRow(rowActual + 1)

                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    REM AQUI ES EL CAMBIO DEL ANCHO
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        flgCabDoble = True
                    End If
                    objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
                    objWorkSheet.GetRow(rowActual).GetCell(x).Row.Height = 560
                Next

                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next
                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If



                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

                    objWorkSheet.CreateRow(i + rowActual)

                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1

                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        '== INICIO == CAH - 20150723 =='
                        If oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" & vbLf & " (F)" Then '==  == CAH - 20150723 =='
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "CODIGO" Then '==  == CAH - 20150723 =='
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "DESCRIPCION" Then '==  == CAH - 20150723 =='
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            '== FIN == CAH - 20150723 =='
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "CLIENTE" And TypoDato = "System.Decimal" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "INGRESOS \n PESO" Or oDs.Tables(intTable).Columns(x).ColumnName = "INGRESOS \n VOLUMEN" Then

                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDecimal(celda))
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE INGRESO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE SALIDA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda) / 100)
                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° PED. RANSA" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = " \n N° CONTRATO" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                            '== INICIO == CAH - 20150723 =='

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "ROTACION  DE INVENTARIO" & vbLf & " (G)" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                            If IsNumeric(celda) Then
                                Dim valor As Double = Math.Round(CDbl(celda), 4)
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                            Else
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            End If


                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PARTICIPACION DE OPERACION" & vbLf & "(H)" Then 'csr
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                            If IsNumeric(celda) Then
                                Dim valor As Double = Math.Round(CDbl(celda), 4)
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                            Else
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            End If


                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "ÍNDICE DE ROTACIÓN POR DÍA " & vbLf & " (365/G)" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                            If IsNumeric(celda) Then
                                Dim valor As Double = Math.Round(CDbl(celda), 4)
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                            Else
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            End If
                            '== FIN == CAH - 20150723 =='

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "LINK" Then

                            Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                            Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                            Try
                                link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                If celda <> "" Then
                                    celda = "2"
                                End If
                            Catch : End Try
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda.ToString)
                            'objWorkSheet.GetColumnStyle(x).SetFont(oFontlink)

                        ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "% Atraso" Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                        ElseIf TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then

                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                End If

                            Else
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                                End If
                            End If
                            '****
                        Else
                            If TypoDato.Equals("System.Date") Then
                                celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            ElseIf TypoDato.Equals("System.DateTime") Then
                                If celda <> "" Then
                                    celda = String.Format("{0:yyyy/MM/dd}", CDate(celda))
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If

                            ElseIf TypoDato.Equals("System.Byte[]") Then


                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                Catch : End Try

                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                                If Not celda Is "" Then
                                    Dim anchorImagen As HSSFClientAnchor
                                    anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                    Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                    imagenAlmacen.LineStyle = 1
                                    imagenAlmacen.Resize()
                                End If
                            Else
                                '== INICIO == CAH - 20150723 =='
                                If celda Is DBNull.Value Then 'csr
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("") 'csr
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                Else
                                    If IsNumeric(celda) Then '==  == CAH - 20150723 =='
                                        Dim valor As Double = CDbl(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(valor)
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleNumber
                                    Else 'csr
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    End If
                                End If
                                '== FIN == CAH - 20150723 =='
                            End If
                        End If
                    Next
                Next


                ''Suma los registros indicados
                Dim strFormula As String
                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
               
                '===================================================================
                For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
    '== FIN == CAH - 20150723 =='


    'CSR-HUNDRED-REPUESTOS-ON-SIDE-PIURA-INICIO
#Region "Creacion de excel sin Gráfica"
    Public Shared Sub ExportarConsultaEriSinGraficos(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Dim path As String = Application.StartupPath.ToString
        Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
        Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)
        Dim objWorkBook As New HSSFWorkbook()
        Dim objWorkSheet As New HSSFSheet(objWorkBook)

        For intTable As Integer = 0 To oDs.Tables.Count - 1
            objWorkSheet = objWorkBook.CreateSheet("Consulta Eri") 'oDs.Tables(intTable).TableName)
            objWorkSheet.CreateRow(3)

            '=============Style======================
            Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
            '===============================
            Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontFiltro.FontHeight = 165
            '===============================
            Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontlink.FontName = "Wingdings"
            oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontlink.FontHeight = 250
            '===============================
            Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220
            '===============================
            Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFont.FontHeight = 170
            '===============================
            Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
            oFontTituloConcepto.FontHeight = 190
            '===============================
            Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
            oFontTituloDescripcion.FontHeight = 170
            '===============================
            styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.BorderRight = CellBorderType.THIN
            styleFiltro.WrapText = True
            styleFiltro.VerticalAlignment = VerticalAlignment.CENTER
            styleFiltro.BorderBottom = CellBorderType.THIN
            styleFiltro.BorderLeft = CellBorderType.THIN
            styleFiltro.BorderTop = CellBorderType.THIN
            styleFiltro.SetFont(oFontFiltro)
            styleFiltro.DataFormat = NPOI.SS.UserModel.CellType.STRING
            styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleCab.WrapText = True
            styleCab.VerticalAlignment = VerticalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleCab.BorderRight = CellBorderType.THIN
            styleCab.BorderBottom = CellBorderType.THIN
            styleCab.BorderLeft = CellBorderType.THIN
            styleCab.BorderTop = CellBorderType.THIN
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            style.WrapText = True
            style.VerticalAlignment = VerticalAlignment.CENTER
            style.FillPattern = FillPatternType.SOLID_FOREGROUND
            style.BorderRight = CellBorderType.THIN
            style.BorderBottom = CellBorderType.THIN
            style.BorderLeft = CellBorderType.THIN
            style.BorderTop = CellBorderType.THIN
            style.SetFont(oFont)
            '===============================
            stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
            stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
            stylePorcentaje.BorderRight = CellBorderType.THIN
            stylePorcentaje.BorderBottom = CellBorderType.THIN
            stylePorcentaje.BorderLeft = CellBorderType.THIN
            stylePorcentaje.BorderTop = CellBorderType.THIN
            stylePorcentaje.WrapText = True
            stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
            stylePorcentaje.SetFont(oFontFiltro)
            '===============================
            styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.WrapText = True
            styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloConcepto.SetFont(oFontTituloConcepto)
            '===============================
            styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.WrapText = True
            styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloDescripcion.SetFont(oFontTituloDescripcion)
            '===============================
            Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.BorderRight = CellBorderType.THIN
            styleNumber.BorderBottom = CellBorderType.THIN
            styleNumber.BorderLeft = CellBorderType.THIN
            styleNumber.BorderTop = CellBorderType.THIN
            styleNumber.WrapText = True
            styleNumber.VerticalAlignment = VerticalAlignment.CENTER
            styleNumber.SetFont(oFontFiltro)
            styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
            styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim rowActual As Integer = 2
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2
            '===========================
            '===========Titulo es centrado segun tamaño de grilla===========
            If Not strTitulo Is Nothing Then
                objWorkSheet.CreateRow(rowActual + 1)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                Next
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                'objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count + 1))
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))

                rowActual += 1
            End If
            '==============Verificamos Filtro a utilizar==================
            For intCont As Integer = 0 To olstrFiltros.Count - 1
                Dim strDescripcionTitulo As String()
                strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                objWorkSheet.CreateRow(rowActual + 1)
                objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))
                For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                Next
                'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            Next
            objWorkSheet.CreateRow(rowActual + 1)
            rowActual += 2
            '===================Se cargan Las Cabeceras=====================
            Dim flgCabDoble As Boolean = False
            objWorkSheet.CreateRow(rowActual)
            For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                '===Estilo===
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                '===Valores===
                Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                Dim valorCabDoble As String()
                valorCabDoble = Split(valorCab, "\n")
                If valorCabDoble.Length = 2 Then
                    flgCabDoble = True
                End If
                objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
            Next
            rowActual = rowActual + 1
            Dim intRepite As Integer = 0
            If flgCabDoble = True Then
                '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                    intRepite = 0
                    For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                            intRepite = intRepite + 1
                        End If
                    Next
                    If intRepite > 1 Then
                        intRepite = intRepite - 1
                        Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                        '===Union===
                        Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                        objWorkSheet.AddMergedRegion(Region)
                        x = x + intRepite
                    End If
                    ' End If
                Next
                '===================================================================
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                    End If
                Next
                rowActual = rowActual + 1
            End If
            '===================Se Carga El Detalle============================
            For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                objWorkSheet.CreateRow(i + rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                    Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString
                    If oDs.Tables(intTable).Columns(x).ColumnName = "CÓDIGO" & vbLf & "MES" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "STOCK" & vbLf & "FISICO" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "STOCK" & vbLf & "SISTEMAS" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "" & vbLf & "ERI (%)" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    End If
                Next
            Next
            '===================================================================
            For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                objWorkSheet.AutoSizeColumn(z, True)
            Next
            '============================================logo=================================
            'create the anchor
            Dim anchor As HSSFClientAnchor
            ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
            anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
            If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                picture.Resize(0.5)
            End If
            If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                'Reset the image to the original size.
                picture.Resize(0.5)
            End If
            ''============================================logo=================================
        Next
        objWorkBook.Write(fs)
        fs.Close()
        AbrirDocumento(archivo)
    End Sub

    Public Shared Sub ExportarReporteRotacionSinGraficos(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Dim path As String = Application.StartupPath.ToString
        Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
        Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)
        Dim objWorkBook As New HSSFWorkbook()
        Dim objWorkSheet As New HSSFSheet(objWorkBook)

        For intTable As Integer = 0 To oDs.Tables.Count - 1
            objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
            objWorkSheet.CreateRow(3)
            '=============Style======================
            Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTexto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
            '===============================
            Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontFiltro.FontHeight = 165
            '===============================
            Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontlink.FontName = "Wingdings"
            oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontlink.FontHeight = 250
            '===============================
            Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220
            '===============================
            Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFont.FontHeight = 170
            '===============================
            Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
            oFontTituloConcepto.FontHeight = 190
            '===============================
            Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
            oFontTituloDescripcion.FontHeight = 170
            '===============================
            styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.BorderRight = CellBorderType.THIN
            styleFiltro.WrapText = True
            styleFiltro.VerticalAlignment = VerticalAlignment.CENTER
            styleFiltro.BorderBottom = CellBorderType.THIN
            styleFiltro.BorderLeft = CellBorderType.THIN
            styleFiltro.BorderTop = CellBorderType.THIN
            styleFiltro.SetFont(oFontFiltro)
            styleFiltro.DataFormat = NPOI.SS.UserModel.CellType.STRING
            styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            styleTexto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleTexto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleTexto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleTexto.BorderRight = CellBorderType.THIN
            styleTexto.WrapText = True
            styleTexto.VerticalAlignment = VerticalAlignment.CENTER
            styleTexto.BorderBottom = CellBorderType.THIN
            styleTexto.BorderLeft = CellBorderType.THIN
            styleTexto.BorderTop = CellBorderType.THIN
            styleTexto.SetFont(oFontFiltro)
            ' styleTexto.DataFormat = NPOI.SS.UserModel.CellType.STRING
            styleTexto.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleCab.WrapText = True
            styleCab.VerticalAlignment = VerticalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleCab.BorderRight = CellBorderType.THIN
            styleCab.BorderBottom = CellBorderType.THIN
            styleCab.BorderLeft = CellBorderType.THIN
            styleCab.BorderTop = CellBorderType.THIN
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            style.WrapText = True
            style.VerticalAlignment = VerticalAlignment.CENTER
            style.FillPattern = FillPatternType.SOLID_FOREGROUND
            style.BorderRight = CellBorderType.THIN
            style.BorderBottom = CellBorderType.THIN
            style.BorderLeft = CellBorderType.THIN
            style.BorderTop = CellBorderType.THIN
            style.SetFont(oFont)
            '===============================
            stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
            stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
            stylePorcentaje.BorderRight = CellBorderType.THIN
            stylePorcentaje.BorderBottom = CellBorderType.THIN
            stylePorcentaje.BorderLeft = CellBorderType.THIN
            stylePorcentaje.BorderTop = CellBorderType.THIN
            stylePorcentaje.WrapText = True
            stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
            stylePorcentaje.SetFont(oFontFiltro)
            '===============================
            styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.WrapText = True
            styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloConcepto.SetFont(oFontTituloConcepto)
            '===============================
            styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.WrapText = True
            styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloDescripcion.DataFormat = NPOI.SS.UserModel.CellType.STRING
            styleTituloDescripcion.SetFont(oFontTituloDescripcion)
            '===============================
            Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.BorderRight = CellBorderType.THIN
            styleNumber.BorderBottom = CellBorderType.THIN
            styleNumber.BorderLeft = CellBorderType.THIN
            styleNumber.BorderTop = CellBorderType.THIN
            styleNumber.WrapText = True
            styleNumber.VerticalAlignment = VerticalAlignment.CENTER
            styleNumber.SetFont(oFontFiltro)
            styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
            styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleNumbercuatro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumbercuatro.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumbercuatro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumbercuatro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumbercuatro.BorderRight = CellBorderType.THIN
            styleNumbercuatro.BorderBottom = CellBorderType.THIN
            styleNumbercuatro.BorderLeft = CellBorderType.THIN
            styleNumbercuatro.BorderTop = CellBorderType.THIN
            styleNumbercuatro.WrapText = True
            styleNumbercuatro.VerticalAlignment = VerticalAlignment.CENTER
            styleNumbercuatro.SetFont(oFontFiltro)
            styleNumbercuatro.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.BorderRight = CellBorderType.THIN
            styleMonto.BorderBottom = CellBorderType.THIN
            styleMonto.BorderLeft = CellBorderType.THIN
            styleMonto.BorderTop = CellBorderType.THIN
            styleMonto.WrapText = True
            styleMonto.VerticalAlignment = VerticalAlignment.CENTER
            styleMonto.SetFont(oFontFiltro)
            styleMonto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim rowActual As Integer = 2
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2
            '===========Titulo es centrado segun tamaño de grilla===========
            If Not strTitulo Is Nothing Then
                objWorkSheet.CreateRow(rowActual + 1)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                Next
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            End If
            '==============Verificamos Filtro a utilizar==================
            For intCont As Integer = 0 To olstrFiltros.Count - 1
                Dim strDescripcionTitulo As String()
                strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                objWorkSheet.CreateRow(rowActual + 1)
                objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))
                For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                Next
                'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            Next
            objWorkSheet.CreateRow(rowActual + 1)
            rowActual += 2
            '===================Se cargan Las Cabeceras=====================
            Dim flgCabDoble As Boolean = False
            objWorkSheet.CreateRow(rowActual)
            For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                '===Estilo===
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                '===Valores===
                Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                Dim valorCabDoble As String()
                valorCabDoble = Split(valorCab, "\n")
                If valorCabDoble.Length = 2 Then
                    flgCabDoble = True
                End If
                objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
            Next
            rowActual = rowActual + 1
            Dim intRepite As Integer = 0
            If flgCabDoble = True Then
                '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                    intRepite = 0
                    For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                            intRepite = intRepite + 1
                        End If
                    Next
                    If intRepite > 1 Then
                        intRepite = intRepite - 1
                        Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                        '===Union===
                        Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                        objWorkSheet.AddMergedRegion(Region)
                        x = x + intRepite
                    End If
                    ' End If
                Next
                '===================================================================
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                    End If

                Next
                rowActual = rowActual + 1
            End If
            '===================Se Carga El Detalle============================

            For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                objWorkSheet.CreateRow(i + rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                    Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString
                    If oDs.Tables(intTable).Columns(x).ColumnName = "CODIGO" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "DESCRIPCION" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" & vbLf & " (F)" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "ROTACION  DE INVENTARIO" & vbLf & " (G)" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                        If IsNumeric(celda) Then
                            Dim valor As Double = Math.Round(CDbl(celda), 4)
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                        Else
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        End If
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PARTICIPACION DE OPERACION" & vbLf & "(H)" Then 'csr
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                        If IsNumeric(celda) Then
                            Dim valor As Double = Math.Round(CDbl(celda), 4)
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                        Else
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        End If
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "ÍNDICE DE ROTACIÓN POR DÍA " & vbLf & " (365/G)" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                        If IsNumeric(celda) Then
                            Dim valor As Double = Math.Round(CDbl(celda), 4)
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                        Else
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        End If

                    Else
                        If celda Is DBNull.Value Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                        Else
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleNumber
                            If celda Is "" Then
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                            Else
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                            End If
                        End If
                    End If
                Next
            Next
            '===================================================================
            For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                objWorkSheet.AutoSizeColumn(z, True)
            Next
            '============================================logo=================================
            'create the anchor
            Dim anchor As HSSFClientAnchor
            ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
            anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
            If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                picture.Resize(0.5)
            End If
            If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                'Reset the image to the original size.
                picture.Resize(0.5)
            End If
            ''============================================logo=================================
        Next

        ''==================================SEGUNDA HOJA ==============================

        For intTable As Integer = 0 To oDs.Tables.Count - 1
            Dim dtRotacion As New DataTable
            dtRotacion = oDs.Tables(0).Copy
            ''================== Creando el nuevo DT =====================
            dtRotacion.Columns.Remove("VALOR UNITARIO S/.")
            dtRotacion.Columns.Remove("STOCK INICIAL \n CANTIDAD")
            dtRotacion.Columns.Remove("STOCK INICIAL \n VALOR")
            dtRotacion.Columns.Remove("ENERO \n INGRESO ")
            dtRotacion.Columns.Remove("ENERO \n DESPACHO ")
            dtRotacion.Columns.Remove("ENERO \n STOCK ")
            dtRotacion.Columns.Remove("FEBRERO \n INGRESO ")
            dtRotacion.Columns.Remove("FEBRERO \n DESPACHO ")
            dtRotacion.Columns.Remove("FEBRERO \n STOCK")
            dtRotacion.Columns.Remove("MARZO \n INGRESO ")
            dtRotacion.Columns.Remove("MARZO \n DESPACHO ")
            dtRotacion.Columns.Remove("MARZO \n STOCK")
            dtRotacion.Columns.Remove("ABRIL \n INGRESO ")
            dtRotacion.Columns.Remove("ABRIL \n DESPACHO ")
            dtRotacion.Columns.Remove("ABRIL \n STOCK")
            dtRotacion.Columns.Remove("MAYO \n INGRESO ")
            dtRotacion.Columns.Remove("MAYO \n DESPACHO ")
            dtRotacion.Columns.Remove("MAYO \n STOCK")
            dtRotacion.Columns.Remove("JUNIO \n INGRESO ")
            dtRotacion.Columns.Remove("JUNIO \n DESPACHO ")
            dtRotacion.Columns.Remove("JUNIO \n STOCK")
            dtRotacion.Columns.Remove("JULIO \n INGRESO ")
            dtRotacion.Columns.Remove("JULIO \n DESPACHO ")
            dtRotacion.Columns.Remove("JULIO \n STOCK")
            dtRotacion.Columns.Remove("AGOSTO \n INGRESO ")
            dtRotacion.Columns.Remove("AGOSTO \n DESPACHO ")
            dtRotacion.Columns.Remove("AGOSTO \n STOCK")
            dtRotacion.Columns.Remove("SETIEMBRE \n INGRESO ")
            dtRotacion.Columns.Remove("SETIEMBRE \n DESPACHO ")
            dtRotacion.Columns.Remove("SETIEMBRE \n STOCK")
            dtRotacion.Columns.Remove("OCTUBRE \n INGRESO ")
            dtRotacion.Columns.Remove("OCTUBRE \n DESPACHO ")
            dtRotacion.Columns.Remove("OCTUBRE \n STOCK")
            dtRotacion.Columns.Remove("NOVIEMBRE \n INGRESO ")
            dtRotacion.Columns.Remove("NOVIEMBRE \n DESPACHO")
            dtRotacion.Columns.Remove("NOVIEMBRE \n STOCK ")
            dtRotacion.Columns.Remove("DICIEMBRE  \n INGRESO ")
            dtRotacion.Columns.Remove("DICIEMBRE  \n DESPACHO ")
            dtRotacion.Columns.Remove("DICIEMBRE  \n STOCK ")
            dtRotacion.Columns.Remove("STOCK FINAL \n CANTIDAD")
            dtRotacion.Columns.Remove("STOCK FINAL \n VALOR")
            dtRotacion.Columns.Remove("STOCK PROMEDIO \n (A)")
            dtRotacion.Columns.Remove("TOTAL INGRESO \n CANTIDAD (B)")
            dtRotacion.Columns.Remove("TOTAL INGRESO \n VALOR (C)")
            dtRotacion.Columns.Remove("TOTAL DESPACHO \n CANTIDAD (D)")
            dtRotacion.Columns.Remove("TOTAL DESPACHO \n VALOR (E)")
            dtRotacion.Columns.Remove("PORCENTAJE" & vbLf & " (F)")
            dtRotacion.Columns.Remove("PARTICIPACION DE OPERACION" & vbLf & "(H)")
            dtRotacion.Columns.Remove("ANALISIS DE ROTACION" & vbLf & "(I)")

            dtRotacion.Columns("ROTACION  DE INVENTARIO" & vbLf & " (G)").ColumnName = "ROTACION  DE INVENTARIO"
            dtRotacion.Columns("ÍNDICE DE ROTACIÓN POR DÍA " & vbLf & " (365/G)").ColumnName = "ÍNDICE DE ROTACIÓN POR DÍA"

            ''================== Creando un DataTable temporal para ordenamiento =====================

            Dim dtTemp As DataTable = dtRotacion.Copy

            'Declaramos las variables 
            Dim IndiceRotacionAnual As Double
            Dim IndiceRotacionDia As Integer

            'Adicionamos las columnas con el tipo de dato necesitado
            dtTemp.Columns.Add("TempIndiceRotacionAnual", Type.GetType("System.Double"))
            dtTemp.Columns.Add("TempIndiceRotacionDia", Type.GetType("System.Int32"))

            'Obtenemos, adicionamos y convertimos los datos STRING a ENTERO 
            For i As Integer = 0 To dtRotacion.Rows.Count - 1
                IndiceRotacionAnual = Convert.ToDouble(Val(dtRotacion.Rows(i)("ROTACION  DE INVENTARIO")))
                IndiceRotacionDia = Convert.ToInt32(Val(dtRotacion.Rows(i)("ÍNDICE DE ROTACIÓN POR DÍA")))
                dtTemp.Rows(i)("TempIndiceRotacionAnual") = IndiceRotacionAnual
                dtTemp.Rows(i)("TempIndiceRotacionDia") = IndiceRotacionDia
            Next

            'Eliminamos las columnas innecesarias 
            dtTemp.Columns.Remove("ROTACION  DE INVENTARIO")
            dtTemp.Columns.Remove("ÍNDICE DE ROTACIÓN POR DÍA")

            'Renombramos las columnas nuevas  
            dtTemp.Columns("TempIndiceRotacionAnual").ColumnName = "ROTACION  DE INVENTARIO"
            dtTemp.Columns("TempIndiceRotacionDia").ColumnName = "ÍNDICE DE ROTACIÓN POR DÍA"

            ''================== Creando el nuevo DT por Año =====================

            'Creamos un DataView para hacer el ordenamiento
            Dim dvRotacionAnual As New DataView
            dvRotacionAnual = dtTemp.DefaultView
            dvRotacionAnual.Sort = "ROTACION  DE INVENTARIO DESC"

            'Asignamos el Dataview ordenado a nuestro DataTable
            Dim dtRotacionAnual As DataTable = dvRotacionAnual.ToTable()
            dtRotacionAnual.TableName = "Rotacion Anual"
            dtRotacionAnual.Columns.Remove("ÍNDICE DE ROTACIÓN POR DÍA")

            ''================== Creando el nuevo DT por Dia =====================

            'Creamos un DataView para hacer el ordenamiento
            Dim dvRotacionDia As New DataView
            dvRotacionDia = dtTemp.DefaultView
            dvRotacionDia.Sort = "ÍNDICE DE ROTACIÓN POR DÍA DESC"

            'Asignamos el Dataview ordenado a nuestro DataTable
            Dim dtRotacionDia As DataTable = dvRotacionDia.ToTable()
            dtRotacionDia.TableName = "Rotacion Dia"
            dtRotacionDia.Columns.Remove("ROTACION  DE INVENTARIO")

            ''================== Nombre de la 2da Hoja =====================
            objWorkSheet = objWorkBook.CreateSheet("Graficos")
            objWorkSheet.CreateRow(3)

            '=============Style======================
            Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTexto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
            '===============================
            Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontFiltro.FontHeight = 165
            '===============================
            Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontlink.FontName = "Wingdings"
            oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontlink.FontHeight = 250
            '===============================
            Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220
            '===============================
            Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
            oFont.FontHeight = 170
            '===============================
            Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
            oFontTituloConcepto.FontHeight = 190
            '===============================
            Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
            oFontTituloDescripcion.FontHeight = 170
            '===============================
            styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.BorderRight = CellBorderType.THIN
            styleFiltro.WrapText = True
            styleFiltro.VerticalAlignment = VerticalAlignment.CENTER
            styleFiltro.BorderBottom = CellBorderType.THIN
            styleFiltro.BorderLeft = CellBorderType.THIN
            styleFiltro.BorderTop = CellBorderType.THIN
            styleFiltro.SetFont(oFontFiltro)
            styleFiltro.DataFormat = NPOI.SS.UserModel.CellType.STRING
            styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            styleTexto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleTexto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleTexto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleTexto.BorderRight = CellBorderType.THIN
            styleTexto.WrapText = True
            styleTexto.VerticalAlignment = VerticalAlignment.CENTER
            styleTexto.BorderBottom = CellBorderType.THIN
            styleTexto.BorderLeft = CellBorderType.THIN
            styleTexto.BorderTop = CellBorderType.THIN
            styleTexto.SetFont(oFontFiltro)
            ' styleTexto.DataFormat = NPOI.SS.UserModel.CellType.STRING
            styleTexto.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            styleCab.WrapText = True
            styleCab.VerticalAlignment = VerticalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleCab.BorderRight = CellBorderType.THIN
            styleCab.BorderBottom = CellBorderType.THIN
            styleCab.BorderLeft = CellBorderType.THIN
            styleCab.BorderTop = CellBorderType.THIN
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
            style.WrapText = True
            style.VerticalAlignment = VerticalAlignment.CENTER
            style.FillPattern = FillPatternType.SOLID_FOREGROUND
            style.BorderRight = CellBorderType.THIN
            style.BorderBottom = CellBorderType.THIN
            style.BorderLeft = CellBorderType.THIN
            style.BorderTop = CellBorderType.THIN
            style.SetFont(oFont)
            '===============================
            stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
            stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
            stylePorcentaje.BorderRight = CellBorderType.THIN
            stylePorcentaje.BorderBottom = CellBorderType.THIN
            stylePorcentaje.BorderLeft = CellBorderType.THIN
            stylePorcentaje.BorderTop = CellBorderType.THIN
            stylePorcentaje.WrapText = True
            stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
            stylePorcentaje.SetFont(oFontFiltro)
            '===============================
            styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.WrapText = True
            styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloConcepto.SetFont(oFontTituloConcepto)
            '===============================
            styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.WrapText = True
            styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloDescripcion.DataFormat = NPOI.SS.UserModel.CellType.STRING
            styleTituloDescripcion.SetFont(oFontTituloDescripcion)
            '===============================
            Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.BorderRight = CellBorderType.THIN
            styleNumber.BorderBottom = CellBorderType.THIN
            styleNumber.BorderLeft = CellBorderType.THIN
            styleNumber.BorderTop = CellBorderType.THIN
            styleNumber.WrapText = True
            styleNumber.VerticalAlignment = VerticalAlignment.CENTER
            styleNumber.SetFont(oFontFiltro)
            styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
            styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
            styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.BorderRight = CellBorderType.THIN
            styleMonto.BorderBottom = CellBorderType.THIN
            styleMonto.BorderLeft = CellBorderType.THIN
            styleMonto.BorderTop = CellBorderType.THIN
            styleMonto.WrapText = True
            styleMonto.VerticalAlignment = VerticalAlignment.CENTER
            styleMonto.SetFont(oFontFiltro)
            styleMonto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
            styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND



            '========================== PRIMER CUADRO ===========================
            Dim rowActual As Integer = 2
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2
            '===========Titulo es centrado segun tamaño de grilla===========
            If Not strTitulo Is Nothing Then
                objWorkSheet.CreateRow(rowActual + 1)
                For x As Integer = 0 To dtRotacionAnual.Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                Next
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, dtRotacionAnual.Columns.Count - 1))
                rowActual += 1
            End If
            '==============Verificamos Filtro a utilizar==================
            For intCont As Integer = 0 To olstrFiltros.Count - 1
                Dim strDescripcionTitulo As String()
                strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                objWorkSheet.CreateRow(rowActual + 1)
                objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                Next

                objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, dtRotacionAnual.Columns.Count - 1))
                rowActual += 1
            Next
            objWorkSheet.CreateRow(rowActual + 1)
            rowActual += 2
            '===================Se cargan Las Cabeceras=====================
            Dim flgCabDoble As Boolean = False
            objWorkSheet.CreateRow(rowActual)
            For x As Integer = 0 To dtRotacionAnual.Columns.Count - 1
                '===Estilo===
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                '===Valores===
                Dim valorCab As String = dtRotacionAnual.Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                Dim valorCabDoble As String()
                valorCabDoble = Split(valorCab, "\n")
                If valorCabDoble.Length = 2 Then
                    flgCabDoble = True
                End If
                objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
            Next
            rowActual = rowActual + 1
            '===================Se Carga El Detalle============================
            For i As Integer = 0 To 19
                objWorkSheet.CreateRow(i + rowActual)
                For x As Integer = 0 To dtRotacionAnual.Columns.Count - 1
                    Dim celda As String = dtRotacionAnual.Rows(i).Item(x).ToString().Trim()
                    Dim TypoDato As String = dtRotacionAnual.Columns.Item(x).DataType.ToString

                    If dtRotacionAnual.Columns(x).ColumnName = "CODIGO" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf dtRotacionAnual.Columns(x).ColumnName = "DESCRIPCION" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf dtRotacionAnual.Columns(x).ColumnName = "ROTACION  DE INVENTARIO" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                    End If
                Next
            Next


            ''========================== Segundo CUADRO ===========================
            rowActual = 30
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2

            '===================Se cargan Las Cabeceras=====================
            flgCabDoble = False
            objWorkSheet.CreateRow(rowActual)
            For x As Integer = 0 To dtRotacionDia.Columns.Count - 1
                '===Estilo===
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                '===Valores===
                Dim valorCab As String = dtRotacionDia.Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                Dim valorCabDoble As String()
                valorCabDoble = Split(valorCab, "\n")
                If valorCabDoble.Length = 2 Then
                    flgCabDoble = True
                End If
                objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
            Next
            rowActual = rowActual + 1
            '===================Se Carga El Detalle============================
            For i As Integer = 0 To 19
                objWorkSheet.CreateRow(i + rowActual)
                For x As Integer = 0 To dtRotacionDia.Columns.Count - 1
                    Dim celda As String = dtRotacionDia.Rows(i).Item(x).ToString().Trim()
                    Dim TypoDato As String = dtRotacionDia.Columns.Item(x).DataType.ToString

                    If dtRotacionDia.Columns(x).ColumnName = "CODIGO" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf dtRotacionDia.Columns(x).ColumnName = "DESCRIPCION" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf dtRotacionDia.Columns(x).ColumnName = "ÍNDICE DE ROTACIÓN POR DÍA" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleTexto
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt32(Val(celda)))
                    End If
                Next
            Next
            '===================================================================
            For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                objWorkSheet.AutoSizeColumn(z, True)
            Next
            '============================================logo=================================
            'create the anchor
            Dim anchor As HSSFClientAnchor
            ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
            anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
            If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                picture.Resize(0.5)
            End If
            If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                'load the picture and get the picture index in the workbook
                Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                'Reset the image to the original size.
                picture.Resize(0.5)
            End If
            ''============================================logo=================================
        Next

        objWorkBook.Write(fs)
        fs.Close()
        AbrirDocumento(archivo)
    End Sub

    Public Shared Sub ExportarEficienciaDespachoSinGraficos(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)
            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1
                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleEspecial As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165

                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250

                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.WrapText = True
                styleFiltro.VerticalAlignment = VerticalAlignment.CENTER

                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.WrapText = True
                styleCab.VerticalAlignment = VerticalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.WrapText = True
                style.VerticalAlignment = VerticalAlignment.CENTER
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                styleEspecial.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleEspecial.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleEspecial.BorderRight = CellBorderType.THIN
                styleEspecial.WrapText = True
                styleEspecial.VerticalAlignment = VerticalAlignment.CENTER
                styleEspecial.BorderBottom = CellBorderType.THIN
                styleEspecial.BorderLeft = CellBorderType.THIN
                styleEspecial.BorderTop = CellBorderType.THIN
                styleEspecial.SetFont(oFontFiltro)
                styleEspecial.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleEspecial.FillPattern = FillPatternType.SOLID_FOREGROUND

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.WrapText = True
                stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.WrapText = True
                styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.WrapText = True
                styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN
                styleNumber.WrapText = True
                styleNumber.VerticalAlignment = VerticalAlignment.CENTER
                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.WrapText = True
                styleMonto.VerticalAlignment = VerticalAlignment.CENTER
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.WrapText = True
                styleLink.VerticalAlignment = VerticalAlignment.CENTER
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                    Next

                    'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                Next
                objWorkSheet.CreateRow(rowActual + 1)
                rowActual += 2
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        flgCabDoble = True
                    End If
                    objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
                Next

                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next
                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If
                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

                    objWorkSheet.CreateRow(i + rowActual)

                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                        If i = 2 Or i = 5 Then
                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then
                                If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If
                                End If
                            Else
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleEspecial
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleEspecial
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToString(celda))
                                End If
                            End If
                        Else
                            '****
                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then
                                If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If
                                Else
                                    If celda Is "" Then
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                    Else
                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                                    End If
                                End If
                                '****
                            ElseIf TypoDato.Equals("System.Byte[]") Then
                                Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                                Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                                Try
                                    link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                                Catch : End Try
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                                If Not celda Is "" Then
                                    Dim anchorImagen As HSSFClientAnchor
                                    anchorImagen = New HSSFClientAnchor(0, 0, 0, 0, x, i + rowActual, 0, 0)
                                    Dim imagenAlmacen = DirectCast(patriarch.CreatePicture(anchorImagen, CargaImagen_NPOI2(oDs.Tables(intTable).Rows(i).Item(x), objWorkBook)), HSSFPicture)
                                    imagenAlmacen.LineStyle = 1
                                    imagenAlmacen.Resize()
                                End If
                            Else
                                If celda Is DBNull.Value Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                End If
                            End If
                        End If
                    Next
                Next
                ''Suma los registros indicados
                Dim strFormula As String
                objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                If Not listSuma Is Nothing Then
                    For Each items As DictionaryEntry In listSuma
                        strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 7).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 1, 0) + 6).ToString & ")"
                        If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMonto
                        objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                    Next
                End If
                '===================================================================
                For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.AutoSizeColumn(z, True)
                Next
                '============================================logo=================================
                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
            Next

            ''============================================Segunda Pagina =================================
            For intTable As Integer = 0 To oDs.Tables.Count - 1
                objWorkSheet = objWorkBook.CreateSheet("Graficos") 'oDs.Tables(intTable).TableName)

                '=============Style======================

                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontCab.FontHeight = 220

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                'styleCab.WrapText = True
                styleCab.VerticalAlignment = VerticalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                'styleTituloConcepto.WrapText = True
                styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                'styleTituloDescripcion.WrapText = True
                styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)
                '===============================

                Dim rowActual As Integer = 2
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0) & "   ")

                    For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                    Next

                    ' objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                    rowActual += 1
                Next
                objWorkSheet.CreateRow(rowActual + 1)
                rowActual += 2


                objWorkSheet.AutoSizeColumn(0, True)


                'create the anchor
                Dim anchor As HSSFClientAnchor
                ' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                anchor = New HSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                ''============================================logo=================================
            Next

            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Creacion de excel con Gráfica"
    Public Shared Sub ExportarConsultaEriConGraficos(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Dim path As String = Application.StartupPath.ToString
        Dim archivoTemplate As String = path & "\ExportToGraphic\Plantilla Eri.xls" 'path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
        Dim archivoReporte As String = path & "reporte" & HelpClass.NowNumeric & ".xls"
        Dim fs As New IO.FileStream(archivoTemplate, IO.FileMode.Open, IO.FileAccess.Read)
        Dim objWorkBook As New NPOIV2.HSSF.UserModel.HSSFWorkbook(fs, True)
        Dim objWorkSheet As NPOIV2.HSSF.UserModel.HSSFSheet

        For intTable As Integer = 0 To oDs.Tables.Count - 1
            objWorkSheet = objWorkBook.GetSheet("Consulta Eri")
            '=============Style======================
            Dim styleFiltro As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleCab As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim style As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloConcepto As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloDescripcion As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim stylePorcentaje As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            '  Dim patriarch As NPOIV2.HSSF.UserModel.HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), NPOIV2.HSSF.UserModel.HSSFPatriarch)
            '===========================
            Dim oFontFiltro As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontFiltro.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontFiltro.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontFiltro.FontHeight = 165
            '===========================
            Dim oFontlink As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontlink.FontName = "Wingdings"
            oFontlink.Underline = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontlink.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontlink.FontHeight = 250
            '===========================
            Dim oFontCab As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOIV2.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220
            '===========================
            Dim oFont As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFont.Color = NPOIV2.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFont.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFont.FontHeight = 170
            '===========================
            Dim oFontTituloConcepto As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloConcepto.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloConcepto.Boldweight = NPOIV2.SS.UserModel.FontBoldWeight.BOLD
            oFontTituloConcepto.FontHeight = 190
            '===========================
            Dim oFontTituloDescripcion As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloDescripcion.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloDescripcion.Boldweight = NPOIV2.SS.UserModel.FontBoldWeight.NORMAL
            oFontTituloDescripcion.FontHeight = 170
            '===========================
            styleFiltro.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleFiltro.BorderRight = CellBorderType.THIN
            styleFiltro.WrapText = True
            styleFiltro.VerticalAlignment = VerticalAlignment.CENTER
            styleFiltro.BorderBottom = CellBorderType.THIN
            styleFiltro.BorderLeft = CellBorderType.THIN
            styleFiltro.BorderTop = CellBorderType.THIN
            styleFiltro.SetFont(oFontFiltro)
            styleFiltro.DataFormat = NPOI.SS.UserModel.CellType.STRING
            styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===========================
            styleCab.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.CENTER
            styleCab.WrapText = True
            styleCab.VerticalAlignment = VerticalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleCab.BorderRight = CellBorderType.THIN
            styleCab.BorderBottom = CellBorderType.THIN
            styleCab.BorderLeft = CellBorderType.THIN
            styleCab.BorderTop = CellBorderType.THIN
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===========================
            style.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREEN.index
            style.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            style.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.CENTER
            style.WrapText = True
            style.VerticalAlignment = VerticalAlignment.CENTER
            style.FillPattern = FillPatternType.SOLID_FOREGROUND
            style.BorderRight = CellBorderType.THIN
            style.BorderBottom = CellBorderType.THIN
            style.BorderLeft = CellBorderType.THIN
            style.BorderTop = CellBorderType.THIN
            style.SetFont(oFont)
            '===========================
            stylePorcentaje.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            stylePorcentaje.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
            stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
            stylePorcentaje.BorderRight = CellBorderType.THIN
            stylePorcentaje.BorderBottom = CellBorderType.THIN
            stylePorcentaje.BorderLeft = CellBorderType.THIN
            stylePorcentaje.BorderTop = CellBorderType.THIN
            stylePorcentaje.WrapText = True
            stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
            stylePorcentaje.SetFont(oFontFiltro)
            '===========================
            styleTituloConcepto.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloConcepto.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.WrapText = True
            styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloConcepto.SetFont(oFontTituloConcepto)
            '===========================
            styleTituloDescripcion.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloDescripcion.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.WrapText = True
            styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloDescripcion.SetFont(oFontTituloDescripcion)
            '===============================
            Dim styleNumber As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumber.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumber.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.BorderRight = CellBorderType.THIN
            styleNumber.BorderBottom = CellBorderType.THIN
            styleNumber.BorderLeft = CellBorderType.THIN
            styleNumber.BorderTop = CellBorderType.THIN
            styleNumber.WrapText = True
            styleNumber.VerticalAlignment = VerticalAlignment.CENTER
            styleNumber.SetFont(oFontFiltro)
            styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
            styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim rowActual As Integer = 2
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2
            '===========Titulo es centrado segun tamaño de grilla===========
            If Not strTitulo Is Nothing Then
                objWorkSheet.CreateRow(rowActual + 1)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                Next
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                'objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count + 1))
                objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            End If
            '==============Verificamos Filtro a utilizar==================
            For intCont As Integer = 0 To olstrFiltros.Count - 1
                Dim strDescripcionTitulo As String()
                strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                objWorkSheet.CreateRow(rowActual + 1)
                objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))
                For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                Next
                '===================================================================
                objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            Next
            '===================================================================
            objWorkSheet.CreateRow(rowActual + 1)
            rowActual += 2
            '===================Se cargan Las Cabeceras=====================
            Dim flgCabDoble As Boolean = False
            objWorkSheet.CreateRow(rowActual)
            For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                '===Estilo===
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                '===Valores===
                Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                Dim valorCabDoble As String()
                valorCabDoble = Split(valorCab, "\n")
                If valorCabDoble.Length = 2 Then
                    flgCabDoble = True
                End If

                objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
            Next
            '===================================================================
            rowActual = rowActual + 1
            Dim intRepite As Integer = 0
            If flgCabDoble = True Then
                '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                    intRepite = 0
                    For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                            intRepite = intRepite + 1
                        End If
                    Next
                    If intRepite > 1 Then
                        intRepite = intRepite - 1
                        Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                        '===Union===
                        Dim Region As New NPOIV2.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                        objWorkSheet.AddMergedRegion(Region)
                        x = x + intRepite
                    End If
                Next
                '===================================================================
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                    End If
                Next
                rowActual = rowActual + 1
            End If

            '===================Se Carga El Detalle============================
            For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                objWorkSheet.CreateRow(i + rowActual)

                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                    Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                    If oDs.Tables(intTable).Columns(x).ColumnName = "CÓDIGO" & vbLf & "MES" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "STOCK" & vbLf & "FISICO" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "STOCK" & vbLf & "SISTEMAS" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)

                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "" & vbLf & "ERI (%)" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda))
                    End If
                Next
            Next
            '===================================================================
            For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                objWorkSheet.AutoSizeColumn(z, True)
            Next
        Next
        'Abrimos el documento
        '--------------------------------------
        Dim file As New FileStream(archivoReporte, FileMode.Create)
        objWorkBook.Write(file)
        file.Close()
        AbrirDocumento(archivoReporte)
    End Sub

    Public Shared Sub ExportarReporteRotacionConGraficos(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Dim path As String = Application.StartupPath.ToString
        Dim archivoTemplate As String = path & "\ExportToGraphic\Plantilla Rotacion.xls" 'path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
        Dim archivoReporte As String = path & "reporte" & HelpClass.NowNumeric & ".xls"
        Dim fs As New IO.FileStream(archivoTemplate, IO.FileMode.Open, IO.FileAccess.Read)
        Dim objWorkBook As New NPOIV2.HSSF.UserModel.HSSFWorkbook(fs, True)
        Dim objWorkSheet As NPOIV2.HSSF.UserModel.HSSFSheet

        For intTable As Integer = 0 To oDs.Tables.Count - 1
            objWorkSheet = objWorkBook.GetSheet("Rotacion Mensual")
            '=============Style======================
            Dim styleFiltro As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTexto As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleCab As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim style As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloConcepto As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloDescripcion As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim stylePorcentaje As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            '===============================
            Dim oFontFiltro As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontFiltro.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontFiltro.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontFiltro.FontHeight = 165
            '===============================
            Dim oFontlink As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontlink.FontName = "Wingdings"
            oFontlink.Underline = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontlink.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontlink.FontHeight = 250
            '===============================
            Dim oFontCab As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOIV2.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220
            '===============================
            Dim oFont As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFont.Color = NPOIV2.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFont.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFont.FontHeight = 170
            '===============================
            Dim oFontTituloConcepto As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloConcepto.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloConcepto.Boldweight = NPOIV2.SS.UserModel.FontBoldWeight.BOLD
            oFontTituloConcepto.FontHeight = 190
            '===============================
            Dim oFontTituloDescripcion As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloDescripcion.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloDescripcion.Boldweight = NPOIV2.SS.UserModel.FontBoldWeight.NORMAL
            oFontTituloDescripcion.FontHeight = 170
            '===============================
            styleFiltro.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.BorderRight = CellBorderType.THIN
            styleFiltro.WrapText = True
            styleFiltro.VerticalAlignment = VerticalAlignment.CENTER
            styleFiltro.BorderBottom = CellBorderType.THIN
            styleFiltro.BorderLeft = CellBorderType.THIN
            styleFiltro.BorderTop = CellBorderType.THIN
            styleFiltro.SetFont(oFontFiltro)
            styleFiltro.DataFormat = NPOIV2.SS.UserModel.CellType.STRING
            styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            styleTexto.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleTexto.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleTexto.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleTexto.BorderRight = CellBorderType.THIN
            styleTexto.WrapText = True
            styleTexto.VerticalAlignment = VerticalAlignment.CENTER
            styleTexto.BorderBottom = CellBorderType.THIN
            styleTexto.BorderLeft = CellBorderType.THIN
            styleTexto.BorderTop = CellBorderType.THIN
            styleTexto.SetFont(oFontFiltro)
            ' styleTexto.DataFormat = NPOIV2.SS.UserModel.CellType.STRING
            styleTexto.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleNumbercuatro As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumbercuatro.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumbercuatro.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumbercuatro.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumbercuatro.BorderRight = CellBorderType.THIN
            styleNumbercuatro.BorderBottom = CellBorderType.THIN
            styleNumbercuatro.BorderLeft = CellBorderType.THIN
            styleNumbercuatro.BorderTop = CellBorderType.THIN
            styleNumbercuatro.WrapText = True
            styleNumbercuatro.VerticalAlignment = VerticalAlignment.CENTER
            styleNumbercuatro.SetFont(oFontFiltro)
            styleNumbercuatro.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            styleCab.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.CENTER
            styleCab.WrapText = True
            styleCab.VerticalAlignment = VerticalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleCab.BorderRight = CellBorderType.THIN
            styleCab.BorderBottom = CellBorderType.THIN
            styleCab.BorderLeft = CellBorderType.THIN
            styleCab.BorderTop = CellBorderType.THIN
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            style.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREEN.index
            style.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            style.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.CENTER
            style.WrapText = True
            style.VerticalAlignment = VerticalAlignment.CENTER
            style.FillPattern = FillPatternType.SOLID_FOREGROUND
            style.BorderRight = CellBorderType.THIN
            style.BorderBottom = CellBorderType.THIN
            style.BorderLeft = CellBorderType.THIN
            style.BorderTop = CellBorderType.THIN
            style.SetFont(oFont)
            '===============================
            stylePorcentaje.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            stylePorcentaje.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
            stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
            stylePorcentaje.BorderRight = CellBorderType.THIN
            stylePorcentaje.BorderBottom = CellBorderType.THIN
            stylePorcentaje.BorderLeft = CellBorderType.THIN
            stylePorcentaje.BorderTop = CellBorderType.THIN
            stylePorcentaje.WrapText = True
            stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
            stylePorcentaje.SetFont(oFontFiltro)
            '===============================
            styleTituloConcepto.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloConcepto.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.WrapText = True
            styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloConcepto.SetFont(oFontTituloConcepto)
            '===============================
            styleTituloDescripcion.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloDescripcion.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.WrapText = True
            styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloDescripcion.DataFormat = NPOIV2.SS.UserModel.CellType.STRING
            styleTituloDescripcion.SetFont(oFontTituloDescripcion)
            '===============================
            Dim styleNumber As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumber.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumber.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.BorderRight = CellBorderType.THIN
            styleNumber.BorderBottom = CellBorderType.THIN
            styleNumber.BorderLeft = CellBorderType.THIN
            styleNumber.BorderTop = CellBorderType.THIN
            styleNumber.WrapText = True
            styleNumber.VerticalAlignment = VerticalAlignment.CENTER
            styleNumber.SetFont(oFontFiltro)
            styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
            styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleMonto As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleMonto.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleMonto.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.BorderRight = CellBorderType.THIN
            styleMonto.BorderBottom = CellBorderType.THIN
            styleMonto.BorderLeft = CellBorderType.THIN
            styleMonto.BorderTop = CellBorderType.THIN
            styleMonto.WrapText = True
            styleMonto.VerticalAlignment = VerticalAlignment.CENTER
            styleMonto.SetFont(oFontFiltro)
            styleMonto.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim rowActual As Integer = 2
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2
            '===========Titulo es centrado segun tamaño de grilla===========
            If Not strTitulo Is Nothing Then
                objWorkSheet.CreateRow(rowActual + 1)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                Next
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            End If
            '==============Verificamos Filtro a utilizar==================
            For intCont As Integer = 0 To olstrFiltros.Count - 1
                Dim strDescripcionTitulo As String()
                strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                objWorkSheet.CreateRow(rowActual + 1)
                objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))
                For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                Next
                'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            Next
            objWorkSheet.CreateRow(rowActual + 1)
            rowActual += 2
            '===================Se cargan Las Cabeceras=====================
            Dim flgCabDoble As Boolean = False
            objWorkSheet.CreateRow(rowActual)
            For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                '===Estilo===
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                '===Valores===
                Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                Dim valorCabDoble As String()
                valorCabDoble = Split(valorCab, "\n")
                If valorCabDoble.Length = 2 Then
                    flgCabDoble = True
                End If
                objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
            Next
            rowActual = rowActual + 1
            Dim intRepite As Integer = 0
            If flgCabDoble = True Then
                '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                    intRepite = 0
                    For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                            intRepite = intRepite + 1
                        End If
                    Next
                    If intRepite > 1 Then
                        intRepite = intRepite - 1
                        Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                        '===Union===
                        Dim Region As New NPOIV2.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                        objWorkSheet.AddMergedRegion(Region)
                        x = x + intRepite
                    End If
                    ' End If
                Next
                '===================================================================
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                    End If

                Next
                rowActual = rowActual + 1
            End If
            '===================Se Carga El Detalle============================

            For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1
                objWorkSheet.CreateRow(i + rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                    Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString
                    If oDs.Tables(intTable).Columns(x).ColumnName = "CODIGO" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToString(celda))
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "DESCRIPCION" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToString(celda))
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PORCENTAJE" & vbLf & " (F)" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "ROTACION  DE INVENTARIO" & vbLf & " (G)" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                        If IsNumeric(celda) Then
                            Dim valor As Double = Math.Round(CDbl(celda), 4)
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                        Else
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        End If
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "PARTICIPACION DE OPERACION" & vbLf & "(H)" Then 'csr
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                        If IsNumeric(celda) Then
                            Dim valor As Double = Math.Round(CDbl(celda), 4)
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                        Else
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        End If
                    ElseIf oDs.Tables(intTable).Columns(x).ColumnName = "ÍNDICE DE ROTACIÓN POR DÍA " & vbLf & " (365/G)" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                        If IsNumeric(celda) Then
                            Dim valor As Double = Math.Round(CDbl(celda), 4)
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                        Else
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        End If
                    Else
                        If celda Is DBNull.Value Then
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                        Else
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleNumber
                            If celda Is "" Then
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                            Else
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                            End If
                        End If
                    End If
                Next
            Next
            '===================================================================
            For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                objWorkSheet.AutoSizeColumn(z, True)
            Next
        Next

        ''==================================SEGUNDA HOJA ==============================

        For intTable As Integer = 0 To oDs.Tables.Count - 1
            Dim dtRotacion As New DataTable
            dtRotacion = oDs.Tables(0).Copy
            ''================== Creando el nuevo DT =====================
            dtRotacion.Columns.Remove("VALOR UNITARIO S/.")
            dtRotacion.Columns.Remove("STOCK INICIAL \n CANTIDAD")
            dtRotacion.Columns.Remove("STOCK INICIAL \n VALOR")
            dtRotacion.Columns.Remove("ENERO \n INGRESO ")
            dtRotacion.Columns.Remove("ENERO \n DESPACHO ")
            dtRotacion.Columns.Remove("ENERO \n STOCK ")
            dtRotacion.Columns.Remove("FEBRERO \n INGRESO ")
            dtRotacion.Columns.Remove("FEBRERO \n DESPACHO ")
            dtRotacion.Columns.Remove("FEBRERO \n STOCK")
            dtRotacion.Columns.Remove("MARZO \n INGRESO ")
            dtRotacion.Columns.Remove("MARZO \n DESPACHO ")
            dtRotacion.Columns.Remove("MARZO \n STOCK")
            dtRotacion.Columns.Remove("ABRIL \n INGRESO ")
            dtRotacion.Columns.Remove("ABRIL \n DESPACHO ")
            dtRotacion.Columns.Remove("ABRIL \n STOCK")
            dtRotacion.Columns.Remove("MAYO \n INGRESO ")
            dtRotacion.Columns.Remove("MAYO \n DESPACHO ")
            dtRotacion.Columns.Remove("MAYO \n STOCK")
            dtRotacion.Columns.Remove("JUNIO \n INGRESO ")
            dtRotacion.Columns.Remove("JUNIO \n DESPACHO ")
            dtRotacion.Columns.Remove("JUNIO \n STOCK")
            dtRotacion.Columns.Remove("JULIO \n INGRESO ")
            dtRotacion.Columns.Remove("JULIO \n DESPACHO ")
            dtRotacion.Columns.Remove("JULIO \n STOCK")
            dtRotacion.Columns.Remove("AGOSTO \n INGRESO ")
            dtRotacion.Columns.Remove("AGOSTO \n DESPACHO ")
            dtRotacion.Columns.Remove("AGOSTO \n STOCK")
            dtRotacion.Columns.Remove("SETIEMBRE \n INGRESO ")
            dtRotacion.Columns.Remove("SETIEMBRE \n DESPACHO ")
            dtRotacion.Columns.Remove("SETIEMBRE \n STOCK")
            dtRotacion.Columns.Remove("OCTUBRE \n INGRESO ")
            dtRotacion.Columns.Remove("OCTUBRE \n DESPACHO ")
            dtRotacion.Columns.Remove("OCTUBRE \n STOCK")
            dtRotacion.Columns.Remove("NOVIEMBRE \n INGRESO ")
            dtRotacion.Columns.Remove("NOVIEMBRE \n DESPACHO")
            dtRotacion.Columns.Remove("NOVIEMBRE \n STOCK ")
            dtRotacion.Columns.Remove("DICIEMBRE  \n INGRESO ")
            dtRotacion.Columns.Remove("DICIEMBRE  \n DESPACHO ")
            dtRotacion.Columns.Remove("DICIEMBRE  \n STOCK ")
            dtRotacion.Columns.Remove("STOCK FINAL \n CANTIDAD")
            dtRotacion.Columns.Remove("STOCK FINAL \n VALOR")
            dtRotacion.Columns.Remove("STOCK PROMEDIO \n (A)")
            dtRotacion.Columns.Remove("TOTAL INGRESO \n CANTIDAD (B)")
            dtRotacion.Columns.Remove("TOTAL INGRESO \n VALOR (C)")
            dtRotacion.Columns.Remove("TOTAL DESPACHO \n CANTIDAD (D)")
            dtRotacion.Columns.Remove("TOTAL DESPACHO \n VALOR (E)")
            dtRotacion.Columns.Remove("PORCENTAJE" & vbLf & " (F)")
            dtRotacion.Columns.Remove("PARTICIPACION DE OPERACION" & vbLf & "(H)")
            dtRotacion.Columns.Remove("ANALISIS DE ROTACION" & vbLf & "(I)")

            dtRotacion.Columns("ROTACION  DE INVENTARIO" & vbLf & " (G)").ColumnName = "ROTACION  DE INVENTARIO"
            dtRotacion.Columns("ÍNDICE DE ROTACIÓN POR DÍA " & vbLf & " (365/G)").ColumnName = "ÍNDICE DE ROTACIÓN POR DÍA"

           
            ''================== Creando un DataTable temporal para ordenamiento =====================

            Dim dtTemp As DataTable = dtRotacion.Copy

            'Declaramos las variables 
            Dim IndiceRotacionAnual As Double
            Dim IndiceRotacionDia As Double

            'Adicionamos las columnas con el tipo de dato necesitado
            dtTemp.Columns.Add("TempIndiceRotacionAnual", Type.GetType("System.Double"))
            dtTemp.Columns.Add("TempIndiceRotacionDia", Type.GetType("System.Double"))

            'Obtenemos, adicionamos y convertimos los datos STRING a NUMERO 
            For i As Integer = 0 To dtRotacion.Rows.Count - 1
                IndiceRotacionAnual = Convert.ToDouble(Val(dtRotacion.Rows(i)("ROTACION  DE INVENTARIO")))
                IndiceRotacionDia = Convert.ToDouble(Val(dtRotacion.Rows(i)("ÍNDICE DE ROTACIÓN POR DÍA")))
                dtTemp.Rows(i)("TempIndiceRotacionAnual") = IndiceRotacionAnual
                dtTemp.Rows(i)("TempIndiceRotacionDia") = IndiceRotacionDia
            Next

            'Eliminamos las columnas innecesarias 
            dtTemp.Columns.Remove("ROTACION  DE INVENTARIO")
            dtTemp.Columns.Remove("ÍNDICE DE ROTACIÓN POR DÍA")

            'Renombramos las columnas nuevas  
            dtTemp.Columns("TempIndiceRotacionAnual").ColumnName = "ROTACION  DE INVENTARIO"
            dtTemp.Columns("TempIndiceRotacionDia").ColumnName = "ÍNDICE DE ROTACIÓN POR DÍA"

            ''================== Creando el nuevo DT por Año =====================

            'Creamos un DataView para hacer el ordenamiento
            Dim dvRotacionAnual As New DataView
            dvRotacionAnual = dtTemp.DefaultView
            dvRotacionAnual.Sort = "ROTACION  DE INVENTARIO DESC"

            'Asignamos el Dataview ordenado a nuestro DataTable
            Dim dtRotacionAnual As DataTable = dvRotacionAnual.ToTable()
            dtRotacionAnual.TableName = "Rotacion Anual"
            dtRotacionAnual.Columns.Remove("ÍNDICE DE ROTACIÓN POR DÍA")

            ''================== Creando el nuevo DT por Dia =====================

            'Creamos un DataView para hacer el ordenamiento
            Dim dvRotacionDia As New DataView
            dvRotacionDia = dtTemp.DefaultView
            dvRotacionDia.Sort = "ÍNDICE DE ROTACIÓN POR DÍA DESC"

            'Asignamos el Dataview ordenado a nuestro DataTable
            Dim dtRotacionDia As DataTable = dvRotacionDia.ToTable()
            dtRotacionDia.TableName = "Rotacion Dia"
            dtRotacionDia.Columns.Remove("ROTACION  DE INVENTARIO")

            ''================== Nombre de la 2da Hoja =====================
            objWorkSheet = objWorkBook.GetSheet("Graficos")
            '=============Style======================
            Dim styleFiltro As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTexto As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleCab As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim style As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloConcepto As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloDescripcion As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim stylePorcentaje As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            '===============================
            Dim oFontFiltro As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontFiltro.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontFiltro.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontFiltro.FontHeight = 165
            '===============================
            Dim oFontlink As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontlink.FontName = "Wingdings"
            oFontlink.Underline = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontlink.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontlink.FontHeight = 250
            '===============================
            Dim oFontCab As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOIV2.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220
            '===============================
            Dim oFont As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFont.Color = NPOIV2.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFont.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFont.FontHeight = 170
            '===============================
            Dim oFontTituloConcepto As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloConcepto.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloConcepto.Boldweight = NPOIV2.SS.UserModel.FontBoldWeight.BOLD
            oFontTituloConcepto.FontHeight = 190
            '===============================
            Dim oFontTituloDescripcion As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloDescripcion.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloDescripcion.Boldweight = NPOIV2.SS.UserModel.FontBoldWeight.NORMAL
            oFontTituloDescripcion.FontHeight = 170
            '===============================
            styleFiltro.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.BorderRight = CellBorderType.THIN
            styleFiltro.WrapText = True
            styleFiltro.VerticalAlignment = VerticalAlignment.CENTER
            styleFiltro.BorderBottom = CellBorderType.THIN
            styleFiltro.BorderLeft = CellBorderType.THIN
            styleFiltro.BorderTop = CellBorderType.THIN
            styleFiltro.SetFont(oFontFiltro)
            styleFiltro.DataFormat = NPOIV2.SS.UserModel.CellType.STRING
            styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            styleTexto.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleTexto.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleTexto.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleTexto.BorderRight = CellBorderType.THIN
            styleTexto.WrapText = True
            styleTexto.VerticalAlignment = VerticalAlignment.CENTER
            styleTexto.BorderBottom = CellBorderType.THIN
            styleTexto.BorderLeft = CellBorderType.THIN
            styleTexto.BorderTop = CellBorderType.THIN
            styleTexto.SetFont(oFontFiltro)
            ' styleTexto.DataFormat = NPOIV2.SS.UserModel.CellType.STRING
            styleTexto.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleNumbercuatro As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumbercuatro.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumbercuatro.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumbercuatro.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumbercuatro.BorderRight = CellBorderType.THIN
            styleNumbercuatro.BorderBottom = CellBorderType.THIN
            styleNumbercuatro.BorderLeft = CellBorderType.THIN
            styleNumbercuatro.BorderTop = CellBorderType.THIN
            styleNumbercuatro.WrapText = True
            styleNumbercuatro.VerticalAlignment = VerticalAlignment.CENTER
            styleNumbercuatro.SetFont(oFontFiltro)
            styleNumbercuatro.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            styleCab.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.CENTER
            styleCab.WrapText = True
            styleCab.VerticalAlignment = VerticalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleCab.BorderRight = CellBorderType.THIN
            styleCab.BorderBottom = CellBorderType.THIN
            styleCab.BorderLeft = CellBorderType.THIN
            styleCab.BorderTop = CellBorderType.THIN
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            style.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREEN.index
            style.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            style.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.CENTER
            style.WrapText = True
            style.VerticalAlignment = VerticalAlignment.CENTER
            style.FillPattern = FillPatternType.SOLID_FOREGROUND
            style.BorderRight = CellBorderType.THIN
            style.BorderBottom = CellBorderType.THIN
            style.BorderLeft = CellBorderType.THIN
            style.BorderTop = CellBorderType.THIN
            style.SetFont(oFont)
            '===============================
            stylePorcentaje.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            stylePorcentaje.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
            stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
            stylePorcentaje.BorderRight = CellBorderType.THIN
            stylePorcentaje.BorderBottom = CellBorderType.THIN
            stylePorcentaje.BorderLeft = CellBorderType.THIN
            stylePorcentaje.BorderTop = CellBorderType.THIN
            stylePorcentaje.WrapText = True
            stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
            stylePorcentaje.SetFont(oFontFiltro)
            '===============================
            styleTituloConcepto.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloConcepto.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.WrapText = True
            styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloConcepto.SetFont(oFontTituloConcepto)
            '===============================
            styleTituloDescripcion.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloDescripcion.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.WrapText = True
            styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloDescripcion.DataFormat = NPOIV2.SS.UserModel.CellType.STRING
            styleTituloDescripcion.SetFont(oFontTituloDescripcion)
            '===============================
            Dim styleNumber As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumber.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumber.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.BorderRight = CellBorderType.THIN
            styleNumber.BorderBottom = CellBorderType.THIN
            styleNumber.BorderLeft = CellBorderType.THIN
            styleNumber.BorderTop = CellBorderType.THIN
            styleNumber.WrapText = True
            styleNumber.VerticalAlignment = VerticalAlignment.CENTER
            styleNumber.SetFont(oFontFiltro)
            styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("0")
            styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleDecimal As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleDecimal.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleDecimal.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleDecimal.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleDecimal.BorderRight = CellBorderType.THIN
            styleDecimal.BorderBottom = CellBorderType.THIN
            styleDecimal.BorderLeft = CellBorderType.THIN
            styleDecimal.BorderTop = CellBorderType.THIN
            styleDecimal.WrapText = True
            styleDecimal.VerticalAlignment = VerticalAlignment.CENTER
            styleDecimal.SetFont(oFontFiltro)
            styleDecimal.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
            styleDecimal.FillPattern = FillPatternType.SOLID_FOREGROUND



            '========================== PRIMER CUADRO ===========================
            Dim rowActual As Integer = 2
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2
            '===========Titulo es centrado segun tamaño de grilla===========
            If Not strTitulo Is Nothing Then
                objWorkSheet.CreateRow(rowActual + 1)
                For x As Integer = 0 To dtRotacionAnual.Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                Next
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 0, rowActual + 1, dtRotacionAnual.Columns.Count - 1))
                rowActual += 1
            End If
            '==============Verificamos Filtro a utilizar==================
            For intCont As Integer = 0 To olstrFiltros.Count - 1
                Dim strDescripcionTitulo As String()
                strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                objWorkSheet.CreateRow(rowActual + 1)
                objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                Next

                objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 1, rowActual + 1, dtRotacionAnual.Columns.Count - 1))
                rowActual += 1
            Next
            objWorkSheet.CreateRow(rowActual + 1)
            rowActual += 2
            '===================Se cargan Las Cabeceras=====================
            Dim flgCabDoble As Boolean = False
            objWorkSheet.CreateRow(rowActual)
            For x As Integer = 0 To dtRotacionAnual.Columns.Count - 1
                '===Estilo===
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                '===Valores===
                Dim valorCab As String = dtRotacionAnual.Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                Dim valorCabDoble As String()
                valorCabDoble = Split(valorCab, "\n")
                If valorCabDoble.Length = 2 Then
                    flgCabDoble = True
                End If
                objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
            Next
            rowActual = rowActual + 1
            '===================Se Carga El Detalle============================
            For i As Integer = 0 To 19
                objWorkSheet.CreateRow(i + rowActual)
                For x As Integer = 0 To dtRotacionAnual.Columns.Count - 1
                    Dim celda As String = dtRotacionAnual.Rows(i).Item(x).ToString().Trim()
                    Dim TypoDato As String = dtRotacionAnual.Columns.Item(x).DataType.ToString

                    If dtRotacionAnual.Columns(x).ColumnName = "CODIGO" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf dtRotacionAnual.Columns(x).ColumnName = "DESCRIPCION" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf dtRotacionAnual.Columns(x).ColumnName = "ROTACION  DE INVENTARIO" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                        If IsNumeric(celda) Then
                            Dim valor As Double = Math.Round(CDbl(celda), 3)
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                        Else
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        End If

                        '    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleDecimal
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                    End If
                Next
            Next


            ''========================== Segundo CUADRO ===========================
            rowActual = 30
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2

            '===================Se cargan Las Cabeceras=====================
            flgCabDoble = False
            objWorkSheet.CreateRow(rowActual)
            For x As Integer = 0 To dtRotacionDia.Columns.Count - 1
                '===Estilo===
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                '===Valores===
                Dim valorCab As String = dtRotacionDia.Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                Dim valorCabDoble As String()
                valorCabDoble = Split(valorCab, "\n")
                If valorCabDoble.Length = 2 Then
                    flgCabDoble = True
                End If
                objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
            Next
            rowActual = rowActual + 1
            '===================Se Carga El Detalle============================
            For i As Integer = 0 To 19
                objWorkSheet.CreateRow(i + rowActual)
                For x As Integer = 0 To dtRotacionDia.Columns.Count - 1
                    Dim celda As String = dtRotacionDia.Rows(i).Item(x).ToString().Trim()
                    Dim TypoDato As String = dtRotacionDia.Columns.Item(x).DataType.ToString

                    If dtRotacionDia.Columns(x).ColumnName = "CODIGO" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf dtRotacionDia.Columns(x).ColumnName = "DESCRIPCION" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                    ElseIf dtRotacionDia.Columns(x).ColumnName = "ÍNDICE DE ROTACIÓN POR DÍA" Then
                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumbercuatro
                        If IsNumeric(celda) Then
                            Dim valor As Double = Math.Round(CDbl(celda), 3)
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(valor)
                        Else
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        End If
                        '    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                        '    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt32(Val(celda)))
                    End If
                Next
            Next
            '===================================================================
            For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                objWorkSheet.AutoSizeColumn(z, True)
            Next
        Next

        'Abrimos el documento
        '--------------------------------------
        Dim file As New FileStream(archivoReporte, FileMode.Create)
        objWorkBook.Write(file)
        file.Close()
        AbrirDocumento(archivoReporte)
    End Sub

    Public Shared Sub ExportarEficienciaDespachoConGraficos(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Dim path As String = Application.StartupPath.ToString
        Dim archivoTemplate As String = path & "\ExportToGraphic\Plantilla Eficiencia Despacho.xls" 'path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
        Dim archivoReporte As String = path & "reporte" & HelpClass.NowNumeric & ".xls"
        Dim fs As New IO.FileStream(archivoTemplate, IO.FileMode.Open, IO.FileAccess.Read)
        Dim objWorkBook As New NPOIV2.HSSF.UserModel.HSSFWorkbook(fs, True)
        Dim objWorkSheet As NPOIV2.HSSF.UserModel.HSSFSheet

        For intTable As Integer = 0 To oDs.Tables.Count - 1
            objWorkSheet = objWorkBook.GetSheet("Eficiencia")
            '=============Style======================
            Dim styleFiltro As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleEspecial As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleCab As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim style As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloConcepto As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloDescripcion As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim stylePorcentaje As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            'Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

            Dim oFontFiltro As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontFiltro.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontFiltro.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontFiltro.FontHeight = 165


            Dim oFontlink As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontlink.FontName = "Wingdings"
            oFontlink.Underline = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontlink.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontlink.FontHeight = 250


            Dim oFontCab As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOIV2.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220

            Dim oFont As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFont.Color = NPOIV2.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFont.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFont.FontHeight = 170

            Dim oFontTituloConcepto As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloConcepto.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloConcepto.Boldweight = NPOIV2.SS.UserModel.FontBoldWeight.BOLD
            oFontTituloConcepto.FontHeight = 190

            Dim oFontTituloDescripcion As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloDescripcion.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloDescripcion.Boldweight = NPOIV2.SS.UserModel.FontBoldWeight.NORMAL
            oFontTituloDescripcion.FontHeight = 170

            styleFiltro.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleFiltro.BorderRight = CellBorderType.THIN
            styleFiltro.WrapText = True
            styleFiltro.VerticalAlignment = VerticalAlignment.CENTER

            styleFiltro.BorderBottom = CellBorderType.THIN
            styleFiltro.BorderLeft = CellBorderType.THIN
            styleFiltro.BorderTop = CellBorderType.THIN
            styleFiltro.SetFont(oFontFiltro)
            styleFiltro.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

            styleCab.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.CENTER
            styleCab.WrapText = True
            styleCab.VerticalAlignment = VerticalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleCab.BorderRight = CellBorderType.THIN
            styleCab.BorderBottom = CellBorderType.THIN
            styleCab.BorderLeft = CellBorderType.THIN
            styleCab.BorderTop = CellBorderType.THIN
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND


            style.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREEN.index
            style.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            style.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.CENTER
            style.WrapText = True
            style.VerticalAlignment = VerticalAlignment.CENTER
            style.FillPattern = FillPatternType.SOLID_FOREGROUND
            style.BorderRight = CellBorderType.THIN
            style.BorderBottom = CellBorderType.THIN
            style.BorderLeft = CellBorderType.THIN
            style.BorderTop = CellBorderType.THIN
            style.SetFont(oFont)


            styleEspecial.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
            styleEspecial.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
            styleEspecial.BorderRight = CellBorderType.THIN
            styleEspecial.WrapText = True
            styleEspecial.VerticalAlignment = VerticalAlignment.CENTER
            styleEspecial.BorderBottom = CellBorderType.THIN
            styleEspecial.BorderLeft = CellBorderType.THIN
            styleEspecial.BorderTop = CellBorderType.THIN
            styleEspecial.SetFont(oFontFiltro)
            styleEspecial.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleEspecial.FillPattern = FillPatternType.SOLID_FOREGROUND

            stylePorcentaje.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            stylePorcentaje.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
            stylePorcentaje.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
            stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
            stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
            stylePorcentaje.BorderRight = CellBorderType.THIN
            stylePorcentaje.BorderBottom = CellBorderType.THIN
            stylePorcentaje.BorderLeft = CellBorderType.THIN
            stylePorcentaje.BorderTop = CellBorderType.THIN
            stylePorcentaje.WrapText = True
            stylePorcentaje.VerticalAlignment = VerticalAlignment.CENTER
            stylePorcentaje.SetFont(oFontFiltro)

            styleTituloConcepto.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloConcepto.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.WrapText = True
            styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloConcepto.SetFont(oFontTituloConcepto)

            styleTituloDescripcion.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloDescripcion.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.WrapText = True
            styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloDescripcion.SetFont(oFontTituloDescripcion)

            '===============================
            Dim styleNumber As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleNumber.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleNumber.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleNumber.BorderRight = CellBorderType.THIN
            styleNumber.BorderBottom = CellBorderType.THIN
            styleNumber.BorderLeft = CellBorderType.THIN
            styleNumber.BorderTop = CellBorderType.THIN
            styleNumber.WrapText = True
            styleNumber.VerticalAlignment = VerticalAlignment.CENTER
            styleNumber.SetFont(oFontFiltro)
            styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
            styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
            '===============================
            Dim styleMonto As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleMonto.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.RIGHT
            styleMonto.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleMonto.BorderRight = CellBorderType.THIN
            styleMonto.BorderBottom = CellBorderType.THIN
            styleMonto.BorderLeft = CellBorderType.THIN
            styleMonto.BorderTop = CellBorderType.THIN
            styleMonto.WrapText = True
            styleMonto.VerticalAlignment = VerticalAlignment.CENTER
            styleMonto.SetFont(oFontFiltro)
            styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
            styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND

            Dim styleLink As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            styleLink.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.CENTER
            styleLink.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleLink.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleLink.BorderRight = CellBorderType.THIN
            styleLink.BorderBottom = CellBorderType.THIN
            styleLink.BorderLeft = CellBorderType.THIN
            styleLink.BorderTop = CellBorderType.THIN
            styleLink.WrapText = True
            styleLink.VerticalAlignment = VerticalAlignment.CENTER
            styleLink.SetFont(oFontlink)
            styleLink.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND

            Dim rowActual As Integer = 2
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2
            '===========================

            '===========Titulo es centrado segun tamaño de grilla===========
            If Not strTitulo Is Nothing Then
                objWorkSheet.CreateRow(rowActual + 1)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                Next
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            End If
            '==============Verificamos Filtro a utilizar==================

            For intCont As Integer = 0 To olstrFiltros.Count - 1
                Dim strDescripcionTitulo As String()
                strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                objWorkSheet.CreateRow(rowActual + 1)
                objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                Next

                'objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            Next
            objWorkSheet.CreateRow(rowActual + 1)
            rowActual += 2
            '===================Se cargan Las Cabeceras=====================
            Dim flgCabDoble As Boolean = False
            objWorkSheet.CreateRow(rowActual)
            For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                '===Estilo===
                objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                '===Valores===
                Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim().Replace("""", "")
                Dim valorCabDoble As String()
                valorCabDoble = Split(valorCab, "\n")
                If valorCabDoble.Length = 2 Then
                    flgCabDoble = True
                End If
                objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
            Next

            rowActual = rowActual + 1
            Dim intRepite As Integer = 0
            If flgCabDoble = True Then
                '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                    intRepite = 0
                    For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                            intRepite = intRepite + 1
                        End If
                    Next
                    If intRepite > 1 Then
                        intRepite = intRepite - 1
                        Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                        '===Union===
                        Dim Region As New NPOIV2.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                        objWorkSheet.AddMergedRegion(Region)
                        x = x + intRepite
                    End If
                    ' End If
                Next
                '===================================================================
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                    End If
                Next
                rowActual = rowActual + 1
            End If
            '===================Se Carga El Detalle============================
            For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

                objWorkSheet.CreateRow(i + rowActual)

                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                    Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).DataType.ToString

                    If i = 2 Or i = 5 Then
                        If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then
                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = stylePorcentaje
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                End If
                            End If
                        Else
                            If celda Is "" Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleEspecial
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                            Else
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleEspecial
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToString(celda))
                            End If
                        End If
                    Else
                        '****
                        If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Int32") Or TypoDato.Equals("System.Double") Or TypoDato.Equals("System.Integer") Then
                            If TypoDato.Equals("System.Decimal") Or TypoDato.Equals("System.Double") Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                End If
                            Else
                                If celda Is "" Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                                End If
                            End If
                            '****
                        ElseIf TypoDato.Equals("System.Byte[]") Then
                            Dim createHelper As HSSFCreationHelper = objWorkBook.GetCreationHelper()
                            Dim link As HSSFHyperlink = createHelper.CreateHyperlink(HyperlinkType.URL)
                            Try
                                link.Address = oDs.Tables(intTable).Rows(i).Item("LINK")
                            Catch : End Try
                            objWorkSheet.GetRow(i + rowActual).CreateCell(x).Hyperlink = link
                            objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleLink
                        Else
                            If celda Is DBNull.Value Then
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                            Else
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                            End If
                        End If
                    End If
                Next
            Next
            '===================================================================
            For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                objWorkSheet.AutoSizeColumn(z, True)
            Next
        Next

        ''============================================Segunda Pagina =================================
        For intTable As Integer = 0 To oDs.Tables.Count - 1
            objWorkSheet = objWorkBook.GetSheet("Graficos") 'oDs.Tables(intTable).TableName)

            '=============Style======================

            Dim styleCab As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloConcepto As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            Dim styleTituloDescripcion As NPOIV2.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
            ' Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

            Dim oFontCab As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontCab.Color = NPOIV2.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontCab.Boldweight = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            oFontCab.FontHeight = 220

            Dim oFontTituloConcepto As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloConcepto.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloConcepto.Boldweight = NPOIV2.SS.UserModel.FontBoldWeight.BOLD
            oFontTituloConcepto.FontHeight = 190

            Dim oFontTituloDescripcion As NPOIV2.SS.UserModel.Font = objWorkBook.CreateFont
            oFontTituloDescripcion.Color = NPOIV2.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
            oFontTituloDescripcion.Boldweight = NPOIV2.SS.UserModel.FontBoldWeight.NORMAL
            oFontTituloDescripcion.FontHeight = 170

            styleCab.FillForegroundColor = NPOIV2.HSSF.Util.HSSFColor.GREEN.index
            styleCab.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.CENTER
            'styleCab.WrapText = True
            styleCab.VerticalAlignment = VerticalAlignment.CENTER
            styleCab.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.WHITE.index
            styleCab.BorderRight = CellBorderType.THIN
            styleCab.BorderBottom = CellBorderType.THIN
            styleCab.BorderLeft = CellBorderType.THIN
            styleCab.BorderTop = CellBorderType.THIN
            styleCab.SetFont(oFontCab)
            styleCab.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

            styleTituloConcepto.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloConcepto.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloConcepto.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            'styleTituloConcepto.WrapText = True
            styleTituloConcepto.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloConcepto.SetFont(oFontTituloConcepto)

            styleTituloDescripcion.FillBackgroundColor = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            styleTituloDescripcion.Alignment = NPOIV2.SS.UserModel.HorizontalAlignment.LEFT
            styleTituloDescripcion.DataFormat = NPOIV2.HSSF.Util.HSSFColor.BLACK.index
            'styleTituloDescripcion.WrapText = True
            styleTituloDescripcion.VerticalAlignment = VerticalAlignment.CENTER
            styleTituloDescripcion.SetFont(oFontTituloDescripcion)
            '===============================

            Dim rowActual As Integer = 2
            '===============Titulo
            objWorkSheet.CreateRow(rowActual).Height = 100 * 2
            '===========================

            '===========Titulo es centrado segun tamaño de grilla===========
            If Not strTitulo Is Nothing Then
                objWorkSheet.CreateRow(rowActual + 1)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleCab
                Next
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strTitulo)
                objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 0, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            End If
            '==============Verificamos Filtro a utilizar==================

            For intCont As Integer = 0 To olstrFiltros.Count - 1
                Dim strDescripcionTitulo As String()
                strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                objWorkSheet.CreateRow(rowActual + 1)
                objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTituloConcepto
                objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0) & "   ")

                For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(x).CellStyle = styleTituloDescripcion
                Next

                ' objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloDescripcion
                objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))
                objWorkSheet.AddMergedRegion(New NPOIV2.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count - 1))
                rowActual += 1
            Next
            objWorkSheet.CreateRow(rowActual + 1)
            rowActual += 2

            objWorkSheet.AutoSizeColumn(0, True)

        Next
        'Abrimos el documento
        '--------------------------------------
        Dim file As New FileStream(archivoReporte, FileMode.Create)
        objWorkBook.Write(file)
        file.Close()
        AbrirDocumento(archivoReporte)
    End Sub

#End Region
    'CSR-HUNDRED-REPUESTOS-ON-SIDE-PIURA-FIN


    'Codigo brindado por:Jose Moran Cardenas'
    Public Shared Sub ExportExcel_Formato03(ByVal oDs As DataSet, ByVal strTitulo As String, ByVal olstrFiltros As List(Of String), Optional ByVal listSuma As Hashtable = Nothing)
        Try

            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()

            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For intTable As Integer = 0 To oDs.Tables.Count - 1

                objWorkSheet = objWorkBook.CreateSheet(oDs.Tables(intTable).TableName)
                'objWorkSheet.CreateRow(3) 'JM

                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloConcepto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTituloDescripcion As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim stylePorcentaje As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim styleDinamico As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165

                Dim oFontFiltroResumen As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltroResumen.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltroResumen.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontFiltroResumen.FontHeight = 165


                Dim oFontlink As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontlink.FontName = "Wingdings"
                oFontlink.Underline = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontlink.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontlink.FontHeight = 250


                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFont.FontHeight = 170

                Dim oFontTituloConcepto As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloConcepto.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloConcepto.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTituloConcepto.FontHeight = 190

                Dim oFontTituloDescripcion As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTituloDescripcion.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTituloDescripcion.Boldweight = NPOI.SS.UserModel.FontBoldWeight.NORMAL
                oFontTituloDescripcion.FontHeight = 170

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFiltroResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFiltroResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFiltroResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltroResumen.BorderRight = CellBorderType.THIN
                styleFiltroResumen.BorderBottom = CellBorderType.THIN
                styleFiltroResumen.BorderLeft = CellBorderType.THIN
                styleFiltroResumen.BorderTop = CellBorderType.THIN
                styleFiltroResumen.SetFont(oFontFiltroResumen)
                styleFiltroResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltroResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER
                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)

                stylePorcentaje.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                stylePorcentaje.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                stylePorcentaje.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%")
                stylePorcentaje.FillPattern = FillPatternType.SOLID_FOREGROUND
                stylePorcentaje.BorderRight = CellBorderType.THIN
                stylePorcentaje.BorderBottom = CellBorderType.THIN
                stylePorcentaje.BorderLeft = CellBorderType.THIN
                stylePorcentaje.BorderTop = CellBorderType.THIN
                stylePorcentaje.SetFont(oFontFiltro)

                styleTituloConcepto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloConcepto.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloConcepto.SetFont(oFontTituloConcepto)

                styleTituloDescripcion.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTituloDescripcion.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTituloDescripcion.SetFont(oFontTituloDescripcion)

                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN

                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoTotal As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoTotal.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoTotal.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoTotal.BorderRight = CellBorderType.THIN
                styleMontoTotal.BorderBottom = CellBorderType.THIN
                styleMontoTotal.BorderLeft = CellBorderType.THIN
                styleMontoTotal.BorderTop = CellBorderType.THIN
                styleMontoTotal.SetFont(oFontFiltroResumen)
                styleMontoTotal.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoTotal.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleMontoResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMontoResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMontoResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleMontoResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMontoResumen.BorderRight = CellBorderType.THIN
                styleMontoResumen.BorderBottom = CellBorderType.THIN
                styleMontoResumen.BorderLeft = CellBorderType.THIN
                styleMontoResumen.BorderTop = CellBorderType.THIN
                styleMontoResumen.SetFont(oFontFiltroResumen)
                styleMontoResumen.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMontoResumen.FillPattern = FillPatternType.SOLID_FOREGROUND



                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim styleFechaResumen As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFechaResumen.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFechaResumen.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_25_PERCENT.index
                styleFechaResumen.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFechaResumen.BorderRight = CellBorderType.THIN
                styleFechaResumen.BorderBottom = CellBorderType.THIN
                styleFechaResumen.BorderLeft = CellBorderType.THIN
                styleFechaResumen.BorderTop = CellBorderType.THIN
                styleFechaResumen.SetFont(oFontFiltroResumen)
                styleFechaResumen.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFechaResumen.FillPattern = FillPatternType.SOLID_FOREGROUND


                Dim styleLink As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleLink.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleLink.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleLink.BorderRight = CellBorderType.THIN
                styleLink.BorderBottom = CellBorderType.THIN
                styleLink.BorderLeft = CellBorderType.THIN
                styleLink.BorderTop = CellBorderType.THIN
                styleLink.SetFont(oFontlink)
                styleLink.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleLink.FillPattern = FillPatternType.SOLID_FOREGROUND

                Dim rowActual As Integer = 0 '2   'JM
                '===============Titulo
                'objWorkSheet.CreateRow(rowActual).Height = 100 * 2 'JM
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                If Not strTitulo Is Nothing Then
                    objWorkSheet.CreateRow(rowActual + 1)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                            Continue For
                        End If
                        objWorkSheet.GetRow(rowActual + 1).CreateCell(x + 1).CellStyle = styleCab
                    Next
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strTitulo)
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 1, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1

                End If
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To olstrFiltros.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(olstrFiltros.Item(intCont), "\n")
                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = styleTituloConcepto
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(0))
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(2).CellStyle = styleTituloDescripcion
                    If intTable = 0 Then
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    ElseIf intTable <> 0 And intCont = 1 Then
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(oDs.Tables(intTable).TableName)
                    Else
                        objWorkSheet.GetRow(rowActual + 1).GetCell(2).SetCellValue(strDescripcionTitulo(1))
                    End If
                    objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual + 1, 2, rowActual + 1, oDs.Tables(intTable).Columns.Count))
                    rowActual += 1
                Next

                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    If oDs.Tables(intTable).Columns(x).ColumnName <> "ESTADOOP" Then
                        '===Estilo===
                        'objWorkSheet.GetRow(rowActual).CreateCell(x + 1).CellStyle = style
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = Replace(oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim(), Chr(34), "")
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length > 1 Then
                            flgCabDoble = True
                        End If
                        'objWorkSheet.GetRow(rowActual).GetCell(x + 1).SetCellValue(valorCabDoble(0))
                        objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
                    End If
                Next


                rowActual = rowActual + 1
                Dim intRepite As Integer = 0
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    'For x As Integer = 1 To oDs.Tables(intTable).Columns.Count - 1  'JM
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            'If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas + 1).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length > 1 Then

                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next

                    rowActual = rowActual + 1

                    '============================================================================
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If x < oDs.Tables(intTable).Columns.Count - 1 Then
                        intRepite = 0
                        For intColumnas As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(intColumnas).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue Then
                                intRepite = intRepite + 1
                            End If
                        Next
                        If intRepite > 1 Then
                            intRepite = intRepite - 1
                            Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                            '===Union===
                            Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + intRepite)
                            objWorkSheet.AddMergedRegion(Region)
                            x = x + intRepite
                        End If
                        ' End If
                    Next

                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = oDs.Tables(intTable).Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 3 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(2))
                        End If
                    Next
                    rowActual = rowActual + 1

                End If

                '===================Se Carga El Detalle============================
                For i As Integer = 0 To oDs.Tables(intTable).Rows.Count - 1

                    objWorkSheet.CreateRow(i + rowActual)
                    For x As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                        'If oDs.Tables(intTable).Columns(x).ColumnName = "ESTADOOP" Then
                        'Continue For
                        Dim celda As String = oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim()
                        Dim TypoDato As String = oDs.Tables(intTable).Columns.Item(x).Caption
                        If TypoDato = oDs.Tables(intTable).Columns.Item(x).ColumnName.ToString Then
                            TypoDato = oDs.Tables(intTable).Columns.Item(x).DataType.ToString
                        End If

                        'If oDs.Tables(intTable).Rows(i).Item(0).ToString.Trim.Equals("-1") Then
                        'objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                        'objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = style
                        Try
                            Select Case TypoDato
                                Case "System.Decimal", "System.Double", "Decimal"

                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                    If celda Is "" Then

                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                    Else

                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(Val(celda)))
                                    End If
                                Case "System.Int32", "System.Integer", "Numero"
                                    If celda Is "" Then

                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue("")
                                    Else

                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToInt64(Val(celda)))
                                    End If
                                Case "System.Date", "Fecha"

                                    If IsDate(celda) Then
                                        celda = String.Format("{0:d}", CDate(celda))

                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                                    Else

                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    End If



                                Case "System.DateTime", "FechaHora"
                                    celda = String.Format("{0:hh:mm:ss tt}", CDate(celda))

                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x + 1).CellStyle = styleFecha
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x + 1).SetCellValue(celda)
                                Case Else
                                    If celda Is DBNull.Value Then

                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue("")
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    Else

                                        objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(oDs.Tables(intTable).Rows(i).Item(x).ToString().Trim())
                                        objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                                    End If

                            End Select
                        Catch ex As Exception

                        End Try


                    Next
                Next

                ' ''Suma los registros indicados
                'Dim strFormula As String
                'objWorkSheet.CreateRow(rowActual + oDs.Tables(intTable).Rows.Count)
                'If Not listSuma Is Nothing Then

                '    For Each items As DictionaryEntry In listSuma
                '        If Math.Ceiling(items.Key) = (intTable + 2) Then
                '            strFormula = "SUM(" & LetraNumero_NPOI(items.Value) & (olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ":" & LetraNumero_NPOI(items.Value) & (oDs.Tables(intTable).Rows.Count + olstrFiltros.Count + IIf(flgCabDoble, 2, 0) + 6).ToString & ")"
                '            If oDs.Tables(intTable).Rows.Count = 0 Then strFormula = "0"
                '            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).CreateCell(items.Value - 1).CellStyle = styleMontoTotal
                '            objWorkSheet.GetRow(rowActual + oDs.Tables(intTable).Rows.Count).GetCell(items.Value - 1).CellFormula = (strFormula)
                '        End If
                '    Next
                'End If

                '===================================================================
                For z As Integer = 0 To oDs.Tables(intTable).Columns.Count - 1
                    objWorkSheet.AutoSizeColumn(z, True)
                Next

                '============================================logo=================================
                ''create the anchor
                'Dim anchor As HSSFClientAnchor
                '' segun la coordenadas ->HSSFClientAnchor(0, 0, 0, 0, x, y, 0, 0)
                'anchor = New HSSFClientAnchor(0, 0, 0, 0, 1, 0, 0, 0)
                'If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                '    'load the picture and get the picture index in the workbook
                '    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                '    picture.Resize(0.5)
                'End If
                'If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                '    'load the picture and get the picture index in the workbook
                '    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                '    'Reset the image to the original size.
                '    picture.Resize(0.5)
                'End If
                ''============================================logo=================================

            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
    Public Shared Function RowToDatatable(ByVal drSelect As DataRow(), ByVal tbl As DataTable) As DataTable

        Dim dt As New DataTable
        dt = tbl.Clone
        For Each row As DataRow In drSelect
            dt.ImportRow(row)
        Next
        Return dt
    End Function
    Public Shared Sub ExportExcel_ConTitulos2(ByVal objListdt As List(Of DataTable), ByVal title As List(Of String), Optional ByVal filtro As Hashtable = Nothing)
        Try
            Dim path As String = Application.StartupPath.ToString
            Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls" 'xml
            Dim fs As New IO.FileStream(archivo, IO.FileMode.Create)

            Dim objWorkBook As New HSSFWorkbook()
            Dim objWorkSheet As New HSSFSheet(objWorkBook)
            For Each odtg As DataTable In objListdt
                objWorkSheet = objWorkBook.CreateSheet(odtg.TableName)
                objWorkSheet.CreateRow(3)
                '=============Style======================
                Dim styleFiltro As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleCab As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim style As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                Dim styleTitulo As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()

                Dim oFontFiltro As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontFiltro.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontFiltro.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontFiltro.FontHeight = 165

                Dim oFontCab As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontCab.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontCab.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFontCab.FontHeight = 220

                Dim oFont As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFont.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFont.Boldweight = NPOI.HSSF.Util.HSSFColor.BLACK.index
                oFont.FontHeight = 170

                Dim oFontTitulo As NPOI.SS.UserModel.Font = objWorkBook.CreateFont
                oFontTitulo.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index '.HSSFColor.BLACK.index '.HSSF.Util.HSSFColor.WHITE.index
                oFontTitulo.Boldweight = NPOI.SS.UserModel.FontBoldWeight.BOLD
                oFontTitulo.FontHeight = 190

                styleFiltro.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFiltro.BorderRight = CellBorderType.THIN
                styleFiltro.BorderBottom = CellBorderType.THIN
                styleFiltro.BorderLeft = CellBorderType.THIN
                styleFiltro.BorderTop = CellBorderType.THIN
                styleFiltro.SetFont(oFontFiltro)
                styleFiltro.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFiltro.FillPattern = FillPatternType.SOLID_FOREGROUND

                styleCab.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleCab.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleCab.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleCab.BorderRight = CellBorderType.THIN
                styleCab.BorderBottom = CellBorderType.THIN
                styleCab.BorderLeft = CellBorderType.THIN
                styleCab.BorderTop = CellBorderType.THIN
                styleCab.SetFont(oFontCab)
                styleCab.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleCab.FillPattern = FillPatternType.SOLID_FOREGROUND

                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER

                style.FillPattern = FillPatternType.SOLID_FOREGROUND
                style.BorderRight = CellBorderType.THIN
                style.BorderBottom = CellBorderType.THIN
                style.BorderLeft = CellBorderType.THIN
                style.BorderTop = CellBorderType.THIN
                style.SetFont(oFont)
                style.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                style.FillPattern = FillPatternType.SOLID_FOREGROUND

                'styleTitulo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index
                styleTitulo.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTitulo.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT
                styleTitulo.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleTitulo.SetFont(oFontTitulo)
                '===============================
                Dim styleNumber As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleNumber.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleNumber.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleNumber.BorderRight = CellBorderType.THIN
                styleNumber.BorderBottom = CellBorderType.THIN
                styleNumber.BorderLeft = CellBorderType.THIN
                styleNumber.BorderTop = CellBorderType.THIN

                styleNumber.SetFont(oFontFiltro)
                styleNumber.DataFormat = HSSFDataFormat.GetBuiltinFormat("#.##0,00")
                styleNumber.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleMonto As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleMonto.Alignment = NPOI.SS.UserModel.HorizontalAlignment.RIGHT
                styleMonto.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleMonto.BorderRight = CellBorderType.THIN
                styleMonto.BorderBottom = CellBorderType.THIN
                styleMonto.BorderLeft = CellBorderType.THIN
                styleMonto.BorderTop = CellBorderType.THIN
                styleMonto.SetFont(oFontFiltro)
                styleMonto.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00")
                styleMonto.FillPattern = FillPatternType.SOLID_FOREGROUND
                '===============================
                Dim styleFecha As NPOI.SS.UserModel.CellStyle = objWorkBook.CreateCellStyle()
                styleFecha.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER
                styleFecha.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.WHITE.index
                styleFecha.BorderRight = CellBorderType.THIN
                styleFecha.BorderBottom = CellBorderType.THIN
                styleFecha.BorderLeft = CellBorderType.THIN
                styleFecha.BorderTop = CellBorderType.THIN
                styleFecha.SetFont(oFontFiltro)
                styleFecha.DataFormat = NPOI.HSSF.Util.HSSFColor.BLACK.index
                styleFecha.FillPattern = FillPatternType.SOLID_FOREGROUND
                '============================================
                Dim patriarch As HSSFPatriarch = DirectCast(objWorkSheet.CreateDrawingPatriarch(), HSSFPatriarch)
                'create the anchor
                Dim anchor As HSSFClientAnchor
                anchor = New HSSFClientAnchor(0, 0, 0, 50, 0, 0, _
                 0, 0)
                If IO.File.Exists(Application.StartupPath & " \Resources\logo.png") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Resources\logo.png", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    ' picture.Resize()
                    picture.Resize(0.5)
                End If
                If IO.File.Exists(Application.StartupPath & "\Imagenes\logo.jpg") Then
                    'load the picture and get the picture index in the workbook
                    Dim picture As HSSFPicture = DirectCast(patriarch.CreatePicture(anchor, LoadImage(Application.StartupPath & " \Imagenes\logo.jpg", objWorkBook)), HSSFPicture)
                    'Reset the image to the original size.
                    picture.Resize(0.5)
                End If
                Dim rowActual As Integer = 4
                '===============Titulo
                objWorkSheet.CreateRow(rowActual).Height = 100 * 2
                '===========================

                '===========Titulo es centrado segun tamaño de grilla===========
                'objWorkSheet.GetRow(rowActual).GetCell(0).SetCellValue(title)
                'objWorkSheet.AddMergedRegion(New NPOI.SS.Util.Region(rowActual, 0, rowActual, odtg.Columns.Count - 1))
                '==============Verificamos Filtro a utilizar==================

                For intCont As Integer = 0 To title.Count - 1
                    Dim strDescripcionTitulo As String()
                    strDescripcionTitulo = Split(title.Item(intCont), "\n")

                    objWorkSheet.CreateRow(rowActual + 1)
                    objWorkSheet.GetRow(rowActual + 1).CreateCell(0).CellStyle = styleTitulo
                    objWorkSheet.GetRow(rowActual + 1).GetCell(0).SetCellValue(strDescripcionTitulo(0))

                    objWorkSheet.GetRow(rowActual + 1).CreateCell(1)
                    ' objWorkSheet.GetRow(rowActual + 1).CreateCell(1).CellStyle = style
                    objWorkSheet.GetRow(rowActual + 1).GetCell(1).SetCellValue(strDescripcionTitulo(1))

                    rowActual += 1
                Next
                objWorkSheet.CreateRow(rowActual + 1)
                rowActual += 1
                If filtro Is Nothing Then
                    rowActual = rowActual + 1
                Else
                    Dim x As Integer = 0
                    For Each items As DictionaryEntry In filtro
                        x = x + 1
                        objWorkSheet.CreateRow(rowActual + x)
                        objWorkSheet.GetRow(rowActual + x).CreateCell(0).CellStyle = style
                        objWorkSheet.GetRow(rowActual + x).GetCell(0).SetCellValue(items.Key.ToString.Trim)

                        objWorkSheet.GetRow(rowActual + x).CreateCell(1).CellStyle = styleFiltro
                        objWorkSheet.GetRow(rowActual + x).GetCell(1).SetCellValue(items.Value.ToString.Trim)

                    Next
                    rowActual = rowActual + filtro.Count + 1
                End If
                '===================Se cargan Las Cabeceras=====================
                Dim flgCabDoble As Boolean = False
                objWorkSheet.CreateRow(rowActual)
                For x As Integer = 0 To odtg.Columns.Count - 1
                    '===Estilo===
                    objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                    '===Valores===
                    Dim valorCab As String = odtg.Columns(x).ColumnName.ToString().Trim()
                    Dim valorCabDoble As String()
                    valorCabDoble = Split(valorCab, "\n")
                    If valorCabDoble.Length = 2 Then
                        flgCabDoble = True
                    Else
                        flgCabDoble = False
                    End If
                    objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(0))
                Next
                rowActual = rowActual + 1
                If flgCabDoble = True Then
                    '==========Limpiamos los Cells Repetidos en el Row Anterior=========
                    For x As Integer = 0 To odtg.Columns.Count - 1
                        If x < odtg.Columns.Count - 1 Then
                            If objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue = objWorkSheet.GetRow(rowActual - 1).GetCell(x + 1).StringCellValue Then
                                Dim valorTemp = objWorkSheet.GetRow(rowActual - 1).GetCell(x).StringCellValue
                                objWorkSheet.GetRow(rowActual - 1).GetCell(x + 1).SetCellValue("")
                                '===Union===
                                Dim Region As New NPOI.SS.Util.Region(rowActual - 1, x, rowActual - 1, x + 1)
                                objWorkSheet.AddMergedRegion(Region)
                            End If
                        End If
                    Next
                    '===================================================================
                    objWorkSheet.CreateRow(rowActual)
                    For x As Integer = 0 To odtg.Columns.Count - 1
                        '===Estilo===
                        objWorkSheet.GetRow(rowActual).CreateCell(x).CellStyle = style
                        '===Valores===
                        Dim valorCab As String = odtg.Columns(x).ColumnName.ToString().Trim()
                        Dim valorCabDoble As String()
                        valorCabDoble = Split(valorCab, "\n")
                        If valorCabDoble.Length = 2 Then
                            objWorkSheet.GetRow(rowActual).GetCell(x).SetCellValue(valorCabDoble(1))
                        End If
                    Next
                    rowActual = rowActual + 1
                End If
                '===================Se Carga El Detalle============================
                For i As Integer = 0 To odtg.Rows.Count - 1
                    objWorkSheet.CreateRow(i + rowActual)
                    For x As Integer = 0 To odtg.Columns.Count - 1
                        Dim celda As String = odtg.Rows(i).Item(x).ToString().Trim()
                        If IsNumeric(celda) Then
                            If Left(celda, 1) <> "0" Then
                                If celda.Contains(".") Then
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleMonto
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda))
                                Else
                                    objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleNumber
                                    objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(Convert.ToDouble(celda))
                                End If
                            Else
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFiltro
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(odtg.Rows(i).Item(x).ToString().Trim())
                            End If
                        Else
                            If IsDate(celda) Then
                                celda = String.Format("{0:d}", CDate(celda))
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).CellStyle = styleFecha
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).SetCellValue(celda)
                            Else
                                objWorkSheet.GetRow(i + rowActual).CreateCell(x).SetCellValue(odtg.Rows(i).Item(x).ToString().Trim())
                                objWorkSheet.GetRow(i + rowActual).GetCell(x).CellStyle = styleFiltro
                            End If
                        End If

                    Next
                Next


                For z As Integer = 0 To odtg.Columns.Count - 1
                    objWorkSheet.AutoSizeColumn(z, True)
                Next
            Next
            objWorkBook.Write(fs)
            fs.Close()
            AbrirDocumento(archivo)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub


End Class

