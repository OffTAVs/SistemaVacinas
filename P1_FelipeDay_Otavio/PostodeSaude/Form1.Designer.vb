<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.CADASTROToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CadastroDePacientesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GestãoDosFuncionairosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CONTROLEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ControleDoEstoqueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ControleDeMateriaisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FICHAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FichaDoPacinteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.DarkGreen
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CADASTROToolStripMenuItem, Me.CONTROLEToolStripMenuItem, Me.FICHAToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'CADASTROToolStripMenuItem
        '
        Me.CADASTROToolStripMenuItem.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.CADASTROToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CadastroDePacientesToolStripMenuItem, Me.GestãoDosFuncionairosToolStripMenuItem})
        Me.CADASTROToolStripMenuItem.Image = CType(resources.GetObject("CADASTROToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CADASTROToolStripMenuItem.Name = "CADASTROToolStripMenuItem"
        Me.CADASTROToolStripMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.CADASTROToolStripMenuItem.Text = "CADASTRO"
        '
        'CadastroDePacientesToolStripMenuItem
        '
        Me.CadastroDePacientesToolStripMenuItem.Name = "CadastroDePacientesToolStripMenuItem"
        Me.CadastroDePacientesToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.CadastroDePacientesToolStripMenuItem.Text = "Cadastro de Pacientes"
        '
        'GestãoDosFuncionairosToolStripMenuItem
        '
        Me.GestãoDosFuncionairosToolStripMenuItem.Name = "GestãoDosFuncionairosToolStripMenuItem"
        Me.GestãoDosFuncionairosToolStripMenuItem.Size = New System.Drawing.Size(201, 22)
        Me.GestãoDosFuncionairosToolStripMenuItem.Text = "Gestão dos funcionairos"
        '
        'CONTROLEToolStripMenuItem
        '
        Me.CONTROLEToolStripMenuItem.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.CONTROLEToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ControleDoEstoqueToolStripMenuItem, Me.ControleDeMateriaisToolStripMenuItem})
        Me.CONTROLEToolStripMenuItem.Name = "CONTROLEToolStripMenuItem"
        Me.CONTROLEToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.CONTROLEToolStripMenuItem.Text = "CONTROLE"
        '
        'ControleDoEstoqueToolStripMenuItem
        '
        Me.ControleDoEstoqueToolStripMenuItem.Name = "ControleDoEstoqueToolStripMenuItem"
        Me.ControleDoEstoqueToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.ControleDoEstoqueToolStripMenuItem.Text = "Controle do Remédios"
        '
        'ControleDeMateriaisToolStripMenuItem
        '
        Me.ControleDeMateriaisToolStripMenuItem.Name = "ControleDeMateriaisToolStripMenuItem"
        Me.ControleDeMateriaisToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
        Me.ControleDeMateriaisToolStripMenuItem.Text = "Controle de Materiais"
        '
        'FICHAToolStripMenuItem
        '
        Me.FICHAToolStripMenuItem.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.FICHAToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FichaDoPacinteToolStripMenuItem})
        Me.FICHAToolStripMenuItem.Image = CType(resources.GetObject("FICHAToolStripMenuItem.Image"), System.Drawing.Image)
        Me.FICHAToolStripMenuItem.Name = "FICHAToolStripMenuItem"
        Me.FICHAToolStripMenuItem.Size = New System.Drawing.Size(140, 20)
        Me.FICHAToolStripMenuItem.Text = "ESQUEMA VACINAL"
        '
        'FichaDoPacinteToolStripMenuItem
        '
        Me.FichaDoPacinteToolStripMenuItem.Name = "FichaDoPacinteToolStripMenuItem"
        Me.FichaDoPacinteToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.FichaDoPacinteToolStripMenuItem.Text = "Ficha do Pacinte"
        '
        'btnLogout
        '
        Me.btnLogout.Location = New System.Drawing.Point(713, 415)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(75, 23)
        Me.btnLogout.TabIndex = 1
        Me.btnLogout.Text = "Sair"
        Me.btnLogout.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnLogout)
        Me.Controls.Add(Me.MenuStrip1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "HOME"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents CADASTROToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CadastroDePacientesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CONTROLEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ControleDoEstoqueToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ControleDeMateriaisToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FICHAToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FichaDoPacinteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GestãoDosFuncionairosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnLogout As Button
End Class
