using EPiServer.Framework.Localization;
using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace SampleEditors.Models.Selection
{
	/// <summary>
	/// This is a generic selection factory to be used with enums for SelectOne or SelectMany editors
	/// </summary>
	/// <typeparam name="TEnum"></typeparam>
	public class EnumSelectionFactory<TEnum> : ISelectionFactory
	{
		public IEnumerable<ISelectItem> GetSelections(
			ExtendedMetadata metadata)
		{
			var values = Enum.GetValues(typeof(TEnum));
			foreach (var value in values)
			{
				yield return new SelectItem
				{
					Text = GetValueName(value),
					Value = value
				};
			}
		}

		private string GetValueName(object value)
		{
			var staticName = Enum.GetName(typeof(TEnum), value);

			string localizationPath = string.Format(
				"/property/enum/{0}/{1}",
				typeof(TEnum).Name.ToLowerInvariant(),
				staticName.ToLowerInvariant());

			string localizedName;
			if (LocalizationService.Current.TryGetString(
				localizationPath,
				out localizedName))
			{
				return localizedName;
			}

			FieldInfo fi = (typeof(TEnum)).GetField(staticName);
			var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
			if (attributes != null && attributes.Length > 0)
				return attributes[0].Description;

			return staticName;
		}
	}
}
