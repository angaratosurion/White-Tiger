package net.pariskoutsioukis.TigerDroid.json;

import net.pariskoutsioukis.TigerDroid.Tools;

import org.apache.http.client.*;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.params.*;
import org.apache.http.impl.client.*;
import org.apache.http.*;
import org.json.*;

import android.util.*;
import android.util.Xml.*;

import java.io.*;
import java.util.zip.ZipEntry;
import java.util.zip.ZipInputStream;

import com.google.gson.*;
public class JSon {
public static  final  String  TAG_COMMANDNAME="commandname";
    public static  final String TAG_ROOT="root";
    public static  final String TAG_RECORDTAG="recordtag";

    public static  final String TAG_USERNAME="username";
    public static  final String TAG_PASSWORD="password";
    public static  final String TAG_DBNAME="dbname";
    public static  final String TAG_TABLENAME="tablename";

    public static  final String TAG_ALG="algorith";
    public static  final String TAG_HASHALG="hashalg";
    public static  final String TAG_DATA="data";
    public static  final String TAG_PASSPHRASE="passphrase";
    public static  final String TAG_FRIENDLYERRORMSG="friendlyerrormsg";
    public static  final String TAG_EXCEPTION="exception";
    public  static final String TAG_DATAFORMAT="dataformat";
   GsonBuilder builder = new GsonBuilder();
   Gson gson = builder.create();

public  CommandModel GetValues (String in)
{
	try
	{
		CommandModel ap =null,tmp;
		StringReader reader ;
        Log.i(Tools.TAG,in);
		JSONObject jsonrdr;
		
		if (in!=null)
		{
			/*reader = new StringReader (in);
			jsonrdr= new JSONObject(in);
			ap=jsonrdr.get(key+"Result").toString();
			*/
			int count;
			ZipInputStream zis;
			//InputStream is ;
			OutputStream os ;
			tmp = gson.fromJson(in, CommandModel.class);
		/*	if( tmp.commandname.equals("LoadTable"))
			{
				byte[] decodedString = Base64.decode(tmp.data,Base64.DEFAULT);
				
				if(decodedString!=null)
				{
					zis= new ZipInputStream(new ByteArrayInputStream(decodedString));
					 ZipEntry ze;
					  ByteArrayOutputStream baos = new ByteArrayOutputStream();
					 ze =zis.getNextEntry();
					 if( ze!=null)
					 {
					
						 byte [] buffer = new byte[(int) ze.getSize()];
					 
					 while ((count = zis.read(buffer)) != -1) 
		             {
		                baos.write(buffer, 0, count); 
		                byte [] bits = baos.toByteArray();
		                //baos.reset();
		                
		                
		             }
					 String t = new String(baos.toByteArray(),"UTF-8");
					 Log.i(Tools.TAG,t);
					 tmp.data=t;
					 }
					
					 
					
				}
			}*/
			ap=tmp;


			
			
			
			
			
			
		}

		
		return ap;
		
	}
	catch(Exception ex)
	{
		
		Tools.Errorhandling(ex);
        Log.i(Tools.TAG,in);
		return null;
		
		
	}
	
}
   public  String [] GetArray(String key,String in)
    {
        try
        {
            String [] ap =null;
            StringReader reader ;

            JSONArray jsonrdr;
            Log.i(Tools.TAG,in);
            if( (key !=null)&&(in!=null))
            {
                reader = new StringReader (in);
                jsonrdr= new JSONArray(in);
             ap = new String[jsonrdr.length()]   ;
                for(int i=0; i <ap.length;i++) {
                    ap[i] = jsonrdr.getString(i)   ;
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
   public JSONObject CreateJSon(String command, String[] params)
   {
       try {
           JSONObject ap = null;
           if (command != null && params != null) {

              ap = new JSONObject();
          /* args[0] root
           args[1] record tag
           * args[2] username
                   * args[3] password
                   * args[4] db name
           * args[5] tablename
                   */
               params=this.CheckEmptyAndReplace(params);
              
               ap.put(TAG_COMMANDNAME, command);
               ap.put(TAG_ROOT, params[0]);
               ap.put(TAG_RECORDTAG,params[1]);
               ap.put(TAG_USERNAME,params[2])  ;
               ap.put(TAG_PASSWORD,params[3]);;
               ap.put(TAG_DBNAME,params[4]);
               ap.put(TAG_TABLENAME,params[5]);
               ap.put(TAG_DATA,params[6]);
               ap.put(TAG_ALG,params[7]);
               ap.put(TAG_HASHALG,params[8]);
               ap.put(TAG_PASSPHRASE,params[9]);
               ap.put(TAG_FRIENDLYERRORMSG,params[10]);
               ap.put(TAG_EXCEPTION,params[11]);
               ap.put(TAG_DATAFORMAT,params[12]);
              // ap= gson.toJson(cmdmodel);





           }


           return ap;
       }

      catch (Exception ex) {
          Tools.Errorhandling(ex);
          return null;


      }

   }
 public String CreateJson(String command, String[] params)
 {
     try {
         String ap = null;
         if (command != null && params != null) {

            // ap = new JSONObject();
        /* args[0] root
         args[1] record tag
         * args[2] username
                 * args[3] password
                 * args[4] db name
         * args[5] tablename
                 */
             params=this.CheckEmptyAndReplace(params);
             /*
             ap.put(TAG_COMMANDNAME, command);
             ap.put(TAG_ROOT, params[0]);
             ap.put(TAG_RECORDTAG,params[1]);
             ap.put(TAG_USERNAME,params[2])  ;
             ap.put(TAG_PASSWORD,params[3]);;
             ap.put(TAG_DBNAME,params[4]);
             ap.put(TAG_TABLENAME,params[5]);
             ap.put(TAG_DATA,params[6]);
             ap.put(TAG_ALG,params[7]);
             ap.put(TAG_HASHALG,params[8]);
             ap.put(TAG_PASSPHRASE,params[9]);
             ap.put(TAG_FRIENDLYERRORMSG,params[10]);
             ap.put(TAG_EXCEPTION,params[11]);
             ap.put(TAG_DATAFORMAT,params[12]);*/
             CommandModel cmdmodel = new CommandModel();
             cmdmodel.commandname=command;
             cmdmodel.root=params[0];
             cmdmodel.recordtag=params[1];
             cmdmodel.username=params[2];
             cmdmodel.password=params[3];
             cmdmodel.dbname=params[4];
             cmdmodel.tablename=params[5];
             cmdmodel.data=params[6];
             cmdmodel.algorith=params[7];
             cmdmodel.hashalg=params[8];
             cmdmodel.passphrase=params[9];
             cmdmodel.friendlyerrormsg=params[10];
             cmdmodel.exception=params[11];
             cmdmodel.dataformat=params[12];
             ap= gson.toJson(cmdmodel);





         }


         return ap;
     }

    catch (Exception ex) {
        Tools.Errorhandling(ex);
        return null;


    }

 }
private String [] CheckEmptyAndReplace(String [] params)
{
	 try
     {
         String [] ap =null;
         int i=0;
         
         if( params !=null)
         {
        	 for(i=2; i<params.length;i++)
        	 {
        		 if(( params[i]=="")||(params[i]==null))
        		 {
        			 params[i]="none";
        		 }
        	 }
        	 if(( params[0]=="")||(params[0]==null))
    		 {
    			 params[0]="pynakas";
    		 }
        	 if(( params[1]=="")||(params[1]==null))
    		 {
    			 params[1]="eggrafi";
    		 }
        	 ap=params;
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
