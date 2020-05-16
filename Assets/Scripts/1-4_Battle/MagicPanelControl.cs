﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicPanelControl : MonoBehaviour {

	public GameObject canvas;
	public DBCalculator dBCalculator;
	public GameObject buttonPrefab;
	private GameObject basicPanel;
	private GameObject content;

	void Awake()
	{
		basicPanel = canvas.transform.Find("BasicPanel").gameObject;
		content = canvas.transform.Find("MagicPanel/ItemTab/Viewport/Content").gameObject;
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
		string unitId = BattleSystem._instance.currentActUnitStatus.unitId;
		List<SkillDAO> magics = dBCalculator.GetMagicsByRoleId(unitId);
		magics.ForEach(p => {
			GameObject skillItem = Instantiate(buttonPrefab, content.transform, false);
			skillItem.transform.Find("Text").GetComponent<Text>().text = p.name;
		});
		GameObject backBtn = Instantiate(buttonPrefab, content.transform, false);
		backBtn.transform.Find("Text").GetComponent<Text>().text = "返回";
		backBtn.GetComponent<Button>().onClick.AddListener(delegate () {
			this.Back();
		});
	}
}
