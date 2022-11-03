using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Combination_Type {
	Common,
	Ammo
}
[System.Serializable]
public class INV_Combinable {
	[Header("The name of this combination")]
	public string Combination_Name;
	[Header("Combinable_ID is the other item and the Converted_ID is the object after the combination")]
	public int combinable_id,converted_id;
	[Header("Type of combination?")]
	public Combination_Type type;
	[Header("Optional message showing after combining")]
	public string Optional_Message;
}
public enum Item_Type {
	Weapon,
	WeaponNoAmmo,
	Heal_Item,
	Poison_Heal_Item,
	Important_Item,
	Combinable_Item
}
[System.Serializable]
public class INV_Item {
	[Header("Item Name")]
	public string Name;
	[Header("Item Identifier(Must be the same as the list index)")]
	public int id;
	[Header("Item Type")]
	public Item_Type type;
	[Header("Item Icon from status menu and Detailed icon for check menu")] 
	public Sprite Icon,BigIcon;
	[Header("Item 3D Model useful for grabbing and enviroment")]
	public GameObject Model;
	[Header("Item Max Amount that can be grabbed")]
	public int maxAmount = 1;
	public Vector3 SizeInSlot;
	[Header("Possible Combinations with other items in the database")]
	public List<INV_Combinable>	Possible_Combinations;
	[Header("Optional Features for weapons and healing items")]
	public float Damage,Heal;
	public int maxAmmo = 0;
	public int initialAmmo = 0;
	public int AmmoItemID;
	[Header("Attack clip for weapons")]
	public AudioClip AttackSClip;
	public AudioClip NoAmmoSClip;
	public AudioClip ReloadSClip;
	public List<GameObject> Particles;
}
[CreateAssetMenu(fileName = "InventoryDatabase", menuName = "REUTILS/InventoryDatabase", order = 1)]
public class InventoryDB : ScriptableObject
{
   public List<INV_Item> Items;
}
