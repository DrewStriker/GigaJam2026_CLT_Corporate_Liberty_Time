using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Core
{
    public class SelfDisable : MonoBehaviour
    {
        [SerializeField] private float duration = 1f;
        private  async void OnEnable()
        {
            await  UniTask.Delay((int)(duration * 1000));
            gameObject.SetActive(false);
            
        }
    }
}