using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XPike.Configuration.Microsoft
{
    /// <summary>
    /// Provides proper deserialization of arrays within a JSON-serialized IConfigSection.
    /// Supports deserializing collections of any object type into an array or into a generic IList / List / IEnumerable.
    /// </summary>
    public class AppSettingsArrayJsonConverter
        : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            throw new NotImplementedException();

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!CanConvert(objectType))
                throw new Exception($"This converter is not compatible with {objectType.FullName}.");

            if (reader.TokenType == JsonToken.Null)
                return null;

            var elementType = objectType.IsArray ? objectType.GetElementType() : objectType.GenericTypeArguments[0];
            var transientType = elementType;

            if (elementType.IsPrimitive)
                transientType = typeof(Nullable<>).MakeGenericType(elementType);

            var dictionaryType = typeof(Dictionary<,>).MakeGenericType(typeof(string), transientType);
            var items = (IDictionary)serializer.Deserialize(reader, dictionaryType);
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));

            foreach (var item in items.Values)
            {
                if (item == null)
                    continue;

                list.Add(Convert.ChangeType(item, elementType));
            }

            if (!objectType.IsArray)
                return list;

            var result = Array.CreateInstance(elementType, list.Count);

            var index = 0;
            foreach (var item in list)
                result.SetValue(item, index++);

            return result;
        }

        public override bool CanConvert(Type objectType) =>
            objectType.IsArray ||
            (objectType.IsGenericType &&
             (objectType.IsAssignableFrom(objectType.GenericTypeArguments[0].MakeArrayType()) ||
              objectType.IsAssignableFrom(typeof(List<>).MakeGenericType(objectType.GenericTypeArguments[0]))));
    }
}