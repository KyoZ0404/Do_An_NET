using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace project
{
    public partial class frmMain : Form
    {
    
        private string username;
        private string password;
     
        string strBill;
        public frmMain()
        {
            InitializeComponent();
            loaddataTable();
            loaddataCategory();
        }
      
        public frmMain(string user, string name, string pass, string type) : this()
        {
            username = user;
            password = pass;
        
            if (type == "ADMIN")
            {        
                tmiAdmin.Visible = true;
            }
        }
    
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có muốn đăng xuất?", "Thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }     
        private void loaddataTable()
        {
            try
            {
                pnlBan.Controls.Clear();
                DataProvider provider = new DataProvider();
                DataTable table = provider.loadTableF();
                int x = 10;
                int y = 10;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Button btn = new Button()
                    {
                        Name = "btnTable" + i,
                        Text = table.Rows[i][0].ToString(),
                        Tag = table.Rows[i][2].ToString(),
                        Width = 100,
                        Height = 50,
                        Location = new Point(x, y),
                    };
              
                    if (table.Rows[i][1].ToString() == "TRONG")
                    {
                        btn.BackColor = ColorTranslator.FromHtml("snow");
                    
                        btn.ContextMenuStrip = cmnSubTable2;
                    }
                    else if (table.Rows[i][1].ToString() == "ONLINE")
                    {
                        btn.BackColor = ColorTranslator.FromHtml("lime");
                       
                        btn.ContextMenuStrip = cmnSubTable;
                    }
                    else if (table.Rows[i][1].ToString() == "DATTRUOC")
                    {
                        btn.BackColor = ColorTranslator.FromHtml("red");
                      
                        btn.ContextMenuStrip = cmnSubTable3;
                    }
                  
                    if (x < pnlBan.Width - 220)
                    {
                        x += 110;
                    }
                    else
                    {
                        x = 10;
                        y += 60;
                    }
                    btn.MouseClick += new MouseEventHandler(btnTable_MouseClick);
                    btn.MouseHover += new EventHandler(btnTable_MouseHover);
                    pnlBan.Controls.Add(btn);
                }
            }
            catch
            {
                MessageBox.Show("Không thể tải bàn!", "Lỗi...");
            }
        }

        public void loaddataBill()
        {
            try
            {   
                pnlBill.Controls.Clear();
                strBill = "";
                DataProvider provider = new DataProvider();
                DataTable table = provider.loadBillInfo(txtNameTable.Text);
              
                int y = 10;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                 
                    strBill += (i + 1) + ".     " + table.Rows[i][2].ToString() + "  X  " + table.Rows[i][3].ToString()+"\n";
                    Label lbl = new Label()
                    {
                        Name = "btnFB" + i,
                    
                        Text = (i + 1) + ".     " + table.Rows[i][2].ToString() + "  X  " + table.Rows[i][3].ToString(),
                        Width = pnlBill.Width - 20,
                        Height = 20,
                        Location = new Point(5, y)
                    };
                    y += 25;
                    pnlBill.Controls.Add(lbl);
                }
            }
            catch 
            {
            }
        }
        
   
        private void loaddataCategory()
        {
            try
            {   
                pnlLoai.Controls.Clear();
                DataProvider provider = new DataProvider();
                DataTable table = provider.loadCategory();
                int x = 10;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Button btn = new Button()
                    {
                        Name = "btnFood" + i,
                        Text = table.Rows[i][0].ToString(), 
                        Width = 120,
                        Height = pnlLoai.Height - 40,
                        Location = new Point(x, pnlLoai.Location.Y - 20),
                        BackColor = ColorTranslator.FromHtml("Snow"),
                    };
                    x += 130;
                    pnlLoai.Controls.Add(btn);
                    btn.Click += new EventHandler(btnCategory_Click);
                }
              
                loaddataFood(table.Rows[0][0].ToString());
            }
            catch
            {
            }
        }

       
        private void loaddataFood(string nameCategory)
        {
            try
            {  
                pnlFood.Controls.Clear();
                DataProvider provider = new DataProvider();
                DataTable table = provider.loadFood(nameCategory);
                txtNameFood.Text = table.Rows[0][0].ToString();
                txtPriceFood.Text = table.Rows[0][2].ToString();  
                int y = 10;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Button btn = new Button()
                    {
                        Name = "btnFood" + i,
                        Text = table.Rows[i][0].ToString(), 
                        Tag = table.Rows[i][2].ToString(), 
                        Width = pnlFood.Width - 40,
                        Height = 50,
                        Location = new Point(pnlFood.Location.X, y),
                        BackColor = ColorTranslator.FromHtml("Snow"),
                    };
                    y += 60;
                    btn.Click += new EventHandler(btnFB_Click);
                    pnlFood.Controls.Add(btn);
                }
            }
            catch
            {
            }
        }

        private void btnTable_MouseHover(object sender, EventArgs e)
        {
            ClickTable(sender, e);
        }

     
        private void btnTable_MouseClick(object sender, EventArgs e)
        {
            ClickTable(sender, e);
        }
        private void ClickTable(object sender, EventArgs e)
        {
    
            if (((Button)sender).BackColor.ToString() == "Color [Snow]")
            {
                txtSTT.Text = "TRONG";
            }
            else if (((Button)sender).BackColor.ToString() == "Color [Lime]")
            {
                txtSTT.Text = "ONLINE";
            }
            else if (((Button)sender).BackColor.ToString() == "Color [Red]")
            {
                txtSTT.Text = "DATTRUOC";
            }
       
            txtNameTable.Text = ((Button)sender).Text;
       
            txtTotal.Text = ((Button)sender).Tag.ToString();
            loaddataBill();
        }

    
        private void btnCategory_Click(object sender, EventArgs e)
        {
            string nameCate = ((Button)sender).Text;
       
            loaddataFood(nameCate);
        }

     
        private void btnFB_Click(object sender, EventArgs e)
        {          
            txtNameFood.Text = ((Button)sender).Text;
            txtPriceFood.Text = ((Button)sender).Tag.ToString();
        }

     
        private void tmiCategory_Click(object sender, EventArgs e)
        {
            try
            {
                Them_Danh_Muc frm = new Them_Danh_Muc();
                frm.ShowDialog();
                this.Show();
                loaddataCategory();
            }
            catch{}
        }

        private void tmiAccount_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdAccount frm = new frmAdAccount();
                frm.ShowDialog();
                this.Show();
            }
            catch{}
        }

        private void gpbTable_SizeChanged(object sender, EventArgs e)
        {
            loaddataTable();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            AddFood();
        }
        private void AddFood()
        {
            try
            {
                if (txtSTT.Text == "ONLINE")
                {
                    frmAddFood addF = new frmAddFood(txtNameTable.Text, txtNameFood.Text, txtSTT.Text);
                    addF.ShowDialog();
                    this.Show();
                    loaddataTable();
                    loaddataBill();
                }
                else if (txtSTT.Text == "DATTRUOC")
                {
                    MessageBox.Show("Bàn đã được đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtSTT.Text == "TRONG")
                {
                    DialogResult ms = MessageBox.Show("Bàn này đang trống. Mở bàn nhé?", "Lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ms == DialogResult.Yes)
                    {
                        frmAddFood addF = new frmAddFood(txtNameTable.Text, txtNameFood.Text, txtSTT.Text);
                        addF.ShowDialog();
                        this.Show();
                        loaddataTable();
                        loaddataBill();
                    }
                }
            }
            catch { }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            PayFood();
        }
        private void PayFood()
        {
            try
            {
                if (txtSTT.Text == "ONLINE")
                {
                    frmPay addF = new frmPay(txtNameTable.Text);
                    addF.ShowDialog();
                    this.Show();
                    loaddataTable();
                    loaddataBill();
                }
                else if (txtSTT.Text == "DATTRUOC")
                {
                    MessageBox.Show("Bàn đã được đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtSTT.Text == "TRONG")
                {
                    MessageBox.Show("Bàn này đang trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }
 
        private void btnReturn_Click(object sender, EventArgs e)
        {
            ReturnFood();
        }
        private void ReturnFood()
        {
            try
            {
                if (txtSTT.Text == "ONLINE")
                {
                    Doi_Tra_Mon addF = new Doi_Tra_Mon(txtNameTable.Text);
                    addF.ShowDialog();
                    this.Show();
                    loaddataTable();
                    loaddataBill();
                }
                else if (txtSTT.Text == "DATTRUOC")
                {
                    MessageBox.Show("Bàn đã được đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtSTT.Text == "TRONG")
                {
                    MessageBox.Show("Bàn này đang trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }
   
        private void btnBlock_Click(object sender, EventArgs e)
        {
            Nghi addF = new Nghi(username, lblName.Text, password);
            addF.ShowDialog();
            this.Show();
        }
 
        private void tsmThemMon_Click(object sender, EventArgs e)
        {
            AddFood();
        }
        private void tsmTraMon_Click(object sender, EventArgs e)
        {
            ReturnFood();
        }
        private void tsmThanhToan_Click(object sender, EventArgs e)
        {
            PayFood();
        }
         
        private void cmnSubTable2_Opening(object sender, CancelEventArgs e)
        {
        }
        private void tmsThemMon2_Click(object sender, EventArgs e)
        {
            AddFood();
        }
 
        private void tsmDatBan_Click(object sender, EventArgs e)
        {
            try
            {
                DataProvider provider = new DataProvider();
                provider.Datban("DATTRUOC", txtNameTable.Text);
                loaddataTable();
            }
            catch { }
        }

        private void cmnSubTable3_Opening(object sender, CancelEventArgs e)
        {
        }
   
        private void tsmMoBan_Click(object sender, EventArgs e)
        {
            try
            {
                DataProvider provider = new DataProvider();
                provider.Datban("TRONG",txtNameTable.Text);
                loaddataTable();
            }
            catch { }
        }
        private void tmiCategory_Click_1(object sender, EventArgs e)
        {
            try
            {
                Them_Danh_Muc frm = new Them_Danh_Muc();
                frm.ShowDialog();
                this.Show();
                loaddataCategory();
            }
            catch { }
        }

        private void tmiFood_Click_1(object sender, EventArgs e)
        {
            try
            {
                QL_Mon frm = new QL_Mon();
                frm.ShowDialog();
                this.Show();
                loaddataCategory();
            }
            catch { }
        }

        private void tmiTable_Click_1(object sender, EventArgs e)
        {
            try
            {
                QL_Ban frm = new QL_Ban();
                frm.ShowDialog();
                this.Show();
                loaddataTable();
            }
            catch { }
        }

        private void tmiAccount_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmAdAccount frm = new frmAdAccount();
                frm.ShowDialog();
                this.Show();
            }
            catch { }
        }
        private void tmiChange_Click_1(object sender, EventArgs e)
        {
            try
            {
                Thong_Tin addF = new Thong_Tin(username, lblName.Text, password);
                addF.ShowDialog();
                this.Show();
                loaddataTable();
                loaddataBill();
            }
            catch
            {
                MessageBox.Show("Không thể thay đổi thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmiSleep_Click(object sender, EventArgs e)
        {
            Nghi addF = new Nghi(username, lblName.Text, password);
            addF.ShowDialog();
            this.Show();
        }
 
        private void tmiLogout_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void gpbFood_Enter(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            skin();
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            Chuyen();
        }
        private void Chuyen()
        {
            try
            {
                if (txtSTT.Text == "ONLINE")
                {
                    Chuyen_Ban addF = new Chuyen_Ban(txtNameTable.Text);
                    addF.ShowDialog();
                    this.Show();
                    loaddataTable();
                    loaddataBill();
                }
                else if (txtSTT.Text == "DATTRUOC")
                {
                    MessageBox.Show("Bàn đã được đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtSTT.Text == "TRONG")
                {
                    MessageBox.Show("Bàn này đang trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }



        public void skin()
        {
            DevExpress.LookAndFeel.DefaultLookAndFeel themes = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            themes.LookAndFeel.SkinName = "Coffee";
        }

        private void btnGop_Click(object sender, EventArgs e)
        {
            Gop();
        }
        private void Gop()
        {
            try
            {
                if (txtSTT.Text == "ONLINE")
                {
                    Gop_Ban addF = new Gop_Ban(txtNameTable.Text);
                    addF.ShowDialog();
                    this.Show();
                    loaddataTable();
                    loaddataBill();
                }
                else if (txtSTT.Text == "DATTRUOC")
                {
                    MessageBox.Show("Bàn đã được đặt", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtSTT.Text == "TRONG")
                {
                    MessageBox.Show("Bàn này đang trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }
    }
}
