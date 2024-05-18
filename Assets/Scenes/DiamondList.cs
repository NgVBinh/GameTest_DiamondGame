using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondList : MonoBehaviour
{
    [SerializeField] List<Diamond> startDiamonds = new List<Diamond>();
    [SerializeField] GameObject diamondPrefab;

    private int amountDiamond;
    void Start()
    {
        GrenateDiamonds();
    }

    private void GrenateDiamonds()
    {
        amountDiamond = startDiamonds.Count * 2;

        foreach (Diamond diamon in startDiamonds)
        {
            GameObject newDiamond = Instantiate(diamondPrefab, transform);
            newDiamond.GetComponent<DimomdDrag>().SetDiamondInfor(diamon);
        }

        foreach (Diamond diamon in startDiamonds)
        {
            GameObject newDiamond = Instantiate(diamondPrefab, transform);
            newDiamond.GetComponent<DimomdDrag>().SetDiamondInfor(diamon);
        }
    }

    public void DecreaseDiamon()
    {
        amountDiamond--;
        Debug.Log(amountDiamond);
        if(amountDiamond <= 0)
        {
            GrenateDiamonds();
        }
    }

}
