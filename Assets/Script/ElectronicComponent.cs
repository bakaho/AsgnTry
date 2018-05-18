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
			this.components = copyFrom.components;
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
	public void UpdateLines() {
		TurnOffAll();
		var lines = this.GetLines();
		bool hasLoop = lines.Find(l => l.isLoop) != null;
		bool hasShort = lines.Find(l => l.isLoop && !l.hasBubble) != null;
		bool win = lines.Find(l => l.isLoop && l.hasBubble) != null;
		if (hasLoop) {
			this.TurnOn();
			if (hasShort) {
				characterX.instance.UpdateState(false);
			}
			else if (win) {
				characterX.instance.UpdateState(true);
			}
		}
	}
	public List<Line> GetLines() {
		List<Line> lines = new List<Line>();
		foreach (var c in this.connects) {
			var line = new Line();
			line.components = new List<ElectronicComponent>() {this};
			lines.Add(line);
			c.FillLine(lines, line);
		}
		return lines;
	}
	public void FillLine(List<Line> lines, Line baseLine) {
		baseLine.components.Add(this);
		if (this is currentMiddle) {
			baseLine.hasBubble = true;
		}
		
		bool isFirst = true;
		for (int i = 0; i < this.connects.Count; i++) {
			var c = this.connects[i];
			if (baseLine.components.Contains(c)) {
				if (c == baseLine.components[0] && c != baseLine.components[baseLine.components.IndexOf(this) - 1]) {
					baseLine.isLoop = true;
				}
				continue;
			}
			var line = baseLine;
			if (!isFirst) {
				line = new Line(baseLine);
				lines.Add(line);
			}
			isFirst = false;
			c.FillLine(lines, line);
		}
	}
	public void TurnOn() {
		if (this.isOn) {
			return;
		}
		this.isOn = true;
		foreach (var c in this.connects) {
			c.TurnOn();
		}
	}
}
