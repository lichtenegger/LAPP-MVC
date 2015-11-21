using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI_BricoMarche.Models
{
    public sealed class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) {
                return false;
            }
            if (value.GetType() != typeof(bool)) {
                throw new InvalidOperationException("Error: " +value+ " is not boolean");
            }
            return (bool)value == true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + "-field must be checked to continue";
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = String.IsNullOrEmpty(ErrorMessage) ? FormatErrorMessage(metadata.DisplayName) : ErrorMessage,
                ValidationType = "enforcetrue"
            };
        }
    }
}