using UnityEngine;
using System;
using System.Collections;

public class FishManager : MonoBehaviour 
{
	public GameObject WaypointManager;

	public float health = 10f;
	public float speed = 10f;
	public int loot = 5;
	public float sinkDepth = -1f;
	public float sinkDuration = 2f;

	public GameObject nextWaypoint;
	GameObject[] pathWaypoints;
	int wpIndex;

	public float currentSpeed = 10f;
	private bool alive = true;
	private float sinkStartTime;
	private Vector3 sinkStartPos;
	private Vector3 sinkEndPos;

	private bool slowed = false;
	private float slowMult = 1f;
	private float slowDuration = 0f;
	private float startSlow = 0f;

	void Start() 
	{
		WaypointManager = GameObject.FindGameObjectWithTag ("WaypointManager");
		Waypoints wp = WaypointManager.GetComponent<Waypoints> () as Waypoints;
		pathWaypoints = wp.waypointArray;
		nextWaypoint = pathWaypoints[1];
		wpIndex = 1;
	}

	void Update ()
	{
		if (slowed)
			currentSpeed = speed * slowMult;
		
		try
		{
			print (pathWaypoints[0]);
			MoveToWaypoint (nextWaypoint.transform);
		}
		catch (Exception e)
		{
			//Debug.Log ("Waypoints not set up properly");
		}

		if (health <= 0)
		{
			currentSpeed = 0;
			StartCoroutine(Die());
		}

		if (slowed && Time.time - startSlow >= slowDuration)
		{
			slowed = false;
			currentSpeed = speed;
			gameObject.GetComponentInChildren<ParticleSystem>().Stop();
		}

		if (!alive) {
			float t = (Time.time - sinkStartTime) / sinkDuration;
			gameObject.transform.position = Vector3.Lerp (sinkStartPos, sinkEndPos, t);
			Debug.Log (sinkStartPos + " " + sinkEndPos + " " + sinkStartTime);
		}
	}

	void OnTriggerEnter(Collider otherObj)
	{
		if (otherObj.tag == "Waypoint") 
		{
			if (pathWaypoints != null && otherObj.gameObject != pathWaypoints [0]) 
			{
				wpIndex++;
				if (pathWaypoints != null && wpIndex < pathWaypoints.Length)
					nextWaypoint = pathWaypoints [wpIndex];
				else
					Destroy (gameObject);
			}
		}
	}

	void MoveToWaypoint(Transform nextWP)
	{
		transform.LookAt (nextWaypoint.transform);
		transform.position = Vector3.MoveTowards(transform.position, nextWP.position, currentSpeed * Time.deltaTime);
	}

	public void Slow(float slowMult, float slowDuration)
	{
		slowed = true;
		this.slowMult = slowMult;
		this.slowDuration = slowDuration;
		startSlow = Time.time;
		gameObject.GetComponentInChildren<ParticleSystem>().Play();
	}

	public bool isSlowed() {
		return slowed;
	}

	public bool isAlive()
	{
		return alive;
	}

	IEnumerator Die() 
	{
		if (alive)
		{
			alive = false;
			sinkStartTime = Time.time;
			sinkStartPos = transform.position;
			sinkEndPos = new Vector3 (sinkStartPos.x, sinkStartPos.y + sinkDepth, sinkStartPos.z);
			PlayerManager pm = GameObject.FindGameObjectsWithTag("PlayerManager")[0].GetComponent<PlayerManager>();
			pm.cashMoney += loot;
			yield return new WaitForSeconds (2);
			Destroy (gameObject);
		}
	}
}
