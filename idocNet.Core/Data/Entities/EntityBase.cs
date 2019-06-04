using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using idocNet.Core.Data.Entities.Validation;
using idocNet.Core.Utils;

namespace idocNet.Core.Data.Entities
{
	public abstract class EntityBase : IEnsureValidation
	{

		#region validations
		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="ValidationException"></exception>
		protected List<ValidationError> ValidateFields(bool throwErrors)
		{
			List<ValidationError> errors = new List<ValidationError>();
			Type t = this.GetType();
			foreach (PropertyInfo property in t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				ValidationAttribute[] attributes = (ValidationAttribute[])property.GetCustomAttributes(typeof(ValidationAttribute), true);
				if (attributes != null && attributes.Length > 0)
				{
					foreach (ValidationAttribute attribute in attributes)
					{
						object value = property.GetValue(this, null);
						if (attribute is RequireFieldAttribute)
						{
							#region Required
							if (value == null)
							{
								errors.Add(new ValidationError(property.Name, ValidationErrorType.NullOrEmpty, attribute.ErrorMessage));
								break;
							}
							else if (value is string)
							{
								if (value.ToString() == "")
								{
									errors.Add(new ValidationError(property.Name, ValidationErrorType.NullOrEmpty, attribute.ErrorMessage));
									break;
								}
							}
							#endregion
						}
						else if (attribute is RangeAttribute)
						{
							#region Range
							if (value != null)
							{
								RangeAttribute rangeAttribute = (RangeAttribute)attribute;
								if (value is IComparable)
								{
									if (((IComparable)value).CompareTo(rangeAttribute.MinValue) < 0 || ((IComparable)value).CompareTo(rangeAttribute.MaxValue) > 0)
									{
										errors.Add(new ValidationError(property.Name, ValidationErrorType.Range, attribute.ErrorMessage));
									}
								}
								else
								{
									throw new ArgumentException("RangeAttribute only applies to IComparable fields.");
								}
							}
							#endregion
						}
						else if (attribute is LengthAttribute)
						{
							#region Length
							if (value != null)
							{
								LengthAttribute lengthAttribute = (LengthAttribute)attribute;
								if (value is string)
								{
									if (((string)value).Length > 0)
									{
										if (((string)value).Length > lengthAttribute.MaxLength)
										{
											errors.Add(new ValidationError(property.Name, ValidationErrorType.MaxLength, attribute.ErrorMessage));
										}
										if (lengthAttribute.MinLength > 0)
										{
											if (((string)value).Length < lengthAttribute.MinLength)
											{
												errors.Add(new ValidationError(property.Name, ValidationErrorType.MinLength, attribute.ErrorMessage));
											}
										}
									}
								}
								else
								{
									throw new ArgumentException("LengthAttribute only applies to string fields.");
								}
							}
							#endregion
						}
						else if (attribute is RegexFormatAttribute)
						{
							#region Regex
							if (value != null)
							{
								RegexFormatAttribute regexAttribute = (RegexFormatAttribute)attribute;
								if (value.ToString() != "" && !Regex.IsMatch(value.ToString(), regexAttribute.Regex, regexAttribute.RegexOptions))
								{
									errors.Add(new ValidationError(property.Name, ValidationErrorType.Format, attribute.ErrorMessage));
								}
							}
							#endregion
						}

					}
				}
			}

			if (errors.Count > 0 && throwErrors)
			{
				throw new ValidationException(errors);
			}

			return errors;
		}

		/// <exception cref="ValidationException"></exception>
		public virtual void ValidateFields()
		{
			this.ValidateFields(true);
		}



		#endregion


		#region data parsing

		public virtual void ParseData(DataRow dr)
		{
			Type t = this.GetType();
			foreach (PropertyInfo property in t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				DataColumAttribute[] attributes = (DataColumAttribute[])property.GetCustomAttributes(typeof(DataColumAttribute), true);
				if (attributes != null && attributes.Length > 0)
				{
					var attribute = attributes[0];
					string colName = !string.IsNullOrEmpty(attribute.Name) ? attribute.Name : property.Name;
					if (dr.Table.Columns.Contains(colName))
					{

						SetValue(property, dr[colName]);
					}
				}
				else
				{
					DataExAttribute[] exAttributes = (DataExAttribute[])property.GetCustomAttributes(typeof(DataExAttribute), true);

					if (exAttributes != null && exAttributes.Length > 0)
					{

						EntityBase instance = (EntityBase)Activator.CreateInstance(property.PropertyType);

						instance.ParseDataEx(dr);


						SetValue(property, instance);
					}
				}
			}

		}

		public virtual void ParseDataEx(DataRow dr)
		{
			Type t = this.GetType();
			foreach (PropertyInfo property in t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				DataColumExAttribute[] attributes = (DataColumExAttribute[])property.GetCustomAttributes(typeof(DataColumExAttribute), true);
				if (attributes != null && attributes.Length > 0)
				{
					foreach (var attribute in attributes)
					{
						string colName = !string.IsNullOrEmpty(attribute.Name) ? attribute.Name : property.Name;
						if (dr.Table.Columns.Contains(colName))
						{


							SetValue(property, dr[colName]);
						}
					}
				}
			}

		}


		private void SetValue(PropertyInfo prop, object value)
		{

			if (value != null && value != DBNull.Value)
			{
				if (prop.PropertyType == typeof(string))
				{
					prop.SetValue(this, value.ToString(), null);
				}
				else if (prop.PropertyType != value.GetType())
				{
					if (prop.PropertyType == typeof(int))
					{
						prop.SetValue(this, int.Parse(value.ToString()), null);
					}
					else if (prop.PropertyType == typeof(float))
					{
						prop.SetValue(this, float.Parse(value.ToString()), null);
					}
					else if (prop.PropertyType == typeof(double))
					{
						prop.SetValue(this, double.Parse(value.ToString()), null);
					}
					else if (prop.PropertyType == typeof(long))
					{
						prop.SetValue(this, long.Parse(value.ToString()), null);
					}
					else
						prop.SetValue(this, value, null);

				}
				else
					prop.SetValue(this, value, null);
			}
		}


		public void SetValue(string name, object value)
		{

			Type t = this.GetType();
			foreach (PropertyInfo property in t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (property.Name.Equals(name))
				{
					this.SetValue(property, value);
					return;

				}
			}
		}

		public object GetValue(string name)
		{

			Type t = this.GetType();
			foreach (PropertyInfo property in t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (property.Name.Equals(name))
				{
					return property.GetValue(this, null);
				}
			}

			return null;
		}


		public bool HasAttribute(string name, Type attType)
		{

			Type t = this.GetType();
			foreach (PropertyInfo property in t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (property.Name.Equals(name))
				{
					var attributes = property.GetCustomAttributes(attType, true);

					if (attributes != null && attributes.Length > 0) return true;

				}
			}

			return false;
		}

		public static List<T> ParseListFromTable<T>(DataTable dt) where T : EntityBase
		{
			List<T> list = new List<T>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<T>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        T instance = (T)Activator.CreateInstance(typeof(T), true);
                        instance.ParseData(dr);
                        list.Add(instance);
                    }
                }
            return list;


        }

		#endregion



		#region uuid
		public static string GenerateNewGuid()
		{
			return UniqueIdGenerator.GetInstance().GetBase32UniqueId(16);
		}
		#endregion

	}
}
