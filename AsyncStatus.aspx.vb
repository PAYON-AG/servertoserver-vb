Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Partial Class _Default
    Inherits System.Web.UI.Page
    Public Result As String = ""

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        getPaymentStatus("8a8294494ce19bdf014ce509f20b13e7")
    End Sub

    Private Sub getPaymentStatus(paymentId As String)
        Dim url As String = "https://test.oppwa.com/v1/payments/" + paymentId +
            "?authentication.userId=8a8294174b7ecb28014b9699220015cc" +
                    "&authentication.password=sy6KJsT8" +
                    "&authentication.entityId=8a8294174b7ecb28014b9699a3cf15d1"

        Dim request As WebRequest = WebRequest.Create(url)
        Dim webresponse As WebResponse = request.GetResponse()
        Dim dataStream As Stream = webresponse.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim response As String = reader.ReadToEnd()
        reader.Close()
        dataStream.Close()
        webresponse.Close()
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim dict As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(response)

        If dict("result")("code").StartsWith("000") Then
            Result = "SUCCESS &lt;br/>&lt;br/> Here is the result of your transaction: &lt;br/>&lt;br/>"
            Result += response
        Else
            Result = "ERROR &lt;br/>&lt;br/> Here is the result of your transaction: &lt;br/>&lt;br/>"
            Result += response
        End If
    End Sub
End Class
