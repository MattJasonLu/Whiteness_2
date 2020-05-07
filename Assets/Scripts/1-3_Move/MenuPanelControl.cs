using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelControl : MonoBehaviour
{
    public GameObject rolePanel;
    public GameObject equipPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Button_1()
    {
        if (!rolePanel.activeSelf)
        {
            rolePanel.SetActive(true);
            equipPanel.SetActive(false);
        }
        else
        {
            rolePanel.SetActive(false);
        }
    }

    public void Button_2()
    {
        if (!equipPanel.activeSelf)
        {
            equipPanel.SetActive(true);
            rolePanel.SetActive(false);
        }
        else
        {
            equipPanel.SetActive(false);
        }
    }

    public void Button_3()
    {

    }

    public void Button_4()
    {

    }

    public void Button_5()
    {

    }

    public void Button_6()
    {

    }

    public void Button_7()
    {

    }
}
