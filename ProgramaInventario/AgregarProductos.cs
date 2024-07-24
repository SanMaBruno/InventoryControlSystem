using System;
using System.Windows.Forms;
using ProgramaInventario.Model;

namespace ProgramaInventario
{
    public partial class AgregarProductos : Form
    {
        public AgregarProductos()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ConfirmarAccion("¿Estás seguro que quieres agregar este producto?", "Alerta!"))
            {
                AgregarProducto();
                LimpiarFormulario();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (ConfirmarAccion("¿Estás seguro que deseas limpiar este formulario?", "Alerta!"))
            {
                LimpiarFormulario();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            CerrarFormulario();
        }

        private bool ConfirmarAccion(string mensaje, string titulo)
        {
            DialogResult dialogResult = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo);
            return dialogResult == DialogResult.Yes;
        }

        private void AgregarProducto()
        {
            using (Inventario_EntitiesDB db = new Inventario_EntitiesDB())
            {
                inventario nuevoInventario = CrearInventario();
                db.inventario.Add(nuevoInventario);
                db.SaveChanges();
            }
        }

        private inventario CrearInventario()
        {
            return new inventario
            {
                Nombre = txtNombreProducto.Text,
                Precio = txtValorProducto.Text,
                Cantidad = txtCantidad.Text
            };
        }

        private void LimpiarFormulario()
        {
            txtNombreProducto.Clear();
            txtValorProducto.Clear();
            txtCantidad.Clear();
            txtDescripcion.Clear();
        }

        private void CerrarFormulario()
        {
            this.Close();
        }
    }
}
