namespace женя;

public class Edge
{
    public Vertex V1;
    public Vertex V2;
    public int Weight;

    public Edge(int weight, Vertex v1, Vertex v2)
    {
        V1 = v1;
        V2 = v2;
        Weight = weight;
    }
}