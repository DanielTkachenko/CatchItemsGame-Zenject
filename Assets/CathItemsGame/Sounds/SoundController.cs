namespace CatchItemsGame
{
    public class SoundController
    {
        private SoundView.Pool _soundPool;
        private SoundConfig _soundConfig;

        public SoundController(
            SoundView.Pool soundPool,
            SoundConfig soundConfig)
        {
            _soundPool = soundPool;
            _soundConfig = soundConfig;
        }
        
        public void Play(SoundName soundName)
        {
            SwitchOff();
            var model = _soundConfig.Get(soundName);
            var sound = _soundPool.Spawn(model);
            sound.AudioSource.Play();
        }

        public void SwitchOff()
        {
            _soundPool.DisableCompletedSounds();
        }

        public void Stop()
        {
            _soundPool.MuteSound();
        }
    }
}