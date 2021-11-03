using System;
using System.Collections.Generic;
using System.Globalization;

namespace RJDev.Outputter.Formatting.Json
{
    public class JsonSerializer
    {
        private static readonly Dictionary<Type, Func<object, string>> TypeSerializers = new()
        {
            { typeof(bool), val => SerializeBoolean((bool)val) },
            { typeof(char), val => SerializeString(((char)val).ToString(), true) },
            { typeof(byte), val => SerializeString(val, false) },
            { typeof(sbyte), val => SerializeString(val, false) },
            { typeof(short), val => SerializeString(val, false) },
            { typeof(ushort), val => SerializeString(val, false) },
            { typeof(int), val => SerializeString(val, false) },
            { typeof(uint), val => SerializeString(val, false) },
            { typeof(long), val => SerializeString(val, false) },
            { typeof(ulong), val => SerializeString(val, false) },
            { typeof(float), val => SerializeSingle((float)val) },
            { typeof(double), val => SerializeDouble((double)val) },
            { typeof(decimal), val => SerializeString(val, false) },
            { typeof(string), val => SerializeString(val, true) },
            { typeof(DateTime), val => SerializeDateTime((DateTime)val) },
            { typeof(DateTimeOffset), val => SerializeDateTimeOffset((DateTimeOffset)val) },
            { typeof(TimeSpan), val => SerializeTimeSpan((TimeSpan)val) }
        };

        private Dictionary<Type, Func<object, string>> typeSerializers = new();

        /// <summary>
        /// Add serializer for given type.
        /// </summary>
        /// <param name="forType"></param>
        /// <param name="serializer"></param>
        public void AddTypeSerializer(Type forType, Func<object, string> serializer)
        {
            this.typeSerializers[forType] = serializer;
        }

        /// <summary>
        /// Serialize value into JSON.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Serialize(object? value)
        {
            if (value == null)
            {
                return "null";
            }

            Type type = value.GetType();

            if (this.typeSerializers.TryGetValue(type, out var specialSerializer))
            {
                return specialSerializer(value);
            }

            if (TypeSerializers.TryGetValue(type, out var serializer))
            {
                return serializer(value);
            }

            return SerializeString(value.ToString(), true);
        }

        private static string SerializeBoolean(bool val)
        {
            return val ? "true" : "false";
        }

        private static string SerializeString(object? val, bool quote)
        {
            if (val is IFormattable formattable)
            {
                return (quote ? "\"" : "") + formattable.ToString(null, CultureInfo.InvariantCulture) + (quote ? "\"" : "");
            }

            return (quote ? "\"" : "") + val.ToString() + (quote ? "\"" : "");
        }

        private static string SerializeSingle(float val)
        {
            return val.ToString("R", CultureInfo.InvariantCulture);
        }

        private static string SerializeDouble(double val)
        {
            return val.ToString("R", CultureInfo.InvariantCulture);
        }

        private static string SerializeDateTime(DateTime val)
        {
            return "\"" + val.ToString("o") + "\"";
        }

        private static string SerializeDateTimeOffset(DateTimeOffset val)
        {
            return "\"" + val.ToString("o") + "\"";
        }

        private static string SerializeTimeSpan(TimeSpan val)
        {
            return "\"" + val.ToString() + "\"";
        }
    }
}