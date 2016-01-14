///////////////////////////////////////////////////////////////
// Processor.cs - All remoteDB operations and queries        //
// ver 1.0                                                   //
// CSE681 - Software Modeling and Analysis, Project #4       //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    HP EliteBook,Core-i5, Windows 10             //
// Author:      Abhilash Udayashankar,SUID 778388557         //
//               (774) 540-1234 ,audayash@syr.edu            //
///////////////////////////////////////////////////////////////
/*
*  Package Operations
* --------------------
* -This package contains all the methods 
*   to handle all the queries and database operations.
*/
/*
* Maintainence
* -----------------
*  Required Files:WriteClient.cs,ReadClient.cs,
*                 CommService.cs,databaselib
*                 
*  Build Process:devenv Project4code.sln /Rebuild debug
*                 Run from Developer Command Prompt
*                 To find: search for developer
*/
/*
* Public Interface
* -----------------
*Main(): used to test the processor.cs
*editName()
*-used to edit names of metadata
*editDescr()
*-used to edit description of metadata
* insert()
* -used to insert the element to db
* delete()
*- used to delete the element from db
*
*/
/*
 * Maintenance History:
 * --------------------
 * ver 1.0: 20 Nov 2015
 * - first release
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Project4Code
{
    public class processor
    {
            // function to insert into database
        public XElement insert(XElement dbe, DBEngine<int, DBElement<int, string>> db)
        {
            DBElement<int, string> elem = new DBElement<int, string>();
            Console.WriteLine("\n");
            Console.WriteLine("\n----------Insert operation----------");
            elem.name = dbe.Element("name").Value;
            elem.descr = dbe.Element("descr").Value;
            elem.payload = dbe.Element("payload").Value;
            List<int> childrenlist = new List<int>();
            XElement db1 = dbe.Element("children");
            foreach (var v in db1.Elements("dbkey")) childrenlist.Add(Int32.Parse(v.Value));
            elem.children = childrenlist;
            bool result = db.insert(Int32.Parse((dbe.Element("key").Value)), elem);
            db.showDB();
            if (result == true)
            {
                XElement s = new XElement("result", "\n element inserted successfully ");
                return s;
            }
            else
            {
                XElement f = new XElement("result", "Failure");
                return f;
            }
        }

        // function to delete from database
        public XElement Delete(XElement dbe, DBEngine<int, DBElement<int, string>> db)
        {
            DBElement<int, string> elem = new DBElement<int, string>();
            Console.WriteLine("\n----------Delete Operation----------");
            Console.Write("\n Now deleting the element with key=3");
            bool result = db.remove(Int32.Parse((dbe.Element("key").Value)));
            db.showDB();
            if (result == true)
            {
                XElement s = new XElement("result", "\n Element deleted successfully ");
                return s;
            }
            else
            {
                XElement f = new XElement("result", "Failure");
                return f;
            }

        }

        // function to edit name
        public XElement EditName(XElement dbe, DBEngine<int, DBElement<int, string>> db)
        {
            Console.Write("\n----------Edit Operation----------");
            Console.Write("\nediting the name of key==5");
            Console.WriteLine("\n name :Dogs is changed to Cats");
            bool result = db.editName<int, DBElement<int, string>, string>(Int32.Parse((dbe.Element("key").Value)), "Cats");
            db.showDB();
            Console.WriteLine("\n");
            if (result == true)
            {
                XElement t = new XElement("result", "\n  element edited successfully ");
                return t;
            }
            else
            {
                XElement f = new XElement("result", " failure ");
                return f;
            }
        }

        //function to edit description
        public XElement editdescr(XElement dbe, DBEngine<int, DBElement<int, string>> db)
        {
            Console.Write("\n ----------Edit description Operation----------");
            Console.Write("\nediting the description  of key= 4");
            Console.WriteLine("\n");
            Console.WriteLine("\n  Bachelors changed to BE ");
            bool result = db.editDescr<int, DBElement<int, string>, string>(Int32.Parse((dbe.Element("key").Value)), " BE ");
            db.showDB();
            if (result == true)
            {
                XElement t = new XElement("result", "\n  element edited successfully ");
                return t;
            }
            else
            {
                XElement f = new XElement("result", " Editing Failure ");
                return f;
            }
        }

        // function to persist the db contents
        public XElement persistdb(XElement dbe, DBEngine<int, DBElement<int, string>> db)
        {
            PersistenceEngine persisengtest = new PersistenceEngine();
            persisengtest.persistdb<int, DBElement<int, string>, string>(db);
            XElement t = new XElement("result", " xml saved successfully ");
            return t;
        }

        //function to getvalue
        public XElement getvalue(XElement dbe, DBEngine<int, DBElement<int, string>> db, QueryEngine QE)
        {
            Console.WriteLine("\n value of the  particular key is returned ");
            DBElement<int, string> dbelem = new DBElement<int, string>();
            QE.queryvalue<int, DBElement<int, string>, string>(db, 2);
            if (QE.Equals(null))
            {
                XElement f = new XElement("result", "\n no key present ");
                return f;
            }
            else
            {
                XElement s = new XElement("result", "\n Value obtained ");
                return s;
            }
        }


        // function to getchildren
        public XElement getchildren(XElement dbe, DBEngine<int, DBElement<int, string>> db, QueryEngine QE)
        {
            Console.WriteLine("\n The children List is obtained ");
            QE.querychildren<int, DBElement<int, string>, string>(db, 1);
            if (QE.Equals(null))
            {
                XElement f = new XElement("result", " no children in list ");
                return f;
            }
            else
            {
                XElement s = new XElement("result", " children obtained from the specified key ");
                return s;
            }
        }


#if (TEST_PROCESSOR)

        // test stub for processor
        // main function
        static void Main(string[] args)
        {
            DBEngine<int, DBElement<int, string>> db = new DBEngine<int, DBElement<int, string>>();
            "Testing the Package".title('=');
            Console.WriteLine();
            Console.WriteLine("\n remote operations are done are and tested");
            Console.Write("\n");
            DBElement<int, string> elem = new DBElement<int, string>();
            Console.WriteLine("\n----------Delete Operation----------");
            Console.Write("\n Now deleting the element with key=3");
            bool result = db.remove(2);
            db.showDB();
            if (result == true)
            {
                XElement s = new XElement("result", "\n Element deleted successfully ");
            }
            else
            {
                XElement f = new XElement("result", "Failure");
            }
        }

#endif

    }
}

