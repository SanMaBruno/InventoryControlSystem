using System;
using System.Linq;
using System.Windows.Forms;
using ProgramaInventario.Model;

namespace ProgramaInventario
{
    public partial class BuscarProductos : Form
    {
        public BuscarProductos()
        {
            InitializeComponent();
        }

        private void btnEliminarSeleccion_Click(object sender, EventArgs e)
        {
            if (ConfirmarAccion("¿Estás seguro que deseas eliminar este producto?", "Alerta!"))
            {
                EliminarProducto();
                CargarInventario();
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProducto();
        }

        private void BuscarProductos_Load(object sender, EventArgs e)
        {
            CargarInventario();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarProducto();
            CargarInventario();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtgvProductos.Columns.Clear();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarInventario();
        }

        private void DgvEditar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarInventario();
        }

        private void txtNombreProducto_TextChanged(object sender, EventArgs e)
        {
            // Método vacío, posiblemente eliminar si no se va a utilizar
        }

        private bool ConfirmarAccion(string mensaje, string titulo)
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        private void EliminarProducto()
        {
            using (var db = new Inventario_EntitiesDB())
            {
                var inventario = db.inventario.FirstOrDefault(inv => inv.Nombre == txtNombreProducto.Text);

                if (inventario != null)
                {
                    db.Entry(inventario).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                else
                {
                    MostrarError("Producto no encontrado.");
                }
            }
        }

        private void EditarProducto()
        {
            using (var db = new Inventario_EntitiesDB())
            {
                var inventario = db.inventario.FirstOrDefault(inv => inv.Nombre == txtNombreProducto.Text);

                if (inventario != null)
                {
                    inventario.Nombre = txtNombreEditar.Text;
                    inventario.Precio = txtPrecioEditar.Text;
                    inventario.Cantidad = txtCantidadEditar.Text;
                    db.Entry(inventario).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    MostrarError("Producto no encontrado.");
                }
            }
        }

        private void BuscarProducto()
        {
            using (var db = new Inventario_EntitiesDB())
            {
                string search = txtNombreProducto.Text;
                var inventarios = from inv in db.inventario
                                  where inv.Nombre.ToUpper().Contains(search.ToUpper())
                                  select new InventarioView
                                  {
                                      Codigo = inv.Codigo,
                                      Nombre = inv.Nombre,
                                      Precio = inv.Precio,
                                      Cantidad = inv.Cantidad
                                  };

                dtgvProductos.DataSource = inventarios.ToList();
            }
        }

        private void CargarInventario()
        {
            using (var db = new Inventario_EntitiesDB())
            {
                var inventario = from inv in db.inventario
                                 select new InventarioView
                                 {
                                     Codigo = inv.Codigo,
                                     Nombre = inv.Nombre,
                                     Precio = inv.Precio,
                                     Cantidad = inv.Cantidad
                                 };
                dtgvProductos.DataSource = inventario.ToList();
            }
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
