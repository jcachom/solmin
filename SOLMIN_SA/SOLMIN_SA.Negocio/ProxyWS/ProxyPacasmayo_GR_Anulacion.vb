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
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

Namespace ProxyPacasmayo_GR_Anulacion
    '
    'Este código fuente fue generado automáticamente por wsdl, Versión=4.0.30319.1.
    '

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="ZWS_EMISION_ANULACION", [Namespace]:="urn:sap-com:document:sap:rfc:functions")> _
    Partial Public Class ZWS_EMISION_ANULACION
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol

        Private ZMMRFC_GRE_RANSA_ANULACIONOperationCompleted As System.Threading.SendOrPostCallback

        '''<remarks/>
        Public Sub New()
            MyBase.New()
            Me.Url = "http://cpcentcl.cpe.com:8001/sap/bc/srt/rfc/sap/zws_gre_ransa_anulacion/800/zws_e" & _
                "mision_anulacion/zws_emision_anulacion"
        End Sub

        '''<remarks/>
        Public Event ZMMRFC_GRE_RANSA_ANULACIONCompleted As ZMMRFC_GRE_RANSA_ANULACIONCompletedEventHandler

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sap-com:document:sap:rfc:functions:ZWS_GRE_RANSA_ANULACION:ZMMRFC_GRE_RANSA_A" & _
            "NULACIONRequest", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Bare)> _
        Public Function ZMMRFC_GRE_RANSA_ANULACION(<System.Xml.Serialization.XmlElementAttribute("ZMMRFC_GRE_RANSA_ANULACION", [Namespace]:="urn:sap-com:document:sap:rfc:functions")> ByVal ZMMRFC_GRE_RANSA_ANULACION1 As ZMMRFC_GRE_RANSA_ANULACION) As <System.Xml.Serialization.XmlElementAttribute("ZMMRFC_GRE_RANSA_ANULACIONResponse", [Namespace]:="urn:sap-com:document:sap:rfc:functions")> ZMMRFC_GRE_RANSA_ANULACIONResponse
            Dim results() As Object = Me.Invoke("ZMMRFC_GRE_RANSA_ANULACION", New Object() {ZMMRFC_GRE_RANSA_ANULACION1})
            Return CType(results(0), ZMMRFC_GRE_RANSA_ANULACIONResponse)
        End Function

        '''<remarks/>
        Public Function BeginZMMRFC_GRE_RANSA_ANULACION(ByVal ZMMRFC_GRE_RANSA_ANULACION1 As ZMMRFC_GRE_RANSA_ANULACION, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("ZMMRFC_GRE_RANSA_ANULACION", New Object() {ZMMRFC_GRE_RANSA_ANULACION1}, callback, asyncState)
        End Function

        '''<remarks/>
        Public Function EndZMMRFC_GRE_RANSA_ANULACION(ByVal asyncResult As System.IAsyncResult) As ZMMRFC_GRE_RANSA_ANULACIONResponse
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0), ZMMRFC_GRE_RANSA_ANULACIONResponse)
        End Function

        '''<remarks/>
        Public Overloads Sub ZMMRFC_GRE_RANSA_ANULACIONAsync(ByVal ZMMRFC_GRE_RANSA_ANULACION1 As ZMMRFC_GRE_RANSA_ANULACION)
            Me.ZMMRFC_GRE_RANSA_ANULACIONAsync(ZMMRFC_GRE_RANSA_ANULACION1, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub ZMMRFC_GRE_RANSA_ANULACIONAsync(ByVal ZMMRFC_GRE_RANSA_ANULACION1 As ZMMRFC_GRE_RANSA_ANULACION, ByVal userState As Object)
            If (Me.ZMMRFC_GRE_RANSA_ANULACIONOperationCompleted Is Nothing) Then
                Me.ZMMRFC_GRE_RANSA_ANULACIONOperationCompleted = AddressOf Me.OnZMMRFC_GRE_RANSA_ANULACIONOperationCompleted
            End If
            Me.InvokeAsync("ZMMRFC_GRE_RANSA_ANULACION", New Object() {ZMMRFC_GRE_RANSA_ANULACION1}, Me.ZMMRFC_GRE_RANSA_ANULACIONOperationCompleted, userState)
        End Sub

        Private Sub OnZMMRFC_GRE_RANSA_ANULACIONOperationCompleted(ByVal arg As Object)
            If (Not (Me.ZMMRFC_GRE_RANSA_ANULACIONCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ZMMRFC_GRE_RANSA_ANULACIONCompleted(Me, New ZMMRFC_GRE_RANSA_ANULACIONCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:sap-com:document:sap:rfc:functions")> _
    Partial Public Class ZMMRFC_GRE_RANSA_ANULACION

        Private i_MOTIVOField As String

        Private i_NRO_GREField As String

        Private i_RUC_EMIField As String

        Private i_TIP_DOCField As String

        Private t_RETUNRField() As BAPIRET2

        Private t_STATUSField() As ZTAB128

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property I_MOTIVO() As String
            Get
                Return Me.i_MOTIVOField
            End Get
            Set(value As String)
                Me.i_MOTIVOField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property I_NRO_GRE() As String
            Get
                Return Me.i_NRO_GREField
            End Get
            Set(value As String)
                Me.i_NRO_GREField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property I_RUC_EMI() As String
            Get
                Return Me.i_RUC_EMIField
            End Get
            Set(value As String)
                Me.i_RUC_EMIField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property I_TIP_DOC() As String
            Get
                Return Me.i_TIP_DOCField
            End Get
            Set(value As String)
                Me.i_TIP_DOCField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlArrayAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified), _
         System.Xml.Serialization.XmlArrayItemAttribute("item", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable:=False)> _
        Public Property T_RETUNR() As BAPIRET2()
            Get
                Return Me.t_RETUNRField
            End Get
            Set(value As BAPIRET2())
                Me.t_RETUNRField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlArrayAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified), _
         System.Xml.Serialization.XmlArrayItemAttribute("item", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable:=False)> _
        Public Property T_STATUS() As ZTAB128()
            Get
                Return Me.t_STATUSField
            End Get
            Set(value As ZTAB128())
                Me.t_STATUSField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <SYSTEM.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1"), _
     SYSTEM.SerializableAttribute(), _
     SYSTEM.Diagnostics.DebuggerStepThroughAttribute(), _
     SYSTEM.ComponentModel.DesignerCategoryAttribute("code"), _
     SYSTEM.Xml.Serialization.XmlTypeAttribute([Namespace]:="urn:sap-com:document:sap:rfc:functions")> _
    Partial Public Class BAPIRET2

        Private tYPEField As String

        Private idField As String

        Private nUMBERField As String

        Private mESSAGEField As String

        Private lOG_NOField As String

        Private lOG_MSG_NOField As String

        Private mESSAGE_V1Field As String

        Private mESSAGE_V2Field As String

        Private mESSAGE_V3Field As String

        Private mESSAGE_V4Field As String

        Private pARAMETERField As String

        Private rOWField As Integer

        Private fIELDField As String

        Private sYSTEMField As String

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property TYPE() As String
            Get
                Return Me.tYPEField
            End Get
            Set(value As String)
                Me.tYPEField = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property ID() As String
            Get
                Return Me.idField
            End Get
            Set(value As String)
                Me.idField = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property NUMBER() As String
            Get
                Return Me.nUMBERField
            End Get
            Set(value As String)
                Me.nUMBERField = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property MESSAGE() As String
            Get
                Return Me.mESSAGEField
            End Get
            Set(value As String)
                Me.mESSAGEField = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property LOG_NO() As String
            Get
                Return Me.lOG_NOField
            End Get
            Set(value As String)
                Me.lOG_NOField = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property LOG_MSG_NO() As String
            Get
                Return Me.lOG_MSG_NOField
            End Get
            Set(value As String)
                Me.lOG_MSG_NOField = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property MESSAGE_V1() As String
            Get
                Return Me.mESSAGE_V1Field
            End Get
            Set(value As String)
                Me.mESSAGE_V1Field = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property MESSAGE_V2() As String
            Get
                Return Me.mESSAGE_V2Field
            End Get
            Set(value As String)
                Me.mESSAGE_V2Field = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property MESSAGE_V3() As String
            Get
                Return Me.mESSAGE_V3Field
            End Get
            Set(value As String)
                Me.mESSAGE_V3Field = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property MESSAGE_V4() As String
            Get
                Return Me.mESSAGE_V4Field
            End Get
            Set(value As String)
                Me.mESSAGE_V4Field = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property PARAMETER() As String
            Get
                Return Me.pARAMETERField
            End Get
            Set(value As String)
                Me.pARAMETERField = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property ROW() As Integer
            Get
                Return Me.rOWField
            End Get
            Set(value As Integer)
                Me.rOWField = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property FIELD() As String
            Get
                Return Me.fIELDField
            End Get
            Set(value As String)
                Me.fIELDField = value
            End Set
        End Property

        '''<remarks/>
        <SYSTEM.Xml.Serialization.XmlElementAttribute(Form:=Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property SYSTEM() As String
            Get
                Return Me.sYSTEMField
            End Get
            Set(value As String)
                Me.sYSTEMField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="urn:sap-com:document:sap:rfc:functions")> _
    Partial Public Class ZTAB128

        Private waField As String

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> _
        Public Property WA() As String
            Get
                Return Me.waField
            End Get
            Set(value As String)
                Me.waField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:sap-com:document:sap:rfc:functions")> _
    Partial Public Class ZMMRFC_GRE_RANSA_ANULACIONResponse

        Private t_RETUNRField() As BAPIRET2

        Private t_STATUSField() As ZTAB128

        '''<remarks/>
        <System.Xml.Serialization.XmlArrayAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified), _
         System.Xml.Serialization.XmlArrayItemAttribute("item", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable:=False)> _
        Public Property T_RETUNR() As BAPIRET2()
            Get
                Return Me.t_RETUNRField
            End Get
            Set(value As BAPIRET2())
                Me.t_RETUNRField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlArrayAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified), _
         System.Xml.Serialization.XmlArrayItemAttribute("item", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable:=False)> _
        Public Property T_STATUS() As ZTAB128()
            Get
                Return Me.t_STATUSField
            End Get
            Set(value As ZTAB128())
                Me.t_STATUSField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")> _
    Public Delegate Sub ZMMRFC_GRE_RANSA_ANULACIONCompletedEventHandler(ByVal sender As Object, ByVal e As ZMMRFC_GRE_RANSA_ANULACIONCompletedEventArgs)

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code")> _
    Partial Public Class ZMMRFC_GRE_RANSA_ANULACIONCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As ZMMRFC_GRE_RANSA_ANULACIONResponse
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), ZMMRFC_GRE_RANSA_ANULACIONResponse)
            End Get
        End Property
    End Class
End Namespace