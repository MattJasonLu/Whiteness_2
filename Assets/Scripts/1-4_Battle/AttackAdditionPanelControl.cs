using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttackAdditionPanelControl : MonoBehaviour {

	public GameObject basicPanel;
	public Text typeName;
	private List<string> additionTypeNames;
	private int currentAdditionTypeIndex = 0;

	void Awake()
	{
		if (additionTypeNames == null)
		{
			additionTypeNames = GetAdditionTypeNames();
		}
		currentAdditionTypeIndex = 0;
		ChangeCurrentAdditionType();
	}

	void Start()
	{
		
	}

	/// <summary>
	/// 左移
	/// </summary>
	public void Button_1()
	{
		currentAdditionTypeIndex -= 1;
		if (currentAdditionTypeIndex == -1)
		{
			currentAdditionTypeIndex = additionTypeNames.Count - 1;
		}
		ChangeCurrentAdditionType();
	}

	/// <summary>
	/// 属性攻击
	/// </summary>
	public void Button_2()
	{
		// TODO
		BattleSystem._instance.OnAttack();
		
	}

	/// <summary>
	/// 右移
	/// </summary>
	public void Button_3()
	{
		currentAdditionTypeIndex += 1;
		if (currentAdditionTypeIndex == additionTypeNames.Count)
		{
			currentAdditionTypeIndex = 0;
		}
		ChangeCurrentAdditionType();
	}

	public void Back()
	{
		gameObject.SetActive(false);
		basicPanel.SetActive(true);
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
	}
}
