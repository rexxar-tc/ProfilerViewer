using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sandbox;
using VRage;
using Timer = System.Windows.Forms.Timer;

namespace ProfilerViewer
{
    public partial class Viewer : Form
    {
        private Timer m_profilerTimer;
        private bool m_profilerPaused;

        public Viewer()
        {
            InitializeComponent();
            m_profilerTimer = new Timer() {Interval = 2000};
            m_profilerTimer.Tick += ProfilerRefresh;
            m_profilerTimer.Start();
        }

        private void CHK_Pause_CheckedChanged(object sender, EventArgs e)
        {
            m_profilerPaused = CHK_Pause.Checked;
        }

        private void CHK_Grids_CheckedChanged(object sender, EventArgs e)
        {
            ProfilerInjection.ProfilePerGrid = CHK_Grids.Checked;
        }

        private void CHK_Blocks_CheckedChanged(object sender, EventArgs e)
        {
            ProfilerInjection.ProfilePerBlock = CHK_Blocks.Checked;
        }

	    private void ProfilerRefresh(object sender, EventArgs e)
	    {
	        if (m_profilerPaused)
	            return;

	        MySimpleProfiler.MySimpleProfilingBlock[] blockCache = null;

            AutoResetEvent ev = new AutoResetEvent(false);

	        MySandboxGame.Static.Invoke(() =>
	                                    {
	                                        var fieldInfo = typeof(MySimpleProfiler).GetField("m_profilingBlocks", BindingFlags.Static | BindingFlags.NonPublic);
	                                        if (fieldInfo == null)
	                                        {
	                                            m_profilerTimer.Stop();
	                                            return;
	                                        }

	                                        var blocks = fieldInfo.GetValue(null) as Dictionary<string, MySimpleProfiler.MySimpleProfilingBlock>;
	                                        if (blocks == null)
	                                        {
	                                            m_profilerTimer.Stop();
	                                            throw new InvalidCastException();
	                                        }

	                                        blockCache = blocks.Values.ToArray();
	                                        ev.Set();
	                                    });

	        ev.WaitOne(10000);

	        if (blockCache == null)
	            return;

	        var graphicsBlocks = new List<MySimpleProfiler.MySimpleProfilingBlock>();
	        var blockBlocks = new List<MySimpleProfiler.MySimpleProfilingBlock>();
	        var otherBlocks = new List<MySimpleProfiler.MySimpleProfilingBlock>();
	        var unknownBlocks = new List<MySimpleProfiler.MySimpleProfilingBlock>();
	        var systemBlocks = new List<MySimpleProfiler.MySimpleProfilingBlock>();
	        var characterBlocks = new List<MySimpleProfiler.MySimpleProfilingBlock>();
            var gridBlocks = new List<MySimpleProfiler.MySimpleProfilingBlock>();
            var modBlocks = new List<MySimpleProfiler.MySimpleProfilingBlock>();

            foreach (var block in blockCache)
	        {
	            switch (block.type)
	            {
	                case MySimpleProfiler.MySimpleProfilingBlock.ProfilingBlockType.GRAPHICS:
	                    graphicsBlocks.Add(block);
	                    break;

	                case MySimpleProfiler.MySimpleProfilingBlock.ProfilingBlockType.BLOCK:
	                    switch (block.Name)
	                    {
	                        case "Grid":
	                            systemBlocks.Add(block);
	                            break;

	                        case "Oxygen":
	                            systemBlocks.Add(block);
	                            break;

	                        case "Conveyor":
	                            systemBlocks.Add(block);
	                            break;

	                        case "Blocks":
	                            systemBlocks.Add(block);
	                            break;

	                        case "Gyro":
	                            systemBlocks.Add(block);
	                            break;

	                        case "Scripts":
	                            systemBlocks.Add(block);
	                            break;

	                        default:
                                if(block.Name.StartsWith("Character"))
                                    characterBlocks.Add(block);
                                else if ( block.Name.StartsWith( "&&GRID&&" ) )
                                    gridBlocks.Add( block );
                                else if(block.Name.StartsWith("&&MOD&&"))
                                    modBlocks.Add(block);
                                else
	                                blockBlocks.Add(block);
	                            break;
	                    }
	                    break;

	                case MySimpleProfiler.MySimpleProfilingBlock.ProfilingBlockType.OTHER:
	                    otherBlocks.Add(block);
	                    break;

	                default:
	                    unknownBlocks.Add(block);
	                    break;
	            }
	        }

	        StringBuilder sb = new StringBuilder();

	        graphicsBlocks.Sort((a, b) => b.Average.CompareTo(a.Average));
	        blockBlocks.Sort((a, b) => b.Average.CompareTo(a.Average));
	        otherBlocks.Sort((a, b) => b.Average.CompareTo(a.Average));
	        unknownBlocks.Sort((a, b) => b.Average.CompareTo(a.Average));
            systemBlocks.Sort((a, b) => b.Average.CompareTo(a.Average));
            characterBlocks.Sort((a, b) => b.Average.CompareTo(a.Average));
            gridBlocks.Sort((a, b) => b.Average.CompareTo(a.Average));

            if (systemBlocks.Any(b => b.Average.IsValid() && b.Average >= 0.001))
	        {
	            sb.AppendLine("Grid systems");

                foreach(var block in systemBlocks)
                {
                    if (!block.Average.IsValid() || block.Average < 0.001)
                        continue;

                    sb.AppendLine($"{block.DisplayName}: {block.Average:N3}ms");
                }

                sb.AppendLine();
            }

            if (gridBlocks.Any(b => b.Average.IsValid() && b.Average >= 0.001))
            {
                sb.AppendLine("Grids:");

                foreach (var block in gridBlocks)
                {
                    if (!block.Average.IsValid() || block.Average < 0.001)
                        continue;

                    sb.AppendLine($"{block.DisplayName.Substring(8)}: {block.Average:N3}ms");
                }

                sb.AppendLine();
            }

            if (modBlocks.Any(b => b.Average.IsValid() && b.Average >= 0.001))
            {
                sb.AppendLine("Mods:");

                foreach (var block in modBlocks)
                {
                    if (!block.Average.IsValid() || block.Average < 0.001)
                        continue;

                    sb.AppendLine($"{block.DisplayName.Substring(7)}: {block.Average:N3}ms");
                }

                sb.AppendLine();
            }

            if (blockBlocks.Any(b => b.Average.IsValid() && b.Average >= 0.001))
	        {
	            sb.AppendLine("Blocks:");

	            foreach (var block in blockBlocks)
	            {
	                if (!block.Average.IsValid() || block.Average < 0.001)
	                    continue;

	                sb.AppendLine($"{block.DisplayName}: {block.Average:N3}ms");
	            }

	            sb.AppendLine();
	        }

	        if (characterBlocks.Any(b => b.Average.IsValid() && b.Average >= 0.001))
	        {
	            sb.AppendLine("Characters:");
	            foreach (var block in characterBlocks)
                {
                    if (!block.Average.IsValid() || block.Average < 0.001)
                        continue;

                    sb.AppendLine($"{block.DisplayName}: {block.Average:N6}ms");
	            }
	            sb.AppendLine();
	        }

	        if (otherBlocks.Any( b => b.Average.IsValid() && b.Average >= 0.001))
	        {
	            sb.AppendLine("Other:");

	            foreach (var block in otherBlocks)
	            {
	                if (!block.Average.IsValid() || block.Average < 0.001)
	                    continue;

	                sb.AppendLine($"{block.DisplayName}: {block.Average:N3}ms");
	            }
	            sb.AppendLine();
	        }

            if (graphicsBlocks.Any(b => b.Average.IsValid() && b.Average >= 0.001))
	        {
	            sb.AppendLine("Graphics:");

	            foreach (var block in graphicsBlocks)
	            {
	                if (!block.Average.IsValid() || block.Average < 0.001)
	                    continue;

	                sb.AppendLine($"{block.DisplayName}: {block.Average:N3}ms");
	            }
	            sb.AppendLine();
	        }

            if (unknownBlocks.Any(b => b.Average.IsValid() && b.Average >= 0.001))
	        {
	            sb.AppendLine("UNKNOWN:");

	            foreach (var block in unknownBlocks)
	            {
	                if (!block.Average.IsValid() || block.Average < 0.001)
	                    continue;

	                sb.AppendLine($"{block.DisplayName}: {block.Average:N3}ms");
	            }
	            sb.AppendLine();
	        }

	        TXT_Profiler.Text = sb.ToString();
	    }
    }
}
