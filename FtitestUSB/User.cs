using MetroFramework;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace FtitestUSB
{
    public partial class User : MetroFramework.Forms.MetroForm
    {
        private String carnet;
        private String nombre;
        private String apellido1;
        private String apellido2;

        private String sexo;
        private String edad;
        private String deporte;
        private String nivelEscolar;
        private String modalidad;

        bool modificar = false;
        public User()
        {
            InitializeComponent();
        }

        private void User_Load(object sender, EventArgs e)
        {

            int width = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Size.Width / 1.3);
            int Height = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Size.Height / 1.3);
            this.Size = new System.Drawing.Size(width, Height);
            updateTable();


        }

        private void validarLetras(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;

            }
        }


        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            carnet = textBoxCarnet.Text;
            nombre = textBoxNombre.Text;
            apellido2 = textBoxApellido2.Text;
            apellido1 = textBoxApellido1.Text;

            deporte = comboBoxDeporte.Text;
         
            sexo = comboBoxSexo.Text;
            nivelEscolar = comboBoxNivelEsco.Text;
            modalidad = comboBoxDivision.Text;
             
            edad = textBoxEdad.Text;

            try
            {
                if (validarCampo())
                {

                    if (!SearchValor())
                    {

                        String commandDb = "Insert into DatosSujetos (CarnetIdentidad, Nombre,PrimerApellido,SegundoApellido,Sexo,Edad,Deporte,NivelEscolar,Modalidad, Eliminado)" +
                          " values('" + carnet + "', '" + nombre + "','" + apellido1 + "','" + apellido2 + "','" + sexo + "','" + edad + "','" + deporte + "','" + nivelEscolar + "','" + modalidad + "', 0)";
                        SQLiteCommand command = new SQLiteCommand(commandDb, Connection.GetInstance().GetConnection());

                        Connection.GetInstance().GetConnection().Open();
                        command.ExecuteNonQuery();
                        Connection.GetInstance().GetConnection().Close();


                        clearCampos();
                        updateTable();


                        MessageBox.Show("El atleta ha sido añadido correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        if (modificar)
                        {

                            if (MessageBox.Show("Desea modificar el atleta ?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                String commandDb = "update DatosSujetos set  CarnetIdentidad= '" + carnet + "' ,Nombre= '" + nombre + "',PrimerApellido= '" + apellido1 + "',SegundoApellido= '" + apellido2 + "',Sexo= '" + sexo + "',Edad= '" + edad + "',Deporte= '" + deporte + "',NivelEscolar= '" + nivelEscolar + "',Modalidad= '" + modalidad + "'  where  CarnetIdentidad = '" + carnet + "'";
                                SQLiteCommand command = new SQLiteCommand(commandDb, Connection.GetInstance().GetConnection());

                                Connection.GetInstance().GetConnection().Open();
                                command.ExecuteNonQuery();
                                Connection.GetInstance().GetConnection().Close();

                                updateTable();
                                clearCampos();
                                modificar = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("El atleta ya existe. Verifique el número de carnet", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                }

            }
            catch (Exception)
            {

                MetroMessageBox.Show(this, "Deben existir valores en la tabla.", "Atención",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);



            }
        }

        private void updateTable()
        {


            dataGridView1.Rows.Clear();

            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand("select * from DatosSujetos where Eliminado=0 ", c))
                {

                    using (SQLiteDataReader read = comm1.ExecuteReader())
                    {
                        while (read.Read())
                        {

                            dataGridView1.Rows.Add(new object[] {
            read.GetValue(read.GetOrdinal("Nombre")),  // Or column name like this
            read.GetValue(read.GetOrdinal("PrimerApellido")),
            read.GetValue(read.GetOrdinal("SegundoApellido")),
            read.GetValue(read.GetOrdinal("CarnetIdentidad")),
            read.GetValue(read.GetOrdinal("Modalidad")),
            read.GetValue(read.GetOrdinal("Edad")),
            read.GetValue(read.GetOrdinal("NivelEscolar")),
            read.GetValue(read.GetOrdinal("Sexo")),
            read.GetValue(read.GetOrdinal("Deporte"))

            });

                        }


                    }
                }

            }



        }

        private void clearCampos()
        {

            textBoxCarnet.Text = "";
            textBoxNombre.Text = "";
            textBoxApellido2.Text = "";
            textBoxApellido1.Text = "";
        
            comboBoxSexo.Text = "Seleccione";
            comboBoxNivelEsco.Text = "Seleccione";
            comboBoxDivision.Text = "Seleccione";
     
            textBoxEdad.Text = "";
            textBoxCarnet.Enabled = true;
            comboBoxDeporte.Text = "Seleccione";
            modificar = false;

        }


        private bool SearchValor()
        {

            Connection.GetInstance().abrirConexion();

            bool res = false;
            String comand = "select * from DatosSujetos  where CarnetIdentidad='" + carnet + "' and Eliminado = 0";


            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm1 = new SQLiteCommand(comand, c))
                {

                    using (SQLiteDataReader reader = comm1.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            res = true;
                        }

                    }
                }

            }

            return res;




        }

        private bool validarCampo()
        {
            bool result = false;


            if (deporte == "Seleccione" || edad == "" || carnet == "" || nombre == "" || apellido1 == "" || apellido2 == "" || sexo == "Seleccione" || nivelEscolar == "Seleccione" /*|| modalidad == "Seleccione"*/)
            {
                MetroMessageBox.Show(this, "Debe llenar llenar todos los datos.", "Atención", MessageBoxButtons.OK,
                          MessageBoxIcon.Warning);

            }
            else
            {
                if (modalidad == "Seleccione")
                    modalidad = "";
                result = true;
            }
                

            return result;
        }

        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            modificar = true;
            textBoxCarnet.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxNombre.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxApellido1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxApellido2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            comboBoxDivision.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBoxEdad.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBoxDeporte.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            comboBoxNivelEsco.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboBoxSexo.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

            textBoxCarnet.Enabled = false;
        }


        private void eliminarAtleta(object sender, EventArgs e)
        {

            try
            {
                if (textBoxCarnet.Text != "")
                {
                    if (MessageBox.Show("Desea eliminar el atleta", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {

                        String tem = textBoxCarnet.Text;




                        using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                        {
                            c.Open();
                            using (SQLiteCommand comm1 = new SQLiteCommand("update DatosSujetos  set Eliminado = 1 where CarnetIdentidad='" + tem + "' ", c))
                            {

                                comm1.ExecuteNonQuery();
                            }

                        }
                        updateTable();
                        clearCampos();








                        MessageBox.Show("El atleta ha sido eliminado correctamente", "Exito", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    }
                }
                else
                    MessageBox.Show("Debe seleccionar un atleta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception)
            {

            }
        }

        private void ComboBoxDivision_DropDown(object sender, EventArgs e)
        {
            SQLiteDataAdapter categoria = new SQLiteDataAdapter("select * from Modalidad ", Connection.GetInstance().GetConnection());
            DataTable data = new DataTable();
            categoria.Fill(data);
            comboBoxDivision.DataSource = data;
            comboBoxDivision.DisplayMember = "Modalidad";
            comboBoxDivision.ValueMember = "Modalidad";
        }

        private void TextBoxCarnet_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
      
        }

        private void textBoxEdad_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TableLayoutPanel1_Click(object sender, EventArgs e)
        {
            clearCampos();
        }
    }

}
