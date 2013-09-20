
namespace JobLogger
{
    /* We want to respect the DependencyInversion principle, so we use an abstract layer (this interface) 
    between the high level class JobLogger and the different low level classes that implement this interface.*/
    interface ILogger
    {
        void Log(LogItem logItem);
    }
}
