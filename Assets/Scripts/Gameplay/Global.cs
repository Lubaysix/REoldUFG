using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public enum Control {
	DPadLeft,
	DPadRight,
	DPadUp,
	DPadDown,
	ButtonSouth,
	ButtonWest,
	ButtonEast,
	ButtonNorth,
	ButtonStart,
	ButtonSelect,
	RightShoulder,
	LeftShoulder,
	LeftTrigger,
	RightTrigger,
	KB_LeftArrow,
	KB_RightArrow,
	KB_UpArrow,
	KB_DownArrow,
	KB_Q,
	KB_W,
	KB_E,
	KB_R,
	KB_T,
	KB_Y,
	KB_U,
	KB_I,
	KB_O,
	KB_P,
	KB_A,
	KB_S,
	KB_D,
	KB_F,
	KB_G,
	KB_H,
	KB_J,
	KB_K,
	KB_L,
	KB_Z,
	KB_X,
	KB_C,
	KB_V,
	KB_B,
	KB_N,
	KB_M,
	KB_Intro,
	KB_LeftShift,
	KB_LeftCTRL,
	KB_RightCTRL,
	KB_RightShift,
	KB_Alt
}
[System.Serializable]
public class NewControls  {
	public Control T_Up,T_Left,T_Down,T_Right,T_Use,T_Run,T_Map,T_Status,T_Start,T_Aim,T_ChangeTarget;
	public bool GamePad;
	public UnityEngine.InputSystem.Controls.ButtonControl Up,Left,Down,Right,Use,Run,Map,Status,Start,Aim,ChangeTarget;
	public NewControls() {
		UpdateInputs(0,GamePad);
	}
	public void UpdateInputs(int GamepadID,bool g) {
		GamePad = g;
		Up = Do(T_Up,GamepadID);
		Down = Do(T_Down,GamepadID);
		Right = Do(T_Right,GamepadID);
		Left = Do(T_Left,GamepadID);
		Use = Do(T_Use,GamepadID);
		Run = Do(T_Run,GamepadID);
		Map = Do(T_Map,GamepadID);
		Status = Do(T_Status,GamepadID);
		Aim = Do(T_Aim,GamepadID);
		ChangeTarget = Do(T_ChangeTarget,GamepadID);
		Start = Do(T_Start,GamepadID);
	}
	UnityEngine.InputSystem.Controls.ButtonControl Do(Control c,int id) {
		if (GamePad) {
		if (Gamepad.all[id] == null) {
			return null;
		}else {
			Debug.Log(string.Join("\n", Gamepad.all));
		}
		
		if (c == Control.DPadLeft) {
			return Gamepad.all[id].dpad.left;
		}else if (c == Control.DPadRight) {
			return Gamepad.all[id].dpad.right;
		}else if (c == Control.DPadUp) {
			return Gamepad.all[id].dpad.up;
		}else if (c == Control.DPadDown) {
			return Gamepad.all[id].dpad.down;
		}else if (c == Control.ButtonSouth) {
			return Gamepad.all[id].buttonSouth;
		}else if (c == Control.ButtonWest) {
			return Gamepad.all[id].buttonWest;
		}else if (c == Control.ButtonNorth) {
			return Gamepad.all[id].buttonNorth;
		}else if (c == Control.ButtonEast) {
			return Gamepad.all[id].buttonEast;
		}else if (c == Control.ButtonStart) {
			return Gamepad.current.startButton;
		}else if (c == Control.ButtonSelect) {
			return Gamepad.all[id].selectButton;
		}else if (c == Control.RightShoulder) {
			return Gamepad.all[id].rightShoulder;
		}else if (c == Control.LeftShoulder) {
			return Gamepad.all[id].leftShoulder;
		}else if (c == Control.RightTrigger) {
			return Gamepad.all[id].rightTrigger;
		}else if (c == Control.LeftTrigger) {
			return Gamepad.all[id].leftTrigger;
		}
		}else { 
		if (c == Control.KB_UpArrow) {
			return Keyboard.current.upArrowKey;
		}else if (c == Control.KB_DownArrow) {
			return Keyboard.current.downArrowKey;
		}else if (c == Control.KB_RightArrow) {
			return Keyboard.current.rightArrowKey;
		}else if (c == Control.KB_LeftArrow) {
			return Keyboard.current.leftArrowKey;
		}else if (c == Control.KB_Z) {
			return Keyboard.current.zKey;
		}else if (c == Control.KB_X) {
			return Keyboard.current.xKey;
		}else if (c == Control.KB_C) {
			return Keyboard.current.cKey;
		}else if (c == Control.KB_V) {
			return Keyboard.current.vKey;
		}else if (c == Control.KB_A) {
			return Keyboard.current.aKey;
		}else if (c == Control.KB_S) {
			return Keyboard.current.sKey;
		}else if (c == Control.KB_D) {
			return Keyboard.current.dKey;
		}else if (c == Control.KB_F) {
			return Keyboard.current.fKey;
		}else if (c == Control.KB_Q) {
			return Keyboard.current.qKey;
		}else if (c == Control.KB_W) {
			return Keyboard.current.wKey;
		}else if (c == Control.KB_E) {
			return Keyboard.current.eKey;
		}else if (c == Control.KB_R) {
			return Keyboard.current.rKey;
		}else if (c == Control.KB_T) {
			return Keyboard.current.tKey;
		}else if (c == Control.KB_G) {
			return Keyboard.current.gKey;
		}else if (c == Control.KB_B) {
			return Keyboard.current.bKey;
		}else if (c == Control.KB_Y) {
			return Keyboard.current.yKey;
		}else if (c == Control.KB_H) {
			return Keyboard.current.hKey;
		}else if (c == Control.KB_N) {
			return Keyboard.current.nKey;
		}else if (c == Control.KB_U) {
			return Keyboard.current.uKey;
		}else if (c == Control.KB_J) {
			return Keyboard.current.jKey;
		}else if (c == Control.KB_M) {
			return Keyboard.current.mKey;
		}else if (c == Control.KB_I) {
			return Keyboard.current.iKey;
		}else if (c == Control.KB_K) {
			return Keyboard.current.kKey;
		}else if (c == Control.KB_O) {
			return Keyboard.current.oKey;
		}else if (c == Control.KB_L) {
			return Keyboard.current.lKey;
		}else if (c == Control.KB_P) {
			return Keyboard.current.pKey;
		}else if (c == Control.KB_LeftShift) {
			return Keyboard.current.leftShiftKey;
		}else if (c == Control.KB_LeftCTRL) {
			return Keyboard.current.leftCtrlKey;
		}else if (c == Control.KB_Alt) {
			return Keyboard.current.altKey;
		}else if (c == Control.KB_RightCTRL) {
			return Keyboard.current.rightCtrlKey;
		}else if (c == Control.KB_RightShift) {
			return Keyboard.current.rightShiftKey;
		}else {
			return null;
		}
		}
		return null;
	}
}
[System.Serializable]
public class Inv_Item {
	public int id;
	public int amount = 1;
	public int ammo = 0;
}
public class Save_Deleted {
	public Save_Deleted(GameObject g) {
		Room = SceneManager.GetActiveScene().name;
		ObjectName = g.name;
	}
	public string Room;
	public string ObjectName;
}
public static class Global
{
	
	public static NewControls n_controls;
	
	public static string STATUS_MENU_SCENE = "Status_Menu_SCN";
	// Gameplay Global Variables
	public static float MainCharHealth = 100f;
	
	public static int EquipedSlot = -1;
	
	public static bool SimulatePSXResolution = false;
	
	// Between Scene Control
	public static bool GoingBack;
	public static string lastScene = "TestRoom001";
	public static string gotoscene = "TestRoom001";
	public static List<BasicEnemy> Enemies = new List<BasicEnemy>();
	public static List<Save_Deleted> DeletedObjects = new List<Save_Deleted>();
	// Last Player Location
	public static Vector3 Main_Rot = new Vector3(0f,270f,0f);
	public static Vector3 Main_Pos = new Vector3(11.96f,1.46f,-9.56f);
	
	// Current Camera Variables
	public static int Cur_Camera_ID = 0;
	public static Camera Current_Camera;
	public static int Pending_Camera_ID = 0;
	
	// Inventory Config
	public static List<Inv_Item> Inventory = new List<Inv_Item>(); // Inventory Main List
	public static InventoryDB Item_DB;
	
	public static void PlayGlobalAudio2D(AudioClip clip) {
		if (GameObject.FindGameObjectWithTag("GlobalAP")) {
			GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>().PlayAudio (clip);
		}
	}
	public static void HealChar(float hp) {
		MainCharHealth += hp;
		if (MainCharHealth > 100f) {
			MainCharHealth = 100f;
		} 
	}
	public static void UpdateInventory() {
		Global.UpdateItemsARRAY();
		GameObject.Find("Inventory").GetComponent<InventoryMenu>().SpawnInventory(true);
	}
	public static void UpdateItemsARRAY() {
		for (int i = 0; i < Inventory.Count; i++) {
			if (Inventory[i].amount <= 0) {
				Inventory.RemoveAt(i);
			}
		}
	}
	public static void LoadSceneInTime(string scene, float time) {
		GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<SceneLoader>().SceneLoadTimer(scene,time);
		GoOutScene();
	}
	public static void GoOutScene() {
		GameObject[] a = GameObject.FindGameObjectsWithTag("Enemy");
		for (int e = 0; e < a.Length; e++) {
			Enemies.Add(a[e].GetComponent<BasicEnemy>());
		}
	}
	public static void EnterStatusMenu(AudioClip EnterMenuSound) {
		MainCharHealth = GameObject.Find("Char").GetComponent<MainMovement>().LeonCP.Health;
		GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>().PlayAudio2D(EnterMenuSound);
		Main_Pos = GameObject.Find("Char").transform.position;
		Main_Rot = GameObject.Find("Char").transform.eulerAngles;
		lastScene = SceneManager.GetActiveScene().name;
		GoOutScene();
		GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<SceneLoader>().SceneLoadTimer(STATUS_MENU_SCENE,1f);
		GameObject.Find("MainUI").GetComponent<UIScript>().Fade(true);
	}
	public static void GoBackGameplay(AudioClip GoBackSound) {
		GoingBack = true;
		GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<GlobalAP>().PlayAudio3D(GoBackSound, new Vector3(0f,1f,-10f));
		GameObject.FindGameObjectWithTag("GlobalAP").GetComponent<SceneLoader>().SceneLoadTimer(lastScene,1f);
		GameObject.Find("MainUI").GetComponent<UIScript>().Fade(true);
	}
}
