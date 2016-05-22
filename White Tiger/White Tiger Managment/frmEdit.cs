using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel.Discovery;
using System.ServiceModel;
using System.Net;
using Hydrobase;
using HydrobaseSDK;

namespace White_Tiger_Managment
{
    public partial class frmEdit : Form
    {

        Util util = new Util();
      //public static White_Tiger.IWhiteTigerService Program.client;
      public DataGridView grid;
      public   string dbname,tablename;
      public   DataSet set;
        public Hydrobase.hydrobaseADO ado= new Hydrobase.hydrobaseADO();
      
        public frmEdit()
        {
            try
            {
              
                InitializeComponent();
                grid = this.dataGridView1;
                Program.ActiveEditForm = this;
              
              
            }
           catch (Exception ex)
            {
                System.Net.WebException webex = new System.Net.WebException();
                MessageBox.Show(ex.ToString());
               if (ex.GetType() != webex.GetType())
                {
                    Program.errorreport(ex);
                }
               else
               {

                   MessageBox.Show(ex.Message);

               }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
             //Program.client.Close();
            this.set.Dispose();
            this.grid.Dispose();
             this.Close();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Login login = new Login();
                login.Show();

            }
            catch (Exception ex)
            {
                System.Net.WebException webex = new System.Net.WebException();
                if (ex.GetType() != webex.GetType())
                {
                    Program.errorreport(ex);
                }
                else
                {

                    MessageBox.Show(ex.Message);

                }

            }
        }

        private void tableToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        

        private void updateTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if ((MainWindow.username != null) && (MainWindow.pass != null))
                {
                    List<object[]> vals = new List<object[]>();
                    foreach (DataRow row in set.Tables[0].Rows)
                    {

                     //   List<object> vals2 =new  List<object>(); 
                     //   vals2.AddRange(row.ItemArray);
                        vals.Add(row.ItemArray);
                        
                    }
                    Program.client.AddRows(Hydrobase.BaseClass.tabletag, Hydrobase.BaseClass.recordtag, MainWindow.username, MainWindow.pass, dbname, set.Tables[0].TableName, vals.ToArray(), true);
                    Program.client.UpdateTable(Hydrobase.BaseClass.tabletag, Hydrobase.BaseClass.recordtag, MainWindow.username, MainWindow.pass, dbname, set.Tables[0].TableName, vals.ToArray());
                }
                else
                {
                    Login log = new Login();
                    log.Show();
                    List<object[]> vals = new List<object[]>();
                    foreach (DataRow row in set.Tables[0].Rows)
                    {
                       // List<object> vals2 = new List<object>();
                       // vals2.AddRange(row.ItemArray);
                        vals.Add(row.ItemArray);
                        
                    }
                    Program.client.AddRows(Hydrobase.BaseClass.tabletag, Hydrobase.BaseClass.recordtag, MainWindow.username, MainWindow.pass, dbname, set.Tables[0].TableName, vals.ToArray(), true);
                    grid = new DataGridView();
                    set.ReadXml(util.StringToStream(Program.client.LoadTable(BaseClass.recordtag, MainWindow.username, MainWindow.pass, dbname,tablename)));
                    //MessageBox.Show(util.StringToStream(frmMain.Program.client.LoadTable(BaseClass.recordtag, frmMain.username, frmMain.pass, lstDbs.Text, lsttables.Text)).ToString());

                    grid.DataSource = set;
                    grid.DataMember = set.Tables[set.Tables.Count - 1].TableName;
                
                }


            }
            catch (Exception ex)
            {
                System.Net.WebException webex = new System.Net.WebException();
                if (ex.GetType() != webex.GetType())
                {
                    Program.errorreport(ex);
                }
                else
                {

                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void addColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddCols addcol = new AddCols();
                addcol.Show();

            }
            catch (Exception ex)
            {

                System.Net.WebException webex = new System.Net.WebException();
                if (ex.GetType() != webex.GetType())
                {
                    Program.errorreport(ex);
                }
                else
                {

                    MessageBox.Show(ex.Message);

                }
            }

        }

        private void dataGridView1_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                

            }
            catch (Exception ex)
            {


            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void encryptToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if ((MainWindow.username != null) && (MainWindow.pass != null))
            {
                List<object[]> vals = new List<object []>();
                foreach (DataRow row in set.Tables[0].Rows)
                {

                    //List<object> vals2 = new List<object>();
                   // vals2.AddRange(row.ItemArray);
                    vals.Add(row.ItemArray);

                }
                if (MainWindow.passphrase != null)
                {
                    Program.client.Encrypt(BaseClass.tabletag, BaseClass.recordtag, MainWindow.username, dbname, tablename, vals.ToArray(), MainWindow.pass, Cryptography.CryptograhyAlgorithm.rijdael.ToString(), Cryptography.HashingAlogrithm.SHA384.ToString(), MainWindow.passphrase);

                }
                else
                {
                    MainWindow.passphrase = Microsoft.VisualBasic.Interaction.InputBox("Type toyr passphrase");
                    if (MainWindow.passphrase != null)
                    {
                        Program.client.Encrypt(BaseClass.tabletag, BaseClass.recordtag, MainWindow.username, dbname, tablename, vals.ToArray(), MainWindow.pass, Cryptography.CryptograhyAlgorithm.rijdael.ToString(), Cryptography.HashingAlogrithm.SHA384.ToString(), MainWindow.passphrase);

                    }

                }
            }
            else
            {

                Login login = new Login();
                login.Show();
                List<object[]> vals = new List<object[]>();
                foreach (DataRow row in set.Tables[0].Rows)
                {

                  //  List<object> vals2 = new List<object>();
                    //vals2.AddRange(row.ItemArray);
                    vals.Add(row.ItemArray);

                }
                if (MainWindow.passphrase != null)
                {
                    Program.client.Encrypt(BaseClass.tabletag, BaseClass.recordtag, MainWindow.username, dbname, tablename, vals.ToArray(), MainWindow.pass, Cryptography.CryptograhyAlgorithm.rijdael.ToString(), Cryptography.HashingAlogrithm.SHA384.ToString(), MainWindow.passphrase);
                }
                else
                {
                    MainWindow.passphrase = Microsoft.VisualBasic.Interaction.InputBox("Type toyr passphrase");
                    if (MainWindow.passphrase != null)
                    {
                        Program.client.Encrypt(BaseClass.tabletag, BaseClass.recordtag, MainWindow.username, dbname, tablename, vals.ToArray(), MainWindow.pass, Cryptography.CryptograhyAlgorithm.rijdael.ToString(), Cryptography.HashingAlogrithm.SHA384.ToString(), MainWindow.passphrase);
                    }
                }

            }
        }

        private void frmEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.client.CloseTable(MainWindow.username, MainWindow.pass, dbname, tablename);

            this.Dispose();
        }

        private void frmEdit_Activated(object sender, EventArgs e)
        {
            try
            {

                Program.ActiveEditForm = this;
            }
            catch (Exception ex)
            {

                Program.errorreport(ex);
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                findwnd srcwind = new findwnd();
                srcwind.tablename = this.tablename;
                srcwind.Kelia = new string[grid.ColumnCount];
                for (int i = 0; i < srcwind.Kelia.Length; i++)
                {

                    srcwind.Kelia[i] = grid.Columns[i].Name;
                }
                srcwind.Show();
                
            }
            catch (Exception ex)
            {

                Program.errorreport(ex);
            }
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.client.CloseTable(MainWindow.username, MainWindow.pass, dbname, tablename);


        }

        

        
    }
}
