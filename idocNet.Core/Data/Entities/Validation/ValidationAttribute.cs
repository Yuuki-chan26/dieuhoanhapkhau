using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace idocNet.Core.Data.Entities.Validation
{
	public class ValidationAttribute : Attribute
	{
		public string ErrorMessage
		{
			get;
			set;
		}
	}

	public class RequireFieldAttribute : ValidationAttribute
	{

	}

	public class RangeAttribute : ValidationAttribute
	{
		public object MinValue
		{
			get;
			set;
		}

		public object MaxValue
		{
			get;
			set;
		}

		public RangeAttribute(object minValue, object maxValue)
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
		}
	}

	public class LengthAttribute : ValidationAttribute
	{
		public int MaxLength
		{
			get;
			set;
		}
		public int MinLength
		{
			get;
			set;
		}

		public LengthAttribute(int minLength, int maxLength)
		{
			this.MinLength = minLength;
			this.MaxLength = maxLength;
		}

		public LengthAttribute(int maxLength)
		{
			this.MaxLength = maxLength;
		}
	}

	public class RegexFormatAttribute : ValidationAttribute
	{
		public string Regex
		{
			get;
			set;
		}

		public RegexOptions RegexOptions
		{
			get;
			set;
		}

		private RegexFormatAttribute()
		{
			this.RegexOptions = RegexOptions.None;
		}

		public RegexFormatAttribute(string regex)
		{
			this.Regex = regex;
		}

		public RegexFormatAttribute(string regex, RegexOptions options)
		{
			this.Regex = regex;
			this.RegexOptions = options;
		}
	}

	public class EmailFormatAttribute : RegexFormatAttribute
	{
		public EmailFormatAttribute()
			: base(@"^[\w\.=-]+@[\w\.-]+\.[\w]{2,3}$", RegexOptions.IgnoreCase)
		{

		}
	}

	public class UrlFormatAttribute : RegexFormatAttribute
	{
		public UrlFormatAttribute()
			: base(@"^https?://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]$", RegexOptions.IgnoreCase)
		{

		}
	}

	public class BirthdayAttribute : RangeAttribute
	{
		public BirthdayAttribute()
			: base(new DateTime(1900, 1, 1), DateTime.Today)
		{

		}
	}
    public class DateFormatAttribute : RegexFormatAttribute
	{
		public DateFormatAttribute()
            : base(@"^(19|20)\d\d[\-\/.](0[1-9]|1[012])[\-\/.](0[1-9]|[12][0-9]|3[01])$", RegexOptions.IgnoreCase)
		{

		}
	}
    public class PhoneFormatAttribute : RegexFormatAttribute
	{
		public PhoneFormatAttribute()
            : base(@"^(0|\+84)\d{2,3}[-.]?\d{3}[-.]?\d{4}$", RegexOptions.IgnoreCase)
		{

		}
	}

    public class DateTimeFormatAttribute : RegexFormatAttribute
	{
        public DateTimeFormatAttribute()
            : base(@"^(19|20)\d\d[\-\/.](0[1-9]|1[012])[\-\/.](0[1-9]|[12][0-9]|3[01]) (([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))$", RegexOptions.IgnoreCase)
		{

		}
	}
}
