'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión del motor en tiempo de ejecución:2.0.50727.5483
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
Imports Db2Manager.RansaData.iSeries.DataObjects


'
'Este código fuente fue generado automáticamente por wsdl, Versión=2.0.50727.42.
'
Namespace ProxyRansa.AnulaRecepcion
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="SI_AnulaRecepcion_Ransa_OutBinding", [Namespace]:="http://vm.milpo.ransa.anularecepcion")>  _
    Partial Public Class SI_AnulaRecepcion_Ransa_OutService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private SI_AnulaRecepcion_Ransa_OutOperationCompleted As System.Threading.SendOrPostCallback
        
        '''<remarks/>
        Public Sub New()
            If ConfigurationWizard.Server = "RANSAT01" Or ConfigurationWizard.Server = "RANSA01" Then
                'If ConfigurationWizard.Library() = "DC@DESLIB" Then
                Me.Url = "http://integracion.gromero.com.pe/milpo/qas/AnulacionRecepcion"
                Me.Credentials = New System.Net.NetworkCredential("XMPIRANSA", "b)W$(Gf~%$3]S>i$RewV6[znpGX%9ip[zgQHqUY}")

            Else
                Me.Url = "http://integracion.gromero.com.pe/milpo/prd/AnulacionRecepcion"
                Me.Credentials = New System.Net.NetworkCredential("XMPIRANSA", "b)W$(Gf~%$3]S>i$RewV6[znpGX%9ip[zgQHqUY}")

            End If


        End Sub
        
        '''<remarks/>
        Public Event SI_AnulaRecepcion_Ransa_OutCompleted As SI_AnulaRecepcion_Ransa_OutCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Bare)>  _
        Public Function SI_AnulaRecepcion_Ransa_Out(<System.Xml.Serialization.XmlElementAttribute([Namespace]:="http://vm.milpo.ransa.anularecepcion")> ByVal MT_AnulaRecepcion_Ransa_Request As DT_AnulaRecepcion) As <System.Xml.Serialization.XmlArrayAttribute("MT_AnulaRecepcion_Ransa_Response", [Namespace]:="http://vm.milpo.ransa.anularecepcion"), System.Xml.Serialization.XmlArrayItemAttribute("Return", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable:=false)> DT_RespuestaAnulaRecepcionReturn()
            Dim results() As Object = Me.Invoke("SI_AnulaRecepcion_Ransa_Out", New Object() {MT_AnulaRecepcion_Ransa_Request})
            Return CType(results(0),DT_RespuestaAnulaRecepcionReturn())
        End Function
        
        '''<remarks/>
        Public Function BeginSI_AnulaRecepcion_Ransa_Out(ByVal MT_AnulaRecepcion_Ransa_Request As DT_AnulaRecepcion, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("SI_AnulaRecepcion_Ransa_Out", New Object() {MT_AnulaRecepcion_Ransa_Request}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndSI_AnulaRecepcion_Ransa_Out(ByVal asyncResult As System.IAsyncResult) As DT_RespuestaAnulaRecepcionReturn()
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),DT_RespuestaAnulaRecepcionReturn())
        End Function
        
        '''<remarks/>
        Public Overloads Sub SI_AnulaRecepcion_Ransa_OutAsync(ByVal MT_AnulaRecepcion_Ransa_Request As DT_AnulaRecepcion)
            Me.SI_AnulaRecepcion_Ransa_OutAsync(MT_AnulaRecepcion_Ransa_Request, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub SI_AnulaRecepcion_Ransa_OutAsync(ByVal MT_AnulaRecepcion_Ransa_Request As DT_AnulaRecepcion, ByVal userState As Object)
            If (Me.SI_AnulaRecepcion_Ransa_OutOperationCompleted Is Nothing) Then
                Me.SI_AnulaRecepcion_Ransa_OutOperationCompleted = AddressOf Me.OnSI_AnulaRecepcion_Ransa_OutOperationCompleted
            End If
            Me.InvokeAsync("SI_AnulaRecepcion_Ransa_Out", New Object() {MT_AnulaRecepcion_Ransa_Request}, Me.SI_AnulaRecepcion_Ransa_OutOperationCompleted, userState)
        End Sub
        
        Private Sub OnSI_AnulaRecepcion_Ransa_OutOperationCompleted(ByVal arg As Object)
            If (Not (Me.SI_AnulaRecepcion_Ransa_OutCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent SI_AnulaRecepcion_Ransa_OutCompleted(Me, New SI_AnulaRecepcion_Ransa_OutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
    End Class
    
    '''<comentarios/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://vm.milpo.ransa.anularecepcion")>  _
    Partial Public Class DT_AnulaRecepcion
        
        Private nUMBTOField As String
        
        Private nUMDOCField As String
        
        Private tINDField As String
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property NUMBTO() As String
            Get
                Return Me.nUMBTOField
            End Get
            Set
                Me.nUMBTOField = value
            End Set
        End Property
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property NUMDOC() As String
            Get
                Return Me.nUMDOCField
            End Get
            Set
                Me.nUMDOCField = value
            End Set
        End Property
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property TIND() As String
            Get
                Return Me.tINDField
            End Get
            Set
                Me.tINDField = value
            End Set
        End Property
    End Class
    
    '''<comentarios/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true, [Namespace]:="http://vm.milpo.ransa.anularecepcion")>  _
    Partial Public Class DT_RespuestaAnulaRecepcionReturn
        
        Private tYPEField As String
        
        Private mSGIDField As String
        
        Private nUMBERField As String
        
        Private mESSAGEField As String
        
        Private dOCNUMField As String
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property TYPE() As String
            Get
                Return Me.tYPEField
            End Get
            Set
                Me.tYPEField = value
            End Set
        End Property
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property MSGID() As String
            Get
                Return Me.mSGIDField
            End Get
            Set
                Me.mSGIDField = value
            End Set
        End Property
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType:="integer")>  _
        Public Property NUMBER() As String
            Get
                Return Me.nUMBERField
            End Get
            Set
                Me.nUMBERField = value
            End Set
        End Property
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property MESSAGE() As String
            Get
                Return Me.mESSAGEField
            End Get
            Set
                Me.mESSAGEField = value
            End Set
        End Property
        
        '''<comentarios/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property DOCNUM() As String
            Get
                Return Me.dOCNUMField
            End Get
            Set
                Me.dOCNUMField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
    Public Delegate Sub SI_AnulaRecepcion_Ransa_OutCompletedEventHandler(ByVal sender As Object, ByVal e As SI_AnulaRecepcion_Ransa_OutCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class SI_AnulaRecepcion_Ransa_OutCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As DT_RespuestaAnulaRecepcionReturn()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),DT_RespuestaAnulaRecepcionReturn())
            End Get
        End Property
    End Class
End Namespace
