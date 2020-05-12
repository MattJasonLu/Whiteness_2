using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RolePanelControl : MonoBehaviour { 

    public GameObject canvas;
    public DBCalculator roleUnitCalculator;

    private GameObject panel_1;
    private GameObject panel_2;
    private GameObject panel_3;
    // 面板1信息
    private Text level_1;
    private Text name_1;
    private Text exp_1;
    private Text nextLevelExp_1;
    private Text initHP_1;
    private Text initEP_1;
    private Text initCP_1;
    private Text STR_1;
    private Text DEF_1;
    private Text ATS_1;
    private Text ADF_1;
    private Text SPD_1;
    private Text CRT_1;
    private Text DEX_1;
    private Text HIT_1;
    private Text LKY_1;
    // 面板2信息
    private Text level_2;
    private Text name_2;
    private Text exp_2;
    private Text nextLevelExp_2;
    private Text initHP_2;
    private Text initEP_2;
    private Text initCP_2;
    private Text STR_2;
    private Text DEF_2;
    private Text ATS_2;
    private Text ADF_2;
    private Text SPD_2;
    private Text CRT_2;
    private Text DEX_2;
    private Text HIT_2;
    private Text LKY_2;
    // 面板3信息
    private Text level_3;
    private Text name_3;
    private Text exp_3;
    private Text nextLevelExp_3;
    private Text initHP_3;
    private Text initEP_3;
    private Text initCP_3;
    private Text STR_3;
    private Text DEF_3;
    private Text ATS_3;
    private Text ADF_3;
    private Text SPD_3;
    private Text CRT_3;
    private Text DEX_3;
    private Text HIT_3;
    private Text LKY_3;


    // Start is called before the first frame update
    void Start()
    {
        // 面板1信息
        panel_1 = canvas.transform.Find("RolePanel/Panel_1").gameObject;
        panel_2 = canvas.transform.Find("RolePanel/Panel_2").gameObject;
        panel_3 = canvas.transform.Find("RolePanel/Panel_3").gameObject;
        level_1 = canvas.transform.Find("RolePanel/Panel_1/Level").GetComponent<Text>();
        name_1 = canvas.transform.Find("RolePanel/Panel_1/Name").GetComponent<Text>();
        exp_1 = canvas.transform.Find("RolePanel/Panel_1/Exp").GetComponent<Text>();
        nextLevelExp_1 = canvas.transform.Find("RolePanel/Panel_1/NextLevelExp").GetComponent<Text>();
        initHP_1 = canvas.transform.Find("RolePanel/Panel_1/InitHP").GetComponent<Text>();
        initEP_1 = canvas.transform.Find("RolePanel/Panel_1/InitEP").GetComponent<Text>();
        initCP_1 = canvas.transform.Find("RolePanel/Panel_1/InitCP").GetComponent<Text>();
        STR_1 = canvas.transform.Find("RolePanel/Panel_1/STR").GetComponent<Text>();
        DEF_1 = canvas.transform.Find("RolePanel/Panel_1/DEF").GetComponent<Text>();
        ATS_1 = canvas.transform.Find("RolePanel/Panel_1/ATK").GetComponent<Text>();
        ADF_1 = canvas.transform.Find("RolePanel/Panel_1/ADF").GetComponent<Text>();
        SPD_1 = canvas.transform.Find("RolePanel/Panel_1/SPD").GetComponent<Text>();
        CRT_1 = canvas.transform.Find("RolePanel/Panel_1/CRT").GetComponent<Text>();
        DEX_1 = canvas.transform.Find("RolePanel/Panel_1/DEX").GetComponent<Text>();
        HIT_1 = canvas.transform.Find("RolePanel/Panel_1/HIT").GetComponent<Text>();
        LKY_1 = canvas.transform.Find("RolePanel/Panel_1/LKY").GetComponent<Text>();
        // 面板2信息
        level_2 = canvas.transform.Find("RolePanel/Panel_2/Level").GetComponent<Text>();
        name_2 = canvas.transform.Find("RolePanel/Panel_2/Name").GetComponent<Text>();
        exp_2 = canvas.transform.Find("RolePanel/Panel_2/Exp").GetComponent<Text>();
        nextLevelExp_2 = canvas.transform.Find("RolePanel/Panel_2/NextLevelExp").GetComponent<Text>();
        initHP_2 = canvas.transform.Find("RolePanel/Panel_2/InitHP").GetComponent<Text>();
        initEP_2 = canvas.transform.Find("RolePanel/Panel_2/InitEP").GetComponent<Text>();
        initCP_2 = canvas.transform.Find("RolePanel/Panel_2/InitCP").GetComponent<Text>();
        STR_2 = canvas.transform.Find("RolePanel/Panel_2/STR").GetComponent<Text>();
        DEF_2 = canvas.transform.Find("RolePanel/Panel_2/DEF").GetComponent<Text>();
        ATS_2 = canvas.transform.Find("RolePanel/Panel_2/ATK").GetComponent<Text>();
        ADF_2 = canvas.transform.Find("RolePanel/Panel_2/ADF").GetComponent<Text>();
        SPD_2 = canvas.transform.Find("RolePanel/Panel_2/SPD").GetComponent<Text>();
        CRT_2 = canvas.transform.Find("RolePanel/Panel_2/CRT").GetComponent<Text>();
        DEX_2 = canvas.transform.Find("RolePanel/Panel_2/DEX").GetComponent<Text>();
        HIT_2 = canvas.transform.Find("RolePanel/Panel_2/HIT").GetComponent<Text>();
        LKY_2 = canvas.transform.Find("RolePanel/Panel_2/LKY").GetComponent<Text>();
        // 面板3信息
        level_3 = canvas.transform.Find("RolePanel/Panel_3/Level").GetComponent<Text>();
        name_3 = canvas.transform.Find("RolePanel/Panel_3/Name").GetComponent<Text>();
        exp_3 = canvas.transform.Find("RolePanel/Panel_3/Exp").GetComponent<Text>();
        nextLevelExp_3 = canvas.transform.Find("RolePanel/Panel_3/NextLevelExp").GetComponent<Text>();
        initHP_3 = canvas.transform.Find("RolePanel/Panel_3/InitHP").GetComponent<Text>();
        initEP_3 = canvas.transform.Find("RolePanel/Panel_3/InitEP").GetComponent<Text>();
        initCP_3 = canvas.transform.Find("RolePanel/Panel_3/InitCP").GetComponent<Text>();
        STR_3 = canvas.transform.Find("RolePanel/Panel_3/STR").GetComponent<Text>();
        DEF_3 = canvas.transform.Find("RolePanel/Panel_3/DEF").GetComponent<Text>();
        ATS_3 = canvas.transform.Find("RolePanel/Panel_3/ATK").GetComponent<Text>();
        ADF_3 = canvas.transform.Find("RolePanel/Panel_3/ADF").GetComponent<Text>();
        SPD_3 = canvas.transform.Find("RolePanel/Panel_3/SPD").GetComponent<Text>();
        CRT_3 = canvas.transform.Find("RolePanel/Panel_3/CRT").GetComponent<Text>();
        DEX_3 = canvas.transform.Find("RolePanel/Panel_3/DEX").GetComponent<Text>();
        HIT_3 = canvas.transform.Find("RolePanel/Panel_3/HIT").GetComponent<Text>();
        LKY_3 = canvas.transform.Find("RolePanel/Panel_3/LKY").GetComponent<Text>();
        SetPlayerInfo();
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

    public void SetPlayerInfo()
    {
        // TODO 获取方式需要调整
        PlayerPrefs.SetInt("PlayerExp_1", 10000);
        PlayerPrefs.SetString("PlayerUnitId_1", "P001");

        // Sprite图片后期需设置
        string playerUnitId_1 = PlayerPrefs.GetString("PlayerUnitId_1");
        int playerExp_1 = PlayerPrefs.GetInt("PlayerExp_1");
        if (playerUnitId_1 != null && playerUnitId_1 != "")
        {
            RoleUnitDAO roleUnit_1 = roleUnitCalculator.GetRoleUnitByIdAndExp(playerUnitId_1, playerExp_1);
            nextLevelExp_1.text = roleUnitCalculator.GetNextLevelExp(playerExp_1).ToString();
            level_1.text = roleUnit_1.level.ToString();
            name_1.text = roleUnit_1.unitName;
            initHP_1.text = roleUnit_1.initHP.ToString();
            initEP_1.text = roleUnit_1.initEP.ToString();
            initCP_1.text = roleUnit_1.initCP.ToString();
            exp_1.text = roleUnit_1.EXP.ToString();
            STR_1.text = roleUnit_1.STR.ToString();
            DEF_1.text = roleUnit_1.DEF.ToString();
            ATS_1.text = roleUnit_1.ATS.ToString();
            ADF_1.text = roleUnit_1.ADF.ToString();
            SPD_1.text = roleUnit_1.SPD.ToString();
            CRT_1.text = roleUnit_1.CRT.ToString();
            DEX_1.text = roleUnit_1.DEX.ToString();
            HIT_1.text = roleUnit_1.HIT.ToString();
            LKY_1.text = "0";
        }
        string playerUnitId_2 = PlayerPrefs.GetString("PlayerUnitId_2");
        int playerExp_2 = PlayerPrefs.GetInt("PlayerExp_2");
        if (playerUnitId_2 != null && playerUnitId_2 != "")
        {
            RoleUnitDAO roleUnit_2 = roleUnitCalculator.GetRoleUnitByIdAndExp(playerUnitId_2, playerExp_2);
            nextLevelExp_2.text = roleUnitCalculator.GetNextLevelExp(playerExp_2).ToString();
            level_2.text = roleUnit_2.level.ToString();
            name_2.text = roleUnit_2.unitName;
            initHP_2.text = roleUnit_2.initHP.ToString();
            initEP_2.text = roleUnit_2.initEP.ToString();
            initCP_2.text = roleUnit_2.initCP.ToString();
            exp_2.text = roleUnit_2.EXP.ToString();
            STR_2.text = roleUnit_2.STR.ToString();
            DEF_2.text = roleUnit_2.DEF.ToString();
            ATS_2.text = roleUnit_2.ATS.ToString();
            ADF_2.text = roleUnit_2.ADF.ToString();
            SPD_2.text = roleUnit_2.SPD.ToString();
            CRT_2.text = roleUnit_2.CRT.ToString();
            DEX_2.text = roleUnit_2.DEX.ToString();
            HIT_2.text = roleUnit_2.HIT.ToString();
            LKY_2.text = "0";
        }
        string playerUnitId_3 = PlayerPrefs.GetString("PlayerUnitId_3");
        int playerExp_3 = PlayerPrefs.GetInt("PlayerExp_3");
        if (playerUnitId_3 != null && playerUnitId_3 != "")
        {
            RoleUnitDAO roleUnit_3 = roleUnitCalculator.GetRoleUnitByIdAndExp(playerUnitId_3, playerExp_3);
            nextLevelExp_3.text = roleUnitCalculator.GetNextLevelExp(playerExp_3).ToString();
            level_3.text = roleUnit_3.level.ToString();
            name_3.text = roleUnit_3.unitName;
            initHP_3.text = roleUnit_3.initHP.ToString();
            initEP_3.text = roleUnit_3.initEP.ToString();
            initCP_3.text = roleUnit_3.initCP.ToString();
            exp_3.text = roleUnit_3.EXP.ToString();
            STR_3.text = roleUnit_3.STR.ToString();
            DEF_3.text = roleUnit_3.DEF.ToString();
            ATS_3.text = roleUnit_3.ATS.ToString();
            ADF_3.text = roleUnit_3.ADF.ToString();
            SPD_3.text = roleUnit_3.SPD.ToString();
            CRT_3.text = roleUnit_3.CRT.ToString();
            DEX_3.text = roleUnit_3.DEX.ToString();
            HIT_3.text = roleUnit_3.HIT.ToString();
            LKY_3.text = "0";
        }
    }
}
