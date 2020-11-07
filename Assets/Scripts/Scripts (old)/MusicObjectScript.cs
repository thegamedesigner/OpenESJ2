using UnityEngine;

public class MusicObjectScript : MonoBehaviour
{
	AudioSource music;
	float[] spectrum = new float[128];

	void Start()
	{
		music = this.gameObject.GetComponent<AudioSource>();

		if (!xa.beenToLevel0)
		{
			GetComponent<AudioSource>().Stop();
		}
	}


	void Update()
	{
		GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		xa.beat_Freq = spectrum[1];
		xa.music_Time = music.time;
		xa.music_Spectrum = spectrum;

		int index = 0;
		while (index < spectrum.Length)
		{
			Debug.DrawLine(new Vector3(2 + transform.position.x + (index * 0.1f), transform.position.y, transform.position.z), new Vector3(2 + transform.position.x + (index * 0.1f), transform.position.y + (spectrum[index] * 15), transform.position.z), Color.white);
			index++;
		}

		if (music.volume != xa.musicVolume) { music.volume = xa.musicVolume; }
	}
}
