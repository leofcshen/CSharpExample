namespace Universe.Services;

public static class SV_OS {
  public static DateTime Get開機時間_WMI查詢() {
    ManagementObjectSearcher searcher = new(SV_WMI.Query開機時間);
    ManagementObject os = searcher.Get().Cast<ManagementObject>().FirstOrDefault()!;
    DateTime bootTime = ManagementDateTimeConverter.ToDateTime(os["LastBootUpTime"].ToString());

    return bootTime;
  }

  public static DateTime Get開機時間_計算經過時間() =>
    DateTime.Now.AddMilliseconds(-Environment.TickCount64);
}
