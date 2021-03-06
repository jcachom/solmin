Imports SOLMIN_ST.ENTIDADES
Imports SOLMIN_ST.NEGOCIO
Imports SOLMIN_ST.NEGOCIO.seguimiento_wap
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports System
Imports System.Text
Imports System.IO
Imports System.Reflection

Public Class frmConsultarSeguimientoWAP
  Private _SelectedNodeIndex As Integer = -1
  Private _SelectedNodeText As String = ""
    Private _CCMPN As String = ""
    Public Property CCMPN() As String
        Get
            Return _CCMPN
        End Get
        Set(ByVal value As String)
            _CCMPN = value
        End Set
    End Property
    Private Sub frmConsultarSeguimientoWAP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Cargando los combos del formulario
        Cargar_Combos()
        HeaderGroupFiltro.ValuesPrimary.Heading = "Opciones de filtro"

    End Sub

    Public Sub Cargar_Combos()

        Dim objSeguimientoWap As New SeguimientoWAP_BLL

        With Me.cboRolWap
            .DataSource = objSeguimientoWap.Listar_Roles_Wap
            .ValueMember = "NCOROL"
            .DisplayMember = "TOBS"
            .SelectedIndex = 0
        End With

        With Me.txtUsuarioWap
            .DataSource = HelpClass.GetDataSetNative(objSeguimientoWap.Listar_Usuarios_Wap)
            .KeyField = "NROMOV"
            .ValueField = "TOBS"
            .DataBind()
        End With

        With Me.cboGrupoSeguimiento
            .DataSource = objSeguimientoWap.Listar_Grupo_Seguimiento_Wap()
            .KeyField = "NGSGWP"
            .ValueField = "TOBS"
            .DataBind()
        End With


    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
        btnProcesarReporte.Enabled = True
    End Sub

    Private Sub btnProcesarReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesarReporte.Click

        Me.Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow_V3()
    End Sub

    Private Sub Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow_V3()

    If Me.cboGrupoSeguimiento.NoHayCodigo = True Then
      HelpClass.MsgBox("Seleccione un grupo de Seguimiento", MessageBoxIcon.Information)
      Exit Sub
    End If

        Dim dtbArrDataTables(3) As DataTable
        Dim lstr_FechaInicio As String = HelpClass.CtypeDate(Me.dtpFechaInicio.Value).ToString.Trim
        Dim lstr_FechaFin As String = HelpClass.CtypeDate(Me.dtpFechaFin.Value).ToString.Trim

        Dim objLogica As New SeguimientoWAP_BLL
        dtbArrDataTables = objLogica.Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow_V3(lstr_FechaInicio, lstr_FechaFin, Me.txtPlaca.Text.Trim, Me.cboGrupoSeguimiento.Codigo)

        gwdDatos.DataSource = Nothing
        gwdDatos.DataSource = dtbArrDataTables(1)
        Me.gwdDatos.Columns(0).Frozen = True 'ciclo
        Me.gwdDatos.Columns(1).Frozen = True 'fecha creacion
        Me.gwdDatos.Columns(2).Frozen = True 'hora creacion
        Me.gwdDatos.Columns(3).Frozen = True 'placa
        Me.gwdDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        Me.gwdDatos.Columns(0).Width = 50

        'Pone el nombre a las columnas 
        gwdDatos.Columns(1).HeaderText = "FECHA CREACIÓN"
        gwdDatos.Columns(2).HeaderText = "HORA CREACIÓN"
        gwdDatos.Columns(3).HeaderText = "PLACA"

        'Pone el nombre a las columnas de eventos
        For intCont As Integer = 4 To gwdDatos.Columns.Count - 1
            For Each row As DataRow In dtbArrDataTables(0).Rows
                If row("NCOEVT").ToString.Trim.Equals(gwdDatos.Columns(intCont).Name) Then
                    gwdDatos.Columns(intCont).HeaderText = row("TNMCEV").ToString.Trim
                End If
            Next
        Next

        Dim objDtEventosCfgDeltaT As DataTable
        objDtEventosCfgDeltaT = dtbArrDataTables(2)

        'Se establece el nombre de la columna Delta
        For intCol As Integer = 0 To gwdDatos.Columns.Count - 1
            gwdDatos.Columns(intCol).SortMode = DataGridViewColumnSortMode.NotSortable

            If gwdDatos.Columns.Item(intCol).Name.StartsWith("Delta:") Then
                Dim lstr_colName As String = gwdDatos.Columns.Item(intCol).Name.ToString.Trim
                lstr_colName = lstr_colName.Replace("Delta:", "")
                Dim lstr_ev2 As String = (lstr_colName.Split("-"))(0)
                Dim lstr_ev1 As String = (lstr_colName.Split("-"))(1)

                For Each rowCfgDeltaEventos As DataRow In objDtEventosCfgDeltaT.Rows
                    If lstr_ev2.Equals(rowCfgDeltaEventos("NIVEL2").ToString.Trim) AndAlso lstr_ev1.Equals(rowCfgDeltaEventos("NIVEL1").ToString.Trim) Then
                        Me.gwdDatos.Columns(intCol).HeaderText = rowCfgDeltaEventos("TOBS").ToString.Trim
                    End If
                Next

                'asigna color y font
                gwdDatos.Columns(intCol).DefaultCellStyle.BackColor = Color.YellowGreen
                gwdDatos.Columns(intCol).DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point, Nothing)

            End If
        Next
        'asigna color Orange a la fila de sumatoria de Deltas
        gwdDatos.Rows(gwdDatos.RowCount - 1).DefaultCellStyle.BackColor = Color.Orange

        For r As Integer = 0 To gwdDatos.Rows.Count - 1
            For c As Integer = 0 To gwdDatos.Columns.Count - 1
                If gwdDatos.Columns(c).HeaderText.StartsWith("TIEMPO") AndAlso _
                        gwdDatos.Rows(r).Cells(c).Value IsNot DBNull.Value AndAlso _
                        gwdDatos.Rows(r).Cells(c).Value.StartsWith("-") Then
                    gwdDatos.Rows(r).Cells(c).Style.BackColor = Color.Red
                End If
            Next
        Next

    End Sub

    Private Sub Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow_V2()
        Dim dtbArrDataTables(3) As DataTable
        Dim lstr_FechaInicio As String = HelpClass.CtypeDate(Me.dtpFechaInicio.Value).ToString.Trim
        Dim lstr_FechaFin As String = HelpClass.CtypeDate(Me.dtpFechaFin.Value).ToString.Trim

        Dim objLogica As New SeguimientoWAP_BLL
        dtbArrDataTables = objLogica.Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow_V2(lstr_FechaInicio, lstr_FechaFin, Me.txtPlaca.Text.Trim)

        gwdDatos.DataSource = Nothing
        gwdDatos.DataSource = dtbArrDataTables(1)
        Me.gwdDatos.Columns(0).Frozen = True
        Me.gwdDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        'Pone el nombre a las columnas de eventos
        For intCont As Integer = 1 To gwdDatos.Columns.Count - 1
            For Each row As DataRow In dtbArrDataTables(0).Rows
                If row("NCOEVT").ToString.Trim.Equals(gwdDatos.Columns(intCont).Name) Then
                    gwdDatos.Columns(intCont).HeaderText = row("TNMCEV").ToString.Trim
                End If
            Next
        Next

        Dim objDtEventosCfgDeltaT As DataTable
        objDtEventosCfgDeltaT = dtbArrDataTables(2)

        'Se establece el nombre de la columna Delta
        For intRow As Integer = 2 To gwdDatos.Columns.Count - 1
            If gwdDatos.Columns.Item(intRow).Name.StartsWith("Delta:") Then
                Dim lstr_colName As String = gwdDatos.Columns.Item(intRow).Name.ToString.Trim
                lstr_colName = lstr_colName.Replace("Delta:", "")
                Dim lstr_ev2 As String = (lstr_colName.Split("-"))(0)
                Dim lstr_ev1 As String = (lstr_colName.Split("-"))(1)

                For Each rowCfgDeltaEventos As DataRow In objDtEventosCfgDeltaT.Rows
                    If lstr_ev2.Equals(rowCfgDeltaEventos("NIVEL2").ToString.Trim) AndAlso lstr_ev1.Equals(rowCfgDeltaEventos("NIVEL1").ToString.Trim) Then
                        Me.gwdDatos.Columns(intRow).HeaderText = rowCfgDeltaEventos("TOBS").ToString.Trim
                    End If
                Next

                'asigna color y font
                gwdDatos.Columns(intRow).DefaultCellStyle.BackColor = Color.YellowGreen
                gwdDatos.Columns(intRow).DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point, Nothing)

            End If
        Next
        'asigna color Orange a la fila de sumatoria de Deltas
        gwdDatos.Rows(gwdDatos.RowCount - 1).DefaultCellStyle.BackColor = Color.Orange

        For r As Integer = 0 To gwdDatos.Rows.Count - 1
            For c As Integer = 0 To gwdDatos.Columns.Count - 1
                If gwdDatos.Columns(c).HeaderText.StartsWith("TIEMPO") AndAlso _
                        gwdDatos.Rows(r).Cells(c).Value IsNot DBNull.Value AndAlso _
                        gwdDatos.Rows(r).Cells(c).Value.StartsWith("-") Then
                    gwdDatos.Rows(r).Cells(c).Style.BackColor = Color.Red
                End If
            Next
        Next

    End Sub

  Private Sub Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow()
    Dim dtbArrDataTables(3) As DataTable
    Dim lstr_FechaInicio As String = HelpClass.CtypeDate(Me.dtpFechaInicio.Value).ToString.Trim
    Dim lstr_FechaFin As String = HelpClass.CtypeDate(Me.dtpFechaFin.Value).ToString.Trim

    Dim objLogica As New SeguimientoWAP_BLL
    dtbArrDataTables = objLogica.Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow(lstr_FechaInicio, lstr_FechaFin, Me.txtPlaca.Text.Trim)

    gwdDatos.DataSource = Nothing
    gwdDatos.DataSource = dtbArrDataTables(1)
    Me.gwdDatos.Columns(0).Frozen = True
    Me.gwdDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    'Pone el nombre a las columnas de acciones
    For intCont As Integer = 1 To gwdDatos.Columns.Count - 1
      For Each row As DataRow In dtbArrDataTables(0).Rows
        If row("NCOEVT").ToString.Trim.Equals(gwdDatos.Columns(intCont).Name) Then
          gwdDatos.Columns(intCont).HeaderText = row("TNOEVT").ToString.Trim
        End If
      Next
    Next

    Dim objDtEventosCfgDeltaT As DataTable
    objDtEventosCfgDeltaT = dtbArrDataTables(2)

    'Se establece el nombre de la columna Delta
    For intRow As Integer = 2 To gwdDatos.Columns.Count - 1
      If gwdDatos.Columns.Item(intRow).Name.StartsWith("Delta:") Then
        Dim lstr_colName As String = gwdDatos.Columns.Item(intRow).Name.ToString.Trim
        lstr_colName = lstr_colName.Replace("Delta:", "")
        Dim lstr_ev2 As String = (lstr_colName.Split("-"))(0)
        Dim lstr_ev1 As String = (lstr_colName.Split("-"))(1)

        For Each rowCfgDeltaEventos As DataRow In objDtEventosCfgDeltaT.Rows
          If lstr_ev2.Equals(rowCfgDeltaEventos("NIVEL2").ToString.Trim) And lstr_ev1.Equals(rowCfgDeltaEventos("NIVEL1").ToString.Trim) Then
            Me.gwdDatos.Columns(intRow).HeaderText = rowCfgDeltaEventos("TOBS").ToString.Trim
          End If
        Next

        'asigna color y font
        gwdDatos.Columns(intRow).DefaultCellStyle.BackColor = Color.YellowGreen
        gwdDatos.Columns(intRow).DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point, Nothing)

      End If
    Next
    'asigna color Orange a la fila de sumatoria de Deltas
    gwdDatos.Rows(gwdDatos.RowCount - 1).DefaultCellStyle.BackColor = Color.Orange

    For r As Integer = 0 To gwdDatos.Rows.Count - 1
      For c As Integer = 0 To gwdDatos.Columns.Count - 1
        If gwdDatos.Columns(c).HeaderText.StartsWith("TIEMPO") Then
          If gwdDatos.Rows(r).Cells(c).Value IsNot DBNull.Value Then
            If gwdDatos.Rows(r).Cells(c).Value.StartsWith("-") Then
              gwdDatos.Rows(r).Cells(c).Style.BackColor = Color.Red
            End If
          End If
        End If
      Next
    Next

  End Sub

  Private Sub Listado_Registro_Eventos_Wap()

    Dim objSeguimientoWap As New SeguimientoWAP_BLL
    Dim objParametros As New Hashtable
    Dim dw As New DataView

    If Me.cboRolWap.SelectedIndex = 0 Then
      objParametros.Add("NCOROL", "")
    Else
      objParametros.Add("NCOROL", Me.cboRolWap.SelectedValue.ToString.Trim)
    End If

    objParametros.Add("FECINI", HelpClass.CtypeDate(Me.dtpFechaInicio.Value).ToString.Trim)
    objParametros.Add("FECFIN", HelpClass.CtypeDate(Me.dtpFechaFin.Value).ToString.Trim)

    dw = objSeguimientoWap.Listar_Acciones_Wap(objParametros).DefaultView

    If Me.txtPlaca.Text.Trim <> "" Then
      dw.RowFilter = "NPLCTR LIKE '%" & Me.txtPlaca.Text.Trim & "%'"
    End If

    If Me.txtUsuarioWap.NoHayCodigo = False Then
      If dw.RowFilter.Length > 0 Then
        dw.RowFilter += " AND NROMOV='" & Me.txtUsuarioWap.Codigo & "'"
      Else
        dw.RowFilter += " NROMOV='" & Me.txtUsuarioWap.Codigo & "'"
      End If
    End If

    Me.gwdDatos.DataSource = dw.ToTable

    Me.gwdDatos.Columns(0).Visible = False
    Me.gwdDatos.Columns(1).HeaderText = "Placa Tracto"
    Me.gwdDatos.Columns(1).Width = 70
    Me.gwdDatos.Columns(2).HeaderText = "Nro Operacion"
    Me.gwdDatos.Columns(2).Width = 70
    Me.gwdDatos.Columns(2).Visible = False
    Me.gwdDatos.Columns(3).HeaderText = "Hora"
    Me.gwdDatos.Columns(3).Width = 60
    Me.gwdDatos.Columns(4).HeaderText = "Fecha"
    Me.gwdDatos.Columns(4).Width = 70
    Me.gwdDatos.Columns(5).HeaderText = "Accion / Evento"
    Me.gwdDatos.Columns(5).Width = 400
    Me.gwdDatos.Columns(6).HeaderText = "Nro Nextel"
    Me.gwdDatos.Columns(6).Width = 80
    Me.gwdDatos.Columns(7).HeaderText = "Rol de Registro"
    Me.gwdDatos.Columns(7).Width = 150

  End Sub

  Private Sub Listar_Diferencias_Tiempos_Wap_Workflow(ByVal cboindex As Integer)
    Dim dtbArrDataTables(2) As DataTable
    Dim lstr_FechaInicio As String = HelpClass.CtypeDate(Me.dtpFechaInicio.Value).ToString.Trim
    Dim lstr_FechaFin As String = HelpClass.CtypeDate(Me.dtpFechaFin.Value).ToString.Trim
    Dim lstr_codRol As String = CInt(Me.cboRolWap.SelectedValue.ToString.Trim)

    Dim objLogica As New SeguimientoWAP_BLL
    dtbArrDataTables = objLogica.Listar_Diferencias_Tiempos_Wap_Workflow(lstr_codRol, cboindex, lstr_FechaInicio, lstr_FechaFin, Me.txtPlaca.Text.Trim, Me.txtUsuarioWap.Codigo)

    gwdDatos.DataSource = Nothing
    gwdDatos.DataSource = dtbArrDataTables(1)
    Me.gwdDatos.Columns(0).Frozen = True
    Me.gwdDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    'Pone el nombre a las columnas de acciones
    For lint_c As Integer = 1 To gwdDatos.Columns.Count - 1 Step 2
      For Each row As DataRow In dtbArrDataTables(0).Rows
        If row("NCOACC").ToString.Trim.Equals(gwdDatos.Columns(lint_c).Name) Then
          gwdDatos.Columns(lint_c).HeaderText = row("TOBS").ToString.Trim
        End If
      Next
    Next

    'Pone nombre a las columnas delta 
    For lint_c As Integer = 2 To gwdDatos.Columns.Count - 1 Step 2
      Me.gwdDatos.Columns(lint_c).HeaderText = "Delta T"
      'asigna color y font
      gwdDatos.Columns(lint_c).DefaultCellStyle.BackColor = Color.YellowGreen
      gwdDatos.Columns(lint_c).DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point, Nothing)
    Next
    'asigna color Orange
    gwdDatos.Rows(gwdDatos.RowCount - 1).DefaultCellStyle.BackColor = Color.Orange

  End Sub

  Private Sub Listado_Registro_Eventos_Wap_Workflow(ByVal cboindex As Integer)
    Dim arrDataTables(2) As DataTable
    Dim fechaInicio As String = HelpClass.CtypeDate(Me.dtpFechaInicio.Value).ToString.Trim
    Dim fechaFin As String = HelpClass.CtypeDate(Me.dtpFechaFin.Value).ToString.Trim
    Dim codRol As String = CInt(Me.cboRolWap.SelectedValue.ToString.Trim)

    Dim objLogica As New SeguimientoWAP_BLL
    arrDataTables = objLogica.listado_Registro_Eventos_Wap_Workflow(codRol, cboindex, fechaInicio, fechaFin, Me.txtPlaca.Text.Trim, Me.txtUsuarioWap.Codigo)

    Me.gwdDatos.DataSource = Nothing
    Me.gwdDatos.DataSource = arrDataTables(1)
    Me.gwdDatos.Columns(0).Frozen = True
    Me.gwdDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    'Pone el nombre a las columnas de acciones
    For c As Integer = 1 To gwdDatos.Columns.Count - 1
      For Each row As DataRow In arrDataTables(0).Rows
        If row("NCOACC").ToString.Trim.Equals(gwdDatos.Columns(c).Name) Then
          gwdDatos.Columns(c).HeaderText = row("TOBS").ToString.Trim
        End If
      Next
    Next

  End Sub

  Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click

    Dim objReporte As New ReportClass
    Dim objPrintForm As New PrintForm
    If Me.gwdDatos.RowCount > 0 Then Me.Imprimir_Indicadores()
    Exit Sub

  End Sub

    Private Sub Imprimir_Indicadores()
        Dim objDataTable As New DataTable
        Dim objDataSet As New DataSet
        Dim objPrintForm As New PrintForm
        Dim objReporte As New rptIndicadorWAP
        Dim lstr_Valor() As String
        Dim lint_Valor As Integer = 1

        objDataTable.TableName = "IndicadoresWAP"
        objDataTable.Columns.Add("NCOACC")
        objDataTable.Columns.Add("TOBS")
        objDataTable.Columns.Add("TOTAL", Type.GetType("System.Int32"))
        For lint_Count As Integer = 4 To Me.gwdDatos.ColumnCount - 1
            Dim row As DataRow = objDataTable.NewRow
            If Me.gwdDatos.Columns(lint_Count).Name.Trim.Substring(0, 1) = "D" Then
                row(0) = "∆" & lint_Valor
                lint_Valor = lint_Valor + 1
            Else
                row(0) = Me.gwdDatos.Columns(lint_Count).Name
            End If
            If Me.gwdDatos.Columns(lint_Count).Name.Contains("Delta") = True Then
                row(1) = "∆" & lint_Valor - 1 & " = (" & Me.gwdDatos.Columns(lint_Count + 1).Name & " - " & Me.gwdDatos.Columns(lint_Count - 1).Name & ")"
            Else
                row(1) = Me.gwdDatos.Columns(lint_Count).HeaderText
            End If
            If Me.gwdDatos.Item(lint_Count, Me.gwdDatos.RowCount - 1).Value.ToString <> "" Then
                lstr_Valor = Me.gwdDatos.Item(lint_Count, Me.gwdDatos.RowCount - 1).Value.ToString.Split(" ")

                row(2) = CType(lstr_Valor(0), Int64) * 60 + CType(lstr_Valor(2), Int64)
            Else
                row(2) = 0
            End If
            objDataTable.Rows.Add(row)
        Next
        objReporte.SetDataSource(objDataTable)
        HelpClass.CrystalReportTextObject(objReporte, "lblFechaInicio", Me.dtpFechaInicio.Value.ToShortDateString())
        HelpClass.CrystalReportTextObject(objReporte, "lblFechaFinal", Me.dtpFechaFin.Value.ToShortDateString())
        HelpClass.CrystalReportTextObject(objReporte, "lblContenido", IIf(Me.txtPlaca.TextLength > 0, "Placa del Tracto : " & Me.txtPlaca.Text, "Toda la Flota"))
        objPrintForm.ShowForm(objReporte, Me)
    End Sub

    Private Sub gwdDatos_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gwdDatos.CellDoubleClick
        Dim lint_indice As Integer = gwdDatos.CurrentCellAddress.Y

        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 OrElse lint_indice < 0 Then
            Exit Sub
        End If

        Dim colName As String = gwdDatos.Columns(gwdDatos.CurrentCellAddress.X).Name

        If colName.Equals("CICLO") Then
            If Me.gwdDatos.Item("CICLO", lint_indice).Value.ToString.Equals("") = True Then Exit Sub
            Dim frm_Informacion_Ciclo As New frmInformacionCicloWAP
            With frm_Informacion_Ciclo
                .IsMdiContainer = True
                .Objeto_Entidad_Ciclo_WAP.CICLO = Me.gwdDatos.Item("CICLO", lint_indice).Value.ToString.Trim
                .Objeto_Entidad_Ciclo_WAP.NPLCTR = Me.gwdDatos.Item("NPLCTR", lint_indice).Value.ToString.Trim
                .ShowDialog()
            End With
        ElseIf colName.Equals("NPLCTR") Then
            Dim NPLCTR As String = gwdDatos.Item(gwdDatos.CurrentCellAddress.X, lint_indice).Value.ToString

            If NPLCTR.Equals("") Then Exit Sub

            'fecha de creacion del ciclo
            Dim FECINI As String
            FECINI = gwdDatos.Item("FCHCRT", lint_indice).Value.ToString()
            FECINI = FECINI.Substring(6, 4) & FECINI.Substring(3, 2) & FECINI.Substring(0, 2)

            'fecha del ultimo checkpoint registrado del ciclo
            Dim FECFIN As String
            FECFIN = FECINI 'default

            Dim str As String = ""
            For i As Integer = gwdDatos.Columns.Count - 2 To 4 Step -1
                str = gwdDatos.Item(i, lint_indice).Value.ToString.Trim
                If Not str.Equals("") Then
                    FECFIN = str.Substring(6, 4) & str.Substring(3, 2) & str.Substring(0, 2)
                    Exit For
                End If
            Next

            Dim f As New frmInfoUnidadAsignadaxFecha(NPLCTR, FECINI, FECFIN)
            f.StartPosition = FormStartPosition.CenterParent
            f.ShowForm(Me)

        ElseIf e.ColumnIndex > 3 Then
            If Me.gwdDatos.Item(e.ColumnIndex, lint_indice).Value.ToString.Trim.Equals("") Then Exit Sub
            If Me.gwdDatos.Columns(e.ColumnIndex).Name.Contains("Delta:") = True Then Exit Sub

            If e.ColumnIndex > 3 Then
                Dim objEntidad As New RegistroWAP
                objEntidad.CICLO = Me.gwdDatos.Item("CICLO", lint_indice).Value.ToString.Trim
                objEntidad.NCOEVT = Me.gwdDatos.Columns(colName).Name
                objEntidad.FRGTRO = Me.gwdDatos.Item(e.ColumnIndex, lint_indice).Value.ToString.Substring(0, 10)
                objEntidad.HRGTRO = Me.gwdDatos.Item(e.ColumnIndex, lint_indice).Value.ToString
                objEntidad.CCMPN = CCMPN
                Dim f As New frmInformacionRegistroWAP(objEntidad)
                f.ShowForm(Me)
            End If
        End If

    End Sub

    Private Sub gwdDatos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gwdDatos.CellClick
        Dim lint_indice As Integer = Me.gwdDatos.CurrentCellAddress.Y
        If e.RowIndex < 0 OrElse Me.gwdDatos.CurrentCellAddress.Y < 0 Then
            Exit Sub
        End If
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

    Private Sub KryptonLabel17_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles KryptonLabel17.Paint

    End Sub

    Private Sub cboGrupoSeguimiento_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
