using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtitestUSB
{
    class Connection
    {
        // "Data Source=C:\\Users\\Hanze\\Desktop\\DatosFatitest.db;Version=3;New=False;Compress=True;foreign keys=true;";
        //"Data Source=DatosFatitest.db;Version=3;New=False;Compress=True;foreign keys=true;Pragma foreign_keys=ON";
        //C:\\Users\\Hanze\\Desktop\\
        private String connectionString = "Data Source=DatosFatitest.db;Version=3;New=False;Compress=True;foreign keys=true;Pragma foreign_keys=ON";
        private SQLiteConnection conection;
        private static Connection _instance;
            
        public static Connection GetInstance()
        {

            return _instance ?? (_instance = new Connection());

        }

        public SQLiteConnection GetConnection()
        {
            return conection;
             
        }

        public String GetConnectionString()
        {
            return connectionString;

        }



        public void abrirConexion()
        {

            conection = new SQLiteConnection(connectionString);

        }



        public void cerrarConexion()
        {

            conection.Close();

        }

    }
}
