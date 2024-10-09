Imports System.Data.SqlClient

Public Class frm_funcionarios


    Private connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"
    Private dt As DataTable

    Private Sub frm_funcionarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CarregarFuncionarios()
        carregar_campos3()

    End Sub

    Private Sub txt_buscaf_TextChanged(sender As Object, e As EventArgs) Handles txt_buscaf.TextChanged
        ' Obter o texto digitado pelo usuário
        Dim filtro As String = txt_buscaf.Text.Trim()

        ' Se o filtro estiver vazio, carrega todos os funcionários novamente
        If filtro = "" Then
            CarregarFuncionarios()
            Exit Sub
        End If

        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"

        Using con As New SqlConnection(connectionString)
            Try
                con.Open()

                ' Consulta para buscar funcionários pelo nome, nome de usuário OU cargo
                Dim query As String = "SELECT FuncionarioID, Nome, Usuario, Cargo " &
                                  "FROM Funcionarios " &
                                  "WHERE Nome LIKE @Filtro OR Usuario LIKE @Filtro OR Cargo LIKE @Filtro"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Filtro", "%" & filtro & "%")

                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)

                        ' Carrega os dados filtrados no DataGridView
                        dgv_funcionarios.DataSource = dt
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Erro ao buscar funcionários: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
            End Try
        End Using
    End Sub

    Private Sub CarregarFuncionarios()
        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"

        Using con As New SqlConnection(connectionString)
            Try
                con.Open()

                ' Consulta para carregar todos os funcionários
                Dim query As String = "SELECT FuncionarioID, Nome, Usuario, Cargo FROM Funcionarios"
                Using cmd As New SqlCommand(query, con)
                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)

                        ' Carrega todos os funcionários no DataGridView
                        dgv_funcionarios.DataSource = dt
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Erro ao carregar funcionários: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
            End Try
        End Using
    End Sub

    Private Sub dgv_funcionarios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_funcionarios.CellContentClick
        ' Verifica se o botão "Excluir" foi clicado
        If e.ColumnIndex = dgv_funcionarios.Columns("Excluir").Index AndAlso e.RowIndex >= 0 Then
            ' Obtém o ID do funcionário selecionado
            Dim funcionarioID As Integer = Convert.ToInt32(dgv_funcionarios.Rows(e.RowIndex).Cells("FuncionarioID").Value)

            ' Solicita confirmação antes de excluir
            Dim result As DialogResult = MessageBox.Show("Tem certeza de que deseja excluir este funcionário?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Exclui o funcionário do banco de dados
                ExcluirFuncionario(funcionarioID)

                ' Recarrega os funcionários no DataGridView
                CarregarFuncionarios()
            End If
        End If

        If e.ColumnIndex = dgv_funcionarios.Columns("Desativar").Index AndAlso e.RowIndex >= 0 Then

            ' Obtém o ID do funcionário selecionado
            Dim funcionarioID As Integer = Convert.ToInt32(dgv_funcionarios.Rows(e.RowIndex).Cells("FuncionarioID").Value)

            Dim result As DialogResult = MessageBox.Show("Tem certeza de que deseja desativar este funcionário?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Exclui o funcionário do banco de dados
                DesativarFuncionario(funcionarioID)

                ' Recarrega os funcionários no DataGridView
                CarregarFuncionarios()
            End If
        End If


        If e.ColumnIndex = dgv_funcionarios.Columns("Ativar").Index AndAlso e.RowIndex >= 0 Then

            ' Obtém o ID do funcionário selecionado
            Dim funcionarioID As Integer = Convert.ToInt32(dgv_funcionarios.Rows(e.RowIndex).Cells("FuncionarioID").Value)

            Dim result As DialogResult = MessageBox.Show("Tem certeza de que deseja ativar este funcionário?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Exclui o funcionário do banco de dados
                AtivarFuncionario(funcionarioID)

                ' Recarrega os funcionários no DataGridView
                CarregarFuncionarios()
            End If
        End If
    End Sub

    Private Sub DesativarFuncionario(funcionarioID As Integer)
        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"

        Using con As New SqlConnection(connectionString)
            Try
                con.Open()
                Dim query As String = "UPDATE Funcionarios SET Ativo = 0 WHERE FuncionarioID = @FuncionarioID"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@FuncionarioID", funcionarioID)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Funcionário desativado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            Catch ex As Exception
                MessageBox.Show("Erro ao desativar o funcionário: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub AtivarFuncionario(funcionarioID As Integer)
        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"

        Using con As New SqlConnection(connectionString)
            Try
                con.Open()
                Dim query As String = "UPDATE Funcionarios SET Ativo = 1 WHERE FuncionarioID = @FuncionarioID"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@FuncionarioID", funcionarioID)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Funcionário ativado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            Catch ex As Exception
                MessageBox.Show("Erro ao ativar o funcionário: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub



    Private Sub ExcluirFuncionario(funcionarioID As Integer)
        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"

        Using con As New SqlConnection(connectionString)
            Try
                con.Open()

                ' Comando SQL para excluir o funcionário pelo ID
                Dim query As String = "DELETE FROM Funcionarios WHERE FuncionarioID = @FuncionarioID"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@FuncionarioID", funcionarioID)

                    ' Executa o comando de exclusão
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Funcionário excluído com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Falha ao excluir o funcionário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Erro ao excluir o funcionário: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Chame a função que carrega os funcionários no DataGridView
        CarregarFuncionarios()

        ' Adicionar uma coluna de botão para exclusão, se ainda não estiver adicionada
        If dgv_funcionarios.Columns("Excluir") Is Nothing Then
            Dim btnExcluir As New DataGridViewButtonColumn()
            btnExcluir.Name = "Excluir"
            btnExcluir.HeaderText = "Excluir"
            btnExcluir.Text = "Excluir"
            btnExcluir.UseColumnTextForButtonValue = True ' Mostra o texto "Excluir" nos botões
            dgv_funcionarios.Columns.Add(btnExcluir)
        End If

        If dgv_funcionarios.Columns("Desativar") Is Nothing Then
            Dim btnDesativar As New DataGridViewButtonColumn()
            btnDesativar.Name = "Desativar"
            btnDesativar.HeaderText = "Desativar"
            btnDesativar.Text = "Desativar"
            btnDesativar.UseColumnTextForButtonValue = True ' Mostra o texto "Excluir" nos botões
            dgv_funcionarios.Columns.Add(btnDesativar)
        End If

        If dgv_funcionarios.Columns("Ativar") Is Nothing Then
            Dim btnAtivar As New DataGridViewButtonColumn()
            btnAtivar.Name = "Ativar"
            btnAtivar.HeaderText = "Ativar"
            btnAtivar.Text = "Ativar"
            btnAtivar.UseColumnTextForButtonValue = True ' Mostra o texto "Excluir" nos botões
            dgv_funcionarios.Columns.Add(btnAtivar)
        End If
    End Sub


End Class