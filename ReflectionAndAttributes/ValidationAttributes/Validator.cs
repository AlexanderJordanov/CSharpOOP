using System;
using System.Linq;
using System.Reflection;
using ValidationAttributes.Attributes;

namespace ValidationAttributes
{
    public static class Validator
    {
        public static bool IsValid(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            PropertyInfo[] customAttsProperties = properties.Where(p => Attribute.IsDefined(p, typeof(MyValidationAttribute), inherit: true)).ToArray();

            foreach(var prop in customAttsProperties)
            {
                var validationAtts = prop.GetCustomAttributes(typeof(MyValidationAttribute), inherit: true).Cast<MyValidationAttribute>();
                foreach(var att in validationAtts)
                {
                    object value = prop.GetValue(obj);
                    if (att.IsValid(value) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
