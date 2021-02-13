using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    [SerializeField]
    HPValue hp = null;
    [SerializeField]
    Image _heart = null;
    [SerializeField]
    Sprite[] _heartState = null;

    private void Start()
    {
        hp.OnVariableChange += VariableChangeHandler;
    }

    private void VariableChangeHandler(int newVal)
    {
        Debug.Log(newVal);
        _heart.sprite = _heartState[newVal];
    }
}
