using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WWWAPI : MonoBehaviour
{
    private static bool isDownloaded;
    #region Singlton
    private static WWWAPI _api;
    public static WWWAPI API
    {
        get
        {
            return _api;
        }
    }

    private void Awake()
    {
        if (_api == null)
        {
            _api = this;
            isDownloaded = false;
            DontDestroyOnLoad(API.gameObject);
        }
    }
    #endregion

    #region WWWClassWithJason
    /* define a delegate named callback*/
    public IEnumerator CreateJsonObject (System.Action<ModelClass> callBack1, int id, ModelClass model/*, AssetBundle assetBundle, string assetName, GameObject asset*/)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + "NDMwMTJmMjItMzRjNi00ZjVkLTg5YWYtNTU3N2M5YTdlZTdi");

        using (WWW www = new WWW("http://portalapi.aladwaa.com/api/ARModels/getARModelById?id="+id, null,headers))
        {
            yield return www;
            if (www == null)
            {
                callBack1(null);
            }
            else
            {
                //convert www.text string to JSON Object
                JSONObject JSONObj = new JSONObject(www.text);

                /*
                 * ha5od el json object da w a search gwah 3la field esmo data
                 * w data da hal2e el value bt3to mn no3 object
                 * w ha5od el object da w a7welo l string
                 */
                string json = JSONObj.GetField("Data").ToString();

                //then, cast string to ModelClass object
                model = JsonUtility.FromJson<ModelClass>(json);

                callBack1(model);
            }
        }
    }

    void AcessData(JSONObject JSONObj)
    {
        switch (JSONObj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < JSONObj.list.Count; i++)
                {
                    string key = (string)JSONObj.keys[i];
                    JSONObject j = (JSONObject)JSONObj.list[i];
                    Debug.Log(key);
                    AcessData(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                foreach (JSONObject j in JSONObj.list)
                {
                    AcessData(j);
                }
                break;
            case JSONObject.Type.STRING:
                Debug.Log(JSONObj.str);
                break;
            case JSONObject.Type.NUMBER:
                Debug.Log(JSONObj.n);
                break;
            case JSONObject.Type.BOOL:
                Debug.Log(JSONObj.b);
                break;
            case JSONObject.Type.NULL:
                Debug.Log("NULL");
                break;
        }
    }
    #endregion

    #region WWWClassWithAssetBundle
    public  IEnumerator DownloadAssetBundle(System.Action <AssetBundle> callBack2, string bundleURL,AssetBundle assetBundle/*, string assetName, GameObject asset, bool isRemote*/)
    {
        if (!isDownloaded)
        {
            using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL))
            {
                UnityWebRequestAsyncOperation operation = www.SendWebRequest();

                while (!operation.isDone)
                {
                    yield return null;
                }

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    callBack2(null);
                }

                else
                {
                        assetBundle = DownloadHandlerAssetBundle.GetContent(www);
                        isDownloaded = true;
                }
            }
        }
        callBack2(assetBundle);
    }

    public GameObject InstantiateObjectFromBundle(AssetBundle assetBundle, string assetName)
    {
          return Instantiate((GameObject)assetBundle.LoadAsset(assetName));
    }

    #endregion

    #region testing
    // private const string url = "https://drive.google.com/open?id=1T3KFjRcHC7eZnSMhkExOSwHEeOaZRyym";

    //private void Start()
    //{
    //    WWW request = new WWW(url);

    //    StartCoroutine(OnResponse(request));

    //}
    //public IEnumerator OnResponse(WWW req)
    //{
    //    yield return req;
    //    Renderer renderer = GetComponent<Renderer>();
    //    renderer.material.mainTexture = req.texture;
    //}

    //public string url;

    //IEnumerator Start()
    //{
    //    var uwr = UnityWebRequestAssetBundle.GetAssetBundle(url);
    //    yield return uwr.SendWebRequest();

    //    Debug.Log("Done");
    //    // Get the designated main asset and instantiate it.
    //    Instantiate(DownloadHandlerAssetBundle.GetContent(uwr).mainAsset as GameObject);
    //}

    //public string uri;
    //private void Start()
    //{
    //    StartCoroutine(Help(uri, "Eman"));
    //}
    //IEnumerator Help(/*Action<GameObject> modelFound,*/ string url, string modelName)
    //{


    //    WWW bundleRequest = new WWW(uri);
    //    while (!bundleRequest.isDone)
    //    {
    //        Debug.Log("downloading....");
    //        //onDLProgress?.Invoke(modelName, bundleRequest.progress);
    //        yield return null;
    //    }

    //    AssetBundle bundle = null;
    //    if (bundleRequest.bytesDownloaded > 0)
    //    {
    //        AssetBundleCreateRequest myRequest = AssetBundle.LoadFromMemoryAsync(bundleRequest.bytes);
    //        while (!myRequest.isDone)
    //        {
    //            Debug.Log("loading....");
    //            yield return null;
    //        }
    //        if (myRequest.assetBundle != null)
    //        {
    //            bundle = myRequest.assetBundle;
    //            GameObject model = null;
    //            if (bundle != null)
    //            {
    //                AssetBundleRequest newRequest = bundle.LoadAssetAsync<GameObject>(modelName);
    //                while (!newRequest.isDone)
    //                {
    //                    Debug.Log("loading ASSET....");
    //                    yield return null;
    //                }
    //                model = (GameObject)newRequest.asset;
    //               // modelFound(model);

    //                bundle.Unload(false);
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError("COULDN'T DOWNLOAD ASSET BUNDLE FROM URL: " + uri);
    //           // modelFound(null);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("COULDN'T DOWNLOAD ASSET BUNDLE FROM URL: " + uri);
    //        //modelFound(null);
    //    }

    //}
    #endregion

}