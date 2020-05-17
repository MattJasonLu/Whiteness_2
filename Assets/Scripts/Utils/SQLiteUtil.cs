using UnityEngine;
using System.Collections;
using System.IO;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

public class SQLiteUtil : MonoBehaviour
{
    public static SQLiteUtil _instance;

    /// <summary>
    /// SQLite数据库辅助类
    /// </summary>
    private SQLiteHelper sql;

    void Awake()
    {
        _instance = this;
        //创建名为sqlite4unity的数据库
        
        sql = new SQLiteHelper("data source=" + Application.streamingAssetsPath + "/sqlite4unity.db");
        //Test();
    }

    void Start()
    {
        
        #region 表操作
        /*
        //创建名为table1的数据表
        sql.CreateTable("LEVELEXP_PLAYER", new string[] { "ID", "LEVEL", "EXP" }, new string[] { "INTEGER", "INTEGER", "INTEGER" });

        //插入两条数据
        for (int i = 1; i <= 100; i++)
        {
            int y = Mathf.FloorToInt(Mathf.Pow(i - 1, 3)) + 60;
            sql.InsertValues("LEVELEXP_PLAYER", new string[] { i + "", i + "", y + "" });
        }
        */

        //sql.InsertValues("table2", new string[] { "'2'", "'李四'", "'25'", "'Li4@163.com'" });

        /*//更新数据，将Name="张三"的记录中的Name改为"Zhang3"
        sql.UpdateValues("table2", new string[] { "Name" }, new string[] { "'Zhang3'" }, "Name", "=", "'张三'");

        //插入3条数据
        sql.InsertValues("table2", new string[] { "3", "'王五'", "25", "'Wang5@163.com'" });
        sql.InsertValues("table2", new string[] { "4", "'王五'", "26", "'Wang5@163.com'" });
        sql.InsertValues("table2", new string[] { "5", "'王五'", "27", "'Wang5@163.com'" });

        //删除Name="王五"且Age=26的记录,DeleteValuesOR方法类似
        sql.DeleteValuesAND("table2", new string[] { "Name", "Age" }, new string[] { "=", "=" }, new string[] { "'王五'", "'26'" });

        //读取整张表
        SqliteDataReader reader = sql.ReadFullTable("table2");
        while (reader.Read())
        {
            //读取ID
            Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
            //读取Name
            Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
            //读取Age
            Debug.Log(reader.GetInt32(reader.GetOrdinal("Age")));
            //读取Email
            Debug.Log(reader.GetString(reader.GetOrdinal("Email")));
        }

        //读取数据表中Age>=25的所有记录的ID和Name
        reader = sql.ReadTable("table1", new string[] { "ID", "Name" }, new string[] { "Age" }, new string[] { ">=" }, new string[] { "'25'" });
        while (reader.Read())
        {
            //读取ID
            Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
            //读取Name
            Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
        }

        //自定义SQL,删除数据表中所有Name="王五"的记录
        sql.ExecuteQuery("DELETE FROM table1 WHERE NAME='王五'");*/
        #endregion

        //关闭数据库连接
        //sql.CloseConnection();
    }

    private void Test()
    {
        /*
        int sum = 0;
        for (int i = 1; i <= 100; i++)
        {
            int f = Mathf.RoundToInt(Mathf.Pow((i - 1), 2) + 60);
            int g = Mathf.RoundToInt(Mathf.Pow((i - 1), 3) + 60);
            sum += g;
            //int h = Mathf.RoundToInt((float)(f * g) / 5);
            sql.UpdateValues("LEVELEXP_PLAYER", new string[] { "EXP" }, new string[] { sum + "" }, "ID", "=", i + "");
        }
        */
        List<EquipUnit> list = GetEquipment();
        list.ForEach(p => Debug.Log(p.itemUnit.itemName));
    }

    public Dictionary<int, int> GetPlayerLevelExpDict()
    {
        Dictionary<int, int> levelDict = new Dictionary<int, int>();
        SqliteDataReader reader = sql.ReadFullTable("LEVELEXP_PLAYER");
        while (reader.Read())
        {
            int level = reader.GetInt32(reader.GetOrdinal("LEVEL"));
            int exp = reader.GetInt32(reader.GetOrdinal("EXP"));
            levelDict.Add(level, exp);
        }
        return levelDict;
    }

    public Dictionary<int, int> GetEnemyLevelExpDict()
    {
        Dictionary<int, int> levelDict = new Dictionary<int, int>();
        SqliteDataReader reader = sql.ReadFullTable("LEVELEXP_ENEMY");
        while (reader.Read())
        {
            int level = reader.GetInt32(reader.GetOrdinal("LEVEL"));
            int exp = reader.GetInt32(reader.GetOrdinal("EXP"));
            levelDict.Add(level, exp);
        }
        return levelDict;
    }

    /// <summary>
    /// 获取背包条目
    /// </summary>
    /// <returns></returns>
    public List<ItemUnit> GetItemUnits()
    {
        List<ItemUnit> itemUnits = new List<ItemUnit>();
        // 自定义SQL
        SqliteDataReader reader = sql.ExecuteQuery("select propsdef.*, playerbag.COUNT from playerbag left join propsdef on playerbag.propsid == propsdef.ID;");
        while (reader.Read())
        {
            ItemUnit item = new ItemUnit();
            item.itemId = reader.GetString(reader.GetOrdinal("ID"));
            item.itemName = reader.GetString(reader.GetOrdinal("NAME"));
            item.mainType = reader.GetInt32(reader.GetOrdinal("MAINTYPE"));
            item.subType = reader.GetInt32(reader.GetOrdinal("SUBTYPE"));
            item.hp = reader.GetInt32(reader.GetOrdinal("HP"));
            item.ep = reader.GetInt32(reader.GetOrdinal("EP"));
            item.cp = reader.GetInt32(reader.GetOrdinal("CP"));
            item.str = reader.GetInt32(reader.GetOrdinal("STR"));
            item.def = reader.GetInt32(reader.GetOrdinal("DEF"));
            item.ats = reader.GetInt32(reader.GetOrdinal("ATS"));
            item.adf = reader.GetInt32(reader.GetOrdinal("ADF"));
            item.spd = reader.GetInt32(reader.GetOrdinal("SPD"));
            item.dex = reader.GetInt32(reader.GetOrdinal("DEX"));
            item.crt = reader.GetInt32(reader.GetOrdinal("CRT"));
            item.hit = reader.GetInt32(reader.GetOrdinal("HIT"));
            item.lky = reader.GetInt32(reader.GetOrdinal("LKY"));
            item.rng = reader.GetInt32(reader.GetOrdinal("RNG"));
            item.price = reader.GetInt32(reader.GetOrdinal("PRICE"));
            item.count = reader.GetInt32(reader.GetOrdinal("COUNT"));
            itemUnits.Add(item);
        }
        return itemUnits;
    }

    /// <summary>
    /// 获取对应主类型的背包条目
    /// </summary>
    /// <returns></returns>
    public List<ItemUnit> GetItemUnitsByMainType(int mainType)
    {
        List<ItemUnit> itemUnits = new List<ItemUnit>();
        // 自定义SQL
        SqliteDataReader reader = sql.ExecuteQuery("select propsdef.*, playerbag.COUNT from playerbag left join propsdef on playerbag.propsid == propsdef.ID where propsdef.maintype = " + mainType + ";");
        while (reader.Read())
        {
            ItemUnit item = new ItemUnit();
            item.itemId = reader.GetString(reader.GetOrdinal("ID"));
            item.itemName = reader.GetString(reader.GetOrdinal("NAME"));
            item.mainType = reader.GetInt32(reader.GetOrdinal("MAINTYPE"));
            item.subType = reader.GetInt32(reader.GetOrdinal("SUBTYPE"));
            item.hp = reader.GetInt32(reader.GetOrdinal("HP"));
            item.ep = reader.GetInt32(reader.GetOrdinal("EP"));
            item.cp = reader.GetInt32(reader.GetOrdinal("CP"));
            item.str = reader.GetInt32(reader.GetOrdinal("STR"));
            item.def = reader.GetInt32(reader.GetOrdinal("DEF"));
            item.ats = reader.GetInt32(reader.GetOrdinal("ATS"));
            item.adf = reader.GetInt32(reader.GetOrdinal("ADF"));
            item.spd = reader.GetInt32(reader.GetOrdinal("SPD"));
            item.dex = reader.GetInt32(reader.GetOrdinal("DEX"));
            item.crt = reader.GetInt32(reader.GetOrdinal("CRT"));
            item.hit = reader.GetInt32(reader.GetOrdinal("HIT"));
            item.lky = reader.GetInt32(reader.GetOrdinal("LKY"));
            item.rng = reader.GetInt32(reader.GetOrdinal("RNG"));
            item.price = reader.GetInt32(reader.GetOrdinal("PRICE"));
            item.count = reader.GetInt32(reader.GetOrdinal("COUNT"));
            itemUnits.Add(item);
        }
        return itemUnits;
    }

    /// <summary>
    /// 获取背包条目
    /// </summary>
    /// <returns></returns>
    public ItemUnit GetItemUnitById(string unitId)
    {
        // 自定义SQL
        SqliteDataReader reader = sql.ExecuteQuery("select * from propsdef where id = '" + unitId + "';");
        while (reader.Read())
        {
            ItemUnit item = new ItemUnit();
            item.itemId = reader.GetString(reader.GetOrdinal("ID"));
            item.itemName = reader.GetString(reader.GetOrdinal("NAME"));
            item.mainType = reader.GetInt32(reader.GetOrdinal("MAINTYPE"));
            item.subType = reader.GetInt32(reader.GetOrdinal("SUBTYPE"));
            item.hp = reader.GetInt32(reader.GetOrdinal("HP"));
            item.ep = reader.GetInt32(reader.GetOrdinal("EP"));
            item.cp = reader.GetInt32(reader.GetOrdinal("CP"));
            item.str = reader.GetInt32(reader.GetOrdinal("STR"));
            item.def = reader.GetInt32(reader.GetOrdinal("DEF"));
            item.ats = reader.GetInt32(reader.GetOrdinal("ATS"));
            item.adf = reader.GetInt32(reader.GetOrdinal("ADF"));
            item.spd = reader.GetInt32(reader.GetOrdinal("SPD"));
            item.dex = reader.GetInt32(reader.GetOrdinal("DEX"));
            item.crt = reader.GetInt32(reader.GetOrdinal("CRT"));
            item.hit = reader.GetInt32(reader.GetOrdinal("HIT"));
            item.lky = reader.GetInt32(reader.GetOrdinal("LKY"));
            item.rng = reader.GetInt32(reader.GetOrdinal("RNG"));
            item.price = reader.GetInt32(reader.GetOrdinal("PRICE"));
            return item;
        }
        return null;
    }

    /// <summary>
    /// 通过角色ID获取角色基本能力值
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public RoleUnitDAO GetRoleUnitById(string id)
    {
        SqliteDataReader reader = sql.ReadTable("ROLEDEF", new string[] { "NAME", "HP", "EP", "CP", "STR", 
            "DEF", "ATS", "ADF", "SPD", "DEX", "RNG", "CRT", "HIT", "ROLETYPE", "WEARTYPE" }, new string[] { "ID" }, 
            new string[] { "=" }, new string[] { "'" + id + "'" });
        while (reader.Read())
        {
            RoleUnitDAO roleUnit = new RoleUnitDAO();
            roleUnit.unitId = id;
            roleUnit.unitName = reader.GetString(reader.GetOrdinal("NAME"));
            roleUnit.initHP = reader.GetInt32(reader.GetOrdinal("HP"));
            roleUnit.initEP = reader.GetInt32(reader.GetOrdinal("EP"));
            roleUnit.initCP = reader.GetInt32(reader.GetOrdinal("CP"));
            roleUnit.STR = reader.GetInt32(reader.GetOrdinal("STR"));
            roleUnit.DEF = reader.GetInt32(reader.GetOrdinal("DEF"));
            roleUnit.ATS = reader.GetInt32(reader.GetOrdinal("ATS"));
            roleUnit.ADF = reader.GetInt32(reader.GetOrdinal("ADF"));
            roleUnit.SPD = reader.GetInt32(reader.GetOrdinal("SPD"));
            roleUnit.DEX = reader.GetInt32(reader.GetOrdinal("DEX"));
            roleUnit.RNG = reader.GetInt32(reader.GetOrdinal("RNG"));
            roleUnit.CRT = reader.GetInt32(reader.GetOrdinal("CRT"));
            roleUnit.HIT = reader.GetInt32(reader.GetOrdinal("HIT"));
            roleUnit.roleType = reader.GetInt32(reader.GetOrdinal("ROLETYPE"));
            roleUnit.wearType = reader.GetInt32(reader.GetOrdinal("WEARTYPE"));
            return roleUnit;
        }
        return null;
    }

    /// <summary>
    /// 通过角色编号获取装备内容
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public List<EquipUnit> GetEquipment()
    {
        List<EquipUnit> equipUnits = new List<EquipUnit>();
        // 自定义SQL
        SqliteDataReader reader = sql.ExecuteQuery("select * from equipdef;");
        while (reader.Read())
        {
            EquipUnit equipUnit = new EquipUnit();
            equipUnit.equipId = reader.GetInt32(reader.GetOrdinal("ID"));
            equipUnit.equipType = reader.GetInt32(reader.GetOrdinal("EQUIPTYPE"));
            string roleId = reader.GetString(reader.GetOrdinal("ROLEID"));
            RoleUnitDAO roleUnit = GetRoleUnitById(roleId);
            equipUnit.roleUnit = roleUnit;
            string itemId = reader.GetString(reader.GetOrdinal("PROPSID"));
            ItemUnit itemUnit = GetItemUnitById(itemId);
            equipUnit.itemUnit = itemUnit;
            equipUnits.Add(equipUnit);
        }
        return equipUnits;
    }

    public void UpdateEquipment(string propsId, string roleId, int equipType)
    {
        SqliteDataReader reader = sql.ExecuteQuery("update equipdef set propsid = '" + propsId + 
            "' where roleid = '" + roleId + "' and equiptype = " + equipType + ";");
    }

    /// <summary>
    /// 通过类型编号获取对应的背包中装备
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<ItemUnit> GetItemUnitByType(int equipType, int wearType)
    {
        // 获取所有背包内容
        List<ItemUnit> itemUnits = GetItemUnits();
        List<ItemUnit> provideUnits = new List<ItemUnit>();
        switch (equipType)
        {
            case 0:
                provideUnits = itemUnits.Where(p => p.mainType == 1 && p.subType == wearType).ToList();
                break;
            case 1:
                provideUnits = itemUnits.Where(p => p.mainType == 1 && p.subType == 6).ToList();
                break;
            case 2:
                provideUnits = itemUnits.Where(p => p.mainType == 1 && p.subType == 6).ToList();
                break;
            case 3:
                provideUnits = itemUnits.Where(p => p.mainType == 0 && p.subType == 0).ToList();
                break;
            case 4:
                provideUnits = itemUnits.Where(p => p.mainType == 0 && p.subType == 1).ToList();
                break;
            case 5:
                provideUnits = itemUnits.Where(p => p.mainType == 0 && p.subType == 2).ToList();
                break;
            case 6:
                provideUnits = itemUnits.Where(p => p.mainType == 0 && p.subType == 3).ToList();
                break;

        }
        return provideUnits;
    }

    /// <summary>
    /// 获取所有技能
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public List<SkillDAO> GetSkills()
    {
        List<SkillDAO> skills = new List<SkillDAO>();
        // 自定义SQL
        SqliteDataReader reader = sql.ExecuteQuery("select * from SKILLDEF;");
        while (reader.Read())
        {
            SkillDAO skillItem = new SkillDAO();
            skillItem.id = reader.GetString(reader.GetOrdinal("ID"));
            skillItem.name = reader.GetString(reader.GetOrdinal("NAME"));
            skillItem.skillType = reader.GetInt32(reader.GetOrdinal("SKILLTYPE"));
            skillItem.desp = reader.GetString(reader.GetOrdinal("DESP"));
            string roleId = reader.GetString(reader.GetOrdinal("ROLEID"));
            RoleUnitDAO roleUnit = GetRoleUnitById(roleId);
            skillItem.roleUnit = roleUnit;
            skillItem.consume = reader.GetInt32(reader.GetOrdinal("CONSUME"));
            skillItem.hp = reader.GetInt32(reader.GetOrdinal("HP"));
            skillItem.ep = reader.GetInt32(reader.GetOrdinal("EP"));
            skillItem.cp = reader.GetInt32(reader.GetOrdinal("CP"));
            skillItem.str = reader.GetInt32(reader.GetOrdinal("STR"));
            skillItem.def = reader.GetInt32(reader.GetOrdinal("DEF"));
            skillItem.ats = reader.GetInt32(reader.GetOrdinal("ATS"));
            skillItem.adf = reader.GetInt32(reader.GetOrdinal("ADF"));
            skillItem.spd = reader.GetInt32(reader.GetOrdinal("SPD"));
            skillItem.dex = reader.GetInt32(reader.GetOrdinal("DEX"));
            skillItem.rng = reader.GetInt32(reader.GetOrdinal("RNG"));
            skillItem.crt = reader.GetInt32(reader.GetOrdinal("CRT"));
            skillItem.hit = reader.GetInt32(reader.GetOrdinal("HIT"));
            skillItem.dot = reader.GetInt32(reader.GetOrdinal("DOT"));
            skillItem.multi = reader.GetInt32(reader.GetOrdinal("MULTI"));
            skillItem.target = reader.GetInt32(reader.GetOrdinal("TARGET"));
            skills.Add(skillItem);
        }
        return skills;
    }

    public List<SkillDAO> GetMagicsByRoleId(string roleId)
    {
        List<SkillDAO> skills = new List<SkillDAO>();
        // 自定义SQL
        SqliteDataReader reader = sql.ExecuteQuery("select * from SKILLDEF where roleid = '" + roleId + "' and skilltype = 0;");
        while (reader.Read())
        {
            SkillDAO skillItem = new SkillDAO();
            skillItem.id = reader.GetString(reader.GetOrdinal("ID"));
            skillItem.name = reader.GetString(reader.GetOrdinal("NAME"));
            skillItem.skillType = reader.GetInt32(reader.GetOrdinal("SKILLTYPE"));
            skillItem.desp = reader.GetString(reader.GetOrdinal("DESP"));
            string tempRoleId = reader.GetString(reader.GetOrdinal("ROLEID"));
            RoleUnitDAO roleUnit = GetRoleUnitById(tempRoleId);
            skillItem.roleUnit = roleUnit;
            skillItem.consume = reader.GetInt32(reader.GetOrdinal("CONSUME"));
            skillItem.hp = reader.GetInt32(reader.GetOrdinal("HP"));
            skillItem.ep = reader.GetInt32(reader.GetOrdinal("EP"));
            skillItem.cp = reader.GetInt32(reader.GetOrdinal("CP"));
            skillItem.str = reader.GetInt32(reader.GetOrdinal("STR"));
            skillItem.def = reader.GetInt32(reader.GetOrdinal("DEF"));
            skillItem.ats = reader.GetInt32(reader.GetOrdinal("ATS"));
            skillItem.adf = reader.GetInt32(reader.GetOrdinal("ADF"));
            skillItem.spd = reader.GetInt32(reader.GetOrdinal("SPD"));
            skillItem.dex = reader.GetInt32(reader.GetOrdinal("DEX"));
            skillItem.rng = reader.GetInt32(reader.GetOrdinal("RNG"));
            skillItem.crt = reader.GetInt32(reader.GetOrdinal("CRT"));
            skillItem.hit = reader.GetInt32(reader.GetOrdinal("HIT"));
            skillItem.dot = reader.GetInt32(reader.GetOrdinal("DOT"));
            skillItem.multi = reader.GetInt32(reader.GetOrdinal("MULTI"));
            skillItem.target = reader.GetInt32(reader.GetOrdinal("TARGET"));
            skills.Add(skillItem);
        }
        return skills;
    }

    public List<SkillDAO> GetTacticsByRoleId(string roleId)
    {
        List<SkillDAO> skills = new List<SkillDAO>();
        // 自定义SQL
        SqliteDataReader reader = sql.ExecuteQuery("select * from SKILLDEF where roleid = '" + roleId + "' and skilltype = 1;");
        while (reader.Read())
        {
            SkillDAO skillItem = new SkillDAO();
            skillItem.id = reader.GetString(reader.GetOrdinal("ID"));
            skillItem.name = reader.GetString(reader.GetOrdinal("NAME"));
            skillItem.skillType = reader.GetInt32(reader.GetOrdinal("SKILLTYPE"));
            skillItem.desp = reader.GetString(reader.GetOrdinal("DESP"));
            string tempRoleId = reader.GetString(reader.GetOrdinal("ROLEID"));
            RoleUnitDAO roleUnit = GetRoleUnitById(tempRoleId);
            skillItem.roleUnit = roleUnit;
            skillItem.consume = reader.GetInt32(reader.GetOrdinal("CONSUME"));
            skillItem.hp = reader.GetInt32(reader.GetOrdinal("HP"));
            skillItem.ep = reader.GetInt32(reader.GetOrdinal("EP"));
            skillItem.cp = reader.GetInt32(reader.GetOrdinal("CP"));
            skillItem.str = reader.GetInt32(reader.GetOrdinal("STR"));
            skillItem.def = reader.GetInt32(reader.GetOrdinal("DEF"));
            skillItem.ats = reader.GetInt32(reader.GetOrdinal("ATS"));
            skillItem.adf = reader.GetInt32(reader.GetOrdinal("ADF"));
            skillItem.spd = reader.GetInt32(reader.GetOrdinal("SPD"));
            skillItem.dex = reader.GetInt32(reader.GetOrdinal("DEX"));
            skillItem.rng = reader.GetInt32(reader.GetOrdinal("RNG"));
            skillItem.crt = reader.GetInt32(reader.GetOrdinal("CRT"));
            skillItem.hit = reader.GetInt32(reader.GetOrdinal("HIT"));
            skillItem.dot = reader.GetInt32(reader.GetOrdinal("DOT"));
            skillItem.multi = reader.GetInt32(reader.GetOrdinal("MULTI"));
            skillItem.target = reader.GetInt32(reader.GetOrdinal("TARGET"));
            skills.Add(skillItem);
        }
        return skills;
    }

    public void Close()
    {
        //关闭数据库连接
        sql.CloseConnection();
    }
}

public class SQLiteHelper
{
    /// <summary>
    /// 数据库连接定义
    /// </summary>
    private SqliteConnection dbConnection;

    /// <summary>
    /// SQL命令定义
    /// </summary>
    private SqliteCommand dbCommand;

    /// <summary>
    /// 数据读取定义
    /// </summary>
    private SqliteDataReader dataReader;

    /// <summary>
    /// 构造函数    
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    public SQLiteHelper(string connectionString)
    {
        try
        {
            //构造数据库连接
            dbConnection = new SqliteConnection(connectionString);
            //打开数据库
            dbConnection.Open();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// 执行SQL命令
    /// </summary>
    /// <returns>The query.</returns>
    /// <param name="queryString">SQL命令字符串</param>
    public SqliteDataReader ExecuteQuery(string queryString)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }

    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public void CloseConnection()
    {
        //销毁Command
        if (dbCommand != null)
        {
            dbCommand.Cancel();
        }
        dbCommand = null;

        //销毁Reader
        if (dataReader != null)
        {
            dataReader.Close();
        }
        dataReader = null;

        //销毁Connection
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
    }

    /// <summary>
    /// 读取整张数据表
    /// </summary>
    /// <returns>The full table.</returns>
    /// <param name="tableName">数据表名称</param>
    public SqliteDataReader ReadFullTable(string tableName)
    {
        string queryString = "SELECT * FROM " + tableName;
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 向指定数据表中插入数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="values">插入的数值</param>
    public SqliteDataReader InsertValues(string tableName, string[] values)
    {
        //获取数据表中字段数目
        int fieldCount = ReadFullTable(tableName).FieldCount;
        //当插入的数据长度不等于字段数目时引发异常
        if (values.Length != fieldCount)
        {
            throw new SqliteException("values.Length!=fieldCount");
        }

        string queryString = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; i++)
        {
            queryString += ", " + values[i];
        }
        queryString += " )";
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 更新指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    /// <param name="key">关键字</param>
    /// <param name="value">关键字对应的值</param>
    public SqliteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string operation, string value)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length");
        }

        string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += ", " + colNames[i] + "=" + colValues[i];
        }
        queryString += " WHERE " + key + operation + value;
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 删除指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    public SqliteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += "OR " + colNames[i] + operations[0] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 删除指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    public SqliteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += " AND " + colNames[i] + operations[i] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 创建数据表
    /// </summary> +
    /// <returns>The table.</returns>
    /// <param name="tableName">数据表名</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colTypes">字段名类型</param>
    public SqliteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
    {
        string queryString = "CREATE TABLE " + tableName + "( " + colNames[0] + " " + colTypes[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            queryString += ", " + colNames[i] + " " + colTypes[i];
        }
        queryString += "  ) ";
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// Reads the table.
    /// </summary>
    /// <returns>The table.</returns>
    /// <param name="tableName">Table name.</param>
    /// <param name="items">Items.</param>
    /// <param name="colNames">Col names.</param>
    /// <param name="operations">Operations.</param>
    /// <param name="colValues">Col values.</param>
    public SqliteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
    {
        string queryString = "SELECT " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            queryString += ", " + items[i];
        }
        queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
        for (int i = 0; i < colNames.Length; i++)
        {
            queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
        }
        return ExecuteQuery(queryString);
    }
}