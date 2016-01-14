///////////////////////////////////////////////////////////////
// PersistenceEngine.cs - Persist database contents to XML   //
// Ver 1.1                                                   //
// Application: Demonstration for CSE681-SMA, Project#4      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    HP EliteBook,Core-i5, Windows 10             //
// Author:      Abhilash Udayashankar,SUID 778388557         //
//               (774) 540-1234                              //
///////////////////////////////////////////////////////////////
/* 
* Package Operations:
* ------------------- 
* This package defines all the methods to persist the database
* contents to XML file 
*
*/
/*
*Public Interface
*---------------
*persistdb()
*-used to persist the database contents to XML 
*Main()
*-main method for package
*-used to test the persistenceEngine
*
/* Maintenance:
* ------------
* Required Files: DBEngine.cs, DBElement.cs,Display.cs,
*                 DBExtensions and UtilityExtensions.cs 
*
* Build Process:  devenv Project2Starter.sln /Rebuild debug
*                 Run from Developer Command Prompt
*                 To find: search for developer
*
* Maintenance History:
* --------------------
* ver 1.1:: 23 Nov 15
 * - Namespace changed
*ver 1.0:09 Oct 15
* -First release
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Xml;
using System.Xml.Linq;


namespace Project4Code
{
    public class PersistenceEngine
    {
        public void persistdb<Key, Value, Data>(DBEngine<Key, Value> db)
        {
            XDocument xml = new XDocument();
            xml.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            XComment comment = new XComment("NoSQL Database");    //title for XML
            xml.Add(comment);
            XElement NoSQLelem = new XElement("NoSQLDB_ELEMENTS");
            XElement type = new XElement("numbers", typeof(Key));
            XElement payload = new XElement("name", typeof(Data));
            xml.Add(NoSQLelem);
            NoSQLelem.Add(type);
            NoSQLelem.Add(payload);
            foreach (Key k1 in db.Keys())               {
                XElement tags = new XElement("open_close");    
                XElement key = new XElement("new_Key", k1);   //tag for new keys generated
                tags.Add(key);
                Value value1;
                db.getValue(k1, out value1);
                DBElement<Key, Data> element = value1 as DBElement<Key, Data>;
                WriteLine(element.showElement());
                XElement dbelement = persistdbelement<Key, Data>(element);
                tags.Add(dbelement);
                NoSQLelem.Add(tags);
                xml.Save("XML_FILE_PROJECT4.xml");                               //XML file name
            }
        }
        public XElement persistdbelement<Key, Data>(DBElement<Key, Data> elem)
        {
            XElement element = new XElement("newelement");
            XElement name = new XElement("name", elem.name);            // name of elements
            XElement descr = new XElement("description", elem.descr);   // description of elements
            element.Add(name);
            element.Add(descr);
            if (elem.children.Count() > 0)
            {
                XElement children = new XElement("Children");            //defines the children of key
                foreach (Key key in elem.children) 
                {
                    XElement k1 = new XElement("key", key);
                    children.Add(k1);
                }
                element.Add(children);
            }
            XElement time = new XElement("newtime", elem.timeStamp);        //provides the timestamp 
            element.Add(time);
            XElement payload = new XElement("newpayload", elem.payload);
            element.Add(payload);
            return element;
        }
    }
        // Test stub for the PersistenceEngine
#if (TEST_PERSISTENCEENGINE)

    class Test_PersistenceEngine
    {         // Main method
        static void Main(string[] args)
        {
            "Testing persistence Package".title('=');
            WriteLine();
            PersistenceEngine persiseng = new PersistenceEngine();
            // creating new databse and new database elements
            DBEngine<int, DBElement<int, string>> pdb = new DBEngine<int, DBElement<int, string>>();

            DBElement<int, string> pel1 = new DBElement<int, string>("first element", "SMA");
            DBElement<int, string> pel2 = new DBElement<int, string>("second element ", "OOD");
            DBElement<int, string> pel3 = new DBElement<int, string>("third element", "DP");

            pel1.payload = "first payload";
            pel1.children.AddRange(new List<int> { 111, 112, 113 });

            pel2.payload = "second payload";
            pel2.children.AddRange(new List<int> { 221, 222, 223 });

            pel3.payload = "third payload";
            pel3.children.AddRange(new List<int> { 331, 332, 333 });

            // inserting elements to database
            pdb.insert(1, pel1);
            pdb.insert(2, pel2);
            pdb.insert(3, pel3);
            pdb.showDB();
            WriteLine();
            persiseng.persistdb<int, DBElement<int, string>, string>(pdb);


        }
    }

#endif
}