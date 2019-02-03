using System.Collections;
using UnityEngine;
using Vuforia;

//parent class
public class TrackableEventHandler : WWWAPI, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    protected TrackableBehaviour mTrackableBehaviour;
    protected ModelClass m_model;
    protected static AssetBundle m_assetBundle;
    protected GameObject m_asset;
    [SerializeField]
    protected string m_assetName;
    #endregion // PROTECTED_MEMBER_VARIABLES
    #region PRIVATE_MEMBER_VARIABLES
   // private WWWAPI www;
    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS
    protected virtual void  Start()
    {
        //www = GameObject.FindWithTag("WWWAPI").GetComponent<WWWAPI>();
        mTrackableBehaviour = this.GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }
    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
    public void OnTrackableStateChanged( TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;
        
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.Trackable + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }
    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS
    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;
        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;
        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
        /*
         * start WWW Class Requests (URL of model + Assetbundle of model)
         */
        StartCoroutine("ShowARContent");
    }

    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;

        ////Destroy 3DModel
        this.GetComponent<AudioSource>().Stop();
        Destroy(m_asset);
        //DestroyImmediate(m_asset, false);
    }
    #endregion // PROTECTED_METHODS
    private void ShowARContent()
    {
        StartCoroutine(WWWAPI.API.CreateJsonObject(
            callBack1 => {
                if (callBack1 != null)
                {
                    print("Json Object is Created");
                    m_model = callBack1;

                    StartCoroutine(WWWAPI.API.DownloadAssetBundle(
                        callBack2 =>
                        {
                            if (callBack2 != null)
                            {
                                print("AssetBundle is downloaded");
                                m_assetBundle = callBack2;
                                m_asset = WWWAPI.API.InstantiateObjectFromBundle(m_assetBundle, m_assetName);
                                m_asset.transform.position = mTrackableBehaviour.transform.GetChild(0).position;
                                m_asset.transform.rotation = mTrackableBehaviour.transform.GetChild(0).rotation;
                                m_asset.transform.localScale = mTrackableBehaviour.transform.GetChild(0).localScale;
                                m_asset.transform.SetParent(this.transform);
                                this.GetComponent<AudioSource>().Play();
                            }
                        }
                        , m_model.Url, m_assetBundle));

                }
            }
            , mTrackableBehaviour.Trackable.ID, m_model));


    }
}
