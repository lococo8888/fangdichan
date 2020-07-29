using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
    public Toggle[] toggles;
    public Image plane1;
    public Image plane2;
    public Image plane3;

    bool isClick1 = false;
    bool isClick2 = false;
    bool isClick3 = false;
    bool isClick4 = false;
    // Use this for initialization
    void Start () {
        toggles[0].onValueChanged.AddListener(OnSelect1);
        toggles[1].onValueChanged.AddListener(OnSelect2);
        toggles[2].onValueChanged.AddListener(OnSelect3);
        toggles[3].onValueChanged.AddListener(OnSelect4);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnSelect1(bool isVal)
    {
        if (isVal)
        {
            plane1.color = new Color(1, 1, 1, 1);
            OnSelectLevelOne(0);
        }
        else
        {
            plane1.color = new Color(0, 0, 0, 0);
        }
    }

    void OnSelect2(bool isVal)
    {
        if (isVal)
        {
            plane2.color = new Color(0, 0.2f, 1, 1);
            OnSelectLevelOne(1);
        }
        else
        {
            plane2.color = new Color(0, 0, 0, 0);
        }
    }

    void OnSelect3(bool isVal)
    {
        if (isVal)
        {
            plane3.color = new Color(0, 1, 0.2f, 1);
            OnSelectLevelOne(2);
        }
        else
        {
            plane3.color = new Color(0, 0, 0, 0);
        }
    }

    void OnSelect4(bool isVal)
    {
        HideAll();
        OnSelectLevelOne(3);
    }

    void HideAll()
    {
        plane1.color = new Color(0, 0, 0, 0);
        plane2.color = new Color(0, 0, 0, 0);
        plane3.color = new Color(0, 0, 0, 0);
    }

    void OnSelectLevelOne(int id)
    {
        if (EventManager.OnSendSelectLevelOne != null)
        {
            Config.LevelOne levelOne = new Config.LevelOne();
            EventManager.OnSendSelectLevelOne(levelOne);
        }
    }
}
