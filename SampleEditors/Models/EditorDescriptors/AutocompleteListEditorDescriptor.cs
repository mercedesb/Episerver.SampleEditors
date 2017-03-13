using EPiServer.ServiceLocation;
using EPiServer.Shell.Modules;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using SampleEditors.Models.Attributes;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SampleEditors.Models.EditorDescriptors
{
	/// <summary>
	/// This editor descriptor is used to apply the correct dojo editor to properties of type IEnumerable<string> for autocomplete functionality
	/// example: [UIHint(MakingWavesUIHint.AutocompleteList)]
	/// </summary>
	[EditorDescriptorRegistration(TargetType = typeof(IEnumerable<string>), UIHint = MakingWavesUIHint.AutocompleteList)]
	public class AutocompleteListEditorDescriptor : EditorDescriptor
	{
		protected const string VALUE_LABEL = "valueLabel";
		protected const string ADD_BUTTON_LABEL = "addButtonLabel";
		protected const string REMOVE_BUTTON_LABEL = "removeButtonLabel";

		public AutocompleteListEditorDescriptor()
		{
			ClientEditingClass = "makingwaves/editors/AutocompleteList";
		}

		protected override void SetEditorConfiguration(ExtendedMetadata metadata)
		{
			// Get the AutocompleteListAttribute with which you can specify the editor configuration, 
			// and override the default settings in the AutocompleteList.js file.
			var stringListAttribute = metadata.Attributes.FirstOrDefault(a => typeof(AutocompleteListAttribute) == a.GetType()) as AutocompleteListAttribute;

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

				string format = ServiceLocator.Current.GetInstance<ModuleTable>().ResolvePath("Shell", "stores/selectionquery/{0}/");
				EditorConfiguration["storeurl"] = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					stringListAttribute.SelectionFactoryType.FullName
				});
			}

			base.SetEditorConfiguration(metadata);
		}
	}
}
