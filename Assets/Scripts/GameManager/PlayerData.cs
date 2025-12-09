using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    public PlayerMovement Movement { get; private set; }
    public PlayerHealth Health { get; private set; }

    void Awake()
    {
        if (Instance == null) 
            Instance = this;
        else { Destroy(gameObject); return; }

        Movement = GetComponent<PlayerMovement>();
        Health = GetComponent<PlayerHealth>();
    }
}