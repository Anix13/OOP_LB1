﻿using System;

namespace HRProject.Models
{
    public class SalaryOverflowException : OverflowException
    {
        public string ErrorCode { get; set; }
        public string AdditionalInfo { get; set; }

        public SalaryOverflowException(string message, string errorCode, string additionalInfo)
            : base(message)
        {
            ErrorCode = errorCode;
            AdditionalInfo = additionalInfo;
        }

        public override string ToString()
        {
            return $"{ErrorCode} :{AdditionalInfo}";
        }
    }
}