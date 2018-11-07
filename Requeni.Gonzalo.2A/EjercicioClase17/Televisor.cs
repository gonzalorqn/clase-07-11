using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //using para que se conecte con la base de datos
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace EjercicioClase17
{
    public class Televisor
    {
        #region Atributos

        public int id;
        public string marca;
        public double precio;
        public int pulgadas;
        public string pais;

        #endregion

        #region Constructores
        public Televisor()
        {

        }
        public Televisor(int id, string marca, double precio, int pulgadas, string pais)
        {
            this.id = id;
            this.marca = marca;
            this.precio = precio;
            this.pulgadas = pulgadas;
            this.pais = pais;
        }

        #endregion

        #region Metodos

        public bool Insertar()
        {
            bool retorno = false;

            SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);
            SqlCommand comando = new SqlCommand();


            comando.CommandText = string.Format("INSERT INTO Televisores values ({0},'{1}',{2},{3},'{4}')", this.id, this.marca, this.precio, this.pulgadas, this.pais);
            comando.CommandType = System.Data.CommandType.Text;
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery(); //ejecuta consultas que no van a retornar ningun tipo de resultado
                conexion.Close();
                retorno = true;
            }
            catch (Exception e)
            {
                throw e;
            }


            return retorno;
        }

        #endregion
    }
}