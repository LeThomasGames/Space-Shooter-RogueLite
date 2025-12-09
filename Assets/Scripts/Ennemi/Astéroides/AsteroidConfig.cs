using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidConfig", menuName = "ScriptableObject/Asteroid Config")]
public class AsteroidConfig : ScriptableObject
{
    public AsteroidData[] allDatas;
}