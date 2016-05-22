using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Hydrobase;
using HydrobaseSDK;


namespace White_Tiger_Managment
{
    public partial class TableCreation : Form
    {
        //WareInputBox inputBox = new WareInputBox();
        public TableCreation()
        {
            InitializeComponent();
        }
        string checkandfixcellname(string cellname)
        {

            try
            {
                string apot = null, tmp;
                if (cellname != null)
                {

                    if ((cellname.Contains(" ") == true) && (cellname.Contains("/") == true))
                    {
                        // MessageBox.Show("hi");
                        tmp = cellname.Replace(" ", "_");
                        apot = tmp.Replace("/", "-");



                    }

                    else if (cellname.Contains(" ") == true)
                    {



                        apot = cellname.Replace(" ", "_");
                    }
                    else if (cellname.Contains("/") == true)
                    {
                        apot = cellname.Replace("/", "-");

                    }
                    else
                    {
                        apot = cellname;
                    }




                }
                return apot;
            }
            catch (Exception e)
            {

               Program.errorreport(e);
                return null;
            }
        }

        private void TableCreation_Load(object sender, EventArgs ev)
        {
            try
            {
                
               // this.DialogFolder. = HydrobaseSDK.SDKBase.UserBasesPath;
                this.DialogFolder.SelectedPath = HydrobaseSDK.SDKBase.UserBasesPath;

            }
            catch (Exception e)
            {

                Program.errorreport(e);
            }
        }

        private void btnSelectDB_Click(object sender, EventArgs ev)
        {

            try
            {
                SelectDB selectdb = new SelectDB();
                selectdb.Show();
                selectdb.Activate();
                selectdb.BringToFront();
                this.txtTablePath.Text = Program.ActiveDb ;


            }
            catch (Exception e)
            {

                Program.errorreport(e);
            }

        }

      
        private void btnAdd_Click(object sender, EventArgs ev)
        {

            try
            {
                string Cell;

                string tmp;
                tmp = checkandfixcellname(txtCellName.Text);
                if (tmp != null)
                {
                    lstCells.Items.Add(tmp);
                    txtCellName.Text = null;
                }

              
            }

            catch (Exception e)
            {

                Program.errorreport(e);
            }
        }

        private void btnRemove_Click(object sender, EventArgs ev)
        {
            try
            {
                int i;
                if (lstCells.SelectedItem != null)
                {
                    //i=lstCells.SelectedIndex;
                    lstCells.Items.Remove(lstCells.SelectedItem);
                    
                }

            }
            catch (Exception e)
            {

                Program.errorreport(e);
            }
        }

        private void btnCancel_Click(object sender, EventArgs ev)
        {
            try
            {
                this.Close();


            }
            catch (Exception e)
            {
                Program.errorreport(e);
            }
        }

        private void btnCreate_Click(object sender, EventArgs ev)
        {
            try
            {
                hydrobaseADO hbADO = new hydrobaseADO();
               
                int i;
                DataRow row;
                DataSet set = new DataSet();

                if ((this.txtTableName.Text != null) || (this.txtTablePath.Text != null) || (lstCells.Items.Count > 0) )
                {

                   List<string> cells = new List<string>();
                    for (i = 0; i < lstCells.Items.Count; i++)
                    {
                        cells.Add( Convert.ToString(lstCells.Items[i]));

                    }



                    Program.client.CreateTable(BaseClass.tabletag, BaseClass.recordtag, MainWindow.username, MainWindow.pass, txtTablePath.Text, txtTableName.Text, cells.ToArray());

                    
                    
                    
                    //MessageBox.Show(Convert.ToString( set.Tables[0].Columns.Count));
                 //set.Tables[0].Rows.Add(cells);

                }
                this.Close();

            }
            catch (Exception e)
            {
                Program.errorreport(e);
            }

        }

        

    }
}