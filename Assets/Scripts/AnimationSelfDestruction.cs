using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSelfDestruction : MonoBehaviour
{
    [SerializeField] private float destructionDelay = -1f;

    private void Start() {
        if(destructionDelay != -1f)
        {
            Destroy(this.gameObject.transform.parent.gameObject, destructionDelay);
        }
    }

    public void SelfDestruction()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
