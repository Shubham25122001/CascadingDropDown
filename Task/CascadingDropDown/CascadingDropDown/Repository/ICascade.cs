using CascadingDropDown.Models;

namespace CascadingDropDown.Repository
{
    public interface ICascade
    {
        List<Data> GetAll();
        Data GetById(int id);
        Data Add(Data Product);
        Data Update(Data Product);
        void Remove(int id);
    }
}
