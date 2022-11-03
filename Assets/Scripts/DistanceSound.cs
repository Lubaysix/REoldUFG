using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceSound : MonoBehaviour
{
	Transform Ch;
	AudioSource self;
    // Start is called before the first frame update
    void Start()
    {
       self = this.GetComponent<AudioSource>();
	   Ch = GameObject.Find("Char").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Ch != null) {
			self.volume = 1f/Vector3.Distance(this.transform.position,Ch.position);
		}
    }
}
