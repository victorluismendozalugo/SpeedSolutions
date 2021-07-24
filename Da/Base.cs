using SpeedSolutions.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Da
{
    public class Base
    {

        public List<T> Consultar<T>(string nombreSP, List<SqlParameter> parametros)
        {
            SQLBaseClass Con = this.CrearConexion();
            return Consultar<T>(nombreSP, parametros, Con, false);
        }

        public List<T> Consultar<T>(string nombreSP, List<SqlParameter> parametros, SQLBaseClass Con, bool esTransaccional)
        {
            List<T> listado = new List<T>();

            try
            {
                Con.ConfigurarComando(nombreSP);
                foreach (var param in parametros)
                {
                    Con.AgregarParametro(param.ParameterName, param.Value != null ? param.Value.ToString() : null, param.SqlDbType);
                }

                if (esTransaccional)
                {
                    Con.EjecutarReaderTransaccional();
                    while (Con.DataReader.Read())
                    {
                        T instancia = Util.CrearInstancia<T>();
                        listado.Add(Util.CastReader<T>(instancia, Con.DataReader));
                    }

                    Con.DataReader.Close();
                }
                else
                {
                    if (Con.EjecutarReader())
                    {
                        while (Con.DataReader.Read())
                        {
                            T instancia = Util.CrearInstancia<T>();
                            listado.Add(Util.CastReader<T>(instancia, Con.DataReader));
                        }

                        Con.DataReader.Close();
                    }
                }

            }

            finally
            {
                if (!esTransaccional)
                {
                    Con.CerrarConexion();
                }
            }
            return listado;
        }

        public T ConsultarPrimero<T>(string nombreSP, List<SqlParameter> parametros)
        {
            SQLBaseClass Con = new SQLBaseClass();
            Con.ObtenerCadenaConexion();
            return ConsultarPrimero<T>(nombreSP, parametros, Con, false);
        }

        public T ConsultarPrimero<T>(string nombreSP, List<SqlParameter> parametros, SQLBaseClass Con, bool esTransaccional)
        {
            var listado = this.Consultar<T>(nombreSP, parametros, Con, esTransaccional);

            return listado.FirstOrDefault();
        }

        public string Consulta(string nombreSP, List<SqlParameter> parametros, string respuesta)
        {
            SQLBaseClass Con = this.CrearConexion();
            string res = "";
            try
            {
                Con.ConfigurarComando(nombreSP);
                foreach (var param in parametros)
                {
                    Con.AgregarParametro(param.ParameterName, param.Value != null ? param.Value.ToString() : null, param.SqlDbType);
                }


                if (Con.EjecutarReader())
                {
                    while (Con.DataReader.Read())
                    {
                        res = (string)Con.DataReader["" + respuesta + ""];
                    }

                    Con.DataReader.Close();
                    Con.CerrarConexion();
                }
            }
            catch (Exception ex)
            {

                res = ex.ToString();
            }

            return res;
        }

        public Respuesta Guardar(string nombreSP, List<SqlParameter> parametros)
        {
            SQLBaseClass Con = new SQLBaseClass();
            Con.ObtenerCadenaConexion();
            return Guardar(nombreSP, parametros, Con, false);
        }

        public Respuesta Guardar(string nombreSP, List<SqlParameter> parametros, SQLBaseClass Con, bool esTransaccional)
        {
            var listado = this.Consultar<Respuesta>(nombreSP, parametros, Con, esTransaccional);

            var result = listado.FirstOrDefault();

            if (result == null)
            {
                throw new Exception("Error al consultar la Base de datos, rutina " + nombreSP);
            }
            else
            {
                Respuesta resp;
                if (result.RESULTADO == "EXITO")
                {
                    resp = new Respuesta()
                    {
                        Data = result.ID,
                        IsValid = true,
                        Message = string.Empty,
                        RESULTADO = result.RESULTADO,
                        Type = Respuesta.SUCCESS
                    };
                }
                else
                {
                    resp = new Respuesta()
                    {
                        Data = null,
                        IsValid = false,
                        Message = result.RESULTADO,
                        RESULTADO = result.RESULTADO,
                        Type = Respuesta.ERROR
                    };
                }
                return resp;
            }
        }

        public SQLBaseClass CrearConexion()
        {
            SQLBaseClass Con = new SQLBaseClass();
            Con.ObtenerCadenaConexion();
            return Con;
        }

        public SQLBaseClass CrearTransaction()
        {
            var Con = this.CrearConexion();
            return CrearTransaction(Con);

        }

        public SQLBaseClass CrearTransaction(SQLBaseClass Con)
        {
            return Con.CrearTransaction();
        }
    }
}