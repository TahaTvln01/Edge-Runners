using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject myCamera;
    [SerializeField] private PolygonCollider2D cd;
    [SerializeField] private Color gizmosColor;

    private void Start()
    {
        FallowPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            myCamera.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            myCamera.SetActive(false);
    }

    private void Update()
    {
        if (PlayerManager.instance.playerJustRespawned)
        {
            FallowPlayer();
        }
        if (PlayerManager.instance.currentPlayer == null && myCamera.activeSelf)
        {
            myCamera.SetActive(false);
        }
    }

    public void FallowPlayer()
    {
        if (PlayerManager.instance.currentPlayer != null)
            myCamera.GetComponent<CinemachineVirtualCamera>().Follow = PlayerManager.instance.currentPlayer.transform;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(cd.bounds.center, cd.bounds.size);
    }
}
