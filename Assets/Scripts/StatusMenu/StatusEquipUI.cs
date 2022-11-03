using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEquipUI : MonoBehaviour
{
	public GameObject UI_Item;
	GameObject E = null;
	void Start() {
		UpdateEquip();
	}
	public void UpdateEquip() {
		if (E == null && Global.EquipedSlot != -1) {
			Equip(Global.EquipedSlot);
		}
	}
	public void Equip(int id) {
		if (E != null && E.GetComponent<StatusItem>().itm.id != id) {
		Destroy(E);
		E = null;
		}
		if (E == null) {
		E = Instantiate(UI_Item,this.transform.position+new Vector3(0f,0f,-1f),Quaternion.identity);
		int a = Global.Inventory.FindIndex(x => x.id == id);
		if (a != -1) {
		E.GetComponent<StatusItem>().itm = Global.Inventory[a];
		}else {
			Debug.LogError("Unknown Error in Equip void (Line 24, variable (int) a)");
		}
		}
	}
	public void UnEquip() {
		if (E != null) {
		Destroy(E);
		E = null;
		}
	}
}
