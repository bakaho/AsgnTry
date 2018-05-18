using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicComponent : MonoBehaviour {

	[System.Serializable]
	public class Line {
		public List<ElectronicComponent> components = new List<ElectronicComponent>();
		public bool isLoop, hasBubble;
		public Line() {
		}
		public Line(Line copyFrom) {
			this.components = new List<ElectronicComponent>(copyFrom.components);
			this.isLoop = copyFrom.isLoop;
			this.hasBubble = copyFrom.hasBubble;
		}
	}

	public static int lastSceneUpdateAt;

	public bool isOn;
	public List<ElectronicComponent> connects = new List<ElectronicComponent>();
	public Material onSkin, offSkin;
	public bool touchCharacter;
	
	private Renderer rend;
	private int lastUpdate;

	void Awake() {
		this.rend = this.GetComponent<Renderer>();
	}
	void Update () {
		//if is turned on, change skin
		this.rend.material = this.isOn ? this.onSkin : this.offSkin;
		if (this.touchCharacter && lastSceneUpdateAt != lastUpdate) {
			lastUpdate = lastSceneUpdateAt;
			this.UpdateLines();
		}
	}
    void OnTriggerEnter(Collider other) {
		var e = other.GetComponent<ElectronicComponent>();
		if (e) {
			this.connects.Add(e);
		}
		if (other.GetComponent<characterX>()) {
			this.touchCharacter = true;
		}
		lastSceneUpdateAt = Time.frameCount;
    }
    void OnTriggerExit(Collider other) {
		var e = other.GetComponent<ElectronicComponent>();
		if (e && this.connects.Contains(e)) {
			this.connects.Remove(e);
		}
		if (other.GetComponent<characterX>()) {
			this.touchCharacter = false;
			this.TurnOffAll();
		}
		lastSceneUpdateAt = Time.frameCount;
    }

	public void TurnOffAll() {
		foreach (var e in FindObjectsOfType<ElectronicComponent>()) {
			e.isOn = false;
		}
	}
	public List<Line> lines;
	public void UpdateLines() {
		var lines = this.GetLines();
		bool hasLoop = lines.Find(l => l.isLoop) != null;
		bool hasShort = lines.Find(l => l.isLoop && !l.hasBubble) != null;
		bool win = lines.Find(l => l.isLoop && l.hasBubble) != null;
		if (hasLoop) {
			if (hasShort) {
				characterX.instance.UpdateState(false);
			}
			else if (win) {
				characterX.instance.UpdateState(true);
			}
		}
		TurnOffAll();
		foreach (var line in lines) {
			if (line.isLoop) {
				foreach (var c in line.components) {
					c.isOn = true;
				}
			}
		}
		this.lines = lines;
	}
	public List<Line> GetLines() {
		List<Line> lines = new List<Line>();
		foreach (var c in this.connects) {
			var line = new Line();
			line.components = new List<ElectronicComponent>() {this};
			c.FillLine(lines, line);
		}
		return lines;
	}
	public void FillLine(List<Line> lines, Line baseLine) {
		baseLine.components.Add(this);
		if (this is currentMiddle) {
			baseLine.hasBubble = true;
		}
		
		int backCount = 0;
		foreach (var next in this.connects) {
			var newLine = new Line(baseLine);
			if (next == newLine.components[newLine.components.Count - 2]) {
				backCount++;
				continue;
			}
			if (newLine.components.Contains(next)) {
				if (next == newLine.components[0]) {
					newLine.isLoop = true;
				}
				lines.Add(newLine);
				continue;
			}
			next.FillLine(lines, newLine);
		}
		if (backCount == this.connects.Count) {
			lines.Add(baseLine);
		}
	}
}
