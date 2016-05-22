using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
 
using Hydrobase;
using HydrobaseSDK;

namespace White_Tiger_Managment
{
    public partial class EditCell : Form

    {
        public DataSet Data = null, TargData;
        public string tablename = null, Dabasepath = null;
        hydrobaseADO hbADO = new hydrobaseADO();
        public string[] Cells;

        public EditCell()
        {
            InitializeComponent();
        }

        private void EditCell_Load(object sender, EventArgs e)
        {

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
        private void btnChange_Click(object sender, EventArgs ev)
        {
            try
            {
                int i;
                string tmp;
              

                if ((cmboCells.Text != null) && (this.txtCellName.Text != null)&&(Data!=null) &&(Data.Tables.Count >0))
                {
                    
                   // hydrobaseADO hbAdo = new hydrobaseADO();
                    i = cmboCells.SelectedIndex;
                    tmp=checkandfixcellname(this.txtCellName.Text);
                    if (tmp != null)
                    {

                        MessageBox.Show("Isn't Implemented Yet");
                        //MessageBox.Show("Please Reload the Table to see the changes.");
                    }





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