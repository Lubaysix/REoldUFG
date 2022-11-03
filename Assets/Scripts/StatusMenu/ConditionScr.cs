using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionScr : MonoBehaviour
{
	public Animator anim;
	public SpriteRenderer rend;
	public Sprite[] sprites;
    // Start is called before the first frame update
    void Update()
    {
        if (Global.MainCharHealth <= 100 && Global.MainCharHealth >= 75) {
			anim.Play("Fine");
			rend.sprite = sprites[0];
		}else if (Global.MainCharHealth <= 74 && Global.MainCharHealth >= 50) {
			anim.Play("CautionN");
			rend.sprite = sprites[1];
		} else if (Global.MainCharHealth >= 25 && Global.MainCharHealth <= 49) {
			anim.Play("Caution");
			rend.sprite = sprites[2];
		} else {
			anim.Play("Danger");
			rend.sprite = sprites[3];
		}
    }
}
