using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagChanger : MonoBehaviour {


    // THIS CHANGES THE TAGS OF ALL AN OBJECT'S CHILDREN TO THE TAG FIELD
    string TAG = "Rock";
    // Use this for initialization
    void OnDrawGizmosSelected()
    {
          ReTag(transform, TAG); 


    }
    void ReTag(Transform _transform, string tag)
    {
        foreach (Transform child in _transform)
        {
            child.gameObject.tag = tag;
            ReTag(child, tag);
        }

    }
}