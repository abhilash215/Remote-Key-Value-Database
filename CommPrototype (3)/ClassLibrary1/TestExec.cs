///////////////////////////////////////////////////////////////
// TestExec.cs - Demonstrates all the functional requirements//
// Ver 1.3                                                   //
// Application: Demonstration for CSE681-SMA, Project#4      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    HP EliteBook,Core-i5, Windows 10             //
// Author:      Abhilash Udayashankar,SUID 778388557         //
//               (774) 540-1234 ,audayash@syr.edu            //
//Original Author:Jim Fawcett, CST 4-187, Syracuse University//
//              (315) 443-3948, jfawcett@twcny.rr.com        //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package begins the demonstration of meeting requirements.
 */
/*
*Public Interface
*------------------
*Main()
*-used to demonstrate the requirements 
/*
 * Maintenance:
 * ------------
 * Required Files: 
 *   TestExec.cs,  DBElement.cs, DBEngine.cs, Display.cs,ItemEditor.cs 
 *   DBExtensions.cs,QueryEngine.cs,Persistenceengine.cs and UtilityExtensions.cs
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver  1.3:: 23 Nov 15
 * -  Namespace changed
 * ver 1.2 : 09 Oct  15
 * ver 1.1 : 24 Sep 15
 * ver 1.0 : 18 Sep 15
 * - first release
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Project4Code
{
    class TestExec
    {
        //Defining new databases for demonstrating requirements
        public  DBEngine<int, DBElement<int, string>> db = new DBEngine<int, DBElement<int, string>>();
        private DBEngine<string, DBElement<string, List<string>>> db2 = new DBEngine<string, DBElement<string, List<string>>>();
        DBEngine<int, DBElement<int, string>> db3 = new DBEngine<int, DBElement<int, string>>();

        void TestR2()
        {
            "Demonstrating Requirement #2".title();
            //creating new element of type int and string
            DBElement<int, string> element = new DBElement<int, string>();
            element.name = "first element of db";
            element.descr = "int and string type";
            element.timeStamp = DateTime.Now;
            element.children.AddRange(new List<int> { 0, 2, 4, 8 });
            element.payload = "first element's payload";
            element.showElement();
            db.insert(1, element);
            db.showDB();
            WriteLine();
            //creating new element of type string and list of strings
            DBElement<string, List<string>> element2 = new DBElement<string, List<string>>();
            element2.name = "second element of db";
            element2.descr = "strings and strings of string types";
            element2.timeStamp = DateTime.Now;
            element2.children.AddRange(new List<string> { "SMA", "OOD", "Project2" });
            element2.payload = new List<string> { "second", "SMA", "project" };
            element2.showEnumerableElement();
            db2.insert("2", element2);
            db2.showEnumerableDB();
            WriteLine();

        }
        void TestR3()
        {
            "Demonstrating Requirement #3".title();
            //demonstrating addition and removal of key/value pairs pairs
            WriteLine("\n Database contents before addition");
            db.showDB();
            WriteLine();
            WriteLine(" \n Database contents after addition-----");
            DBElement<int, string> insert1 = new DBElement<int, string>();
            insert1.name = "first inserted element";
            insert1.descr = "int and string values database";
            insert1.timeStamp = DateTime.Now;
            insert1.children.AddRange(new List<int> { 7, 7, 8, 3, 8, 8, 5, 5, 7 });
            insert1.payload = "first inserted elements payload";
            db.insert(2, insert1);

            DBElement<int, string> insert2 = new DBElement<int, string>();
            insert2.name = "second inserted element";
            insert2.descr = "int and string values";
            insert2.timeStamp = DateTime.Now;
            insert2.children.AddRange(new List<int> { 1, 3, 5, 7 });
            insert2.payload = "second inserted elements payload";
            db.insert(3, insert2);
            db.showDB();
            WriteLine();
            WriteLine(" \n Database before contents are removed");
            db.showDB();
            WriteLine("\n \n second element to be removed");
            db.remove(2);
            db.showDB();
            WriteLine();
        }
        void TestR4()
        {
            "Demonstrating Requirement #4".title();
            WriteLine("\n Database before addition ");
            db.showDB();
            //Demonstrating addition of relationships
            bool ar1 = db.addRelations<int, DBElement<int, string>, string>(1, 3);
            bool ar2 = db.addRelations<int, DBElement<int, string>, string>(3, 0);

            WriteLine("\n \n addition of relationships  ");
            db.showDB();
            if (ar1 && ar2)
                WriteLine(" \n addition  Succesfull ");
            
            //Demonstrating removal of relationships
            WriteLine(" \n DataBase before removing relationships");
            db.showDB();
            bool remove1 = db.removeRelation<int, DBElement<int, string>, string>(1, 2);
            bool remove2 = db.removeRelation<int, DBElement<int, string>, string>(3, 1);
            WriteLine();
            WriteLine("\n DataBase contents after removal");
            db.showDB();
            if (remove1 && remove2)
                WriteLine("\n Removal Successful");
                      

            //Demonstrating editing of metadata text
            WriteLine("\n DataBase before editing  metadata text ");
            db2.showEnumerableDB();

            bool editn1 = db2.editName<string, DBElement<string, List<string>>, List<string>>("2", "Edited name for second element");
            bool editn2 = db2.editDescr<string, DBElement<string, List<string>>, List<string>>("2", "New description for second element");
            WriteLine("\n \n DataBase after editing");
            db2.showEnumerableDB();
        }
        void TestR5()
        {
            // persisting database contents to an XML file
            "\n Demonstrating Requirement #5".title();
            WriteLine();

            PersistenceEngine persisengtest = new PersistenceEngine();
            persisengtest.persistdb<int, DBElement<int, string>, string>(db3);
            WriteLine();
            db.showDB();
            WriteLine("\n \n XML file created"); // XML file created

        }
        void TestR6()
        {
            "Demonstrating Requirement #6".title();
            WriteLine();
        }
        void TestR7()
        {
            //Demonstrating queries
            "Demonstrating Requirement #7".title();
            WriteLine();
            QueryEngine queryv = new QueryEngine();
            DBElement<int, string> displayresc = new DBElement<int, string>();
            DBElement<int, string> displayres = new DBElement<int, string>();

            //demonstrating query 1
            Console.WriteLine("\n \n getting value for key 1");
            displayres = queryv.queryvalue<int, DBElement<int, string>, string>(db, 1);
            if (displayres != null)
                displayres.showElement();

            DBElement<string, List<string>> valuestring = new DBElement<string, List<string>>();
            Console.WriteLine("\n \n getting value for key 2");
            valuestring = queryv.queryvalue<string, DBElement<string, List<string>>, string>(db2, "2");
            if (valuestring != null)
                valuestring.showEnumerableElement();

            // demonstrating query 2
            Console.WriteLine("\n \n getting children for key 1");
            List<int> tchildlist = new List<int>();
            tchildlist = queryv.querychildren<int, DBElement<int, string>, string>(db, 1);
            if (tchildlist != null)
            {
                foreach (int i in tchildlist)
                    Console.WriteLine(i);
            }


        }
        void TestR8()
        {
            "Demonstrating Requirement #8".title();
            WriteLine();
        }
        static void Main(string[] args)
        {
            TestExec exec = new TestExec();
            "Demonstrating Project#2 Requirements".title('=');
            WriteLine();
            exec.TestR2();
            exec.TestR3();
            exec.TestR4();
            exec.TestR5();
            exec.TestR6();
            exec.TestR7();
            exec.TestR8();
            Write("\n\n");
            Console.ReadKey();
        }
    }
}
