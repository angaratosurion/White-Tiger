using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceTools;
using White_Tiger.UserManagment;

namespace WTServiceServiceInstaller
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {
                if (args != null)
                {
                    if (args[0] == "-is")
                    {

                        if ((args[1] != null) && (args[2] != null) && (args[3]) != null)
                        {


                            White_Tiger.Config conf = new White_Tiger.Config();
                            conf.CreateFile();

                            if (!ServiceTools.ServiceInstaller.ServiceIsInstalled(args[1]))
                            {

                                if (ServiceTools.ServiceInstaller.GetServiceStatus(args[1]) == ServiceState.Starting)
                                {


                                    ServiceTools.ServiceInstaller.StopService(args[1]);
                                }
                                White_Tiger.UserManagment.UserManager usrmngr = new White_Tiger.UserManagment.UserManager();
                            //    usrmngr.CreateUser("root", "root", "", true, "");
                           //     usrmngr.CreateRole("root","root","Administrator",RoleType.Administrator);

                                Console.WriteLine("Installing Service :" + args[1]);
                                ServiceTools.ServiceInstaller.InstallAndStart(args[1], args[2], args[3]);
                            }
                        }
                    }
                    else if (args[0] == "-rs")
                    {
                        if (ServiceTools.ServiceInstaller.ServiceIsInstalled(args[1]))
                        {
                            if (ServiceTools.ServiceInstaller.GetServiceStatus(args[1]) == ServiceState.Starting)
                            {

                                ServiceTools.ServiceInstaller.StopService(args[1]);

                            }
                            Console.WriteLine("Removing Service :" + args[1]);
                            ServiceTools.ServiceInstaller.Uninstall(args[1]);
                        }
                    }

                }
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
       

        
        }
    }

