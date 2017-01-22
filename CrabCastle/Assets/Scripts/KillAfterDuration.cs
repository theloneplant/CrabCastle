using UnityEngine;
using System.Collections;

public class KillAfterDuration : MonoBehaviour {
	private ParticleSystem ps;
	private float startTime;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime >= ps.duration)
			Destroy(gameObject);
	}
}
