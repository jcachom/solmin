Public Class mUtilEstructuraST

    Public Enum TipoDato
        ALFANUMERICO
        NUMERICO
        FECHA
    End Enum
    Public Function EstructuraValidacionColumna() As DataTable
        Dim dtReglaValidacion As New DataTable
        dtReglaValidacion.Columns.Add("COLUMNA")
        dtReglaValidacion.Columns.Add("MAX_LONGITUD")
        dtReglaValidacion.Columns.Add("TIPO_DATO")
        dtReglaValidacion.Columns.Add("ENTEROS")
        dtReglaValidacion.Columns.Add("DECIMALES")
        dtReglaValidacion.Columns.Add("OBLIGATORIEDAD")
        dtReglaValidacion.Columns.Add("LISTA_VALORES")
        Return dtReglaValidacion
    End Function
    Public Function ValidarEstructuraColumna(dtListaRegla As DataTable, Columna As String, ValorCelda As String) As String

        Dim dr() As DataRow
        Dim pMAX_LONGITUD As Int64 = 0
        Dim pTIPO_DATO As String = ""
        Dim pENTEROS As Int64 = 0
        Dim pDECIMALES As Int64 = 0
        Dim pOBLIGATORIEDAD As String = ""
        Dim pLISTA_VALORES As String = ""
        Dim msg As String = ""
        Dim fecha As String = ""
        dr = dtListaRegla.Select("COLUMNA='" & Columna & "'")
        If dr.Length = 0 Then
            msg = "Regla validación no asignada a Col. [," & Columna & "]" & Chr(10)
            Return msg
        End If
        pTIPO_DATO = ("" & dr(0)("TIPO_DATO"))
        If pTIPO_DATO = "" Then
            msg = msg & " Tipo Dato no asignado," & Chr(10)
            Return msg
        End If

        pOBLIGATORIEDAD = ("" & dr(0)("OBLIGATORIEDAD"))
        If pOBLIGATORIEDAD = "S" And ValorCelda = "" Then
            msg = msg & " valor obligatorio," & Chr(10)
        End If

        Select Case pTIPO_DATO
            Case TipoDato.ALFANUMERICO.ToString
                pMAX_LONGITUD = Val("" & dr(0)("MAX_LONGITUD"))
                Dim LargoTexto As Int64 = ValorCelda.Length
                If LargoTexto > pMAX_LONGITUD Then
                    msg = msg & "Máximo caracteres " & pMAX_LONGITUD & "," & Chr(10)
                End If

            Case TipoDato.FECHA

                If ValorCelda <> "" Then
                    fecha = GetFecha(ValorCelda)
                    If Not IsDate(fecha) Then
                        msg = msg & " Fecha no válida(YYYYMMdd)," & Chr(10)
                    End If
                End If

            Case TipoDato.NUMERICO.ToString
                Dim ValorEntero As Int64 = 0
                Dim ParteDecimal As Decimal = 0
                Dim LargoEnteros As Int64 = 0
                Dim LargoDecimal As Int64 = 0
                pENTEROS = Val("" & dr(0)("ENTEROS"))
                pDECIMALES = Val("" & dr(0)("DECIMALES"))

                If ValorCelda <> "" Then
                    If Not IsNumeric(ValorCelda) Then
                        msg = msg & " Debe ser numérico," & Chr(10)
                    Else
                        ValorEntero = ValorCelda
                        Dim numValor As Decimal = ValorCelda
                        ParteDecimal = numValor - ValorEntero
                        LargoEnteros = ValorEntero.ToString.Length
                        If ParteDecimal > 0 Then
                            LargoDecimal = ParteDecimal.ToString.Length - 2
                        Else
                            LargoDecimal = 0
                        End If
                        If LargoEnteros > pENTEROS Then
                            msg = msg & "Max. enteros " & pENTEROS & "," & Chr(10)
                        End If
                        If LargoDecimal > pDECIMALES Then
                            msg = msg & "Max. decimales " & pDECIMALES & "," & Chr(10)
                        End If
                    End If
                End If


        End Select
        pLISTA_VALORES = ("" & dr(0)("LISTA_VALORES"))
        Dim listaCad() As String
        If pLISTA_VALORES <> "" And pOBLIGATORIEDAD = "S" Then
            Dim Encontrado As Boolean = False
            listaCad = pLISTA_VALORES.Split(",")
            For Each item As String In listaCad
                If item = ValorCelda Then
                    Encontrado = True
                    Exit For
                End If
            Next
            If Encontrado = False Then
                msg = msg & "Valor permitidos [ " & pLISTA_VALORES & "]" & Chr(10)
            End If
        End If
        If msg <> "" Then
            msg = "[" & Columna & "]:" & msg
        End If

        Return msg

    End Function

    Private Function GetFecha(ByVal xFecha As Object) As String
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

End Class
