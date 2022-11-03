using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Vector3 Spd;
	public bool Enabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Enabled) {
        this.transform.eulerAngles += new Vector3(Spd.x*Time.deltaTime,Spd.y*Time.deltaTime,Spd.z*Time.deltaTime);
		}
	}
}
