using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplashShot : MonoBehaviour {
	public Transform target = null;
	[SerializeField] private GameObject hit;
	[SerializeField] private float speed = 10f;
	[SerializeField] private float collisionDist = 0.25f;
	[SerializeField] private float splashRadius = 3.5f;
	[SerializeField] private float damage = 1.5f;

	private Transform[] fishList = null;

	// Update is called once per frame
	void Update () {
		GetFishList();
		if (target != null) {
			Vector3 move = (target.position - transform.position).normalized * speed * Time.deltaTime;
			transform.position = transform.position + move;
			Debug.Log (target.position);

			if ((target.position - transform.position).magnitude <= collisionDist) {
				foreach(Transform fish in fishList) {
					float dist = (fish.position - transform.position).magnitude;
					if (dist < splashRadius) {
						FishManager fm = fish.GetComponent<FishManager> ();
						if (fm != null)
							fm.health -= damage;
					}
				}
				GameObject newHit = Instantiate (hit);
				newHit.transform.position = target.transform.position;
				Destroy (this.gameObject);
			}
		}
	}

	public void GetFishList() {
		// Gets the fish in the current wave, ignoring the first element which is always the parent element
		fishList = GameObject.FindGameObjectsWithTag("FishGroup")[0].GetComponentsInChildren<Transform>().Skip(1).ToArray();
	}
}
