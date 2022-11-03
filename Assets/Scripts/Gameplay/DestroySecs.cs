using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySecs : MonoBehaviour
{
	public float secs;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("dest",secs);
    }

    // Update is called once per frame
    void dest()
    {
        Destroy(this.gameObject);
    }
}
