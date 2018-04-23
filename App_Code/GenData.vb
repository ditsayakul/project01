Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Web.Configuration
Imports System.Xml.Serialization
Imports System.Xml
Imports Microsoft.VisualBasic
Imports Oracle.DataAccess.Client
Imports System.Data.Odbc

Public Class GenData

    Dim Ufile As UFile

    Private _sql As String
    Private _path As String
    Private conn As New OracleConnection(System.Configuration.ConfigurationManager.AppSettings("Oraconn"))
    Private connMysql As New OdbcConnection(System.Configuration.ConfigurationManager.AppSettings("Mysqlcon"))

    Public Function gentext(ByVal path As String, ByVal sqltext As String) As Boolean

        Dim strSQL As String = sqltext
        Dim cmd As New OracleDataAdapter(strSQL, conn)
        conn.Open()

        Dim DT As DataTable = New DataTable()
        Dim pathtext As String = path
        Dim Ufile As New UFile
        cmd.Fill(DT)

        Dim _chk As Boolean = Ufile.Exporttext(pathtext, DT)
        conn.Close()
        Return _chk

    End Function

    Public Function gentextMysql(ByVal path As String, ByVal sqltext As String) As Boolean

        Dim strSQL As String = sqltext
        Dim dtReader As OdbcDataReader
        Dim objCmd As OdbcCommand = New OdbcCommand(strSQL, connMysql)
        connMysql.Open()
        dtReader = objCmd.ExecuteReader()

        Dim DT As DataTable = New DataTable()
        Dim pathtext As String = path
        Dim Ufile As New UFile
        DT.Load(dtReader)

        Dim _chk As Boolean = Ufile.Exporttext(pathtext, DT)
        connMysql.Close()
        Return _chk

    End Function


    Public Function genxml(ByVal path As String, ByVal sqltext As String) As Boolean

        Dim strSQL As String = sqltext
        Dim cmd As New OracleDataAdapter(strSQL, conn)
        conn.Open()

        Dim DT As DataTable = New DataTable()
        Dim pathtext As String = path
        Dim Ufile As New UFile
        cmd.Fill(DT)

        Dim _chk As Boolean = Ufile.WriteToXML(DT, pathtext)
        conn.Close()
        Return _chk

    End Function

    Public Function genvalue(ByVal sqltext As String) As String

        Dim strSQL As String = sqltext
        Dim cmd As New OracleDataAdapter(strSQL, conn)
        Dim value As String

        conn.Open()
        'Dim myReaderit As SqlClient.SqlDataReader = cmd.ExecuteReader()
        'If myReaderit.Read() = True Then
        '    value = myReaderit.GetValue(0)
        Return value
        'End If

        conn.Close()
    End Function

    Public Property sql() As String
        Get
            Return _sql
        End Get
        Set(ByVal value As String)
            _sql = value
        End Set
    End Property

    Public Property path() As String
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property
End Class
