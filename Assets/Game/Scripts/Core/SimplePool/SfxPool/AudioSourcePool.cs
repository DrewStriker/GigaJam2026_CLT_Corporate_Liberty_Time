using System.Collections;
using UnityEngine;

namespace Game.Core.SimplePool.SfxPool
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourcePool : PoolObject
    {
        private AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
            source.playOnAwake = false;
        }

        public void Play(AudioClip clip, float volume = 1f)
        {
            source.clip = clip;
            source.volume = volume;
            source.Play();
            StopAllCoroutines();
            StartCoroutine(DisableWhenFinished());
        }

        private IEnumerator DisableWhenFinished()
        {
            yield return new WaitWhile(() => source.isPlaying);
            gameObject.SetActive(false);
        }
    }
}