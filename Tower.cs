using UnityEngine;

public class Tower : MonoBehaviour
{
    protected Node nodeBuiltOn;
    protected int level = 0;
    protected TowerCollection towerCollection;

    public void SetNode(Node node)
    {
        this.nodeBuiltOn = node;
        nodeBuiltOn.SetBuilt(true);
    }

    public Node GetNode()
    {
        return nodeBuiltOn;
    }
}
