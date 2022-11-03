using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		if (GameObject.FindGameObjectWithTag("Room").GetComponent<Room>().Cameras[Global.Cur_Camera_ID] != null) {
        this.transform.LookAt(GameObject.FindGameObjectWithTag("Room").GetComponent<Room>().Cameras[Global.Cur_Camera_ID].transform);
		}
    }
}
