using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDAO
{
    public string id;
    public string name;
    // 0为魔法，1为战技
    public int skillType;
    // 属性
    public AdditionType additionType;
    // 描述
    public string desp;
    public RoleUnitDAO roleUnit;
    // 所需ep或cp量
    public int consume;
    public int hp;
    public int ep;
    public int cp;
    public int str;
    public int def;
    public int ats;
    public int adf;
    public int spd;
    public int dex;
    public int rng;
    public int crt;
    public int hit;
    // dot回合数，默认0
    public int dot;
    // 是否为群体技能，0为单体，1为群体
    public int multi;
    // 受益或损益目标，0为敌人，1为玩家角色
    public int target;
}
