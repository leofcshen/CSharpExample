namespace Universe.Services;

//public partial class SV_Windows {
//  [STAThread]
//  public static void Run_擷圖螢幕範圍(Rectangle rectangle) {
//    Bitmap bitmap = new(rectangle.Width, rectangle.Height);

//    using (Graphics g = Graphics.FromImage(bitmap)) {
//      g.CopyFromScreen(rectangle.Location, Point.Empty, rectangle.Size);
//    }

//    BitmapSource bitmapSource = ConvertBitmapToBitmapSource(bitmap);
//    Clipboard.SetImage(bitmapSource);
//  }

//  /// <summary>將 Bitmap 轉換為 BitmapSource</summary>
//  private static BitmapSource ConvertBitmapToBitmapSource(Bitmap bitmap) {
//    using MemoryStream memoryStream = new MemoryStream();
//    bitmap.Save(memoryStream, ImageFormat.Png);
//    memoryStream.Seek(0, SeekOrigin.Begin);

//    BitmapImage bitmapImage = new BitmapImage();
//    bitmapImage.BeginInit();
//    bitmapImage.StreamSource = memoryStream;
//    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
//    bitmapImage.EndInit();
//    // 需要凍結來確保可以跨執行緒使用
//    bitmapImage.Freeze();

//    return bitmapImage;
//  }
//}
