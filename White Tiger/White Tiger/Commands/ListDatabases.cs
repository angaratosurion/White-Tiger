using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace White_Tiger.Commands
{
   public class ListDatabases:ICommand
    {
        /// <summary>
        /// the Name of the command
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return "List Databases";
        }/// <summary>
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
                    string[] cols;

                    cols = program.whitetiger.ListDatabases(args[2], args[3]);
                    if (cols != null)
                    {
                        for (int i = 0; i < cols.Length; i++)
                        {
                            ap += "," + cols[i];
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
