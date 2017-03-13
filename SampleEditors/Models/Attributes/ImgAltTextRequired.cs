using EPiServer.Core;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SampleEditors.Models.Attributes
{
	/// <summary>    
	/// Validate all images in Xhtml have alt tags.
	/// You can add this attribute to XhtmlString at runtime via an intialization module 
	/// to prompt the editor with an error when an image is missing the alt tag
	/// example: TypeDescriptor.AddAttributes(typeof(XhtmlString), new[] { new ImgAltTextRequired() });
	/// </summary>  
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ImgAltTextRequiredAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			return ValidateXhtmlString(value as XhtmlString);
		}

		private bool ValidateXhtmlString(XhtmlString xhtml)
		{
			if (xhtml == null || xhtml.IsEmpty)
				return true;

			var doc = new HtmlDocument();
			doc.LoadHtml(xhtml.ToString());

			// if there are images without alt tags, invalid
			IEnumerable<HtmlNode> imgs = doc.DocumentNode.Descendants("img");
			if (imgs != null && imgs.Any())
			{
				if (imgs.Any(i => i.Attributes["alt"] == null || string.IsNullOrWhiteSpace(i.Attributes["alt"].Value)))
					return false;
			}

			return true;
		}

		public override string FormatErrorMessage(string name)
		{
			return "All images must have a description for the alt text";
		}
	}
}
