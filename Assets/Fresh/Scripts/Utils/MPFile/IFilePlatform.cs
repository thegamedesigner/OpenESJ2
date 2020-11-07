using System;
using System.IO;
using System.Text;

public interface IFilePlatform
{
	// Common System.IO.File interface
	void Copy(string sourceFileName, string destFileName, bool overwrite);
	void Delete(string path);
	bool Exists(string path);
	FileAttributes GetAttributes(string path);
	DateTime GetCreationTimeUtc(string path);
	DateTime GetLastAccessTimeUtc(string path);
	DateTime GetLastWriteTimeUtc(string path);
	void Move(string sourceFileName, string destFileName);
	byte[] ReadAllBytes(string path);
	void SetAttributes(string path, FileAttributes fileAttributes);
	void WriteAllBytes(string path, byte[] bytes);

	// Cloud Stuff
	bool Forget(string path);
	bool Persists(string path);
	void Share(string path, System.Action<int> result);
	bool IsCloudEnabledForApp();
	bool IsCloudEnabledForUser();
	bool IsCloudEnabledForPlatform();
}

public interface IFilePlatformExtended : IFilePlatform
{
	void AppendAllText(string path, string contents, Encoding encoding);
	StreamWriter AppendText(string path);
	FileStream Create(string path, int bufferSize);
	StreamWriter CreateText(string path);
	//void Decrypt(string path);
	//void Encrypt(string path);
	DateTime GetCreationTime(string path);
	DateTime GetLastAccessTime(string path);
	DateTime GetLastWriteTime(string path);
	FileStream Open(string path, FileMode mode, FileAccess access, FileShare share);
	FileStream OpenRead(string path);
	StreamReader OpenText(string path);
	FileStream OpenWrite(string path);
	string[] ReadAllLines(string path, Encoding encoding);
	string ReadAllText(string path, Encoding encoding);
	void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);
	void SetCreationTime(string path, DateTime creationTime);
	void SetLastAccessTime(string path, DateTime lastAccessTime);
	void SetLastWriteTime(string path, DateTime lastWriteTime);
	void SetCreationTimeUtc(string path, DateTime creationTimeUtc);
	void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);
	void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);
	void WriteAllLines(string path, string[] contents, Encoding encoding);
	void WriteAllText(string path, string contents, Encoding encoding);
}
