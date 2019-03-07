using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebScope
{
    public class Api
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmds"></param>
        /// <param name="vals"></param>
        /// <returns></returns>
        public static byte[] PackMsg(float[] cmds, float[] vals)
        {
            int dataSize = sizeof(float);
            byte[] buff = new byte[(cmds.Count() + vals.Count()) * dataSize];

            for (int i = 0; i < cmds.Count(); i++)
            {
                byte[] bytes = BitConverter.GetBytes(cmds[i]);
                Array.Copy(bytes, 0, buff, i * dataSize, dataSize);
            }

            for (int i = 0; i < vals.Count(); i++)
            {
                byte[] bytes = BitConverter.GetBytes(vals[i]);
                Array.Copy(bytes, 0, buff, (i + cmds.Count()) * dataSize, dataSize);
            }

            return buff;
        }


    }
}
