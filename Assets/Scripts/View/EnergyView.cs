using Dataers;
using TMPro;
using UnityEngine;

namespace View
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI textMesh;
        
        private void Update()
        {
            textMesh.text = PlayerEnergy.Energy.ToString();
        }
    }
}
