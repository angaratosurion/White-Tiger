using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace White_Tiger
{
    /// <summary>
    /// The preferences of the server
    /// </summary>
  public   class prefs
  {
      bool filesecurity,servallowusercreation,autodiscovery,usecompression,autobackup,windowsauth;
      string servusername,userpassword;
      /// <summary>
      /// Gets or sets if server will check file permisions
      /// </summary>
      public bool Filesecurity
      {

          get { return filesecurity; }
          set { filesecurity = value; }

      }
      /// <summary>
      /// Gets or sets default username of service
      /// </summary>
      public string ServiceUserName
      {

          get { return servusername ; }
          set { servusername = value; }

      }
      /// <summary>
      /// Gets or sets default passworde of service
      /// </summary>
      public string ServicePassword
      {
          get { return userpassword; }
          set { userpassword = value; }


      }
      public bool AllowServiceToCreateUser
      {

          get { return servallowusercreation; }
          set { servallowusercreation = value; }
      }
      public bool Auto_Discovery
      {
          get { return autodiscovery; }
          set { autodiscovery = value; }

      }
      public bool UseCompressition
      {
          get { return usecompression; }
          set { usecompression = value; }
      }
      public bool AutoBackup { get { return autobackup; } set { autobackup = value; } }
      public bool WindowsAutherication { get { return windowsauth; } set { windowsauth = value; } }

    }
}
