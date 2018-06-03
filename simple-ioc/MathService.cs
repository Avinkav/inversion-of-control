using System;

namespace dotnet_c_sharp
{
    public class MathService
    {
        private readonly BeautyService _beautyService;
        private int tracker;

        public MathService(BeautyService beautyService)
        {
            _beautyService = beautyService;
            tracker = 0;
        }

        public void Add(int v)
        {
            tracker += v;
            _beautyService.BeautifyStart();
            Console.WriteLine($"Math service is adding {v} to sum: {tracker} ");
            _beautyService.BeautifyEnd();
        }

    }

}