using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;

    Vector2 offset;
    Material material;

    void Awake() {
        material= GetComponent<SpriteRenderer>().material;
    }
    // Start is called before the first frame update
    void Update()
    {
        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
