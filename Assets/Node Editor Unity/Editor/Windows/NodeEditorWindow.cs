using UnityEditor;
using UnityEngine;

public class NodeEditorWindow : EditorWindow {

	public static NodeEditorWindow currentWindow;
	public NodeGraph currentGraph = null;
	public NodePropertyView propertyView;
	public NodeWorkView workView;
	public float viewPercentage = 0.75f;

	public static void InitEditorWindow () {
		currentWindow = (NodeEditorWindow) EditorWindow.GetWindow<NodeEditorWindow> ();
		currentWindow.titleContent = new GUIContent ("Node Editor");
		CreateViews ();
	}

	private void OnEnable () {}

	private void OnDestroy () {
		//Save as backup
	}

	private void OnGUI () {
		if (propertyView == null || workView == null) {
			CreateViews ();
			return;
		}
		//Get event and Process it
		Event e = Event.current;
		ProcessEvents (e);
		//UpdateView
		workView.UpdateView (e, position, new Rect (0f, 0f, viewPercentage, 1f), currentGraph);
		propertyView.UpdateView (e, new Rect (position.width, position.y, position.width, position.height), new Rect (viewPercentage, 0f, 1 - viewPercentage, 1f), currentGraph);
		Repaint ();
	}
	static void CreateViews () {
		if (currentWindow != null) {
			currentWindow.propertyView = new NodePropertyView ();
			currentWindow.workView = new NodeWorkView ();
		}
		else {
			currentWindow = (NodeEditorWindow) EditorWindow.GetWindow<NodeEditorWindow> ();
		}
	}
	public void ProcessEvents (Event e) {
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftArrow) {
			this.viewPercentage -= 0.01f;
		}
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.RightArrow) {
			this.viewPercentage += 0.01f;
		}
	}
}
