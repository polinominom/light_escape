using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNamesUtil : MonoBehaviour {


	public static string walk = "creature1walkforward";
	public static string idle = "creature1Idle";
	public static string attackOne = "creature1Attack1";
	public static string attackTwo = "creature1Attack2";
	public static string attackThree = "creature1Attack3";
	public static string die = "creature1Die";
	public static string gethit = "creature1GetHit";
	public static string taunt = "creature1Taunt";
	public static string spawn = "creature1Spawn";
	public static string roar = "creature1roar";

	public static string GetAnimName(string enemy, string name)
	{
		if (enemy.Equals ("Monster")) {
			if (name.Equals ("idle"))
				return idle;
			else if (name.Equals ("walk"))
				return walk;
			else if (name.Equals ("attack"))
				return attackTwo;
			else
				return idle;
		} else if (enemy.Equals ("LittleMonster")) {
			if (name.Equals ("idle"))
				return LittleMonsterAnimationNames.idle;
			else if (name.Equals ("walk"))
				return LittleMonsterAnimationNames.walk;
			else if (name.Equals ("attack"))
				return LittleMonsterAnimationNames.normalAttackOne;
			else
				return LittleMonsterAnimationNames.idle;
		} else
			return idle;
	}
}
