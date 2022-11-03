using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]

public class ObjectNameUPD : MonoBehaviour
{
	public Object obj;
	public InventoryDB db;
    // Update is called once per frame
    void Update()
    {
		if (obj != null) {
			if (db != null) {
				obj.gameObject.name = "Pickup -- ID : " + obj.Itm.id.ToString()  + " / " + db.Items[obj.Itm.id].Name;
			}else {
				obj.gameObject.name = "Pickup -- ID : " + obj.Itm.id.ToString();
			}
		}
    }
}
