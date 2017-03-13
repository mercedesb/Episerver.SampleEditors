using EPiServer.Shell.ObjectEditing;
using System;

namespace SampleEditors.Models.Attributes
{
	/// <summary>
	/// This attribute is used to define the SelectionFactory to use for the autocomplete suggestions
	/// Use this attribute in conjunction with [BackingType(typeof(PropertyStringList))] and [UIHint(MakingWavesUIHint.AutocompleteList)]
	/// in order to get the correct editor experience
	/// example: [AutocompleteList(typeof(CategorySelectionQuery))]
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class AutocompleteListAttribute : Attribute
	{
		public string ValueLabel { get; set; }
		public string AddButtonLabel { get; set; }
		public string RemoveButtonLabel { get; set; }

		public virtual Type SelectionFactoryType { get; set; }

		public AutocompleteListAttribute(Type selectionFactoryType)
			: this(selectionFactoryType, "Value", "+", "X")
		{ }

		public AutocompleteListAttribute(Type selectionFactoryType, string valueLabel)
			: this(selectionFactoryType, valueLabel, "+", "X")
		{ }

		public AutocompleteListAttribute(Type selectionFactoryType, string valueLabel, string addButtonLabel, string removeButtonLabel)
		{
			if (selectionFactoryType.IsAssignableFrom(typeof(ISelectionQuery)))
			{
				throw new ArgumentException("SelectionFactoryType needs to implement ISelectionQuery");
			}
			this.SelectionFactoryType = selectionFactoryType;

			ValueLabel = valueLabel;
			AddButtonLabel = addButtonLabel;
			RemoveButtonLabel = removeButtonLabel;
		}
	}
}
