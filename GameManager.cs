using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    SpriteRenderer temp_SpriteRendererOne;
    SpriteRenderer temp_SpriteRendererTwo;
    public GameObject bar;
    private int barAmount = 100;
    private GameObject temp;
    GameObject[] barArray = new GameObject[100];
    List<int> barValues = new List<int>();
    float selectionWaitTime = 0.090f;
    float mergeWaitTime = 0.09f;
    float bubbleWaitTime = 0.09f;
    float barScaling = 11f;
    bool running = false;
    float speedModifier = 1f;



    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < barAmount; i++)
        {
            barValues.Add(i + 1);
        }

        Shuffle(barValues);
        Shuffle(barValues);
        Shuffle(barValues);

        generateBars();
        //RunSelectionSort();
        //StartCoroutine(SelectionSort(barArray));
        //StartCoroutine(BubbleSort(barArray));
        //StartCoroutine(InsertionSort(barArray));


    }

    // Update is called once per frame
    void Update()
    {

    }


    void generateBars()
    {
        for (int i = 0; i < barAmount; i++)
        {
            GameObject temp = Instantiate(bar, new Vector3(i, i, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.075f, barValues[i] / barScaling, 1);
            //float tempfloat = (float)i;
            temp.transform.SetPositionAndRotation(new Vector3(((float)i + 1f) / 10f, temp.transform.localScale.y / 2), Quaternion.identity);


            barArray.SetValue(temp, i);
        }
    }

    public void RunSelectionSort()
    {
        if (running == false)
        {
            running = true;
            StartCoroutine(SelectionSort(barArray));
        }

        if (running == true)
        {
            Debug.Log("Sort is running!");
        }
    }

    public void RunBubbleSort()
    {
        if (running == false)
        {
            running = true;
            StartCoroutine(BubbleSort(barArray));
        }

        if (running == true)
        {
            Debug.Log("Sort is running!");
        }


    }

    public void RunInsertionSort()
    {
        if (running == false)
        {
            running = true;
            StartCoroutine(InsertionSort(barArray));
        }

        if (running == true)
        {
            Debug.Log("Sort is running!");
        }


    }


    /*
     * resets the hight of each bar and corrects each y position based on new scale
     */
    public void resetData()
    {
        if (running == false)
        {
            UpdateVisualSpeed(1f);
            running = true;
            Shuffle(barValues);

            for (int i = 0; i < barArray.Length; i++)
            {
                barArray[i].transform.localScale = new Vector3(0.075f, barValues[i] / barScaling, 1);
                barArray[i].transform.SetPositionAndRotation(new Vector3(((float)i + 1f) / 10f, barArray[i].transform.localScale.y / 2), Quaternion.identity);
            }
            running = false;
        }
        if (running == true)
        {
            Debug.Log("Sort is running!");
        }
        
    }

    IEnumerator SelectionSort(GameObject[] arr)
    {
        int arrLength = arr.Length;

        for (int i = 0; i < arrLength - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < arrLength; j++)
            {
                if (arr[j].transform.localScale.y < arr[minIndex].transform.localScale.y)
                {
                    minIndex = j;
                }
            }

            temp_SpriteRendererOne = arr[i].GetComponent<SpriteRenderer>();
            temp_SpriteRendererTwo = arr[minIndex].GetComponent<SpriteRenderer>();

            temp_SpriteRendererTwo.color = Color.red;
            temp_SpriteRendererOne.color = Color.green;


            yield return new WaitForSeconds(selectionWaitTime / speedModifier);

            GameObject temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;

            //MoveBars(arr[i],i);
            //MoveBars(arr[minIndex],minIndex);




            arr[i].transform.position = new Vector3(((float)i + 1) / 10f, arr[i].transform.position.y, 0);
            //yield return new WaitForSeconds(0.25f);
            arr[minIndex].transform.position = new Vector3(((float)minIndex + 1) / 10f, arr[minIndex].transform.position.y, 0);
            yield return new WaitForSeconds(selectionWaitTime / speedModifier);
            temp_SpriteRendererOne.color = Color.white;
            temp_SpriteRendererTwo.color = Color.white;
        }
        running = false;
    }

    IEnumerator BubbleSort(GameObject[] arr)
    {

        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j].transform.localScale.y > arr[j + 1].transform.localScale.y)
                {
                    GameObject temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;

                    temp_SpriteRendererOne = arr[j].GetComponent<SpriteRenderer>();
                    temp_SpriteRendererTwo = arr[j + 1].GetComponent<SpriteRenderer>();

                    temp_SpriteRendererTwo.color = Color.red;
                    temp_SpriteRendererOne.color = Color.green;
                    yield return new WaitForSeconds(bubbleWaitTime / speedModifier);

                    arr[j].transform.position = new Vector3(((float)j + 1) / 10f, arr[j].transform.position.y, 0);
                    //yield return new WaitForSeconds(0.25f);
                    arr[j + 1].transform.position = new Vector3(((float)(j + 1) + 1) / 10f, arr[j + 1].transform.position.y, 0);

                    yield return new WaitForSeconds(bubbleWaitTime / speedModifier);
                    temp_SpriteRendererOne.color = Color.white;
                    temp_SpriteRendererTwo.color = Color.white;
                }

            }

        }


        //yield return new WaitForSeconds(0.25f);
        running = false;
    }


    IEnumerator InsertionSort(GameObject[] arr)
    {


        for (int i = 1; i < arr.Length; i++)
        {
            GameObject key = arr[i];
            int prev = i - 1;

            //hide the key
            //key.SetActive(false);

            //get sprite renderers
            temp_SpriteRendererOne = key.GetComponent<SpriteRenderer>();
            //temp_SpriteRendererTwo = arr[prev].GetComponent<SpriteRenderer>();

            //change colors
            //temp_SpriteRendererTwo.color = Color.red;
            temp_SpriteRendererOne.color = Color.green;

            //wait to continue
            //yield return new WaitForSeconds(mergeWaitTime);

            //Debug.Log("i = " + i + " prev: " + prev);

            //moving all prev over til the key is in front
            while (prev >= 0 && key.transform.localScale.y < arr[prev].transform.localScale.y)
            {

                arr[1 + prev] = arr[prev];
                //arr[1 + prev].transform.position = new Vector3(((float)System.Array.IndexOf(arr, arr[1 + prev]) + 1) / 10f, arr[1 + prev].transform.position.y, 0);
                arr[1 + prev].transform.position = new Vector3(((float)1 + prev + 1) / 10f, arr[1 + prev].transform.position.y, 0);

                prev = prev - 1;
                key.transform.position = new Vector3(((float)1 + prev + 1) / 10f, key.transform.position.y, 0);
                yield return new WaitForSeconds(mergeWaitTime / speedModifier);

            }


            //update key to open position
            arr[1 + prev] = key;
            arr[1 + prev].transform.position = new Vector3(((float)System.Array.IndexOf(arr, arr[1 + prev]) + 1) / 10f, arr[1 + prev].transform.position.y, 0);

            //unhide key
            //arr[1 + prev].SetActive(true);


            //temp_SpriteRendererTwo.color = Color.white;
            temp_SpriteRendererOne.color = Color.white;
            yield return new WaitForSeconds(mergeWaitTime / speedModifier);
        }
        running = false;

    }

    void Shuffle(List<int> bv)
    {
        for (int i = 0; i < bv.Count; i++)
        {

            int randNum1 = Random.Range(0, bv.Count);
            int randNum2 = Random.Range(0, bv.Count);

            if (randNum1 != randNum2)
            {
                int temp1 = bv[randNum1];
                int temp2 = bv[randNum2];

                bv[randNum1] = temp2;
                bv[randNum2] = temp1;
            }
            else
            {
                randNum1 = Random.Range(0, bv.Count);
                if (randNum1 != randNum2)
                {
                    int temp1 = bv[randNum1];
                    int temp2 = bv[randNum2];

                    bv[randNum1] = temp2;
                    bv[randNum2] = temp1;
                }
            }

        }
    }

    public void NormalSpeed()
    {
        speedModifier = 1f;
    }

    public void TwoXSpeed()
    {
        speedModifier = 2f;
    }

    public void ThreeXSpeed()
    {
        speedModifier = 3f;
    }

    public void FourXSpeed()
    {
        speedModifier = 4f;
    }

    public void UpdateVisualSpeed(float vs)
    {
        speedModifier = 1f * vs;
    }
}
