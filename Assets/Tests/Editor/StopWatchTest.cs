using ExtraUniRx;
using NUnit.Framework;
using UniRx;

namespace UnityModule {

    public class StopWatchTest {

        [Test]
        public void StartStopPauseResumeTest() {
            var oscillator = new Subject<float>();
            var stopWatch = new StopWatch(oscillator);
            var observer = new TestObserver<float>();

            stopWatch.TimeAsObservable.Subscribe(observer);
            Assert.IsFalse(stopWatch.IsPlaying);
            Assert.AreEqual(0, observer.OnNextCount);
            
            stopWatch.Start();
            Assert.IsTrue(stopWatch.IsPlaying);
            Assert.AreEqual(1, observer.OnNextCount);
            Assert.AreEqual(0, observer.OnNextValues[0]);
            
            oscillator.OnNext(1f);
            Assert.IsTrue(stopWatch.IsPlaying);
            Assert.AreEqual(2, observer.OnNextCount);
            Assert.AreEqual(1f, observer.OnNextValues[1]);
            
            oscillator.OnNext(1f);
            Assert.IsTrue(stopWatch.IsPlaying);
            Assert.AreEqual(3, observer.OnNextCount);
            Assert.AreEqual(2f, observer.OnNextValues[2]);
            
            stopWatch.Stop();
            Assert.IsFalse(stopWatch.IsPlaying);
            Assert.AreEqual(3, observer.OnNextCount);
            
            stopWatch.Start();
            Assert.IsTrue(stopWatch.IsPlaying);
            Assert.AreEqual(4, observer.OnNextCount);
            Assert.AreEqual(0f, observer.OnNextValues[3]);
            
            stopWatch.Pause();
            Assert.IsFalse(stopWatch.IsPlaying);
            Assert.AreEqual(4, observer.OnNextCount);
            
            oscillator.OnNext(1f);
            oscillator.OnNext(1f);
            Assert.IsFalse(stopWatch.IsPlaying);
            Assert.AreEqual(4, observer.OnNextCount);
            
            stopWatch.Resume();
            Assert.IsTrue(stopWatch.IsPlaying);
            Assert.AreEqual(4, observer.OnNextCount);
            
            oscillator.OnNext(1f);
            Assert.IsTrue(stopWatch.IsPlaying);
            Assert.AreEqual(5, observer.OnNextCount);
            Assert.AreEqual(1f, observer.OnNextValues[4]);
        }

    }

}