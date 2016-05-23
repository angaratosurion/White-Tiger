package net.pariskoutsioukis.TigerDroid.xml;

import java.util.ArrayList;
import java.util.List;

import net.pariskoutsioukis.TigerDroid.Tools;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

/**
 * The implementation of the  handler for the Xml SAX Parser
 * */
public class XmlSaxHandler extends DefaultHandler 
{
	String ele=null,content;
	 List<String []> recs = new ArrayList<String []>();
	List <String> rec;
	String [] cells;
	/**
	 *  The Records contained in the xml file 
	 * */

	public List<String []>  GetRecords()
	{
		return this.recs;
	}
	/**
	 *  Gets  Cells which exist in the xml file
	 *  */
	public String [] GetCells()
	{
		return cells;
	}
	/**
	 *  Sets cells which the handler will search for in the xml file 
	 *  */
	public void setCells(String [] cel)
	{
		cells=cel;
	}
	/**
	 *  Sets the element which indicates the start of a row
	 *   */
	public void setElemnt(String tele)
	{
		ele=tele;
	}
	/**
	 * Gets the element which is the start of a row
	 * */
	public String getElement()
	{
		return ele;
	}
	/**
	 * It initializes a new record in the records list  
	 * */
	@Override
	 public void startElement(String uri, String localName, 
             String qName, Attributes attributes) 
             //throws SAXException 
	 {
		try
		{
			if(qName==ele.replace("_.","_"))
			{
				rec= new  ArrayList<String>();
			}
			else 
			{
				
			}
			
		}
		catch(Exception ex)
		{
			Tools.Errorhandling(ex);
			//return null;
		}
		 
		 
	 }
	/**
	 *  It fills the newlly created record with values form the xml file if 
	 * of course the cells array isnt null
	 * For params look the android documentation on SAX Parser Handlers
	 * */
	@Override
	  public void endElement(String uri, String localName, String qName)
	  {
		 {
				try
				{int i;
					 if(this.cells==null)
					 {
						 return;
					 }
					if(qName==ele)
					{ 
						String [] trec = new String [rec.size()];
						for(int j=0;j <rec.size();j++)
						{
							trec[j]=String.valueOf(rec.toArray()[j]);
							
							
						}

						this.recs.add(trec);
					}
					else 
					{
						for (i=0;i<cells.length;i++)
						{
						
							if(qName==cells[i].replace("_.","_"))
							{
								rec.add(content);
							}
						}
						
					}
					
				}
				catch(Exception ex)
				{
					Tools.Errorhandling(ex);
					//return null;
				}
	  }
	                         
	 
	
}

	@Override
	  public void characters(char[] ch, int start, int length) 
	          throws SAXException
	{
	   
	    try
		{
	    	 content = String.copyValueOf(ch, start, length).trim();
			
		}
		catch(Exception ex)
		{
			Tools.Errorhandling(ex);
			//return null;
		}
	  }
}