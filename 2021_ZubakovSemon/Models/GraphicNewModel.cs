using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_ZubakovSemon.Models
{
    public class GraphicNewModel
    {
        public string DataSet { get; set; }
        public string AdditionalData { get; set; }

        public string RasnostData { get; set; }

        public double Right { get; set; }
        public double Left { get; set; }

        public GraphicNewModel(MathLib.StenkaMathLibNew stenka)
        {
            string[] data = getCoord(stenka);

            DataSet = data[0];
            AdditionalData = data[1];
            RasnostData = data[2];
        }

        private string[] getCoord(MathLib.StenkaMathLibNew stenka)
        {
            string data = "[";
            string data1 = "{label: 'T', type: 'line', borderWidth: 3, borderColor: 'orange', data:[";
            string data2 = "[";
            for (int i = 0; i < stenka.SloyM.Length; i++)
            {
                data += "{" + $"x: {GetStringDouble(stenka.SloyM[i])}, y: {GetStringDouble(stenka.t_r[i])} " + "},";
                data1 += "{" + $"x: {GetStringDouble(stenka.SloyM[i])}, y: {GetStringDouble(stenka.Tr[i])} " + "},";
                data2 += "{" + $"x: {GetStringDouble(stenka.SloyM[i])}, y: {GetStringDouble(stenka.Trasnost[i])} " + "},";
            }

            data = data.Trim(',');
            data += "]";
            data2 = data2.Trim(',');
            data2 += "]";
            data1 = data1.Trim(',');
            data1 += "]}";

            return new string[] { data, data1, data2 };
        }

        private string GetStringDouble(double val)
        {
            return val.ToString().Replace(',', '.');
        }

    }
}
