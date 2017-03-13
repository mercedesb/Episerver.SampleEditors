using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;

namespace SampleEditors.Models.EditorDescriptors
{
	/// <summary>
	/// This editor descriptor is used to apply a date only (no time) editor to a property of time DateTime or DateTime?
	/// example: [UIHint(MakingWavesUIHint.DateOnly)]
	/// </summary>
	[EditorDescriptorRegistration(TargetType = typeof(DateTime), UIHint = MakingWavesUIHint.DateOnly), EditorDescriptorRegistration(TargetType = typeof(DateTime?), UIHint = MakingWavesUIHint.DateOnly)]
	public class DateOnlyEditorDescriptor : EditorDescriptor
	{
		public DateOnlyEditorDescriptor()
		{
			ClientEditingClass = "dijit/form/DateTextBox";
		}
	}
}
