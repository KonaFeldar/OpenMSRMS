using System.Drawing;

namespace StoreOperations_SDK
{
    public interface IAppExtension
    {
        string ExtensionName { get; }
        string ExtensionDescription { get; }
        Image ButtonIcon { get; }

        void Run();
    }
}