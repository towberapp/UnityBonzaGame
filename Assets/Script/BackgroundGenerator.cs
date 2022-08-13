using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class BackgroundGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject folderBackground;
        [SerializeField] private GameObject backCube;

        [SerializeField] private int xPole;
        [SerializeField] private int yPole;

        private void Start()
        {
            for (int y = 0; y < yPole; y++)
            {
                for (int x = 0; x < xPole; x++)
                {
                    GameObject cube = Instantiate(backCube, new Vector2(x, y), Quaternion.identity, folderBackground.transform);
                    Color col = cube.GetComponent<SpriteRenderer>().color;
                    col.a = Random.Range(0.1f, 0.4f);
                    cube.GetComponent<SpriteRenderer>().color = col;
                }
            }
        }
    }
}
