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

    }
}
