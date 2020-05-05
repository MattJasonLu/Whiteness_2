using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// 攻击属性增幅
/// </summary>
public class AttackAddition {

	// 属性
	public AdditionType additionType = 0;
	// 增幅
	public int HPOffset;
	public int EPOffset;
	public int CPOffset;
	public int STROffset;
	public int DEFOffset;
	public int ATSOffset;
	public int ADFOffset;
	public int SPDOffset;
	public int DEXOffset;
	public int CRTOffset;
	public int HITOffset;
	// 增幅比例
	public int HPOffsetRatio;
	public int EPOffsetRatio;
	public int CPOffsetRatio;
	public int STROffsetRatio;
	public int DEFOffsetRatio;
	public int ATSOffsetRatio;
	public int ADFOffsetRatio;
	public int SPDOffsetRatio;
	public int DEXOffsetRatio;
	public int CRTOffsetRatio;
	public int HITOffsetRatio;
	// dot回合数
	public int dotDamage;
	public int turnCount;

}

public enum AdditionType
{
	[Description("普")]
	Common = 0,     // 普
	[Description("金")]
	Venus = 1,      // 金
	[Description("木")]
	Jupiter = 2,    // 木
	[Description("水")]
	Mercury = 3,    // 水
	[Description("火")]
	Mars = 4,       // 火
	[Description("土")]
	Saturn = 5,     // 土
	[Description("风")]
	Uranus = 6,     // 风
	[Description("冰")]
	Neptune = 7,    // 冰
	[Description("暗")]
	Pluto = 8		// 暗
}