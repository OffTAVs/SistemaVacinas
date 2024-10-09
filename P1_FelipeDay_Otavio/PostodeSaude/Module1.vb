Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Module Module1
    Public diretorio, SQL, aux_NomeCompleto, resp As String
    Public db As New ADODB.Connection
    Public rs As New ADODB.Recordset
    Public cont, aux_idcliente As Integer

    Sub conecta_banco()
        'String de Conexão ADO SQL-SERVER
        Try
            db = CreateObject("ADODB.Connection") 'Data Source=LAB12
            'LAB5-prof
            'Data Source=DESKTOP-KLTPHQC\SQLEXPRESS
            db.Open("Provider=SQLOLEDB;Data Source=LAB5-11;Initial Catalog=SistemaVacinas;trusted_connection=yes;")
            MsgBox("Conexão OK", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "AVISO")
        Catch ex As Exception
            MsgBox("Erro ao Conectar!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "AVISO")
        End Try
    End Sub

    Sub limpar_cadastro_funcionario()
        Try
            ' Usando o 'Me' para acessar o formulário atual
            With frm_login
                ' Limpar os campos de texto
                .txt_usuario.Clear()
                .txt_senha.Clear()

                ' Limpar os itens dos ComboBox (se houver)
                ' Exemplo se você tiver um ComboBox para cargo

                ' Focar o controle 'txt_nome'
            End With
        Catch ex As Exception
            ' Exibir detalhes do erro
            MsgBox("Erro ao limpar cadastro: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "AVISO")
        End Try
    End Sub

    Sub carregar_campos()
        Try
            With frm_login.cmb_login.Items
                .Add("Médico")
                .Add("Enfermeiro")
                .Add("Técnico")
                .Add("Admin")
                frm_login.cmb_login.SelectedIndex = 0
            End With
        Catch ex As Exception

        End Try
    End Sub
    Sub carregar_campos2()
        Try
            With frm_cadpacientes.cmb_campo.Items
                .Add("NomeCompleto")
                .Add("NomeUsuario")
            End With
            frm_cadpacientes.cmb_campo.SelectedIndex = 1
        Catch ex As Exception

        End Try
    End Sub
    Sub carregar_campos3()
        Try
            With frm_funcionarios.cmb_campof.Items
                .Add("Cargo")
                .Add("NomeUsuario")
            End With
            frm_funcionarios.cmb_campof.SelectedIndex = 1
        Catch ex As Exception

        End Try
    End Sub
    Sub carregar_dados()
        Try
            SQL = "select * from Pacientes order by NomeCompleto asc"
            rs = db.Execute(SQL)
            cont = 0
            With frm_cadpacientes.dgv_pacientes
                .Rows.Clear()
                Do While rs.EOF = False
                    cont = cont + 1
                    .Rows.Add(cont, rs.Fields(1).Value, rs.Fields(3).Value, rs.Fields(2).Value, rs.Fields(4).Value, Nothing, Nothing)
                    rs.MoveNext()
                Loop
            End With

        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

End Module
