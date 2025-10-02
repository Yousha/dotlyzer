/*
 * Name: Dotlyzer
 * Description: CLI & interactive based process analyzer and diagnostic tool.
 * Version: 1.0.0.1
 * Locale: en_International
 * Last update: 2025
 * Architecture: multi-arch
 * API: .NET 8
 * Compiler: C# 12
 * Builder: MsBuild 16.9
 * License:
 * Copyright: Copyright (c) 2025 Yousha Aleayoub.
 * Producer: Yousha Aleayoub
 * Maintainer: Yousha Aleayoub
 * Contact: yousha.a@hotmail.com
 * Link: https://yousha.blog.ir
 */

#define PRODUCTION

#if DEBUG
#warning Debug mode is active.
#endif

namespace Dotlyzer
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.IO;
   using System.Linq;
   using System.Management;
   using System.Runtime.InteropServices;
   using System.Security.Principal;

   internal class Program
   {
      private static void Main(string[] args)
      {
         Console.Clear();

         while (true)
         {
            Console.ResetColor();
            Console.WriteLine("Dotlyzer 1.0.0.1\n");
            Console.WriteLine("Menu:");
            Console.WriteLine("  1. List Processes");
            Console.WriteLine("  2. Analyze Process");
            Console.WriteLine("  3. About");
            Console.WriteLine("  4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
               ListAllProcesses();
            }
            else if (choice == "2")
            {
               InspectProcessMenu();
            }
            else if (choice == "3")
            {
               ShowAbout();
            }
            else if (choice == "4")
            {
               break;
            }
            else
            {
               Console.WriteLine("Invalid option. Please try again.\n");
            }
         }
      }

      internal static void ShowAbout()
      {
         Console.ForegroundColor = ConsoleColor.White;
         Console.WriteLine("\n[About]");
         Console.ResetColor();
         Console.WriteLine("Developer/Maiantainer: Yousha Aleayoyb");
         Console.WriteLine("Version: 1.0.0.1");
         Console.WriteLine("Description: CLI & interactive based process analyzer and diagnostic tool.");
         Console.WriteLine("Features:");
         Console.WriteLine("  - Process listing and inspection");
         Console.WriteLine("  - Memory analysis");
         Console.WriteLine("  - Thread analysis");
         Console.WriteLine("  - Diagnostic features");
         Console.WriteLine("  - Profiling capabilities");
         Console.WriteLine("  - Minidump creation");
         Console.WriteLine("  - Performance monitoring");
         Console.WriteLine("\nPress any key to continue...");
         Console.ReadKey();
         Console.Clear();
      }

      internal static void ListAllProcesses()
      {
         Console.Clear();
         Console.WriteLine("Running Processes:");
         Console.WriteLine(new string('-', 50));
         var processes = Process.GetProcesses().OrderBy(p => p.ProcessName);
         foreach (var p in processes)
         {
            try
            {
               bool isDotNet = p.Modules.Cast<ProcessModule>()
                   .Any(m => IsDotNetRuntime(m.ModuleName));
               string tag = isDotNet ? "[.NET]" : "";
               if (isDotNet)
               {
                  Console.ForegroundColor = ConsoleColor.Cyan;
                  Console.WriteLine($"{p.Id,6} | {p.ProcessName,-30} {tag}");
                  Console.ResetColor();
               }
               else
               {
                  Console.WriteLine($"{p.Id,6} | {p.ProcessName,-30} {tag}");
               }
            }
            catch { /* Skip inaccessible */ }
         }
         Console.WriteLine("\nPress any key to continue...");
         Console.ReadKey();
         Console.Clear();
      }

      static void InspectProcessMenu()
      {
         Console.Write("\nEnter Process ID to analyze: ");
         string input = Console.ReadLine()?.Trim();

         if (!int.TryParse(input, out int pid))
         {
            Console.WriteLine("Invalid Process ID.\n");
            return;
         }

         try
         {
            using var process = Process.GetProcessById(pid);

            while (true)
            {
               Console.Clear();
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine($"\n--- Analyzing Process: {process.ProcessName} (PID: {pid}) ---");
               Console.ResetColor();

               Console.WriteLine("\nAnalyze Options:");
               Console.WriteLine("  1. System Diagnostics");
               Console.WriteLine("  2. Memory Analysis");
               Console.WriteLine("  3. Thread Analysis");
               Console.WriteLine("  4. Process Diagnostic");
               Console.WriteLine("  5. Profiling");
               Console.WriteLine("  6. Create Minidump");
               Console.WriteLine("  7. Back");
               Console.Write("Choose an option: ");
               string choice = Console.ReadLine();

               if (choice == "1")
               {
                  ShowSystemDiagnostics(process);
                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
               }
               else if (choice == "2")
               {
                  ShowMemoryAnalysis(process);
                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
               }
               else if (choice == "3")
               {
                  ShowThreadAnalysis(process);
                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
               }
               else if (choice == "4")
               {
                  ShowDiagnosticFeatures(process);
                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
               }
               else if (choice == "5")
               {
                  ShowProfilingFeatures(process);
                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
               }
               else if (choice == "6")
               {
                  CreateMiniDump(pid);
                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
               }
               else if (choice == "7")
               {
                  break;
               }
               else
               {
                  Console.WriteLine("Invalid option. Please try again.");
                  Console.WriteLine("\nPress any key to continue...");
                  Console.ReadKey();
               }
            }
         }
         catch (ArgumentException)
         {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Process {pid} not found.");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
         }
         catch (Exception ex)
         {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Error: {ex.Message}");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
         }
      }

      internal static void ShowSystemDiagnostics(Process process)
      {
         Console.ForegroundColor = ConsoleColor.Cyan;
         Console.WriteLine("\n[System Diagnostics]");
         Console.ResetColor();
         Console.WriteLine($"  Working Set:     {FormatBytes(process.WorkingSet64)}");
         Console.WriteLine($"  Private Memory:  {FormatBytes(process.PrivateMemorySize64)}");
         Console.WriteLine($"  Virtual Memory:  {FormatBytes(process.VirtualMemorySize64)}");
         Console.WriteLine($"  Handle Count:    {process.HandleCount:N0}");
         Console.WriteLine($"  Thread Count:    {process.Threads.Count}");
         Console.WriteLine($"  Start Time:      {process.StartTime:yyyy-MM-dd HH:mm:ss}");
         Console.WriteLine($"  Total CPU Time:  {process.TotalProcessorTime}");
         Console.WriteLine($"  Main Module:     {GetMainModule(process)}");
         Console.WriteLine($"  Command Line:    {GetCommandLine(process)}");
      }

      internal static string GetMainModule(Process process)
      {
         try
         {
            return process.MainModule?.FileName ?? "Unknown";
         }
         catch
         {
            return "Access Denied";
         }
      }

      internal static string GetCommandLine(Process process)
      {
         try
         {
            var searcher = new ManagementObjectSearcher(
                $"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {process.Id}");
            var collection = searcher.Get();

            foreach (ManagementObject obj in collection)
            {
               return obj["CommandLine"]?.ToString() ?? "N/A";
            }
         }
         catch
         {
         }
         return "Unavailable";
      }

      internal static void ShowMemoryAnalysis(Process process)
      {
         Console.ForegroundColor = ConsoleColor.Magenta;
         Console.WriteLine("\n[Memory Analysis]");
         Console.ResetColor();
         try
         {
            // Memory working set details.
            Console.WriteLine($"  Working Set:     {FormatBytes(process.WorkingSet64)}");
            Console.WriteLine($"  Peak Working Set: {FormatBytes(process.PeakWorkingSet64)}");
            Console.WriteLine($"  Private Memory:  {FormatBytes(process.PrivateMemorySize64)}");
            Console.WriteLine($"  Virtual Memory:  {FormatBytes(process.VirtualMemorySize64)}");
            // Page file usage.
            Console.WriteLine($"  Paged Memory:    {FormatBytes(process.PagedMemorySize64)}");
            Console.WriteLine($"  Peak Paged:      {FormatBytes(process.PeakPagedMemorySize64)}");
            // GC information.
            var gcCount0 = GC.CollectionCount(0);
            var gcCount1 = GC.CollectionCount(1);
            var gcCount2 = GC.CollectionCount(2);
            Console.WriteLine($"  GC Collections:  Gen0:{gcCount0}, Gen1:{gcCount1}, Gen2:{gcCount2}");
            // Module memory usage.
            try
            {
               Console.WriteLine("\n[Top Memory Consuming Modules]:");
               var modules = process.Modules.Cast<ProcessModule>()
                   .OrderByDescending(m => m.ModuleMemorySize)
                   .Take(10);

               foreach (var module in modules)
               {
                  Console.WriteLine($"    {module.ModuleName} - {FormatBytes(module.ModuleMemorySize)}");
               }
            }
            catch
            {
               Console.WriteLine("    [Unable to enumerate modules - access denied]");
            }

            // Memory regions analysis. (if possible)
            AnalyzeMemoryRegions(process);
         }
         catch (Exception ex)
         {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ⚠️ Memory analysis error: {ex.Message}");
            Console.ResetColor();
         }
      }

      internal static void ShowThreadAnalysis(Process process)
      {
         Console.ForegroundColor = ConsoleColor.Magenta;
         Console.WriteLine("\n[Thread Analysis]");
         Console.ResetColor();
         try
         {
            Console.WriteLine($"  Total Threads: {process.Threads.Count}");

            // Thread states summary.
            var threadStates = new Dictionary<string, int>();
            foreach (ProcessThread thread in process.Threads)
            {
               string state = GetThreadState(thread);
               if (threadStates.ContainsKey(state))
                  threadStates[state]++;
               else
                  threadStates[state] = 1;
            }

            Console.WriteLine("  Thread States:");
            foreach (var state in threadStates.OrderBy(kvp => kvp.Key))
            {
               Console.WriteLine($"    {state.Key}: {state.Value}");
            }

            // Top CPU consuming threads.
            Console.WriteLine("\n  Top CPU Consuming Threads:");
            var sortedThreads = process.Threads.Cast<ProcessThread>()
                .OrderByDescending(t => t.TotalProcessorTime)
                .Take(5);

            foreach (var thread in sortedThreads)
            {
               Console.WriteLine($"    Thread ID: {thread.Id,4} | CPU Time: {thread.TotalProcessorTime}");
            }

            // Thread timing details.
            Console.WriteLine("\n  Thread Timing Details:");
            foreach (ProcessThread thread in process.Threads)
            {
               Console.WriteLine($"    Thread ID: {thread.Id,4} | State: {GetThreadState(thread)} | Priority: {thread.BasePriority}");
               Console.WriteLine($"      Start Time: {thread.StartTime:HH:mm:ss} | Total CPU: {thread.TotalProcessorTime}");
            }
         }
         catch (Exception ex)
         {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ⚠️ Thread analysis error: {ex.Message}");
            Console.ResetColor();
         }
      }

      internal static string GetThreadState(ProcessThread thread)
      {
         try
         {
            switch (thread.ThreadState)
            {
               case ThreadState.Running:
                  return "Running";
               case ThreadState.Wait:
                  return "Waiting";
               case ThreadState.Standby:
                  return "Standby";
               case ThreadState.Terminated:
                  return "Terminated";
               case ThreadState.Transition:
                  return "Transition";
               case ThreadState.Unknown:
                  return "Unknown";
               case ThreadState.Initialized:
                  return "Initialized";
               case ThreadState.Ready:
                  return "Ready";
               default:
                  return thread.ThreadState.ToString();
            }
         }
         catch
         {
            return "Unknown";
         }
      }

      internal static void ShowDiagnosticFeatures(Process process)
      {
         Console.ForegroundColor = ConsoleColor.Magenta;
         Console.WriteLine("\n[Diagnostic Features]");
         Console.ResetColor();
         try
         {
            // Assembly/Module inspection.
            ShowLoadedAssemblies(process);

            // Exception information. (if available)
            ShowExceptionInfo(process);

            // File handles.
            ShowFileHandles(process);

            // Environment variables.
            ShowEnvironmentVariables(process);

            // Performance counters.
            ShowPerformanceCounters(process);

            // Process permissions.
            ShowProcessPermissions(process);
         }
         catch (Exception ex)
         {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ⚠️ Diagnostic features error: {ex.Message}");
            Console.ResetColor();
         }
      }

      internal static void ShowProfilingFeatures(Process process)
      {
         Console.ForegroundColor = ConsoleColor.Magenta;
         Console.WriteLine("\n[Profiling Features]");
         Console.ResetColor();
         try
         {
            // CPU Profiling.
            ShowCPUProfiling(process);

            // Memory Profiling.
            ShowMemoryProfiling(process);

            // Timing Analysis.
            ShowTimingAnalysis(process);

            // GC Profiling.
            ShowGCProfiling(process);

            // Performance Timeline.
            ShowPerformanceTimeline(process);
         }
         catch (Exception ex)
         {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  ⚠️ Profiling features error: {ex.Message}");
            Console.ResetColor();
         }
      }

      internal static void ShowCPUProfiling(Process process)
      {
         Console.WriteLine("\n  [CPU Profiling]:");
         try
         {
            var startTime = process.StartTime;
            var totalCpuTime = process.TotalProcessorTime;
            var userTime = process.UserProcessorTime;
            var kernelTime = process.PrivilegedProcessorTime;

            Console.WriteLine($"    Total CPU Time: {totalCpuTime}");
            Console.WriteLine($"    User Time: {userTime}");
            Console.WriteLine($"    Kernel Time: {kernelTime}");

            // Calculate CPU usage percentage. (approximate)
            var uptime = DateTime.Now - startTime;
            if (uptime.TotalSeconds > 0)
            {
               var cpuPercent = (totalCpuTime.TotalSeconds / uptime.TotalSeconds) * 100;
               Console.WriteLine($"    Approx CPU %: {cpuPercent:F2}%");
            }

            // Thread CPU usage.
            Console.WriteLine("\n    Thread CPU Usage:");
            var threads = process.Threads.Cast<ProcessThread>()
                .OrderByDescending(t => t.TotalProcessorTime)
                .Take(5);

            foreach (var thread in threads)
            {
               Console.WriteLine($"      Thread {thread.Id}: {thread.TotalProcessorTime}");
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine($"    [CPU profiling unavailable: {ex.Message}]");
         }
      }

      internal static void ShowMemoryProfiling(Process process)
      {
         Console.WriteLine("\n  [Memory Profiling]:");
         try
         {
            var workingSet = process.WorkingSet64;
            var privateMemory = process.PrivateMemorySize64;
            var virtualMemory = process.VirtualMemorySize64;

            Console.WriteLine($"    Working Set: {FormatBytes(workingSet)}");
            Console.WriteLine($"    Private Memory: {FormatBytes(privateMemory)}");
            Console.WriteLine($"    Virtual Memory: {FormatBytes(virtualMemory)}");

            // Memory growth analysis.
            var peakWorkingSet = process.PeakWorkingSet64;
            var peakPagedMemory = process.PeakPagedMemorySize64;

            Console.WriteLine($"    Peak Working Set: {FormatBytes(peakWorkingSet)}");
            Console.WriteLine($"    Peak Paged Memory: {FormatBytes(peakPagedMemory)}");

            if (peakWorkingSet > 0)
            {
               var utilization = (double)workingSet / peakWorkingSet * 100;
               Console.WriteLine($"    Working Set Utilization: {utilization:F1}%");
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine($"    [Memory profiling unavailable: {ex.Message}]");
         }
      }

      internal static void ShowTimingAnalysis(Process process)
      {
         Console.WriteLine("\n  [Timing Analysis]:");
         try
         {
            var startTime = process.StartTime;
            var uptime = DateTime.Now - startTime;

            Console.WriteLine($"    Start Time: {startTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"    Uptime: {uptime}");
            Console.WriteLine($"    Total CPU Time: {process.TotalProcessorTime}");

            // Thread timing.
            Console.WriteLine("\n    Thread Timing:");
            var threads = process.Threads.Cast<ProcessThread>()
                .OrderByDescending(t => t.TotalProcessorTime)
                .Take(3);

            foreach (var thread in threads)
            {
               Console.WriteLine($"      Thread {thread.Id}: {thread.TotalProcessorTime}");
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine($"    [Timing analysis unavailable: {ex.Message}]");
         }
      }

      internal static void ShowGCProfiling(Process process)
      {
         Console.WriteLine("\n  [GC Profiling]:");
         try
         {
            var gen0Count = GC.CollectionCount(0);
            var gen1Count = GC.CollectionCount(1);
            var gen2Count = GC.CollectionCount(2);

            Console.WriteLine($"    Gen 0 Collections: {gen0Count}");
            Console.WriteLine($"    Gen 1 Collections: {gen1Count}");
            Console.WriteLine($"    Gen 2 Collections: {gen2Count}");

            // Note: These are for the current process, not the target process.
            Console.WriteLine($"    [Note: GC stats are for current process, not target process]");
         }
         catch (Exception ex)
         {
            Console.WriteLine($"    [GC profiling unavailable: {ex.Message}]");
         }
      }

      internal static void ShowPerformanceTimeline(Process process)
      {
         Console.WriteLine("\n  [Performance Timeline]:");
         try
         {
            // Create a simple timeline based on process metrics.
            Console.WriteLine($"    {DateTime.Now:HH:mm:ss} - Current Metrics:");
            Console.WriteLine($"      Memory: {FormatBytes(process.WorkingSet64)}");
            Console.WriteLine($"      CPU Time: {process.TotalProcessorTime}");
            Console.WriteLine($"      Threads: {process.Threads.Count}");
            Console.WriteLine($"      Handles: {process.HandleCount}");

            Console.WriteLine("\n    [Real-time monitoring requires continuous sampling]");
            Console.WriteLine("    Use external profilers for detailed timeline analysis");
         }
         catch (Exception ex)
         {
            Console.WriteLine($"    [Performance timeline unavailable: {ex.Message}]");
         }
      }

      internal static void ShowLoadedAssemblies(Process process)
      {
         Console.WriteLine("\n  [Loaded Assemblies/Modules]:");
         try
         {
            var modules = process.Modules.Cast<ProcessModule>().Take(20);
            int count = 0;
            foreach (var module in modules)
            {
               Console.WriteLine($"    {++count,2}. {Path.GetFileName(module.FileName)}");
               Console.WriteLine($"        Path: {module.FileName}");
               Console.WriteLine($"        Size: {FormatBytes(module.ModuleMemorySize)}");
            }

            if (process.Modules.Count > 20)
            {
               Console.WriteLine($"    ... and {process.Modules.Count - 20} more modules");
            }
         }
         catch
         {
            Console.WriteLine("    [Unable to enumerate modules - access denied]");
         }
      }

      internal static void ShowExceptionInfo(Process process)
      {
         Console.WriteLine("\n  [Exception Information]:");
         try
         {
            // Note: This is limited without debugging symbols.
            Console.WriteLine("    Exception information requires debugging symbols");
            Console.WriteLine("    Check Windows Event Log for application errors");
         }
         catch
         {
            Console.WriteLine("    [Unable to access exception information]");
         }
      }

      static void ShowFileHandles(Process process)
      {
         Console.WriteLine("\n  [File Handles]:");
         try
         {
            // This is limited in .NET - actual handle enumeration requires Win32 APIs.
            Console.WriteLine($"    Handle Count: {process.HandleCount}");
            Console.WriteLine("    Detailed handle enumeration requires advanced debugging APIs");
         }
         catch
         {
            Console.WriteLine("    [Unable to access handle information]");
         }
      }

      internal static void ShowEnvironmentVariables(Process process)
      {
         Console.WriteLine("\n  [Environment Variables]:");
         try
         {
            // For current process only - getting env vars of another process is complex.
            Console.WriteLine("    Environment variables of target process require advanced debugging");
            Console.WriteLine($"    Current Process Directory: {Environment.CurrentDirectory}");
            Console.WriteLine($"    Current Process Args: {string.Join(" ", Environment.GetCommandLineArgs())}");
         }
         catch
         {
            Console.WriteLine("    [Unable to access environment information]");
         }
      }

      internal static void ShowPerformanceCounters(Process process)
      {
         Console.WriteLine("\n  [Performance Counters]:");
         try
         {
            Console.WriteLine($"    User Time: {process.UserProcessorTime}");
            Console.WriteLine($"    Privileged Time: {process.PrivilegedProcessorTime}");
            Console.WriteLine($"    Handles: {process.HandleCount:N0}");
            Console.WriteLine($"    Threads: {process.Threads.Count:N0}");
         }
         catch (Exception ex)
         {
            Console.WriteLine($"    [Performance counters unavailable: {ex.Message}]");
         }
      }

      internal static void ShowProcessPermissions(Process process)
      {
         Console.WriteLine("\n  [Process Permissions]:");
         try
         {
            Console.WriteLine($"    Process ID: {process.Id}");
            Console.WriteLine($"    Session ID: {process.SessionId}");
            Console.WriteLine($"    Priority Class: {process.PriorityClass}");

            // Check if running as administrator..
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            Console.WriteLine($"    Running as Admin: {principal.IsInRole(WindowsBuiltInRole.Administrator)}");
         }
         catch (Exception ex)
         {
            Console.WriteLine($"    [Permission info unavailable: {ex.Message}]");
         }
      }

      internal static void AnalyzeMemoryRegions(Process process)
      {
         Console.WriteLine("\n[Memory Regions Analysis]:");
         try
         {
            Console.WriteLine("  Memory regions analysis requires advanced debugging APIs");
            Console.WriteLine($"  Memory layout available: {Environment.Is64BitProcess}");

            // Show memory architecture info.
            Console.WriteLine($"  Process Architecture: {GetProcessArchitecture(process)}");
            Console.WriteLine($"  Is 64-bit Process: {Is64BitProcess(process)}");
         }
         catch
         {
            Console.WriteLine("  [Memory region analysis unavailable]");
         }
      }

      internal static string GetProcessArchitecture(Process process)
      {
         try
         {
            if (Environment.Is64BitOperatingSystem)
            {
               bool isWow64;
               IsWow64Process(process.Handle, out isWow64);
               return isWow64 ? "x86 (32-bit)" : "x64 (64-bit)";
            }
            return "x86 (32-bit)";
         }
         catch
         {
            return "Unknown";
         }
      }

      internal static bool Is64BitProcess(Process process)
      {
         if (!Environment.Is64BitOperatingSystem)
            return false;

         try
         {
            bool isWow64;
            IsWow64Process(process.Handle, out isWow64);
            return !isWow64;
         }
         catch
         {
            return false;
         }
      }

      internal static bool IsDotNetRuntime(string moduleName)
      {
         return moduleName.Equals("mscoree.dll", StringComparison.OrdinalIgnoreCase) ||
                moduleName.Equals("clr.dll", StringComparison.OrdinalIgnoreCase) ||
                moduleName.Equals("coreclr.dll", StringComparison.OrdinalIgnoreCase);
      }

      internal static void CreateMiniDump(int pid)
      {
         Console.Write("Dump type - [n]ormal or [f]ull memory? (default: normal): ");
         string type = Console.ReadLine()?.Trim().ToLower() ?? "n";
         bool fullMemory = type.StartsWith('f');

         string fileName = "dumps" + Path.DirectorySeparatorChar + $"dump_{pid}_{DateTime.Now:yyyyMMdd_HHmmss}.dmp";
         Console.WriteLine($"Creating dump: {fileName} ...");

         try
         {
            Directory.CreateDirectory("dumps");
            using var process = Process.GetProcessById(pid);
            using var file = File.Create(fileName);
            uint dumpType = fullMemory ? 2u : 0u; // MiniDumpWithFullMemory = 2

            bool success = MiniDumpWriteDump(
                process.Handle,
                (uint)pid,
                file.SafeFileHandle,
                dumpType,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero
            );

            if (success)
            {
               Console.ForegroundColor = ConsoleColor.Green;
               Console.WriteLine($"✅ Dump saved: {Path.GetFullPath(fileName)}");
               Console.ResetColor();
            }
            else
            {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine("❌ Failed to create dump (try running as Administrator)");
               Console.ResetColor();
            }
         }
         catch (Exception ex)
         {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Error: {ex.Message}");
            Console.ResetColor();
         }
      }

      internal static string FormatBytes(long bytes)
      {
         string[] units = { "B", "KB", "MB", "GB" };
         int i = 0;
         double size = bytes;
         while (size >= 1024 && i < units.Length - 1)
         {
            size /= 1024;
            i++;
         }
         return $"{size:F1} {units[i]}";
      }

      // P/Invoke for minidump.
      [DllImport("dbghelp.dll", SetLastError = true)]
      static extern bool MiniDumpWriteDump(
          IntPtr hProcess,
          uint ProcessId,
          SafeHandle hFile,
          uint DumpType,
          IntPtr ExceptionParam,
          IntPtr UserStreamParam,
          IntPtr CallbackParam);

      [DllImport("kernel32.dll", SetLastError = true)]
      static extern bool IsWow64Process(IntPtr hProcess, out bool wow64Process);
   }
}
