using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GameObject[] g = GameObject.FindGameObjectsWithTag("Enemy");
		if (g.Length > 0) {
		if (Global.Enemies.Count > 0 && Global.Enemies != null) {
			for (int e = 0; e < Global.Enemies.Count; e++) {
				if (GameObject.Find(Global.Enemies[e].self_name) != null) {
				GameObject ee = GameObject.Find(Global.Enemies[e].self_name);
				ee.transform.position = Global.Enemies[e].pos;
				ee.transform.eulerAngles = Global.Enemies[e].ang;
				ee.GetComponent<BasicEnemy>().HP = Global.Enemies[e].HP;
				}
			}
		}
		}
		if (Global.DeletedObjects.Count > 0 && Global.DeletedObjects != null) {
			for (int d = 0; d < Global.DeletedObjects.Count; d++) {
				if (SceneManager.GetActiveScene().name == Global.DeletedObjects[d].Room) {
					if (GameObject.Find(Global.DeletedObjects[d].ObjectName) != null) {
					Destroy(GameObject.Find(Global.DeletedObjects[d].ObjectName));
					}
				}
			}
		}
    }
}
