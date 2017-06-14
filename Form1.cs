using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoArbolesBinarios
{
    public partial class Form1 : Form
    {
        Arbol tree = new Arbol();
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdGenerar_Click(object sender, EventArgs e)
        {
            tree = new Arbol();
            tree.descomponerYLlenar(txtExp.Text);
            txtMostrarPre.Text = tree.preOrden();
            txtMostrarPost.Text = tree.postOrden();
        }

        private void cmdEvPre_Click(object sender, EventArgs e)
        {
            txtEvPre.Text = Convert.ToString(tree.evaluarPre(txtMostrarPre.Text));
        }

        private void cmdEvPost_Click(object sender, EventArgs e)
        {
            txtEvPost.Text = Convert.ToString(tree.evaluarPost(txtMostrarPost.Text));
        }
    }
}
