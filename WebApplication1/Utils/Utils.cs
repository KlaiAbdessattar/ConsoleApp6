using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Text.Json.JsonElement;

namespace ConsoleApp6.Utils
{
    static public class Utilities
    {
        public static KeyValuePair<string, object> CastFrom(Object obj)
        {
            var type = obj.GetType();
            if (type.IsGenericType)
            {
                if (type == typeof(KeyValuePair<,>))
                {
                    var key = type.GetProperty("Key");
                    var value = type.GetProperty("Value");
                    var keyObj = key.GetValue(obj, null).ToString();
                    var valueObj = value.GetValue(obj, null);
                    return new KeyValuePair<string, object>(keyObj, valueObj);
                }
            }
            throw new ArgumentException(" ### -> public static KeyValuePair<object , object > CastFrom(Object obj) : Error : obj argument must be KeyValuePair<,>");
        }

        public static object ConvertJsonElementToObject(JsonElement element, Type type)
        {
            if (element.ValueKind == JsonValueKind.Number && (type == typeof(bool?) || type == typeof(bool)))
            {
                int value = (int)JsonSerializer.Deserialize(element.GetRawText(), typeof(int));
                return value == 1;
            }
            return JsonSerializer.Deserialize(element.GetRawText(), type);
        }

        public static int? GetIdFromNavigationPoperty(Object element)
        {
            if (element == null)
                return null;
            return GetIdFromNavigationPoperty((JsonElement)element);
        }


        public static int? GetIdFromNavigationPoperty(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                JsonElement idProperty = element.GetProperty("id");
                return (int?)ConvertJsonElementToObject(idProperty, typeof(int?));
            }
            return (int?)ConvertJsonElementToObject(element, typeof(int?)) ?? null;
        }

        public static IList GetGenericListOfType(Type type)
        {
            Type listType = typeof(List<>).MakeGenericType(new[] { type });
            return (IList)Activator.CreateInstance(listType);
        }

        public static object CreateInstanceOfType(Type type, params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        public static string ToCSharpName(string name)
        {
            string cSharpName = char.ToUpper(name[0]) + name.Substring(1);
            int length = cSharpName.Length;
            for (int i = 0; i < length; i++)
            {
                if (cSharpName[i] == '_')
                {
                    cSharpName = cSharpName.Remove(i, 1);
                    string camelCase = cSharpName[i].ToString().ToUpper();
                    cSharpName = cSharpName.Remove(i, 1);
                    cSharpName = cSharpName.Insert(i, camelCase);
                    length--;
                }
            }
            return cSharpName;
        }

        public static string ToJavascriptName(string name)
        {
            return char.ToLower(name[0]) + name.Substring(1);
        }

        public static bool EnumeratorHasItems(ObjectEnumerator enumerator)
        {
            return enumerator.Count() > 0;
        }
        public static bool EnumeratorHasItems(ArrayEnumerator enumerator)
        {
            return enumerator.Count() > 0;
        }
        public static IEnumerable<Type> LoadClassesByNameSpace(string nameSpace)
        {
            return Assembly.GetExecutingAssembly().GetTypes()
              .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal));
        }

        public static Type GetTypeByIdentity(string identity)
        {
            return Assembly.GetExecutingAssembly().GetTypes()
              .Where(t => String.Equals(t.FullName, identity, StringComparison.Ordinal)).Take(1).SingleOrDefault();
        }

        public static Type LoadClassByNameSpaceAndName(string nameSpace, string name)
        {
            return Assembly.GetExecutingAssembly().GetTypes()
              .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal) && String.Equals(t.Name, name)).Take(1).SingleOrDefault();
        }

        public static int GetConstructorParamsNumber(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Length;
        }

        public static object[] CreateArrayOfNull(int n)
        {
            object[] array = new object[n];
            for (int i = 0; i < n; i++)
                array[i] = null;
            return array;
        }

        public static string GetMonthAndYear(DateTime date)
        {
            return date.Year + "-" + date.Month.ToString().PadLeft(2, '0');
        }
        public static string FormatDate(DateTime date)
        {
            return date.Year + "-" + date.Month.ToString().PadLeft(2, '0') + "-" + date.Day.ToString().PadLeft(2, '0');
        }

        public static DateTime GetDateTimeFromMonthAndYear(string monthYear)
        {
            string[] split = monthYear.Split('-');
            int year = Convert.ToInt32(split[0]);
            int month = Convert.ToInt32(split[1]);
            return new DateTime(year, month, 1);
        }

        public static DateTime MonthStart(DateTime date)
        {
            DateTime noTime = date.Date;
            return noTime.AddDays(1 - noTime.Day);
        }
        public static DateTime MonthEnd(DateTime date)
        {
            DateTime noTime = date.Date;
            return MonthStart(noTime.AddMonths(1)).AddDays(-1);
        }

        public static string GetMonth(DateTime date)
        {
            return date.Month > 9 ? date.Month.ToString() : "0" + date.Month.ToString();
        }
    }
}
