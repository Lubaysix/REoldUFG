using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCursor : MonoBehaviour
{
	public float XGrid = 0.4f;
	public float YGrid = 0.3f;
	public float Speed = 15f;
	public InventoryMenu m;
	public AudioClip Move,Accept,Cancel;
	public AudioSource src;
	float x,y,ix,iy,xx,yy;
	public bool Selected;
	StatusItem item_selected;
	RaycastHit2D hit;
	public float collider_radious;
	public GameObject CommandWindow;
	void Start() {
		m = GameObject.Find("Inventory").GetComponent<InventoryMenu>();
		CommandWindow = GameObject.Find("CMD_Win");
		xx = this.transform.position.x;
		yy = this.transform.position.y;
		
	}
	void ab() {
		Selected = false;
	}
	public void Deselect() {
		src.clip = Accept;
		src.Play();
		Invoke("ab",0.05f);
		
	}
    // Update is called once per frame
    void Update()
    {
		
		hit = Physics2D.CircleCast(this.transform.position,collider_radious,Vector2.right);
		if (hit.collider != null && hit.collider.tag == "UI_Item") {
			item_selected = hit.collider.GetComponent<StatusItem>();
		}
		if (hit.collider == null) {
			item_selected = null;
		}
		this.transform.position = Vector3.Lerp(this.transform.position,new Vector3(xx,yy,this.transform.position.z)+new Vector3(x,-y,0f),Speed*Time.deltaTime);
		if (Global.n_controls.Use.wasPressedThisFrame && item_selected != null) {
			src.clip = Accept;
			src.Play();
			if (CommandWindow.GetComponent<CommandWindowUI>().Combining) {
				if (item_selected != CommandWindow.GetComponent<CommandWindowUI>().item) {
					Selected = true;
					CommandWindow.GetComponent<CommandWindowUI>().Selected = true;
					CommandWindow.GetComponent<CommandWindowUI>().combineItem = item_selected;
					CommandWindow.GetComponent<CommandWindowUI>().Combine();
					CommandWindow.GetComponent<CommandWindowUI>().Y = this.transform.position.y;
					Global.UpdateInventory();
				}
			} else {
					if (!CommandWindow.GetComponent<CommandWindowUI>().Selected) {
						Selected = true;
						CommandWindow.GetComponent<CommandWindowUI>().Selected = true;
						CommandWindow.GetComponent<CommandWindowUI>().item = item_selected;
						CommandWindow.GetComponent<CommandWindowUI>().Y = this.transform.position.y;
						
					}
			}
		}
		if (Global.n_controls.Run.wasPressedThisFrame && Selected) {
			src.clip = Cancel;
			src.Play();
			Selected = false;
			CommandWindow.GetComponent<CommandWindowUI>().Selected = false;
		}
		if (!Selected) {
			if (Global.n_controls.Right.wasPressedThisFrame && ix < m.columns-1) {
				x += XGrid;
				ix++;
				src.clip = Move;
				src.Play();
			}
			if (Global.n_controls.Left.wasPressedThisFrame && ix > 0) {
				x -= XGrid;
				ix--;
				src.clip = Move;
				src.Play();
			}
			if (Global.n_controls.Down.wasPressedThisFrame && iy < m.rows-1) {
				y += YGrid;
				iy++;
				src.clip = Move;
				src.Play();
			}
			if (Global.n_controls.Up.wasPressedThisFrame && iy > 0) {
				y -= YGrid;
				iy--;
				src.clip = Move;
				src.Play();
			}
		}
    }
}
