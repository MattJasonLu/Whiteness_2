using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleUnit : MonoBehaviour {
	// 单位编号
	public string unitId = "U001";
	// 名字
	public string unitName = "";
	// 等级
	public int level = 1;
	// 血量
	public int HP = 100;
	// 血量上限
	public int initHP = 100;
	// 魔法
	public int EP = 100;
	// 魔法上限
	public int initEP = 100;
	// 能量
	public int CP = 100;
	// 能量上限
	public int initCP = 100;
	// 攻击
	public int STR = 20;
	// 防御
	public int DEF = 10;
	// 法术强度
	public int ATS = 10;
	// 法术防御
	public int ADF = 10;
	// 速度
	public int SPD = 30;
	// 闪避
	public int DEX = 10;
	// 攻击范围，0为近战，1为远程
	public int RNG = 0;
	// 暴击
	public int CRT = 10;
	// 命中率
	public int HIT = 90;
	// 角色类型，0为玩家，1为敌人
	public int roleType = 0;
	// 当前经验值
	public int EXP = 100;
	// 是否死亡
	public bool dead = false;
	// 角色显示面板预制体
	public GameObject rolePanel;
	private Text hpText;
	private Text epText;
	private Text cpText;
	private Slider hpSlider;
	private Slider epSlider;
	private Slider cpSlider;

	void Start()
	{

	}

	void Update()
	{
		if (rolePanel != null)
		{
			hpText.text = HP.ToString();
			epText.text = EP.ToString();
			cpText.text = CP.ToString();
			hpSlider.value = HP;
			epSlider.value = EP;
			cpSlider.value = CP;
		}
	}

	public void SetInitData(RoleUnit roleUnit)
	{
		unitName = roleUnit.unitName;
		level = roleUnit.level;
		initHP = roleUnit.initHP; 
		HP = roleUnit.initHP;
		initEP = roleUnit.initEP;
		EP = roleUnit.initEP;
		initCP = roleUnit.initCP;
		CP = roleUnit.initCP;
		STR = roleUnit.STR;
		DEF = roleUnit.DEF;
		ATS = roleUnit.ATS;
		ADF = roleUnit.ADF;
		SPD = roleUnit.SPD;
		DEX = roleUnit.DEX;
		RNG = roleUnit.RNG;
		CRT = roleUnit.CRT;
		HIT = roleUnit.HIT;
		EXP = roleUnit.EXP;
		roleType = roleUnit.roleType;
	}

	public void SetPanel(GameObject rolePanel)
	{
		this.rolePanel = rolePanel;
		hpText = this.rolePanel.transform.Find("HPText").GetComponent<Text>();
		epText = this.rolePanel.transform.Find("EPText").GetComponent<Text>();
		cpText = this.rolePanel.transform.Find("CPText").GetComponent<Text>();
		hpSlider = this.rolePanel.transform.Find("HPSlider").GetComponent<Slider>();
		epSlider = this.rolePanel.transform.Find("EPSlider").GetComponent<Slider>();
		cpSlider = this.rolePanel.transform.Find("CPSlider").GetComponent<Slider>();
		hpSlider.maxValue = initHP;
		hpSlider.value = hpSlider.maxValue;
		epSlider.maxValue = initEP;
		epSlider.value = epSlider.maxValue;
		cpSlider.maxValue = initCP;
		cpSlider.value = cpSlider.maxValue;
	}


	/// <summary>
	/// 获取攻击值
	/// </summary>
	/// <returns></returns>
	public int GetAttackValue()
	{
		return STR;
	}

	/// <summary>
	/// 使角色受到伤害
	/// </summary>
	/// <param name="damage"></param>
	/// <returns>实际受到的伤害</returns>
	public int GetDamageValue(int damage)
	{
		return damage -= DEF;
	}

}
