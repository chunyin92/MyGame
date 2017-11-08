using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Water : MonoBehaviour
{
    public enum WaterType
    {
        Both = 0,
        Reflection = 1,
        Refraction = 2
    }
    public WaterType Type = WaterType.Both;
    public bool DisablePixelLights = true;
    public int TextureSize = 512;
    public float ReflectClipPlaneOffset = 0.07f;
    public float RefractionAngle = 0;

    public LayerMask ReflectLayers = -1;
    public LayerMask RefractLayers = -1;

    private static Camera _reflectionCamera;
    private static Camera _refractionCamera;

    private RenderTexture _reflectionTexture;
    private RenderTexture _refractionTexture;
    private int _OldTextureSize = 0;

    private float _type = (float)WaterType.Both;

    private static bool s_InsideWater = false;

    // This is called when it's known that the object will be rendered by some
    // camera. We render reflections / refractions and do other updates here.
    // Because the script executes in edit mode, reflections for the scene view
    // camera will just work!
    public void OnWillRenderObject()
    {
        var rend = GetComponent<Renderer>();
        if (!enabled || !rend || !rend.sharedMaterial || !rend.enabled)
            return;

        Camera cam = Camera.current;
        if (!cam)
            return;

        // Safeguard from recursive reflections.        
        if (s_InsideWater)
            return;
        s_InsideWater = true;

        Material[] materials = rend.sharedMaterials;

        // Optionally disable pixel lights for reflection
        int oldPixelLightCount = QualitySettings.pixelLightCount;
        if (DisablePixelLights)
            QualitySettings.pixelLightCount = 0;

        if (Type == WaterType.Both || Type == WaterType.Reflection)
        {
            DrawReflectionRenderTexture(cam);
            foreach (Material mat in materials)
            {
                if (mat.HasProperty("_ReflectionTex") && _reflectionTexture!=null)
                    mat.SetTexture("_ReflectionTex", _reflectionTexture);
            }
        }

        if (Type == WaterType.Both || Type == WaterType.Refraction)
        {
            // FIXME: need or not???
            //this.gameObject.layer = 4;
            DrawRefractionRenderTexture(cam);
            foreach (Material mat in materials)
            {
                if (mat.HasProperty("_RefractionTex") && _refractionTexture != null)
                    mat.SetTexture("_RefractionTex", _refractionTexture);
            }
        }

        _type = (float)Type;
        //Matrix4x4 projmtx = UV_Tex2DProj2Tex2D(transform, cam);
        foreach (Material mat in materials)
        {
            //if (mat.HasProperty("_ProjMatrix"))
            //    mat.SetMatrix("_ProjMatrix", projmtx);
            if (mat.HasProperty("_Type"))
                mat.SetFloat("_Type", _type);
        }

        // Restore pixel light count
        if (DisablePixelLights)
            QualitySettings.pixelLightCount = oldPixelLightCount;

        s_InsideWater = false;
    }
    
    void DrawReflectionRenderTexture(Camera cam)
    {
        Vector3 pos = transform.position;
        Vector3 normal = transform.up;

        CreateObjects(cam, ref _reflectionTexture, ref _reflectionCamera);
        UpdateCameraModes(cam, _reflectionCamera);

        // Reflect camera around reflection plane
        float d = -Vector3.Dot(normal, pos) - ReflectClipPlaneOffset;
        Vector4 reflectionPlane = new Vector4(normal.x, normal.y, normal.z, d);

        Matrix4x4 reflection = CalculateReflectionMatrix(Matrix4x4.zero, reflectionPlane);

        Vector3 oldpos = cam.transform.position;
        Vector3 newpos = reflection.MultiplyPoint(oldpos);
        _reflectionCamera.worldToCameraMatrix = cam.worldToCameraMatrix * reflection;

        // Setup oblique projection matrix so that near plane is our reflection
        // plane. This way we clip everything below/above it for free.
        Vector4 clipPlane = CameraSpacePlane(_reflectionCamera, pos, normal, 1.0f, ReflectClipPlaneOffset);
        // FIXME check?
        Matrix4x4 projection = cam.CalculateObliqueMatrix(clipPlane);
        _reflectionCamera.projectionMatrix = projection;

        _reflectionCamera.cullingMask = ~(1 << 4) & ReflectLayers.value; // never render water layer
        _reflectionCamera.targetTexture = _reflectionTexture;

        GL.invertCulling = true;
        _reflectionCamera.transform.position = newpos;
        Vector3 euler = cam.transform.eulerAngles;
        _reflectionCamera.transform.eulerAngles = new Vector3(0, euler.y, euler.z);
        _reflectionCamera.Render();
        _reflectionCamera.transform.position = oldpos;
        GL.invertCulling = false;
    }

    void DrawRefractionRenderTexture(Camera cam)
    {
        CreateObjects(cam, ref _refractionTexture, ref _refractionCamera);
        UpdateCameraModes(cam, _refractionCamera);

        Vector3 pos = transform.position;
        Vector3 normal = transform.up;

        Matrix4x4 projection = cam.worldToCameraMatrix;
        projection *= Matrix4x4.Scale(new Vector3(1, Mathf.Clamp(1 - RefractionAngle, 0.001f, 1), 1));
        _refractionCamera.worldToCameraMatrix = projection;
        
        Vector4 clipPlane = CameraSpacePlane(_refractionCamera, pos, normal, -1.0f, 0f);

        _refractionCamera.projectionMatrix = cam.CalculateObliqueMatrix(clipPlane);

        _refractionCamera.cullingMask = ~(1 << 4) & RefractLayers.value; // never render water layer
        _refractionCamera.targetTexture = _refractionTexture;
        _refractionCamera.transform.position = cam.transform.position;
        _refractionCamera.transform.rotation = cam.transform.rotation;
        _refractionCamera.Render();
    }

    // Cleanup all the objects we possibly have created
    void OnDisable()
    {
        if (_reflectionTexture)
        {
            DestroyImmediate(_reflectionTexture);
            _reflectionTexture = null;
        }
        if (_reflectionCamera)
        {
            DestroyImmediate(_reflectionCamera.gameObject);
            _reflectionCamera = null;
        }

        if (_refractionTexture)
        {
            DestroyImmediate(_refractionTexture);
            _refractionTexture = null;
        }
        if (_refractionCamera)
        {
            DestroyImmediate(_refractionCamera.gameObject);
            _refractionCamera = null;
        }
    }

    private void UpdateCameraModes(Camera src, Camera dest)
    {
        if (dest == null)
            return;
        // set camera to clear the same way as current camera
        dest.clearFlags = src.clearFlags;
        dest.backgroundColor = src.backgroundColor;
        if (src.clearFlags == CameraClearFlags.Skybox)
        {
            Skybox sky = src.GetComponent(typeof(Skybox)) as Skybox;
            Skybox mysky = dest.GetComponent(typeof(Skybox)) as Skybox;
            if (!sky || !sky.material)
            {
                mysky.enabled = false;
            }
            else
            {
                mysky.enabled = true;
                mysky.material = sky.material;
            }
        }
        // update other values to match current camera.
        // even if we are supplying custom camera&projection matrices,
        // some of values are used elsewhere (e.g. skybox uses far plane)
        dest.farClipPlane = src.farClipPlane;
        dest.nearClipPlane = src.nearClipPlane;
        dest.orthographic = src.orthographic;
        dest.fieldOfView = src.fieldOfView;
        dest.aspect = src.aspect;
        dest.orthographicSize = src.orthographicSize;
    }

    // On-demand create any objects we need
    private void CreateObjects(Camera currentCamera, ref RenderTexture renderTex, ref Camera destCam)
    {
        if (!renderTex || _OldTextureSize != TextureSize)
        {
            if (renderTex)
                DestroyImmediate(renderTex);
            renderTex = new RenderTexture(TextureSize, TextureSize, 16);
            renderTex.name = "__RefRenderTexture" + GetInstanceID();
            renderTex.isPowerOfTwo = true;
            renderTex.hideFlags = HideFlags.DontSave;
            renderTex.antiAliasing = 4;
            renderTex.anisoLevel = 0;
            _OldTextureSize = TextureSize;
        }
        
        if (!destCam) // catch both not-in-dictionary and in-dictionary-but-deleted-GO
        {
            GameObject go = new GameObject("__RefCamera for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));
            destCam = go.GetComponent<Camera>();
            destCam.enabled = false;
            destCam.transform.position = transform.position;
            destCam.transform.rotation = transform.rotation;
            destCam.gameObject.AddComponent<FlareLayer>();
            go.hideFlags = HideFlags.HideAndDontSave;
        }
    }    

    // Given position/normal of the plane, calculates plane in camera space.
    private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign, float clipPlaneOffset)
    {
        Vector3 offsetPos = pos + normal * clipPlaneOffset;
        Matrix4x4 m = cam.worldToCameraMatrix;
        Vector3 cpos = m.MultiplyPoint(offsetPos);
        Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
        return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
    }

    // Calculates reflection matrix around the given plane
    private Matrix4x4 CalculateReflectionMatrix(Matrix4x4 reflectionMat, Vector4 plane)
    {
        reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
        reflectionMat.m01 = (-2F * plane[0] * plane[1]);
        reflectionMat.m02 = (-2F * plane[0] * plane[2]);
        reflectionMat.m03 = (-2F * plane[3] * plane[0]);

        reflectionMat.m10 = (-2F * plane[1] * plane[0]);
        reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
        reflectionMat.m12 = (-2F * plane[1] * plane[2]);
        reflectionMat.m13 = (-2F * plane[3] * plane[1]);

        reflectionMat.m20 = (-2F * plane[2] * plane[0]);
        reflectionMat.m21 = (-2F * plane[2] * plane[1]);
        reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
        reflectionMat.m23 = (-2F * plane[3] * plane[2]);

        reflectionMat.m30 = 0F;
        reflectionMat.m31 = 0F;
        reflectionMat.m32 = 0F;
        reflectionMat.m33 = 1F;

        return reflectionMat;
    }

    // Not used

    //// Extended sign: returns -1, 0 or 1 based on sign of a
    //private float getSign(float a)
    //{
    //    if (a > 0.0f) return 1.0f;
    //    if (a < 0.0f) return -1.0f;
    //    return 0.0f;
    //}

    //private Matrix4x4 UV_Tex2DProj2Tex2D(Transform transform, Camera cam)
    //{
    //    Matrix4x4 scaleOffset = Matrix4x4.TRS(
    //        new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
    //    Vector3 scale = transform.lossyScale;
    //    Matrix4x4 _ProjMatrix = transform.localToWorldMatrix * Matrix4x4.Scale(new Vector3(1.0f / scale.x, 1.0f / scale.y, 1.0f / scale.z));
    //    _ProjMatrix = scaleOffset * cam.projectionMatrix * cam.worldToCameraMatrix * _ProjMatrix;
    //    return _ProjMatrix;
    //}

    //for testing
    //void OnGUI ()
    //{
    //    if (_reflectionTexture != null)
    //        GUI.DrawTexture (new Rect (10, -400, 400, 800), _reflectionTexture, ScaleMode.ScaleToFit, false, 2.0f);

    //    if (_refractionTexture != null)
    //        GUI.DrawTexture (new Rect (1000, -400, 400, 800), _refractionTexture, ScaleMode.ScaleToFit, false, 2.0f);

    //}
}