using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePanelControl : MonoBehaviour
{
    public GameObject panel_1;
    public GameObject panel_2;
    public GameObject panel_3;

    // Start is called before the first frame update
    void Start()
    {
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
}
