using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionAfterBurn : MonoBehaviour
{
    public SpriteRenderer render;
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            render.enabled = true;
        }
        else
        {
            render.enabled = false;
        }
    }
}
