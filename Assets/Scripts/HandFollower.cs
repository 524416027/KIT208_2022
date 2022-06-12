using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollower : MonoBehaviour
{
    private Rigidbody rb = null;
    [SerializeField] private GameObject _followTarget = null;
    public GameObject FollowTarget
    {
        get { return _followTarget; }
        set { _followTarget = value; }
    }
    [SerializeField] private bool _isFollowing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _followTarget.GetComponent<PlayerGrab>().HandGrabContainerObj = this.gameObject;
    }

    private void FixedUpdate()
    {
        if (_isFollowing)
        {
            //credit to: https://www.youtube.com/watch?v=VG8hLKyTiJQ
            rb.velocity = (_followTarget.transform.position - this.transform.position) / Time.fixedDeltaTime;

            Quaternion rotationDifference = _followTarget.transform.rotation * Quaternion.Inverse(this.transform.rotation);
            rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
            Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

            rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
        }
    }

    public void ToggleIsFollowing()
    {
        _isFollowing = !_isFollowing;
    }
}
