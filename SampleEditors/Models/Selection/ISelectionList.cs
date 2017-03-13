using EPiServer.Shell.ObjectEditing;
using System.Collections.Generic;

namespace SampleEditors.Models.Interfaces
{
	public interface ISelectionList
	{
		IEnumerable<ISelectItem> Selections { get; }
	}
}
