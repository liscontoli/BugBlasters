using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using Unity.VisualScripting;
using System;

namespace BugBlasters
{
    public class GamePlayManager : MonoBehaviour
    {
        public static GamePlayManager GamePlayManagerInstance;
        public Text KillCounter;
        public int ValCounter = 0;
        public Image DeviceHealthFill;
        public float HealthFillAmount = 1f;


        public Text LevT, LevM, LevNote;
        public GameObject LevelInfo;

        public List<int> TargetKill = new List<int>() { 0, 15, 20, 25, 30, 35};

        public List<GameObject> Devices = new List<GameObject>();

        public int LevelNumber;

        public GameObject ContinueBtn;

        void Start()
        {
            GamePlayManagerInstance = this;
           TargetKill = new List<int>() { 0, 15, 20, 25, 30, 40};

            InitLevelGame();
        }
        public void InitLevelGame()
        {
            GetLevelAndTargetCounter();
            ValCounter = 0;
            HealthFillAmount = 1f;

            KillCounter.text = ValCounter.ToString() + "/" + TargetKill[LevelNumber];
            DeviceHealthFill.fillAmount = HealthFillAmount;
        }
        public void UpdateKillCounter(int val)
        {
            ValCounter += val;
            KillCounter.text = ValCounter.ToString() + "/"+ TargetKill[LevelNumber];
            if (ValCounter == TargetKill[LevelNumber])
            {
                GenerateBugs.GenerateBugsInstance.LevelStart = false;
                SetLevelAndTargetCounter(Math.Min(5, LevelNumber + 1));
                SetupLevelData();
                InitLevelGame();

            }
        }
        public void UpdateDeviceHealthFill(float BugsDamage)
        {
            HealthFillAmount -= BugsDamage;
            DeviceHealthFill.fillAmount = HealthFillAmount;
            if (HealthFillAmount <= 0f)
            {
                GenerateBugs.GenerateBugsInstance.LevelStart = false;
                GameManager.GameManagerInstance.ShowGameOverMenu(ValCounter);
                AudioManager.AudioManagerInstance.PlayGameOverMusic();
            }
        }

        public void GetLevelAndTargetCounter()
        {

            if (!PlayerPrefs.HasKey("LevelNumber"))
            {
                LevelNumber = 1;
                PlayerPrefs.SetInt("LevelNumber", 1);
                PlayerPrefs.Save();
            }
            else
            {
                LevelNumber = PlayerPrefs.GetInt("LevelNumber");
                SetContinueBtn();
            }
        }

        public void RestLevelAndTargetCounter()
        {
            LevelNumber = 1;
            PlayerPrefs.SetInt("LevelNumber", 1);
            PlayerPrefs.Save();
            SetContinueBtn();
        }

        public void SetLevelAndTargetCounter(int lev)
        {
            LevelNumber = lev;
            PlayerPrefs.SetInt("LevelNumber", lev);
            PlayerPrefs.Save();
            SetContinueBtn();

        }
        public void SetContinueBtn()
        {
            if (LevelNumber >= 2)
            {
                ContinueBtn.GetComponent<Button>().interactable = true;
            }
            else
            {
                ContinueBtn.GetComponent<Button>().interactable = false;
            }
        }


        public void SetupLevelData()
        {
            int index = LevelNumber;
            ShowActiveDevice(index);
            if (index == 1)
            {
                LevT.text = "	Level " + index + ": Save Mobile Phone";
                LevM.text = "	Level mission: Kill " + TargetKill[index] + " ladybugs";
                LevNote.text = "		1  TAP to kill black ladybugs";
            }
            else if (index == 2)
            {
                LevT.text = "	Level " + index + ": Save Tablet";
                LevM.text = "	Level mission: Kill " + TargetKill[index] + " ladybugs";
                LevNote.text = "		1  TAP to kill black ladybugs";
            }
            else if (index == 3)
            {
                LevT.text = "	Level " + index + ": Save Laptop";
                LevM.text = "	Level mission: Kill " + TargetKill[index] + " ladybugs";
                LevNote.text = "		1  TAP to kill black ladybugs \n		2  TAP to kill green ladybugs";
            }
            else if (index == 4)
            {
                LevT.text = "	Level " + index + ": Save Desktop";
                LevM.text = "	Level mission: Kill " + TargetKill[index] + " ladybugs";
                LevNote.text = "		1  TAP to kill black ladybugs \n		2  TAP to kill green ladybugs";
            }
            else if (index == 5)
            {
                LevT.text = "	Level " + index + ": Save Super Computer";
                LevM.text = "	Level mission: Kill " + TargetKill[index] + " ladybugs";
                LevNote.text = "		1  TAP to kill black ladybugs \n		2  TAP to kill green ladybugs";
            }
            LevelInfo.SetActive(true);
            AudioManager.AudioManagerInstance.PlayLevelUpMusic();

        }
        public void ShowActiveDevice(int index)
        {
            for (int i = 0; i < Devices.Count; i++)
            {
                if (i == index - 1)
                {
                    Devices[i].SetActive(true);
                }
                else
                {
                    Devices[i].SetActive(false);
                }
            }
        }

    }
}