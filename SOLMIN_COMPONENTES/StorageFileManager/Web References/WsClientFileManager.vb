﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión del motor en tiempo de ejecución:2.0.50727.5466
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Data
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
 System.Web.Services.WebServiceBindingAttribute(Name:="WsClientFileManagerSoap", [Namespace]:="http://ransa.biz/")>  _
Partial Public Class WsClientFileManager
    Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
    
    Private getFolderAllOperationCompleted As System.Threading.SendOrPostCallback
    
    Private getSubFolderOperationCompleted As System.Threading.SendOrPostCallback
    
    Private getFileOperationCompleted As System.Threading.SendOrPostCallback
    
    Private createDirectoryOperationCompleted As System.Threading.SendOrPostCallback
    
    Private saveFileOperationCompleted As System.Threading.SendOrPostCallback
    
    Private deleteFileOperationCompleted As System.Threading.SendOrPostCallback
    
    Private modifyFileOperationCompleted As System.Threading.SendOrPostCallback
    
    Private getFileCountOperationCompleted As System.Threading.SendOrPostCallback
    
    '''<remarks/>
    Public Sub New()
        MyBase.New()
        ' Me.Url = "http://localhost:19075/SOLMINDOCS/WsClientFileManager.asmx"
        Me.Url = "http://ranmolapl01/DocumentosWeb/WsClientFileManager.asmx"
    End Sub
    
    '''<remarks/>
    Public Event getFolderAllCompleted As getFolderAllCompletedEventHandler
    
    '''<remarks/>
    Public Event getSubFolderCompleted As getSubFolderCompletedEventHandler
    
    '''<remarks/>
    Public Event getFileCompleted As getFileCompletedEventHandler
    
    '''<remarks/>
    Public Event createDirectoryCompleted As createDirectoryCompletedEventHandler
    
    '''<remarks/>
    Public Event saveFileCompleted As saveFileCompletedEventHandler
    
    '''<remarks/>
    Public Event deleteFileCompleted As deleteFileCompletedEventHandler
    
    '''<remarks/>
    Public Event modifyFileCompleted As modifyFileCompletedEventHandler
    
    '''<remarks/>
    Public Event getFileCountCompleted As getFileCountCompletedEventHandler
    
    '''<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ransa.biz/getFolderAll", RequestNamespace:="http://ransa.biz/", ResponseNamespace:="http://ransa.biz/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function getFolderAll(ByVal CCMPN As String, ByVal CCLNT As Integer) As System.Data.DataTable
        Dim results() As Object = Me.Invoke("getFolderAll", New Object() {CCMPN, CCLNT})
        Return CType(results(0),System.Data.DataTable)
    End Function
    
    '''<remarks/>
    Public Function BegingetFolderAll(ByVal CCMPN As String, ByVal CCLNT As Integer, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("getFolderAll", New Object() {CCMPN, CCLNT}, callback, asyncState)
    End Function
    
    '''<remarks/>
    Public Function EndgetFolderAll(ByVal asyncResult As System.IAsyncResult) As System.Data.DataTable
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),System.Data.DataTable)
    End Function
    
    '''<remarks/>
    Public Overloads Sub getFolderAllAsync(ByVal CCMPN As String, ByVal CCLNT As Integer)
        Me.getFolderAllAsync(CCMPN, CCLNT, Nothing)
    End Sub
    
    '''<remarks/>
    Public Overloads Sub getFolderAllAsync(ByVal CCMPN As String, ByVal CCLNT As Integer, ByVal userState As Object)
        If (Me.getFolderAllOperationCompleted Is Nothing) Then
            Me.getFolderAllOperationCompleted = AddressOf Me.OngetFolderAllOperationCompleted
        End If
        Me.InvokeAsync("getFolderAll", New Object() {CCMPN, CCLNT}, Me.getFolderAllOperationCompleted, userState)
    End Sub
    
    Private Sub OngetFolderAllOperationCompleted(ByVal arg As Object)
        If (Not (Me.getFolderAllCompletedEvent) Is Nothing) Then
            Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
            RaiseEvent getFolderAllCompleted(Me, New getFolderAllCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
        End If
    End Sub
    
    '''<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ransa.biz/getSubFolder", RequestNamespace:="http://ransa.biz/", ResponseNamespace:="http://ransa.biz/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function getSubFolder(ByVal CCMPN As String, ByVal CCLNT As Integer, ByVal IdFolder As Integer, ByVal pNameFile As String) As System.Data.DataSet
        Dim results() As Object = Me.Invoke("getSubFolder", New Object() {CCMPN, CCLNT, IdFolder, pNameFile})
        Return CType(results(0),System.Data.DataSet)
    End Function
    
    '''<remarks/>
    Public Function BegingetSubFolder(ByVal CCMPN As String, ByVal CCLNT As Integer, ByVal IdFolder As Integer, ByVal pNameFile As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("getSubFolder", New Object() {CCMPN, CCLNT, IdFolder, pNameFile}, callback, asyncState)
    End Function
    
    '''<remarks/>
    Public Function EndgetSubFolder(ByVal asyncResult As System.IAsyncResult) As System.Data.DataSet
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),System.Data.DataSet)
    End Function
    
    '''<remarks/>
    Public Overloads Sub getSubFolderAsync(ByVal CCMPN As String, ByVal CCLNT As Integer, ByVal IdFolder As Integer, ByVal pNameFile As String)
        Me.getSubFolderAsync(CCMPN, CCLNT, IdFolder, pNameFile, Nothing)
    End Sub
    
    '''<remarks/>
    Public Overloads Sub getSubFolderAsync(ByVal CCMPN As String, ByVal CCLNT As Integer, ByVal IdFolder As Integer, ByVal pNameFile As String, ByVal userState As Object)
        If (Me.getSubFolderOperationCompleted Is Nothing) Then
            Me.getSubFolderOperationCompleted = AddressOf Me.OngetSubFolderOperationCompleted
        End If
        Me.InvokeAsync("getSubFolder", New Object() {CCMPN, CCLNT, IdFolder, pNameFile}, Me.getSubFolderOperationCompleted, userState)
    End Sub
    
    Private Sub OngetSubFolderOperationCompleted(ByVal arg As Object)
        If (Not (Me.getSubFolderCompletedEvent) Is Nothing) Then
            Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
            RaiseEvent getSubFolderCompleted(Me, New getSubFolderCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
        End If
    End Sub
    
    '''<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ransa.biz/getFile", RequestNamespace:="http://ransa.biz/", ResponseNamespace:="http://ransa.biz/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function getFile(ByVal obj As FileData) As FileData
        Dim results() As Object = Me.Invoke("getFile", New Object() {obj})
        Return CType(results(0),FileData)
    End Function
    
    '''<remarks/>
    Public Function BegingetFile(ByVal obj As FileData, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("getFile", New Object() {obj}, callback, asyncState)
    End Function
    
    '''<remarks/>
    Public Function EndgetFile(ByVal asyncResult As System.IAsyncResult) As FileData
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),FileData)
    End Function
    
    '''<remarks/>
    Public Overloads Sub getFileAsync(ByVal obj As FileData)
        Me.getFileAsync(obj, Nothing)
    End Sub
    
    '''<remarks/>
    Public Overloads Sub getFileAsync(ByVal obj As FileData, ByVal userState As Object)
        If (Me.getFileOperationCompleted Is Nothing) Then
            Me.getFileOperationCompleted = AddressOf Me.OngetFileOperationCompleted
        End If
        Me.InvokeAsync("getFile", New Object() {obj}, Me.getFileOperationCompleted, userState)
    End Sub
    
    Private Sub OngetFileOperationCompleted(ByVal arg As Object)
        If (Not (Me.getFileCompletedEvent) Is Nothing) Then
            Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
            RaiseEvent getFileCompleted(Me, New getFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
        End If
    End Sub
    
    '''<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ransa.biz/createDirectory", RequestNamespace:="http://ransa.biz/", ResponseNamespace:="http://ransa.biz/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function createDirectory(ByVal objEntidad As FileData) As FileData
        Dim results() As Object = Me.Invoke("createDirectory", New Object() {objEntidad})
        Return CType(results(0),FileData)
    End Function
    
    '''<remarks/>
    Public Function BegincreateDirectory(ByVal objEntidad As FileData, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("createDirectory", New Object() {objEntidad}, callback, asyncState)
    End Function
    
    '''<remarks/>
    Public Function EndcreateDirectory(ByVal asyncResult As System.IAsyncResult) As FileData
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),FileData)
    End Function
    
    '''<remarks/>
    Public Overloads Sub createDirectoryAsync(ByVal objEntidad As FileData)
        Me.createDirectoryAsync(objEntidad, Nothing)
    End Sub
    
    '''<remarks/>
    Public Overloads Sub createDirectoryAsync(ByVal objEntidad As FileData, ByVal userState As Object)
        If (Me.createDirectoryOperationCompleted Is Nothing) Then
            Me.createDirectoryOperationCompleted = AddressOf Me.OncreateDirectoryOperationCompleted
        End If
        Me.InvokeAsync("createDirectory", New Object() {objEntidad}, Me.createDirectoryOperationCompleted, userState)
    End Sub
    
    Private Sub OncreateDirectoryOperationCompleted(ByVal arg As Object)
        If (Not (Me.createDirectoryCompletedEvent) Is Nothing) Then
            Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
            RaiseEvent createDirectoryCompleted(Me, New createDirectoryCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
        End If
    End Sub
    
    '''<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ransa.biz/saveFile", RequestNamespace:="http://ransa.biz/", ResponseNamespace:="http://ransa.biz/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function saveFile(ByVal objEntidad As FileData) As FileData
        Dim results() As Object = Me.Invoke("saveFile", New Object() {objEntidad})
        Return CType(results(0),FileData)
    End Function
    
    '''<remarks/>
    Public Function BeginsaveFile(ByVal objEntidad As FileData, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("saveFile", New Object() {objEntidad}, callback, asyncState)
    End Function
    
    '''<remarks/>
    Public Function EndsaveFile(ByVal asyncResult As System.IAsyncResult) As FileData
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),FileData)
    End Function
    
    '''<remarks/>
    Public Overloads Sub saveFileAsync(ByVal objEntidad As FileData)
        Me.saveFileAsync(objEntidad, Nothing)
    End Sub
    
    '''<remarks/>
    Public Overloads Sub saveFileAsync(ByVal objEntidad As FileData, ByVal userState As Object)
        If (Me.saveFileOperationCompleted Is Nothing) Then
            Me.saveFileOperationCompleted = AddressOf Me.OnsaveFileOperationCompleted
        End If
        Me.InvokeAsync("saveFile", New Object() {objEntidad}, Me.saveFileOperationCompleted, userState)
    End Sub
    
    Private Sub OnsaveFileOperationCompleted(ByVal arg As Object)
        If (Not (Me.saveFileCompletedEvent) Is Nothing) Then
            Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
            RaiseEvent saveFileCompleted(Me, New saveFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
        End If
    End Sub
    
    '''<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ransa.biz/deleteFile", RequestNamespace:="http://ransa.biz/", ResponseNamespace:="http://ransa.biz/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function deleteFile(ByVal objEntidad As FileData) As Boolean
        Dim results() As Object = Me.Invoke("deleteFile", New Object() {objEntidad})
        Return CType(results(0),Boolean)
    End Function
    
    '''<remarks/>
    Public Function BegindeleteFile(ByVal objEntidad As FileData, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("deleteFile", New Object() {objEntidad}, callback, asyncState)
    End Function
    
    '''<remarks/>
    Public Function EnddeleteFile(ByVal asyncResult As System.IAsyncResult) As Boolean
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),Boolean)
    End Function
    
    '''<remarks/>
    Public Overloads Sub deleteFileAsync(ByVal objEntidad As FileData)
        Me.deleteFileAsync(objEntidad, Nothing)
    End Sub
    
    '''<remarks/>
    Public Overloads Sub deleteFileAsync(ByVal objEntidad As FileData, ByVal userState As Object)
        If (Me.deleteFileOperationCompleted Is Nothing) Then
            Me.deleteFileOperationCompleted = AddressOf Me.OndeleteFileOperationCompleted
        End If
        Me.InvokeAsync("deleteFile", New Object() {objEntidad}, Me.deleteFileOperationCompleted, userState)
    End Sub
    
    Private Sub OndeleteFileOperationCompleted(ByVal arg As Object)
        If (Not (Me.deleteFileCompletedEvent) Is Nothing) Then
            Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
            RaiseEvent deleteFileCompleted(Me, New deleteFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
        End If
    End Sub
    
    '''<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ransa.biz/modifyFile", RequestNamespace:="http://ransa.biz/", ResponseNamespace:="http://ransa.biz/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function modifyFile(ByVal objEntidad As FileData) As Boolean
        Dim results() As Object = Me.Invoke("modifyFile", New Object() {objEntidad})
        Return CType(results(0),Boolean)
    End Function
    
    '''<remarks/>
    Public Function BeginmodifyFile(ByVal objEntidad As FileData, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("modifyFile", New Object() {objEntidad}, callback, asyncState)
    End Function
    
    '''<remarks/>
    Public Function EndmodifyFile(ByVal asyncResult As System.IAsyncResult) As Boolean
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),Boolean)
    End Function
    
    '''<remarks/>
    Public Overloads Sub modifyFileAsync(ByVal objEntidad As FileData)
        Me.modifyFileAsync(objEntidad, Nothing)
    End Sub
    
    '''<remarks/>
    Public Overloads Sub modifyFileAsync(ByVal objEntidad As FileData, ByVal userState As Object)
        If (Me.modifyFileOperationCompleted Is Nothing) Then
            Me.modifyFileOperationCompleted = AddressOf Me.OnmodifyFileOperationCompleted
        End If
        Me.InvokeAsync("modifyFile", New Object() {objEntidad}, Me.modifyFileOperationCompleted, userState)
    End Sub
    
    Private Sub OnmodifyFileOperationCompleted(ByVal arg As Object)
        If (Not (Me.modifyFileCompletedEvent) Is Nothing) Then
            Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
            RaiseEvent modifyFileCompleted(Me, New modifyFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
        End If
    End Sub
    
    '''<remarks/>
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ransa.biz/getFileCount", RequestNamespace:="http://ransa.biz/", ResponseNamespace:="http://ransa.biz/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
    Public Function getFileCount(ByVal objEntidad As FileData) As Decimal
        Dim results() As Object = Me.Invoke("getFileCount", New Object() {objEntidad})
        Return CType(results(0),Decimal)
    End Function
    
    '''<remarks/>
    Public Function BegingetFileCount(ByVal objEntidad As FileData, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
        Return Me.BeginInvoke("getFileCount", New Object() {objEntidad}, callback, asyncState)
    End Function
    
    '''<remarks/>
    Public Function EndgetFileCount(ByVal asyncResult As System.IAsyncResult) As Decimal
        Dim results() As Object = Me.EndInvoke(asyncResult)
        Return CType(results(0),Decimal)
    End Function
    
    '''<remarks/>
    Public Overloads Sub getFileCountAsync(ByVal objEntidad As FileData)
        Me.getFileCountAsync(objEntidad, Nothing)
    End Sub
    
    '''<remarks/>
    Public Overloads Sub getFileCountAsync(ByVal objEntidad As FileData, ByVal userState As Object)
        If (Me.getFileCountOperationCompleted Is Nothing) Then
            Me.getFileCountOperationCompleted = AddressOf Me.OngetFileCountOperationCompleted
        End If
        Me.InvokeAsync("getFileCount", New Object() {objEntidad}, Me.getFileCountOperationCompleted, userState)
    End Sub
    
    Private Sub OngetFileCountOperationCompleted(ByVal arg As Object)
        If (Not (Me.getFileCountCompletedEvent) Is Nothing) Then
            Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
            RaiseEvent getFileCountCompleted(Me, New getFileCountCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
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
 System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://ransa.biz/")>  _
Partial Public Class FileData
    
    Private idFileField As Integer
    
    Private idFolderField As Decimal
    
    Private idClienteField As Decimal
    
    Private idCompañiaField As String
    
    Private pathField As String
    
    Private fileNameField As String
    
    Private descripcionField As String
    
    Private usuarioField As String
    
    Private dataField() As Byte
    
    Private tamanioField As Decimal
    
    Private tipoFileField As String
    
    '''<comentarios/>
    Public Property IdFile() As Integer
        Get
            Return Me.idFileField
        End Get
        Set
            Me.idFileField = value
        End Set
    End Property
    
    '''<comentarios/>
    Public Property IdFolder() As Decimal
        Get
            Return Me.idFolderField
        End Get
        Set
            Me.idFolderField = value
        End Set
    End Property
    
    '''<comentarios/>
    Public Property IdCliente() As Decimal
        Get
            Return Me.idClienteField
        End Get
        Set
            Me.idClienteField = value
        End Set
    End Property
    
    '''<comentarios/>
    Public Property IdCompañia() As String
        Get
            Return Me.idCompañiaField
        End Get
        Set
            Me.idCompañiaField = value
        End Set
    End Property
    
    '''<comentarios/>
    Public Property Path() As String
        Get
            Return Me.pathField
        End Get
        Set
            Me.pathField = value
        End Set
    End Property
    
    '''<comentarios/>
    Public Property FileName() As String
        Get
            Return Me.fileNameField
        End Get
        Set
            Me.fileNameField = value
        End Set
    End Property
    
    '''<comentarios/>
    Public Property Descripcion() As String
        Get
            Return Me.descripcionField
        End Get
        Set
            Me.descripcionField = value
        End Set
    End Property
    
    '''<comentarios/>
    Public Property Usuario() As String
        Get
            Return Me.usuarioField
        End Get
        Set
            Me.usuarioField = value
        End Set
    End Property
    
    '''<comentarios/>
    <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")>  _
    Public Property Data() As Byte()
        Get
            Return Me.dataField
        End Get
        Set
            Me.dataField = value
        End Set
    End Property
    
    '''<comentarios/>
    Public Property Tamanio() As Decimal
        Get
            Return Me.tamanioField
        End Get
        Set
            Me.tamanioField = value
        End Set
    End Property
    
    '''<comentarios/>
    Public Property TipoFile() As String
        Get
            Return Me.tipoFileField
        End Get
        Set
            Me.tipoFileField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
Public Delegate Sub getFolderAllCompletedEventHandler(ByVal sender As Object, ByVal e As getFolderAllCompletedEventArgs)

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code")>  _
Partial Public Class getFolderAllCompletedEventArgs
    Inherits System.ComponentModel.AsyncCompletedEventArgs
    
    Private results() As Object
    
    Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
        MyBase.New(exception, cancelled, userState)
        Me.results = results
    End Sub
    
    '''<remarks/>
    Public ReadOnly Property Result() As System.Data.DataTable
        Get
            Me.RaiseExceptionIfNecessary
            Return CType(Me.results(0),System.Data.DataTable)
        End Get
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
Public Delegate Sub getSubFolderCompletedEventHandler(ByVal sender As Object, ByVal e As getSubFolderCompletedEventArgs)

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code")>  _
Partial Public Class getSubFolderCompletedEventArgs
    Inherits System.ComponentModel.AsyncCompletedEventArgs
    
    Private results() As Object
    
    Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
        MyBase.New(exception, cancelled, userState)
        Me.results = results
    End Sub
    
    '''<remarks/>
    Public ReadOnly Property Result() As System.Data.DataSet
        Get
            Me.RaiseExceptionIfNecessary
            Return CType(Me.results(0),System.Data.DataSet)
        End Get
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
Public Delegate Sub getFileCompletedEventHandler(ByVal sender As Object, ByVal e As getFileCompletedEventArgs)

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code")>  _
Partial Public Class getFileCompletedEventArgs
    Inherits System.ComponentModel.AsyncCompletedEventArgs
    
    Private results() As Object
    
    Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
        MyBase.New(exception, cancelled, userState)
        Me.results = results
    End Sub
    
    '''<remarks/>
    Public ReadOnly Property Result() As FileData
        Get
            Me.RaiseExceptionIfNecessary
            Return CType(Me.results(0),FileData)
        End Get
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
Public Delegate Sub createDirectoryCompletedEventHandler(ByVal sender As Object, ByVal e As createDirectoryCompletedEventArgs)

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code")>  _
Partial Public Class createDirectoryCompletedEventArgs
    Inherits System.ComponentModel.AsyncCompletedEventArgs
    
    Private results() As Object
    
    Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
        MyBase.New(exception, cancelled, userState)
        Me.results = results
    End Sub
    
    '''<remarks/>
    Public ReadOnly Property Result() As FileData
        Get
            Me.RaiseExceptionIfNecessary
            Return CType(Me.results(0),FileData)
        End Get
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
Public Delegate Sub saveFileCompletedEventHandler(ByVal sender As Object, ByVal e As saveFileCompletedEventArgs)

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code")>  _
Partial Public Class saveFileCompletedEventArgs
    Inherits System.ComponentModel.AsyncCompletedEventArgs
    
    Private results() As Object
    
    Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
        MyBase.New(exception, cancelled, userState)
        Me.results = results
    End Sub
    
    '''<remarks/>
    Public ReadOnly Property Result() As FileData
        Get
            Me.RaiseExceptionIfNecessary
            Return CType(Me.results(0),FileData)
        End Get
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
Public Delegate Sub deleteFileCompletedEventHandler(ByVal sender As Object, ByVal e As deleteFileCompletedEventArgs)

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code")>  _
Partial Public Class deleteFileCompletedEventArgs
    Inherits System.ComponentModel.AsyncCompletedEventArgs
    
    Private results() As Object
    
    Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
        MyBase.New(exception, cancelled, userState)
        Me.results = results
    End Sub
    
    '''<remarks/>
    Public ReadOnly Property Result() As Boolean
        Get
            Me.RaiseExceptionIfNecessary
            Return CType(Me.results(0),Boolean)
        End Get
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
Public Delegate Sub modifyFileCompletedEventHandler(ByVal sender As Object, ByVal e As modifyFileCompletedEventArgs)

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code")>  _
Partial Public Class modifyFileCompletedEventArgs
    Inherits System.ComponentModel.AsyncCompletedEventArgs
    
    Private results() As Object
    
    Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
        MyBase.New(exception, cancelled, userState)
        Me.results = results
    End Sub
    
    '''<remarks/>
    Public ReadOnly Property Result() As Boolean
        Get
            Me.RaiseExceptionIfNecessary
            Return CType(Me.results(0),Boolean)
        End Get
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")>  _
Public Delegate Sub getFileCountCompletedEventHandler(ByVal sender As Object, ByVal e As getFileCountCompletedEventArgs)

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code")>  _
Partial Public Class getFileCountCompletedEventArgs
    Inherits System.ComponentModel.AsyncCompletedEventArgs
    
    Private results() As Object
    
    Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
        MyBase.New(exception, cancelled, userState)
        Me.results = results
    End Sub
    
    '''<remarks/>
    Public ReadOnly Property Result() As Decimal
        Get
            Me.RaiseExceptionIfNecessary
            Return CType(Me.results(0),Decimal)
        End Get
    End Property
End Class
