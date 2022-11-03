using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ENV_WP {
	public GameObject gb;
	public int id;
}
public class WeaponController : MonoBehaviour
{
	public List<ENV_WP> WPList;
	public MainMovement m;
	void Start() {
		UpdateWeapon();
	}
	public void ChangeWeapon(int id) {
		Global.EquipedSlot = id;
		UpdateWeapon();
	}
	public void UpdateWeapon() {
		for (int i = 0; i < WPList.Count; i++) {
			WPList[i].gb.SetActive(false);
		}
		int a = WPList.FindIndex(x => x.id == Global.EquipedSlot);
		if (a != -1) {
		WPList[a].gb.SetActive(true);
		}
	}
}
