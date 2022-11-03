using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSpawn : MonoBehaviour
{
	public float Secs;
	public GameObject gb;
	public Vector3 Offset;
	public bool RandomizeOffset;
	float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
		if (timer >= Secs) {
			timer = 0f;
			if (RandomizeOffset) {
			Instantiate(gb,this.transform.position+new Vector3(Random.Range(-Offset.x,Offset.x),Random.Range(-Offset.y,Offset.y),Random.Range(-Offset.z,Offset.z)),Quaternion.identity);
			}else {	
			GameObject g = Instantiate(gb,this.transform.position+Offset,Quaternion.identity);
			g.transform.localScale = this.transform.localScale;
			g.transform.eulerAngles = this.transform.eulerAngles;
			g.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
			}
		}
    }
}
