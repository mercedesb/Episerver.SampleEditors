using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using SampleEditors.Models.Attributes;
using SampleEditors.Models.CustomProperties;
using System.Collections.Generic;
using System.Linq;

namespace SampleEditors.Models.EditorDescriptors
{
	/// <summary>
	/// This editor descriptor is used to apply the correct dojo editor to properties of type IEnumerable<KeyValueItem>
	/// </summary>
	[EditorDescriptorRegistration(TargetType = typeof(IEnumerable<KeyValueItem>))]
	public class KeyValueItemsEditorDescriptor : EditorDescriptor
	{
		protected const string KEY_LABEL = "keyLabel";
		protected const string VALUE_LABEL = "valueLabel";
		protected const string ADD_BUTTON_LABEL = "addButtonLabel";
		protected const string REMOVE_BUTTON_LABEL = "removeButtonLabel";
		protected const string KEY_VALIDATION_EXPRESSION = "keyValidationExpression";
		protected const string KEY_VALIDATION_MESSAGE = "keyValidationMessage";
		protected const string VALUE_VALIDATION_EXPRESSION = "valueValidationExpression";
		protected const string VALUE_VALIDATION_MESSAGE = "valueValidationMessage";
		protected const string SHOW_SELECTED_BY_DEFAULT_CHECKBOX = "showSelectedByDefaultCheckbox";

		public KeyValueItemsEditorDescriptor()
		{
			ClientEditingClass = "makingwaves/editors/KeyValueItems";
		}


		protected override void SetEditorConfiguration(ExtendedMetadata metadata)
		{
			// Get the KeyValueItemsAttribute with which you can specify the editor configuration, 
			// and override the default settings in the KeyValueItems.js file.
			var keyValueItemsAttribute = metadata.Attributes.FirstOrDefault(a => typeof(KeyValueItemsAttribute) == a.GetType()) as KeyValueItemsAttribute;

			if (keyValueItemsAttribute != null)
			{
				if (!string.IsNullOrEmpty(keyValueItemsAttribute.KeyLabel))
					EditorConfiguration[KEY_LABEL] = keyValueItemsAttribute.KeyLabel;
				else
					EditorConfiguration.Remove(KEY_LABEL);

				if (!string.IsNullOrEmpty(keyValueItemsAttribute.ValueLabel))
					EditorConfiguration[VALUE_LABEL] = keyValueItemsAttribute.ValueLabel;
				else
					EditorConfiguration.Remove(VALUE_LABEL);

				if (!string.IsNullOrEmpty(keyValueItemsAttribute.AddButtonLabel))
					EditorConfiguration[ADD_BUTTON_LABEL] = keyValueItemsAttribute.AddButtonLabel;
				else
					EditorConfiguration.Remove(ADD_BUTTON_LABEL);

				if (!string.IsNullOrEmpty(keyValueItemsAttribute.RemoveButtonLabel))
					EditorConfiguration[REMOVE_BUTTON_LABEL] = keyValueItemsAttribute.RemoveButtonLabel;
				else
					EditorConfiguration.Remove(REMOVE_BUTTON_LABEL);

				if (!string.IsNullOrEmpty(keyValueItemsAttribute.KeyValidationExpression))
					EditorConfiguration[KEY_VALIDATION_EXPRESSION] = keyValueItemsAttribute.KeyValidationExpression;
				else
					EditorConfiguration.Remove(KEY_VALIDATION_EXPRESSION);

				if (!string.IsNullOrEmpty(keyValueItemsAttribute.KeyValidationMessage))
					EditorConfiguration[KEY_VALIDATION_MESSAGE] = keyValueItemsAttribute.KeyValidationMessage;
				else
					EditorConfiguration.Remove(KEY_VALIDATION_MESSAGE);

				if (!string.IsNullOrEmpty(keyValueItemsAttribute.ValueValidationExpression))
					EditorConfiguration[VALUE_VALIDATION_EXPRESSION] = keyValueItemsAttribute.ValueValidationExpression;
				else
					EditorConfiguration.Remove(VALUE_VALIDATION_EXPRESSION);

				if (!string.IsNullOrEmpty(keyValueItemsAttribute.ValueValidationMessage))
					EditorConfiguration[VALUE_VALIDATION_MESSAGE] = keyValueItemsAttribute.ValueValidationMessage;
				else
					EditorConfiguration.Remove(VALUE_VALIDATION_MESSAGE);

				EditorConfiguration[SHOW_SELECTED_BY_DEFAULT_CHECKBOX] = keyValueItemsAttribute.ShowSelectedByDefaultCheckbox;

			}

			base.SetEditorConfiguration(metadata);
		}
	}
}
