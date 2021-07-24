using System;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Web;

namespace SpeedSolutions
{

    public class SQLBaseClass : IDisposable
    {
        protected SqlCommand Comando;
        public string Servidor = "servidor1";
        /// <summary>
        /// String que contiene la cadena de conexion
        /// </summary>
        protected string cadenaConexion;

        /// <summary>
        /// Obtiene la cadena de conexion (solo lectura)
        /// </summary>
        public string CadenaConexion
        {
            get { return cadenaConexion; }
        }

        /// <summary>
        /// Establece la conexion al servidor SQLServer
        /// </summary>
        public SqlConnection Conexion;

        /// <summary>
        /// Establece el comando a ejecutar
        /// </summary>
        public SqlCommand sqlCommand;

        /// <summary>
        /// DataReader
        /// </summary>
        private SqlDataReader _dataReader;

        /// <summary>
        /// DataReader que contiene la informacion de la consulta
        /// </summary>
        public SqlDataReader DataReader
        {
            get { return _dataReader; }
        }

        private SqlDataAdapter _dataAdapter;

        public SqlDataAdapter DataAdapter
        {
            get { return _dataAdapter; }
            set { _dataAdapter = value; }
        }

        public SqlTransaction Transaction { get; set; }

        /// <summary>
        /// Inicializa la cadena de conexion y el objetod e conexion a la base de datos
        /// </summary>
        public SQLBaseClass()
        {

            //this.Conexion = new SqlConnection(this.cadenaConexion);
        }

        /// <summary>
        /// Destruye el Objeto de Conexion
        /// </summary>
        ~SQLBaseClass()
        {
            this.LiberarMemoria();
        }

        /// <summary>
        /// Destruye los objetos
        /// </summary>
        public void Dispose()
        {
            this.LiberarMemoria();
        }

        /// <summary>
        /// Libera la memoria de los objetos
        /// </summary>
        private void LiberarMemoria()
        {
            if (this.Conexion != null)
            {
                this.CerrarConexion();
                this.Conexion.Dispose();
            }
            if (this.sqlCommand != null)
                this.sqlCommand.Dispose();
        }

        public void ObtenerCadenaConexion()
        {
            ObtenerCadenaConexion(this.Servidor);
        }
        /// <summary>
        /// Obtiene la cadenad e conexion
        /// </summary>
        public void ObtenerCadenaConexion(string servidor)
        {
            this.ObtenerCadenaConexion2(servidor);

        }

        /// <summary>
        /// Obtiene la cadena de conexion y la regresa como string.
        /// </summary>
        public string ObtenerCadenaConexion2(string servidor)
        {
            XmlDocument xmlServidores = new XmlDocument();
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                xmlServidores.Load(path + @"\" + "ServerConnections.xml");

                XmlNodeList listaServidor = xmlServidores.GetElementsByTagName(servidor);
                string server = string.Empty;
                string user = string.Empty;
                string database = string.Empty;
                string password = string.Empty;

                foreach (XmlNode nodo in listaServidor)
                {
                    server = nodo.Attributes["TagName"].Value;
                    user = nodo.Attributes["Usuario"].Value;
                    database = nodo.Attributes["DataBase"].Value;
                    password = nodo.Attributes["Password"].Value;
                }

                //Autentificacion de windows
                //this.cadenaConexion = "Data Source=(LOCAL); Initial Catalog=ERPSite; Integrated Security=True";                
                //Autentificacion de sql server
                this.cadenaConexion = "server=" + server + ";user=" + user + ";database=" + database + ";password=" + password + ";";
                this.Conexion = new SqlConnection(this.cadenaConexion);


            }
            finally
            {
                if (xmlServidores != null)
                    xmlServidores = null;
            }

            return cadenaConexion;

        }


        /// <summary>
        /// Abre la conexion
        /// </summary>
        /// <returns>true la conexion fue abierta</returns>
        protected bool AbrirConexion()
        {
            if (this.CerrarConexion())
                this.Conexion.Open();
            return true;
        }

        /// <summary>
        /// Cierra la conexion
        /// </summary>
        /// <returns>true si la conexion fue cerrada</returns>
        public bool CerrarConexion()
        {
            try
            {
                if (this.Conexion.State != ConnectionState.Closed)
                    this.Conexion.Close();
            }
            catch (System.Exception ex)
            {


            }
            return true;
        }

        /// <summary>
        /// Configura el Objeto SqlCommand para ejecutra un Procedimiento Alamcenado 
        /// </summary>
        /// <param name="storeProcedureName">Nombre del Procedimiento que se va a Ejecutar</param>
        public void ConfigurarComando(string storeProcedureName)
        {
            this.sqlCommand = new SqlCommand(storeProcedureName, this.Conexion);
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Agrega un parametro tipo SqlParameter
        /// </summary>
        /// <param name="parametro">objeto SqlParametero que tiene el nombre, valor y tipo de parametro a enviar</param>
        public void AgregarParametro(SqlParameter parametro)
        {
            this.sqlCommand.Parameters.Add(parametro);
        }

        /// <summary>
        /// Agrega un parametro del tipo string al objeto sqlCommand
        /// </summary>
        /// <param name="Nombre">Nombre del parametro</param>
        /// <param name="value">Valor</param>
        /// <param name="tamanio">Tamaño del string</param>
        public void AgregarParametro(string Nombre, string value, int tamanio)
        {
            this.sqlCommand.Parameters.Add(Nombre, SqlDbType.VarChar, tamanio);
            this.sqlCommand.Parameters[Nombre].Value = value;
        }

        /// <summary>
        /// Agrega un parametro del tipo Int32/int16 al  objeto sqlCommand
        /// </summary>
        /// <param name="Nombre">Nombre del parametro</param>
        /// <param name="value">valor</param>
        /// <param name="type">SqlDbType.Int16/ SqlDbType.Int16</param>
        public void AgregarParametro(string Nombre, int value, SqlDbType type)
        {
            this.sqlCommand.Parameters.Add(Nombre, type);
            this.sqlCommand.Parameters[Nombre].Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public void AgregarParametro(string Nombre, decimal value, SqlDbType type)
        {
            this.sqlCommand.Parameters.Add(Nombre, type);
            this.sqlCommand.Parameters[Nombre].Value = value;
        }

        /// <summary>
        /// Agrega in parametro NCHAR/VARCHAR/ETC
        /// </summary>
        /// <param name="Nombre">Nombre del parametro</param>
        /// <param name="value">valor</param>
        /// <param name="type">CHAR/NCHAR/VARCHAR</param>
        /// <param name="length">Tamaño de Caracters</param>
        public void AgregarParametro(string Nombre, string value, SqlDbType type, int length)
        {
            this.sqlCommand.Parameters.Add(Nombre, type, length);
            this.sqlCommand.Parameters[Nombre].Value = value;
        }

        /// <summary>
        /// Agrega in parametro NCHAR/VARCHAR/ETC
        /// </summary>
        /// <param name="Nombre">Nombre del parametro</param>
        /// <param name="value">valor</param>
        /// <param name="type">CHAR/NCHAR/VARCHAR</param>
        /// <param name="length">Tamaño de Caracters</param>
        public void AgregarParametro(string Nombre, string value, SqlDbType type)
        {
            this.sqlCommand.Parameters.Add(Nombre, type);
            this.sqlCommand.Parameters[Nombre].Value = value;
        }

        /// <summary>
        ///  Agrega un parametro del tipo DateTime al  objeto sqlCommand
        /// </summary>
        /// <param name="Nombre">ombre del parametro</param>
        /// <param name="value">DateTime</param>
        public void AgregarParametro(string Nombre, DateTime value)
        {
            this.sqlCommand.Parameters.Add(Nombre, SqlDbType.DateTime);
            this.sqlCommand.Parameters[Nombre].Value = value;
        }

        /// <summary>
        ///  Agrega un parametro del tipo DateTime al  objeto sqlCommand
        /// </summary>
        /// <param name="Nombre">ombre del parametro</param>
        /// <param name="value">DateTime</param>
        public void AgregarParametroSmall(string Nombre, DateTime value)
        {
            this.sqlCommand.Parameters.Add(Nombre, SqlDbType.SmallDateTime);
            this.sqlCommand.Parameters[Nombre].Value = value;
        }

        /// <summary>
        /// Agrega in parametro byte
        /// </summary>
        /// <param name="Nombre">Nombre del parametro</param>
        /// <param name="value">valor</param>
        /// <param name="type">byte []</param>        
        public void AgregarParametro(string Nombre, byte[] value, SqlDbType type)
        {
            this.sqlCommand.Parameters.Add(Nombre, type);
            this.sqlCommand.Parameters[Nombre].Value = value;
        }

        /// <summary>
        /// Ejecuta un comnado NonQuery
        /// </summary>
        /// <returns>true/false si el comando se ejecuto satisfactoriamente</returns>
        public bool EjecutarConsulta()
        {
            bool succes = true;
            try
            {
                this.AbrirConexion();
                this.sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                succes = false;
            }
            finally
            {
                this.CerrarConexion();
            }
            return succes;
        }

        /// <summary>
        /// Ejecuta una consulta para devolver un resultado de filas
        /// </summary>
        /// <returns>true/false si la consutla fue exitosa</returns>
        public bool EjecutarReader()
        {
            bool succes = true;
            try
            {
                this.AbrirConexion();
                this.sqlCommand.CommandTimeout = 300;
                this._dataReader = this.sqlCommand.ExecuteReader();
            }
            catch (SqlException ex)
            {
                this.CerrarConexion();
                succes = false;
            }
            return succes;
        }

        public void EjecutarReaderTransaccional()
        {

            //this.AbrirConexion();
            this.sqlCommand.CommandTimeout = 300;
            if (this.Transaction != null)
            {
                this.sqlCommand.Transaction = this.Transaction;
            }
            this._dataReader = this.sqlCommand.ExecuteReader();
        }

        /// <summary>
        /// Ejecuta una consulta para devolver un unico valor
        /// </summary>
        /// <param name="value">variable donde se tomara el valor arrojado por la consulta</param>
        /// <returns>true/false si la consulta fue exitosa o no</returns>
        public bool EjectuarEscalar(out string value)
        {
            bool succes = true;
            value = string.Empty;
            try
            {
                this.AbrirConexion();
                value = this.sqlCommand.ExecuteScalar().ToString();
            }
            catch (SqlException)
            {
                succes = false;
            }
            catch (Exception)
            {
                succes = false;
            }
            finally
            {
                this.CerrarConexion();
            }
            return succes;
        }

        public DataTable EjecutarProcedimientodeConsulta()
        {

            DataTable dtConsulta = new DataTable();
            _dataAdapter.SelectCommand = this.Comando;
            if (Comando.Connection.State == ConnectionState.Closed) { this.Comando.Connection.Open(); }
            this._dataAdapter.Fill(dtConsulta);
            this.Comando.Connection.Close();
            return dtConsulta;

        }


        public SQLBaseClass CrearTransaction()
        {
            this.AbrirConexion();
            if (this.Transaction != null)
            {
                this.Transaction = null;
            }

            this.Transaction = this.Conexion.BeginTransaction();

            return this;
        }

        public SQLBaseClass CommitTransaction()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Commit();
                this.Transaction = null;
            }
            this.CerrarConexion();
            return this;
        }

        public SQLBaseClass RollbackTransaction()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Rollback();
                this.Transaction = null;
            }
            this.CerrarConexion();
            return this;
        }
    }
}