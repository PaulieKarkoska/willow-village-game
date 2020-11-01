using Invector.vCharacterController.AI;
using Invector.vMelee;
using UnityEngine;

public class AllyNpc : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField]
    private GameObject[] weaponObjects;

    private void Start()
    {
        var weaponIndex = Random.Range(0, weaponObjects.Length);
        var weapon = weaponObjects[weaponIndex];
        weapon.SetActive(true);
        GetComponent<vMeleeManager>().SetRightWeapon(weapon);
    }
}