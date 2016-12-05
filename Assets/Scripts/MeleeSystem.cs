using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

    public int minDamage = 25;
    public int maxDamage = 50;
    public float weaponRange = 3.5f;

    public Camera FPCamera;

    public TreeHealth treeHealth;

    private void Update()
    {
        Ray ray = FPCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hitInfo;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hitInfo, weaponRange))
            {
                if (hitInfo.collider.tag == "Tree") ;
                {
                    treeHealth = hitInfo.collider.GetComponentInParent<TreeHealth>();
                    AttackTree();
                }
            }
        }
    }

    private void AttackTree()
    {
        int damge = Random.Range(minDamage, maxDamage);
        treeHealth.health -= damge;
    }
}


