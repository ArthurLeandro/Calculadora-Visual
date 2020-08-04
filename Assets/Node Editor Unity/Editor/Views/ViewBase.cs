using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class ViewBase {
  public string viewTitle;
  public Rect viewRect;
  protected NodeGraph currentGraph;
  protected GUISkin viewSkin;

  public ViewBase (string _title) {
    this.viewTitle = _title;
    GetEditorSKin ();
  }

  public virtual void UpdateView (Event e, Rect _editorRect, Rect _percentageRect, NodeGraph _graph) {
    if (this.viewSkin == null) {
      GetEditorSKin ();
      return;
    }
    this.currentGraph = _graph;
    if (_graph != null) {
      viewTitle = currentGraph.graphName;
    }
    else {
      viewTitle = "No graph Loaded";
    }
    this.viewRect = new Rect (_editorRect.x * _percentageRect.x, _editorRect.y * _percentageRect.y, _editorRect.width * _percentageRect.width, _editorRect.height * _percentageRect.height);
    ProcessEvents (e);
  }
  public virtual void ProcessEvents (Event e) {}
  protected void GetEditorSKin () {
    this.viewSkin = (GUISkin) Resources.Load ("GUISkins/EditorSkins/NodeEditorSkin");
  }
}
