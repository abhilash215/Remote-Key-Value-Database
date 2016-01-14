///////////////////////////////////////////////////////////////
// QueryEngine.cs - Defines queries for noSQL database       //
// Ver 1.1                                                   //
// Application: Demonstration for CSE681-SMA, Project#4      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    HP EliteBook,Core-i5, Windows 10             //
// Author:      Abhilash Udayashankar,SUID 778388557         //
//               (774) 540-1234 ,audayash@syr.edu            //
///////////////////////////////////////////////////////////////
/*
/* Package Operations:
* -------------------
* This package defines the queries to meet requirement 7
*
*/
/*
*Public interface
*-----------------
*queryvalue()
*-function used to query the value for key
*querychildren()
*-function used to query the children of key
*Main()
*-used to test the QueryEngine package
*
*
/* Maintenance:
* ------------
* Required Files: DBEngine.cs, DBElement.cs,Display.cs,DBExtensions.cs,ItemEditor.cs
*                 PersistenceEngine.cs,QueryEngine.cs,UtilityExtensions.cs 
*
* Build Process:  devenv Project2Starter.sln /Rebuild debug
*                 Run from Developer Command Prompt
*                 To find: search for developer
*
* Maintenance History:
* --------------------
* ver 1.1: : 23 Nov 15
 * -  Namespace changed
*ver 1.0:09 Oct 15
* -First release
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4Code
{
    public class QueryEngine
    {
        public Value queryvalue<Key, Value, Data>(DBEngine<Key, Value> db, Key key)
        {
            Value getqueryvalue;
            bool key_present = db.getValue(key, out getqueryvalue);
            if (key_present)                                 // check if key is present
                return getqueryvalue;                        //if present return value
            else
                Console.WriteLine("key not present");        // else error message
            return default(Value);
        }

        public List<key> querychildren<key, value, Data>(DBEngine<key, value> db, key Key)
        {
            value getqueryvalue;
            bool key_present = db.getValue(Key, out getqueryvalue);             // check if key present
            DBElement<key, Data> temp = getqueryvalue as DBElement<key, Data>;  // create new element to store value
            if (key_present)
                return temp.children;
            else
                Console.WriteLine("invalid key");                               // if key not present error message
            return null;
        }
    }
}

        // test stub to test the package Queryengine

#if (TEST_QUERYENGINE)
        class Test_QueryEngine    {
        public static void Main()        {
          "Testing QueryEngine Package".title('=');
            Console.WriteLine();
            QueryEngine queryv = new QueryEngine();
            DBEngine<int, DBElement<int, string>> newdbq = new DBEngine<int, DBElement<int, string>>();
            DBEngine<string, DBElement<string, List<string>>> newdbqs = new DBEngine<string, DBElement<string, List<string>>>();
            DBElement<int, string> newqelem1 = new DBElement<int, string>();
            DBElement<int, string> newqelem2 = new DBElement<int, string>();
            DBElement<int, string> newqelem3 = new DBElement<int, string>();
            newqelem1.name = "query elemnt 1";
            newqelem1.descr = "testing qurey get value for 1";
            newqelem1.payload = "new query element payload 1";
            newqelem1.children.AddRange(new List<int> { 1, 22, 333 });
            newqelem2.name = "query elemnt 2";
            newqelem2.descr = "testing query get value for 2";
            newqelem2.payload = "new query element payload 2";
            newqelem2.children.AddRange(new List<int> { 010, 020, 030 });
            newqelem3.name = "query elemnt 3";
            newqelem3.descr = "testing quey get value for 3";
            newqelem3.payload = "new query element payload  3";
            newqelem3.children.AddRange(new List<int> { 777, 222, 333 });
            newdbq.insert(1, newqelem1);
            newdbq.insert(2, newqelem2);
            newdbq.insert(3, newqelem3);
            newdbq.showDB();
            DBElement<int, string> displayres = new DBElement<int, string>();
            Console.WriteLine("\n \n getting value for key 2");
            displayres = queryv.queryvalue<int, DBElement<int, string>, string>(newdbq, 2);
            if (displayres != null)
                displayres.showElement();
           Console.WriteLine("\n \n getting children for key 3");
            List<int> childlist = new List<int>();
            childlist = queryv.querychildren<int, DBElement<int, string>, string>(newdbq, 3);
            if (childlist != null)            {
                foreach (int i in childlist)
                    Console.WriteLine(i);            }
            DBElement<string, List<string>> newelem1q = new DBElement<string, List<string>>(" element 1", "1 is first");
            DBElement<string, List<string>> newelem2q = new DBElement<string, List<string>>(" element 2", "2 is second");
            DBElement<string, List<string>> newelem3q = new DBElement<string, List<string>>(" element 3", " 3 is third");
            newelem1q.children.AddRange(new List<string> { " one", "one", "one" });
            newelem1q.payload = new List<string> { "h", "e", "l", "l", "o" };
            newelem2q.children.AddRange(new List<string> { " two", "two", "two" });
            newelem2q.payload = new List<string> { "h", "d", "w", "f", "b" };
           newelem3q.children.AddRange(new List<string> { " three", "three", "three" });
            newelem3q.payload = new List<string> { "a", "e", "i", "o", "u" };
           newdbqs.insert("one", newelem1q);
            newdbqs.insert("two", newelem2q);
            newdbqs.insert("three", newelem3q);
            newdbqs.showEnumerableDB();
           DBElement<string, List<string>> valuestring = new DBElement<string, List<string>>();
            Console.WriteLine("\n \n getting value for key 2");
            valuestring = queryv.queryvalue<string, DBElement<string, List<string>>, string>(newdbqs, "two");
            if (valuestring != null)
                valuestring.showEnumerableElement();
            DBElement<string, List<string>> valuestrings = new DBElement<string, List<string>>();
            Console.WriteLine("\n \n getting children for key five");
            List<string> childlists = new List<string>();
            childlists = queryv.querychildren<string, DBElement<string, List<string>>, string>(newdbqs, "five");
            if (childlists != null)            {
                foreach (string cstring in childlists)
                    Console.WriteLine(cstring);            }        }    }}
    #endif

