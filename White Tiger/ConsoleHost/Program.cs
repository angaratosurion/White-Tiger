using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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

using System.ServiceModel.Description;
using White_Tiger;
using White_Tiger.Protocols.TCP;
using White_Tiger.UserManagment;
namespace ConsoleHost
{
    class Program
    {
      //  ServiceHost host;
       //static  
        bool run_batch = false;
       static  TCPServer tcpServer = new TCPServer();
        static void Main(string[] args)
        {
            try
            {

                //  args[0] = "0";
                ServiceHost host = null,jsonhost;
                if (args.Length >= 1)
                {


                    if (args[0] != "-batch")
                    {




                        Uri localaddress = new Uri("http://" + Dns.GetHostByName(Environment.MachineName).AddressList[0] + "/White_Tiger");
                        jsonhost = new ServiceHost(typeof(WhiteTigerServiceJson));

                        //  host = new ServiceHost(typeof(White_Tiger.WhiteTigerService), localaddress);
                        host = new ServiceHost(typeof(White_Tiger.WhiteTigerService));
                      
                       UserManager userman = new UserManager();
                        White_Tiger.Config conf = new Config();
                      //  System.Threading.Thread.Sleep(20000);
                        //  RequestAdditionalTime(60000);

                        host.Credentials.WindowsAuthentication.AllowAnonymousLogons = true;
                        host.Credentials.WindowsAuthentication.IncludeWindowsGroups = true;
                        jsonhost.Credentials.WindowsAuthentication.AllowAnonymousLogons = true;
                        jsonhost.Credentials.WindowsAuthentication.IncludeWindowsGroups = true;
                        conf.CreateFile();
                        conf.ReadConfig();
                        // userman.CreateUser(White_Tiger.WhiteTigerService.pref.ServiceUserName, White_Tiger.WhiteTigerService.pref.ServicePassword, "dddd", true);
                      //  userman.CreateUser("root", "root", "", true, "");
                      //  userman.CreateRole1stTime("root", "root", "Administrator", RoleType.Administrator);
                        host.Open();
                        jsonhost.Open();
                        
                        

                        int i = 0;

                        foreach (ServiceEndpoint end in host.Description.Endpoints)
                        {
                            Console.WriteLine("Endoipnts: " + end.Address.ToString());
                            // i++;
                        }
                        Console.WriteLine("White Tiger Json:");
                        foreach (ServiceEndpoint end in jsonhost.Description.Endpoints)
                        {
                            Console.WriteLine("Endoipnts: " + end.Address.ToString());
                            // i++;
                        }
                    }
                    else if (args[0] == "-batch")
                    {
                        int i;
                        for (i = 0; i < args.Length; i++)
                        {
                            Console.WriteLine("i " + i + "=  " + args[i]);

                        }
                        System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\CopytoLocalTemp.bat");
                    }
                }
                else
                {

                    Uri localaddress = new Uri("http://" + Dns.GetHostByName(Environment.MachineName).AddressList[0] + "/White_Tiger");

                    //  host = new ServiceHost(typeof(White_Tiger.WhiteTigerService), localaddress);
                    host = new ServiceHost(typeof(White_Tiger.WhiteTigerService));
                    jsonhost = new ServiceHost(typeof(WhiteTigerServiceJson));

                   UserManager userman = new UserManager();
                    White_Tiger.Config conf = new Config();
                    //  System.Threading.Thread.Sleep(20000);
                    //  RequestAdditionalTime(60000);

                    host.Credentials.WindowsAuthentication.AllowAnonymousLogons = true;
                    host.Credentials.WindowsAuthentication.IncludeWindowsGroups = true;
                    jsonhost.Credentials.WindowsAuthentication.AllowAnonymousLogons = true;
                    jsonhost.Credentials.WindowsAuthentication.IncludeWindowsGroups = true;
                    conf.CreateFile();
                    conf.ReadConfig();
                    // userman.CreateUser(White_Tiger.WhiteTigerService.pref.ServiceUserName, White_Tiger.WhiteTigerService.pref.ServicePassword, "dddd", true);
                  //  userman.CreateUser("root", "root", "", true, "");
                 //   userman.CreateRole1stTime("root", "root", "Administrator", RoleType.Administrator);
                    host.Open();
                    jsonhost.Open();

                    int i = 0;
                    foreach (ServiceEndpoint end in host.Description.Endpoints)
                    {
                        Console.WriteLine("Endoipnts: " + end.Address.ToString());
                        //i++;
                    }

                    Console.WriteLine("White Tiger Json:");
                    foreach (ServiceEndpoint end in jsonhost.Description.Endpoints)
                    {
                        Console.WriteLine("Endoipnts: " + end.Address.ToString());
                        // i++;
                    }

                }
             
             // tcpServer.StartServer();
                
                Console.ReadLine();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
