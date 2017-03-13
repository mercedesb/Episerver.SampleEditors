using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleEditors.Models.CustomProperties
{
	/// <summary>
	/// This is a basic selection query that can be used to drive autocomplete functionality from the categories stored in Episerver
	/// </summary>
	[ServiceConfiguration(typeof(ISelectionQuery))]
	public class CategorySelectionQuery : ISelectionQuery
	{
		CategoryRepository _categoryRepo;
		List<SelectItem> _items;
		public CategorySelectionQuery()
		{
			_categoryRepo = ServiceLocator.Current.GetInstance<CategoryRepository>();
			_items = GetItems();
		}

		protected virtual List<SelectItem> GetItems()
		{
			Category root = _categoryRepo.GetRoot();
			IEnumerable<Category> categories = root.GetList().Cast<Category>().Where(cat => cat.Selectable);
			List<SelectItem> items = new List<SelectItem>();
			foreach (var category in categories)
			{

				items.Add(new SelectItem { Text = GetDisplayText(category), Value = category.LocalizedDescription });
			}

			return items;
		}

		protected virtual string GetDisplayText(Category category)
		{
			Category root = _categoryRepo.GetRoot();
			Category current = category;

			string display = category.LocalizedDescription;
			while (current.Parent != root)
			{
				display = string.Format("{0} > {1}", current.Parent.LocalizedDescription, display);
				current = current.Parent;
			}

			return display;
		}

		//Will be called when the editor types something in the selection editor.        
		public virtual IEnumerable<ISelectItem> GetItems(string query)
		{
			return _items.Where(i => i.Value.ToString().StartsWith(query, StringComparison.OrdinalIgnoreCase));
		}

		//Will be called when initializing an editor with an existing value to get the corresponding text representation.        
		public virtual ISelectItem GetItemByValue(string value)
		{
			ISelectItem current = _items.FirstOrDefault(i => i.Value.Equals(value));

			if (current != null)
				return current;

			return new SelectItem { Text = value, Value = value };
		}
	}
}
