using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    [SerializeField] private Collider[] colliders = null;
    [SerializeField] private PlayerGrab _onGrabbingScript = null;
    [SerializeField] private GameObject ringMeshObj = null;
    [SerializeField] private float currentSpinSpeed = 0f;
    [SerializeField] private float[] spinSpeed = new float[] { 0f, 5f, 15f, 25f };

    public PlayerGrab OnGrabbingScript
    {
        get { return _onGrabbingScript; }
        set { _onGrabbingScript = value; }
    }
    [SerializeField] private Transform startPoint = null;

    private void Update()
    {
        ringMeshObj.transform.Rotate(Vector3.forward * currentSpinSpeed * Time.deltaTime, Space.Self);
    }

    public void ResetRound()
    {
        _onGrabbingScript.ReleaseHolding();

        this.gameObject.transform.position = startPoint.position;
        this.gameObject.transform.rotation = startPoint.rotation; 
    }

    public void ChangeColliderType(bool isTrigger)
    {
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = isTrigger;
        }
    }

    public void ChangeSpinSpeed(int index)
    {
        currentSpinSpeed = spinSpeed[index];
        Debug.Log("current spin speed is: " + currentSpinSpeed);
    }
}
