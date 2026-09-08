using UnityEngine;

namespace HoaR.Turret.Shooting
{
    public class BulletDamageDealer : MonoBehaviour
    {
        void OnTriggerEnter(Collider collider)
        {
            Debug.Log(collider.transform.parent.name);
        }
    }
}