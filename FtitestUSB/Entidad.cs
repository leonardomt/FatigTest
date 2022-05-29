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
    public partial class Entidad : MetroFramework.Forms.MetroForm
    {

     
        public Entidad()
        {
            InitializeComponent();
            updateTable();

        }



        private void Entidades_Load(object sender, EventArgs e)
        {
            int width = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Size.Width / 1.3);
            int Height = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Size.Height / 1.3);
            this.Size = new System.Drawing.Size(width, Height);
            dataGridView1.Columns[0].Width = dataGridView1.Width;
        }

        private void FlowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

            try
            {

                if (textBox1.Text != "")
                {
                    String Entidad = textBox1.Text;

                    String commandDb = "insert into Entidades ( Entidad) values ('" + Entidad + "')";
                    SQLiteCommand command = new SQLiteCommand(commandDb, Connection.GetInstance().GetConnection());
                    Connection.GetInstance().GetConnection().Open();
                    command.ExecuteNonQuery();
                    Connection.GetInstance().GetConnection().Close();

                    updateTable();
                }
                else
                    MetroMessageBox.Show(this, "Deben existir valores en la tabla.", "Atención",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);


            }
            catch
            {

            }


        }

        private void updateTable()
        {
            dataGridView1.Rows.Clear();
            //    dataGridView1.Columns[0].Width = dataGridView1.Width;
            Connection.GetInstance().GetConnection().Open();
            SQLiteCommand comm = new SQLiteCommand("select * from Entidades  ", Connection.GetInstance().GetConnection());
            using (SQLiteDataReader read = comm.ExecuteReader())
            {
                while (read.Read())
                {

                    dataGridView1.Rows.Add(new object[] {
            read.GetValue(read.GetOrdinal("Entidad"))  // Or column name like this
           

            });

                }

                read.Close();
            }

            Connection.GetInstance().GetConnection().Close();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String entidad = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (MessageBox.Show("Desea eliminar la entidad", "Eliminar",MessageBoxButtons.YesNo)== DialogResult.Yes)
            {
                String commandDb = "delete from Entidades where Entidad='" + entidad + "'";
                SQLiteCommand command = new SQLiteCommand(commandDb, Connection.GetInstance().GetConnection());
                Connection.GetInstance().GetConnection().Open();
                command.ExecuteNonQuery();
                Connection.GetInstance().GetConnection().Close();
                MessageBox.Show("La entidad ha sido eliminada con éxito", "Exito", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                updateTable();

            }
            else
                MetroMessageBox.Show(this, "Debe seleccionar un valor en la tabla", "Atención",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
