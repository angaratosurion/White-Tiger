using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hydrobase;
using HydrobaseSDK;
using System.ServiceModel;
using System.Security;
using System.Security.AccessControl;
using System.Security.Authentication;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Security.Permissions;
using System.IO;
using System.Xml;
using System.Reflection;

namespace White_Tiger
{
    /// <summary>
    /// This class handles the security of the server
    /// </summary>
    public class SecurityTG
    {
        public SecurityTG()
        {
            Config conf = new Config();
            conf.CreateFile();
            conf.ReadConfig();

        }
        /// <summary>
        /// checks if user has rights to edit the file
        /// </summary>
        /// <param name="username">user's name</param>
        /// <param name="path">path of file name</param>
        /// <returns>true if can has right to edit file false otherwise</returns>
        public Boolean UserHasAccessOnFile(string username,string path)
        {
            try
            { 
                Boolean ap=false;
               // System.Windows.Forms.MessageBox.Show("hhh");
                WhiteTigerService.conf.ReadConfig();
                if (WhiteTigerService.pref.Filesecurity == true)
                {
                    if ((username != null) || (path != null) || (File.Exists(path) != false))
                    {
                        
                        FileInfo fileinf = new FileInfo(path);

                        System.Security.AccessControl.FileSecurity fs = fileinf.GetAccessControl();
                        AuthorizationRuleCollection aucol = fs.GetAccessRules(true, true,
        typeof(System.Security.Principal.SecurityIdentifier));
                        WindowsIdentity wi = new WindowsIdentity(username);
                        for (int i = 0; i < aucol.Count; i++)
                        {
                            FileSystemAccessRule fsac = (FileSystemAccessRule)aucol[i];
                            WindowsPrincipal pr = new WindowsPrincipal(wi);


                            if ((wi.User.Equals(fsac.IdentityReference) == true) || (pr.IsInRole((SecurityIdentifier)fsac.IdentityReference) == true))
                            {

                                ap = true;
                                break;

                            }


                        }


                    }
                    // ap = true;
                }
                else
                {
                   // System.Windows.Forms.MessageBox.Show("hi ");

                    ap = true;
                }
           
                return ap;
            }
           
            catch (Exception e)
            {

                program.errorreport(e);
                return false;
            }

        }
        /// <summary>
        /// checks if user has rights to acess the directory
        /// </summary>
        /// <param name="username">user's name</param>
        /// <param name="path">path of directory</param>
        /// <returns>true if can has right to acess directory false otherwise</returns>
        public Boolean UserHasAccessOnDirectory(string username, string path)
        {
            try
            {
                Boolean ap = false;
                //  System.Windows.Forms.MessageBox.Show("hi 2");
                WhiteTigerService.conf.ReadConfig();
                if (WhiteTigerService.pref.Filesecurity == true)
                {
                    if ((username != null) || (path != null) || (Directory.Exists(path) != false))
                    {
                        
                        DirectoryInfo dirinf = new DirectoryInfo(path);

                        System.Security.AccessControl.DirectorySecurity ds = dirinf.GetAccessControl();
                        AuthorizationRuleCollection aucol = ds.GetAccessRules(true, true,
    typeof(System.Security.Principal.WindowsIdentity));
                        WindowsIdentity wi = new WindowsIdentity(username);
                        //  System.Windows.Forms.MessageBox.Show("hi 1");
                        for (int i = 0; i < aucol.Count; i++)
                        {
                            FileSystemAccessRule drsac = (FileSystemAccessRule)aucol[i];
                            WindowsPrincipal pr = new WindowsPrincipal(wi);

                            if ((wi.User.Equals(drsac.IdentityReference) == true) || (pr.IsInRole((SecurityIdentifier)drsac.IdentityReference) == true))
                            {
                                //System.Windows.Forms.MessageBox.Show(wi.User.Value + "\n" + drsac.IdentityReference.Value);

                                ap = true;
                                break;

                            }


                        }

                    }
                }
                else 
                {

                   // System.Windows.Forms.MessageBox.Show("hi ");
                    ap = true;
                }
                return ap;
            }
            

            catch (Exception e)
            {

                program.errorreport(e);
                return false;
            }

        }

        /// <summary>
        /// sets the acess on the user as owner of the file
        /// </summary>
        /// <param name="username">name of user that will be owner of the file</param>
        /// <param name="path">path of the file</param>
        public void SetUserAcessOnFile(string username, string path)
        {
            try
            {
                if ((username != null) || (path != null) ||(File.Exists(path) != false))
            
                {
                    File.SetAccessControl(path,new FileSecurity(path,AccessControlSections.Owner));

                }


            }

            catch (Exception e)
            {


                program.errorreport(e);
                
            }
        }
        /// <summary>
        /// sets the acess on the user as owner of the Directory
        /// </summary>
        /// <param name="username">name of user that will be owner of the directory</param>
        /// <param name="path">path of the directory</param>
        public void SetUserAcessOnDir(string username, string path)
        {
            try
            {
                if ((username != null) || (path != null) || (Directory.Exists(path) != false))
                {
                   Directory.SetAccessControl(path, new DirectorySecurity(path,AccessControlSections.Owner));
                   

                }


            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }
    }
}
