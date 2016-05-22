using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using White_Tiger.ClientManagement;
using White_Tiger.DataContracts;

namespace White_Tiger
{
    [ServiceContract]
    public interface IWhiteTigerServiceJson
    {
      //  [OperationContract]
       // [WebInvoke(UriTemplate = "Login/{dbname}", Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]

     //   bool Login(DataContracts.CommandModel args);
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateDatabase", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        void CreateDataBase(CommandModel args);
        [OperationContract]
        [WebInvoke(UriTemplate = "CreateTable", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        void CreateTable(CommandModel args);
        [OperationContract]
        [WebInvoke(UriTemplate = "LoadTable", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        CommandModel LoadTable(CommandModel args);
        [OperationContract]
        [WebInvoke(UriTemplate = "UpdateTable", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        void UpdateTable(CommandModel args);
        [OperationContract]
        [WebInvoke(UriTemplate = "EncryptTable", Method="POST",ResponseFormat=WebMessageFormat.Json,BodyStyle =  WebMessageBodyStyle.Bare)]
        void Encrypt(CommandModel args);
        [OperationContract]
        [WebInvoke(UriTemplate = "DecryptTable",  ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        CommandModel Decrypt(CommandModel args);
        [OperationContract]
        [WebInvoke(UriTemplate = "Backup", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        void Backup(CommandModel args);
        [OperationContract]
        [WebInvoke(UriTemplate = "GetVersion",  ResponseFormat=WebMessageFormat.Json,BodyStyle =  WebMessageBodyStyle.Bare)]
       string GetVersion();
        //[OperationContract]
        //[WebGet(UriTemplate = "Find/")]
        //string Find(CommandModel args);
        [OperationContract]

        DataTable FindTable(CommandModel args);

        [OperationContract]
        [WebInvoke(UriTemplate = "AddRow", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        void AddRow(CommandModel args);

        [OperationContract]

        [WebInvoke(UriTemplate = "AddRows", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        void AddRows(CommandModel args);
        [OperationContract]

        [WebInvoke(UriTemplate = "RemoveRow", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        void RemoveRow(CommandModel args);
        [OperationContract]

        [WebInvoke(UriTemplate = "ListDataBases",  ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        CommandModel ListDatabases(CommandModel args);
        [OperationContract]

        [WebInvoke(UriTemplate = "ListTables",  ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        CommandModel ListTables(CommandModel args);
        [OperationContract]

        [WebInvoke(UriTemplate = "AddColumns", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        void AddColumns(CommandModel args);
        [OperationContract]

        [WebInvoke(UriTemplate = "IsTableEncrypted", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        CommandModel IsTableEncrypted(CommandModel args);
        [OperationContract]

        [WebInvoke(UriTemplate = "CloseTable", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        void CloseTable(CommandModel args);
        [WebInvoke(UriTemplate = "GetColumns",  ResponseFormat = WebMessageFormat.Json, BodyStyle =  WebMessageBodyStyle.Bare)]
        CommandModel GetColumns(CommandModel args);

        [OperationContract]    
        
        [WebGet(UriTemplate = "GetFile/{Name}")]
        Stream GetFile(string Name);

    }
}
