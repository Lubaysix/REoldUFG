using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ProximityDetectType {
	Leon2Enemy,
	None
}
public class ProximityDetect : MonoBehaviour
{
	public ProximityDetectType type;
	public GameObject CHAR;
	public float Dist;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Char") != null) {
			CHAR = GameObject.Find("Char");	
		}else {
			Debug.LogWarning("<color=red>There isn't a Main Character in the scene, please put one!</color>");
			this.enabled = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (CHAR != null && type == ProximityDetectType.Leon2Enemy) {
			GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
			int a = 0;
			for (int en = 0; en < Enemies.Length; en++) {
				if (Vector3.Distance(CHAR.transform.position,Enemies[en].transform.position) < Dist) {
					a += 1;
				}
				
			}
			if (a > 0) {
				CHAR.GetComponent<MainMovement>().LeonCP.EnemyNear = true;
			}else {
				CHAR.GetComponent<MainMovement>().LeonCP.EnemyNear = false;
			}
		}
    }
}
