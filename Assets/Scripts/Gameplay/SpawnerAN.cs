using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAN : MonoBehaviour
{
	public int Probabilities;
	public GameObject Obj;
	public float off;
	public void Spawn() {
		int rand = Random.Range(0,Probabilities);
		if (rand < 45) {
			Instantiate(Obj,this.transform.position+new Vector3(Random.Range(-off,off),Random.Range(-off,off),Random.Range(-off,off)),Quaternion.identity);
		}
		if (rand < 25) {
			Instantiate(Obj,this.transform.position+new Vector3(Random.Range(-off,off),Random.Range(-off,off),Random.Range(-off,off)),Quaternion.identity);
		}
		if (rand < 15) {
			Instantiate(Obj,this.transform.position+new Vector3(Random.Range(-off,off),Random.Range(-off,off),Random.Range(-off,off)),Quaternion.identity);
		}
		if (rand < 5) {
			Instantiate(Obj,this.transform.position+new Vector3(Random.Range(-off,off),Random.Range(-off,off),Random.Range(-off,off)),Quaternion.identity);
		}
	}
}
