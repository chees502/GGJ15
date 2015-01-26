using UnityEngine;
using UnityEditor;
using System.Collections;

public class AddMeshColliderToEverything : Editor {
    [MenuItem("Assets/AddAllTheColliders")]
    static void DoShit() {
        MeshFilter[] meshes = GameObject.FindObjectsOfType<MeshFilter>();
        foreach (MeshFilter mf in meshes) {
            if (mf.gameObject.GetComponent<Collider>() == null) {
                if(mf.gameObject.layer != LayerMask.NameToLayer("Water")) {
                    mf.gameObject.AddComponent<BoxCollider>();
                } else {
                    mf.gameObject.AddComponent<MeshCollider>();
                }
            }

            if (mf.gameObject.GetComponent<Rigidbody>() == null &&
                mf.gameObject.layer != LayerMask.NameToLayer("Water")) {
                    mf.gameObject.AddComponent<Rigidbody>();
            }
        }
    }
}
