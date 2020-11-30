using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1.Models;
using static System.Text.Json.JsonElement;

namespace ConsoleApp6.Utils
{
    public class WaterlineCriteria
    {
        public JsonElement? where { get; set; }
        public JsonElement? sort { get; set; }
        public JsonElement? populate { get; set; }
        public int? limit { get; set; }
        public int? skip { get; set; }

        public static string AND_OPERATOR = " && ";
        public static string OR_OPERATOR = " || ";

        public Dictionary<string, object> defaultWhere { get; set; }
        public Dictionary<string, object> defaultWhereCollaborator { get; set; }

        public List<TEntity> Process<TEntity>(DbSet<TEntity> dbSet) where TEntity : class
        {
            try
            {
                //where
                IQueryable<TEntity> queryable = dbSet.AsQueryable<TEntity>();
                List<object> parameters = new List<object>();
                if (where.HasValue)
                {
                    StringBuilder whereQuery = new StringBuilder("");
                    ObjectEnumerator enumerator = where.Value.EnumerateObject();
                    int paramIndex = 0;
                    while (enumerator.MoveNext())
                    {
                        JsonProperty property = enumerator.Current;
                        if (property.Name == "or")
                        {
                            ArrayEnumerator orEnumerator = property.Value.EnumerateArray();
                            if (Utilities.EnumeratorHasItems(orEnumerator))
                            {
                                whereQuery.Append("(");
                                while (orEnumerator.MoveNext())
                                {
                                    ObjectEnumerator orCriteriaEnumerator = orEnumerator.Current.EnumerateObject();
                                    if (Utilities.EnumeratorHasItems(orCriteriaEnumerator))
                                    {
                                        if (orCriteriaEnumerator.Count() > 1)
                                            whereQuery.Append("(");
                                        while (orCriteriaEnumerator.MoveNext())
                                        {
                                            SimpleWhere<TEntity>(whereQuery, parameters, orCriteriaEnumerator.Current, ref paramIndex);
                                            whereQuery.Append(AND_OPERATOR);
                                        }
                                        whereQuery.Remove(whereQuery.Length - AND_OPERATOR.Length, AND_OPERATOR.Length);
                                        if (orCriteriaEnumerator.Count() > 1)
                                            whereQuery.Append(")");
                                        whereQuery.Append(OR_OPERATOR);
                                    }
                                }
                                whereQuery.Remove(whereQuery.Length - OR_OPERATOR.Length, OR_OPERATOR.Length);
                                whereQuery.Append(")");
                            }
                        }
                        else
                        {
                            SimpleWhere<TEntity>(whereQuery, parameters, property, ref paramIndex);
                        }
                        whereQuery.Append(AND_OPERATOR);
                    }
                    if (this.defaultWhere != null)
                    {
                        foreach (string key in defaultWhere.Keys)
                        {
                            StringBuilder defaultWhereQuery = new StringBuilder("@" + paramIndex);
                            defaultWhereQuery.Append(".Contains(").Append(key).Append(")");
                            parameters.Add(defaultWhere[key]);
                            paramIndex++;
                            whereQuery.Append(defaultWhereQuery);
                            whereQuery.Append(AND_OPERATOR);
                        }
                    }
                    if (this.defaultWhereCollaborator != null)
                    {
                        foreach (string key in defaultWhereCollaborator.Keys)
                        {
                            StringBuilder defaultWhereQuery = new StringBuilder("@" + paramIndex);
                            defaultWhereQuery.Append(".Contains(").Append(key).Append(")");
                            parameters.Add(defaultWhereCollaborator[key]);
                            paramIndex++;
                            whereQuery.Append(defaultWhereQuery);
                            whereQuery.Append(AND_OPERATOR);
                        }
                    }
                    if (!string.IsNullOrEmpty(whereQuery.ToString()))
                    {
                        whereQuery.Remove(whereQuery.Length - AND_OPERATOR.Length, AND_OPERATOR.Length);
                        queryable = queryable.Where(whereQuery.ToString(), parameters.ToArray());
                    }
                }
                //sort
                if (sort.HasValue)
                {
                    StringBuilder sortQuery = new StringBuilder("");
                    ObjectEnumerator enumerator = sort.Value.EnumerateObject();
                    if (Utilities.EnumeratorHasItems(enumerator))
                    {
                        while (enumerator.MoveNext())
                        {
                            JsonProperty property = enumerator.Current;
                            Sort<TEntity>(sortQuery, property);
                            sortQuery.Append(", ");
                        }
                        if (!string.IsNullOrEmpty(sortQuery.ToString()))
                        {
                            sortQuery.Remove(sortQuery.Length - 2, 2);
                            queryable = queryable.OrderBy(sortQuery.ToString());
                        }
                    }
                }
                //skip
                if (skip.HasValue)
                {
                    queryable = queryable.Skip(skip.Value);
                }
                //limit
                if (limit.HasValue)
                {
                    queryable = queryable.Take(limit.Value);
                }

                return queryable.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SimpleWhere<TEntity>(StringBuilder query, List<object> parameters, JsonProperty property, ref int paramIndex)
        {
            InfosOfProperty infos = GetModelPropertyInfo<TEntity>(property.Name);
            string propertyName = infos.Name;
            Type fieldType = infos.Type;
            StringBuilder condition;
            switch (property.Value.ValueKind)
            {
                case JsonValueKind.Number:
                case JsonValueKind.False:
                case JsonValueKind.True:
                case JsonValueKind.String:
                case JsonValueKind.Null:
                    condition = new StringBuilder(propertyName);
                    condition.Append("=");
                    condition.Append("@").Append(paramIndex);
                    query.Append(condition);
                    if (property.Value.ValueKind != JsonValueKind.Null)
                        parameters.Add(Utilities.ConvertJsonElementToObject(property.Value, fieldType));
                    else
                        parameters.Add(null);
                    paramIndex++;
                    break;
                case JsonValueKind.Array:
                    condition = new StringBuilder("@").Append(paramIndex);
                    condition.Append(".Contains(").Append(propertyName).Append(")");
                    IList values = Utilities.GetGenericListOfType(fieldType);
                    ArrayEnumerator valuesEnumerator = property.Value.EnumerateArray();
                    while (valuesEnumerator.MoveNext())
                    {
                        values.Add(Utilities.ConvertJsonElementToObject(valuesEnumerator.Current, fieldType));
                    }
                    query.Append(condition);
                    parameters.Add(values);
                    paramIndex++;
                    break;
                case JsonValueKind.Object:
                    ObjectEnumerator enumerator = property.Value.EnumerateObject();
                    if (enumerator.Count() > 1)
                        query.Append("(");
                    while (enumerator.MoveNext())
                    {
                        condition = new StringBuilder(propertyName);
                        JsonProperty operatorProperty = enumerator.Current;
                        switch (operatorProperty.Name)
                        {
                            case ">=":
                            case "<=":
                            case "<":
                            case ">":
                            case "=":
                            case "!":
                                condition.Append(operatorProperty.Name).Append("@").Append(paramIndex);
                                parameters.Add(Utilities.ConvertJsonElementToObject(operatorProperty.Value, fieldType));
                                break;
                            case "contains":
                                condition.Append(".Contains(@").Append(paramIndex).Append(")");
                                parameters.Add(Utilities.ConvertJsonElementToObject(operatorProperty.Value, fieldType));
                                break;
                            case "startsWith":
                                condition.Append(".StartsWith(@").Append(paramIndex).Append(")");
                                parameters.Add(Utilities.ConvertJsonElementToObject(operatorProperty.Value, fieldType));
                                break;
                            case "endsWith":
                                condition.Append(".EndsWith(@").Append(paramIndex).Append(")");
                                parameters.Add(Utilities.ConvertJsonElementToObject(operatorProperty.Value, fieldType));
                                break;
                            default: throw new Exception("Invalid criteria (WHERE)");
                        }
                        condition.Append(AND_OPERATOR);
                        query.Append(condition);
                        paramIndex++;
                    }
                    query.Remove(query.Length - AND_OPERATOR.Length, AND_OPERATOR.Length);
                    if (enumerator.Count() > 1)
                        query.Append(")");
                    break;
                default: throw new Exception("Invalid criteria (WHERE)");
            }
        }

        private void Sort<TEntity>(StringBuilder query, JsonProperty property)
        {
            InfosOfProperty infos = GetModelPropertyInfo<TEntity>(property.Name);
            string propertyName = infos.Name;
            switch (property.Value.ToString().ToLower())
            {
                case "asc":
                case "1":
                    query.Append(propertyName);
                    query.Append(" ");
                    query.Append("ASC");
                    break;
                case "desc":
                case "-1":
                    query.Append(propertyName);
                    query.Append(" ");
                    query.Append("DESC");
                    break;
            }
        }

        private InfosOfProperty GetModelPropertyInfo<TEntity>(string name)
        {
            string[] names;
            if (name.IndexOf('.') > -1)
            {
                names = name.Split('.');
            }
            else
            {
                names = new string[1] { name };
            }
            StringBuilder nestedPropertyName = new StringBuilder("");
            PropertyInfo property = null;
            string propertyName = null;
            Type type = typeof(TEntity);
            int i = 0;
            while (i < names.Length - 1)
            {
                propertyName = Utilities.ToCSharpName(names[i]);
                nestedPropertyName.Append(propertyName);
                nestedPropertyName.Append('.');
                property = type.GetProperty(propertyName);
                type = property.PropertyType;
                i++;
            }
            propertyName = Utilities.ToCSharpName(names[i]);
            property = type.GetProperty(propertyName);

            type = property.PropertyType;
            nestedPropertyName.Append(property.Name);
            if (property.PropertyType.IsSubclassOf(typeof(Model)))
            {
                nestedPropertyName.Append(".").Append("Id");
                type = typeof(int);
            }

            return new InfosOfProperty
            {
                Name = nestedPropertyName.ToString(),
                Type = type
            };

        }
        private struct InfosOfProperty
        {
            public string Name;
            public Type Type;
        }

    }
}
