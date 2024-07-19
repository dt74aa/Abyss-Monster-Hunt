using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBuilet : MonoBehaviour
{
    public float Time;
    void Start()
    {
        Destroy(this.gameObject, Time);
    }

}
