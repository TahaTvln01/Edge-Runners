using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Player player;
    [SerializeField] private float direction;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PlayerManager.instance.currentPlayer != null)
        {
            player = PlayerManager.instance.currentPlayer.GetComponent<Player>();
            player.ButtonXInput = direction;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PlayerManager.instance.currentPlayer != null)
        {
            player = PlayerManager.instance.currentPlayer.GetComponent<Player>();
            player.ButtonXInput = 0;
        }
    }
}