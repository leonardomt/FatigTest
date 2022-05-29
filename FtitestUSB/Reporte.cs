using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FtitestUSB
{
    public partial class Reporte : MetroFramework.Forms.MetroForm
    {

        bool controlButton1 = false;
        bool controlButton2 = false;
        bool controlButton3 = false;
        bool controlButton5 = false;
        bool controlButton6 = false;
        bool controlButton7 = false;
        List<String> orden = new List<String>();
        Esperar es = new Esperar();

        public Reporte()
        {



            InitializeComponent();


            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }





        private void backgroundWorker1_RunWorkerCompleted(object sender,
RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                es.Close();
            }
            else if (e.Error != null)
            {
                //  MessageBox.Show("Error. Details: " + (e.Error as Exception).ToString());
                es.Close();
            }
            else
            {
                MessageBox.Show("The task has been completed. Results: " + e.Result.ToString());
            }
        }


        private void backgroundWorker1_ProgressChanged(object sender,
ProgressChangedEventArgs e)
        {

        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                backgroundWorker1.ReportProgress(0);
                return;
            }

            if (!backgroundWorker1.IsBusy)
                es.Show();


        }







        private void CheckBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                comboBox6.Enabled = false;
                comboBox6.DataSource = null;
                comboBox6.Text = "Seleccione";
                comboBox6.DataSource = null;

                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                comboBox9.Enabled = true;
                tableLayoutPanel13.Enabled = true;


                buscarPruebasHechas();





            }
            else
            {

                comboBox6.Enabled = true;
                checkBox4.Checked = false;
                tableLayoutPanel13.Enabled = false;
                dataGridView1.Rows.Clear();
                LimpiarBotones();

            }


        }

        public void LimpiarBotones()
        {


            quitarColumnaEncuesta();
            eliminarColumnFlickerAntes();
            quitarPPAntes();
            quitarColumnaEBorg();
            eliminarColumnFlickerDespues();
            quitarPPDespues();
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;

            button1.BackColor = Color.LightGray;
            button1.ForeColor = Color.Black;
            controlButton1 = false;
            button2.BackColor = Color.LightGray;
            button2.ForeColor = Color.Black;
            controlButton2 = false;
            button3.BackColor = Color.LightGray;
            button3.ForeColor = Color.Black;
            controlButton3 = false;
            button5.BackColor = Color.LightGray;
            button5.ForeColor = Color.Black;
            controlButton5 = false;
            button6.BackColor = Color.LightGray;
            button6.ForeColor = Color.Black;
            controlButton6 = false;
            button7.BackColor = Color.LightGray;
            button7.ForeColor = Color.Black;
            controlButton7 = false;
        }

        public void quitarColumna()
        {
            quitarColumnaEncuesta();
            eliminarColumnFlickerAntes();
            quitarPPAntes();
            quitarColumnaEBorg();
            eliminarColumnFlickerDespues();
            quitarPPDespues();
        }

        private void ComboBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ComboBox6_DropDown(object sender, EventArgs e)
        {
            SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct CarnetIdentidad,(Nombre||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join Test_Principal on Test_Principal.Atleta =  DatosSujetos.CarnetIdentidad  ", Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();
            categoria.Fill(data);
            comboBox6.DataSource = data;
            comboBox6.DisplayMember = "fila";
            comboBox6.ValueMember = "CarnetIdentidad";
        }

        private void ComboBox9_DropDown(object sender, EventArgs e)
        {
            if (comboBox6.SelectedValue == null)
            {
                SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct Deporte FROM DatosSujetos inner join Test_Principal on Test_Principal.Atleta =  DatosSujetos.CarnetIdentidad and Eliminado='0' ", Connection.GetInstance().GetConnection());
                DataTable data = new DataTable();
                categoria.Fill(data);
                comboBox9.DataSource = data;
                comboBox9.DisplayMember = "Deporte";
                comboBox9.ValueMember = "Deporte";
            }
            else
            {
                SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct Deporte FROM DatosSujetos inner join Test_Principal on Test_Principal.Atleta =  DatosSujetos.CarnetIdentidad and Eliminado='0' and  DatosSujetos.CarnetIdentidad='" + comboBox6.SelectedValue.ToString() + "' ", Connection.GetInstance().GetConnection());
                DataTable data = new DataTable();
                categoria.Fill(data);
                comboBox9.DataSource = data;
                comboBox9.DisplayMember = "Deporte";
                comboBox9.ValueMember = "Deporte";
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                try
                {

                    string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillaReporte.xlsx");

                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Excel (*.xlsx)|*.xls";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar Pruebas";

                    if (comboBox6.Text != "Seleccione")
                        fichero.FileName = "Pruebas " + comboBox6.Text;
                    else
                        fichero.FileName = "Pruebas Todas";

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(rutaProject))
                        {

                            if (File.Exists(fichero.FileName))
                            {
                                File.Delete(fichero.FileName);
                            }

                            File.Copy(rutaProject, fichero.FileName, true);


                            Application.DoEvents();
                            es.Show();



                            String rutaFichero = fichero.FileName.ToString();

                            Microsoft.Office.Interop.Excel.Application ExApp;
                            ExApp = new Microsoft.Office.Interop.Excel.Application();
                            Microsoft.Office.Interop.Excel._Workbook oWBook;
                            Microsoft.Office.Interop.Excel._Worksheet oSheet;
                            oWBook = ExApp.Workbooks.Open(rutaFichero, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWBook.ActiveSheet;

                            int k = 3;

                            for (int row = 0; row < dataGridView1.Rows.Count; row++)

                            {
                                String mediaAntes = "";
                                String mediaDespues = "";

                                //Se pone k+1 pq el primer elemento de la tabla es el id de todala prueba

                              
                                oSheet.Cells[k, 1] = dataGridView1.Rows[row].Cells["nombre"].Value.ToString(); //Nombre
                                oSheet.Cells[k, 2] = dataGridView1.Rows[row].Cells["apellido1"].Value;// Primer apellido
                                oSheet.Cells[k, 3] = dataGridView1.Rows[row].Cells["apellido2"].Value;//Segundo Apellido
                                oSheet.Cells[k, 4] = dataGridView1.Rows[row].Cells["deporte"].Value;//Deporte
                                oSheet.Cells[k, 5] = dataGridView1.Rows[row].Cells["modalidad"].Value;//Modalida
                                oSheet.Cells[k, 48] = dataGridView1.Rows[row].Cells["fecha"].Value.ToString();


                                if (dataGridView1.Columns.Contains("pregunta1"))
                                {

                                    oSheet.Cells[k, 6] = dataGridView1.Rows[row].Cells["respuesta1"].Value;
                                    oSheet.Cells[k, 7] = dataGridView1.Rows[row].Cells["respuesta2"].Value;
                                    oSheet.Cells[k, 8] = dataGridView1.Rows[row].Cells["respuesta3"].Value;

                                }

                                if (dataGridView1.Columns.Contains("tfrecuencia"))
                                {


                                    oSheet.Cells[k, 17] = dataGridView1.Rows[row].Cells["kmediciones"].Value;
                                    oSheet.Cells[k, 18] = dataGridView1.Rows[row].Cells["media"].Value;
                                    oSheet.Cells[k, 19] = dataGridView1.Rows[row].Cells["desviacion"].Value;
                                    oSheet.Cells[k, 20] = dataGridView1.Rows[row].Cells["rango"].Value;

                                    oSheet.Cells[k, 21] = dataGridView1.Rows[row].Cells["m1"].Value;
                                    oSheet.Cells[k, 22] = dataGridView1.Rows[row].Cells["m2"].Value;
                                    oSheet.Cells[k, 23] = dataGridView1.Rows[row].Cells["m3"].Value;
                                    oSheet.Cells[k, 24] = dataGridView1.Rows[row].Cells["m4"].Value;
                                    oSheet.Cells[k, 25] = dataGridView1.Rows[row].Cells["m5"].Value;
                                    oSheet.Cells[k, 26] = dataGridView1.Rows[row].Cells["m6"].Value;
                                    oSheet.Cells[k, 27] = dataGridView1.Rows[row].Cells["m7"].Value;
                                    oSheet.Cells[k, 2] = dataGridView1.Rows[row].Cells["m8"].Value;
                                    oSheet.Cells[k, 29] = dataGridView1.Rows[row].Cells["m9"].Value;
                                    oSheet.Cells[k, 30] = dataGridView1.Rows[row].Cells["m10"].Value;

                                    mediaAntes = dataGridView1.Rows[row].Cells["media"].Value != null ? dataGridView1.Rows[row].Cells["media"].Value.ToString() : "";

                                }


                                if (dataGridView1.Columns.Contains("estadofisico"))
                                {

                                    oSheet.Cells[k, 11] = dataGridView1.Rows[row].Cells["estadofisico"].Value;
                                    oSheet.Cells[k, 12] = dataGridView1.Rows[row].Cells["actividad"].Value;
                                    oSheet.Cells[k, 13] = dataGridView1.Rows[row].Cells["estadoanimico"].Value;


                                }


                                if (dataGridView1.Columns.Contains("esfuerzo"))
                                {
                                    oSheet.Cells[k, 9] = dataGridView1.Rows[row].Cells["esfuerzo"].Value;
                                    oSheet.Cells[k, 10] = dataGridView1.Rows[row].Cells["puntuacion"].Value;
                                }

                                if (dataGridView1.Columns.Contains("tfrecuenciaD"))
                                {


                                    oSheet.Cells[k, 31] = dataGridView1.Rows[row].Cells["kmedicionesD"].Value;
                                    oSheet.Cells[k, 32] = dataGridView1.Rows[row].Cells["mediaD"].Value;
                                    oSheet.Cells[k, 33] = dataGridView1.Rows[row].Cells["desviacionD"].Value;
                                    oSheet.Cells[k, 34] = dataGridView1.Rows[row].Cells["rangoD"].Value;

                                    oSheet.Cells[k, 35] = dataGridView1.Rows[row].Cells["m1D"].Value;
                                    oSheet.Cells[k, 36] = dataGridView1.Rows[row].Cells["m2D"].Value;
                                    oSheet.Cells[k, 37] = dataGridView1.Rows[row].Cells["m3D"].Value;
                                    oSheet.Cells[k, 38] = dataGridView1.Rows[row].Cells["m4D"].Value;
                                    oSheet.Cells[k, 39] = dataGridView1.Rows[row].Cells["m5D"].Value;
                                    oSheet.Cells[k, 40] = dataGridView1.Rows[row].Cells["m6D"].Value;
                                    oSheet.Cells[k, 41] = dataGridView1.Rows[row].Cells["m7D"].Value;
                                    oSheet.Cells[k, 42] = dataGridView1.Rows[row].Cells["m8D"].Value;
                                    oSheet.Cells[k, 43] = dataGridView1.Rows[row].Cells["m9D"].Value;
                                    oSheet.Cells[k, 44] = dataGridView1.Rows[row].Cells["m10D"].Value;

                                    mediaDespues = dataGridView1.Rows[row].Cells["mediaD"].Value != null ? dataGridView1.Rows[row].Cells["mediaD"].Value.ToString() : "";


                                }



                                if (dataGridView1.Columns.Contains("estadofisicoD"))
                                {
                                    oSheet.Cells[k, 14] = dataGridView1.Rows[row].Cells["estadofisicoD"].Value;
                                    oSheet.Cells[k, 15] = dataGridView1.Rows[row].Cells["actividadD"].Value;
                                    oSheet.Cells[k, 16] = dataGridView1.Rows[row].Cells["estadoanimicoD"].Value;
                                }




                                if (mediaAntes != "" && mediaDespues != "" && controlButton2 && controlButton6)
                                {
                                    Double mA = Convert.ToDouble(mediaAntes);
                                    Double mD = Convert.ToDouble(mediaDespues);

                                    Double porciento = (mA / mD) * Convert.ToInt64(numericUpDown1.Value.ToString());


                                    oSheet.Cells[k, 45] = Math.Round(porciento, 2).ToString();
                                    oSheet.Cells[k, 46] = Math.Round(mD - mA, 2).ToString();
                                    oSheet.Cells[k, 47] = Math.Round((mD - mA) - porciento, 2).ToString();

                                }

                                k++;

                            }




                            if (backgroundWorker1.IsBusy)
                            {
                                backgroundWorker1.CancelAsync();
                            }

                            //ExApp.Visible = false;
                            //     ExApp.UserControl = true;
                            es.Close();
                            oWBook.Save();
                            //ExApp.ActiveWorkbook.Close(true, ExApp, Type.Missing);
                            ExApp.Quit();



                            MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start(fichero.FileName);

                        }


                    }




                }
                catch (Exception e_x)
                {
                    Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "Esperar").SingleOrDefault<Form>();
                    if (existe != null)
                        es.Close();
                    MessageBox.Show("Ocurrio un error consulte al desarrollador" + e_x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
            {
                MessageBox.Show("Deben existir valores en la tabla", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //try
            //{
            //    if (comboBox6.SelectedValue != null || checkBox4.Checked)
            //    {

            //        if (controlButton1 || controlButton2 || controlButton1 || controlButton5 || controlButton6 || controlButton7)
            //        {
            //            buscaPruebas();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Debe seleccionar una prueba para exportar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Dede seleccionar al menos un atleta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }

            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Ocurrio un error consulte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


        }

        private void buscaPruebas()
        {

            try
            {

                String fechaIni = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                String fechaFin = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                String TestP = "";




                if (comboBox6.SelectedValue != null && comboBox9.SelectedValue != null)
                {

                    TestP = "select * from Test_Principal  inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  where Test_Principal.Atleta='" + comboBox6.SelectedValue.ToString() + "' and  DatosSujetos.Modalidad='" + comboBox9.SelectedValue.ToString() + "'  and Fecha between '" + fechaIni + "' and '" + fechaFin + "' ";

                }



                if (comboBox6.SelectedValue != null && comboBox9.SelectedValue == null)
                {

                    TestP = "select * from Test_Principal  inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  where Test_Principal.Atleta='" + comboBox6.SelectedValue.ToString() + "'  and Fecha between '" + fechaIni + "' and '" + fechaFin + "' ";

                }


                if (checkBox4.Checked && comboBox9.SelectedValue != null)
                {

                    TestP = "select * from Test_Principal  inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  where  DatosSujetos.Modalidad='" + comboBox9.SelectedValue.ToString() + "'  and Fecha between '" + fechaIni + "' and '" + fechaFin + "' ";

                }

                if (checkBox4.Checked && comboBox9.SelectedValue == null)
                {

                    TestP = "select * from Test_Principal  inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  where  Fecha between '" + fechaIni + "' and '" + fechaFin + "' ";

                }



                //   SQLiteCommand test_P = new SQLiteCommand(TestP, Connection.GetInstance().GetConnection());

                doExcel(TestP);



            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error consulte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void doExcel(String test_P)
        {

            try
            {


                string rutaProject = Path.Combine(System.Windows.Forms.Application.StartupPath, @"PlantillaReporte.xlsx");

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xls";
                fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                fichero.Title = "Exportar Pruebas";

                if (comboBox6.Text != "Seleccione")
                    fichero.FileName = "Pruebas " + comboBox6.Text;
                else
                    fichero.FileName = "Pruebas Todas";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(rutaProject))
                    {

                        if (File.Exists(fichero.FileName))
                        {
                            File.Delete(fichero.FileName);
                        }

                        File.Copy(rutaProject, fichero.FileName, true);





                        String rutaFichero = fichero.FileName.ToString();

                        Microsoft.Office.Interop.Excel.Application ExApp;
                        ExApp = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel._Workbook oWBook;
                        Microsoft.Office.Interop.Excel._Worksheet oSheet;
                        oWBook = ExApp.Workbooks.Open(rutaFichero, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWBook.ActiveSheet;




                        using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand(test_P, c))
                            {
                                using (SQLiteDataReader readerTest = comm1.ExecuteReader())
                                {
                                    int row = 3;


                                    while (readerTest.Read())
                                    {


                                        String id = readerTest["id"].ToString();

                                        String encuesta = "";
                                        String flickerA = "";
                                        String flickrD = "";
                                        String Borg = "";
                                        String ppA = "";
                                        String ppD = "";

                                        String mediaAntes = "";
                                        String mediaDespues = "";

                                        bool entroA = false;
                                        bool entroD = false;

                                        if (comboBox6.SelectedValue == null && checkBox4.Checked)

                                        {
                                            encuesta = "Select * from Pregunta3 inner join Test_Principal on Pregunta3.id= Test_Principal.Pregunta_3 where Test_Principal.Pregunta_3 ='" + readerTest["Pregunta_3"].ToString() + "'";
                                            flickerA = "Select * from Pruebas inner join Test_Principal on Pruebas.Id= Test_Principal.Flicker_Antes where Pruebas.Id ='" + readerTest["Flicker_Antes"].ToString() + "'";
                                            flickrD = "Select * from Pruebas inner join Test_Principal on Pruebas.Id= Test_Principal.Flicker_Despues where Pruebas.Id ='" + readerTest["Flicker_Despues"].ToString() + "'";
                                            Borg = "Select * from Borg inner join Test_Principal on Borg.id= Test_Principal.Borg where Borg.id ='" + readerTest["Borg"].ToString() + "'";
                                            ppA = "Select * from PerfilPolaridad inner join Test_Principal on PerfilPolaridad.Id= Test_Principal.PerfilPolaridad_Antes where Test_Principal.PerfilPolaridad_Antes ='" + readerTest["PerfilPolaridad_Antes"].ToString() + "'";
                                            ppD = "Select * from PerfilPolaridad inner join Test_Principal on PerfilPolaridad.Id= Test_Principal.PerfilPolaridad_Despues where Test_Principal.PerfilPolaridad_Despues ='" + readerTest["PerfilPolaridad_Despues"].ToString() + "'";
                                        }

                                        if (comboBox6.SelectedValue != null && !checkBox4.Checked)

                                        {
                                            encuesta = "Select * from Pregunta3 inner join Test_Principal on Pregunta3.id= Test_Principal.Pregunta_3 where Test_Principal.Pregunta_3 ='" + readerTest["Pregunta_3"].ToString() + "' and  Test_Principal.Atleta='" + readerTest["Atleta"].ToString() + "'";
                                            flickerA = "Select * from Pruebas inner join Test_Principal on Pruebas.Id= Test_Principal.Flicker_Antes where Pruebas.Id ='" + readerTest["Flicker_Antes"].ToString() + "' and  Test_Principal.Atleta='" + readerTest["Atleta"].ToString() + "'";
                                            flickrD = "Select * from Pruebas inner join Test_Principal on Pruebas.Id= Test_Principal.Flicker_Despues where Pruebas.Id ='" + readerTest["Flicker_Despues"].ToString() + "' and  Test_Principal.Atleta='" + readerTest["Atleta"].ToString() + "'";
                                            Borg = "Select * from Borg inner join Test_Principal on Borg.id= Test_Principal.Borg where Borg.id ='" + readerTest["Borg"].ToString() + "' and  Test_Principal.Atleta='" + readerTest["Atleta"].ToString() + "'";
                                            ppA = "Select * from PerfilPolaridad inner join Test_Principal on PerfilPolaridad.Id= Test_Principal.PerfilPolaridad_Antes where Test_Principal.PerfilPolaridad_Antes ='" + readerTest["PerfilPolaridad_Antes"].ToString() + "' and  Test_Principal.Atleta='" + readerTest["Atleta"].ToString() + "'";
                                            ppD = "Select * from PerfilPolaridad inner join Test_Principal on PerfilPolaridad.Id= Test_Principal.PerfilPolaridad_Despues where Test_Principal.PerfilPolaridad_Despues ='" + readerTest["PerfilPolaridad_Despues"].ToString() + "' and  Test_Principal.Atleta='" + readerTest["Atleta"].ToString() + "'";
                                        }

                                        String nombre = readerTest["Atleta"].ToString();
                                        String Atleta = "Select * from DatosSujetos      where DatosSujetos.CarnetIdentidad ='" + readerTest["Atleta"].ToString() + "'";


                                        oSheet.Cells[row, 48] = readerTest["Fecha"].ToString();


                                        using (SQLiteConnection con1 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                                        {
                                            con1.Open();
                                            using (SQLiteCommand atleta = new SQLiteCommand(Atleta, con1))
                                            {
                                                using (SQLiteDataReader atletaData = atleta.ExecuteReader())
                                                {
                                                    atletaData.Read();

                                                    string r = atletaData["Nombre"].ToString();

                                                    oSheet.Cells[row, 1] = atletaData["Nombre"].ToString();
                                                    oSheet.Cells[row, 2] = atletaData["PrimerApellido"].ToString();
                                                    oSheet.Cells[row, 3] = atletaData["SegundoApellido"].ToString();
                                                    oSheet.Cells[row, 4] = atletaData["Modalidad"].ToString();
                                                    oSheet.Cells[row, 5] = atletaData["Deporte"].ToString();
                                                }
                                            }
                                        }



                                        using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                                        {
                                            con2.Open();
                                            using (SQLiteCommand commandPregunta3 = new SQLiteCommand(encuesta, con2))
                                            {
                                                using (SQLiteDataReader readerEncuesta = commandPregunta3.ExecuteReader())
                                                {


                                                    if (controlButton1)
                                                    {

                                                        if (readerEncuesta.HasRows)
                                                        {
                                                            readerEncuesta.Read();
                                                            oSheet.Cells[row, 6] = readerEncuesta["Respuesta1"].ToString();
                                                            oSheet.Cells[row, 7] = readerEncuesta["Respuesta2"].ToString();
                                                            oSheet.Cells[row, 8] = readerEncuesta["Respuesta3"].ToString();
                                                        }
                                                    }
                                                }
                                            }
                                        }







                                        using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                                        {
                                            con2.Open();
                                            using (SQLiteCommand flickerAntes = new SQLiteCommand(flickerA, con2))
                                            {
                                                using (SQLiteDataReader readerFlickerA = flickerAntes.ExecuteReader())
                                                {

                                                    if (controlButton2)
                                                    {

                                                        if (readerFlickerA.HasRows)
                                                        {
                                                            readerFlickerA.Read();
                                                            oSheet.Cells[row, 17] = readerFlickerA["KMEDICIONES"].ToString();
                                                            oSheet.Cells[row, 18] = readerFlickerA["MEDIA"].ToString();
                                                            oSheet.Cells[row, 19] = readerFlickerA["DESVIACION"].ToString();
                                                            oSheet.Cells[row, 20] = readerFlickerA["RANGO"].ToString();
                                                            oSheet.Cells[row, 21] = readerFlickerA["M1"].ToString();
                                                            oSheet.Cells[row, 22] = readerFlickerA["M2"].ToString();
                                                            oSheet.Cells[row, 23] = readerFlickerA["M3"].ToString();
                                                            oSheet.Cells[row, 24] = readerFlickerA["M4"].ToString();
                                                            oSheet.Cells[row, 25] = readerFlickerA["M5"].ToString();
                                                            oSheet.Cells[row, 26] = readerFlickerA["M6"].ToString();
                                                            oSheet.Cells[row, 27] = readerFlickerA["M7"].ToString();
                                                            oSheet.Cells[row, 28] = readerFlickerA["M8"].ToString();
                                                            oSheet.Cells[row, 29] = readerFlickerA["M9"].ToString();
                                                            oSheet.Cells[row, 30] = readerFlickerA["M10"].ToString();
                                                            entroA = true;
                                                            mediaAntes = readerFlickerA["Media"].ToString();

                                                        }


                                                    }

                                                }
                                            }
                                        }



                                        using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                                        {
                                            con2.OpenAndReturn();
                                            using (SQLiteCommand flickerDespues = new SQLiteCommand(flickrD, con2))
                                            {
                                                using (SQLiteDataReader readerFlickerD = flickerDespues.ExecuteReader())
                                                {


                                                    if (controlButton6)
                                                    {

                                                        if (readerFlickerD.HasRows)
                                                        {
                                                            readerFlickerD.Read();
                                                            oSheet.Cells[row, 31] = readerFlickerD["KMEDICIONES"].ToString();
                                                            oSheet.Cells[row, 32] = readerFlickerD["MEDIA"].ToString();
                                                            oSheet.Cells[row, 33] = readerFlickerD["DESVIACION"].ToString();
                                                            oSheet.Cells[row, 34] = readerFlickerD["RANGO"].ToString();
                                                            oSheet.Cells[row, 35] = readerFlickerD["M1"].ToString();
                                                            oSheet.Cells[row, 36] = readerFlickerD["M2"].ToString();
                                                            oSheet.Cells[row, 37] = readerFlickerD["M3"].ToString();
                                                            oSheet.Cells[row, 38] = readerFlickerD["M4"].ToString();
                                                            oSheet.Cells[row, 39] = readerFlickerD["M5"].ToString();
                                                            oSheet.Cells[row, 40] = readerFlickerD["M6"].ToString();
                                                            oSheet.Cells[row, 41] = readerFlickerD["M7"].ToString();
                                                            oSheet.Cells[row, 42] = readerFlickerD["M8"].ToString();
                                                            oSheet.Cells[row, 43] = readerFlickerD["M9"].ToString();
                                                            oSheet.Cells[row, 44] = readerFlickerD["M10"].ToString();

                                                            entroD = true;
                                                            mediaDespues = readerFlickerD["Media"].ToString();
                                                        }


                                                    }
                                                }
                                            }
                                        }



                                        using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                                        {
                                            con2.Open();
                                            using (SQLiteCommand borg = new SQLiteCommand(Borg, con2))
                                            {
                                                using (SQLiteDataReader readerBorg = borg.ExecuteReader())
                                                {
                                                    if (controlButton5)
                                                    {

                                                        if (readerBorg.HasRows)
                                                        {
                                                            readerBorg.Read();
                                                            oSheet.Cells[row, 9] = readerBorg["Esfuerzo"].ToString();
                                                            oSheet.Cells[row, 10] = readerBorg["Puntuacion"].ToString();
                                                        }
                                                    }

                                                }
                                            }
                                        }



                                        using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                                        {
                                            con2.Open();
                                            using (SQLiteCommand perfilPA = new SQLiteCommand(ppA, con2))
                                            {
                                                using (SQLiteDataReader readerPPA = perfilPA.ExecuteReader())
                                                {
                                                    if (controlButton1)
                                                    {

                                                        if (readerPPA.HasRows)
                                                        {
                                                            readerPPA.Read();
                                                            oSheet.Cells[row, 11] = readerPPA["EF"].ToString();
                                                            oSheet.Cells[row, 12] = readerPPA["A"].ToString();
                                                            oSheet.Cells[row, 13] = readerPPA["EA"].ToString();
                                                        }
                                                    }

                                                }
                                            }
                                        }



                                        using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                                        {
                                            con2.Open();
                                            using (SQLiteCommand perfilPD = new SQLiteCommand(ppD, con2))
                                            {
                                                using (SQLiteDataReader readerPPD = perfilPD.ExecuteReader())
                                                {


                                                    if (controlButton7)
                                                    {
                                                        if (readerPPD.HasRows)
                                                        {
                                                            readerPPD.Read();
                                                            oSheet.Cells[row, 14] = readerPPD["EF"].ToString();
                                                            oSheet.Cells[row, 15] = readerPPD["A"].ToString();
                                                            oSheet.Cells[row, 16] = readerPPD["EA"].ToString();
                                                        }
                                                    }
                                                }
                                            }
                                        }




                                        if (entroA && entroD && controlButton2 && controlButton6)
                                        {
                                            Double mA = Convert.ToDouble(mediaAntes);
                                            Double mD = Convert.ToDouble(mediaDespues);

                                            Double porciento = (mA / mD) * Convert.ToInt64(numericUpDown1.Value.ToString());


                                            oSheet.Cells[row, 45] = Math.Round(porciento, 2).ToString();
                                            oSheet.Cells[row, 46] = Math.Round(mD - mA, 2).ToString();
                                            oSheet.Cells[row, 47] = Math.Round((mD - mA) - porciento, 2).ToString();

                                        }

                                        row++;


                                    }

                                }

                            }

                        }



                        //ExApp.Visible = false;
                        //     ExApp.UserControl = true;
                        oWBook.Save();
                        //ExApp.ActiveWorkbook.Close(true, ExApp, Type.Missing);
                        ExApp.Quit();



                        MessageBox.Show("Se ha generado el reporte correctamente ", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }




            }
            catch (Exception e_x)
            {
                MessageBox.Show("Ocurrio un error consulte al desarrollador" + e_x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


        }

        private void CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (controlButton2 && controlButton6)
            {
                label19.Visible = true;
                numericUpDown1.Visible = true;

            }

            else
            {
                label19.Visible = false;
                numericUpDown1.Visible = false;
            }
        }

        private void ComboBox6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                tableLayoutPanel13.Enabled = true;
                buscarPruebasHechas();
            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error consulte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void buscarPruebasHechas()
        {
            String fechaIni = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            String fechaFin = dateTimePicker2.Value.ToString("yyyy-MM-dd");



            String TestP = "";


            if (comboBox6.SelectedValue != null && checkBox4.Checked == false)
            {
                TestP = "select * from Test_Principal inner join DatosSujetos on DatosSujetos.CarnetIdentidad = Test_Principal.Atleta   where  Fecha between '" + fechaIni + "' and '" + fechaFin + "' and Atleta='" + comboBox6.SelectedValue.ToString() + "' ";
            }


            if (comboBox6.SelectedValue == null && checkBox4.Checked != false)
            {
                TestP = "select * from Test_Principal  inner join DatosSujetos on DatosSujetos.CarnetIdentidad = Test_Principal.Atleta   where  Fecha between '" + fechaIni + "' and '" + fechaFin + "'  ";
            }

            if (comboBox9.SelectedValue != null && checkBox4.Checked)
            {
                TestP = "select * from Test_Principal  inner join DatosSujetos on DatosSujetos.CarnetIdentidad = Test_Principal.Atleta   where  Fecha between '" + fechaIni + "' and '" + fechaFin + "' and Deporte='" + comboBox9.SelectedValue.ToString() + "'  ";
            }



            using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                con2.Open();
                using (SQLiteCommand test_P = new SQLiteCommand(TestP, con2))
                {
                    using (SQLiteDataReader read = test_P.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear();
                        quitarColumna();
                        if (read.HasRows)
                        {
                            while (read.Read())
                            {


                                dataGridView1.Rows.Add(new object[] {
             read.GetValue(read.GetOrdinal("id")),
            read.GetValue(read.GetOrdinal("Nombre")),
            read.GetValue(read.GetOrdinal("PrimerApellido")),
            read.GetValue(read.GetOrdinal("SegundoApellido")),
            read.GetValue(read.GetOrdinal("Deporte")),
            read.GetValue(read.GetOrdinal("Modalidad")),

            DateTime.Parse(read.GetValue(read.GetOrdinal("Fecha")).ToString()).ToString("dd/MM/yyyy"),



                            });


                                String f = read["Flicker_Antes"].ToString();

                                if (read["Flicker_Antes"].ToString() != "")
                                {

                                    button2.Visible = true;


                                }



                                if (read["Flicker_Despues"].ToString() != "")
                                {

                                    button6.Visible = true;

                                }


                                if (read["Pregunta_3"].ToString() != "")
                                {

                                    button1.Visible = true;

                                }


                                if (read["Borg"].ToString() != "")
                                {

                                    button5.Visible = true;

                                }


                                if (read["PerfilPolaridad_Antes"].ToString() != "")
                                {

                                    button3.Visible = true;

                                }

                                if (read["PerfilPolaridad_Despues"].ToString() != "")
                                {

                                    button7.Visible = true;



                                }







                                dateTimePicker1.Enabled = true;
                                dateTimePicker2.Enabled = true;
                                comboBox9.Enabled = true;

                            }

                            if (controlButton2)
                                añadirColumnFlickerAntes();
                            if (controlButton6)
                                añadirColumnFlickerDespues();
                            if (controlButton1)
                                añadirColumnaEncuesta();
                            if (controlButton5)
                                añadirColumnaEBorg();
                            if (controlButton3)
                                añadirPPAntes();
                            if (controlButton7)
                                añadirPPDespues();
                        }
                        else
                        {
                            LimpiarBotones();

                        }
                    }
                }
            }



        }



        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            buscarPruebasHechas();
        }

        private void tableLayoutPanel14_Click(object sender, EventArgs e)
        {
            comboBox9.DataSource = null;
            comboBox9.Text = "Seleccione";
            buscarPruebasHechas();
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!controlButton1)
            {
                orden.Add("1");
                button1.BackColor = Color.FromArgb(70, 70, 96);
                button1.ForeColor = Color.White;
                controlButton1 = true;
                añadirColumnaEncuesta();
            }
            else
            {
                orden.Remove("1");
                button1.BackColor = Color.LightGray;
                button1.ForeColor = Color.Black;
                controlButton1 = false;
                quitarColumnaEncuesta();

            }
        }

        private void añadirColumnaEncuesta()
        {
            int i = dataGridView1.Columns.Count;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.Columns.Add("pregunta1", "Pregunta 1");
            dataGridView1.Columns.Add("pregunta2", "Pregunta 2");
            dataGridView1.Columns.Add("pregunta3", "Pregunta 3");

            dataGridView1.Columns.Add("respuesta1", "Respuesta 1");
            dataGridView1.Columns.Add("respuesta2", "Respuesta 2");
            dataGridView1.Columns.Add("respuesta3", "Respuesta 3");



            int k = 0;
            while (k < dataGridView1.Rows.Count)
            {
                using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    con2.Open();
                    string idTest = dataGridView1.Rows[k].Cells[0].Value.ToString();
                    using (SQLiteCommand test_P = new SQLiteCommand("Select * from Test_Principal inner join Pregunta3 on Test_Principal.Pregunta_3 = Pregunta3.id where Test_Principal.id='" + idTest + "'  ", con2))
                    {

                        using (SQLiteDataReader read = test_P.ExecuteReader())
                        {

                            if (read.HasRows)
                            {
                                read.Read();
                                String p1 = read["Pregunta1"].ToString();
                                String p2 = read["Pregunta2"].ToString();
                                String p3 = read["Pregunta3"].ToString();

                                String r1 = read["Respuesta1"].ToString();
                                String r2 = read["Respuesta2"].ToString();
                                String r3 = read["Respuesta3"].ToString();

                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 6].Value = p1;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 5].Value = p2;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 4].Value = p3;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 3].Value = r1;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 2].Value = r2;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 1].Value = r3;

                            }
                        }
                    }
                }
                k++;
            }


            while (i < dataGridView1.Columns.Count)
            {

                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(70, 70, 96);
                dataGridView1.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                i++;
            }

        }

        private void quitarColumnaEncuesta()
        {
            if (dataGridView1.Columns.Count == 7)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (dataGridView1.Columns.Contains("pregunta1"))
            {
                dataGridView1.Columns.Remove("pregunta1");
                dataGridView1.Columns.Remove("pregunta2");
                dataGridView1.Columns.Remove("pregunta3");

                dataGridView1.Columns.Remove("respuesta1");
                dataGridView1.Columns.Remove("respuesta2");
                dataGridView1.Columns.Remove("respuesta3");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (!controlButton2)
            {
                orden.Add("2");
                button2.BackColor = Color.FromArgb(47, 117, 180);
                button2.ForeColor = Color.White;

                controlButton2 = true;
                añadirColumnFlickerAntes();

            }
            else
            {
                orden.Remove("2");
                button2.BackColor = Color.LightGray;
                button2.ForeColor = Color.Black;
                controlButton2 = false;
                eliminarColumnFlickerAntes();

            }

            if (controlButton2 && controlButton6)
                tableLayoutPanel1.Visible = true;
            else
                tableLayoutPanel1.Visible = false;
        }

        private void añadirColumnFlickerAntes()
        {

            //DataGridViewColumn res = new DataGridViewColumn();
            //res.Name = "tfrecuencia";
            //res.HeaderText= "Fecuencia";

            //res.CellTemplate = new DataGridViewTextBoxCell();

            //dataGridView1.Columns.Add(res);
            //dataGridView1.Columns["tfrecuencia"].DefaultCellStyle.BackColor = Color.CadetBlue;
            //dataGridView1.Columns["tfrecuencia"].DefaultCellStyle.ForeColor = Color.White;
            int i = dataGridView1.Columns.Count;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridView1.Columns.Add("tfrecuencia", "Fecuencia");
            dataGridView1.Columns.Add("kmediciones", "Cant.Mediciones");
            dataGridView1.Columns.Add("rango", "Rango");
            dataGridView1.Columns.Add("media", "Media");

            dataGridView1.Columns.Add("sumatoria", "Sumatoria");
            dataGridView1.Columns.Add("desviacion", "Desviación");
            dataGridView1.Columns.Add("5porciento", "Porciento %5");
            dataGridView1.Columns.Add("promediodiferecia", "Promedio Diferencia");


            dataGridView1.Columns.Add("m1", "M1");
            dataGridView1.Columns.Add("m2", "M2");
            dataGridView1.Columns.Add("m3", "M3");

            dataGridView1.Columns.Add("m4", "M4");
            dataGridView1.Columns.Add("m5", "M5");
            dataGridView1.Columns.Add("m6", "M6");

            dataGridView1.Columns.Add("m7", "M7");
            dataGridView1.Columns.Add("m8", "M8");
            dataGridView1.Columns.Add("m9", "M9");
            dataGridView1.Columns.Add("m10", "M10");



            int k = 0;
            while (k < dataGridView1.Rows.Count)
            {
                using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    con2.Open();
                    string idTest = dataGridView1.Rows[k].Cells[0].Value.ToString();
                    using (SQLiteCommand test_P = new SQLiteCommand("Select * from Test_Principal inner join Pruebas on Test_Principal.Flicker_Antes = Pruebas.id where Test_Principal.id='" + idTest + "'  ", con2))
                    {

                        using (SQLiteDataReader read = test_P.ExecuteReader())
                        {

                            if (read.HasRows)
                            {
                                read.Read();
                                String frecuencia = read["TFRECUENCIA"].ToString();
                                String cantMed = read["KMEDICIONES"].ToString();
                                String rango = read["RANGO"].ToString();
                                String media = read["MEDIA"].ToString();

                                String sumatoria = read["SUMATORIA"].ToString();
                                String desv = read["DESVIACION"].ToString();
                                String porciento = read["PORCIENTO5MEDIA"].ToString();



                                String diferencia = read["DIFERENCIAPROMEDIO"].ToString();
                                String m1 = read["M1"].ToString();
                                String m2 = read["M2"].ToString();

                                String m3 = read["M3"].ToString();
                                String m4 = read["M4"].ToString();
                                String m5 = read["M5"].ToString();



                                String m6 = read["M6"].ToString();
                                String m7 = read["M7"].ToString();

                                String m8 = read["M8"].ToString();
                                String m9 = read["M9"].ToString();
                                String m10 = read["M10"].ToString();




                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 18].Value = frecuencia;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 17].Value = cantMed;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 16].Value = rango;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 15].Value = media;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 14].Value = sumatoria;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 13].Value = desv;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 12].Value = porciento;


                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 11].Value = diferencia;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 10].Value = m1;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 9].Value = m2;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 8].Value = m3;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 7].Value = m4;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 6].Value = m5;




                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 5].Value = m6;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 4].Value = m7;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 3].Value = m8;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 2].Value = m9;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 1].Value = m10;


                            }
                        }
                    }
                }
                k++;
            }




            while (i < dataGridView1.Columns.Count)
            {
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(47, 117, 180);
                dataGridView1.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                i++;
            }


        }

        private void eliminarColumnFlickerAntes()
        {

            if (dataGridView1.Columns.Count == 7)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }


            if (dataGridView1.Columns.Contains("tfrecuencia"))
            {
                dataGridView1.Columns.Remove("tfrecuencia");
                dataGridView1.Columns.Remove("kmediciones");
                dataGridView1.Columns.Remove("media");
                dataGridView1.Columns.Remove("rango");

                dataGridView1.Columns.Remove("sumatoria");
                dataGridView1.Columns.Remove("desviacion");
                dataGridView1.Columns.Remove("5porciento");
                dataGridView1.Columns.Remove("promediodiferecia");


                dataGridView1.Columns.Remove("m1");
                dataGridView1.Columns.Remove("m2");
                dataGridView1.Columns.Remove("m3");

                dataGridView1.Columns.Remove("m4");
                dataGridView1.Columns.Remove("m5");
                dataGridView1.Columns.Remove("m6");

                dataGridView1.Columns.Remove("m7");
                dataGridView1.Columns.Remove("m8");
                dataGridView1.Columns.Remove("m9");
                dataGridView1.Columns.Remove("m10");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            if (!controlButton3)
            {
                orden.Add("3");
                button3.BackColor = Color.FromArgb(5, 80, 82);
                button3.ForeColor = Color.White;
                controlButton3 = true;
                añadirPPAntes();
            }
            else
            {
                orden.Remove("3");
                button3.BackColor = Color.LightGray;
                button3.ForeColor = Color.Black;
                controlButton3 = false;
                quitarPPAntes();

            }
        }

        private void añadirPPAntes()
        {
            int i = dataGridView1.Columns.Count;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.Columns.Add("estadofisico", "Estado Físico (EF)");
            dataGridView1.Columns.Add("actividad", "Actividad");
            dataGridView1.Columns.Add("estadoanimico", "Estado Anímico");

            int k = 0;
            while (k < dataGridView1.Rows.Count)
            {
                using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    con2.Open();
                    string idTest = dataGridView1.Rows[k].Cells[0].Value.ToString();
                    using (SQLiteCommand test_P = new SQLiteCommand("Select * from Test_Principal inner join PerfilPolaridad on Test_Principal.PerfilPolaridad_Antes = PerfilPolaridad.id where Test_Principal.id='" + idTest + "'  ", con2))
                    {

                        using (SQLiteDataReader read = test_P.ExecuteReader())
                        {

                            if (read.HasRows)
                            {
                                read.Read();
                                String p1 = read["EF"].ToString();
                                String p2 = read["A"].ToString();
                                String p3 = read["EA"].ToString();




                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 3].Value = p1;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 2].Value = p2;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 1].Value = p3;

                            }
                        }
                    }
                }
                k++;
            }



            while (i < dataGridView1.Columns.Count)
            {
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(5, 80, 82);
                dataGridView1.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                i++;
            }
        }

        private void quitarPPAntes()
        {
            if (dataGridView1.Columns.Count == 7)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (dataGridView1.Columns.Contains("estadofisico"))
            {
                dataGridView1.Columns.Remove("estadofisico");
                dataGridView1.Columns.Remove("actividad");
                dataGridView1.Columns.Remove("estadoanimico");
            }

        }

        private void Button5_Click(object sender, EventArgs e)
        {

            if (!controlButton5)
            {
                orden.Add("5");
                button5.BackColor = Color.FromArgb(180, 184, 151);
                button5.ForeColor = Color.White;
                controlButton5 = true;
                añadirColumnaEBorg();
            }
            else
            {
                orden.Remove("5");
                button5.BackColor = Color.LightGray;
                button5.ForeColor = Color.Black;
                controlButton5 = false;
                quitarColumnaEBorg();

            }
        }

        private void añadirColumnaEBorg()
        {
            int i = dataGridView1.Columns.Count;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.Columns.Add("esfuerzo", "Esfuerzo");
            dataGridView1.Columns.Add("puntuacion", "Puntuación");



            int k = 0;
            while (k < dataGridView1.Rows.Count)
            {
                using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    con2.Open();
                    string idTest = dataGridView1.Rows[k].Cells[0].Value.ToString();
                    using (SQLiteCommand test_P = new SQLiteCommand("Select * from Test_Principal inner join Borg on Test_Principal.Borg = Borg.id where Test_Principal.id='" + idTest + "'  ", con2))
                    {

                        using (SQLiteDataReader read = test_P.ExecuteReader())
                        {

                            if (read.HasRows)
                            {
                                read.Read();
                                String p1 = read["Esfuerzo"].ToString();
                                String p2 = read["Puntuacion"].ToString();


                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 2].Value = p1;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 1].Value = p2;

                            }
                        }
                    }
                }
                k++;
            }




            while (i < dataGridView1.Columns.Count)
            {
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(180, 184, 151);
                dataGridView1.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                i++;
            }

        }
        private void quitarColumnaEBorg()
        {
            if (dataGridView1.Columns.Count == 7)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (dataGridView1.Columns.Contains("esfuerzo"))
            {
                dataGridView1.Columns.Remove("esfuerzo");
                dataGridView1.Columns.Remove("puntuacion");
            }

        }

        private void Button6_Click(object sender, EventArgs e)
        {

            if (!controlButton6)
            {
                orden.Add("6");
                button6.BackColor = Color.FromArgb(92, 61, 46);
                button6.ForeColor = Color.White;

                controlButton6 = true;
                añadirColumnFlickerDespues();
            }
            else
            {
                orden.Remove("6");
                button6.BackColor = Color.LightGray;
                button6.ForeColor = Color.Black;
                controlButton6 = false;
                eliminarColumnFlickerDespues();

            }

            if (controlButton2 && controlButton6)
                tableLayoutPanel1.Visible = true;
            else
                tableLayoutPanel1.Visible = false;
        }

        private void añadirColumnFlickerDespues()
        {
            int i = dataGridView1.Columns.Count;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.Columns.Add("tfrecuenciaD", "Fecuencia");
            dataGridView1.Columns.Add("kmedicionesD", "Cant.Mediciones");
            dataGridView1.Columns.Add("rangoD", "Rango");
            dataGridView1.Columns.Add("mediaD", "Media");

            dataGridView1.Columns.Add("sumatoriaD", "Sumatoria");
            dataGridView1.Columns.Add("desviacionD", "Desviación");
            dataGridView1.Columns.Add("5porcientoD", "Porciento %5");
            dataGridView1.Columns.Add("promediodifereciaD", "Promedio Diferencia");


            dataGridView1.Columns.Add("m1D", "M1");
            dataGridView1.Columns.Add("m2D", "M2");
            dataGridView1.Columns.Add("m3D", "M3");

            dataGridView1.Columns.Add("m4D", "M4");
            dataGridView1.Columns.Add("m5D", "M5");
            dataGridView1.Columns.Add("m6D", "M6");

            dataGridView1.Columns.Add("m7D", "M7");
            dataGridView1.Columns.Add("m8D", "M8");
            dataGridView1.Columns.Add("m9D", "M9");
            dataGridView1.Columns.Add("m10D", "M10");




            int k = 0;
            while (k < dataGridView1.Rows.Count)
            {
                using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    con2.Open();
                    string idTest = dataGridView1.Rows[k].Cells[0].Value.ToString();
                    using (SQLiteCommand test_P = new SQLiteCommand("Select * from Test_Principal inner join Pruebas on Test_Principal.Flicker_Despues = Pruebas.id where Test_Principal.id='" + idTest + "'  ", con2))
                    {

                        using (SQLiteDataReader read = test_P.ExecuteReader())
                        {
                            if (read.HasRows)
                            {
                                read.Read();
                                String frecuencia = read["TFRECUENCIA"].ToString();
                                String cantMed = read["KMEDICIONES"].ToString();
                                String rango = read["RANGO"].ToString();
                                String media = read["MEDIA"].ToString();

                                String sumatoria = read["SUMATORIA"].ToString();
                                String desv = read["DESVIACION"].ToString();
                                String porciento = read["PORCIENTO5MEDIA"].ToString();



                                String diferencia = read["DIFERENCIAPROMEDIO"].ToString();
                                String m1 = read["M1"].ToString();
                                String m2 = read["M2"].ToString();

                                String m3 = read["M3"].ToString();
                                String m4 = read["M4"].ToString();
                                String m5 = read["M5"].ToString();



                                String m6 = read["M6"].ToString();
                                String m7 = read["M7"].ToString();

                                String m8 = read["M8"].ToString();
                                String m9 = read["M9"].ToString();
                                String m10 = read["M10"].ToString();




                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 18].Value = frecuencia;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 17].Value = cantMed;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 16].Value = rango;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 15].Value = media;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 14].Value = sumatoria;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 13].Value = desv;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 12].Value = porciento;


                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 11].Value = diferencia;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 10].Value = m1;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 9].Value = m2;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 8].Value = m3;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 7].Value = m4;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 6].Value = m5;




                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 5].Value = m6;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 4].Value = m7;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 3].Value = m8;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 2].Value = m9;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 1].Value = m10;


                            }
                        }
                    }
                }
                k++;
            }


            while (i < dataGridView1.Columns.Count)
            {
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(92, 61, 46);
                dataGridView1.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                i++;
            }

        }

        private void eliminarColumnFlickerDespues()
        {

            if (dataGridView1.Columns.Count == 7)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (dataGridView1.Columns.Contains("tfrecuenciaD"))
            {
                dataGridView1.Columns.Remove("tfrecuenciaD");
                dataGridView1.Columns.Remove("kmedicionesD");
                dataGridView1.Columns.Remove("mediaD");
                dataGridView1.Columns.Remove("rangoD");
                dataGridView1.Columns.Remove("sumatoriaD");
                dataGridView1.Columns.Remove("desviacionD");
                dataGridView1.Columns.Remove("5porcientoD");
                dataGridView1.Columns.Remove("promediodifereciaD");


                dataGridView1.Columns.Remove("m1D");
                dataGridView1.Columns.Remove("m2D");
                dataGridView1.Columns.Remove("m3D");

                dataGridView1.Columns.Remove("m4D");
                dataGridView1.Columns.Remove("m5D");
                dataGridView1.Columns.Remove("m6D");

                dataGridView1.Columns.Remove("m7D");
                dataGridView1.Columns.Remove("m8D");
                dataGridView1.Columns.Remove("m9D");
                dataGridView1.Columns.Remove("m10D");
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {

            if (!controlButton7)
            {
                orden.Add("7");
                button7.BackColor = Color.FromArgb(98, 131, 149);
                button7.ForeColor = Color.White;

                controlButton7 = true;
                añadirPPDespues();
            }
            else
            {
                orden.Remove("7");
                button7.BackColor = Color.LightGray;
                button7.ForeColor = Color.Black;
                controlButton7 = false;
                quitarPPDespues();
            }
        }


        private void añadirPPDespues()
        {
            int i = dataGridView1.Columns.Count;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.Columns.Add("estadofisicoD", "Estado Físico (EF)");
            dataGridView1.Columns.Add("actividadD", "Actividad");
            dataGridView1.Columns.Add("estadoanimicoD", "Estado Anímico");


            int k = 0;
            while (k < dataGridView1.Rows.Count)
            {
                using (SQLiteConnection con2 = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    con2.Open();
                    string idTest = dataGridView1.Rows[k].Cells[0].Value.ToString();
                    using (SQLiteCommand test_P = new SQLiteCommand("Select * from Test_Principal inner join PerfilPolaridad on Test_Principal.PerfilPolaridad_Despues = PerfilPolaridad.id where Test_Principal.id='" + idTest + "'  ", con2))
                    {

                        using (SQLiteDataReader read = test_P.ExecuteReader())
                        {

                            if (read.HasRows)
                            {
                                read.Read();
                                String p1 = read["EF"].ToString();
                                String p2 = read["A"].ToString();
                                String p3 = read["EA"].ToString();




                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 3].Value = p1;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 2].Value = p2;
                                dataGridView1.Rows[k].Cells[dataGridView1.ColumnCount - 1].Value = p3;

                            }
                        }
                    }
                }
                k++;
            }



            while (i < dataGridView1.Columns.Count)
            {
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(98, 131, 149);
                dataGridView1.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                i++;
            }

        }
        private void quitarPPDespues()
        {
            if (dataGridView1.Columns.Count == 7)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (dataGridView1.Columns.Contains("estadofisicoD"))
            {
                dataGridView1.Columns.Remove("estadofisicoD");
                dataGridView1.Columns.Remove("actividadD");
                dataGridView1.Columns.Remove("estadoanimicoD");
            }

        }

        private void ComboBox9_SelectionChangeCommitted(object sender, EventArgs e)
        {
            buscarPruebasHechas();
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }






}
