using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StartController))]
public class Room : MonoBehaviour
{
   public Camera[] Cameras;
   public RenderTexture rend;
   public Material RenderMaterial;
   void Start() {
	if (Global.GoingBack) {
		   if (Global.gotoscene != "") {
			Global.Current_Camera = Cameras[Global.Pending_Camera_ID];
			for (int c = 0; c < Cameras.Length; c++) {
				Cameras[c].gameObject.SetActive(false);
			}
			Cameras[Global.Pending_Camera_ID].gameObject.SetActive(true);
			Global.gotoscene = "";
		   }else {
		   Global.Current_Camera = Cameras[Global.Cur_Camera_ID];
			for (int c = 0; c < Cameras.Length; c++) {
				Cameras[c].gameObject.SetActive(false);
			}
			Cameras[Global.Cur_Camera_ID].gameObject.SetActive(true);
		   }
	}else {
	Global.Current_Camera = Cameras[0];
	}
	if (Global.SimulatePSXResolution) {
	rend = new RenderTexture(256, 224, 32, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
	for (int i = 0; i < Cameras.Length; i++) {
			Cameras[i].GetComponent<Camera>().targetTexture = rend;
	}
	rend.filterMode = FilterMode.Point;
	rend.Create();
	RenderMaterial.mainTexture = rend;
	}
   }
}
