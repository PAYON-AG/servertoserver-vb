Imports System
Imports System.IO
Imports System.Net
Imports System.Text

Partial Class _Default
    Inherits System.Web.UI.Page
    Public responseData As String = ""

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        responseData = deleteRegistration()("result")("description")
    End Sub

    Public Function deleteRegistration() As Dictionary(Of String, Object)
        Dim url As String = "https://test.oppwa.com/v1/registrations/8a82944a4d05ab2b014d05f1321e0dc5" +
                    "?authentication.userId=8a8294174b7ecb28014b9699220015cc" +
                    "&authentication.password=sy6KJsT8" +
                    "&authentication.entityId=8a8294174b7ecb28014b9699a3cf15d1"

        Dim request As WebRequest = WebRequest.Create(url)
        request.Method = "DELETE"
        request.ContentType = "application/x-www-form-urlencoded"
        Dim dataStream As Stream = request.GetRequestStream()
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
