using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace White_Tiger.Commands
{
    /// <summary>
    /// Updates the table
    /// </summary>
    public class SaveTable:ICommand
    {/// <summary>
        /// the Name of the command
        /// </summary>
        /// <returns></returns
        public string Name()
        {
            return "Save Table";
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
                   // string[] cells = args[6].Split(',');
                    DataSet set= new DataSet();
                    set.ReadXml(args[6]);
                    List<object[]> vals= new List<object[]>();
                    if (set.Tables != null)
                    {
                        foreach(DataRow r in set.Tables[0].Rows)
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
                            program.whitetiger.Backup(args[2], args[4], args[5], args[3]);
                            program.whitetiger.UpdateTable(args[0], args[1], args[2], args[3], args[4], args[5], vals);
                        }
                    }
                }
                return ap;

            }
            catch (Exception ex)
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
