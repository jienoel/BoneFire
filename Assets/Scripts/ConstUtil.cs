using System.Collections;
using System.Collections.Generic;

public class ColorTable
{
    public const int Red = 0;
    public const int Yellow = 1;
    public const int Blue = 2;
    public const int Max = 3;

    public static bool isColorValid(int color)
    {
        return color < Max;
    }
}

public class BodyID
{
    public const int Zero = 0;
    public const int One = 1;
    public const int Two = 2;
    public const int Three = 3;
    public const int Four = 4;
}


public class AnimatorParam
{
    public const string Color = "Color";


    public const string BoolDie = "Die";


    public const string FloatSpeed = "Speed";

    public const string TriggerHurt = "Hurt";

    public const string TrigerAttackPre = "AttackPre";

    public const string TriggerAttack = "Attack";

    public const string BoolHasAtkTarget = "HasAtkTarget";
}

public enum AnimatorParamType
{
    Float,
    Trigger,
    Bool,
    Int
}



