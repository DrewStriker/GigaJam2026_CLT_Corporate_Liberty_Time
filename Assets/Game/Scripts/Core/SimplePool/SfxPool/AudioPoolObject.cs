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

        public void Play(AudioClip clip, float volume = 1f)
        {
            source.clip = clip;
            source.volume = volume;
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