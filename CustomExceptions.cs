using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OOP_LB1
{
    public class CustomOverflowException : OverflowException
    {
        public string AdditionalInfo { get; }

        public CustomOverflowException(string message, string additionalInfo) : base(message)
        {
            AdditionalInfo = additionalInfo;
        }
    }

    public static class MessageBoxHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public static void ShowMessage(string text, string caption)
        {
            MessageBox(IntPtr.Zero, text, caption, 0);
        }
    }
}
