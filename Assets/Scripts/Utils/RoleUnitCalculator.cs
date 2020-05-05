using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleUnitCalculator : MonoBehaviour {

	SQLiteUtil sqliteUtil;
	Dictionary<int, int> playerLevelExpDict;
	Dictionary<int, int> enemyLevelExpDict;


	// Use this for initialization
	void Start () {
		sqliteUtil = SQLiteUtil._instance;
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
			if (playerLevelExpDict == null)
			{
				playerLevelExpDict = sqliteUtil.GetPlayerLevelExpDict();
			}
			role.EXP = playerLevelExpDict[level];
		}
		else
		{
			if (enemyLevelExpDict == null)
			{
				enemyLevelExpDict = sqliteUtil.GetEnemyLevelExpDict();
			}
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
}
