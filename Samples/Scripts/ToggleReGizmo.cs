using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleReGizmo : MonoBehaviour
{
    bool active;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            active = !active;
            ReGizmo.Core.ReGizmo.SetActive(active);
        }
    }
}
