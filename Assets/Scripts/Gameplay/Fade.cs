using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
	public float Speed;
	public float a;
	public SpriteRenderer rend;
	float al;
	void Start() {
		al = rend.color.a;
		Invoke("Dest",5f);
	}
    void Update()
    {
     rend.color = new Color(rend.color.r,rend.color.g,rend.color.b,al);
		al = Mathf.Lerp(al,a,Speed*Time.deltaTime);
    }
	void Dest() {
		Destroy(this.gameObject);
	}
}
