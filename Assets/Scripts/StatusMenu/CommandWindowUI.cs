using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum CMD_Button_Type {
	UseOrEquip,
	Combine,
	Check,
	New,
	PlaceHolder // Does nothing when you press it.
}
[System.Serializable]
public class Cmd_Button {
	public CMD_Button_Type type;
	public SpriteRenderer _Renderer;
	public Sprite HighLight, Off;
}
public class CommandWindowUI : MonoBehaviour
{
	public bool Selected;
	public float positionHideX,positionShowX;
	public float Speed;
	public float Y,maxY;
	public StatusItem item;
	public StatusItem combineItem;
	public bool Combining;
	public InventoryCursor cursor;
	public StatusEquipUI E_UI;
	public AudioClip MoveAC,CancelAC;
	int index;
	public List<Cmd_Button> cmd_buttons = new List<Cmd_Button>();
	
	void Start() {
		Highlight_BTN(index);
	}
    // Start is called before the first frame update
	public void Combine() {
		if (item != null && combineItem != null) {
			int f = Global.Item_DB.Items[item.itm.id].Possible_Combinations.FindIndex(x => x.combinable_id == combineItem.itm.id);
			if (f != -1) {
				 if (Global.Item_DB.Items[item.itm.id].Possible_Combinations[f].type == Combination_Type.Common) {
					Inv_Item i = new Inv_Item();
					i.id = Global.Item_DB.Items[item.itm.id].Possible_Combinations[f].converted_id;
					i.amount = 1;
					Global.Inventory.Add(i);
					Global.Inventory[item.arr_index].amount--;
					Global.Inventory[combineItem.arr_index].amount--;
					Global.UpdateInventory();
				 } else  if (Global.Item_DB.Items[item.itm.id].Possible_Combinations[f].type == Combination_Type.Ammo)  {
					
					if (Global.Inventory[combineItem.arr_index].ammo < Global.Item_DB.Items[combineItem.itm.id].maxAmmo) {
						//int f2 = Global.Item_DB.Items[item.itm.id].Possible_Combinations.FindIndex(x => x.id);
						if (Global.Item_DB.Items[item.itm.id].type == Item_Type.Combinable_Item) {
						int a,b;
						b = Mathf.Abs(Global.Item_DB.Items[combineItem.itm.id].maxAmmo-(Global.Inventory[combineItem.arr_index].ammo+Global.Inventory[item.arr_index].amount));
						a = Global.Inventory[item.arr_index].amount-b;
						Debug.Log(a+"/"+b);
						Global.Inventory[item.arr_index].amount -= a;
						Global.Inventory[combineItem.arr_index].ammo+= a;
						Global.UpdateInventory(); 
						}else if (Global.Item_DB.Items[item.itm.id].type == Item_Type.Weapon) {
						int a,b;
						b = Mathf.Abs(Global.Item_DB.Items[item.itm.id].maxAmmo-(Global.Inventory[item.arr_index].ammo+Global.Inventory[combineItem.arr_index].amount));
						a = Global.Inventory[combineItem.arr_index].amount-b;
						Debug.Log(a+"/"+b);
						Global.Inventory[combineItem.arr_index].amount -= a;
						Global.Inventory[item.arr_index].ammo+= a;
						Global.UpdateInventory(); 
						}else {
							Debug.Log("<color=yellow>Unknown ammo combination warning</color>");
							Global.UpdateInventory(); 
						}
					}
					
				 }
			}else {
				Debug.Log("<color=yellow>Status UI Warning! | No combinations found between these objects!</color>");
			}
			item = null;
			combineItem = null;
			Combining = false;
			index = 0;
		}
	}
    void Highlight_BTN(int ind) {
		for (int i = 0; i < cmd_buttons.Count; i++) {
			cmd_buttons[i]._Renderer.sprite = cmd_buttons[i].Off;
		}
		cmd_buttons[ind]._Renderer.sprite= cmd_buttons[ind].HighLight;
	}
	void PressButton(int ind) {
		
		if (cmd_buttons[ind].type == CMD_Button_Type.PlaceHolder) {
			// Play decide sound.
		} else if (cmd_buttons[ind].type == CMD_Button_Type.New) {
			// Place some code here if you want to make a new type of button
		} else if (cmd_buttons[ind].type == CMD_Button_Type.UseOrEquip) {
			// Use or equip button, normally this button is in the top, but you can place it whenever you want
			//if (Global.Inventory[item.arr_index]
			
			if (Global.Item_DB.Items[item.itm.id].type == Item_Type.Heal_Item) {
				Global.HealChar(Global.Item_DB.Items[item.itm.id].Heal);
				Global.Inventory[item.arr_index].amount--;
			}else if (Global.Item_DB.Items[item.itm.id].type == Item_Type.Poison_Heal_Item) {
				Global.HealChar(Global.Item_DB.Items[item.itm.id].Heal);
				Global.Inventory[item.arr_index].amount--;
			}else {

				if (Global.Item_DB.Items[item.itm.id].type == Item_Type.Weapon || Global.Item_DB.Items[item.itm.id].type == Item_Type.WeaponNoAmmo) {
						if (Global.EquipedSlot != Global.Item_DB.Items[item.itm.id].id) {
							E_UI.Equip(Global.Item_DB.Items[item.itm.id].id);
							Global.EquipedSlot = Global.Item_DB.Items[item.itm.id].id;
						} else {
							Global.EquipedSlot = -1;
							E_UI.UnEquip();
						}
					}
			}
			} else if (cmd_buttons[ind].type == CMD_Button_Type.Combine) {
			Combining = true;
		    cursor.Deselect();
		} else {
			
		}
		if (cmd_buttons[ind].type != CMD_Button_Type.Combine) {
		Global.UpdateInventory();
		item = null;
		combineItem = null;
		Combining = false;
		index = 0;
		cursor.Deselect();
		}
	}
	void UpdateWindow()
    {
		
		if (Global.n_controls.Use.wasPressedThisFrame) {
			PressButton(index);
		}
		if (!Combining) {
        if (Global.n_controls.Down.wasPressedThisFrame) {
			Global.PlayGlobalAudio2D(MoveAC);
			if (index >= 2) {
				index = 0;
			}else {
				index++;
			}
			
		}
		if (Global.n_controls.Up.wasPressedThisFrame) {
			Global.PlayGlobalAudio2D(MoveAC);
			if (index <= 0) {
				index = 2;
			}else {
				index--;
			}
		
		}
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (Combining && Selected && Global.n_controls.Run.wasPressedThisFrame) {
			Global.PlayGlobalAudio2D(CancelAC);
			Global.UpdateInventory();
			item = null;
			combineItem = null;
			Combining = false;
			index = 0;
			cursor.Deselect();
		}
		Highlight_BTN(index);
		if (cursor == null) {
			if (GameObject.FindGameObjectWithTag("UICursor") != null) {
				cursor = GameObject.FindGameObjectWithTag("UICursor").GetComponent<InventoryCursor>();
			}
		}
		if (item == null) {
			Selected = false;
		}
		Y = Mathf.Clamp(Y,maxY,1000f);
        if (Selected) {
			this.transform.position = Vector3.Lerp(this.transform.position,new Vector3(positionShowX,Y,10f),Speed*Time.deltaTime);
			UpdateWindow();
		}else {
			this.transform.position = Vector3.Lerp(this.transform.position,new Vector3(positionHideX,Y,10f),Speed*Time.deltaTime);
		}
    }
}
