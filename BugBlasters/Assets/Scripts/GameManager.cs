using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using Unity.VisualScripting;
namespace BugBlasters
{
    public class GameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public static GameManager GameManagerInstance;
        public GameObject MainMenu;
        public GameObject SettingsMenu;
        public GameObject ExitMenu;
        public GameObject SignUpMenu;
        public GameObject SignInMenu;

        public GameObject GamePlayMenu;
        public GameObject GameOverMenu;

        public GameObject GameLevel;


        public Text HighScore, NormalScore;


        public enum GameState { MainMenu = 0, GamePlay = 1, SignUp = 2, SignIn = 3 }
        public GameState State;


        private void Awake()
        {
            GameManagerInstance = this;
        }
        void Start()
        {

            ChangeState(GameState.MainMenu);

        }
        public void ChangeState(GameState newState)
        {
            State = newState;
            switch (newState)
            {
                case GameState.MainMenu:
                    HandleStarting();
                    break;
                case GameState.GamePlay:
                    HandleGamePlay();
                    break;
                case GameState.SignIn:
                    HandleSignIn();
                    break;
                case GameState.SignUp:
                    HandleSignUp();
                    break;
                default:
                    break;

            }
            Debug.Log($"New state: {newState}");
        }
        private void HandleStarting()
        {
            //if Player is Login
            if (!Authentication.AuthenticationInstance.signedIn)
            {
                ChangeState(GameState.SignIn);
            }
            else
            {
                HideExitMenu();
                MainMenu.SetActive(true);
                HideGamePlayMenu();
            }

        }
        private void HandleGamePlay()
        {
            HideMainMenu();
            GamePlayMenu.SetActive(true);
            GamePlayManager.GamePlayManagerInstance.SetupLevelData();

        }
        private void HandleSignUp()
        {
            Authentication.AuthenticationInstance.ShowInfoCenterData(2);
            SignUpMenu.SetActive(true);
            HideSignInMenu();
        }
        private void HandleSignIn()
        {
            Authentication.AuthenticationInstance.ShowInfoCenterData(2);
            SignInMenu.SetActive(true);
            HideSignUpMenu();

        }




        public void StartLevelGamePlay()
        {
            GameLevel.SetActive(true);
            GenerateBugs.GenerateBugsInstance.LevelStart = true;
            GenerateBugs.GenerateBugsInstance.GenerateNewBugs(GamePlayManager.GamePlayManagerInstance.LevelNumber);
            GamePlayManager.GamePlayManagerInstance.LevelInfo.SetActive(false);


        }





        public void ShowSignUpMenu()
        {
            ChangeState(GameState.SignUp);

        }
        public void ShowSignInMenu()
        {
            ChangeState(GameState.SignIn);

        }
        public void HideSignUpMenu()
        {
            SignUpMenu.SetActive(false);
        }
        public void HideSignInMenu()
        {
            SignInMenu.SetActive(false);
        }

        public void ShowMainMenu()
        {
            ChangeState(GameState.MainMenu);
        }
        public void HideMainMenu()
        {
            MainMenu.SetActive(false);
        }


        public void ShowGamePlayMenu()
        {
            GamePlayManager.GamePlayManagerInstance.RestLevelAndTargetCounter();
            ChangeState(GameState.GamePlay);
        }
        public void ShowGamePlayMenuContinuebtn()
        {
            ChangeState(GameState.GamePlay);
        }
        public void HideGamePlayMenu()
        {

            GamePlayMenu.SetActive(false);
            GameLevel.SetActive(false);
        }




        public void ShowSettingsMenu()
        {

            SettingsMenu.SetActive(true);

        }
        public void HideSettingsMenu()
        {

            SettingsMenu.SetActive(false);

        }
        public void ShowExitMenu()
        {

            Time.timeScale = 0f;
            ExitMenu.SetActive(true);

        }
        public void HideExitMenu()
        {

            Time.timeScale = 1f;
            ExitMenu.SetActive(false);
        }
        public void ShowGameOverMenu(int Score)
        {

            if (!PlayerPrefs.HasKey("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", Score);

            }
            if (PlayerPrefs.GetInt("HighScore") < Score)
            {
                PlayerPrefs.SetInt("HighScore", Score);
            }

            HighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
            NormalScore.text = Score.ToString();
            GameOverMenu.SetActive(true);
        }
        public void HideGameOverMenu()
        {
            GameOverMenu.SetActive(false);
        }

        public void ExitGame()
        {

            if (State == GameState.MainMenu)
            {
                Application.Quit();

            }
            else if (State == GameState.GamePlay)
            {
                HideGameOverMenu();
                GamePlayMenu.SetActive(false);
                GameLevel.SetActive(false);
                GamePlayManager.GamePlayManagerInstance.InitLevelGame();
                GenerateBugs.GenerateBugsInstance.LevelStart = false;
                ChangeState(GameState.MainMenu);
            }
        }



        public void RestartGame()
        {

            HideGameOverMenu();
            GamePlayManager.GamePlayManagerInstance.InitLevelGame();
            GenerateBugs.GenerateBugsInstance.LevelStart = true;
            GenerateBugs.GenerateBugsInstance.GenerateNewBugs(GamePlayManager.GamePlayManagerInstance.LevelNumber);
            GameLevel.SetActive(true);
        }
    }
}