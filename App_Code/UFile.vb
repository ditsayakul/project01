Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text
Imports System.Web.UI.HtmlControls
Imports System.Web

Public Class UFile

    Public Function WriteToXML(ByVal dt As DataTable, ByVal fileName As String) As Boolean
        'write the datatable to xml 
        Dim _chk As Integer
        Try
            Dim xml As String = "<?xml version=""1.0"" encoding=""UTF-8""?>"
            xml += "<pie></pie>"
            Dim doc As New XmlDocument()
            doc.LoadXml(xml)
            Dim xw As New XmlTextWriter(fileName, System.Text.Encoding.UTF8)
            Dim rootElement As XmlElement = doc.DocumentElement
            For Each row As DataRow In dt.Rows

                For i As Integer = 0 To dt.Columns.Count - 1
                    Dim colElement As XmlElement() = New XmlElement(dt.Columns.Count - 1) {}
                    'กำหนด Element 
                    colElement(i) = doc.CreateElement("slice")
                    'กำหนด Attribute
                    colElement(i).SetAttribute("title", Mid(row(0).ToString(), 2, row(0).ToString().Length()))

                    colElement(i).SetAttribute("color", row(2).ToString())

                    If i = 1 Then

                        colElement(i).InnerText = Fix(row(i).ToString())
                        rootElement.AppendChild(colElement(i))

                    End If

                Next

            Next
            doc.WriteTo(xw)
            xw.Close()

            _chk = 1
        Catch ex As Exception
            _chk = 0
        End Try

        If _chk = 1 Then
            Return True
        Else
            Return False
        End If

    End Function


    Public Function Exporttext(ByVal path As String, ByVal table As DataTable) As Boolean
        Dim output As New StreamWriter(path, False, UnicodeEncoding.Default)
        Dim _chk As Integer
        Try
            For Each row As DataRow In table.Rows
                For Each value As Object In row.ItemArray
                    output.Write(value)
                    'output.WriteLine()
                Next
                output.WriteLine()
            Next
            output.Close()

            _chk = 1
        Catch ex As Exception
            _chk = 0
        End Try

        If _chk = 1 Then
            Return True
        Else
            Return False
        End If

    End Function

    'Public Function Readtext(ByVal path As String) As String
    '    Dim StrWer As StreamReader

    '    Dim _error As Boolean
    '    Dim txt_er As String

    '    Try
    '        StrWer = File.OpenText(path)
    '        Do Until StrWer.EndOfStream
    '            'test = StrWer.ReadLine() & "<br>"
    '        Loop
    '        StrWer.Close()
    '        _error = True
    '        'test = "Files Writed."
    '    Catch ex As Exception
    '        _error = False
    '        'test = "Read failed. (" & ex.Message & ")"
    '    End Try
    '    If _error = True Then

    '        If IsNothing(path) Then
    '            txt_er = ""
    '        End If

    '        Return txt_er
    '    Else
    '        txt_er = "ไม่สามารถอ่านข้อมูลได้"
    '        Return txt_er
    '    End If

    'End Function

End Class



