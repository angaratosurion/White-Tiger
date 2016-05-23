package net.pariskoutsioukis.TigerDroid.xml;

import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.OutputStream;
import java.io.StringWriter;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.xmlpull.v1.XmlSerializer;

import android.content.Context;
import android.database.Cursor;
import android.util.Xml;
import net.pariskoutsioukis.TigerDroid.Tools;
import net.pariskoutsioukis.TigerDroid.Sqllite.SqlLiteManager;

/**
 * Handles the Xml File Creation
 * */
public class xmlDocumentCreator {

	/** 
	 * Creates an xml file with the given file name 
	 * @param filename name of the file with the path supplied
	 * @param cur a database cursor with the datbase contents
	 * @param ctx Application Context 
	 * @param primarykeys the list of primary keys, in case you o use unique values
	 * in Text also
	 * */
		
		public void  CreateXmlToFile(String filename, Cursor cur,Context ctx,
				String [] primarykeys)
		{
			try
			{
				if( filename !=null  && cur!=null)
				{
					
					
					
					//FileOutputStream _stream = ctx.openFileOutput(filename, Context.MODE_WORLD_WRITEABLE);
					String xml =this.CreateXml(cur, primarykeys);
					FileWriter wr = new FileWriter(filename,true);
					wr.append(xml);
					wr.flush();
					wr.close();
						
				
					
					
					
				}
				
			}
			catch(Exception ex)
			{
				Tools.Errorhandling(ex);
			}
		}
	

		/** 
		 * Creates an xml Document and returns it as string
		
		 * @param cur a database cursor with the datbase contents
		 * @param ctx Application Context 
		 * @param primarykeys the list of primary keys, in case you o use unique values
		 * in Text also
		 * @return the contents of the cursor as a String
		 * */
				
		public String  CreateXml( Cursor cur,String [] primarykeys)
		{
			try
			{
				String ap= null;
				if(  cur!=null)
				{
					 XmlSerializer serializer = Xml.newSerializer();
					 StringWriter writer = new StringWriter();
					 serializer.setOutput(writer);
				        serializer.startDocument("UTF-8", true);
				        serializer.startTag("", "pynakas");

					do
					{
						serializer.startTag("","eggrafi");
						for (int i=0;i<cur.getColumnCount();i++)
						{
						
							if(!cur.getColumnName(i).equalsIgnoreCase(SqlLiteManager.KEY_ID))
							{
							
								if( primarykeys!=null)
								{
							
									for(int j =0;j<primarykeys.length;j++)
								
									{
									
										if(cur.getColumnName(i).equalsIgnoreCase(primarykeys[j]))
									
										{
								
										String colname=cur.getColumnName(i).substring(1);
										serializer.startTag("","_."+colname);
										serializer.text(cur.getString(i));
										serializer.endTag("", "_."+colname);
									
										}
									
										else
									
										{
										serializer.startTag("",cur.getColumnName(i));
										serializer.text(cur.getString(i));
										serializer.endTag("", cur.getColumnName(i));
										}
								
									}
								}
								else
								{
									serializer.startTag("",cur.getColumnName(i));
									serializer.text(cur.getString(i));
									serializer.endTag("", cur.getColumnName(i));
								}
						
							}
						}
						serializer.endTag("","eggrafi");						
						
					} while(cur.moveToNext())   ;
					serializer.endDocument();			
				
			
					
					
					
					ap= writer.toString();
					
				
				
					
					
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
