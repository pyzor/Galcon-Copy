using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ShipMaterial : MonoBehaviour, IMaterialPropertyColor {
    
    private MeshRenderer _meshRenderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    public void SetColor(Color color) {
        _materialPropertyBlock.SetColor(MaterialProperties.Color, color);
        ApplyPropertyBlock();
    }

    private void ApplyPropertyBlock() {
        _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
    }

    private void Awake() {
        _materialPropertyBlock = new MaterialPropertyBlock();
        _materialPropertyBlock.SetColor(MaterialProperties.Color, Color.white);
        _meshRenderer = GetComponent<MeshRenderer>();
        ApplyPropertyBlock();
    }
}
