using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sandbox;
using Sandbox.ModAPI;
using VRage;
using VRage.Plugins;
using VRage.Utils;

namespace ProfilerViewer
{
    public class ProfilerViewer : IPlugin
    {
        private Viewer viewer;

        public void Dispose()
        {
            viewer?.Close();
        }

        public void Init(object gameInstance)
        {
            MyLog.Default.WriteLineAndConsole("##PLUGIN: ProfilerViewer plugin initializing....");
            ProfilerInjection.Init();
        }

        public void Update()
        {
            if (MyAPIGateway.Session == null)
                return;

            if (viewer != null && viewer.Visible)
                return;

            Task.Run(() =>
                     {
                         try
                         {
                             viewer = new Viewer();
                             //viewer.Show();
                             Application.Run(viewer);
                         }
                         catch (Exception ex)
                         {
                             MyLog.Default.WriteLineAndConsole(ex.ToString());
                             MySandboxGame.Static.Invoke(() => { throw ex; });
                             throw;
                         }
                     });
        }
    }
}
