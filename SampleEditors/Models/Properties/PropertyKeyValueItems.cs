using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SampleEditors.Models.CustomProperties
{
	/// <summary>
	/// This property is used for the editor to define a list of key/value pairs
	/// </summary>
	[PropertyDefinitionTypePlugIn(Description = "A property for list of key-value-items.", DisplayName = "Key-Value Items")]
	public class PropertyKeyValueItems : PropertyLongString
	{
		public override Type PropertyValueType
		{
			get
			{
				return typeof(IEnumerable<KeyValueItem>);
			}
		}

		public override object SaveData(PropertyDataCollection properties)
		{
			return LongString;
		}

		public override object Value
		{
			get
			{
				var value = base.Value as string;

				if (value == null)
				{
					return null;
				}
				return JsonConvert.DeserializeObject<IEnumerable<KeyValueItem>>(value);
			}
			set
			{
				if (value is IEnumerable<KeyValueItem>)
				{
					base.Value = JsonConvert.SerializeObject(value);
				}
				else
				{
					base.Value = value;
				}
			}
		}

		public override IPropertyControl CreatePropertyControl()
		{
			//No support for legacy edit mode
			return null;
		}
	}


	public class KeyValueItem
	{
		public string Key { get; set; }
		public string Value { get; set; }
		public bool SelectedByDefault { get; set; }

		public KeyValueItem() { }

		public KeyValueItem(string key, string value)
		{
			Key = key;
			Value = value;
			SelectedByDefault = false;
		}

		public KeyValueItem(string key, string value, bool selectedByDefault)
		{
			Key = key;
			Value = value;
			SelectedByDefault = selectedByDefault;
		}
	}
}
