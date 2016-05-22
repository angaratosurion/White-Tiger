using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hydrobase;
using HydrobaseSDK;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.IO;
using White_Tiger.ClientManagement;
using System.Web;
using White_Tiger.DataContracts;


namespace White_Tiger
{
    [ServiceContract(CallbackContract = typeof(IClientManager))]
    public interface IWhiteTigerService
    {
       
       
        [OperationContract]
       

        bool Login(string username, string password);
        [OperationContract]
       
        void CreateDataBase(string username, string pass, string dbname);
        [OperationContract]
       
        void CreateTable(string root,string recordtag,string username, string pass, string dbname, string tablename, string[] TableCells);
        [OperationContract]
        
        string LoadTable( string recordtag, string username, string pass, string dbname, string table);
        [OperationContract]

       RemoteFileInfo LoadTableAsFile(string recordtag, string username, string pass, string dbname, string table);
        [OperationContract]
        
        void UpdateTable(string root, string recordtag, string username, string pass, string dbname, string table, List<object[]> rows);
         [OperationContract]
         
              void Encrypt(string root, string recordtag, string username, string dbname, string table, List<object[]> rows, string pass, string alg, string hashalg,string passphrase);
        [OperationContract]
       
         string  Decrypt(string root, string recordtag, string username, string dbname, string table, string pass, string alg, string hashalg,string passphrase);
        [OperationContract]
       
        void Backup(string username, string dbname, string table, string pass);
        [OperationContract]
       
      string GetVersion();
        [OperationContract]
       
        string Find(string username, string pass,string dbname, string table, string cell, string value);
      [OperationContract]

        DataTable FindTable(string username, string pass, string tablename);
     
     [OperationContract]
    
      void AddRow(string root, string recordtag, string username, string pass, string dbname, string table, object[] vals, bool autoupd);
   
      [OperationContract]

     
     void AddRows(string root, string recordtag, string username, string pass, string dbname, string table, List<object[]> vals, bool autoaupd);
        [OperationContract]

       
        void RemoveRow(string username, string pass, string dbname, string table, object[] primkeys);
        [OperationContract]

     
         string [] ListDatabases(string username,string pass);
        [OperationContract]

        
        string[] ListTables(string username, string pass, string dbname);
        [OperationContract]

       
        void  AddColumns(string root, string recordtag, string username, string pass, string dbname, string table, object[] cols, bool autoupd);
        [OperationContract]

       
        bool IsTableEncrypted(string username, string pass, string dbname, string tablename);
        [OperationContract]

       
        void CloseTable(string username, string pass, string dbname, string tablename);
       
      string[] GetColumns(string username, string pass, string dbname, string tablename);
     
        [OperationContract]
        void CreateUser(string username, string pass, string newuser, string newpass,string passphrase);
        [OperationContract]
        string  GetUserList(string username, string pass);
        [OperationContract]
        void EditUser(string username, string pass,  string newpass, string passphrase);
        [OperationContract]
        void DeleteUser(string username, string pass, string deluser);

    
    }
    }


        
