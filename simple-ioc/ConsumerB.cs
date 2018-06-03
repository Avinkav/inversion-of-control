using System;

namespace dotnet_c_sharp
{
    public class ConsumerB
    {
        private readonly MathService _mathService;

        public ConsumerB(MathService mathService){
            _mathService = mathService;
        }

        public void Subtract(int v)
        {
            _mathService.Add(-1 * v);
        }
    }

}