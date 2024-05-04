using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    [SerializeField] private int _xLength, _yLength;
    [SerializeField] private int _tileCount;
    [SerializeField] private int _tileOffset;
    [SerializeField] private Image _bg;
    [SerializeField] private Image _tilePrefab;
    
    private RectTransform _bgRect;
    private RectTransform _tileRect;
    
    private void Start()
    {
        _bgRect = _bg.GetComponent<RectTransform>();
        _tileRect = _tilePrefab.GetComponent<RectTransform>();
        ResizeBackground();
        ResizeTiles();
        SpawnTiles();
    }
    
    private void ResizeBackground()
    {
        if (_bgRect != null)
        {
            Vector2 rectSize = new Vector2(_bgRect.rect.width,_bgRect.rect.height);
            _bgRect.sizeDelta = new Vector2(rectSize.x * _xLength, rectSize.y * _yLength);
        }
    }
    private void ResizeTiles()
    {
        var totalRectArea = _xLength * _yLength;
        var eachTileArea = totalRectArea / _tileCount;
        var tileEdge = Mathf.Sqrt(eachTileArea);
        var tileInitial = _tileRect.rect.width;
        _tileRect.sizeDelta = new Vector2(tileEdge * tileInitial , tileEdge * tileInitial);
    }

    private void SpawnTiles()
    {
        var initialSpawnPosition = RectTransformUtility.WorldToScreenPoint(null, GetTopLeftOfBg());
        var xCount = _bgRect.rect.width / _tileRect.rect.width;
        var yCount = _bgRect.rect.height / _tileRect.rect.height;
        
        var edgeSize = _tileRect.rect.width;
        for (int i = 1; i <= yCount; i++)
        {
            for (int j=1; j <= xCount; j++)
            {
                var xStepOffset = j * edgeSize;
                var yStepOffset =  i * edgeSize;
                var imageInstance = Instantiate(_tilePrefab, new Vector2((initialSpawnPosition.x + xStepOffset)  ,(initialSpawnPosition.y - yStepOffset) ) , Quaternion.identity, transform);
                imageInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
                ApplyTileOffset(imageInstance.GetComponent<RectTransform>());
            }
        }
    }

    private void ApplyTileOffset(RectTransform tileRect)
    {
        var tileInitial = tileRect.rect.width;
        var offsetFactor = tileInitial - _tileOffset;
        tileRect.sizeDelta = new Vector2(  offsetFactor, offsetFactor);
    }
    


    Vector2 GetTopLeftOfBg() => new Vector2(_bgRect.rect.xMin + _tileRect.rect.x, _bgRect.rect.yMax - _tileRect.rect.y);
}
