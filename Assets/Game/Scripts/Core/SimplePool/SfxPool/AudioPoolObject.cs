using UnityEngine;

namespace Game.Core.SimplePool.SfxPool
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPoolObject : PoolObject
    {
        private float endTime;
        private bool running;
        private AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
            source.playOnAwake = false;
        }


        private void Update()
        {
            if (!running) return;

            if (AudioSettings.dspTime >= endTime)
            {
                running = false;
                gameObject.SetActive(false);
            }
        }

        public void Play(AudioClip clip, float volume = 1f, bool pitchVariation = false)
        {
            source.clip = clip;
            source.volume = volume;
            source.pitch = 1f;
            source.pitch += pitchVariation ? Random.Range(-0.1f, 0.15f) : 0f;
            source.Play();

            endTime = (float)(AudioSettings.dspTime + clip.length);
            running = true;
        }

        // private IEnumerator DisableWhenFinished()
        // {
        //     yield return new WaitWhile(() => source.isPlaying);
        //     gameObject.SetActive(false);
        // }
    }
}