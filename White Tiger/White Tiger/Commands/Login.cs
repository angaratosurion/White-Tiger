using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using White_Tiger;
namespace White_Tiger.Commands
{
    /// <summary>
    /// Logins to the server
    /// </summary>
   public class Login :ICommand
    {
      
        /// <summary>
        /// the Name of the command
        /// </summary>
        /// <returns></returns
       public string Name()
       {
      return "Login"; 
       }
       // <summary>
       /// the type fo command
       /// </summary>
       /// <returns></returns>
       public CommandType Type()
       {
          return    CommandType.Autherication; 

       }
       /// <summary>
       /// a Help text
       /// </summary>
       /// <returns></returns>
       public string Help()
       {
           return "";

       }
       /// <summary>
       /// the version
       /// </summary>
       /// <returns></returns>
       public string Version()
       {
           return "1.0";
       }
       ///
       /// <summary>
       /// Executes it
       /// </summary>
       /// <param name="args"></param>
       /// <returns></returns>
     public   string Execute( string [] args)
       {
           try
           {
               bool ap = false;
               if ((args != null))
               {
                   ap = program.whitetiger.Login(args[0], args[1]);

               }

               return ap.ToString();
           }
           catch (Exception ex)
           {
               return  ex.ToString();
             //  return null;
           }


           
       }
   

    }
}
