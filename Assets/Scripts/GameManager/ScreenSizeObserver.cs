using System;
using UnityEngine;

public class ScreenSizeObserver : MonoBehaviour
{
    public static event Action<Vector2Int> OnScreenSizeChanged;

    private Vector2Int previousSize;

    void Start()
    {
        previousSize = new Vector2Int(Screen.width, Screen.height);
        OnScreenSizeChanged?.Invoke(previousSize); // Appelle au démarrage si utile
    }

    void Update()
    {
        Vector2Int currentSize = new Vector2Int(Screen.width, Screen.height);
        if (currentSize != previousSize)
        {
            previousSize = currentSize;
            OnScreenSizeChanged?.Invoke(currentSize);
        }
    }
}
