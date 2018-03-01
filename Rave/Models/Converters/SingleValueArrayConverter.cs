using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rave.Models.Converters
{
    public class SingleValueArrayConverter<T>: JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject) {
                return (object)Newtonsoft.Json.Linq.JToken.Load(reader).ToObject<T>();
            }
            else {
                return System.Activator.CreateInstance(objectType);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }
}