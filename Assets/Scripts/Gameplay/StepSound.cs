using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour
{
	public AudioClip[] Steps;
	public AudioClip[] HurtSounds1;
	public AudioSource source;
	public void PlayStep() {
		source.clip = Steps[Random.Range(0,Steps.Length)];
		source.Play();
	}
	public void PlayHurt() {
		source.clip = HurtSounds1[Random.Range(0,HurtSounds1.Length)];
		source.Play();
	}
}
