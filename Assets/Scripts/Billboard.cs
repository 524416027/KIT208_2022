using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform target = null;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        this.gameObject.transform.LookAt(target);
    }
}
