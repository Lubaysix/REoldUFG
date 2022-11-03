using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysicsParticle : MonoBehaviour
{
	Rigidbody body;
	public float minRot,maxRot;
	public float minForce,maxForce;
	public float bounces;
	public float bounceForce;
	public Vector3 offset;
	public Transform rota;
	float cur_bounce;
	float force;
	float rot;
    // Start is called before the first frame update
    void Start()
    {
		this.transform.position+= offset;
        body = this.GetComponent<Rigidbody>();
		rot = Random.Range(minRot,maxRot);
		force = Random.Range(minForce,maxForce);
		body.velocity = new Vector3(Random.Range(-force,force),Random.Range(-force,force),Random.Range(-force,force));
	}

    // Update is called once per frame
    void Update()
    {
		if (cur_bounce < bounces) {
        rota.eulerAngles += new Vector3(0f,0f,rot*Time.deltaTime);
		}
    }
	void dest() {
		Destroy(this.gameObject);
	}
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Solid") {
			if (cur_bounce < bounces) {
				cur_bounce++;
				body.velocity = new Vector3(body.velocity.x,bounceForce/cur_bounce,body.velocity.z);
				this.GetComponent<AudioSource>().Play();
			}else {
				body.constraints = RigidbodyConstraints.FreezePosition;
				body.velocity = new Vector3(0f,0f,0f);
				Invoke("dest",1f);
			}
		}
	}
}
