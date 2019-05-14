using RedisStackOverflow.Entities.Redis;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json;
using FluentValidation;
using RedisStackOverflow.Data.Configurations;

namespace RedisStackOverflow.Data.Utils
{
    public class RedisEntityHelper<TEntity, TValidator>
        where TEntity : RedisDefaultKey<TEntity, TValidator>, new()
        where TValidator : AbstractValidator<TEntity>
    {
        public const string RedisInputDateTimeFormat = "yyyyMMddHHmmssffffff";
        private readonly CultureInfo _defaultFormat;

        public RedisEntityHelper()
        {
            _defaultFormat = DataGlobalization.GetDefaultCultureInfo();
        }

        public string GetEntityKey(ulong id)
        {
            return typeof(TEntity).Name + ":" + id.ToString();
        }

        public HashEntry[] GetHashSets(
            TEntity entity, 
            List<HashEntry> entries = null)
        {
            var props =
                entity.GetType()
                    .GetProperties(
                        BindingFlags.Public
                        | BindingFlags.Instance);

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
                                _defaultFormat);
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
                                .ToString(RedisInputDateTimeFormat);
                        break;

                    case TypeCode.String:
                        hashEntry = (string)p.GetValue(entity, null);
                        break;

                    default:
                        hashEntry = 
                            JsonConvert.SerializeObject(
                                p.GetValue(entity, null));
                        break;
                }

                entries.Add(new HashEntry(p.Name,hashEntry));
            }

            return entries.ToArray();
        }

        public TEntity GetEntity(
            HashEntry[] entries)
        {
            var newEntity = new TEntity();
            var props = 
                typeof(TEntity).GetProperties(
                        BindingFlags.Public
                        | BindingFlags.Instance);

            foreach(var entry in entries)
            {
                var prop =
                    props.FirstOrDefault(
                        p => p.Name == entry.Name);
                if (prop == null)
                    continue;

                var propTypeCode = 
                    Type.GetTypeCode(
                        prop.PropertyType);

                switch (propTypeCode)
                {
                    case TypeCode.Byte:
                        prop.SetValue(
                            newEntity,
                            Convert.ToByte(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.SByte:
                        prop.SetValue(
                            newEntity,
                            Convert.ToSByte(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.Int16:
                        prop.SetValue(
                            newEntity,
                            Convert.ToInt16(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.Int32:
                        prop.SetValue(
                            newEntity,
                            Convert.ToInt32(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.Int64:
                        prop.SetValue(
                            newEntity,
                            Convert.ToInt64(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.UInt16:
                        prop.SetValue(
                            newEntity,
                            Convert.ToUInt16(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.UInt32:
                        prop.SetValue(
                            newEntity,
                            Convert.ToUInt64(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.UInt64:
                        prop.SetValue(
                            newEntity,
                            Convert.ToUInt64(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.Char:
                        prop.SetValue(
                            newEntity,
                            Convert.ToChar(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.Boolean:
                        prop.SetValue(
                            newEntity,
                            Convert.ToBoolean(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.Decimal:
                        prop.SetValue(
                            newEntity,
                            Convert.ToDecimal(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.Double:
                        prop.SetValue(
                            newEntity,
                            Convert.ToDouble(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.Single:
                        prop.SetValue(
                            newEntity,
                            Convert.ToSingle(
                                entry.Value.ToString()));
                        break;

                    case TypeCode.DateTime:
                        prop.SetValue(
                            newEntity,
                            DateTime.ParseExact(
                                entry.Value.ToString(),
                                RedisInputDateTimeFormat, 
                                CultureInfo.InvariantCulture));
                        break;

                    case TypeCode.String:
                        prop.SetValue(newEntity, entry.Value.ToString());
                        break;

                    default:
                        try
                        {
                            var deserializedObject =
                                JsonConvert.DeserializeObject(
                                    entry.Value,
                                    prop.PropertyType);
                            prop.SetValue(
                                newEntity,
                                deserializedObject);
                        }
                        catch
                        {
                            //  log
                        }
                        break;
                }
            }

            return newEntity;
        }
    }
}
