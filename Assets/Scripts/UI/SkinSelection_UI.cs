using TMPro;
using UnityEngine;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private int[] skinPrice;
    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private bool[] skinEquiped;
    private int SkinID = 0;

    [Header("Components")]
    [SerializeField] Animator anim;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject equipButton;
    [SerializeField] GameObject equiped;
    [SerializeField] private TextMeshProUGUI bankText;
    public void SetupSkinInfo()
    {

        for (int i = 1; i < skinPurchased.Length; i++)
        {
            bool skinUnlocked = PlayerPrefs.GetInt("SkinPurchased" + SkinID) == 1;

            if (skinUnlocked)
            {
                skinPurchased[i] = true;
            }else
            {
                skinPurchased[i] = false;
            }
        }

        if (!skinPurchased[SkinID])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = skinPrice[SkinID].ToString();

        buyButton.SetActive(!skinPurchased[SkinID]);
        equipButton.SetActive(skinPurchased[SkinID] & !skinEquiped[SkinID]);
        equiped.SetActive(skinEquiped[SkinID]);
        anim.SetInteger("SkinID", SkinID);
    }

    public bool EnoughMoney()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");

        if (totalFruits >= skinPrice[SkinID])
        {
            totalFruits = totalFruits - skinPrice[SkinID];
            PlayerPrefs.SetInt("TotalFruitsCollected", totalFruits);
            AudioManager.instance.PlaySFX(5);
            return true;
        }
        AudioManager.instance.PlaySFX(7);
        return false;
    }
    public void Buy()
    {
        if (EnoughMoney())
        {
            PlayerPrefs.SetInt("SkinPurchased" + SkinID, 1);
            skinPurchased[SkinID] = true;
            SetupSkinInfo();
            bankText.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();
        }
    }

    public void Equip()
    {
        for (int i = 0; i < skinPrice.Length; i++)
        {
            skinEquiped[i] = false;
        }
        AudioManager.instance.PlaySFX(4);
        skinEquiped[SkinID] = true;
        PlayerManager.instance.choosenSkinID = SkinID;
        SetupSkinInfo();
        PlayerPrefs.SetInt("EquipedSkin", SkinID);
    }

    private void OnEnable()
    {
        SkinID = PlayerPrefs.GetInt("EquipedSkin");
        Equip();
        bankText.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();
    }
    public void NextSkin()
    {
        SkinID++;
        if (SkinID > 3)
            SkinID = 0;
        AudioManager.instance.PlaySFX(4);
        SetupSkinInfo();
    }
    public void PreviousSkin()
    {
        SkinID--;
        if (SkinID < 0)
            SkinID = 3;
        AudioManager.instance.PlaySFX(4);
        SetupSkinInfo();
    }
}
