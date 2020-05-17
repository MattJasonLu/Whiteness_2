using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUnitDAO
{
    // 编号
    public string id;
    // 标题
    public string title;
    // 描述
    public string desp;
    // 发起人编号
    public string initRoleId;
    // 目标人编号
    public string destRoleId;
    // 目标物品编号
    public string destItemId;
    // 目标数量
    public int targetCount;
    // 已完成数量
    public int completedCount;
    // 任务类型 0:主线, 1:支线, 3:其他
    public int mainType;
    // 目标类型 0:怪物, 1:角色, 2:物品
    public int subType;
    // 状态 0:未领取, 1:已领取, 2:已完成, 3:已失效
    public int state;
}
