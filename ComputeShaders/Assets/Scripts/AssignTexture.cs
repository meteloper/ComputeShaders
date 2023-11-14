using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FisrtLesson
{
    public class AssignTexture : MonoBehaviour
    {
        public ComputeShader shader;
        public int texResolution = 256;

        Renderer rend;
        RenderTexture outputTexture;
        int kernelHandle;

        // Start is called before the first frame update
        void Start()
        {
            outputTexture = new RenderTexture(texResolution, texResolution, 0);
            outputTexture.enableRandomWrite = true;
            outputTexture.Create();
            outputTexture.filterMode = FilterMode.Point;
            rend = GetComponent<Renderer>();
            rend.enabled = true;
            InitShader();
        }

        private void InitShader()
        {
            kernelHandle = shader.FindKernel("CSMain");
            shader.SetTexture(kernelHandle, "Result", outputTexture);

            rend.material.SetTexture("_MainTex", outputTexture);
            DispatchShader(texResolution/16, texResolution/16);
        }

        private void DispatchShader(int x, int y)
        {
            shader.Dispatch(kernelHandle, x, y, 1);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                DispatchShader(texResolution/8, texResolution/8);
            }
        }



    }
}

