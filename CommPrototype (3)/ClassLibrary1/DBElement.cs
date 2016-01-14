///////////////////////////////////////////////////////////////
// DBElement.cs - Define element for noSQL database          //
// Ver 1.3                                                  //
// Application: Demonstration for CSE681-SMA, Project#2      //
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
 * This package implements the DBElement<Key, Data> type, used by 
 * DBEngine<key, Value> where Value is DBElement<Key, Data>.
 *
 * The DBElement<Key, Data> state consists of metadata and an
 * instance of the Data type.
 *
 * This DBElement type is used by both:
 *
 *   ItemFactory - used to ensure that all db elements have the
 *                 same structure even if built by different
 *                 software parts.
 *   ItemEditor  - used to ensure that db elements are edited
 *                 correctly and maintain the intended structure.
 *   
 */
 /*
 *Public Interface:
 *-----------------
 * DBElement()
 * - used to initialise the database elements
 * Main()
 *-main method of the package
 */
/*
 * Maintenance:
 * ------------
 * Required Files: DBElement.cs, UtilityExtensions.cs
 *
 * Build Process:  devenv Project2Starter.sln /Rebuild debug
 *                 Run from Developer Command Prompt
 *                 To find: search for developer
 *
 * Maintenance History:
 * --------------------
 * ver 1.3 : 23 Nov 15
 * -  Namespace changed
 * ver 1.2 : 09 Oct 15
 * _  added comments
 * ver 1.1 : 24 Sep 15
 * - removed extension methods, removed tests from test stub
 * - Testing now  uses DBEngineTest.cs
 * ver 1.0 : 13 Sep 15
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
    ////////////////////////////////////////////////////////////////////
    // DBElement<Key, Data> class
    // - Instances of this class are the "values" in our key/value 
    //   noSQL database.
    // - Key and Data are unspecified classes, to be supplied by the
    //   application that uses the noSQL database.
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Data"></typeparam>
    //////////////////////////////////////////////////////////////////////

    public class DBElement<Key, Data>
    {
        // Creation of metadata
        public string name { get; set; }          // metadata    |
        public string descr { get; set; }         // metadata    |
        public DateTime timeStamp { get; set; }   // metadata   value
        public List<Key> children { get; set; }   // metadata    |
        public Data payload { get; set; }         // data        |

        public DBElement(string Name = "unnamed", string Descr = "undescribed")
        {
            name = Name;
            descr = Descr;
            timeStamp = DateTime.Now;
            children = new List<Key>();
        }
    }

#if (TEST_DBELEMENT)


    class TestDBElement
    {            // Main Method for DBElement package
        static void Main(string[] args)
        {
            "Testing DBElement Package".title('=');
            WriteLine();

            Write("\n  All testing of DBElement class moved to DBElementTest package.");
            Write("\n  This allow use of DBExtensions package without circular dependencies.");

            Write("\n\n");
        }
    }
#endif
}
