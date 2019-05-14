using FluentValidation;
using RedisStackOverflow.Entities.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedisStackOverflow.Data.Utils
{
    public class ReflectionHelper<T>
    {
        public PropertyInfo GetPropertyInfo<TValue>(
            Expression<Func<T, TValue>> selector)
        {
            switch (selector.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var member =
                        ((MemberExpression)selector.Body)
                            .Member;
                    return (PropertyInfo)member;


                case ExpressionType.Convert:
                    var operand =
                        ((UnaryExpression)selector.Body).Operand;
                    return (PropertyInfo)((MemberExpression)operand).Member;

                default:
                    throw new Exception("Invalid expression");
            }
        }
        public string GetPropertyName<TValue>(
            Expression<Func<T, TValue>> selector)
        {
            return GetPropertyInfo(selector).Name;
        }
        public IEnumerable<string> GetPropertyNameAsEnumerable<TValue>(
            Expression<Func<T, TValue>> selector)
        {
            return new string[] {
                GetPropertyName(selector)
            };
        }
    }
}
