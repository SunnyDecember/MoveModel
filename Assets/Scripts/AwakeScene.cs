using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AwakeScene : MonoBehaviour
{
	void Start ()
    {
        SceneManager.LoadSceneAsync("Joystick-Direct-SimpleFPS");
	}
}
