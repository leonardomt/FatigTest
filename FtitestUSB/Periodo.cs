using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FtitestUSB
{
    public partial class Periodo : MetroFramework.Forms.MetroForm
    {
        public Periodo()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
                {
                    String periodo = textBox1.Text;
                    String macrocilco = textBox2.Text;
                    String etapa = textBox3.Text;

                    String commandDb = "update Periodo set  periodo = '" + periodo + "', macrociclo = '" + macrocilco + "',etapa = '" + etapa + "' where id = '1' ";

                    using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
                    {
                        c.Open();
                        using (SQLiteCommand comm = new SQLiteCommand(commandDb, c))
                        {

                            comm.ExecuteNonQuery();

                        }

                    }

                    labelPeriodo.Text = textBox1.Text;
                    labelMacrociclo.Text = textBox2.Text;
                    labelEtapa.Text = textBox3.Text;


                    MessageBox.Show(this, "Se ha modificado correctamente el período", "Exito",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else

                    MessageBox.Show(this, "Deben llenar todos los campos.", "Atención",
               MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch
            {

                MessageBox.Show(this, "Ha ocurrido un error inesperado", "Error",
           MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Etapas_Load(object sender, EventArgs e)
        {

            String commandDb = "select * from periodo ";
            SQLiteCommand command = new SQLiteCommand(commandDb, Connection.GetInstance().GetConnection());
           
            using (SQLiteConnection c = new SQLiteConnection(Connection.GetInstance().GetConnectionString()))
            {
                c.Open();
                using (SQLiteCommand comm = new SQLiteCommand(commandDb, c))
                {
                    using (SQLiteDataReader reader = comm.ExecuteReader())
                    {
                        reader.Read();
                        textBox1.Text = reader["periodo"].ToString();
                        textBox2.Text = reader["macrociclo"].ToString();
                        textBox3.Text = reader["etapa"].ToString();

                        labelPeriodo.Text = reader["periodo"].ToString();
                        labelMacrociclo.Text = reader["macrociclo"].ToString();
                        labelEtapa.Text = reader["etapa"].ToString();
                    }


                }

            }

                                  
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
