using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProjectClient
{
    public partial class ShopperForm : Form
    {
        ShopperServerHandler ssHandlerShopperForm;
        public ShopperForm(ShopperServerHandler ssHandler)
        {
            InitializeComponent();
            ssHandlerShopperForm = ssHandler;
            this.Text = $"Shop Client, User:{ssHandlerShopperForm.currentShopper.Username}";
            loadDropDown();
            loadOrders();
        }

        public void loadDropDown()
        {
            List<Product> products = ssHandlerShopperForm.GetProducts();
            foreach(Product product in products)
            {
                cmbProducts.Items.Add(product.Name);
            }
        }

        public void loadOrders()
        {
            try
            {
                List<Order> orders = ssHandlerShopperForm.GetOrders();
                if (orders != null)
                {
                    foreach (Order order in orders)
                    {
                        listViewPurchases.Items.Add($"{order.ProductName}, {order.Quantity.ToString()}, {order.User}");
                    }
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("Server is unavailable!", " Server Unavailable!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            listViewPurchases.Clear();
            loadOrders();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {

            string result = ssHandlerShopperForm.Purchase(cmbProducts.Text);
            if(result == null)
            {
                MessageBox.Show("Server is unavailable!", " Server Unavailable!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result.Contains("DONE"))
            {
                listViewPurchases.Clear();
                loadOrders();
            } else if (result.Contains("NOT_AVAILABLE"))
            {
                MessageBox.Show("This product is not available", "", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                MessageBox.Show("The specified product is not valid", "", MessageBoxButtons.OK, MessageBoxIcon.None);
            }

        }
    }
}
