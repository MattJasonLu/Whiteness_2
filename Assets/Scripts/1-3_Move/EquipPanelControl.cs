using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanelControl : MonoBehaviour
{
    public GameObject canvas;
    public DBCalculator dBCalculator;
    public GameObject equipItemPrefab;
    private GameObject panel_1;
    private GameObject panel_2;
    private GameObject panel_3;
    private GameObject content_1;
    private GameObject content_2;
    private GameObject content_3;
    private GameObject equip_1_1;
    private GameObject equip_1_2;
    private GameObject equip_1_3;
    private GameObject equip_1_4;
    private GameObject equip_1_5;
    private GameObject equip_1_6;
    private GameObject equip_1_7;
    private GameObject equip_2_1;
    private GameObject equip_2_2;
    private GameObject equip_2_3;
    private GameObject equip_2_4;
    private GameObject equip_2_5;
    private GameObject equip_2_6;
    private GameObject equip_2_7;
    private GameObject equip_3_1;
    private GameObject equip_3_2;
    private GameObject equip_3_3;
    private GameObject equip_3_4;
    private GameObject equip_3_5;
    private GameObject equip_3_6;
    private GameObject equip_3_7;
    private GameObject changePanel;
    private GameObject changePanelContent;
    List<GameObject> equipGoList_1 = new List<GameObject>();
    List<GameObject> equipGoList_2 = new List<GameObject>();
    List<GameObject> equipGoList_3 = new List<GameObject>();
    // 当前面板所对应的角色编号
    private string currentRoleUnitId;

    // Start is called before the first frame update
    void Start()
    {
        // 面板1信息
        panel_1 = canvas.transform.Find("EquipPanel/Panel_1").gameObject;
        panel_2 = canvas.transform.Find("EquipPanel/Panel_2").gameObject;
        panel_3 = canvas.transform.Find("EquipPanel/Panel_3").gameObject;
        content_1 = canvas.transform.Find("EquipPanel/Panel_1/Content").gameObject;
        content_2 = canvas.transform.Find("EquipPanel/Panel_2/Content").gameObject;
        content_3 = canvas.transform.Find("EquipPanel/Panel_3/Content").gameObject;
        equip_1_1 = canvas.transform.Find("EquipPanel/Panel_1/Content/Equip_1").gameObject;
        equip_1_2 = canvas.transform.Find("EquipPanel/Panel_1/Content/Equip_2").gameObject;
        equip_1_3 = canvas.transform.Find("EquipPanel/Panel_1/Content/Equip_3").gameObject;
        equip_1_4 = canvas.transform.Find("EquipPanel/Panel_1/Content/Equip_4").gameObject;
        equip_1_5 = canvas.transform.Find("EquipPanel/Panel_1/Content/Equip_5").gameObject;
        equip_1_6 = canvas.transform.Find("EquipPanel/Panel_1/Content/Equip_6").gameObject;
        equip_1_7 = canvas.transform.Find("EquipPanel/Panel_1/Content/Equip_7").gameObject;
        equip_2_1 = canvas.transform.Find("EquipPanel/Panel_2/Content/Equip_1").gameObject;
        equip_2_2 = canvas.transform.Find("EquipPanel/Panel_2/Content/Equip_2").gameObject;
        equip_2_3 = canvas.transform.Find("EquipPanel/Panel_2/Content/Equip_3").gameObject;
        equip_2_4 = canvas.transform.Find("EquipPanel/Panel_2/Content/Equip_4").gameObject;
        equip_2_5 = canvas.transform.Find("EquipPanel/Panel_2/Content/Equip_5").gameObject;
        equip_2_6 = canvas.transform.Find("EquipPanel/Panel_2/Content/Equip_6").gameObject;
        equip_2_7 = canvas.transform.Find("EquipPanel/Panel_2/Content/Equip_7").gameObject;
        equip_3_1 = canvas.transform.Find("EquipPanel/Panel_3/Content/Equip_1").gameObject;
        equip_3_2 = canvas.transform.Find("EquipPanel/Panel_3/Content/Equip_2").gameObject;
        equip_3_3 = canvas.transform.Find("EquipPanel/Panel_3/Content/Equip_3").gameObject;
        equip_3_4 = canvas.transform.Find("EquipPanel/Panel_3/Content/Equip_4").gameObject;
        equip_3_5 = canvas.transform.Find("EquipPanel/Panel_3/Content/Equip_5").gameObject;
        equip_3_6 = canvas.transform.Find("EquipPanel/Panel_3/Content/Equip_6").gameObject;
        equip_3_7 = canvas.transform.Find("EquipPanel/Panel_3/Content/Equip_7").gameObject;
        changePanel = canvas.transform.Find("EquipPanel/ChangePanel").gameObject;
        changePanelContent = canvas.transform.Find("EquipPanel/ChangePanel/EquipTab/Viewport/Content").gameObject;
        equipGoList_1.Add(equip_1_1);
        equipGoList_1.Add(equip_1_2);
        equipGoList_1.Add(equip_1_3);
        equipGoList_1.Add(equip_1_4);
        equipGoList_1.Add(equip_1_5);
        equipGoList_1.Add(equip_1_6);
        equipGoList_1.Add(equip_1_7);
        equipGoList_2.Add(equip_2_1);
        equipGoList_2.Add(equip_2_2);
        equipGoList_2.Add(equip_2_3);
        equipGoList_2.Add(equip_2_4);
        equipGoList_2.Add(equip_2_5);
        equipGoList_2.Add(equip_2_6);
        equipGoList_2.Add(equip_2_7);
        equipGoList_3.Add(equip_3_1);
        equipGoList_3.Add(equip_3_2);
        equipGoList_3.Add(equip_3_3);
        equipGoList_3.Add(equip_3_4);
        equipGoList_3.Add(equip_3_5);
        equipGoList_3.Add(equip_3_6);
        equipGoList_3.Add(equip_3_7);
        SetEquipContent();
        Toggle_1();
    }

    public void Toggle_1()
    {
        panel_1.SetActive(true);
        panel_2.SetActive(false);
        panel_3.SetActive(false);
        changePanel.SetActive(false);
        currentRoleUnitId = "P001";
    }

    public void Toggle_2()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(true);
        panel_3.SetActive(false);
        currentRoleUnitId = "P002";
    }

    public void Toggle_3()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(false);
        panel_3.SetActive(true);
        currentRoleUnitId = "P003";
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 设置装备内容
    /// </summary>
    public void SetEquipContent()
    {
        List<EquipUnit> equipUnits = dBCalculator.GetEquipContent();
        List<EquipUnit> equipUnits1 = equipUnits.Where(p => p.roleUnit.unitId == "P001").ToList();
        List<EquipUnit> equipUnits2 = equipUnits.Where(p => p.roleUnit.unitId == "P002").ToList();
        List<EquipUnit> equipUnits3 = equipUnits.Where(p => p.roleUnit.unitId == "P003").ToList();
        for (int i = 0; i < equipGoList_1.Count; i++)
        {
            EquipUnit equipUnit = equipUnits1.Where(p => p.equipType == i).FirstOrDefault();
            if (equipUnit != null)
            {
                equipGoList_1[i].transform.Find("Name").GetComponent<Text>().text = equipUnit.itemUnit.itemName;
                equipGoList_1[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + equipUnit.itemUnit.itemId, typeof(Sprite)) as Sprite;
            }
        }
        for (int i = 0; i < equipGoList_2.Count; i++)
        {
            EquipUnit equipUnit = equipUnits2.Where(p => p.equipType == i).FirstOrDefault();
            if (equipUnit != null)
            {
                equipGoList_2[i].transform.Find("Name").GetComponent<Text>().text = equipUnit.itemUnit.itemName;
                equipGoList_2[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + equipUnit.itemUnit.itemId, typeof(Sprite)) as Sprite;
            }
        }
        for (int i = 0; i < equipGoList_3.Count; i++)
        {
            EquipUnit equipUnit = equipUnits3.Where(p => p.equipType == i).FirstOrDefault();
            if (equipUnit != null)
            {
                equipGoList_3[i].transform.Find("Name").GetComponent<Text>().text = equipUnit.itemUnit.itemName;
                equipGoList_3[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + equipUnit.itemUnit.itemId, typeof(Sprite)) as Sprite;
            }
        }

    }

    /// <summary>
    /// 设置更换面板的装备列表
    /// </summary>
    public void SetChangeEquipList(int type)
    {
        // 删除content下所有子物体
        int childCount = changePanelContent.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(changePanelContent.transform.GetChild(0).gameObject);
        }
        RoleUnit role = dBCalculator.GetRoleUnitById(currentRoleUnitId);
        int wearType = role.wearType;
        // 获取匹配的装备
        List<ItemUnit> itemUnits = dBCalculator.GetBagContentByType(type, wearType);
        itemUnits.ForEach(p =>
        {
            GameObject equipItemGo = Instantiate(equipItemPrefab, changePanelContent.transform, false);
            equipItemGo.transform.Find("Name").GetComponent<Text>().text = p.itemName;
            equipItemGo.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load("ItemImg/" + p.itemId, typeof(Sprite)) as Sprite;
        });
        changePanel.SetActive(true);
    }

    public void OnChangePanelClose()
    {
        changePanel.SetActive(false);
    }
}
