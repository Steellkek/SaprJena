using System.Xml;

namespace женя;

public class LocalFile
{
    private string FileWay = "File.xml";
    private string ResultWay = "Result.xml";

    public List<List<int>> ReadGraph()
    {
        List<List<int>> matrix = new();
        int length = 1;
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(FileWay);
        var xRoot = xDoc.SelectSingleNode("root/graph");
        if (xRoot != null)
        {
            // обходим все дочерние узлы элемента user
            foreach (XmlNode childnode in xRoot.ChildNodes)
            {
                if (childnode.Name == "n")
                {
                    length = Int32.Parse(childnode.InnerText);
                }

                if (childnode.Name == "matrix")
                {
                    matrix = childnode.InnerText
                        .Split(Array.Empty<string>(), StringSplitOptions.RemoveEmptyEntries)
                        .Select((s, i) => new {N = int.Parse(s), I = i})
                        .GroupBy(at => at.I / length, at => at.N, (k, g) => g.ToList())
                        .ToList();
                    ;
                }
            }
        }

        return matrix;
    }

    public int ReadRadius()
    {
        int Radius = new int();
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(FileWay);
        Radius = Int32.Parse(xDoc.SelectSingleNode("root/Radius").InnerText);
        return Radius;
    }
    
    public void WriteResult(int radius, Vertex[] bestmassive, Graph graph)
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(ResultWay);
        var xRoot = xDoc.SelectSingleNode("root/Result");
        if (xRoot != null)
        {
            foreach (XmlNode childnode in xRoot.ChildNodes)
            {
                if (childnode.Name=="Radius")
                {
                    childnode.InnerText = radius.ToString();
                }

                if (childnode.Name=="area")
                {
                    string x = "";
                    foreach (var vertex in bestmassive)
                    {
                        x += vertex.Number-1 + " ";
                    }

                    childnode.InnerText = x;
                }
                
                if (childnode.Name=="d")
                {
                    childnode.InnerText = bestmassive.Min(v => v.d).ToString();
                }
                
                if (childnode.Name=="Matrix")
                {
                    childnode.InnerText = graph.WriteMatrix();
                }
            }
        }
        xDoc.Save("Result.xml");
    }
}