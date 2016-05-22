using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel.Discovery;
using System.ServiceModel;
using System.Net;
using Hydrobase;
using HydrobaseSDK;
using System.IO;
using System.Xml;
using NLog;
using System.Reflection;

//using White_TigerReference;

namespace White_Tiger_Managment
{

   public class Program
   {
       public static White_TigerServiceReference.WhiteTigerServiceClient client;//= new White_TigerServiceReference.WhiteTigerServiceProgram.client();
       public static MainWindow Mainwnd;
       public static frmEdit ActiveEditForm;
       public static string ActiveDb;
       internal static Logger logman = NLog.LogManager.GetCurrentClassLogger();
       public static string TempPath;
        static void Main(string[] args)
        {
          //System.Windows.Forms.Application.Run(new frmMain());
            Mainwnd = new MainWindow();
            System.Windows.Forms.Application.Run(Mainwnd);
            CreateTempPath();
        }
       // public static TheDarkOwlLogger.TheDarkOwlLogger Bugger = new TheDarkOwlLogger.TheDarkOwlLogger(false, true, true, "WhiteTiger");
        public static void errorreport(Exception e)
        {

            NoNullAllowedException nonullallowedext = new NoNullAllowedException();

            System.Data.RowNotInTableException exrownotinTable = new RowNotInTableException();
            System.ArgumentOutOfRangeException excarg = new ArgumentOutOfRangeException();
            System.IO.FileNotFoundException filenotfound = new FileNotFoundException();
            System.Xml.XmlException xmlexc = new XmlException();
            if ((e.GetType() != nonullallowedext.GetType()) && (e.GetType() != exrownotinTable.GetType())
                 && (e.GetType() != excarg.GetType()) && (e.GetType() != filenotfound.GetType()) &&
                 (e.GetType() != xmlexc.GetType()))
            {
                logman.TraceException(e.Message, e);

            }
        }
       public static void CreateTempPath()
        {
            try
            {
                string apppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "");
                TempPath = Path.Combine(apppath, "Temp");
                if( !Directory.Exists(TempPath))
                {
                    Directory.CreateDirectory(TempPath);
                }

            }
            catch (Exception ex)
            {

                errorreport(ex);
            }

        }
    }
    }

