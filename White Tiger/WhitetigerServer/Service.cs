using System.ComponentModel;
using System.ServiceModel;
using System.ServiceProcess;
using System.Configuration;
using System.Configuration.Install;
using ServiceTools;
using System;
using System.Reflection;

namespace WhitetigerServer
{
    static class Service
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string [] args)
        {


            try
            {


                if (System.Environment.UserInteractive)
                {
                    string parameter = string.Concat(args);
                    switch (parameter)
                    {
                        case "--install":
                            ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                             White_Tiger.UserManagment.UserManager usrmngr = new White_Tiger.UserManagment.UserManager();
                                usrmngr.CreateUser("root", "", "", true, "");
                            break;
                        case "--uninstall":
                            ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                            break;
                    }

                }
                else
                {
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[] 
                    { 
                        new WhiteTigerService() 
                    };
                    ServiceBase.Run(ServicesToRun);

                }
       
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
           
            
                
        }
    }
}
