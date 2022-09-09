using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsEshm
{
    public partial class ProductDetails : Form
    {
        private DataAccessLayer _dataAccessLayer;
        private Products _products;

        public ProductDetails()
        {
            InitializeComponent();
            _dataAccessLayer = new DataAccessLayer();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveProduct();
            this.Close();
            ((Main)this.Owner).PopulateProducts();
        }

        //INSERT
        private Products SaveProduct()
        {
            Products product = new Products();
            product.ProductName = txtProductName.Text; //Guardar en modelo "Products" el texto ingresado del usuario
            product.Price = float.Parse(txtPrice.Text);
            product.Quantity = int.Parse(txtQuantity.Text);
            product.Category = txtCategory.Text;
            product.DateEntry = txtDateEntry.Text;

            product.Id = _products != null ? _products.Id : 0; //Verificando si el Id del modelo es null o contiene info

            if (product.Id == 0)
            {
                _dataAccessLayer.InsertProduct(product); //Insertando
            }
            else
                _dataAccessLayer.UpdateProduct(product); //Actualizando

            return product;   
        }

        //SELECT
        public void LoadProduct(Products products)
        {
            _products = products;
            if (products != null)
            {
                ClearForm();

                txtProductName.Text = products.ProductName; //Almacenando data en TextBoxs de lo que contiene el modelo
                txtPrice.Text = products.Price.ToString();
                txtQuantity.Text = products.Quantity.ToString();
                txtCategory.Text = products.Category;
                txtDateEntry.Text = products.DateEntry;
            }
        }

        private void ClearForm()
        {
            txtProductName.Text = string.Empty; //Limpiando TextBoxs
            txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtCategory.Text = string.Empty;
            txtDateEntry.Text = string.Empty;
        }
    }
}
