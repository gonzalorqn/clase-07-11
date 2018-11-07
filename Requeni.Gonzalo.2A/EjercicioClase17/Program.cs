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
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conexion = new SqlConnection(Properties.Settings.Default.conexion);//conexion es la variable que creamos en la configuracion
            SqlCommand comando = new SqlCommand();
            List<Televisor> televisores = new List<Televisor>();

            comando.CommandText = "SELECT *FROM Televisores";         //estos dos van de la mano
            comando.CommandType = System.Data.CommandType.Text;

            comando.Connection = conexion; //es una propiedad

            conexion.Open();//abre conexion

            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Console.Write(lector["pais"] + " - ");
                //Console.Write(lector[0] + " - ");

                Televisor tele = new Televisor(lector.GetInt32(0), lector.GetString(1), lector.GetDouble(2), lector.GetInt32(3), lector.GetString(4));//leo lo de la base de datos y lo agrego a la lista de  televisor
                                                                                                                                                      //  Televisor tele = new Televisor((int)lector["id"],(string) lector["marca"], (double)lector["precio"], (int)lector["pulgadas"], (string)lector["pais"]);//todos devuelven object , ahora tenemos que castearlo

                televisores.Add(tele);
            }

            conexion.Close();//cierra conexion

            XmlSerializer sr = new XmlSerializer(typeof(List<Televisor>)); // nole pasamos un solo televisr sino la LISTA para que serialize una coleccion de objetos
            XmlTextWriter xw = new XmlTextWriter("Televisor.xml", Encoding.UTF8);
            XmlTextReader xr = new XmlTextReader("Televisor.xml");
            sr.Serialize(xw, televisores);
            xw.Close(); // lo cerramos pa que no pinche

            List<Televisor> listaTelevisorRead;
            listaTelevisorRead = (List<Televisor>)sr.Deserialize(xr);
            xr.Close();

            conexion.Open();

            lector = comando.ExecuteReader(); //genero replica 

            DataTable dataTable = new DataTable("Televisor"); //representacion en memoria de una tabal en base de datos

            dataTable.Load(lector); //ya lo agrega al data table

            conexion.Close(); //volvemos a cerrar

            dataTable.WriteXmlSchema("Televisores_esquema.xml"); //guardamos el esquema
            dataTable.WriteXml("Televisore_dt.xml"); //leemos el esquema

            DataTable dataTable2 = new DataTable();

            dataTable2.ReadXmlSchema("Televisores_esquema.xml");//primero leemos el esquema
            dataTable2.ReadXml("Televisore_dt.xml");

            Televisor telenueva = new Televisor(5, "sony", 22000, 45, "Estados Unidos");

            Console.WriteLine(telenueva.Insertar());

            Console.ReadKey();
        }
    }
}