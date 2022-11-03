using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
	public string self_name;
	public float HP;
	public int damage;
	public GameObject BloodFX;
	public Transform pr;
	public Vector3 pos,ang;
	public Animator anim;
	public List<string> HurtAnimations;
	public bool animation;
	void Update() {
		if (HP <= 0) {
			Global.DeletedObjects.Add(new Save_Deleted(this.gameObject));
			int i = Global.Enemies.FindIndex(x => x.self_name == self_name);
			Global.Enemies.RemoveAt(i);
			Destroy(this.gameObject);
		}
		self_name = this.gameObject.name;
		pos = this.transform.position;
		ang = this.transform.eulerAngles;
	}
	public void Hurt(float damage) {
		if (animation) {
			if (Random.Range(0,15) > 11) {
			anim.Play(HurtAnimations[Random.Range(0,HurtAnimations.Count)]);
			}
		}
		int a = Random.Range(1,5);
		for (int i = 0; i < a; i++) {
				Instantiate(BloodFX,pr.position+new Vector3(Random.Range(-0.2f,0.2f),Random.Range(-0.2f,0.2f),Random.Range(-0.2f,0.2f)),Quaternion.identity);
		}
		HP -= damage;
	}
    void OnTriggerEnter(Collider col) {
		if (col.tag == "Bullet" && GameObject.Find("Char").GetComponent<MainMovement>().Attacking) {
			Hurt(col.gameObject.GetComponent<WeaponO>().Damage);
			if (col.gameObject.GetComponent<WeaponO>().ID == 0) {
			GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>().PlayWPAudio(col.gameObject.GetComponent<WeaponO>().ID,this.transform.position);
			}else {
			GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>().PlayWPAudio(col.gameObject.GetComponent<WeaponO>().ID,col.transform.position);	
			}
		}
	}
}
