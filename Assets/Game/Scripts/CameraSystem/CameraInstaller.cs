using Zenject;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.CameraSystem
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineBasicMultiChannelPerlin cinemachineNoise;

        public override void InstallBindings()
        {
            Container.Bind<ICameraShakeService>()
                .To<CameraShakeService>()
                .AsSingle()
                .WithArguments(cinemachineNoise);
        }
    }
}