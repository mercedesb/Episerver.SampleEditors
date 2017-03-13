using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace SampleEditors.Models.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
		 AllowMultiple = true, Inherited = true)]
	public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
	{
		private string DefaultErrorMessageFormatString = "The {0} is required";
		public List<string> DependentProperties { get; }
		public List<string> DependentValues { get; }
		public string Props { get; }
		public string Vals { get; }
		public string requiredFieldValue { get; }

		//To avoid multiple rules with same name
		public static Dictionary<string, int> countPerField = null;
		//Required if you want to use this attribute multiple times
		private object _typeId = new object();
		public override object TypeId
		{
			get { return _typeId; }
		}
		public RequiredIfAttribute(string dependentProperties,
					  string dependentValues = "", string requiredValue = "val")
		{
			if (string.IsNullOrWhiteSpace(dependentProperties))
			{
				throw new ArgumentNullException("dependentProperties");
			}
			string[] props = dependentProperties.Trim().Split(new char[] { ',' });
			if (props != null && props.Length == 0)
			{
				throw new ArgumentException("Prameter Invalid:DependentProperties");
			}

			if (props.Contains("") || props.Contains(null))
			{
				throw new ArgumentException("Prameter Invalid:DependentProperties," +
							 "One of the Property Name is Empty");
			}

			string[] vals = null;
			if (!string.IsNullOrWhiteSpace(dependentValues))
				vals = dependentValues.Trim().Split(new char[] { ',' });

			if (vals != null && vals.Length != props.Length)
			{
				throw new ArgumentException("Different Number " +
						"Of DependentProperties And DependentValues");
			}

			DependentProperties = new List<string>();
			DependentProperties.AddRange(props);
			Props = dependentProperties.Trim();
			if (vals != null)
			{
				DependentValues = new List<string>();
				DependentValues.AddRange(vals);
				Vals = dependentValues.Trim();
			}

			if (requiredValue == "val")
				requiredFieldValue = "val";
			else if (string.IsNullOrWhiteSpace(requiredValue))
			{
				requiredFieldValue = string.Empty;
				DefaultErrorMessageFormatString = "The {0} should not be given";
			}
			else
			{
				requiredFieldValue = requiredValue;
				DefaultErrorMessageFormatString =
						  "The {0} should be:" + requiredFieldValue;
			}

			if (props.Length == 1)
			{
				if (vals != null)
				{
					ErrorMessage = DefaultErrorMessageFormatString +
										", When " + props[0] + " is ";
					if (vals[0] == "val")
						ErrorMessage += " given";
					else if (vals[0] == "")
						ErrorMessage += " not given";
					else
						ErrorMessage += vals[0];
				}
				else
					ErrorMessage = DefaultErrorMessageFormatString +
										", When " + props[0] + " is given";
			}
			else
			{
				if (vals != null)
				{
					ErrorMessage = DefaultErrorMessageFormatString +
										", When " + dependentProperties + " are: ";
					foreach (string val in vals)
					{
						if (val == "val")
							ErrorMessage += "AnyValue,";
						else if (val == "")
							ErrorMessage += "Empty,";
						else
							ErrorMessage += val + ",";
					}
					ErrorMessage = ErrorMessage.Remove(ErrorMessage.Length - 1);
				}
				else
					ErrorMessage = DefaultErrorMessageFormatString + ", When " +
										dependentProperties + " are given";
			}
		}

		protected override ValidationResult IsValid(object value,
								 ValidationContext validationContext)
		{
			//Validate Dependent Property Values First
			for (int i = 0; i < DependentProperties.Count; i++)
			{
				var contextProp =
				  validationContext.ObjectInstance.GetType().
				  GetProperty(DependentProperties[i]);
				var contextPropVal = Convert.ToString(contextProp.GetValue(
													  validationContext.ObjectInstance, null));

				var requiredPropVal = "val";
				if (DependentValues != null)
					requiredPropVal = DependentValues[i];

				if (requiredPropVal ==
						 "val" && string.IsNullOrWhiteSpace(contextPropVal))
					return ValidationResult.Success;
				else if (requiredPropVal == string.Empty &&
								!string.IsNullOrWhiteSpace(contextPropVal))
					return ValidationResult.Success;
				else if (requiredPropVal != string.Empty && requiredPropVal !=
								"val" && requiredPropVal != contextPropVal)
					return ValidationResult.Success;
			}

			string fieldVal = (value != null ? value.ToString() : string.Empty);

			if (requiredFieldValue == "val" && fieldVal.Length == 0)
				return new ValidationResult(ServiceLocator.Current.GetInstance<LocalizationService>().GetString(ErrorMessage));
			else if (requiredFieldValue == string.Empty && fieldVal.Length != 0)
				return new ValidationResult(ServiceLocator.Current.GetInstance<LocalizationService>().GetString(ErrorMessage));
			else if (requiredFieldValue != string.Empty && requiredFieldValue != "val" && requiredFieldValue != fieldVal)
				return new ValidationResult(ServiceLocator.Current.GetInstance<LocalizationService>().GetString(ErrorMessage));

			return ValidationResult.Success;
		}

		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, ServiceLocator.Current.GetInstance<LocalizationService>().GetString(ErrorMessage), name);
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
				 ModelMetadata metadata, ControllerContext context)
		{
			int count = 0;
			string Key = metadata.ContainerType.FullName + "." + metadata.GetDisplayName();

			if (countPerField == null)
				countPerField = new Dictionary<string, int>();

			if (countPerField.ContainsKey(Key))
			{
				count = ++countPerField[Key];
			}
			else
				countPerField.Add(Key, count);

			yield return new RequiredIfValidationRule(string.Format(ErrorMessageString,
					metadata.GetDisplayName()), requiredFieldValue, Props, Vals, count);
		}
	}

	public class RequiredIfValidationRule : ModelClientValidationRule
	{
		public RequiredIfValidationRule(string errorMessage, string reqVal,
				 string otherProperties, string otherValues, int count)
		{
			string tmp = count == 0 ? "" : Char.ConvertFromUtf32(96 + count);
			ErrorMessage = errorMessage;
			ValidationType = "requiredif" + tmp;
			ValidationParameters.Add("reqval", reqVal);
			ValidationParameters.Add("others", otherProperties);
			ValidationParameters.Add("values", otherValues);
		}
	}
}

