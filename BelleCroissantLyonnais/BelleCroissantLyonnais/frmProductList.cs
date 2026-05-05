using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BelleCroissantLyonnais.Models;
using BelleCroissantLyonnais.Services;

namespace BelleCroissantLyonnais
{
    public partial class frmProductList : Form
    {
        private readonly ProductService _service;

        public frmProductList()
        {
            InitializeComponent();
            _service = new ProductService();
        }

        private void frmProductList_Load(object sender, EventArgs e)
        {
            ConfigurarColumnas();
            CargarProductos();
            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        private void ConfigurarColumnas()
        {
            dgvProducts.Columns.Clear();
            dgvProducts.AutoGenerateColumns = false;

            dgvProducts.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colActive",
                HeaderText = "Active",
                DataPropertyName = "Active",
                Width = 60,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "ProductName",
                DataPropertyName = "ProductName",
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCategory",
                HeaderText = "Category",
                DataPropertyName = "Category",
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrice",
                HeaderText = "Price",
                DataPropertyName = "Price",
                Width = 80,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCost",
                HeaderText = "Cost",
                DataPropertyName = "Cost",
                Width = 80,
                SortMode = DataGridViewColumnSortMode.Automatic,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            // Columna Edit como enlace
            dgvProducts.Columns.Add(new DataGridViewLinkColumn
            {
                Name = "colEdit",
                HeaderText = "Action",
                Text = "edit",
                UseColumnTextForLinkValue = true,
                Width = 50,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                LinkColor = Color.Blue,
                VisitedLinkColor = Color.Blue,
                ActiveLinkColor = Color.DarkBlue,
                TrackVisitedState = false
            });

            // Columna Delete como enlace
            dgvProducts.Columns.Add(new DataGridViewLinkColumn
            {
                Name = "colDelete",
                HeaderText = "",
                Text = "delete",
                UseColumnTextForLinkValue = true,
                Width = 55,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                LinkColor = Color.Blue,
                VisitedLinkColor = Color.Blue,
                ActiveLinkColor = Color.DarkBlue,
                TrackVisitedState = false
            });

            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
        }

        private void CargarProductos(string filtro = "")
        {
            List<Product> productos = _service.Search(filtro);
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = productos;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            CargarProductos(txtSearch.Text);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            var form = new frmAddEditProduct(_service, null);
            if (form.ShowDialog() == DialogResult.OK)
                CargarProductos(txtSearch.Text);
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var producto = (Product)dgvProducts.Rows[e.RowIndex].DataBoundItem;
            if (producto == null) return;

            string colName = dgvProducts.Columns[e.ColumnIndex].Name;

            if (colName == "colEdit")
            {
                var form = new frmAddEditProduct(_service, producto);
                if (form.ShowDialog() == DialogResult.OK)
                    CargarProductos(txtSearch.Text);
            }
            else if (colName == "colDelete")
            {
                var confirm = MessageBox.Show(
                    $"¿Estás seguro de eliminar '{producto.ProductName}'?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    _service.DeleteProduct(producto.Id);
                    CargarProductos(txtSearch.Text);
                }
            }
        }
    }
}