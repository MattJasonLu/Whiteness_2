﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBCalculator : MonoBehaviour {

	SQLiteUtil sqliteUtil;
	Dictionary<int, int> playerLevelExpDict;
	Dictionary<int, int> enemyLevelExpDict;


	// Use this for initialization
	void Start () {
		sqliteUtil = SQLiteUtil._instance;
		if (playerLevelExpDict == null)
		{
			playerLevelExpDict = sqliteUtil.GetPlayerLevelExpDict();
		}
		if (enemyLevelExpDict == null)
		{
			enemyLevelExpDict = sqliteUtil.GetEnemyLevelExpDict();
		}
	}
	
	/// <summary>
	/// 通过ID和等级计算角色属性
	/// </summary>
	/// <param name="id"></param>
	/// <param name="level"></param>
	/// <returns></returns>
	public RoleUnit GetRoleUnitByIdAndLevel(string id, int level)
	{
		if (sqliteUtil == null)
		{
			sqliteUtil = SQLiteUtil._instance;
		}
		RoleUnit role = sqliteUtil.GetRoleUnitById(id);
		if (role.roleType == 0)
		{
			role.EXP = playerLevelExpDict[level];
		}
		else
		{
			role.EXP = enemyLevelExpDict[level];
		}
		role.level = level;
		role.initHP += level;
		role.initEP += level;
		role.initCP += level;
		role.STR += level;
		role.DEF += level; 
		role.ATS += level;
		role.ADF += level;
		role.SPD += level;
		return role;
	}

	/// <summary>
	/// 通过ID和经验值获取角色
	/// </summary>
	/// <param name="id"></param>
	/// <param name="exp"></param>
	/// <returns></returns>
	public RoleUnit GetRoleUnitByIdAndExp(string id, int exp)
	{
		int level = GetPlayerLevelByExp(exp);
		RoleUnit role = GetRoleUnitByIdAndLevel(id, level);
		role.EXP = exp;
		return role;
	}

	/// <summary>
	/// 通过经验值获取玩家当前等级
	/// </summary>
	/// <param name="exp"></param>
	/// <returns></returns>
	public int GetPlayerLevelByExp(int exp)
	{
		int[] array = playerLevelExpDict.Values.ToArray<int>();
		int left = 0;
		int right = array.Length - 1;
		while (left <= right)
		{
			int middle = left + ((right - left) >> 1);
			if (array[middle] > exp)
				right = middle - 1;
			else
				left = middle + 1;
		}
		return array[right + 1] > exp ? right + 1 : -1;
	}

	public int GetNextLevelExp(int exp)
	{
		int nextLevelExp = 0;
		int level = GetPlayerLevelByExp(exp);
		if (level < playerLevelExpDict.Count)
		{
			nextLevelExp = playerLevelExpDict[level + 1] - exp;
		}
		return nextLevelExp;
	}

	/// <summary>
	/// 获取背包内容
	/// </summary>
	/// <returns></returns>
	public List<ItemUnit> GetBagContent()
	{
		return sqliteUtil.GetItemUnits();
	}
}