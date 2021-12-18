using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_GameManager : MonoBehaviour
{
    // singleton
    static DT_GameManager _singleton = null;
    static public DT_GameManager Instance
    {
        get
        {
            if (_singleton == null)
            {
                GameObject go = new GameObject();
                _singleton = go.AddComponent<DT_GameManager>();
                DontDestroyOnLoad(go);
            }
            return _singleton;
        }
    }


    private void OnDestroy()
    {
        if (_singleton == this)
            _singleton = null;
    }

    public DT_DropSeat DropSeat
    { set; private get; }


    // game functions
    public void DropReady()
    {

    }

    public void DropGo()
    {

    }

    public void DropBrake()
    {

    }
}
