using UnityEngine;

public class StackFILO : IStackFILO<GameObject>
{
    private GameObject[] stack;
    private int indexCount;

    public void Initialize(int amountPieces)
    {
        indexCount = 0;
        stack = new GameObject[amountPieces];
    }

    public void Push(GameObject item)
    {
        stack[indexCount] = item;
        indexCount++;
    }

    public GameObject Pop()
    {
        if (!IsEmpty())
        {
            indexCount--;
            var aux = stack[indexCount];
            stack[indexCount] = null;
            return aux;
        }
        else return null;
    }

    public GameObject Peek()
    {
        if (!IsEmpty()) return stack[indexCount - 1];
        else return null;
    }

    public bool IsEmpty()
    {
        return indexCount == 0;
    }

    public int GetIndexCount()
    {
        return indexCount;
    }
}
