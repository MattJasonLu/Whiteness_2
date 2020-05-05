using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicPanelControl : MonoBehaviour {

	// 各类面板
	public GameObject magicPanel;
	public GameObject skillPanel;
	public GameObject comboPanel;
	public GameObject itemPanel;
	public GameObject attackAdditionPanel;

	void Awake()
	{
		// 启动时禁用面板
		magicPanel.SetActive(false);
		skillPanel.SetActive(false);
		comboPanel.SetActive(false);
		itemPanel.SetActive(false);
		attackAdditionPanel.SetActive(false);
	}

	// 普通攻击
	public void CommonAttack()
	{
		attackAdditionPanel.SetActive(true);
		gameObject.SetActive(false);
		//BattleSystem._instance.OnAttack();
	}

	// 显示魔法面板
	public void ShowMagicPanel()
	{
		magicPanel.SetActive(true);
		gameObject.SetActive(false);
	}

	// 显示战技面板
	public void ShowSkillPanel()
	{
		skillPanel.SetActive(true);
		gameObject.SetActive(false);
	}

	// 显示连击面板
	public void ShowComboPanel()
	{
		comboPanel.SetActive(true);
		gameObject.SetActive(false);
	}

	// 显示物品面板
	public void ShowItemPanel()
	{
		itemPanel.SetActive(true);
		gameObject.SetActive(false);
	}

	// 撤退
	public void Retreat()
	{
		LevelLoader._instance.LoadPreviousLevel();
	}


}
