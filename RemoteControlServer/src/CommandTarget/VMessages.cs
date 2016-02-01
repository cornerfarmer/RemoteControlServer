using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace RemoteControlServer.CommandTarget
{
    class VMessages
    {
        IntPtr eClassHandle;

        public VMessages(string windowDescription = "", string parentWindowDescription = "")
        {
            if (windowDescription != "")
            {
                if (parentWindowDescription == "")
                    eClassHandle = Win32.FindWindow(null, windowDescription);
                else
                {
                    eClassHandle = Win32.FindWindowEx(Win32.FindWindow(null, parentWindowDescription), (IntPtr)null, null, windowDescription);
                    if (eClassHandle.ToInt32() == 0)
                        eClassHandle = Win32.FindWindow(null, windowDescription);
                }
            }
            else
            {
                eClassHandle = Win32.GetForegroundWindow();
            }

            //Win32.FindWindow(null, "Untitled - Notepad");
            //Win32.FindWindowEx(eClassHandle, IntPtr.Zero, "Edit", null);
        }

        public Boolean windowExists()
        {
            if ((int)eClassHandle == 0)
                return false;
            return true;
        }



        public void sendKey(System.Windows.Forms.Keys pKey)
        {
            keyDown(pKey);
            Thread.Sleep(10);
            keyUp(pKey);
        }

        public void keyDown(System.Windows.Forms.Keys pKey)
        {
            Win32.PostMessage(eClassHandle, Win32.WM_KEYDOWN, (int)pKey, 0x00140001);
        }

        public void keyUp(System.Windows.Forms.Keys pKey)
        {
            Win32.PostMessage(eClassHandle, Win32.WM_KEYUP, (int)pKey, 0xC0000001);
        }
        

        public void Wait(float pSeconds)
        { System.Threading.Thread.Sleep((Int32)(pSeconds * 1000)); }



    }
}
