using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hydrobase;
using ICSharpCode.SharpZipLib.Zip;
using White_Tiger.UserManagment;

namespace White_Tiger
{
   public  class Tools
    {
      public  Hydrobase.Cryptography.CryptograhyAlgorithm GetCryptographyAlgorythmEnum(string tenum)
       {
           try
           {
               Cryptography.CryptograhyAlgorithm ap = default(Cryptography.CryptograhyAlgorithm);
               if (Enum.TryParse<Cryptography.CryptograhyAlgorithm>(tenum, out ap))
               {

                   return ap;

               }
               else
               {
                   return default(Cryptography.CryptograhyAlgorithm);

               }

               return ap;
           }

           catch (Exception ex)
           {

               return default(Cryptography.CryptograhyAlgorithm);

           }
       }
      public Hydrobase.Cryptography.HashingAlogrithm GetHashingAlgorythmEnum(string tenum)
      {
          try
          {
              Cryptography.HashingAlogrithm ap = default(Cryptography.HashingAlogrithm);
              if (Enum.TryParse<Cryptography.HashingAlogrithm>(tenum, out ap))
              {

                  return ap;

              }
              else
              {
                  return default(Cryptography.HashingAlogrithm);

              }

              return ap;
          }

          catch (Exception ex)
          {

              return default(Cryptography.HashingAlogrithm);

          }
      }
      public RoleType GetUserRoleEnum(string tenum)
      {
          try
          {
              RoleType ap = default(RoleType);
              if (Enum.TryParse<RoleType>(tenum, out ap))
              {

                  return ap;

              }
              else
              {
                  return default(RoleType);

              }

              return ap;
          }

          catch (Exception ex)
          {

              return default(RoleType);

          }
      }

      public string EncodeCompressData(string args, string username, string tablename,string dbname,bool compressdata)
      {
          try
          {
              string ap = "";



              // ZipFile z = new ZipFile();
              Maintenance_Backup back = new Maintenance_Backup();

            
            

                  if ((args != null) && (username != null) && (tablename != null)
                      &&(dbname!=null))
                  {
                      string path = Path.Combine(MultyUser.GetUsersMainFolder("system"), "WhiteTiger", MultyUser.tmpFolder);


                      if (Directory.Exists(path) != true)
                      {
                          back.CreateFolder(path);
                      }

                      string rndfile = tablename + Guid.NewGuid().ToString() + ".hbt";
                      string file = Path.Combine(path, rndfile);
                      string zipdir = path;// Path.Combine(MultyUser.GetUsersMainFolder("system"), "WhiteTiger", MultyUser.tmpFolder, "zip");
                      string zipfile = Path.Combine(zipdir, rndfile+".zip");

                      if (Directory.Exists(file) != true)
                      {
                          back.CreateFolder(file);
                      }
                      StreamWriter wr = File.CreateText(Path.Combine(file,rndfile));
                      wr.WriteLine(args);
                      wr.Flush();
                      wr.Close();
                      wr.Dispose();
                      if (compressdata)
                      {
                          FastZip zip = new FastZip();
                          zip.CreateZip(zipfile, file , false,null);


                          ap = zipfile;
                      }
                      else
                      {
                          ap = file;
                      }
                  }

              return ap;

          }
          catch (Exception ex)
          {
              program.errorreport(ex);

              return null;
          }
      }
      //public string CompressData(string args, string username, string tablename, string dbname)
      //{
      //    try
      //    {
      //        string ap = "";



      //        ZipFile z = new ZipFile();
      //        Maintenance_Backup back = new Maintenance_Backup();




      //        if ((args != null) && (username != null) && (tablename != null)
      //            && (dbname != null))
      //        {
      //            string path = Path.Combine(MultyUser.GetUsersMainFolder("system"), "WhiteTiger", MultyUser.tmpFolder,
      //                dbname,
      //                "xml");
      //            string rndfile = tablename + Guid.NewGuid().ToString() + ".zip";
      //            string file = Path.Combine(path, Path.GetFileNameWithoutExtension(rndfile));
      //            string zipdir = Path.Combine(MultyUser.GetUsersMainFolder("system"), "WhiteTiger", MultyUser.tmpFolder,
      //                dbname, "zip");
      //            string zipfile = Path.Combine(zipdir, rndfile);

      //            if (Directory.Exists(path) != true)
      //            {
      //                back.CreateFolder(path);
      //            }
      //            FastZip zip = new FastZip();
      //            zip.CreateZip(zipfile, file, false, "*.hbt");


      //        }

      //        return ap;

      //    }
      //    catch (Exception ex)
      //    {
      //        program.errorreport(ex);

      //        return null;
      //    }
      //}
   }
}
