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

    [SerializeField]
    private Transform rightHand;
    [SerializeField]
    private Transform leftHand;

    private void Start()
    {
        var weaponIndex = Random.Range(0, weaponObjects.Length);
        var weapon = weaponObjects[weaponIndex];
        weapon.SetActive(true);
        GetComponent<vMeleeManager>().SetRightWeapon(weapon);

        SetSkin();
    }

    private void SetSkin()
    {
        var chosenSkin = Random.Range(1, 2);
        if (chosenSkin == 1)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}