using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    public AudioClip OpenDoorCLP;
	public AudioClip CloseDoorCLP;
	public AudioClip[] Steps;
	public Animator Char;
	bool w;
	void Start() {
		Global.Current_Camera = Camera.main;
		
	}
	void Skip() {
		
			Debug.Log("Skipped Door");
			GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>().PlayAudio2D(CloseDoorCLP);
			Leave();
		
	}
	void Update() {
		if (Global.n_controls.Map.wasPressedThisFrame) {
		Skip();
		}
	}
	void OpenDoor() {
		GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>().PlayAudio2D(OpenDoorCLP);
	}
	void Step() {
		GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>().PlayAudio2D(Steps[Random.Range(0,Steps.Length)]);
	}
	void ToggleWalk() {}
	void CloseDoor() {
		GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>().PlayAudio2D(CloseDoorCLP);
		Invoke("Leave",1f);
	}
	void Leave() {
		Global.GoingBack = true;
		SceneManager.LoadScene(Global.gotoscene);
	}
}
