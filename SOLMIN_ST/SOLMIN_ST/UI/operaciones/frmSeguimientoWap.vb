Imports SOLMIN_ST.ENTIDADES
Imports SOLMIN_st.NEGOCIO
Imports SOLMIN_st.NEGOCIO.seguimiento_wap
Imports SOLMIN_ST.NEGOCIO.operaciones
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports System
Imports System.Text
Imports System.IO
Imports System.Reflection

Public Class frmConsultarRegistrosWap

    Private _CCMPN As String
    Public Property CCMPN() As String
        Get
            Return _CCMPN
        End Get
        Set(ByVal value As String)
            _CCMPN = value
        End Set
    End Property

    Private Sub frmSeguimientoWap_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Cargando los combos del formulario
        Cargar_Combos()
        'Estableciendo el  combo de reporte wap
        Me.cboTipoReporteWap.SelectedIndex = 0

    End Sub

    Public Sub Cargar_Combos()

        Dim objSeguimientoWap As New SeguimientoWAP_BLL
        Try
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
        Catch : End Try


    End Sub

    Private Sub Listar_Diferencias_Tiempos_Wap()

        Dim objSeguimientoWap As New SeguimientoWAP_BLL
        Dim objParametros As New Hashtable
        Dim dw As New DataView

        objParametros.Add("NCOROL", IIf(Me.cboRolWap.SelectedIndex = 0, "", Me.cboRolWap.SelectedValue.ToString.Trim))
        objParametros.Add("FECINI", HelpClass.CtypeDate(Me.dtpFechaInicio.Value).ToString.Trim)
        objParametros.Add("FECFIN", HelpClass.CtypeDate(Me.dtpFechaFin.Value).ToString.Trim)

        dw = objSeguimientoWap.Listar_Diferencia_Tiempos_Wap(objParametros).DefaultView

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
        Me.gwdDatos.Columns(1).Width = 60
        Me.gwdDatos.Columns(2).HeaderText = "Nro Operacion"
        Me.gwdDatos.Columns(2).Width = 60
        Me.gwdDatos.Columns(2).Visible = False
        Me.gwdDatos.Columns(3).HeaderText = "Fecha"
        Me.gwdDatos.Columns(3).Width = 80
        Me.gwdDatos.Columns(4).HeaderText = "Dias de Diferencia"
        Me.gwdDatos.Columns(4).Width = 90
        Me.gwdDatos.Columns(5).HeaderText = "Fec Chk. Pnt. Ant."
        Me.gwdDatos.Columns(5).Width = 90
        Me.gwdDatos.Columns(6).HeaderText = "Hora "
        Me.gwdDatos.Columns(6).Width = 60
        Me.gwdDatos.Columns(7).HeaderText = "Tiempo de Diferencia"
        Me.gwdDatos.Columns(7).Width = 120
        Me.gwdDatos.Columns(8).HeaderText = "Hora Chk. Pnt. Ant."
        Me.gwdDatos.Columns(8).Width = 120
        Me.gwdDatos.Columns(9).HeaderText = "Evento / Accion"
        Me.gwdDatos.Columns(9).Width = 250
        Me.gwdDatos.Columns(10).HeaderText = "Nro Nextel"
        Me.gwdDatos.Columns(10).Width = 100
        Me.gwdDatos.Columns(11).HeaderText = "Rol "
        Me.gwdDatos.Columns(11).Width = 100

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

    Private Sub Listado_Registro_Eventos_Wap_Workflow(ByVal cboindex As Integer)
        Dim dtbArrDataTables(2) As DataTable
        Dim lstr_FechaInicio As String = HelpClass.CtypeDate(Me.dtpFechaInicio.Value).ToString.Trim
        Dim lstr_FechaFin As String = HelpClass.CtypeDate(Me.dtpFechaFin.Value).ToString.Trim
        Dim lstr_codRol As String = CInt(Me.cboRolWap.SelectedValue.ToString.Trim)

        Dim objLogica As New SeguimientoWAP_BLL
        dtbArrDataTables = objLogica.listado_Registro_Eventos_Wap_Workflow(lstr_codRol, cboindex, lstr_FechaInicio, lstr_FechaFin, Me.txtPlaca.Text.Trim, Me.txtUsuarioWap.Codigo)

        Me.gwdDatos.DataSource = Nothing
        Me.gwdDatos.DataSource = dtbArrDataTables(1)
        Me.gwdDatos.Columns(0).Frozen = True
        Me.gwdDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        'Pone el nombre a las columnas de acciones
        For lint_c As Integer = 1 To gwdDatos.Columns.Count - 1
            For Each row As DataRow In dtbArrDataTables(0).Rows
                If row("NCOACC").ToString.Trim.Equals(gwdDatos.Columns(lint_c).Name) Then
                    gwdDatos.Columns(lint_c).HeaderText = row("TOBS").ToString.Trim
                End If
            Next
        Next

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
        For lint_c As Integer = 1 To gwdDatos.Columns.Count - 1
            For Each row As DataRow In dtbArrDataTables(0).Rows
                If row("NCOACC").ToString.Trim.Equals(gwdDatos.Columns(lint_c).Name) Then
                    gwdDatos.Columns(lint_c).HeaderText = row("TNOEVT").ToString.Trim
                End If
            Next
        Next

        Dim dtbEventosCfgDeltaT As DataTable
        dtbEventosCfgDeltaT = dtbArrDataTables(2)

        'Se establece el nombre de la columna Delta
        For lint_c As Integer = 2 To gwdDatos.Columns.Count - 1
            If gwdDatos.Columns.Item(lint_c).Name.StartsWith("Delta:") Then
                Dim lstr_colName As String = gwdDatos.Columns.Item(lint_c).Name.ToString.Trim
                lstr_colName = lstr_colName.Replace("Delta:", "")
                Dim lstr_ev2 As String = (lstr_colName.Split("-"))(0)
                Dim lstr_ev1 As String = (lstr_colName.Split("-"))(1)

                For Each rowCfgDeltaEventos As DataRow In dtbEventosCfgDeltaT.Rows
                    If lstr_ev2.Equals(rowCfgDeltaEventos("NIVEL2").ToString.Trim) And lstr_ev1.Equals(rowCfgDeltaEventos("NIVEL1").ToString.Trim) Then
                        Me.gwdDatos.Columns(lint_c).HeaderText = rowCfgDeltaEventos("TOBS").ToString.Trim
                    End If
                Next

                'asigna color y font
                gwdDatos.Columns(lint_c).DefaultCellStyle.BackColor = Color.YellowGreen
                gwdDatos.Columns(lint_c).DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point, Nothing)

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

    Private Sub gwdDatos_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gwdDatos.CellClick
        If e.RowIndex < 0 Or Me.gwdDatos.CurrentCellAddress.Y < 0 Then
            Exit Sub
        End If
        carga_datos_grilla()
    End Sub

    Private Sub carga_datos_grilla(Optional ByVal Raise_by_Consulta As Boolean = False)

        Try
            Dim objSeguimientoWap As New SeguimientoWAP_BLL
            Dim objParametros As New Hashtable

            If Raise_by_Consulta = True Then
                If Me.txtPlaca.Text = "" Then
                    HelpClass.MsgBox("Debe de seleccionar un vehículo para hacer el seguimiento")
                    Exit Sub
                End If
                objParametros.Add("NPLCTR", Me.txtPlaca.Text)
            Else
                objParametros.Add("NPLCTR", Me.gwdDatos.Item(1, Me.gwdDatos.CurrentCellAddress.Y).Value)
            End If

            objParametros.Add("FECINI", HelpClass.CtypeDate(Me.dtpFechaInicio.Value).ToString.Trim)
            objParametros.Add("FECFIN", HelpClass.CtypeDate(Me.dtpFechaFin.Value).ToString.Trim)

            Me.gwdDatosGPS.DataSource = objSeguimientoWap.Listar_Posiciones_GPS(objParametros)

            Me.gwdDatosGPS.Columns(0).Visible = False
            Me.gwdDatosGPS.Columns(1).Width = 90
            Me.gwdDatosGPS.Columns(1).HeaderText = "Fecha"
            Me.gwdDatosGPS.Columns(2).Width = 60
            Me.gwdDatosGPS.Columns(2).HeaderText = "Hora"
            Me.gwdDatosGPS.Columns(3).Width = 100
            Me.gwdDatosGPS.Columns(3).HeaderText = "Latitud GPS"
            Me.gwdDatosGPS.Columns(4).Width = 100
            Me.gwdDatosGPS.Columns(4).HeaderText = "Longitud GPS"

            If Raise_by_Consulta = True Then
                Me.HeaderDatos.ValuesPrimary.Heading = "Vehiculo " & Me.txtPlaca.Text
            Else
                Me.HeaderDatos.ValuesPrimary.Heading = "Vehiculo " & Me.gwdDatos.Item(1, Me.gwdDatos.CurrentCellAddress.Y).Value
            End If
        Catch : End Try

    End Sub

    Private Sub gwdDatosGPS_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gwdDatosGPS.CellClick

        If e.RowIndex < 0 Or Me.gwdDatosGPS.CurrentCellAddress.Y < 0 Then
            Exit Sub
        End If
        cargar_datos_wap()

    End Sub

    Private Sub cargar_datos_wap()

        Dim longitud As String = Me.gwdDatosGPS.Item(4, gwdDatosGPS.CurrentCellAddress.Y).Value.ToString.Trim
        Dim latitud As String = Me.gwdDatosGPS.Item(3, gwdDatosGPS.CurrentCellAddress.Y).Value.ToString.Trim

        If Me.HeaderBrowser.Panel.Controls.Count > 0 Then
            Me.HeaderBrowser.Panel.Controls.Clear()
        End If

        Dim GpsBrowser As New WebBrowser
        Me.HeaderBrowser.Panel.Controls.Clear()
        Me.HeaderBrowser.Panel.Controls.Add(GpsBrowser)
        GpsBrowser.Dock = DockStyle.Fill

        'Cargando en el browser   
        GpsBrowser.Navigate("http://asp.ransa.com.pe/wapmineria/tracking/frmGpsBrowser.aspx?txt=&ancho=400&alto=300&lon=" & longitud & "&lat=" & latitud & "")

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub gwdDatos_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gwdDatos.KeyUp

        If Me.gwdDatos.CurrentCellAddress.Y < 0 Then
            Exit Sub
        End If

        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            carga_datos_grilla()
        End If

    End Sub

    Private Sub gwdDatosGPS_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gwdDatosGPS.KeyUp

        If Me.gwdDatosGPS.CurrentCellAddress.Y < 0 Then
            Exit Sub
        End If

        If e.KeyCode <> Keys.Up Or e.KeyCode <> Keys.Down Then
            cargar_datos_wap()
        End If

    End Sub

    Private Sub btnMaximizarMapa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMaximizarMapa.Click

        If Me.gwdDatosGPS.Rows.Count > 0 Then
            Dim longitud As String = Me.gwdDatosGPS.Item(4, gwdDatosGPS.CurrentCellAddress.Y).Value.ToString.Trim
            Dim latitud As String = Me.gwdDatosGPS.Item(3, gwdDatosGPS.CurrentCellAddress.Y).Value.ToString.Trim
            Dim objGpsBrowser As New frmMapa
            objGpsBrowser.Hora = Me.gwdDatosGPS.Item(2, gwdDatosGPS.CurrentCellAddress.Y).Value.ToString.Trim
            objGpsBrowser.Fecha = Me.gwdDatosGPS.Item(1, gwdDatosGPS.CurrentCellAddress.Y).Value.ToString.Trim
            objGpsBrowser.WindowState = FormWindowState.Maximized
            objGpsBrowser.ShowForm(latitud, longitud, Me)
        End If

    End Sub

    Private Sub ButtonSpecAny1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSpecAny1.Click

        'Creando la cadena de longitud y de latitud para
        Dim lstr_posicion_gps As String = ""
        Dim lstr_documento_xml As String = ""
        Dim archivo As String = ""
        Dim pStart As New System.Diagnostics.Process
        Dim str_localizacion As String = ""
        Dim lint_indice As Integer = 0

        lstr_documento_xml += "<?xml version=""1.0"" encoding=""UTF-8""?>"
        lstr_documento_xml += "<kml xmlns=""http://www.opengis.net/kml/2.2"" xmlns:gx=""http://www.google.com/kml/ext/2.2"" xmlns:kml=""http://www.opengis.net/kml/2.2"" xmlns:atom=""http://www.w3.org/2005/Atom"">"
        lstr_documento_xml += "<Document>"
        lstr_documento_xml += "	<name>SeguimientoWap.kml</name>"
        lstr_documento_xml += "	<open>1</open>"
        lstr_documento_xml += "	<Style id=""sh_truck"">"
        lstr_documento_xml += "		<IconStyle>"
        lstr_documento_xml += "			<scale>1.1</scale>"
        lstr_documento_xml += "			<Icon>"
        lstr_documento_xml += "				<href>http://maps.google.com/mapfiles/kml/shapes/truck.png</href>"
        lstr_documento_xml += "			</Icon>"
        lstr_documento_xml += "			<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>"
        lstr_documento_xml += "		</IconStyle>"
        lstr_documento_xml += "	</Style>"
        lstr_documento_xml += "	<Style id=""default+icon=http://asp.ransa.com.pe/wapmineria/logoransa.jpg"">"
        lstr_documento_xml += "		<IconStyle>"
        lstr_documento_xml += "			<Icon>"
        lstr_documento_xml += "				<href>http://maps.google.com/mapfiles/kml/shapes/truck.png</href>"
        lstr_documento_xml += "			</Icon>"
        lstr_documento_xml += "		</IconStyle>"
        lstr_documento_xml += "	</Style>"
        lstr_documento_xml += "	<StyleMap id=""sh_truck"">"
        lstr_documento_xml += "		<Pair>"
        lstr_documento_xml += "			<key>normal</key>"
        lstr_documento_xml += "			<styleUrl>#sn_ylw-pushpin</styleUrl>"
        lstr_documento_xml += "		</Pair>"
        lstr_documento_xml += "		<Pair>"
        lstr_documento_xml += "			<key>highlight</key>"
        lstr_documento_xml += "			<styleUrl>#sh_truck</styleUrl>"
        lstr_documento_xml += "		</Pair>"
        lstr_documento_xml += "	</StyleMap>"
        lstr_documento_xml += "	<Style id=""default+icon=http://maps.google.com/mapfiles/kml/shapes/truck.png"">"
        lstr_documento_xml += "		<IconStyle>"
        lstr_documento_xml += "			<scale>1.1</scale>"
        lstr_documento_xml += "			<Icon>"
        lstr_documento_xml += "				<href>http://maps.google.com/mapfiles/kml/shapes/truck.png</href>"
        lstr_documento_xml += "			</Icon>"
        lstr_documento_xml += "		</IconStyle>"
        lstr_documento_xml += "		<LabelStyle>"
        lstr_documento_xml += "			<scale>1.1</scale>"
        lstr_documento_xml += "		</LabelStyle>"
        lstr_documento_xml += "	</Style>"
        lstr_documento_xml += "	<StyleMap id=""default+nicon=http://maps.google.com/mapfiles/kml/shapes/truck.png"">"
        lstr_documento_xml += "		<Pair>"
        lstr_documento_xml += "			<key>normal</key>"
        lstr_documento_xml += "			<styleUrl>#default+icon=http://maps.google.com/mapfiles/kml/shapes/truck.png</styleUrl>"
        lstr_documento_xml += "		</Pair>"
        lstr_documento_xml += "		<Pair>"
        lstr_documento_xml += "			<key>highlight</key>"
        lstr_documento_xml += "			<styleUrl>#default+icon=http://maps.google.com/mapfiles/kml/shapes/truck.png</styleUrl>"
        lstr_documento_xml += "		</Pair>"
        lstr_documento_xml += "	</StyleMap>"
        lstr_documento_xml += "	<Style id=""sh_ylw-pushpin"">"
        lstr_documento_xml += "		<IconStyle>"
        lstr_documento_xml += "			<scale>1.3</scale>"
        lstr_documento_xml += "			<Icon>"
        lstr_documento_xml += "				<href>http://maps.google.com/mapfiles/kml/shapes/truck.png</href>"
        lstr_documento_xml += "			</Icon>"
        lstr_documento_xml += "			<hotSpot x=""20"" y=""2"" xunits=""pixels"" yunits=""pixels""/>"
        lstr_documento_xml += "		</IconStyle>"
        lstr_documento_xml += "	</Style>"
        lstr_documento_xml += "	<Folder>"
        lstr_documento_xml += "		<name>Seguimiento Wap</name>"
        lstr_documento_xml += "		<open>1</open>"
        lstr_documento_xml += "		<Placemark>"
        lstr_documento_xml += "			<name>ruta001</name>"
        lstr_documento_xml += "			<styleUrl>#msn_ylw-pushpin</styleUrl>"
        lstr_documento_xml += "			<LineString>"
        lstr_documento_xml += "				<tessellate>1</tessellate>"
        lstr_documento_xml += "				<coordinates>"
        lstr_documento_xml += "				</coordinates>"
        lstr_documento_xml += "			</LineString>"
        lstr_documento_xml += "		</Placemark>"


        For Each objRow As DataGridViewRow In Me.gwdDatosGPS.SelectedRows

            If objRow.Cells(4).Value.ToString.Trim <> "" And objRow.Cells(3).Value.ToString.Trim <> "" Then

                lstr_documento_xml += "		<Placemark>"
                lstr_documento_xml += "			<name> " & lint_indice & " :" & objRow.Cells(1).Value.ToString.Trim & " | " & objRow.Cells(2).Value.ToString.Trim & "</name>"
                lstr_documento_xml += "			<open>1</open>"
                lstr_documento_xml += "			<styleUrl>#default+nicon=http://asp.ransa.com.pe/wapmineria/logoransa.jpg</styleUrl>"
                lstr_documento_xml += "			<Point>"
                lstr_documento_xml += "				<coordinates>" & objRow.Cells(4).Value.ToString.Trim & "," & objRow.Cells(3).Value.ToString.Trim & ",0</coordinates>"
                lstr_documento_xml += "			</Point>"
                lstr_documento_xml += "		</Placemark>"

                str_localizacion += objRow.Cells(4).Value.ToString.Trim & "," & objRow.Cells(3).Value.ToString.Trim & ", 0 "
                lint_indice += 1
            End If

        Next

        lstr_documento_xml += "	<Placemark>"
        lstr_documento_xml += "			<name>Seguimiento Vehicular</name>"
        lstr_documento_xml += "			<description>Seguimiento de Vehiculo</description>"
        lstr_documento_xml += "			<styleUrl>#msn_ylw-pushpin6</styleUrl>"
        lstr_documento_xml += "			<LineString>"
        lstr_documento_xml += "				<tessellate>1</tessellate>"
        lstr_documento_xml += "				<coordinates>"

        lstr_documento_xml += str_localizacion

        lstr_documento_xml += "</coordinates>"
        lstr_documento_xml += "			</LineString>"
        lstr_documento_xml += "	</Placemark>"


        lstr_documento_xml += "	</Folder>"
        lstr_documento_xml += "</Document>"
        lstr_documento_xml += "</kml>"


        Try
            archivo = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ruta_" & HelpClass.TodayNumeric & "" & HelpClass.NowNumeric & ".kml"
            Dim oFile As New IO.StreamWriter(archivo, False, Encoding.ASCII)
            oFile.WriteLine(lstr_documento_xml)
            oFile.Close()
            Process.Start(archivo)
        Catch : End Try

    End Sub

    Private Sub ButtonSpecHeaderGroup1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcesar.Click

        If dtpFechaFin.Value < dtpFechaInicio.Value Then
            HelpClass.MsgBox("La fecha de inicio no puede ser posterior a la fecha final.")
            Exit Sub
        End If

        Select Case Me.cboTipoReporteWap.SelectedIndex
            Case 0
                Me.Listado_Registro_Eventos_Wap()
            Case 1
                Listar_Diferencias_Tiempos_Wap_Workflow(Me.cboTipoReporteWap.SelectedIndex)
            Case 2
                carga_datos_grilla(True)
            Case 3
                Listado_Registro_Eventos_Wap_Workflow(Me.cboTipoReporteWap.SelectedIndex)
            Case 4
                Listar_Diferencias_Tiempos_x_Config_Eventos_Wap_Workflow()
            Case 5
                Listar_Seguimiento_Flota()
        End Select

    End Sub

    Private Sub ButtonSpecHeaderGroup2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprimir.Click

        Dim objReporte As New ReportClass
        Dim objPrintForm As New PrintForm
        Select Case Me.cboTipoReporteWap.SelectedIndex
            Case 0
                objReporte = New rptListadoEventosRegistrados
                'Obteniendo la data
                objReporte.SetDataSource(CType(Me.gwdDatos.DataSource, DataTable))
                HelpClass.CrystalReportTextObject(objReporte, "lblFechaInicio", Me.dtpFechaInicio.Value.ToShortDateString())
                HelpClass.CrystalReportTextObject(objReporte, "lblFechaFinal", Me.dtpFechaFin.Value.ToShortDateString())
                HelpClass.CrystalReportTextObject(objReporte, "lblPlaca", Me.txtPlaca.Text)
                HelpClass.CrystalReportTextObject(objReporte, "lblUsuario", Me.txtUsuarioWap.Descripcion)

                objPrintForm.ShowForm(objReporte, Me)
            Case 1
                If Me.gwdDatos.RowCount > 0 Then Me.Imprimir_Indicadores()
                Exit Sub
        End Select

    End Sub

    Private Sub Imprimir_Indicadores()
        Dim objDataTable As New DataTable
        Dim objDataSet As New DataSet
        Dim objPrintForm As New PrintForm
        Dim objReporte As New rptIndicadorWAP
        Dim lstr_Valor As String = ""
        Dim lint_Valor As Integer = 1

        objDataTable.TableName = "IndicadoresWAP"
        objDataTable.Columns.Add("NCOACC")
        objDataTable.Columns.Add("TOBS")
        objDataTable.Columns.Add("TOTAL", Type.GetType("System.Int32"))
        For lint_Count As Integer = 1 To Me.gwdDatos.ColumnCount - 1
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
                lstr_Valor = Me.gwdDatos.Item(lint_Count, Me.gwdDatos.RowCount - 1).Value
                row(2) = CType(lstr_Valor.Substring(0, lstr_Valor.Length - 1).Trim, Int32)
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

    Private Sub ButtonSpecAny2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSpecAny2.Click
        Try


            If Me.gwdDatosGPS.Rows.Count > 0 Then
                'Creando la cadena de longitud y de latitud para
                Dim lstr_posicion_gps As String = ""
                Dim lstr_documento_xml As String = ""
                Dim archivo As String = ""
                Dim pStart As New System.Diagnostics.Process
                Dim str_localizacion As String = ""
                Dim lint_indice As Integer = 0


                For Each objRow As DataGridViewRow In Me.gwdDatosGPS.SelectedRows

                    If objRow.Cells(4).Value.ToString.Trim <> "" And objRow.Cells(3).Value.ToString.Trim <> "" Then
                        str_localizacion += "" & objRow.Cells(3).Value.ToString.Trim & "," & objRow.Cells(4).Value.ToString.Trim & "|"
                    End If

                Next

                str_localizacion = (Me.gwdDatosGPS.SelectedRows.Count) & "|" & str_localizacion.Substring(0, str_localizacion.Length - 2)

                Dim GpsBrowser As New WebBrowser
                Me.HeaderBrowser.Panel.Controls.Clear()
                Me.HeaderBrowser.Panel.Controls.Add(GpsBrowser)

                GpsBrowser.Dock = DockStyle.Fill
                'Cargando en el browser
                GpsBrowser.Navigate("http://asp.ransa.com.pe/wapmineria/tracking/frmGpsBrowser.aspx?txt=&ancho=400&alto=300&lon=" & 0 & "&lat=" & 0 & "&mappath=" & str_localizacion)

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub cboTipoReporteWap_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoReporteWap.SelectedIndexChanged
        Select Case cboTipoReporteWap.SelectedIndex
            Case 1, 4
                BtnImprimir.Visible = True
            Case Else
                BtnImprimir.Visible = False
        End Select
    End Sub

    Private Sub ButtonSpecHeaderGroup4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportar.Click

        'averiguando si es que existe el directorio a exportar
        Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\tempo\"

        If IO.Directory.Exists(path) = False Then
            IO.Directory.CreateDirectory(path)
        End If

        Dim archivo As String = path & "reporte" & HelpClass.NowNumeric & ".xls"

        Dim xls As New IO.StreamWriter(archivo, False)

        xls.WriteLine("<TABLE border='1'>")

        xls.WriteLine("<tr>")
        For columna As Integer = 0 To Me.gwdDatos.Columns.Count - 1
            xls.Write("<td>" & Me.gwdDatos.Columns.Item(columna).HeaderText.ToString() & "</td>")
        Next
        xls.WriteLine("</tr>")

        For fila As Integer = 0 To Me.gwdDatos.Rows.Count - 1
            xls.WriteLine("<tr>")
            For columna As Integer = 0 To Me.gwdDatos.Columns.Count - 1
                xls.Write("<td>" & Me.gwdDatos.Item(columna, fila).Value.ToString() & "</td>")
            Next
            xls.WriteLine("</tr>")
        Next

        xls.WriteLine("</TABLE>")

        xls.Flush()
        xls.Close()
        xls.Dispose()

        Help.ShowHelp(Me, archivo)

    End Sub

    Private Sub ButtonSpecAny3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSpecAny3.Click

        Try
            'Imprimiendo el control Web : Rutina Sobrecargada
            For Each obj As Control In Me.HeaderBrowser.Panel.Controls
                If TypeOf obj Is WebBrowser Then
                    CType(obj, Windows.Forms.WebBrowser).Print()
                End If
            Next
        Catch : End Try

    End Sub

    Private Sub Listar_Seguimiento_Flota()

        'Solo para flota Propia
        Dim objEntidad As New ENTIDADES.mantenimientos.TransportistaTracto
        Dim objTracto As New NEGOCIO.mantenimientos.Tracto_BLL
        Dim dtbDatos As New DataTable

        dtbDatos = objTracto.Listar_Tractos_Transportista_Propio(_CCMPN)

        Me.gwdDatos.DataSource = dtbDatos
        Me.gwdDatos.Columns(1).HeaderText = "PLACA"
        Me.gwdDatos.Columns(2).HeaderText = "LATITUD"
        Me.gwdDatos.Columns(3).HeaderText = "LONGITUD"


    End Sub

End Class