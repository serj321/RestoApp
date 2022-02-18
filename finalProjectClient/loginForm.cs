using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalProjectClient
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void loginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //Thread myThread = new Thread(() =>
            //{
                ShopperServerHandler ssHandler = new ShopperServerHandler(txtHostName.Text, txtAccountNo.Text);
                try
                {
                    if (ssHandler.CheckConnect())
                    {
                        //this.Invoke(new Action(() => this.Hide()));
                        ShopperForm shopperForm = new ShopperForm(ssHandler);
                        //this.Invoke(new MethodInvoker(this.Hide));
                        //shopperForm.Invoke((MethodInvoker)delegate() {
                        //    shopperForm.Show();
                        //});
                        shopperForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Client Number Invalid!", "Client Number Invalid!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Application.Exit();
                    }
                }
                catch (SocketException)
                {
                    MessageBox.Show("Server Unavailable!", "Server Unavailable!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                }
            //});
            //myThread.Start();
        }


        private void inputTextChanged(object sender, EventArgs e)
        {
            if(txtAccountNo.TextLength > 0 && txtHostName.TextLength > 0)
            {
                btnConnect.Enabled = true;
            }
            else
            {
                btnConnect.Enabled = false;
            }
        }
    }
}
