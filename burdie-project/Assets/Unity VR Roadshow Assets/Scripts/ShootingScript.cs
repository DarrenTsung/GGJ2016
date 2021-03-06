﻿//This script allows us to shoot at the enemies in the game

using UnityEngine;

public class ShootingScript : MonoBehaviour
{
	public ParticleSystem impactEffect;	//Particle effect for visual feedback of shot

	AudioSource gunFireAudio;			//Sound clip for audio feedback of shot
	RaycastHit rayHit;					//What line did we shoot down

	void Start()
	{
		//Get a reference to the audio
		gunFireAudio = GetComponent<AudioSource>();
	}

	void Update()
	{
		//If we "fire"...
		if (Input.GetButtonDown("Fire1"))
		{
			//...play our audio...
			gunFireAudio.Stop();
			gunFireAudio.Play();

			//...and create a ray
			if (Physics.Raycast(transform.position, transform.forward, out rayHit, 100f))
			{
				//If the ray hits something (didn't shoot the sky), move the impact effect to that
				//location and play it
				impactEffect.transform.position = rayHit.point;
				impactEffect.transform.rotation = Quaternion.Euler(270, 0, 0);
				impactEffect.Stop();
				impactEffect.Play();

				//If we hit an enemy Destroy it
				if (rayHit.transform.tag == "Enemy")
					Destroy(rayHit.transform.gameObject);
			}
		}
	}
}
