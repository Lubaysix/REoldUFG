using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
		GameObject[] objects = GameObject.FindGameObjectsWithTag(this.gameObject.tag);
		if (objects.Length > 1) {
			for (int i = 1; i < objects.Length; i++) {
				Destroy(objects[i]);
			}
		}
    }
}
