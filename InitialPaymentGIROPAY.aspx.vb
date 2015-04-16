﻿Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Partial Class _Default
    Inherits System.Web.UI.Page
    Public responseData As String = ""

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        responseData = initialPayment()("result")("description")
    End Sub

    Public Function initialPayment() As Dictionary(Of String, Object)
        Dim url As String = "https://test.oppwa.com/v1/payments"
        Dim data As String = "authentication.userId=8a8294174b7ecb28014b9699220015cc" +
                    "&authentication.password=sy6KJsT8" +
                    "&authentication.entityId=8a8294184c0378fe014c046e80340da9" +
                    "&amount=92.12" +
                    "&currency=EUR" +
                    "&paymentBrand=GIROPAY" +
                    "&paymentType=PA" +
                    "&bankAccount.bic=TESTDETT421" +
                    "&bankAccount.iban=DE14940593100000012346" +
                    "&bankAccount.country=DE" +
                    "&shopperResultUrl=https://docs.oppwa.com"

        Dim request As WebRequest = WebRequest.Create(url)
        request.Method = "POST"
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(data)
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = byteArray.Length
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()
        Dim webresponse As WebResponse = request.GetResponse()
        dataStream = webresponse.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim response As String = reader.ReadToEnd()
        reader.Close()
        dataStream.Close()
        webresponse.Close()
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim dict As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(response)

        Return dict
    End Function
End Class
