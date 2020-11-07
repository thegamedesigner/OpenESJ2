using System;
using System.IO;

public class MPFile
{
	public enum DataPath
	{
		Data = 0,
		PersistentData,
		TempData,
		StreamingAssets
	}

	[Flags]
	public enum EncodeSettings
	{
		EncodeDisable = 0,
		EncodeFileName = 1 << 0,
		EncodeFilePath = 1 << 1
	}

	public readonly string[] DATA_PATHS = {
		UnityEngine.Application.dataPath,
		UnityEngine.Application.persistentDataPath,
		UnityEngine.Application.temporaryCachePath,
		UnityEngine.Application.streamingAssetsPath
	};

	private IFilePlatform platform        = null;
	private EncodeSettings encodeSettings = EncodeSettings.EncodeDisable;
	private string rootPath               = string.Empty;

	public MPFile(IFilePlatform platform, DataPath rootPath, EncodeSettings encodeSettings = EncodeSettings.EncodeDisable)
	{
		this.platform       = platform;
		this.encodeSettings = encodeSettings;
		this.SetRootPath(rootPath);
	}

	public void SetRootPath(DataPath rootPath)
	{
		this.rootPath = DATA_PATHS[(int)rootPath];
	}

	private string RootPath(string path)
	{
		if (!Path.IsPathRooted(path)) {
			path = Path.Combine(this.rootPath, path);
		}
		return path;
	}

	public void Copy(string src, string dest, bool overwrite)
	{
		src  = this.RootPath(src);
		dest = this.RootPath(dest);
		this.platform.Copy(src, dest, overwrite);
	}

	public void Delete(string path)
	{
		this.platform.Delete(this.RootPath(path));
	}

	public bool Exists(string path)
	{
		return this.platform.Exists(this.RootPath(path));
	}

	public FileAttributes GetAttributes(string path)
	{
		return this.platform.GetAttributes(this.RootPath(path));
	}

	public DateTime GetCreationTimeUtc(string path)
	{
		return this.platform.GetCreationTimeUtc(this.RootPath(path));
	}

	public DateTime GetLastAccessTimeUtc(string path)
	{
		return this.platform.GetLastAccessTimeUtc(this.RootPath(path));
	}

	public DateTime GetLastWritetimeUtc(string path)
	{
		return this.platform.GetLastWriteTimeUtc(this.RootPath(path));
	}

	public void Move(string src, string dest)
	{
		src  = this.RootPath(src);
		dest = this.RootPath(dest);
		this.platform.Move(src, dest);
	}

	public byte[] ReadAllBytes(string path)
	{
		return this.platform.ReadAllBytes(this.RootPath(path));
	}

	public void SetAttributes(string path, FileAttributes fileAttributes)
	{
		this.platform.SetAttributes(this.RootPath(path), fileAttributes);
	}

	public void WriteAllBytes(string path, byte[] bytes)
	{
		this.platform.WriteAllBytes(this.RootPath(path), bytes);
	}

	public bool Forget(string path)
	{
		return this.platform.Forget(this.RootPath(path));
	}

	public bool Persists(string path)
	{
		return this.platform.Persists(this.RootPath(path));
	}

	public void Share(string path, System.Action<int> result)
	{
		this.platform.Share(this.RootPath(path), result);
	}

	public bool IsCloudEnabledForPlatform()
	{
		return this.platform.IsCloudEnabledForPlatform();
	}

	public bool IsCloudEnabledForApp() {
		return this.platform.IsCloudEnabledForApp();
	}

	public bool IsCloudEnabledForUser()
	{
		return this.platform.IsCloudEnabledForUser();
	}
}
