using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

[Serializable]
public class NodeBase : ScriptableObject {
  public Vector2 WIDTHandHEIGHT;
  private bool changeOccured = false;
  public string nodeName;
  public Rect nodeRect;
  public bool isSelected = false;
  public NodeGraph parentGraph;
  protected GUISkin nodeSkin;
  protected NodeType type;

  public virtual void InitNode () {}
  public virtual void UpdateNode (Event e, Rect _viewRect) {
    ProcessEvents (e, _viewRect);
    if (changeOccured) {
      this.nodeRect.width = WIDTHandHEIGHT.x;
      this.nodeRect.height = WIDTHandHEIGHT.y;
      changeOccured = false;
    }
  }
  public virtual void DrawNodeProperties () {
    Vector2 beforeChange = this.WIDTHandHEIGHT;
    WIDTHandHEIGHT.x = EditorGUILayout.FloatField ("Largura do Retângulo", WIDTHandHEIGHT.x);
    WIDTHandHEIGHT.y = EditorGUILayout.FloatField ("Altura do Retângulo", WIDTHandHEIGHT.y);
    if (beforeChange.x != WIDTHandHEIGHT.x || beforeChange.x != WIDTHandHEIGHT.x) {
      changeOccured = true;
    }
  }
  public virtual void UpdateNodeGUI (Event e, Rect _viewRect, GUISkin _viewSkin) {
    ProcessEvents (e, _viewRect);
    if (!isSelected)
      GUI.Box (nodeRect, nodeName, _viewSkin.GetStyle ("NodeDefault"));
    else
      GUI.Box (nodeRect, nodeName, _viewSkin.GetStyle ("NodeSelected"));
    EditorUtility.SetDirty (this);
  }
  void ProcessEvents (Event _e, Rect _viewRect) {
    if (isSelected) {
      if (_viewRect.Contains (_e.mousePosition)) {
        if (_e.type == EventType.MouseDrag) {
          nodeRect.x += _e.delta.x;
          nodeRect.y += _e.delta.y;
        }
      }
    }
  }
}
