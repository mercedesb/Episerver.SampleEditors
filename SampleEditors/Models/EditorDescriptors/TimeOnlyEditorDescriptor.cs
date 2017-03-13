using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;

namespace SampleEditors.Models.EditorDescriptors
{
	/// <summary>
	/// This editor descriptor is used to apply a time only (no date) editor to a property of time DateTime or DateTime?
	/// example: [UIHint(MakingWavesUIHint.TimeOnly)]
	/// </summary>
	[EditorDescriptorRegistration(TargetType = typeof(DateTime), UIHint = MakingWavesUIHint.TimeOnly), EditorDescriptorRegistration(TargetType = typeof(DateTime?), UIHint = MakingWavesUIHint.TimeOnly)]
	public class TimeOnlyEditorDescriptor : EditorDescriptor
	{
		public TimeOnlyEditorDescriptor()
		{
			ClientEditingClass = "dijit/form/TimeTextBox";
		}
	}
}
