﻿using System.Collections.Generic;
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
	// 属性攻击
	private AttackAddition attackAddition;
	// 技能实现
	private SkillDAO skill;
	// 使用道具
	private ItemUnit item;
	/// <summary>
	/// 人物是否被点击过
	/// </summary>
	public bool isClicked = false;

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
		// 如果血量为0，设置为死亡单元
		if (HP <= 0)
		{
			Debug.Log("角色死亡");
			transform.gameObject.tag = "DeadUnit";
			dead = true;
			transform.gameObject.SetActive(false);
			//Destroy(transform.gameObject, 0.3f);
		}
	}

	public void SetInitData(RoleUnitDAO roleUnit)
	{
		unitName = roleUnit.unitName;
		level = roleUnit.level;
		initHP = roleUnit.initHP; 
		HP = roleUnit.initHP;
		initEP = roleUnit.initEP;
		EP = roleUnit.initEP;
		initCP = roleUnit.initCP;
		CP = 0;
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

	public void SetSkill(SkillDAO skill)
	{
		this.skill = skill;
	}

	public SkillDAO GetSkill()
	{
		return this.skill;
	}

	public void SetItem(ItemUnit item)
	{
		this.item = item;
	}

	public ItemUnit GetItem()
	{
		return this.item;
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
		int unHIT = 100 - attacker.HIT;	// 获取未命中率
		int CRT = attacker.CRT;	// 获取暴击率
		int STR = attacker.STR; // 获取攻击力
		int SPD = attacker.SPD; // 获取速度
		int roll = Random.Range(1, total + 1);
		// 1:受到攻击的状态，2:伤害提示文字，3:附加状态命中对象，4:附加效果提示文字
		if (roll <= unHIT)
		{
			realDamage = 0;
			realResult.Add("MISS");
			realResult.Add("Miss");
		}
		else if (roll > unHIT && roll <= unHIT + CRT)
		{
			realResult.Add("CRT");
			realResult.Add("");
		}
		else
		{
			realResult.Add("HIT");
			realResult.Add("");
		}
		// 计算技能增益
		if (attacker.skill != null)
		{
			// 如果为魔法
			if (attacker.skill.skillType == 0)
			{
				// 减去EP
				attacker.EP -= attacker.skill.consume;
				if (realResult[0] != "MISS")
				{
					// 增加CP
					attacker.CP += 10;
					// 受体为敌人
					if (attacker.skill.target == 0)
					{
						// 增幅攻击力
						STR = (int)(STR + STR * ((float)attacker.skill.str / 100));
						if (realResult[0] == "CRT")
						{
							realDamage = 2 * STR - this.DEF;
							this.HP = this.HP - realDamage;
							// 被攻击者增加CP
							this.CP = this.CP + 15;
							realResult[1] = "-暴击" + realDamage;
						}
						else if (realResult[0] == "HIT")
						{
							realDamage = STR - this.DEF;
							this.HP = this.HP - realDamage;
							// 被攻击者增加CP
							this.CP = this.CP + 10;
							realResult[1] = "-" + realDamage;
						}
					}
					// 受体为友方
					else
					{
						realResult.Add("Player");
						// 增益为永久或几回合
						if (attacker.skill.str != 0)
						{
							attacker.STR = (int)(STR + STR * ((float)attacker.skill.str / 100));
							realResult.Add("攻击UP");
						}
						else if (attacker.skill.spd != 0)
						{
							attacker.SPD = (int)(SPD + SPD * ((float)attacker.skill.spd / 100));
							realResult.Add("速度UP");
						}
					}
				}
			}
			// 如果为战技
			else
			{
				// 减去EP
				attacker.CP -= attacker.skill.consume;
				if (realResult[0] != "MISS")
				{
					// 受体为敌人
					if (attacker.skill.target == 0)
					{
						// 增幅攻击力
						STR = (int)(STR + STR * ((float)attacker.skill.str / 100));

						if (realResult[0] == "CRT")
						{
							realDamage = 2 * STR - this.DEF;
							this.HP = this.HP - realDamage;
							realResult[1] = "-暴击" + realDamage;
						}
						else if (realResult[0] == "HIT")
						{
							realDamage = STR - this.DEF;
							this.HP = this.HP - realDamage;
							realResult[1] = "-" + realDamage;
						}
					}
					// 受体为友方
					else
					{
						realResult.Add("Player");
						// 增益为永久或几回合
						if (attacker.skill.str != 0)
						{
							attacker.STR = (int)(STR + STR * ((float)attacker.skill.str / 100));
							realResult.Add("攻击UP");
						}
						else if (attacker.skill.spd != 0)
						{
							attacker.SPD = (int)(SPD + SPD * ((float)attacker.skill.spd / 100));
							realResult.Add("速度UP");
						}
					}
				}
			}
			
			// 清空技能
			attacker.skill = null;
		} 
		// 普通攻击
		else
		{
			// 增加CP
			attacker.CP += 5;
			if (realResult[0] == "CRT")
			{
				realDamage = 2 * STR - this.DEF;
				this.HP = this.HP - realDamage;
				// 被攻击者增加CP
				this.CP = this.CP + 15;
				realResult[1] = "-暴击" + realDamage;
			}
			else if (realResult[0] == "HIT")
			{
				realDamage = STR - this.DEF;
				this.HP = this.HP - realDamage;
				// 被攻击者增加CP
				this.CP = this.CP + 10;
				realResult[1] = "-" + realDamage;
			}
		}
		// 属性攻击
		if (attacker.attackAddition != null && realResult[0] != "MISS")
		{
			
			switch (attacker.attackAddition.additionType)
			{
				case AdditionType.Common:
					break;
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
			// 清空属性加成
			attacker.attackAddition = null;
		}
		// 设置上下界
		if (attacker.HP <= 0) attacker.HP = 0;
		if (attacker.HP >= attacker.initHP) attacker.HP = attacker.initHP;
		if (attacker.EP <= 0) attacker.EP = 0;
		if (attacker.EP >= attacker.initEP) attacker.EP = attacker.initEP;
		if (attacker.CP <= 0) attacker.CP = 0;
		if (attacker.CP >= attacker.initCP) attacker.CP = attacker.initCP;
		if (this.HP <= 0) this.HP = 0;
		if (this.HP >= this.initHP) this.HP = this.initHP;
		if (this.EP <= 0) this.EP = 0;
		if (this.EP >= this.initEP) this.EP = this.initEP;
		if (this.CP <= 0) this.CP = 0;
		if (this.CP >= this.initCP) this.CP = this.initCP;
		return realResult;
	}

	public List<string> GetBenefit()
	{
		int value;
		List<string> realResult = new List<string>();
		if (this.GetItem() != null)
		{
			if (this.GetItem().hp != 0)
			{
				int oldHp = this.HP;
				this.HP += this.GetItem().hp;
				if (this.HP <= 0) this.HP = 0;
				if (this.HP >= this.initHP) this.HP = this.initHP;
				value = this.HP - oldHp;
				realResult.Add("HP");
				realResult.Add("+" + value);

			}
			else if (this.GetItem().ep != 0)
			{
				int oldEp = this.EP;
				this.EP += this.GetItem().ep;
				if (this.EP <= 0) this.EP = 0;
				if (this.EP >= this.initEP) this.EP = this.initEP;
				value = this.EP - oldEp;
				realResult.Add("EP");
				realResult.Add("+" + value);

			}
			else if (this.GetItem().cp != 0)
			{
				int oldCp = this.CP;
				this.CP += this.GetItem().cp;
				if (this.CP <= 0) this.CP = 0;
				if (this.CP >= this.initCP) this.CP = this.initCP;
				value = this.CP - oldCp;
				realResult.Add("CP");
				realResult.Add("+" + value);
			}
		}
		// 清空物品
		this.item = null;
		return realResult;
	}

	/// <summary>
	/// 获取攻击的名称
	/// </summary>
	/// <returns></returns>
	public string GetAttackName()
	{
		string name = "";
		if (this.item != null)
		{
			name = this.item.itemName;
		}
		else if (this.skill != null)
		{
			name = skill.name;
		}
		else if (this.attackAddition == null || this.attackAddition.additionType == AdditionType.Common)
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

	/// <summary>
	/// 鼠标点击模型
	/// </summary>
	private void OnMouseDown() 
	{
		if (!isClicked)
		{
			isClicked = true;
		}
	}	

}
