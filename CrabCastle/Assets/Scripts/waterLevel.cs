using UnityEngine;
using System.Collections;

public class waterLevel : MonoBehaviour 
{
	public float maxWaterLevelSpeed = 0.20f;

	public float waterPingSpeed = 10.0f;

	public float waterLevelSpeed = 0.020f;

	void Start () 
	{
	}
	
	void Update () 
	{
		PingWater ();
	}

	void PingWater()
	{
		//waterLevelSpeed = Mathf.PingPong (Time.time, 1.0f);
		transform.Translate (transform.up * waterLevelSpeed * Time.deltaTime);
	}
}
