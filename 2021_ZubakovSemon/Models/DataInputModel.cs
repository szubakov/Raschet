using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_ZubakovSemon.Models
{
    public class DataInputNewModel
    {
        [LiteDB.BsonId(true)]
        public int ID { get; set; } = 0;

        public double[] SloyM { get; set; }

        /// <summary>
        /// Высота слоя H0, м
        /// </summary> 
        public double H0 { get; set; }

        /// <summary>
        /// Начальная температура материала t', 0С
        /// </summary> 
        /// 
        public double TempMat { get; set; }

        /// <summary>
        /// Начальная температура газа t', 0С
        /// </summary> 
        /// 
        public double T0Gasa { get; set; }

        /// <summary>
        /// Скорость газа на свободное сечение шахты Wг, м/с
        /// </summary> 
        public double VoGas { get; set; }

        /// <summary>
        /// Средняя теплоемкость газа Cг, кДж/(м3 • К).
        /// </summary> 
        public double asred { get; set; }

        /// <summary>
        ///  Расход материалов,Cм кг/с
        /// </summary> 
        public double Cm { get; set; }

        /// <summary>
        ///  Теплоемкость материалов Gм, кДж/(кг • К)
        /// </summary> 
        public double Gm { get; set; }

        /// <summary>
        /// Объемный коэффициент теплоотдачи av, Вт/(м3 • К).
        /// </summary> 
        public double av { get; set; }

        /// <summary>
        /// Диаметр аппарата, м 
        /// </summary>
        public double D { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public bool IsSaved { get; set; }
        public string ActionSave { get; set; }

    }
}
