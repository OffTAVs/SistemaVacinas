Imports System.Data.SqlClient

Public Class Form1


    Public FuncionarioID As Integer ' ID do funcionário
    Public TipoUsuario As String ' Tipo de usuário

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Chama a função que ajusta a visibilidade com base no tipo de usuário
        ConfigurarAcessoUsuario()


    End Sub

    Private Sub ConfigurarAcessoUsuario()
        If TipoUsuario = "Médico" Or TipoUsuario = "Enfermeiro" Or TipoUsuario = "Técnico" Then
            ' Esconder painel de configurações
            frm_funcionarios.Visible = False
            GestãoDosFuncionairosToolStripMenuItem.Visible = False

            ' Desabilitar alguns controles que só o administrador pode usar
            ' btn_deletar.Enabled = False
            ' txt_criticalData.Enabled = False

        ElseIf TipoUsuario = "Admin" Then
            ' O administrador pode ver tudo
            'frm_funcionarios.Visible = True
            GestãoDosFuncionairosToolStripMenuItem.Visible = True
            FICHAToolStripMenuItem.Visible = False
            ' btn_deletar.Enabled = True
            ' txt_criticalData.Enabled = True
        End If
    End Sub

    Private Sub CadastroDePacientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CadastroDePacientesToolStripMenuItem.Click
        frm_cadpacientes.ShowDialog()
    End Sub

    Private Sub FichaDoPacinteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FichaDoPacinteToolStripMenuItem.Click

        Dim ficha As New frm_ficha()
        ficha.FuncionarioID = FuncionarioID ' Passa o ID do funcionário para o frm_ficha
        ficha.Show()
    End Sub

    Private Sub GestãoDosFuncionairosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GestãoDosFuncionairosToolStripMenuItem.Click
        frm_funcionarios.ShowDialog()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
        frm_login.ShowDialog()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' Mostra a tela de login novamente
        Dim loginForm As New frm_login()
        loginForm.Show()

        ' Limpa os dados do funcionário atual
        Me.FuncionarioID = 0
        Me.TipoUsuario = String.Empty

        ' Esconde o Form1 atual para voltar ao login
        Me.Hide()
    End Sub


End Class



