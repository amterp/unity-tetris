using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : MonoBehaviour
{
    private Dictionary<GameObject, Transform> _gameObjectsToOriginalParentTransforms;

    void Awake()
    {
        _gameObjectsToOriginalParentTransforms = new Dictionary<GameObject, Transform>();
    }

    public void Hide(GameObject uiGameObjectToHide)
    {
        if (_gameObjectsToOriginalParentTransforms.ContainsKey(uiGameObjectToHide)) return;
        _gameObjectsToOriginalParentTransforms.Add(uiGameObjectToHide, uiGameObjectToHide.transform.parent);
        uiGameObjectToHide.transform.SetParent(transform);
    }

    public void Unhide(GameObject uiGameObjectToUnhide)
    {
        if (!_gameObjectsToOriginalParentTransforms.ContainsKey(uiGameObjectToUnhide)) return;
        uiGameObjectToUnhide.transform.SetParent(_gameObjectsToOriginalParentTransforms[uiGameObjectToUnhide]);
        _gameObjectsToOriginalParentTransforms.Remove(uiGameObjectToUnhide);
    }
}
