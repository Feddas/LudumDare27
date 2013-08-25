using UnityEngine;
using System.Collections;

/// <summary>
/// Component of Road.unity scenes Player gameobject
/// </summary>
public class Movement : MonoBehaviour
{
	public GameObject nguiStationArrival;
	public UISlider bladderSlider;
	public float Responsiveness = 0.3f; //time in seconds before the player can make another move.
	public AudioClip Water;
	public AudioClip Soda;
	public AudioClip Coffee;
	
	bool inMiddleOfRoad, firstUpdate = true;
	RoadSide currentPosition = new RoadSide(RoadSide.MiddleOfRoad);
	float distance, //z distance traveled
		deltaTimeResponded = 0,
		bladderMax;
	Transform cachedTransform;
//	
//	void Awake()
//	{
//	}
	
	void Start()
	{
		bladderMax = bladderSlider.foreground.localScale.x;
		cachedTransform = this.transform;
		distance = cachedTransform.position.z;
		Globals.BladderAmmo = 0;
	}
	
	void Update()
	{
		if (firstUpdate)
		{
			updateSlider(0f);
			firstUpdate = false;
		}
		distance += Time.deltaTime * 10;
		UpdatePostionZ(cachedTransform, distance);
		
		//determine how often the player can move.
		if (deltaTimeResponded < Responsiveness)
		{
			deltaTimeResponded += Time.deltaTime;
			return;
		}
		deltaTimeResponded = 0;
		
		//move player
		if (Input.GetAxis("Horizontal") > 0 //right
			&& currentPosition < RoadSide.SideRight)
		{
			currentPosition++;
			UpdatePostionX(cachedTransform, currentPosition * 10);
		}
		else if (Input.GetAxis("Horizontal") < 0 //left
			&& currentPosition > RoadSide.SideLeft)
		{
			currentPosition--;
			UpdatePostionX(cachedTransform, currentPosition * 10);
		}
	}
	
	private void OnCollisionEnter(Collision hitInfo)
	{
		if (hitInfo.gameObject.tag == "Building")
		{
			Debug.Log(hitInfo.gameObject.tag);
			Time.timeScale = 0;
			NGUITools.SetActive(nguiStationArrival, true);
		}
		else if (hitInfo.gameObject.tag == "Drink")
		{
			Drink drank = hitInfo.gameObject.GetComponent<PowerUp>().DrinkType;
			if (drank == Drink.Unknown)
				return;
			
			Globals.BladderAmmo += (int)drank;
			Destroy(hitInfo.gameObject);
			
			updateSlider(Globals.BladderAmmo/100f);
			//Debug.Log("drank " + drank.ToString() + ". now at " + bladderAmmo);
			
			if (drank == Drink.Water || drank == Drink.Beer)
				audio.clip = Water;
			else if (drank == Drink.Soda)
				audio.clip = Soda;
			else if (drank == Drink.Coffee)
				audio.clip = Coffee;
			audio.Play();
		}
	}
	
	private void updateSlider(float percent)
	{
		bladderSlider.foreground.localScale = new Vector3(
			bladderMax * percent,
			bladderSlider.foreground.localScale.y,
			bladderSlider.foreground.localScale.z);
	}
	
	public static void UpdatePostionX(Transform trans, float newX)
	{
		Vector3 newVector = new Vector3(newX, trans.position.y, trans.position.z);
		trans.position = newVector;
	}
	
	public static void UpdatePostionY(Transform trans, float newY)
	{
		Vector3 newVector = new Vector3(trans.position.x, newY, trans.position.z);
		trans.position = newVector;
	}
	
	public static void UpdatePostionZ(Transform trans, float newZ)
	{
		Vector3 newVector = new Vector3(trans.position.x, trans.position.y, newZ);
		trans.position = newVector;
	}
}