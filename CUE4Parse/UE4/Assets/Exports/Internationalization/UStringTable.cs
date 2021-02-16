﻿using System;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.UObject;
using Newtonsoft.Json;

namespace CUE4Parse.UE4.Assets.Exports.Internationalization
{
    [JsonConverter(typeof(UStringTableConverter))]
    public class UStringTable : UObject
    {
        public FStringTable StringTable { get; private set; }
        public int StringTableId { get; private set; } // Index of the string in the NameMap

        public UStringTable() { }
        public UStringTable(FObjectExport exportObject) : base(exportObject) { }

        public override void Deserialize(FAssetArchive Ar, long validPos)
        {
            base.Deserialize(Ar, validPos);

            StringTable = new FStringTable(Ar);
            StringTableId = Ar.Read<int>();
        }
    }
    
    public class UStringTableConverter : JsonConverter<UStringTable>
    {
        public override void WriteJson(JsonWriter writer, UStringTable value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            
            // export type
            writer.WritePropertyName("Type");
            writer.WriteValue(value.ExportType);
            
            // export properties
            writer.WritePropertyName("Export");
            writer.WriteStartObject();
            {
                writer.WritePropertyName("StringTable");
                serializer.Serialize(writer, value.StringTable);
                
                writer.WritePropertyName("StringTableId");
                writer.WriteValue(value.StringTableId);
            }
            writer.WriteEndObject();
            
            writer.WriteEndObject();
        }

        public override UStringTable ReadJson(JsonReader reader, Type objectType, UStringTable existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}