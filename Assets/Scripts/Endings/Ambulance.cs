using UnityEngine;
using System.Collections;

/// <summary>
/// Component of Ambulance.unity scenes "Main Camera" gameobject
/// </summary>
public class Ambulance : MonoBehaviour
{
	public GameObject nguiPassOut;
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
			NGUITools.SetActive(nguiPassOut, false);
			NGUITools.SetActive(nguiRetry, true);
		}
	}
	
	public void OnClickRetry()
	{
		Globals.IsFirstRun = false;
		Application.LoadLevel(0);
	}
}
