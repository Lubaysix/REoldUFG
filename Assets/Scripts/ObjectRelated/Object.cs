using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[RequireComponent(typeof(ObjectNameUPD))]
public class Object : MonoBehaviour
{
	public Inv_Item Itm;
	GameObject a;
	void Start() {
		ModelUpdate(Global.Item_DB.Items);
		if (Global.Item_DB.Items[Itm.id].type == Item_Type.Weapon) {
			Itm.ammo = Global.Item_DB.Items[Itm.id].initialAmmo;
		}
	}
	public void ModelUpdate(List<INV_Item> DB) {
		if (a != null) {
			Destroy(a);
			a = null;
		}
		if (DB[Itm.id].Model != null) {
		a = Instantiate(DB[Itm.id].Model,this.transform.position,DB[Itm.id].Model.transform.rotation);
		RaycastHit hit;
		if (Physics.Raycast(a.transform.position,Vector3.down,out hit, Mathf.Infinity)) {
			a.transform.position = hit.point+ new Vector3(0f,0.002f,0f);
		}
		a.transform.SetParent(this.transform);
		}
	}
	void OnTriggerStay(Collider col) {
		if (col.gameObject.name == "Char") {
			MainMovement m = col.gameObject.GetComponent<MainMovement>();
			if (m.Mstatus == MovementStatus.Normal) {
			if (Global.n_controls.Use.wasPressedThisFrame) {
				m.Mstatus = MovementStatus.Disabled;
				m.Anim.Play("Grab");
				m.Item = this.gameObject;
				m.Grab();
			}
			}
		}
	}
}
