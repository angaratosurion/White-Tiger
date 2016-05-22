using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using White_Tiger;
using System.Net;
using White_Tiger.UserManagment;


namespace WhitetigerServer
{
    public partial class WhiteTigerService : ServiceBase
    {  
        ServiceHost host  ,jsonhost;// = new ServiceHost(typeof(White_Tiger.WhiteTigerService));
        
        public WhiteTigerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
           // IPEndPoint LocalIP = new IPEndPoint(Dns.GetHostByName(Environment.MachineName).AddressList[0],80);
          //  Uri localaddress = new Uri("http://" + Dns.GetHostByName(Environment.MachineName).AddressList[0]);
            try
            {
                host = new ServiceHost(typeof(White_Tiger.WhiteTigerService));
                
                        Uri localaddress = new Uri("http://" + Dns.GetHostByName(Environment.MachineName).AddressList[0] + "/White_Tiger");
                     

              UserManager  userman = new UserManager();
                White_Tiger.Config conf = new Config();

                
                this.AutoLog = true;
                host.Credentials.WindowsAuthentication.AllowAnonymousLogons = true;
                host.Credentials.WindowsAuthentication.IncludeWindowsGroups = true;
                host.Open();
                conf.ReadConfig();
                   jsonhost = new ServiceHost(typeof(WhiteTigerServiceJson));

                 jsonhost.Credentials.WindowsAuthentication.AllowAnonymousLogons = true;
                        jsonhost.Credentials.WindowsAuthentication.IncludeWindowsGroups = true;
                  jsonhost.Open();
                //userman.CreateUser(White_Tiger.WhiteTigerService.pref.ServiceUserName, White_Tiger.WhiteTigerService.pref.ServicePassword, "dddd", true);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
           
        }

        protected override void OnStop()
        {
            host.Close();
        }
    }
}
