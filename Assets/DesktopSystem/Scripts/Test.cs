using UnityEngine;

public class Test : MonoBehaviour  {
    
    
    
    void Start() {
        WebViewObject webViewObject = GetComponent<WebViewObject>();
        webViewObject.Init();
        webViewObject.LoadURL("http://www.google.com");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
