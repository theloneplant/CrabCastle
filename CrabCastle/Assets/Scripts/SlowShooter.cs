using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlowShooter : MonoBehaviour {
	[SerializeField] private GameObject shot;
	[SerializeField] private GameObject turret;
	[SerializeField] private Vector3 shotOffset;
	[SerializeField] private float range = 10f;
	[SerializeField] private float shotInterval = 1f;
	[SerializeField] private float spinRate = 500f;

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
		// Only shoot the closest enemy
		if (fishList.Length > 0) {
			currentTarget = null;
			distToTarget = float.PositiveInfinity;
			foreach(Transform fish in fishList) {
				if (fish != null) {
					float dist = (fish.position - transform.position).magnitude;
					FishManager fm = fish.GetComponent<FishManager>();
					if (fm != null && distToTarget > dist && fm.isAlive() && !fm.isSlowed()) {
						currentTarget = fish;
						distToTarget = dist;
					}
				}
			}
		}
	}

	private void HandleShoot() {
		if (currentTarget != null && Time.time - timeFromLastShot > shotInterval) {
			timeFromLastShot = Time.time;

			GameObject newShot = Instantiate(shot);
			newShot.transform.position = transform.position + shotOffset;
			Shot shotComp = newShot.GetComponent<Shot> ();
			if (shotComp != null)
				shotComp.target = currentTarget;
			SplashShot splashShotComp = newShot.GetComponent<SplashShot> ();
			if (splashShotComp != null)
				splashShotComp.target = currentTarget;
			SlowShot slowShotComp = newShot.GetComponent<SlowShot> ();
			if (slowShotComp != null)
				slowShotComp.target = currentTarget;
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
			Destroy (rotateObj);
		}
		else {
			float step = spinRate * Time.deltaTime;
			turret.transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.identity, step);
		}
	}

	public void GetFishList() {
		// Gets the fish in the current wave, ignoring the first element which is always the parent element
		fishList = GameObject.FindGameObjectsWithTag("FishGroup")[0].GetComponentsInChildren<Transform>().Skip(1).ToArray();
	}
}
