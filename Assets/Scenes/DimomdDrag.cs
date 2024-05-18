using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DimomdDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Diamond diamondInfor;

    private Vector3 startPosition;
    private Transform parentToReturnTo = null;
    private Transform gridParent;

    private GridManager gridManager;

    public Vector3 jewelPosInGrid { get; private set; }

    public bool addedToGrid = false;

    private void OnValidate()
    {
        if (diamondInfor != null)
        {
            GetComponent<Image>().sprite = diamondInfor.sprite;
        }
    }
    private void Start()
    {
        gridManager = GetComponentInParent<GridManager>();
        gridParent = gridManager.gridTransform;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        parentToReturnTo = this.transform.parent;
        //this.transform.SetParent(gridParent);

        if (jewelPosInGrid != null)
        {
            gridManager.RemoveDiamondToGrid(jewelPosInGrid);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CheckGrid(eventData, FindClosestCell()))
        {

            gridManager.AddDiamondToGrid(this.gameObject, jewelPosInGrid);
        }


    }
    private bool CheckGrid(PointerEventData eventData, Transform _closestCell)
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject != this.gameObject)
            {
                if (result.gameObject.transform == _closestCell && _closestCell.childCount == 0)
                {
                    transform.SetParent(result.gameObject.transform);
                    transform.position = result.gameObject.transform.position;

                    if (!addedToGrid)
                    {
                        gridManager.diamondList.DecreaseDiamon();
                        addedToGrid = true;
                    }
                    return true;
                }
            }
        }

        transform.SetParent(parentToReturnTo);
        transform.position = startPosition;
        return false;
    }
    private Transform FindClosestCell()
    {
        Transform closestCell = null;
        float closestDistance = Mathf.Infinity;

        for (int row = 0; row < gridParent.childCount; row++)
        {
            for (int coloum = 0; coloum < gridParent.GetChild(row).childCount; coloum++)
            {
                float distance = Vector3.Distance(transform.position, gridParent.GetChild(row).GetChild(coloum).position);
                if (distance < closestDistance)
                {
                    closestCell = gridParent.GetChild(row).GetChild(coloum);
                    closestDistance = distance;
                    jewelPosInGrid = new Vector2(row, coloum);
                }
            }
        }

        return closestCell;
    }

    public RectTransform GetRectTransform()
    {
        return this.GetComponent<RectTransform>();
    }

    public void SetDiamondInfor(Diamond _newDiamondInfor)
    {
        this.diamondInfor = _newDiamondInfor;
        GetComponent<Image>().sprite = diamondInfor.sprite;
    }
}
