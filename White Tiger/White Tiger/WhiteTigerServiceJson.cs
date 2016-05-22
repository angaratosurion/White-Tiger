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
using System.IO.Compression;
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
using White_Tiger.DataContracts;
//using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.ServiceModel.Web;
using ICSharpCode.SharpZipLib.Core;

namespace White_Tiger
{
   /// <summary>
   /// This is the Json implementation of WhiteTiger Service  it basically  does the same as the SOAP one 
   /// just initialize the CommandModel and oyu are ready
   /// </summary>
  public  class WhiteTigerServiceJson:IWhiteTigerServiceJson
    {
      WhiteTigerService wtserv = new WhiteTigerService(false);

      public bool Login(DataContracts.CommandModel args)
        {
            try
            {
                if (args != null)
                {
                    return this.wtserv.Login(args.username, args.password);
                }
                return false;
            }
          catch(Exception ex)
            {
                program.errorreport(ex);
                return false;
            }
        }

        public void CreateDataBase(DataContracts.CommandModel args)
        {
            try
            {
                if (args != null)
                {
                    this.wtserv.CreateDataBase(args.username, args.password, args.dbname);
                }
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                //return false;
            }
        }

        public void CreateTable(DataContracts.CommandModel args)
        {
            try
            {
                if (args != null)
                {
                    this.wtserv.CreateTable(args.root, args.recordtag, args.username, args.password, args.dbname, args.tablename, args.data.Split(','));

                }
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                
            }
        }

        public CommandModel LoadTable(CommandModel args)
        {
            try
            {
                if (args != null)
                {
                    string tmp = this.wtserv.LoadTable(args.recordtag, args.username, args.password, args.dbname,
                           args.tablename);
                    if (tmp != null)
                    {
                        DataSet set = new DataSet();
                        TextReader rdr = new StringReader(tmp);
                        

                        set.ReadXml(rdr);
                        DataTable table = set.Tables[0];
                        tmp = "";
                        foreach (DataColumn col in table.Columns)
                        {
                            if (col.ColumnName.StartsWith("_."))
                            {
                                string t = col.ColumnName.Replace("_.", "_");
                               // t.Replace("_.", "_");
                                col.ColumnName = t;
                            }


                        }
                        StringWriter wr = new StringWriter();
                        set.WriteXml(wr);
                        set.Dispose();
                        tmp = wr.ToString();

                    }
                   
                    args.data = this.EncodeAndCompressData(tmp, args.username, args.tablename);
                    args.dataformat="xml";

                    return args;
                }
              return null;
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                args.exception = ex.ToString();
                args.friendlyerrormsg = ex.Message;
                return args;
            }
        }

        public void UpdateTable(DataContracts.CommandModel args)
        {
            try
            {
                DataSet set = new DataSet();
                if ((args !=null) &&(args.data!=null))
                {
                    StringReader str = new StringReader(args.data);

                set.ReadXml(str);
                List<object[]> vals = new List<object[]>();
                    if ( set.Tables ==null)
                    {
                        return;
                    }
                    foreach(DataRow r in set.Tables[0].Rows)
                    {
                        vals.Add(r.ItemArray);
                    }
                    
                this.wtserv.UpdateTable(args.root, args.recordtag, args.username, args.password, args.dbname, args.tablename,vals);
                }
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
              //  return false;
            }
        }

        public void Encrypt(DataContracts.CommandModel args)
        {
            try
            {
                DataSet set = new DataSet();
                if ((args != null) && (args.data != null))
                {
                    set.ReadXml(args.data);
                    List<object[]> vals = new List<object[]>();
                    if (set.Tables == null)
                    {
                        return;
                    }
                    foreach (DataRow r in set.Tables[0].Rows)
                    {
                        vals.Add(r.ItemArray);
                    }
                    this.wtserv.Encrypt(args.root, args.recordtag, args.username, args.dbname, args.tablename, vals, args.password, args.algorith
                        , args.hashalg, args.passphrase);
                }
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                //  return false;
            }
        }

        public CommandModel Decrypt(CommandModel args)
        {
            try
            {
               
              //  if (args != null)
                {
                  
                   args.data= this.wtserv.Decrypt(args.root,args.recordtag,args.username,args.dbname,
                       args.tablename,  args.password
                       ,args.algorith, args.hashalg,args.passphrase);
                   args.dataformat = "xml";
                   return args;
                }
               // return null;
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                args.exception = ex.ToString();
                args.friendlyerrormsg = ex.Message;
                return args;
            }
        }

        public void Backup(DataContracts.CommandModel args)
        {
            try
            {

                if (args != null)
                {

                    this.wtserv.Backup(args.username, args.dbname, args.tablename, args.password);
                }
                
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                
            }
        }

        public string GetVersion()
        {
            try
            {

                //if (args != null)
                {

                  return   this.wtserv.GetVersion();
                }

            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                return null;

            }
        }



        public DataTable FindTable(CommandModel args)
        {
            try
            {

              //  if (args != null)
                {

                    return this.wtserv.FindTable(args.username, args.password, args.tablename);
                }

            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                return null;

            }
        }

        public void AddRow(DataContracts.CommandModel args)
        {
            try
            {

                if (args != null)
                {

                    this.wtserv.AddRow(args.root,args.recordtag ,args.username,  args.password,args.dbname, args.tablename,args.data.Split(','),true);
                }

            }
            catch (Exception ex)
            {
                program.errorreport(ex);

            }
        }

        public void AddRows(DataContracts.CommandModel args)
        {
            try
            {
               DataSet set = new DataSet();
                if ((args != null) && (args.data != null))
                {
                    set.ReadXml(args.data);
                    List<object[]> vals = new List<object[]>();
                    if (set.Tables == null)
                    {
                        return;
                    }
                    foreach (DataRow r in set.Tables[0].Rows)
                    {
                        vals.Add(r.ItemArray);
                    }
                    this.wtserv.AddRows(args.root, args.recordtag, args.username, args.password, args.dbname, args.tablename,vals, true);

                    
                }
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                //  return false;
            }
        }

        public void RemoveRow(DataContracts.CommandModel args)
        {
            try
            {

                if (args != null)
                {

                    this.wtserv.RemoveRow(args.username, args.password, args.dbname, args.tablename, args.data.Split(','));
                }

            }
            catch (Exception ex)
            {
                program.errorreport(ex);

            }
        }

        public CommandModel ListDatabases(CommandModel args)
        {
            try
            {
               if (args != null)
                {
                    string[] dbs = this.wtserv.ListDatabases(args.username, args.password);
                    args.data = "";
                    foreach (string r in dbs)
                    {
                        args.data += r+",";
                    }
                    args.dataformat = "array";

                }
                return args;
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                args.exception = ex.ToString();
                args.friendlyerrormsg = ex.Message;
                return args;
            }
        }

        public CommandModel ListTables(CommandModel args)
        {
             try
            {
             //   if (args != null)
                {
                    string[] dbs= this.wtserv.ListTables(args.username, args.password,args.dbname);
                    foreach (string r in dbs)
                    {
                        args.data += "," + r;
                    }
                    args.dataformat = "array";
                }
                return args;
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                args.exception = ex.ToString();
                args.friendlyerrormsg = ex.Message;
                return args;
            }
        }

        public void AddColumns(DataContracts.CommandModel args)
        {
            try
            {
             //   DataSet set = new DataSet();
                if ((args != null) && (args.data != null))
                {
                   
                    this.wtserv.AddColumns(args.root, args.recordtag, args.username, args.password, args.dbname, args.tablename, args.data.Split(','),true);


                }
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                //  return false;
            }
        }

        public CommandModel IsTableEncrypted(CommandModel args)
        {
            try
            {
                if (args != null)
                {
                    args.data= this.wtserv.IsTableEncrypted(args.username, args.password,args.dbname,
                        args.tablename).ToString();
                    args.dataformat = "bool";
                }
                return args;
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                args.exception = ex.ToString();
                args.friendlyerrormsg = ex.Message;
                return args;
            }
        }

        public void CloseTable(CommandModel args)
        {
            try
            {
                //   DataSet set = new DataSet();
             //   if (args != null) 
                {
                    string path = Path.Combine(MultyUser.GetUsersMainFolder("system"), "WhiteTiger", MultyUser.tmpFolder);
                    string[] files;

                    this.wtserv.CloseTable(args.username,args.password, args.dbname, args.tablename);
                    if( Directory.Exists(path))
                    {
                        files = Directory.GetFiles(path);
                        if( files !=null)
                        {
                            foreach(string f in files)
                            {
                                File.Delete(f);
                            }
                        }
                    }
               
                
                
                }
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                //  return false;
            }
        }

        public CommandModel GetColumns(CommandModel args)
        {
            try
            {
              //  if (args != null)
                {
                    string[] dbs = this.wtserv.GetColumns(args.username, args.password, args.dbname,
                        args.tablename);
                    foreach (string r in dbs)
                    {
                        args.data += "," + r;
                    }
                    args.dataformat = "array";
                }
                return args;
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                args.exception = ex.ToString();
                args.friendlyerrormsg = ex.Message;
                return args;
            }
        }
        public Stream GetFile(string Name)
        {

            try
            {
                Stream ap = null;
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";


                if(Name !=null)
                {   
                    string path = Path.Combine(MultyUser.GetUsersMainFolder("System"),
                        "WhiteTiger",MultyUser.tmpFolder);
                   // string tname=args.data.Replace("/","\\");
                    //string Name=tname.Substring(tname.LastIndexOf("\\")+1);
                    string filename = Path.Combine(path, Name);
                    if(File.Exists(filename))
                    {
                        ap = File.OpenRead(filename);
                    }

                }

                return ap;
            }
            catch (Exception ex)
            {
                program.errorreport(ex);
               
                return null;
            }

        }


        private string EncodeAndCompressData(string args, string username, string tablename)
        {
            try
            {
                string ap = "";



                // ZipFile z = new ZipFile();
                Maintenance_Backup back = new Maintenance_Backup();

                if (WhiteTigerService.pref.UseCompressition)
                {

                    if ((args != null) && (username != null) && (tablename != null))
                    {



                        string path = Path.Combine(MultyUser.GetUsersMainFolder("system"), "WhiteTiger", MultyUser.tmpFolder);

                        if (Directory.Exists(path) != true)
                        {
                            back.CreateFolder(path);
                        }

                        string rndfile = tablename + Guid.NewGuid().ToString() + ".hbt";
                        string file = Path.Combine(path, rndfile);
                        FileInfo fi = new FileInfo(file);
                        /*  StreamWriter f = File.CreateText(file);
                          f.Write(args);
                          f.Close();
                          f.Flush();*/
                        FileStream fsOut = File.Create(file + ".zip");
                        ZipOutputStream zipOutputStream = new ZipOutputStream(fsOut);
                        zipOutputStream.SetLevel(9);
                        //FileStream fs = File.OpenRead(file);
                        ZipEntry entry = new ZipEntry(ZipEntry.CleanName(file));
                        entry.Size = fi.Length;
                        entry.DateTime = fi.LastWriteTime;
                        byte[] buffer = new byte[4096];
                        using (FileStream streamReader = File.OpenRead(file))
                        {
                            StreamUtils.Copy(streamReader, zipOutputStream, buffer);
                        }
                        zipOutputStream.PutNextEntry(entry);





                        zipOutputStream.Close();



                        ap = this.GetServiceUrl() + "GetFile/" + Path.GetFileName(file) + ".zip";




                    }
                }
                else
                {

                    if ((args != null) && (username != null) && (tablename != null))
                    {
                        string path = Path.Combine(MultyUser.GetUsersMainFolder("System"),
                           "WhiteTiger", MultyUser.tmpFolder);

                        if (Directory.Exists(path) != true)
                        {
                            back.CreateFolder(path);
                        }

                        string rndfile = tablename + Guid.NewGuid().ToString() + ".hbt";
                        string file = Path.Combine(path, rndfile);
                        StreamWriter wr = File.CreateText(file);
                        wr.WriteLine(args);
                        wr.Flush();
                        wr.Close();
                        wr.Dispose();



                        ap = this.GetServiceUrl() + "GetFile/" + Path.GetFileName(file);
                    }

                }
                return ap;

            }
            catch (Exception ex)
            {
                program.errorreport(ex);

                return null;
            }
        }
    
      private string GetServiceUrl()
      {
          try
          {
              string ap=null;
              ap = "http://" + Dns.GetHostByName(Environment.MachineName).AddressList[0] + "/White_Tiger/json/";

              

                  return ap;
          }

          catch (Exception ex)
          {
              program.errorreport(ex);

              return null;
          }

      }
    }
}
