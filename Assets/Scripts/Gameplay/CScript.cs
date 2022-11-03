using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScript : MonoBehaviour
{
	public GameObject WhiteFade;
	public Vector3 SafeCameraPlace;
    public void Dead() {
		GameObject wf = Instantiate(WhiteFade,new Vector3(0f,0f,0f),Quaternion.identity);
		wf.transform.position = this.transform.position + new Vector3(0f,0f,1.5f);
		wf.transform.eulerAngles = this.transform.eulerAngles;
	}
}
