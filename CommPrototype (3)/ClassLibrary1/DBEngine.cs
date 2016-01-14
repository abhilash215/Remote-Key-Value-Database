///////////////////////////////////////////////////////////////
// DBElement.cs - Define  noSQL database                     //
// Ver 1.4                                                   //
// Application: Demonstration for CSE681-SMA, Project#2      //
// Language:    C#, ver 6.0, Visual Studio 2015              //
// Platform:    HP EliteBook,Core-i5, Windows 10             //
// Author:      Abhilash Udayashankar,SUID 778388557         //
//              (774) 540-1234 ,audayash@syr.edu             // 
//Original Author:Jim Fawcett, CST 4-187, Syracuse University//
//              (315) 443-3948, jfawcett@twcny.rr.com        //
///////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 * This package implements DBEngine<Key, Value> where Value
 * is the DBElement<key, Data> type.
 */
/*
*Public Interface
*-----------------
*DBEngine()
*-stores the key and value of elements
*Insert()
*-used to insert values into elements
*Remove()
*-used to remove the value of keys
*containsKey()
*-used to check if key is present in the database
*Main()
*-Main method for the package
/*
* Maintenance:
* ------------
* Required Files: DBEngine.cs, DBElement.cs, and
*                 UtilityExtensions.cs
*
* Build Process:  devenv Project2Starter.sln /Rebuild debug
*                 Run from Developer Command Prompt
*                 To find: search for developer
*
* Maintenance History:
* --------------------
* ver 1.4: : 23 Nov 15
 * -  Namespace changed
* ver 1.3 :09  Oct 15
* -  added remove function to delete key/value pairs
* - comments are added 
* ver 1.2 : 24 Sep 15
* - removed extensions methods and tests in test stub
* - testing is now done in DBEngineTest.cs to avoid circular references
* ver 1.1 : 15 Sep 15
* - fixed a casting bug in one of the extension methods
* ver 1.0 : 08 Sep 15
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
    public class DBEngine<Key, Value>
    {
        private Dictionary<Key, Value> dbStore;
        public DBEngine()
        {
            dbStore = new Dictionary<Key, Value>();
        }
        //Function to insert values
        public bool insert(Key key, Value val)
        {
            if (dbStore.Keys.Contains(key))
                return false;
            dbStore[key] = val;
            return true;
        }
        // Function to get value for key
        public Value getValueForKey(Key key)
        {
            if (containsKey(key))
            {
                return dbStore[key];
            }
            return default(Value);
        }
        // function to output value when key is given
        public bool getValue(Key key, out Value val)
        {
            if (dbStore.Keys.Contains(key))
            {
                val = dbStore[key];
                return true;
            }
            val = default(Value);
            return false;
        }

        public IEnumerable<Key> Keys()
        {
            return dbStore.Keys;
       }

        // Function to remove a key/value pair
        public bool remove(Key key)
        {
            if (!dbStore.Keys.Contains(key))
                return false;
            dbStore.Remove(key);
            return true;
        }
        // Function to check if keys are present in the database
        public bool containsKey(Key key)
        {
            return dbStore.Keys.Contains(key);
        }
        public int Count()
        {
            return dbStore.Keys.Count;
        }
    }
#if (TEST_DBENGINE)

    class TestDBEngine
    {
        // Main method for DBEngine package
        static void Main(string[] args)
        {
            "Testing DBEngine Package".title('=');
            WriteLine();

            Write("\n  All testing of DBEngine class moved to DBEngineTest package.");
            Write("\n  This allow use of DBExtensions package without circular dependencies.");

            Write("\n\n");
        }
    }
#endif
}
