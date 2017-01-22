using UnityEngine;
using System.Collections;

public class scaleTower : MonoBehaviour 
{

	public float towerScaleRate = 2.0f;

	// Use this for initialization
	void Start () 
	{
		transform.localScale = new Vector3(1.0f, 0.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.localScale.y < 1.0f)
		{
			Vector3 finalScale = new Vector3 (1, 1, 1);
			transform.localScale = Vector3.Lerp (transform.localScale, finalScale, towerScaleRate * Time.deltaTime);
		}
	}
}
