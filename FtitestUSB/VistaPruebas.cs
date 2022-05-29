using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FtitestUSB
{
    public partial class VistaPruebas : MetroFramework.Forms.MetroForm
    {
        public VistaPruebas()
        {
            InitializeComponent();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void llenarTabla()
        {

            dataGridView1.Rows.Clear();


            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand("select * from Pruebas inner join DatosSujetos   on Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet = '" + comboBox1.SelectedValue.ToString() + "'", c))
                {
                    using (SQLiteDataReader read = comm1.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            dataGridView1.Rows.Add(new object[] {
            read.GetValue(read.GetOrdinal("Nombre")),
            read.GetValue(read.GetOrdinal("PrimerApellido")),
            read.GetValue(read.GetOrdinal("SegundoApellido")),
        //    read.GetValue(read.GetOrdinal("CarnetIdentidad")),
            read.GetValue(read.GetOrdinal("FECHA")),
        //    read.GetValue(read.GetOrdinal("HORA")),
        //    read.GetValue(read.GetOrdinal("Sexo")),
            read.GetValue(read.GetOrdinal("Modalidad")),
            read.GetValue(read.GetOrdinal("Deporte")),
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

        private void habilitarCampo()
        {
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox5.Enabled = true;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;
            button3.Enabled = true;



        }
        private void ComboBox1_DropDown(object sender, EventArgs e)
        {

            SQLiteDataAdapter categoria = new SQLiteDataAdapter("select CarnetIdentidad,(Nombre||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos where Eliminado='0'  ", Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();
            categoria.Fill(data);
            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "fila";
            comboBox1.ValueMember = "CarnetIdentidad";

        }

        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            habilitarCampo();
            llenarTabla();

        }





        public void filtrarTabla()
        {
            String command = crearFiltro();
            dataGridView1.Rows.Clear();



            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand(command, c))
                {
                    using (SQLiteDataReader read = comm1.ExecuteReader())
                    {


                        while (read.Read())
                        {


                            dataGridView1.Rows.Add(new object[] {
            read.GetValue(read.GetOrdinal("Nombre")),
            read.GetValue(read.GetOrdinal("PrimerApellido")),
            read.GetValue(read.GetOrdinal("SegundoApellido")),
          //  read.GetValue(read.GetOrdinal("CarnetIdentidad")),
            read.GetValue(read.GetOrdinal("FECHA")),
          //  read.GetValue(read.GetOrdinal("HORA")),
          //  read.GetValue(read.GetOrdinal("Sexo")),
            read.GetValue(read.GetOrdinal("Modalidad")),
            read.GetValue(read.GetOrdinal("Deporte")),
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




        public String crearFiltro()
        {
            String command = null;
            String y = comboBox4.Text;
            bool flag = false;



            string valorCombo2 = "Seleccione";
            string valorCombo3 = "Seleccione";
            string valorCombo4 = "Seleccione";





            if (comboBox3.SelectedValue == null)
                valorCombo3 = comboBox3.Text;
            else
                valorCombo3 = comboBox3.SelectedValue.ToString();



            if (comboBox4.SelectedValue == null)
                valorCombo4 = comboBox4.Text;
            else
                valorCombo4 = comboBox4.SelectedValue.ToString();




            if (comboBox1.Text != "Seleccione")
            {
                command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'";

                if (checkBox1.Checked)
                {

                    command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'   and  TFRECUENCIA='A' ";

                    if (checkBox1.Checked && checkBox2.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'   and  TFRECUENCIA='A' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox1.Checked && checkBox3.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'  and  TFRECUENCIA='A' and TIPOMEDICION='DESCENDENTE' ";
                    }


                }


                if (checkBox5.Checked)
                {

                    command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'   and  TFRECUENCIA='D' ";

                    if (checkBox5.Checked && checkBox2.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'   and  TFRECUENCIA='D' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox5.Checked && checkBox3.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'  and  TFRECUENCIA='D' and TIPOMEDICION='DESCENDENTE' ";
                    }


                }


                if (checkBox2.Checked)
                {

                    command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'   and TIPOMEDICION='ASCENDENTE' ";

                    if (checkBox2.Checked && checkBox1.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'  and TFRECUENCIA='A' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox2.Checked && checkBox5.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'   and  TFRECUENCIA='D' and TIPOMEDICION='ASCENDENTE' ";
                    }


                }


                if (checkBox3.Checked)
                {

                    command = "select * from Pruebas   inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad  where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'   and TIPOMEDICION='DESCENDENTE' ";

                    if (checkBox3.Checked && checkBox1.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'  and  TFRECUENCIA='A' and TIPOMEDICION='DESCENDENTE' ";

                    }

                    if (checkBox3.Checked && checkBox5.Checked)
                    {
                        command = "select * from Pruebas   inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Ncarnet='" + comboBox1.SelectedValue.ToString() + "'  and  TFRECUENCIA='D' and TIPOMEDICION='DESCENDENTE' ";
                    }



                }





                if (valorCombo2 != "Seleccione")
                {
                    command = command + " and Sexo='" + valorCombo2 + "'";
                }

                if (valorCombo3 != "Seleccione")
                {
                    command = command + "and Modalidad='" + valorCombo3 + "'";
                }

                if (valorCombo4 != "Seleccione")
                {
                    command = command + "and FECHA='" + valorCombo4 + "'";
                }





                if (!checkBox1.Checked && !checkBox5.Checked && !checkBox2.Checked && !checkBox2.Checked && !checkBox3.Checked && valorCombo2 == "Seleccione" && valorCombo3 == "Seleccione" && valorCombo4 == "Seleccione")
                {

                    command = "select* from Pruebas inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad  where Ncarnet = '" + comboBox1.SelectedValue.ToString().ToString() + "' ";
                }
            }

            else
            {
                if (checkBox1.Checked)
                {

                    command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where  TFRECUENCIA='A' ";

                    if (checkBox1.Checked && checkBox2.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where  TFRECUENCIA='A' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox1.Checked && checkBox3.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where  TFRECUENCIA='A' and TIPOMEDICION='DESCENDENTE' ";
                    }

                    flag = true;
                }



                if (checkBox5.Checked)
                {

                    command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where      TFRECUENCIA='D' ";

                    if (checkBox5.Checked && checkBox2.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where  TFRECUENCIA='D' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox5.Checked && checkBox3.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where   TFRECUENCIA='D' and TIPOMEDICION='DESCENDENTE' ";
                    }

                    flag = true;
                }


                if (checkBox2.Checked)
                {

                    command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where  TIPOMEDICION='ASCENDENTE' ";

                    if (checkBox2.Checked && checkBox1.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where   TFRECUENCIA='A' and TIPOMEDICION='ASCENDENTE' ";

                    }

                    if (checkBox2.Checked && checkBox5.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where    TFRECUENCIA='D' and TIPOMEDICION='ASCENDENTE' ";
                    }

                    flag = true;
                }


                if (checkBox3.Checked)
                {

                    command = "select * from Pruebas   inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad  where  TIPOMEDICION='DESCENDENTE' ";

                    if (checkBox3.Checked && checkBox1.Checked)
                    {
                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where  TFRECUENCIA='A' and TIPOMEDICION='DESCENDENTE' ";

                    }

                    if (checkBox3.Checked && checkBox5.Checked)
                    {
                        command = "select * from Pruebas   inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where    TFRECUENCIA='D' and TIPOMEDICION='DESCENDENTE' ";
                    }

                    flag = true;
                }

                //////////////////////////////////////////////////////
                ///Obtener Valor combo real///

                /////////////////////////////////////////////////////////////////////////////////////

                if (!flag)
                {



                    if (valorCombo2 != "Seleccione" && valorCombo3 == "Seleccione" && valorCombo4 == "Seleccione")

                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Sexo='" + valorCombo2 + "'";

                    if (valorCombo2 == "Seleccione" && valorCombo3 != "Seleccione" && valorCombo4 == "Seleccione")

                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Modalidad='" + valorCombo3 + "'";

                    if (valorCombo2 == "Seleccione" && valorCombo3 == "Seleccione" && valorCombo4 != "Seleccione")

                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where FECHA='" + valorCombo4 + "'";

                    if (valorCombo2 != "Seleccione" && valorCombo3 != "Seleccione" && valorCombo4 == "Seleccione")

                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Sexo='" + valorCombo2 + "'  and  Modalidad='" + valorCombo3 + "'";

                    if (valorCombo2 != "Seleccione" && valorCombo3 == "Seleccione" && valorCombo4 != "Seleccione")

                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Sexo='" + valorCombo2 + "'  and  FECHA='" + valorCombo4 + "'";

                    if (valorCombo2 == "Seleccione" && valorCombo3 != "Seleccione" && valorCombo4 != "Seleccione")

                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Modalidad='" + valorCombo3 + "'  and  FECHA='" + valorCombo4 + "'";

                    if (valorCombo2 != "Seleccione" && valorCombo3 != "Seleccione" && valorCombo4 != "Seleccione")

                        command = "select * from Pruebas  inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad where Modalidad='" + valorCombo3 + "'  and  FECHA='" + valorCombo4 + "' and  Sexo='" + valorCombo2 + "'  ";
                }
                else
                {
                    if (valorCombo2 != "Seleccione")
                    {
                        command = command + " and Sexo='" + valorCombo2 + "'";
                    }

                    if (valorCombo3 != "Seleccione")
                    {
                        command = command + "and Modalidad='" + valorCombo3 + "'";
                    }

                    if (valorCombo4 != "Seleccione")
                    {
                        command = command + "and FECHA='" + valorCombo4 + "'";
                    }

                }


                if (!checkBox1.Checked && !checkBox5.Checked && !checkBox2.Checked && !checkBox2.Checked && !checkBox3.Checked && valorCombo2 == "Seleccione" && valorCombo3 == "Seleccione" && valorCombo4 == "Seleccione")
                {

                    command = "select* from Pruebas inner join   DatosSujetos on  Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad    ";
                }

            }




            return command;

        }



        private void CheckBox5_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox1.Checked = false;

            }
            else
            {
                checkBox5.Checked = false;
            }
            filtrarTabla();
        }


        private void CheckBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox5.Checked = false;

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
                checkBox3.Checked = false;

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
                checkBox2.Checked = false;

            }
            else
            {
                checkBox3.Checked = false;

            }
            filtrarTabla();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xls)|*.xls";
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application aplicacion;
                    Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                    Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                    aplicacion = new Microsoft.Office.Interop.Excel.Application();



                    libros_trabajo = aplicacion.Workbooks.Add();
                    hoja_trabajo =
                        (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

                    hoja_trabajo.Cells[1, 1] = "Nombre";
                    hoja_trabajo.Cells[1, 2] = "Primer Apellido";
                    hoja_trabajo.Cells[1, 3] = "Segundo Apellido";
                    hoja_trabajo.Cells[1, 4] = "Carnet de Identidad";
                    hoja_trabajo.Cells[1, 5] = "Fecha";
                    hoja_trabajo.Cells[1, 6] = "Hora";
                    hoja_trabajo.Cells[1, 7] = "Sexo";
                    hoja_trabajo.Cells[1, 8] = "Modalidad";
                    hoja_trabajo.Cells[1, 9] = "Deporte";
                    hoja_trabajo.Cells[1, 10] = "T_Frecuencia";
                    hoja_trabajo.Cells[1, 11] = "Tipo de Medición";
                    hoja_trabajo.Cells[1, 12] = "Media";
                    hoja_trabajo.Cells[1, 13] = "Sumatoria";
                    hoja_trabajo.Cells[1, 14] = "Desviación";
                    hoja_trabajo.Cells[1, 15] = "% Media";

                    hoja_trabajo.Cells[1, 16] = "Medición 1";
                    hoja_trabajo.Cells[1, 17] = "Medición 2";
                    hoja_trabajo.Cells[1, 18] = "Medición 3";
                    hoja_trabajo.Cells[1, 19] = "Medición 4";
                    hoja_trabajo.Cells[1, 20] = "Medición 5";
                    hoja_trabajo.Cells[1, 21] = "Medición 6";
                    hoja_trabajo.Cells[1, 22] = "Medición 7";
                    hoja_trabajo.Cells[1, 23] = "Medición 8";
                    hoja_trabajo.Cells[1, 24] = "Medición 9";
                    hoja_trabajo.Cells[1, 25] = "Medición 10";
                    hoja_trabajo.Cells[1, 26] = "Medición 11";
                    hoja_trabajo.Cells[1, 27] = "Medición 12";
                    hoja_trabajo.Cells[1, 28] = "Medición 13";
                    hoja_trabajo.Cells[1, 29] = "Medición 14";
                    hoja_trabajo.Cells[1, 30] = "Medición 15";
                    hoja_trabajo.Cells[1, 31] = "Medición 16";
                    hoja_trabajo.Cells[1, 32] = "Medición 17";
                    hoja_trabajo.Cells[1, 33] = "Medición 18";
                    hoja_trabajo.Cells[1, 34] = "Medición 19";
                    hoja_trabajo.Cells[1, 35] = "Medición 20";

                    hoja_trabajo.get_Range("A1", "AJ1").Font.Bold = true;
                    //    hoja_trabajo.get_Range("A1", "AG1").VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    //Recorremos el DataGridView rellenando la hoja de trabajo
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                        {
                            hoja_trabajo.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                    }


                    libros_trabajo.SaveAs(fichero.FileName,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    libros_trabajo.Close(true);
                    aplicacion.Quit();


                    MessageBox.Show(this, "Se han exportados los datos correctamente", "Exito",
               MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }

        private void Reportes_Load(object sender, EventArgs e)
        {




            tableLayoutPanel1.BringToFront();
            tableLayoutPanel10.BringToFront();
            tableLayoutPanel21.BringToFront();
            tableLayoutPanel15.BringToFront();


        }




        /*private async void llenarTablaIni()
        {
            String command = "select * from Pruebas inner join DatosSujetos on Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad ";
            dataGridView1.Rows.Clear();
            Connection.GetInstance().GetConnection().Open();
            SQLiteCommand comm = new SQLiteCommand(command, Connection.GetInstance().GetConnection());

            using (DbDataReader read = await comm.ExecuteReaderAsync())
            {


                while (await read.ReadAsync())
                {


                    dataGridView1.Rows.Add(new object[] {
        //    read.GetValue(read.GetOrdinal("Nombre")),
        //    read.GetValue(read.GetOrdinal("PrimerApellido")),
        //    read.GetValue(read.GetOrdinal("SegundoApellido")),
        //    read.GetValue(read.GetOrdinal("CarnetIdentidad")),
            read.GetValue(read.GetOrdinal("FECHA")),
       //     read.GetValue(read.GetOrdinal("HORA")),
        //    read.GetValue(read.GetOrdinal("Sexo")),
            read.GetValue(read.GetOrdinal("Modalidad")),
            read.GetValue(read.GetOrdinal("Deporte")),
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

                read.Close();
            }

            Connection.GetInstance().GetConnection().Close();



        }
        */


        private void ComboBox3_DropDown(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Seleccione")
            {
                SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct Modalidad from Pruebas inner join DatosSujetos on Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad  where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' ", Connection.GetInstance().GetConnection());
                DataTable data = new DataTable();
                categoria.Fill(data);
                comboBox3.DataSource = data;
                comboBox3.DisplayMember = "Modalidad";
                comboBox3.ValueMember = "Modalidad";
            }
            else

            {
                SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct Modalidad from Pruebas inner join DatosSujetos on Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad   ", Connection.GetInstance().GetConnection());
                DataTable data = new DataTable();
                categoria.Fill(data);
                comboBox3.DataSource = data;
                comboBox3.DisplayMember = "Modalidad";
                comboBox3.ValueMember = "Modalidad";
            }

        }

        private void ComboBox4_DropDown(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Seleccione")
            {
                SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct FECHA from Pruebas where Ncarnet='" + comboBox1.SelectedValue.ToString() + "' ", Connection.GetInstance().GetConnection());
                DataTable data = new DataTable();
                categoria.Fill(data);
                comboBox4.DataSource = data;
                comboBox4.DisplayMember = "FECHA";
                comboBox4.ValueMember = "FECHA";
            }
            else
            {
                SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct FECHA from Pruebas ", Connection.GetInstance().GetConnection());
                DataTable data = new DataTable();
                categoria.Fill(data);
                comboBox4.DataSource = data;
                comboBox4.DisplayMember = "FECHA";
                comboBox4.ValueMember = "FECHA";
            }
        }

        private void ComboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filtrarTabla();
        }

        private void ComboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filtrarTabla();
        }

        private void ComboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filtrarTabla();
        }

        private void Button3_Click(object sender, EventArgs e)
        {


            comboBox3.DataSource = null;
            comboBox4.DataSource = null;
            comboBox1.DataSource = null;


            comboBox1.Text = "Seleccione";

            comboBox3.Text = "Seleccione";
            comboBox4.Text = "Seleccione";


            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox5.Checked = false;
            //  llenarTablaIni();

            if (checkBox7.Checked)
                llenarTablaTodoFlicker();
            else
                dataGridView1.Rows.Clear();
        }

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void TableLayoutPanel11_Paint(object sender, PaintEventArgs e)
        {

        }

        /*   private void ComboBox6_DropDown(object sender, EventArgs e)
           {
               SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct CarnetIdentidad,(Nombre||' '||PrimerApellido ||' '||SegundoApellido) As fila FROM DatosSujetos inner join Test_Principal on Test_Principal.Atleta =  DatosSujetos.CarnetIdentidad  ", Connection.GetInstance().GetConnection());
               DataTable data = new DataTable();
               categoria.Fill(data);
               comboBox6.DataSource = data;
               comboBox6.DisplayMember = "fila";
               comboBox6.ValueMember = "CarnetIdentidad";

           }
           */
        /*  private void CheckBox4_CheckStateChanged(object sender, EventArgs e)
          {
              if (checkBox4.Checked)
              {
                  comboBox6.Enabled = false;
                  comboBox6.DataSource = null;
                  comboBox6.Text = "Seleccione";
              }
              else
              {
                  comboBox6.Enabled = true;
              }

          }*/

        /*   private void ComboBox9_DropDown(object sender, EventArgs e)
           {
               SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct Modalidad FROM DatosSujetos inner join Test_Principal on Test_Principal.Atleta =  DatosSujetos.CarnetIdentidad and Eliminado='0' ", Connection.GetInstance().GetConnection());
               DataTable data = new DataTable();
               categoria.Fill(data);
               comboBox9.DataSource = data;
               comboBox9.DisplayMember = "Modalidad";
               comboBox9.ValueMember = "Modalidad";
           }

               */

        /*   private void Button4_Click(object sender, EventArgs e)
           {
               String fechaIni = dateTimePicker1.Value.ToString("yyyy-mm-dd");
               String fechaFin= dateTimePicker2.Value.ToString("yyyy-mm-dd");

               String command = "select * from DatosSujetos inner join Test_Principal on Test_Principal.Atleta =  DatosSujetos.CarnetIdentidad where   Fecha  BETWEEN  '" + fechaIni + "' and '" + fechaFin + "' and Eliminado='0'";  
               bool entro = false;

               bool timeOutOffRange = compare();

               if (!timeOutOffRange)
               {

                   if (checkBox4.Checked || comboBox6.SelectedValue != null)
                   {

                       if (checkBox4.Checked == false && comboBox6.SelectedValue != null)
                       {
                           command = command + " and CarnetIdentidad='" + comboBox6.SelectedValue.ToString() + "'";
                       }

                       if (comboBox9.SelectedValue != null)
                       {
                           command=command + " and Modalidad='"+ comboBox9 .SelectedValue.ToString()+ "'";
                       }



                   }
                   else
                   {
                       MessageBox.Show("Debe seleccionar los atletas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   }


               }
               else
               {
                   MessageBox.Show("La fecha final no debe ser  menor que la inicial  ", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               }



           }*/

        /*   private bool compare( )
           {
               bool res = false;

               if (dateTimePicker1.Value.Month > dateTimePicker2.Value.Month || dateTimePicker1.Value.Day > dateTimePicker2.Value.Day || dateTimePicker1.Value.Year > dateTimePicker2.Value.Year)
                   res = true;

               return res;
           }
           */

        private void ComboBox8_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filtrarTablaEncuesta();
            comboBox5.Enabled = true;
        }

        private void ComboBox8_DropDown(object sender, EventArgs e)
        {
            SQLiteDataAdapter categoria = new SQLiteDataAdapter(" select distinct  CarnetIdentidad,(Nombre||' '||PrimerApellido ||' '||SegundoApellido) As fila from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join Pregunta3 on Test_principal.Pregunta_3=Pregunta3.id where Eliminado='0' ", Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();
            categoria.Fill(data);
            comboBox8.DataSource = data;
            comboBox8.DisplayMember = "fila";
            comboBox8.ValueMember = "CarnetIdentidad";

            comboBox5.Text = "Seleccione";
        }

        private async void filtrarTablaEncuesta()
        {


            String command = "";

            if (!checkBox8.Checked)
            {
                if (comboBox5.SelectedValue != null)
                {
                    command = "select *  from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join Pregunta3 on Test_principal.Pregunta_3=Pregunta3.id where Ncarnet='" + comboBox8.SelectedValue.ToString() + "' and Pregunta3.Fecha = '" + comboBox5.SelectedValue.ToString() + "' ";
                }
                else
                {
                    command = "select *  from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join Pregunta3 on Test_principal.Pregunta_3=Pregunta3.id where Ncarnet='" + comboBox8.SelectedValue.ToString() + "' ";
                }
            }
            else
            {
                command = "select *  from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join Pregunta3 on Test_principal.Pregunta_3=Pregunta3.id where  Pregunta3.Fecha = '" + comboBox5.SelectedValue.ToString() + "' ";
            }


            dataGridView2.Rows.Clear();



            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand(command, c))
                {
                    using (DbDataReader read = await comm1.ExecuteReaderAsync())
                    {


                        while (await read.ReadAsync())
                        {

                            dataGridView2.Rows.Add(new object[] {
             read.GetValue(read.GetOrdinal("Nombre")),
            read.GetValue(read.GetOrdinal("PrimerApellido")),
            read.GetValue(read.GetOrdinal("SegundoApellido")),
                         read.GetValue(read.GetOrdinal("Fecha")),
            read.GetValue(read.GetOrdinal("Respuesta1")),
            read.GetValue(read.GetOrdinal("Respuesta2")),
            read.GetValue(read.GetOrdinal("Respuesta3")),
             read.GetValue(read.GetOrdinal("Deporte")),
            read.GetValue(read.GetOrdinal("Modalidad"))




            });

                        }


                    }
                }

            }

        }

        private void ComboBox5_DropDown(object sender, EventArgs e)
        {

            String conmand = "select distinct Fecha from Pregunta3 ";
            if (!checkBox8.Checked)
                conmand += " where Ncarnet = '" + comboBox8.SelectedValue.ToString() + "' ";

            SQLiteDataAdapter categoria = new SQLiteDataAdapter(conmand, Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();

            categoria.Fill(data);
            comboBox5.DataSource = data;
            comboBox5.DisplayMember = "Fecha";
            comboBox5.ValueMember = "Fecha";
        }

        private void ComboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filtrarTablaEncuesta();
        }

        private void ComboBox6_DropDown(object sender, EventArgs e)
        {
            SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct  CarnetIdentidad, (Nombre || ' ' || PrimerApellido || ' ' || SegundoApellido) As fila from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join  PerfilPolaridad on Test_principal.PerfilPolaridad_Antes = PerfilPolaridad.Id  or  Test_principal.PerfilPolaridad_Despues = PerfilPolaridad.Id where Eliminado='0'", Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();
            categoria.Fill(data);
            comboBox6.DataSource = data;
            comboBox6.DisplayMember = "fila";
            comboBox6.ValueMember = "CarnetIdentidad";

            comboBox7.Text = "Seleccione";

        }

        private void ComboBox6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filtrarTablaPerfilPolaridad();
            comboBox7.Enabled = true;
            checkBox4.Enabled = true;
            checkBox6.Enabled = true;
        }

        private async void filtrarTablaPerfilPolaridad()
        {


            String command = "";

            if (comboBox6.Text != "Seleccione")
            {

                if (comboBox7.SelectedValue != null)
                {
                    command = "select * from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join  PerfilPolaridad on Test_principal.PerfilPolaridad_Antes = PerfilPolaridad.Id  or  Test_principal.PerfilPolaridad_Despues = PerfilPolaridad.Id where Ncarnet ='" + comboBox6.SelectedValue.ToString() + "' and Test_principal.Fecha = '" + comboBox7.SelectedValue.ToString() + "' ";


                }
                else
                {

                    command = "select * from   Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join  PerfilPolaridad on Test_principal.PerfilPolaridad_Antes = PerfilPolaridad.Id  or  Test_principal.PerfilPolaridad_Despues = PerfilPolaridad.Id  where Ncarnet='" + comboBox6.SelectedValue.ToString() + "' ";
                }

                if (checkBox4.Checked)
                    command = command + " and TFrecuencia='A'";

                if (checkBox6.Checked)
                    command = command + " and TFrecuencia='D'";
            }
            else
            {
                command = "select * from   Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join  PerfilPolaridad on Test_principal.PerfilPolaridad_Antes = PerfilPolaridad.Id  or  Test_principal.PerfilPolaridad_Despues = PerfilPolaridad.Id ";

                if (checkBox4.Checked)
                    command = command + " where TFrecuencia='A'";

                if (checkBox6.Checked)
                    command = command + " where TFrecuencia='D'";

                if (comboBox7.SelectedValue != null)
                {
                    if (!checkBox4.Checked && !checkBox6.Checked)
                    {
                        command += "where Test_principal.Fecha = '" + comboBox7.SelectedValue.ToString() + "' ";
                    }
                    else
                        command += "and Test_principal.Fecha = '" + comboBox7.SelectedValue.ToString() + "' ";

                }
            }





            dataGridView3.Rows.Clear();


            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand(command, c))
                {
                    using (DbDataReader read = await comm1.ExecuteReaderAsync())
                    {


                        while (await read.ReadAsync())
                        {

                            dataGridView3.Rows.Add(new object[] {
             read.GetValue(read.GetOrdinal("Nombre")),
            read.GetValue(read.GetOrdinal("PrimerApellido")),
            read.GetValue(read.GetOrdinal("SegundoApellido")),
                          read.GetValue(read.GetOrdinal("Fecha")),
            read.GetValue(read.GetOrdinal("EF")),
            read.GetValue(read.GetOrdinal("A")),
            read.GetValue(read.GetOrdinal("EA")),
            read.GetValue(read.GetOrdinal("TFrecuencia")),
             read.GetValue(read.GetOrdinal("Deporte")),
            read.GetValue(read.GetOrdinal("Modalidad"))




            });

                        }


                    }
                }
            }




        }

        private void ComboBox7_DropDown(object sender, EventArgs e)
        {

            String command = "select distinct Fecha from PerfilPolaridad ";

            if (!checkBox9.Checked)
            {
                command += " where Ncarnet = '" + comboBox6.SelectedValue.ToString() + "'";
            }

            SQLiteDataAdapter categoria = new SQLiteDataAdapter(command, Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();

            categoria.Fill(data);
            comboBox7.DataSource = data;
            comboBox7.DisplayMember = "Fecha";
            comboBox7.ValueMember = "Fecha";
        }

        private void ComboBox7_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filtrarTablaPerfilPolaridad();
        }

        private void CheckBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {

                checkBox6.Checked = false;

            }
            else
            {
                checkBox4.Checked = false;
            }


            filtrarTablaPerfilPolaridad();
        }

        private void CheckBox6_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {

                checkBox4.Checked = false;

            }
            else
            {
                checkBox6.Checked = false;
            }


            filtrarTablaPerfilPolaridad();
        }

        private void ComboBox10_DropDown(object sender, EventArgs e)
        {
            SQLiteDataAdapter categoria = new SQLiteDataAdapter("select distinct  CarnetIdentidad, (Nombre || ' ' || PrimerApellido || ' ' || SegundoApellido) As fila from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join  Borg on Test_principal.Borg = Borg.id  where Eliminado='0'", Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();
            categoria.Fill(data);
            comboBox10.DataSource = data;
            comboBox10.DisplayMember = "fila";
            comboBox10.ValueMember = "CarnetIdentidad";


            comboBox9.Text = "Seleccione";

        }





        private async void filtrarTablaBorg()
        {
            String command = "";

            if (!checkBox10.Checked)
            {
                if (comboBox9.SelectedValue != null)
                {
                    command = "select * from   Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join  Borg on Test_principal.Borg = Borg.id    where Ncarnet ='" + comboBox10.SelectedValue.ToString() + "' and Test_principal.Fecha = '" + comboBox9.SelectedValue.ToString() + "' ";
                }
                else
                {
                    command = "select *  from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join  Borg on Test_principal.Borg = Borg.id    where Ncarnet='" + comboBox10.SelectedValue.ToString() + "' ";
                }
            }
            else
            {
                if (comboBox9.SelectedValue != null)
                {
                    command = "select *  from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join  Borg on Test_principal.Borg = Borg.id where Test_principal.Fecha = '" + comboBox9.SelectedValue.ToString() + "'";
                }
                else
                    command = "select *  from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join  Borg on Test_principal.Borg = Borg.id ";
            }




            dataGridView4.Rows.Clear();

            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand(command, c))
                {
                    using (DbDataReader read = await comm1.ExecuteReaderAsync())
                    {

                        while (await read.ReadAsync())
                        {

                            dataGridView4.Rows.Add(new object[] {
            read.GetValue(read.GetOrdinal("Nombre")),
            read.GetValue(read.GetOrdinal("PrimerApellido")),
            read.GetValue(read.GetOrdinal("SegundoApellido")),
             read.GetValue(read.GetOrdinal("Fecha")),
            read.GetValue(read.GetOrdinal("Esfuerzo")),
            read.GetValue(read.GetOrdinal("Puntuacion")),
            read.GetValue(read.GetOrdinal("Deporte")),
            read.GetValue(read.GetOrdinal("Modalidad"))

            });

                        }


                    }
                }
            }






        }

        private void ComboBox10_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filtrarTablaBorg();

            comboBox9.Enabled = true;
        }

        private void ComboBox9_DropDown(object sender, EventArgs e)
        {
            String command = "select distinct Fecha from Borg ";

            if (!checkBox10.Checked)
                command += " where Ncarnet = '" + comboBox10.SelectedValue.ToString() + "'";

            SQLiteDataAdapter categoria = new SQLiteDataAdapter(command, Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();
            categoria.Fill(data);
            comboBox9.DataSource = data;
            comboBox9.DisplayMember = "Fecha";
            comboBox9.ValueMember = "Fecha";
        }

        private void ComboBox9_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filtrarTablaBorg();
        }

        private void ComboBox11_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void TableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                habilitarCampo();
                comboBox1.Text = "Seleccione";
                comboBox1.Enabled = false;




                llenarTablaTodoFlicker();
            }
            else
            {
                comboBox1.Text = "Seleccione";
                comboBox1.Enabled = true;

                checkBox1.Checked = false;
                checkBox5.Checked = false;
                checkBox3.Checked = false;
                checkBox2.Checked = false;

                comboBox3.Text = "Seleccione";
                comboBox4.Text = "Seleccione";


                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox5.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                button3.Enabled = false;


                dataGridView1.Rows.Clear();
            }

        }

        private void llenarTablaTodoFlicker()
        {

            dataGridView1.Rows.Clear();


            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand("select * from Pruebas inner join DatosSujetos   on Pruebas.Ncarnet = DatosSujetos.CarnetIdentidad ", c))
                {
                    using (SQLiteDataReader read = comm1.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            dataGridView1.Rows.Add(new object[] {
            read.GetValue(read.GetOrdinal("Nombre")),
            read.GetValue(read.GetOrdinal("PrimerApellido")),
            read.GetValue(read.GetOrdinal("SegundoApellido")),
        //    read.GetValue(read.GetOrdinal("CarnetIdentidad")),
            read.GetValue(read.GetOrdinal("FECHA")),
        //    read.GetValue(read.GetOrdinal("HORA")),
        //    read.GetValue(read.GetOrdinal("Sexo")),
            read.GetValue(read.GetOrdinal("Modalidad")),
            read.GetValue(read.GetOrdinal("Deporte")),
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

        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                llenarTablaTodoEncuesta();
                comboBox8.Enabled = false;
                comboBox5.Enabled = true;

                comboBox8.Text = "Seleccione";
                comboBox5.Text = "Seleccione";
            }
            else
            {
                comboBox8.Enabled = true;
                comboBox5.Enabled = false;
                comboBox5.Text = "Seleccione";
                dataGridView2.Rows.Clear();
            }
        }

        private async void llenarTablaTodoEncuesta()
        {



            String command = "select *  from Test_principal inner join DatosSujetos on Test_principal.Atleta = DatosSujetos.CarnetIdentidad  inner join Pregunta3 on Test_principal.Pregunta_3=Pregunta3.id  ";



            dataGridView2.Rows.Clear();



            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand(command, c))
                {
                    using (DbDataReader read = await comm1.ExecuteReaderAsync())
                    {


                        while (await read.ReadAsync())
                        {

                            dataGridView2.Rows.Add(new object[] {
                                  read.GetValue(read.GetOrdinal("Nombre")),
            read.GetValue(read.GetOrdinal("PrimerApellido")),
            read.GetValue(read.GetOrdinal("SegundoApellido")),
                         read.GetValue(read.GetOrdinal("Fecha")),
            read.GetValue(read.GetOrdinal("Respuesta1")),
            read.GetValue(read.GetOrdinal("Respuesta2")),
            read.GetValue(read.GetOrdinal("Respuesta3")),
             read.GetValue(read.GetOrdinal("Deporte")),
            read.GetValue(read.GetOrdinal("Modalidad"))




            });

                        }


                    }
                }

            }
        }

        private void CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                comboBox6.Enabled = false;
                comboBox7.Enabled = false;
                checkBox4.Enabled = true;
                checkBox6.Enabled = true;

                comboBox7.Enabled = true;
                comboBox6.Text = "Seleccione";
                comboBox7.Text = "Seleccione";
                comboBox7.DataSource = null;
                comboBox6.DataSource = null;

                filtrarTablaPerfilPolaridad();

            }
            else
            {
                comboBox6.DataSource = null;
                comboBox7.DataSource = null;
                comboBox6.Enabled = true;
                comboBox7.Enabled = true;
                checkBox4.Enabled = false;
                checkBox6.Enabled = false;
                comboBox7.Text = "Seleccione";
                comboBox6.Text = "Seleccione";


                comboBox7.Enabled = false;
                checkBox4.CheckState = CheckState.Unchecked;
                checkBox6.CheckState = CheckState.Unchecked;

                dataGridView3.Rows.Clear();
            }
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                comboBox10.Enabled = false;
                comboBox9.Enabled = true;

                comboBox10.Text = "Seleccione";
                comboBox9.Text = "Seleccione";
                filtrarTablaBorg();

            }
            else
            {
                comboBox10.Enabled = true;
                comboBox9.Enabled = false;
                comboBox9.Text = "Seleccione";
                dataGridView4.Rows.Clear();


            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {


            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Excel (*.xlsx)|*.xls";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar a Excel";
                    string tie = DateTime.Now.ToString("dd-MM-yyyy");

                    if (comboBox1.Text != "Seleccione")
                        fichero.FileName = comboBox1.Text + "_" + tie;
                    else
                        fichero.FileName = "Flicker" + "_" + tie;

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(fichero.FileName))
                        {
                            File.Delete(fichero.FileName);
                        }





                        Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                        // creating new WorkBook within Excel application  
                        Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                        // creating new Excelsheet in workbook  
                        Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                        // see the excel sheet behind the program  
                        //  app.Visible = true;
                        // get the reference of first sheet. By default its name is Sheet1.  
                        // store its reference to worksheet  
                        worksheet = workbook.Sheets["Hoja1"];
                        worksheet = workbook.ActiveSheet;
                        // changing the name of active sheet  
                        worksheet.Name = "Flicker";
                        // storing header part in Excel  

                        int k = 1;
                        int r = 0;



                        while (r < dataGridView1.Columns.Count - 2)
                        {
                            String headerText = dataGridView1.Columns[r].HeaderText;
                            if (headerText != "Hoja de Perfil" && headerText != "Gráfico" && headerText != "Detalles" && headerText != "id")
                            {
                                worksheet.Cells[1, k] = headerText;
                                k++;

                            }
                            r++;
                        }

                        Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                        oRng.EntireColumn.AutoFit();

                        // storing Each row and column value to excel sheet  

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            int columna = 1;
                            for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                            {
                                String tempVal = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                if (tempVal != "Hoja de Perfil" && tempVal != "Gráfico" && tempVal != "Detalles" && tempVal != "id")
                                {


                                    if (dataGridView1.Columns[j].HeaderText == "Fecha")
                                    {

                                        worksheet.Cells[i + 2, columna] = DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                        //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                        //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                        //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 2, columna] = tempVal;
                                    }


                                    columna++;



                                }

                            }
                        }
                        // save the application  

                        workbook.SaveAs(fichero.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        //app.Visible = true;
                        // Exit from the application  


                        app.Quit();


                        MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }
                else

                    MessageBox.Show(this, "Deben existir valores en la tabla", "Atención",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Un error ha ocurrido exportando el fichero" + ex.Message);
                throw;
            }


        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Excel (*.xlsx)|*.xls";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Export to Excel";
                    string tie = DateTime.Now.ToString("dd-MM-yyyy");

                    if (comboBox8.Text != "Seleccione")
                        fichero.FileName = comboBox8.Text + "Encuesta" + "_" + tie;
                    else
                        fichero.FileName = "Encuesta" + "_" + tie;

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(fichero.FileName))
                        {
                            File.Delete(fichero.FileName);
                        }





                        Microsoft.Office.Interop.Excel._Application app1 = new Microsoft.Office.Interop.Excel.Application();
                        // creating new WorkBook within Excel application  
                        Microsoft.Office.Interop.Excel._Workbook workbook1 = app1.Workbooks.Add(Type.Missing);
                        // creating new Excelsheet in workbook  
                        Microsoft.Office.Interop.Excel._Worksheet worksheet1 = null;
                        // see the excel sheet behind the program  
                        //  app.Visible = true;
                        // get the reference of first sheet. By default its name is Sheet1.  
                        // store its reference to worksheet  
                        worksheet1 = workbook1.Sheets["Hoja1"];
                        worksheet1 = workbook1.ActiveSheet;
                        // changing the name of active sheet  
                        worksheet1.Name = "Encuesta";
                        // storing header part in Excel  

                        int k = 1;
                        int r = 0;



                        while (r < dataGridView2.Columns.Count - 2)
                        {
                            String headerText = dataGridView2.Columns[r].HeaderText;

                            worksheet1.Cells[1, k] = dataGridView2.Columns[r].HeaderText;
                            k++;

                            r++;
                        }

                        Microsoft.Office.Interop.Excel.Range oRng = worksheet1.get_Range("A1", "WWW1");
                        oRng.EntireColumn.AutoFit();

                        // storing Each row and column value to excel sheet  

                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            int columna = 1;
                            for (int j = 0; j < dataGridView2.Columns.Count - 2; j++)
                            {
                                String tempVal = dataGridView2.Rows[i].Cells[j].Value.ToString();


                                if (dataGridView2.Columns[j].HeaderText == "Fecha")
                                {

                                    worksheet1.Cells[i + 2, columna] = DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                }
                                else
                                {
                                    worksheet1.Cells[i + 2, columna] = tempVal;
                                }


                                columna++;





                            }
                        }
                        // save the application  

                        workbook1.SaveAs(fichero.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        //app.Visible = true;
                        // Exit from the application  


                        app1.Quit();


                        MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }
                else

                    MessageBox.Show(this, "Deben existir valores en la tabla", "Atención",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Un error ha ocurrido exportando el fichero" + ex.Message);
                throw;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.Rows.Count > 0)
                {
                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Excel (*.xlsx)|*.xls";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Export to Excel";
                    string tie = DateTime.Now.ToString("dd-MM-yyyy");

                    if (comboBox6.Text != "Seleccione")
                        fichero.FileName = comboBox6.Text + "Perfil de Polaridad" + "_" + tie;
                    else
                        fichero.FileName = "Perfil de Polaridad" + "_" + tie;

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(fichero.FileName))
                        {
                            File.Delete(fichero.FileName);
                        }





                        Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                        // creating new WorkBook within Excel application  
                        Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                        // creating new Excelsheet in workbook  
                        Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                        // see the excel sheet behind the program  
                        //  app.Visible = true;
                        // get the reference of first sheet. By default its name is Sheet1.  
                        // store its reference to worksheet  
                        worksheet = workbook.Sheets["Hoja1"];
                        worksheet = workbook.ActiveSheet;
                        // changing the name of active sheet  
                        worksheet.Name = "perfil_de_Polaridad";
                        // storing header part in Excel  

                        int k = 1;
                        int r = 0;



                        while (r < dataGridView3.Columns.Count - 2)
                        {


                            worksheet.Cells[1, k] = dataGridView3.Columns[r].HeaderText;
                            k++;
                            r++;
                        }

                        Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                        oRng.EntireColumn.AutoFit();

                        // storing Each row and column value to excel sheet  

                        for (int i = 0; i < dataGridView3.Rows.Count; i++)
                        {
                            int columna = 1;
                            for (int j = 0; j < dataGridView3.Columns.Count - 2; j++)
                            {
                                String tempVal = dataGridView3.Rows[i].Cells[j].Value.ToString();



                                if (dataGridView3.Columns[j].HeaderText == "Fecha")
                                {

                                    worksheet.Cells[i + 2, columna] = DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                }
                                else
                                {
                                    worksheet.Cells[i + 2, columna] = tempVal;
                                }


                                columna++;





                            }
                        }
                        // save the application  

                        workbook.SaveAs(fichero.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        //app.Visible = true;
                        // Exit from the application  


                        app.Quit();


                        MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }
                else

                    MessageBox.Show(this, "Deben existir valores en la tabla", "Atención",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Un error ha ocurrido exportando el fichero" + ex.Message);
                throw;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.Rows.Count > 0)
                {
                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Excel (*.xlsx)|*.xls";
                    fichero.InitialDirectory = "C:\\Users\\" + Environment.UserName.ToString() + "\\Desktop";
                    fichero.Title = "Exportar a Excel";
                    string tie = DateTime.Now.ToString("dd-MM-yyyy");

                    if (comboBox10.Text != "Seleccione")
                        fichero.FileName = comboBox10.Text + "Escala de Borg" + "_" + tie;
                    else
                        fichero.FileName = "Escala de Borg" + "_" + tie;

                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(fichero.FileName))
                        {
                            File.Delete(fichero.FileName);
                        }


                        Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                        // creating new WorkBook within Excel application  
                        Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                        // creating new Excelsheet in workbook  
                        Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                        // see the excel sheet behind the program  
                        //  app.Visible = true;
                        // get the reference of first sheet. By default its name is Sheet1.  
                        // store its reference to worksheet  
                        worksheet = workbook.Sheets["Hoja1"];
                        worksheet = workbook.ActiveSheet;
                        // changing the name of active sheet  
                        worksheet.Name = "escala_de_borg";
                        // storing header part in Excel  

                        int k = 1;
                        int r = 0;



                        while (r < dataGridView4.Columns.Count - 2)
                        {
                            String headerText = dataGridView4.Columns[r].HeaderText;

                            worksheet.Cells[1, k] = headerText;
                            k++;


                            r++;
                        }

                        Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range("A1", "WWW1");
                        oRng.EntireColumn.AutoFit();

                        // storing Each row and column value to excel sheet  

                        for (int i = 0; i < dataGridView4.Rows.Count; i++)
                        {
                            int columna = 1;
                            for (int j = 0; j < dataGridView4.Columns.Count - 2; j++)
                            {
                                String tempVal = dataGridView4.Rows[i].Cells[j].Value.ToString();



                                if (dataGridView4.Columns[j].HeaderText == "Fecha")
                                {

                                    worksheet.Cells[i + 2, columna] = DateTime.Parse(tempVal).ToString("dd/MM/yyyy");
                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].EntireColumn.NumberFormat = "DD/MM/YYYY";
                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                                    //    worksheet.Range[worksheet.Cells[i + 2], worksheet.Cells[i + 2]].WrapText = true;
                                }
                                else
                                {
                                    worksheet.Cells[i + 2, columna] = tempVal;
                                }


                                columna++;





                            }
                        }
                        // save the application  

                        workbook.SaveAs(fichero.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        //app.Visible = true;
                        // Exit from the application  


                        app.Quit();


                        MessageBox.Show("Se han exportado los resultados exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start(fichero.FileName);
                    }

                }
                else

                    MessageBox.Show(this, "Deben existir valores en la tabla", "Atención",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Un error ha ocurrido exportando el fichero" + ex.Message);
                throw;
            }
        }
    }
}
