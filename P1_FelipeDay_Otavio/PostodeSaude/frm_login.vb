Imports System.Data.SqlClient

Public Class frm_login

    Private connectionString As String = "Data Source=LAB5-11;Initial Catalog=SistemaVacinas;Integrated Security=True"

    Private Sub frm_login_Load(sender As Object, e As EventArgs) Handles Me.Load
        conecta_banco()
        carregar_campos()
    End Sub

    Private Sub bnt_entrar_Click(sender As Object, e As EventArgs) Handles bnt_entrar.Click
        Dim usuario As String = txt_usuario.Text.Trim()
        Dim senha As String = txt_senha.Text.Trim()
        Dim cargo As String = cmb_login.Text.Trim()

        ' Valida a entrada
        If ValidarEntrada(usuario, senha, cargo) Then
            ' Chama a função de autenticação
            Dim funcionarioID As Integer = 0
            Dim tipoUsuario As String = String.Empty

            If AutenticarFuncionario(usuario, senha, funcionarioID, tipoUsuario) Then
                ' Se a autenticação for bem-sucedida, abre o Form1
                Dim form1 As New Form1()
                form1.FuncionarioID = funcionarioID ' Passa o ID do funcionário para o Form1
                form1.TipoUsuario = tipoUsuario ' Passa o tipo de usuário para o Form1
                form1.Show()
                Me.Hide() ' Esconde o formulário de login
            Else
                ' Notifica o usuário se as credenciais estiverem incorretas
                MessageBox.Show("Nome de usuário ou senha incorretos. Tente novamente.", "Erro de Autenticação", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

    End Sub



    Function ValidarEntrada(usuario As String, senha As String, cargo As String) As Boolean
        If String.IsNullOrWhiteSpace(usuario) Then
            MessageBox.Show("O nome de usuário é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If String.IsNullOrWhiteSpace(senha) Then
            MessageBox.Show("A senha é obrigatória.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If



        Return True
    End Function

    Private Function AutenticarFuncionario(usuario As String, senha As String, ByRef funcionarioID As Integer, ByRef tipoUsuario As String) As Boolean
        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"

        Using con As New SqlConnection(connectionString)
            con.Open()

            Dim query As String = "SELECT FuncionarioID, Cargo, Ativo FROM Funcionarios WHERE Usuario = @Usuario AND Senha = @Senha"

            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Usuario", usuario)
                cmd.Parameters.AddWithValue("@Senha", senha)

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        funcionarioID = Convert.ToInt32(dr("FuncionarioID")) ' Obtém o ID do funcionário
                        tipoUsuario = dr("Cargo").ToString() ' Obtém o cargo do funcionário

                        ' Verifica se o funcionário está ativo
                        Dim ativo As Boolean = If(dr("Ativo") IsNot DBNull.Value, Convert.ToBoolean(dr("Ativo")), False)

                        If ativo Then
                            Return True ' Autenticação bem-sucedida
                        Else
                            MessageBox.Show("Funcionário desativado. Não é possível fazer login.", "Erro de Autenticação", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False ' Funcionário desativado
                        End If
                    End If
                End Using
            End Using
        End Using
        Return False
    End Function


End Class
