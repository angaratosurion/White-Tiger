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
using White_Tiger.ClientManagement;

namespace White_Tiger
{
    /// <summary>
    /// Class that contains the events of the service
    /// </summary>
   public   class Events
    {
       ClientManager clintmng = new ClientManager();
       //WhiteTigerService tig = new WhiteTigerService();
       /// <summary>
       ///when a row changes it contants the client to reload table
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="ev"></param>
       public void TableRowChanged(object sender, DataRowChangeEventArgs ev)
       {
           try
           {
              

           }
           catch (Exception e)
           {

               program.errorreport(e);
               
           }

       }
    }
}
