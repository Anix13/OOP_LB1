using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB1
{
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
