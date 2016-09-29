using System.Threading.Tasks;

namespace Auth0Demo.Abstractions
{
    public interface ILoginUiPresenter
    {
        Task PresentLoginUi(object page);
    }
}
