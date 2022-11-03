using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Room room;
	public int id;
	public void OnTriggerStay(Collider col) 
	{
		if (col.tag == "Player") {
			Debug.LogWarning("<color=green>"+id.ToString()+"</color>");
		Global.Current_Camera = room.Cameras[id];
		for (int c = 0; c < room.Cameras.Length; c++) {
			room.Cameras[c].gameObject.SetActive(false);
		}
		room.Cameras[id].gameObject.SetActive(true);
		Global.Cur_Camera_ID = id;
		}
	}
}
