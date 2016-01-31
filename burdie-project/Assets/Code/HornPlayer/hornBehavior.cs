using UnityEngine;
using System.Collections;

public class hornBehavior : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		animator.speed = 0.25f;
	}
}
