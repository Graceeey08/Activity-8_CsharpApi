using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpApi
{
    public partial class Product : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Product()
        {
            InitializeComponent();
        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Clear();
                HttpResponseMessage response = await client.GetAsync("http://localhost/phpapi/productapi.php");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                txtOutput.Text = responseBody;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            // Declare the product variable outside the try block
            var product = new
            {
                productname = txtProductname.Text,
                price = decimal.Parse(txtPrice.Text),
                quantity = int.Parse(txtQuantity.Text)
            };

            try
            {
                // Serialize the product object to JSON using Newtonsoft.Json
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Post the JSON to the API
                HttpResponseMessage response = await client.PostAsync("http://localhost/phpapi/productapi.php", content);
                response.EnsureSuccessStatusCode();

                // Read and display the response
                string responseBody = await response.Content.ReadAsStringAsync();
                txtOutput.Text = responseBody;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

                // Display the JSON being sent
                txtOutput.Text = $"Error: {ex.Message}\nJSON Sent: {JsonConvert.SerializeObject(product)}";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var otherforms = new Admin();
            this.Hide();
            otherforms.Show();
        }

        private void txtProductname_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
