using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public int blockAmount;
    public float blockMinWidth, blockMaxWidth;
    public int dangerZoneAmount;
    [Space(20)]
    public bool setRandomRotation;
    [SerializeField] private GameObject blockPrefab;

    [SerializeField] private GameObject dangerPrefab;

    private void Start()
    {
        SpawnBlocks();
        SpawnDangerZones();
    }

    private void SpawnDangerZones()
    {
        for (int i = 0; i < dangerZoneAmount; i++)
        {
            Vector3 randPosition = new Vector3(Random.Range(-5, 17),
                                        1f, Random.Range(-5, 10));

            Instantiate(dangerPrefab, randPosition, Quaternion.identity);
        }
    }

    private void SpawnBlocks()
    {
        for (int i = 0; i < blockAmount; i++)
        {
            //Random position for block spawn
            Vector3 randPosition = new Vector3(Random.Range(-17, 17),
                                        1f, Random.Range(-10, 10));

            GameObject block = Instantiate(blockPrefab, randPosition, Quaternion.identity);
            //Set random X scale for block
            block.transform.localScale = new Vector3(Random.Range(blockMinWidth, blockMaxWidth),
                                            1f,
                                            1f);
            //Set random Y rotation for block
            if (setRandomRotation)
            {
                var euler = block.transform.eulerAngles;
                euler.y = Random.Range(0f, 360f);
                block.transform.eulerAngles = euler;
            }
        }
    }
}
