using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpeedSolutions
{
    public class Util
    {
        public static T CrearInstancia<T>()
        {
            if (typeof(T).IsValueType)
            {
                return default(T);
            }
            else if (typeof(T) == typeof(String))
            {
                return (T)Convert.ChangeType(String.Empty, typeof(T));
            }
            else
            {
                return Activator.CreateInstance<T>();
            }
        }


        /**
         * Castea el resultado de un dataReader a la entidad
         * */
        public static T CastReader<T>(T entidad, System.Data.SqlClient.SqlDataReader reader)
        {

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var nombre = reader.GetName(i);
                var valor = reader.GetValue(i);
                try
                {
                    /*
                   if (nombre == "puntos_acumulados")
                   {
                       throw new Exception(reader.GetDataTypeName(i));
                   }
                   /**/
                    switch (reader.GetDataTypeName(i))
                    {
                        case "varchar":
                        case "char":
                        case "text":
                            if (reader.IsDBNull(i))
                            {
                                setInstanceProperty<string>((T)entidad, nombre, null);
                            }
                            else
                            {
                                setInstanceProperty<string>((T)entidad, nombre, valor.ToString());
                            }
                            break;
                        case "smallint":
                            if (reader.IsDBNull(i))
                                setInstanceProperty<short?>((T)entidad, nombre, null);
                            else
                            {
                                try
                                {
                                    setInstanceProperty<short>((T)entidad, nombre, reader.GetInt16(i));
                                }
                                catch (NullReferenceException ex)
                                {
                                    setInstanceProperty<short?>((T)entidad, nombre, reader.GetInt16(i));
                                }
                            }
                            break;
                        case "int":
                            if (reader.IsDBNull(i))
                                setInstanceProperty<int?>((T)entidad, nombre, null);
                            else
                            {
                                try
                                {
                                    setInstanceProperty<int>((T)entidad, nombre, reader.GetInt32(i));
                                }
                                catch (NullReferenceException ex)
                                {
                                    setInstanceProperty<int?>((T)entidad, nombre, reader.GetInt32(i));
                                }
                            }
                            break;
                        case "bigint":
                            if (reader.IsDBNull(i))
                                setInstanceProperty<long?>((T)entidad, nombre, null);
                            else
                            {
                                try
                                {
                                    setInstanceProperty<long>((T)entidad, nombre, reader.GetInt64(i));
                                }
                                catch (NullReferenceException ex)
                                {
                                    setInstanceProperty<long?>((T)entidad, nombre, reader.GetInt64(i));
                                }
                            }
                            break;
                        case "bit":
                            if (reader.IsDBNull(i))
                                setInstanceProperty<bool?>((T)entidad, nombre, null);
                            else
                            {
                                try
                                {
                                    setInstanceProperty<bool>((T)entidad, nombre, reader.GetBoolean(i));
                                }
                                catch (NullReferenceException ex)
                                {
                                    setInstanceProperty<bool?>((T)entidad, nombre, reader.GetBoolean(i));
                                }
                            }

                            break;
                        case "datetime":
                            if (reader.IsDBNull(i))
                                setInstanceProperty<DateTime?>((T)entidad, nombre, null);
                            else
                            {
                                try
                                {
                                    setInstanceProperty<DateTime>((T)entidad, nombre, reader.GetDateTime(i));
                                }
                                catch (NullReferenceException ex)
                                {
                                    setInstanceProperty<DateTime?>((T)entidad, nombre, reader.GetDateTime(i));
                                }
                            }
                            break;
                        case "decimal":
                            if (reader.IsDBNull(i))
                                setInstanceProperty<decimal?>((T)entidad, nombre, null);
                            else
                            {
                                try
                                {
                                    setInstanceProperty<decimal>((T)entidad, nombre, reader.GetDecimal(i));
                                }
                                catch (NullReferenceException ex)
                                {
                                    setInstanceProperty<decimal?>((T)entidad, nombre, reader.GetDecimal(i));
                                }
                            }

                            break;
                        case "double":
                        case "float":
                            if (reader.IsDBNull(i))
                                setInstanceProperty<double?>((T)entidad, nombre, null);
                            else
                            {
                                try
                                {
                                    setInstanceProperty<double>((T)entidad, nombre, reader.GetDouble(i));
                                }
                                catch (NullReferenceException ex)
                                {
                                    setInstanceProperty<double?>((T)entidad, nombre, reader.GetDouble(i));
                                }
                            }

                            break;
                        default:
                            setInstanceProperty<string>((T)entidad, nombre, valor != null ? valor.ToString() : null);
                            break;
                    }
                }
                catch (NullReferenceException ex)
                {

                }

            }
            return entidad;

        }

        /***
         * setInstanceProperty<int>( myWidget , "InstanceWidgetProperty" , 123 ) ;
         * */
        protected static void setInstanceProperty<PROPERTY_TYPE>(object instance, string propertyName, PROPERTY_TYPE value)
        {
            Type type = instance.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public, null, typeof(PROPERTY_TYPE), new Type[0], null);

            propertyInfo.SetValue(instance, value, null);

            return;
        }
        /*
                protected void setInstanceProperty<T>(T instance, string propertyName, object value)
                {
                    Type type = instance.GetType();
                    PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public, null, typeof(object), new Type[0], null);

                    propertyInfo.SetValue(instance, value, null);

                    return;
                }
                */
        /**
         *    setStaticProperty<int>( typeof(Widget) , "StaticWidgetProperty" , 72 ) ;
         * */
        private static void setStaticProperty<PROPERTY_TYPE>(Type type, string propertyName, PROPERTY_TYPE value)
        {
            PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static, null, typeof(PROPERTY_TYPE), new Type[0], null);

            propertyInfo.SetValue(null, value, null);

            return;
        }

    }
}
