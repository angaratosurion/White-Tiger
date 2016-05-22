package net.pariskoutsioukis.TigerDroid.Sqllite;

/**
 * Created by angarato surion on 10/1/2014.
 */

import android.content.ContentValues;
import android.nfc.Tag;
import android.util.Log;
import net.pariskoutsioukis.TigerDroid.*;

import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserFactory;






import android.content.Context;

import com.android.internal.util.XmlUtils;

import org.xml.sax.helpers.*;

import android.database.*;
import android.database.sqlite.*;

import java.util.ArrayList;
import java.util.List;

public class SqlLiteManager extends SQLiteOpenHelper  {
   //SQLiteDatabase db = new SQLiteDatabase();
   SQLiteDatabase DB;
    String DATABASE_NAME,Table_Name;
 public  static final String KEY_ID="_id";
    String [] pkeys;
    String [] Cells;
    SQLiteDatabase.CursorFactory factor;
    String CREATE_TABLE="";

    /***
     *Gets the Cells of the table
     * @return
     */
    public String [] GetCells()
    {
    	return Cells;
    }

    /***
     * Sets the cells of the table
     *
     * @param cells  cells of the table
     */
    public void setCells(String [] cells)
    {
    	Cells=cells;
    }

    /***
     * Gets the primary keys of the table
     * @return
     */
    public String [] getPrimaryKeys()
    {
    	return pkeys;
    }

    /***
     * sets the primary keys of the table   except the _id one
     * @param keys
     */
    public void setPrimaryKeys(String [] keys)
    {
    	pkeys=keys;
    }

    /***
     * Constractor of the SqlLiteMnanger
     * @param context
     * @param name
     * @param factory
     * @param version
     */
    public SqlLiteManager (Context context, String name,SQLiteDatabase.CursorFactory factory, int version)
    {  super(context, name, factory, version);
        this.DATABASE_NAME=name;
        this.factor=factory;
    }

    /***
     * Constractor of the SqlLiteMnanger
     * @param cont
     * @param dbname
     * @param table
     */
   
	public SqlLiteManager(Context cont ,String dbname,String table)
    {
        super(cont,dbname,null,1);
        DATABASE_NAME=dbname;
        Table_Name=table;

    }

    /***
     * Executes the create table script , which creates the table
     * @param db
     */
    @Override
    public void onCreate(SQLiteDatabase db) {
        try
        {
           // SQLiteDatabase db= this.getWritableDatabase();
            db.execSQL(CREATE_TABLE);
            this.DB=db;
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
        }

    }

    /***
     *Drops the existing table and creates the newer version
     * @param db
     * @param i
     * @param i2
     */
    @Override
    public void onUpgrade(SQLiteDatabase db, int i, int i2) {
        try
        {
            db.execSQL("DROP TABLE IF EXISTS " +Table_Name);
            onCreate(db);
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
        }

    }

    /***
     *  Creates or Opens the Table from the given database
     * @param dbfilename    the name of the database
     * @param tablename    Name of the table
     * @param tcells    cells of the table
     */
    public void CreateorOpenDB(String dbfilename,String tablename,String [] tcells)
    {
        try
        {
            int eventType ;
            if ( (dbfilename !=null) &&(tablename!=null))
            {    DATABASE_NAME=dbfilename;
                 Table_Name=tablename;
                ArrayList<String> tprimkey= new ArrayList<String>();

                Cells=tcells;
              this.getWritableDatabase().execSQL("DROP TABLE IF EXISTS " +Table_Name);

                CREATE_TABLE = "CREATE TABLE "+ tablename +"(\n";
                for (int i=0;i<tcells.length;i++)
                {
                    if ( tcells[i].startsWith("_."))
                    {
                        CREATE_TABLE+=tcells[i].replace("_.","_") + " TEXT UNIQUE, ";
                        tprimkey.add(tcells[i]);
                    }
                    else
                    {
                        if(i<tcells.length-1) {
                            CREATE_TABLE += tcells[i] + " TEXT, ";
                        }
                        else
                        {
                            CREATE_TABLE += tcells[i] + " TEXT";

                        }
                    }

                }

                CREATE_TABLE+=", "+KEY_ID+"  INTEGER PRIMARY KEY AUTOINCREMENT);";

                  this.DB = this.getWritableDatabase()  ;
                this.DB.execSQL(CREATE_TABLE);
                pkeys= (String [])tprimkey.toArray();

            }






        }
        catch(Exception ex)
        {
            Tools.Errorhandling(ex);
        }


    }

    /***
     *  Creates or Opens the Table from the given database
     * @param dbfilename     the name of the database
     * @param tablename    Name of the table
     * @param tcells       cells of the table
     * @param dropexisting   if true it drops the existing table
     */
    public void CreateorOpenDB(String dbfilename,String tablename,String [] tcells,Boolean dropexisting)
    {
        try
        {
            int eventType ;
            if ( (dbfilename !=null) &&(tablename!=null))
            {    DATABASE_NAME=dbfilename;
                 Table_Name=tablename;
                ArrayList<String> tprimkey= new ArrayList<String>();

                Cells=tcells;
                if ( dropexisting)
                {
                  this.getWritableDatabase().execSQL("DROP TABLE IF EXISTS " +Table_Name);
                }

                CREATE_TABLE = "CREATE TABLE "+ tablename +"(\n";
                for (int i=0;i<tcells.length;i++)
                {
                    if ( tcells[i].startsWith("_."))
                    {
                        CREATE_TABLE+=tcells[i].replace("_.","_") + " TEXT UNIQUE, ";
                        tprimkey.add(tcells[i]);
                    }
                    else
                    {
                        if(i<tcells.length-1) {
                            CREATE_TABLE += tcells[i] + " TEXT, ";
                        }
                        else
                        {
                            CREATE_TABLE += tcells[i] + " TEXT";

                        }
                    }

                }

                CREATE_TABLE+=", "+KEY_ID+"  INTEGER PRIMARY KEY AUTOINCREMENT);";

                  this.DB = this.getWritableDatabase()  ;
                this.DB.execSQL(CREATE_TABLE);
                Object [] k= tprimkey.toArray();

                for(int i=0;i<k.length;i++) {
                    pkeys[i]= String.valueOf(k[i]);
                }

            }






        }
        catch(Exception ex)
        {
            Tools.Errorhandling(ex);
        }


    }


    private int [] indexPrimaryKeyPositions()
    {

    	try
    	{
    		
    		int [] ap=null;
    		int i=0,j=0;
    		if ((pkeys!=null) &&(this.Cells!=null))
    		{
    			ap= new int [pkeys.length];
    			for(i=0;i<Cells.length;i++)
    			{
    				for(j=0;j<pkeys.length;j++)    				
    				{
    					Log.i(Tools.TAG,"Pkeys" + pkeys[j]);
    					if ( Cells[i]==pkeys[j])
    					{
    					
    						ap[j]=i;
    					}
    				}
    			}
    		}
    		
    		
    		
    		return ap;
    		
    	}
    	 catch (Exception e)
        {
            Tools.Errorhandling(e);
            return null;
        }
    	
    }

    /***
     * Checks if a record with the given values exists based on the primary keys.
     * @param vals  array with the values of the row
     * @return   true if it already exists
     */
    public Boolean CheckifRecordExist(Object [] vals)
    {
    	try
    	{
    		Boolean ap= false;
    		int i=0;
    		int [] pos;
    		
    		if ( (vals!=null&&(this.pkeys!=null)))
    		{
    			pos= this.indexPrimaryKeyPositions();
    			if(pos !=null)
    			{
    			String Qry = "SELECT * FROM " + Table_Name +" WHERE ";
    			if (pkeys.length>1)
    			{
    			
    			
    				for(i=0;i<pkeys.length;i++)
    			
    				{
    				Qry+=pkeys[i]+" = "+vals[pos[i]]+" AND ";
    			
    			
    				}
    			}
    			else
    			{
    				Qry+=pkeys[0]+" = \""+vals[pos[0]]+"\"";
    			}
    				SQLiteDatabase db = this.getReadableDatabase();
    				Cursor cursor = db.rawQuery(Qry, null);
    				
    				
    				if(cursor.getCount()>0)
    				{
    					ap=true;
    				}
    				cursor.close();
                
    			}
    		}
    		
    		
    		
    		return ap;
    	}
    	  catch (Exception e)
        {
            Tools.Errorhandling(e);
            return false;
        }
    }

    /***
     * Creates a new non existing record on the table containing the given values
     * @param vals the array containing the values to be added
     */
    public void addNewRecord(String [] vals)
    {
        try {
            if(( Cells !=null)&&(vals!=null) &&(vals.length==Cells.length))
            {
              
                ContentValues values = new ContentValues();
                if (!this.CheckifRecordExist(vals))
                {
                	  SQLiteDatabase db = this.getWritableDatabase();
                //	return ;
                
            
                	for ( int i =0 ; i< Cells.length;i++)
                	{
             //      Log.i(Tools.TAG,String.valueOf(i));
               
                		Log.i(Tools.TAG,Cells[i]);
            	  
                        values.put(Cells[i].replace("_.","_"),String.valueOf(vals[i]));
               
                	}
                if (values.size()>0) 
                {
                    db.insert(Table_Name, null, values);
                    db.close();
                  }
                db.close();
                }
            }
            
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
        }

    }

    /***
     * Gets all the Records in the Table
     * @return    Cursor which contains all the records in the table
     */
    public Cursor getAllRecords()
    {
        try {
           // List<Waypoint> lstpoi = new ArrayList<Waypoint>();
            String selectqry = "SELECT * FROM " + Table_Name  ;
            SQLiteDatabase db = this.getReadableDatabase();
            Cursor cursor = db.rawQuery(selectqry, null);
            if( cursor !=null)
            {
                cursor.moveToFirst();
            }

            return cursor;
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
            return null;
        }

    }

    /***
     * Gets all the Records in the Table with only the selected collumns  and ordered by collumn
     * @param tcells   selectted collums
     * @param orderby   the collumn which they will by order by
     * @return a cursor with all the Records in the Table with only the selected collumns  and ordered by collumn
     */
    public Cursor getAllRecords(String[] tcells,String orderby)
    {
        try {

            SQLiteDatabase db = this.getReadableDatabase();

            Cursor cursor = db.query(Table_Name,tcells,null,null,null,null
                    ,orderby);
            if( cursor !=null)
            {
                cursor.moveToFirst();
            }

            return cursor;
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
            return null;
        }

    }

    /***
     * all the Records in the Table with only the selected columns
     * @param tcells  selected collums
     * @return  a cursor with  all the Records in the Table with only the selected columns
     */
    public Cursor getAllRecords(String[] tcells)
    {
        try {


            Cursor cursor = this.getAllRecords(tcells,null);

            return cursor;
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
            return null;
        }

    }

    /***
     * Count of Records in the table
     * @return  Count of Records in the table
     */
    public int getRecordCount()
    {
        try {
            String countQry = "SELECT * FROM " + Table_Name;
            SQLiteDatabase db = this.getReadableDatabase();
            Cursor cursor = db.rawQuery(countQry, null);
            cursor.close();
            return cursor.getCount();
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
            return -1;
        }


    }

    /***
     *     updates the record which contains the values of the given array based on the primary key
     * @param vals
     * @return  the number of affected records
     */
    public int updateRecord(String [] vals)
    {
        try {
            if ( Cells!=null) {
                SQLiteDatabase db = this.getWritableDatabase();
                ContentValues values = new ContentValues();
                int p=0;
                for(int i=0;i<Cells.length;i++)
                {
                    values.put(Cells[i],vals[i]);
                }
                if ( pkeys !=null) {
                    for( int i=0 ;i<Cells.length;i++)
                    {
                          if( Cells[i]==pkeys[0])
                          {
                              p=i;
                              break;
                          }
                    }
                    return db.update(Table_Name, values, pkeys[0] + " =?", new String[]{vals[p]});
                }
                return db.update(Table_Name, values, null,null);
            }
            return 0;
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
            return -1;
        }


    }

    /***
     * Deletes the record with the given id
     * @param id   the id of the record to be deleted
     */
    public  void deleteRecord(int id)
    {
        try {
            SQLiteDatabase db = this.getWritableDatabase();
            //db.delete(Table_Name, KEY_ID + " =?", new String[]{String.valueOf(id)});
            db.close();
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
        }
    }

    /***
     * Deletes the record based on the value the given column has
     * @param col the name of the collumn which must have the given val
     * @param val  the value that the selected column must have in order to be deleted.
     */
    public  void deleteRecord(String col, String val)
    {
        try {
        	
        	  if (col != null && val != null)
        	  {
            SQLiteDatabase db = this.getWritableDatabase();
            db.delete(Table_Name, col + " =?", new String[]{val});
            
        	  
            db.close();
        	 
        	  }
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
        }
    }

    /***
     *  Gets the record which in the given column name has similar values like the given one.
     * @param col   the name of the column which must have the given value
     * @param val  the value that the selected column must have .
     * @return  the record which in the given column name has the given value
     */
    public Cursor findRecord(String col, String val)
    {
        try {
            if (col != null && val != null) {
                SQLiteDatabase db = this.getReadableDatabase();
                String selectqry= "SELECT * FROM "+ Table_Name +" WHERE "+col + " LIKE ?";

               
              Cursor cursor = db.rawQuery(selectqry,new String []{val});
             

                if (cursor != null) {
                    cursor.moveToFirst();
                    return cursor;
                }
               // return poi;
            }
            return null;
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
            return null;
        }


    }

    /***
     *   Gets the record which in the given column name has the given value
     * @param col  the name of the column which must have the given value
     * @param val  the value that the selected column must have .
     * @return the record which in the given column name has the given value
     */
    public Cursor getRecord(String col, String val)
    {
        try {
            if (col != null && val != null) {
                SQLiteDatabase db = this.getReadableDatabase();
                String selectqry= "SELECT * FROM "+ Table_Name +" WHERE "+col + " =? COLLATE NOCASE";

                Cursor cursor = db.rawQuery(selectqry,new String []{val});

                if (cursor != null) {
                    cursor.moveToFirst();
                    return cursor;
                }
               // return poi;
            }
            return null;
        }
        catch (Exception e)
        {
            Tools.Errorhandling(e);
            return null;
        }


    }

}
