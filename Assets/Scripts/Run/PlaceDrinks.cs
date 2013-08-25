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
	public UILabel Timer;
	//public IList<GameObject> drinks = new List<GameObject>();
	
	void Start()
	{
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
			//drinks.Add(drink);
		}
	}
	
//	private void removeAllDrinks()
//	{
//		if (drinks == null || drinks.Count < 1)
//		{
//			drinks = new List<GameObject>();
//			return;
//		}
//		
//		foreach (var drink in drinks)
//		{
//			if (drink != null)
//				Destroy(drink);
//		}
//		drinks = new List<GameObject>();
//	}
//	
//	void reset()
//	{
//		Player.transform.position = new Vector3(0f, 0.6f, 0f);
//		removeAllDrinks();
//		setupDrinks();
//		NGUITools.SetActive(nguiStationArrival, false);
//		Time.timeScale = 1;
//	}
	
	private void TryATinkle(int minutesWaiting)
	{
		float theValue = Globals.BladderAmmo * (minutesWaiting / 60);
		Debug.Log("result: " + theValue + " BladderAmmo:" + Globals.BladderAmmo);
		
		//if (theValue > 80) //ambulance
		//Application.LoadLevel(1);
		
		//if (theValue < 58) //fail audio
		
		
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
		NGUITools.SetActive(nguiStationArrival, false);
		NGUITools.SetActive(nguiStationWait, true);
		//Application.LoadLevel(1);
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