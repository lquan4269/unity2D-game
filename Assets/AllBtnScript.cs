using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllBtnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject OverPanel;
    public GameObject PlayPanel;
    public GameObject playbtn;
    void Start()
    {      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOverPanel()
    {
        OverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowPlayPanel()
    {
        PlayPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReplayBtn()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }
    public void Playbtn() 
    {
        playbtn = GameObject.Find("Playbtn");
        playbtn.SetActive(false);
    }
}
