using Data.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace IOC.Installers
{
    public class AsteroidInstaller : MonoInstaller<AsteroidInstaller>
    {
        [SerializeField] private AsteroidSettings _asteroidSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(_asteroidSettings);
        }
    }
}
