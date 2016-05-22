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
    public partial class SelectDB : Form
    {
        public SelectDB()
        {
            InitializeComponent();
        }

        private void SelectDB_Load(object sender, EventArgs ev)
        {
            try
            {
                lstDbs.Items.AddRange(Program.client.ListDatabases(MainWindow.username, MainWindow.pass).ToArray());

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


                Program.ActiveDb = lstDbs.Text;
               // frmEdit.dbname = lstDbs.Text;
                this.Close();

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
    }
}
