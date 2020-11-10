using Invector.vMelee;
using UnityEngine;

public class AllyNpc : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private vMeleeManager meleeManager;

    [Header("Skins")]
    [SerializeField]
    private GameObject[] skins;
    [SerializeField]
    private GameObject helmet;

    private void Start()
    {
        if (Shield_Buyable.shieldIsPurchased)
        {
            weapon.GetComponent<vMeleeWeapon>().enabled = true;
            weapon.SetActive(true);
            meleeManager.SetLeftWeapon(weapon);
        }

        if (Sword_Buyable.swordIsPurchased)
        {
            shield.GetComponent<vMeleeWeapon>().enabled = true;
            shield.SetActive(true);
            meleeManager.SetRightWeapon(shield);
        }

        SetSkin();
    }

    private void SetSkin()
    {
        foreach (var skin in skins)
            skin.SetActive(false);

        skins[Random.Range(0, 2)].SetActive(true);

        if (AllySpawner.armorLevel >= 2)
            helmet.SetActive(true);
    }
}