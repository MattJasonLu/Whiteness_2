using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelControl : MonoBehaviour
{
    public GameObject rolePanel;
    public GameObject equipPanel;
    public GameObject bagPanel;
    public GameObject mapPanel;

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
            bagPanel.SetActive(false);
            mapPanel.SetActive(false);
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
            bagPanel.SetActive(false);
            mapPanel.SetActive(false);
        }
        else
        {
            equipPanel.SetActive(false);
        }
    }

    public void Button_3()
    {
        if (!bagPanel.activeSelf)
        {
            bagPanel.SetActive(true);
            equipPanel.SetActive(false);
            rolePanel.SetActive(false);
            mapPanel.SetActive(false);
        }
        else
        {
            bagPanel.SetActive(false);
        }
    }

    public void Button_4()
    {
        if (!mapPanel.activeSelf)
        {
            mapPanel.SetActive(true);
            rolePanel.SetActive(false);
            equipPanel.SetActive(false);
            bagPanel.SetActive(false);
        }
        else
        {
            mapPanel.SetActive(false);
        }
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
