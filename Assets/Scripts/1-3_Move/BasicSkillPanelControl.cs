using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BasicSkillPanelControl : MonoBehaviour
{
    public GameObject canvas;
    public GameObject skillItemPrefab;
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
        panel_1 = canvas.transform.Find("SkillPanel/ItemTab_1").gameObject;
        panel_2 = canvas.transform.Find("SkillPanel/ItemTab_2").gameObject;
        panel_3 = canvas.transform.Find("SkillPanel/ItemTab_3").gameObject;
        content_1 = canvas.transform.Find("SkillPanel/ItemTab_1/Viewport/Content").gameObject;
        content_2 = canvas.transform.Find("SkillPanel/ItemTab_2/Viewport/Content").gameObject;
        content_3 = canvas.transform.Find("SkillPanel/ItemTab_3/Viewport/Content").gameObject;
        SetSkillContent();
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
        GameManager._instance.OnResume();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 设置技能内容
    /// </summary>
    private void SetSkillContent()
    {
        List<SkillDAO> itemUnits = dBCalculator.GetSkills();
        List<SkillDAO> list1 = itemUnits.Where(p => p.roleUnit.unitId == "P001").ToList();
        List<SkillDAO> list2 = itemUnits.Where(p => p.roleUnit.unitId == "P002").ToList();
        List<SkillDAO> list3 = itemUnits.Where(p => p.roleUnit.unitId == "P003").ToList();
        list1.ForEach(p => {
            GameObject skillItem = Instantiate(skillItemPrefab, content_1.transform, false);
            skillItem.transform.Find("Name").GetComponent<Text>().text = p.name;
            skillItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.id, typeof(Sprite)) as Sprite;
            skillItem.transform.Find("Desp").GetComponent<Text>().text = p.desp;
        });
        list2.ForEach(p => {
            GameObject skillItem = Instantiate(skillItemPrefab, content_2.transform, false);
            skillItem.transform.Find("Name").GetComponent<Text>().text = p.name;
            skillItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.id, typeof(Sprite)) as Sprite;
            skillItem.transform.Find("Desp").GetComponent<Text>().text = p.desp;
        });
        list3.ForEach(p => {
            GameObject skillItem = Instantiate(skillItemPrefab, content_3.transform, false);
            skillItem.transform.Find("Name").GetComponent<Text>().text = p.name;
            skillItem.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.id, typeof(Sprite)) as Sprite;
            skillItem.transform.Find("Desp").GetComponent<Text>().text = p.desp;
        });
    }
}
