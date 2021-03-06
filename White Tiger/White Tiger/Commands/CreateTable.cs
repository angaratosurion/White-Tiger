﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace White_Tiger.Commands
{
    /// <summary>
    /// 
    /// Creates the Table
    /// </summary>
  public  class CreateTable:ICommand
    {
        /// <summary>
        /// the Name of the command
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
           return  "Create Table";
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
                if(args !=null)
                {
                    string [] cells = args[6].Split(',');

                     program.whitetiger.CreateTable(args[0], args[1], args[2], args[3], args[4], args[5], cells);
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
