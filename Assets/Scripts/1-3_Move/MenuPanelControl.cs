using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MenuPanelControl : MonoBehaviour
{
    public GameObject canvas;
    private GameObject rolePanel;
    private GameObject equipPanel;
    private GameObject bagPanel;
    private GameObject mapPanel;
    private GameObject skillPanel;
    private GameObject taskPanel;
    private GameObject settingPanel;

    void Awake()
    {
        rolePanel = canvas.transform.Find("RolePanel").gameObject;
        equipPanel = canvas.transform.Find("EquipPanel").gameObject;
        bagPanel = canvas.transform.Find("BagPanel").gameObject;
        mapPanel = canvas.transform.Find("MapPanel").gameObject;
        skillPanel = canvas.transform.Find("SkillPanel").gameObject;
        taskPanel = canvas.transform.Find("TaskPanel").gameObject;
        settingPanel = canvas.transform.Find("SettingPanel").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Button_1()
    {
        if (!rolePanel.activeSelf)
        {
            GameManager._instance.OnPause();
            rolePanel.SetActive(true);
            equipPanel.SetActive(false);
            bagPanel.SetActive(false);
            mapPanel.SetActive(false);
            skillPanel.SetActive(false);
            taskPanel.SetActive(false);
            settingPanel.SetActive(false);
        }
        else
        {
            GameManager._instance.OnResume();
            rolePanel.SetActive(false);
        }
    }

    public void Button_2()
    {
        if (!equipPanel.activeSelf)
        {
            GameManager._instance.OnPause();
            equipPanel.SetActive(true);
            rolePanel.SetActive(false);
            bagPanel.SetActive(false);
            mapPanel.SetActive(false);
            skillPanel.SetActive(false);
            taskPanel.SetActive(false);
            settingPanel.SetActive(false);
        }
        else
        {
            GameManager._instance.OnResume();
            equipPanel.SetActive(false);
        }
    }

    public void Button_3()
    {
        if (!bagPanel.activeSelf)
        {
            GameManager._instance.OnPause();
            bagPanel.SetActive(true);
            equipPanel.SetActive(false);
            rolePanel.SetActive(false);
            mapPanel.SetActive(false);
            skillPanel.SetActive(false);
            taskPanel.SetActive(false);
            settingPanel.SetActive(false);
        }
        else
        {
            GameManager._instance.OnResume();
            bagPanel.SetActive(false);
        }
    }

    public void Button_4()
    {
        if (!mapPanel.activeSelf)
        {
            GameManager._instance.OnPause();
            mapPanel.SetActive(true);
            rolePanel.SetActive(false);
            equipPanel.SetActive(false);
            bagPanel.SetActive(false);
            skillPanel.SetActive(false);
            taskPanel.SetActive(false);
            settingPanel.SetActive(false);
        }
        else
        {
            GameManager._instance.OnResume();
            mapPanel.SetActive(false);
        }
    }

    public void Button_5()
    {
        if (!skillPanel.activeSelf)
        {
            GameManager._instance.OnPause();
            skillPanel.SetActive(true);
            mapPanel.SetActive(false);
            rolePanel.SetActive(false);
            equipPanel.SetActive(false);
            bagPanel.SetActive(false);
            taskPanel.SetActive(false);
            settingPanel.SetActive(false);
        }
        else
        {
            GameManager._instance.OnResume();
            skillPanel.SetActive(false);
        }
    }

    public void Button_6()
    {
        if (!taskPanel.activeSelf)
        {
            GameManager._instance.OnPause();
            taskPanel.SetActive(true);
            skillPanel.SetActive(false);
            mapPanel.SetActive(false);
            rolePanel.SetActive(false);
            equipPanel.SetActive(false);
            bagPanel.SetActive(false);
            settingPanel.SetActive(false);
        }
        else
        {
            GameManager._instance.OnResume();
            taskPanel.SetActive(false);
        }
    }

    public void Button_7()
    {
        if (!settingPanel.activeSelf)
        {
            GameManager._instance.OnPause();
            settingPanel.SetActive(true);
            rolePanel.SetActive(false);
            equipPanel.SetActive(false);
            bagPanel.SetActive(false);
            mapPanel.SetActive(false);
            skillPanel.SetActive(false);
            taskPanel.SetActive(false);
        }
        else
        {
            GameManager._instance.OnResume();
            settingPanel.SetActive(false);
        }
    }
}
