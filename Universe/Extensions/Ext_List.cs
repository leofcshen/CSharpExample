namespace Universe.Extensions;

public static class Ext_List {
  public static void RemoveAfter<T>(this List<T> list, int index, bool isThrow = false) {
    if (index < 0 || index >= list.Count) {
      if (isThrow) {
        throw new ArgumentOutOfRangeException(nameof(index), "索引超出範圍");
      }
      return;
    }
    list.RemoveRange(index, list.Count - index);
  }
}

public static class SV_Windwos1 {
  public static List<int> Find佔用Port的ProcessID(int port = -1) {
    bool is不指定Port = port == -1;
    //cmd => netstat -a -n -o
    var psi = new ProcessStartInfo {
      FileName = "netstat",
      Arguments = "-a -n -o", // 顯示所有連接、數值地址及進程ID
      RedirectStandardOutput = true, // 將輸出重定向
      UseShellExecute = false, // 不使用操作系統 shell 來執行
      CreateNoWindow = true // 不顯示命令提示符窗口
    };

    // 啟動 Process 並讀取結果
    using (var process = Process.Start(psi)) {
      using (var reader = process.StandardOutput) {
        string result = reader.ReadToEnd(); // 讀取所有輸出

        //if (is不指定Port) {
        //  Console.WriteLine(result); // 輸出到控制台
        //  return;
        //}

        string targetPort = port.ToString();
        string[] lines = result.Split(Environment.NewLine); // 按行分割結果

        List<int> list = new();
        foreach (var line in lines) {
          // 檢查是否包含目標端口號
          if (line.Contains($":{targetPort}")) {
            var tokens = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // PID 通常是最後一個 token
            if (tokens.Length > 4) {
              string processId = tokens[tokens.Length - 1];
              if (int.TryParse(processId, out int pid)) {
                list.Add(pid);
                Console.WriteLine($"端口 {targetPort} 被進程 {pid} 占用");
              }
            }
          }
        }
        return list;
      }
    }
  }
}
