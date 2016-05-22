using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hydrobase;
using HydrobaseSDK;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Reflection;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Net.Security;
using System.Net;
using System.Timers;
using White_Tiger.ClientManagement;

using System.Security;
using System.Security.Permissions;
using System.Runtime.CompilerServices;
using White_Tiger.Protocols.TCP;
using White_Tiger.UserManagment;
using System.Windows.Forms;

using ICSharpCode.SharpZipLib.Zip;
using System.ServiceModel.Web;
using ICSharpCode.SharpZipLib.Core;
using White_Tiger.DataContracts;
namespace White_Tiger
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]



    // [ServiceBehavior(IncludeExceptionDetailInFaults=true)]

    public class WhiteTigerService : IWhiteTigerService
    {
        TCPServer tcpserver;
        Tools tools = new Tools();
         Events evnts = new Events();
        hydrobaseADO ado = new hydrobaseADO(true);
        internal static DataSetCollection LoadedDb = new DataSetCollection();
        // static List<DataSet> LoadedDb = new List<DataSet>();
        SecurityTG sc = new SecurityTG();
        public static Config conf = new Config();
        public static prefs pref = new prefs();
        UserManager userman = new UserManager();
        internal static List<ClientInfo> ClientIPs = new List<ClientInfo>();

        public WhiteTigerService(bool runtcpserver)
        {
            try
            {
                conf.CreateFile();
                conf.ReadConfig();
                userman.CreateUser(pref.ServiceUserName, pref.ServicePassword, "Default username of whiste tiger Server", true);
                if (White_Tiger.WhiteTigerService.pref.Auto_Discovery == true)
                {





                }
                if( runtcpserver)
                {
                    this.tcpserver = new TCPServer();
                    this.tcpserver.StartServer();
                }
            }
            catch (Exception e)
            {
                program.errorreport(e);
                //  return e.ToString();
            }

        }
        public WhiteTigerService()
        {
            try
            {
                conf.CreateFile();
                conf.ReadConfig();
              //  userman.CreateUser(pref.ServiceUserName, pref.ServicePassword, "Default username of whiste tiger Server", true);
                if (White_Tiger.WhiteTigerService.pref.Auto_Discovery == true)
                {





                }
            }
            catch (Exception e)
            {
                program.errorreport(e);
                //  return e.ToString();
            }


        }
        string ActiveDbpath;
        /// <summary>
        /// Returns the version of White tiger server
        /// </summary>
        /// <returns>Returns the version of White tiger server</returns>
        public string GetVersion()
        {
            try
            {
                string Ekdo = null;



                Ekdo = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                return Ekdo;
            }
            catch (Exception e)
            {

                program.errorreport(e);
                return null;
            }


        }
        /// <summary>
        /// authoricates with the white tiger server using the given usrname and password
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="password">password of the user</param>
        /// <returns>truw if logins successfuly or false if doesnt</returns>
        public bool Login(string username, string password)
        {
            try
            {
                /*var identity = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity;*/

              //  MessageBox.Show(username);
             
                bool isValid = userman.Login(username, password);
                //System.Windows.Forms.MessageBox.Show(isValid.ToString());


                return isValid;

            }
            catch (Exception e)
            {
                program.errorreport(e);
                return false;

            }

        }
        /// <summary>
        /// Create a database with the given name
        /// </summary>
        /// <param name="username">user's name that database belogs</param>
        /// <param name="pass">user's password</param>
        /// <param name="dbname">name of the database</param>
        public void CreateDataBase(string username, string pass, string dbname)
        {
            try
            {
                if ((this.Login(username, pass) == true) && ((username != null) || (string.IsNullOrEmpty(dbname) == false)))
                {
                    // System.Console.WriteLine("hhhh");
                    // System.Windows.Forms.MessageBox.Show("ggg");
                    MultyUser.ReadConfig();
                    ActiveDbpath = MultyUser.GetDataBasePath(username, dbname);
                    SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(username);

                    if (sc.UserHasAccessOnDirectory(username, Path.Combine(SDKBase.UserMainPath, BaseClass.DataFolder)) == true)
                    {
                        ado.DataBaseCreation(dbname, Path.Combine(SDKBase.UserMainPath, BaseClass.DataFolder));
                        sc.SetUserAcessOnDir(username, Path.Combine(SDKBase.UserMainPath, BaseClass.DataFolder) + "\\" + dbname);

                    }

                    Maintenance_Backup bk = new Maintenance_Backup();


                }

            }
            catch (Exception e)
            {
                program.errorreport(e);

            }

        }
        /// <summary>
        /// create a  new table with given,root,record tag ,in the given database of the given user
        /// </summary>
        /// <param name="root">the root element that xml file will have</param>
        /// <param name="recordtag">main child element that will contain the cell's of the 
        /// table as children</param>
        /// <param name="username">name of the user that table belongs</param>
        /// <param name="pass">the password of the user</param>
        /// <param name="dbname">name of the database</param>
        /// <param name="tablename">table's name</param>
        /// <param name="TableCells">list of cells</param>
        public void CreateTable(string root, string recordtag, string username, string pass, string dbname, string tablename, string[] TableCells)
        {
            try
            {
                if ((this.Login(username, pass) == true) && ((string.IsNullOrEmpty(dbname) == false) || (string.IsNullOrEmpty(username) != false)
                    || (TableCells != null) || (string.IsNullOrEmpty(tablename) == false) || (root != null) || (recordtag != null)))
                {

                    MultyUser.ReadConfig();
                    ActiveDbpath = MultyUser.GetDataBasePath(username, dbname);
                    SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(username);
                    if ((root == BaseClass.tabletag) && (recordtag == BaseClass.recordtag))
                    {

                        if (sc.UserHasAccessOnDirectory(username, Path.Combine(SDKBase.UserMainPath, BaseClass.DataFolder)) == true)
                        {

                            ado.TableCreation(Path.Combine(ActiveDbpath, tablename + BaseClass.default_fileextension), TableCells, "hhh");

                            sc.SetUserAcessOnFile(username, Path.Combine(ActiveDbpath, tablename + BaseClass.default_fileextension));


                        }
                    }
                    else
                    {


                        XmlTextWriter xmlWriter;
                        XmlElement xmlRiza = null;
                        XmlDocument xmlDoc = new XmlDocument();


                        xmlWriter = new XmlTextWriter(Path.Combine(ActiveDbpath, tablename + ".xml"), System.Text.Encoding.UTF8);

                        xmlWriter.WriteStartDocument(true);

                        xmlWriter.WriteStartElement(root);





                        xmlRiza = xmlDoc.CreateElement(recordtag);

                        for (int i = 0; i < TableCells.Length; i++)
                        {

                            XmlElement xmlStoixeio = xmlDoc.CreateElement(TableCells[i]);

                            xmlRiza.AppendChild(xmlStoixeio);


                            xmlStoixeio = null;

                            xmlDoc.InsertAfter(xmlRiza, xmlDoc.LastChild);


                        }


                        xmlDoc.Save(xmlWriter);


                        xmlWriter.Flush();


                        xmlWriter.Close();



                        xmlWriter = null;



                        xmlDoc = null;





                    }


                }

            }
            catch (Exception e)
            {
                program.errorreport(e);

            }


        }
        /// <summary>
        /// Load table of given name belonging to  given user name , database.
        /// and returns the contents in xml format
        /// </summary>
        /// <param name="recordtag">Child that contains the cells as children</param>
        /// <param name="username">name of user table belongs to </param>
        /// <param name="pass">password of username</param>
        /// <param name="dbname">name of the database table belongs to</param>
        /// <param name="tablename">name of the table </param>
        /// <returns>returns the contents in xml format</returns>

        public string LoadTable(string recordtag, string username, string pass, string dbname, string tablename)
        {
            try
            {
                string ap = "";

                if ((this.Login(username, pass) == true)
                    && ((string.IsNullOrEmpty(dbname) == false)
                    || (string.IsNullOrEmpty(username) != false) ||
                    (string.IsNullOrEmpty(tablename) == false) || (recordtag != null)))
                {
                    // System.Console.WriteLine("hhhh");
                    // DataSet set;
                    MultyUser.ReadConfig();
                    ActiveDbpath = MultyUser.GetDataBasePath(username, dbname);
                    SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(username);
                    if( pref.AutoBackup)
                    {
                        this.Backup(username, dbname, tablename, pass);
                    }
                    // LoadedDb.SelectDataSet(username + "_" + tablename).Tables.Add();
                    if (recordtag == BaseClass.recordtag)
                    {
                        //if (sc.UserHasAccessOnFile(username, Path.Combine(ActiveDbpath, tablename + BaseClass.default_fileextension)) == true)
                        {
                            //  System.Windows.Forms.MessageBox.Show(Path.Combine(ActiveDbpath, tablename + BaseClass.default_fileextension));
                            LoadedDb.Add(new DataSet(username + "_" + tablename));

                            // System.Windows.Forms.MessageBox.Show(LoadedDb.SelectDataSet(username+"_"+tablename).DataSetName);


                            ado.AttachDataBaseinDataSet(LoadedDb.SelectDataSet(username + "_" + tablename), Path.Combine(ActiveDbpath, tablename + BaseClass.default_fileextension), false, true);
                            // LoadedDb.SelectDataSet(username + "_" + tablename).Tables.Add(username.Replace(" ","_") + "_" + tablename);
                            //  ado.AttachDataBaseinDataTable(LoadedDb.SelectDataSet(username + "_" + tablename).Tables[username + "_" + tablename], Path.Combine(ActiveDbpath, tablename + BaseClass.default_fileextension));
                            if (LoadedDb.SelectDataSet(BaseClass.tabletag) != null)
                            {
                                LoadedDb.SelectDataSet(BaseClass.tabletag).DataSetName = username + "_" + tablename;
                            }
                        
                            //System.Windows.Forms.MessageBox.Show(LoadedDb.SelectDataSet(BaseClass.tabletag).DataSetName);

                            MemoryStream ms = new MemoryStream();
                            //StreamWriter writer = new StreamWriter(ms);
                            //System.Windows.Forms.MessageBox.Show(LoadedDb.SelectDataSet(username + "_" + tablename).Tables.Count.ToString());
                            DataSet tset = new DataSet(username + "_" + tablename);
                            tset.Tables.Add(LoadedDb.SelectDataSet(username + "_" + tablename).Tables[tablename].Copy());



                            tset.Tables[0].TableName = tablename;
                            tset.Tables[0].WriteXml(ms);

                            LoadedDb.SelectDataSet(username + "_" + tablename).Tables[0].RowChanged += new DataRowChangeEventHandler(evnts.TableRowChanged);

                            ap = "";
                            ap = Encoding.UTF8.GetString(ms.ToArray()); ;
                            // System.Windows.Forms.MessageBox.Show(ap.ToString());
                            RemoteEndpointMessageProperty clientEndpoint =
                          OperationContext.Current.IncomingMessageProperties[
                         RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;


                            ClientInfo clientinf = new ClientInfo();
                            clientinf.IP = clientEndpoint.Address;
                            clientinf.Port = clientEndpoint.Port;
                            clientinf.Username = username;
                            clientinf.Table = tablename;
                            ClientIPs.Add(clientinf);
                            return ap;



                        }

                    }
                    else
                    {

                        if (sc.UserHasAccessOnFile(username, Path.Combine(ActiveDbpath, tablename + ".xml")) == true)
                        {
                            if (LoadedDb.SelectDataSet(username + "_" + tablename).Tables.Count > 0)
                            {
                                // ado.AttachDataBaseinDataSet(LoadedDb.SelectDataSet(username + "_" + tablename), Path.Combine(ActiveDbpath, tablename + BaseClass.default_fileextension), false, true);
                                LoadedDb.SelectDataSet(username + "_" + tablename).ReadXml(Path.Combine(ActiveDbpath, tablename + ".xml"));
                                LoadedDb.SelectDataSet(username + "_" + tablename).Tables[LoadedDb.SelectDataSet(username + "_" + tablename).Tables.Count - 1].TableName = username + "/" + tablename;
                                MemoryStream ms = new MemoryStream();
                                //StreamWriter writer = new StreamWriter(ms);

                                DataSet tset = new DataSet();
                                tset.Tables.Add(LoadedDb.SelectDataSet(username + "_" + tablename).Tables[LoadedDb.SelectDataSet(username + "_" + tablename).Tables.Count - 1].Copy());

                                tset.Tables[0].TableName = tablename;
                                tset.Tables[0].WriteXml(ms);



                                ap = Encoding.UTF8.GetString(ms.ToArray()); ;
                                // System.Console.WriteLine(ap.ToString());

                                RemoteEndpointMessageProperty clientEndpoint =
                        OperationContext.Current.IncomingMessageProperties[
                       RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;



                                ClientInfo clientinf = new ClientInfo();
                                clientinf.IP = clientEndpoint.Address;
                                clientinf.Port = clientEndpoint.Port;
                                clientinf.Username = username;
                                clientinf.Table = tablename;
                                ClientIPs.Add(clientinf);
                                return ap;



                            }
                        }
                        else
                        {
                            LoadedDb.SelectDataSet(username + "_" + tablename).ReadXml(Path.Combine(ActiveDbpath, tablename + ".xml"));
                            LoadedDb.SelectDataSet(username + "_" + tablename).Tables[0].TableName = username + "/" + tablename;
                            MemoryStream ms = new MemoryStream();

                            LoadedDb.SelectDataSet(username + "_" + tablename).Tables[0].WriteXml(ms);
                            //   StreamReader rder = new StreamReader(ms);
                            ap = Encoding.UTF8.GetString(ms.ToArray()); ;
                            System.Console.WriteLine(ap.ToString());
                            return ap;

                        }

                    }










                }
                return ap;
            }
            catch (Exception e)
            {
                program.errorreport(e);
                return e.ToString();
            }

        }

        /// <summary>
        /// Updates the given table 
        /// </summary>
        /// <param name="root">the root element the xml file will have </param>
        /// <param name="recordtag">the xml element that will represent the record</param>
        /// <param name="username">name of user that table belongs to</param>
        /// <param name="pass">password of user</param>
        /// <param name="dbname">name of database that table belongs to </param>
        /// <param name="tablename">name of the table</param>
        /// <param name="rows">Rows to addes in table</param>
        public void UpdateTable(string root, string recordtag, string username, string pass, string dbname, string tablename, List<object[]> rows)
        {
            try
            {


                if ((this.Login(username, pass) == true) && ((string.IsNullOrEmpty(dbname) == false) || (tablename != null)
                     || (root != null) || (recordtag != null)))
                //||(PassWord !=null ))// ||(Data !=null))
                {

                    MultyUser.ReadConfig();
                    ActiveDbpath = MultyUser.GetDataBasePath(username, dbname);
                    SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(username);
                    if ((root == BaseClass.tabletag) && (recordtag == BaseClass.recordtag))
                    {
                        //  System.Windows.Forms.MessageBox.Show(tablename);
                        if (rows != null)
                        {
                            if (this.FindTable(username, pass, tablename) != null)
                            {
                                foreach (object[] vals in rows)
                                {
                                    this.FindTable(username, pass, tablename).Rows.Add(vals);
                                }

                            }
                            else
                            {
                                this.LoadTable(recordtag, username, pass, dbname, tablename);
                                this.FindTable(username, pass, tablename).Rows.Clear();
                                foreach (object[] vals in rows)
                                {
                                    this.FindTable(username, pass, tablename).Rows.Add(vals);
                                   
                                }

                            }
                        }
                      //  if (sc.UserHasAccessOnFile(username, Path.Combine(ActiveDbpath, tablename + BaseClass.default_fileextension)) == true)
                        {
                            ado.SaveTable(LoadedDb.SelectDataSet(username + "_" + tablename), Path.Combine(ActiveDbpath, tablename + BaseClass.default_fileextension), this.FindTableIndex(username, pass, tablename), "hhhh");
                        }
                    }
                    else
                    {
                        if (rows != null)
                        {
                            foreach (object[] vals in rows)
                            {
                                this.FindTable(username, pass, tablename).Rows.Add(vals);
                            }
                        }
                      //  if (sc.UserHasAccessOnFile(username, Path.Combine(ActiveDbpath, tablename + ".xml")) == true)
                        {
                            int i = this.FindTableIndex(username, pass, tablename);
                            LoadedDb.SelectDataSet(username + "_" + tablename).Tables[i].TableName = tablename;
                            LoadedDb.SelectDataSet(username + "_" + tablename).Tables[i].WriteXml(Path.Combine(ActiveDbpath, tablename + ".xml"));
                        }

                    }

                }

            }
            catch (Exception e)
            {
                program.errorreport(e);
                //System.Windows.Forms.MessageBox.Show(e.ToString());

                // return null;
            }
        }
        /// <summary>
        /// Find the table with the given name and username and return him
        /// </summary>
        /// <param name="username"> name of user's that table belongs to</param>
        /// <param name="pass">users password</param>
        /// <param name="tablename">name of table</param>
        /// <returns>the table </returns>
        public DataTable FindTable(string username, string pass, string tablename)
        {

            try
            {

                DataTable ap = null;
                if ((this.Login(username, pass) == true) && ((string.IsNullOrEmpty(username) == false) || (string.IsNullOrEmpty(tablename) == false)))
                {

                    if (LoadedDb.SelectDataSet(username + "_" + tablename) != null)
                    {
                        ap = LoadedDb.SelectDataSet(username + "_" + tablename).Tables[0];


                    }


                }


                return ap;
            }



            catch (Exception e)
            {
                program.errorreport(e);
                // System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }

        }
        /// <summary>
        /// Searchs the table and returns it's index
        /// </summary>
        /// <param name="username">user's name</param>
        /// <param name="pass">user's pasword</param>
        /// <param name="tablename">name of the table </param>
        /// <returns>the index in the datatable collection you are searching </returns>
        public int FindTableIndex(string username, string pass, string tablename)
        {

            try
            {

                int ap = -1;
                if ((this.Login(username, pass) == true) && ((string.IsNullOrEmpty(username) == false) || (string.IsNullOrEmpty(tablename) == false)))
                {

                    if (LoadedDb.SelectDataSet(username + "_" + tablename) != null)
                    {
                        ap = LoadedDb.SelectDataSet(username + "_" + tablename).Tables.IndexOf(tablename);


                    }


                }


                return ap;
            }



            catch (Exception e)
            {
                program.errorreport(e);
                // System.Windows.Forms.MessageBox.Show(e.ToString());
                return -2;
            }

        }
        /// <summary>
        /// Adds the given rows and  encrypts the contents of the table using the user's password as passphrase
        /// </summary>
        /// <param name="root">root element of xml files</param>
        /// <param name="recordtag">element that holds the cells</param>
        /// <param name="username">user's name table belongs to</param>
        /// <param name="dbname">name of database</param>
        /// <param name="tablename">name of table</param>
        /// <param name="rows">rows to be added</param>
        /// <param name="pass">password of userfor autherication</param>
        /// <param name="alg">cryptographic algorithm to be used</param>
        /// <param name="hashalg">hashing algorithm to be used for the creation of password</param>
        /// <param name="passphrase">the  passhprase  will be usedfor encryption</param>

        public void Encrypt(string root, string recordtag, string username, string dbname, string tablename, List<object[]> rows, string pass, string alg, string hashalg, string passphrase)
        {
            try
            {
                if ((this.Login(username, pass) == true) && ((username != null) || (pass != null) ||
                    (dbname != null) || (rows != null) || (alg != null)
                    || (hashalg != null) || (dbname != null) || (tablename != null) || (root != null)
                    || (recordtag != null) || (passphrase != null)))
                {
                    MultyUser.ReadConfig();
                    Cryptography.CryptograhyAlgorithm talg = tools.GetCryptographyAlgorythmEnum(alg);
                    Cryptography.HashingAlogrithm thashalg = tools.GetHashingAlgorythmEnum(hashalg);
                    ActiveDbpath = MultyUser.GetDataBasePath(username, dbname);
                    SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(username);
                    Cryptography crp = new Cryptography();
                    if (pref.AutoBackup)
                    {
                        this.Backup(username, dbname, tablename, pass);
                    }
                    this.AddRows(username, pass, dbname, tablename, rows);
                    if ((root == BaseClass.tabletag) && (recordtag == BaseClass.recordtag))
                    {
                        if (talg == Cryptography.CryptograhyAlgorithm.rijdael)
                        {
                            if (thashalg == Cryptography.HashingAlogrithm.SHA384)
                            {
                                if (sc.UserHasAccessOnFile(username, Path.Combine(ActiveDbpath, tablename + BaseClass.defualt_encryptedtableextensionwithSha384)) == true)
                                {
                                    crp.SaveEncryptedDataTable(LoadedDb.SelectDataSet(username + "_" + tablename), ActiveDbpath + "\\" + tablename + BaseClass.defualt_encryptedtableextensionwithSha384, passphrase, this.FindTableIndex(username, pass, tablename), null, Hydrobase.Cryptography.HashingAlogrithm.SHA384, Cryptography.CryptograhyAlgorithm.rijdael);

                                }
                            }
                            else if (thashalg == Cryptography.HashingAlogrithm.SHA1)
                            {
                                if (sc.UserHasAccessOnFile(username, Path.Combine(ActiveDbpath, tablename + BaseClass.defualt_encryptedtableextensionwithSha1)) == true)
                                {
                                    crp.SaveEncryptedDataTable(LoadedDb.SelectDataSet(username + "_" + tablename), ActiveDbpath + "\\" + tablename + BaseClass.defualt_encryptedtableextensionwithSha1, passphrase, this.FindTableIndex(username, pass, tablename), null, Hydrobase.Cryptography.HashingAlogrithm.SHA1, Cryptography.CryptograhyAlgorithm.rijdael);

                                }
                            }
                        }
                        else
                        {

                            System.Windows.Forms.MessageBox.Show("not implemented");
                        }
                    }
                }

            }
            catch (Exception e)
            {
                program.errorreport(e);
                // System.Windows.Forms.MessageBox.Show(e.ToString());
                // return null;
            }

        }
        /// <summary>
        /// Decrypts the table's file and returns the contents.
        /// </summary>
        /// <param name="root">root element of xml files</param>
        /// <param name="recordtag"> >element that holds the cells </param>
        /// <param name="username"> user's name table belongs to </param>
        /// <param name="dbname">name of database </param>
        /// <param name="tablename"> name of table </param>
        /// <param name="pass">password of user</param>
        /// <param name="alg">cryptographic algorithm to be used</param>
        /// <param name="hashalg">hashing algorithm to be used for the creation of password</param>
        /// <param name="passphrase">passphrse to be used for decryption</param>
        /// <returns>contents of the table</returns>

        public string Decrypt(string root, string recordtag, string username, string dbname, string tablename, string pass, string alg, string hashalg, string passphrase)
        {
            try
            {
                string ap = null;
                if ((this.Login(username, pass) == true) && ((username != null) || (pass != null) ||
                   (dbname != null) || (alg != null)
                   || (hashalg != null) || (dbname != null) || (tablename != null) || (root != null)
                   || (recordtag != null) || (passphrase != null)))
                {
                    MultyUser.ReadConfig();
                    ActiveDbpath = MultyUser.GetDataBasePath(username, dbname);
                    SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(username);
                    Cryptography crp = new Cryptography();
                    Cryptography.CryptograhyAlgorithm talg = tools.GetCryptographyAlgorythmEnum(alg);
                    Cryptography.HashingAlogrithm thashalg = tools.GetHashingAlgorythmEnum(hashalg);
                    if (pref.AutoBackup)
                    {
                        this.Backup(username, dbname, tablename, pass);
                    }
                    if ((root == BaseClass.tabletag) && (recordtag == BaseClass.recordtag))
                    {
                        if (talg == Cryptography.CryptograhyAlgorithm.rijdael)
                        {
                            if (thashalg == Cryptography.HashingAlogrithm.SHA384)
                            {
                                crp.SaveDecryptedDataTable(LoadedDb.SelectDataSet(username + "_" + tablename), ActiveDbpath + "\\" + tablename + BaseClass.defualt_encryptedtableextensionwithSha384, passphrase, this.FindTableIndex(username, pass, tablename), null, Hydrobase.Cryptography.HashingAlogrithm.SHA384, Cryptography.CryptograhyAlgorithm.rijdael);
                            }
                            else if (thashalg == Cryptography.HashingAlogrithm.SHA1)
                            {

                                crp.SaveDecryptedDataTable(LoadedDb.SelectDataSet(username + "_" + tablename), ActiveDbpath + "\\" + tablename + BaseClass.defualt_encryptedtableextensionwithSha1, passphrase, this.FindTableIndex(username, pass, tablename), null, Hydrobase.Cryptography.HashingAlogrithm.SHA1, Cryptography.CryptograhyAlgorithm.rijdael);
                            }
                            FindTable(username, pass, tablename).Clear();
                            LoadedDb.SelectDataSet(username + "_" + tablename).Tables[this.FindTableIndex(username, pass, tablename)].Dispose();
                            ado.AttachDataBaseinDataSet(LoadedDb.SelectDataSet(username + "_" + tablename), ActiveDbpath + "\\" + tablename + BaseClass.default_fileextension);
                            MemoryStream ms = new MemoryStream();
                            FindTable(username, pass, tablename).WriteXml(ms);
                            ap = Convert.ToString(Encoding.UTF8.GetString(ms.ToArray()));
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("not implemented");


                    }
                }

                return ap;


            }
            catch (Exception e)
            {
                program.errorreport(e);
                // System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }

        }
        /// <summary>
        /// Backups of table
        /// </summary>
        /// <param name="username">user's name table belongs to</param>
        /// <param name="dbname">name of database</param>
        /// <param name="table"> name of table</param>
        /// <param name="pass">password of user</param>
        public void Backup(string username, string dbname, string table, string pass)
        {
            try
            {
                if ((this.Login(username, pass) == true) && ((username != null) || (pass != null) ||
                    (dbname != null)))
                {
                    MultyUser.ReadConfig();
                    ActiveDbpath = MultyUser.GetDataBasePath(username, dbname);
                    SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(username);
                    ado.CreateBackupofDB(ActiveDbpath, "*.*", SDKBase.UserMainPath, dbname,false);



                }


            }
            catch (Exception e)
            {
                program.errorreport(e);
                // System.Windows.Forms.MessageBox.Show(e.ToString());
                //return null;
            }

        }
        /// <summary>
        /// Search  the table for the given value in gthe specified cell
        /// </summary>
        /// <param name="username"> user's name table belongs to </param>
        /// <param name="pass">password of user</param>
        /// <param name="dbname">name of database</param>
        /// <param name="tablename">name of table</param>
        /// <param name="cell">cell that contains the value we search for</param>
        /// <param name="value">value to look for</param>
        /// <returns>the rows of the table containing the value we look for</returns>
        public string Find(string username, string pass, string dbname, string tablename, string cell, string value)
        {
            try
            {
                string ap = null;
                if ((this.Login(username, pass) == true) && ((string.IsNullOrEmpty(dbname) == false) || (string.IsNullOrEmpty(dbname) == false)
                    || (cell != null) || (value != null)))
                //||(DataSet !=null ))
                {
                    // System.Console.WriteLine("hhhh");

                    MultyUser.ReadConfig();
                    ActiveDbpath = MultyUser.GetDataBasePath(username, dbname);
                    SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(username);
                    // LoadedDb.SelectDataSet(username + "_" + tablename).Tables.Add();
                    DataSet set = new DataSet();
                    //     System.Windows.Forms.MessageBox.Show(this.FindTable(username, pass, tablename).TableName);

                    //      System.Windows.Forms.MessageBox.Show(LoadedDb.SelectDataSet(username + "_" + tablename).Tables.Count.ToString());

                    string tmp = ado.Search(LoadedDb.SelectDataSet(username + "_" + tablename), cell, value, username + "  " + tablename, null, set, this.FindTableIndex(username, pass, tablename));
                    // System.Windows.Forms.MessageBox.Show(tmp);

                    MemoryStream ms = new MemoryStream();
                    set.WriteXml(ms);
                    ap = Encoding.UTF8.GetString(ms.ToArray());






                }
                return ap;
            }
            catch (Exception e)
            {
                program.errorreport(e);
                return "<error/>";
            }

        }
        /// <summary>
        /// adds the given values to the table
        /// </summary>
        /// <param name="username">user's name table belongs to</param>
        /// <param name="pass">password of user</param>
        /// <param name="dbname">name of database</param>
        /// <param name="table">name of table</param>
        /// <param name="vals">values to add</param>
        public void AddRow(string username, string pass, string dbname, string table, object[] vals)
        {
            try
            {

                if ((this.Login(username, pass) == true) && ((string.IsNullOrEmpty(dbname) == false) || (string.IsNullOrEmpty(dbname) == false)
                     || (string.IsNullOrEmpty(pass) == false) || (vals != null)))
                //||(DataSet !=null ))
                {


                    int i = this.FindTableIndex(username, pass, table);
                    LoadedDb.SelectDataSet(username + "_" + table).Tables[i].Rows.Add(vals);

                }


            }
            catch (Exception e)
            {
                program.errorreport(e);
                //  return null;
            }

        }
        /// <summary>
        /// adds the given List of values to the table
        /// </summary>
        /// <param name="username">user's name table belongs to</param>
        /// <param name="pass">password of user</param>
        /// <param name="dbname">name of database</param>
        /// <param name="table">name of table</param>
        /// <param name="vals">list of values to add</param>
        public void AddRows(string username, string pass, string dbname, string table, List<object[]> vals)
        {
            try
            {

                if ((this.Login(username, pass) == true) && ((string.IsNullOrEmpty(dbname) == false) || (string.IsNullOrEmpty(dbname) == false)
                    || (string.IsNullOrEmpty(pass) == false) || (vals != null)))
                //||(DataSet !=null ))
                {
                    foreach (object[] row in vals)
                    {

                        this.AddRow(username, pass, dbname, table, row);

                    }


                }
            }
            catch (Exception e)
            {
                program.errorreport(e);
                //  return null;
            }


        }
        /// <summary>
        /// adds the given values to the table
        /// </summary>
        /// <param name="root">root element of xml files</param>
        /// <param name="recordtag"> >element that holds the cells </param>
        /// <param name="username">user's name table belongs to</param>
        /// <param name="pass">password of user</param>
        /// <param name="dbname">name of database</param>
        /// <param name="table">name of table</param>
        /// <param name="vals">values to add</param>
        /// <param name="autoupd">if true the auto saves the table</param>
        public void AddRow(string root, string recordtag, string username, string pass, string dbname, string table, object[] vals, bool autoupd)
        {
            try
            {

                if ((this.Login(username, pass) == true) && ((string.IsNullOrEmpty(dbname) == false) || (string.IsNullOrEmpty(dbname) == false)
                    || (string.IsNullOrEmpty(pass) == false) || (vals != null)))
                //||(DataSet !=null ))
                {


                    int i = this.FindTableIndex(username, pass, table);
                    LoadedDb.SelectDataSet(username + "_" + table).Tables[i].Rows.Add(vals);
                    if (autoupd == true)
                    {
                        this.UpdateTable(root, recordtag, username, pass, dbname, table, null);
                        // System.Windows.Forms.MessageBox.Show("hi " + this.FindTable(username, pass, table).TableName);

                    }

                }


            }
            catch (Exception e)
            {
                program.errorreport(e);
                //  return null;
            }

        }
        /// <summary>
        /// adds the given values to the table
        /// </summary>
        /// <param name="root">root element of xml files</param>
        /// <param name="recordtag"> element that holds the cells </param>
        /// <param name="username">user's name table belongs to</param>
        /// <param name="pass">password of user</param>
        /// <param name="dbname">name of database</param>
        /// <param name="table">name of table</param>
        /// <param name="vals">values to add</param>
        /// <param name="autoupd">if true the auto saves the table</param>
        public void AddRows(string root, string recordtag, string username, string pass, string dbname, string table, List<object[]> vals, bool autoupd)
        {
            try
            {

                if ((this.Login(username, pass) == true) && ((string.IsNullOrEmpty(dbname) == false) || (string.IsNullOrEmpty(dbname) == false)
                    || (string.IsNullOrEmpty(pass) == false) || (vals != null)))
                //||(DataSet !=null ))
                {
                    foreach (object[] row in vals)
                    {

                        this.AddRow(username, pass, dbname, table, row);

                    }
                    if (autoupd == true)
                    {
                        this.UpdateTable(root, recordtag, username, pass, dbname, table, null);


                    }


                }
            }
            catch (Exception e)
            {
                program.errorreport(e);
                //  return null;
            }


        }
        /// <summary>
        /// Removes the rows that contained the specified values in primary keys
        /// </summary>
        /// <param name="username">user's name table belongs to</param>
        /// <param name="pass">password of user</param>
        /// <param name="dbname">name of database</param>
        /// <param name="primkeys">values that must have the rows to the primary key values</param>
        public void RemoveRow(string username, string pass, string dbname, string table, object[] primkeys)
        {
            try
            {

                if ((this.Login(username, pass) == true) && ((string.IsNullOrEmpty(dbname) == false) || (string.IsNullOrEmpty(dbname) == false)
                    || (string.IsNullOrEmpty(pass) == false) || (primkeys != null)))
                //||(DataSet !=null ))
                {
                    DataRow row = this.FindTable(username, pass, table).Rows.Find(primkeys);
                    if (row != null)
                    {
                        this.FindTable(username, pass, table).Rows.Remove(row);
                    }

                }


            }
            catch (Exception e)
            {
                program.errorreport(e);
                //  return null;
            }

        }
        /// <summary>
        /// Returns the list of databases that the user has
        /// </summary>
        /// <param name="username">user's name</param>
        /// <param name="pass">user's pass</param>
        /// <returns>Returns the list of databases that the user has</returns>
        public string[] ListDatabases(string username, string pass)
        {
            try
            {
                string[] ap = null;
                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false))
                //||(DataSet !=null ))
                {
                    MultyUser.ReadConfig();

                    SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(username);
                    string[] tap = Directory.GetDirectories(Path.Combine(SDKBase.UserMainPath, BaseClass.DataFolder));
                    // System.Windows.Forms.MessageBox.Show(tap.Length.ToString());
                    ap = new string[tap.Length];
                    for (int i = 0; i < tap.Length; i++)
                    {
                        string tmp = tap[i].Substring(tap[i].LastIndexOf("\\") + 1);
                        ap[i] = tmp;
                    }

                }
                return ap;


            }
            catch (Exception e)
            {
                program.errorreport(e);
                return null;
            }




        }
        /// Returns the list of databases that the database has
        /// </summary>
        /// <param name="username">user's name</param>
        /// <param name="pass">user's pass</param>
        /// <param name="dbname">name of database</param>
        /// <returns>Returns the list of databases that the database has</returns>
        public string[] ListTables(string username, string pass, string dbname)
        {

            try
            {
                string[] ap = null;
                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false) || (dbname != null))
                //||(DataSet !=null ))
                {
                    MultyUser.ReadConfig();

                    SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(username);
                    string[] tap = Directory.GetFiles(Path.Combine(Path.Combine(SDKBase.UserMainPath, BaseClass.DataFolder), dbname));
                    ap = new string[tap.Length];
                    for (int i = 0; i < tap.Length; i++)
                    {
                        ap[i] = Path.GetFileNameWithoutExtension(tap[i]);
                    }

                }
                return ap;


            }
            catch (Exception e)
            {
                program.errorreport(e);
                return null;
            }


        }
        /// <summary>
        /// Adds given columns to the table
        /// </summary>
        /// <param name="username">user's name</param>
        /// <param name="pass">user's password</param>
        /// <param name="dbname">name of database</param>
        /// <param name="table">name of table</param>
        /// <param name="cols">name of columns to add.</param>
        public void AddColumns(string username, string pass, string dbname, string table, object[] cols)
        {
            try
            {
                string[] ap = null;
                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false) || (dbname != null) || (cols != null))
                //||(DataSet !=null ))
                {
                    int i = this.FindTableIndex(username, pass, table);
                    foreach (string col in cols)
                    {

                        LoadedDb.SelectDataSet(username + "_" + table).Tables[i].Columns.Add(col);
                    }
                }

            }
            catch (Exception e)
            {
                program.errorreport(e);

            }


        }
        /// <summary>
        /// Adds given columns to the table
        /// </summary>
        /// <param name="root">root element of xml files</param>
        /// <param name="recordtag"> element that holds the cells </param>
        /// <param name="username">user's name</param>
        /// <param name="pass">user's password</param>
        /// <param name="dbname">name of database</param>
        /// <param name="table">name of table</param>
        /// <param name="cols">name of columns to add.</param>
        /// <param name="autoupd">if true auto saves the table</param>
        public void AddColumns(string root, string recordtag, string username, string pass, string dbname, string table, object[] cols, bool autoupd)
        {

            try
            {
                string[] ap = null;
                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false) || (dbname != null) || (cols != null))
                //||(DataSet !=null ))
                {
                    this.AddColumns(username, pass, dbname, table, cols);
                    if (autoupd == true)
                    {
                        this.UpdateTable(root, recordtag, username, pass, dbname, table, null);

                    }
                }

            }
            catch (Exception e)
            {
                program.errorreport(e);

            }


        }

        /// <summary>
        /// Checks if the given name of table is encrypted and returns true if it is , otherwise returns false
        /// </summary>
        /// <param name="username">user's name</param>
        /// <param name="pass">user's password</param>
        /// <param name="dbname">name of database</param>
        /// <param name="tablename">name of table</param>
        /// <returns>returns true if it is , otherwise returns false</returns>
        public bool IsTableEncrypted(string username, string pass, string dbname, string tablename)
        {


            try
            {
                bool ap = false;

                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false) || (dbname != null) || (tablename != null))
                //||(DataSet !=null ))
                {
                    if ((Path.GetExtension(Path.Combine(Path.Combine(SDKBase.UserMainPath, BaseClass.DataFolder), dbname, tablename)) == BaseClass.defualt_encryptedtableextensionwithSha384) ||
                    (Path.GetExtension(Path.Combine(Path.Combine(SDKBase.UserMainPath, BaseClass.DataFolder), dbname, tablename)) == BaseClass.defualt_encryptedtableextensionwithSha1))
                    {


                        ap = true;
                    }


                }



                return ap;


            }
            catch (Exception e)
            {
                program.errorreport(e);
                return false;
            }


        }
       
        /// <summary>
        /// Closes the requsted table
        /// </summary>
        /// <param name="username">user's name</param>
        /// <param name="pass">user's password</param>
        /// <param name="dbname">name of database</param>
        /// <param name="tablename">name of table</param>
        public void CloseTable(string username, string pass, string dbname, string tablename)
        {

            try
            {
                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false) || (dbname != null) || (tablename != null))
                //||(DataSet !=null ))
                {
                    DataTable table = this.FindTable(username, pass, tablename);
                    int ind = this.FindTableIndex(username, pass, tablename);
                    if (table != null)
                    {
                        List<object[]> vals = new List<object[]>();
                        foreach (DataRow row in LoadedDb.SelectDataSet(username + "_" + tablename).Tables[table.TableName].Rows)
                        {
                            vals.Add(row.ItemArray);


                        }
                        this.UpdateTable(BaseClass.tabletag, BaseClass.recordtag, username, pass, dbname, tablename, vals);
                        LoadedDb.SelectDataSet(username + "_" + tablename).Tables[table.TableName].Dispose();
                        LoadedDb.SelectDataSet(username + "_" + tablename).Dispose();
                        LoadedDb.Remove(LoadedDb.SelectDataSet(username + "_" + tablename));
                        //System.Windows.Forms.MessageBox.Show(LoadedDb.SelectDataSet(username + "_" + tablename).DataSetName);




                    }
                    else if (ind > -1)
                    {

                        LoadedDb.SelectDataSet(username + "_" + tablename).Dispose();

                    }
                    string path = Path.Combine(MultyUser.GetUsersMainFolder("system"), "WhiteTiger", MultyUser.tmpFolder);

                    if(Directory.Exists(path))
                    {
                        Directory.Delete(path, true);

                    }

                }

            }
            catch (Exception e)
            {
                program.errorreport(e);
                // return false;
            }

        }
       /// <summary>
       /// Returns the columns of the requested table
       /// </summary>
        /// <param name="username">user's name</param>
        /// <param name="pass">user's password</param>
        /// <param name="dbname">name of database</param>
        /// <param name="tablename">name of table</param>
        /// <returns> the columns of the requested table </returns>
        public string[] GetColumns(string username, string pass, string dbname, string tablename)
        {

            try
            {
                string[] ap = null;
                int i = 0;
                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false) || (dbname != null) || (tablename != null))
                //||(DataSet !=null ))
                {
                    DataTable table = this.FindTable(username, pass, tablename);
                    //DataColumn col ;
                    int ind = this.FindTableIndex(username, pass, tablename);
                    if ((table != null) && (table.Columns != null))
                    {
                        ap = new string[table.Columns.Count];
                        for (i = 0; i < table.Columns.Count; i++)
                        {
                            ap[i] = table.Columns[i].ColumnName.Replace(" ", "_");


                        }
                    }
                  /*  if ( ind<0)
                    {
                        return null;
                    }*/
                    table = LoadedDb.SelectDataSet(ind).Tables[0];
                    if ((table != null) && (table.Columns != null))
                    {
                        ap = new string[table.Columns.Count];
                        for (i = 0; i < table.Columns.Count; i++)
                        {
                            ap[i] = table.Columns[i].ColumnName.Replace(" ", "_");


                        }
                    }

                }
                return ap;
            }
            catch (Exception e)
            {
                program.errorreport(e);
                return null; ;
            }


        }
    
        /// <summary>
        /// Creates a  new user with the given username password and passphrase
        /// </summary>
        /// <param name="username">Name of an Administrator</param>
        /// <param name="pass"> Administrator's password</param>
        /// <param name="newuser">The name of the new user</param>
        /// <param name="newpass">The password of the new user</param>
        /// <param name="passphrase">The passphrase of the new user</param>
        public void CreateUser(string username, string pass, string newuser, string newpass,string passphrase)
        {
            try
            {
                string[] ap = null;
                int i = 0;
                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false) || (newuser != null) )
                //||(DataSet !=null ))
                {
                    if (this.userman.FindUser(newuser) == null)
                    {
                        this.userman.CreateUser(newuser, newpass, "--", false, passphrase);

                    }


                }
                
            }
            catch (Exception e)
            {
                program.errorreport(e);
                
            }
        }

        /// <summary>
        /// Gets the list of users
        /// </summary>
       /// <param name="username">Name of an Administrator</param>
        /// <param name="pass"> Administrator's password</param>
        /// <returns> the list of users</returns>
        public string  GetUserList(string username, string pass)
        {
            try
            {
                string ap = null;
                int i = 0;
                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false) )
                //||(DataSet !=null ))
                {
                   


                }
                return ap;

            }
            catch (Exception e)
            {
                program.errorreport(e);
                return null;

            }
        }
        /// <summary>
        /// edits the user withh the given username password and passphrase
        /// </summary>
        /// <param name="username">Name of an Administrator</param>
        /// <param name="pass">Name of Administrator's password</param>
        /// <param name="newuser">The name of the new user</param>
        /// <param name="newpass">The password of the new user</param>
        /// <param name="passphrase">The passphrase of the new user</param>
        public void EditUser(string username, string pass, string newpass, string passphrase)
        {
            try
            {
                string[] ap = null;
                int i = 0;
                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false) || (newpass != null))
                //||(DataSet !=null ))
                {
                    if (this.userman.FindUser(username) == null)
                    {
                        this.userman.EditUser(username,pass, newpass, passphrase,false);


                    }


                }

            }
            catch (Exception e)
            {
                program.errorreport(e);

            }
        }
        /// <summary>
        /// deletes  the user withh the given username password and passphrase
        /// </summary>
        /// <param name="username">Name of an Administrator</param>
        /// <param name="pass">Name of Administrator's password</param>
        /// <param name="deluser">Tuser to be deleted</param>        
        public void DeleteUser(string username, string pass, string deluser)
        {
           try
            {
                string[] ap = null;
                int i = 0;
                if ((this.Login(username, pass) == true) || (string.IsNullOrEmpty(pass) == false) || (deluser != null) )
                //||(DataSet !=null ))
                {

                    this.DeleteUser(username, pass, deluser);

                   

                }
                
            }
            catch (Exception e)
            {
                program.errorreport(e);
                
            }
        }
        /// <summary>
        /// Load table of given name belonging to  given user name , database.
        /// and returns the contents as zip archive
        /// </summary>
        /// <param name="recordtag">Child that contains the cells as children</param>
        /// <param name="username">name of user table belongs to </param>
        /// <param name="pass">password of username</param>
        /// <param name="dbname">name of the database table belongs to</param>
        /// <param name="tablename">name of the table </param>
        /// <returns>returns the contents as zip archive</returns>

        public RemoteFileInfo LoadTableAsFile(string recordtag, string username, string pass, string dbname, string table)
        {
            try
            {
                  byte[] buffer ;
           int bytesRead = 1;
                RemoteFileInfo ap = null;
                Maintenance_Backup back = new Maintenance_Backup();
                DataSet set;
            
                if ((this.Login(username, pass) == true)
                    && ((string.IsNullOrEmpty(dbname) == false)
                    || (string.IsNullOrEmpty(username) != false) ||
                    (string.IsNullOrEmpty(table) == false) || (recordtag != null)))
                {

                    //string path = Path.Combine(MultyUser.GetUsersMainFolder("system"), "WhiteTiger", MultyUser.tmpFolder,dbname,
                    //    "xml");

                    //if (Directory.Exists(path) != true)
                    //{
                    //    back.CreateFolder(path);
                    //}

                    
                   
                    String xml = this.LoadTable(recordtag,username,pass,dbname,table);
                

                    if( xml!=null)
                    {
                        
                            ap = new RemoteFileInfo();
                            FastZip zip = new FastZip();
                       string  zipfile =  tools.EncodeCompressData(xml, username, table, dbname,true );
                            FileStream str=File.OpenRead(zipfile);
                            buffer = new byte[str.Length];
                             while(bytesRead>0)
                             {
                                 bytesRead =str.Read(buffer, 0, buffer.Length);
                             }
                             str.Close();
                            ap.FileByteStream= buffer;
                            ap.FileName = zipfile;
                            ap.Length = new FileInfo(zipfile).Length;

                            

                       

                    }







                }
                return ap;
            }
            catch (Exception e)
            {
                RemoteFileInfo a = new RemoteFileInfo();
                program.errorreport(e);
                a.error = e.ToString();
                return a;
            }
        }
    }
}
    



