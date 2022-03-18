using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StapleShoot : MonoBehaviour
{
    public int power = 700;
    public void Shoot() {
        // instantiate prefab
        GameObject StaplePrefab = (GameObject)Resources.Load("Prefabs/Staple", typeof(GameObject));
        GameObject Staple = Instantiate(StaplePrefab, transform.position, Quaternion.identity);
        Staple.GetComponent<Rigidbody>().AddForce(transform.right * power);
    }
}