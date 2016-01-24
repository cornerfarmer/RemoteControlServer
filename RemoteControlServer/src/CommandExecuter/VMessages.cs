using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RemoteControlServer.CommandExecuter
{
    class VMessages
    {
        string[] eWindowClassStructure;
        Int32 eClassHandle;

        public VMessages(string pWindowClassStructure)
        {
            eWindowClassStructure = pWindowClassStructure.Split(new string[] { "//" }, StringSplitOptions.None);
            eClassHandle = Win32.FindWindow(null, eWindowClassStructure[0]);
            if (eWindowClassStructure.Count() > 1)
            {
                for (int i = 1; i < eWindowClassStructure.Count(); i++)
                {
                    eClassHandle = Win32.FindWindowEx(eClassHandle, IntPtr.Zero, eWindowClassStructure[i], null);
                }

            }

            //Win32.FindWindow(null, "Untitled - Notepad");
            //Win32.FindWindowEx(eClassHandle, IntPtr.Zero, "Edit", null);
        }

        public Boolean windowExists()
        {
            if (eClassHandle == 0)
                return false;
            return true;
        }


        public void sendText(string pText)
        {
            char[] tmpText = pText.ToCharArray();

            foreach (char c in tmpText)
            {
                sendChar(c);
            }

        }

        public void sendKey(System.Windows.Forms.Keys pKey)
        {
            Win32.PostMessage(eClassHandle, Win32.WM_KEYDOWN, (IntPtr)Win32.VkKeyScan((char)pKey), (IntPtr)0x00140001);
            Win32.PostMessage(eClassHandle, Win32.WM_CHAR, (IntPtr)(0x00000000 + (Int32)pKey), (IntPtr)0x00140001);
            Win32.PostMessage(eClassHandle, Win32.WM_KEYUP, (IntPtr)Win32.VkKeyScan((char)pKey), (IntPtr)0xC0140001);
        }

        public void sendChar(char pChar)
        {
            Win32.PostMessage(eClassHandle, Win32.WM_KEYDOWN, (IntPtr)Win32.VkKeyScan(pChar), (IntPtr)0x00140001);
            //Win32.PostMessage(eClassHandle, Win32.WM_CHAR, 0x00000000 + (Int32)pChar, 0x00140001);
            try
            {
                Win32.PostMessage(eClassHandle, Win32.WM_KEYUP, (IntPtr)Win32.VkKeyScan(pChar), (IntPtr)0xC0140001);
            }
            catch
            {
                int fsd = 2;
                fsd = 2;


            }
        }
        public void sendmessage(uint msg, int wParam, long lParam)
        {
            Win32.SendMessage(eClassHandle, msg, wParam, lParam);
        }

        public void Wait(float pSeconds)
        { System.Threading.Thread.Sleep((Int32)(pSeconds * 1000)); }



    }
}
