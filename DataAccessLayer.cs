using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsEshm
{
    public class DataAccessLayer
    {
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Products_BD;Data Source=DESKTOP-S52VLHK\\SQLEXPRESS"); //String de Conexion a base de datos local SQL SERVER

        //INSERT
        public void InsertProduct(Products product)
        {
            try
            {
                conn.Open();
                string productName = product.ProductName;
                float price = product.Price;
                int quantity = product.Quantity;
                string category = product.Category;
                string dateEntry = product.DateEntry;

                string query = @" INSERT INTO Products (ProductName, Price, Quantity, Category, DateEntry)
                                VALUES ('" + productName + "','" + price + "','" + quantity + "','" + category + "','" + dateEntry + "')";

                SqlCommand command = new SqlCommand(query, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        //SELECT & FILTRO
        public List<Products> GetProducts(string search = null)
        {
            List<Products> products = new List<Products>();

            try
            {
                conn.Open();
                string query = @"SELECT Id, ProductName, Price, Quantity, Category, DateEntry
                                FROM Products ";

                SqlCommand command = new SqlCommand();

                //FILTRO
                if (!string.IsNullOrEmpty(search))
                {
                    query += @"WHERE ProductName LIKE @Search OR Price LIKE @Search OR Quantity LIKE @Search OR Category LIKE @Search OR DateEntry LIKE @Search";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{search}%"));
                }

                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Products
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        ProductName = reader["ProductName"].ToString(),
                        Price = float.Parse(reader["Price"].ToString()),
                        Quantity = int.Parse(reader["Quantity"].ToString()),
                        Category = reader["Category"].ToString(),
                        DateEntry = reader["DateEntry"].ToString()
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return products;
        }

        //UPDATE
        public void UpdateProduct(Products product)
        {
            try
            {
                conn.Open();
                int id = product.Id;
                string productName = product.ProductName;
                float price = product.Price;
                int quantity = product.Quantity;
                string category = product.Category;
                string dateEntry = product.DateEntry;

                string query = @"UPDATE Products
                                SET ProductName = '" + productName + "', Price = '" + price + "', Quantity = '" + quantity + "', Category = '" + category + "', DateEntry = '" + dateEntry + "'  WHERE Id = " + id + "";

                SqlCommand command = new SqlCommand(query, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        //DELETE
        public void DeleteProduct(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Products WHERE Id = '" + id + "'";

                SqlCommand command = new SqlCommand(query, conn);
                command.ExecuteNonQuery();                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
