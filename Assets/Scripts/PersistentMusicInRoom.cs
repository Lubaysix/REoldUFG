using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusicInRoom : MonoBehaviour
{
	string Room;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
		Room = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != Room && SceneManager.GetActiveScene().name != Global.STATUS_MENU_SCENE) {
			this.GetComponent<AudioSource>().volume = Mathf.Lerp(this.GetComponent<AudioSource>().volume,0f,10f*Time.deltaTime);
			if (this.GetComponent<AudioSource>().volume < 0.02f) {
				Destroy(this.gameObject);
			}
		}
    }
}
