using UnityEngine;

[ExecuteInEditMode]
public class NoiseTest : MonoBehaviour
{
    public Color color1;
    public Color color2;
    public int time;
    public float lucanarity;
    public float octaves;
    public enum NoiseType
    {
        ClassicPerlin,
        PeriodicPerlin,
        Simplex,
        SimplexNumericalGrad,
        SimplexAnalyticalGrad
    }

    [SerializeField]
    NoiseType _noiseType;

    [SerializeField]
    bool _is3D;

    [SerializeField]
    bool _isFractal;

    [SerializeField]
    Shader shader;

    Material _material;

    void Update()
    {
        if (_material == null)
        {
            _material = new Material(shader);
            _material.hideFlags = HideFlags.DontSave;
            GetComponent<Renderer>().material = _material;
        }
        _material.SetFloat("_x", color1.r);
        _material.SetFloat("_y", color1.g);
        _material.SetFloat("_z", color1.b);
        _material.SetFloat("_x2", color2.r);
        _material.SetFloat("_y2", color2.g);
        _material.SetFloat("_z2", color2.b);
        _material.SetInt("_b", time);
        _material.SetFloat("_lucanarity", lucanarity);
        _material.SetFloat("_octaves", octaves);
        _material.shaderKeywords = null;

        if (_noiseType == NoiseType.ClassicPerlin)
            _material.EnableKeyword("CNOISE");
        else if (_noiseType == NoiseType.PeriodicPerlin)
            _material.EnableKeyword("PNOISE");
        else if (_noiseType == NoiseType.Simplex)
            _material.EnableKeyword("SNOISE");
        else if (_noiseType == NoiseType.SimplexNumericalGrad)
            _material.EnableKeyword("SNOISE_NGRAD");
        else // SimplexAnalyticalGrad
            _material.EnableKeyword("SNOISE_AGRAD");

        if (_is3D)
            _material.EnableKeyword("THREED");

        if (_isFractal)
            _material.EnableKeyword("FRACTAL");
    }
}
