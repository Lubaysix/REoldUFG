using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InteractableType {
	Text,
	Door
}
public class Interactable : MonoBehaviour
{
    public string[] text;
	public InteractableType type;
	public string ifdoorscene;
	public Vector3 ifdoorplayerposition,ifdoorplayerangle;
	public int ifdoorid;
	public int ifdoorcamid;
}
