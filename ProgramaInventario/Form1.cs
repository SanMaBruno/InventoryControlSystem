using System;
using System.Windows.Forms;

namespace ProgramaInventario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void agregarProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarFormulario(new AgregarProductos());
        }

        private void buscarProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarFormulario(new BuscarProductos());
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarAplicacion();
        }

        private void MostrarFormulario(Form formulario)
        {
            formulario.ShowDialog();
        }

        private void CerrarAplicacion()
        {
            this.Close();
        }
    }
}
