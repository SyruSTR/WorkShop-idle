using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePanel : MonoBehaviour
{
    private bool _woodPanel = true;

    private void Start()
    {
        _woodPanel = true;
    }
    public void RotatePanel_(bool activeWood)
    {
        Debug.Log($"{_woodPanel ^ activeWood}");
        if (_woodPanel ^ activeWood)
        {
            if (_woodPanel)
                transform.rotation = new Quaternion(0, 180.0f, 0, 0);
            else
                transform.rotation = new Quaternion(0, 0, 0, 0);

            _woodPanel = activeWood;
        }
    }

}
