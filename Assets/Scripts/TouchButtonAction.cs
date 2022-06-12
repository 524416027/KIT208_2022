using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchButtonAction : MonoBehaviour
{
    [SerializeField] private UnityEvent actionEvent = null;

    public void ActionPerform()
    {
        actionEvent.Invoke();
    }
}
