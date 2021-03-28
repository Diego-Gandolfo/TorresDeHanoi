using UnityEngine;

public class StackFILO : IStackFILO<GameObject>
{
    private GameObject[] stack;
    private int indexCount;

    public void Initialize()
    {
        indexCount = 0;
        stack = new GameObject[5];
    }

    public void Push(GameObject item)
    {
        stack[indexCount] = item;
        indexCount++;
    }

    public GameObject Pop()
    {
        var aux = stack[indexCount - 1];
        stack[indexCount - 1] = null;
        indexCount--;
        return aux;
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

    public GameObject[] GetStack()
    {
        return stack;
    }
}
