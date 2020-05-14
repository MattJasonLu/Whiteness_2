using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DBCalculator : MonoBehaviour {

	SQLiteUtil sqliteUtil;
	Dictionary<int, int> playerLevelExpDict;
	Dictionary<int, int> enemyLevelExpDict;


	// Use this for initialization
	void Start () 
	{
		Init();
	}

	public void Init()
	{
		sqliteUtil = SQLiteUtil._instance;
		SetDict();
	}

	public void SetDict()
	{
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
	public RoleUnitDAO GetRoleUnitByIdAndLevel(string id, int level)
	{
		if (sqliteUtil == null)
		{
			Init();
		}
		RoleUnitDAO role = sqliteUtil.GetRoleUnitById(id);
		if (role.roleType == 0)
		{
			int exp = 0;
			playerLevelExpDict.TryGetValue(level, out exp);
			role.EXP = exp;
		}
		else
		{
			int exp = 0;
			enemyLevelExpDict.TryGetValue(level, out exp);
			role.EXP = exp;
		}
		role.level = level;
		role.initHP += level * 5;
		role.initEP += level * 3;
		role.initCP += level;
		role.STR += level * 2;
		role.DEF += level * 2; 
		role.ATS += level * 2;
		role.ADF += level * 2;
		role.SPD += level * 2;
		return role;
	}

	/// <summary>
	/// 通过ID和经验值获取角色
	/// </summary>
	/// <param name="id"></param>
	/// <param name="exp"></param>
	/// <returns></returns>
	public RoleUnitDAO GetRoleUnitByIdAndExp(string id, int exp)
	{
		int level = GetPlayerLevelByExp(exp);
		RoleUnitDAO role = GetRoleUnitByIdAndLevel(id, level);
		role.EXP = exp;
		return role;
	}

	/// <summary>
	/// 获取角色基本属性
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public RoleUnitDAO GetRoleUnitById(string id)
	{
		if (sqliteUtil == null)
		{
			Init();
		}
		RoleUnitDAO role = sqliteUtil.GetRoleUnitById(id);
		return role;
	}

	/// <summary>
	/// 通过经验值获取玩家当前等级
	/// </summary>
	/// <param name="exp"></param>
	/// <returns></returns>
	public int GetPlayerLevelByExp(int exp)
	{
		if (sqliteUtil == null)
		{
			Init();
		}
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

	public List<ItemUnit> GetBagContentByType(int equipType, int wearType)
	{
		return sqliteUtil.GetItemUnitByType(equipType, wearType);
	}

	/// <summary>
	/// 获取装备内容
	/// </summary>
	/// <returns></returns>
	public List<EquipUnit> GetEquipContent()
	{
		return sqliteUtil.GetEquipment();
	}

	/// <summary>
	/// 更新装备
	/// </summary>
	/// <param name="propsId"></param>
	/// <param name="roleId"></param>
	/// <param name="equipType"></param>
	public void UpdateEquipContent(string propsId, string roleId, int equipType)
	{
		sqliteUtil.UpdateEquipment(propsId, roleId, equipType);
	}
}
