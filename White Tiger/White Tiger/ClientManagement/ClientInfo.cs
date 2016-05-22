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
using White_Tiger;

namespace White_Tiger.ClientManagement
{ 
    /// <summary>
    /// Class that represents info about client
    /// </summary>
   public  class ClientInfo //:IClientInfo
    {
       string username, ip, table;
       int port;
       /// <summary>
       /// Name that client has logon with
       /// </summary>
       public string Username
       {

           get { return username; }
           set { username = value; }
       }
       /// <summary>
       /// Ip address of the client
       /// </summary>
       public string IP
       {
           get { return ip; }
           set { ip = value; }
       }
       /// <summary>
       /// Clients port
       /// </summary>
       public int Port
       {

           get { return port; }
           set { port = value; }
       }
       /// <summary>
       /// Clients  table
       /// </summary>
       public string Table
       {

           get { return table; }
           set { table = value; }

       }
      
    }
}
