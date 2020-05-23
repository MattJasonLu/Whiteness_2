using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private GameObject panel_4;
    private GameObject panel_5;
    private GameObject panel_6;
    private GameObject panel_7;
    private GameObject content_1;
    private GameObject content_2;
    private GameObject content_3;
    private GameObject content_4;
    private GameObject content_5;
    private GameObject content_6;
    private GameObject content_7;

    // Start is called before the first frame update
    void Start()
    {
        // 面板1信息
        panel_1 = canvas.transform.Find("BagPanel/ItemTab_1").gameObject;
        panel_2 = canvas.transform.Find("BagPanel/ItemTab_2").gameObject;
        panel_3 = canvas.transform.Find("BagPanel/ItemTab_3").gameObject;
        panel_4 = canvas.transform.Find("BagPanel/ItemTab_4").gameObject;
        panel_5 = canvas.transform.Find("BagPanel/ItemTab_5").gameObject;
        panel_6 = canvas.transform.Find("BagPanel/ItemTab_6").gameObject;
        panel_7 = canvas.transform.Find("BagPanel/ItemTab_7").gameObject;
        content_1 = canvas.transform.Find("BagPanel/ItemTab_1/Viewport/Content").gameObject;
        content_2 = canvas.transform.Find("BagPanel/ItemTab_2/Viewport/Content").gameObject;
        content_3 = canvas.transform.Find("BagPanel/ItemTab_3/Viewport/Content").gameObject;
        content_4 = canvas.transform.Find("BagPanel/ItemTab_4/Viewport/Content").gameObject;
        content_5 = canvas.transform.Find("BagPanel/ItemTab_5/Viewport/Content").gameObject;
        content_6 = canvas.transform.Find("BagPanel/ItemTab_6/Viewport/Content").gameObject;
        content_7 = canvas.transform.Find("BagPanel/ItemTab_7/Viewport/Content").gameObject;
        SetBagContent();
        Toggle_1();
    }

    public void Toggle_1()
    {
        panel_1.SetActive(true);
        panel_2.SetActive(false);
        panel_3.SetActive(false);
        panel_4.SetActive(false);
        panel_5.SetActive(false);
        panel_6.SetActive(false);
        panel_7.SetActive(false);
    }

    public void Toggle_2()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(true);
        panel_3.SetActive(false);
        panel_4.SetActive(false);
        panel_5.SetActive(false);
        panel_6.SetActive(false);
        panel_7.SetActive(false);
    }

    public void Toggle_3()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(false);
        panel_3.SetActive(true);
        panel_4.SetActive(false);
        panel_5.SetActive(false);
        panel_6.SetActive(false);
        panel_7.SetActive(false);
    }

    public void Toggle_4()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(false);
        panel_3.SetActive(false);
        panel_4.SetActive(true);
        panel_5.SetActive(false);
        panel_6.SetActive(false);
        panel_7.SetActive(false);
    }

    public void Toggle_5()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(false);
        panel_3.SetActive(false);
        panel_4.SetActive(false);
        panel_5.SetActive(true);
        panel_6.SetActive(false);
        panel_7.SetActive(false);
    }

    public void Toggle_6()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(false);
        panel_3.SetActive(false);
        panel_4.SetActive(false);
        panel_5.SetActive(false);
        panel_6.SetActive(true);
        panel_7.SetActive(false);
    }

    public void Toggle_7()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(false);
        panel_3.SetActive(false);
        panel_4.SetActive(false);
        panel_5.SetActive(false);
        panel_6.SetActive(false);
        panel_7.SetActive(true);
    }

    public void OnClose()
    {
        GameManager._instance.OnResume();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 设置背包内容
    /// </summary>
    private void SetBagContent()
    {
        List<ItemUnit> itemUnits = dBCalculator.GetBagContent();
        List<ItemUnit> list1 = itemUnits.Where(p => p.mainType == 0).ToList<ItemUnit>();
        List<ItemUnit> list2 = itemUnits.Where(p => p.mainType == 1).ToList<ItemUnit>();
        List<ItemUnit> list3 = itemUnits.Where(p => p.mainType == 2).ToList<ItemUnit>();
        List<ItemUnit> list4 = itemUnits.Where(p => p.mainType == 3).ToList<ItemUnit>();
        List<ItemUnit> list5 = itemUnits.Where(p => p.mainType == 4).ToList<ItemUnit>();
        List<ItemUnit> list6 = itemUnits.Where(p => p.mainType == 5).ToList<ItemUnit>();
        itemUnits.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(bagItemPrefab, content_1.transform, false);
            bagItem.transform.Find("Name").GetComponent<Text>().text = p.itemName;
            bagItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.itemId, typeof(Sprite)) as Sprite;
            bagItem.transform.Find("Count").GetComponent<Text>().text = p.count.ToString();
        });
        list1.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(bagItemPrefab, content_2.transform, false);
            bagItem.transform.Find("Name").GetComponent<Text>().text = p.itemName;
            bagItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.itemId, typeof(Sprite)) as Sprite;
            bagItem.transform.Find("Count").GetComponent<Text>().text = p.count.ToString();
        });
        list2.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(bagItemPrefab, content_3.transform, false);
            bagItem.transform.Find("Name").GetComponent<Text>().text = p.itemName;
            bagItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.itemId, typeof(Sprite)) as Sprite;
            bagItem.transform.Find("Count").GetComponent<Text>().text = p.count.ToString();
        });
        list3.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(bagItemPrefab, content_4.transform, false);
            bagItem.transform.Find("Name").GetComponent<Text>().text = p.itemName;
            bagItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.itemId, typeof(Sprite)) as Sprite;
            bagItem.transform.Find("Count").GetComponent<Text>().text = p.count.ToString();
        });
        list4.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(bagItemPrefab, content_5.transform, false);
            bagItem.transform.Find("Name").GetComponent<Text>().text = p.itemName;
            bagItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.itemId, typeof(Sprite)) as Sprite;
            bagItem.transform.Find("Count").GetComponent<Text>().text = p.count.ToString();
        });
        list5.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(bagItemPrefab, content_6.transform, false);
            bagItem.transform.Find("Name").GetComponent<Text>().text = p.itemName;
            bagItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.itemId, typeof(Sprite)) as Sprite;
            bagItem.transform.Find("Count").GetComponent<Text>().text = p.count.ToString();
        });
        list5.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(bagItemPrefab, content_7.transform, false);
            bagItem.transform.Find("Name").GetComponent<Text>().text = p.itemName;
            bagItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.itemId, typeof(Sprite)) as Sprite;
            bagItem.transform.Find("Count").GetComponent<Text>().text = p.count.ToString();
        });
    }
}
