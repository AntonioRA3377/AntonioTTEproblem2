Imports MySql.Data.MySqlClient

Public Class Form1

    Dim connectionString As String = "server=localhost; userid=root; password=root; database=musicstudio"

    Public Sub LoadData()
        Try
            Using conn As New MySqlConnection(connectionString)
                Dim query As String = "SELECT * FROM tracks_tbl"
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView1.DataSource = table

                If DataGridView1.Columns.Contains("id") Then
                    DataGridView1.Columns("id").Visible = False
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.AddRange({"Pop", "Rock", "Jazz", "Hip Hop", "Classic"})
        LoadData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "INSERT INTO tracks_tbl(id, title, artist, genre, duration) VALUES (@id, @title, @artist, @genre, @duration)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", LabelID.Text)
                    cmd.Parameters.AddWithValue("@title", TextBox4.Text)
                    cmd.Parameters.AddWithValue("@artist", TextBox2.Text)
                    cmd.Parameters.AddWithValue("@genre", ComboBox1.Text)
                    cmd.Parameters.AddWithValue("@duration", TextBox3.Text)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MsgBox("Track added successfully!")
            LoadData()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "UPDATE tracks_tbl SET title=@title, artist=@artist, genre=@genre, duration=@duration WHERE id=@id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", LabelID.Text)
                    cmd.Parameters.AddWithValue("@title", TextBox4.Text)
                    cmd.Parameters.AddWithValue("@artist", TextBox2.Text)
                    cmd.Parameters.AddWithValue("@genre", ComboBox1.Text)
                    cmd.Parameters.AddWithValue("@duration", TextBox3.Text)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MsgBox("Track updated successfully!")
            LoadData()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "DELETE FROM tracks_tbl WHERE id=@id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", LabelID.Text)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            MsgBox("Track deleted successfully!")
            LoadData()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Using conn As New MySqlConnection(connectionString)
                Dim query As String = "SELECT * FROM tracks_tbl WHERE title LIKE @title"
                Dim adapter As New MySqlDataAdapter(query, conn)
                adapter.SelectCommand.Parameters.AddWithValue("@title", "%" & TextBox4.Text & "%")
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView1.DataSource = table
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                LabelID.Text = row.Cells("id").Value.ToString()
                TextBox4.Text = row.Cells("title").Value.ToString()
                TextBox2.Text = row.Cells("artist").Value.ToString()
                ComboBox1.Text = row.Cells("genre").Value.ToString()
                TextBox3.Text = row.Cells("duration").Value.ToString()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
