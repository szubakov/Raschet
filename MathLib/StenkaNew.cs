using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MathLib
{
    public class StenkaMathLibNew
    {
        #region --- Исходные данные

        public StenkaMathLibNew()
        {
            SloyM = new double[] { 0, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0 };
        }

    private double _H0;
        /// <summary>
        /// Высота слоя H0, м
        /// </summary> 
        public double H0
        {
            get { return _H0; }
            set { _H0 = value; }
        }

        private double _TempMat;
        /// <summary>
        /// Начальная температура материала t', 0С
        /// </summary> 
        /// 
        public double TempMat
        {
            get { return _TempMat; }
            set { _TempMat = value; }
        }

        private double _T0Gasa;
        /// <summary>
        /// Начальная температура газа t', 0С
        /// </summary> 
        /// 
        public double T0Gasa
        {
            get { return _T0Gasa; }
            set { _T0Gasa = value; }
        }

        private double _VoGas;
        /// <summary>
        /// Скорость газа на свободное сечение шахты Wг, м/с
        /// </summary> 
        public double VoGas
        {
            get { return _VoGas; }
            set { _VoGas = value; }
        }

        private double _asred;
        /// <summary>
        /// Средняя теплоемкость газа Cг, кДж/(м3 • К).
        /// </summary> 
        public double asred
        {
            get { return _asred; }
            set { _asred = value; }
        }

        private double _Cm;
        /// <summary>
        ///  Расход материалов,Cм кг/с
        /// </summary> 
        public double Cm
        {
            get { return Math.Round(_Cm, 3); }
            set { _Cm = value; }
        }

        private double _Gm;
        /// <summary>
        ///  Теплоемкость материалов Gм, кДж/(кг • К)
        /// </summary> 
        public double Gm
        {
            get { return Math.Round(_Gm, 3); }
            set { _Gm = value; }
        }

        private double _av;
        /// <summary>
        /// Объемный коэффициент теплоотдачи av, Вт/(м3 • К).
        /// </summary> 
        public double av
        {
            get { return _av; }
            set { _av = value; }
        }


        private double _D;
        /// <summary>
        /// Диаметр аппарата, м 
        /// </summary>
        public double D
        {
            get { return _D; }
            set { _D = value; }
        }

        #endregion --- Исходные данные

        #region --- Расчетные показатели

        /// <summary>
        /// Площадь
        /// </summary>
        public double S()
        {
            double _S = Math.PI * Math.Pow(D/2,2);
            return Math.Round(_S, 3);
        }


        /// <summary>
        ///Отношение теплоемкостей потоков
        /// </summary>  
        public double m()
        {
            double _ОtnPotokov = (Cm * Gm) / (S() * VoGas * asred);
            return Math.Round(_ОtnPotokov, 3);
        }
        /// <summary>
        /// Полная относительная высота слоя 
        /// </summary>  
        public double Y0()
        {
            double _HOtnos = (av * S() * H0) / (1000 * asred * VoGas * S());
            return Math.Round(_HOtnos, 3);
        }


        public double[] SloyM { get; set; } = new double[] { 0, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0 };

        public double[] Y { get; set; }
        public double[] Exp1 { get; set; }
        public double[] MExp1 { get; set; }
     
        public double[] Vr { get; set; }
     
        public double[] v_r { get; set; }
       
        public double[] Tr { get; set; }
      
        public double[] t_r { get; set; }
        public double[] Trasnost { get; set; }

        public double[] getY()
        {
            Y = new double[SloyM.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                Y[i] = Math.Round(av * SloyM[i] / (1000 * asred * VoGas),3);
            }

            return Y;
        }

        public double MExpY0()
        {
            return Math.Round(1 - m() * Math.Exp(-(1 - m()) * Y0() / m()), 3);
        }

        public double[] GetExp1()
        {
            getY();

            Exp1 = new double[SloyM.Length];
            for (int i = 0; i < Exp1.Length; i++)
            {
                Exp1[i] = Math.Round(1 - Math.Exp((m() - 1) * Y[i] / m()),3);
            }

            return Exp1;
        }

        public double[] GetMExp1()
        {
            GetExp1();

            MExp1 = new double[SloyM.Length];
            for (int i = 0; i < Exp1.Length; i++)
            {
                MExp1[i] = Math.Round(1 - m() * Math.Exp((m() - 1) * Y[i] / m()), 3);
            }

            return MExp1;
        }

        public double[] GetVr()
        {
            GetMExp1();

            Vr = new double[SloyM.Length];
            for (int i = 0; i < Exp1.Length; i++)
            {
                Vr[i] = Math.Round(Exp1[i]/MExpY0(), 3);
            }

            return Vr;
        }

        public double[] Get_vr()
        {
            GetVr();

            v_r = new double[SloyM.Length];
            for (int i = 0; i < Exp1.Length; i++)
            {
                v_r[i] = Math.Round(MExp1[i] / MExpY0(), 3);
            }

            return v_r;
        }

        public double[] Get_tr()
        {
            Get_vr();

            t_r = new double[SloyM.Length];
            for (int i = 0; i < Exp1.Length; i++)
            {
                t_r[i] = Math.Round(TempMat + (T0Gasa - TempMat) * Vr[i], 3);
            }

            return t_r;
        }

        public double[] GetTr()
        {
            Get_tr();

            Tr = new double[SloyM.Length];
            for (int i = 0; i < Exp1.Length; i++)
            {
                Tr[i] = Math.Round(TempMat + (T0Gasa - TempMat) * v_r[i], 3);
            }

            return Tr;
        }

        public double[] GetTrasn()
        {
            GetTr();

            Trasnost = new double[SloyM.Length];
            for (int i = 0; i < Exp1.Length; i++)
            {
                Trasnost[i] = Math.Round(t_r[i]-Tr[i], 3);
            }

            return Trasnost;
        }

        public void Raschet()
        {
            GetTrasn();
        }



        #endregion --- Расчетные показатели

    }

}
