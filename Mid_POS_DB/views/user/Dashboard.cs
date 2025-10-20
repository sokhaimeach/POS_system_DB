using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mid_POS_DB.views.user;
using Mid_POS_DB.models;
using Mid_POS_DB.views.Inventery;
using Mid_POS_DB.views.sale;
using System.Data.SqlClient;

namespace Mid_POS_DB.views.user
{
    public partial class Dashboard : Form
    {
        private int childFormNumber = 0;

        public Dashboard()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private Form activeForm = null;

        private void OpenChildForm(Form ChildForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.FormBorderStyle = FormBorderStyle.None;
            ChildForm.Dock = DockStyle.Fill;
            panelChilden.Controls.Add(ChildForm);
            panelChilden.Tag = ChildForm;
            ChildForm.Show();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Close();
        }

        private void roleManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoleForm roleForm = new RoleForm();
            StatusForm.Text = "Role";
            OpenChildForm(roleForm);
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            StatusForm.Text = "User";
            OpenChildForm(userForm);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string _sql = "select * from tblProduct where UnitInStock <= 10;";
            Database.cmd = new SqlCommand(_sql, Database.con);
            Database.cmd.ExecuteNonQuery();
            Database.dataAdapter = new SqlDataAdapter(Database.cmd);
            Database.tbl = new DataTable();
            Database.dataAdapter.Fill(Database.tbl);
            if (Database.tbl.Rows.Count > 0)
            {
                AlertStockForm alertStockForm = new AlertStockForm();
                alertStockForm.ShowDialog();
            }
            if(User.Permission == "Admin")
            {
                securityToolStripMenuItem.Enabled = true;
                btnUser.Enabled = true;
                btnRole.Enabled = true;
            }
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            StatusForm.Text = "Product";
            OpenChildForm(productForm);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            if (!securityToolStripMenuItem.Enabled) {
                MessageBox.Show($"You don't have permission, please change you role");
                return;
            }
            userManagementToolStripMenuItem_Click((object) sender, e);
        }

        private void btnRole_Click(object sender, EventArgs e)
        {
            roleManagementToolStripMenuItem_Click((object) sender, e);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            productToolStripMenuItem_Click((object) sender, e);
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            StatusForm.Text = "Category";
            OpenChildForm(categoryForm);
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupplierForm supplierForm = new SupplierForm();
            StatusForm.Text = "Supplier";
            OpenChildForm(supplierForm);
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            StatusForm.Text = "Customer";
            OpenChildForm(customerForm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            categoryToolStripMenuItem_Click(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            supplierToolStripMenuItem_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            customerToolStripMenuItem_Click(sender, e);
        }

        private void saleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaleForm saleForm = new SaleForm();
            StatusForm.Text = "Sale";
            OpenChildForm(saleForm);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saleToolStripMenuItem1_Click(sender, e);
        }
    }
}
