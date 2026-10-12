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
        string connectionString = @"Server=localhost\SQLEXPRESS; Database=ParcialProgramacion1; trusted_Connection=True;";
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
                txtNombre.Enabled = false;
                txtCategoria.Enabled = false;
                txtPrecio.Enabled = false;
                txtStock.Enabled = false;
            }
        }

        private void tsbNuevo_Click_1(object sender, EventArgs e)
        {
            // Agrego el estado para el botón Nuevo y los demás elementos.
            tsbNuevo.Enabled = false;
            tsbGuardar.Enabled = true;
            tsbBuscar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbEliminar.Enabled = false;
            txtId.Enabled = false;
            txtNombre.Enabled = true;
            txtCategoria.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = true;
            txtNombre.Focus();
            nuevo = true;
        }
        private void tsbGuardar_Click(object sender, EventArgs e)
        {
             
             // Cuando hago click en el botón de Nuevo, para que proceda la inserción de un registro
          if (nuevo)
          {
             string sql = "INSERT INTO Productos (Nombre, Categoria, Precio, Existencias)"
                                  + "VALUES ('" + txtNombre.Text + "', '" + txtCategoria.Text + "', '" + txtPrecio.Text + "', '" + txtStock.Text + "')";
              
             //Agrego conexion a sql
             SqlConnection con = new SqlConnection(connectionString);
             SqlCommand cmd = new SqlCommand(sql, con);
             cmd.CommandType = CommandType.Text;
             con.Open();

             //Pongo un try catch para que atrape errores y muestre mensaje de error.
          try
          {
             int i = cmd.ExecuteNonQuery();
             if (i > 0)
             MessageBox.Show("Registro ingresado correctamente !");
          }
          catch (Exception ex)
          {
             MessageBox.Show("Error: " + ex.ToString());
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
             string sql = "UPDATE Productos SET Nombre='" + txtNombre.Text +
                        "', Categoria='" +txtCategoria.Text +
                        "', Precio='" + txtPrecio.Text +
                        "', Existencias='" + txtStock.Text + 
                        "' WHERE id=" + txtId.Text ;

              SqlConnection con = new SqlConnection(connectionString);
              SqlCommand cmd = new SqlCommand(sql, con);
              cmd.CommandType = CommandType.Text;
              con.Open();
          try
          {
              int i = cmd.ExecuteNonQuery();
              if (i > 0)
              MessageBox.Show("Registro actualizado correctamente!");
          }
          catch (Exception ex)
          {
              MessageBox.Show("Error: " + ex.ToString());
          }
          finally
          {
              // Cerramos la Conexión. 
              con.Close();
          }

          }
            //Pongo nuevamente los estados de los botones después de actualizar 
            txtId.Text = "";
            txtNombre.Text = "";
            txtCategoria.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            tstld.Text = "";

            tsbNuevo.Enabled = true;
            tsbGuardar.Enabled = false;
            tsbBuscar.Enabled = true;
            tsbCancelar.Enabled = false;
            tsbEliminar.Enabled = false;
            tstld.Enabled = true;
            txtId.Enabled = false;
            txtNombre.Enabled = false;
            txtCategoria.Enabled = false;
            txtPrecio.Enabled = false;
            txtStock.Enabled = false;
        }


        private void Cancelar_Click(object sender, EventArgs e)
        {
            //Agrego los estados para los elementos del form para el boton cancelar
            txtId.Text = "";
            txtNombre.Text = "";
            txtCategoria.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            tstld.Text = "";

            tsbNuevo.Enabled = true;
            tsbGuardar.Enabled = false;
            tsbBuscar.Enabled = true;
            tsbCancelar.Enabled = false;
            tsbEliminar.Enabled = false;
            tstld.Enabled = true;
            txtId.Enabled = false;
            txtNombre.Enabled = false;
            txtCategoria.Enabled = false;
            txtPrecio.Enabled = false;
            txtStock.Enabled = false;
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            // Si hace click en el botón eliminar procede a eliminar en la Base de Datos. 
            string sql = "DELETE FROM Productos WHERE ID=" + txtId.Text;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();

        try
        {
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            MessageBox.Show("Registro eliminado correctamente !");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro: " + ex.ToString());
        }
        finally
        {
            // Cierro la Conexión 
            con.Close();
        }

            // Defino otra vez estados para los elementos del Form, cuando se haga click en el boton eliminar. 
            tsbNuevo.Enabled = true;
            tsbGuardar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbEliminar.Enabled = false;
            tstld.Enabled = true;
            tsbBuscar.Enabled = true;
            txtNombre.Enabled = false;
            txtPrecio.Enabled = false;
            txtStock.Enabled = false;
            txtId.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
        }
        

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            //Insertamos conexion nuevamente con la BD, porque va a buscar registros.
            string sql = "SELECT * FROM Productos WHERE IdProducto = @Id";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            if (!int.TryParse(tstld.Text, out int id))
            {
                MessageBox.Show("Por favor solo ingrese un Id valido).");
                return;
            }
            cmd.Parameters.AddWithValue("@Id", int.Parse(tstld.Text));
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();

            //Agregamos otro atrapa errores
        try
        {         
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
             
             
             txtId.Text = reader["IdProducto"].ToString();
             txtNombre.Text = reader["Nombre"].ToString();
             txtCategoria.Text = reader["Categoria"].ToString();
             txtPrecio.Text = reader["Precio"].ToString();
             txtStock.Text = reader["Existencias"].ToString();
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
              //Cierro la conexion nuevamente.
              con.Close();
        }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
        //Finalmente, al dar click en el boton salir cierro la pestaña del formulario
        this.Close();
        }

    }
}
   





