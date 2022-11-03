using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
	public Animator anim;
	public float minForce,maxForce;
	float force;
	void Start() {
		force = Random.Range(minForce,maxForce);
		this.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-force,force),Random.Range(-force,force),Random.Range(-force,force));
	}
    void OnTriggerEnter(Collider col) {
		if (col.tag == "Solid") {
			Debug.LogWarning("Collided with" + col.gameObject.name);
			anim.Play("Splatter");
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
			this.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
		}
	}
}
