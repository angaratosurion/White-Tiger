package net.pariskoutsioukis.TigerDroid.REST;
//import android.app.Activity;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.database.Cursor;
import android.util.Log;
import com.koushikdutta.async.future.Future;
import com.koushikdutta.async.http.AsyncHttpClient;
import com.koushikdutta.async.http.AsyncHttpGet;
import com.koushikdutta.async.http.AsyncHttpPost;
import com.koushikdutta.async.http.AsyncHttpResponse;
import com.koushikdutta.async.http.body.JSONObjectBody;
import net.pariskoutsioukis.TigerDroid.Sqllite.SqlLiteManager;
import net.pariskoutsioukis.TigerDroid.Tools;
import net.pariskoutsioukis.TigerDroid.json.CommandModel;
import net.pariskoutsioukis.TigerDroid.json.JSon;
import net.pariskoutsioukis.TigerDroid.xml.XmlSax;
import net.pariskoutsioukis.TigerDroid.xml.xmlDocumentCreator;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpConnectionParams;
import org.apache.http.params.HttpParams;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

/**
 * The White Tiger Client for Android default implementation
 * */
public class TigerDroidClient {

com.koushikdutta.async.http.AsyncHttpClient client;
    String uri;
    Tools tools = new Tools();
    HttpParams httpParameters = new BasicHttpParams();
    int timeoutConnection = 30000;
    int timeoutSocket = timeoutConnection*2;
      public  static float CurrentActionProgress=0;
    int t=0;
   
    
  	JSon  jsmngr= new JSon();
 
  	XmlSax xmlmngr = new XmlSax();
    //public final long MAXREQSize=2147483647;
    
    public String[] Cells;
    Activity act;
 
   Context ctx;
   /**
    * The default contructor
    * */
    public TigerDroidClient()
    {
       
    	
    	




    }
    
    /** 
     * The default contractor which sets the context localy (probably will not be used)
     * */
    public TigerDroidClient(Context cont)
    {
       
    	this();
        ctx=cont;
    	
    	 




    }
    /**
     * The default contractor which sets the context localy (probably will not be used)
     * */
    public TigerDroidClient(Context cont,Activity tact)
    {

        this();
        ctx=cont;
        act=tact;







    }


    /** Gets the uri of the White Tiger address*/

    public String GetURI() {
        try {

            return uri;

        } catch (Exception ex) {

            Tools.Errorhandling(ex);
            return null;


        }


    }
    /** 
     * Sets the IP of the White Tiger
     * */

    public void SetURI(String serverip) {


        try {
            if (serverip!= null) {

                uri ="http://"+ serverip+"/White_Tiger/Json/";

            }

        } catch (Exception ex) {
            //return null;
            Tools.Errorhandling(ex);

        }

    }
    /**
     * Gets the Version of the White Tiger Server*/

    public String GetServerVersion() {

        try {
            String ap = null;


            ap = ExecutePostCommand("GetVersion", null);


            return ap;
        } catch (Exception ex) {
            Tools.Errorhandling(ex);
            return null;


        }
    }

  
/** 
 * Executes a Get request on the given url given with the given parameters
 * @param url the url in which the get request will  happen
 * @param params the array with the params 
 * @return the local path in which the file is saved
 * */
  String  ExecuteGetFileCommand(String url,String [] params) {


        try {
          String ap = null;
           String jscmd,filename=null,command="GetFile";
        
            StringBuilder builder = new StringBuilder();
            HttpConnectionParams.setConnectionTimeout(httpParameters, timeoutConnection*80);
        	HttpConnectionParams.setSoTimeout(httpParameters, timeoutSocket*80);
        	
    		int idx = url.lastIndexOf("/");
    		if ( idx >=0)
    		{
    		filename= url.substring(idx + 1) ;
    		}
    	
    		 
               
               
            
         
             
            
	          
	     HttpGet getrequest = new HttpGet(url);
       
	 AsyncHttpGet req= new AsyncHttpGet(getrequest.getURI().toString());

       Future<File> tmp3= client.executeFile(req,"/sdcard/"+filename, new AsyncHttpClient.FileCallback()
        {


        	@Override
        	public void onCompleted(Exception e, AsyncHttpResponse arg1, File arg2)
        	{
	
        		if (e != null)
        		{
        			e.printStackTrace();
        			return;
        		}
        	  Log.i(Tools.TAG,arg2.getName());
                TigerDroidClient.CurrentActionProgress=100;
        	  t++;

		
        	}

           @Override
           public void onConnect(AsyncHttpResponse response) {

               super.onConnect(response);
               TigerDroidClient.CurrentActionProgress=0;
           }

           @Override
           public void onProgress(AsyncHttpResponse response, long downloaded, long total) {


                        float x= downloaded/total;
               TigerDroidClient.CurrentActionProgress=x;





               super.onProgress(response, downloaded, total);
           }
        }) ;
     
   
       ap=tmp3.get().getAbsolutePath();
  
      
        
   
	
 
		

            return ap;
        } 
        catch (Exception ex)
        {
            Tools.Errorhandling(ex);
            return null;


        }


    }


  /**
   * Executes a Post request on the given url given with the given parameters
   *
   * @param params the array with the params 
   * @return the result of the post request in xml format
   * */
   String  ExecutePostCommand(String command, String[] params) {


        try {
            String  ap = null;
			String tmp;
            String jscmd;
            String filename=null;
            CommandModel cmd=null;
            if (command != null) {
            
            	// tmp="";
              jscmd= jsmngr.CreateJson(command,params).toString();
          
          this.client= AsyncHttpClient.getDefaultInstance();
          
             
            HttpPost request = new HttpPost(uri+command);
                request.setHeader("Accept", "application/json");
                request.setHeader("Content-type", "application/json");
                //request.
                StringEntity se = new StringEntity(jscmd);
                se.setContentType("application/json");
              //  client.setParams(params);
                AsyncHttpPost req= new AsyncHttpPost(request.getURI().toString());
                
                
              
          
              
             
                request.setEntity(se);
               // StringBody body =  AsynchttpRequest..
                req.setBody(new JSONObjectBody(jsmngr.CreateJSon(command, params)));
               req.asHttpRequest().setHeaders(request.getAllHeaders());
             //  AsyncHttpRequest req2 = AsyncHttpRequest.create(request);
             
               
               
               // body.
              // req.se
             //  client.setTimeout(timeoutSocket*3);
               // AsyncHttpClient.StringCallback

              Future<String> tmp2= client.executeString(req,new AsyncHttpClient.StringCallback() {

                  @Override
                  public void onConnect(AsyncHttpResponse response)
                  {

                      TigerDroidClient.CurrentActionProgress=0;

                      super.onConnect(response);
                  }
				
				@Override
				public void onCompleted(Exception e, AsyncHttpResponse arg1, String arg2)
				{
					if (e != null) {
						 e.printStackTrace();
						 return;
						 }
				//	cmd= jsmngr.GetValues(arg2);

                    TigerDroidClient.CurrentActionProgress=100;




                }

                  @Override
                  public void onProgress(AsyncHttpResponse response, long downloaded, long total) {
                   //   if (progdlg!=null)
                    {
                        float x= downloaded/total;
                        TigerDroidClient.CurrentActionProgress=x;



                    }



                      super.onProgress(response, downloaded, total);
                  }
              });
             cmd = this.jsmngr.GetValues(tmp2.get());
           
         	
            
                if ((cmd.exception==null) ||(cmd.exception!="none"))
                {
                  return cmd.data;
                }
            
           
            	
        	
            	
            		
            
            
            
            
            
       	          
            		
            		  
            	}
                






            


            return ap;
        } 
        catch (Exception ex) {
            Tools.Errorhandling(ex);
            return null;


        }


    }

   /** 
    * Returns the Databases belonging to the given user
    * @param username the name of the user
    * @param pass the password of the user
    * @return the Databases belonging to the given user
    * */

    public String[] ListDatabses(String username, String pass) {
        try {
            String[] ap = null;

            if ((username != null) && (pass != null)) {
                String[] params = new String[13];
                params[0] = username;
                params[1] = pass;
                String t= ExecutePostCommand("ListDatabases", params);
               // ap = this.xmlmngr.GetArray(this.jsmngr.GetValues("ListDatabasesResult"+res));
                ap=t.split(",");


            }

            return ap;
        } catch (Exception ex) {
            Tools.Errorhandling(ex);
            return null;


        }


    }

    /** 
     * Returns the tables  belonging to the given database
     * @param username the name of the user
     * @param pass the password of the user
     * @param dbname name of the database
     * @return the tables  belonging to the given database
     * */
    public String[] ListTables(String username, String pass, String dbname) {
        try {
            String[] ap = null;

            if ((username != null) && (pass != null) && (dbname != null)) {
                String[] params = new String[13];
                params[2] = username;
                params[3] = pass;
               ap= ExecutePostCommand("ListTables",params).split(",");
              //  ap = null;

            }

            return ap;
        } catch (Exception ex) {
            Tools.Errorhandling(ex);
            return null;


        }


    }

    
 /** 
  * The name of the xml element which contains the rows
  * @param username name of the user
  * @param tablename name of the table
  * @return he name of the xml element which contains the rows
  * */
   public String GetElemntName(String username,String tablename)
   {
       try {
           String ap = "";
           if ((username != null)&&(tablename!=null)) {


              ap=username+"_"+tablename;

           }

           return ap;
       }

       catch( Exception ex)
       {
           Tools.Errorhandling(ex);
           return null;

       }
   }
   /**
    * Saves the Table  on the White tiger Server
    * @param recordtag name of the xml tag which contains the records values
    * @param username name of the user
    * @param pass user's password
    * @param dbname name of the database
    * @param tablename name of the table
    * @param sqlmngr the SqlLiteManager object which  contain the table
    * */
   public void SaveTable(String recordtag,String username,String pass,String dbname,String tablename
			,SqlLiteManager sqlmngr)
			{
	   try
		{
			

			
			if((username!=null) &&(pass!=null) &&(dbname!=null)
				&&(tablename!=null) &&(recordtag!=null) &&(sqlmngr!=null))
			{
				 String[] params = new String[13];
	             
	             params[0]="pynakas";
	     		params[1]=recordtag;
	     		params[2]=username;
	     		params[3]=pass;
	     		params[4]=dbname;
	     		params[5]=tablename;
	     		params[6]="";
	     		params[7]="";
	     		params[8]="";
	     		Cursor cur = sqlmngr.getAllRecords();
	     		xmlDocumentCreator docr= new xmlDocumentCreator();
	     		 String sdoc=docr.CreateXml(cur,sqlmngr.getPrimaryKeys() );
	     		 params[6]=sdoc;
	     		this.ExecutePostCommand("UpdateTable", params);
	     		
				

			
	     		
				
			}
			
			
		}
		catch(Exception ex)
		{
			Tools.Errorhandling(ex);
			
			
			
		}
	   
	   
			}

   /**
    * Closed the Table  on the White tiger Server
    * @param username name of the user
    * @param pass user's password
    * @param dbname name of the database
    * @param tablename name of the table
    * */
   public void CloseTable(String username,String pass,String dbname,String tablename)
			{
	   try
		{
			

			
			if((username!=null) &&(pass!=null) &&(dbname!=null)
				&&(tablename!=null) )
			{
				 String[] params = new String[13];
	             
	             params[0]="pynakas";
	     		
	     		params[2]=username;
	     		params[3]=pass;
	     		params[4]=dbname;
	     		params[5]=tablename;
	     		params[6]="";
	     		params[7]="";
	     		params[8]="";	     		
	     		this.ExecutePostCommand("CloseTable", params);
	     		
				

			
	     		
				
			}
			
			
		}
		catch(Exception ex)
		{
			Tools.Errorhandling(ex);
			
			
			
		}
	   
	   
			}

   /**
    * Load the Table  on the sqlliste manager
    * @param recordtag name of the xml tag which contains the records values
    * @param username name of the user
    * @param pass user's password
    * @param dbname name of the database
    * @param tablename name of the table
    * @param sqlmngr the sqllitemanager object which will contain the table
    * */
public void LoadTableToSQLLite(String recordtag,String username,String pass,String dbname,String tablename
		,SqlLiteManager sqlmngr)
{
	try
	{
		

		
		if((username!=null) &&(pass!=null) &&(dbname!=null)
			&&(tablename!=null) &&(recordtag!=null) &&(sqlmngr!=null))
		{
			 String[] params = new String[13];
             
             params[0]="pynakas";
     		params[1]=recordtag;
     		params[2]=username;
     		params[3]=pass;
     		params[4]=dbname;
     		params[5]=tablename;
     		params[6]="";
     		params[7]="";
     		params[8]="";
			

		
     		String urlpath=ExecutePostCommand("LoadTable", params);
     		params[6]=urlpath;
     		File xml=new File(this.ExecuteGetFileCommand(urlpath,params));
     	 
     		String elemnt =this.GetElemntName(username, tablename);
            xmlmngr.AttachXmltoSQLLite(sqlmngr, xml, tablename,Cells,true);
			
		}
		
		
	}
	catch(Exception ex)
	{
		Tools.Errorhandling(ex);
		
		
		
	}
}

/**
 * Decrypts the Table  on the sqlliste manager
 * @param root the root element of the xml
 *  @param recordtag name of the xml tag which contains the records values
    * @param username name of the user    * 
    * @param dbname name of the database
    * @param table name of the table
    * @param pass user's password
    * @param alg the encryption algorith which is used in the encryption of the table
    * @param hasalg the Hash algorith which is used in the hash generation of the key
    * @param passphrase the passphrase which is used to decrypt the database
    * @param sqlmngr the sqllitemanager object which will contain the table
    * @return the sqllitemanager object which will contain the table
 * */
public SqlLiteManager DecryptTable(String root, String recordtag, String username, String dbname, String table, String pass, String alg,
		String hashalg,
		String passphrase,
SqlLiteManager sqlmngr)
{
	try
	{
		SqlLiteManager ap=null;
		
		String [] params = new String[13];
		if((root!=null)&&(recordtag!=null)&&(username!=null)&&
				(dbname!=null)&&(table!=null)&&
				(pass!=null)&&(alg!=null) &&
				(hashalg!=null)&&(passphrase!=null))
			
		{		
		params[0]=root;
		params[1]=recordtag;
		params[2]=username;
		params[3]=dbname;
		params[4]=table;
		params[5]=pass;
		params[6]=alg;
		params[7]=hashalg;
		params[8]=passphrase;
		
			//ap= xmlmngr.ReadXml(this.jsmngr.GetValues("DecryptTable",ExecutePostCommand("DecryptTable",params )));
			
			
		
		}
		return ap;
	}
	catch(Exception ex)
	{
		Tools.Errorhandling(ex);
		return null;
		
		
	}
}


/***
 * Checks to see if the table is encrypted
 * @param username name of the user    
 *  @param pass user's password
  * @param dbname name of the database
  * @param tablename name of the table
    @return true if it is encrypted
 *  */
public Boolean IsTableEncrypted(String username, String pass, String dbname, String tablename)
{try
{
	Boolean ap=false;
	
	if((username!=null) &&(pass!=null))
	{
		String [] params= new  String [13];
		params[2]=username;
		params[3]=pass;
		params[4]=dbname;
		params[5]=tablename;
		ap= Boolean.parseBoolean(ExecutePostCommand("IsTableEncrypted",params ));
		
		
	}
	
	return ap;
}
catch(Exception ex)
{
	Tools.Errorhandling(ex);
	return false;
	
	
}
	
	
}
/** 
 * The Columns in an array
  @param username name of the user    
 *  @param pass user's password
  * @param dbname name of the database
  * @param tablename name of the table
  * @return The Columns in an array
 * */
public String [] GetColumns(String username, String pass, String dbname, String tablename)
{
	try
	{ String [] ap=null; int i;
	List<String> a = new ArrayList<String>();
		if((username!=null) &&(pass!=null) &&(dbname !=null)&&(tablename!=null))
		{
			String [] params= new  String [13];
			params[2]=username;
			params[3]=pass;
			params[4]=dbname;
			params[5]=tablename;
			String tmp=  this.ExecutePostCommand("GetColumns", params);
            ap=tmp.split(",");
            for(i=0;i<ap.length;i++)
            {
            	if(ap[i]!="none")
            	{
            	a.add(ap[i]);
            	}
            }
            ap=(String[]) a.toArray();
			
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
