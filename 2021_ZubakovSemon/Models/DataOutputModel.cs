using MathLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _2021_ZubakovSemon.Models
{
    public class DataOutputNewModel
    {
        private StenkaMathLibNew _stenka = new StenkaMathLibNew();

        private DataInputNewModel _DataInput = new DataInputNewModel();
        public StenkaMathLibNew StenkaData { get { return _stenka; } }

        public DataOutputNewModel()
        {

        }
        public DataOutputNewModel(DataInputNewModel DataInput):base()
        {
            _DataInput = DataInput;

            #region --- Передать исходные данные в экземпляр библиотеки

            _stenka.H0 = _DataInput.H0;
            _stenka.TempMat = _DataInput.TempMat;
            _stenka.T0Gasa = _DataInput.T0Gasa;
            _stenka.VoGas = _DataInput.VoGas;
            _stenka.asred = _DataInput.asred;
            _stenka.Cm = _DataInput.Cm;
            _stenka.Gm = _DataInput.Gm;
            _stenka.av = _DataInput.av;
            _stenka.D = _DataInput.D;

            #endregion --- Передать исходные данные в экземпляр библиотеки

            Raschet();
        }



        #region --- Получить расчетные показатели
        public double[] SloyM { get { return _stenka.SloyM; } }
        public double[] Y { get; set; }
        public double[] Exp1 { get; set; }
        public double[] MExp1 { get; set; }

        [JsonPropertyName("_Vr")]
        public double[] Vr { get; set; }
        [JsonPropertyName("vr")]
        public double[] v_r { get; set; }
        [JsonPropertyName("_Tr")]
        public double[] Tr { get; set; }
        [JsonPropertyName("tr")]
        public double[] t_r { get; set; }
        public double[] Trasnost { get; set; }

        public void Raschet()
        {
            _stenka.Raschet();

            Y = _stenka.Y;
            Exp1 = _stenka.Exp1;
            MExp1 = _stenka.MExp1;
            Vr = _stenka.Vr;
            v_r = _stenka.v_r;
            Tr = _stenka.Tr;
            t_r = _stenka.t_r;
            Trasnost = _stenka.Trasnost;
        }

        #endregion --- Получить расчетные показатели

    }

    public class ArvicheOutputs
    {
        public DataInputNewModel[] DataSet { get; set; }
    }
}
