Imports System.Data.SqlClient

Public Class frm_cadpacientes

    ' Definimos a variável para a conexão SQL Server
    Dim db As SqlConnection
    Private modoEdicao As Boolean = False

    Private Sub frm_cadpaciente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CarregarDadosPacientes()
        carregar_campos2()

    End Sub

    Private Sub img_foto_Click(sender As Object, e As EventArgs) Handles img_foto.Click
        Try
            With OpenFileDialog1
                .Title = "Selecione uma foto"
                .Filter = "Imagens (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
                .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) ' Diretório inicial
                If .ShowDialog() = DialogResult.OK Then
                    diretorio = .FileName ' Caminho da foto
                    img_foto.Image = Image.FromFile(diretorio) ' Carrega a imagem no controle PictureBox
                End If
            End With
        Catch ex As Exception
            MsgBox("Erro ao carregar a foto: " & ex.Message, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
    ' Função para conectar ao banco de dados
    Private Sub ConectarBD()
        Try
            ' String de conexão para o SQL Server (ajuste conforme seu servidor e banco de dados)
            Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"
            db = New SqlConnection(connectionString)
            db.Open()
        Catch ex As Exception
            MsgBox("Erro ao conectar ao banco de dados: " & ex.Message, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

    Private Sub txt_nomecad_LostFocus(sender As Object, e As EventArgs) Handles txt_nomecad.LostFocus
        ' Verifica se o campo do nome não está vazio
        If String.IsNullOrWhiteSpace(txt_nomecad.Text) Then
            'MsgBox("O campo Nome do Paciente está vazio.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Atenção")
            Exit Sub
        End If

        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"

        Using con As New SqlConnection(connectionString)
            Try
                con.Open()

                ' Consulta o paciente pelo nome completo
                Dim queryPaciente As String = "SELECT * FROM Pacientes WHERE NomeCompleto = @NomeCompleto"
                Using cmd As New SqlCommand(queryPaciente, con)
                    cmd.Parameters.AddWithValue("@NomeCompleto", txt_nomecad.Text)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        If dr.HasRows Then
                            dr.Read()

                            ' Preenche os campos com os dados do paciente
                            txt_nomecad.Text = dr("NomeCompleto").ToString()
                            txt_usuariocad.Text = dr("NomeUsuario").ToString()
                            txt_enderecocad.Text = dr("Endereco").ToString()
                            txt_filiacaocad.Text = dr("Filiacao").ToString()
                            cmb_data.Text = dr("DataNascimento").ToString()
                            txt_fone.Text = dr("Fone").ToString()
                            txt_certidao.Text = dr("Certidao").ToString()

                            ' Verifica se o campo de foto não é nulo ou vazio antes de carregar
                            If Not IsDBNull(dr("FOTO")) AndAlso Not String.IsNullOrEmpty(dr("FOTO").ToString()) Then
                                img_foto.Load(dr("FOTO").ToString())
                            Else
                                img_foto.Image = Nothing ' Remove a imagem se não houver uma foto
                            End If

                            ' Exibe uma mensagem informando que o paciente foi encontrado
                            MsgBox("Paciente encontrado! Os dados foram preenchidos.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Atenção")
                        Else
                            ' Paciente não encontrado, continua o cadastro normalmente
                            '  MsgBox("Paciente não encontrado! Você pode continuar o cadastro.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Atenção")
                            ' Você pode optar por limpar os campos aqui, se necessário
                            LimparCampos1()
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Erro ao consultar paciente: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
                ' LimparCampos() ' Descomente se desejar limpar os campos em caso de erro
            End Try
        End Using ' A conexão é fechada automaticamente aqui
    End Sub


    ' Função para limpar todos os campos da interface
    Private Sub LimparCampos()
        txt_nomecad.Text = ""
        txt_usuariocad.Text = ""
        txt_enderecocad.Text = ""
        txt_filiacaocad.Text = ""
        cmb_data.Text = ""
        txt_fone.Text = ""
        txt_certidao.Text = ""
        img_foto.Load(Application.StartupPath & "\imagem\nova_foto.png")
    End Sub

    Private Sub LimparCampos1()

        txt_usuariocad.Text = ""
        txt_enderecocad.Text = ""
        txt_filiacaocad.Text = ""
        cmb_data.Text = ""
        txt_fone.Text = ""
        txt_certidao.Text = ""
        img_foto.Load(Application.StartupPath & "\imagem\nova_foto.png")
    End Sub



    ' Função para gravar o paciente no banco de dados
    Private Sub btn_gravar_Click(sender As Object, e As EventArgs) Handles btn_gravar.Click
        Try
            ' Verifica se os campos obrigatórios estão preenchidos
            If txt_nomecad.Text = "" OrElse txt_usuariocad.Text = "" OrElse txt_enderecocad.Text = "" OrElse txt_filiacaocad.Text = "" OrElse txt_fone.Text = "" Then
                MsgBox("Preencha todos os campos obrigatórios!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Aviso")
                Exit Sub
            End If

            ' Conectando ao banco de dados
            ConectarBD()

            If modoEdicao Then
                ' Atualiza os dados do paciente no banco de dados
                Dim SQL As String = "UPDATE Pacientes SET NomeUsuario = @NomeUsuario, Endereco = @Endereco, Filiacao = @Filiacao, DataNascimento = @DataNascimento, Fone = @Fone, Certidao = @Certidao, FOTO = @FOTO WHERE NomeCompleto = @NomeCompleto"
                Dim cmd As New SqlCommand(SQL, db)
                cmd.Parameters.AddWithValue("@NomeCompleto", txt_nomecad.Text)
                cmd.Parameters.AddWithValue("@NomeUsuario", txt_usuariocad.Text)
                cmd.Parameters.AddWithValue("@Endereco", txt_enderecocad.Text)
                cmd.Parameters.AddWithValue("@Filiacao", txt_filiacaocad.Text)
                cmd.Parameters.AddWithValue("@DataNascimento", CDate(cmb_data.Text)) ' Converte para data
                cmd.Parameters.AddWithValue("@Fone", txt_fone.Text)
                cmd.Parameters.AddWithValue("@Certidao", txt_certidao.Text)
                cmd.Parameters.AddWithValue("@FOTO", diretorio)

                cmd.ExecuteNonQuery()
                MsgBox("Registro atualizado com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
            Else
                ' Verifica se o paciente já existe no banco de dados pelo nome completo
                Dim SQL As String = "SELECT COUNT(*) FROM Pacientes WHERE NomeCompleto = @NomeCompleto"
                Dim cmd As New SqlCommand(SQL, db)
                cmd.Parameters.AddWithValue("@NomeCompleto", txt_nomecad.Text)

                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count = 0 Then ' Se o paciente não existir

                    ' Insere os dados do paciente no banco de dados
                    SQL = "INSERT INTO Pacientes (NomeCompleto, NomeUsuario, Endereco, Filiacao, DataNascimento, Fone, Certidao, FOTO) " &
                      "VALUES (@NomeCompleto, @NomeUsuario, @Endereco, @Filiacao, @DataNascimento, @Fone, @Certidao, @FOTO)"
                    cmd = New SqlCommand(SQL, db)
                    cmd.Parameters.AddWithValue("@NomeCompleto", txt_nomecad.Text)
                    cmd.Parameters.AddWithValue("@NomeUsuario", txt_usuariocad.Text)
                    cmd.Parameters.AddWithValue("@Endereco", txt_enderecocad.Text)
                    cmd.Parameters.AddWithValue("@Filiacao", txt_filiacaocad.Text)
                    cmd.Parameters.AddWithValue("@DataNascimento", CDate(cmb_data.Text)) ' Converte para data
                    cmd.Parameters.AddWithValue("@Fone", txt_fone.Text)
                    cmd.Parameters.AddWithValue("@Certidao", txt_certidao.Text)
                    cmd.Parameters.AddWithValue("@FOTO", diretorio)

                    cmd.ExecuteNonQuery()
                    MsgBox("Registro enviado com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                Else
                    MsgBox("Paciente já cadastrado!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Aviso")
                End If
            End If

            ' Limpa os campos após o cadastro ou edição
            limpar_campos()
            modoEdicao = False ' Reseta o modo de edição após a gravação

        Catch ex As Exception
            MsgBox("Erro ao gravar: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "AVISO")
        Finally
            ' Fecha a conexão com o banco de dados
            If db IsNot Nothing AndAlso db.State = ConnectionState.Open Then
                db.Close()
            End If
        End Try
    End Sub


    ' Função para limpar os campos após a gravação
    Private Sub limpar_campos()
        txt_nomecad.Clear()
        txt_usuariocad.Clear()
        txt_enderecocad.Clear()
        txt_filiacaocad.Clear()
        txt_fone.Clear()
        txt_certidao.Clear()
        diretorio = ""
        ' Limpa outros campos e reseta controles como imagem do paciente, se necessário
    End Sub
    Private Sub CarregarDadosPacientes()
        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"
        Using con As New SqlConnection(connectionString)
            Try
                con.Open()

                ' Consulta para buscar todos os pacientes
                Dim query As String = "SELECT PacienteID, NomeCompleto, NomeUsuario, Endereco, Filiacao, DataNascimento, Fone, Certidao FROM Pacientes"
                Using da As New SqlDataAdapter(query, con)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    ' Limpa as colunas existentes (caso já existam botões)
                    dgv_pacientes.Columns.Clear()

                    ' Carrega os dados no DataGridView
                    dgv_pacientes.DataSource = dt

                    ' Adicionando colunas de Editar e Excluir
                    Dim btnEdit As New DataGridViewButtonColumn()
                    btnEdit.HeaderText = "Editar"
                    btnEdit.Name = "Editar"
                    btnEdit.Text = "Editar"
                    btnEdit.UseColumnTextForButtonValue = True
                    dgv_pacientes.Columns.Add(btnEdit)

                    Dim btnDelete As New DataGridViewButtonColumn()
                    btnDelete.HeaderText = "Excluir"
                    btnDelete.Name = "Excluir"
                    btnDelete.Text = "Excluir"
                    btnDelete.UseColumnTextForButtonValue = True
                    dgv_pacientes.Columns.Add(btnDelete)
                End Using
            Catch ex As Exception
                MsgBox("Erro ao carregar dados dos pacientes: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
            End Try
        End Using
    End Sub
    Private Sub txt_busca_TextChanged(sender As Object, e As EventArgs) Handles txt_busca.TextChanged
        ' Obter o texto digitado pelo usuário
        Dim filtro As String = txt_busca.Text.Trim()

        ' Se o filtro estiver vazio, carrega todos os pacientes novamente
        If filtro = "" Then
            CarregarDadosPacientes()
            Exit Sub
        End If

        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"

        Using con As New SqlConnection(connectionString)
            Try
                con.Open()

                ' Consulta para buscar pacientes com base na seleção do ComboBox
                Dim query As String = "SELECT PacienteID, NomeCompleto, NomeUsuario, Endereco, Filiacao, DataNascimento, Fone, Certidao FROM Pacientes WHERE 1=1 "

                If cmb_campo.SelectedItem.ToString() = "NomeCompleto" Then
                    query &= "AND NomeCompleto LIKE @Filtro"
                ElseIf cmb_campo.SelectedItem.ToString() = "NomeUsuario" Then
                    query &= "AND NomeUsuario LIKE @Filtro"
                End If

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Filtro", "%" & filtro & "%")

                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)

                        ' Carrega os dados filtrados no DataGridView
                        dgv_pacientes.DataSource = dt
                    End Using
                End Using
            Catch ex As Exception
                MsgBox("Erro ao buscar pacientes: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "ATENÇÃO")
            End Try
        End Using
    End Sub


    ' Método para carregar dados no DataGridView

    Private Sub carregar_dados1()
        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"
        Using con As New SqlConnection(connectionString)
            Dim SQL As String = "SELECT NomeCompleto, NomeUsuario, Filiacao, DataNascimento, Fone, Endereco, Certidao FROM Pacientes"
            Using cmd As New SqlCommand(SQL, con)
                con.Open()
                Dim dt As New DataTable()
                dt.Load(cmd.ExecuteReader())
                dgv_pacientes.DataSource = dt

                ' Limpa as colunas existentes (caso já existam botões)
                dgv_pacientes.Columns.Clear()

                ' Define a fonte de dados novamente
                dgv_pacientes.DataSource = dt

                ' Adicionando colunas de Editar e Excluir
                Dim btnEdit As New DataGridViewButtonColumn()
                btnEdit.HeaderText = "Editar"
                btnEdit.Name = "Editar"
                btnEdit.Text = "Editar"
                btnEdit.UseColumnTextForButtonValue = True
                dgv_pacientes.Columns.Add(btnEdit)

                Dim btnDelete As New DataGridViewButtonColumn()
                btnDelete.HeaderText = "Excluir"
                btnDelete.Name = "Excluir"
                btnDelete.Text = "Excluir"
                btnDelete.UseColumnTextForButtonValue = True
                dgv_pacientes.Columns.Add(btnDelete)
            End Using
        End Using
    End Sub


    ' Evento de clique nas células do DataGridView
    Private Sub dgv_pacientes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_pacientes.CellContentClick
        Try
            ' Verifica se a célula clicada é válida (não cabeçalho)
            If e.RowIndex >= 0 Then
                Dim selectedRow As DataGridViewRow = dgv_pacientes.Rows(e.RowIndex)

                ' Verifica se o botão de edição foi clicado
                If e.ColumnIndex = dgv_pacientes.Columns("Editar").Index Then
                    Dim nomeCompleto As String = selectedRow.Cells("NomeCompleto").Value.ToString() ' Obtém o nome completo
                    EditarPaciente(nomeCompleto) ' Chama o método de edição
                ElseIf e.ColumnIndex = dgv_pacientes.Columns("Excluir").Index Then
                    Dim nomeCompleto As String = selectedRow.Cells("NomeCompleto").Value.ToString() ' Obtém o nome completo
                    ExcluirPaciente(nomeCompleto) ' Chama o método de exclusão
                End If
            End If
        Catch ex As Exception
            MsgBox("Erro: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Erro")
        End Try
    End Sub

    ' Método para editar paciente

    Private Sub EditarPaciente(nomeCompleto As String)
        ' Conexão ao banco de dados
        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"
        Using con As New SqlConnection(connectionString)
            con.Open() ' Abre a conexão

            ' Consulta para buscar os dados do paciente
            Dim SQL As String = "SELECT * FROM Pacientes WHERE NomeCompleto = @NomeCompleto"
            Using cmd As New SqlCommand(SQL, con)
                cmd.Parameters.AddWithValue("@NomeCompleto", nomeCompleto)

                ' Executa a consulta e obtém os dados
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    If dr.HasRows Then
                        dr.Read()

                        ' Preenche os campos com os dados obtidos
                        txt_nomecad.Text = dr("NomeCompleto").ToString()
                        txt_usuariocad.Text = dr("NomeUsuario").ToString()
                        txt_filiacaocad.Text = dr("Filiacao").ToString()
                        cmb_data.Text = dr("DataNascimento").ToString()
                        txt_fone.Text = dr("Fone").ToString()
                        txt_enderecocad.Text = dr("Endereco").ToString()
                        txt_certidao.Text = dr("Certidao").ToString()

                        ' Carrega a foto se houver
                        If Not IsDBNull(dr("FOTO")) AndAlso Not String.IsNullOrEmpty(dr("FOTO").ToString()) Then
                            img_foto.Load(dr("FOTO").ToString())
                        Else
                            img_foto.Image = Nothing ' Se não houver foto
                        End If

                        ' Habilita o botão de salvar
                        btn_gravar.Enabled = True ' Certifique-se de que você tem um botão com esse nome

                        ' Define o modo de edição
                        modoEdicao = True
                    End If
                End Using
            End Using
        End Using
    End Sub


    Private Sub AtualizarPaciente()
        If txt_nomecad.Text = "" OrElse txt_usuariocad.Text = "" OrElse txt_enderecocad.Text = "" OrElse txt_filiacaocad.Text = "" OrElse txt_fone.Text = "" Then
            MsgBox("Preencha todos os campos obrigatórios!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Aviso")
            Exit Sub
        End If

        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"

        Using con As New SqlConnection(connectionString)
            Try
                con.Open()

                ' Atualiza os dados do paciente
                Dim SQL As String = "UPDATE Pacientes SET NomeUsuario = @NomeUsuario, Endereco = @Endereco, Filiacao = @Filiacao, DataNascimento = @DataNascimento, Fone = @Fone, Certidao = @Certidao, FOTO = @FOTO WHERE NomeCompleto = @NomeCompleto"
                Using cmd As New SqlCommand(SQL, con)
                    cmd.Parameters.AddWithValue("@NomeCompleto", txt_nomecad.Text)
                    cmd.Parameters.AddWithValue("@NomeUsuario", txt_usuariocad.Text)
                    cmd.Parameters.AddWithValue("@Endereco", txt_enderecocad.Text)
                    cmd.Parameters.AddWithValue("@Filiacao", txt_filiacaocad.Text)
                    cmd.Parameters.AddWithValue("@DataNascimento", CDate(cmb_data.Text)) ' Converte para data
                    cmd.Parameters.AddWithValue("@Fone", txt_fone.Text)
                    cmd.Parameters.AddWithValue("@Certidao", txt_certidao.Text)
                    cmd.Parameters.AddWithValue("@FOTO", diretorio) ' Se a foto foi alterada

                    cmd.ExecuteNonQuery()
                    MsgBox("Registro atualizado com sucesso!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
                End Using
            Catch ex As Exception
                MsgBox("Erro ao atualizar: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "AVISO")
            Finally
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End Try
        End Using
    End Sub

    ' Método para excluir paciente
    Private Sub ExcluirPaciente(nomeCompleto As String)
        ' Conexão ao banco de dados
        Dim connectionString As String = "Data Source=LAB5-11;Database=SistemaVacinas;Integrated Security=True;"
        Using con As New SqlConnection(connectionString)
            con.Open() ' Abre a conexão

            ' Mensagem de confirmação para exclusão
            Dim resp As MsgBoxResult = MsgBox("Deseja realmente excluir o paciente " & nomeCompleto & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "ATENÇÃO")
            If resp = MsgBoxResult.Yes Then
                ' Executa a exclusão
                Dim SQL As String = "DELETE FROM Pacientes WHERE NomeCompleto = @NomeCompleto"
                Using cmd As New SqlCommand(SQL, con)
                    cmd.Parameters.AddWithValue("@NomeCompleto", nomeCompleto)
                    cmd.ExecuteNonQuery()
                End Using

                ' Recarrega os dados no DataGridView
                carregar_dados()
            End If
        End Using
    End Sub


End Class