﻿
        public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<VOTYPE>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, VOTYPE value)
            {
                parameter.DbType = System.Data.DbType.DateTime;
                parameter.Value = value.Value;
            }

            public override VOTYPE Parse(object value)
            {
                return value switch
                {
                    System.DateTime dtValue => new VOTYPE(dtValue),
                    string stringValue when 
                        !string.IsNullOrEmpty(stringValue) &&
                        System.DateTime.TryParseExact(stringValue, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var result) => new VOTYPE(result),
                    _ => throw new System.InvalidCastException($"Unable to cast object of type {value.GetType()} to VOTYPE"),
                };
            }
        }