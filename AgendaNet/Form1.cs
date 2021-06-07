using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Repositorio;

namespace AgendaNet
{
    public partial class Form1 : Form
    {
        Empresa empresa;
        List<Empresa> empresas;

        public Form1()
        {
            InitializeComponent();
        }

        private void AtualizarDGV()
        {
            this.empresas = API<Empresa>.get("/empresa/listar.php");
            dgvEstados.DataSource = this.empresas;
        }

        public void PreencherFormulario()
        {
            if (this.empresa != null)
            {
                txtNome.Text = this.empresa.nome_empresa;
                txtCnpj.Text = this.empresa.cnpj_empresa;
                txtLogin.Text = this.empresa.login_empresa;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = '*';
            this.AtualizarDGV();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            this.empresa = new Empresa()
            {
                nome_empresa = txtNome.Text,
                cnpj_empresa = txtCnpj.Text,
                login_empresa = txtLogin.Text,
                senha_empresa = txtSenha.Text
            };

            API<Empresa>.post("/empresa/inserir.php", empresa);
            this.AtualizarDGV();
        }


        private void DgvClick(object sender, DataGridViewCellEventArgs e)
        {
            int cod_empresa = int.Parse(dgvEstados.CurrentRow.Cells[0].Value.ToString());
            this.empresa = API<Empresa>.get("/empresa/consultar.php", cod_empresa)[0];
            this.PreencherFormulario();
        }

    }
}
