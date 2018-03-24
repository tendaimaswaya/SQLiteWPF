using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteWPF
{
    class SetupSQLite
    {
        public static SQLiteConnection sqliteconn = new SQLiteConnection("Data Source = " + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\MySQLiteDB\" + "MyDB.db" + "; version=3; new=False; datetimeformat=CurrentCulture"));

        public static bool isFirstTime;

        public static string DB_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\MySQLiteDB\" + "MyDB.sqlite");//DataBase Name 

        #region Constructor
        public SetupSQLite()
        {
            if (!checkExists("MyDB.sqlite").Result)
            {       //check for the existence of the database
                using (var db = new SQLiteConnection(DB_PATH))
                {
                    CreateSQLiteDatabase();
                    isFirstTime = true;     //using this to avoid calling the create command in the Main Window if the database already exists
                }
            }
            else
            {
                isFirstTime = false;
            }
        }
        #endregion
        public async Task<bool> checkExists(string filename)
        {
            return System.IO.File.Exists(DBDirectory() + @"\MySQLiteDB\" + filename + "");
        }
        public static string DBDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);       //fetch App Data Local directory
        }
        public void CreateSQLiteDatabase()
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MySQLiteDB")); //creating directory name MySQLiteDB in the App Data Local directory where we will store our SQLite files
            SQLiteConnection.CreateFile(Path.Combine(DBDirectory() + @"\MySQLiteDB\MyDB.sqlite"));
            sqliteconn.Open();

            var command = sqliteconn.CreateCommand();       //create command using the SQLiteConnection      
            command.CommandText = "CREATE TABLE IF NOT EXISTS my_tbl(id INTEGER PRIMARY KEY, date DATETIME NOT NULL, name VARCHAR(50), age int, amount REAL, comments TEXT)";
            command.ExecuteNonQuery();      //execute the create command


            // command.CommandText = "CREATE TABLE IF NOT EXISTS my_second_tbl()";
            //  command.ExecuteNonQuery(); 
            //this is how you would go on to create another table executing non queries one after the other


        }
    }
}
