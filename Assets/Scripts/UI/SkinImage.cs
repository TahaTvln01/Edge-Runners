using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinImage : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private void OnEnable()
    {
        SetupSkinImage();
    }
    public void SetupSkinImage()
    {
        anim.SetInteger("SkinID", PlayerPrefs.GetInt("EquipedSkin"));
        PlayerManager.instance.choosenSkinID = PlayerPrefs.GetInt("EquipedSkin");
    }
}
