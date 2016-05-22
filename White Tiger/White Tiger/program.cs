using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hydrobase;
using HydrobaseSDK;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using NLog;


namespace White_Tiger
{

  public   class program
    {
        internal static WhiteTigerService whitetiger = new WhiteTigerService();
        internal static Logger logman = NLog.LogManager.GetCurrentClassLogger();
        //public static TheDarkOwlLogger.TheDarkOwlLogger Bugger = new TheDarkOwlLogger.TheDarkOwlLogger(false, true, true, "WhiteTiger");
        public static void errorreport(Exception e)
        {
           
            NoNullAllowedException nonullallowedext = new NoNullAllowedException();
          //  System.Windows.Forms.MessageBox.Show(e.ToString());

            System.Data.RowNotInTableException exrownotinTable = new RowNotInTableException();
            System.ArgumentOutOfRangeException excarg = new ArgumentOutOfRangeException();
            System.IO.FileNotFoundException filenotfound = new FileNotFoundException();
            System.Xml.XmlException xmlexc = new XmlException();
           if ((e.GetType() != nonullallowedext.GetType()) && (e.GetType() != exrownotinTable.GetType())
                && (e.GetType() != excarg.GetType()) && (e.GetType() != filenotfound.GetType()) &&
                (e.GetType() != xmlexc.GetType())) 
            {
                program.logman.TraceException(e.Message, e);

            }
        }
       
        
    }
}
