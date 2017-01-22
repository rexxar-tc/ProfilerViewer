This is a plugin for SpaceEngineers that does two things. First, it dumps internal profiler data to a handy window so you can see what's going wrong in your world. Second, it injects extra code int he game to give you even more detailed profiling.

To run it, download this dll and put it in your Bin64 folder, then start SE with the argument -plugin ProfilerViewer.dll

The window will automatically appear when the world is loaded.

**WARNING:** If you tick the "Profile Grids" and "Profile Blocks" options, you will likely see extreme performance loss. Use it only when you need to.

Most of the code for this plugin was taken from SESE, so it behaves identically to the profiling system in SESE.

You can also use this plugin to profile your own mods. Before the code you want to profile, add the line MySimpleProfiler.Begin("&&MOD&&Some Descriptive Text Here");
Afterward, call MySimpleProfiler.End("&&MOD&&Some Descriptive Text Here");

Adding &&MOD&& to the beginning of your block name makes the plugin sort your profiling blocks into a separate mod category.