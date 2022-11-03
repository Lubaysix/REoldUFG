using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponAnimationPack {
	public string Name = "Unequiped";
	[Header("Set ItemID to -1 for no item animations")]
	public int ItemID = -1;
	public List<string> WalkStates;
	public List<string> RunStates;
	public List<string> IdleStates;
	[Header("Set all of the variables after empty if it's not an item")]
	public string Up_Aim,Straight_Aim,Down_Aim;
	public string Up_Attack,Straight_Attack,Down_Attack;
	public string Reload;
}
[CreateAssetMenu(fileName = "Character", menuName = "REUTILS/Character", order = 1)]
public class RE_Character : ScriptableObject
{
   public string CharacterName;
   public GameObject Model;
   public Animator Anim;
   public List<WeaponAnimationPack> Animations;
}
