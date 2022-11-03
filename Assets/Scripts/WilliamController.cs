using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamController : MonoBehaviour
{
	Transform mainCharacter;
	public Animator anim;
	public float Distance;
	public float WalkSpeed;
	public float RotSpeed,RotSpeed2,transition_speed;
	public GameObject Empty;
	public bool Attacking;
	
	public float Secs;
	float cur_spd,rot;
	GameObject emt;
	float timer;
    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.Find("Char").transform;
		emt = Instantiate(Empty,this.transform.position,Quaternion.identity);
		emt.transform.SetParent(this.transform);
		rot = this.transform.eulerAngles.y;
		emt.transform.eulerAngles = new Vector3(0f,this.transform.eulerAngles.y,0f);
    }
	void UnAtk() {
		Attacking = false;
	}
	
    // Update is called once per frame
    void Update()
    {
		
		float dist = Vector3.Distance(this.transform.position,mainCharacter.position);
		this.transform.Translate(0f,0f,cur_spd*Time.deltaTime);
		this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,Mathf.LerpAngle(this.transform.eulerAngles.y,rot,RotSpeed*Time.deltaTime),this.transform.eulerAngles.z);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt_Minor_01") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt_Minor_02") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt_Big")) {
		if (dist < Distance && dist > 3f) {
				
			emt.transform.LookAt(mainCharacter);
			float a = emt.transform.eulerAngles.y;
			rot = Mathf.LerpAngle(rot,a,RotSpeed2*Time.deltaTime);
			cur_spd = Mathf.Lerp(cur_spd,WalkSpeed,15f*Time.deltaTime);
			if (!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
			anim.CrossFadeInFixedTime("Walk",transition_speed,0);
			}
				
		}else if (dist <= 3f) {
			emt.transform.LookAt(mainCharacter);
			float a = emt.transform.eulerAngles.y;
			rot = Mathf.LerpAngle(rot,a,RotSpeed2*Time.deltaTime);
			timer += Time.deltaTime;
			if (timer >= Secs) {
				timer = 0f;
				anim.CrossFadeInFixedTime("Attack_0"+Mathf.FloorToInt(Random.Range(0.9f,2.4f)).ToString(),transition_speed,0);
				Attacking = true;
				Invoke("UnAtk",1.6f);
			}else {
					
				if (!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
				anim.CrossFadeInFixedTime("Idle",transition_speed,0);
				}
					
			}
			cur_spd = Mathf.Lerp(cur_spd,0f,15f*Time.deltaTime);
		} else {
				
			cur_spd = Mathf.Lerp(cur_spd,0f,15f*Time.deltaTime);
			if (!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			anim.CrossFadeInFixedTime("Idle",transition_speed,0);
			}
				
		
		}
		}else {
			cur_spd = Mathf.Lerp(cur_spd,0f,15f*Time.deltaTime);
		}
    }
}
