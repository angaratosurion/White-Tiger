using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace White_Tiger_Managment
{
    public partial class AddCols : Form
    {
        public AddCols()
        {
            InitializeComponent();
        }
       

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i=0; i < lstCells.SelectedItems.Count; i++)
                {

                    lstCells.Items.RemoveAt(i);

                }

            }
            catch (Exception ex)
            {
                Program.errorreport(ex);

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtColname.Text != null)
                {
                    if (cbxPrimaryKey.Checked == true)
                    {

                        lstCells.Items.Add(Hydrobase.BaseClass.CapTagPrimKey + txtColname.Text.Replace(" ","_"));
                    }
                    else
                    {
                        lstCells.Items.Add( txtColname.Text.Replace(" ", "_"));

                    }
                }

            }
            catch (Exception ex)
            {

                Program.errorreport(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Program.errorreport(ex);

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> col = new List<object>();
                for (int i = 0; i < lstCells.Items.Count; i++)
                {
                    col.Add(lstCells.Items[i]);
                }
                Program.client.AddColumns(Hydrobase.BaseClass.tabletag, Hydrobase.BaseClass.recordtag, MainWindow.username, MainWindow.pass, Program.ActiveEditForm.dbname, Program.ActiveEditForm.set.Tables[0].TableName, col.ToArray(), true);
                this.Close();
                
            }
            catch (Exception ex)
            {
                Program.errorreport(ex);

            }
        }
    }
}
