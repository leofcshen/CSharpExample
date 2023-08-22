# 使用說明

本專案為 C# 使用範例

## ConsoleAppDemo
此項目請調整 Program.cs 的下列列舉值執行對應的 Demo
```csharp
/// <summary> 要執行的動作 </summary>
private static readonly EnumDemo Action = EnumDemo.None;
```
## Polyglot-Notebooks

此項目請用 Visual Studio Code 搭配套件 Polyglot Notebooks 使用
如果有使用到 MyLibrary.dll 的方法，請於開啟時先執行下列程式碼區塊

```CSharp
//引用 MyLibrary
#r "Library/MyLibrary.dll"
using MyLibrary.Extensions;
```
