Imports SOLMIN_ST.ENTIDADES
Imports SOLMIN_ST.NEGOCIO
Imports SOLMIN_ST.NEGOCIO.seguimiento_wap
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports System
Imports System.Text
Imports System.IO
Imports System.Reflection
Imports CrystalDecisions.Shared


Public Class frmConsultarSeguimientoWAP_1

    Private Sub frmConsultarSeguimientoWAP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cargar_Combos()
        HeaderGroupFiltro.ValuesPrimary.Heading = "Opciones de filtro"
    End Sub

    Public Sub Cargar_Combos()
        Dim objSeguimientoWap As New SeguimientoWAP_BLL
        With Me.cboGrupoSeguimiento
            .DataSource = objSeguimientoWap.Listar_Grupo_Seguimiento_Wap()
            .KeyField = "NGSGWP"
            .ValueField = "TOBS"
            .DataBind()
        End With
    End Sub

    Private Sub btnProcesarReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesarReporte.Click
        Me.Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow_V3()
    End Sub

    Private Sub Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow_V3()
        If Me.cboGrupoSeguimiento.NoHayCodigo = True Then
            HelpClass.MsgBox("Seleccione un Grupo de Seguimiento", MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim lstr_FechaInicio As Integer = Convert.ToInt32(HelpClass.CtypeDate(Me.dtpFechaInicio.Value))
        Dim lstr_FechaFin As Integer = Convert.ToInt32(HelpClass.CtypeDate(Me.dtpFechaFin.Value))

        Dim lstr_CSGWP As String = ""
        If Me.cboGrupoSeguimiento.Codigo.Trim = "02" Then
            lstr_CSGWP = "04"
        Else
            lstr_CSGWP = "01"
        End If

        Dim dtbArrDataTables As DataTable
        dtbArrDataTables = (New SeguimientoWAP_BLL).Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow_V3_1(lstr_FechaInicio, lstr_FechaFin, Me.txtPlaca.Text.Trim, Me.cboGrupoSeguimiento.Codigo.Trim, lstr_CSGWP)

        gwdDatos.DataSource = Nothing
        gwdDatos.DataSource = dtbArrDataTables


        Me.gwdDatos.Columns("NCOREG").Frozen = True
        Me.gwdDatos.Columns("NCOREG").HeaderText = "CICLO"
        Me.gwdDatos.Columns("NCOREG").Width = 40
        Me.gwdDatos.Columns("SESTRG").Frozen = True
        Me.gwdDatos.Columns("SESTRG").HeaderText = "ESTADO"
        Me.gwdDatos.Columns("SESTRG").Width = 40
        Me.gwdDatos.Columns("FCHCRT").Frozen = True
        Me.gwdDatos.Columns("FCHCRT").HeaderText = "FECHA CREACIÓN"
        Me.gwdDatos.Columns("HRACRT").Frozen = True
        Me.gwdDatos.Columns("HRACRT").HeaderText = "HORA CREACIÓN"
        Me.gwdDatos.Columns("NPLCTR").Frozen = True
        Me.gwdDatos.Columns("NPLCTR").HeaderText = "PLACA"
        Me.gwdDatos.Columns("TIEMPO ACUMULADO").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Me.gwdDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        For Each column As DataGridViewColumn In gwdDatos.Columns
            If column.HeaderText.StartsWith("TIEMPO") Then
                column.DefaultCellStyle.BackColor = Color.YellowGreen
                column.DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point, Nothing)
            End If
        Next

        For r As Integer = 0 To gwdDatos.Rows.Count - 1
            For c As Integer = 0 To gwdDatos.Columns.Count - 1
                If gwdDatos.Columns(c).HeaderText.StartsWith("TIEMPO") AndAlso _
                        gwdDatos.Rows(r).Cells(c).Value IsNot DBNull.Value AndAlso _
                        gwdDatos.Rows(r).Cells(c).Value.StartsWith("-") Then
                    gwdDatos.Rows(r).Cells(c).Style.BackColor = Color.Red
                End If
            Next
        Next

        'asigna color Orange a la fila de sumatoria de Deltas
        If gwdDatos.RowCount > 0 Then
            gwdDatos.Rows(gwdDatos.RowCount - 1).DefaultCellStyle.BackColor = Color.Orange
        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If Me.cboGrupoSeguimiento.NoHayCodigo = True Then
            HelpClass.MsgBox("Seleccione un Grupo de Seguimiento", MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim objReporte As New ReportClass
        Dim objPrintForm As New PrintForm
        If Me.gwdDatos.RowCount > 0 Then
            Me.Imprimir_Indicadores()
        End If
        Exit Sub

    End Sub

    Private Sub Imprimir_Indicadores()
        Dim objDataTable As New DataTable
        Dim objDataSet As New DataSet
        Dim objPrintForm As New PrintForm
        Dim objReporte As New rptIndicadorWAP
        Dim lint_Valor As Integer = 1
        Dim lint_Index As Integer = 1

        objDataTable.TableName = "IndicadoresWAP"
        objDataTable.Columns.Add("NCOACC")
        objDataTable.Columns.Add("TOBS")
        objDataTable.Columns.Add("TOTAL", Type.GetType("System.Int32"))
        For lint_Count As Integer = 5 To Me.gwdDatos.ColumnCount - 3
            Dim row As DataRow = objDataTable.NewRow

            If gwdDatos.Columns(lint_Count).HeaderText.StartsWith("TIEMPO") Then
                row(0) = "∆" & lint_Valor
                lint_Valor = lint_Valor + 1
            Else
                row(0) = lint_Index ' Me.gwdDatos.Columns(lint_Count).Name
                lint_Index = lint_Index + 1
            End If

            If Me.gwdDatos.Columns(lint_Count).Name.Contains("TIEMPO") = True Then
                'row(1) = "∆" & lint_Valor - 1 & " = (" & Me.gwdDatos.Columns(lint_Count - 2).Name & " - " & Me.gwdDatos.Columns(lint_Count - 1).Name & ")"
                row(1) = "∆" & lint_Valor - 1 & " = (" & lint_Index - 2 & " - " & lint_Index - 1 & ")"
            Else
                row(1) = Me.gwdDatos.Columns(lint_Count).HeaderText
            End If

            Dim Tiempo As String = String.Empty
            Dim enMin As Double = 0
            Tiempo = Me.gwdDatos.Item(lint_Count, Me.gwdDatos.RowCount - 1).Value.ToString()
            If Tiempo <> "" Then
                If Tiempo.Length < 10 And Tiempo.IndexOf(".") = -1 Then
                    If Tiempo.Substring(0, 1) = "-" Then
                        Dim hor As Integer = CInt(Tiempo.Substring(1, 2))
                        Dim min As Integer = CInt(Tiempo.Substring(4, 2))
                        Dim seg As Integer = CInt(Tiempo.Substring(7, 2))
                        enMin = (hor * -1) * 60 + (min * -1) + (seg * -1) / 60
                    Else
                        Dim hor As Integer = CInt(Tiempo.Substring(0, 2))
                        Dim min As Integer = CInt(Tiempo.Substring(3, 2))
                        Dim seg As Integer = CInt(Tiempo.Substring(6, 2))
                        enMin = hor * 60 + min + seg / 60
                    End If
                ElseIf Tiempo.Length > 9 And Tiempo.IndexOf(".") <> -1 Then
                    Dim punto As Integer = Tiempo.IndexOf(".")
                    If Tiempo.Substring(0, 1) = "-" Then
                        Dim dia As Integer = CInt(Tiempo.Substring(1, punto))
                        Dim hor As Integer = CInt(Tiempo.Substring(punto + 1, 2))
                        Dim min As Integer = CInt(Tiempo.Substring(punto + 4, 2))
                        Dim seg As Integer = CInt(Tiempo.Substring(punto + 7, 2))
                        enMin = ((dia * -1) * 24 * 60) + ((hor * -1) * 60) + (min * -1) + ((seg * -1) / 60)
                    Else
                        Dim dia As Integer = CInt(Tiempo.Substring(0, punto))
                        Dim hor As Integer = CInt(Tiempo.Substring(punto + 1, 2))
                        Dim min As Integer = CInt(Tiempo.Substring(punto + 4, 2))
                        Dim seg As Integer = CInt(Tiempo.Substring(punto + 7, 2))
                        enMin = (dia * 24 * 60) + (hor * 60) + min + (seg / 60)
                    End If
                End If
                row(2) = enMin
            Else
                row(2) = 0
            End If



            objDataTable.Rows.Add(row)
        Next
        objReporte.SetDataSource(objDataTable)
        HelpClass.CrystalReportTextObject(objReporte, "lblFechaInicio", Me.dtpFechaInicio.Value.ToShortDateString())
        HelpClass.CrystalReportTextObject(objReporte, "lblFechaFinal", Me.dtpFechaFin.Value.ToShortDateString())
        HelpClass.CrystalReportTextObject(objReporte, "lblContenido", IIf(Me.txtPlaca.TextLength > 0, "Placa del Tracto : " & Me.txtPlaca.Text, "Toda la Flota"))
        HelpClass.CrystalReportTextObject(objReporte, "lblTipoSeguimiento", Me.cboGrupoSeguimiento.Descripcion.Trim)
        HelpClass.CrystalReportTextObject(objReporte, "lblUsuario", USUARIO)
        objPrintForm.ShowForm(objReporte, Me)
    End Sub

    Private Sub btnExportarExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportarExcel.Click
        If gwdDatos.Rows.Count > 0 Then
            ExportarAExcel(gwdDatos)
        End If
    End Sub

    Private Sub ExportarAExcel(ByVal grilla As DataGridView)
        'averiguando si es que existe el directorio a exportar
        Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tempo\"

        If IO.Directory.Exists(path) = False Then
            IO.Directory.CreateDirectory(path)
        End If
        Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls"
        Dim xls As New IO.StreamWriter(archivo, True, Encoding.Default)

        xls.WriteLine("<TABLE border='1'>")
        xls.WriteLine("<tr>")
        For columna As Integer = 0 To grilla.Columns.Count - 1
            xls.Write("<td style='background-color:#FEF7DB'>" & grilla.Columns.Item(columna).HeaderText.ToString() & "</td>")
        Next
        xls.WriteLine("</tr>")
        xls.WriteLine("<tr>")
        For columna As Integer = 0 To grilla.Columns.Count - 1
            xls.Write("<td >" & "</td>")
        Next
        xls.WriteLine("</tr>")
        For fila As Integer = 0 To grilla.Rows.Count - 1
            xls.WriteLine("<tr>")
            For columna As Integer = 0 To grilla.Columns.Count - 1
                If grilla.Item(columna, fila).Value.ToString.Trim.Length > 1 AndAlso (grilla.Item(columna, fila).Value.ToString.Trim.Substring(grilla.Item(columna, fila).Value.ToString.Trim.Length - 2, 2).Trim.Equals("pm") OrElse grilla.Item(columna, fila).Value.ToString.Trim.Substring(grilla.Item(columna, fila).Value.ToString.Trim.Length - 2, 2).Trim.Equals("am")) Then
                    xls.Write("<td>" & grilla.Item(columna, fila).Value.ToString.Trim.Substring(0, grilla.Item(columna, fila).Value.ToString.Trim.Length - 2).Trim & "</td>")
                Else
                    xls.Write("<td>" & grilla.Item(columna, fila).Value.ToString.Trim & "</td>")
                End If
            Next
            xls.WriteLine("</tr>")
        Next
        xls.WriteLine("</TABLE>")
        xls.Flush()
        xls.Close()
        xls.Dispose()
        Help.ShowHelp(Me, archivo)
    End Sub

    Private Sub btnReporterSeguimiento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReporterSeguimiento.Click
        If Not Me.cboGrupoSeguimiento.Codigo.Trim.Equals("02") Then
            HelpClass.MsgBox("Seleccione Grupo de Seguimiento: Industrias", MessageBoxIcon.Information)
            Exit Sub
        End If
        If gwdDatos.Rows.Count > 0 Then
            Dim objDataTable As DataTable = gwdDatos.DataSource
            Dim objPrintForm As New PrintForm
            Dim objReporte As New rptSeguimientoWapLocal
            objReporte.SetDataSource(objDataTable)
            HelpClass.CrystalReportTextObject(objReporte, "lblTipoSeguimiento", Me.cboGrupoSeguimiento.Descripcion)
            HelpClass.CrystalReportTextObject(objReporte, "lblUsuario", USUARIO)
            objPrintForm.ShowForm(objReporte, Me)

            '-------------------
            '-------------------
            'Dim objDataTable As DataTable = gwdDatos.DataSource
            'Dim objPrintForm As New PrintForm
            'Dim objReporte As New rptSeguimientoWapLocalDinamico
            'If objDataTable.Rows.Count > 0 Then
            '    For column As Integer = 0 To objDataTable.Columns.Count - 1
            '        Dim Array(objDataTable.Rows.Count - 1) As String
            '        For row As Integer = 0 To objDataTable.Rows.Count - 1
            '            Dim valor As String = objDataTable.Rows(row)(column).ToString().Trim()
            '            Array(row) = IIf(valor = String.Empty, "   ", valor) '    valor ' 
            '        Next
            '        objReporte.SetParameterValue("@Parametro" + column.ToString(), Array)
            '    Next
            'End If
            'HelpClass.CrystalReportTextObject(objReporte, "lblTipoSeguimiento", Me.cboGrupoSeguimiento.Descripcion)
            'HelpClass.CrystalReportTextObject(objReporte, "lblUsuario", USUARIO)
            'objPrintForm.ShowForm(objReporte, Me)
            '-------------------
            '-------------------
        Else
            MessageBox.Show("Aún no se a Procesado el Reporte.")
        End If
    End Sub

  Private Sub btnVerRegistroWAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerRegistroWAP.Click
    If gwdDatos.CurrentCellAddress.Y < 0 Then
      Exit Sub
    End If

    Dim NPLCUN As String = gwdDatos.Item("NPLCTR", gwdDatos.CurrentCellAddress.Y).Value.ToString.Trim()
    Dim NCOREG As String = gwdDatos.Item("NCOREG", gwdDatos.CurrentCellAddress.Y).Value.ToString.Trim()
    Dim SESTRG As String = gwdDatos.Item("SESTRG", gwdDatos.CurrentCellAddress.Y).Value.ToString.Trim()
    Dim NGSGWP As String = Me.cboGrupoSeguimiento.Codigo.Trim
    Dim f As New frmRegistroWAP(NPLCUN)

    f.NCOREG = NCOREG
    f.NGSGWP = NGSGWP
    f.SESTRG = SESTRG
    f.CCMPN = "EZ"
    f.ShowForm(Me)

    Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow_V3()
  End Sub

    Private Sub cboGrupoSeguimiento_Texto_KeyEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboGrupoSeguimiento.Texto_KeyEnter
        If Me.cboGrupoSeguimiento.NoHayCodigo = True OrElse Me.cboGrupoSeguimiento.Codigo.Trim.Equals("01") Then
            Me.btnReporterSeguimiento.Visible = False
            Me.Separator2.Visible = False
        Else
            Me.btnReporterSeguimiento.Visible = True
            Me.Separator2.Visible = True
        End If
    End Sub
End Class
