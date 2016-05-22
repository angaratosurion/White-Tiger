using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Hydrobase;
using HydrobaseSDK;
using System.Globalization;
// 



namespace White_Tiger_Managment
{      
        
    public partial  class frmResults : Form
    {
        DataSet DtsResults;
        //XMLhydrobase HBClass;
        hydrobaseADO HBClassADO;
 public  string   AppFolder;
        public string KeimenoAnaz,PedAnaz;
        CultureInfo GrCult1 = new CultureInfo("el-GR");
        CultureInfo GrCult2 = new CultureInfo("el");
     //   TheDarkOwlLogger.TheDarkOwlLogger Bugger = new TheDarkOwlLogger.TheDarkOwlLogger();
        public string tablename;


        const int searchEndsWith = 1;
        const int searchStartsWith = 2;
        const int searchMatchesWith = 3;
         static PluginCollection SearchplugColl;
         //PluginHundler.PluginHundler plugHundler = new PluginHundler.PluginHundler();
        /*
        protected override void OnLoad(EventArgs e)
        {
            this.copyplugins();
            this.OnLoad(e);

        }
        void copyplugins()
        {
            try
            {
                int i;
                string[] infoparam = new string[6];
                SearchplugColl = new PluginCollection();
                if (Mainform.Loaded_plugins != null)
                {
                    //SearchplugColl= Mainform.pluginHundel.FindAPluginsBasedOnKind(Mainform.Loaded_plugins, Convert.ToString(SDKBase.HydroPluginKind.SearchHydroPlugin));
                  for (i = 0; i < Mainform.Loaded_plugins.Count; i++)
                    {
                        if (Mainform.pluginHundel.FindAPluginBasedOnKind(Mainform.Loaded_plugins, Convert.ToString(SDKBase.HydroPluginKind.SearchHydroPlugin)) != null)
                        {
                            SearchplugColl.Add(Mainform.pluginHundel.FindAPluginBasedOnKind(Mainform.Loaded_plugins, Convert.ToString(SDKBase.HydroPluginKind.SearchHydroPlugin)));
                        }

                    }
                    Mainform.pluginHundel.CopyPluginBasedOnKind(Mainform.Loaded_plugins,SearchplugColl,Convert.ToString(SDKBase.HydroPluginKind.SearchHydroPlugin));
                }

            }
            catch (Exception e)
            {
                this.Bugger.TakeTheException2(e, Application.ProductName, Application.ProductVersion);
            }

        }
        public void ExecutePlugins(params string[] tparam)
        {

            try
            {
                string tmp;

                //hydrobaseADO hbAdo = new hydrobaseADO();
                //MessageBox.Show(Convert.ToString(Loaded_plugins.Count));


                // PluginContent context = new PluginContent();

                //PluginContent context = (PluginContent)hbAdo;
                //MessageBox.Show(context.ToString());

                if (SearchplugColl != null)
                {
                    //MessageBox.Show("hi");

                    foreach (GeneralPluginSDK plugin in SearchplugColl )
                    {
                        PluginContent context = new PluginContent();
                        //MessageBox.Show(context.ToString());
                        
                        plugin.MakeAction(context, this, null, tparam);
                        //MessageBox.Show(Convert.ToString(this.Tag));
                        
                        tmp = Convert.ToString(this.Tag);
                        //MessageBox.Show(tmp);



                    }
                   

                    //MessageBox.Show(Convert.ToString(Loaded_plugins.Count));

                }
            




            catch (Exception e)
            {
                Bugger.TakeTheException2(e, Application.ProductName, Application.ProductVersion);


            }



        }
        */
        public frmResults()
        {
            //HBClass = new XMLhydrobase();
            HBClassADO = new hydrobaseADO();
            InitializeComponent();
        }
        private int findWhoRadioButtonisChecked()
        {
            try
            {
                int ap=0;
                if (findwnd.timirdbSearchEndsWith == true)
                {
                    ap = searchEndsWith;

                }
                else if (findwnd.timirdbSearchStartsWith == true)
                {
                    ap = searchStartsWith;

                }
                else if (findwnd.timirdbSearchForeMatching  == true)
                {
                    ap = searchMatchesWith;

                }
                return ap;


            }
            catch (Exception e)
            {
                Program.errorreport(e);
                return -1;
            }


        }
        private void frmResults_Load(object sender, EventArgs e)
        {
            try
            {
               this.AppFolder = SDKBase.UserMainPath;
                
               /* if ((Mainform.AppFolder == "") || (Mainform.AppFolder==null))
                {
                   this.AppFolder = MultyUser.GetUsersMainFolder(Environment.UserName);
                }

                if ((White_Tiger_Managment.LocaleHundler.SelectedCulture.DisplayName == GrCult1.DisplayName) || (White_Tiger_Managment.LocaleHundler.SelectedCulture.DisplayName == GrCult2.DisplayName))
                {
                    this.Text = "Αποτελέσματα";

                }
                else
                {
                    this.Text = "Results";

                
                }*/

                //this.AppFolder = GeneralPluginSDK.AppFolder;

                this.Text = "Results";



                int j,i;
                //White_Tiger_Managment.Mainform tdiscm = new Mainform();

                //AppFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", " ");
                //MessageBox.Show(this.AppFolder);
                //DataRow dtrRows;
               
                DtsResults = new DataSet();
               //  Program.ActiveEditForm.set = new DataSet();
               // Program.ActiveEditForm.set.ReadXml(AppFolder+"\\discs\\" +tablename);
                //MessageBox.Show(Convert.ToString(this. Program.ActiveEditForm.set.Tables.Count ));
              // HBClassADO.AttachDataBaseinDataSet( Program.ActiveEditForm.set, this.AppFolder + "\\" + BaseClass.DataFolder + "\\discs\\" + tablename + BaseClass.default_fileextension);
               // if (tablename == Mainform.disctable)
                {
                    HBClassADO.AttachDataBaseinDataSet( Program.ActiveEditForm.set, this.AppFolder + "\\" + BaseClass.tmpFolder + "\\" + tablename + BaseClass.default_fileextension);

                    // Program.ActiveEditForm.set.ReadXml(this.AppFolder + "\\" + BaseClass.tmpFolder + "\\" + tablename + BaseClass.default_fileextension);

                    ////MessageBox.Show( Program.ActiveEditForm.set.Tables[0].TableName );
                    ////MessageBox.Show( Program.ActiveEditForm.set.Tables[0].Columns[0].ColumnName);
                    //KeimenoAnaz = null;
                    j = tablename.IndexOf(".");


                    i = this.findWhoRadioButtonisChecked();
                    switch (i)
                    {


                        case searchEndsWith:
                            {
                                HBClassADO.SeatchEndsWith( Program.ActiveEditForm.set, this.PedAnaz, this.KeimenoAnaz, tablename, dgrResults, DtsResults, 0);

                                break;

                            }
                        case searchStartsWith:
                            {
                                HBClassADO.SeatchStartsWith( Program.ActiveEditForm.set, this.PedAnaz, this.KeimenoAnaz, tablename, dgrResults, DtsResults, 0);
                                break;

                            }
                        case searchMatchesWith:
                            {
                                HBClassADO.Search( Program.ActiveEditForm.set, this.PedAnaz, this.KeimenoAnaz, tablename, dgrResults, DtsResults, 0);
                                break;
                            }



                    }
                }
                /*else
                {
                   // MessageBox.Show(tablename);


                    HBClassADO.AttachDataBaseinDataSet( Program.ActiveEditForm.set, this.AppFolder + "\\" + BaseClass.tmpFolder + "\\" + Mainform.tcases  + BaseClass.default_fileextension);

                    // Program.ActiveEditForm.set.ReadXml(this.AppFolder + "\\" + BaseClass.tmpFolder + "\\" + tablename + BaseClass.default_fileextension);

                    ////MessageBox.Show( Program.ActiveEditForm.set.Tables[0].TableName );
                    ////MessageBox.Show( Program.ActiveEditForm.set.Tables[0].Columns[0].ColumnName);
                    //KeimenoAnaz = null;
                    j = tablename.IndexOf(".");


                    i = this.findWhoRadioButtonisChecked();
                    switch (i)
                    {


                        case searchEndsWith:
                            {
                                HBClassADO.SeatchEndsWith( Program.ActiveEditForm.set, this.PedAnaz, this.KeimenoAnaz, Mainform.casestable , dgrResults, DtsResults, 0);

                                break;

                            }
                        case searchStartsWith:
                            {
                                HBClassADO.SeatchStartsWith( Program.ActiveEditForm.set, this.PedAnaz, this.KeimenoAnaz, Mainform.casestable, dgrResults, DtsResults, 0);
                                break;

                            }
                        case searchMatchesWith:
                            {
                                HBClassADO.Search( Program.ActiveEditForm.set, this.PedAnaz, this.KeimenoAnaz, Mainform.casestable, dgrResults, DtsResults, 0);
                                break;
                            }



                    }

                } */


                //DtsResults.Tables.Add(dtbTable);
                //DtsResults.ReadXml(hydrobase.arxapotel);
                //this.dgrResults.DataSource=this.dgrResults;
                // this.dgrResults.DataMember = hydrobase.eggrafi;


                //HBClassADO.SendtoDataGrid(this.dgrResults, this.DtsResults , hydrobase.eggrafi + "_" + "_" + tdiscm.KeliaDsc[0] + " " + hydrobase.arxapotel);

            }
            catch (Exception ex)
            {
                Program.errorreport(ex);
            }


        }

        private void dgrResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}