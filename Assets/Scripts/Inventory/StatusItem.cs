using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusItem : MonoBehaviour
{
	public Inv_Item itm;
	public int arr_index;
	public GameObject TXTAmmo,TXTItem;
	public Vector3 OffsetTXT;
	SpriteFont font;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = Global.Item_DB.Items[itm.id].Icon;
		this.transform.localScale = Global.Item_DB.Items[itm.id].SizeInSlot;
		if (Global.Item_DB.Items[itm.id].maxAmount > 1 && Global.Item_DB.Items[itm.id].type != Item_Type.Weapon) {
			if (Global.Item_DB.Items[itm.id].type == Item_Type.Combinable_Item) {
				font = Instantiate(TXTAmmo,this.transform.position+OffsetTXT,Quaternion.identity).GetComponent<SpriteFont>();
			
			}else {
				font = Instantiate(TXTItem,this.transform.position+OffsetTXT,Quaternion.identity).GetComponent<SpriteFont>();
			}
			font.transform.SetParent(this.transform);
		}
		if (Global.Item_DB.Items[itm.id].type == Item_Type.Weapon) {
			font = Instantiate(TXTItem,this.transform.position+OffsetTXT,Quaternion.identity).GetComponent<SpriteFont>();
			font.transform.SetParent(this.transform);
		}
    }
	void Update() {
		if (font != null) {
			if (Global.Item_DB.Items[itm.id].type != Item_Type.Weapon) {
			font.text = itm.amount.ToString();
			}else {
			font.text = itm.ammo.ToString();	
			}
		}
	}
}
