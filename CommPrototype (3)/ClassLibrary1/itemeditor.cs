///////////////////////////////////////////////////////////////
// ItemEditor.cs - DB elements are edited correctly          //
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
 * This is used to ensure that db elements are edited
 * correctly and maintain the intended structure
 */
/* Maintenance:
* ------------
* Required Files: DBEngine.cs, DBElement.cs,Display.cs and
*                 UtilityExtensions.cs 
*
* Build Process:  devenv Project2Starter.sln /Rebuild debug
*                 Run from Developer Command Prompt
*                 To find: search for developer
*
*Public Interface
*------------------
*addRelations()
*-used to add relationship 
* removeRelation()
*-used to remove relationship
*editName()
*-used to edit names of metadata
*editDescr()
*-used to edit description of metadata
*editInstance()
*-used to edit the instances
*Main()
*-used to test the ItemEditor
*
* Maintenance History:
* --------------------
* ver 1.1: 23 Nov 15
* -Namespace changed
*ver 1.0: 09 Oct 15
* -First release
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Project4Code
{
    public static class ItemEditor
    {                  // Function to perform addition of relationships
        public static bool addRelations<Key, Value, Data>(this DBEngine<Key, Value> dbedit, Key key1, Key key2)
        {
            Value val1, val2;
            bool key1_present = dbedit.getValue(key1, out val1);
            // check if key1 is present
            if (key1_present)
            {
                bool key2_present = dbedit.getValue(key2, out val2);
                // check if key2 is present
                if (key2_present)
                {
                    DBElement<Key, Data> element = val1 as DBElement<Key, Data>;
                    element.children.Add(key1);
                }
            }
            return true;
        }
        // function to perform removal of relationships
        public static bool removeRelation<Key, Value, Data>(this DBEngine<Key, Value> dbedit, Key key1, Key key2)
        {
            Value value;
            if (dbedit.getValue(key1, out value))
            {
                DBElement<Key, Data> elem = value as DBElement<Key, Data>;
                if (elem.children.Contains(key2))
                    elem.children.Remove(key2);
            }
            return true;
        }

        // Function to perform editing of name in metedata
        public static bool editName<Key, Value, Data>(this DBEngine<Key, Value> dbedit, Key key1, string new_name)
        {
            Value value;
            if (dbedit.getValue(key1, out value))
            {
                DBElement<Key, Data> elem = value as DBElement<Key, Data>;
                elem.name = new_name;
                return true;
            }
            else return false;
        }
        // Function to perform editing of description of metadata
        public static bool editDescr<Key, Value, Data>(this DBEngine<Key, Value> dbedit, Key key1, string new_descr)
        {
            Value value;
            if (dbedit.getValue(key1, out value))
            {
                DBElement<Key, Data> elem = value as DBElement<Key, Data>;
                elem.descr = new_descr;
                return true;
            }
            else return false;
        }
        // function to edit instances 
        public static bool editInstance<Key, Value, Data>(this DBEngine<Key, Value> dbedit, Key key1, Data new_instance)
        {
            Value value;
            if (dbedit.getValue(key1, out value))
            {
                DBElement<Key, Data> elem = value as DBElement<Key, Data>;
                elem.payload = new_instance;
                return true;
            }
            else return false;
        }
    }

    // Test Stub to test the implementation of 
    // ItemEditor package
#if (TEST_ITEMEDITOR)
    class TestItemEditor
    {
        static void Main(string[] args)
        {
            "Testing ItemEditor Package".title('=');
            WriteLine();

            Write("\n --- Test DBElement<int,string> ---");

            // new database created to test the functionaity
            DBEngine<int, DBElement<int, string>> db = new DBEngine<int, DBElement<int, string>>();

            // new elements are created and inserted to the database
            DBElement<int, string> elem1 = new DBElement<int, string>("first element", "first element description");
            DBElement<int, string> elem2 = new DBElement<int, string>("second element", "second element description");
            DBElement<int, string> elem3 = new DBElement<int, string>("third element", "third element description");

            elem1.payload = "Payload of 1";
            elem1.children.AddRange(new List<int> { 1, 2, 3 });

            elem2.payload = "Payload of 2";
            elem2.children.AddRange(new List<int> { 11, 22, 33 });

            elem3.payload = "Payload of 3";
            elem3.children.AddRange(new List<int> { 111, 222, 333 });

            // inserting elements to database
            db.insert(1, elem1);
            db.insert(2, elem2);
            db.insert(3, elem3);
            db.showDB();

            // testing  addition of relationships to elements
            Write("\n\n  Testing of addition of new relationships");
            bool addr1 = db.addRelations<int, DBElement<int, string>, string>(1, 2);
            bool addr2 = db.addRelations<int, DBElement<int, string>, string>(2, 3);
            bool addr3 = db.addRelations<int, DBElement<int, string>, string>(3, 111);
            db.showDB();

            // testing  removing relationships to elements
            Write("\n\n  Testing of removal  of relations");
            bool remr1 = db.removeRelation<int, DBElement<int, string>, string>(2, 11);
            bool remr2 = db.removeRelation<int, DBElement<int, string>, string>(1, 3);

            db.showDB();
            if (remr1 && remr2) Write("\n \n Removal succeded");

            // Testing editing of text data
            Write("\n\n  Testing  of editing of text data");
            bool ed_name1 = db.editName<int, DBElement<int, string>, string>(1, "renaming of element 1");
            bool ed_descr = db.editDescr<int, DBElement<int, string>, string>(1, "editing description of element 1");
            bool ed_inst = db.editInstance<int, DBElement<int, string>, string>(1, "new instance for element 1");
            db.showDB();

            Write("\n\n --- Test DBElement<string,List<string>> ---");

            // creating elements to new database of type string,List of strings
            DBElement<string, List<string>> new_elem1 = new DBElement<string, List<string>>("new element 1", "Description of 1");
            DBElement<string, List<string>> new_elem2 = new DBElement<string, List<string>>("new element 2", "Description of 2");
            DBElement<string, List<string>> new_elem3 = new DBElement<string, List<string>>("new element 3", "Description of 3");
            //creating  new database
            DBEngine<string, DBElement<string, List<string>>> new_db = new DBEngine<string, DBElement<string, List<string>>>();

            new_elem1.payload = new List<string> { "First data in payload ", "Second data in payload", "Third data in payload" };
            new_elem1.children.AddRange(new List<string> { "one", "two", "three" });

            new_elem2.payload = new List<string> { "DP", "SMA", "OOD" };
            new_elem2.children.AddRange(new List<string> { "four", "five", "six" });

            new_elem3.payload = new List<string> { "CE", "CS", "EE" };
            new_elem3.children.AddRange(new List<string> { "seven", "eight", "nine" });

            // inserting elements to database
            new_db.insert("One", new_elem1);
            new_db.insert("Two", new_elem2);
            new_db.insert("Three", new_elem3);
            new_db.showEnumerableDB();

            // testing  addition of relationships to elements
            Write("\n\n  testing addition of relationship ");
            bool add1 = new_db.addRelations<string, DBElement<string, List<string>>, List<string>>("One", "two");
            bool add2 = new_db.addRelations<string, DBElement<string, List<string>>, List<string>>("Two", "three");
            bool add3 = new_db.addRelations<string, DBElement<string, List<string>>, List<string>>("Three", "one");

            new_db.showEnumerableDB();
            if (add1 && add2 && add3) Write("\n \n Adding relationship  successful.");

            // testing  removing relationships to elements
            Write("\n \n  Now going to test removing of relationships in DB-element: Element-One");
            bool rem1 = new_db.removeRelation<string, DBElement<string, List<string>>, List<string>>("One", "Nine");
            bool rem2 = new_db.removeRelation<string, DBElement<string, List<string>>, List<string>>("One", "two");

            new_db.showEnumerableDB();
            if (rem1 && rem2) Write("\n \n Deleting of relationships succeeded ");

            // Testing   editing of text data
            Write("\n \n  Now going to test edition of name, description and replacing instance of payload with new instance in Element-One.");
            new_db.editName<string, DBElement<string, List<string>>, List<string>>("One", "Edited name for Element-One");
            new_db.editDescr<string, DBElement<string, List<string>>, List<string>>("One", "New description for Element-One");
            new_db.editInstance<string, DBElement<string, List<string>>, List<string>>("One", new List<string> { "New payload - String One", "New payload - String Two", "New payload - String Three" });
            new_db.showEnumerableDB();
            WriteLine();
        }
    }
#endif
}
