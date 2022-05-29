using FTD2XX_NET;
using System;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FtitestUSB
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //  private SQLiteConnection connectionSQLite = null;
        private OleDbConnection connectionAccess = null;
        private String pathMdb = null;
        int cantP = 0;
        private int HeightF;
        private int WidthF;

  
        public Form1()
        {

            Thread t = new Thread(new ThreadStart(Loading));
            t.Start();
            Thread.Sleep(1000);


            InitializeComponent();
            this.Refresh();
            t.Abort();
            this.BringToFront();


            Connection.GetInstance().abrirConexion();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            metroTileAtletas.TabStop = false;
            metroTilePruebas.TabStop = false;
            metroTileReportes.TabStop = false;
            metroTileVisualizar.TabStop = false;

        }

        
     

        void Loading()
        {
            try
            {
                System.Windows.Forms.Application.Run(new Loading());
            }
            catch (Exception)
            {

                throw;
            }


        }







        private async Task ImportarDatosAsync()
        {

            //  connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=C:\\Users\\Hanze\\Desktop\\DATOSFATIGTEST.mdb");
            connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            await Connection.GetInstance().GetConnection().OpenAsync();
            await connectionAccess.OpenAsync();

            //  Connection.GetInstance().GetConnection().Open();
            // connectionAccess.Open();


            OleDbCommand conAcce = new OleDbCommand("select * from DatosSujetos", connectionAccess);

            DbDataReader redear = await conAcce.ExecuteReaderAsync();

            //            DbDataReader redear = conAcce.ExecuteReader();

            while (await redear.ReadAsync())
            {

                String carnet = redear["Carnet de Identidad"].ToString();

                SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where CarnetIdentidad  =" + carnet, Connection.GetInstance().GetConnection());

                DbDataReader redear2 = await con2.ExecuteReaderAsync();
                // DbDataReader redear2 = con2.ExecuteReader();

                if (!redear2.HasRows)
                {


                    /*    String nombre = redear["Nombre"].ToString();
                        String apellido = redear["Primer Apellido"].ToString();
                        String apellido2 = redear["Segundo Apellido"].ToString();
                        String sexo = redear["Sexo"].ToString();
                        String edad = redear["Edad"].ToString();
                        String deporte = redear["Deporte"].ToString();
                        String nivel = redear["Nivel Escolar"].ToString();
                        String modalidad = redear["Modalidad"].ToString();
                        */

                    String nombre = redear["Nombre"].ToString();
                    String apellido = redear["Primer Apellido"].ToString();
                    String apellido2 = redear["Segundo Apellido"].ToString();
                    String sexo = redear["Sexo"].ToString();
                    String edad = redear["Edad"].ToString();
                    String deporte = redear["Ocupacion"].ToString();
                    String nivel = redear["Nivel Escolar"].ToString();
                    String modalidad = redear["División"].ToString();

                    String Entidad = redear["Entidad"].ToString();

                    String insert = "Insert into DatosSujetos (CarnetIdentidad, Nombre,PrimerApellido,SegundoApellido,Sexo,Edad,Deporte,NivelEscolar,Modalidad,Entidad)" +
                        " values('" + carnet + "', '" + nombre + "','" + apellido + "','" + apellido2 + "','" + sexo + "','" + edad + "','" + deporte + "','" + nivel + "','" + modalidad + "','" + Entidad + "')";

                    SQLiteCommand con3 = new SQLiteCommand(insert, Connection.GetInstance().GetConnection());
                    // con3.ExecuteNonQueryAsync();

                    con3.ExecuteNonQuery();


                }



                OleDbCommand conAcce2 = new OleDbCommand("select * from PRUEBAFATIGTEST where Ncarnet = '" + carnet + "'", connectionAccess);
                DbDataReader dataReader = await conAcce2.ExecuteReaderAsync();



                if (dataReader.HasRows)
                {

                    while (await dataReader.ReadAsync())
                    {

                        String fecha = dataReader["FECHA"].ToString();
                        String Hora = dataReader["HORA"].ToString();

                        DateTime fechaTemp = Convert.ToDateTime(fecha);
                        DateTime horaTemp = Convert.ToDateTime(Hora);




                        fecha = fechaTemp.ToString("yyyy-MM-dd");

                        //  fecha = fechaTemp.Day.ToString() + "/" + fechaTemp.Day.ToString() + "/" + fechaTemp.Year.ToString();

                        Hora = horaTemp.Hour.ToString() + ":" + horaTemp.Minute.ToString() + ":" + horaTemp.Second.ToString();


                        SQLiteCommand conAcceTemp = new SQLiteCommand("select * from Pruebas where Ncarnet = '" + carnet + "' and FECHA= '" + fecha
                            + "' and HORA = '" + Hora + "'", Connection.GetInstance().GetConnection());

                        DbDataReader dataReaderTemp = await conAcceTemp.ExecuteReaderAsync();


                        if (!dataReaderTemp.HasRows)
                        {

                            String frecuencia = dataReader["TFRECUENCIA"].ToString();
                            String mediciones = dataReader["KMEDICIONES"].ToString();
                            String media = dataReader["MEDIA"].ToString();
                            String sum = dataReader["SUMATORIA"].ToString();
                            String des = dataReader["DESVIACION"].ToString();
                            String porciento = dataReader["PORCIENTO5MEDIA"].ToString();
                            String diferencia = dataReader["DIFERENCIAPROMEDIO"].ToString();
                            String tiempo = dataReader["TIPOMEDICION"].ToString();
                            String M1 = dataReader["M1"].ToString();
                            String M2 = dataReader["M2"].ToString();
                            String M3 = dataReader["M3"].ToString();
                            String M4 = dataReader["M4"].ToString();
                            String M5 = dataReader["M5"].ToString();
                            String M6 = dataReader["M6"].ToString();
                            String M7 = dataReader["M7"].ToString();
                            String M8 = dataReader["M8"].ToString();
                            String M9 = dataReader["M9"].ToString();
                            String M10 = dataReader["M10"].ToString();
                            /*   String M11 = dataReader["M11"].ToString();
                               String M12 = dataReader["M12"].ToString();
                               String M13 = dataReader["M13"].ToString();
                               String M14 = dataReader["M14"].ToString();
                               String M15 = dataReader["M15"].ToString();
                               String M16 = dataReader["M16"].ToString();
                               String M17 = dataReader["M17"].ToString();
                               String M18 = dataReader["M18"].ToString();
                               String M19 = dataReader["M19"].ToString();
                               String M20 = dataReader["M20"].ToString();
                               */

                            /*     String insert2 = "Insert into Pruebas (Ncarnet,FECHA,HORA,TFRECUENCIA,KMEDICIONES,MEDIA,SUMATORIA,DESVIACION,PORCIENTO5MEDIA" +
                                     ",DIFERENCIAPROMEDIO,TIPOMEDICION,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,M11,M12,M13,M14,M15,M16,M17,M18,M19,M20)" +
                           " values('" + carnet + "','" + fecha + "','" + Hora + "','" + frecuencia + "','" + mediciones + "','" + media + "','" + sum + "','" + des + "'," +
                           "'" + porciento + "','" + diferencia + "','" + tiempo + "','" + M1 + "','" + M2 + "','" + M3 + "','" + M4 + "'," +
                           "'" + M5 + "','" + M6 + "','" + M7 + "','" + M8 + "','" + M9 + "','" + M10 + "','" + M11 + "','" + M12 + "','" + M13 + "','" + M14 + "'," +
                           "'" + M15 + "','" + M16 + "','" + M17 + "','" + M18 + "','" + M19 + "','" + M20 + "')";
                           */


                            String insert2 = "Insert into Pruebas (Ncarnet,FECHA,HORA,TFRECUENCIA,KMEDICIONES,MEDIA,SUMATORIA,DESVIACION,PORCIENTO5MEDIA" +
                               ",DIFERENCIAPROMEDIO,TIPOMEDICION,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10)" +
                     " values('" + carnet + "','" + fecha + "','" + Hora + "','" + frecuencia + "','" + mediciones + "','" + media + "','" + sum + "','" + des + "'," +
                     "'" + porciento + "','" + diferencia + "','" + tiempo + "','" + M1 + "','" + M2 + "','" + M3 + "','" + M4 + "'," +
                     "'" + M5 + "','" + M6 + "','" + M7 + "','" + M8 + "','" + M9 + "','" + M10 + "')";

                            SQLiteCommand con4 = new SQLiteCommand(insert2, Connection.GetInstance().GetConnection());

                            con4.ExecuteNonQueryAsync();

                        }

                    }
                }


            }

            ////////// Entidad Inserccion de entidad///////////////////////////

            /*
            OleDbCommand conAcce3 = new OleDbCommand("select * from Entidades", connectionAccess);
            DbDataReader dataReaderAcces = await conAcce3.ExecuteReaderAsync();

            if (dataReaderAcces.HasRows)
            {
                while (await dataReaderAcces.ReadAsync())
                {
                    String entidad = dataReaderAcces["Entidad"].ToString();
                    SQLiteCommand conSqli = new SQLiteCommand("select * from Entidades where Entidad= '" + entidad + "'", connectionSQLite);
                    DbDataReader dataReaderSqlite = await conSqli.ExecuteReaderAsync();

                    if (!dataReaderSqlite.HasRows)
                    {
                        String command = "Insert into Entidades (Entidad)values('" + entidad + "')";
                        SQLiteCommand conSqli2 = new SQLiteCommand(command, connectionSQLite);
                        await conSqli2.ExecuteNonQueryAsync();
                    }

                }

            }
            */



            //// Divisioines//////////////////////
            OleDbCommand conAcce5 = new OleDbCommand("select * from Divisiones", connectionAccess);
            DbDataReader dataReaderAcces4 = await conAcce5.ExecuteReaderAsync();


            if (dataReaderAcces4.HasRows)
            {
                while (await dataReaderAcces4.ReadAsync())
                {
                    String modalidad = dataReaderAcces4["División"].ToString();
                    SQLiteCommand conSqli4 = new SQLiteCommand("select * from Modalidad where Modalidad= '" + modalidad + "'", Connection.GetInstance().GetConnection());

                    DbDataReader dataReaderSqlite4 = await conSqli4.ExecuteReaderAsync();
                    //DbDataReader dataReaderSqlite4 = conSqli4.ExecuteReader();


                    if (!dataReaderSqlite4.HasRows)
                    {
                        String command = "Insert into Modalidad (Modalidad)values('" + modalidad + "')";
                        SQLiteCommand conSqli3 = new SQLiteCommand(command, Connection.GetInstance().GetConnection());
                        await conSqli3.ExecuteNonQueryAsync();
                        //conSqli3.ExecuteNonQuery();
                    }

                }

            }

            Connection.GetInstance().GetConnection().Close();
            connectionAccess.Close();

        }


        private /*async Task*/void ImportarDatos()
        {

            //  connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=C:\\Users\\Hanze\\Desktop\\DATOSFATIGTEST.mdb");
            connectionAccess = new OleDbConnection("Provider= Microsoft.Jet.OLEDB.4.0 ; Data Source=" + pathMdb);
            //    Connection.GetInstance().GetConnection().OpenAsync();
            //    await connectionAccess.OpenAsync();

            Connection.GetInstance().GetConnection().Open();
            connectionAccess.Open();


            OleDbCommand conAcce = new OleDbCommand("select * from DatosSujetos", connectionAccess);

            //    DbDataReader redear = await conAcce.ExecuteReaderAsync();

            DbDataReader redear = conAcce.ExecuteReader();

            while (/*await redear.ReadAsync()*/redear.Read())
            {

                String carnet = redear["Carnet de Identidad"].ToString();

                SQLiteCommand con2 = new SQLiteCommand("Select * from DatosSujetos where CarnetIdentidad  =" + carnet, Connection.GetInstance().GetConnection());

                //  DbDataReader redear2 = await con2.ExecuteReaderAsync();
                DbDataReader redear2 = con2.ExecuteReader();

                if (!redear2.HasRows)
                {


                    /*    String nombre = redear["Nombre"].ToString();
                        String apellido = redear["Primer Apellido"].ToString();
                        String apellido2 = redear["Segundo Apellido"].ToString();
                        String sexo = redear["Sexo"].ToString();
                        String edad = redear["Edad"].ToString();
                        String deporte = redear["Deporte"].ToString();
                        String nivel = redear["Nivel Escolar"].ToString();
                        String modalidad = redear["Modalidad"].ToString();
                        */

                    String nombre = redear["Nombre"].ToString();
                    String apellido = redear["Primer Apellido"].ToString();
                    String apellido2 = redear["Segundo Apellido"].ToString();
                    String sexo = redear["Sexo"].ToString();
                    String edad = redear["Edad"].ToString();
                    String deporte = redear["Ocupacion"].ToString();
                    String nivel = redear["Nivel Escolar"].ToString();
                    String modalidad = redear["División"].ToString();

                    String Entidad = redear["Entidad"].ToString();

                    String insert = "Insert into DatosSujetos (CarnetIdentidad, Nombre,PrimerApellido,SegundoApellido,Sexo,Edad,Deporte,NivelEscolar,Modalidad,Entidad)" +
                        " values('" + carnet + "', '" + nombre + "','" + apellido + "','" + apellido2 + "','" + sexo + "','" + edad + "','" + deporte + "','" + nivel + "','" + modalidad + "','" + Entidad + "')";

                    SQLiteCommand con3 = new SQLiteCommand(insert, Connection.GetInstance().GetConnection());
                    // con3.ExecuteNonQueryAsync();

                    con3.ExecuteNonQuery();


                }






                OleDbCommand conAcce2 = new OleDbCommand("select * from PRUEBAFATIGTEST where Ncarnet = '" + carnet + "'", connectionAccess);
                DbDataReader dataReader = conAcce2.ExecuteReader();


                if (dataReader.HasRows)
                {

                    while (/*await dataReader.ReadAsync()*/ dataReader.Read())
                    {

                        String fecha = dataReader["FECHA"].ToString();
                        String Hora = dataReader["HORA"].ToString();

                        DateTime fechaTemp = Convert.ToDateTime(fecha);
                        DateTime horaTemp = Convert.ToDateTime(Hora);




                        fecha = fechaTemp.ToString("yyyy-MM-dd");

                        //  fecha = fechaTemp.Day.ToString() + "/" + fechaTemp.Day.ToString() + "/" + fechaTemp.Year.ToString();

                        Hora = horaTemp.Hour.ToString() + ":" + horaTemp.Minute.ToString() + ":" + horaTemp.Second.ToString();


                        SQLiteCommand conAcceTemp = new SQLiteCommand("select * from Pruebas where Ncarnet = '" + carnet + "' and FECHA= '" + fecha
                            + "' and HORA = '" + Hora + "'", Connection.GetInstance().GetConnection());

                        //   DbDataReader dataReaderTemp = await conAcceTemp.ExecuteReaderAsync();
                        DbDataReader dataReaderTemp = conAcceTemp.ExecuteReader();

                        if (!dataReaderTemp.HasRows)
                        {

                            String frecuencia = dataReader["TFRECUENCIA"].ToString();
                            String mediciones = dataReader["KMEDICIONES"].ToString();
                            String media = dataReader["MEDIA"].ToString();
                            String sum = dataReader["SUMATORIA"].ToString();
                            String des = dataReader["DESVIACION"].ToString();
                            String porciento = dataReader["PORCIENTO5MEDIA"].ToString();
                            String diferencia = dataReader["DIFERENCIAPROMEDIO"].ToString();
                            String tiempo = dataReader["TIPOMEDICION"].ToString();
                            String M1 = dataReader["M1"].ToString();
                            String M2 = dataReader["M2"].ToString();
                            String M3 = dataReader["M3"].ToString();
                            String M4 = dataReader["M4"].ToString();
                            String M5 = dataReader["M5"].ToString();
                            String M6 = dataReader["M6"].ToString();
                            String M7 = dataReader["M7"].ToString();
                            String M8 = dataReader["M8"].ToString();
                            String M9 = dataReader["M9"].ToString();
                            String M10 = dataReader["M10"].ToString();
                            /*   String M11 = dataReader["M11"].ToString();
                               String M12 = dataReader["M12"].ToString();
                               String M13 = dataReader["M13"].ToString();
                               String M14 = dataReader["M14"].ToString();
                               String M15 = dataReader["M15"].ToString();
                               String M16 = dataReader["M16"].ToString();
                               String M17 = dataReader["M17"].ToString();
                               String M18 = dataReader["M18"].ToString();
                               String M19 = dataReader["M19"].ToString();
                               String M20 = dataReader["M20"].ToString();
                               */

                            /*     String insert2 = "Insert into Pruebas (Ncarnet,FECHA,HORA,TFRECUENCIA,KMEDICIONES,MEDIA,SUMATORIA,DESVIACION,PORCIENTO5MEDIA" +
                                     ",DIFERENCIAPROMEDIO,TIPOMEDICION,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,M11,M12,M13,M14,M15,M16,M17,M18,M19,M20)" +
                           " values('" + carnet + "','" + fecha + "','" + Hora + "','" + frecuencia + "','" + mediciones + "','" + media + "','" + sum + "','" + des + "'," +
                           "'" + porciento + "','" + diferencia + "','" + tiempo + "','" + M1 + "','" + M2 + "','" + M3 + "','" + M4 + "'," +
                           "'" + M5 + "','" + M6 + "','" + M7 + "','" + M8 + "','" + M9 + "','" + M10 + "','" + M11 + "','" + M12 + "','" + M13 + "','" + M14 + "'," +
                           "'" + M15 + "','" + M16 + "','" + M17 + "','" + M18 + "','" + M19 + "','" + M20 + "')";
                           */


                            String insert2 = "Insert into Pruebas (Ncarnet,FECHA,HORA,TFRECUENCIA,KMEDICIONES,MEDIA,SUMATORIA,DESVIACION,PORCIENTO5MEDIA" +
                               ",DIFERENCIAPROMEDIO,TIPOMEDICION,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10)" +
                     " values('" + carnet + "','" + fecha + "','" + Hora + "','" + frecuencia + "','" + mediciones + "','" + media + "','" + sum + "','" + des + "'," +
                     "'" + porciento + "','" + diferencia + "','" + tiempo + "','" + M1 + "','" + M2 + "','" + M3 + "','" + M4 + "'," +
                     "'" + M5 + "','" + M6 + "','" + M7 + "','" + M8 + "','" + M9 + "','" + M10 + "')";

                            SQLiteCommand con4 = new SQLiteCommand(insert2, Connection.GetInstance().GetConnection());
                            //    con4.ExecuteNonQueryAsync();
                            con4.ExecuteNonQuery();

                        }

                    }
                }


            }

            ////////// Entidad Inserccion de entidad///////////////////////////

            /*
            OleDbCommand conAcce3 = new OleDbCommand("select * from Entidades", connectionAccess);
            DbDataReader dataReaderAcces = await conAcce3.ExecuteReaderAsync();

            if (dataReaderAcces.HasRows)
            {
                while (await dataReaderAcces.ReadAsync())
                {
                    String entidad = dataReaderAcces["Entidad"].ToString();
                    SQLiteCommand conSqli = new SQLiteCommand("select * from Entidades where Entidad= '" + entidad + "'", connectionSQLite);
                    DbDataReader dataReaderSqlite = await conSqli.ExecuteReaderAsync();

                    if (!dataReaderSqlite.HasRows)
                    {
                        String command = "Insert into Entidades (Entidad)values('" + entidad + "')";
                        SQLiteCommand conSqli2 = new SQLiteCommand(command, connectionSQLite);
                        await conSqli2.ExecuteNonQueryAsync();
                    }

                }

            }
            */



            //// Divisioines//////////////////////
            OleDbCommand conAcce5 = new OleDbCommand("select * from Divisiones", connectionAccess);
            // DbDataReader dataReaderAcces4 = await conAcce5.ExecuteReaderAsync();
            DbDataReader dataReaderAcces4 = conAcce5.ExecuteReader();

            if (dataReaderAcces4.HasRows)
            {
                while (/*await dataReaderAcces4.ReadAsync()  */dataReaderAcces4.Read())
                {
                    String modalidad = dataReaderAcces4["División"].ToString();
                    SQLiteCommand conSqli4 = new SQLiteCommand("select * from Modalidad where Modalidad= '" + modalidad + "'", Connection.GetInstance().GetConnection());

                    //DbDataReader dataReaderSqlite4 = await conSqli4.ExecuteReaderAsync();
                    DbDataReader dataReaderSqlite4 = conSqli4.ExecuteReader();


                    if (!dataReaderSqlite4.HasRows)
                    {
                        String command = "Insert into Modalidad (Modalidad)values('" + modalidad + "')";
                        SQLiteCommand conSqli3 = new SQLiteCommand(command, Connection.GetInstance().GetConnection());
                        // await conSqli3.ExecuteNonQueryAsync();
                        conSqli3.ExecuteNonQuery();
                    }

                }

            }

            Connection.GetInstance().GetConnection().Close();
            connectionAccess.Close();

        }



        private void MetroTile4_Click(object sender, EventArgs e)
        {
            try
            {

               VerificarHardware res = new VerificarHardware();
               bool result= res.configurarHardware();

                if (result)
                {
                    //Application.DoEvents();
                    //Esperar es = new Esperar();
                    //es.Show();
                    Prueba p = new Prueba();
                    //es.Close();
                    p.ShowDialog();

                }
                else
                {

                    MessageBox.Show("Conecte el visor ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }




            }
            catch (Exception)

            {


                MessageBox.Show("Conecte el visor a la PC", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }



        }



        private void Form1_Resize(object sender, EventArgs e)
        {
            HeightF = this.Height;
            WidthF = this.Width;
            metroTileAtletas.Location = new Point(WidthF / 2 - 375, HeightF / 2 - 50);
            metroTilePruebas.Location = new Point(WidthF / 2 - 150, HeightF / 2 - 50);
            metroTileVisualizar.Location = new Point(WidthF / 2 + 75, HeightF / 2 - 50);
            metroTileReportes.Location = new Point(WidthF / 2 + 295, HeightF / 2 - 50);

            label1.Location = new Point(label1.Location.X, HeightF / 2 - 200);

        }



        private void MetroTile2_Click(object sender, EventArgs e)
        {


            //Application.DoEvents();
            //Esperar es = new Esperar();
            //es.Show();
            User user = new User();
            //es.Close();
            user.ShowDialog();
        }

        private void SdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MetroTile1_Click(object sender, EventArgs e)
        {
            //Application.DoEvents();
            //Esperar es = new Esperar();
            //es.Show();
            VistaPruebas rep = new VistaPruebas();
            //es.Close();
            rep.ShowDialog();
        }

        private void AsdToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = @"C\:";
            openFileDialog1.Title = "Open mdb File";
            openFileDialog1.Filter = "mdb File | *.mdb";
            openFileDialog1.FileName = "";


            //   openFileDialog1.ShowDialog();


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(openFileDialog1.FileName))
                    {
                        pathMdb = openFileDialog1.FileName;

                        ImportarDatosAsync();
                        // ImportarDatos();
                        MessageBox.Show("Los datos se han importados correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception es)
                {

                    MessageBox.Show("Ha ocurrido un erroral importar los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void AsdToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Modalidad div = new Modalidad();
            div.ShowDialog();
        }

        private void AsdToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Entidad en = new Entidad();
            en.ShowDialog();
        }

        private void EtapasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Periodo etapa = new Periodo();
            etapa.ShowDialog();
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acerca acer = new Acerca();
            acer.Show();
        }

        private void MetroTile3_Click(object sender, EventArgs e)
        {
            //Application.DoEvents();
            //Esperar es = new Esperar();
            //es.Show();
            Reporte re = new Reporte();
            //es.Close();
            re.Show();
        }


        public bool ArduinoPort()
        {
            bool res = false;

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2",
          @"SELECT * FROM Win32_PnPEntity where DeviceID Like ""USB%"""))
            collection = searcher.Get();
            
            String arduinoID = "USB\\VID_1A86&PID_7523";


            String aux = null;

            foreach (var device in collection)
            {
                String temp = device.GetPropertyValue("DeviceID").ToString().Substring(0,21);
                
                if (temp == arduinoID)
                {
                    res = true;
                    aux = temp;

                }
                   


            }

           

            return res;

        }

        private void DispositivoHardwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoHardware tipo = new TipoHardware();
            tipo.ShowDialog();
        }
    }
}
