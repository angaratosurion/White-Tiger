package net.pariskoutsioukis.TigerDroid.xml;

import java.io.File;
import javax.xml.parsers.SAXParser;
import javax.xml.parsers.SAXParserFactory;

import net.pariskoutsioukis.TigerDroid.Tools;
import net.pariskoutsioukis.TigerDroid.Sqllite.SqlLiteManager;

/**
 *  The class which reads the xml and adds it to the sql table
 * */

public class XmlSax 
{
	/**
	 *  It attaches thew xml file to sqlmanger and returns it.
	 * @param sqlmngr the sqlManager object in which the xml content will be added 
	 * @param the File in which contains the xml	 * 
	 * @param ele the element which is repeating and has the cells of the xml as children nodes.	 * 
	 * @param an String array which has the elements
	 * */
	public SqlLiteManager  AttachXmltoSQLLite(SqlLiteManager sqlmngr,File xml, String ele,
			String [] cells)
	{
	    try
	    {

			sqlmngr = this.AttachXmltoSQLLite(sqlmngr,xml,ele,cells,true)      ;
	        
	           

	     return sqlmngr;
	    }
	    catch (Exception e)
	    {
	        Tools.Errorhandling(e);
	        return null;
	    }
	}
	/**
	 *  It attaches thew xml file to sqlmanger and returns it.
	 * @param sqlmngr the sqlManager object in which the xml content will be added
	 * @param the File in which contains the xml	 *
	 * @param ele the element which is repeating and has the cells of the xml as children nodes.	 *
	 * @param an String array which has the elements
	 * @param DeleteFile  if true deletes the attached file
	 * */
	public SqlLiteManager  AttachXmltoSQLLite(SqlLiteManager sqlmngr,File xml, String ele,
											  String [] cells,Boolean DeleteFile)
	{
		try
		{
			SqlLiteManager ap =null;
			SAXParserFactory parserFactor = SAXParserFactory.newInstance();
			SAXParser parser = parserFactor.newSAXParser();
			XmlSaxHandler handler = new XmlSaxHandler();
			//  parser.parse(ClassLoader.getSystemResourceAsStream("xml/employee.xml"),      handler);


			// Log.i(Tools.TAG,rec[0]);

			if((sqlmngr!=null)&&(xml!=null) &&(ele !=null)&&(cells!=null))
			{

				handler.setCells(cells);
				handler.setElemnt(ele);
				File txml = Tools.ReplaceInfile(xml);

				parser.parse(txml, handler);
				for ( String[] rec: handler.GetRecords())
				{
					sqlmngr.addNewRecord(rec);



				}
				if ( DeleteFile) {
					xml.delete();
				}
			   txml.delete();

			}

			return sqlmngr;
		}
		catch (Exception e)
		{
			Tools.Errorhandling(e);
			return null;
		}
	}



}
