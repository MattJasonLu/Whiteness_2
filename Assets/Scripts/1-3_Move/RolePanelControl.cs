using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RolePanelControl : MonoBehaviour { 

    public GameObject canvas;
    public RoleUnitCalculator roleUnitCalculator;

    private GameObject panel_1;
    private GameObject panel_2;
    private GameObject panel_3;
    private Text level;
    

    // Start is called before the first frame update
    void Start()
    {
        panel_1 = canvas.transform.Find("RolePanel/Panel_1").gameObject;
        panel_2 = canvas.transform.Find("RolePanel/Panel_2").gameObject;
        panel_3 = canvas.transform.Find("RolePanel/Panel_3").gameObject;
        level = canvas.transform.Find("RolePanel/Panel_1/Level").GetComponent<Text>();
        LoadPlayerLevel();
        Toggle_1();
    }

    public void Toggle_1()
    {
        panel_1.SetActive(true);
        panel_2.SetActive(false);
        panel_3.SetActive(false);
    }

    public void Toggle_2()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(true);
        panel_3.SetActive(false);
    }

    public void Toggle_3()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(false);
        panel_3.SetActive(true);
    }

    public void OnClock()
    {
        this.gameObject.SetActive(false);
    }

    public void LoadPlayerLevel()
    {
        PlayerPrefs.SetInt("PlayerExp_1", 10000);
        int playerExp_1 = PlayerPrefs.GetInt("PlayerExp_1");
        int level = roleUnitCalculator.GetPlayerLevelFromExp(playerExp_1);
        this.level.text = level.ToString();
    }
}
