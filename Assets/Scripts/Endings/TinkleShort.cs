using UnityEngine;
using System.Collections;

/// <summary>
/// Component of TinkleShort.unity scenes "Main Camera" gameobject
/// </summary>
public class TinkleShort : MonoBehaviour
{
	public GameObject nguiToiletTime;
	public GameObject nguiRetry;
	
	private bool showingPassOut = true;
	
	void Start()
	{
		NGUITools.SetActive(nguiRetry, false);
	}
	
	void Update()
	{
		if(showingPassOut && Input.GetMouseButtonDown(0))
		{
			showingPassOut = false;
			NGUITools.SetActive(nguiToiletTime, false);
			NGUITools.SetActive(nguiRetry, true);
		}
	}
	
	public void OnClickRetry()
	{
		Globals.IsFirstRun = false;
		Application.LoadLevel(0);
	}
}