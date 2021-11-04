using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiRequired
{
    public class JsonConverterFormatDatetime : JsonConverter<DateTime>
    {
        private static DateTime Greenwich_Mean_Time = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
        private const string DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private string _dateTimeFormat;
        private CultureInfo _culture;
        public string DateTimeFormat
        {
            get => _dateTimeFormat ?? string.Empty;
            set => _dateTimeFormat = (string.IsNullOrEmpty(value)) ? null : value;
        }
        public CultureInfo Culture
        {
            get => _culture ?? CultureInfo.CurrentCulture;
            set => _culture = value;
        }
        private const int Limit = 10000;
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                var unixTime = reader.GetInt64();
                var dt = new DateTime(Greenwich_Mean_Time.Ticks + unixTime * Limit);
                return dt;
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                var strTime = reader.GetString();
                var dt = DateTime.Parse(strTime, _culture ?? CultureInfo.CurrentCulture);
                return dt;
            }
            else
            {
                return reader.GetDateTime();
            }
        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, _culture ?? CultureInfo.CurrentCulture));
        }
    }
}
