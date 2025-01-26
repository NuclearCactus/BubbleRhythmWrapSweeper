using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIsableOnWebGLBuild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        return;
#else
        Destroy(gameObject);
#endif

    }

}
