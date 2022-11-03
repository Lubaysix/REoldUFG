using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public float WeaponShootDistance;
	public int id;
	public Transform Cannon;
    public void Shoot() {
		if (Global.Item_DB.Items[id].Particles.Count > 0) {
			for (int p = 0; p < Global.Item_DB.Items[id].Particles.Count; p++) {
				Instantiate(Global.Item_DB.Items[id].Particles[p],Cannon.position,Cannon.rotation);
			}
		}
		RaycastHit hit;
		if (Physics.Raycast(Cannon.position,Cannon.forward,out hit,WeaponShootDistance)) {
			if (hit.collider.tag == "Enemy") {
				hit.collider.GetComponent<BasicEnemy>().Hurt(Global.Item_DB.Items[id].Damage);
			}
		}
	}
}
