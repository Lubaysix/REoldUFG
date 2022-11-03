using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRESCH : MonoBehaviour
{
	public Vector2 res;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(Mathf.FloorToInt(res.x),Mathf.FloorToInt(res.y),true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
