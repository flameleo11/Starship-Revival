using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{

    public GameObject starprefab;
    public GameObject nebulaprefab;
    public GameObject starparent;
    public GameObject nebulaparent;
    public int numofnebulas;
    public int numofneededstars;
    public List<Color> starcolors;
    public List<GameObject> starlist;
    public List<GameObject> nebulalist;
    public int startingstars;
    public int scale;
    public int octaves;
    public float persistance;
    public float lucanarity;
    public Vector2 offset;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(nebulas());
        StartCoroutine(spawnstars());
    }
    public IEnumerator spawnstars()
    {
        for (int i = 0; i < startingstars; i++)
        {
            var temp = Instantiate(starprefab);
            temp.transform.SetParent(starparent.transform);
            temp.GetComponent<Image>().color = starcolors[Random.Range(0, starcolors.Count - 1)];
            Vector2 xrange = new Vector2(GetComponent<Canvas>().pixelRect.xMin, GetComponent<Canvas>().pixelRect.xMax);
            Vector2 yrange = new Vector2(GetComponent<Canvas>().pixelRect.yMin, GetComponent<Canvas>().pixelRect.yMax);

            temp.transform.position = new Vector3(Random.Range(xrange.x, xrange.y), Random.Range(yrange.x, yrange.y));
            starlist.Add(temp);
        }
        while (true)
        {
            if (starlist.Count < numofneededstars)
            {
                yield return new WaitForSeconds(Random.Range(0, .5f));
                var temp = Instantiate(starprefab);
                temp.transform.SetParent(starparent.transform);
                temp.GetComponent<Image>().color = starcolors[Random.Range(0, starcolors.Count - 1)];
                Vector2 xrange = new Vector2(GetComponent<Canvas>().pixelRect.xMin, GetComponent<Canvas>().pixelRect.xMax);
                temp.transform.position = new Vector3(Random.Range(xrange.x, xrange.y), GetComponent<Canvas>().pixelRect.yMax + 5);
                starlist.Add(temp);
            }
            yield return new WaitUntil(() => starlist.Count < numofneededstars);
        }
    }

    public IEnumerator nebulas()
    {
        //GameObject nebulaprefab = Instantiate(nebulaprefab);
        while (true)
        {
            if (nebulalist.Count < numofnebulas)
            {
                GameObject temp2 = Instantiate(nebulaprefab);

                temp2.transform.SetParent(nebulaparent.transform);
                temp2.transform.localPosition = new Vector3(0, 0, 0);
                Sprite temp = Sprite.Create(new Texture2D((int)temp2.GetComponent<Image>().GetPixelAdjustedRect().width, (int)temp2.GetComponent<Image>().GetPixelAdjustedRect().height), new Rect(0, 0, (int)temp2.GetComponent<Image>().GetPixelAdjustedRect().width, (int)temp2.GetComponent<Image>().GetPixelAdjustedRect().height), new Vector2(.5f, .5f));
                Color[] pix = new Color[temp.texture.width * temp.texture.height];

                float[,] noiseMap = Noise.GenerateNoiseMap(temp.texture.width, temp.texture.height, Random.seed, scale, octaves, persistance, lucanarity, offset);
                Debug.Log(noiseMap[0, 0]);
                Debug.Log(noiseMap[0, 3]);
                Debug.Log(noiseMap[0, 2]);
                Debug.Log(noiseMap[0, 1]);
                int width = noiseMap.GetLength(0);
                int height = noiseMap.GetLength(1);

                Texture2D texture = new Texture2D(width, height);

                Color[] colourMap = new Color[width * height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        colourMap[y * width + x] = Color.Lerp(Color.black, Color.cyan, noiseMap[x, y]);
                    }
                }
                nebulalist.Add(temp2);
                temp.texture.SetPixels(colourMap);
                temp.texture.Apply();
                temp2.GetComponent<Image>().sprite = temp;
            }
            yield return new WaitUntil(() => numofnebulas <= nebulalist.Count);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
