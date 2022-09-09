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
    public partial class Main : Form
    {
        private DataAccessLayer _dataAccessLayer;

        public Main()
        {
            InitializeComponent();
            _dataAccessLayer = new DataAccessLayer(); //Inicialar variable para que no sea null.
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenDetailsDialog();
        }
        
        private void OpenDetailsDialog()
        {
            ProductDetails productDetails = new ProductDetails();
            productDetails.ShowDialog(this);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            PopulateProducts();
        }

        //SELECT & FILTER
        public void PopulateProducts(string searchText = null)
        {
            List<Products> products = _dataAccessLayer.GetProducts(searchText);
            grid_Products.DataSource = products;
        }

        //FILTRO
        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateProducts(txtSearch.Text); //Poblar el Data Grid con la informacion ingresada en el txtSearch de filtro
            txtSearch.Text = String.Empty;
        }

        //EDIT or DELETE
        private void grid_Products_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)grid_Products.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Value.ToString() == "Edit")
            {
                ProductDetails productDetails = new ProductDetails();

                productDetails.LoadProduct(new Products
                {
                    Id = int.Parse(grid_Products.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    ProductName = grid_Products.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    Price = float.Parse(grid_Products.Rows[e.RowIndex].Cells[2].Value.ToString()),
                    Quantity = int.Parse(grid_Products.Rows[e.RowIndex].Cells[3].Value.ToString()),
                    Category = grid_Products.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    DateEntry = grid_Products.Rows[e.RowIndex].Cells[5].Value.ToString()
                });

                productDetails.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Delete")
            {
                _dataAccessLayer.DeleteProduct(int.Parse(grid_Products.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateProducts();
            }
        }
    }
}
