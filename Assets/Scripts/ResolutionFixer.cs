using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionFixer : MonoBehaviour
{
	public Camera mainCamera;
	public RenderTexture targetTexture;
	public GameObject MainCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (Global.SimulatePSXResolution) {
			
			mainCamera.targetTexture = targetTexture;
			MainCanvas.SetActive(true);
		}
    }
}
