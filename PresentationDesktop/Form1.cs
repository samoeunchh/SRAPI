using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:49986/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        static readonly HttpClient client = new HttpClient();
        private async void Form1_Load(object sender, EventArgs e)
        {
            //Get Data
            HttpResponseMessage response = await client.GetAsync("api/Brands");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<List<Brand>>();
                listView1.Items.Clear();
                foreach (var item in data)
                {
                    var li = listView1.Items.Add(item.BrandId.ToString());
                    li.SubItems.Add(item.BrandName);
                    li.SubItems.Add(item.Description);
                }
            }
            else
            {
                MessageBox.Show("No record!");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBrandName.Text)) MessageBox.Show("Brand name is required");
            var brand = new Brand
            {
                BrandName = txtBrandName.Text,
                Description = txtDescription.Text
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Brands", brand);
            if (response.IsSuccessStatusCode)
            {
                txtBrandName.Text = "";
                txtDescription.Text = "";
                MessageBox.Show("Record was saved");
                Form1_Load(sender,e);
            }
            else
            {
                var errorMessage = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                MessageBox.Show(errorMessage);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string message = "Do you want to delete?";
                string title = "Close Window";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    var id = listView1.SelectedItems[0].Text;
                    HttpResponseMessage response = await client.DeleteAsync("api/Brands/"+ id);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Delete sucess");
                        Form1_Load(sender,e);
                    }
                    else
                    {
                        MessageBox.Show("Delete Failed");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item for deleting");
            }
        }
    }
}
