namespace UniverseWindows.Services;

public partial class SV_Windows {
  [STAThread]
  public static void Run_擷圖螢幕範圍(int x1, int y1, int x2, int y2) {
    Rectangle rectangle = new(x1, y1, x2, y2);
    Run_擷圖螢幕範圍(rectangle);
  }

  [STAThread]
  public static void Run_擷圖螢幕範圍(Rectangle rectangle) {
    Bitmap bitmap = new(rectangle.Width, rectangle.Height);
    using Graphics g = Graphics.FromImage(bitmap);
    g.CopyFromScreen(rectangle.Location, System.Drawing.Point.Empty, rectangle.Size);
    BitmapSource bitmapSource = ConvertBitmapToBitmapImage(bitmap);
    Clipboard.SetImage(bitmapSource);
  }

  /// <summary>將 Bitmap 轉換為 BitmapImage</summary>
  private static BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap) {
    using MemoryStream memoryStream = new();
    bitmap.Save(memoryStream, ImageFormat.Png);
    memoryStream.Seek(0, SeekOrigin.Begin);
    BitmapImage bitmapImage = new();
    bitmapImage.BeginInit();
    bitmapImage.StreamSource = memoryStream;
    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
    bitmapImage.EndInit();
    // 需要凍結來確保可以跨執行緒使用
    bitmapImage.Freeze();

    return bitmapImage;
  }
}
