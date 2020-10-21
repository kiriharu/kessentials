using Vintagestory.API.Server;

namespace Kvsl.Utils
{
    public abstract class AbstractTimerEvent
    {
        public double Timer;
        public abstract void Run();
        protected AbstractTimerEvent(ICoreServerAPI api) {}
    }
}