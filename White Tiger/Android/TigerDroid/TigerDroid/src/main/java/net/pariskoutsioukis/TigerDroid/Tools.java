

package net.pariskoutsioukis.TigerDroid;
import java.io.*;
import java.util.Date;

//import android.annotation.SuppressLint;
import android.os.*;
import android.util.Log;
/** a Class with some basic tools */
public class Tools {
	
/** name of the file with the bugs*/
private static String bugfile = "errors.txt";

/** 
 * Converts the given array to String and adds a '&' between each element
 * @param params the array to be converted to string
 * @return the given array to String  with  '&' between each element
 * */
	public String ArrayParamToString(String[] params)
	{
		try
		{ int i;
			String ap=null;
			
			if((params!=null) &&(params.length >0))
			{
				
				for(i=0;i<params.length;i++)
				{
					ap+="&"+params[i];
				}
				
			}
			
			
			return ap;
		}
		catch(Exception ex)
		{
			Tools.Errorhandling(ex);
			return null;
			
			
		}
		
		
	}
//@SuppressLint("ParserError")
public static final String TAG="TigerDroid";
/** Here happens the error handling, saves the exception to a file
 * @param ex the exception which happened
 * */
    public static void Errorhandling(Exception ex)
    {
        Log.d(TAG, ex.toString(), ex);
        File f = new File(Environment.getExternalStorageDirectory()+"/TigerDroid/Logs");
        File ef =new  File(Environment.getExternalStorageDirectory()+"/TigerDroid/Logs/"+TAG+bugfile);
        Date date =new Date();
       // throw (ex);

        if ( !f.exists())
        {
            f.mkdir();
        }
        if ( ef.exists())
        {
            try {
                BufferedReader br = new BufferedReader(new FileReader(ef));
                String line,in="",out="";

                while ((line = br.readLine()) != null) {
                    in+=line+'\n';
                    // text.append('\n');
                }
                br.close();
                BufferedWriter bw = new BufferedWriter(new FileWriter(ef));
                out=in+"\n"+date.toString()+"\n"+ex.toString()+" : "+ex.fillInStackTrace().toString() ;
                bw.write(out);
                bw.close();


            }
            catch (IOException n)
            {
                // ef.createNewFile();
            }



        }
        else {
            try {
                ef.createNewFile();
                String out="";


                BufferedWriter bw = new BufferedWriter(new FileWriter(ef));
                out=date.toString()+"\n"+ex.toString()+" : "+ex.fillInStackTrace().toString() ;
                bw.write(out);
                bw.close();


            }
            catch (IOException n)
            {
                // ef.createNewFile();
            }

        }


    }

    /***
     * Replaces the characters _. in the xml elements  and returns them to a new file
     * @param file xml file to read
     * @return Replaces the characters _. in the xml elements  and returns them to a new file
     */
    public static File ReplaceInfile(File file)
    {
        try
        {  File ap=null;
            if ( file !=null && file.exists())
            {   BufferedReader br = new BufferedReader(new FileReader(file));
                String line,in="",out="";

                while ((line = br.readLine()) != null)
                {   String lin1 ,lin2;
                  lin1= line.replace("<_.","<_");
                    lin2=lin1.replace("</_.","</_");
                    line=lin2;
                    in+=line+'\n';
                    // text.append('\n');
                }
                ap=new  File(file.getAbsolutePath() +".tmp");
                ap.createNewFile();
                br.close();
                BufferedWriter bw = new BufferedWriter(new FileWriter(ap));
                bw.write(in);
                bw.close();


            }
            return ap;

        }
        catch(Exception ex)
        {
            Tools.Errorhandling(ex);
            return null;

        }
    }

    }

