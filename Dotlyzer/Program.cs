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
   using Dotlyzer.Libraries;

   internal class Program
   {
      private static void Main(string[] args)
      {
         Console.Clear();

         while (true)
         {
            Console.ResetColor();
            Console.WriteLine($"{Constants.NAME} {Constants.VERSION}\n");
            Console.WriteLine("Menu:");
            Console.WriteLine("  1. List Processes");
            Console.WriteLine("  2. Analyze Process");
            Console.WriteLine("  3. About");
            Console.WriteLine("  4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
               Analyze.ListAllProcesses();
            }
            else if (choice == "2")
            {
               Analyze.InspectProcessMenu();
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
         Console.WriteLine($"Version: {Constants.VERSION}");
         Console.WriteLine($"Description: {Constants.DESCRIPTION}");
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
   }
}
