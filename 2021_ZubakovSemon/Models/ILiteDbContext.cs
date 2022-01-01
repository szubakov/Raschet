namespace _2021_ZubakovSemon.Models
{
    public interface ILiteDbContext
    {
        DataInputNewModel[] GetRaschet(string email);
        void SaveRaschet(DataInputNewModel[] data);
        DataInputNewModel GetRaschet(int ID);

    }
}