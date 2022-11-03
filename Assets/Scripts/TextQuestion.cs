using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextQuestion : MonoBehaviour
{
	public RPGText src;
	public Vector3[] Options;
	public Transform Cursor;
	public float cursorSpeed = 5f;
	public GlobalAP AS;
	public AudioClip moveCursorClip,decideCursorClip,cancelCursorClip;
	int index;
    void Start()
    {
        AS = GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>();
    }

    
    void Update()
    {
		Cursor.localPosition = Vector3.Lerp(Cursor.localPosition,Options[index],cursorSpeed*Time.deltaTime);
		if (Global.n_controls.Use.wasPressedThisFrame) {
			
			if (index == 1) {
				// NO
				AS.PlayAudio(cancelCursorClip);
				src.Decide(false);
			} else if (index == 0) {
				// YES
				AS.PlayAudio(decideCursorClip);
				src.Decide(true);
			} else {
				// Unknown
			}
			Destroy(this.gameObject);
		}
        if (Global.n_controls.Right.wasPressedThisFrame) {
			AS.PlayAudio(moveCursorClip);
			if (index >= Options.Length) {
				index  = 0;
			}else {
				index += 1;
			}
		}
		if (Global.n_controls.Left.wasPressedThisFrame) {
			AS.PlayAudio(moveCursorClip);
			if (index <= 0) {
				index = Options.Length;
			}else {
				index -= 1;
			}
		}
    }
}
