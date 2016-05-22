using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace testclient
{
   public  class DBHandler
    {

       public void addrows(DataTable table)
       {
           try
           {
               if ( (table != null))
               { int i =0;
               object[] vals = new object[table.Columns.Count];

               for (i = 0; i < table.Columns.Count;i++ )
               {

                   Console.WriteLine("{0} :", table.Columns[i].Caption);
                   vals[i] = Console.ReadLine();


               }
               table.Rows.Add(vals);

               }

           }
           catch (Exception ex)
           {


           }
       }
    }
}
