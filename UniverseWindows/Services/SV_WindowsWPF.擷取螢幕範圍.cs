namespace UniverseWindows.Services;

public partial class SV_WindowsWPF {
  [STAThread]
  public static BitmapImage Run_擷圖螢幕範圍到剪貼簿(int x, int y, int width, int height)
    => Run_擷圖螢幕範圍到剪貼簿(new Rectangle(x, y, width, height));

  [STAThread]
  public static BitmapImage Run_擷圖螢幕範圍到剪貼簿(Rectangle rectangle) {
    Bitmap bitmap = new(rectangle.Width, rectangle.Height);
    using Graphics graphics = Graphics.FromImage(bitmap);
    graphics.CopyFromScreen(rectangle.Location, System.Drawing.Point.Empty, rectangle.Size);
    BitmapImage bitmapImage = ConvertBitmapToBitmapImage(bitmap);
    Clipboard.SetImage(bitmapImage);

    return bitmapImage;
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
