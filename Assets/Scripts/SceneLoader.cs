using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
	float time;
	string scene;
	public void SceneLoadTimer(string _scene, float _time) {
		time = _time;
		scene = _scene;
		StartCoroutine(LoadSceneTimerIE());
	}
	IEnumerator LoadSceneTimerIE() {
		yield return new WaitForSeconds(time);
		SceneManager.LoadScene(scene);
	} 
}
