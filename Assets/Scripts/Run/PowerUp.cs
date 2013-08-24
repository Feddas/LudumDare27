using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	private float SpinSpeed = 1;
	float y0;
	float amplitude = .2f;
	Transform cachedTransform;
	
	void Start()
	{
		cachedTransform = this.transform;
		y0 = cachedTransform.position.y;
	}
	
	// Update is called once per frame
	void Update()
	{
		//bounce up and down
		float newY = y0 + amplitude * Mathf.Sin(SpinSpeed * Time.time);
		Movement.UpdatePostionY(cachedTransform, newY);
		
		//spin
		cachedTransform.RotateAroundLocal(Vector3.up, SpinSpeed * Time.deltaTime);
	}
}
