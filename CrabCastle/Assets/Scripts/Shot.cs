using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
	public Transform target = null;
	[SerializeField] private float speed = 10f;
	[SerializeField] private float collisionDist = 0.25f;

	// Update is called once per frame
	void Update () {
		if (target != null) {
			Vector3 move = (target.position - transform.position).normalized * speed * Time.deltaTime;
			transform.position = transform.position + move;
			Debug.Log (target.position);

			if ((target.position - transform.position).magnitude <= collisionDist) {
				Debug.Log ("IM HIT");
				Destroy (this.gameObject);
			}
		}
	}
}
