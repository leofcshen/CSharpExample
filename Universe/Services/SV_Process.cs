namespace Universe.Services;

public class SV_Process {
  public static void ExecuteCommand(string command) {
    ProcessStartInfo processStartInfo = new("cmd", "/c " + command) {
      RedirectStandardOutput = true,
      UseShellExecute = false,
      CreateNoWindow = true
    };

    using Process process = new() { StartInfo = processStartInfo };
    ExcuteProcess(process);
  }

  public static void ExecuteProcessStartInfo(ProcessStartInfo processStartInfo) {
    using Process process = new() { StartInfo = processStartInfo };
    ExcuteProcess(process);
  }

  public static void ExcuteProcess(Process process) {
    process.Start();
    process.WaitForExit();
  }
}
