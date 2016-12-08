using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Altamedia : MonoBehaviour
{
    public string NextScene;
    void Awake()
    {
        AMGlobal.Settings.Load();
        AMGlobal.ProjectSettings.Load();

        DontDestroyOnLoad(gameObject);
    }
	
	void Start () {
        SceneManager.LoadScene(NextScene);

    }
	
	// Update is called once per frame
	void Update () {
            
	}


}




