using UnityEngine;
using System.Collections;

/// <summary>
/// Component of Win.unity scenes "Main Camera" gameobject
/// </summary>
public class Win : MonoBehaviour
{
	public GameObject nguiToiletTime;
	public GameObject nguiWin;
	
	private bool showingPassOut = true;
	
	void Start()
	{
		NGUITools.SetActive(nguiWin, false);
	}
	
	void Update()
	{
		if(showingPassOut && Input.GetMouseButtonDown(0))
		{
			showingPassOut = false;
			NGUITools.SetActive(nguiToiletTime, false);
			NGUITools.SetActive(nguiWin, true);
		}
	}
}