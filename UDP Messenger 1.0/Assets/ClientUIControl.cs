using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientUIControl : MonoBehaviour {

    public Toggle[] toggles;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        EventManager.OnSelectLevelOne += OnSelectLevelOne;
    }

    private void OnDisable()
    {
        EventManager.OnSelectLevelOne -= OnSelectLevelOne;
    }

    void OnSelectLevelOne(Config.LevelOne levelOne)
    {
    }
}
