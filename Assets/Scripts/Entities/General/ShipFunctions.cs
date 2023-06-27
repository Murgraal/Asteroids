using Data.ScriptableObjects;
using Gameflow;
using UnityEngine;

namespace Entities.General
{
    public static class ShipFunctions
    {
        public static void RotateShip(InputData data, Transform transform, float rotateSpeed)
        {
            if (data.LeftPressed)
            {
                transform.Rotate(0,0,1 * rotateSpeed * Time.deltaTime );
            }

            if (data.RightPressed)
            {
                transform.Rotate(0, 0, -1 * rotateSpeed * Time.deltaTime);
            }
        }

        public static void Shoot(
            Vector3 position,
            Vector3 direction,
            float shootSpeed,
            GameEntityFactory<Projectile> projectileFactory, 
            string projectileTag, string layer = "Projectile")
        {
            var projectile = projectileFactory.Spawn();
            projectile.transform.position = position;
            projectile.gameObject.layer = LayerMask.NameToLayer(layer);
            projectile.transform.rotation = Quaternion.identity;
            projectile.transform.tag = projectileTag;
            projectile.Rigid.AddForce(direction.normalized * shootSpeed,ForceMode2D.Impulse);
        }
    }
}