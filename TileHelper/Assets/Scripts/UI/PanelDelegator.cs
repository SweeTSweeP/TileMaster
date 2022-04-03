using Tile;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PanelDelegator : MonoBehaviour
    {
        private const int OneMeter = 100;
        
        [SerializeField] private TMP_InputField seamSizeField;
        [SerializeField] private TMP_InputField angleValueField;
        [SerializeField] private TMP_InputField biasValueField;
        [SerializeField] private TMP_Text areaText;

        private ITilePlacer tilePlacer;

        private float seamSize;
        private float angleValue;
        private float biasValue;

        [Inject]
        public void Construct(ITilePlacer tilePlacer)
        {
            this.tilePlacer = tilePlacer;
        }

        private void Start()
        {
            seamSizeField.onValueChanged.AddListener(delegate { OnSeamValueChanged(); });
            angleValueField.onValueChanged.AddListener(delegate { OnAngleValueChanged(); });
            biasValueField.onValueChanged.AddListener(delegate { OnBiasValueChanged(); });
            tilePlacer.AreaCalculated += OnAreaCalculated;
        }

        private void OnDestroy()
        {
            seamSizeField.onValueChanged.RemoveAllListeners();
            angleValueField.onValueChanged.RemoveAllListeners();
            biasValueField.onValueChanged.RemoveAllListeners();
        }

        private void OnSeamValueChanged()
        {
            if (float.TryParse(seamSizeField.text, out var result)) seamSize = result / OneMeter;
            tilePlacer.PlaceWallTiles(seamSize, angleValue, biasValue);
        }

        private void OnAngleValueChanged()
        {
            if (float.TryParse(angleValueField.text, out var result)) angleValue = result;
            tilePlacer.PlaceWallTiles(seamSize, angleValue, biasValue);
        }

        private void OnBiasValueChanged()
        {
            if (float.TryParse(biasValueField.text, out var result)) biasValue = result / OneMeter;
            tilePlacer.PlaceWallTiles(seamSize, angleValue, biasValue);
        }

        private void OnAreaCalculated(float area)
        {
            areaText.text = $"Area: {area/OneMeter}";
        }
    }
}
