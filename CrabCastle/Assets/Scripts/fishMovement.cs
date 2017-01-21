using UnityEngine;
using System.Collections;

public class fishMovement : MonoBehaviour 
{

	public GameObject WaypointManager;

	public float fishSpeed = 10.0f;

	public GameObject nextWaypoint;
	GameObject[] pathWaypoints;
	int wpIndex;

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
		print (pathWaypoints[0]);
		MoveToWaypoint (nextWaypoint.transform);
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
		transform.position = Vector3.MoveTowards(transform.position, nextWP.position, fishSpeed * Time.deltaTime);
	}
}
