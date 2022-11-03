using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusMenuSCR : MonoBehaviour
{
	public AudioClip gobackclp;
	public string keyconfscene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (Global.n_controls.Status.wasPressedThisFrame) {
		 Global.GoBackGameplay(gobackclp);
	 }
		//if (Global.n_controls.Status.wasPressedThisFrame) {
		//	GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<SceneLoader>().SceneLoadTimer(keyconfscene,1f);
		//}	
    }
}
