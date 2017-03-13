using System;

namespace SampleEditors.Models.Attributes
{
	/// <summary>
	/// This attribute is used to create a property editor for an enumerable of strings
	/// Use this attribute on properties of type IEnumerable<string> and in conjunction with [BackingType(typeof(PropertyStringList))]
	/// in order to get the correct editor experience
	/// example: [StringList("Specialty")]
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class StringListAttribute : Attribute
	{
		public string ValueLabel { get; set; }
		public string AddButtonLabel { get; set; }
		public string RemoveButtonLabel { get; set; }
		public string ValidationExpression { get; set; }
		public string ValidationMessage { get; set; }

		public StringListAttribute(string valueLabel)
			: this(valueLabel, "+", "X", string.Empty, string.Empty)
		{ }

		public StringListAttribute(string valueLabel,
									  string addButtonLabel,
									  string removeButtonLabel,
									  string validationExpression,
									  string validationMessage)
		{
			ValueLabel = valueLabel;
			AddButtonLabel = addButtonLabel;
			RemoveButtonLabel = removeButtonLabel;
			ValidationExpression = validationExpression;
			ValidationMessage = validationMessage;
		}
	}
}
