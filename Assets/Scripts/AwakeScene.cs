using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AwakeScene : MonoBehaviour
{
    public Image _image;
    AsyncOperation async;

    void Awake()
    {
        
    }

	void Start ()
    {
        async = SceneManager.LoadSceneAsync("Joystick-Direct-SimpleFPS");
        async.completed += (a) =>
        {
            Debug.Log("1  " + async.progress);
        };
	}
	
	void Update ()
    {
        _image.transform.Rotate(0, 0, Time.deltaTime * 20);
        Debug.Log("2  " + async.progress);
    }
}
