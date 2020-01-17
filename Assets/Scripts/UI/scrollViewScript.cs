using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class scrollViewScript : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject scrollItemPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        StringReader sr = new StringReader(Resources.Load<TextAsset>("Recipe/RecipeBook").text);
        string line = "";

        while((line = sr.ReadLine()) != null)
        {
            Debug.Log(line);
            generateItem(line);
        }
    }

    void generateItem(string text)
    {
        GameObject scrollItemObj = Instantiate(scrollItemPrefab);
        scrollItemObj.transform.SetParent(scrollView.content.transform, false); 
        scrollItemObj.GetComponentInChildren<Text>().text = text;      
    }
}
