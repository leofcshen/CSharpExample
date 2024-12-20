﻿using System.Diagnostics;
using Universe.Extensions;
using Universe.Services;

namespace ConsoleApp_Net;

internal class Program {
  static void Main(/*string[] args*/) {
    try {
      //SV_Windows.SV_Shortcut.Create_編輯環境變數();
      //SV_NetworkDriver.Mount(@"\\192.168.200.215\專案區\006.檔案交換區", "F", "A11084", "6084");
      SV_NetworkDriver.Mount(@"\\192.168.200.215\專案區", "a", "A11084", "6084");
      SV_NetworkDriver.Unmount("Z");
      return;
      var iterations = 1000000000;

      // 方法一：nameof
      var stopwatch = Stopwatch.StartNew();
      for (int i = 0; i < iterations; i++) {
        var a = "abc哦".GetLength中文算二();
      }
      stopwatch.Stop();
      Console.WriteLine($"{"A".PadLeft(10)}: {stopwatch.ElapsedMilliseconds} ms");

      // 方法一：nameof
      stopwatch.Restart();
      for (int i = 0; i < iterations; i++) {
        var b = "abc哦".GetLength中文算二_1();
      }
      stopwatch.Stop();
      Console.WriteLine($"{"A".PadLeft(10)}: {stopwatch.ElapsedMilliseconds} ms");

      stopwatch.Restart();
      for (int i = 0; i < iterations; i++) {
        var b = "abc哦".GetLength中文算二_2();
      }
      stopwatch.Stop();
      Console.WriteLine($"{"A".PadLeft(10)}: {stopwatch.ElapsedMilliseconds} ms");

      //SV_Windows.Run清空資源回收筒();
    } catch (Exception ex) {
      Console.WriteLine(ex.Message);
    }
  }
}
