using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MagicPanelControl : MonoBehaviour {

	public GameObject canvas;
	public DBCalculator dBCalculator;
	public GameObject buttonPrefab;
	private GameObject basicPanel;
	private GameObject content;

	public Text typeName;
	private List<string> additionTypeNames;
	private int currentAdditionTypeIndex = 0;

	private List<SkillDAO> magics;

	void Awake()
	{
		basicPanel = canvas.transform.Find("BasicPanel").gameObject;
		content = canvas.transform.Find("MagicPanel/ItemTab/Viewport/Content").gameObject;
		typeName = canvas.transform.Find("MagicPanel/PropTitle/Text").GetComponent<Text>();
		if (additionTypeNames == null)
		{
			additionTypeNames = GetAdditionTypeNames();
		}
		currentAdditionTypeIndex = 0;
		ChangeCurrentAdditionType();
	}

	void Start()
	{
		SetMagic();
	}

	public void Back()
	{
		gameObject.SetActive(false);
		basicPanel.SetActive(true);
	}

	/// <summary>
	/// 设置技能
	/// </summary>
	public void SetMagic()
	{
		for (int i = 0; i < content.transform.childCount; i++)
		{
			Destroy(content.transform.GetChild(i).gameObject);
		}
		string unitId = BattleSystem._instance.currentActUnitStatus.unitId;
		int currentEP = BattleSystem._instance.currentActUnitStatus.EP;
		magics = dBCalculator.GetMagicsByRoleId(unitId);
		List<SkillDAO> subMagics = magics.Where(p => p.additionType == (AdditionType)Enum.GetValues(typeof(AdditionType)).GetValue(currentAdditionTypeIndex)).ToList();
		subMagics.ForEach(p => {
			GameObject skillBtn = Instantiate(buttonPrefab, content.transform, false);
			skillBtn.transform.Find("Text").GetComponent<Text>().text = p.name;
			skillBtn.GetComponent<Button>().onClick.AddListener(delegate ()
			{
				// 发动对应技能
				LaunchMagic(p);
			});
			// 如果ep值不够则该战技不可用
			if (p.consume > currentEP)
			{
				skillBtn.GetComponent<Button>().interactable = false;
			}
		});
		GameObject backBtn = Instantiate(buttonPrefab, content.transform, false);
		backBtn.transform.Find("Text").GetComponent<Text>().text = "返回";
		backBtn.GetComponent<Button>().onClick.AddListener(delegate () {
			this.Back();
		});
	}

	// 发动魔法攻击
	void LaunchMagic(SkillDAO skill)
	{
		//StartCoroutine("Magic_" + magicId);
		BattleSystem._instance.OnAttack(skill);
	}

	IEnumerator Magic_SM001()
	{
		yield return new WaitForSeconds(1f);
		Debug.Log("发动烈火斩击");
	}

	IEnumerator Magic_SM002()
	{
		yield return new WaitForSeconds(1f);
		Debug.Log("火羽");
	}

	/// <summary>
	/// 左移
	/// </summary>
	public void LeftBtn()
	{
		currentAdditionTypeIndex -= 1;
		if (currentAdditionTypeIndex == -1)
		{
			currentAdditionTypeIndex = additionTypeNames.Count - 1;
		}
		ChangeCurrentAdditionType();
	}

	/// <summary>
	/// 右移
	/// </summary>
	public void RightBtn()
	{
		currentAdditionTypeIndex += 1;
		if (currentAdditionTypeIndex == additionTypeNames.Count)
		{
			currentAdditionTypeIndex = 0;
		}
		ChangeCurrentAdditionType();
	}

	/// <summary>
	/// 获取所有枚举名字
	/// </summary>
	/// <returns></returns>
	List<string> GetAdditionTypeNames()
	{
		List<string> nameList = new List<string>();
		foreach (var e in Enum.GetValues(typeof(AdditionType)))
		{
			// 转换成Description后添加至List
			object objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
			nameList.Add((objArr as DescriptionAttribute).Description);
		}
		return nameList;
	}

	void ChangeCurrentAdditionType()
	{
		typeName.text = additionTypeNames[currentAdditionTypeIndex];
		SetMagic();
	}
}
