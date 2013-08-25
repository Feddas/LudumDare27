using UnityEngine;
using System.Collections;

/// <summary>
/// Component of the prefabs Drink gameobject
/// </summary>
public class PowerUp : MonoBehaviour {
	public Drink DrinkType = Drink.Unknown;
	public Material Beer;
	public Material Coffee;
	public Material Soda;
	public Material Water;
	private float SpinSpeed = 1;
	
	float y0;
	float amplitude = .2f;
	Transform cachedTransform;
	UnityEngine.Renderer cachedRenderer;
	
	void Start()
	{
		cachedTransform = this.transform;
		cachedRenderer = this.renderer;
		
		y0 = cachedTransform.position.y;
		
		if (DrinkType == Drink.Unknown)
		{
			DrinkType = (Drink)Random.Range(7, 11);
		}
		cachedRenderer.material = getDrinkMaterial(DrinkType);
	}
	
	// Update is called once per frame
	void Update()
	{
		//bounce up and down
		float newY = y0 + amplitude * Mathf.Sin(SpinSpeed * Time.timeSinceLevelLoad);
		Movement.UpdatePostionY(cachedTransform, newY);
		
		//spin
		cachedTransform.RotateAroundLocal(Vector3.up, SpinSpeed * Time.deltaTime);
	}
	
	private Material getDrinkMaterial(Drink drinkType)
	{
		switch (drinkType)
		{
		case Drink.Beer:
			return Beer;
		case Drink.Coffee:
			return Coffee;
		case Drink.Soda:
			return Soda;
		case Drink.Water:
		case Drink.Unknown:
		default:
			return Water;
		}
	}
}

/// <summary>
/// Drink types with values corresponding to how much they makes you pee
/// Scientific research: http://answers.yahoo.com/question/index?qid=20090123173756AA1TtJF
/// </summary>
public enum Drink
{
	Unknown = 0,
	Water = 7,
	Beer = 8,
	Coffee = 9,
	Soda = 10,
}