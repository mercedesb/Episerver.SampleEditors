using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using SampleEditors.Models.Selection;
using System;

namespace SampleEditors.Models.EditorDescriptors
{
	/// <summary>
	/// This editor descriptor is used to apply a dropdown editor to a property of time DayOfWeek or DayOfWeek?
	/// example: [UIHint(MakingWavesUIHint.DayOfWeek)]
	/// </summary>
	[EditorDescriptorRegistration(TargetType = typeof(DayOfWeek), UIHint = MakingWavesUIHint.DayOfWeek), EditorDescriptorRegistration(TargetType = typeof(DayOfWeek?), UIHint = MakingWavesUIHint.DayOfWeek)]
	public class DayOfWeekEditorDescriptor : EnumEditorDescriptor<DayOfWeek>
	{
	}

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class DayOfWeekSelectionAttribute : SelectManyAttribute
	{
		public override Type SelectionFactoryType
		{
			get
			{
				return typeof(EnumSelectionFactory<DayOfWeek>);
			}
			set
			{
				base.SelectionFactoryType = value;
			}
		}
	}
}
