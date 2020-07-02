using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace DogusTeknoloji.SmartKPIMiner.Core
{
    public class JsonPathConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType) => true;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var targetObj = Activator.CreateInstance(objectType);

            foreach (var property in objectType.GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                var jsonPropertyAttribute = property.GetCustomAttributes(true)
                                                    .OfType<JsonPropertyAttribute>()
                                                    .FirstOrDefault();
                if (jsonPropertyAttribute == null)
                {
                    throw new JsonReaderException($"{nameof(JsonPropertyAttribute)} is mandatory when using {nameof(JsonPathConverter)}");
                }

                var jsonPath = jsonPropertyAttribute.PropertyName;
                var token = jObject.SelectToken(jsonPath);

                if (token != null && token.Type != JTokenType.Null)
                {
                    var jsonConverterAttribute = property.GetCustomAttributes(true)
                                                         .OfType<JsonConverterAttribute>()
                                                         .FirstOrDefault();
                    object value;

                    if (jsonConverterAttribute == null)
                    {
                        serializer.Converters.Clear();
                        value = token.ToObject(property.PropertyType, serializer);
                    }
                    else
                    {
                        value = JsonConvert.DeserializeObject(token.ToString(),
                                                              property.PropertyType,
                                                              (JsonConverter)Activator.CreateInstance(jsonConverterAttribute.ConverterType));
                    }
                    property.SetValue(targetObj, value, null);
                }
            }

            return targetObj;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
