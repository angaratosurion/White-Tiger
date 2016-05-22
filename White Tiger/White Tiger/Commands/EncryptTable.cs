using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace White_Tiger.Commands
{
    /// <summary>
    /// Encyrpts the table
    /// </summary>
 public   class EncryptTable:ICommand
    {
        /// <summary>
        /// the Name of the command
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return "Encrypt Table";
        }
        /// <summary>
        /// Executes it
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Execute(string[] args)
        {
              
            try
            {
                string ap = null;
                if (args != null)
                {
                    DataSet set = new DataSet();
                    set.ReadXml(args[6]);
                    List<object[]> vals = new List<object[]>();
                    if (set.Tables != null)
                    {
                        foreach (DataRow r in set.Tables[0].Rows)
                        {
                            vals.Add(r.ItemArray);
                        }
                        if (vals != null)
                        {
                            /* args[0] root
                            args[1] record tag
                            * args[2] username
                            * args[3] password
                            * args[4] db name
                            * args[5] tablename
                           */
                            program.whitetiger.Encrypt(args[0], args[1], args[2], args[4], args[5], vals, args[3], args[7], args[8], args[9]);
                        }
                    }
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
