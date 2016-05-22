using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Hydrobase;
using NLog;
// 

namespace White_Tiger_Managment
{
    public partial class findwnd : Form
    {
        DataSet DtsResults, DtsData;
        //XMLhydrobase HBClass;
        hydrobaseADO HBClassADO;
        internal static Logger logman = NLog.LogManager.GetCurrentClassLogger();
      //  TheDarkOwlLogger.TheDarkOwlLogger Bugger = new TheDarkOwlLogger.TheDarkOwlLogger();
        string AppFolder;
        public static bool timirdbSearchForeMatching, timirdbSearchEndsWith, timirdbSearchStartsWith;
        public string tablename;
        public string[] Kelia;

        public findwnd()
        {
            InitializeComponent();
        }

        private void btnSearch_click(object sender, EventArgs er)
        {
            try
            {
                frmResults frmRes = new frmResults();
                //MessageBox.Show(txtSearched.Text);
                //MessageBox.Show(this.lstFields.Text);
               if ((this.txtSearched.Text != null)||(this.lstFields.Text!=null))
                {   
                    frmRes.KeimenoAnaz = this.txtSearched.Text;
                    frmRes.PedAnaz = this.lstFields.Text;

                    ////MessageBox.Show(frmRes.PedAnaz);
                    timirdbSearchEndsWith = rdbSearchEndsWith.Checked;
                    timirdbSearchStartsWith = rdbSearchStartsWith.Checked;
                    timirdbSearchForeMatching = rdbSearchForeMatching.Checked;
                    frmRes.tablename = tablename;
                   
                   frmRes.Show();
                    this.Close();

                }


            }
            catch (Exception e)
            {
                Program.errorreport(e);

            }


        }
        private void findwnd_Load(object sender, EventArgs e)
        {
            int i;
            //White_Tiger_Managment.Mainform
            //this.Text = "Ψάξε το βιβλίο που θές.";
            //Mainform bokmain = new Mainform();

            /*if (tablename == Mainform.disctable)
            {

                this.lstFields.Items.AddRange(Mainform.discKeliaDsc);
            }
            else
            {
                this.lstFields.Items.AddRange(Mainform.casesKelia );

            }*/
            lstFields.Items.AddRange(Kelia );

          
        }
        private void lstFields_Click(object sender, EventArgs er)
        {
            try
            {
                this.lblMsg.Text = "Search in Cell  " + lstFields.Text + " for: ";


            }
            catch (Exception e)
            {
                Program.errorreport(e);

            }



        }
        private void lstFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstFields_Click(sender, e);
        }
    }
}