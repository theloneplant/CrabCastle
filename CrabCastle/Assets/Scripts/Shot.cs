using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
	public Transform target = null;
	[SerializeField] private GameObject hit;
	[SerializeField] private float speed = 10f;
	[SerializeField] private float collisionDist = 0.25f;
	[SerializeField] private float damage = 3.5f;

	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 move = (target.position - transform.position).normalized * speed * Time.deltaTime;
			transform.position = transform.position + move;
			Debug.Log (target.position);

			if ((target.position - transform.position).magnitude <= collisionDist) {
				FishManager fm = target.GetComponent<FishManager> ();
				fm.health -= damage;
				GameObject newHit = Instantiate (hit);
				newHit.transform.position = target.transform.position;
				Destroy (this.gameObject);
			}
		}
	}
}
