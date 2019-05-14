using RedisStackOverflow.Entities.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace RedisStackOverflow.Data.Utils
{
    public static class RedisEntityHelper
    {
        private static readonly string FbitsCultureInfoName = "fbits";
        private static readonly CultureInfo DefaultFormat;

        static RedisEntityHelper()
        {
            var cultureInfo = CultureInfo.GetCultureInfo(FbitsCultureInfoName);
            if (cultureInfo == null)
            {
                CultureAndRegionInfoBuilder.Unregister(FbitsCultureInfoName);
                var _cultureInfo =
                    new CultureAndRegionInfoBuilder(
                        FbitsCultureInfoName,
                        CultureAndRegionModifiers.None);
                _cultureInfo.LoadDataFromCultureInfo(CultureInfo.GetCultureInfo(1041));
                _cultureInfo.RegionEnglishName = "Traycorp - Fbits";
                _cultureInfo.CultureEnglishName = "Traycorp - Fbits";
                _cultureInfo.CultureNativeName = "Traycorp - Fbits";
                _cultureInfo.RegionNativeName = "Traycorp - Fbits";
                _cultureInfo.ThreeLetterISORegionName = "fbt";
                _cultureInfo.ThreeLetterISOLanguageName = "fbt";
                _cultureInfo.ThreeLetterWindowsLanguageName = "fbt";
                _cultureInfo.ThreeLetterWindowsRegionName = "fbt";
                _cultureInfo.TwoLetterISORegionName = "fb";
                _cultureInfo.TwoLetterISOLanguageName = "fb";
                _cultureInfo.GregorianDateTimeFormat.DateSeparator = "-";
                _cultureInfo.ISOCurrencySymbol = "BRL";
                _cultureInfo.CurrencyNativeName = "Real";
                _cultureInfo.CurrencyEnglishName = "Real";
                _cultureInfo.Register();
                cultureInfo = CultureInfo.GetCultureInfo(FbitsCultureInfoName);
            }

            DefaultFormat = cultureInfo;
        }

        public static HashEntry[] GetHashSets<T>(
            this T entity, 
            List<HashEntry> entries = null)
            where T : IRedisKey
        {
            var props =
                entity.GetType()
                    .GetProperties(
                        BindingFlags.Public
                        | BindingFlags.Instance
                        | BindingFlags.DeclaredOnly);

            if (props.Length == 0)
                return null;

            if(entries == null)
                entries = new List<HashEntry>(props.Length);
            foreach (var p in props)
            {
                var propTypeCode = Type.GetTypeCode(p.PropertyType);
                string hashEntry = null;

                switch(propTypeCode)
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        hashEntry = 
                            Convert.ToString(
                                p.GetValue(entity, null),
                                DefaultFormat);
                        break;

                    case TypeCode.Char:
                        hashEntry = new string((char)p.GetValue(entity, null), 1);
                        break;

                    case TypeCode.Boolean:
                        hashEntry = ((bool)p.GetValue(entity, null)) ? "1" : "0";
                        break;

                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Single:
                        hashEntry = 
                            Convert.ToString(
                                p.GetValue(entity, null), 
                                CultureInfo.InvariantCulture);
                        break;

                    case TypeCode.DateTime:
                        hashEntry =
                            ((DateTime)p.GetValue(entity, null))
                                .ToString("yyyyMMddhhmmssffffff");
                        break;

                    case TypeCode.String:
                        hashEntry = ((string)p.GetValue(entity, null));
                        break;

                    //case TypeCode.Object:
                    //    hashEntry = "null";
                    //    break;

                    default:
                        //var complexObject = p.GetValue(entity, null) as IRedisKey;
                        //if (complexObject == null)
                        //    hashEntry = "null";
                        //else
                        //    GetHashSets(complexObject, entries);
                        hashEntry = "null";
                        break;
                }

                entries.Add(new HashEntry(p.Name,hashEntry));
            }

            return entries.ToArray();
        }
    }
}
