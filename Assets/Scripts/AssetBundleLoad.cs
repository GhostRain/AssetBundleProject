using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoad : MonoBehaviour {

    public GameObject cubeObject;
    public Transform posTrans;
    /// <summary>
    /// 资源名
    /// </summary>
    public string assetName;
    /// <summary>
    /// ab包名
    /// </summary>
    public string assetBundleName;
    /// <summary>
    /// ab包url
    /// </summary>
    private string assetBundleUrl;
    private void Awake()
    {
        assetBundleUrl = "file://" + Application.streamingAssetsPath + "/" + assetBundleName;
        //StartCoroutine(LoadAssetBundle(assetBundleUrl, cubeObject, assetName));
        StartCoroutine(LoadPerfabAssetBundle(assetBundleUrl, assetName, posTrans));
    }

    /// <summary>
    /// 加载单张纹理
    /// </summary>
    /// <param name="url"></param>
    /// <param name="gameObject"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    IEnumerator LoadAssetBundle(string url,GameObject gameObject,string assetName = null)
    {
        if(string.IsNullOrEmpty(url))
        {
            Debug.Log("Error URL:" + url);
        }
        using (UnityWebRequest request = UnityWebRequest.GetAssetBundle(url))
        {
            yield return request.SendWebRequest();
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            if(bundle != null)
            {
                if(string.IsNullOrEmpty(assetName))
                {
                    gameObject.GetComponent<Renderer>().material.mainTexture = (Texture)bundle.mainAsset;
                }
                else
                {
                    gameObject.GetComponent<Renderer>().material.mainTexture = (Texture)bundle.LoadAsset(assetName);
                }
            }
            else
            {
                Debug.Log("load assetbundle Error:" + request.error);
            }
        }
    }

    /// <summary>
    /// 加载perfab
    /// </summary>
    /// <param name="url"></param>
    /// <param name="assetBundle"></param>
    /// <param name="posTransform"></param>
    /// <returns></returns>
    IEnumerator LoadPerfabAssetBundle(string url,string assetBundle,Transform posTransform)
    {
        if (string.IsNullOrEmpty(url))
        {
            Debug.Log("Error URL:" + url);
        }
        using (UnityWebRequest request = UnityWebRequest.GetAssetBundle(url))
        {
            yield return request.SendWebRequest();
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            if(bundle != null)
            {
                if (string.IsNullOrEmpty(assetName))
                {
                    if (posTransform == null)
                    {
                        Instantiate(bundle.mainAsset);
                    }
                    else
                    {
                        GameObject gameObject = (GameObject)Instantiate(bundle.mainAsset);
                        gameObject.transform.position = posTransform.position;
                    }
                }
                else
                {
                    if(posTransform == null)
                    {
                        Instantiate(bundle.LoadAsset(assetName));
                    }
                    else
                    {
                        GameObject gameObject = (GameObject)Instantiate(bundle.LoadAsset(assetName));
                        gameObject.transform.position = posTransform.position;
                    }
                }
            }
            else
            {
                Debug.Log("load assetbundle Error:" + request.error);
            }
        }
    }
}
