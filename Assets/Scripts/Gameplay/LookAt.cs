using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
	public GameObject normalo;
	public float Speed;
	GameObject ro;
    // Start is called before the first frame update
    void Start()
    {
        ro = Instantiate(normalo,this.transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        ro.transform.LookAt(GameObject.Find("Char").transform);
		this.transform.eulerAngles = new Vector3(Mathf.LerpAngle(this.transform.eulerAngles.x,ro.transform.eulerAngles.x,Speed*Time.deltaTime),Mathf.LerpAngle(this.transform.eulerAngles.y,ro.transform.eulerAngles.y,Speed*Time.deltaTime),Mathf.LerpAngle(this.transform.eulerAngles.z,ro.transform.eulerAngles.z,Speed*Time.deltaTime));
    }
}
