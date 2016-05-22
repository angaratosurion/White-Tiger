using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hydrobase;
using HydrobaseSDK;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Reflection;

namespace White_Tiger
{
    /// <summary>
    /// Handles the configuration of the server
    /// </summary>
    public class Config
    {
        static  string[] cells = {BaseClass.FileTagPrimKey+"FileSecurity",
                                     BaseClass.FileTagPrimKey+"ServiceUserName",
                                     BaseClass.FileTagPrimKey+"ServicePassword",
                                 BaseClass.FileTagPrimKey+"AllowServiceTocreateUsers",
                                 BaseClass.FileTagPrimKey+"Auto_Discovery",BaseClass.FileTagPrimKey+"usecompresionn",
                                 BaseClass.FileTagPrimKey+"auto_backup",BaseClass.FileTagPrimKey+"Windows_autherication"};
       public  const string confile="config.xml",defaultusername="white tiger",defaultpass="aprser15@84";
        hydrobaseADO ado = new hydrobaseADO();
        DataSet set = new DataSet();
        /// <summary>
        /// Creates config file
        /// </summary>
        public void CreateFile()
        {
            try
            {
                
                if(File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "") + "\\" + confile)!=true)

                {
                    ado.TableCreation(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "") + "\\" + confile,cells,"ffff");
                    object[] vals = new object[cells.Length];
                    vals[0] = false;
                    vals[1] = defaultusername;
                    vals[2] = defaultpass;
                    vals[3] = false;
                    vals[4] = false;
                    vals[5] = false;
                    vals[6] = true;
                    vals[7] = false;


                    ado.AttachDataBaseinDataSet(set, Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "") + "\\" + confile);
                    if (set.Tables[0] != null)
                    {
                        set.Tables[0].Rows[0].ItemArray = vals;
                        ado.SaveTable(set, Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "") + "\\" + confile, 0, "");


                    }

                }


            }
            catch (Exception e)
            {

                program.errorreport(e);
              
            }


        }
        /// <summary>
        /// Reads Config file
        /// </summary>
        public void ReadConfig()
        {

            try
            {
                if (File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "") + "\\" + confile) == true)
                {
                    WhiteTigerService.pref = new prefs();

                    ado.AttachDataBaseinDataSet(set,Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "") + "\\" + confile);
                    if (set.Tables[0] != null)
                    {
                        object[] val = set.Tables[0].Rows[0].ItemArray;
                        if (val != null)
                        {
                            if (val[0] != null)
                            {
                                WhiteTigerService.pref.Filesecurity = Convert.ToBoolean(val[0]);
                            }
                            else
                            {
                                WhiteTigerService.pref.Filesecurity = false;

                            }
                            if (val[1] != null)
                            {
                                WhiteTigerService.pref.ServiceUserName = Convert.ToString(val[1]);
                            }
                            else
                            {
                                WhiteTigerService.pref.ServiceUserName = defaultusername;

                            }
                            if (val[2] != null)
                            {
                                WhiteTigerService.pref.ServicePassword = Convert.ToString(val[2]);
                            }
                            else
                            {
                                WhiteTigerService.pref.ServicePassword = defaultpass;

                            }
                            if (val[3] != null)
                            {
                                WhiteTigerService.pref.AllowServiceToCreateUser = Convert.ToBoolean(val[3]);
                            }
                            else
                            {
                                WhiteTigerService.pref.AllowServiceToCreateUser = true;

                            }
                            if (val[4] != null)
                            {
                                WhiteTigerService.pref.Auto_Discovery = Convert.ToBoolean(val[4]);
                            }
                            else
                            {
                                WhiteTigerService.pref.Auto_Discovery = false;

                            }
                            if( val[5]!=null)
                            {
                                WhiteTigerService.pref.UseCompressition = Convert.ToBoolean(val[5]);

                            }
                            else
                            {
                                WhiteTigerService.pref.UseCompressition = false;
                            }
                            if (val[6]!=null)
                            {
                                WhiteTigerService.pref.AutoBackup = Convert.ToBoolean(val[6]);

                            }
                            else
                            {
                                WhiteTigerService.pref.AutoBackup = true;
                            }
                            if (val[7] != null)
                            {
                                WhiteTigerService.pref.WindowsAutherication= Convert.ToBoolean(val[7]);

                            }
                            else
                            {
                                WhiteTigerService.pref.WindowsAutherication= true;
                            }
                        }

                    }

                }


            }
            catch (Exception e)
            {

                program.errorreport(e);

            }

        }
    }
}
