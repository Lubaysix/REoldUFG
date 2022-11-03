using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
public class Controls {
	
	public KeyCode Right,Left,Forward,Back;
	public KeyCode Run, Interact, Aim, Status, Map, Pause;
	public Controls(KeyCode[] codes) {
		Right = codes[0];
		Left = codes[1];
		Forward = codes[2];
		Back = codes[3];
		Run = codes[4];
		Interact = codes[5];
		Aim = codes[6];
		Status = codes[7];
		Map = codes[8];
		Pause = codes[9];
	}
}
[System.Serializable]
public class CharPreset {
	public float WalkSpeedN,RunSpeedN;
	public float WalkSpeedD,RunSpeedD;
	public float WalkSpeedN1,WalkSpeedD1;
	public float TurnSpeed;
	public float Health = 100f;
	public bool EnemyNear,Hurt,Using,Reloading;
}
public enum Status {
	Fine,
	Caution,
	CautionD,
	Danger
}
public enum MovementStatus {
	Normal,
	Aim,
	Disabled,
	WaitingAction
}
public class MainMovement : MonoBehaviour
{
	[Header("Llaslfdsajfiposad")]
	
	public RE_Character Self_Animation_DB;
	public Animator Anim;
	public Status Current_Status;
	public KeyCode Hurt;
	public MovementStatus Mstatus;
	public bool Attacking;
	public CharPreset LeonCP;
	public AudioSource SourceAS;
	public AudioClip AttackKnifeCLP,AttackKnifeStabCLP,DeathSound;
	public GameObject Blood;
	public int Randomness;
	public Camera Current_Camera;
	public CScript CS;
	public UIScript UI;
	public bool Dead;
	public WeaponController weapons;
	public bool canInteract = true;
	float spd;
	public float RDistance;
	public bool CanRun = true;
	public GameObject Item;
	public bool Wall;
	public RPGText IntUI;
	public AudioClip Accept;
	float animspd = 1f;
	Vector3 p;
	float rot;
	int ind;
    // Start is called before the first frame update
    void Start()
    {
		ind = SearchForIDInSelf(Global.EquipedSlot);
		if (ind >= 0) {
			Debug.Log("<color=green>Char | Character initialized succesfully</color>");
		}else {
			Debug.LogError("<color=red>Char | There was an error initializing the Main Character (ErrorID = 0, ind variable is less than 0, this variable is important for animations to work!)</color>");
		}
		
		this.gameObject.name = "Char";
        if (Global.GoingBack) {
			this.transform.position = Global.Main_Pos;
			this.transform.eulerAngles = Global.Main_Rot;
			LeonCP.Health = Global.MainCharHealth;
		}
		rot = this.transform.eulerAngles.y;
		
    }
	void ItemGrab() {
		
		UI.ShowItem(Item.GetComponent<Object>().Itm.id,2);
		Mstatus = MovementStatus.WaitingAction;
		IntUI.type = TextType.YesNo;
		IntUI.WriteSingleLine("Will you pickup '"+Global.Item_DB.Items[Item.GetComponent<Object>().Itm.id].Name+"'?");
	}
	public void Grab() {
		if (!LeonCP.Using) {
		LeonCP.Using = true;
		Invoke("ItemGrab",1.5f);
		}
	}
	public int SearchForIDInSelf(int id) {
		int a = Self_Animation_DB.Animations.FindIndex(x => x.ItemID == id);
		if (a != -1) {
			return a;
		}else {
			return a;
		}
		return -1;
	}
	string Calc_Health(string Animation, float Health,bool Backwards) {
		if (Backwards) 
		{
			if (LeonCP.Health >= 75) {
				return "Walk_Back";
			}
			if (LeonCP.Health < 75 && LeonCP.Health >= 25) {
				return "Walk_Caution"+"_Back";
			}
			if (LeonCP.Health < 25 && LeonCP.Health > 0) {
				return "Walk_Danger"+"_Back";
			}
		}
		else 
		{
			
			if (Animation == "Walk") {
				if (LeonCP.Health >= 75) {
					return Self_Animation_DB.Animations[ind].WalkStates[0];
				}
				if (LeonCP.Health < 75 && LeonCP.Health >= 25) {
					return Self_Animation_DB.Animations[ind].WalkStates[1];
				}
				if (LeonCP.Health < 25 && LeonCP.Health > 0) {
					return Self_Animation_DB.Animations[ind].WalkStates[2];
				}
			} else if (Animation == "Run") {
				if (LeonCP.Health >= 75) {
					return Self_Animation_DB.Animations[ind].RunStates[0];
				}
				if (LeonCP.Health < 75 && LeonCP.Health >= 25) {
					return Self_Animation_DB.Animations[ind].RunStates[1];
				}
				if (LeonCP.Health < 25 && LeonCP.Health > 0) {
					return Self_Animation_DB.Animations[ind].RunStates[2];
				}
			} else {
				if (LeonCP.Health >= 75) {
					return Animation;
				}
				if (LeonCP.Health < 75 && LeonCP.Health >= 25) {
					return Animation+"_Caution";
				}
				if (LeonCP.Health < 25 && LeonCP.Health > 0) {
					return Animation+"_Danger";
				}	
			}
			
		}
		return "";
	}
	string ChooseAnimationMainChar(string Animation, float Health) {
		if (LeonCP.Health <= 0) {
			return "Dead_Normally";	
		}
		else if (Animation == "Turn") {
			if (LeonCP.Health >= 75) {
				return Self_Animation_DB.Animations[ind].WalkStates[0];
			}
			if (LeonCP.Health < 75 && LeonCP.Health > 0) {
				return Self_Animation_DB.Animations[ind].WalkStates[1];
			}
		}else if (Animation == "Back") {
			if (LeonCP.EnemyNear) {
				return "WalkBackEnemyNear";
			}else {
				return Calc_Health(Animation,Health,true);
			}
		} else {
			return Calc_Health(Animation,Health,false);
		}
		return "";
	}
	void PlayAnimation(float speed, float transition_speed, string animation) {
		if (!Anim.IsInTransition(0) && !Anim.GetCurrentAnimatorStateInfo(0).IsName(ChooseAnimationMainChar(animation,LeonCP.Health))) {
				Anim.CrossFadeInFixedTime(ChooseAnimationMainChar(animation,LeonCP.Health),transition_speed,0);
				Anim.speed = animspd;
		}
		
	}
	void PlayAnimationChoose(float speed, float transition_speed, string animation) {
		
		if (!Anim.IsInTransition(0) && !Anim.GetCurrentAnimatorStateInfo(0).IsName(animation)) {
				Anim.CrossFadeInFixedTime(animation,transition_speed,0);
				Anim.speed = speed;
		}
		
	}
	void GetRight() {
		Mstatus = MovementStatus.Normal;
		LeonCP.Hurt = false;
	}
	
	void OnCollisionEnter(Collision col) {
		
		if (col.gameObject.tag == "Enemy") {
			Debug.Log(this.transform.eulerAngles.y-col.gameObject.transform.eulerAngles.y);
			if ((this.transform.eulerAngles.y-col.gameObject.transform.eulerAngles.y)-90f > 179f) {
				GetHurt(-1,col.gameObject.GetComponent<BasicEnemy>().damage);
			}else {
				GetHurt(1,col.gameObject.GetComponent<BasicEnemy>().damage);
			}
			
		}
	}
	public void GetHurt(int type, float damage) {
		if (!Dead) {
		LeonCP.Health -= damage;
		LeonCP.Hurt = true;
		Mstatus = MovementStatus.Disabled;
		if (type == 1) {
			Anim.Play("Hurt_Front");
		}
		else if (type == -1) {
			Anim.Play("Hurt_Back");
		} 
		int r = Random.Range(1,Randomness);
		for (int i = 0; i < r; i++) {
			Instantiate(Blood,this.transform.position+new Vector3(Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f),Random.Range(-0.25f,0.25f)),Quaternion.identity);
		}
		Invoke("GetRight",0.75f);
		}
	}
	void Update2() {
		if (Global.n_controls.Status.wasPressedThisFrame && LeonCP.Health > 0) {
			Global.EnterStatusMenu(Accept);
		}
	}
	public void UnGrab(bool yes) {
		if (Item != null) {
				
				if (yes) {
					int i = Global.Inventory.FindIndex(x => x.id == Item.GetComponent<Object>().Itm.id && x.amount < Global.Item_DB.Items[Item.GetComponent<Object>().Itm.id].maxAmount);
					if (i != -1) {
					Debug.Log("<color=green>Found same item in inventory, adding amount!</color> (Picked up item name : "+Global.Item_DB.Items[Item.GetComponent<Object>().Itm.id].Name+", with an amount of "+Item.GetComponent<Object>().Itm.amount.ToString()+")");
						if (Item.GetComponent<Object>().Itm.amount+Global.Inventory[i].amount > Global.Item_DB.Items[Item.GetComponent<Object>().Itm.id].maxAmount) {
							Debug.Log("<color=green>Item amount picked is greater than the item maxAmount, splitting! </color> (Picked up item name : "+Global.Item_DB.Items[Item.GetComponent<Object>().Itm.id].Name+", with an amount of "+Item.GetComponent<Object>().Itm.amount.ToString()+")");
							int a,b;
							a = Mathf.Abs(Global.Item_DB.Items[Item.GetComponent<Object>().Itm.id].maxAmount-(Global.Inventory[i].amount+Item.GetComponent<Object>().Itm.amount));
							b = Item.GetComponent<Object>().Itm.amount-a;
							Global.Inventory[i].amount += b; 
							Inv_Item c = new Inv_Item();
							c.id = Item.GetComponent<Object>().Itm.id;
							c.amount = a;
							c.ammo = Global.Item_DB.Items[Item.GetComponent<Object>().Itm.id].initialAmmo;
							Global.Inventory.Add(c);
						}else {
							Global.Inventory[i].amount += Item.GetComponent<Object>().Itm.amount; 	
							
						}
					
					}else {
					Debug.Log("<color=green>Item not found in current inventory!, adding to it!</color> (Picked up item name : "+Global.Item_DB.Items[Item.GetComponent<Object>().Itm.id].Name+", with an amount of "+Item.GetComponent<Object>().Itm.amount.ToString()+")");
					Global.Inventory.Add(Item.GetComponent<Object>().Itm);
					
					}
					Global.UpdateItemsARRAY();
					Global.DeletedObjects.Add(new Save_Deleted(Item));
					Destroy(Item);
					LeonCP.Using = false;
					UI.UnShowItem(2);
					Mstatus = MovementStatus.Normal;
					Item = null;
				}else {
					LeonCP.Using = false;
					UI.UnShowItem(2);
					Mstatus = MovementStatus.Normal;
					Item = null;
				}
			}	
	}
    // Update is called once per frame
    void Update()
    {
		if (Mstatus == MovementStatus.WaitingAction && Item != null) {
			if (Global.n_controls.Use.wasPressedThisFrame) {
				
			}
		} 
		if (Global.Current_Camera != null) {
		Debug.Log(Global.Current_Camera.gameObject.name);
		}
		if (!Attacking && !LeonCP.Reloading) {
			Update2();
			this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,Mathf.LerpAngle(this.transform.eulerAngles.y,rot,10f*Time.deltaTime),this.transform.eulerAngles.z);
		}
		
		if (LeonCP.Using) {
			if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Grab")) {
			Anim.Play("Grab");
			}
		}
		RaycastHit hit;
		if (Physics.Raycast(this.transform.position,this.transform.forward,out hit,RDistance+(spd*Time.deltaTime))) {
			
				if (hit.collider.tag == "Interactable") {
					Debug.Log("<color=green>Collision With Interactable</color>");
					if (Global.n_controls.Use.wasPressedThisFrame && Mstatus == MovementStatus.Normal && canInteract) {
						if (hit.collider.GetComponent<Interactable>().type == InteractableType.Door) {
							Global.GoingBack = true;
							Global.Main_Pos = hit.collider.GetComponent<Interactable>().ifdoorplayerposition;
							Global.Main_Rot = hit.collider.GetComponent<Interactable>().ifdoorplayerangle;
							Global.gotoscene = hit.collider.GetComponent<Interactable>().ifdoorscene;
							Global.MainCharHealth = LeonCP.Health;
							GameObject.Find("MainUI").GetComponent<UIScript>().Fade(true);
							Global.Pending_Camera_ID = hit.collider.GetComponent<Interactable>().ifdoorcamid;
							Global.LoadSceneInTime("DoorTransition_"+hit.collider.GetComponent<Interactable>().ifdoorid.ToString(),1f);
							Mstatus = MovementStatus.Disabled;
							
						}else {
						IntUI.Write(hit.collider.GetComponent<Interactable>().text);
						IntUI.type = TextType.Normal;
						}
					}//-12.31 1.46 -16.66
				}		
				Wall = true;
			
		}else {
			Wall = false;
		}
		if (!LeonCP.Reloading && !LeonCP.Using) {
		Global.Current_Camera = Current_Camera;
		this.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f);
		if (Mstatus == MovementStatus.WaitingAction) {
			if (Global.n_controls.Use.wasPressedThisFrame) {
				Mstatus = MovementStatus.Normal;
				
			}
		}
		if (LeonCP.Health <= 0 && !Dead) {
			
			SourceAS.clip = DeathSound;
			SourceAS.Play();
			Anim.Play("Dead_Normally");
			CS.Dead();
			Dead = true;
		}
		if (Dead) {
			GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = Mathf.Lerp(GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume,0f,0.2f);
			Mstatus = MovementStatus.Disabled;
		}
		if (Mstatus == MovementStatus.Disabled) {
			spd = Mathf.Lerp(spd,0f,0.5f);
			if (!LeonCP.Hurt && !LeonCP.Using) {
			PlayAnimation(1f,0.1f,"Idle");
			}
		}
		this.transform.Translate(0f,0f,spd * Time.deltaTime);
		if (!LeonCP.Hurt && Mstatus != MovementStatus.Aim && !LeonCP.Using && spd >= -0.05f && spd <= 0.05f && !Global.n_controls.Left.isPressed && !Global.n_controls.Right.isPressed) {
			PlayAnimation(1f,0.1f,"Idle");
		} 
		if (Mstatus != MovementStatus.Disabled && Mstatus != MovementStatus.WaitingAction){
		
        if (Global.n_controls.Right.isPressed) {
			if (Mstatus != MovementStatus.Aim) {
			if (!Anim.GetCurrentAnimatorStateInfo(0).IsName(ChooseAnimationMainChar("Run",LeonCP.Health)) && !Anim.GetCurrentAnimatorStateInfo(0).IsName(ChooseAnimationMainChar("Back",LeonCP.Health))) {
				if (spd <= 0.1f) {
				PlayAnimation(1f,0.25f,"Turn");
				}
			}
		}
		if (!Attacking) {
		//this.transform.Rotate(0f,LeonCP.TurnSpeed * Time.deltaTime,0f);
			rot += LeonCP.TurnSpeed;
		}
		}
		if (Global.n_controls.Left.isPressed) {
			if (Mstatus != MovementStatus.Aim) {
			if (!Anim.GetCurrentAnimatorStateInfo(0).IsName(ChooseAnimationMainChar("Run",LeonCP.Health)) && !Anim.GetCurrentAnimatorStateInfo(0).IsName(ChooseAnimationMainChar("Back",LeonCP.Health))) {
				if (spd <= 0.1f) {
				PlayAnimation(1f,0.25f,"Turn");
				}
			}
			}
			if (!Attacking) {
			rot -= LeonCP.TurnSpeed;
			}
		}
		if (Global.n_controls.Run.wasPressedThisFrame && Global.n_controls.Down.isPressed) {
			if (!Global.n_controls.Right.isPressed && !Global.n_controls.Left.isPressed) {
				rot += 180f;
			}
		}
		
		if (Mstatus != MovementStatus.Aim && !LeonCP.Reloading) {
		if (Global.n_controls.Down.isPressed) {
			if (LeonCP.Health > 25) {
			spd = Mathf.Lerp(spd,-LeonCP.WalkSpeedN,0.1f);
			}else {
			spd = Mathf.Lerp(spd,-LeonCP.WalkSpeedD,0.1f);	
			}
			
			PlayAnimation(1f,0.2f,"Back");
		}
		if (Global.n_controls.Up.isPressed && !Global.n_controls.Run.isPressed) {
			if (LeonCP.Health > 25) {
			spd = Mathf.Lerp(spd,LeonCP.WalkSpeedN,0.1f);
			}else {
			spd = Mathf.Lerp(spd,LeonCP.WalkSpeedD,0.1f);	
			}
			
		}
	
		if (Global.n_controls.Up.isPressed && Global.n_controls.Run.isPressed && CanRun) {
			if (LeonCP.Health > 25) {
			spd = Mathf.Lerp(spd,LeonCP.RunSpeedN,0.2f);
			}else {
			spd = Mathf.Lerp(spd,LeonCP.RunSpeedD,0.1f);	
			}
		}
		
		if (LeonCP.Health < 25) {
			if (spd > 1.5f) {
			PlayAnimation(1f,0.2f,"Run");
			} else if (spd >= 0.02 && spd < 4f) {
			PlayAnimation(1f,0.2f,"Walk");
			} else {}
		}else {
			if (spd > 4f) {
			PlayAnimation(1f,0.2f,"Run");
			} else if (spd >= 0.02 && spd < 4f) {
			PlayAnimation(1f,0.2f,"Walk");
			} else {}
		}
		
		
		if (!Global.n_controls.Up.isPressed && !Global.n_controls.Down.isPressed) {
			spd = Mathf.Lerp(spd,0f,0.5f);
			if (!Global.n_controls.Right.isPressed && !Global.n_controls.Left.isPressed && !LeonCP.Using && !LeonCP.Hurt) {
				if (spd <= 0.15f) {
				PlayAnimation(1f,0.3f,"Idle");
				}
			}
		}
		Attacking = false;
		}else {
			spd = Mathf.Lerp(spd,0f,0.5f);
		}
		
		if (Global.n_controls.ChangeTarget.wasPressedThisFrame) {
			GetHurt(1,26);
			SourceAS.clip = AttackKnifeStabCLP;
			SourceAS.Play();
		}
		if(!Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Knife_Down") && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Knife_Front") && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Knife_Up")) {
		Attacking = false;
		}
		
		
	  }
		}
	  p = this.transform.position;
	  if (!Dead) {
		UpdateAim();
		
		if (Self_Animation_DB.Animations[ind].Reload != "" && !Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Reload)) {
			LeonCP.Reloading = false;
		}
		if (Self_Animation_DB.Animations[ind].Reload != "" && Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Reload)) {
			LeonCP.Reloading = true;
		}
	  }
	  
    }
	void UpdateAim() {
		if (Global.n_controls.Aim.isPressed) {
		if (Global.EquipedSlot != -1) {
			Mstatus = MovementStatus.Aim;
			
		
		
					if (Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Down_Attack) || Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Straight_Attack) || Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Up_Attack)) {
						Attacking = true;
					}
					if (Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Up_Aim) || Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Straight_Aim) || Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Up_Aim)) {
						Attacking = false;
				
					}
					if (!LeonCP.Reloading) {
					if (Global.n_controls.Up.isPressed) {
						if (!Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Up_Aim) && !Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Up_Attack)) {
							if (!Attacking) {
								PlayAnimationChoose(1f,0.1f,Self_Animation_DB.Animations[ind].Up_Aim);
							}
							
						}
						if (Global.n_controls.Use.wasPressedThisFrame && !Attacking) {
							int a = Global.Inventory.FindIndex(x => x.id == Global.EquipedSlot);
							if (a != -1) {
								if (Global.Item_DB.Items[Global.EquipedSlot].type == Item_Type.Weapon) {
								if (Global.Inventory[a].ammo > 0) {
									Global.Inventory[a].ammo--;
									Anim.Play(Self_Animation_DB.Animations[ind].Up_Attack);
									
									int abc = weapons.WPList.FindIndex(x => x.id == Global.EquipedSlot);
									if (abc != -1) {
										weapons.WPList[abc].gb.GetComponent<Gun>().Shoot();
									}
									
									SourceAS.clip = Global.Item_DB.Items[Self_Animation_DB.Animations[ind].ItemID].AttackSClip;
									SourceAS.Play();
								}else {
									SourceAS.clip = Global.Item_DB.Items[Self_Animation_DB.Animations[ind].ItemID].NoAmmoSClip;
									SourceAS.Play();
								}
								}else {
								Anim.Play(Self_Animation_DB.Animations[ind].Up_Attack);
								SourceAS.clip = Global.Item_DB.Items[Self_Animation_DB.Animations[ind].ItemID].AttackSClip;
								SourceAS.Play();
								}
							}
						}
					}
					else if (Global.n_controls.Down.isPressed) {
						if (!Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Down_Aim) && !Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Down_Attack))  {
							if (!Attacking) {
								PlayAnimationChoose(1f,0.1f,Self_Animation_DB.Animations[ind].Down_Aim);
							}
						}
						if (Global.n_controls.Use.wasPressedThisFrame && !Attacking) {
							int a = Global.Inventory.FindIndex(x => x.id == Global.EquipedSlot);
							if (a != -1) {
								if (Global.Item_DB.Items[Global.EquipedSlot].type == Item_Type.Weapon) {
								if (Global.Inventory[a].ammo > 0) {
									Global.Inventory[a].ammo--;
									Anim.Play(Self_Animation_DB.Animations[ind].Down_Attack);
									
									int abc = weapons.WPList.FindIndex(x => x.id == Global.EquipedSlot);
									if (abc != -1) {
										weapons.WPList[abc].gb.GetComponent<Gun>().Shoot();
									}
									
									SourceAS.clip = Global.Item_DB.Items[Self_Animation_DB.Animations[ind].ItemID].AttackSClip;
									SourceAS.Play();
								}else {
									SourceAS.clip = Global.Item_DB.Items[Self_Animation_DB.Animations[ind].ItemID].NoAmmoSClip;
									SourceAS.Play();
								}
								}else {
								Anim.Play(Self_Animation_DB.Animations[ind].Down_Attack);
								SourceAS.clip = Global.Item_DB.Items[Self_Animation_DB.Animations[ind].ItemID].AttackSClip;
								SourceAS.Play();
								}
							}
						}
					} else {
						if (!Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Straight_Aim) && !Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Straight_Attack))  {
							if (!Attacking && !LeonCP.Reloading) {
								if (Self_Animation_DB.Animations[ind].Reload != "" && !Anim.GetCurrentAnimatorStateInfo(0).IsName(Self_Animation_DB.Animations[ind].Reload)) {

									PlayAnimationChoose(1f,0.1f,Self_Animation_DB.Animations[ind].Straight_Aim);
								
								}
								if (Self_Animation_DB.Animations[ind].Reload == "") {
								
									PlayAnimationChoose(1f,0.1f,Self_Animation_DB.Animations[ind].Straight_Aim);
								
								}
							}
						}
						if (Global.n_controls.Run.wasPressedThisFrame ) {
							Reload();
							
						}
						if (Global.n_controls.Use.wasPressedThisFrame && !Attacking) {
							
							int a = Global.Inventory.FindIndex(x => x.id == Global.EquipedSlot);
							if (a != -1) {
								if (Global.Item_DB.Items[Global.EquipedSlot].type == Item_Type.Weapon) {
								if (Global.Inventory[a].ammo > 0) {
									Global.Inventory[a].ammo--;
									Anim.Play(Self_Animation_DB.Animations[ind].Straight_Attack);
									
									int abc = weapons.WPList.FindIndex(x => x.id == Global.EquipedSlot);
									if (abc != -1) {
										weapons.WPList[abc].gb.GetComponent<Gun>().Shoot();
									}
									
									SourceAS.clip = Global.Item_DB.Items[Self_Animation_DB.Animations[ind].ItemID].AttackSClip;
									SourceAS.Play();
								}else {
									SourceAS.clip = Global.Item_DB.Items[Self_Animation_DB.Animations[ind].ItemID].NoAmmoSClip;
									SourceAS.Play();
								}
								}else {
								Anim.Play(Self_Animation_DB.Animations[ind].Straight_Attack);
								SourceAS.clip = Global.Item_DB.Items[Self_Animation_DB.Animations[ind].ItemID].AttackSClip;
								SourceAS.Play();
								}
							}
						}
					}
				}
		}
			
				
			
		}else {
			if(!Attacking) {
			Mstatus = MovementStatus.Normal;
			
			
			}
		}
	}
	public void Reload() 
	{
		if (Self_Animation_DB.Animations[ind].Reload != "") {
		int a = Global.Inventory.FindIndex(x => x.id == Global.EquipedSlot);
		int b = Global.Inventory.FindIndex(x => x.id == Global.Item_DB.Items[Global.EquipedSlot].AmmoItemID);
			if (a != -1 && Global.Inventory[b].amount > 0 && Global.Inventory[a].ammo < Global.Item_DB.Items[Global.EquipedSlot].maxAmmo) {
				if (b != -1) {
			    LeonCP.Reloading = true;
				PlayAnimationChoose(1f,0.25f,Self_Animation_DB.Animations[ind].Reload);
				SourceAS.clip = Global.Item_DB.Items[Self_Animation_DB.Animations[ind].ItemID].ReloadSClip;
				SourceAS.Play();
				int matha = Mathf.Abs(Global.Item_DB.Items[Global.EquipedSlot].maxAmmo-(Global.Inventory[a].ammo+Global.Inventory[b].amount));
				int mathb = Mathf.Abs(Global.Inventory[b].amount-matha);
				Debug.LogWarning(matha+"/"+mathb);
				Global.Inventory[a].ammo += mathb;
				Global.Inventory[b].amount -= mathb;
				}
			}
		}
	}
}


