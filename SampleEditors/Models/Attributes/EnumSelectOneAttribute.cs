using EPiServer.Shell.ObjectEditing;
using SampleEditors.Models.Selection;
using System;

namespace SampleEditors.Models.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class EnumSelectOneAttribute : SelectOneAttribute
	{
		public EnumSelectOneAttribute(Type enumType)
		{
			EnumType = enumType;
		}

		public Type EnumType { get; set; }

		public override Type SelectionFactoryType
		{
			get
			{
				return typeof(EnumSelectionFactory<>).MakeGenericType(EnumType);
			}
			set
			{
				base.SelectionFactoryType = value;
			}
		}
	}
}
