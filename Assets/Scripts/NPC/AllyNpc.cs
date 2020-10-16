using Invector.vCharacterController.AI;
using Invector.vMelee;
using UnityEngine;

public class AllyNpc : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField]
    private GameObject[] weaponObjects;

    static System.Random rand = new System.Random();

    private void Start()
    {
        var weaponIndex = rand.Next(weaponObjects.Length);
        var weapon = weaponObjects[weaponIndex];
        weapon.SetActive(true);
        GetComponent<vMeleeManager>().SetRightWeapon(weapon);
    }
}