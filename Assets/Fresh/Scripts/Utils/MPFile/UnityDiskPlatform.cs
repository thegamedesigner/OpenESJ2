using System;
using System.IO;

/// <summary>
/// Class for writing to the normal filesystem from within Unity
/// </summary>
public class UnityDiskPlatform : IFilePlatform
{
	public virtual void Copy(string src, string dest, bool overwrite)
	{
		if (Directory.Exists(dest)) {
			if (Directory.Exists(src)) {
				// TODO: CopyDirectory
				return;
			} else {
				dest = Path.Combine(dest, Path.GetFileName(src));
			}
		}
		File.Copy(src, dest, overwrite);
	}

	public virtual void Delete(string path)
	{
		File.Delete(path);
	}

	public virtual bool Exists(string path)
	{
		return File.Exists(path);
	}

	public virtual FileAttributes GetAttributes(string path)
	{
		return File.GetAttributes(path);
	}

	public virtual DateTime GetCreationTimeUtc(string path)
	{
		return File.GetCreationTimeUtc(path);
	}

	public virtual DateTime GetLastAccessTimeUtc(string path)
	{
		return File.GetLastAccessTimeUtc(path);
	}

	public virtual DateTime GetLastWriteTimeUtc(string path)
	{
		return File.GetLastWriteTimeUtc(path);
	}

	public virtual void Move(string src, string dest)
	{
		if (Directory.Exists(dest)) {
			if (Directory.Exists(src)) {
				Directory.Move(src, dest);
				return;
			} else {
				dest = Path.Combine(dest, Path.GetFileName(src));
			}
		}
		File.Move(src, dest);
	}
	
	public virtual byte[] ReadAllBytes(string path)
	{
		return File.ReadAllBytes(path);
	}
	
	public virtual void SetAttributes(string path, FileAttributes fileAttributes) {
		File.SetAttributes(path, fileAttributes);
	}
	
	public virtual void WriteAllBytes(string path, byte[] bytes)
	{
		File.WriteAllBytes(path, bytes);
	}

	// Cloud Stuff
	public virtual bool Forget(string path)
	{
		return true;
	}
	
	public virtual bool Persists(string path)
	{
		return false;
	}
	
	public virtual void Share(string path, System.Action<int> result)
	{
		if (result != null) {
			result.Invoke(-1);
		}
	}

	public virtual bool IsCloudEnabledForPlatform()
	{
		return false;
	}
	
	public virtual bool IsCloudEnabledForApp()
	{
		return false;
	}
	
	public virtual bool IsCloudEnabledForUser()
	{
		return false;
	}
}
