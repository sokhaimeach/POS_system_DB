namespace Mid_POS_DB.views.Inventery
{
    partial class AlertStockForm
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dgAlterStock = new System.Windows.Forms.DataGridView();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAlterStock)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(992, 663);
            this.panel4.TabIndex = 9;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.dgAlterStock);
            this.panel6.Font = new System.Drawing.Font("Monospac821 BT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(17, 12);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(963, 639);
            this.panel6.TabIndex = 1;
            // 
            // dgAlterStock
            // 
            this.dgAlterStock.AllowUserToAddRows = false;
            this.dgAlterStock.AllowUserToDeleteRows = false;
            this.dgAlterStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAlterStock.BackgroundColor = System.Drawing.Color.White;
            this.dgAlterStock.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgAlterStock.ColumnHeadersHeight = 50;
            this.dgAlterStock.GridColor = System.Drawing.Color.Black;
            this.dgAlterStock.Location = new System.Drawing.Point(18, 22);
            this.dgAlterStock.Name = "dgAlterStock";
            this.dgAlterStock.ReadOnly = true;
            this.dgAlterStock.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgAlterStock.RowTemplate.Height = 40;
            this.dgAlterStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAlterStock.Size = new System.Drawing.Size(927, 600);
            this.dgAlterStock.TabIndex = 1;
            // 
            // AlertStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 663);
            this.Controls.Add(this.panel4);
            this.Name = "AlertStockForm";
            this.Text = "AlertStockForm";
            this.Load += new System.EventHandler(this.AlertStockForm_Load);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAlterStock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dgAlterStock;
    }
}