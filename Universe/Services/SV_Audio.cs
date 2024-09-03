namespace Universe.Services;

public static class SV_Audio {
  private static readonly MMDeviceEnumerator _enumerator = new(Guid.NewGuid());

  /// <summary>
  /// 取得預設輸出裝置名稱
  /// </summary>
  /// <returns></returns>
  public static string Get預設輸出裝置名稱()
    => _enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console).DeviceFriendlyName;

  /// <summary>
  /// 用輸出裝置名稱查輸出裝置 ID
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public static string Get輸出裝置ID(string name) {
    MMDeviceCollection devices = _enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
    var device = devices.FirstOrDefault(x => x.DeviceFriendlyName == name) ??
      throw new Exception($"找不到名稱是 {name} 的輸出裝置");
    return device.ID;
  }

  /// <summary>
  /// 設定預設輸出裝置為 ID 的裝置
  /// </summary>
  /// <param name="id"></param>
  public static void Run設定預設輸出裝置(string id) {
    MMDevice device = _enumerator.GetDevice(id);
    _enumerator.SetDefaultAudioEndpoint(device);
  }
}
