using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InventoryMenu : MonoBehaviour
{
	public float XGrid,YGrid,XGridM,YGridM,XGridSlot,YGridSlot;
	public int columns,rows;
	public float RightBorderOffsetX,LeftBorderOffsetX;
	public Vector3 SlotInitialOffset;
	public GameObject BorderTop,BorderBottom,BorderTopM,BorderBottomM;
	public GameObject[] BorderMiddle;
	public GameObject Slot,Cursor,UI_Item;
	public float secs;
	float timer;
	GameObject curCursor;
	CommandWindowUI CMD;
	List<GameObject> Border = new List<GameObject>();
	List<GameObject> Slots = new List<GameObject>();
	List<GameObject> Items = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
		CMD = GameObject.Find("CMD_Win").GetComponent<CommandWindowUI>();
		Global.UpdateItemsARRAY();
        SpawnInventory(false);
    }
	
	public void SpawnInventory(bool c) {
		if (Border != null) {
			for (int i = 0; i < Border.Count; i++) {
				Destroy(Border[i]);
			}
			Border.Clear();
		}
		int a = 0;
		if (rows >= 3) {
			GameObject top = Instantiate(BorderTop,this.transform.position,Quaternion.identity);
			top.transform.SetParent(this.transform);
			GameObject topm = Instantiate(BorderTopM,this.transform.position+new Vector3(LeftBorderOffsetX,0f,0f), Quaternion.identity);
			GameObject topm2 = Instantiate(BorderTopM,this.transform.position+new Vector3(RightBorderOffsetX,0f,0f), Quaternion.identity);
			for (a = 1; a < rows-1; a++) {
				GameObject middle = Instantiate(BorderMiddle[Random.Range(0,BorderMiddle.Length)],this.transform.position-new Vector3(-LeftBorderOffsetX,YGridM*a,0f), Quaternion.identity);
				GameObject middle2 = Instantiate(BorderMiddle[Random.Range(0,BorderMiddle.Length)],this.transform.position+new Vector3(RightBorderOffsetX,-(YGridM*a),0f), Quaternion.identity);
				middle.transform.SetParent(this.transform);
				middle2.transform.SetParent(this.transform);
				Border.Add(middle);
				Border.Add(middle2);
			}
			GameObject bottom = Instantiate(BorderBottom,this.transform.position-new Vector3(0f,YGrid*(a+1),0f),Quaternion.identity);
			bottom.transform.SetParent(this.transform);
			GameObject bm = Instantiate(BorderBottomM,this.transform.position-new Vector3(-LeftBorderOffsetX,YGridM*a), Quaternion.identity);
			GameObject bm2 = Instantiate(BorderBottomM,this.transform.position+new Vector3(RightBorderOffsetX,-(YGridM*a),0f), Quaternion.identity);
			bm.transform.SetParent(this.transform);
			bm2.transform.SetParent(this.transform);
			topm.transform.SetParent(this.transform);
			topm2.transform.SetParent(this.transform);
			Border.Add(top);
			Border.Add(bottom);
			Border.Add(bm);
			Border.Add(bm2);
			Border.Add(topm);
			Border.Add(topm2);
		}
		CreateSlots(c);
		
	}
	void CreateSlots(bool b) {
		
		//GameObject slot = Instantiate(Slot,this.transform.position+new Vector3(x*XGridSlot,-(y*YGridSlot))+SlotInitialOffset,Quaternion.identity);
		//slot.transform.SetParent(this.transform);
		
		
		if (!b) {
		if (Slots != null) {
			for (int i = 0; i < Slots.Count; i++) {
				Destroy(Slots[i]);
			}
			Slots.Clear();
		}
		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < columns; x++) {
				GameObject slot = Instantiate(Slot,this.transform.position+new Vector3(x*XGridSlot,-(y*YGridSlot))+SlotInitialOffset,Quaternion.identity);
				Slots.Add(slot);
			}
		}
		curCursor = Instantiate(Cursor,Slots[0].transform.position,Quaternion.identity);
		}
		
		SpawnItems();
	}
	void SpawnItems() {
		if (Items != null) {
			for (int i = 0; i < Items.Count; i++) {
				Destroy(Items[i]);
			}
			Items.Clear();
		}
		for (int x = 0; x < Global.Inventory.Count; x++) {
			GameObject ui_i = Instantiate(UI_Item,Slots[x].transform.position+new Vector3(0f,0f,-1f),Quaternion.identity);
			ui_i.GetComponent<StatusItem>().itm = Global.Inventory[x];
			ui_i.GetComponent<StatusItem>().arr_index = x;
			Items.Add(ui_i);
		}
	}
	
    // Update is called once per frame
    void Update()
    {
		
		
        if (Gamepad.current.buttonSouth.wasPressedThisFrame || Gamepad.current.buttonWest.wasPressedThisFrame) {
			if (!CMD.Combining && !CMD.Selected) {
			Global.UpdateInventory();
			}
		}
    }
}
