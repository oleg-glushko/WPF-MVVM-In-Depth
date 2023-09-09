using System;
using System.ComponentModel;
using System.Globalization;

namespace ZzaDashboard.Converters;

public class StringToGuidConverter : TypeConverter
{
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        var sourceString = (string)value;
        if (sourceString != null && Guid.TryParse(sourceString, out var guid))
            return guid;
        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string))
            return true;

        return base.CanConvertFrom(context, sourceType);
    }
}
