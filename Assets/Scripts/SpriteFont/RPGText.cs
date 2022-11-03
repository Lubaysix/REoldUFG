using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum TextType {
	Normal,
	YesNo
}
public class RPGText : MonoBehaviour
{
	public TextType type;
	public List<string> Text;
	public GameObject YesNoQuestionTxt;
	public SpriteFont sprfont;
	public float timer;
	float tmr;
	float c_timer;
	int index2;
	int index;
	public bool WaitingAction;
	public Vector3 YNPos;
	MainMovement m;
	public void Write(string[] strings) {
		if (strings.Length >= 1) {
		Text.Clear();
		for (int i = 0; i < strings.Length; i++) {
			Text.Add(strings[i]);
		}
		WaitingAction = false;
		index = 0;
		index2 = 0;
		}else {
			Debug.Log("The Interactable Trigger has no text in it, but is trying to display text on screen!, Replacing string");
			Text.Clear();
			Text.Add("Error!");
			Text.Add("Error!!");
			Text.Add("Error!!.");
			Text.Add("Error!!!..");
			Text.Add("Error!!!...");
			WaitingAction = false;
			index = 0;
			index2 = 0;

		}
	}
	public void WriteSingleLine(string str) {
		Text.Clear();
		Text.Add(str);
		WaitingAction = false;
		index = 0;
		index2 = 0;
	}
	public void Decide(bool yes) {
		if (m.LeonCP.Using) {
				m.UnGrab(yes);
		}
	}
	void Start() {
		
		if (GameObject.Find("Char") != null) {
			m = GameObject.Find("Char").GetComponent<MainMovement>();	
		}else {
			Debug.LogWarning("<color=red>There isn't a Main Character in the scene, please put one!</color>");
			this.enabled = false;
		}
	}
	void CanInteract() {
		m.canInteract = true;
	}
	void Action() {
		if (WaitingAction) {
			int l;
			if (Text.Count == 1) {
				l = 0;
			}else {
				l = Text.Count-1;
			}
			if (index2 >= l) {
				if (type == TextType.Normal) {
					sprfont.text = "";
					Text.Clear();
					WaitingAction = false;
					Invoke("CanInteract",0.1f);
					index2 = 0;
					index = 0;
					if (m.LeonCP.Using) {
						
					}else {
						m.Mstatus = MovementStatus.Normal;
					}
				}else if (type == TextType.YesNo) {
					Instantiate(YesNoQuestionTxt,this.transform.position+YNPos,Quaternion.identity).GetComponent<TextQuestion>().src = this;
					sprfont.text = "";
					Text.Clear();
					index2 = 0;
					index = 0;
					WaitingAction = false;
				}else {
					
				}
			}else {
				sprfont.text = "";
				index2 += 1;
				index = 0;
			}
		}
	}
    // Update is called once per frame
    void Update()
    {
		
		
		if (Text != null && Text.Count >= 1) {
		m.canInteract = false;
		m.Mstatus = MovementStatus.Disabled;
        tmr += Time.deltaTime;
		if (tmr >= c_timer) {
			tmr = 0f;
			if (index <= Text[index2].Length-1) {
			sprfont.text = Text[index2].Substring(0,index);
			index += 1;
			WaitingAction = false;
				if (index >= Text[index2].Length) {
					WaitingAction = true;
					sprfont.text += "<";
				}
			}
		}
		}
		if (Global.n_controls.Use.wasPressedThisFrame) {
			Action();
		}
		if (Global.n_controls.Use.isPressed) {
			
			c_timer = timer/2.5f;
		}else {
			c_timer = timer;
		}
		
		
    }
}
