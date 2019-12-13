using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class scrollViewScript : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject scrollItemPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        generateItem("salut");
        generateItem("homère");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateItem(string text)
    {
        GameObject scrollItemObj = Instantiate(scrollItemPrefab);
        Button button = scrollItemObj.GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
        scrollItemObj.transform.SetParent(scrollView.content.transform, false);

        //scrollItemObj.transform.Find("Text").gameObject.GetComponent<Text>().text = text;
        
    }

    void LoadScene()
    {
        SceneManager.LoadScene("KitchenScene", LoadSceneMode.Single);
    }
}
