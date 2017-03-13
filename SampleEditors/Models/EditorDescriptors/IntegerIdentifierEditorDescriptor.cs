using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using SampleEditors.Utility;
using System;
using System.Collections.Generic;

namespace SampleEditors.Models.EditorDescriptors
{
	/// <summary>
	/// This editor descriptor is used to format an integer property without comma separators, this is most useful when the user is defining IDs
	/// example: [UIHint(MakingWavesUIHint.IntegerIdentifier)]
	/// </summary>
	[EditorDescriptorRegistration(TargetType = typeof(int), UIHint = MakingWavesUIHint.IntegerIdentifier), EditorDescriptorRegistration(TargetType = typeof(int?), UIHint = MakingWavesUIHint.IntegerIdentifier)]
	public class IntegerIdentifierEditorDescriptor : IntegerEditorDescriptor
	{
		public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
		{
			base.ModifyMetadata(metadata, attributes);
			object constraints = base.EditorConfiguration["constraints"];
			constraints = constraints.AddProperty("Pattern", "#");
			base.EditorConfiguration["constraints"] = constraints;
			this.SetEditorConfiguration(metadata);
		}
	}
}
