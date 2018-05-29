using System;
using ExtraUniRx;
using UniRx;

namespace UnityModule
{
    public interface IStopWatch
    {
        /// <summary>
        /// Start timer.
        /// </summary>
        void Start();

        /// <summary>
        /// Stop timer.
        /// </summary>
        void Stop();

        /// <summary>
        /// Resume timer.
        /// </summary>
        void Resume();

        /// <summary>
        /// Pause timer.
        /// </summary>
        void Pause();

        /// <summary>
        /// StopWatch is playing or not.
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Current time of stopwatch
        /// </summary>
        float Time { get; }

        /// <summary>
        /// Get current time sence it starts. 
        /// </summary>
        /// <returns></returns>
        UniRx.IObservable<float> TimeAsObservable { get; }

        /// <summary>
        /// Observable stream of IsPlaying flag.
        /// </summary>
        UniRx.IObservable<bool> IsPlayingAsObservable { get; }
    }

    public class StopWatch : IStopWatch
    {
        public UniRx.IObservable<float> TimeAsObservable => this.timeProperty;

        public float Time => this.timeProperty.Value;

        public bool IsPlaying => this.IsPlayingProperty.Value;

        public UniRx.IObservable<bool> IsPlayingAsObservable => this.IsPlayingProperty;

        private SubjectProperty<bool> IsPlayingProperty = new SubjectProperty<bool>();

        private SubjectProperty<float> timeProperty { get; } = new SubjectProperty<float>();

        private UniRx.IObservable<float> oscillatorObservable { get; set; }

        private IDisposable subscription { get; set; }

        /// <summary>
        /// Create StopWatch by default frame update oscillator
        /// </summary>
        public StopWatch() : this(Observable.EveryUpdate().Select(_ => UnityEngine.Time.deltaTime))
        {
        }

        /// <summary>
        /// StopWatch for counting game time.
        /// </summary>
        /// <param name="oscillatorObservable">oscillator to watch time. it should output the difference of time interval.</param>
        public StopWatch(UniRx.IObservable<float> oscillatorObservable)
        {
            this.oscillatorObservable = oscillatorObservable;
        }

        public void Start()
        {
            if (this.IsPlaying)
            {
                this.Stop();
            }

            this.IsPlayingProperty.Value = true;
            this.subscription = this.oscillatorObservable
                .StartWith(0)
                .Where(_ => this.IsPlaying)
                .Scan((sum, it) => sum + it)
                .Subscribe(this.timeProperty);
        }

        public void Stop()
        {
            this.IsPlayingProperty.Value = false;
            this.subscription?.Dispose();
        }

        public void Resume()
        {
            this.IsPlayingProperty.Value = true;
        }

        public void Pause()
        {
            this.IsPlayingProperty.Value = false;
        }
    }
}