using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.Linq;
 public enum SpriteFontType {
     Update,
     UpdateLastLetter,
     Start
 }
public class SpriteFont : MonoBehaviour
{
    public Font SprFont;
    public float distance_between_letters;
    public GameObject Char;
    public string text,textbefore;
    public string name;
    float x,y;
	public int MaxLength;
	public List<Transform> Created;
    public SpriteFontType Type;
	public Vector3 size = new Vector3(1f,1f,1f);
    // Update is called once per frame
    void Start(){
		for(int l = 0; l < MaxLength; l++) {
			GameObject ch = Instantiate(Char,new Vector3(-1000,1000,2),Quaternion.identity);
			ch.name = this.gameObject.name + "_" + l.ToString();
			ch.transform.parent = this.transform;
			Created.Add(ch.transform);
		}
         if (Type == SpriteFontType.Start) {
            Write(text);
        }
    }
    void Update ()
    {
        if (Type == SpriteFontType.Update) {
            if (textbefore != text) {
                textbefore = text;
                Write(text);
            }
        }else if (Type == SpriteFontType.UpdateLastLetter) {
            if (textbefore != text) {
                if (text != "") {
                    textbefore = text;
                    //LastLetterWrite(text,true);
                }else {
                    Write(text);
                    textbefore = text;
                }
                
            }
        }
       
     
    }
    public void SubtractLetter() {
        if (text.Length > 0) {
            text = text.Substring(0,text.Length-1);
       /* for (int chars = 0; chars < GameObject.FindObjectsOfType<GameObject> ().Length; chars++) {
            if (GameObject.FindObjectsOfType<GameObject> () [chars].name == name && GameObject.FindObjectsOfType<GameObject> () [chars].tag == "LastLetter") {
                Destroy(GameObject.FindObjectsOfType<GameObject> () [chars]);
            }
        }*/
        Write(text);
        }
        
    }
   /* public void LastLetterWrite(string txt, bool AddingLetter) {
        int x2 = 0;
        if (AddingLetter) {
            x2 = -1;
        }else {
            x2 = 0;
        }
        char[] split = txt.ToCharArray();
            x = txt.Length;
            GameObject gb = Instantiate(Char,new Vector3(this.transform.position.x + (x * distance_between_letters),this.transform.position.y - (x * y),2f),Quaternion.identity);
            SpriteRenderer rend = gb.GetComponent<SpriteRenderer>();
            gb.transform.parent = this.transform;
            gb.name = name;
        gb.transform.localPosition = new Vector3(gb.transform.localPosition.x, gb.transform.localPosition.y, 20f);
        for (int n = 0; n < SprFont.FontSprites.Count; n++ ){
                if (SprFont.FontSprites[n].name == split[(int)x+x2].ToString()) {
                    rend.sprite = SprFont.FontSprites[n].sprite;    
                }
            }
            if (split[(int)x+x2] == ' ') {
                rend.sprite = SprFont.Space;
            }
        x = 0;
    }*/
	void DeleteLetters() {
		//for (int chars = 0; chars < GameObject.FindObjectsOfType<GameObject> ().Length; chars++) {
		//    if (GameObject.FindObjectsOfType<GameObject> () [chars].name == name) {
		//        Destroy(GameObject.FindObjectsOfType<GameObject> () [chars]);
		//    }
		//}
		for (int chars = 0; chars < Created.Count; chars++) {
			Created[chars].position = new Vector3(-1000f,1000f,2f);
		}
	}
    public void Write(string txt) {
        

		if (Created != null && text.Length <= MaxLength) {
			DeleteLetters();
        char[] split = txt.ToCharArray();
		
        for (int letter = 0; letter < text.Length; letter++) {

            x += 1;
           
             //GameObject gb = Instantiate(Char,new Vector3(this.transform.position.x + (x * distance_between_letters),this.transform.position.y - (letter * y),20f),Quaternion.identity);
				GameObject gb = Created[letter].gameObject;
				gb.transform.position = new Vector3(this.transform.position.x + (x * distance_between_letters),this.transform.position.y - (letter * y),20f);
			SpriteRenderer rend = gb.GetComponent<SpriteRenderer>();
            gb.transform.parent = this.transform;
            gb.transform.localPosition = new Vector3(gb.transform.localPosition.x, gb.transform.localPosition.y, 20f);
			gb.name = this.gameObject.name + "_"+ letter + "_Used";
            gb.transform.localScale = size;
            for (int n = 0; n < SprFont.FontSprites.Count; n++ ){
                if (SprFont.FontSprites[n].name == split[letter].ToString()) {
                    rend.sprite = SprFont.FontSprites[n].sprite;    
                }
            }
            if (split[letter] == ' ') {
                rend.sprite = SprFont.Space;
            }
            
           
        }
        x = 0;
    }
}
}
