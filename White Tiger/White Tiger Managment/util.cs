using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;

namespace White_Tiger_Managment
{
  public   class Util
  {
      public Stream  StringToStream(string text)
      {
         try
          {
              Stream str = null;
              if (text != null)
              {
                  byte[] bitar = Encoding.UTF8.GetBytes(text);
                  MemoryStream ms = new MemoryStream(bitar);
                  str = ms;
                
                
              }
              return str;

          }
          catch (Exception ex)
          {
              Program.errorreport(ex);
              return null;

          }


      }

      public void ByteToFile(string filename,byte [] str)
      {
          try
          {
              //Stream str = null;
              byte[] buffer ;
           int bytesRead = 0,i=0;
           FileStream file;
             
              if ( filename != null && !File.Exists(filename))
              {

                ;
                  file = File.OpenWrite(filename);
                
                     
                      file.Write(str, 0, str.Length);


                
                  file.Flush();
                  file.Close();

              }
              
          }
          catch (Exception ex)
          {
              Program.errorreport(ex);
            
          }


      }
      
      public string RepleaceEmptyString(object[] vals)
      {

          try
          {
              return null;

          }
          catch (Exception ex)
          {
              return null;

          }


      }

      
    }
}
