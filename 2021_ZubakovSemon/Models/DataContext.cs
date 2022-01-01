using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_ZubakovSemon.Models
{
    public class LiteDbContext: ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext(string options)
        {
            Database = new LiteDatabase(options);
        }

        public DataInputNewModel[] GetRaschet(string email)
        {
           return Database.GetCollection<DataInputNewModel>().Find(c => c.Email == email).ToArray();
        }

        public DataInputNewModel GetRaschet(int ID)
        {
            return Database.GetCollection<DataInputNewModel>().Find(c => c.ID == ID).FirstOrDefault();
        }

        public void SaveRaschet(DataInputNewModel[] data)
        {
            foreach (var item in data)
            {
                Database.GetCollection<DataInputNewModel>().Insert(item);
            }
        }
    }
}
