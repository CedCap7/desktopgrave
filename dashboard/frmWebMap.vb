Imports Microsoft.Web.WebView2.WinForms
Imports System.Runtime.InteropServices

Public Class frmWebMap
    Private Async Sub frmWebMap_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize WebView2
        Await InitializeAsync()
    End Sub

    Private Async Function InitializeAsync() As Task
        ' Initialize WebView2
        Await WebView21.EnsureCoreWebView2Async()

        ' Add the JavaScript interface
        AddHandler WebView21.CoreWebView2.WebMessageReceived, AddressOf HandleWebMessage

        ' Navigate to the map page
        WebView21.CoreWebView2.Navigate("https://doncarloscemetery.io/")
    End Function

    Private Sub HandleWebMessage(sender As Object, e As Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs)
        Try
            ' Process the plot data received from JavaScript
            Dim plotDataJson As String = e.WebMessageAsJson
            MessageBox.Show("Plot selected: " & plotDataJson)
        Catch ex As Exception
            MessageBox.Show("Error processing plot data: " & ex.Message)
        End Try
    End Sub

    Private Sub btnRefreshMap_Click(sender As Object, e As EventArgs) Handles btnRefreshMap.Click
        ' Reload the map
        If WebView21.CoreWebView2 IsNot Nothing Then
            WebView21.CoreWebView2.Reload()
        End If
    End Sub

    Private Sub WebView21_Click(sender As Object, e As EventArgs) Handles WebView21.Click

    End Sub
End Class