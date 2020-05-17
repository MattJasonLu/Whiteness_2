using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanelControl : MonoBehaviour
{
    public GameObject canvas;
    public GameObject taskItemPrefab;
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
        panel_1 = canvas.transform.Find("TaskPanel/ItemTab_1").gameObject;
        panel_2 = canvas.transform.Find("TaskPanel/ItemTab_2").gameObject;
        panel_3 = canvas.transform.Find("TaskPanel/ItemTab_3").gameObject;
        content_1 = canvas.transform.Find("TaskPanel/ItemTab_1/Viewport/Content").gameObject;
        content_2 = canvas.transform.Find("TaskPanel/ItemTab_2/Viewport/Content").gameObject;
        content_3 = canvas.transform.Find("TaskPanel/ItemTab_3/Viewport/Content").gameObject;
        SetTaskContent();
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
    /// 设置背包内容
    /// </summary>
    private void SetTaskContent()
    {
        // 全部
        List<TaskUnitDAO> tasks = dBCalculator.GetTasks();
        // 主线
        List<TaskUnitDAO> mainTasks = tasks.Where(p => p.mainType == 0).ToList<TaskUnitDAO>();
        // 支线
        List<TaskUnitDAO> subTasks = tasks.Where(p => p.mainType == 1).ToList<TaskUnitDAO>();
        tasks.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(taskItemPrefab, content_1.transform, false);
            bagItem.transform.Find("Title").GetComponent<Text>().text = p.title;
            bagItem.transform.Find("Desp").GetComponent<Text>().text = p.desp;
            bagItem.transform.Find("Situation").GetComponent<Text>().text = p.completedCount + "/" + p.targetCount;
        });
        mainTasks.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(taskItemPrefab, content_2.transform, false);
            bagItem.transform.Find("Title").GetComponent<Text>().text = p.title;
            bagItem.transform.Find("Desp").GetComponent<Text>().text = p.desp;
            bagItem.transform.Find("Situation").GetComponent<Text>().text = p.completedCount + "/" + p.targetCount;
        });
        subTasks.ForEach(p => {
            GameObject bagItem = GameObject.Instantiate(taskItemPrefab, content_3.transform, false);
            bagItem.transform.Find("Title").GetComponent<Text>().text = p.title;
            bagItem.transform.Find("Desp").GetComponent<Text>().text = p.desp;
            bagItem.transform.Find("Situation").GetComponent<Text>().text = p.completedCount + "/" + p.targetCount;
        });
    }
}
