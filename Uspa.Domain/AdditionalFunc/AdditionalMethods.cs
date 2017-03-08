using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Uspa.Domain.AdditionalFunc
{
    public static class AdditionalMethods
    {
        public static IQueryable<T> Sorting<T>(this IQueryable<T> source, string ordering, params object[] values)
        {
            Stopwatch fd = new Stopwatch();
            fd.Start();



            var type = typeof(T);
            var property = type.GetProperty(ordering);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            //MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));


            fd.Stop();
            var ds = fd.Elapsed; 
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> SortingString<T>(this IQueryable<T> source, ref string ordering, ref string prev)
        {
            MethodCallExpression resultExp = null;

            if (!string.IsNullOrEmpty(ordering))
            {
                var type = typeof(T);
                var property = type.GetProperty(ordering);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                //MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                // = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));


                if (!string.IsNullOrEmpty(prev))
                {
                    if (prev.Contains(ordering))
                    {
                        if (prev[prev.Length - 1] == 'A')
                        {
                            prev = ordering + 'D';
                            //query = query.OrderByDescending(i => propertyInfo.GetValue(i, null));
                            resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                        }
                        else if (prev[prev.Length - 1] == 'D')
                        {
                            prev = ordering + 'A';
                            //query = query.OrderBy(i => propertyInfo.GetValue(i, null));
                            resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                        }
                        else
                        {
                            //query = query.OrderBy(i => propertyInfo.GetValue(i, null));
                            prev = ordering + 'A';
                            resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                        }
                    }
                    else
                    {
                        //query = query.OrderBy(i => propertyInfo.GetValue(i, null));
                        prev = ordering + 'A';
                        resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                    }
                }
                else
                {
                    //query = query.OrderBy(i => propertyInfo.GetValue(i, null));
                    prev = ordering + 'A';
                    resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                }
            }

            return source.Provider.CreateQuery<T>(resultExp); 
        }

        public static IQueryable<T> SortingDate<T>(this IQueryable<T> source, ref string ordering, ref string prev)
        {
            MethodCallExpression resultExp = null;

            if (!string.IsNullOrEmpty(ordering))
            {
                var type = typeof(T);
                var property = type.GetProperty(ordering);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                //MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                // = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));


                if (!string.IsNullOrEmpty(prev))
                {
                    if (prev.Contains(ordering))
                    {
                        if (prev[prev.Length - 1] == 'A')
                        {
                            prev = ordering + 'D';
                            resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                        }
                        else if (prev[prev.Length - 1] == 'D')
                        {
                            prev = ordering + 'A';
                            resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                        }
                        else
                        {
                            prev = ordering + 'A';
                            resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                        }
                    }
                    else
                    {
                        prev = ordering + 'A';
                        resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                    }
                }
                else
                {
                    prev = ordering + 'A';
                    resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
                }
            }

            return source.Provider.CreateQuery<T>(resultExp);
        }


    }
}
