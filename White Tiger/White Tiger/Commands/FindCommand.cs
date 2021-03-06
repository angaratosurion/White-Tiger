﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace White_Tiger.Commands
{
   public class FindCommand:ICommand
    {
       /// <summary>
        /// the Name of the command
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return "IsTableEncrypted";
        }
        // <summary>
        /// the type fo command
        /// </summary>
        /// <returns></returns>
        public CommandType Type()
        {
            return CommandType.SQL;

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
        /// Executes it
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Execute(string[] args)
        {
            try
            {
                string ap = "";
                if ((args != null))
                {
                    ap =program.whitetiger.Find(args[2],args[3],args[4],args[5],args[6],args[10]);

                }

                return ap;
            }
            catch (Exception ex)
            {
                return ex.ToString();
                // return null;
            }



        }


    }
    }

