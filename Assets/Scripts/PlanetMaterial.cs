using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PlanetMaterial : MonoBehaviour, IMaterialPropertyColor, IMaterialPropertySelected {

    private MeshRenderer _meshRenderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    public void SetColor(Color color) {
        _materialPropertyBlock.SetColor(MaterialProperties.Color, color);
        ApplyPropertyBlock();
    }

    public void SetSelected(bool state) {
        _materialPropertyBlock.SetFloat(MaterialProperties.Selected, (state) ? 1f : 0f);
        ApplyPropertyBlock();
    }

    private void ApplyPropertyBlock() {
        _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
    }

    private void Awake() {
        _materialPropertyBlock = new MaterialPropertyBlock();
        _materialPropertyBlock.SetColor(MaterialProperties.Color, Color.white);
        _materialPropertyBlock.SetFloat(MaterialProperties.Selected, 0);
        _meshRenderer = GetComponent<MeshRenderer>();
        ApplyPropertyBlock();
    }
}
