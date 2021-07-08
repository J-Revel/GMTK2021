using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSidedSprite : MonoBehaviour
{
    public Vector3 A;
    public Vector3 B;
    public float f;
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock propertyBlock;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        propertyBlock = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(propertyBlock);
    }

    void Update()
    {
        A = transform.parent.forward;
        B = Camera.main.transform.forward;
        f = Vector3.Dot(A, B);
        transform.localRotation = f > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        propertyBlock.SetFloat("Flip", f > 0 ? 0f : 1.0f);
        spriteRenderer.SetPropertyBlock(propertyBlock);
    }
}
