using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAP : MonoBehaviour
{
	AudioSource src;
	public AudioClip[] WeaponsClips;
	void Start() {
		src = this.GetComponent<AudioSource>();
	}
	public void PlayWPAudio(int ID,Vector3 Position) {
		this.transform.position = Position;
		src.clip = WeaponsClips[ID];
		src.Play();
	} 
	public void PlayAudio2D(AudioClip clip){
		this.transform.position = Global.Current_Camera.transform.position;
		src.clip = clip;
		src.Play();
	}
	public void PlayAudio(AudioClip clip){
		
		src.clip = clip;
		src.Play();
	}
	public void PlayAudio3D(AudioClip clip,Vector3 Position){
		this.transform.position = Position;
		src.clip = clip;
		src.Play();
	}
}
