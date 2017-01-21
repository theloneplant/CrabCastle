using System.Collections;
using System.Linq;
using UnityEngine;

public class StandardShooter : MonoBehaviour {
	[SerializeField] private GameObject fishWaveParent;
	[SerializeField] private GameObject shot;
	[SerializeField] private GameObject turret;
	[SerializeField] private Vector3 shotOffset;
	[SerializeField] private float range = 5f;
	[SerializeField] private float shotInterval = 0.5f;
	[SerializeField] private float spinRate = 1f;

	private Transform[] fishList = null;
	private Transform 	currentTarget = null;
	private float distToTarget = float.PositiveInfinity;
	private float timeFromLastShot;

	// Use this for initialization
	void Start () {
		GetFishList();
		timeFromLastShot = -shotInterval;
	}

	// Update is called once per frame
	void Update () {
		TargetFind ();
		HandleShoot ();
		SpinTurret ();
	}

	private void TargetFind() {
		// Check if target is alive, then update distance and check if it's in range
		if (currentTarget != null) {
			distToTarget = (currentTarget.position - transform.position).magnitude;

			if (distToTarget > range) { // Target out of range, reset target to null & reset dist
				currentTarget = null;
				distToTarget = float.PositiveInfinity;
			}
		}
		else { // Target died, reset distance
			distToTarget = float.PositiveInfinity;
		}

		// Determine new target if we don't have one
		if (currentTarget == null && fishList.Length > 0) {
			currentTarget = fishList[0];
			foreach(Transform fish in fishList) {
				float dist = (fish.position - transform.position).magnitude;
				if (distToTarget > dist) {
					currentTarget = fish;
					distToTarget = dist;
				}
			}

			// Couldn't find anything in range, don't shoot
			if (distToTarget > range) {
				currentTarget = null;
				distToTarget = float.PositiveInfinity;
			}
			else
				Debug.Log ("new target!!!");
		}
	}

	private void HandleShoot() {
		if (currentTarget != null && Time.time - timeFromLastShot > shotInterval) {
			timeFromLastShot = Time.time;

			GameObject newShot = Instantiate(shot);
			newShot.transform.position = transform.position + shotOffset;
			newShot.GetComponent<Shot>().target = currentTarget;
		}
	}

	private void SpinTurret() {
		if (currentTarget != null) {
			float step = spinRate * Time.deltaTime;
			Vector3 targetLoc = currentTarget.position;
			targetLoc.y = transform.position.y;
			GameObject rotateObj = new GameObject ();
			rotateObj.transform.position = turret.transform.position;
			rotateObj.transform.LookAt (targetLoc);
			turret.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateObj.transform.rotation, step);
		}
	}

	public void GetFishList() {
		// Gets the fish in the current wave, ignoring the first element which is always the parent element
		fishList = fishWaveParent.GetComponentsInChildren<Transform>().Skip(1).ToArray();
	}
}
