'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión del motor en tiempo de ejecución:2.0.50727.3634
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'Este código fuente fue generado automáticamente por wsdl, Versión=2.0.50727.42.
'

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Web.Services.WebServiceBindingAttribute(Name:="ws_hoc_output_grSoap", [Namespace]:="http://ransa.net/")>  _
Partial Public Class ws_hoc_output_gr
    Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
    
    Private consulta_guia_trasladoOperationCompleted As System.Threading.SendOrPostCallback
    
    '''<remarks/>
    Public Sub New()
        MyBase.New
        Me.Url = "http://secure.ransa.net/wssolmin/hoc/ws_hoc_output_gr.asmx"
    End Sub
    
    '''<remarks/>
    Public Event consulta_guia_trasladoCompleted As consulta_guia_trasladoCompletedEventHandler
    
    '''<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ransa.net/consulta_guia_traslado", RequestNamespace:="http://ransa.net/", ResponseNamespace:="http://ransa.net/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function consulta_guia_traslado(ByVal usuario As String, ByVal password As String, ByVal Codcliente As String, ByVal Nguia As String, ByVal Tipo As Integer) As String
        Dim results() As Object = Me.Invoke("consulta_guia_traslado", New Object() {usuario, password, Codcliente, Nguia, Tipo})
        Return CType(results(0),String)
    End Function
    
    '''<remarks/>
    Public Function Beginconsulta_guia_traslado(ByVal usuario As String, ByVal password As String, ByVal Codcliente As String, ByVal Nguia As String, ByVal Tipo As Integer, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("consulta_guia_traslado", New Object() {usuario, password, Codcliente, Nguia, Tipo}, callback, asyncState)
    End Function
    
    '''<remarks/>
    Public Function Endconsulta_guia_traslado(ByVal asyncResult As System.IAsyncResult) As String
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),String)
    End Function
    
    '''<remarks/>
    Public Overloads Sub consulta_guia_trasladoAsync(ByVal usuario As String, ByVal password As String, ByVal Codcliente As String, ByVal Nguia As String, ByVal Tipo As Integer)
        Me.consulta_guia_trasladoAsync(usuario, password, Codcliente, Nguia, Tipo, Nothing)
    End Sub
    
    '''<remarks/>
    Public Overloads Sub consulta_guia_trasladoAsync(ByVal usuario As String, ByVal password As String, ByVal Codcliente As String, ByVal Nguia As String, ByVal Tipo As Integer, ByVal userState As Object)
        If (Me.consulta_guia_trasladoOperationCompleted Is Nothing) Then
            Me.consulta_guia_trasladoOperationCompleted = AddressOf Me.Onconsulta_guia_trasladoOperationCompleted
        End If
        Me.InvokeAsync("consulta_guia_traslado", New Object() {usuario, password, Codcliente, Nguia, Tipo}, Me.consulta_guia_trasladoOperationCompleted, userState)
    End Sub
    
    Private Sub Onconsulta_guia_trasladoOperationCompleted(ByVal arg As Object)
        If (Not (Me.consulta_guia_trasladoCompletedEvent) Is Nothing) Then
            Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
            RaiseEvent consulta_guia_trasladoCompleted(Me, New consulta_guia_trasladoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
        End If
    End Sub
    
    '''<remarks/>
    Public Shadows Sub CancelAsync(ByVal userState As Object)
        MyBase.CancelAsync(userState)
    End Sub
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
Public Delegate Sub consulta_guia_trasladoCompletedEventHandler(ByVal sender As Object, ByVal e As consulta_guia_trasladoCompletedEventArgs)

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code")>  _
Partial Public Class consulta_guia_trasladoCompletedEventArgs
    Inherits System.ComponentModel.AsyncCompletedEventArgs
    
    Private results() As Object
    
    Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
        MyBase.New(exception, cancelled, userState)
        Me.results = results
    End Sub
    
    '''<remarks/>
    Public ReadOnly Property Result() As String
        Get
            Me.RaiseExceptionIfNecessary
            Return CType(Me.results(0),String)
        End Get
    End Property
End Class
