using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI3.FPS
{
    public static class StartFPS
    {
        public async static  void Start(Process p,int fps)
        {
            Unlocker unlocker = new Unlocker(p, fps);
            var result = await unlocker.StartProcessAndUnlockAsync();
        }
    }
}
