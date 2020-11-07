using Invector.vMelee;
using UnityEngine;

public class AllyNpc : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField]
    private GameObject[] weaponObjects;
    [SerializeField]
    private Quaternion weaponRotation;
    [SerializeField]
    private Vector3 weaponPosition;

    [Header("Skins")]
    [SerializeField]
    private GameObject[] skins;
    [SerializeField]
    private GameObject helmet;

    [SerializeField]
    private Transform rightHand;
    [SerializeField]
    private Transform leftHand;

    private void Start()
    {
        var weaponIndex = AllySpawner.weaponLevel;
        //var weaponIndex = Random.Range(0, weaponObjects.Length);
        var weapon = Instantiate(weaponObjects[weaponIndex], rightHand, true);
        weapon.transform.position = weaponPosition;
        weapon.transform.rotation = weaponRotation;

        GetComponent<vMeleeManager>().SetRightWeapon(weapon);

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