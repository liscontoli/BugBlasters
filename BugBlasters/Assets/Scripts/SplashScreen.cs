using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BugBlasters
{
    public class SplashScreen : MonoBehaviour
    {
        // Start is called before the first frame update

        public Image LoadingBar;
        public Text LoadingTxt;
        public string NextScreen;

        void Start()
        {




            StartCoroutine(SplashScreenToMainScreen());
        }








        private IEnumerator SplashScreenToMainScreen()
        {
            int Counter = 0;
            if (CheckInternetConnection())
            {
                while (Counter <= 100)
                {
                    LoadingTxt.text = "LOADING : " + Counter + "%";
                    LoadingBar.fillAmount = LoadingBar.fillAmount + 0.01f;
                    Counter++;
                    yield return new WaitForSeconds(0.02f);
                }
                SceneManager.LoadScene(NextScreen);

            }
            else
            {
                LoadingTxt.text = "Check your Internet Connection !";
            }
        }
        private bool CheckInternetConnection()
        {
            return (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork);
        }



    }
}