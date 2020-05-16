using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicPanelControl : MonoBehaviour {

	// 各类面板
	public GameObject canvas;
	private GameObject basicPanel;
	private GameObject magicPanel;
	private GameObject skillPanel;
	private GameObject comboPanel;
	private GameObject itemPanel;
	private GameObject attackAdditionPanel;

	void Awake()
	{
		basicPanel = canvas.transform.Find("BasicPanel").gameObject;
		magicPanel = canvas.transform.Find("MagicPanel").gameObject;
		skillPanel = canvas.transform.Find("SkillPanel").gameObject;
		comboPanel = canvas.transform.Find("ComboPanel").gameObject;
		itemPanel = canvas.transform.Find("ItemPanel").gameObject;
		attackAdditionPanel = canvas.transform.Find("AttackAdditionPanel").gameObject;
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
		basicPanel.SetActive(false);
	}

	// 显示魔法面板
	public void ShowMagicPanel()
	{
		magicPanel.SetActive(true);
		basicPanel.SetActive(false);
	}

	// 显示战技面板
	public void ShowSkillPanel()
	{
		skillPanel.SetActive(true);
		basicPanel.SetActive(false);
	}

	// 显示连击面板
	public void ShowComboPanel()
	{
		comboPanel.SetActive(true);
		basicPanel.SetActive(false);
	}

	// 显示物品面板
	public void ShowItemPanel()
	{
		itemPanel.SetActive(true);
		basicPanel.SetActive(false);
	}

	// 撤退
	public void Retreat()
	{
		basicPanel.SetActive(false);
		LevelLoader._instance.LoadPreviousLevel();
	}


}
