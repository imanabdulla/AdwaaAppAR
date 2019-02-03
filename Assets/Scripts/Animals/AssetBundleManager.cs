using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleManager : MonoBehaviour
{
    private static  AssetBundleManager _aBM;

    public static AssetBundleManager ABM
    {
        get
        {
            return _aBM;
        }
    }


    private void Awake()
    {
        if (_aBM == null)
        {
            _aBM = this;
            DontDestroyOnLoad(ABM.gameObject);
        }
    }
}
