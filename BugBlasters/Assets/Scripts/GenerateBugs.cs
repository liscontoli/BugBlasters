using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


// CONTROL BUG POSITION, SPAWN RATE, AND BUG SPEED
namespace BugBlasters
{
    public class GenerateBugs : MonoBehaviour
    {
        public static GenerateBugs GenerateBugsInstance;
        List<float> Angle = new List<float>() { 0f, 90f, 180f, 270f, 45f, 135f, 225f, 315f };
        public List<Transform> StartPos1;
        public List<Transform> StartPos2;
        public List<Transform> StartPos3;
        public List<Transform> StartPos4;
        public List<Transform> StartPos5;
        public GameObject LadyBugBlackPref;
        public GameObject LadyBugGreenPref;
        public GameObject LadyBugsContainer;
        public bool LevelStart = false;
        void Awake()
        {
            GenerateBugsInstance = this;
        }
        void Start()
        {
        }

        void Update()
        {

        }

        async void FixedUpdate()
        {

        }

        // Controlling bugs in each level

        public async void GenerateNewBugs(int LevIndex)
        {
            int index = 0;
            int LadyBugBlackCount = 0;
            int LadyBugGreenCount = 0;
            if (LevIndex == 1)
            {
                while (LevelStart)
                {


                    await Awaitable.WaitForSecondsAsync(3f);
                    int r = Random.Range(0, Angle.Count);
                    this.transform.localRotation = Quaternion.Euler(0, 0, Angle[r]);
                    GameObject Ladybug = Instantiate(LadyBugBlackPref, transform.position, this.transform.localRotation, this.transform);
                    Ladybug.GetComponent<BugMovement>().AttackPower = 1f / 5f;
                    Ladybug.GetComponent<BugMovement>().LadyBugHealth = 1;
                    Ladybug.transform.localPosition = new Vector3(0f, -7f, 0f);

                    Ladybug.transform.SetParent(LadyBugsContainer.transform);




                    Ladybug.SetActive(true);
                    index++;
                }
                DeleteAllBugs();
            }
            else if (LevIndex == 2)
            {
                while (LevelStart)
                {
                    await Awaitable.WaitForSecondsAsync(Mathf.Max(0.7f, 3.2f - (index * 0.05f))); //0.4f
                    int r = Random.Range(0, StartPos2.Count);
                    GameObject Ladybug = Instantiate(LadyBugBlackPref, StartPos2[r].position, StartPos2[r].localRotation, LadyBugsContainer.transform);
                    Ladybug.GetComponent<BugMovement>().AttackPower = 1f / 10f;
                    Ladybug.GetComponent<BugMovement>().LadyBugHealth = 1;
                    Ladybug.GetComponent<BugMovement>().smoothTime = Mathf.Max(0.35f, 2.2f - (index * 0.05f)); //0.5f
                    Ladybug.name = "smoothTime:" + index.ToString() + "  -   " + (3f - (index * 0.06f)); //0.6f
                    Ladybug.SetActive(true);
                    index++;
                }
                DeleteAllBugs();
            } 
            else if (LevIndex == 3) // LEVEL 3 PERFECT SPAWN RATE AND BUG SPEED
            {
                while (LevelStart)
                {
                    await Awaitable.WaitForSecondsAsync(Mathf.Max(0.8f, 3.0f - (index * 0.06f))); //0.4
                    int r = Random.Range(0, StartPos3.Count);
                    int r1 = Random.Range(0, 2);
                    GameObject Ladybug;
                    if (r1 == 1)
                    {
                        Ladybug = Instantiate(LadyBugGreenPref, StartPos3[r].position, StartPos3[r].localRotation, LadyBugsContainer.transform);
                        Ladybug.GetComponent<BugMovement>().AttackPower = 1f / 5f;
                        Ladybug.GetComponent<BugMovement>().LadyBugHealth = 2;
                    }
                    else
                    {
                        Ladybug = Instantiate(LadyBugBlackPref, StartPos3[r].position, StartPos3[r].localRotation, LadyBugsContainer.transform);
                        Ladybug.GetComponent<BugMovement>().AttackPower = 1f / 10f;
                        Ladybug.GetComponent<BugMovement>().LadyBugHealth = 1;
                    }
                    Ladybug.GetComponent<BugMovement>().smoothTime = Mathf.Max(0.5f, 1.9f - (index * 0.03f)); //0.5f
                    Ladybug.name = "smoothTime:" + index.ToString() + "  -   " + (3f - (index * 0.03f)); //0.6f
                    Ladybug.SetActive(true);
                    index++;
                }
                DeleteAllBugs();
            }
            else if (LevIndex == 4) // LEVEL 4 PERFECT SPAWN RATE AND BUG SPEED
            {
                while (LevelStart)
                {
                    await Awaitable.WaitForSecondsAsync(Mathf.Max(1.0f, 3.0f - (index * 0.045f))); //0.4f
                    int r = Random.Range(0, StartPos4.Count);
                    int r1 = Random.Range(0, 2);
                    GameObject Ladybug;
                    if (r1 == 1)
                    {
                        Ladybug = Instantiate(LadyBugGreenPref, StartPos4[r].position, StartPos4[r].localRotation, LadyBugsContainer.transform);
                        Ladybug.GetComponent<BugMovement>().AttackPower = 1f / 5f;
                        Ladybug.GetComponent<BugMovement>().LadyBugHealth = 2;
                    }
                    else
                    {
                        Ladybug = Instantiate(LadyBugBlackPref, StartPos4[r].position, StartPos4[r].localRotation, LadyBugsContainer.transform);
                        Ladybug.GetComponent<BugMovement>().AttackPower = 1f / 10f;
                        Ladybug.GetComponent<BugMovement>().LadyBugHealth = 1;
                    }
                    Ladybug.GetComponent<BugMovement>().smoothTime = Mathf.Max(0.45f, 1.9f - (index * 0.04f)); //0.5f
                    Ladybug.name = "smoothTime:" + index.ToString() + "  -   " + (3f - (index * 0.06f)); // 0.6f
                    Ladybug.SetActive(true);
                    index++;
                }
                DeleteAllBugs();
            }
            else if (LevIndex == 5) // LEVEL 5 PERFECT SPAWN RATE AND BUG SPEED
            {
                while (LevelStart)
                {
                    await Awaitable.WaitForSecondsAsync(Mathf.Max(1.0f, 3.0f - (index * 0.04f))); // 0.4f
                    int r = Random.Range(0, StartPos5.Count);
                    int r1 = Random.Range(0, 2);
                    GameObject Ladybug;
                    if (r1 == 1)
                    {
                        Ladybug = Instantiate(LadyBugGreenPref, StartPos5[r].position, StartPos5[r].localRotation, LadyBugsContainer.transform);
                        Ladybug.GetComponent<BugMovement>().AttackPower = 1f / 5f;
                        Ladybug.GetComponent<BugMovement>().LadyBugHealth = 2;
                    }
                    else
                    {
                        Ladybug = Instantiate(LadyBugBlackPref, StartPos5[r].position, StartPos5[r].localRotation, LadyBugsContainer.transform);
                        Ladybug.GetComponent<BugMovement>().AttackPower = 1f / 10f;
                        Ladybug.GetComponent<BugMovement>().LadyBugHealth = 1;
                    }
                    Ladybug.GetComponent<BugMovement>().smoothTime = Mathf.Max(0.5f, 2.0f - (index * 0.035f)); //0.5f
                    Ladybug.name = "smoothTime:" + index.ToString() + "  -   " + (2.0f - (index * 0.035f)); //0.6f
                    Ladybug.SetActive(true);
                    index++;
                }
                DeleteAllBugs();
            }
        }
        // Destroy bugs as they leave stage
        public void DeleteAllBugs()
        {
            LadyBugsContainer.gameObject.SetActive(false);
            int nbChildren = LadyBugsContainer.transform.childCount;
            for (int i = nbChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(LadyBugsContainer.transform.GetChild(i).gameObject);
            }
            LadyBugsContainer.gameObject.SetActive(true);
        }

        public List<int> BugsType(int CountBugsBlack, int CountBugsGreen)
        {
            int allbugs = CountBugsBlack + CountBugsGreen;
            List<int> buglist = new List<int>(allbugs);
            for (int i = 0; i < allbugs; i++)
            {
                buglist.Add(0);
                int r = Random.Range(0, 2);
                if (r == 1 && CountBugsGreen > 0)
                {
                    CountBugsGreen--;
                    buglist[i] = 1;
                }
            }
            return buglist;
        }
    }







}

/*

        public async void GenerateNewBugs()
    {
        int index = 0;
        while (LevelStart && index < 30)
        {
            await Awaitable.WaitForSecondsAsync(2f);
            //int r = Random.Range(0, Angle.Count);
            int r = Random.Range(0, StartPos.Count);
            //     transform.localRotation = Quaternion.Euler(0, 0, Angle[Index[index]]);
            index++;
            GameObject Ladybug = Instantiate(LadyBugBlackPref,  transform.position, StartPos[r].localRotation, this.transform);
            //Ladybug.GetComponent<BugMovement>().LadyBugHealth = 1;
            //Ladybug.transform.localPosition = new Vector3(0f, -7f, 0f);
           // Ladybug.transform.SetParent(LadyBugsContainer.transform);
            //Ladybug.SetActive(true);
        //}
        //DeleteAllBugs();
 //   }*/