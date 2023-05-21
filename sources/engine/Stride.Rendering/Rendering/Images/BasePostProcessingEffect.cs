using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core;
using Stride.Graphics;
using Stride.Rendering.Images;

namespace Stride.Rendering.Rendering.Images
{

    [DataContract]
    public abstract class BasePostProcessingEffect : ImageEffectShader
    {
        public BasePostProcessingEffect(string effectName = null, bool delaySetRenderTargets = false) : base(effectName, delaySetRenderTargets)
        {

        }

        public abstract bool IsVaild(Texture[] inputs, Texture output);
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="inputs">
        /// 0:ColorTexture
        /// 1:depthStencil
        /// 2:normal
        /// 3:specularRoughness
        /// 4,5:reflection
        /// 6:velocity
        /// </param>
        /// <param name="output"></param>
        public virtual void Draw(RenderDrawContext context, Texture[] inputs, Texture output)
        {
            bool isVaild = IsVaild(inputs, output);
            if(isVaild)
            {
                var len = inputs?.Length ?? 0;
                for (int i = 0; i < len; i++)
                {
                    var input = inputs[i];
                    if (input != null) SetInput(i, input);
                }
                SetOutput(output);
                Draw(context);
            }
        }
    }
}
