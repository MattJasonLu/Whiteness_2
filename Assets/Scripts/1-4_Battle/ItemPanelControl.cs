﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelControl : MonoBehaviour {

	public GameObject canvas;
	public DBCalculator dBCalculator;
	public GameObject buttonPrefab;
	private GameObject basicPanel;
	private GameObject content;

	void Awake()
	{
		basicPanel = canvas.transform.Find("BasicPanel").gameObject;
		content = canvas.transform.Find("ItemPanel/ItemTab/Viewport/Content").gameObject;
	}

	void Start()
	{
		SetItems();
	}

	public void Back()
	{
		gameObject.SetActive(false);
		basicPanel.SetActive(true);
	}

	/// <summary>
	/// 设置技能
	/// </summary>
	public void SetItems()
	{
		List<ItemUnit> items = dBCalculator.GetItemUnitsByMainType(2);
		items.ForEach(p => {
			GameObject item = Instantiate(buttonPrefab, content.transform, false);
			item.transform.Find("Text").GetComponent<Text>().text = p.itemName + " X" + p.count;
		});
		GameObject backBtn = Instantiate(buttonPrefab, content.transform, false);
		backBtn.transform.Find("Text").GetComponent<Text>().text = "返回";
		backBtn.GetComponent<Button>().onClick.AddListener(delegate () {
			this.Back();
		});
	}
}
