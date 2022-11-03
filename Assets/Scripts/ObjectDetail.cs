using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetail : MonoBehaviour
{
	public Vector3 MaxOffset;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position =  this.transform.position+new Vector3(Random.Range(-MaxOffset.x,MaxOffset.x),Random.Range(-MaxOffset.y,MaxOffset.y),Random.Range(-MaxOffset.z,MaxOffset.z));
		this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,Random.Range(0f,360f),this.transform.eulerAngles.z);
    }
}
