using System.Collections.Generic;
using System;

[Serializable]
public class AttackData
{
    public List<AttackInfoWrapper> attacks;
}

[Serializable]
public class AttackInfoWrapper
{
    public string key; // キーとして使いたい文字列
    public AttackInfo attackInfo; // AttackInfoオブジェクト
}

[Serializable]
public class AttackInfo
{
    public int startup;
    public int active;
    public int recovery;
    public int transitionFrames;
    public int damage;
}