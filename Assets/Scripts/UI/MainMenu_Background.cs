using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Background : MonoBehaviour
{
    [SerializeField] private Vector2 backgroundSpeed;
    [SerializeField] private MeshRenderer mesh;

    private void Update()
    {
        mesh.material.mainTextureOffset += backgroundSpeed * Time.deltaTime;
    }
}
