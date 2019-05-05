using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Anti_Memory_Dumping
{
    public partial class Main : Form
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr ZeroMemory(IntPtr addr, IntPtr size);

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualProtect(IntPtr lpAddress, IntPtr dwSize, IntPtr flNewProtect, ref IntPtr lpflOldProtect);

        public static void EraseSection(IntPtr address, int size)
        {
            IntPtr sz = (IntPtr)size;
            IntPtr dwOld = default(IntPtr);
            VirtualProtect(address, sz, (IntPtr)0x40, ref dwOld);
            ZeroMemory(address, sz);
            IntPtr temp = default(IntPtr);
            VirtualProtect(address, sz, dwOld, ref temp);
        }

        public Main()
        {
            InitializeComponent();

            int[] sectiontabledwords = new int[] { 0x8, 0xC, 0x10, 0x14, 0x18, 0x1C, 0x24 };
            int[] peheaderbytes = new int[] { 0x1A, 0x1B };
            int[] peheaderwords = new int[] { 0x4, 0x16, 0x18, 0x40, 0x42, 0x44, 0x46, 0x48, 0x4A, 0x4C, 0x5C, 0x5E };

            var process = System.Diagnostics.Process.GetCurrentProcess();
            var base_address = process.MainModule.BaseAddress;
            var dwpeheader = Marshal.ReadInt32((IntPtr)(base_address.ToInt32() + 0x3C));
            var wnumberofsections = Marshal.ReadInt16((IntPtr)(base_address.ToInt32() + dwpeheader + 0x6));

            for (int i = 0; i < peheaderwords.Length; i++)
            {
                EraseSection((IntPtr)(base_address.ToInt32() + dwpeheader + peheaderwords[i]), 1);
            }
            for (int i = 0; i < peheaderbytes.Length; i++)
            {
                EraseSection((IntPtr)(base_address.ToInt32() + dwpeheader + peheaderbytes[i]), 2);
            }

            int x = 0;
            int y = 0;

            while (x <= wnumberofsections)
            {
                if (y == 0)
                {
                    EraseSection((IntPtr)((base_address.ToInt32() + dwpeheader + 0xFA + (0x28 * x)) + 0x20), 2);
                }

                y++;

                if (y == sectiontabledwords.Length)
                {
                    x++;
                    y = 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I don't know why I made this button haha", "iYaReM", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
