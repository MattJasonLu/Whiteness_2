using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleUnitDAO
{
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
}
