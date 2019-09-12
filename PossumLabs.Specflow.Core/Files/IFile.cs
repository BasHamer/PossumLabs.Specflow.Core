using PossumLabs.Specflow.Core.Variables;
using System.IO;

namespace PossumLabs.Specflow.Core.Files
{
    public interface IFile: IEntity
    {
        Stream Stream { get; }
    }
}