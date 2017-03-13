using System;

namespace SampleEditors.Models.Attributes
{
	/// <summary>
	/// This attribute is used to create a property editor for an enumerable of key/value pairs
	/// Use this attribute on properties of type IEnumerable<KeyValueItem> and in conjunction with [BackingType(typeof(PropertyKeyValueItems))]
	/// in order to get the correct editor experience
	/// example: [KeyValueItems("Heading (e.g. Monday-Thursday)", "Value (e.g. 11:30 am - 10:00 pm")]
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class KeyValueItemsAttribute : Attribute
	{
		public string KeyLabel { get; set; }
		public string ValueLabel { get; set; }
		public string AddButtonLabel { get; set; }
		public string RemoveButtonLabel { get; set; }
		public bool ShowSelectedByDefaultCheckbox { get; set; }
		public string KeyValidationExpression { get; set; }
		public string ValueValidationExpression { get; set; }
		public string KeyValidationMessage { get; set; }
		public string ValueValidationMessage { get; set; }

		public KeyValueItemsAttribute(string keyLabel, string valueLabel)
			: this(keyLabel, valueLabel, false, "+", "X", string.Empty, string.Empty, string.Empty, string.Empty)
		{ }

		public KeyValueItemsAttribute(string keyLabel,
									  string valueLabel,
									  bool showSelectedCheckbox,
									  string addButtonLabel,
									  string removeButtonLabel,
									  string keyValidationExpression,
									  string keyValidationMessage,
									  string valueValidationExpression,
									  string valueValidationMessage)
		{
			KeyLabel = keyLabel;
			ValueLabel = valueLabel;
			ShowSelectedByDefaultCheckbox = showSelectedCheckbox;
			AddButtonLabel = addButtonLabel;
			RemoveButtonLabel = removeButtonLabel;

			KeyValidationExpression = keyValidationExpression;
			KeyValidationMessage = keyValidationMessage;

			ValueValidationExpression = valueValidationExpression;
			ValueValidationMessage = valueValidationMessage;
		}
	}
}