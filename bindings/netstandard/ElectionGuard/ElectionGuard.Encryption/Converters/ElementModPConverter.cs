﻿using System;
using Newtonsoft.Json;

namespace ElectionGuard.Converters
{
    public class ElementModPConverter : JsonConverter<ElementModP>
    {
        public override ElementModP ReadJson(JsonReader reader, Type objectType, ElementModP existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return value != null ? new ElementModP(value) : null;
        }

        public override void WriteJson(JsonWriter writer, ElementModP value, JsonSerializer serializer)
        {
            var json = value.ToHex();
            writer.WriteValue(json);
        }
    }
}