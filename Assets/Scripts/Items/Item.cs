using UnityEngine;

public class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public float Weight;
    public bool IsStackable;
    public int Stacks;
    public int MaxStacks;

    public object Clone()
    {
        return MemberwiseClone();
    }
}
