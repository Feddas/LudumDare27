using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	public float Responsiveness = 0.3f; //time in seconds before the player can make another move.
	
	bool inMiddleOfRoad;
	RoadSide currentPosition = new RoadSide(RoadSide.MiddleOfRoad);
	float distance, //z distance traveled
		deltaTimeResponded = 0;
	Transform cachedTransform;
	
	void Start()
	{
		cachedTransform = this.transform;
		distance = cachedTransform.position.z;
	}
	
	void Update()
	{
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