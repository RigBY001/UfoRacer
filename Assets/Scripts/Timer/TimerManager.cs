using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour {
	public static TimerManager Singleton {get; private set;}
	List<Timer> timers = new List<Timer>();
	Stack<int> free = new Stack<int>();

	void Awake() {
		if (Singleton && Singleton != this) {
			Destroy(this);
		} else {
			Singleton = this;
		}
	}

	void Update() {
		if (timers.Count == 0 || timers.Count == free.Count) return;

		float delta = Time.deltaTime;
		int len = timers.Count;

		for (int i = 0; i < len; i++) {
			Timer timer = timers[i];
			if (timer == null) continue;

			timer.currentTime += delta;
			if (timer.currentTime < timer.length) continue;

			timer.timeOut.Invoke();

			if (timer.Loop) {
				timer.currentTime -= timer.length;
				continue;
			} 

			timer.Stop();
		}
	}

	public int Add(Timer timer) {
		if (free.Count != 0) {
			int idx = free.Pop();
			timers[idx] = timer;
			return idx;
		}

		timers.Add(timer);
		return timers.Count - 1;
	}

	public void Remove(int idx) {
		timers[idx] = null;
		free.Push(idx);
	}

	public void Clear() {
		timers = new List<Timer>();
		free = new Stack<int>();
	}
}