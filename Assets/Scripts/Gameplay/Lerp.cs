using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
	public Vector3 Angle,Position,Size;
	public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3(Mathf.LerpAngle(this.transform.eulerAngles.x,Angle.x,Speed*Time.deltaTime),Mathf.LerpAngle(this.transform.eulerAngles.y,Angle.y,Speed*Time.deltaTime),Mathf.LerpAngle(this.transform.eulerAngles.z,Angle.z,Speed*Time.deltaTime));
		this.transform.localPosition = Vector3.Lerp(this.transform.localPosition,Position,Speed*Time.deltaTime);
		this.transform.localScale = Vector3.Lerp(this.transform.localScale,Size,Speed*Time.deltaTime);
	}
}
