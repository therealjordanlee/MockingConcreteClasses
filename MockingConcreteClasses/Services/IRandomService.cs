using MockingConcreteClasses.Models;
using System.Threading.Tasks;

namespace MockingConcreteClasses.Services
{
    public interface IRandomService
    {
        Task AddTestEntity(TestModel model);
    }
}
