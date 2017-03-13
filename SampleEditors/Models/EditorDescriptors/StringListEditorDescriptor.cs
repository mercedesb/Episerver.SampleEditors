using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using SampleEditors.Models.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace SampleEditors.Models.EditorDescriptors
{
	/// <summary>
	/// This editor descriptor is used to apply the correct dojo editor to properties of type IEnumerable<string>
	/// </summary>
	[EditorDescriptorRegistration(TargetType = typeof(IEnumerable<string>))]
	public class StringListEditorDescriptor : EditorDescriptor
	{
		protected const string VALUE_LABEL = "valueLabel";
		protected const string ADD_BUTTON_LABEL = "addButtonLabel";
		protected const string REMOVE_BUTTON_LABEL = "removeButtonLabel";
		protected const string VALIDATION_EXPRESSION = "validationExpression";
		protected const string VALIDATION_MESSAGE = "validationMessage";

		public StringListEditorDescriptor()
		{
			ClientEditingClass = "makingwaves/editors/StringList";
		}

		protected override void SetEditorConfiguration(ExtendedMetadata metadata)
		{
			// Get the StringListAttribute with which you can specify the editor configuration, 
			// and override the default settings in the StringList.js file.
			var stringListAttribute = metadata.Attributes.FirstOrDefault(a => typeof(StringListAttribute) == a.GetType()) as StringListAttribute;

			if (stringListAttribute != null)
			{
				if (!string.IsNullOrEmpty(stringListAttribute.ValueLabel))
					EditorConfiguration[VALUE_LABEL] = stringListAttribute.ValueLabel;
				else
					EditorConfiguration.Remove(VALUE_LABEL);

				if (!string.IsNullOrEmpty(stringListAttribute.AddButtonLabel))
					EditorConfiguration[ADD_BUTTON_LABEL] = stringListAttribute.AddButtonLabel;
				else
					EditorConfiguration.Remove(ADD_BUTTON_LABEL);

				if (!string.IsNullOrEmpty(stringListAttribute.RemoveButtonLabel))
					EditorConfiguration[REMOVE_BUTTON_LABEL] = stringListAttribute.RemoveButtonLabel;
				else
					EditorConfiguration.Remove(REMOVE_BUTTON_LABEL);

				if (!string.IsNullOrEmpty(stringListAttribute.ValidationExpression))
					EditorConfiguration[VALIDATION_EXPRESSION] = stringListAttribute.ValidationExpression;
				else
					EditorConfiguration.Remove(VALIDATION_EXPRESSION);

				if (!string.IsNullOrEmpty(stringListAttribute.ValidationMessage))
					EditorConfiguration[VALIDATION_MESSAGE] = stringListAttribute.ValidationMessage;
				else
					EditorConfiguration.Remove(VALIDATION_MESSAGE);
			}

			base.SetEditorConfiguration(metadata);
		}
	}
}
