
namespace finalProjectClient
{
    partial class ShopperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.ProductColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewPurchases = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // cmbProducts
            // 
            this.cmbProducts.FormattingEnabled = true;
            this.cmbProducts.Location = new System.Drawing.Point(227, 12);
            this.cmbProducts.Name = "cmbProducts";
            this.cmbProducts.Size = new System.Drawing.Size(158, 21);
            this.cmbProducts.TabIndex = 2;
            // 
            // btnPurchase
            // 
            this.btnPurchase.Location = new System.Drawing.Point(310, 195);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(75, 23);
            this.btnPurchase.TabIndex = 3;
            this.btnPurchase.Text = "Purchase";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Click += new System.EventHandler(this.btnPurchase_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(227, 195);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ProductColumn
            // 
            this.ProductColumn.Text = "Product";
            this.ProductColumn.Width = 300;
            // 
            // listViewPurchases
            // 
            this.listViewPurchases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProductColumn});
            this.listViewPurchases.Enabled = false;
            this.listViewPurchases.HideSelection = false;
            this.listViewPurchases.Location = new System.Drawing.Point(12, 12);
            this.listViewPurchases.Name = "listViewPurchases";
            this.listViewPurchases.Size = new System.Drawing.Size(198, 206);
            this.listViewPurchases.TabIndex = 5;
            this.listViewPurchases.UseCompatibleStateImageBehavior = false;
            this.listViewPurchases.View = System.Windows.Forms.View.List;
            // 
            // ShopperForm
            // 
            this.AcceptButton = this.btnPurchase;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 229);
            this.Controls.Add(this.listViewPurchases);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnPurchase);
            this.Controls.Add(this.cmbProducts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "ShopperForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ColumnHeader ProductColumn;
        private System.Windows.Forms.ListView listViewPurchases;
    }
}