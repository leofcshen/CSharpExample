namespace Universe.Services;

public static class SV_NetworkDriver {
  private static readonly WshNetwork _wshNetwork = new();

  private static void MapDrive(string driveLetter, string UNC, bool isPersistent, string userName, string password) {
    if (driveLetter != "") {
      DisconnectDrive(driveLetter, true, true);
    } else {
      DisconnectDrive(UNC, true, true);
    }
    object persistent = isPersistent;
    object user = userName;
    object pwd = password;
    _wshNetwork.MapNetworkDrive(driveLetter, UNC, ref persistent, ref user, ref pwd);
  }

  private static void DisconnectDrive(string UNC_or_DriveName, bool willForce, bool isPersistent) {
    try {
      object force = willForce;
      object updateProfile = isPersistent;
      _wshNetwork.RemoveNetworkDrive(UNC_or_DriveName, ref force, ref updateProfile);
    } catch (Exception ex) {
      Console.WriteLine(ex.Message);
      Console.WriteLine(ex.StackTrace);
    }
  }

  /// <summary>
  /// 掛載網路磁碟機
  /// <example>
  ///   範例代碼
  ///   <code>
  ///     SV_NetworkDriver.Mount(@"\\192.168.1.109\­個人資料夾", "z", "username", "password");
  ///   </code>
  /// </example>
  /// </summary>
  /// <param name="source"></param>
  /// <param name="driverLabel">磁碟機代號</param>
  /// <param name="user"></param>
  /// <param name="password"></param>
  public static void Mount(String source, string driverLabel, string user, string password) =>
    MapDrive(driverLabel + ":", source, false, user, password);

  /// <summary>
  /// 取消掛載網路磁碟機
  /// </summary>
  /// <param name="driveLable"></param>
  public static void Unmount(string driveLable) =>
    DisconnectDrive(driveLable + ":", true, true);

  public static char GetFreeLabel() {
    char letter = ' ';
    // Allocate space for alphabet
    ArrayList driveLetters = new(26);
    // increment from ASCII values for A-Z
    for (int i = 65; i < 91; i++) {
      // Add uppercase letters to possible drive letters
      driveLetters.Add(Convert.ToChar(i));
    }

    // removed used drive letters from possible drive letters
    foreach (string drive in Directory.GetLogicalDrives()) {
      driveLetters.Remove(drive[0]);
    }

    foreach (char drive in driveLetters) {
      //comboDrives.Items.Add(drive); // add unused drive letters to the combo box
      letter = drive;
      break;
    }

    return letter;
  }
}
