using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.Cinemachine;

namespace Game.CameraSystem
{
    public interface ICameraShakeService
    {
        UniTask Shake(float amplitude, float frequency, float duration, CancellationToken ct = default);
    }

    public class CameraShakeService : ICameraShakeService
    {
        private CinemachineBasicMultiChannelPerlin noise;

        public CameraShakeService(CinemachineBasicMultiChannelPerlin noise)
        {
            this.noise = noise;
            Reset();
        }

        public async UniTask Shake(float amplitude = 3, float frequency = 10, float duration = 0.2f,
            CancellationToken ct = default)
        {
            noise.AmplitudeGain = amplitude;
            noise.FrequencyGain = frequency;

            await UniTask.Delay((int)(1000 * duration), cancellationToken: ct);
            Reset();
        }

        private void Reset()
        {
            noise.AmplitudeGain = 0f;
            noise.FrequencyGain = 0f;
        }
    }
}