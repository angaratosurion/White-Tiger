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

namespace White_Tiger.ClientManagement

{
    public class ClientManager:IClientManager
    {

        public List<ClientInfo> SearchClient(string username, string table)
        {

            try
            {
                List<ClientInfo> ap = null;

                if ((username != null) && (table != null))
                {
                    ap = new List<ClientInfo>();

                    foreach (ClientInfo clinf in WhiteTigerService.ClientIPs)
                    {
                        if ((clinf != null) && (clinf.Username == username) && (clinf.Table == table))
                        {

                            ap.Add(clinf);

                        }


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
        public void ContactClient(enumMessage mesg)
        {
            try
            {
                //List<ClientInfo> ap = null;

              




            }
            catch (Exception e)
            {

                program.errorreport(e);


            }

        }
        

        
    }
}
