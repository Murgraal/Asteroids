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

        public static void Shoot(Vector3 position,Vector3 direction, GameEntityFactory<Projectile> projectileFactory)
        {
            var projectile = projectileFactory.Spawn();
            projectile.transform.position = position;
            projectile.transform.rotation = Quaternion.identity;
            projectile.Rigid.AddForce(direction,ForceMode2D.Impulse);
        }
    }
}