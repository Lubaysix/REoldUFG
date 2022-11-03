using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEBullet : MonoBehaviour
{
	public float Damage;
	public WilliamController controller;
	public AudioClip AttackSound;
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.name == "Char") {
			GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>().PlayAudio3D(AttackSound,this.transform.position);
			col.gameObject.GetComponent<MainMovement>().GetHurt(1,Damage);
			
		}
	}
}
