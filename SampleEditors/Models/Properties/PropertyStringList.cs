using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SampleEditors.Models.CustomProperties
{
	/// <summary>
	/// This property is used for the editor to define a list of strings
	/// </summary>
	[PropertyDefinitionTypePlugIn(Description = "A property for list of string items.", DisplayName = "String List")]
	public class PropertyStringList : PropertyLongString
	{
		public override Type PropertyValueType
		{
			get
			{
				return typeof(IEnumerable<string>);
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
				return JsonConvert.DeserializeObject<IEnumerable<string>>(value);
			}
			set
			{
				if (value is IEnumerable<string>)
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
}
