using EPiServer.Shell.ObjectEditing;
using SampleEditors.Models.Interfaces;
using System.Collections.Generic;

namespace SampleEditors.Models.CustomProperties
{

	/// <summary>
	/// This selection factory allows the developer to create a pre-defined list of values to use in SelectOnes or SelectManys
	/// This may be a static list or the result of a GET call to an external service or any other use case you can think of
	/// example : [SelectMany(SelectionFactoryType = typeof(SelectionListSelectionFactory<Amenities>))]
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SelectionListSelectionFactory<T> : ISelectionFactory where T : ISelectionList, new()
	{
		public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
		{
			T selectionList = new T();
			return selectionList.Selections;
		}
	}
}
