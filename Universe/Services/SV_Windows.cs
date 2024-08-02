using System.Runtime.InteropServices;

namespace Universe.Services;

public static partial class SV_Windows {
	[ComImport]
	[Guid("00021401-0000-0000-C000-000000000046")]
	internal class ShellLink;

	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("000214F9-0000-0000-C000-000000000046")]
	internal interface IShellLink {
		void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
		void GetIDList(out IntPtr ppidl);
		void SetIDList(IntPtr pidl);
		void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder pszName, int cchMaxName);
		void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
		void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder pszDir, int cchMaxPath);
		void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
		void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder pszArgs, int cchMaxPath);
		void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
		void GetHotkey(out short pwHotkey);
		void SetHotkey(short wHotkey);
		void GetShowCmd(out int piShowCmd);
		void SetShowCmd(int iShowCmd);
		void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder pszIconPath, int cchIconPath, out int piIcon);
		void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
		void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
		void Resolve(IntPtr hwnd, int fFlags);
		void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
	}

	[ComImport]
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IPersist {
		void GetClassID(out Guid pClassID);
	}

	[ComImport]
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IPersistFile : IPersist {
		new void GetClassID(out Guid pClassID);
		[PreserveSig]
		int IsDirty();
		void Load([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, uint dwMode);
		void Save([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [In, MarshalAs(UnmanagedType.Bool)] bool fRemember);
		void SaveCompleted([In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName);
		void GetCurFile([In, MarshalAs(UnmanagedType.LPWStr)] string ppszFileName);
	}

	[ComImport]
	[Guid("45E2B4AE-B1C3-11D0-B92F-00A0C90312E1")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IShellLinkDataList {
		void AddDataBlock(IntPtr pDataBlock);
		void CopyDataBlock(uint dwSig, out IntPtr ppDataBlock);
		void RemoveDataBlock(uint dwSig);
		void GetFlags(out uint pdwFlags);
		void SetFlags(uint dwFlags);
	}

	public static void Run_產生編輯hosts捷徑到桌面() {
		string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Edit-hosts.lnk");

		if (File.Exists(shortcutPath)) {
			throw new IOException($"捷徑已存在_{shortcutPath}");
		}

		IShellLink link = (IShellLink)new ShellLink();

		link.SetDescription("Edit hosts");
		link.SetPath(@"C:\Windows\System32\notepad.exe");
		link.SetArguments(@"C:\Windows\System32\drivers\etc\hosts");
		link.SetWorkingDirectory(@"C:\Windows\System32");

		// 設定以管理員開啟
		IShellLinkDataList datalist = (IShellLinkDataList)link;
		const uint SLDF_RUNAS_USER = 0x2000;
		datalist.GetFlags(out uint flags);
		flags |= SLDF_RUNAS_USER;
		datalist.SetFlags(flags);

		// Save the shortcut
		IPersistFile file = (IPersistFile)link;
		file.Save(shortcutPath, false);
	}
}
