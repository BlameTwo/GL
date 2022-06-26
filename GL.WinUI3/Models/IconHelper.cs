using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GL.UI.Models
{
    static class IconHelper
    {

        
        
        
        
        [DllImport("User32.dll")]
        public static extern int PrivateExtractIcons(
                 string lpszFile, 
                 int nIconIndex,  
                 int cxIcon,      
                 int cyIcon,      
                 IntPtr[] phicon, 
                 int[] piconid,   
                 int nIcons,      
                 int flags        
             );

        //details:https://msdn.microsoft.com/en-us/library/windows/desktop/ms648063(v=vs.85).aspx
        //Destroys an icon and frees any memory the icon occupied.
        [DllImport("User32.dll")]
        public static extern bool DestroyIcon(
             IntPtr hIcon //A handle to the icon to be destroyed. The icon must not be in use.
         );
        public static Bitmap GetIcon(string file)
        {

            //选中文件中的图标总数
            var iconTotalCount = PrivateExtractIcons(file, 0, 0, 0, null, null, 0, 0);

            //用于接收获取到的图标指针
            IntPtr[] hIcons = new IntPtr[iconTotalCount];
            //对应的图标id
            int[] ids = new int[iconTotalCount];
            //成功获取到的图标个数
            var successCount = PrivateExtractIcons(file, 0, 256, 256, hIcons, ids, iconTotalCount, 0);

            //遍历并保存图标
            for (var i = 0; i < successCount; i++)
            {
                //指针为空，跳过
                if (hIcons[i] == IntPtr.Zero) continue;

                using (var ico = Icon.FromHandle(hIcons[i]))
                {
                    return ico.ToBitmap();
                }
                //内存回收
                //DestroyIcon(hIcons[i]);
            }
            return null;
        }
    }


}
