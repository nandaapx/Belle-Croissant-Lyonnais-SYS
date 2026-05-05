using System;
using System.Windows.Forms;
using BelleCroissantLyonnais.Models;
using BelleCroissantLyonnais.Services;

namespace BelleCroissantLyonnais
{
    public partial class frmAddEditProduct : Form
    {
        private readonly ProductService _service;
        private readonly Product _producto;
        private readonly bool _esEdicion;

        public frmAddEditProduct(ProductService service, Product producto)
        {
            InitializeComponent();
            _service = service;
            _producto = producto;
            _esEdicion = producto != null;
        }

        private void frmAddEditProduct_Load(object sender, EventArgs e)
        {
            cmbCategory.Items.AddRange(new string[]
            {
                "Bread", "Pastries", "Tarte", "Viennoiserie", "Cake", "Other"
            });

            if (_esEdicion)
            {
                Text = "Edit Product";
                txtProductName.Text = _producto.ProductName;
                cmbCategory.SelectedItem = _producto.Category;
                nudPrice.Value = _producto.Price;
                nudCost.Value = _producto.Cost;
                txtDescription.Text = _producto.Description;
                chkActive.Checked = _producto.Active;
                chkSeasonal.Checked = _producto.Seasonal;
                dtpIntroducedDate.Value = _producto.IntroducedDate;
            }
            else
            {
                Text = "Add Product";
                dtpIntroducedDate.Value = DateTime.Today;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Debes seleccionar una categoría.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nudPrice.Value <= 0)
            {
                MessageBox.Show("El precio debe ser mayor que 0.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nudCost.Value <= 0)
            {
                MessageBox.Show("El costo debe ser mayor que 0.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nudCost.Value >= nudPrice.Value)
            {
                MessageBox.Show("El costo debe ser menor que el precio.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_esEdicion)
            {
                _producto.ProductName = txtProductName.Text.Trim();
                _producto.Category = cmbCategory.SelectedItem.ToString();
                _producto.Price = nudPrice.Value;
                _producto.Cost = nudCost.Value;
                _producto.Description = txtDescription.Text.Trim();
                _producto.Active = chkActive.Checked;
                _producto.Seasonal = chkSeasonal.Checked;
                _producto.IntroducedDate = dtpIntroducedDate.Value;
                _service.UpdateProduct(_producto);
            }
            else
            {
                var nuevo = new Product
                {
                    ProductName = txtProductName.Text.Trim(),
                    Category = cmbCategory.SelectedItem.ToString(),
                    Price = nudPrice.Value,
                    Cost = nudCost.Value,
                    Description = txtDescription.Text.Trim(),
                    Active = chkActive.Checked,
                    Seasonal = chkSeasonal.Checked,
                    IntroducedDate = dtpIntroducedDate.Value
                };
                _service.AddProduct(nuevo);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }



 

        private void frmAddEditProduct_Load_1(object sender, EventArgs e)
        {

        }
    }
}
