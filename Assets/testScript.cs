using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class testScript : MonoBehaviour
{
    Dictionary<int, Vector3> dirArray = new Dictionary<int, Vector3>();   

    private float waitTime = 1f; // за какое время перевестить объект
    private float pauseTime = 5f; // пауза между движениями

    void Start()
    {
        dirArray.Add(1, Vector3.down);
        dirArray.Add(4, Vector3.right);
        dirArray.Add(2, Vector3.up);

        StartCoroutine(ChangeDirectionCoroutines());
    }

    IEnumerator MoveDirection(Vector3 target, int moveCount)
    {                    
        float elapsedTime = 0;        
        while (elapsedTime < waitTime * moveCount)
        {            
            transform.Translate(target * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }        
    }

    IEnumerator ChangeDirectionCoroutines()
    {
        int count = 0;
        while (true)
        {
            var item = dirArray.ElementAt(count);
            int moveCount = item.Key;
            Vector3 target = item.Value;

            StartCoroutine(MoveDirection(target, moveCount));
            count++;
            if (count == dirArray.Count) count = 0;
            yield return new WaitForSeconds(pauseTime);
        }
    }
}
