using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject ItemShow,ItemShowNew,FadeIn,FadeOut;
	GameObject itm;
	void Start() {
		Fade(false);
	}
	public void ShowItem(int id, int type) {
		if (type == 0) {
		ItemShow.GetComponent<SpriteRenderer>().sprite = Global.Item_DB.Items[id].BigIcon;
		ItemShow.SetActive(true);
		ItemShow.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		ItemShow.transform.eulerAngles = new Vector3(0f,0f,-150f);
		} else if (type == 2 ) {
		ItemShowNew.SetActive(true);
		ItemShowNew.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
		ItemShowNew.transform.eulerAngles = new Vector3(0f,0f,-150f);
		itm = Instantiate(Global.Item_DB.Items[id].Model,this.transform.position+new Vector3(0f,-1.5f,4f),Quaternion.identity);
		itm.transform.eulerAngles = new Vector3(Random.Range(-15f,15f),0f,0f);
		itm.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
		itm.GetComponent<Rotate>().Enabled = true;
	
	    } else {
			
		}
	}

	public void UnShowItem(int type) {
		if (type == 0) {
		ItemShow.SetActive(false);
		} else if (type == 2) {
			if (itm != null) {
				Destroy(itm);
				itm = null;
			}
			ItemShowNew.SetActive(false);
		}else {
			
		}
	}
	public void Fade(bool In) {
		if (In) {
			Instantiate(FadeIn,this.transform.position+new Vector3(0f,0f,5f),Quaternion.identity);
		}else {
			Instantiate(FadeOut,this.transform.position+new Vector3(0f,0f,5f),Quaternion.identity);
		}
	}
}
