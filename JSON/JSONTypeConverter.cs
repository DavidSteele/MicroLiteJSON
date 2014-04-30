using System;
using System.Collections.Generic;
using System.Text;

using MicroLite.TypeConverters;
using Newtonsoft.Json;

namespace JSON
{
    // These are responsible for serializing/deserializing the objects to/from JSON

    public class JSONTypeConverter<T> : ITypeConverter
    {
        public bool CanConvert(Type propertyType)
        {
            return propertyType == typeof(T);
        }

        public object ConvertFromDbValue(object value, Type propertyType)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            return JsonConvert.DeserializeObject(value.ToString(), propertyType);
        }

        public object ConvertFromDbValue(System.Data.IDataReader reader, int index, Type type)
        {
            if (reader == null)
            {
                return null;
            }

            var JSONData = reader.GetString(index);

            return JsonConvert.DeserializeObject(JSONData, type);
        }

        public object ConvertToDbValue(object value, Type propertyType)
        {
            if (value == null)
            {
                return value;
            }

            return JsonConvert.SerializeObject(value);
        }

    }

}
