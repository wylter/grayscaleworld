/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor {

    [CustomEditor(typeof(PlatformTile))]
    [CanEditMultipleObjects]
    internal class PlatformTileEditor : Editor {

        //Editor to choose the destination layer
        public override void OnInspectorGUI() {

            ((PlatformTile)target).destinationLayer = (destinationOption)EditorGUILayout.EnumPopup("Destination Tilemap:", ((PlatformTile)target).destinationLayer);
            EditorGUILayout.Space();

            base.OnInspectorGUI();

            EditorUtility.SetDirty(target);
            SceneView.RepaintAll();
        }

    }

}
*/  