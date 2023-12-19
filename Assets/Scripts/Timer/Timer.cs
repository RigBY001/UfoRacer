using UnityEngine.Events;

public class Timer {
	public float length;
	public float currentTime = 0;
	public UnityEvent timeOut = new UnityEvent();
	public bool Loop {get; set;}

	bool paused = false;
	bool running = false;
	int managerIndex;

	public Timer(float l) {
		length = l;
	}

	public void Start() {
		currentTime = 0;
		paused = false;
		if (!running) {
			AddToManager();
			running = true;
		}
	}

	public void Stop() {
		currentTime = 0;
		paused = false;
		if (running) {
			RemoveFromManager();
			running = false;
		}
	}

	public void Pause() {
		if (running) {
			RemoveFromManager();
			paused = true;
			running = false;
		}
	}

	public void Unpause(){
		if (paused) {
			AddToManager();
			paused = false;
			running = true;
		}
	}

	public bool IsRunning() {
		return running;
	}

	public float Progress() {
		return currentTime / length;
	}

	void AddToManager() {
		managerIndex = TimerManager.Singleton.Add(this);
	}

	void RemoveFromManager() {
		TimerManager.Singleton.Remove(managerIndex);
	}
}