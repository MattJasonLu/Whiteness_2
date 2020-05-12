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
	// 穿戴类型
	public int wearType = 0;
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
	private AttackAddition attackAddition;

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

	public void SetAttackAddition(AttackAddition attackAddition)
	{
		this.attackAddition = attackAddition;
	}

	public AttackAddition GetAttackAddition()
	{
		return this.attackAddition;
	}

	/// <summary>
	/// 计算受到的实际伤害
	/// </summary>
	/// <param name="attacker">攻击者</param>
	/// <returns>受到的伤害值</returns>
	public List<string> GetRealDamage(RoleUnit attacker)
	{
		int realDamage = 0;
		List<string> realResult = new List<string>();
		int total = 100;
		int unHIT = 100 - attacker.HIT;
		int CRT = attacker.CRT;
		int roll = Random.Range(1, total + 1);
		if (roll <= unHIT)
		{
			realDamage = 0;
			realResult.Add("MISS");
			realResult.Add("Miss");
		}
		else if (roll > unHIT && roll <= unHIT + CRT)
		{
			realDamage = 2 * attacker.STR - this.DEF;
			this.HP = this.HP - realDamage;
			realResult.Add("CRT");
			realResult.Add("-暴击" + realDamage);
		}
		else
		{
			realDamage = attacker.STR - this.DEF;
			this.HP = this.HP - realDamage;
			realResult.Add("HIT");
			realResult.Add("-" + realDamage);
		}
		// 计算增幅
		if (attacker.attackAddition != null && realResult[0] != "MISS")
		{
			switch (attacker.attackAddition.additionType)
			{
				case AdditionType.Venus:
					attacker.STR = (int)((float)attacker.STR * (1 + attacker.attackAddition.STROffsetRatio));
					realResult.Add("Player");
					realResult.Add("攻击UP");
					break;
				case AdditionType.Jupiter:
					attacker.ADF = (int)((float)attacker.ADF * (1 + attacker.attackAddition.ADFOffsetRatio));
					realResult.Add("Player");
					realResult.Add("特防UP");
					break;
				case AdditionType.Mercury:
					attacker.HP += (int)((float)attacker.initHP * attacker.attackAddition.HPOffsetRatio);
					realResult.Add("Player");
					realResult.Add("血量UP");
					break;
				case AdditionType.Mars:
					realResult.Add("Enemy");
					realResult.Add("DOT伤害");
					break;
				case AdditionType.Saturn:
					attacker.DEF = (int)((float)attacker.DEF * (1 + attacker.attackAddition.DEFOffsetRatio));
					realResult.Add("Player");
					realResult.Add("防御UP");
					break;
				case AdditionType.Uranus:
					attacker.SPD = (int)((float)attacker.SPD * (1 + attacker.attackAddition.SPDOffsetRatio));
					realResult.Add("Player");
					realResult.Add("速度UP");
					break;
				case AdditionType.Neptune:
					this.SPD = (int)((float)this.SPD * (1 - attacker.attackAddition.SPDOffsetRatio));
					realResult.Add("Enemy");
					realResult.Add("速度DOWN");
					break;
				case AdditionType.Pluto:
					this.HIT = (int)((float)this.HIT * (1 - attacker.attackAddition.HITOffsetRatio));
					realResult.Add("Enemy");
					realResult.Add("命中DOWN");
					break;
			}
		}
		return realResult;
	}

	/// <summary>
	/// 获取攻击的名称
	/// </summary>
	/// <returns></returns>
	public string GetAttackName()
	{
		string name = "";
		if (this.attackAddition == null || this.attackAddition.additionType == AdditionType.Common)
		{
			name = "普通攻击";
		}
		else
		{
			string prop = AttackAddition.GetDescriptionByEnum(this.attackAddition.additionType);
			name = prop + "属性攻击";
		}
		return name;
	}

	

}
