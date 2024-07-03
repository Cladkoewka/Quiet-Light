using UnityEngine;

namespace _Project.CodeBase.Services.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;
        [SerializeField] private AudioSource _windSound;
        [SerializeField] private AudioSource _axeSound;
        [SerializeField] private AudioSource _stepSound;
        [SerializeField] private AudioSource _heartbeat;
        [SerializeField] private AudioSource _change;
        [SerializeField] private AudioSource _cutTree;

        public void SetMusic(bool isPlaying)
        {
            if(isPlaying)
                _music.Play();
            else
                _music.Pause();
        }

        public void SetWindSound(bool isPlaying)
        {
            if(isPlaying)
                _windSound.Play();
            else
                _windSound.Pause();
        }

        public void SetHearbeatSound(bool isPlaying)
        {
            if(isPlaying)
                _heartbeat.Play();
            else
                _heartbeat.Stop();
        }

        public void SetFootstepSound(bool isPlaying)
        {
            if(isPlaying)
                _stepSound.Play();
            else
                _stepSound.Stop();
        }

        public void SetAxeSound(bool isPlaying)
        {
            if(isPlaying)
                _axeSound.Play();
            else
                _axeSound.Stop();
        }
        public void PlayChangeSound() => _change.Play();
        public void PlayCutTreeSound() => _cutTree.Play();
    }
}