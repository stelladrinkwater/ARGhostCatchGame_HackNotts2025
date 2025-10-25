using System.Threading.Tasks;
using Google.XR.ARCoreExtensions;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARAnchorManager))]
public class AnchorCreator : MonoBehaviour
{
    ARAnchorManager anchorManager;

    [SerializeField]
    public bool makeAnchor = false;

    [SerializeField]
    public ARAnchor anchorToHost;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anchorManager = GetComponent<ARAnchorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (makeAnchor)
        {
            // Create a new anchor at the origin with no rotation
            anchorManager.HostCloudAnchorAsync(anchorToHost, 1);
            
            // Reset the flag
            makeAnchor = false;
        }
    }
}
