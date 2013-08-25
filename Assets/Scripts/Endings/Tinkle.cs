using UnityEngine;
using System.Collections;

/// <summary>
/// Component of TinkleShort.unity scenes "Main Camera" gameobject
/// Component of Win.unity scenes "Main Camera" gameobject
/// </summary>
public class Tinkle : MonoBehaviour
{
	public GameObject nguiHud;
	public GameObject nguiStory;
	public GameObject nguiClose;
	public UILabel Timer;
	public float TimeToTinkle = 10;
	public AudioClip Applause;
	
	private bool showingPassOut = true;
	private bool stillTinkling = true;
	private float timeStart;
	
	void Start()
	{
		NGUITools.SetActive(nguiHud, false);
		NGUITools.SetActive(nguiClose, false);
		if (TimeToTinkle != 10)
		{
			float bladderAmmo = Globals.BladderAmmo;
			TimeToTinkle = Random.Range(bladderAmmo, bladderAmmo + 30f)/10f;
		}
	}
	
	void Update()
	{
		if (showingPassOut && Input.GetMouseButtonDown(0))
		{
			showingPassOut = false;
			NGUITools.SetActive(nguiStory, false);
			NGUITools.SetActive(nguiHud, true);
			timeStart = Time.timeSinceLevelLoad;
			Time.timeScale = 1;
			audio.Play();
		}
		else if (showingPassOut == false)
		{
			float timeTinkling = Time.timeSinceLevelLoad - timeStart;;
			
			if (timeTinkling < TimeToTinkle)
			{
				Timer.text = timeTinkling.ToString("00.00");
			}
			else if (timeTinkling > TimeToTinkle + 1f)
			{
				//Time.timeScale = 0;
				NGUITools.SetActive(nguiHud, false);
				NGUITools.SetActive(nguiClose, true);
			}
			else if (timeTinkling > TimeToTinkle && stillTinkling)
			{
				stillTinkling = false;
				Timer.text = TimeToTinkle.ToString("00.00");
				audio.Stop();
				if (Applause != null)
				{
					audio.clip = Applause;
					audio.Play();
				}
			}
		}
	}
	
	public void OnClickRetry()
	{
			Debug.Log(TimeToTinkle.ToString() + "retry start");
		Globals.IsFirstRun = false;
		Application.LoadLevel(0);
			Debug.Log(TimeToTinkle.ToString() + "retry end");
	}
}