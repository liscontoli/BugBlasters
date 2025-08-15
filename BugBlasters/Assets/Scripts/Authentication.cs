using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*using Firebase;
using Firebase.Auth;
using Firebase.Extensions;*/
using Unity.Services.Core;
using Unity.Services.Authentication;

using System.Threading.Tasks;
using Unity.VisualScripting;

namespace BugBlasters
{
    public class Authentication : MonoBehaviour
    {
        public static Authentication AuthenticationInstance;
        public InputField SignUpEmail, SignUpPass;
        public InputField SignInEmail, SignInPass;
        public Text InfoSignUpEmail, InfoSignUpPass;
        public Text InfoSignInEmail, InfoSignInPass;
        public GameObject InfoCenterSignUp, InfoCenterSignIn;
        public bool signedIn;
        public int code = -1;
        async void Awake()
        {
            AuthenticationInstance = this;
            await UnityServices.InitializeAsync();
            SetupEvents();
            InitPlayerID();

        }

        void Start()
        {
            Debug.Log(" NNNNN" + AuthenticationService.Instance.IsSignedIn);
            Debug.Log(" NNNNN1" + AuthenticationService.Instance.IsExpired);
            if (GetPlayerID() == "NULL")
            {
                signedIn = false;
            }
            else
            {
                signedIn = true;
            }
        }
        public void CheckInputField(InputField InputFieldType)
        {
            if (InputFieldType.contentType == InputField.ContentType.EmailAddress)
            {
                if (GameManager.GameManagerInstance.State == GameManager.GameState.SignUp)
                {
                    if (!(SignUpEmail.text.Contains('@')))
                    {
                        InfoSignUpEmail.text = "";
                    }
                    else
                    {
                        InfoSignUpEmail.text = "";
                    }
                }
                else if (GameManager.GameManagerInstance.State == GameManager.GameState.SignIn)
                {
                    if (!(SignInEmail.text.Contains('@')))
                    {
                        InfoSignInEmail.text = "";
                    }
                    else
                    {
                        InfoSignInEmail.text = "";
                    }
                }

            }
            else if (InputFieldType.contentType == InputField.ContentType.Password)
            {
                if (GameManager.GameManagerInstance.State == GameManager.GameState.SignUp)
                {
                    if (!(SignUpPass.text.Length >= 8))
                    {
                        InfoSignUpPass.text = "Password length more than 8 char A-Z a-z !@! 0-9";
                    }
                    else
                    {
                        InfoSignUpPass.text = "";
                    }
                }
                else if (GameManager.GameManagerInstance.State == GameManager.GameState.SignIn)
                {

                    if (!(SignInPass.text.Length >= 8))
                    {
                        InfoSignInPass.text = "Password length more than 8 char A-Z a-z !@! 0-9";
                    }
                    else
                    {
                        InfoSignInPass.text = "";
                    }
                }
            }
        }
        public void SetFormToEmpty()
        {

            SignUpEmail.text = "";
            SignUpPass.text = "";
            SignInEmail.text = "";
            SignInPass.text = "";
            InfoSignUpEmail.text = "";
            InfoSignUpPass.text = "";
            InfoSignInEmail.text = "";
            InfoSignInPass.text = "";
        }
        public async void ShowInfoCenterData(int InfoCode)
        {
            if (GameManager.GameManagerInstance.State == GameManager.GameState.SignUp)
            {
                if (InfoCode == 0)
                {
                    InfoCenterSignUp.transform.GetChild(0).gameObject.SetActive(true);
                    InfoCenterSignUp.transform.GetChild(1).gameObject.SetActive(false);
                }
                else if (InfoCode == 1)
                {
                    InfoCenterSignUp.transform.GetChild(0).gameObject.SetActive(false);
                    InfoCenterSignUp.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
            else if (GameManager.GameManagerInstance.State == GameManager.GameState.SignIn)
            {
                if (InfoCode == 0)
                {
                    InfoCenterSignIn.transform.GetChild(0).gameObject.SetActive(true);
                    InfoCenterSignIn.transform.GetChild(1).gameObject.SetActive(false);
                }
                else if (InfoCode == 1)
                {
                    InfoCenterSignIn.transform.GetChild(0).gameObject.SetActive(false);
                    InfoCenterSignIn.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
            if (InfoCode == 2)
            {
                InfoCenterSignUp.transform.GetChild(0).gameObject.SetActive(false);
                InfoCenterSignUp.transform.GetChild(1).gameObject.SetActive(false);
                InfoCenterSignIn.transform.GetChild(0).gameObject.SetActive(false);
                InfoCenterSignIn.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        public void SignOut()
        {
            code = 0;
            AuthenticationService.Instance.SignOut(true);
            Debug.Log("Player signed out.");
            SetPlayerID("NULL");
            signedIn = false;
            GameManager.GameManagerInstance.HideMainMenu();
            GameManager.GameManagerInstance.HideSettingsMenu();
            GameManager.GameManagerInstance.ShowSignInMenu();
            SetFormToEmpty();
        }
        public async void Sign_UP_button_press()
        {
            string email = SignUpEmail.text;
            string password = SignUpPass.text;
            await SignUpWithUsernamePasswordAsync(email, password);
        }
        public async void Sign_IN_button_press()
        {
            string email = SignInEmail.text;
            string password = SignInPass.text;
            await SignInWithUsernamePasswordAsync(email, password);
        }




        async Task SignUpWithUsernamePasswordAsync(string username, string password)
        {
            try
            {
                await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);


                Debug.Log("SignUp successful.");
                ShowInfoCenterData(1);
            }
            catch (AuthenticationException ex)
            {

                Debug.Log("AuthenticationException." + ex.Message);
                ShowInfoCenterData(0);

            }
            catch (RequestFailedException ex)
            {

                Debug.Log("RequestFailedException." + ex.Message);
                ShowInfoCenterData(0);

            }
        }


        async Task SignInWithUsernamePasswordAsync(string username, string password)
        {
            try
            {
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
                Debug.Log("SignIn successful.");
                ShowInfoCenterData(1);
            }
            catch (AuthenticationException ex)
            {


                Debug.Log("AuthenticationException." + ex.Message);
                ShowInfoCenterData(0);
            }
            catch (RequestFailedException ex)
            {

                Debug.Log("RequestFailedException." + ex.Message);
                ShowInfoCenterData(0);
            }
        }



        void SetupEvents()
        {
            AuthenticationService.Instance.SignedIn += () =>
            {
                SetPlayerID(AuthenticationService.Instance.PlayerId);
                ShowInfoCenterData(1);
                signedIn = true;
                GameManager.GameManagerInstance.HideSignUpMenu();
                GameManager.GameManagerInstance.HideSignInMenu();
                GameManager.GameManagerInstance.ShowMainMenu();
                SetFormToEmpty();

            };

            AuthenticationService.Instance.SignInFailed += (err) =>
            {
                //Debug.LogError(err);
            };

            AuthenticationService.Instance.SignedOut += () =>
            {


            };

            AuthenticationService.Instance.Expired += () =>
              {
                  Debug.Log("Player session could not be refreshed and expired.");
              };
        }
        public void InitPlayerID()
        {
            if (!PlayerPrefs.HasKey("PlayerID"))
            {
                PlayerPrefs.SetString("PlayerID", "NULL");
                PlayerPrefs.Save();
            }
        }
        public void SetPlayerID(string id)
        {
            PlayerPrefs.SetString("PlayerID", id);
            PlayerPrefs.Save();
        }
        public string GetPlayerID()
        {
            return PlayerPrefs.GetString("PlayerID");
        }
    }
}
