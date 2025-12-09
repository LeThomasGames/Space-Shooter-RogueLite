using UnityEngine;
public enum AsteroidSize
{
    Huge,
    Large,
    Medium,
    Small
}

[CreateAssetMenu(fileName = "AsteroidData", menuName = "ScriptableObject/Asteroid Data")]
public class AsteroidData : ScriptableObject
{
    public AsteroidSize size;
    public int maxHealth;
    public Sprite[] sprites;
    public int contactDamage;
    public float minSplitForce = 1f;
    public float maxSplitForce = 3f;
}