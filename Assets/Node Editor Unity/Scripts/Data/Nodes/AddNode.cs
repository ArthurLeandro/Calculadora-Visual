using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
[Serializable]
public class AddNode : NodeBase {

  public float nodeSum;
  public NodeOutput output;
  public NodeInput inputA;
  public NodeInput inputB;
  public AddNode () {
    this.output = new NodeOutput ();
    this.inputA = new NodeInput ();
    this.inputB = new NodeInput ();
  }
  public override void InitNode () {
    base.InitNode ();
    type = NodeType.ADD;
    nodeRect = new Rect (10f, 10f, 200f, 65f);
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
    if (GUI.Button (new Rect (nodeRect.x - 24f, (nodeRect.y + (nodeRect.height * 0.33f)) - 14f, 24f, 24f), "", _viewSkin.GetStyle ("NodeInput"))) {
      if (parentGraph != null) {
        inputA.inputNode = parentGraph.connectionNode;
        inputA.isOccupied = inputA.inputNode != null ? true : false;
        parentGraph.wantsConnection = false;
        parentGraph.connectionNode = null;
      }
    }
    if (GUI.Button (new Rect (nodeRect.x - 24f, (nodeRect.y + (nodeRect.height * 0.33f) * 2f) - 8f, 24f, 24f), "", _viewSkin.GetStyle ("NodeInput"))) {
      if (parentGraph != null) {
        inputB.inputNode = parentGraph.connectionNode;
        inputB.isOccupied = inputA.inputNode != null ? true : false;
        parentGraph.wantsConnection = false;
        parentGraph.connectionNode = null;
      }
    }
    if (inputA.isOccupied && inputB.isOccupied) {
      FloatNode nodea = (FloatNode) inputA.inputNode;
      FloatNode nodeb = (FloatNode) inputB.inputNode;
      if (nodea != null && nodeb != null) {
        nodeSum = nodea.nodeValue + nodeb.nodeValue;
      }
      else {
        nodeSum = 0.0f;
      }
    }
    DrawInputLines ();
  }
  public override void DrawNodeProperties () {
    base.DrawNodeProperties ();
    EditorGUILayout.FloatField ("Sum: ", nodeSum);
  }

  void DrawInputLines () {
    if (inputA.isOccupied && inputA.inputNode != null) {
      DrawLine (inputA, 1f);
    }
    else {
      inputA.isOccupied = false;
    }
    if (inputB.isOccupied && inputB.inputNode != null) {
      DrawLine (inputB, 2f);
    }
    else {
      inputB.isOccupied = false;
    }
  }
  void DrawLine (NodeInput curInput, float inputID) {
    Handles.BeginGUI ();
    Handles.color = Color.cyan;
    Handles.DrawLine (
      new Vector3 (curInput.inputNode.nodeRect.x + curInput.inputNode.nodeRect.width + 24f,
        curInput.inputNode.nodeRect.y + (curInput.inputNode.nodeRect.height * 0.5f), 0f),
      new Vector3 (nodeRect.x - 24f, (nodeRect.y + (nodeRect.height * 0.33f) * inputID), 0f)
    );
    Handles.EndGUI ();
  }

}
