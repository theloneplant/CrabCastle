using UnityEngine;
using System.Collections;

public class waterWakeMove : MonoBehaviour 
{
	public float wakeSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(transform.forward * wakeSpeed * Time.deltaTime);
	}
}
