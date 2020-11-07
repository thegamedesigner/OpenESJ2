using UnityEngine;

public class ButtFormat
{
	// Metadata
	public int version        = 0;
	public int buttVersion    = 0;
	public int bgBanding      = 0;
	public Color bgTop        = Color.cyan;
	public Color bgBottom     = Color.blue;
	public string contentHash = null;
	public string name        = null;
	public string author      = null;

	public void ParseButt(string data)
	{
	}

	public void ParseBinaryButt(byte[] data)
	{
	}

	public void ParsePngButt(byte[] data)
	{
		// search for the png's IEND token (49 45 4E 44 AE 42 60 82)
		int offset = 0;
		byte[] token = new byte[] { 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82 };

		// read from the index immediately after the token ends until the end of the file.
		byte[] level = data.ShallowCopyRange<byte>(offset);
	}
}
