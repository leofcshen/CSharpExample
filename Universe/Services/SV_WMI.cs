namespace Universe.Services;

public static class SV_WMI {
  public const string Query開機時間 = "SELECT LastBootUpTime FROM Win32_OperatingSystem";
}
