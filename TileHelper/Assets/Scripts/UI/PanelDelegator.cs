using Data;
using Tile;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PanelDelegator : MonoBehaviour
    {
        [SerializeField] private TMP_InputField seamSizeField;
        [SerializeField] private TMP_InputField angleValueField;
        [SerializeField] private TMP_InputField biasValueField;
        [SerializeField] private TMP_Text areaText;
        [SerializeField] private TilePlacer tilePlacer;

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
            float.TryParse(seamSizeField.text, out var result);
            TileProperties.SeamSize = result/100;
        }

        private void OnAngleValueChanged()
        {
            float.TryParse(angleValueField.text, out var result);
            TileProperties.AngleValue = result;
        }

        private void OnBiasValueChanged()
        {
            float.TryParse(biasValueField.text, out var result);
            TileProperties.BiasValue = result/100;
        }

        private void OnAreaCalculated(float area)
        {
            areaText.text = $"Area: {area}";
        }
    }
}
