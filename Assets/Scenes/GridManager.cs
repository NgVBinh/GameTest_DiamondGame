using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid")]
    public GameObject rowPrefab;
    public GameObject cellPrefab;
    public int rows = 6;
    public int columns = 6;
    public Transform gridTransform;

    [Header("Coin")]
    public GameObject coinPrefabs;
    private Vector3 coinSpawnPos;

    private GameObject[,] cellsGrid;

    public DiamondList diamondList;

    void Start()
    {
        CreateGrid(rows, columns);
        cellsGrid = new GameObject[rows, columns];
        if (diamondList == null) Debug.Log("null");
        

    }

    void CreateGrid(int _row, int _cellInRow)
    {
        for (int row = 0; row < _row; row++)
        {
            GameObject newRow = Instantiate(rowPrefab, gridTransform);
            for (int col = 0; col < _cellInRow; col++)
            {
                Instantiate(cellPrefab, newRow.transform);
            }
        }
    }
    public void RemoveDiamondToGrid(Vector3 _diamondIndex)
    {
        cellsGrid[(int)_diamondIndex.x, (int)_diamondIndex.y] = null;
    }
    public void AddDiamondToGrid(GameObject _newDiamond, Vector3 _diamondIndex)
    {
        cellsGrid[(int)_diamondIndex.x, (int)_diamondIndex.y] = _newDiamond;

        CheckDiamondAround(_newDiamond, _diamondIndex);

    }

    private void CheckDiamondAround(GameObject _newDiamond, Vector3 _diamondIndex)
    {
        if (CheckNearDiamond(_newDiamond, new Vector3(_diamondIndex.x - 1, _diamondIndex.y)))
        {
            DisplayManager.instance.IncreaseCoin();
            Destroy(Instantiate(coinPrefabs, coinSpawnPos, Quaternion.identity, transform), 1f);
        }
        else if (CheckNearDiamond(_newDiamond, new Vector3(_diamondIndex.x + 1, _diamondIndex.y)))
        {
            DisplayManager.instance.IncreaseCoin();
            Destroy(Instantiate(coinPrefabs, coinSpawnPos, Quaternion.identity, transform), 1f);

        }
        else if (CheckNearDiamond(_newDiamond, new Vector3(_diamondIndex.x, _diamondIndex.y - 1)))
        {
            DisplayManager.instance.IncreaseCoin();
            Destroy(Instantiate(coinPrefabs, coinSpawnPos, Quaternion.identity, transform), 1f);

        }
        else if (CheckNearDiamond(_newDiamond, new Vector3(_diamondIndex.x, _diamondIndex.y + 1)))
        {
            DisplayManager.instance.IncreaseCoin();
            Destroy(Instantiate(coinPrefabs, coinSpawnPos, Quaternion.identity, transform), 1f);
        }
    }

    public bool CheckNearDiamond(GameObject _diamondCheck, Vector3 _cellIndexCheck)
    {
        int rowIndex = (int)_cellIndexCheck.x;
        int ColIndex = (int)_cellIndexCheck.y;
        if(rowIndex < 0 || ColIndex < 0 || rowIndex>= rows || ColIndex>=columns) { return false; }
        
        GameObject nearDiamond = cellsGrid[rowIndex, ColIndex];
        //Debug.Log(_cellIndexCheck.x + "   " + _cellIndexCheck.y);
        if (nearDiamond != null)
        {
            DimomdDrag diamondCheckScript = _diamondCheck.GetComponent<DimomdDrag>();
            DimomdDrag diamondNearScript = nearDiamond.GetComponent<DimomdDrag>();
            if (diamondCheckScript.diamondInfor.name == diamondNearScript.diamondInfor.name)
            {
                coinSpawnPos =( diamondCheckScript.GetRectTransform().position + diamondNearScript.GetRectTransform().position )/ 2;

                RemoveDiamondToGrid(diamondCheckScript.jewelPosInGrid);
                RemoveDiamondToGrid(diamondNearScript.jewelPosInGrid);

                Destroy(nearDiamond,0.2f );
                Destroy(_diamondCheck,0.2f);

                return true;
            }
        }

        return false;
    }
}
