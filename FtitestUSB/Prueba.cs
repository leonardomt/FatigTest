using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTD2XX_NET;

namespace FtitestUSB
{
    public partial class Prueba : MetroFramework.Forms.MetroForm
    {

        private String IdSujetos;
        private bool bandera = false;
        private bool reemplazar = false;
        private bool updateTestMessage = true;
        private bool hacer = false;
        private bool isNewTest = false;
        //     private Evaluar[] calificar = null;
        private List<String> signo = new List<String>();
        private List<Evaluar> calificarBorg = new List<Evaluar>();

        private List<int> spamBorg = new List<int>();
        int contador = 1;
        //  int contador2 = 0;
        String esfuerzo = "";
        String puntos = "";
        bool validar = false;

        String calidadSueno = "";
        String bebida = "";
        String actividad = "";


        private FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
        private FTDI myFtdiDevice;
        public Prueba()
        {
            InitializeComponent();
            myFtdiDevice = new FTDI();
        }

        private void initializeBorgMatrix()
        {
            /* signo = new List<String>  { "+", "+", "+", "*", "-", "-","-" }, {"-","-","-","*", "+", "+", "+" }, { "+", "+", "+", "*", "-", "-", "-" },
                 { "+", "+", "+", "*", "-", "-", "-" }, { "+", "+", "+", "*", "-", "-", "-" },{"-","-","-","*", "+", "+", "+" },{ "+", "+", "+", "*", "-", "-", "-" },
                 { "+", "+", "+", "*", "-", "-", "-" }, { 9"+", "+", "+", "*", "-", "-", "-" },{10"-","-","-","*", "+", "+", "+" },{11"","","","", "", "", "" },{ 12"+", "+", "+", "*", "-", "-", "-" },
                 {13 "+", "+", "+", "*", "-", "-", "-" },{14"-","-","-","*", "+", "+", "+" },{15 "+", "+", "+", "*", "-", "-", "-" },{16"-","-","-","*", "+", "+", "+" },{ 17"+", "+", "+", "*", "-", "-", "-" },
                 {18"-","-","-","*", "+", "+", "+" }, {19"-","-","-","*", "+", "+", "+" },{20"+", "+", "+", "*", "-", "-", "-" },{21 "+", "+", "+", "*", "-", "-", "-" },{22"-","-","-","*", "+", "+", "+" },
                 {23 "+", "+", "+", "*", "-", "-", "-" }, { 24"+", "+", "+", "*", "-", "-", "-" }();

             */
            signo.Clear();
            calificarBorg.Clear();

            signo = new List<String>(new string[] { "+++*---", "---*+++", "+++*---" ,"+++*---", "+++*---","---*+++" ,
             "+++*---", "+++*---","+++*---","---*+++", "---*+++","+++*---" , "+++*---","---*+++","+++*---","---*+++","+++*---",
             "---*+++","---*+++","+++*---","+++*---","---*+++", "+++*---","+++*---"
            });


            Evaluar eva1 = new Evaluar(signo[0], "EF");  // 0,3,4,10,13,15,22,23
            Evaluar eva2 = new Evaluar(signo[3], "EF");
            Evaluar eva3 = new Evaluar(signo[4], "EF");
            Evaluar eva4 = new Evaluar(signo[10], "EF");
            Evaluar eva5 = new Evaluar(signo[13], "EF");
            Evaluar eva6 = new Evaluar(signo[15], "EF");
            Evaluar eva7 = new Evaluar(signo[22], "EF");
            Evaluar eva8 = new Evaluar(signo[23], "EF");


            Evaluar eva9 = new Evaluar(signo[1], "A");   //1,5,7,8,9,17,18,21
            Evaluar eva10 = new Evaluar(signo[5], "A");
            Evaluar eva11 = new Evaluar(signo[7], "A");
            Evaluar eva12 = new Evaluar(signo[8], "A");
            Evaluar eva13 = new Evaluar(signo[9], "A");
            Evaluar eva14 = new Evaluar(signo[17], "A");
            Evaluar eva15 = new Evaluar(signo[18], "A");
            Evaluar eva16 = new Evaluar(signo[21], "A");


            Evaluar eva17 = new Evaluar(signo[2], "EA");  //2,6,11,12,14,16,19,20
            Evaluar eva18 = new Evaluar(signo[6], "EA");
            Evaluar eva19 = new Evaluar(signo[11], "EA");
            Evaluar eva20 = new Evaluar(signo[12], "EA");
            Evaluar eva21 = new Evaluar(signo[14], "EA");
            Evaluar eva22 = new Evaluar(signo[16], "EA");
            Evaluar eva23 = new Evaluar(signo[19], "EA");
            Evaluar eva24 = new Evaluar(signo[20], "EA");



            calificarBorg.Add(eva1);
            calificarBorg.Add(eva9);
            calificarBorg.Add(eva17);
            calificarBorg.Add(eva2);
            calificarBorg.Add(eva3);
            calificarBorg.Add(eva10);
            calificarBorg.Add(eva18);
            calificarBorg.Add(eva11);
            calificarBorg.Add(eva12);
            calificarBorg.Add(eva13);
            calificarBorg.Add(eva4);
            calificarBorg.Add(eva19);
            calificarBorg.Add(eva20);
            calificarBorg.Add(eva5);
            calificarBorg.Add(eva21);
            calificarBorg.Add(eva6);
            calificarBorg.Add(eva22);
            calificarBorg.Add(eva14);
            calificarBorg.Add(eva15);
            calificarBorg.Add(eva23);
            calificarBorg.Add(eva24);
            calificarBorg.Add(eva16);
            calificarBorg.Add(eva7);
            calificarBorg.Add(eva8);


        }

        private void Prueba_Load(object sender, EventArgs e)
        {
            int width = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Size.Width / 1.3);
            int Height = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Size.Height / 1.3);
            this.Size = new System.Drawing.Size(width, Height);


            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
            {
                openPorFTDI();

            }

            if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
            {
                configurarPuertoArduino();

            }


            dataGridView2.Columns[0].Width = dataGridView2.Width - 43;
            //label3.Width = groupBox2.Width;


            tableLayoutPanel5.BringToFront();
            tableLayoutPanel47.BringToFront();
            tableLayoutPanel24.BringToFront();
            tableLayoutPanel31.BringToFront();
            tableLayoutPanel23.BringToFront();
            tableLayoutPanel1.BringToFront();
            tableLayoutPanel21.BringToFront();

            initializeBorgMatrix();

        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox6.Checked = false;

            }
            else
            {
                checkBox5.Checked = false;
            }
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                checkBox5.Checked = false;

            }
            else
            {
                checkBox6.Checked = false;
            }
        }

        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                checkBox8.Checked = false;

            }
            else
            {
                checkBox7.Checked = false;
            }
        }

        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                checkBox7.Checked = false;

            }
            else
            {
                checkBox8.Checked = false;
            }
        }

        private void ComboBox1_DropDown(object sender, EventArgs e)
        {



            SQLiteDataAdapter categoria = new SQLiteDataAdapter("select CarnetIdentidad,(Nombre||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos  where Eliminado='0' ", Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();
            categoria.Fill(data);
            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "fila";
            comboBox1.ValueMember = "CarnetIdentidad";

            comboBox2.Text = "Seleccione";

        }

        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (IdSujetos != "")
            {

                String comand = "select * from DatosSujetos where CarnetIdentidad='" + comboBox1.SelectedValue.ToString().ToString() + "' and Eliminado ='0' ";

                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(comand, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            read.Read();
                            label20.Text = read["Nombre"].ToString();
                            label22.Text = read["Edad"].ToString();
                            label25.Text = read["PrimerApellido"].ToString();
                            label30.Text = read["SegundoApellido"].ToString();
                            label35.Text = read["Sexo"].ToString();
                            label27.Text = read["Deporte"].ToString();
                            label32.Text = read["Modalidad"].ToString();

                            label1.Text = read["Nombre"].ToString() + " " + read["PrimerApellido"].ToString() + " " + read["SegundoApellido"].ToString();
                            label127.Text = read["Nombre"].ToString() + " " + read["PrimerApellido"].ToString() + " " + read["SegundoApellido"].ToString();
                            label45.Text = read["Nombre"].ToString() + " " + read["PrimerApellido"].ToString() + " " + read["SegundoApellido"].ToString();
                            label128.Text = read["Nombre"].ToString() + " " + read["PrimerApellido"].ToString() + " " + read["SegundoApellido"].ToString();

                        }

                    }

                }



                llenarTablaFlicker();
                CalcularValoresTabla();
                CalcularMediaHistorica();
                habilitarCampo();
                EstadisticaValores();

                calcularMediasHistoricas();
                mostrarPruebasDiaAtleta();
                BuscarPruebasDia();

                filtrarTabla();



            }
        }



        private void mostrarPruebasDiaAtleta()
        {


            try
            {
                String fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");
                String Ncarnet = comboBox1.SelectedValue.ToString();

                if (comboBox2.SelectedValue != null)
                    fecha = comboBox2.SelectedValue.ToString();


                String commandPPA = "select * from PerfilPolaridad where Fecha = '" + fecha + "' and Ncarnet=  '" + Ncarnet + "' and  TFrecuencia='A' ";
                String commandPPD = "select * from PerfilPolaridad where Fecha = '" + fecha + "' and Ncarnet=  '" + Ncarnet + "' and  TFrecuencia='D' ";

                String commandDb = "select * from Pregunta3 where Fecha = '" + fecha + "' and Ncarnet=  '" + Ncarnet + "'";
                String commandDBBorg = "select * from Borg where Fecha = '" + fecha + "' and Ncarnet=  '" + Ncarnet + "'";

                String commandFlicker = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and  Fecha = '" + fecha + "'";

                dataGridView3.Rows.Clear();
                //   SQLiteCommand command = new SQLiteCommand(commandDb, Connection.GetInstance().GetConnection());
                //  SQLiteCommand commandBorg = new SQLiteCommand(commandDBBorg, Connection.GetInstance().GetConnection());

                //  SQLiteCommand commPPA = new SQLiteCommand(commandPPA, Connection.GetInstance().GetConnection());
                //   SQLiteCommand commPPD = new SQLiteCommand(commandPPD, Connection.GetInstance().GetConnection());




                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(commandDb, c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            if (read.HasRows)
                            {
                                read.Read();

                                label132.Text = read["Respuesta1"].ToString();
                                label133.Text = read["Respuesta2"].ToString();
                                label134.Text = read["Respuesta3"].ToString();

                            }
                            else
                            {
                                label132.Text = "";
                                label133.Text = "";
                                label134.Text = "";
                            }
                        }



                    }

                }


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(commandDBBorg, c))
                    {
                        using (SQLiteDataReader readBorg = comm1.ExecuteReader())
                        {
                            if (readBorg.HasRows)
                            {
                                readBorg.Read();
                                label136.Text = readBorg["Esfuerzo"].ToString();
                                label138.Text = readBorg["Puntuacion"].ToString();

                            }
                            else
                            {
                                label136.Text = "";
                                label138.Text = "";
                            }
                        }


                    }

                }


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(commandPPA, c))
                    {
                        using (SQLiteDataReader readPPA = comm1.ExecuteReader())
                        {
                            if (readPPA.HasRows)
                            {
                                readPPA.Read();
                                label6.Text = readPPA["EF"].ToString();
                                label7.Text = readPPA["A"].ToString();
                                label10.Text = readPPA["EA"].ToString();
                            }
                            else
                            {
                                label6.Text = "";
                                label7.Text = "";
                                label10.Text = "";

                            }
                        }

                    }

                }


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(commandPPD, c))
                    {
                        using (SQLiteDataReader readPPD = comm1.ExecuteReader())
                        {

                            if (readPPD.HasRows)
                            {
                                readPPD.Read();
                                label119.Text = readPPD["EF"].ToString();
                                label120.Text = readPPD["A"].ToString();
                                label121.Text = readPPD["EA"].ToString();
                            }
                            else
                            {
                                label119.Text = "";
                                label120.Text = "";
                                label121.Text = "";

                            }
                        }

                    }


                }



                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(commandFlicker, c))
                    {
                        using (SQLiteDataReader readFlicker = comm1.ExecuteReader())
                        {

                            while (readFlicker.Read())
                            {

                                dataGridView3.Rows.Add(new object[] {
            readFlicker.GetValue(readFlicker.GetOrdinal("FECHA")),  // Or column name like this
            readFlicker.GetValue(readFlicker.GetOrdinal("HORA")),
            readFlicker.GetValue(readFlicker.GetOrdinal("TFRECUENCIA")),
              readFlicker.GetValue(readFlicker.GetOrdinal("TIPOMEDICION")),
            readFlicker.GetValue(readFlicker.GetOrdinal("MEDIA")),
            readFlicker.GetValue(readFlicker.GetOrdinal("SUMATORIA")),
            readFlicker.GetValue(readFlicker.GetOrdinal("DESVIACION")),
            readFlicker.GetValue(readFlicker.GetOrdinal("RANGO")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M1")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M2")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M3")),
             readFlicker.GetValue(readFlicker.GetOrdinal("M4")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M5")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M6")),
              readFlicker.GetValue(readFlicker.GetOrdinal("M7")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M8")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M9")),
             readFlicker.GetValue(readFlicker.GetOrdinal("M10")),
           /* readFlicker.GetValue(readFlicker.GetOrdinal("M11")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M12")),
              readFlicker.GetValue(readFlicker.GetOrdinal("M13")),
             readFlicker.GetValue(readFlicker.GetOrdinal("M14")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M15")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M16")),
             readFlicker.GetValue(readFlicker.GetOrdinal("M17")),
             readFlicker.GetValue(readFlicker.GetOrdinal("M18")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M19")),
            readFlicker.GetValue(readFlicker.GetOrdinal("M20")),*/
              readFlicker.GetValue(readFlicker.GetOrdinal("Id"))


            });

                            }

                        }


                    }
                }






            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);

            }




        }

        private void BuscarPruebasDia()
        {
            try
            {
                String carnet = comboBox1.SelectedValue.ToString();
                String fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");

                if (comboBox2.SelectedValue != null)
                    fecha = comboBox2.SelectedValue.ToString();

                String comand = "select * from Test_Principal  where Atleta='" + carnet + "' and Fecha='" + fecha + "' ";

                radioButton11.Checked = false;
                radioButton15.Checked = false;
                radioButton14.Checked = false;
                radioButton13.Checked = false;
                radioButton16.Checked = false;
                radioButton17.Checked = false;


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm = new SQLiteCommand(comand, c))
                    {
                        using (SQLiteDataReader read = comm.ExecuteReader())

                        {
                            if (read.HasRows)
                            {

                                read.Read();

                                String res = read["Flicker_Despues"].ToString();

                                if (read["Flicker_Antes"].ToString() != "")
                                {
                                    radioButton11.Checked = true;
                                    label14.Text = read["Flicker_Antes"].ToString();
                                }


                                if (read["Flicker_Despues"].ToString() != "")
                                {
                                    radioButton15.Checked = true;
                                    label17.Text = read["Flicker_Despues"].ToString();
                                }


                                if (read["Pregunta_3"].ToString() != "")
                                {
                                    radioButton14.Checked = true;
                                    label13.Text = read["Pregunta_3"].ToString();
                                }


                                if (read["PerfilPolaridad_Antes"].ToString() != "")
                                {
                                    radioButton13.Checked = true;
                                    label15.Text = read["PerfilPolaridad_Antes"].ToString();
                                }


                                if (read["Borg"].ToString() != "")
                                {
                                    radioButton16.Checked = true;
                                    label18.Text = read["Borg"].ToString();
                                }


                                if (read["PerfilPolaridad_Despues"].ToString() != "")
                                {
                                    radioButton17.Checked = true;
                                    label16.Text = read["PerfilPolaridad_Despues"].ToString();
                                }




                                label12.Text = read["id"].ToString();

                            }
                            else
                            {
                                /* radioButton11.Checked = false;
                                 radioButton15.Checked = false;
                                 radioButton14.Checked = false;
                                 radioButton13.Checked = false;
                                 radioButton16.Checked = false;
                                 radioButton17.Checked = false;*/
                            }
                        }


                    }
                }







            }
            catch (Exception e)
            {
                MessageBox.Show("Ocurrio un error consulte al desarrollador" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }


        }

        private void habilitarCampo()
        {
            button7.Enabled = true;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            button4.Enabled = true;
            button6.Enabled = true;
            button9.Enabled = true;
            button5.Enabled = true;
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            checkBox7.Enabled = true;
            checkBox8.Enabled = true;
            comboBoxFecha.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;

        }

        private void llenarTablaFlicker()
        {


            dataGridView1.Rows.Clear();



            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm = new SQLiteCommand("select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' ", c))
                {
                    using (SQLiteDataReader read = comm.ExecuteReader())
                    {
                        while (read.Read())
                        {



                            dataGridView1.Rows.Add(new object[] {
                            read.GetValue(read.GetOrdinal("FECHA")),  // Or column name like this
                            read.GetValue(read.GetOrdinal("HORA")),
                            read.GetValue(read.GetOrdinal("TFRECUENCIA")),
                            read.GetValue(read.GetOrdinal("TIPOMEDICION")),
                            read.GetValue(read.GetOrdinal("MEDIA")),
                            read.GetValue(read.GetOrdinal("SUMATORIA")),
                            read.GetValue(read.GetOrdinal("DESVIACION")),
                            read.GetValue(read.GetOrdinal("RANGO")),
                            read.GetValue(read.GetOrdinal("M1")),
                            read.GetValue(read.GetOrdinal("M2")),
                            read.GetValue(read.GetOrdinal("M3")),
                            read.GetValue(read.GetOrdinal("M4")),
                            read.GetValue(read.GetOrdinal("M5")),
                            read.GetValue(read.GetOrdinal("M6")),
                            read.GetValue(read.GetOrdinal("M7")),
                            read.GetValue(read.GetOrdinal("M8")),
                            read.GetValue(read.GetOrdinal("M9")),
                            read.GetValue(read.GetOrdinal("M10")),

                            read.GetValue(read.GetOrdinal("Id"))


                        });

                        }


                    }
                }
            }





        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if (myFtdiDevice.IsOpen || arduinoPort.IsOpen)
            {
                if (checkBox5.Checked || checkBox6.Checked)
                {
                    if (checkBox7.Checked || checkBox8.Checked)
                    {



                        bool antes = validarPruebaAntes();
                        bool despues = validarPruebaDespues();




                        if (updateTestMessage)
                        {
                            if (checkBox7.Checked)
                            {
                                if (antes == true)
                                {
                                    if (MessageBox.Show("Ya existe una prueba antes de la carga en el día. Desea reemplazarla ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        reemplazar = true;
                                        hacer = true;
                                        updateTestMessage = false;
                                    }
                                }
                                else
                                {
                                    hacer = true;
                                }


                            }

                            if (checkBox8.Checked)

                            {
                                if (despues == true && antes == true)
                                {
                                    if (MessageBox.Show("Ya existe una prueba despues de la carga en el día. Desea reemplzarla ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        reemplazar = true;
                                        hacer = true;
                                        updateTestMessage = false;
                                    }
                                }

                                if (despues == false && antes == true)
                                {
                                    hacer = true;

                                }

                                if (antes == false)
                                {
                                    MessageBox.Show("Debe realizar un prueba antes de la carga", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    hacer = false;
                                }
                            }

                        }




                        if (dataGridView2.Rows.Count <= 9)
                        {

                            if (hacer == true)
                            {
                                String val = "2"; //ascendente
                                if (checkBox5.Checked)
                                {
                                    val = "1"; //Descendente
                                }

                                if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                                {
                                    arduinoPort.DiscardOutBuffer();
                                    arduinoPort.Write(val);
                                    label23.Invoke(new System.Action(() => label23.Visible = true));
                                    bandera = true;
                                    desabilitarCheckBox();
                                    numericUpDown1.Enabled = false;
                                    button2.Enabled = false;
                                    button1.Enabled = false;


                                }

                                if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                                {
                                    sendCommandFTDI(val);

                                    label23.Invoke(new System.Action(() => label23.Visible = true));
                                    desabilitarCheckBox();
                                    numericUpDown1.Enabled = false;
                                    button2.Enabled = false;
                                    button1.Enabled = false;

                                    string result = readFromFTDI1Byte(val);

                                    dataGridView2.Invoke(new System.Action(() => dataGridView2.Rows.Add(result)));



                                    label23.Invoke(new System.Action(() => label23.Visible = false));
                                    button2.Invoke(new System.Action(() => button2.Enabled = true));
                                    button1.Invoke(new System.Action(() => button1.Enabled = true));
                                    EstadisticaCurso();

                                }






                            }
                        }
                        else
                        {
                            MessageBox.Show("Solo se admiten 10 muestras por cada prueba", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }


                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar en que momento se realiza la prueba", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                else
                {

                    MessageBox.Show("Debe seleccionar el tipo de prueba", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Conecte el visor a la PC.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private bool verificarVisor()
        {

            VerificarHardware res = new VerificarHardware();
            bool result = res.configurarHardware();

            return result;

        }

        private void sendCommandFTDI(String val)
        {

            if (myFtdiDevice.IsOpen)
            {

                myFtdiDevice.Purge(0);
                byte[] r = new byte[3];



                if (val == "2")//Ascendente
                {
                    r[0] = 1;
                    r[1] = 84;
                    r[2] = 85;
                }


                if (val == "1") //ascendente
                {
                    r[0] = 1;
                    r[1] = 80;
                    r[2] = 81;
                }


                UInt32 numBytesWritten = 3;
                ftStatus = myFtdiDevice.Write(r, r.Length, ref numBytesWritten);


            }
        }

        private string readFromFTDI1Byte(string val)
        {
            string entradaDatos = "";

            Application.DoEvents();
            UInt32 numBytesAvailable = 0;
            UInt32 numBytesExpected = 3;
            myFtdiDevice.Purge(0);


            while (numBytesAvailable < numBytesExpected)
            {

                ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);

                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {

                    throw new Exception("Failed to get number of bytes available to read; error: " + ftStatus);
                }



            }

            if (numBytesAvailable != numBytesExpected)
            {
                //throw new Exception("Error: Invalid data in buffer. (1350)");

                UInt32 numBytesRead1 = 5;
                byte[] rawData1 = new byte[numBytesExpected];
                ftStatus = myFtdiDevice.Read(rawData1, 5, ref numBytesRead1);
            }


            UInt32 numBytesRead = 0;
            byte[] rawData = new byte[numBytesExpected];
            ftStatus = myFtdiDevice.Read(rawData, numBytesAvailable, ref numBytesRead);


            string hex1 = rawData[1].ToString("X");
            string hex2 = rawData[2].ToString("X");
            //string res = re.ToString("X");

            //int hexa1 = int.Parse(res, System.Globalization.NumberStyles.HexNumber);
            //int de = Convert.ToInt32(re.ToString(), 16);
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(rawData);

            entradaDatos = hex1 + hex2;
            int temp = int.Parse(entradaDatos, System.Globalization.NumberStyles.HexNumber);
            entradaDatos = Math.Round(valorFTDI(temp, val), 1).ToString();
            //  entradaDatos = Encoding.ASCII.GetString(rawData, 0, 1).ToString();



            return entradaDatos;
        }



        private String pruebasIguales()
        {
            String res = "";
            String commnad = null;


            String y = DateTime.Now.Date.ToString("yyyy-MM-dd");


            /*   if(checkBox5.Checked )
               commnad = "select * from Pruebas inner join Test_Principal on Pruebas.Ncarnet = Test_Principal.Atleta where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and Test_Principal.Fecha='" + y + "' and TIPOMEDICION='ASCENDENTE'";


               if (checkBox6.Checked)
                   commnad = "select * from Pruebas inner join Test_Principal on Pruebas.Ncarnet = Test_Principal.Atleta where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and Test_Principal.Fecha='" + y + "' and TIPOMEDICION='DESCENDENTE'";
                   */

            commnad = "select * from Pruebas inner join Test_Principal on Pruebas.Ncarnet = Test_Principal.Atleta where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and Pruebas.FECHA='" + y + "' ";

            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm = new SQLiteCommand(commnad, c))
                {
                    using (SQLiteDataReader read = comm.ExecuteReader())
                    {
                        if (read.HasRows)
                        {
                            read.Read();
                            res = read["TIPOMEDICION"].ToString();
                        }

                    }
                }
            }
            return res;


        }





        private void desabilitarCheckBox()
        {
            checkBox5.Enabled = false;
            checkBox6.Enabled = false;
            checkBox7.Enabled = false;
            checkBox8.Enabled = false;

        }

        private void configurarPuertoArduino()
        {
            try
            {
                arduinoPort = new SerialPort();
                string[] puertosDisponibles = SerialPort.GetPortNames();
                if (puertosDisponibles.Length > 0)
                {
                    string port = puertosDisponibles[puertosDisponibles.Length - 1];
                    arduinoPort.PortName = port;
                    arduinoPort.BaudRate = 9600;
                    arduinoPort.Parity = Parity.None;
                    arduinoPort.DataBits = 8;
                    arduinoPort.Handshake = Handshake.None;
                    arduinoPort.RtsEnable = true;
                    arduinoPort.DataReceived += new SerialDataReceivedEventHandler(recibirArduino);
                    arduinoPort.Open();
                }
                else
                {
                    MessageBox.Show("El visor no está conectado a la PC. Para poder realizar la prueba del Flicker cierre la venta de prueba, conecte el visor y reabra la ventana nuevamente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }


            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrió un error en el puerto de comunicación. Por favor consulte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void openPorFTDI()
        {
            if (!myFtdiDevice.IsOpen)
            {


                UInt32 ftdiDeviceCount = 0;

                ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

                ///lista de dispositivos ftdi
                FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

                //asigna la lista de dispositivos
                ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);

                if (ftdiDeviceList.Length != 0)
                {

                    ftStatus = myFtdiDevice.OpenBySerialNumber(ftdiDeviceList[0].SerialNumber);


                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                    {
                        ftStatus = myFtdiDevice.SetBaudRate(9600);
                        ftStatus = myFtdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8,
                            FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_NONE);
                        // port = true;
                    }
                    //else
                    //    conection = 2;
                    // MessageBox.Show("Ocurrio un error al abrir el puerto de comunicacion.Reinicie la aplicacion");
                }
                //else
                //    // MessageBox.Show("Verifique si el dispositivo esta conectado");
                //    conection = 1;
            }

            if (myFtdiDevice.IsOpen)
            {
                //int y = 0;
            }

            //return port;
        }


        private void recibirArduino(object sender, SerialDataReceivedEventArgs e)
        {

            SerialPort sp = (SerialPort)sender;
            string entradaDatos = sp.ReadExisting(); // Almacena los datos recibidos en la variable tipo string.
            entradaDatos = entradaDatos.Substring(0, 5);                             // Console.WriteLine("Dato recibido desde Arduino: " + entradaDatos); // Muestra en pantalla los datos recibidos.

            if (bandera)
            {

                dataGridView2.Invoke(new System.Action(() => dataGridView2.Rows.Add(entradaDatos)));

                arduinoPort.DiscardInBuffer();
                bandera = false;
                label23.Invoke(new System.Action(() => label23.Visible = false));
                button2.Invoke(new System.Action(() => button2.Enabled = true));
                button1.Invoke(new System.Action(() => button1.Enabled = true));
                EstadisticaCurso();


            }


        }

        private void EstadisticaCurso()
        {
            int i = 0;
            double sumatoria = 0;
            double media = 0;
            double desviacion = 0;
            double porciento = 0;
            List<double> listDesviacion = new List<double>();

            while (i < dataGridView2.Rows.Count)
            {
                double Temp = Convert.ToDouble(dataGridView2.Rows[i].Cells[0].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);

                listDesviacion.Add(Temp);
                sumatoria += Temp;

                i++;

            }



            label44.Invoke(new System.Action(() => label44.Text = Convert.ToString(Math.Round(listDesviacion.Max() - listDesviacion.Min(), 2))));

            media = Convert.ToDouble(Math.Round(sumatoria / i, 2), System.Globalization.CultureInfo.InvariantCulture);
            double spinner = Convert.ToInt32(numericUpDown1.Value);
            porciento = Math.Round(Convert.ToInt32(numericUpDown1.Value) * media / 100, 2);
            desviacion = desviacionFunc(listDesviacion, media);

            label33.Invoke(new System.Action(() => label33.Text = media.ToString()));

            labelDesviacion.Invoke(new System.Action(() => labelDesviacion.Text = desviacion.ToString()));
        }

        private double desviacionFunc(List<double> list, double media)
        {
            double desviacion = 0;
            double num = 0;
            List<double> listTemp = new List<double>();
            foreach (var item in list)
            {

                num += Math.Pow(item - media, 2);

            }


            double varianza = num / (list.Count - 1);
            desviacion = Math.Round(Math.Sqrt(varianza), 2);

            if (desviacion.ToString() == "NaN" || desviacion.ToString() == "NeuN")
                desviacion = 0;


            return desviacion;

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    bool res = true;

                    if (pruebasIguales() == "ASCENDENTE" && checkBox6.Checked || pruebasIguales() == "DESCENDENTE" && checkBox5.Checked)

                    {
                        if (MessageBox.Show("El tipo de medicion de la prueba es diferente.Quisiera guardar la prueba?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)

                        {
                            dataGridView2.Rows.Clear();
                            res = false;
                            checkBox5.Enabled = true;
                            checkBox6.Enabled = true;
                            checkBox7.Enabled = true;
                            checkBox8.Enabled = true;
                            updateTestMessage = true;
                            hacer = false;
                            reemplazar = false;


                        }



                    }



                    if (res)
                    {
                        salvarDatos();
                        dataGridView2.Rows.Clear();
                        label33.Text = "0.0";
                        labelDesviacion.Text = "0.0";

                        label44.Text = "0.0";
                        checkBox5.Checked = false;
                        checkBox6.Checked = false;
                        checkBox7.Checked = false;
                        checkBox8.Checked = false;

                        habilitarCheckBox();
                        numericUpDown1.Enabled = true;

                        updateTestMessage = true;
                        hacer = false;
                        reemplazar = false;

                        mostrarPruebasDiaAtleta();
                        CalcularMediaHistorica();


                        EstadisticaValores();

                        calcularMediasHistoricas();

                        //aqui guardo la prueba en a tabla principal
                        GuardaPruebas();

                        BuscarPruebasDia();

                        MessageBox.Show("La prueba fue añadida correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Debe existir muestras realizadas en la prueba", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error consulte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private double valorFTDI(double puntero, string val)
        {
            double valor = 0;
            bool res = false;
            int valIni = 0;

            if (val == "1")
            {
                valor = 10.0;
                valIni = 4096;
            }
            else
            {
                valor = 60.0;
                valIni = 5097;
            }

            while (!res)
            {
                if (valIni == puntero)
                {
                    res = true;
                }
                else
                {

                    if (val == "1")
                    {
                        valIni += 1;
                        if (valIni % 2 == 0)
                            valor += 0.1;
                    }
                    else
                    {
                        valIni -= 1;
                        if (valIni % 2 != 0)
                            valor -= 0.1;
                    }




                }

            }

            if (val == "2")
                valor += 0.1;



                return valor;
        }

        private void habilitarCheckBox()
        {
            checkBox5.Enabled = true;
            checkBox6.Enabled = true;
            checkBox7.Enabled = true;
            checkBox8.Enabled = true;
        }

        private void salvarDatos()
        {

            try
            {
                double suma = 0.0;
                String frecuencia = null;
                double media = 0.0;
                double porciento = 0.0;
                double diferencia = 0.0;
                String tipoMedicion = null;
                double des = 0.0;

                String fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");
                String hora = DateTime.Now.ToString("hh:mm:ss");

                if (checkBox5.Checked)
                {
                    tipoMedicion = "ASCENDENTE";
                }

                if (checkBox6.Checked)
                {
                    tipoMedicion = "DESCENDENTE";
                }

                if (checkBox7.Checked)
                {
                    frecuencia = "A";
                }

                if (checkBox8.Checked)
                {
                    frecuencia = "D";
                }

                // los valosres de la medicion los


                String valores = ",'A','B','C','D','E','F','G','H','I','J' )";


                /*  String insert2 = "Insert into Pruebas (Ncarnet,FECHA,HORA,TFRECUENCIA,KMEDICIONES,MEDIA,SUMATORIA,DESVIACION,PORCIENTO5MEDIA" +
                      ",DIFERENCIAPROMEDIO,TIPOMEDICION,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,M11,M12,M13,M14,M15,M16,M17,M18,M19,M20)" +
            " values('" + comboBox1.SelectedValue.ToString() + "','" + fecha + "','" + Hora + "','" + frecuencia + "','" + mediciones + "','" + media + "','" + sum + "','" + des + "'," +
            "'" + porciento + "','" + diferencia + "','" + tiempo + "','" + M1 + "','" + M2 + "','" + M3 + "','" + M4 + "'," +
            "'" + M5 + "','" + M6 + "','" + M7 + "','" + M8 + "','" + M9 + "','" + M10 + "','" + M11 + "','" + M12 + "','" + M13 + "','" + M14 + "'," +
            "'" + M15 + "','" + M16 + "','" + M17 + "','" + M18 + "','" + M19 + "','" + M20 + "')";  */

                List<String> list = new List<string>(new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"/*, "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T"*/ });
                List<double> listDesvi = new List<double>();


                int i = 1;
                int count = 0;
                while (i <= 10)
                {
                    if (count < dataGridView2.Rows.Count)
                    {
                        String value = dataGridView2.Rows[count].Cells["Column30"].Value.ToString();

                        valores = valores.Replace(list[count], value);
                        String temporal = dataGridView2.Rows[count].Cells[0].Value.ToString();
                        double temp = Convert.ToDouble(temporal, System.Globalization.CultureInfo.InvariantCulture);

                        suma += temp;
                        listDesvi.Add(temp);
                    }
                    else
                    {
                        valores = valores.Replace(list[count], "0");
                    }

                    i++;
                    count++;
                }


                media = Math.Round(Convert.ToDouble(suma) / Convert.ToDouble(dataGridView2.Rows.Count), 2);
                suma = Convert.ToDouble(suma);

                porciento = Math.Round(5 * media / 100, 2);
                des = desviacionFunc(listDesvi, media);
                String command = null;
                String commandTemp = null;
                String rango = label44.Text.ToString();

                if (reemplazar == true)
                {
                    command = "update Pruebas set  Ncarnet= '" + comboBox1.SelectedValue.ToString() + "' ,FECHA= '" + fecha + "',HORA= '" + hora + "',TFRECUENCIA= '" + frecuencia + "',KMEDICIONES= '" + dataGridView2.Rows.Count + "',MEDIA= '" + media.ToString() + "',SUMATORIA= '" + suma.ToString() + "',DESVIACION= '" + des.ToString() + "',PORCIENTO5MEDIA= '" + porciento.ToString() + "',DIFERENCIAPROMEDIO= '" + diferencia.ToString() + "',TIPOMEDICION= '" + tipoMedicion.ToString() + "',RANGO= '" + rango.ToString() + "', ";

                    int cont = 0;
                    int k = 1;
                    while (cont < 10)
                    {
                        String temp = "";

                        if (cont < dataGridView2.Rows.Count)
                        {
                            temp = "M" + k + "=" + "'" + dataGridView2.Rows[cont].Cells["Column30"].Value.ToString() + "',";
                        }
                        else
                        {
                            temp = "M" + k + "=" + "'" + 0.0 + "',";
                        }

                        command = command + temp;
                        cont++;
                        k++;

                    }

                    // TIPOMEDICION='" + tipoMedicion + "' and 

                    command = command.Substring(0, command.Length - 1);
                    command = command + " where TFRECUENCIA ='" + frecuencia + "' and  FECHA='" + fecha + "' and  Ncarnet='" + comboBox1.SelectedValue.ToString() + "'";
                    commandTemp = command;

                }
                else
                {
                    // command = "Insert into Pruebas (Ncarnet,FECHA,HORA,TFRECUENCIA,KMEDICIONES,MEDIA,SUMATORIA,DESVIACION,PORCIENTO5MEDIA,DIFERENCIAPROMEDIO,TIPOMEDICION,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,M11,M12,M13,M14,M15,M16,M17,M18,M19,M20) values ('" + comboBox1.SelectedValue.ToString() + "','" + fecha + "','" + hora + "','" + frecuencia + "','" + dataGridView2.Rows.Count + "','" + media + "','" + suma + "','" + des + "','" + porciento + "','" + diferencia + "', '" + tipoMedicion + "'";
                    command = "Insert into Pruebas (Ncarnet,FECHA,HORA,TFRECUENCIA,KMEDICIONES,MEDIA,SUMATORIA,DESVIACION,PORCIENTO5MEDIA,DIFERENCIAPROMEDIO,TIPOMEDICION,RANGO,M1,M2,M3,M4,M5,M6,M7,M8,M9,M10) values ('" + comboBox1.SelectedValue.ToString() + "','" + fecha + "','" + hora + "','" + frecuencia.ToString() + "','" + dataGridView2.Rows.Count + "','" + media.ToString() + "','" + suma.ToString() + "','" + des.ToString() + "','" + porciento.ToString() + "','" + diferencia.ToString() + "', '" + tipoMedicion.ToString() + "','" + rango.ToString() + "'";
                    commandTemp = command + valores;
                    isNewTest = true;
                }



                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm = new SQLiteCommand(commandTemp, c))
                    {
                        comm.ExecuteNonQuery();
                    }

                }




                if (frecuencia == "A" && radioButton11.Checked == false)
                    radioButton11.Checked = true;


                if (frecuencia == "D" && radioButton15.Checked == false)
                    radioButton15.Checked = true;


                filtrarTabla();

            }
            catch (Exception e)
            {

                MessageBox.Show("Ocurrio un error consulte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }





        }



        private bool validarPruebaAntes()
        {

            bool res = false;


            String y = DateTime.Now.Date.ToString("yyyy-MM-dd");
            String comand = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='A' ";



            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm = new SQLiteCommand(comand, c))
                {
                    using (SQLiteDataReader read = comm.ExecuteReader())
                    {
                        if (read.HasRows)
                            res = true;


                    }
                }
            }



            return res;
        }


        private bool validarPruebaDespues()
        {

            bool res = false;


            String y = DateTime.Now.Date.ToString("yyyy-MM-dd");
            String comand = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='D' ";

            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm = new SQLiteCommand(comand, c))
                {
                    using (SQLiteDataReader read = comm.ExecuteReader())
                    {
                        if (read.HasRows)
                            res = true;


                    }
                }
            }
            return res;
        }


        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                filtrarTabla();
            }
            else
            {
                checkBox1.Checked = false;
            }
            */
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                filtrarTabla();
            }
            else
            {
                checkBox2.Checked = false;
            }
            */
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            /*

           if (checkBox3.Checked)
           {
               checkBox4.Checked = false;
               filtrarTabla();
           }
           else
           {
               checkBox3.Checked = false;
           }
           */
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            /*  if (checkBox4.Checked)
              {
                  checkBox3.Checked = false;
                  filtrarTabla();

              }
              else
              {
                  checkBox4.Checked = false;
              }
              */

        }



        public void filtrarTabla()
        {
            String command = crearFiltro();
            dataGridView1.Rows.Clear();



            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm = new SQLiteCommand(command, c))
                {
                    using (SQLiteDataReader read = comm.ExecuteReader())
                    {



                        while (read.Read())
                        {


                            dataGridView1.Rows.Add(new object[] {

            read.GetValue(read.GetOrdinal("FECHA")),  // Or column name like this
            read.GetValue(read.GetOrdinal("HORA")),
            read.GetValue(read.GetOrdinal("TFRECUENCIA")),
            read.GetValue(read.GetOrdinal("TIPOMEDICION")),
            read.GetValue(read.GetOrdinal("MEDIA")),
            read.GetValue(read.GetOrdinal("SUMATORIA")),
            read.GetValue(read.GetOrdinal("DESVIACION")),
            read.GetValue(read.GetOrdinal("RANGO")),
            read.GetValue(read.GetOrdinal("M1")),
            read.GetValue(read.GetOrdinal("M2")),
            read.GetValue(read.GetOrdinal("M3")),
            read.GetValue(read.GetOrdinal("M4")),
            read.GetValue(read.GetOrdinal("M5")),
            read.GetValue(read.GetOrdinal("M6")),
            read.GetValue(read.GetOrdinal("M7")),
            read.GetValue(read.GetOrdinal("M8")),
            read.GetValue(read.GetOrdinal("M9")),
            read.GetValue(read.GetOrdinal("M10")),

               read.GetValue(read.GetOrdinal("Id"))


            });

                        }
                    }


                }

            }


            CalcularValoresTabla();
        }

        private void CalcularValoresTabla()
        {
            double media = 0;
            double sumatoria = 0;
            List<double> listDes = new List<double>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int i = 8;

                while (i < row.Cells.Count - 1)
                {
                    double temp = Convert.ToDouble(row.Cells[i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    listDes.Add(temp);
                    sumatoria += temp;
                    media++;
                    i++;

                }

            }

        }



        private void CalcularMediaHistorica()
        {
            double media = 0;
            double sumatoria = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int i = 8;

                while (i < row.Cells.Count - 1)
                {
                    double temp = Convert.ToDouble(row.Cells[i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);

                    sumatoria += temp;
                    media++;
                    i++;

                }
            }


        }



        public String crearFiltro()
        {
            String command = null;
            String y = "";

            if (comboBoxFecha.SelectedValue != null)
                y = comboBoxFecha.SelectedValue.ToString();

            if (y != "")
            {

                if (checkBox1.Checked)
                {

                    command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='A' ";

                    if (checkBox1.Checked && checkBox3.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='A' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox1.Checked && checkBox4.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='A' and TIPOMEDICION='DESCENDENTE' ";
                    }
                }

                if (checkBox2.Checked)
                {

                    command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='D' ";
                    if (checkBox2.Checked && checkBox3.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='D' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox2.Checked && checkBox4.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='D' and TIPOMEDICION='DESCENDENTE' ";
                    }
                }


                if (checkBox3.Checked)
                {

                    command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and TIPOMEDICION='ASCENDENTE' ";

                    if (checkBox3.Checked && checkBox1.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='A' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox3.Checked && checkBox2.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='D' and TIPOMEDICION='ASCENDENTE' ";
                    }
                }



                if (checkBox4.Checked)
                {

                    command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and TIPOMEDICION='DESCENDENTE' ";

                    if (checkBox4.Checked && checkBox1.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='A' and TIPOMEDICION='DESCENDENTE' ";

                    }

                    if (checkBox4.Checked && checkBox2.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "' and  TFRECUENCIA='D' and TIPOMEDICION='DESCENDENTE' ";
                    }
                }



                if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox3.Checked && !checkBox4.Checked)
                {


                    command = "select* from Pruebas where Ncarnet = '" + comboBox1.SelectedValue.ToString().ToString() + "' and FECHA='" + y + "' ";
                }
            }
            else
            {

                if (checkBox1.Checked)
                {

                    command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'   and  TFRECUENCIA='A' ";

                    if (checkBox1.Checked && checkBox3.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'  and  TFRECUENCIA='A' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox1.Checked && checkBox4.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'  and  TFRECUENCIA='A' and TIPOMEDICION='DESCENDENTE' ";
                    }
                }

                if (checkBox2.Checked)
                {

                    command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'   and  TFRECUENCIA='D' ";
                    if (checkBox2.Checked && checkBox3.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and  TFRECUENCIA='D' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox2.Checked && checkBox4.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and   TFRECUENCIA='D' and TIPOMEDICION='DESCENDENTE' ";
                    }
                }


                if (checkBox3.Checked)
                {

                    command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and   TIPOMEDICION='ASCENDENTE' ";

                    if (checkBox3.Checked && checkBox1.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and   TFRECUENCIA='A' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox3.Checked && checkBox2.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and   TFRECUENCIA='D' and TIPOMEDICION='ASCENDENTE' ";
                    }
                }



                if (checkBox4.Checked)
                {

                    command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and  TIPOMEDICION='DESCENDENTE' ";

                    if (checkBox4.Checked && checkBox1.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and   TFRECUENCIA='A' and TIPOMEDICION='DESCENDENTE' ";

                    }

                    if (checkBox4.Checked && checkBox2.Checked)
                    {
                        command = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and  TFRECUENCIA='D' and TIPOMEDICION='DESCENDENTE' ";
                    }
                }



                if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox3.Checked && !checkBox4.Checked)
                {

                    command = "select* from Pruebas where Ncarnet = '" + comboBox1.SelectedValue.ToString().ToString() + "' ";
                }

            }



            return command;

        }

        private void CheckBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;

            }
            else
            {
                checkBox1.Checked = false;

            }
            filtrarTabla();
        }

        private void CheckBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;

            }
            else
            {
                checkBox2.Checked = false;
            }
            filtrarTabla();
        }

        private void CheckBox3_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {

                checkBox4.Checked = false;

            }
            else
            {
                checkBox3.Checked = false;
            }


            filtrarTabla();
        }

        private void CheckBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox3.Checked = false;


            }
            else
            {
                checkBox4.Checked = false;

            }
            filtrarTabla();

        }


        public void EstadisticaValores()
        {

            String y = DateTime.Now.Date.ToString("yyyy-MM-dd");

            if (comboBox2.SelectedValue != null)
                y = comboBox2.SelectedValue.ToString();


            label142.Text = "0.0";
            label143.Text = "0.0";
            label145.Text = "0.0";
            label37.Text = "0.0";

            String comand = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and FECHA='" + y + "'";

            double before = 0;
            double after = 0;

            int i = 0;
            int k = 0;

            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand(comand, c))
                {
                    using (SQLiteDataReader read = comm1.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            int count = 1;
                            while (count <= 10)

                            {
                                if (read["TFRECUENCIA"].ToString() == "A" && read["M" + count].ToString() != "0")
                                {
                                    before += Convert.ToDouble(read["M" + count], System.Globalization.CultureInfo.InvariantCulture);
                                    i++;
                                }

                                if (read["TFRECUENCIA"].ToString() == "D" && read["M" + count].ToString() != "0")
                                {
                                    after += Convert.ToDouble(read["M" + count], System.Globalization.CultureInfo.InvariantCulture);
                                    k++;
                                }

                                count++;
                            }
                        }

                    }

                }

            }


            double mediaAntes = Math.Round(Convert.ToDouble(before / i), 2);
            double mediaDespues = Math.Round(Convert.ToDouble(after / k), 2);



            double diferencia = Math.Round(mediaDespues - mediaAntes, 2);
            double porCientoDiferencia = Convert.ToInt32(numericUpDown1.Value) * mediaAntes / 100;

            if (mediaAntes.ToString() != "NaN" && mediaAntes.ToString() != "NeuN")
                label142.Text = Math.Round(mediaAntes, 2).ToString();

            if (mediaDespues.ToString() != "NaN" && mediaDespues.ToString() != "NeuN")
                label143.Text = Math.Round(mediaDespues, 2).ToString();


            if (diferencia.ToString() != "NaN" && diferencia.ToString() != "NeuN")
                label145.Text = Math.Round(diferencia, 2).ToString();



            if (porCientoDiferencia.ToString() != "NaN" && porCientoDiferencia.ToString() != "0.0" && porCientoDiferencia.ToString() != "NeuN")
                label37.Text = Math.Round(porCientoDiferencia, 2).ToString();


            if (diferencia < 0)
            {
                double temp = diferencia * (-1);
                if (temp > porCientoDiferencia)

                {

                    label145.ForeColor = System.Drawing.Color.Red;
                    label144.ForeColor = System.Drawing.Color.Red;

                }
            }





        }


        private void calcularMediasHistoricas()
        {


            try
            {
                String command1 = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and TIPOMEDICION='ASCENDENTE' and  TFRECUENCIA='A' ";
                String command2 = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and TIPOMEDICION='DESCENDENTE' and  TFRECUENCIA='A' ";

                String command3 = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and TIPOMEDICION='ASCENDENTE' and  TFRECUENCIA='D' ";
                String command4 = "select * from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' and TIPOMEDICION='DESCENDENTE' and  TFRECUENCIA='D' ";






                double mediaAsceAntes = 0.0;
                double mediaDescAntes = 0.0;
                double mediaAsceDespues = 0.0;
                double mediaDesceDesp = 0.0;

                int contador1 = 0;
                int contador2 = 0;
                int contador3 = 0;
                int contador4 = 0;

                double acumulador1 = 0.0;
                double acumulador2 = 0.0;
                double acumulador3 = 0.0;
                double acumulador4 = 0.0;



                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(command1, c))
                    {
                        using (SQLiteDataReader reader1 = comm1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                int i = 1;
                                while (i <= 10)
                                {
                                    double temp = Convert.ToDouble(reader1["M" + i].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                                    if (temp != 0)
                                    {
                                        acumulador1 += Convert.ToDouble(reader1["M" + i].ToString(), System.Globalization.CultureInfo.InvariantCulture);

                                        contador1++;
                                    }
                                    i++;

                                }

                            }

                        }


                    }
                }


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(command2, c))
                    {
                        using (SQLiteDataReader reader2 = comm1.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                int i = 1;
                                while (i <= 10)
                                {
                                    double temp = Convert.ToDouble(reader2["M" + i].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                                    if (temp != 0)
                                    {
                                        acumulador2 += Convert.ToDouble(reader2["M" + i].ToString(), System.Globalization.CultureInfo.InvariantCulture);

                                        contador2++;
                                    }
                                    i++;

                                }

                            }

                        }


                    }
                }

                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(command3, c))
                    {
                        using (SQLiteDataReader reader3 = comm1.ExecuteReader())
                        {
                            while (reader3.Read())
                            {
                                int i = 1;
                                while (i <= 10)
                                {
                                    double temp = Convert.ToDouble(reader3["M" + i].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                                    if (temp != 0)
                                    {
                                        acumulador3 += Convert.ToDouble(reader3["M" + i].ToString(), System.Globalization.CultureInfo.InvariantCulture);

                                        contador3++;
                                    }
                                    i++;

                                }

                            }

                        }


                    }
                }

                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(command4, c))
                    {
                        using (SQLiteDataReader reader4 = comm1.ExecuteReader())
                        {
                            while (reader4.Read())
                            {
                                int i = 1;
                                while (i <= 10)
                                {
                                    double temp = Convert.ToDouble(reader4["M" + i].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                                    if (temp != 0)
                                    {
                                        acumulador4 += Convert.ToDouble(reader4["M" + i].ToString(), System.Globalization.CultureInfo.InvariantCulture);

                                        contador4++;
                                    }
                                    i++;

                                }

                            }

                        }


                    }
                }




                if (acumulador1 != 0)
                    mediaAsceAntes = Convert.ToDouble(Math.Round(acumulador1 / contador1, 2), System.Globalization.CultureInfo.InvariantCulture);
                if (acumulador2 != 0)
                    mediaDescAntes = Convert.ToDouble(Math.Round(acumulador2 / contador2, 2), System.Globalization.CultureInfo.InvariantCulture);
                if (acumulador3 != 0)
                    mediaAsceDespues = Convert.ToDouble(Math.Round(acumulador3 / contador3, 2), System.Globalization.CultureInfo.InvariantCulture);
                if (acumulador4 != 0)
                    mediaDesceDesp = Convert.ToDouble(Math.Round(acumulador4 / contador4, 2), System.Globalization.CultureInfo.InvariantCulture);


                label43.Text = Convert.ToDouble(Math.Round(mediaAsceAntes, 2), System.Globalization.CultureInfo.InvariantCulture).ToString();
                label147.Text = Math.Round(Convert.ToDouble(mediaAsceDespues, System.Globalization.CultureInfo.InvariantCulture), 2).ToString();
                label106.Text = Convert.ToString(Math.Round(Convert.ToDouble(mediaAsceDespues - mediaAsceAntes, System.Globalization.CultureInfo.InvariantCulture), 2));


                label114.Text = Math.Round(Convert.ToDouble(mediaDescAntes, System.Globalization.CultureInfo.InvariantCulture), 2).ToString();
                label115.Text = Math.Round(Convert.ToDouble(mediaDesceDesp, System.Globalization.CultureInfo.InvariantCulture), 2).ToString();
                label116.Text = Math.Round(Convert.ToDouble(mediaDesceDesp - mediaDescAntes, System.Globalization.CultureInfo.InvariantCulture), 2).ToString();



            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }





        }


        private void Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                if (MessageBox.Show("Desea eliminar la muestra ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);

                if (dataGridView2.Rows.Count == 0)
                {
                    checkBox5.Enabled = true;
                    checkBox6.Enabled = true;
                    checkBox7.Enabled = true;
                    checkBox8.Enabled = true;
                    updateTestMessage = true;

                }
            }

            else
                MessageBox.Show("Deben  exitir valores en tabla", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);


        }

        private void Prueba_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
            {
                myFtdiDevice.Close();

            }

            if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
            {
                arduinoPort.Close();

            }

        }

        private void GuardaPruebas()
        {

            if (isNewTest)
            {

                String idAtleta = "";
                if (comboBox1.Text != "")
                    idAtleta = comboBox1.SelectedValue.ToString();

                String fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");
                String hora = DateTime.Now.ToString("hh:mm:ss");



                String idPregunta3 = null;
                String FlickerA = null;
                String Periodo = null;
                String FlickerD = null;
                String Borg = null;
                String ppA = null;
                String ppD = null;



                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand("select id from Pregunta3 where Fecha='" + fecha + "'  and Ncarnet='" + idAtleta + "' ORDER  BY id Desc", c))
                    {
                        using (SQLiteDataReader read = comm1.ExecuteReader())
                        {
                            read.Read();
                            if (read.HasRows)
                                idPregunta3 = read["id"].ToString();

                        }


                    }

                }


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand("select Id from Pruebas where TFRECUENCIA='A' and FECHA='" + fecha + "' and Ncarnet='" + idAtleta + "'  ORDER  BY id Desc", c))
                    {
                        using (SQLiteDataReader reader2 = comm1.ExecuteReader())
                        {
                            reader2.Read();
                            if (reader2.HasRows)
                                FlickerA = reader2["Id"].ToString();

                        }
                    }

                }


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand("select Id from Pruebas where TFRECUENCIA='D' and FECHA='" + fecha + "' and Ncarnet='" + idAtleta + "'  ORDER  BY id Desc", c))
                    {
                        using (SQLiteDataReader reader4 = comm1.ExecuteReader())
                        {
                            reader4.Read();
                            if (reader4.HasRows)
                                FlickerD = reader4["Id"].ToString();

                        }


                    }

                }


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand("select Id from Borg where Fecha='" + fecha + "' and Ncarnet='" + idAtleta + "'  ORDER  BY id Desc", c))
                    {
                        using (SQLiteDataReader reader5 = comm1.ExecuteReader())
                        {

                            reader5.Read();
                            if (reader5.HasRows)
                                Borg = reader5["Id"].ToString();

                        }

                    }

                }


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand("select Id from PerfilPolaridad where   TFrecuencia='A' and  Fecha='" + fecha + "' and Ncarnet='" + idAtleta + "'  ORDER  BY id Desc", c))
                    {
                        using (SQLiteDataReader reader6 = comm1.ExecuteReader())
                        {
                            reader6.Read();
                            if (reader6.HasRows)
                                ppA = reader6["Id"].ToString();

                        }

                    }

                }


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand("select Id from PerfilPolaridad where    TFrecuencia='D' and Fecha='" + fecha + "' and Ncarnet='" + idAtleta + "'  ORDER  BY id Desc", c))
                    {
                        using (SQLiteDataReader reader7 = comm1.ExecuteReader())
                        {
                            reader7.Read();
                            if (reader7.HasRows)
                                ppD = reader7["Id"].ToString();

                        }

                    }

                }


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand("select Id from Periodo ORDER  BY id ASC", c))
                    {
                        using (SQLiteDataReader reader3 = comm1.ExecuteReader())
                        {
                            reader3.Read();

                            if (reader3.HasRows)
                                Periodo = reader3["id"].ToString();

                        }


                    }

                }




                String comand = "";

                if (findTest() == false)
                {

                    comand = "Insert into  Test_Principal  (Flicker_Antes , Pregunta_3 ,Fecha,Flicker_Despues,Periodo,Borg,Atleta,PerfilPolaridad_Antes,PerfilPolaridad_Despues)" +
                    " values('" + FlickerA + "', '" + idPregunta3 + "','" + fecha + "' ,'" + FlickerD + "','" + Periodo + "','" + Borg + "','" + idAtleta + "','" + ppA + "','" + ppD + "' )";


                }
                else
                {
                    comand = "update Test_Principal set Flicker_Antes='" + FlickerA + "' ,  Pregunta_3='" + idPregunta3 + "',  Fecha='" + fecha + "' ,  Flicker_Despues='" + FlickerD + "',  Periodo='" + Periodo + "' ,  Borg='" + Borg + "', PerfilPolaridad_Antes='" + ppA + "'  ,  PerfilPolaridad_Despues='" + ppD + "'  where Fecha= '" + fecha + "'  and Atleta=  '" + idAtleta + "' ";

                }

                comand = comand.Replace("''", "null");


                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm1 = new SQLiteCommand(comand, c))
                    {
                        comm1.ExecuteNonQuery();

                    }

                }



            }

        }

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }



        private void ComboBox2_DropDown(object sender, EventArgs e)
        {
            SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct FECHA from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' ", Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();
            categoria.Fill(data);
            comboBoxFecha.DataSource = data;
            comboBoxFecha.DisplayMember = "FECHA";
            comboBoxFecha.ValueMember = "FECHA";

        }

        public bool findElement(List<String> lis, String temp)
        {
            bool result = false;

            foreach (var item in lis)
            {
                if (item == temp)
                    result = true;
            }

            return result;

        }

        private void ComboBoxFecha_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filtrarTabla();
        }


        private void NumericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            try
            {


                if (comboBox1.SelectedValue != null)
                {
                    if (verificarRadioButton())
                    {
                        String commandDb = null;
                        String fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        String Ncarnet = comboBox1.SelectedValue.ToString();
                        bool pregunta = existe3Pregunta();
                        bool entro = false;

                        if (pregunta == true)
                        {

                            if (MessageBox.Show("Ya existe una prueba realizada en el día. Desea reemplazarla ?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                commandDb = "update Pregunta3 set Respuesta1='" + calidadSueno + "' ,  Respuesta2='" + bebida + "',  Respuesta3='" + actividad + "'    where Fecha= '" + fecha + "' and Ncarnet = '" + Ncarnet + "' ";
                                entro = true;
                            }

                        }


                        if (pregunta == false)
                        {
                            commandDb = "Insert into Pregunta3 (Pregunta1, Pregunta2 ,Pregunta3,Respuesta1,Respuesta2,Respuesta3,Fecha,Ncarnet)" +
                             " values('" + label36.Text + "', '" + label39.Text + "','" + label40.Text + "' ,'" + calidadSueno + "','" + bebida + "','" + actividad + "','" + fecha + "','" + Ncarnet + "')";
                            isNewTest = true;
                            entro = true;
                        }

                        if (entro)
                        {



                            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                            {
                                c.Open();
                                using (SQLiteCommand comm = new SQLiteCommand(commandDb, c))
                                {
                                    comm.ExecuteNonQuery();

                                }
                            }



                            ClearRadioButton();

                            //guardar las pruebas en la tabla principal
                            GuardaPruebas();

                            //Mostrar las pruebas que ha realizado el atleta
                            mostrarPruebasDiaAtleta();

                            BuscarPruebasDia();
                            MessageBox.Show("Los datos de la prueba se han guardado correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Debe responder todas las preguntas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                else
                    MessageBox.Show("Debe seleccionar un atleta en la pestana de Atleta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }



            catch (Exception es)
            {

                MessageBox.Show("Ocurrio un error consulte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool verificarRadioButton()
        {
            bool res = false;

            int count = 0;

            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked || radioButton5.Checked)
                count++;


            if (radioButton6.Checked || radioButton7.Checked)
                count++;

            if (radioButton8.Checked || radioButton9.Checked)
                count++;

            if (count == 3)
                res = true;

            return res;
        }

        private void ClearRadioButton()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            radioButton7.Checked = false;
            radioButton8.Checked = false;
            radioButton9.Checked = false;
        }

        private bool existe3Pregunta()
        {
            bool res = false;
            String fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");
            String Ncarnet = comboBox1.SelectedValue.ToString();
            String commandDb = "select * from Pregunta3 where Fecha = '" + fecha + "' and Ncarnet=  '" + Ncarnet + "'";



            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand command = new SQLiteCommand(commandDb, c))
                {

                    using (SQLiteDataReader read = command.ExecuteReader())
                    {
                        if (read.HasRows)
                            res = true;


                    }


                }
            }



            return res;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                calidadSueno = "Mal";
            }

            if (radioButton2.Checked)
            {
                calidadSueno = "Regular";
            }

            if (radioButton3.Checked)
            {
                calidadSueno = "Bien";
            }

            if (radioButton4.Checked)
            {
                calidadSueno = "Muy Bien";
            }

            if (radioButton5.Checked)
            {
                calidadSueno = "Excelente";
            }
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                bebida = "Si";
            }

            if (radioButton7.Checked)
            {
                bebida = "No";
            }
        }

        private void RadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                actividad = "Si";
            }

            if (radioButton8.Checked)
            {
                actividad = "No";
            }
        }

        private void Button5_Click_1(object sender, EventArgs e)
        {
            llenarTablaFlicker();
            CalcularValoresTabla();
            CalcularMediaHistorica();


            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            comboBoxFecha.DataSource = null;


        }








        private void Button9_Click(object sender, EventArgs e)
        {

            try
            {
                if (comboBox1.SelectedValue != null)
                {

                    if (validar)
                    {
                        String Ncarnet = comboBox1.SelectedValue.ToString();
                        bool hasOne = false;



                        String fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");
                        bool hacer = false;
                        String commandS = "";

                        using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                        {
                            c.Open();
                            using (SQLiteCommand comm = new SQLiteCommand("select * from Borg where Fecha='" + fecha + "' and   Ncarnet='" + Ncarnet + "' ", c))
                            {
                                using (SQLiteDataReader reader = comm.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                        hasOne = true;



                                    if (hasOne)
                                    {
                                        if (MessageBox.Show("Ya existe una prueba realizada en el día. Desea reemplazarla ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                        {
                                            commandS = " update Borg set Esfuerzo= '" + esfuerzo + "', Puntuacion='" + puntos + "' where Fecha ='" + fecha + "' and Ncarnet='" + Ncarnet + "'  ";
                                            hacer = true;
                                        }
                                    }
                                    else
                                    {

                                        commandS = "Insert into Borg(Esfuerzo, Puntuacion , Fecha,Ncarnet) values('" + esfuerzo + "', '" + puntos + "','" + fecha + "' ,'" + Ncarnet + "')";
                                        hacer = true;
                                        isNewTest = true;
                                    }




                                }

                            }


                        }


                        if (hacer)
                        {
                            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                            {
                                c.Open();
                                using (SQLiteCommand comm = new SQLiteCommand(commandS, c))
                                {
                                    comm.ExecuteNonQuery();
                                }

                                radioButton16.Checked = true;

                                mostrarPruebasDiaAtleta();

                                GuardaPruebas();
                                BuscarPruebasDia();
                                validar = false;
                                clearCheck();
                                MessageBox.Show("Los datos se han guardado satisfactoriamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }


                    }
                    else
                        MessageBox.Show("Debe puntuar una calificación", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                    MessageBox.Show("Debe seleccionar un atleta en la pestana de Atleta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error consulte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }



        private void validarNumero(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;

        }





        public bool findTest()
        {

            String carnet = comboBox1.SelectedValue.ToString();
            String fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");

            bool res = false;


            String comand = "select * from Test_Principal  where Atleta='" + carnet + "' and Fecha='" + fecha + "' ";



            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand(comand, c))
                {
                    using (SQLiteDataReader read = comm1.ExecuteReader())
                    {

                        if (read.HasRows)
                            res = true;
                    }

                }

            }


            return res;


        }



        private void ComboBox2_DropDown_1(object sender, EventArgs e)
        {
            SQLiteDataAdapter categoria = new SQLiteDataAdapter("select Fecha  FROM Test_Principal where Atleta='" + comboBox1.SelectedValue.ToString() + "'  ", Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();
            categoria.Fill(data);
            comboBox2.DataSource = data;
            comboBox2.DisplayMember = "Fecha";
            comboBox2.ValueMember = "Fecha";
        }



        private void ComboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                EstadisticaValores();
                mostrarPruebasDiaAtleta();
                BuscarPruebasDia();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio un error consulte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }




        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                double tantes = Convert.ToDouble(label142.Text, System.Globalization.CultureInfo.InvariantCulture);
                double tdespues = Convert.ToDouble(label143.Text, System.Globalization.CultureInfo.InvariantCulture);

                double diferencia = tdespues - tantes;
                double porCientoDiferencia = Convert.ToInt32(numericUpDown1.Value) * diferencia / 100;

                if (tantes != 0 && tdespues != 0)
                {
                    if (porCientoDiferencia.ToString() != "NaN")
                        label37.Text = Math.Round(porCientoDiferencia, 2).ToString();

                    if (porCientoDiferencia == 0)
                        label37.Text = "0.0";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error consulte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        //Calificacion Perfilpolariad
        private void Button7_Click(object sender, EventArgs e)
        {


            try
            {
                if (virificarAntesCarga())
                {
                    if (verificarCombo() == 24)
                    {
                        if (radioButton10.Checked == true || radioButton12.Checked == true)
                        {

                            int A = 0;
                            int EA = 0;
                            int EF = 0;

                            List<CheckBox> list = pasarCheckLista(tableLayoutPanel22.Controls);
                            int i = 1;
                            int k = 0;


                            foreach (CheckBox item in list)
                            {

                                if (item.Checked)
                                {

                                    Evaluar evaluar = calificarBorg[k];

                                    String signos = evaluar.signos;
                                    int val = 0;
                                    signos = signos.Substring(i - 1, 1);

                                    if (signos != "*")
                                    {

                                        val = Convert.ToInt32(evaluar.puntos.Substring(i - 1, 1));
                                        if (signos == "-")
                                        {
                                            if (evaluar.valor == "EA")
                                                EA = EA - val;
                                            if (evaluar.valor == "EF")
                                                EF = EF - val;

                                            if (evaluar.valor == "A")
                                                A = A - val;
                                        }

                                        else
                                        {
                                            if (evaluar.valor == "EA")
                                                EA = EA + val;
                                            if (evaluar.valor == "EF")
                                                EF = EF + val;

                                            if (evaluar.valor == "A")
                                                A = A + val;
                                        }



                                    }


                                }


                                i++;

                                if (i == 8)
                                {
                                    i = 1;
                                    k++;
                                }

                            }


                            bool executeCommand = false;
                            String fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            String Ncarnet = comboBox1.SelectedValue.ToString();

                            String commandDb = "";


                            if (radioButton10.Checked == true)
                            {

                                if (findTestBorg("A") == false)
                                {

                                    commandDb = "Insert into  PerfilPolaridad  (EF , A ,EA,Fecha,Ncarnet,TFrecuencia)" +
                                     " values('" + EF + "', '" + A + "','" + EA + "' ,'" + fecha + "','" + Ncarnet + "','A' )";


                                    isNewTest = true;
                                    executeCommand = true;

                                }

                                else
                                {
                                    if (MessageBox.Show("Ya existe una prueba antes de la carga en el día. Desea reemplazarla ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        commandDb = commandDb = "update PerfilPolaridad set EF='" + EF + "' ,  A='" + A + "',  EA='" + EA + "'  where Fecha= '" + fecha + "'  and Ncarnet=  '" + Ncarnet + "' and  TFrecuencia='A' ";

                                        executeCommand = true;
                                    }

                                }

                            }



                            if (radioButton12.Checked == true)
                            {

                                if (findTestBorg("D") == false)
                                {




                                    commandDb = "Insert into  PerfilPolaridad  (EF , A ,EA,Fecha,Ncarnet,TFrecuencia)" +
                                      " values('" + EF + "', '" + A + "','" + EA + "' ,'" + fecha + "','" + Ncarnet + "','D' )";

                                    isNewTest = true;
                                    executeCommand = true;

                                }

                                else
                                {
                                    if (MessageBox.Show("Ya existe una prueba antes de la carga en el día. Desea reemplazarla ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {

                                        commandDb = "update PerfilPolaridad set EF='" + EF + "' ,  A='" + A + "',  EA='" + EA + "'     where Fecha= '" + fecha + "'  and Ncarnet=  '" + Ncarnet + "' and  TFrecuencia='D' ";
                                        executeCommand = true;
                                    }

                                }

                            }




                            if (executeCommand)
                            {

                                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                                {
                                    c.Open();
                                    using (SQLiteCommand comm = new SQLiteCommand(commandDb, c))
                                    {

                                        comm.ExecuteNonQuery();

                                    }

                                }


                                if (radioButton10.Checked == true)
                                    radioButton13.Checked = true;
                                else
                                    radioButton17.Checked = true;



                                GuardaPruebas();
                                limpiarCheckBox();
                                mostrarPruebasDiaAtleta();
                                BuscarPruebasDia();
                                MessageBox.Show("La prueba fue añadida correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                            MessageBox.Show("Debe seleccionar la frecuencia en la que se realiza la prueba", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        MessageBox.Show("Debe puntuar para cada característica", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                {
                    MessageBox.Show("Debe existir una prueba antes de la carga", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }



        }

        private bool virificarAntesCarga()
        {
            bool res = false;

            if (radioButton12.Checked)
            {

                using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                {
                    c.Open();
                    using (SQLiteCommand comm = new SQLiteCommand("select * from PerfilPolaridad where Fecha = '" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' and Ncarnet=  '" + comboBox1.SelectedValue.ToString() + "' and TFrecuencia ='A'", c))
                    {
                        using (SQLiteDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows)
                                res = true;
                        }


                    }

                }
            }
            else
                res = true;

            return res;
        }

        private void limpiarCheckBox()
        {

            foreach (var item in tableLayoutPanel22.Controls)
            {
                if (item is CheckBox)
                {
                    CheckBox check = item as CheckBox;
                    if (check.Checked == true)
                        check.Checked = false;
                }
            }

            radioButton10.Checked = false;
            radioButton12.Checked = false;
        }

        private bool findTestBorg(String val)
        {
            bool res = false;

            String fecha = DateTime.Now.Date.ToString("yyyy-MM-dd");
            String Ncarnet = comboBox1.SelectedValue.ToString();
            String commandDb = "";


            if (val == "A")
                commandDb = "select * from PerfilPolaridad where Fecha = '" + fecha + "' and Ncarnet=  '" + Ncarnet + "' and TFrecuencia ='A'";
            else
                commandDb = "select * from PerfilPolaridad where Fecha = '" + fecha + "' and Ncarnet=  '" + Ncarnet + "' and TFrecuencia ='D'";




            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm = new SQLiteCommand(commandDb, c))
                {
                    using (SQLiteDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.HasRows)
                            res = true;
                    }


                }

            }





            return res;

        }

        private List<CheckBox> pasarCheckLista(TableLayoutControlCollection controls)
        {
            List<CheckBox> list = new List<CheckBox>();

            foreach (var item in controls)
            {
                if (item is CheckBox)
                {
                    CheckBox check = item as CheckBox;
                    list.Add(check);
                }

            }
            list.Reverse();

            return list;
        }

        private int verificarCombo()
        {
            int i = 0;
            int count = 1;
            spamBorg.Clear();

            this.tableLayoutPanel22.CellPaint += new TableLayoutCellPaintEventHandler(this.TableLayoutPanel22_CellPaint_2);
            this.Refresh();

            //   tableLayoutPanel22.GetControlFromPosition(0, 1).BackColor = Color.Red;
            int row = 0;
            List<CheckBox> list = pasarCheckLista(tableLayoutPanel22.Controls);
            bool res = false;
            foreach (var item in list)
            {
                if (item is CheckBox)
                {
                    CheckBox check = item as CheckBox;
                    if (check.Checked)
                    {
                        i++;
                        res = true;
                    }


                    if (count == 7 && res == false)
                    {
                        row = tableLayoutPanel22.GetRow(check);
                        spamBorg.Add(row);
                    }




                    count++;

                    if (count == 8)
                    {
                        count = 1;
                        res = false;
                    }
                }
            }

            if (i != 24)
            {

                this.tableLayoutPanel22.CellPaint += new TableLayoutCellPaintEventHandler(this.TableLayoutPanel22_CellPaint_1);
                this.Refresh();
            }

            return i;
        }


        private void TableLayoutPanel22_CellPaint_1(object sender, TableLayoutCellPaintEventArgs e)
        {
            int colum = e.Column;

            if (spamBorg.Contains(e.Row) && colum != 0 && colum != 8)
            {
                // if (contador != 9 && contador != 1)
                {
                    var rectangle = e.CellBounds;
                    rectangle.Inflate(-1, -1);

                    ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Red, ButtonBorderStyle.Solid);
                }


                contador++;

                if (contador == 10)
                {
                    contador = 1;
                }

            }

        }



        private void TableLayoutPanel22_CellPaint_2(object sender, TableLayoutCellPaintEventArgs e)
        {
            int colum = e.Column;

            if (spamBorg.Contains(e.Row) && colum != 0 && colum != 8)
            {
                // if (contador != 9 && contador != 1)
                {
                    var rectangle = e.CellBounds;
                    rectangle.Inflate(-1, -1);

                    ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Transparent, ButtonBorderStyle.Solid);
                }


                contador++;

                if (contador == 10)
                {
                    contador = 1;
                }

            }

        }

        private void RadioButton11_CheckedChanged(object sender, EventArgs e)
        {


            /*radioButton11.Checked = false;
            radioButton15.Checked = false;
            radioButton14.Checked = false;
            radioButton13.Checked = false;
            radioButton16.Checked = false;
            radioButton17.Checked = false;*/
        }

        private void ComboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {


            e.Handled = true;


        }

        private void TableLayoutPanel76_Paint(object sender, PaintEventArgs e)
        {

        }



        private void clearCheck()
        {

            checkBox22.Checked = false;
            checkBox23.Checked = false;
            checkBox179.Checked = false;
            checkBox180.Checked = false;
            checkBox181.Checked = false;
            checkBox182.Checked = false;
            checkBox183.Checked = false;
            checkBox184.Checked = false;
            checkBox185.Checked = false;
            checkBox188.Checked = false;
            checkBox187.Checked = false;
            checkBox186.Checked = false;
            checkBox189.Checked = false;
            checkBox190.Checked = false;
            checkBox191.Checked = false;
        }

        private void CheckBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked)
            {
                checkBox191.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox186.Checked = false;
                checkBox187.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;

                esfuerzo = "Muy muy duro";
                puntos = "20";
                validar = true;
            }
            else
            {
                checkBox22.Checked = false;
                validar = false;
            }
        }

        private void CheckBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.Checked)
            {
                checkBox191.Checked = false;
                checkBox22.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox186.Checked = false;
                checkBox187.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;
                esfuerzo = "Muy muy duro";
                puntos = "19";

                validar = true;

            }
            else
            {
                checkBox23.Checked = false;
                validar = false;
            }
        }

        private void CheckBox179_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox179.Checked)
            {
                checkBox191.Checked = false;
                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox186.Checked = false;
                checkBox187.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;

                esfuerzo = "Muy muy duro";
                puntos = "18";
                validar = true;
            }
            else
            {
                checkBox129.Checked = false;
                validar = false;
            }
        }

        private void CheckBox180_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox180.Checked)
            {
                checkBox191.Checked = false;
                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox186.Checked = false;
                checkBox187.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;

                esfuerzo = "Muy duro";
                puntos = "17";
                validar = true;

            }
            else
            {
                checkBox180.Checked = false;
                validar = false;
            }
        }

        private void CheckBox181_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox181.Checked)
            {
                checkBox191.Checked = false;
                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox186.Checked = false;
                checkBox187.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;

                esfuerzo = "Muy duro";
                puntos = "16";
                validar = true;
            }
            else
            {
                checkBox181.Checked = false;
                validar = false;
            }
        }

        private void CheckBox182_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox182.Checked)
            {
                checkBox191.Checked = false;
                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox186.Checked = false;
                checkBox187.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;
                esfuerzo = "Duro";
                puntos = "15";
                validar = true;
            }
            else
            {
                checkBox182.Checked = false;
                validar = false;
            }
        }

        private void CheckBox183_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox183.Checked)
            {
                checkBox191.Checked = false;
                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox186.Checked = false;
                checkBox187.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;

                esfuerzo = "Duro";
                puntos = "14";
                validar = true;
            }
            else
            {
                checkBox183.Checked = false;
                validar = false;
            }
        }

        private void CheckBox184_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox184.Checked)
            {
                checkBox191.Checked = false;
                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox185.Checked = false;
                checkBox186.Checked = false;
                checkBox187.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;

                esfuerzo = "Algo duro";
                puntos = "13";
                validar = true;
            }
            else
            {
                checkBox184.Checked = false;
                validar = false;
            }
        }

        private void CheckBox185_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox185.Checked)
            {
                checkBox191.Checked = false;
                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox186.Checked = false;
                checkBox187.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;

                esfuerzo = "Algo duro";
                puntos = "12";
                validar = true;
            }
            else
            {
                checkBox185.Checked = false;
                validar = false;
            }
        }

        private void CheckBox186_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox186.Checked)
            {
                checkBox191.Checked = false;
                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox187.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;
                esfuerzo = "Bastante suave";
                puntos = "11";
                validar = true;
            }
            else
            {
                checkBox186.Checked = false;
                validar = false;
            }
        }

        private void CheckBox187_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox187.Checked)
            {
                checkBox191.Checked = false;
                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox186.Checked = false;
                checkBox188.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;
                esfuerzo = "Bastante suave";
                puntos = "10";
                validar = true;
            }
            else
            {
                checkBox187.Checked = false;
                validar = false;
            }
        }

        private void CheckBox188_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox188.Checked)
            {

                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox186.Checked = false;
                checkBox187.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;
                checkBox191.Checked = false;

                esfuerzo = "Muy suave";
                puntos = "9";
                validar = true;
            }
            else
            {
                checkBox188.Checked = false;
                validar = false;
            }
        }

        private void CheckBox189_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox189.Checked)
            {

                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox188.Checked = false;
                checkBox187.Checked = false;
                checkBox186.Checked = false;
                checkBox190.Checked = false;
                checkBox191.Checked = false;


                esfuerzo = "Muy suave";
                puntos = "8";
                validar = true;
            }
            else
            {
                checkBox189.Checked = false;
                validar = false;
            }
        }

        private void CheckBox190_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox190.Checked)
            {

                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox188.Checked = false;
                checkBox187.Checked = false;
                checkBox186.Checked = false;
                checkBox189.Checked = false;
                checkBox191.Checked = false;


                esfuerzo = "Muy muy suave";
                puntos = "7";
                validar = true;
            }
            else
            {
                checkBox190.Checked = false;
                validar = false;
            }
        }

        private void CheckBox191_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox191.Checked)
            {

                checkBox22.Checked = false;
                checkBox23.Checked = false;
                checkBox179.Checked = false;
                checkBox180.Checked = false;
                checkBox181.Checked = false;
                checkBox182.Checked = false;
                checkBox183.Checked = false;
                checkBox184.Checked = false;
                checkBox185.Checked = false;
                checkBox188.Checked = false;
                checkBox187.Checked = false;
                checkBox186.Checked = false;
                checkBox189.Checked = false;
                checkBox190.Checked = false;

                esfuerzo = "Muy muy suave";
                puntos = "6";
                validar = true;
            }
            else
            {
                checkBox191.Checked = false;
                validar = false;
            }
        }









        //prueba perfil de polaridad

        private void CheckBox12_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;
                checkBox15.Checked = false;
                //filtrarTabla();
            }
            else
            {
                checkBox12.Checked = false;
            }
        }

        private void CheckBox13_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox14.Checked = false;
                checkBox15.Checked = false;
                //filtrarTabla();
            }
            else
            {
                checkBox13.Checked = false;
            }
        }


        // ***********************************************************************
        private void CheckBox14_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox15.Checked = false;
                //filtrarTabla();
            }
            else
            {
                checkBox14.Checked = false;
            }
        }

        private void CheckBox15_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked)
            {
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;

            }
            else
            {
                checkBox15.Checked = false;
            }
        }

        private void CheckBox16_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked)
            {
                checkBox17.Checked = false;
                checkBox18.Checked = false;
                checkBox19.Checked = false;
                checkBox20.Checked = false;
                checkBox21.Checked = false;
                checkBox24.Checked = false;

            }
            else
            {
                checkBox16.Checked = false;
            }






        }

        private void CheckBox17_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked)
            {
                checkBox16.Checked = false;
                checkBox18.Checked = false;
                checkBox19.Checked = false;
                checkBox20.Checked = false;
                checkBox21.Checked = false;
                checkBox24.Checked = false;

            }
            else
            {
                checkBox17.Checked = false;
            }
        }

        private void CheckBox18_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox18.Checked)
            {
                checkBox17.Checked = false;
                checkBox16.Checked = false;
                checkBox19.Checked = false;
                checkBox20.Checked = false;
                checkBox21.Checked = false;
                checkBox24.Checked = false;

            }
            else
            {
                checkBox18.Checked = false;
            }
        }

        private void CheckBox19_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked)
            {
                checkBox17.Checked = false;
                checkBox16.Checked = false;
                checkBox18.Checked = false;
                checkBox20.Checked = false;
                checkBox21.Checked = false;
                checkBox24.Checked = false;

            }
            else
            {
                checkBox19.Checked = false;
            }
        }

        private void CheckBox20_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox20.Checked)
            {
                checkBox17.Checked = false;
                checkBox16.Checked = false;
                checkBox19.Checked = false;
                checkBox18.Checked = false;
                checkBox21.Checked = false;
                checkBox24.Checked = false;

            }
            else
            {
                checkBox20.Checked = false;
            }
        }

        private void CheckBox21_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
            {
                checkBox17.Checked = false;
                checkBox16.Checked = false;
                checkBox19.Checked = false;
                checkBox18.Checked = false;
                checkBox20.Checked = false;
                checkBox24.Checked = false;

            }
            else
            {
                checkBox21.Checked = false;
            }
        }
        //**********************************************************************************
        private void CheckBox24_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox24.Checked)
            {
                checkBox17.Checked = false;
                checkBox16.Checked = false;
                checkBox19.Checked = false;
                checkBox18.Checked = false;
                checkBox20.Checked = false;
                checkBox21.Checked = false;

            }
            else
            {
                checkBox24.Checked = false;
            }
        }

        private void CheckBox25_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox25.Checked)
            {
                checkBox26.Checked = false;
                checkBox27.Checked = false;
                checkBox28.Checked = false;
                checkBox29.Checked = false;
                checkBox30.Checked = false;
                checkBox31.Checked = false;

            }
            else
            {
                checkBox25.Checked = false;
            }

        }

        private void CheckBox26_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox26.Checked)
            {
                checkBox25.Checked = false;
                checkBox27.Checked = false;
                checkBox28.Checked = false;
                checkBox29.Checked = false;
                checkBox30.Checked = false;
                checkBox31.Checked = false;

            }
            else
            {
                checkBox26.Checked = false;
            }
        }

        private void CheckBox27_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox27.Checked)
            {
                checkBox25.Checked = false;
                checkBox26.Checked = false;
                checkBox28.Checked = false;
                checkBox29.Checked = false;
                checkBox30.Checked = false;
                checkBox31.Checked = false;

            }
            else
            {
                checkBox27.Checked = false;
            }
        }

        private void CheckBox28_CheckStateChanged(object sender, EventArgs e)
        {

            if (checkBox28.Checked)
            {
                checkBox25.Checked = false;
                checkBox26.Checked = false;
                checkBox27.Checked = false;
                checkBox29.Checked = false;
                checkBox30.Checked = false;
                checkBox31.Checked = false;

            }
            else
            {
                checkBox28.Checked = false;
            }

        }

        private void CheckBox29_CheckStateChanged(object sender, EventArgs e)
        {

            if (checkBox29.Checked)
            {
                checkBox25.Checked = false;
                checkBox26.Checked = false;
                checkBox27.Checked = false;
                checkBox28.Checked = false;
                checkBox30.Checked = false;
                checkBox31.Checked = false;

            }
            else
            {
                checkBox29.Checked = false;
            }
        }

        private void CheckBox30_CheckStateChanged(object sender, EventArgs e)
        {


            if (checkBox30.Checked)
            {
                checkBox25.Checked = false;
                checkBox26.Checked = false;
                checkBox27.Checked = false;
                checkBox28.Checked = false;
                checkBox29.Checked = false;
                checkBox31.Checked = false;

            }
            else
            {
                checkBox30.Checked = false;
            }
        }

        private void CheckBox31_CheckStateChanged(object sender, EventArgs e)
        {

            if (checkBox31.Checked)
            {
                checkBox25.Checked = false;
                checkBox26.Checked = false;
                checkBox27.Checked = false;
                checkBox28.Checked = false;
                checkBox29.Checked = false;
                checkBox30.Checked = false;

            }
            else
            {
                checkBox31.Checked = false;
            }
        }
        //^****************************************************************************************************
        private void CheckBox32_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox32.Checked)
            {
                checkBox33.Checked = false;
                checkBox34.Checked = false;
                checkBox35.Checked = false;
                checkBox36.Checked = false;
                checkBox37.Checked = false;
                checkBox38.Checked = false;

            }
            else
            {
                checkBox37.Checked = false;
            }
        }

        private void CheckBox33_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox33.Checked)
            {
                checkBox32.Checked = false;
                checkBox34.Checked = false;
                checkBox35.Checked = false;
                checkBox36.Checked = false;
                checkBox37.Checked = false;
                checkBox38.Checked = false;

            }
            else
            {
                checkBox33.Checked = false;
            }
        }

        private void CheckBox34_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox34.Checked)
            {
                checkBox32.Checked = false;
                checkBox33.Checked = false;
                checkBox35.Checked = false;
                checkBox36.Checked = false;
                checkBox37.Checked = false;
                checkBox38.Checked = false;

            }
            else
            {
                checkBox34.Checked = false;
            }
        }

        private void CheckBox35_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox35.Checked)
            {
                checkBox32.Checked = false;
                checkBox33.Checked = false;
                checkBox34.Checked = false;
                checkBox36.Checked = false;
                checkBox37.Checked = false;
                checkBox38.Checked = false;

            }
            else
            {
                checkBox35.Checked = false;
            }
        }

        private void CheckBox36_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox36.Checked)
            {
                checkBox32.Checked = false;
                checkBox33.Checked = false;
                checkBox34.Checked = false;
                checkBox35.Checked = false;
                checkBox37.Checked = false;
                checkBox38.Checked = false;

            }
            else
            {
                checkBox36.Checked = false;
            }
        }

        private void CheckBox37_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox37.Checked)
            {
                checkBox32.Checked = false;
                checkBox33.Checked = false;
                checkBox34.Checked = false;
                checkBox35.Checked = false;
                checkBox36.Checked = false;
                checkBox38.Checked = false;

            }
            else
            {
                checkBox37.Checked = false;
            }
        }

        private void CheckBox38_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox38.Checked)
            {
                checkBox32.Checked = false;
                checkBox33.Checked = false;
                checkBox34.Checked = false;
                checkBox35.Checked = false;
                checkBox36.Checked = false;
                checkBox37.Checked = false;

            }
            else
            {
                checkBox38.Checked = false;
            }
        }

        private void CheckBox39_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox39.Checked)
            {
                checkBox40.Checked = false;
                checkBox41.Checked = false;
                checkBox42.Checked = false;
                checkBox43.Checked = false;
                checkBox44.Checked = false;
                checkBox45.Checked = false;

            }
            else
            {
                checkBox39.Checked = false;
            }
        }
        //******************************************************************************
        private void CheckBox40_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox40.Checked)
            {
                checkBox39.Checked = false;
                checkBox41.Checked = false;
                checkBox42.Checked = false;
                checkBox43.Checked = false;
                checkBox44.Checked = false;
                checkBox45.Checked = false;

            }
            else
            {
                checkBox40.Checked = false;
            }
        }

        private void CheckBox41_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox41.Checked)
            {
                checkBox39.Checked = false;
                checkBox40.Checked = false;
                checkBox42.Checked = false;
                checkBox43.Checked = false;
                checkBox44.Checked = false;
                checkBox45.Checked = false;

            }
            else
            {
                checkBox41.Checked = false;
            }
        }

        private void CheckBox42_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox42.Checked)
            {
                checkBox39.Checked = false;
                checkBox40.Checked = false;
                checkBox41.Checked = false;
                checkBox43.Checked = false;
                checkBox44.Checked = false;
                checkBox45.Checked = false;

            }
            else
            {
                checkBox42.Checked = false;
            }
        }

        private void CheckBox43_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox43.Checked)
            {
                checkBox39.Checked = false;
                checkBox40.Checked = false;
                checkBox41.Checked = false;
                checkBox42.Checked = false;
                checkBox44.Checked = false;
                checkBox45.Checked = false;

            }
            else
            {
                checkBox43.Checked = false;
            }
        }

        private void CheckBox44_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox44.Checked)
            {
                checkBox39.Checked = false;
                checkBox40.Checked = false;
                checkBox41.Checked = false;
                checkBox42.Checked = false;
                checkBox43.Checked = false;
                checkBox45.Checked = false;

            }
            else
            {
                checkBox44.Checked = false;
            }
        }

        private void CheckBox45_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox45.Checked)
            {
                checkBox39.Checked = false;
                checkBox40.Checked = false;
                checkBox41.Checked = false;
                checkBox42.Checked = false;
                checkBox43.Checked = false;
                checkBox44.Checked = false;

            }
            else
            {
                checkBox45.Checked = false;
            }
        }

        private void CheckBox9_CheckStateChanged(object sender, EventArgs e)
        {


            if (checkBox9.Checked)
            {
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;
                checkBox15.Checked = false;

            }
            else
            {
                checkBox9.Checked = false;
            }


        }

        private void CheckBox10_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                checkBox9.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;
                checkBox15.Checked = false;

            }
            else
            {
                checkBox10.Checked = false;
            }
        }


        //*******************************************************
        private void CheckBox11_CheckStateChanged(object sender, EventArgs e)
        {

            if (checkBox11.Checked)
            {
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;
                checkBox15.Checked = false;

            }
            else
            {
                checkBox11.Checked = false;
            }
        }

        private void CheckBox46_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox46.Checked)
            {
                checkBox47.Checked = false;
                checkBox48.Checked = false;
                checkBox49.Checked = false;
                checkBox50.Checked = false;
                checkBox51.Checked = false;
                checkBox52.Checked = false;

            }
            else
            {
                checkBox46.Checked = false;
            }

        }

        private void CheckBox47_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox47.Checked)
            {
                checkBox46.Checked = false;
                checkBox48.Checked = false;
                checkBox49.Checked = false;
                checkBox50.Checked = false;
                checkBox51.Checked = false;
                checkBox52.Checked = false;

            }
            else
            {
                checkBox47.Checked = false;
            }
        }

        private void CheckBox48_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox48.Checked)
            {
                checkBox46.Checked = false;
                checkBox47.Checked = false;
                checkBox49.Checked = false;
                checkBox50.Checked = false;
                checkBox51.Checked = false;
                checkBox52.Checked = false;

            }
            else
            {
                checkBox48.Checked = false;
            }
        }


        private void CheckBox49_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox49.Checked)
            {
                checkBox46.Checked = false;
                checkBox47.Checked = false;
                checkBox48.Checked = false;
                checkBox50.Checked = false;
                checkBox51.Checked = false;
                checkBox52.Checked = false;

            }
            else
            {
                checkBox49.Checked = false;
            }
        }



        private void CheckBox50_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox50.Checked)
            {
                checkBox46.Checked = false;
                checkBox47.Checked = false;
                checkBox48.Checked = false;
                checkBox49.Checked = false;
                checkBox51.Checked = false;
                checkBox52.Checked = false;

            }
            else
            {
                checkBox50.Checked = false;
            }
        }



        private void CheckBox51_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox51.Checked)
            {
                checkBox46.Checked = false;
                checkBox47.Checked = false;
                checkBox48.Checked = false;
                checkBox49.Checked = false;
                checkBox50.Checked = false;
                checkBox52.Checked = false;

            }
            else
            {
                checkBox51.Checked = false;
            }
        }



        private void CheckBox52_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox52.Checked)
            {
                checkBox46.Checked = false;
                checkBox47.Checked = false;
                checkBox48.Checked = false;
                checkBox49.Checked = false;
                checkBox50.Checked = false;
                checkBox51.Checked = false;

            }
            else
            {
                checkBox52.Checked = false;
            }
        }



        //***********************************
        private void CheckBox53_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox53.Checked)
            {
                checkBox54.Checked = false;
                checkBox55.Checked = false;
                checkBox56.Checked = false;
                checkBox57.Checked = false;
                checkBox58.Checked = false;
                checkBox59.Checked = false;

            }
            else
            {
                checkBox53.Checked = false;
            }
        }

        private void CheckBox54_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox54.Checked)
            {
                checkBox53.Checked = false;
                checkBox55.Checked = false;
                checkBox56.Checked = false;
                checkBox57.Checked = false;
                checkBox58.Checked = false;
                checkBox59.Checked = false;

            }
            else
            {
                checkBox54.Checked = false;
            }
        }

        private void CheckBox55_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox55.Checked)
            {
                checkBox53.Checked = false;
                checkBox54.Checked = false;
                checkBox56.Checked = false;
                checkBox57.Checked = false;
                checkBox58.Checked = false;
                checkBox59.Checked = false;

            }
            else
            {
                checkBox55.Checked = false;
            }
        }

        private void CheckBox56_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox56.Checked)
            {
                checkBox53.Checked = false;
                checkBox54.Checked = false;
                checkBox55.Checked = false;
                checkBox57.Checked = false;
                checkBox58.Checked = false;
                checkBox59.Checked = false;

            }
            else
            {
                checkBox56.Checked = false;
            }
        }

        private void CheckBox57_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox57.Checked)
            {
                checkBox53.Checked = false;
                checkBox54.Checked = false;
                checkBox55.Checked = false;
                checkBox56.Checked = false;
                checkBox58.Checked = false;
                checkBox59.Checked = false;

            }
            else
            {
                checkBox57.Checked = false;
            }
        }

        private void CheckBox58_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox58.Checked)
            {
                checkBox53.Checked = false;
                checkBox54.Checked = false;
                checkBox55.Checked = false;
                checkBox56.Checked = false;
                checkBox57.Checked = false;
                checkBox59.Checked = false;

            }
            else
            {
                checkBox58.Checked = false;
            }
        }

        private void CheckBox59_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox59.Checked)
            {
                checkBox53.Checked = false;
                checkBox54.Checked = false;
                checkBox55.Checked = false;
                checkBox56.Checked = false;
                checkBox57.Checked = false;
                checkBox58.Checked = false;

            }
            else
            {
                checkBox59.Checked = false;
            }
        }
        //**********************************************************************************
        private void CheckBox60_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox60.Checked)
            {
                checkBox61.Checked = false;
                checkBox62.Checked = false;
                checkBox63.Checked = false;
                checkBox64.Checked = false;
                checkBox65.Checked = false;
                checkBox66.Checked = false;

            }
            else
            {
                checkBox60.Checked = false;
            }
        }


        private void CheckBox61_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox61.Checked)
            {
                checkBox60.Checked = false;
                checkBox62.Checked = false;
                checkBox63.Checked = false;
                checkBox64.Checked = false;
                checkBox65.Checked = false;
                checkBox66.Checked = false;

            }
            else
            {
                checkBox61.Checked = false;
            }
        }


        private void CheckBox62_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox62.Checked)
            {
                checkBox60.Checked = false;
                checkBox61.Checked = false;
                checkBox63.Checked = false;
                checkBox64.Checked = false;
                checkBox65.Checked = false;
                checkBox66.Checked = false;

            }
            else
            {
                checkBox62.Checked = false;
            }
        }


        private void CheckBox63_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox63.Checked)
            {
                checkBox60.Checked = false;
                checkBox61.Checked = false;
                checkBox62.Checked = false;
                checkBox64.Checked = false;
                checkBox65.Checked = false;
                checkBox66.Checked = false;

            }
            else
            {
                checkBox63.Checked = false;
            }
        }


        private void CheckBox64_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox64.Checked)
            {
                checkBox60.Checked = false;
                checkBox61.Checked = false;
                checkBox62.Checked = false;
                checkBox63.Checked = false;
                checkBox65.Checked = false;
                checkBox66.Checked = false;

            }
            else
            {
                checkBox64.Checked = false;
            }
        }


        private void CheckBox65_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox65.Checked)
            {
                checkBox60.Checked = false;
                checkBox61.Checked = false;
                checkBox62.Checked = false;
                checkBox63.Checked = false;
                checkBox64.Checked = false;
                checkBox66.Checked = false;

            }
            else
            {
                checkBox65.Checked = false;
            }
        }


        private void CheckBox66_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox66.Checked)
            {
                checkBox60.Checked = false;
                checkBox61.Checked = false;
                checkBox62.Checked = false;
                checkBox63.Checked = false;
                checkBox64.Checked = false;
                checkBox65.Checked = false;


            }
            else
            {
                checkBox66.Checked = false;
            }
        }




        //***************************************************************************************

        private void CheckBox67_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox67.Checked)
            {
                checkBox68.Checked = false;
                checkBox69.Checked = false;
                checkBox70.Checked = false;
                checkBox71.Checked = false;
                checkBox72.Checked = false;
                checkBox73.Checked = false;


            }
            else
            {
                checkBox67.Checked = false;
            }
        }


        private void CheckBox68_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox68.Checked)
            {
                checkBox67.Checked = false;
                checkBox69.Checked = false;
                checkBox70.Checked = false;
                checkBox71.Checked = false;
                checkBox72.Checked = false;
                checkBox73.Checked = false;


            }
            else
            {
                checkBox68.Checked = false;
            }
        }


        private void CheckBox69_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox69.Checked)
            {
                checkBox67.Checked = false;
                checkBox68.Checked = false;
                checkBox70.Checked = false;
                checkBox71.Checked = false;
                checkBox72.Checked = false;
                checkBox73.Checked = false;


            }
            else
            {
                checkBox69.Checked = false;
            }
        }


        private void CheckBox70_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox70.Checked)
            {
                checkBox67.Checked = false;
                checkBox68.Checked = false;
                checkBox69.Checked = false;
                checkBox71.Checked = false;
                checkBox72.Checked = false;
                checkBox73.Checked = false;


            }
            else
            {
                checkBox70.Checked = false;
            }
        }


        private void CheckBox71_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox71.Checked)
            {
                checkBox67.Checked = false;
                checkBox68.Checked = false;
                checkBox69.Checked = false;
                checkBox70.Checked = false;
                checkBox72.Checked = false;
                checkBox73.Checked = false;


            }
            else
            {
                checkBox71.Checked = false;
            }
        }

        private void CheckBox72_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox72.Checked)
            {
                checkBox67.Checked = false;
                checkBox68.Checked = false;
                checkBox69.Checked = false;
                checkBox70.Checked = false;
                checkBox71.Checked = false;
                checkBox73.Checked = false;


            }
            else
            {
                checkBox72.Checked = false;
            }
        }

        private void CheckBox73_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox73.Checked)
            {
                checkBox67.Checked = false;
                checkBox68.Checked = false;
                checkBox69.Checked = false;
                checkBox70.Checked = false;
                checkBox71.Checked = false;
                checkBox72.Checked = false;


            }
            else
            {
                checkBox73.Checked = false;
            }
        }


        //**************************************************************
        private void CheckBox74_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox74.Checked)
            {
                checkBox75.Checked = false;
                checkBox76.Checked = false;
                checkBox77.Checked = false;
                checkBox78.Checked = false;
                checkBox79.Checked = false;
                checkBox80.Checked = false;


            }
            else
            {
                checkBox74.Checked = false;
            }
        }



        private void CheckBox75_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox75.Checked)
            {
                checkBox74.Checked = false;
                checkBox76.Checked = false;
                checkBox77.Checked = false;
                checkBox78.Checked = false;
                checkBox79.Checked = false;
                checkBox80.Checked = false;


            }
            else
            {
                checkBox75.Checked = false;
            }
        }


        private void CheckBox76_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox76.Checked)
            {
                checkBox74.Checked = false;
                checkBox75.Checked = false;
                checkBox77.Checked = false;
                checkBox78.Checked = false;
                checkBox79.Checked = false;
                checkBox80.Checked = false;


            }
            else
            {
                checkBox76.Checked = false;
            }
        }



        private void CheckBox77_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox77.Checked)
            {
                checkBox74.Checked = false;
                checkBox75.Checked = false;
                checkBox76.Checked = false;
                checkBox78.Checked = false;
                checkBox79.Checked = false;
                checkBox80.Checked = false;


            }
            else
            {
                checkBox77.Checked = false;
            }
        }



        private void CheckBox78_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox78.Checked)
            {
                checkBox74.Checked = false;
                checkBox75.Checked = false;
                checkBox76.Checked = false;
                checkBox77.Checked = false;
                checkBox79.Checked = false;
                checkBox80.Checked = false;


            }
            else
            {
                checkBox78.Checked = false;
            }
        }



        private void CheckBox79_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox79.Checked)
            {
                checkBox74.Checked = false;
                checkBox75.Checked = false;
                checkBox76.Checked = false;
                checkBox77.Checked = false;
                checkBox78.Checked = false;
                checkBox80.Checked = false;


            }
            else
            {
                checkBox79.Checked = false;
            }
        }




        private void CheckBox80_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox80.Checked)
            {
                checkBox74.Checked = false;
                checkBox75.Checked = false;
                checkBox76.Checked = false;
                checkBox77.Checked = false;
                checkBox78.Checked = false;
                checkBox79.Checked = false;


            }
            else
            {
                checkBox80.Checked = false;
            }
        }



        //**************************************************************

        private void CheckBox81_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox81.Checked)
            {
                checkBox82.Checked = false;
                checkBox83.Checked = false;
                checkBox84.Checked = false;
                checkBox85.Checked = false;
                checkBox86.Checked = false;
                checkBox87.Checked = false;


            }
            else
            {
                checkBox81.Checked = false;
            }
        }



        private void CheckBox82_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox82.Checked)
            {
                checkBox81.Checked = false;
                checkBox83.Checked = false;
                checkBox84.Checked = false;
                checkBox85.Checked = false;
                checkBox86.Checked = false;
                checkBox87.Checked = false;


            }
            else
            {
                checkBox82.Checked = false;
            }
        }


        private void CheckBox83_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox83.Checked)
            {
                checkBox81.Checked = false;
                checkBox84.Checked = false;
                checkBox82.Checked = false;
                checkBox85.Checked = false;
                checkBox86.Checked = false;
                checkBox87.Checked = false;


            }
            else
            {
                checkBox83.Checked = false;
            }
        }



        private void CheckBox84_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox84.Checked)
            {
                checkBox81.Checked = false;
                checkBox83.Checked = false;
                checkBox82.Checked = false;
                checkBox85.Checked = false;
                checkBox86.Checked = false;
                checkBox87.Checked = false;


            }
            else
            {
                checkBox84.Checked = false;
            }
        }



        private void CheckBox85_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox85.Checked)
            {
                checkBox81.Checked = false;
                checkBox83.Checked = false;
                checkBox82.Checked = false;
                checkBox84.Checked = false;
                checkBox86.Checked = false;
                checkBox87.Checked = false;


            }
            else
            {
                checkBox85.Checked = false;
            }
        }



        private void CheckBox86_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox86.Checked)
            {
                checkBox81.Checked = false;
                checkBox83.Checked = false;
                checkBox82.Checked = false;
                checkBox84.Checked = false;
                checkBox85.Checked = false;
                checkBox87.Checked = false;


            }
            else
            {
                checkBox86.Checked = false;
            }
        }



        private void CheckBox87_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox87.Checked)
            {
                checkBox81.Checked = false;
                checkBox83.Checked = false;
                checkBox82.Checked = false;
                checkBox84.Checked = false;
                checkBox85.Checked = false;
                checkBox86.Checked = false;


            }
            else
            {
                checkBox87.Checked = false;
            }
        }





        ////********************************************************************************************+
        ///


        private void CheckBox88_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox88.Checked)
            {
                checkBox89.Checked = false;
                checkBox90.Checked = false;
                checkBox91.Checked = false;
                checkBox92.Checked = false;
                checkBox93.Checked = false;
                checkBox94.Checked = false;


            }
            else
            {
                checkBox88.Checked = false;
            }
        }

        private void CheckBox89_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox89.Checked)
            {
                checkBox88.Checked = false;
                checkBox90.Checked = false;
                checkBox91.Checked = false;
                checkBox92.Checked = false;
                checkBox93.Checked = false;
                checkBox94.Checked = false;


            }
            else
            {
                checkBox89.Checked = false;
            }
        }



        private void CheckBox90_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox90.Checked)
            {
                checkBox88.Checked = false;
                checkBox89.Checked = false;
                checkBox91.Checked = false;
                checkBox92.Checked = false;
                checkBox93.Checked = false;
                checkBox94.Checked = false;


            }
            else
            {
                checkBox90.Checked = false;
            }
        }

        private void CheckBox91_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox91.Checked)
            {
                checkBox88.Checked = false;
                checkBox89.Checked = false;
                checkBox90.Checked = false;
                checkBox92.Checked = false;
                checkBox93.Checked = false;
                checkBox94.Checked = false;


            }
            else
            {
                checkBox91.Checked = false;
            }
        }



        private void CheckBox92_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox92.Checked)
            {
                checkBox88.Checked = false;
                checkBox89.Checked = false;
                checkBox90.Checked = false;
                checkBox91.Checked = false;
                checkBox93.Checked = false;
                checkBox94.Checked = false;


            }
            else
            {
                checkBox92.Checked = false;
            }
        }




        private void CheckBox93_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox93.Checked)
            {
                checkBox88.Checked = false;
                checkBox89.Checked = false;
                checkBox90.Checked = false;
                checkBox91.Checked = false;
                checkBox92.Checked = false;
                checkBox94.Checked = false;


            }
            else
            {
                checkBox93.Checked = false;
            }
        }



        private void CheckBox94_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox94.Checked)
            {
                checkBox88.Checked = false;
                checkBox89.Checked = false;
                checkBox90.Checked = false;
                checkBox91.Checked = false;
                checkBox92.Checked = false;
                checkBox93.Checked = false;


            }
            else
            {
                checkBox94.Checked = false;
            }
        }

        ///^*******************************************************
        private void CheckBox95_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox95.Checked)
            {
                checkBox96.Checked = false;
                checkBox97.Checked = false;
                checkBox98.Checked = false;
                checkBox99.Checked = false;
                checkBox100.Checked = false;
                checkBox101.Checked = false;


            }
            else
            {
                checkBox95.Checked = false;
            }
        }



        private void CheckBox96_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox96.Checked)
            {
                checkBox95.Checked = false;
                checkBox97.Checked = false;
                checkBox98.Checked = false;
                checkBox99.Checked = false;
                checkBox100.Checked = false;
                checkBox101.Checked = false;


            }
            else
            {
                checkBox96.Checked = false;
            }
        }



        private void CheckBox97_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox97.Checked)
            {
                checkBox95.Checked = false;
                checkBox96.Checked = false;
                checkBox98.Checked = false;
                checkBox99.Checked = false;
                checkBox100.Checked = false;
                checkBox101.Checked = false;


            }
            else
            {
                checkBox97.Checked = false;
            }
        }



        private void CheckBox98_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox98.Checked)
            {
                checkBox95.Checked = false;
                checkBox96.Checked = false;
                checkBox97.Checked = false;
                checkBox99.Checked = false;
                checkBox100.Checked = false;
                checkBox101.Checked = false;


            }
            else
            {
                checkBox98.Checked = false;
            }
        }


        private void CheckBox99_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox99.Checked)
            {
                checkBox95.Checked = false;
                checkBox96.Checked = false;
                checkBox97.Checked = false;
                checkBox98.Checked = false;
                checkBox100.Checked = false;
                checkBox101.Checked = false;


            }
            else
            {
                checkBox99.Checked = false;
            }
        }



        private void CheckBox100_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox100.Checked)
            {
                checkBox95.Checked = false;
                checkBox96.Checked = false;
                checkBox97.Checked = false;
                checkBox98.Checked = false;
                checkBox99.Checked = false;
                checkBox101.Checked = false;


            }
            else
            {
                checkBox100.Checked = false;
            }
        }

        private void CheckBox101_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox101.Checked)
            {
                checkBox95.Checked = false;
                checkBox96.Checked = false;
                checkBox97.Checked = false;
                checkBox98.Checked = false;
                checkBox99.Checked = false;
                checkBox100.Checked = false;


            }
            else
            {
                checkBox101.Checked = false;
            }
        }


        ///******************************************************************

        private void CheckBox102_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox102.Checked)
            {
                checkBox103.Checked = false;
                checkBox104.Checked = false;
                checkBox105.Checked = false;
                checkBox106.Checked = false;
                checkBox107.Checked = false;
                checkBox108.Checked = false;


            }
            else
            {
                checkBox102.Checked = false;
            }
        }


        private void CheckBox103_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox103.Checked)
            {
                checkBox102.Checked = false;
                checkBox104.Checked = false;
                checkBox105.Checked = false;
                checkBox106.Checked = false;
                checkBox107.Checked = false;
                checkBox108.Checked = false;


            }
            else
            {
                checkBox103.Checked = false;
            }
        }



        private void CheckBox104_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox104.Checked)
            {
                checkBox102.Checked = false;
                checkBox103.Checked = false;
                checkBox105.Checked = false;
                checkBox106.Checked = false;
                checkBox107.Checked = false;
                checkBox108.Checked = false;


            }
            else
            {
                checkBox104.Checked = false;
            }
        }



        private void CheckBox105_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox105.Checked)
            {
                checkBox102.Checked = false;
                checkBox103.Checked = false;
                checkBox104.Checked = false;
                checkBox106.Checked = false;
                checkBox107.Checked = false;
                checkBox108.Checked = false;


            }
            else
            {
                checkBox105.Checked = false;
            }
        }


        private void CheckBox106_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox106.Checked)
            {
                checkBox102.Checked = false;
                checkBox103.Checked = false;
                checkBox104.Checked = false;
                checkBox105.Checked = false;
                checkBox107.Checked = false;
                checkBox108.Checked = false;


            }
            else
            {
                checkBox106.Checked = false;
            }
        }



        private void CheckBox107_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox107.Checked)
            {
                checkBox102.Checked = false;
                checkBox103.Checked = false;
                checkBox104.Checked = false;
                checkBox105.Checked = false;
                checkBox106.Checked = false;
                checkBox108.Checked = false;


            }
            else
            {
                checkBox107.Checked = false;
            }
        }



        private void CheckBox108_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox108.Checked)
            {
                checkBox102.Checked = false;
                checkBox103.Checked = false;
                checkBox104.Checked = false;
                checkBox105.Checked = false;
                checkBox106.Checked = false;
                checkBox107.Checked = false;


            }
            else
            {
                checkBox108.Checked = false;
            }
        }


        ///******************************************************************



        private void CheckBox109_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox109.Checked)
            {
                checkBox110.Checked = false;
                checkBox111.Checked = false;
                checkBox112.Checked = false;
                checkBox113.Checked = false;
                checkBox114.Checked = false;
                checkBox115.Checked = false;


            }
            else
            {
                checkBox109.Checked = false;
            }
        }



        private void CheckBox110_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox110.Checked)
            {
                checkBox109.Checked = false;
                checkBox111.Checked = false;
                checkBox112.Checked = false;
                checkBox113.Checked = false;
                checkBox114.Checked = false;
                checkBox115.Checked = false;


            }
            else
            {
                checkBox110.Checked = false;
            }
        }


        private void CheckBox111_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox111.Checked)
            {
                checkBox109.Checked = false;
                checkBox110.Checked = false;
                checkBox112.Checked = false;
                checkBox113.Checked = false;
                checkBox114.Checked = false;
                checkBox115.Checked = false;


            }
            else
            {
                checkBox111.Checked = false;
            }
        }



        private void CheckBox112_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox112.Checked)
            {
                checkBox109.Checked = false;
                checkBox110.Checked = false;
                checkBox111.Checked = false;
                checkBox113.Checked = false;
                checkBox114.Checked = false;
                checkBox115.Checked = false;


            }
            else
            {
                checkBox112.Checked = false;
            }
        }



        private void CheckBox113_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox113.Checked)
            {
                checkBox109.Checked = false;
                checkBox110.Checked = false;
                checkBox111.Checked = false;
                checkBox112.Checked = false;
                checkBox114.Checked = false;
                checkBox115.Checked = false;


            }
            else
            {
                checkBox113.Checked = false;
            }
        }




        private void CheckBox114_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox114.Checked)
            {
                checkBox109.Checked = false;
                checkBox110.Checked = false;
                checkBox111.Checked = false;
                checkBox112.Checked = false;
                checkBox113.Checked = false;
                checkBox115.Checked = false;


            }
            else
            {
                checkBox114.Checked = false;
            }
        }




        private void CheckBox115_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox115.Checked)
            {
                checkBox109.Checked = false;
                checkBox110.Checked = false;
                checkBox111.Checked = false;
                checkBox112.Checked = false;
                checkBox113.Checked = false;
                checkBox114.Checked = false;


            }
            else
            {
                checkBox115.Checked = false;
            }
        }


        ///******************************************************************
        private void CheckBox116_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox116.Checked)
            {
                checkBox117.Checked = false;
                checkBox118.Checked = false;
                checkBox119.Checked = false;
                checkBox120.Checked = false;
                checkBox121.Checked = false;
                checkBox122.Checked = false;


            }
            else
            {
                checkBox116.Checked = false;
            }
        }



        private void CheckBox117_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox117.Checked)
            {
                checkBox116.Checked = false;
                checkBox118.Checked = false;
                checkBox119.Checked = false;
                checkBox120.Checked = false;
                checkBox121.Checked = false;
                checkBox122.Checked = false;


            }
            else
            {
                checkBox117.Checked = false;
            }
        }



        private void CheckBox118_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox118.Checked)
            {
                checkBox116.Checked = false;
                checkBox117.Checked = false;
                checkBox119.Checked = false;
                checkBox120.Checked = false;
                checkBox121.Checked = false;
                checkBox122.Checked = false;


            }
            else
            {
                checkBox118.Checked = false;
            }
        }




        private void CheckBox119_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox119.Checked)
            {
                checkBox116.Checked = false;
                checkBox117.Checked = false;
                checkBox118.Checked = false;
                checkBox120.Checked = false;
                checkBox121.Checked = false;
                checkBox122.Checked = false;


            }
            else
            {
                checkBox119.Checked = false;
            }
        }


        private void CheckBox120_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox120.Checked)
            {
                checkBox116.Checked = false;
                checkBox117.Checked = false;
                checkBox118.Checked = false;
                checkBox119.Checked = false;
                checkBox121.Checked = false;
                checkBox122.Checked = false;


            }
            else
            {
                checkBox120.Checked = false;
            }
        }


        private void CheckBox121_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox121.Checked)
            {
                checkBox116.Checked = false;
                checkBox117.Checked = false;
                checkBox118.Checked = false;
                checkBox119.Checked = false;
                checkBox120.Checked = false;
                checkBox122.Checked = false;


            }
            else
            {
                checkBox121.Checked = false;
            }
        }


        private void CheckBox122_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox122.Checked)
            {
                checkBox116.Checked = false;
                checkBox117.Checked = false;
                checkBox118.Checked = false;
                checkBox119.Checked = false;
                checkBox120.Checked = false;
                checkBox121.Checked = false;


            }
            else
            {
                checkBox122.Checked = false;
            }
        }






        //////**************************************************************************




        private void CheckBox123_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox123.Checked)
            {
                checkBox124.Checked = false;
                checkBox125.Checked = false;
                checkBox126.Checked = false;
                checkBox127.Checked = false;
                checkBox128.Checked = false;
                checkBox129.Checked = false;


            }
            else
            {
                checkBox123.Checked = false;
            }
        }


        private void CheckBox124_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox124.Checked)
            {
                checkBox123.Checked = false;
                checkBox125.Checked = false;
                checkBox126.Checked = false;
                checkBox127.Checked = false;
                checkBox128.Checked = false;
                checkBox129.Checked = false;


            }
            else
            {
                checkBox124.Checked = false;
            }
        }



        private void CheckBox125_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox125.Checked)
            {
                checkBox123.Checked = false;
                checkBox124.Checked = false;
                checkBox126.Checked = false;
                checkBox127.Checked = false;
                checkBox128.Checked = false;
                checkBox129.Checked = false;


            }
            else
            {
                checkBox125.Checked = false;
            }
        }


        private void CheckBox126_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox126.Checked)
            {
                checkBox123.Checked = false;
                checkBox124.Checked = false;
                checkBox125.Checked = false;
                checkBox127.Checked = false;
                checkBox128.Checked = false;
                checkBox129.Checked = false;


            }
            else
            {
                checkBox126.Checked = false;
            }
        }



        private void CheckBox127_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox127.Checked)
            {
                checkBox123.Checked = false;
                checkBox124.Checked = false;
                checkBox125.Checked = false;
                checkBox126.Checked = false;
                checkBox128.Checked = false;
                checkBox129.Checked = false;


            }
            else
            {
                checkBox127.Checked = false;
            }
        }



        private void CheckBox128_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox128.Checked)
            {
                checkBox123.Checked = false;
                checkBox124.Checked = false;
                checkBox125.Checked = false;
                checkBox126.Checked = false;
                checkBox127.Checked = false;
                checkBox129.Checked = false;


            }
            else
            {
                checkBox128.Checked = false;
            }
        }



        private void CheckBox129_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox129.Checked)
            {
                checkBox123.Checked = false;
                checkBox124.Checked = false;
                checkBox125.Checked = false;
                checkBox126.Checked = false;
                checkBox127.Checked = false;
                checkBox128.Checked = false;


            }
            else
            {
                checkBox129.Checked = false;
            }
        }
        //***************************************************************************************
        private void CheckBox130_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox130.Checked)
            {
                checkBox131.Checked = false;
                checkBox132.Checked = false;
                checkBox133.Checked = false;
                checkBox134.Checked = false;
                checkBox135.Checked = false;
                checkBox136.Checked = false;


            }
            else
            {
                checkBox130.Checked = false;
            }
        }




        private void CheckBox131_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox131.Checked)
            {
                checkBox130.Checked = false;
                checkBox132.Checked = false;
                checkBox133.Checked = false;
                checkBox134.Checked = false;
                checkBox135.Checked = false;
                checkBox136.Checked = false;


            }
            else
            {
                checkBox131.Checked = false;
            }
        }


        private void CheckBox132_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox132.Checked)
            {
                checkBox130.Checked = false;
                checkBox131.Checked = false;
                checkBox133.Checked = false;
                checkBox134.Checked = false;
                checkBox135.Checked = false;
                checkBox136.Checked = false;


            }
            else
            {
                checkBox132.Checked = false;
            }
        }

        private void CheckBox133_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox133.Checked)
            {
                checkBox130.Checked = false;
                checkBox131.Checked = false;
                checkBox132.Checked = false;
                checkBox134.Checked = false;
                checkBox135.Checked = false;
                checkBox136.Checked = false;


            }
            else
            {
                checkBox133.Checked = false;
            }
        }


        private void CheckBox134_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox134.Checked)
            {
                checkBox130.Checked = false;
                checkBox131.Checked = false;
                checkBox132.Checked = false;
                checkBox133.Checked = false;
                checkBox135.Checked = false;
                checkBox136.Checked = false;


            }
            else
            {
                checkBox134.Checked = false;
            }
        }



        private void CheckBox135_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox135.Checked)
            {
                checkBox130.Checked = false;
                checkBox131.Checked = false;
                checkBox132.Checked = false;
                checkBox133.Checked = false;
                checkBox134.Checked = false;
                checkBox136.Checked = false;


            }
            else
            {
                checkBox135.Checked = false;
            }
        }



        private void CheckBox136_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox136.Checked)
            {
                checkBox130.Checked = false;
                checkBox131.Checked = false;
                checkBox132.Checked = false;
                checkBox133.Checked = false;
                checkBox134.Checked = false;
                checkBox135.Checked = false;


            }
            else
            {
                checkBox136.Checked = false;
            }
        }
        //****************************************************************************
        private void CheckBox137_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox137.Checked)
            {
                checkBox138.Checked = false;
                checkBox139.Checked = false;
                checkBox140.Checked = false;
                checkBox141.Checked = false;
                checkBox142.Checked = false;
                checkBox143.Checked = false;


            }
            else
            {
                checkBox137.Checked = false;
            }
        }



        private void CheckBox138_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox138.Checked)
            {
                checkBox137.Checked = false;
                checkBox139.Checked = false;
                checkBox140.Checked = false;
                checkBox141.Checked = false;
                checkBox142.Checked = false;
                checkBox143.Checked = false;


            }
            else
            {
                checkBox138.Checked = false;
            }
        }




        private void CheckBox139_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox139.Checked)
            {

                checkBox137.Checked = false;
                checkBox138.Checked = false;
                checkBox140.Checked = false;
                checkBox141.Checked = false;
                checkBox142.Checked = false;
                checkBox143.Checked = false;

            }
            else
            {
                checkBox139.Checked = false;
            }

        }



        private void CheckBox140_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox140.Checked)
            {

                checkBox137.Checked = false;
                checkBox138.Checked = false;
                checkBox139.Checked = false;
                checkBox141.Checked = false;
                checkBox142.Checked = false;
                checkBox143.Checked = false;

            }
            else
            {
                checkBox140.Checked = false;
            }

        }







        private void CheckBox141_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox141.Checked)
            {

                checkBox137.Checked = false;
                checkBox138.Checked = false;
                checkBox139.Checked = false;
                checkBox140.Checked = false;
                checkBox142.Checked = false;
                checkBox143.Checked = false;

            }
            else
            {
                checkBox141.Checked = false;
            }

        }


        private void CheckBox142_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox142.Checked)
            {

                checkBox137.Checked = false;
                checkBox138.Checked = false;
                checkBox139.Checked = false;
                checkBox140.Checked = false;
                checkBox141.Checked = false;
                checkBox143.Checked = false;

            }
            else
            {
                checkBox142.Checked = false;
            }

        }




        private void CheckBox143_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox143.Checked)
            {

                checkBox137.Checked = false;
                checkBox138.Checked = false;
                checkBox139.Checked = false;
                checkBox140.Checked = false;
                checkBox141.Checked = false;
                checkBox142.Checked = false;

            }
            else
            {
                checkBox143.Checked = false;
            }

        }


        ///**********************************************************************************************
        private void CheckBox144_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox144.Checked)
            {

                checkBox145.Checked = false;
                checkBox146.Checked = false;
                checkBox147.Checked = false;
                checkBox148.Checked = false;
                checkBox149.Checked = false;
                checkBox150.Checked = false;

            }
            else
            {
                checkBox144.Checked = false;
            }
        }



        private void CheckBox145_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox145.Checked)
            {

                checkBox144.Checked = false;
                checkBox146.Checked = false;
                checkBox147.Checked = false;
                checkBox148.Checked = false;
                checkBox149.Checked = false;
                checkBox150.Checked = false;

            }
            else
            {
                checkBox145.Checked = false;
            }
        }



        private void CheckBox146_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox146.Checked)
            {

                checkBox144.Checked = false;
                checkBox145.Checked = false;
                checkBox147.Checked = false;
                checkBox148.Checked = false;
                checkBox149.Checked = false;
                checkBox150.Checked = false;

            }
            else
            {
                checkBox146.Checked = false;
            }
        }




        private void CheckBox147_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox147.Checked)
            {

                checkBox144.Checked = false;
                checkBox145.Checked = false;
                checkBox146.Checked = false;
                checkBox148.Checked = false;
                checkBox149.Checked = false;
                checkBox150.Checked = false;

            }
            else
            {
                checkBox147.Checked = false;
            }
        }



        private void CheckBox148_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox148.Checked)
            {

                checkBox144.Checked = false;
                checkBox145.Checked = false;
                checkBox146.Checked = false;
                checkBox147.Checked = false;
                checkBox149.Checked = false;
                checkBox150.Checked = false;

            }
            else
            {
                checkBox148.Checked = false;
            }
        }




        private void CheckBox149_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox149.Checked)
            {

                checkBox144.Checked = false;
                checkBox145.Checked = false;
                checkBox146.Checked = false;
                checkBox147.Checked = false;
                checkBox148.Checked = false;
                checkBox150.Checked = false;

            }
            else
            {
                checkBox149.Checked = false;
            }
        }


        private void CheckBox150_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox150.Checked)
            {

                checkBox144.Checked = false;
                checkBox145.Checked = false;
                checkBox146.Checked = false;
                checkBox147.Checked = false;
                checkBox148.Checked = false;
                checkBox149.Checked = false;

            }
            else
            {
                checkBox150.Checked = false;
            }
        }



        //********************************************************************


        private void CheckBox151_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox151.Checked)
            {

                checkBox152.Checked = false;
                checkBox153.Checked = false;
                checkBox154.Checked = false;
                checkBox155.Checked = false;
                checkBox156.Checked = false;
                checkBox157.Checked = false;

            }
            else
            {
                checkBox151.Checked = false;
            }
        }


        private void CheckBox152_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox152.Checked)
            {

                checkBox151.Checked = false;
                checkBox153.Checked = false;
                checkBox154.Checked = false;
                checkBox155.Checked = false;
                checkBox156.Checked = false;
                checkBox157.Checked = false;

            }
            else
            {
                checkBox152.Checked = false;
            }
        }


        private void CheckBox153_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox153.Checked)
            {

                checkBox151.Checked = false;
                checkBox152.Checked = false;
                checkBox154.Checked = false;
                checkBox155.Checked = false;
                checkBox156.Checked = false;
                checkBox157.Checked = false;

            }
            else
            {
                checkBox153.Checked = false;
            }
        }



        private void CheckBox154_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox154.Checked)
            {

                checkBox151.Checked = false;
                checkBox152.Checked = false;
                checkBox153.Checked = false;
                checkBox155.Checked = false;
                checkBox156.Checked = false;
                checkBox157.Checked = false;

            }
            else
            {
                checkBox154.Checked = false;
            }
        }


        private void CheckBox155_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox155.Checked)
            {

                checkBox151.Checked = false;
                checkBox152.Checked = false;
                checkBox153.Checked = false;
                checkBox154.Checked = false;
                checkBox156.Checked = false;
                checkBox157.Checked = false;

            }
            else
            {
                checkBox155.Checked = false;
            }
        }


        private void CheckBox156_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox156.Checked)
            {

                checkBox151.Checked = false;
                checkBox152.Checked = false;
                checkBox153.Checked = false;
                checkBox154.Checked = false;
                checkBox155.Checked = false;
                checkBox157.Checked = false;

            }
            else
            {
                checkBox156.Checked = false;
            }
        }




        private void CheckBox157_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox157.Checked)
            {

                checkBox151.Checked = false;
                checkBox152.Checked = false;
                checkBox153.Checked = false;
                checkBox154.Checked = false;
                checkBox155.Checked = false;
                checkBox156.Checked = false;

            }
            else
            {
                checkBox157.Checked = false;
            }
        }

        //************************************************************
        private void CheckBox158_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox158.Checked)
            {

                checkBox159.Checked = false;
                checkBox160.Checked = false;
                checkBox161.Checked = false;
                checkBox162.Checked = false;
                checkBox163.Checked = false;
                checkBox164.Checked = false;

            }
            else
            {
                checkBox158.Checked = false;
            }
        }



        private void CheckBox159_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox159.Checked)
            {

                checkBox158.Checked = false;
                checkBox160.Checked = false;
                checkBox161.Checked = false;
                checkBox162.Checked = false;
                checkBox163.Checked = false;
                checkBox164.Checked = false;

            }
            else
            {
                checkBox159.Checked = false;
            }
        }




        private void CheckBox160_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox160.Checked)
            {

                checkBox158.Checked = false;
                checkBox159.Checked = false;
                checkBox161.Checked = false;
                checkBox162.Checked = false;
                checkBox163.Checked = false;
                checkBox164.Checked = false;

            }
            else
            {
                checkBox160.Checked = false;
            }
        }



        private void CheckBox161_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox161.Checked)
            {

                checkBox158.Checked = false;
                checkBox159.Checked = false;
                checkBox160.Checked = false;
                checkBox162.Checked = false;
                checkBox163.Checked = false;
                checkBox164.Checked = false;

            }
            else
            {
                checkBox161.Checked = false;
            }
        }



        private void CheckBox162_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox162.Checked)
            {

                checkBox158.Checked = false;
                checkBox159.Checked = false;
                checkBox160.Checked = false;
                checkBox161.Checked = false;
                checkBox163.Checked = false;
                checkBox164.Checked = false;

            }
            else
            {
                checkBox162.Checked = false;
            }
        }


        private void CheckBox163_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox163.Checked)
            {

                checkBox158.Checked = false;
                checkBox159.Checked = false;
                checkBox160.Checked = false;
                checkBox161.Checked = false;
                checkBox162.Checked = false;
                checkBox164.Checked = false;

            }
            else
            {
                checkBox163.Checked = false;
            }
        }



        private void CheckBox164_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox164.Checked)
            {

                checkBox158.Checked = false;
                checkBox159.Checked = false;
                checkBox160.Checked = false;
                checkBox161.Checked = false;
                checkBox162.Checked = false;
                checkBox163.Checked = false;

            }
            else
            {
                checkBox164.Checked = false;
            }
        }
        //***************************************************************************************
        private void CheckBox165_CheckStateChanged(object sender, EventArgs e)
        {


            if (checkBox165.Checked)
            {

                checkBox166.Checked = false;
                checkBox167.Checked = false;
                checkBox168.Checked = false;
                checkBox169.Checked = false;
                checkBox170.Checked = false;
                checkBox171.Checked = false;

            }
            else
            {
                checkBox165.Checked = false;
            }
        }



        private void CheckBox166_CheckStateChanged(object sender, EventArgs e)
        {


            if (checkBox166.Checked)
            {

                checkBox165.Checked = false;
                checkBox167.Checked = false;
                checkBox168.Checked = false;
                checkBox169.Checked = false;
                checkBox170.Checked = false;
                checkBox171.Checked = false;

            }
            else
            {
                checkBox166.Checked = false;
            }
        }




        private void CheckBox167_CheckStateChanged(object sender, EventArgs e)
        {


            if (checkBox167.Checked)
            {

                checkBox165.Checked = false;
                checkBox166.Checked = false;
                checkBox168.Checked = false;
                checkBox169.Checked = false;
                checkBox170.Checked = false;
                checkBox171.Checked = false;

            }
            else
            {
                checkBox167.Checked = false;
            }
        }



        private void CheckBox168_CheckStateChanged(object sender, EventArgs e)
        {


            if (checkBox168.Checked)
            {

                checkBox165.Checked = false;
                checkBox166.Checked = false;
                checkBox167.Checked = false;
                checkBox169.Checked = false;
                checkBox170.Checked = false;
                checkBox171.Checked = false;

            }
            else
            {
                checkBox168.Checked = false;
            }
        }



        private void CheckBox169_CheckStateChanged(object sender, EventArgs e)
        {


            if (checkBox169.Checked)
            {

                checkBox165.Checked = false;
                checkBox166.Checked = false;
                checkBox167.Checked = false;
                checkBox168.Checked = false;
                checkBox170.Checked = false;
                checkBox171.Checked = false;

            }
            else
            {
                checkBox169.Checked = false;
            }
        }



        private void CheckBox170_CheckStateChanged(object sender, EventArgs e)
        {


            if (checkBox170.Checked)
            {

                checkBox165.Checked = false;
                checkBox166.Checked = false;
                checkBox167.Checked = false;
                checkBox168.Checked = false;
                checkBox169.Checked = false;
                checkBox171.Checked = false;

            }
            else
            {
                checkBox170.Checked = false;
            }
        }



        private void CheckBox171_CheckStateChanged(object sender, EventArgs e)
        {


            if (checkBox171.Checked)
            {

                checkBox165.Checked = false;
                checkBox166.Checked = false;
                checkBox167.Checked = false;
                checkBox168.Checked = false;
                checkBox169.Checked = false;
                checkBox170.Checked = false;

            }
            else
            {
                checkBox171.Checked = false;
            }
        }

        ///*********************************************************************************

        private void CheckBox172_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox172.Checked)
            {

                checkBox173.Checked = false;
                checkBox174.Checked = false;
                checkBox175.Checked = false;
                checkBox176.Checked = false;
                checkBox177.Checked = false;
                checkBox178.Checked = false;

            }
            else
            {
                checkBox172.Checked = false;
            }
        }



        private void CheckBox173_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox173.Checked)
            {

                checkBox172.Checked = false;
                checkBox174.Checked = false;
                checkBox175.Checked = false;
                checkBox176.Checked = false;
                checkBox177.Checked = false;
                checkBox178.Checked = false;

            }
            else
            {
                checkBox173.Checked = false;
            }
        }


        private void CheckBox174_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox174.Checked)
            {

                checkBox172.Checked = false;
                checkBox173.Checked = false;
                checkBox175.Checked = false;
                checkBox176.Checked = false;
                checkBox177.Checked = false;
                checkBox178.Checked = false;

            }
            else
            {
                checkBox174.Checked = false;
            }
        }



        private void CheckBox175_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox175.Checked)
            {

                checkBox172.Checked = false;
                checkBox173.Checked = false;
                checkBox174.Checked = false;
                checkBox176.Checked = false;
                checkBox177.Checked = false;
                checkBox178.Checked = false;

            }
            else
            {
                checkBox175.Checked = false;
            }
        }



        private void CheckBox176_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox176.Checked)
            {

                checkBox172.Checked = false;
                checkBox173.Checked = false;
                checkBox174.Checked = false;
                checkBox175.Checked = false;
                checkBox177.Checked = false;
                checkBox178.Checked = false;

            }
            else
            {
                checkBox176.Checked = false;
            }
        }




        private void CheckBox177_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox177.Checked)
            {

                checkBox172.Checked = false;
                checkBox173.Checked = false;
                checkBox174.Checked = false;
                checkBox175.Checked = false;
                checkBox176.Checked = false;
                checkBox178.Checked = false;

            }
            else
            {
                checkBox177.Checked = false;
            }
        }



        private void CheckBox178_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox178.Checked)
            {

                checkBox172.Checked = false;
                checkBox173.Checked = false;
                checkBox174.Checked = false;
                checkBox175.Checked = false;
                checkBox176.Checked = false;
                checkBox177.Checked = false;

            }
            else
            {
                checkBox178.Checked = false;
            }
        }

        private void TableLayoutPanel22_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {


            if ((e.Column + e.Row) % 2 == 1)
                e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);
            else
                e.Graphics.FillRectangle(Brushes.Red, e.CellBounds);

            if (e.Column == 1 && e.Row == 0)
            {
                var rectangle = e.CellBounds;
                rectangle.Inflate(-1, -1);
            }
            //    ControlPaint.DrawBorder3D(e.Graphics, rectangle, Border3DStyle.Raised, Border3DSide.All); // 3D border
            //  ControlPaint.DrawBorder(e.Graphics, rectangle, Color.Red, ButtonBorderStyle.Solid); // dotted border




        }

        private void Button3_Click_1(object sender, EventArgs e)
        {

            try
            {

                if (radioButton11.Checked || radioButton14.Checked || radioButton15.Checked || radioButton16.Checked || radioButton13.Checked || radioButton17.Checked)
                {


                    if (MessageBox.Show("Desea eliminar la prueba", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {

                        String comando = "delete from Test_Principal where  Id='" + label12.Text.ToString() + "' ";
                        String comandoFA = "delete from Pruebas where  Id='" + label14.Text.ToString() + "' ";
                        String comandoFD = "delete from Pruebas where  Id='" + label17.Text.ToString() + "' ";
                        String comandoPPA = "delete from PerfilPolaridad where  Id='" + label15.Text.ToString() + "' ";
                        String comandoPPD = "delete from PerfilPolaridad where  Id='" + label16.Text.ToString() + "'  ";
                        String comandoB = "delete from Borg where  id='" + label18.Text.ToString() + "' ";
                        String comandoEn = "delete from Pregunta3 where  id='" + label13.Text.ToString() + "' ";

                        label12.Text = "";
                        label14.Text = "";
                        label17.Text = "";
                        label15.Text = "";
                        label16.Text = "";
                        label18.Text = "";
                        label13.Text = "";

                        var res = Connection.GetInstance().GetConnection();


                        using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                        {
                            c.Open();
                            using (SQLiteCommand comm = new SQLiteCommand(comando, c))
                            {

                                String t = c.PoolCount.ToString();
                                comm.ExecuteNonQuery();


                            }

                        }


                        using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                        {
                            c.Open();
                            using (SQLiteCommand comm = new SQLiteCommand(comandoFA, c))
                            {

                                comm.ExecuteNonQuery();


                            }

                        }


                        using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                        {
                            c.Open();
                            using (SQLiteCommand comm = new SQLiteCommand(comandoFD, c))
                            {

                                comm.ExecuteNonQuery();


                            }

                        }


                        using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                        {
                            c.Open();
                            using (SQLiteCommand comm = new SQLiteCommand(comandoPPA, c))
                            {

                                comm.ExecuteNonQuery();


                            }

                        }


                        using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                        {
                            c.Open();
                            using (SQLiteCommand comm = new SQLiteCommand(comandoPPD, c))
                            {

                                comm.ExecuteNonQuery();


                            }
                        }


                        using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                        {
                            c.Open();
                            using (SQLiteCommand comm = new SQLiteCommand(comandoB, c))
                            {

                                comm.ExecuteNonQuery();

                            }

                        }


                        using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                        {
                            c.Open();
                            using (SQLiteCommand comm = new SQLiteCommand(comandoEn, c))
                            {

                                comm.ExecuteNonQuery();


                            }

                        }


                        dataGridView3.Rows.Clear();

                        radioButton11.Checked = false;
                        radioButton14.Checked = false;
                        radioButton15.Checked = false;
                        radioButton16.Checked = false;
                        radioButton13.Checked = false;
                        radioButton17.Checked = false;

                        mostrarPruebasDiaAtleta();
                        limpiarValoresPruebaFlicker();

                        MessageBox.Show("La prueba ha sido eliminado con éxito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No existen pruebas para este protocolo", "Antención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);

            }


        }

        public void limpiarValoresPruebaFlicker()
        {
            label43.Text = "0.0";
            // label47.Text = "0.0";
            label106.Text = "0.0";
            label114.Text = "0.0";
            label115.Text = "0.0";
            label116.Text = "0.0";
            label142.Text = "0.0";
            label143.Text = "0.0";
            label145.Text = "0.0";
            label37.Text = "0.0";
            label44.Text = "0.0";

            labelDesviacion.Text = "0.0";
            label33.Text = "0.0";


            label20.Text = "";
            label25.Text = "";
            label30.Text = "";
            label35.Text = "";
            label22.Text = "";
            label27.Text = "";
            label32.Text = "";

            dataGridView1.Rows.Clear();

            label145.ForeColor = System.Drawing.Color.Black;
            label144.ForeColor = System.Drawing.Color.Black;

        }

        private void label90_Click(object sender, EventArgs e)
        {

        }

        private void MetroTabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }
    }
}
