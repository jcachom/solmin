﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión de runtime:4.0.30319.42000
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
'Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
'
Namespace WSmiqpowerplus
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="AddDataSoap", [Namespace]:="http://powerplus.miq.com/miqransaws")>  _
    Partial Public Class AddData
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private sendDataSetOperationCompleted As System.Threading.SendOrPostCallback
        
        Private sendDataSet1OperationCompleted As System.Threading.SendOrPostCallback
        
        Private sendDataSet2OperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetDataSetOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetDataSet1OperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetDataSet2OperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.SOLMIN_SC.Negocio.My.MySettings.Default.SOLMIN_SC_Negocio_WSmiqpowerplus_AddData
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event sendDataSetCompleted As sendDataSetCompletedEventHandler
        
        '''<remarks/>
        Public Event sendDataSet1Completed As sendDataSet1CompletedEventHandler
        
        '''<remarks/>
        Public Event sendDataSet2Completed As sendDataSet2CompletedEventHandler
        
        '''<remarks/>
        Public Event GetDataSetCompleted As GetDataSetCompletedEventHandler
        
        '''<remarks/>
        Public Event GetDataSet1Completed As GetDataSet1CompletedEventHandler
        
        '''<remarks/>
        Public Event GetDataSet2Completed As GetDataSet2CompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://powerplus.miq.com/miqransaws/AddAll", RequestElementName:="AddAll", RequestNamespace:="http://powerplus.miq.com/miqransaws", ResponseElementName:="AddAllResponse", ResponseNamespace:="http://powerplus.miq.com/miqransaws", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function sendDataSet(ByVal ds As System.Data.DataSet, ByVal _typeTable As typeTable) As <System.Xml.Serialization.XmlElementAttribute("AddAllResult")> String
            Dim results() As Object = Me.Invoke("sendDataSet", New Object() {ds, _typeTable})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub sendDataSetAsync(ByVal ds As System.Data.DataSet, ByVal _typeTable As typeTable)
            Me.sendDataSetAsync(ds, _typeTable, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub sendDataSetAsync(ByVal ds As System.Data.DataSet, ByVal _typeTable As typeTable, ByVal userState As Object)
            If (Me.sendDataSetOperationCompleted Is Nothing) Then
                Me.sendDataSetOperationCompleted = AddressOf Me.OnsendDataSetOperationCompleted
            End If
            Me.InvokeAsync("sendDataSet", New Object() {ds, _typeTable}, Me.sendDataSetOperationCompleted, userState)
        End Sub
        
        Private Sub OnsendDataSetOperationCompleted(ByVal arg As Object)
            If (Not (Me.sendDataSetCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent sendDataSetCompleted(Me, New sendDataSetCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.WebMethodAttribute(MessageName:="sendDataSet1"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://powerplus.miq.com/miqransaws/AddT005", RequestElementName:="AddT005", RequestNamespace:="http://powerplus.miq.com/miqransaws", ResponseElementName:="AddT005Response", ResponseNamespace:="http://powerplus.miq.com/miqransaws", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function sendDataSet(ByVal ds As System.Data.DataSet, ByVal _typeTable As typeTable, ByVal _CCOD As CCOD) As <System.Xml.Serialization.XmlElementAttribute("AddT005Result")> String
            Dim results() As Object = Me.Invoke("sendDataSet1", New Object() {ds, _typeTable, _CCOD})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub sendDataSet1Async(ByVal ds As System.Data.DataSet, ByVal _typeTable As typeTable, ByVal _CCOD As CCOD)
            Me.sendDataSet1Async(ds, _typeTable, _CCOD, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub sendDataSet1Async(ByVal ds As System.Data.DataSet, ByVal _typeTable As typeTable, ByVal _CCOD As CCOD, ByVal userState As Object)
            If (Me.sendDataSet1OperationCompleted Is Nothing) Then
                Me.sendDataSet1OperationCompleted = AddressOf Me.OnsendDataSet1OperationCompleted
            End If
            Me.InvokeAsync("sendDataSet1", New Object() {ds, _typeTable, _CCOD}, Me.sendDataSet1OperationCompleted, userState)
        End Sub
        
        Private Sub OnsendDataSet1OperationCompleted(ByVal arg As Object)
            If (Not (Me.sendDataSet1CompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent sendDataSet1Completed(Me, New sendDataSet1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.WebMethodAttribute(MessageName:="sendDataSet2"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://powerplus.miq.com/miqransaws/UpdateCustomTable", RequestElementName:="UpdateCustomTable", RequestNamespace:="http://powerplus.miq.com/miqransaws", ResponseElementName:="UpdateCustomTableResponse", ResponseNamespace:="http://powerplus.miq.com/miqransaws", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function sendDataSet(ByVal ds As System.Data.DataSet, ByVal _typeTable As typeTable, ByVal fieldsCustom As String) As <System.Xml.Serialization.XmlElementAttribute("UpdateCustomTableResult")> String
            Dim results() As Object = Me.Invoke("sendDataSet2", New Object() {ds, _typeTable, fieldsCustom})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub sendDataSet2Async(ByVal ds As System.Data.DataSet, ByVal _typeTable As typeTable, ByVal fieldsCustom As String)
            Me.sendDataSet2Async(ds, _typeTable, fieldsCustom, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub sendDataSet2Async(ByVal ds As System.Data.DataSet, ByVal _typeTable As typeTable, ByVal fieldsCustom As String, ByVal userState As Object)
            If (Me.sendDataSet2OperationCompleted Is Nothing) Then
                Me.sendDataSet2OperationCompleted = AddressOf Me.OnsendDataSet2OperationCompleted
            End If
            Me.InvokeAsync("sendDataSet2", New Object() {ds, _typeTable, fieldsCustom}, Me.sendDataSet2OperationCompleted, userState)
        End Sub
        
        Private Sub OnsendDataSet2OperationCompleted(ByVal arg As Object)
            If (Not (Me.sendDataSet2CompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent sendDataSet2Completed(Me, New sendDataSet2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://powerplus.miq.com/miqransaws/GetDataAll", RequestElementName:="GetDataAll", RequestNamespace:="http://powerplus.miq.com/miqransaws", ResponseElementName:="GetDataAllResponse", ResponseNamespace:="http://powerplus.miq.com/miqransaws", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function GetDataSet(ByVal _typeTable As typeTable) As <System.Xml.Serialization.XmlElementAttribute("GetDataAllResult")> System.Data.DataSet
            Dim results() As Object = Me.Invoke("GetDataSet", New Object() {_typeTable})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetDataSetAsync(ByVal _typeTable As typeTable)
            Me.GetDataSetAsync(_typeTable, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetDataSetAsync(ByVal _typeTable As typeTable, ByVal userState As Object)
            If (Me.GetDataSetOperationCompleted Is Nothing) Then
                Me.GetDataSetOperationCompleted = AddressOf Me.OnGetDataSetOperationCompleted
            End If
            Me.InvokeAsync("GetDataSet", New Object() {_typeTable}, Me.GetDataSetOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetDataSetOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetDataSetCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetDataSetCompleted(Me, New GetDataSetCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.WebMethodAttribute(MessageName:="GetDataSet1"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://powerplus.miq.com/miqransaws/GetDataWhere", RequestElementName:="GetDataWhere", RequestNamespace:="http://powerplus.miq.com/miqransaws", ResponseElementName:="GetDataWhereResponse", ResponseNamespace:="http://powerplus.miq.com/miqransaws", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function GetDataSet(ByVal _typeTable As typeTable, ByVal whereValue As String) As <System.Xml.Serialization.XmlElementAttribute("GetDataWhereResult")> System.Data.DataSet
            Dim results() As Object = Me.Invoke("GetDataSet1", New Object() {_typeTable, whereValue})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetDataSet1Async(ByVal _typeTable As typeTable, ByVal whereValue As String)
            Me.GetDataSet1Async(_typeTable, whereValue, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetDataSet1Async(ByVal _typeTable As typeTable, ByVal whereValue As String, ByVal userState As Object)
            If (Me.GetDataSet1OperationCompleted Is Nothing) Then
                Me.GetDataSet1OperationCompleted = AddressOf Me.OnGetDataSet1OperationCompleted
            End If
            Me.InvokeAsync("GetDataSet1", New Object() {_typeTable, whereValue}, Me.GetDataSet1OperationCompleted, userState)
        End Sub
        
        Private Sub OnGetDataSet1OperationCompleted(ByVal arg As Object)
            If (Not (Me.GetDataSet1CompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetDataSet1Completed(Me, New GetDataSet1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.WebMethodAttribute(MessageName:="GetDataSet2"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://powerplus.miq.com/miqransaws/GetDataKey", RequestElementName:="GetDataKey", RequestNamespace:="http://powerplus.miq.com/miqransaws", ResponseElementName:="GetDataKeyResponse", ResponseNamespace:="http://powerplus.miq.com/miqransaws", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function GetDataSet(ByVal _typeTable As typeTable, ByVal key As String, ByVal value As String) As <System.Xml.Serialization.XmlElementAttribute("GetDataKeyResult")> System.Data.DataSet
            Dim results() As Object = Me.Invoke("GetDataSet2", New Object() {_typeTable, key, value})
            Return CType(results(0),System.Data.DataSet)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetDataSet2Async(ByVal _typeTable As typeTable, ByVal key As String, ByVal value As String)
            Me.GetDataSet2Async(_typeTable, key, value, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetDataSet2Async(ByVal _typeTable As typeTable, ByVal key As String, ByVal value As String, ByVal userState As Object)
            If (Me.GetDataSet2OperationCompleted Is Nothing) Then
                Me.GetDataSet2OperationCompleted = AddressOf Me.OnGetDataSet2OperationCompleted
            End If
            Me.InvokeAsync("GetDataSet2", New Object() {_typeTable, key, value}, Me.GetDataSet2OperationCompleted, userState)
        End Sub
        
        Private Sub OnGetDataSet2OperationCompleted(ByVal arg As Object)
            If (Not (Me.GetDataSet2CompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetDataSet2Completed(Me, New GetDataSet2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3062.0"),  _
     System.SerializableAttribute(),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://powerplus.miq.com/miqransaws")>  _
    Public Enum typeTable
        
        '''<remarks/>
        m001
        
        '''<remarks/>
        m002
        
        '''<remarks/>
        m003
        
        '''<remarks/>
        m004
        
        '''<remarks/>
        m005
        
        '''<remarks/>
        m006
        
        '''<remarks/>
        m007
        
        '''<remarks/>
        m008
        
        '''<remarks/>
        m009
        
        '''<remarks/>
        m010
        
        '''<remarks/>
        m011
        
        '''<remarks/>
        m012
        
        '''<remarks/>
        t001
        
        '''<remarks/>
        t002
        
        '''<remarks/>
        t003
        
        '''<remarks/>
        t004
        
        '''<remarks/>
        t005
        
        '''<remarks/>
        t006
        
        '''<remarks/>
        t007
        
        '''<remarks/>
        t008
        
        '''<remarks/>
        t009
        
        '''<remarks/>
        t010
        
        '''<remarks/>
        t011
        
        '''<remarks/>
        t012
        
        '''<remarks/>
        t013
        
        '''<remarks/>
        m013
    End Enum
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3062.0"),  _
     System.SerializableAttribute(),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://powerplus.miq.com/miqransaws")>  _
    Public Enum CCOD
        
        '''<remarks/>
        YRCLOGISTIC
        
        '''<remarks/>
        RANSA
        
        '''<remarks/>
        ALL
    End Enum
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")>  _
    Public Delegate Sub sendDataSetCompletedEventHandler(ByVal sender As Object, ByVal e As sendDataSetCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class sendDataSetCompletedEventArgs
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
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")>  _
    Public Delegate Sub sendDataSet1CompletedEventHandler(ByVal sender As Object, ByVal e As sendDataSet1CompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class sendDataSet1CompletedEventArgs
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
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")>  _
    Public Delegate Sub sendDataSet2CompletedEventHandler(ByVal sender As Object, ByVal e As sendDataSet2CompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class sendDataSet2CompletedEventArgs
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
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")>  _
    Public Delegate Sub GetDataSetCompletedEventHandler(ByVal sender As Object, ByVal e As GetDataSetCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetDataSetCompletedEventArgs
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")>  _
    Public Delegate Sub GetDataSet1CompletedEventHandler(ByVal sender As Object, ByVal e As GetDataSet1CompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetDataSet1CompletedEventArgs
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
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")>  _
    Public Delegate Sub GetDataSet2CompletedEventHandler(ByVal sender As Object, ByVal e As GetDataSet2CompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetDataSet2CompletedEventArgs
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
End Namespace
