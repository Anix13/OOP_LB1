using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HRProject.Models
{
    public class CustomException : Exception
    {
        public string ErrorCode { get; set; }
        public string AdditionalInfo { get; set; }

        public CustomException(string message, string errorCode, string additionalInfo)
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