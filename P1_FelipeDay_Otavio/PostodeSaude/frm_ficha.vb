Imports System.Data.SqlClient

Public Class frm_ficha

    ' Variável para armazenar o ID do paciente e do funcionário
    ' Variáveis para armazenar o ID do paciente e do funcionário
    Private PacienteID As Integer
    Public FuncionarioID As Integer ' ID do funcionário autenticado
    Private connectionString As String = "Data Source=LAB5-11;Initial Catalog=SistemaVacinas;Integrated Security=True"

    ' Lista de vacinas disponíveis
    Private vacinasDisponiveis As New List(Of String) From {"BCG", "Hepatite B", "Febre Amarela", "Poliomielite", "DTP", "Sarampo"}

    ' Quando o formulário for carregado, busca o nome do funcionário


    ' Função para obter o nome do funcionário pelo ID
    Private Function ObterNomeFuncionario(funcionarioId As Integer) As String
        Dim nome As String = String.Empty
        Using con As New SqlConnection(connectionString)
            con.Open()
            Dim query As String = "SELECT Nome FROM Funcionarios WHERE FuncionarioID = @FuncionarioID"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@FuncionarioID", funcionarioId)
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        nome = dr("Nome").ToString()
                    End If
                End Using
            End Using
        End Using
        Return nome
    End Function
    Private Sub txt_nomef_LostFocus(sender As Object, e As EventArgs) Handles txt_nomef.LostFocus
        Using con As New SqlConnection(connectionString)
            Try
                con.Open()

                ' Consulta o paciente pelo nome completo
                Dim queryPaciente As String = "SELECT * FROM Pacientes WHERE NomeCompleto = @NomeCompleto"
                Using cmd As New SqlCommand(queryPaciente, con)
                    cmd.Parameters.AddWithValue("@NomeCompleto", txt_nomef.Text)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        If dr.HasRows Then
                            dr.Read()
                            PacienteID = dr("PacienteID")
                            txt_nomef.Text = dr("NomeCompleto").ToString()
                            img_fotof.Load(dr("FOTO").ToString())

                            ' Após carregar os dados do paciente, buscar as vacinas associadas
                            BuscarVacinas(PacienteID)
                        Else
                            MsgBox("Paciente não encontrado!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Atenção")
                            LimparCampos() ' Limpar campos quando o paciente não for encontrado
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Erro ao consultar paciente: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
                LimparCampos() ' Limpar campos em caso de erro
            End Try
        End Using ' A conexão é fechada automaticamente aqui
    End Sub

    ' Função para limpar todos os campos da interface
    Private Sub LimparCampos()
        txt_nomef.Text = ""
        img_fotof.Load(Application.StartupPath & "\imagem\nova_foto.png")
        chk_vacinas.Items.Clear() ' Limpa a lista de vacinas
    End Sub

    ' Função para buscar as vacinas do paciente e exibir na interface (CheckedListBox)
    Private Sub BuscarVacinas(PacienteID As Integer)
        Using con As New SqlConnection(connectionString)
            Try
                con.Open()

                ' Consulta as vacinas associadas ao PacienteID
                Dim queryVacinas As String = "SELECT Nome FROM Vacinas WHERE PacienteID = @PacienteID"
                Using cmd As New SqlCommand(queryVacinas, con)
                    cmd.Parameters.AddWithValue("@PacienteID", PacienteID)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        ' Limpar o CheckedListBox antes de carregar os dados
                        chk_vacinas.Items.Clear()

                        ' Adicionar todas as vacinas disponíveis ao CheckedListBox
                        For Each vacina In vacinasDisponiveis
                            chk_vacinas.Items.Add(vacina, False) ' Todas desmarcadas inicialmente
                        Next

                        ' Marcar as vacinas já tomadas pelo paciente
                        While dr.Read()
                            Dim vacinaTomada As String = dr("Nome").ToString()
                            For i As Integer = 0 To chk_vacinas.Items.Count - 1
                                If chk_vacinas.Items(i).ToString() = vacinaTomada Then
                                    chk_vacinas.SetItemChecked(i, True) ' Marca como tomada
                                End If
                            Next
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Erro ao consultar vacinas: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
            End Try
        End Using ' A conexão é fechada automaticamente aqui
    End Sub

    ' Evento do botão "Gravar" para salvar as vacinas selecionadas no banco de dados
    Private Sub btn_gravar_Click(sender As Object, e As EventArgs) Handles btn_gravar.Click
        If PacienteID = 0 Then
            MsgBox("Paciente não encontrado! Insira o nome corretamente.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Aviso")
            Exit Sub
        End If

        Using con As New SqlConnection(connectionString)
            Try
                con.Open()

                ' Deletar vacinas antigas do paciente (para atualizar as vacinas)
                Dim queryDeleteVacinas As String = "DELETE FROM Vacinas WHERE PacienteID = @PacienteID"
                Using cmd As New SqlCommand(queryDeleteVacinas, con)
                    cmd.Parameters.AddWithValue("@PacienteID", PacienteID)
                    cmd.ExecuteNonQuery()
                End Using

                ' Inserir as vacinas selecionadas no banco de dados
                For Each item As Object In chk_vacinas.CheckedItems
                    Dim queryInsertVacina As String = "INSERT INTO Vacinas (Nome, DataAplicacao, PacienteID, FuncionarioID) VALUES (@Nome, @DataAplicacao, @PacienteID, @FuncionarioID)"
                    Using cmd As New SqlCommand(queryInsertVacina, con)
                        cmd.Parameters.AddWithValue("@Nome", item.ToString())
                        cmd.Parameters.AddWithValue("@DataAplicacao", DateTime.Now)
                        cmd.Parameters.AddWithValue("@PacienteID", PacienteID)
                        cmd.Parameters.AddWithValue("@FuncionarioID", FuncionarioID)
                        cmd.ExecuteNonQuery()
                    End Using
                Next

                MsgBox("Dados e vacinas salvos com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Sucesso")
            Catch ex As Exception
                MsgBox("Erro ao gravar: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Erro")
            End Try
        End Using ' A conexão é fechada automaticamente aqui
    End Sub

    ' Função para inicializar a lista de vacinas ao carregar o formulário
    Private Sub frm_ficha_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Adicionar vacinas disponíveis ao CheckedListBox na inicialização
        For Each vacina In vacinasDisponiveis
            chk_vacinas.Items.Add(vacina, False)
        Next
        txt_nomeFuncionario.Text = "Funcionário responsável: " & ObterNomeFuncionario(FuncionarioID)
    End Sub

    Private Sub Txt_nomef_TextChanged(sender As Object, e As EventArgs) Handles txt_nomef.TextChanged

    End Sub
End Class