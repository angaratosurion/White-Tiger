using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace White_Tiger.Commands
{
    /// <summary>
    /// Decrypts the table
    /// </summary>
    public class DecryptTable:ICommand
    {  /// <summary>
        /// the Name of the command
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return "Decrypt Table";
        }
        /// <summary>
        /// Executes it
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Execute(string[] args)
        {
            string ap = null;

            try
            {
               if(args !=null)
                {
                  
                             /* args[0] root
                             args[1] record tag
                             * args[2] username
                             * args[3] password
                             * args[4] db name
                             * args[5] tablename
                            */
                    ap=program.whitetiger.Decrypt(args[0], args[1], args[2], args[4], args[5], args[3],args[6],args[7],args[8]);
                        
                }
                return ap;

            }
            catch(Exception ex)
            {
                program.errorreport(ex);
                return ex.ToString();
            }
        }
        /// <summary>
        /// the version
        /// </summary>
        /// <returns></returns>
        public string Version()
        {
            return "1.0";
        }
        /// <summary>
        /// a Help text
        /// </summary>
        /// <returns></returns>
        public string Help()
        {
            return "";
        }
        // <summary>
        /// the type fo command
        /// </summary>
        /// <returns></returns>
        public CommandType Type()
        {
            return CommandType.SQL;
        }
    }
}
