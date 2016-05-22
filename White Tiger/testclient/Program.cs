using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using Hydrobase;

//using White_TigerReference;

namespace testclient
{
    class Program
    {
        static void Main(string[] args)
        {
           // try
            {
             //   string[] cells = { Hydrobase.BaseClass.FileTagPrimKey + "test", "irock" };
               // string dbname = "test";
                string PassWord = "serres1984";

                Streamhandler strhhandler = new Streamhandler();
                DBHandler dbhandl = new DBHandler();
             White_TigerServiceReference.WhiteTigerServiceClient client = new testclient.White_TigerServiceReference.WhiteTigerServiceClient();
                System.Console.WriteLine("White Tiger service Test Ver {0}", client.GetVersion());

                // White_TigerServiceReference.DataInfo inf = new testclient.White_TigerServiceReference.DataInfo();
                // string dbname = dbname;
            //    string username = Environment.UserName;
                //string tablename = "testtable";
                List<string> TableCells = new List<string>();
                
                             
                string cmd;
                while ((cmd = Console.ReadLine()) != "exit")
                {
                    switch (cmd)
                    {
                        case "create database":
                            {

                                string username, pass, dbname;
                                System.Console.WriteLine("Type username :");
                                username = Console.ReadLine();
                                System.Console.WriteLine("Type Password :");
                                pass = Console.ReadLine();
                                Console.WriteLine("Type database's Name:");
                                dbname = System.Console.ReadLine();
                                client.ClientCredentials.Windows.ClientCredential.UserName = username;
                                client.ClientCredentials.Windows.ClientCredential.Password = pass;
                
                                client.CreateDataBase(username, pass, dbname);
                                break;

                            }
                        case "create table":
                            {


                                string username, pass, dbname,tablename,celstr;
                                List<string> cells = new List<string>();
                                System.Console.WriteLine("Type username :");
                                username = Console.ReadLine();
                                System.Console.WriteLine("Type Password :");
                                pass = Console.ReadLine();
                                Console.WriteLine("Type database's Name:");
                                dbname = System.Console.ReadLine();
                                Console.WriteLine("Type table's Name:");
                               tablename  = System.Console.ReadLine();
                               Console.WriteLine("Type cells divided with ','");
                               celstr = Console.ReadLine();
                               cells.AddRange(celstr.Split(','));
                               client.ClientCredentials.Windows.ClientCredential.UserName = username;
                               client.ClientCredentials.Windows.ClientCredential.Password = pass;
                
                               client.CreateTable(BaseClass.tabletag, BaseClass.recordtag, username, pass, dbname, tablename, cells);
                               break;                          


                            }
                        case "encrypt":
                            {
                                string username, pass, dbname, tablename, celstr;
                                List<List<object>> cells = new List<List<object>>();
                                System.Console.WriteLine("Type username :");
                                username = Console.ReadLine();
                                System.Console.WriteLine("Type Password :");
                                pass = Console.ReadLine();
                                Console.WriteLine("Type database's Name:");
                                dbname = System.Console.ReadLine();
                                Console.WriteLine("Type table's Name:");
                                tablename = System.Console.ReadLine();
                                
                                client.ClientCredentials.Windows.ClientCredential.UserName = username;
                                client.ClientCredentials.Windows.ClientCredential.Password = pass;
                             string xmlcont=client.LoadTable(BaseClass.recordtag, username, pass, dbname, tablename);

                             DataSet set = new DataSet();
                                set.ReadXml(strhhandler.StringToStream(xmlcont));
                                dbhandl.addrows(set.Tables[0]);

                                foreach (DataRow row in set.Tables[0].Rows)
                                {
                                    List<object> tlis = new List<object>();
                                    tlis.AddRange(row.ItemArray);
                                    cells.Add(tlis);

                                }

                       client.Encrypt(BaseClass.tabletag, BaseClass.recordtag, username, dbname, tablename, cells, pass, Cryptography.CryptograhyAlgorithm.rijdael, Cryptography.HashingAlogrithm.SHA384);
                                

                                

                                break;
                            }
                        case "decrypt":
                            {
                                string username, pass, dbname, tablename, celstr;
                                System.Console.WriteLine("Type username :");
                                username = Console.ReadLine();
                                System.Console.WriteLine("Type Password :");
                                pass = Console.ReadLine();
                                Console.WriteLine("Type database's Name:");
                                dbname = System.Console.ReadLine();
                                Console.WriteLine("Type table's Name:");
                                tablename = System.Console.ReadLine();
                                List<List<object>> vals = new List<List<object>>();
                                vals= client.Decrypt(BaseClass.tabletag, BaseClass.recordtag, username, dbname, tablename, pass, Cryptography.CryptograhyAlgorithm.rijdael, Cryptography.HashingAlogrithm.SHA384);
                                

                               

                                break;
                            }
                        case "backup":
                            {
                                 string username, pass, dbname, tablename, celstr;
                                System.Console.WriteLine("Type username :");
                                username = Console.ReadLine();
                                System.Console.WriteLine("Type Password :");
                                pass = Console.ReadLine();
                                Console.WriteLine("Type database's Name:");
                                dbname = System.Console.ReadLine();
                                Console.WriteLine("Type table's Name:");
                                tablename = System.Console.ReadLine();

                                client.ClientCredentials.Windows.ClientCredential.UserName = username;
                                client.ClientCredentials.Windows.ClientCredential.Password = pass;
                                client.Backup(username, dbname, tablename, pass);
                                break;

                                

                            }
                        case "list databases":
                            {
                                string username, pass, dbname, tablename, celstr;
                                System.Console.WriteLine("Type username :");
                                username = Console.ReadLine();
                                System.Console.WriteLine("Type Password :");
                                pass = Console.ReadLine();
                            List<string> dbs=    client.ListDatabases(username, pass);
                            foreach (string fld in dbs)
                            {

                                Console.WriteLine(fld);
                            }
                                break;

                            }
                    }



                }
            

           /* catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                System.Console.ReadLine();

            }*/
        }
    }
    }
}
