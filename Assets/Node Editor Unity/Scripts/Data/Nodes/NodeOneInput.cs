using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
[Serializable]
public class NodeOneInput : NodeBase {

  public float nodeValue;
  public NodeInput input;
  public NodeOutput output;
  public NodeOneInput () {
    input = new NodeInput ();
    output = new NodeOutput ();
  }
  public override void InitNode () {
    base.InitNode ();
    type = NodeType.ONEINPUTONLY;
    nodeRect = new Rect (10f, 10f, 150f, 65f);
  }
  public override void UpdateNode (Event e, Rect _viewRect) {
    base.UpdateNode (e, _viewRect);
  }

  public override void UpdateNodeGUI (Event e, Rect _viewRect, GUISkin _viewSkin) {
    base.UpdateNodeGUI (e, _viewRect, _viewSkin);
    if (GUI.Button (new Rect ((nodeRect.x + nodeRect.width), (nodeRect.y + ((nodeRect.height * 0.5f) - 12f)), 24f, 24f), "", _viewSkin.GetStyle ("NodeOutput"))) {
      if (parentGraph != null) {
        parentGraph.wantsConnection = true;
        parentGraph.connectionNode = this;
      }
    }
  }
  public override void DrawNodeProperties () {
    base.DrawNodeProperties ();
    nodeValue = EditorGUILayout.FloatField ("Float Value", nodeValue);
  }
}
