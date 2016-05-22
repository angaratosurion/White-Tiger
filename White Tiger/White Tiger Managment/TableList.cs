using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hydrobase;
using HydrobaseSDK;
using White_Tiger_Managment.White_TigerServiceReference;
using Newtonsoft.Json.Converters;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace White_Tiger_Managment
{
    public partial class TableList : Form
    {
        Util util = new Util();
        public TableList()
        {
            InitializeComponent();
         
           
             
            
          
        }

        private void lstDbs_Click(object sender, EventArgs ev)
        {
            try
            {
               
                lsttables.Items.Clear();
                if ((MainWindow.username != null) && (MainWindow.pass != null))
                {
                    this.lsttables.Items.AddRange(Program.client.ListTables (MainWindow.username, MainWindow.pass,lstDbs.Text).ToArray());
                }
                else
                {

                    Login log = new Login();
                    log.Show();
                    this.lsttables.Items.AddRange(Program.client.ListTables(MainWindow.username, MainWindow.pass, lstDbs.Text).ToArray());
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

        private void lstDbs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lsttables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lsttables_DoubleClick(object sender, EventArgs ev)
        {
            
        }

        private void DBList_Load(object sender, EventArgs ev)
        {
            try
            {
                if (Program.client != null)
                {
                    string[] vals = Program.client.ListDatabases(MainWindow.username, MainWindow.pass).ToArray();
                    
                    if (vals!= null)
                    {
                        lstDbs.Items.AddRange(vals);
                    }
                    else
                    {
                        lstDbs.Items.Add("No DataBases Found!");

                    }
                    
                }

            }
            catch (Exception e)
            {
                Program.errorreport(e);

            }
            
        }

        private void btnLoad_Click(object sender, EventArgs ev)
        {
            try
            {
                hydrobaseADO ado = new hydrobaseADO();
                string zipfile;
                string destdir;
                
                if ((MainWindow.username != null) && (MainWindow.pass != null))
                {
                    
               
                    frmEdit frmedit = new frmEdit();
                    ado.CloseDataBase(frmedit.set);
                    ado.RemoveTableFromDataGrid(frmedit.grid);
                    //frmedit.IsMdiChild = true;
                    frmedit.MdiParent = Program.Mainwnd;
                    frmedit.Show();
                   
                    frmedit.set = new DataSet();
                   
                  //  frmMain.grid = new DataGridView();
                  // MessageBox.Show(Program.client.LoadTable(BaseClass.recordtag, MainWindow.username, MainWindow.pass, lstDbs.Text, lsttables.Text));
                    if (Program.client.IsTableEncrypted(MainWindow.username, MainWindow.pass, lstDbs.Text, lsttables.Text) == false)
                    {


                        RemoteFileInfo zfile = Program.client.LoadTableAsFile(BaseClass.recordtag,
                            MainWindow.username, MainWindow.pass, lstDbs.Text, lsttables.Text);

                        if (zfile != null)
                        {
                            zipfile = Path.Combine(Program.TempPath, zfile.FileName);
                            byte[] str = zfile.FileByteStream;
                            util.ByteToFile(zipfile, str);
                            FastZip z = new FastZip();
                            destdir = Path.Combine(Program.TempPath, Path.GetFileNameWithoutExtension(zfile.FileName));
                            if (!Directory.Exists(destdir))
                            {
                                Directory.CreateDirectory(destdir);
                            }
                            z.ExtractZip(zipfile, destdir, null);
                            string[] files = Directory.GetFiles(destdir);
                            if (files != null)
                            {
                                frmedit.set.ReadXml(files[0]);
                            }


                        }

                        
                        
                      //  frmedit.set.ReadXml(util.StringToStream());
                      
                        
                        
                        //frmedit.set.ReadXml(util.StringToStream(Program.client.LoadTable(BaseClass.recordtag, MainWindow.username, MainWindow.pass, lstDbs.Text, lsttables.Text)));
                       // MessageBox.Show(Program.client.LoadTable(BaseClass.recordtag, MainWindow.username, MainWindow.pass, lstDbs.Text, lsttables.Text));
                    }
                    else
                    {
                        if (MainWindow.passphrase != null)
                        {
                            frmedit.set.ReadXml(util.StringToStream(Program.client.Decrypt(BaseClass.tabletag, BaseClass.recordtag, MainWindow.username, lstDbs.Text, lsttables.Text, MainWindow.pass, Cryptography.CryptograhyAlgorithm.rijdael.ToString().ToString(), Cryptography.HashingAlogrithm.SHA384.ToString().ToString(), MainWindow.passphrase)));
                        }
                        else
                        {
                            MainWindow.passphrase = Microsoft.VisualBasic.Interaction.InputBox("Type your passphrase");
                            if (MainWindow.passphrase != null)
                            {
                                frmedit.set.ReadXml(util.StringToStream(Program.client.Decrypt(BaseClass.tabletag, BaseClass.recordtag, MainWindow.username, lstDbs.Text, lsttables.Text, MainWindow.pass, Cryptography.CryptograhyAlgorithm.rijdael.ToString().ToString(), Cryptography.HashingAlogrithm.SHA384.ToString().ToString(), MainWindow.passphrase)));
                            }
                        }

                    }
                   // MessageBox.Show(Program.client.LoadTable(BaseClass.recordtag,MainWindow.username, MainWindow.pass, lstDbs.Text, lsttables.Text));

                    if (frmedit.set.Tables.Count > 0)
                    {
                        frmedit.grid.DataSource = frmedit.set;
                        frmedit.grid.DataMember = frmedit.set.Tables[frmedit.set.Tables.Count - 1].TableName;
                        ado.ConnectEventstoDataGrid(frmedit.grid);
                        ado.ConnectEventstoDataTable(frmedit.set.Tables[0]);
                        frmedit.Text = frmedit.grid.DataMember;
                        frmedit.dbname = lstDbs.Text;
                        frmedit.tablename = lsttables.Text;
                        frmedit.Text = String.Format("{0}\\{1}", frmedit.dbname, frmedit.tablename);
                    }
                }
                else
                {

                    Login log = new Login();
                    log.Show();
                    frmEdit frmedit = new frmEdit();
                    frmedit.MdiParent = Program.Mainwnd;
                   // frmedit.IsMdiChild = true;
                    frmedit.set = new DataSet();
                    frmedit.set.ReadXml(util.StringToStream(Program.client.LoadTable(BaseClass.recordtag, MainWindow.username, MainWindow.pass, lstDbs.Text, lsttables.Text)));

                    frmedit.grid.DataSource = frmedit.set;
                    frmedit.grid.DataMember = frmedit.set.Tables[frmedit.set.Tables.Count - 1].TableName;
                    frmedit.Text = frmedit.grid.DataMember;
                     frmedit.dbname = lstDbs.Text;
                   frmedit.tablename = lsttables.Text;
                    frmedit.Text= String.Format("{0}\\{1}",frmedit.dbname,frmedit.tablename);
                    ado.ConnectEventstoDataGrid(frmedit.grid);
                    ado.ConnectEventstoDataTable(frmedit.set.Tables[0]);
                }
               
                this.Close();

            }
          catch (Exception e)
            {
                Program.errorreport(e);
                MessageBox.Show(e.ToString());
                this.Close();

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
