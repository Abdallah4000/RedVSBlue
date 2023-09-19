using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class GameManeger : MonoBehaviour
{
    public static GameManeger GM { get; private set; }
    private void Awake()
    {
        if (GM != null && GM != this)
        {
            Destroy(this);
        }
        else
        {
            GM = this;
        }

    }

    [SerializeField] List<GameObject> CellsContainers = new List<GameObject>();
    [SerializeField] List<GameObject> AllFires = new List<GameObject>();
    [SerializeField] List<GameObject> AllBoxes = new List<GameObject>();


    [SerializeField] GameObject CP;
    [SerializeField] GameObject COP;
    [SerializeField] GameObject CUP;

    [SerializeField] GameObject RC;
    [SerializeField] GameObject RP;
    [SerializeField] GameObject RPO;

    [SerializeField] GameObject LC;
    [SerializeField] GameObject LP;
    [SerializeField] GameObject LPO;

    [SerializeField] Color c1;
    [SerializeField] Color c2;
    [SerializeField] Color c3;
    [SerializeField] Color c4;
    [SerializeField] Color c5;
    [SerializeField] Color c6;

    [SerializeField] GameObject BackGroundImge;



    [SerializeField] GameObject SideFire1;
    [SerializeField] GameObject SideFire2;

    [SerializeField] Sprite SideFireRed;
    [SerializeField] Sprite SideFireBlue;


    [SerializeField] GameObject UpFireRedWin;
    [SerializeField] GameObject UpFireBlueWin;
    [SerializeField] GameObject RedTurn;
    [SerializeField] GameObject BlueTurn;



    [SerializeField] List<GameObject> R1 = new List<GameObject>();
    [SerializeField] List<GameObject> R2 = new List<GameObject>();
    [SerializeField] List<GameObject> R3 = new List<GameObject>();
    [SerializeField] List<GameObject> C1 = new List<GameObject>();
    [SerializeField] List<GameObject> C2 = new List<GameObject>();
    [SerializeField] List<GameObject> C3 = new List<GameObject>();
    [SerializeField] List<GameObject> D1 = new List<GameObject>();
    [SerializeField] List<GameObject> D2 = new List<GameObject>();



    [SerializeField] Sprite RedBox;
    [SerializeField] Sprite BlueBox;
    [SerializeField] Sprite DefultBox;

    [SerializeField] List<GameObject> RHeart = new List<GameObject>();
    [SerializeField] List<GameObject> BHeart = new List<GameObject>();

    int RHstate = 2;
    int BHstate = 2;

    bool Red = false;
    bool Blue = false;





    GameObject holding;

    int RedValue = 1;
    int BlueValue = 2;


    public bool RedToPlay = true;
    public bool BlueToPlay = false;


    [SerializeField] float MoveingSpeed = .4f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetTheGame();
            MoveContainersDown();

        }
    }


    public void HoldMe(GameObject Fire)
    {

        if (Fire.gameObject.GetComponent<Fire>().CanBeHold)
        {
            holding = Fire;
        }
        else if (!Fire.gameObject.GetComponent<Fire>().CanBeHold && holding != null)
        {
            CheckThat(Fire.transform.parent.gameObject);
        }
        else
        {
            holding = null;
        }

    }
    public void CheckThat(GameObject container)
    {
        if (container.gameObject.GetComponent<ContainerCell>().embty)
        {
            if (holding != null)
            {

                if ((holding.gameObject.GetComponent<Fire>().IsRedFire) && (RedToPlay))
                {
                    MoveFromTo(container, RedValue, RedBox, false);

                    RedTurn.gameObject.SetActive(false);
                    BlueTurn.gameObject.SetActive(true);

                    RedToPlay = false;
                    BlueToPlay = true;

                }

                else if ((!holding.gameObject.GetComponent<Fire>().IsRedFire) && BlueToPlay)
                {

                    MoveFromTo(container, BlueValue, BlueBox, false);

                    RedTurn.gameObject.SetActive(true);
                    BlueTurn.gameObject.SetActive(false);

                    RedToPlay = true;
                    BlueToPlay = false;
                }

                CheckAnyWin();
            }
        }
        else if (holding != null && !container.gameObject.GetComponent<ContainerCell>().embty)
        {
            if (container.gameObject.GetComponent<ContainerCell>().Firevalue <
                holding.gameObject.GetComponent<Fire>().FirePounts)

            {
                if (holding.gameObject.GetComponent<Fire>().IsRedFire && RedToPlay)
                {

                    MoveFromTo(container, RedValue, RedBox, false);
                    RedToPlay = false;
                    BlueToPlay = true;

                }
                else if (!holding.gameObject.GetComponent<Fire>().IsRedFire && BlueToPlay)
                {

                    MoveFromTo(container, BlueValue, BlueBox, false);
                    RedToPlay = true;
                    BlueToPlay = false;
                }

                CheckAnyWin();
            }
        }
    }


    public void MoveFromTo(GameObject container, int FValue, Sprite sprite, bool Bo)
    {

        holding.transform.DOMove(container.transform.position, MoveingSpeed);

        container.gameObject.GetComponent<ContainerCell>().value = FValue;
        container.gameObject.GetComponent<ContainerCell>().chengeImge(sprite);
        container.gameObject.GetComponent<ContainerCell>().embty = Bo;
        holding.gameObject.GetComponent<Fire>().CanBeHold = Bo;

        holding.gameObject.transform.parent = container.gameObject.transform;

        container.gameObject.GetComponent<ContainerCell>().Firevalue =
            holding.gameObject.GetComponent<Fire>().FirePounts;

        holding = null;
    }

    public void CheckAnyWin()
    {
        CheckHere(R1);
        CheckHere(R2);
        CheckHere(R3);
        CheckHere(C1);
        CheckHere(C2);
        CheckHere(C3);
        CheckHere(D1);
        CheckHere(D2);

    }

    public void CheckHere(List<GameObject> x)
    {

        if ((x[0].gameObject.GetComponent<ContainerCell>().value != 0) &&
            (x[1].gameObject.GetComponent<ContainerCell>().value != 0) &&
            (x[2].gameObject.GetComponent<ContainerCell>().value != 0))
        {
            if ((x[0].gameObject.GetComponent<ContainerCell>().value == RedValue) &&
                (x[1].gameObject.GetComponent<ContainerCell>().value == RedValue) &&
                (x[2].gameObject.GetComponent<ContainerCell>().value == RedValue))
            {
                MoveContainersAway();
                LightOnTheFire(RedValue);

                Blue = true;
                HeartState(BHeart, BHstate);



                RedToPlay = false;
                BlueToPlay = false;

            }
            else if ((x[0].gameObject.GetComponent<ContainerCell>().value == BlueValue) &&
                (x[1].gameObject.GetComponent<ContainerCell>().value == BlueValue) &&
                (x[2].gameObject.GetComponent<ContainerCell>().value == BlueValue))
            {
                MoveContainersAway();
                LightOnTheFire(BlueValue);


                Red = true;
                HeartState(RHeart, RHstate);




                RedToPlay = false;
                BlueToPlay = false;

            }

        }

    }

    public void ResetTheGame()
    {
        LightOffTheFire();

        RedTurn.gameObject.SetActive(true);
        BlueTurn.gameObject.SetActive(false);

        UpFireRedWin.gameObject.SetActive(false);
        UpFireBlueWin.gameObject.SetActive(false);



        for (int i = 0; i < CellsContainers.Count; i++)
        {

            AllFires[i].gameObject.transform.parent = CellsContainers[i].gameObject.transform;
            AllFires[i].gameObject.transform.DOMove(CellsContainers[i].gameObject.transform.position, MoveingSpeed);


            AllFires[i].gameObject.GetComponent<Fire>().CanBeHold = true;
        }
        for (int i = 0; i < AllBoxes.Count; i++)
        {
            AllBoxes[i].gameObject.GetComponent<ContainerCell>().chengeImge(DefultBox);
            AllBoxes[i].gameObject.GetComponent<ContainerCell>().value = 0;
            AllBoxes[i].gameObject.GetComponent<ContainerCell>().embty = true;
            AllBoxes[i].gameObject.GetComponent<ContainerCell>().Firevalue = 0;

        }

        RedToPlay = true;
        BlueToPlay = false;

        holding = null;

        StartCoroutine(MoveAfterTime());



    }


    IEnumerator MoveAfterTime()
    {
        yield return new WaitForSeconds(.5f);
        MoveContainersBack();

    }

    public void MoveContainersBack()
    {
        RC.transform.DOMove(RPO.transform.position, .3F);
        LC.transform.DOMove(LPO.transform.position, .3F);

    }
    public void MoveContainersAway()
    {
        RC.transform.DOMove(RP.transform.position, .3F);
        LC.transform.DOMove(LP.transform.position, .3F);

    }

    public void MoveContainersUp()
    {
        CP.transform.DOMove(CUP.transform.position, .3F);
    }
    public void MoveContainersDown()
    {
        CP.transform.DOMove(COP.transform.position, .3F);
    }

    public void LightOnTheFire(int fv)
    {
        RedTurn.gameObject.SetActive(false);
        BlueTurn.gameObject.SetActive(false);


        SideFire1.gameObject.SetActive(true);
        SideFire2.gameObject.SetActive(true);

        if(fv == RedValue)
        {
            SideFire1.gameObject.GetComponent<Image>().sprite = SideFireRed;
            SideFire2.gameObject.GetComponent<Image>().sprite = SideFireRed;

            UpFireRedWin.gameObject.SetActive(true);


        }
        else if(fv == BlueValue)
        {
            SideFire1.gameObject.GetComponent<Image>().sprite = SideFireBlue;
            SideFire2.gameObject.GetComponent<Image>().sprite = SideFireBlue;

            UpFireBlueWin.gameObject.SetActive(true);


        }
    }


    public void LightOffTheFire()
    {
        SideFire1.gameObject.SetActive(false);
        SideFire2.gameObject.SetActive(false);

    }


    public void HeartState(List<GameObject> x, int value)
    {
        if (Red)
        {
            if(value >= 0 )
            {
                x[value].gameObject.SetActive(false);
                RHstate--;


            }
            if (value == 0 )
            {

                StartOver();

                Debug.Log("red win the game");
            }



            Red = false;

        }

        else if (Blue)
        {
            if (value >= 0)
            {
                x[value].gameObject.SetActive(false);
                BHstate--;


            }
            if (value == 0)
            {

                StartOver();
                Debug.Log("blue win the game");
            }


            Blue = false;
        }
    }


    public void StartOver()
    {
        RHstate = 2;
        BHstate = 2;

        for(int i = 0; i < BHeart.Count; i++)
        {
            BHeart[i].gameObject.SetActive(true);
            RHeart[i].gameObject.SetActive(true);

        }

    }


    public void PlayAgain()
    {
        MoveContainersDown();
        ResetTheGame();

    }

    public void StartTheFire()
    {
        MoveContainersDown();
        ResetTheGame();
    }

    public void SetColor( int color)
    {
        switch (color)
        {
            case 1:
                BackGroundImge.gameObject.GetComponent<Image>().color = c1;
                break;
            case 2:
                BackGroundImge.gameObject.GetComponent<Image>().color = c2;
                break;
            case 3:
                BackGroundImge.gameObject.GetComponent<Image>().color = c3;
                break;
            case 4:
                BackGroundImge.gameObject.GetComponent<Image>().color = c4;
                break;
            case 5:
                BackGroundImge.gameObject.GetComponent<Image>().color = c5;
                break;
            case 6:
                BackGroundImge.gameObject.GetComponent<Image>().color = c6;
                break;
            default:
                BackGroundImge.gameObject.GetComponent<Image>().color = c1;
                break;

        }
    }



}
