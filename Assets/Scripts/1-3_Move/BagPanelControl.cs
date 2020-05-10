using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanelControl : MonoBehaviour
{
    public GameObject canvas;
    public GameObject bagItemPrefab;
    public DBCalculator dBCalculator;
    private GameObject panel_1;
    private GameObject panel_2;
    private GameObject panel_3;
    private GameObject content_1;
    private GameObject content_2;
    private GameObject content_3;

    // Start is called before the first frame update
    void Start()
    {
        // 面板1信息
        panel_1 = canvas.transform.Find("BagPanel/ItemTab_1").gameObject;
        panel_2 = canvas.transform.Find("BagPanel/ItemTab_2").gameObject;
        panel_3 = canvas.transform.Find("BagPanel/ItemTab_3").gameObject;
        content_1 = canvas.transform.Find("BagPanel/ItemTab_1/Viewport/Content").gameObject;
        content_2 = canvas.transform.Find("BagPanel/ItemTab_2/Viewport/Content").gameObject;
        content_3 = canvas.transform.Find("BagPanel/ItemTab_3/Viewport/Content").gameObject;
        SetBagContent();
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

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }

    private void SetBagContent()
    {
        List<ItemUnit> itemUnits = dBCalculator.GetBagContent();
        itemUnits.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(bagItemPrefab, content_1.transform, false);
            bagItem.transform.Find("Name").GetComponent<Text>().text = p.itemName;
            bagItem.transform.Find("Count").GetComponent<Text>().text = p.count.ToString();
        });
    }
}
