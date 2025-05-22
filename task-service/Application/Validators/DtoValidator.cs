namespace task_service.Application.Validators
{
    using System;
    using System.Reflection;

    public static class DtoValidator
    {
        public static bool HasEmptyValues(object dto)
        {
            if (dto == null) return true;

            PropertyInfo[] properties = dto.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(dto);

                if (property.PropertyType == typeof(string))
                {
                    if (string.IsNullOrWhiteSpace((string)value)) return true;
                }

                else 
                    if (IsDefaultValue(value, property.PropertyType))
                    {
                        return true;
                    }
            }

            return false;
        }

        private static bool IsDefaultValue(object value, Type type)
        {
            return type.IsValueType
                ? value.Equals(Activator.CreateInstance(type))
                : value == null;
        }
    }
}
