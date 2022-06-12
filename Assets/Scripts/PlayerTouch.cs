using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent.gameObject.GetComponent<TouchButtonAction>().ActionPerform();
    }
}
