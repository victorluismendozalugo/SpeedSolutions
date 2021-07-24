using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedSolutions.Utils
{
    public class Respuesta : IRespuesta
    {
        public const string SUCCESS = "success";
        public const string ERROR = "error";
        public const string ALERT = "alert";
        public const string SESSION = "session";
        public const string INFO = "info";
        //Constructor
        public Respuesta()
        {
            this.IsValid = true;
            this.Message = string.Empty;
            this.Type = Respuesta.SUCCESS;
        }
        //Declaracion de variables como en las entidades
        public string RESULTADO { get; set; }
        public int ID { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }

        public object Data { get; set; }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Respuesta PublishException(Exception ex)
        {
            Respuesta response = new Respuesta();
            response.IsValid = false;
            response.Message = ex.Message;
            response.Type = Respuesta.ERROR;
            return response;
        }

        public static Respuesta PublishError(string mensaje)
        {
            Respuesta response = new Respuesta();
            response.IsValid = false;
            response.Message = mensaje;
            response.Type = Respuesta.ERROR;
            return response;
        }

        public static Respuesta PublishAlert(string mensaje)
        {
            Respuesta response = new Respuesta();
            response.IsValid = false;
            response.Message = mensaje;
            response.Type = Respuesta.ALERT;
            return response;
        }

        public static Respuesta PublishSuccess(object datos, string mensaje)
        {
            Respuesta response = new Respuesta();
            response.IsValid = true;
            response.Message = mensaje;
            response.Type = Respuesta.SUCCESS;
            response.Data = datos;
            return response;
        }

        public static Respuesta PublishSuccess(object datos)
        {
            return Respuesta.PublishSuccess(datos, string.Empty);
        }


        public static Respuesta PublishSession(string mensaje)
        {
            Respuesta response = new Respuesta()
            {
                IsValid = false,
                Message = mensaje,
                Type = Respuesta.SESSION
            };

            return response;
        }


    }

    public class Respuesta<T> : Respuesta
    {
        public T Data { get; set; }

        public static Respuesta<T> PublishError(T datos, string mensaje)
        {
            Respuesta<T> response = new Respuesta<T>();
            response.IsValid = false;
            response.Data = datos;
            response.Message = mensaje;
            response.Type = Respuesta.ERROR;
            return response;
        }

        public static Respuesta<T> PublishAlert(T datos, string mensaje)
        {
            Respuesta<T> response = new Respuesta<T>();
            response.IsValid = false;
            response.Data = datos;
            response.Message = mensaje;
            response.Type = Respuesta.ALERT;
            return response;
        }

        public static Respuesta<T> PublishSuccess(T datos, string mensaje)
        {
            Respuesta<T> response = new Respuesta<T>();
            response.IsValid = true;
            response.Data = datos;
            response.Message = mensaje;
            response.Type = Respuesta.SUCCESS;
            return response;
        }

        public static Respuesta<T> PublishSuccess(T datos)
        {
            return Respuesta<T>.PublishSuccess(datos, string.Empty);
        }

        public static Respuesta<T> PublishInfo(T datos, string mensaje)
        {
            Respuesta<T> response = new Respuesta<T>();
            response.IsValid = true;
            response.Data = datos;
            response.Message = mensaje;
            response.Type = Respuesta.INFO;
            return response;
        }
    }
}