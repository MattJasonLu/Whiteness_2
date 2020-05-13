using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClose()
    {
        GameManager._instance.OnResume();
        gameObject.SetActive(false);
    }
}
