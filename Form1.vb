Imports System.IO
Imports System.Net

Public Class Form1
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

        Dim opf As New OpenFileDialog
        opf.ShowDialog()
        Dim f As New FileInfo(opf.FileName)
        ProgressBar.Maximum = f.Length

        Dim IP As String = "Your IP - or - Your Website's Name"
        Dim USER As String = "FTP Username"
        Dim PASS As String = "FTP Passwprd"

        Dim request As FtpWebRequest = WebRequest.Create("ftp://" & IP & "/" & Guid.NewGuid.ToString & f.Extension)
        request.Credentials = New NetworkCredential(USER, PASS)
        request.Method = WebRequestMethods.Ftp.UploadFile

        Using fileStream As Stream = File.OpenRead(opf.FileName),
              ftpStream As Stream = request.GetRequestStream()
            Dim read As Integer
            Do
                Dim buffer() As Byte = New Byte(10240) {}
                read = fileStream.Read(buffer, 0, buffer.Length)
                If read > 0 Then
                    ftpStream.Write(buffer, 0, read)
                    ProgressBar.Value = fileStream.Position '
                End If
            Loop While read > 0
        End Using
        ProgressBar.Value = 0


    End Sub
End Class
