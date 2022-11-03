using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSetter : MonoBehaviour
{
	//public Controls controls;
	//public bool UseDefault = true;
	public NewControls controls;
	public bool gamepad;
	public InventoryDB Item_Database;
    void Start()
    {
	   Global.Item_DB = Item_Database;
	   controls.UpdateInputs(0,gamepad);
       Global.n_controls = controls;
    }
	/*public void UseDefaultControls() {
		KeyCode[] codes = {KeyCode.RightArrow,KeyCode.LeftArrow,KeyCode.UpArrow,KeyCode.DownArrow,KeyCode.X,KeyCode.Z,KeyCode.C,KeyCode.A,KeyCode.S,KeyCode.D};
		Global.controls = new Controls(codes);
	}*/
}
