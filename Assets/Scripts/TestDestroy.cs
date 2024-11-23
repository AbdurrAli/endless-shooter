using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDestroy : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(player, 3);
    }
}
