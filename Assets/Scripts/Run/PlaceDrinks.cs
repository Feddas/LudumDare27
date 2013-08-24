using UnityEngine;
using System.Collections;

public class PlaceDrinks : MonoBehaviour
{
	public GameObject DrinkPrefab;
	
	void Start()
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
	
	void Update()
	{
	}
}
