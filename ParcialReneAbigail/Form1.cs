using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParcialReneAbigail
{
    public partial class Form1 : Form
    {
        string connectionString = @"Server=LAPTOP-MTE4OKKU\SQLEXPRESS,trusted_Connection=True";
        bool nuevo;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            {
                tsbNuevo.Enabled = true;
                tsbGuardar.Enabled = false;


                tsbBuscar.Enabled = true;
                tsbEliminar.Enabled = false;
                txtId.Enabled = true;
                tsbBuscar.Enabled = true;
                txtNombre.Enabled = false;
                txtPrecio.Enabled = false;
            }
        }


        private void txtId_TextChanged(object sender, EventArgs e)
            {

            }

        private void tsbNuevo_Click_1(object sender, EventArgs e)
        {
            // Cargo el estado para el botón Nuevo y los demás elementos.
            tsbNuevo.Enabled = false;
            tsbGuardar.Enabled = true;
            tsbEliminar.Enabled = false;
            tsbBuscar.Enabled = false;
            txtNombre.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = true;
            txtNombre.Focus();
            nuevo = true;
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            {
                {
                    // Cuando hago click en el botón Nuevo que proceda la inserción de un registro
            if (nuevo)
                    {
                        string sql = "INSERT INTO RENEABIGAIL (ID, NOMBRE, PRECIO, STOCK)"
                                  + "VALUES ('" + txtId.Text + "', '" + txtNombre.Text + "', '" + txtPrecio.Text
                                  + "', '" + txtStock.Text + "')";

                        SqlConnection con = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        try
                {
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                                MessageBox.Show("Registro ingresado correctamente !");
                       }
                catch (Exception ex)
                        {
                            MessageBox.Show("Erro: " + ex.ToString());
                        }
                        finally
                        {
                            // Cierro la Conexión. 
                            con.Close();
                        }
                    }
                    else
                    {
                        // Procedo a realizar la actualización del registro en la Base de Datos. 
                        string sql = "UPDATE PARCIALRENEABIGAIL SET NOMBRE='" + txtNombre.Text +
                            "', PRECIO='" + txtPrecio.Text +
                            "', " + "STOCK='" + txtStock.Text + "' WHERE id=" + txtId.Text + "";

                        SqlConnection con = new SqlConnection(connectionString);
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        try
                        {
                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                                MessageBox.Show("Registro actualizado correctamente !");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro: " + ex.ToString());
                        }
                        finally
                        {
                            // Cierro la Conexión. 
                            con.Close();
                        }
                    }


                }
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            {
                tsbNuevo.Enabled = true;
                tsbGuardar.Enabled = false;
                tsbEliminar.Enabled = false;
                txtId.Enabled = true;
                tsbBuscar.Enabled = true;
                txtNombre.Enabled = false;
                txtPrecio.Enabled = false;
                txtId.Text = "";
                txtNombre.Text = "";
                txtPrecio.Text = "";
                
            }


        }
        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            {
                string sql = "SELECT * FROM Productos WHERE IdProducto=" + tstId.Text;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader;

                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tsbNuevo.Enabled = false;
                        tsbGuardar.Enabled = true;
                        tsbEliminar.Enabled = true;
                        txtId.Enabled = false;
                        tsbBuscar.Enabled = false;
                        txtNombre.Enabled = true;
                        txtPrecio.Enabled = true;
                        txtNombre.Focus();

                        txtId.Text = reader[0].ToString();
                        txtNombre.Text = reader[1].ToString();
                        txtPrecio.Text = reader[3].ToString();
                        nuevo = false;
                    }
                    else
                    {
                        MessageBox.Show("¡Ningún registro encontrado con el Id ingresado!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
   





