using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Component of Road.unity scenes Street gameobject
/// Handles general game initilization
/// </summary>
public class PlaceDrinks : MonoBehaviour
{
	public GameObject nguiDialogStart;
	public GameObject nguiStationArrival;
	public GameObject nguiStationWait;
	public GameObject DrinkPrefab;
	public GameObject Player;
	public UILabel WaitChoice;
	public UILabel Timer;
	
	void Start()
	{
		if (nguiStationArrival == null)
		{
			return;
		}
		
		if (Globals.IsFirstRun)
		{
			Time.timeScale = 0; //wait for "Go" click
		}
		else
		{
			NGUITools.SetActive(nguiDialogStart, false);
			Time.timeScale = 1;
		}
		
		NGUITools.SetActive(nguiStationArrival, false);
		NGUITools.SetActive(nguiStationWait, false);
		setupDrinks();
	}
	
	void Update()
	{
		if (Timer == null)
			return;
		
		if (Time.timeSinceLevelLoad < 1.5)
			Timer.text = "10.00";
		else if (Time.timeSinceLevelLoad > 11.5)
			Timer.text = "00.00";
		else
			Timer.text = (11.5 - Time.timeSinceLevelLoad).ToString("00.00");
	}
	
	private void setupDrinks()
	{
		for (int i = 2; i < 12; i++)
		{
			Vector3 drinkPos = DrinkPrefab.transform.position;
			drinkPos.z = i * 10;
			drinkPos.x = UnityEngine.Random.Range(-1, 2) * 10;
			DrinkPrefab.transform.position = drinkPos;
			var drink = Instantiate(DrinkPrefab) as GameObject;
		}
	}
	
	private void TryATinkle(int minutesWaiting)
	{	
		switch (getGameResult(minutesWaiting, Globals.BladderAmmo))
		{
		case GameResult.Ambulance:
			Debug.Log("Ambulance");
			Application.LoadLevel(1);
			break;
		case GameResult.TinkleShort:
			Debug.Log("TinkleShort");
			Application.LoadLevel(2);
			break;
		case GameResult.Win:
			Debug.Log("Win");
			Application.LoadLevel(3);
			break;
		default:
			break;
		}
	}
	
	private GameResult getGameResult(int minutesWaiting, int bladderAmmo)
	{
		switch (minutesWaiting)
		{
		case 30:
			return getWaitTypeResult(bladderAmmo, 63, 69);
		case 60:
			return getWaitTypeResult(bladderAmmo, 56, 63);
		case 90:
			return getWaitTypeResult(bladderAmmo, 50, 56);
		default:
			return GameResult.Unknown;
		}
	}
	
	private GameResult getWaitTypeResult(int bladderAmmo, int minPoints, int maxPoints)
	{
		if (bladderAmmo < minPoints)
			return GameResult.TinkleShort;
		else if (bladderAmmo > maxPoints)
			return GameResult.Ambulance;
		else
			return GameResult.Win;
	}
	
	#region Button Events
	public void OnClickGo()
	{
		Time.timeScale = 1;
		NGUITools.SetActive(nguiDialogStart, false);
	}
	
	public void OnClickRetry()
	{
		Globals.IsFirstRun = false;
		Application.LoadLevel(0);
	}
	
	public void OnClickGoIn()
	{
		WaitChoice.text = string.Format(
			"You'd say you drank ohhh... {0}% of what you could hold. Once inside, you're asked, \"Given how much you've drank. How long do you need to let it go through your system so you pee for exactly 10 seconds?\"",
			Globals.BladderAmmo);
		NGUITools.SetActive(nguiStationArrival, false);
		NGUITools.SetActive(nguiStationWait, true);
	}
	
	public void OnClick30()
	{
		TryATinkle(30);
	}
	
	public void OnClick60()
	{
		TryATinkle(60);
	}
	
	public void OnClick90()
	{
		TryATinkle(90);
	}
	#endregion Button Events
}

enum GameResult 
{
	Unknown,
	Win,
	TinkleShort,
	Ambulance,
}