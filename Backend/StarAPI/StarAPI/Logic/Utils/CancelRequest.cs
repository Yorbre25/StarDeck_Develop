namespace StarAPI.Logic.Utils
{
    public sealed class CancelRequest
    {
        private static readonly CancelRequest instance = new CancelRequest();
        public bool terminate = false;
        public bool start = false;

        static CancelRequest()
        {
        }

        private CancelRequest()
        {
        }

        public static CancelRequest Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
