﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.MI.Application.Validation
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            ValidationDictionary = new Dictionary<string, string>();
        }

        public Dictionary<string, string> ValidationDictionary { get; set; }

        public void AddError(string propertyName, string validationError)
        {
            ValidationDictionary.Add(propertyName, validationError);
        }

        public void AddError(string propertyName)
        {
            ValidationDictionary.Add(propertyName, $"{propertyName} has not been supplied");
        }

        public bool IsValid()
        {
            if (ValidationDictionary == null)
            {
                return false;
            }

            return !ValidationDictionary.Any();
        }

        public bool IsUnauthorized { get; set; }
    }
}
